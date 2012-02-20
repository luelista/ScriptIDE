Imports System.Windows.Forms
Imports siaScriptSyncMini.FileTypeAndIcon

Public Class frmTB_scriptSync

  Dim tshelper As ToolStrip_DontEatClickEvent
  Dim sortMan As EV.Windows.Forms.ListViewSortManager


  Public Overrides Function GetPersistString() As String
    Return tbScriptSync_ID
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    IDE.BeforeShowAddinWindow(tbScriptSync_ID, Me)
    MyBase.Show()
  End Sub

  Private Sub tsbLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbLogin.Click
    onChangeLogin()
    showAppFiles()
  End Sub

  Private Sub tsbRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbRefresh.Click
    showAppFiles()
  End Sub

  Private Sub tsbUploadScript_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUploadScript.Click
    Dim fileSpec = IDE.getActiveTabFilespec()
    If fileSpec.ToLower.StartsWith("loc:/") = False Then Exit Sub
    fileSpec = fileSpec.Substring(5)

    Dim rErrMes As String
    upload_file(globAktAppID, fileSpec, IO.Path.GetFileName(fileSpec), IO.Path.GetDirectoryName(fileSpec), "", rErrMes)

    If rErrMes.StartsWith("OK") Then
      My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
      showAppFiles()
    Else
      doLogin(twLoginuser, twLoginPass)
      MsgBox("Beim Hochladen ist ein Fehler aufgetreten: " + vbNewLine + rErrMes, MsgBoxStyle.Exclamation)
    End If
  End Sub

  Sub colorizeCatLabels()
    With ScriptSyncSkin.GetSingleton()
      For Each ctrl In flpBookmarks.Controls
        If ctrl.tag = globAktAppID Then
          ctrl.backColor = .ActiveGroupBackColor : ctrl.foreColor = .ActiveGroupForeColor
        Else
          ctrl.backColor = .InactiveGroupBackColor : ctrl.foreColor = .InactiveGroupForeColor
        End If
      Next
    End With
  End Sub

  Sub showAllCats()
    Dim Lines = getAppList("10") '10=syncFiles

    For i = 3 To Lines.Length - 1
      If Lines(i).Trim = "" Then Continue For
      Dim Parts() = Split(Lines(i), vbTab)

      If Parts(4).Contains("hidden2") Then Continue For '4=Tags
      addBookmark(Parts(2), Parts(0))
    Next
    colorizeCatLabels()
  End Sub
  Sub addBookmark(ByVal btext As String, ByVal btag As Object)
    Dim lnk As New Label
    lnk.AutoSize = True
    lnk.Text = btext
    lnk.Tag = btag
    lnk.Cursor = Cursors.Hand
    'lnk.LinkColor = Color.LightGray
    flpBookmarks.Controls.Add(lnk)
    AddHandler lnk.MouseDown, AddressOf lnkBookmark_mouseDown
  End Sub
  Sub lnkBookmark_mouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
    Dim lnk As Label = sender
    If e.Button = Windows.Forms.MouseButtons.Left Then
      globAktAppID = lnk.Tag
      IDE.Glob.para("siaScriptSyncMini__appID") = globAktAppID
      showAppFiles()
      colorizeCatLabels()
    End If
  End Sub


  Sub showAppFiles()
    'Dim Parts() As String = IGrid1.CurRow.Tag
    If globAktAppID = 0 Then Exit Sub 'Achtung!
    globAktappInfo = New MWupd3File(getAppInfo(globAktAppID), False)
    'selectedAppInfo = info

    ListView1.Items.Clear()
    For Each file In globAktappInfo.Files
      Dim lvi = ListView1.Items.Add(file(FilePara.FileName))
      lvi.SubItems.Add(file(FilePara.AendDIZ))
      Dim fileDate = file(FilePara.AendDat)
      fileDate = fileDate.Substring(0, Len(fileDate) - 3)
      lvi.SubItems.Add(fileDate)

      lvi.ImageIndex = RegisteredFileType.getImageIndexForFileExt(ImageList1, file(FilePara.FileName))
      lvi.Tag = file
    Next
  End Sub

  Private Sub ListView1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseClick
    If e.Button = Windows.Forms.MouseButtons.Right Then
      Dim lvi = ListView1.GetItemAt(e.X, e.Y)
      If lvi Is Nothing Then Exit Sub

      ContextMenuStrip1.Tag = lvi
      Dim file() As String = lvi.Tag
      If file(FilePara.LocalTarget) <> "" Then
        DownloadNachToolStripMenuItem.Visible = True
        DownloadNachToolStripMenuItem.Text = "Download nach: " + file(FilePara.LocalTarget)
      Else
        DownloadNachToolStripMenuItem.Visible = False
      End If
      ContextMenuStrip1.Show(ListView1, e.X, e.Y)

    End If
  End Sub


  Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
    If ListView1.SelectedItems.Count <> 1 Then Exit Sub
    saveItem(ListView1.SelectedItems(0), Nothing)
  End Sub


  Private Sub frmTB_scriptSync_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
    If e.Data.GetDataPresent("FileDrop") Then
      e.Effect = Windows.Forms.DragDropEffects.Copy
    End If
  End Sub
  Private Sub frmTB_scriptSync_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
    If e.Data.GetDataPresent("FileDrop") Then
      For Each fileSpec As String In e.Data.GetData("FileDrop")
        Dim rErrMes As String
        upload_file(globAktAppID, fileSpec, IO.Path.GetFileName(fileSpec), IO.Path.GetDirectoryName(fileSpec), "", rErrMes)

        If rErrMes.StartsWith("OK") Then
          My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)

        Else
          doLogin(twLoginuser, twLoginPass)
          MsgBox("Beim Hochladen ist ein Fehler aufgetreten: " + vbNewLine + rErrMes, MsgBoxStyle.Exclamation)
        End If
      Next
      showAppFiles()
    End If
  End Sub


  Private Sub frmTB_scriptSync_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    globAktAppID = IDE.Glob.para("siaScriptSyncMini__appID", "0")
    showAllCats()
    showAppFiles()
  End Sub


  'Private Sub DownloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  saveItem(ContextMenuStrip1.Tag, False)
  'End Sub

  Sub saveItem(ByVal lvi As ListViewItem, ByVal targetDir As String)
    Dim file() As String = lvi.Tag
    Dim url = globAktappInfo.MorePara("RootURL") + file(FilePara.FileID) + ".dat"

    Dim targetFile As String
    'If file(FilePara.LocalTarget) = "" Or alwaysSaveAsDlg Then
    If String.IsNullOrEmpty(targetDir) Then
      SaveFileDialog1.FileName = file(FilePara.FileName)
      SaveFileDialog1.OverwritePrompt = False
      If SaveFileDialog1.ShowDialog() <> Windows.Forms.DialogResult.OK Then
        Exit Sub
      End If
      targetFile = SaveFileDialog1.FileName
    Else
      'targetFile = IDE.Glob.fp(file(FilePara.LocalTarget), file(FilePara.FileName))
      targetFile = IDE.Glob.fp(targetDir, file(FilePara.FileName))
    End If

    If IO.File.Exists(targetFile) Then
      Dim locDate = IO.File.GetLastWriteTime(targetFile)
      Dim remoteDate As Date, msg As String, msgIcon As MsgBoxStyle = MsgBoxStyle.Exclamation
      TT.Write("AendDat to parse:", file(FilePara.AendDat), "dump")
      msg = "Die Datei " + targetFile + " existiert schon. Möchten Sie sie durch die Version auf dem Server ersetzen?" + vbNewLine + vbNewLine
      If Not Date.TryParseExact(file(FilePara.AendDat), "yyyy-MM-dd HH:mm:ss", Nothing, Globalization.DateTimeStyles.None, remoteDate) Then
        msg += "Das Änderungsdatum der Datei auf dem Server konnte nicht ermittelt werden."
      ElseIf remoteDate > locDate Then
        msg += "Die Datei auf dem Server ist neuer als die lokale Datei." : msgIcon = MsgBoxStyle.Information
      ElseIf remoteDate < locDate Then
        msg += "ACHTUNG: Die Datei auf dem Server ist älter als die lokale Datei."
      End If
      If MsgBox(msg, MsgBoxStyle.YesNo Or msgIcon, "Datei überschreiben") = MsgBoxResult.No Then Exit Sub
    End If

    Try
      My.Computer.Network.DownloadFile(url, targetFile, "", "", True, 10000, True)
      If IDE.IsFileOpened("loc:/" + targetFile) Then
        Dim tab As IDockContentForm = IDE.NavigateFile("loc:/" + targetFile)
        tab.onRead()
      Else
        IDE.NavigateFile("loc:/" + targetFile)
      End If

    Catch ex As Exception
      MsgBox("Fehler beim Herunterladen aufgetreten:" + vbNewLine + ex.Message, MsgBoxStyle.Exclamation)
    End Try

  End Sub

  Private Sub DownloadNachToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadNachToolStripMenuItem.Click
    Dim file() As String = ContextMenuStrip1.Tag.Tag
    saveItem(ContextMenuStrip1.Tag, file(FilePara.LocalTarget))
  End Sub

  Private Sub SpeichernUnterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeichernUnterToolStripMenuItem.Click
    saveItem(ContextMenuStrip1.Tag, Nothing)
  End Sub

  Private Sub ordnerNameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeichernUnterToolStripMenuItem.Click
    saveItem(ContextMenuStrip1.Tag, IDE.GetSettingsFolder() + sender.Text)
  End Sub

  Private Sub frmTB_scriptSync_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ToolStrip1.Renderer = New Office2007Renderer.Office2007Renderer()
    sortMan = New EV.Windows.Forms.ListViewSortManager(ListView1, New Type() { _
                 GetType(EV.Windows.Forms.ListViewTextCaseInsensitiveSort), _
                 GetType(EV.Windows.Forms.ListViewTextCaseInsensitiveSort), _
                 GetType(EV.Windows.Forms.ListViewTextCaseInsensitiveSort)}, _
                 1, SortOrder.Ascending)
    tshelper = New ToolStrip_DontEatClickEvent(ToolStrip1)

    Dim dirList() = IO.Directory.GetDirectories(IDE.GetSettingsFolder())
    For Each dirName In dirList
      Dim cmi = ContextMenuStrip1.Items.Add(IO.Path.GetFileName(dirName))
      AddHandler cmi.Click, AddressOf ordnerNameToolStripMenuItem_Click
    Next
  End Sub


  Private Sub LoeschenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoeschenToolStripMenuItem.Click
    Dim file() As String = ContextMenuStrip1.Tag.Tag 'cms.Tag ist ListViewItem, davon Tag ist String[]
    If MsgBox("Möchten Sie die Datei " + file(FilePara.FileName) + " wirklich unwiderruflich löschen?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub

    Dim url = "http://mwupd3.teamwiki.net/request.php?c=remove_file"
    Dim post = "fileid=" + File(FilePara.FileID) + "&appid=" + File(FilePara.AppID)

    Dim RES = TwAjax.postUrl(url, post, "twnetSID=" + twSessID)
    Dim lines() = Split(RES, vbNewLine)
    If checkIfErrorResult(lines) = False Then Exit Sub

    showAppFiles()

  End Sub


End Class