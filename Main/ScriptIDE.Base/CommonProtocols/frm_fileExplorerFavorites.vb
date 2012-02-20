Public Class frm_fileExplorerFavorites
  Dim dd As New iGDragDropManager

  Private Sub frm_fileExplorerFavorites_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      Dim file = IO.File.ReadAllText(ParaService.SettingsFolder + "fileExplorer_bookmarks.txt")

      Igrid_put(IGrid1, file)

    Catch ex As Exception

    End Try
    dd.AllowMoveWithinOneGrid = True
    dd.AllowMove = True
    dd.Manage(IGrid1, True, True)
  End Sub

  Sub saveSettings()
    Dim cont As String
    Igrid_get(IGrid1, cont)

    IO.File.WriteAllText(ParaService.SettingsFolder + "fileExplorer_bookmarks.txt", cont)

    tbFileExplorer.refreshBookmarksMenu()
  End Sub

  Private Sub btnAddDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDir.Click
    Dim ir = IGrid1.Rows.Add
    ir.Cells(0).Value = IO.Path.GetFileName(tbFileExplorer.txtLocFolder.Text)
    ir.Cells(1).Value = tbFileExplorer.txtLocFolder.Text
    IGrid1.CurRow = ir
    ir.EnsureVisible()
    saveSettings()
  End Sub

  Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
    If IGrid1.CurRow IsNot Nothing Then
      IGrid1.Rows.RemoveAt(IGrid1.CurRow.Index)
      saveSettings()
    End If
  End Sub

  Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
    saveSettings()
    Me.Close()
  End Sub

  Private Sub IGrid1_AfterCommitEdit(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGAfterCommitEditEventArgs) Handles IGrid1.AfterCommitEdit
    saveSettings()
  End Sub
End Class