Imports System.Windows.Forms

Public Class frm_selectCommand

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

  Sub readCommandList()
    lvwTbCmds.Items.Clear() : imlToolbarIcons.Images.Clear()
    Dim path = AddInTree.GetTreeNode("/Workspace/ToolbarCommands")
    Dim lvi As ListViewItem
    lvi = lvwTbCmds.Items.Add("-", "<Trennzeichen>", "")

    For Each cod In path.Codons
      lvi = lvwTbCmds.Items.Add(cod.Id, cod.Id, "")
      If Not String.IsNullOrEmpty(cod.Properties("icon")) Then
        imlToolbarIcons.Images.Add(cod.Id, ResourceLoader.GetImageCached(cod.Properties("icon")))
        lvi.ImageKey = cod.Id
      End If
      lvi.Tag = cod
    Next

  End Sub

  Private Sub frm_selectCommand_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    readCommandList()
  End Sub
End Class
