Public Class frm_editBookmark

  Private Sub frm_editBookmark_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub


  Private Sub btnEditbookmark_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditbookmark_save.Click
    Dim lnk As LinkLabel = DirectCast(Me.Tag, LinkLabel)
    lnk.Text = txtEditbookmark_text.Text
    lnk.Tag = txtEditbookmark_url.Text

    Me.Close()
  End Sub

  Private Sub btnEditbookmark_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditbookmark_close.Click
    Me.Close()
  End Sub

  Private Sub btnEditbookmark_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditbookmark_delete.Click
    Workbench.Instance.flpBookmarks.Controls.Remove(Me.Tag)
    Me.Close()
  End Sub

End Class