Imports System.Windows.Forms

Public Class frm_createToolbar

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

  Private Sub frm_createToolbar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim path = AddInTree.GetTreeNode("/Workspace/Toolbars")
    For Each cod In path.Codons
      If cod.Name = "DefaultView" Then
        ComboBox1.Items.Add(cod.Id)
      End If
    Next
    ComboBox1.SelectedIndex = 0
    TextBox1.Focus() : TextBox1.SelectAll()

  End Sub
End Class
