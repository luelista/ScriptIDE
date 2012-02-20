' <file>
'     <copyright see="prj:///doc/copyright.txt"/>
'     <license see="prj:///doc/license.txt"/>
'     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
'     <version>$Revision: 915 $</version>
' </file>

Imports System.Collections

''' <summary>
''' This doozer lazy-loads another doozer when it has to build an item.
''' It is used internally to wrap doozers specified in addins.
''' </summary>
Public Class LazyLoadDoozer
  Implements IDoozer
  Private addIn As AddinInstance
  Private m_name As String
  Private m_className As String

  Public ReadOnly Property Name() As String
    Get
      Return m_name
    End Get
  End Property

  Public ReadOnly Property ClassName() As String
    Get
      Return m_className
    End Get
  End Property

  Public Sub New(ByVal addIn As AddinInstance, ByVal properties As Properties)
    Me.addIn = addIn
    Me.m_name = properties("name")

    Me.m_className = properties("class")
  End Sub

  ''' <summary>
  ''' Gets if the doozer handles codon conditions on its own.
  ''' If this property return false, the item is excluded when the condition is not met.
  ''' </summary>
  Public ReadOnly Property HandleConditions() As Boolean Implements IDoozer.HandleConditions
    Get
      Dim doozer As IDoozer = DirectCast(addIn.CreateObject(m_className), IDoozer)
      If doozer Is Nothing Then
        Return False
      End If
      AddInTree.Doozers(m_name) = doozer
      Return doozer.HandleConditions
    End Get
  End Property

  Public Function BuildItem(ByVal caller As Object, ByVal codon As Codon, ByVal subItems As ArrayList) As Object Implements IDoozer.BuildItem
    Dim doozer As IDoozer = DirectCast(addIn.CreateObject(m_className), IDoozer)
    If doozer Is Nothing Then
      Return Nothing
    End If
    AddInTree.Doozers(m_name) = doozer
    Return doozer.BuildItem(caller, codon, subItems)
  End Function

  Public Overloads Overrides Function ToString() As String
    Return [String].Format("[LazyLoadDoozer: className = {0}, name = {1}]", m_className, m_name)
  End Function

End Class