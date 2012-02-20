Public Class frmTB_rtfFilelist

  Public Overrides Function GetPersistString() As String
    Return tbTwajaxExplorer_ID
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    IDE.BeforeShowAddinWindow(Me.GetPersistString(), Me)
    MyBase.Show()
  End Sub



  Private Sub frmTB_rtfFilelist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    ToolStrip2.Show()

    iniNotesFileList(TreeView1)
  End Sub


  Private Sub tsb_FilelistNewfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_FilelistNewfile.Click
    ' isBUSY = True
    Dim nam = InputBox("Bitte gib den Name für die neue Notiz ein:", "Neue Notiz anlegen", "")
    If nam = "" Then Exit Sub 'isBUSY = False : Exit Sub

    If nam.StartsWith("note/") = False Then nam = "note/" + nam
    'If nam.EndsWith(".txt") = False Then nam = nam + ".txt"

    TwAjax.SaveFile("memo", nam, "Neue Notiz..." + vbNewLine + Now.ToLongDateString)

    iniNotesFileList(TreeView1)
    'isBUSY = False
  End Sub

  Private Sub tsb_FilelistRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_FilelistRefresh.Click
    iniNotesFileList(TreeView1)

  End Sub

  Private Sub TreeView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseUp
    Dim nod = TreeView1.GetNodeAt(e.Location)
    If nod Is Nothing Then Exit Sub

    If e.Button = Windows.Forms.MouseButtons.Right Then

      TreeView1.SelectedNode = nod
      If nod.Tag <> "" Then
        gotoRtfNote(nod)
      End If
    End If

    If nod.Tag = "" Then
      nod.Toggle()
    End If

  End Sub
  Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
    If e.Node.Tag <> "" Then
      gotoRtfNote(e.Node)
    End If
  End Sub

  Sub gotoRtfNote(ByVal nod As Windows.Forms.TreeNode)
    Dim filename As String = nod.Tag
    filename = Replace(filename, "note/", "twajax:/memo/note/")
    IDE.NavigateFile(filename)
  End Sub

  Private Sub tsb_FilelistRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_FilelistRename.Click
    If TreeView1.SelectedNode Is Nothing Then Exit Sub

    Dim oldName As String = TreeView1.SelectedNode.Tag
    Dim newName = InputBox("Datei umbenennen" + vbNewLine + "Alter Dateiname: " + oldName, , oldName)
    If newName = "" Then Exit Sub

    If newName.StartsWith("note/") = False Then newName = "note/" + newName
    'If newName.EndsWith(".txt") = False Then newName = newName + ".txt"

    Dim url = "http://teamwiki.de/twiki/vb_tool.php?m=rename"
    url += "&app=memo" + "&file=" + oldName + "&newfile=" + newName
    MsgBox(TwAjax.getUrlContent(url))

    iniNotesFileList(TreeView1)
  End Sub

  Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

  End Sub

End Class