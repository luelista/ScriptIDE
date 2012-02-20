Public Class frmTB_ftpExplorer
  Dim FtpCutfileName As String = "", FtpCutfilePath As String = ""

  Public Overrides Function GetPersistString() As String
    Return tbPrefix + "FTPBrowser"
  End Function
  Shadows Sub Show()
    cls_IDEHelper.GetSingleton().BeforeShowAddinWindow(tbPrefix + "FTPBrowser", Me)
    MyBase.Show()
  End Sub


  Private Sub btnFtpUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFtpUp.Click, AufwaertsToolStripMenuItem.Click
    onLevelUp()
  End Sub

  Sub onLevelUp()
    txtFtpCurDir.Text = txtFtpCurDir.Text.Substring(0, txtFtpCurDir.TextLength - 1)
    txtFtpCurDir.Text = txtFtpCurDir.Text.Substring(0, txtFtpCurDir.Text.LastIndexOf("/") + 1)

    fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)

  End Sub

  Private Sub btnFtpRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ObersteEbeneToolStripMenuItem.Click
    fillFtpFilelist(activeFtpCon, "/")
  End Sub


  Sub ftpNavSelectedRow()
    If getCurRow() Is Nothing Then Exit Sub
    If getCurRow().Tag = "LEVELUP" Then
      onLevelUp()
    ElseIf getCurRow().Tag = "DIR" Then

      txtFtpCurDir.Text += getCurRow().Text + "/"
      fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)
    Else
      onNavigate("ftp:/" + activeFtpCon + txtFtpCurDir.Text + getCurRow().Text)

    End If
  End Sub

  Sub setCurRow(ByVal lvi As ListViewItem)
    While ListView1.SelectedItems.Count <> 0
      ListView1.SelectedItems(0).Selected = False
    End While
    If lvi IsNot Nothing Then lvi.Selected = True
  End Sub

  Private Sub ListView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles ListView1.ItemDrag
    Dim dat As New DataObject
    'dat.SetData("FileDrop", New String() {e.Item.tag})
    Dim files(ListView1.SelectedItems.Count - 1) As String
    For i = 0 To files.Length - 1
      files(i) = "ftp:/" + activeFtpCon + ParaService.FP_unix(txtFtpCurDir.Text, ListView1.SelectedItems(i).Text)
    Next
    dat.SetData("siURLDrop", files)
    dat.SetText(e.Item.tag)
    ListView1.DoDragDrop(dat, DragDropEffects.Copy Or DragDropEffects.Link Or DragDropEffects.Move Or DragDropEffects.Scroll)
  End Sub

  Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
    ftpNavSelectedRow()
  End Sub

  Private Sub ListView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDown
    If e.Button = Windows.Forms.MouseButtons.Right Then
      Dim lvi = ListView1.GetItemAt(e.X, e.Y)
      setCurRow(lvi)
      Application.DoEvents()
    End If
  End Sub

  Private Sub ListView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseUp
    If e.Button = Windows.Forms.MouseButtons.Right Then
      Application.DoEvents()
      Dim lvi = ListView1.GetItemAt(e.X, e.Y)

      OeffnenToolStripMenuItem.Visible = False
      SpeichernUnterToolStripMenuItem.Visible = False
      UmbenennenToolStripMenuItem.Visible = False
      LoeschenToolStripMenuItem.Visible = False
      NameKopierenToolStripMenuItem.Visible = False
      PfadKopierenToolStripMenuItem.Visible = False
      AusschneidenToolStripMenuItem.Enabled = False
      EinfuegenToolStripMenuItem.Enabled = FtpCutfilePath <> ""
      EinfuegenToolStripMenuItem.Text = If(FtpCutfileName = "", "Einfügen", "Einfügen: " + FtpCutfileName)

      If lvi Is Nothing Then
      Else
        lvi.Selected = True
        If lvi.Tag = "DIR" Then
          NameKopierenToolStripMenuItem.Visible = True
        Else
          SpeichernUnterToolStripMenuItem.Visible = True
          NameKopierenToolStripMenuItem.Visible = True
          PfadKopierenToolStripMenuItem.Visible = True
        End If
        OeffnenToolStripMenuItem.Visible = True
        UmbenennenToolStripMenuItem.Visible = True
        LoeschenToolStripMenuItem.Visible = True
        AusschneidenToolStripMenuItem.Enabled = True
      End If

      cmFtpFilelist.Show(sender, e.X, e.Y)
    End If
  End Sub

  'Private Sub igFtpFilelist_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles igFtpFilelist.MouseUp
  '  If e.Button = Windows.Forms.MouseButtons.Right Then

  '    OeffnenToolStripMenuItem.Visible = False
  '    SpeichernUnterToolStripMenuItem.Visible = False
  '    UmbenennenToolStripMenuItem.Visible = False
  '    LoeschenToolStripMenuItem.Visible = False
  '    NameKopierenToolStripMenuItem.Visible = False
  '    PfadKopierenToolStripMenuItem.Visible = False
  '    AusschneidenToolStripMenuItem.Enabled = False
  '    EinfuegenToolStripMenuItem.Enabled = FtpCutfilePath <> ""
  '    EinfuegenToolStripMenuItem.Text = If(FtpCutfileName = "", "Einfügen", "Einfügen: " + FtpCutfileName)
  '    If row Is Nothing Then
  '      igFtpFilelist.SetCurRow(-1)
  '    Else
  '      igFtpFilelist.SetCurRow(row.Index)
  '      If igFtpFilelist.CurRow.Cells(1).Value = "A" Then
  '        NameKopierenToolStripMenuItem.Visible = True
  '      Else
  '        SpeichernUnterToolStripMenuItem.Visible = True
  '        NameKopierenToolStripMenuItem.Visible = True
  '        PfadKopierenToolStripMenuItem.Visible = True
  '      End If
  '      OeffnenToolStripMenuItem.Visible = True
  '      UmbenennenToolStripMenuItem.Visible = True
  '      LoeschenToolStripMenuItem.Visible = True
  '      AusschneidenToolStripMenuItem.Enabled = True
  '    End If

  '    cmFtpFilelist.Show(sender, e.X, e.Y)

  '  End If
  'End Sub


  'Sub resizeTabBarPanel()
  '  On Error Resume Next
  '  Dim allElWidth As Integer = 0, maxElHeight As Integer = 0, rowCount As Integer = 1
  '  For Each el As Control In pnlTabbar.Controls
  '    allElWidth += el.Width
  '    If allElWidth > pnlTabbar.Width Then allElWidth = 0 : rowCount += 1
  '    maxElHeight = Math.Max(maxElHeight, el.Height)
  '  Next
  '  'Dim rows = Math.Ceiling(allElWidth / pnlTabbar.Width)
  '  'pnlTabbar.Height = rowCount * maxElHeight
  '  SplitContainer1.Height = Me.ClientSize.Height - StatusStrip1.Height - pnlTabbar.Height

  'End Sub

  Private Sub frm_main_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    '  resizeTabBarPanel()
    '  ListView1.Columns(0).Width = ListView1.Width - 25

  End Sub

  Private Sub btnFtpRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFtpRefresh.Click
    fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)

  End Sub

  Private Sub btnSelFtpCon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelFtpCon.Click
    Dim ctx As New ContextMenuStrip
    Dim el As ToolStripMenuItem
    For Each kvp In ftpConnections
      el = ctx.Items.Add(kvp.Key)
      If el.Text = activeFtpCon Then el.Checked = True
      AddHandler el.Click, AddressOf ctxSelServerClick
    Next
    ctx.Items.Add("-")
    el = ctx.Items.Add("Bearbeiten ...")
    AddHandler el.Click, AddressOf ctxSelServerClick

    ctx.Show(sender, 0, sender.height)
  End Sub

  Private Sub ctxSelServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    If sender.text = "Bearbeiten ..." Then
      '  OutlookBar1.SetSelectionChanged(OutlookBar1.Buttons(3))
      'frm_windowManager.Show()
      'frm_windowManager.Activate()
      'frm_windowManager.TabControl1.SelectedIndex = 3
      Workbench.ShowOptionsDialog("ftphosts")
    Else
      activeFtpCon = sender.text
      readBookmarks()
      txtFtpCurDir.Text = "/"
      tvwFolders.Nodes.Clear()
      fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)
    End If
  End Sub

  Private Sub btnFtpRenameFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFtpRenameFile.Click, UmbenennenToolStripMenuItem.Click
    Dim fileSpec As String = getSelectedFtpFilespec()
    If fileSpec = "" Then Exit Sub
    Dim newName = InputBox("Datei/Ordner umbenennen - bitte neuen Name eingeben:", "FTP", getCurRow().Text)
    If newName = "" Or newName = getCurRow().Text Then Exit Sub

    connectToServer(activeFtpCon)

    Dim res = ftpRename(fileSpec, txtFtpCurDir.Text + newName)
    If res = False Then
      MsgBox("Fehler beim Umbenennen" + vbNewLine + "alt: " + fileSpec + vbNewLine + "neu: " + txtFtpCurDir.Text + newName + vbNewLine + showFtpError(), MsgBoxStyle.Critical)
    Else
      fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)
    End If
  End Sub

  Private Sub btnFtpDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFtpDownload.Click, SpeichernUnterToolStripMenuItem.Click
    Dim fileSpec As String = getSelectedFtpFilespec()
    If fileSpec = "" Then Exit Sub
    connectToServer(activeFtpCon)

    Using ofd As New SaveFileDialog
      ofd.Title = "Datei " + fileSpec + " downloaden nach ..."
      ofd.FileName = getCurRow().Text

      If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
        ftpDownload(ofd.FileName, fileSpec)
      End If

    End Using
  End Sub

  Private Sub btnFtpDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFtpDelete.Click, LoeschenToolStripMenuItem.Click
    Dim fileSpec As String = getSelectedFtpFilespec()
    If fileSpec = "" Then Exit Sub
    connectToServer(activeFtpCon)

    If MsgBox("Sind Sie sicher, dass Sie das folgende Objekt unwiderruflich löschen möchten?" + _
              vbNewLine + vbNewLine + fileSpec, MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "FTP") _
       = MsgBoxResult.No Then Exit Sub

    Dim res = ftpDelete(fileSpec, getCurRow.Tag = "DIR")
    If res = False Then
      MsgBox("Fehler beim Löschen" + vbNewLine + "Path: " + fileSpec + vbNewLine + showFtpError(), MsgBoxStyle.Critical)
    Else
      fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)
    End If
  End Sub

  Private Sub btnFtpCreateFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFtpCreateFile.Click, NeueDateiToolStripMenuItem.Click
    Using ced As New frm_createNew
      ced.CreateIn = txtFtpCurDir.Text
      If ced.ShowDialog = Windows.Forms.DialogResult.OK Then
        Select Case ced.SelectedElementType
          Case frm_createNew.ElementType.File
            createNewFile(ced.ElementName, ced.NavigateAfter)
          Case frm_createNew.ElementType.Folder
            createNewFolder(ced.ElementName, ced.NavigateAfter)
        End Select
      End If
    End Using
  End Sub

  Sub createNewFile(ByVal newFileName As String, ByVal navigateAfter As Boolean)
    Dim filespec = ParaService.FP_unix(txtFtpCurDir.Text, newFileName)
    ftpSaveTextFile(activeFtpCon, filespec, "")
    fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)
    If navigateAfter Then onNavigate("ftp:/" + activeFtpCon + filespec)
  End Sub

  Sub createNewFolder(ByVal newFolderName As String, ByVal navigateAfter As Boolean)
    connectToServer(activeFtpCon)
    Dim res = ftpCreateDir(ParaService.FP_unix(txtFtpCurDir.Text, newFolderName))
    If res = True Then
      If navigateAfter Then fillFtpFilelist(activeFtpCon, ParaService.FP_unix(txtFtpCurDir.Text, newFolderName) + "/")
    Else
      MsgBox("Fehler beim Ordner anlegen" + vbNewLine + "Path: " + newFolderName + vbNewLine + showFtpError(), MsgBoxStyle.Critical)
    End If
  End Sub

  Private Sub btnFtpUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFtpUpload.Click, HochladenToolStripMenuItem.Click
    Using ofd As New OpenFileDialog
      ofd.Title = "Datei hochladen nach ftp://" + activeFtpCon + txtFtpCurDir.Text + " ..."
      ofd.Filter = "Alle Dateien|*.*"
      If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
        connectToServer(activeFtpCon)
        Dim remoteFile = ParaService.FP_unix(txtFtpCurDir.Text, IO.Path.GetFileName(ofd.FileName))
        Dim res = ftpUpload(ofd.FileName, remoteFile)
        If res = False Then
          MsgBox("Fehler beim Hochladen" + vbNewLine + "Loc: " + ofd.FileName + "Srv: " + remoteFile + vbNewLine + showFtpError(), MsgBoxStyle.Critical)
        Else
          fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)
        End If
      End If
    End Using
  End Sub

  Private Sub LinkLabel10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    fillFtpFilelist(activeFtpCon, sender.tag)

  End Sub

  Private Sub OeffnenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OeffnenToolStripMenuItem.Click
    ftpNavSelectedRow()
  End Sub

  Private Sub NameKopierenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameKopierenToolStripMenuItem.Click
    Dim filename = getCurRow().Text
    Clipboard.Clear()
    Clipboard.SetText(filename)
  End Sub

  Private Sub PfadKopierenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PfadKopierenToolStripMenuItem.Click
    Dim filespec As String = getSelectedFtpFilespec()
    Clipboard.Clear()
    Clipboard.SetText(filespec)
  End Sub

  Private Sub AusschneidenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AusschneidenToolStripMenuItem.Click
    Dim filespec As String = getSelectedFtpFilespec()
    FtpCutfileName = getCurRow().Text
    FtpCutfilePath = filespec
  End Sub

  Function getSelectedFtpFilespec() As String
    If ListView1.SelectedItems.Count <> 1 Then Return ""
    Return ParaService.FP_unix(txtFtpCurDir.Text, getCurRow().Text)
  End Function

  Function getCurRow() As ListViewItem
    If ListView1.SelectedItems.Count <> 1 Then Return Nothing
    Return ListView1.SelectedItems(0)
  End Function

  Private Sub EinfuegenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EinfuegenToolStripMenuItem.Click
    If FtpCutfilePath <> "" Then
      Dim target = ParaService.FP_unix(txtFtpCurDir.Text, FtpCutfileName)
      Dim res = ftpRename(FtpCutfilePath, target)
      If res = False Then
        MsgBox("Fehler beim Verschieben" + vbNewLine + "von: " + FtpCutfilePath + vbNewLine + "nach: " + target + vbNewLine + showFtpError(), MsgBoxStyle.Critical)
      Else
        fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)
      End If
      FtpCutfileName = ""
      FtpCutfilePath = ""
    End If
  End Sub

  Private Sub LinkLabel11_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    onNavigate("ftp:/teamwiki.net/httpdocs/.htaccess")
  End Sub

  Private Sub ListView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListView1.DragEnter
    If e.Data.GetDataPresent("FileDrop") Then
      e.Effect = DragDropEffects.Copy
    End If
  End Sub

  Private Sub Listview1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles ListView1.DragDrop
    If e.Data.GetDataPresent("FileDrop") Then
      e.Effect = DragDropEffects.Copy
      'frm_pleasewait.Show()
      'frm_pleasewait.ProgressBar1.Style = ProgressBarStyle.Blocks
      cls_IDEHelper.GetSingleton().ContentHelper.StatusText = "Dateien werden hochgeladen ..."
      Application.DoEvents()
      connectToServer(activeFtpCon)
      Dim files() As String = e.Data.GetData("FileDrop")
      'frm_pleasewait.ProgressBar1.Value = 0 : frm_pleasewait.ProgressBar1.Maximum = files.Length
      Dim i = 0
      For Each sourceFile In files
        Dim filename = IO.Path.GetFileName(sourceFile)
        'frm_pleasewait.ProgressBar1.Increment(1)
        i += 1
        cls_IDEHelper.GetSingleton().ContentHelper.StatusText = "" & i & " / " & files.Length & ": " & filename
        Application.DoEvents()
        Dim remoteFile = ParaService.FP_unix(txtFtpCurDir.Text, filename)
        Dim res = ftpUpload(sourceFile, remoteFile)
        If res = False Then
          MsgBox("Fehler beim Hochladen" + vbNewLine + "Loc: " + sourceFile + "Srv: " + remoteFile + vbNewLine + showFtpError(), MsgBoxStyle.Critical)
        Else
        End If
      Next
      fillFtpFilelist(activeFtpCon, txtFtpCurDir.Text)
      cls_IDEHelper.GetSingleton().ContentHelper.StatusText = "Fertig"
      'frm_pleasewait.Hide()
    End If
  End Sub

  Private Sub AlleDateienCachenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlleDateienCachenToolStripMenuItem.Click
    For Each lvi In ListView1.Items
      If lvi.tag <> "DIR" Then
        Dim filespec = txtFtpCurDir.Text + lvi.text
        '  Debug.Print(filespec)
        cls_IDEHelper.GetSingleton().ContentHelper.StatusText = "Caching " + filespec + " ..."
        Application.DoEvents()
        ftpReadTextFile(activeFtpCon, filespec)
      End If
    Next
    MsgBox("Fertig")
    cls_IDEHelper.GetSingleton().ContentHelper.StatusText = "Fertig"
  End Sub

  Private Sub frmTB_ftpExplorer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    chkUseFtpProxy.Checked = ParaService.Glob.para("frmTB_ftpExplorer__useFtpProxy", "FALSE") = "TRUE"

  End Sub

  Private Sub FlowLayoutPanel1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles flpBookmarks.DoubleClick
    Dim btag As String = txtFtpCurDir.Text
    Dim abPos = btag.LastIndexOf("/", btag.Length - 2)
    Dim btext As String = InputBox("Beschriftung eingeben:", , btag.Substring(abPos, btag.Length - abPos))
    If btext = "" Then Exit Sub
    addBookmark(btext, btag)
    saveBookmarks()
  End Sub

  Function getBookmarksFilename() As String
    Return ParaService.SettingsFolder + "ftp_" + activeFtpCon + "\bookmarksFTP.txt"
  End Function
  Sub readBookmarks()
    Dim bmFile = getBookmarksFilename()
    If Not IO.File.Exists(bmFile) Then
      IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(bmFile))
      IO.File.WriteAllText(bmFile, "")
    End If
    Dim lines() = IO.File.ReadAllLines(bmFile)
    flpBookmarks.Controls.Clear()
    For Each li In lines
      If li = "" Then Continue For
      Dim parts() = Split(li, vbTab)
      addBookmark(parts(0), parts(1))
    Next
  End Sub
  Sub saveBookmarks()
    Dim sb As New System.Text.StringBuilder
    For Each ctrl In flpBookmarks.Controls
      sb.AppendLine(ctrl.text + vbTab + ctrl.tag)
    Next
    IO.File.WriteAllText(getBookmarksFilename, sb.ToString)

  End Sub
  Sub addBookmark(ByVal btext As String, ByVal btag As Object)
    Dim lnk As New LinkLabel
    lnk.AutoSize = True
    lnk.Text = btext
    lnk.Tag = btag
    lnk.LinkColor = Color.DarkSlateBlue
    flpBookmarks.Controls.Add(lnk)
    AddHandler lnk.MouseDown, AddressOf lnkBookmark_mouseDown
  End Sub
  Sub lnkBookmark_mouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
    Dim lnk As LinkLabel = sender
    'If isKeyPressed(Keys.ControlKey) Then
    '  lnk.DoDragDrop(lnk, DragDropEffects.Move)
    If e.Button = Windows.Forms.MouseButtons.Left Then
      fillFtpFilelist(activeFtpCon, lnk.Tag, False)
      '  onNavigate(lnk.Tag)
    ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
      flpBookmarks.Controls.Remove(sender)
      saveBookmarks()
      '  Dim f As New frm_editBookmark
      '  f.Tag = lnk
      '  f.txtEditbookmark_text.Text = lnk.Text
      '  f.txtEditbookmark_url.Text = lnk.Tag
      '  f.grpEditbookmark.Show()
      '  f.Top = Me.Top + 100
      '  f.Left = Me.Left + 200
      '  f.ShowDialog()
      '  f.Dispose()
    End If
  End Sub


  Private Sub btnNavFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNavFile.Click
    Dim url = InputBox("Gib eine URL ein, die navigiert werden soll...", , ParaService.Glob.para("frmTB_ftpExplorer__lastUrlnav"))
    If url = "" Then Exit Sub
    ParaService.Glob.para("frmTB_ftpExplorer__lastUrlnav") = url
    onNavigate(url)
  End Sub

  Private Sub chkShowTreeview_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowTreeview.CheckedChanged
    SplitContainer1.Panel1Collapsed = Not chkShowTreeview.Checked
  End Sub

  Private Sub tvwFolders_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwFolders.NodeMouseClick
    fillFtpFilelist(activeFtpCon, "/" + e.Node.FullPath + "/")
  End Sub

  Private Sub chkUseFtpProxy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseFtpProxy.CheckedChanged
    ParaService.Glob.para("frmTB_ftpExplorer__useFtpProxy") = If(chkUseFtpProxy.Checked, "TRUE", "FALSE")
    useFtpProxy = chkUseFtpProxy.Checked
  End Sub

End Class