

Public Class prop_Debug
  Implements IPropertyPage




  Public Sub readProperties() Implements Core.IPropertyPage.readProperties
    AutoStart()
  End Sub

  Public Sub saveProperties() Implements Core.IPropertyPage.saveProperties


  End Sub


  Private Sub prop_Debug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub
  Sub AutoStart()
    Dim rootNode = AddInTree.GetTreeNode("").ChildNodes("")
    'Dim out As New StringBuilder()
    TreeView1.Nodes.Clear()
    recWalkTree(rootNode, "", TreeView1.Nodes)
    TreeView1.ExpandAll()
    'ZZ.setOutMonitor(out.Tostring)
  End Sub

  Sub recWalkTree(ByVal tn As AddInTreeNode, ByVal ind As String, ByVal tc As TreeNodeCollection)
    For Each subTn As KeyValuePair(Of String, AddInTreeNode) In tn.ChildNodes
      'out.AppendLine(ind + "[+] " + subTn.Key)
      Dim item = tc.Add(subTn.Key)
      item.Tag = subTn
      recWalkTree(subTn.Value, ind + "   ", item.Nodes)
    Next
    For Each cod As Codon In tn.Codons
      Dim item = tc.Add(cod.Id)
      item.Tag = cod
      'out.AppendLine(ind + "  | " + cod.Id)
    Next
  End Sub

  Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

  End Sub
End Class
