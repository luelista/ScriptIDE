' <file>
'     <copyright see="prj:///doc/copyright.txt"/>
'     <license see="prj:///doc/license.txt"/>
'     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
'     <version>$Revision: 3287 $</version>
' </file>

Imports System.Collections
Imports System.Collections.Generic

''' <summary>
''' Represents an extension path in the <see cref="AddInTree"/>.
''' </summary>
Public NotInheritable Class AddInTreeNode
  Private m_childNodes As New Dictionary(Of String, AddInTreeNode)()
  Private m_codons As New List(Of Codon)()
  Private isSorted As Boolean = False

  ''' <summary>
  ''' A dictionary containing the child paths.
  ''' </summary>
  Public ReadOnly Property ChildNodes() As Dictionary(Of String, AddInTreeNode)
    Get
      Return m_childNodes
    End Get
  End Property

  ''' <summary>
  ''' A list of child <see cref="Codon"/>s.
  ''' </summary>
  Public ReadOnly Property Codons() As List(Of Codon)
    Get
      Return m_codons
    End Get
  End Property

  Public Function GetChildItem(ByVal childItemID As String) As Codon
    Return GetChildItem(childItemID, False)
  End Function

  Public Function GetChildItem(ByVal childItemID As String, ByVal recursive As Boolean) As Codon
    For Each codon As Codon In m_codons
      If codon.Id = childItemID Then
        Return codon
      End If
    Next
    If recursive And m_childNodes.Count > 0 Then
      For Each subPath In m_childNodes
        Return subPath.Value.GetChildItem(childItemID, True)
      Next
    End If
    Return Nothing
  End Function


  '
  '		public void BinarySerialize(BinaryWriter writer)
  '		{
  '			if (!isSorted) {
  '				(new SortCodons(this)).Execute();
  '				isSorted = true;
  '			}
  '			writer.Write((ushort)codons.Count);
  '			foreach (Codon codon in codons) {
  '				codon.BinarySerialize(writer);
  '			}
  '
  '			writer.Write((ushort)childNodes.Count);
  '			foreach (KeyValuePair<string, AddInTreeNode> child in childNodes) {
  '				writer.Write(AddInTree.GetNameOffset(child.Key));
  '				child.Value.BinarySerialize(writer);
  '			}
  '		}

  ''' <summary>
  ''' Supports sorting codons using InsertBefore/InsertAfter
  ''' </summary>
  Private NotInheritable Class TopologicalSort
    Private m_codons As List(Of Codon)
    Private visited As Boolean()
    Private sortedCodons As List(Of Codon)
    Private indexOfName As Dictionary(Of String, Integer)

    Public Sub New(ByVal codons As List(Of Codon))
      Me.m_codons = codons
      visited = New Boolean(codons.Count - 1) {}
      sortedCodons = New List(Of Codon)(codons.Count)
      indexOfName = New Dictionary(Of String, Integer)(codons.Count)
      ' initialize visited to false and fill the indexOfName dictionary
      For i As Integer = 0 To codons.Count - 1
        visited(i) = False
        indexOfName(codons(i).Id) = i
      Next
    End Sub

    Private Sub InsertEdges()
      ' add the InsertBefore to the corresponding InsertAfter
      For i As Integer = 0 To m_codons.Count - 1
        Dim before As String = m_codons(i).InsertBefore
        If before IsNot Nothing AndAlso before <> "" Then
          If indexOfName.ContainsKey(before) Then
            Dim after As String = m_codons(indexOfName(before)).InsertAfter
            If after Is Nothing OrElse after = "" Then
              m_codons(indexOfName(before)).InsertAfter = m_codons(i).Id
            Else
              m_codons(indexOfName(before)).InsertAfter = after & ","c & Convert.ToString(m_codons(i).Id)
            End If
          Else
            TT.Write("Codon (" + before + ") specified in the insertbefore of this codon does not exist:", m_codons(i).ToString, "warn")
          End If
        End If
      Next
    End Sub

    Public Function Execute() As List(Of Codon)
      InsertEdges()

      ' Visit all codons
      For i As Integer = 0 To m_codons.Count - 1
        Visit(i)
      Next
      Return sortedCodons
    End Function

    Private Sub Visit(ByVal codonIndex As Integer)
      If visited(codonIndex) Then
        Return
      End If
      Dim after As String() = m_codons(codonIndex).InsertAfter.Split(New Char() {","c})
      For Each s As String In after
        If s Is Nothing OrElse s.Length = 0 Then
          Continue For
        End If
        If indexOfName.ContainsKey(s) Then
          Visit(indexOfName(s))
        Else
          TT.Write("Codon (" + m_codons(codonIndex).InsertAfter + ") specified in the insertafter of this codon does not exist:", m_codons(codonIndex).ToString, "warn")
        End If
      Next
      sortedCodons.Add(m_codons(codonIndex))
      visited(codonIndex) = True
    End Sub
  End Class

  Public Sub EnsureSorted()
    If Not isSorted Then
      m_codons = (New TopologicalSort(m_codons)).Execute()
      isSorted = True
    End If
  End Sub

  ''' <summary>
  ''' Builds the child items in this path. Ensures that all items have the type T.
  ''' </summary>
  ''' <param name="caller">The owner used to create the objects.</param>
  Public Function BuildChildItems(Of T)(ByVal caller As Object) As List(Of T)
    Dim items As New List(Of T)(m_codons.Count)
    EnsureSorted()
    For Each codon As Codon In m_codons
      Dim subItems As ArrayList = Nothing
      If m_childNodes.ContainsKey(codon.Id) Then
        subItems = m_childNodes(codon.Id).BuildChildItems(caller)
      End If
      Dim result As Object = codon.BuildItem(caller, subItems)
      If result Is Nothing Then
        Continue For
      End If
      Dim [mod] As IBuildItemsModifier = TryCast(result, IBuildItemsModifier)
      If [mod] IsNot Nothing Then
        [mod].Apply(items)
      ElseIf TypeOf result Is T Then
        items.Add(DirectCast(result, T))
      Else
        Throw New InvalidCastException(("The AddInTreeNode <" + codon.Name & " id='") + codon.Id & "' returned an instance of " & result.[GetType]().FullName & " but the type " & GetType(T).FullName & " is expected.")
      End If
    Next
    Return items
  End Function

  ''' <summary>
  ''' Builds the child items in this path.
  ''' </summary>
  ''' <param name="caller">The owner used to create the objects.</param>
  Public Function BuildChildItems(ByVal caller As Object) As ArrayList
    Dim items As New ArrayList(m_codons.Count)
    EnsureSorted
    For Each codon As Codon In m_codons
      Dim subItems As ArrayList = Nothing
      If m_childNodes.ContainsKey(codon.Id) Then
        subItems = m_childNodes(codon.Id).BuildChildItems(caller)
      End If
      Dim result As Object = codon.BuildItem(caller, subItems)
      If result Is Nothing Then
        Continue For
      End If
      Dim [mod] As IBuildItemsModifier = TryCast(result, IBuildItemsModifier)
      If [mod] IsNot Nothing Then
        [mod].Apply(items)
      Else
        items.Add(result)
      End If
    Next
    Return items
  End Function

  ''' <summary>
  ''' Builds a specific child items in this path.
  ''' </summary>
  ''' <param name="childItemID">
  ''' The ID of the child item to build.
  ''' </param>
  ''' <param name="caller">The owner used to create the objects.</param>
  ''' <param name="subItems">The subitems to pass to the doozer</param>
  ''' <exception cref="TreePathNotFoundException">
  ''' Occurs when <paramref name="childItemID"/> does not exist in this path.
  ''' </exception>
  Public Function BuildChildItem(ByVal childItemID As String, ByVal caller As Object, ByVal subItems As ArrayList) As Object
    For Each codon As Codon In m_codons
      If codon.Id = childItemID Then
        Return codon.BuildItem(caller, subItems)
      End If
    Next
    Throw New IO.FileNotFoundException("Addin tree path not found", childItemID)
  End Function
End Class

