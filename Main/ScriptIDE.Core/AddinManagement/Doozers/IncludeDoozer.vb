' <file>
'     <copyright see="prj:///doc/copyright.txt"/>
'     <license see="prj:///doc/license.txt"/>
'     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
'     <version>$Revision: 1185 $</version>
' </file>

Imports System.Collections

	''' <summary>
	''' Includes one or multiple items from another location in the addin tree.
	''' You can use the attribute "item" (to include a single item) OR the
	''' attribute "path" (to include all items from the target path).
	''' </summary>
	''' <attribute name="item">
	''' When this attribute is used, the include doozer builds the item that is at the
	''' addin tree location specified by this attribute.
	''' </attribute>
	''' <attribute name="path">
	''' When this attribute is used, the include doozer builds all items inside the
	''' path addin tree location specified by this attribute and returns an
	''' <see cref="IBuildItemsModifier"/> which includes all items in the output list.
	''' </attribute>
	''' <usage>Everywhere</usage>
	''' <returns>
	''' Any object, depending on the included codon(s).
	''' </returns>
	Public Class IncludeDoozer
		Implements IDoozer
		''' <summary>
		''' Gets if the doozer handles codon conditions on its own.
		''' If this property return false, the item is excluded when the condition is not met.
		''' </summary>
    Public ReadOnly Property HandleConditions() As Boolean Implements IDoozer.HandleConditions
      Get
        Return False
      End Get
    End Property

    Public Function BuildItem(ByVal caller As Object, ByVal codon As Codon, ByVal subItems As ArrayList) As Object Implements IDoozer.BuildItem
      Dim item As String = codon.Properties("item")
      Dim path As String = codon.Properties("path")
      If item IsNot Nothing AndAlso item.Length > 0 Then
        ' include item
        Return AddInTree.BuildItem(item, caller)
      ElseIf path IsNot Nothing AndAlso path.Length > 0 Then
        ' include path (=multiple items)
        Return New IncludeReturnItem(caller, path)
      Else
        MsgBox("<Include> requires the attribute 'item' (to include one item) or the attribute 'path' (to include multiple items)")
        Return Nothing
      End If
    End Function

		Private Class IncludeReturnItem
			Implements IBuildItemsModifier
			Private path As String
			Private caller As Object

			Public Sub New(caller As Object, path As String)
				Me.caller = caller
				Me.path = path
			End Sub

      Public Sub Apply(ByVal items As IList) Implements IBuildItemsModifier.Apply
        Dim node As AddInTreeNode
        Try
          node = AddInTree.GetTreeNode(path)
          For Each o As Object In node.BuildChildItems(caller)
            items.Add(o)
          Next
        Catch generatedExceptionName As IO.FileNotFoundException
          MsgBox("IncludeDoozer: AddinTree-Path not found: " & path, MsgBoxStyle.Critical)
        End Try
      End Sub
		End Class
	End Class
