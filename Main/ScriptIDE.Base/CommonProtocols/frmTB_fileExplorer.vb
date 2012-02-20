Public Class frmTB_fileExplorer

  Dim tshelper As ToolStrip_DontEatClickEvent
  Dim sortMan As EV.Windows.Forms.ListViewSortManager
  Dim bmWindow As frm_fileExplorerFavorites

  Enum expMode
    folder
    locSearch
    everything
  End Enum


  Private _currentMode As expMode
  Public Property CurrentMode() As expMode
    Get
      Return _currentMode
    End Get
    Set(ByVal value As expMode)
      _currentMode = value

      If value = expMode.folder Then
        tspShowHide.Text = "p"
        pnlSearch.Hide()
        col_Pfad.Width = 0
        ColumnHeader2.Width = 90
        ColumnHeader3.Width = 60
        ListView1.Height = SplitContainer3.Panel2.Height - ToolStrip1.Height '-pnlSearch.Height
        ListView1.SmallImageList = imlLocalFolder
      Else
        tspShowHide.Text = "s"
        pnlSearch.Show()
        col_Pfad.Width = Math.Max(100, ListView1.Width - ColumnHeader1.Width - 30)
        ColumnHeader2.Width = 0
        ColumnHeader3.Width = 0
        ListView1.Height = SplitContainer3.Panel2.Height - ToolStrip1.Height - pnlSearch.Height
        ListView1.Items.Clear()
        ListView1.SmallImageList = imlSearchResult
        txtSearch.Focus()
        txtSearch.SelectAll()

        Everything.SetMax(0)
        Everything.Query(True)
        If Everything.GetLastError() = 2 Then
          rbSearchEverywhere.Enabled = False
          rbSearchLocal.Checked = True
          LinkLabel1.Visible = True
        Else
          rbSearchEverywhere.Enabled = True
          LinkLabel1.Visible = False
        End If
      End If

      If value <> expMode.everything And evSearch IsNot Nothing Then
        evSearch.Dispose()
        evSearch = Nothing
      End If

      Select Case value
        Case expMode.folder
          refreshFolderList()

        Case expMode.locSearch
          ListView1.Items.Clear()

        Case expMode.everything
          evSearch = New EveryThingIPCWindow
          evSearch.StartSearch(txtSearch.Text, ListView1)

      End Select
    End Set
  End Property

  'Sub showHideSearchPnl(ByVal newState As Boolean)
  '  If newState Then 'soll öffnen
  '    tspShowHide.Text = "s"
  '    pnlSearch.Show()
  '    col_Pfad.Width = Math.Max(100, ListView1.Width - ColumnHeader1.Width - 30)
  '    ColumnHeader2.Width = 0
  '    ColumnHeader3.Width = 0
  '    ListView1.Height = SplitContainer3.Panel2.Height - ToolStrip1.Height - pnlSearch.Height
  '    ListView1.Items.Clear()
  '    ListView1.SmallImageList = imlSearchResult
  '    txtSearch.Focus()
  '    txtSearch.SelectAll()

  '    Everything.SetMax(0)
  '    Everything.Query(True)
  '    If Everything.GetLastError() = 2 Then
  '      rbSearchEverywhere.Enabled = False
  '      rbSearchLocal.Checked = True
  '    Else
  '      rbSearchEverywhere.Enabled = True
  '    End If
  '  Else
  '    If evSearch IsNot Nothing Then
  '      evSearch.Dispose()
  '      evSearch = Nothing
  '    End If
  '    tspShowHide.Text = "p"
  '    pnlSearch.Hide()
  '    col_Pfad.Width = 0
  '    ColumnHeader2.Width = 90
  '    ColumnHeader3.Width = 60
  '    ListView1.Height = SplitContainer3.Panel2.Height - ToolStrip1.Height '-pnlSearch.Height
  '    ListView1.SmallImageList = imlLocalFolder
  '    refreshFolderList()
  '  End If
  'End Sub

  Public Overrides Function GetPersistString() As String
    Return tbPrefix + "LocalFileBrowser"
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    cls_IDEHelper.GetSingleton().BeforeShowAddinWindow(tbPrefix + "LocalFileBrowser", Me)
    MyBase.Show()
  End Sub


  'Private Sub ftvLocalbrowser_FolderClick(ByVal sender As System.Object, ByVal e As AxCCRPFolderTV6.__FolderTreeview_FolderClickEvent) Handles ftvLocalbrowser.FolderClick
  '  IGrid2.DefaultCol.CellStyle.EmptyStringAs = TenTec.Windows.iGridLib.iGEmptyStringAs.EmptyString
  'End Sub

  'Private Sub IGrid2_CellMouseUp(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellMouseUpEventArgs) Handles IGrid2.CellMouseUp
  '  If e.Button = Windows.Forms.MouseButtons.Right Then
  '    IGrid2.SetCurCell(e.RowIndex, e.ColIndex)
  '    Application.DoEvents()
  '    Dim fileSpec As String = IGrid2.CurRow.Tag

  '    If IO.Directory.Exists(fileSpec) Then
  '      ftvLocalbrowser.SelectedFolder.name = fileSpec
  '      'ElseIf fileSpec.ToUpper.EndsWith(".RTF") Then
  '      'gotoNote("file:/" + fileSpec)
  '    Else
  '      onNavigate(fileSpec)
  '      'gotoNote("loc:/" + fileSpec)
  '    End If
  '  End If
  'End Sub

  Private Sub ftvLocalbrowser_SelectionChange(ByVal sender As Object, ByVal e As AxCCRPFolderTV6.__FolderTreeview_SelectionChangeEvent) Handles AxFolderTreeview2.SelectionChange
   
  End Sub

  Private Sub frmTB_fileExplorer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    ParaService.Glob.para("frmTB_fileExplorer__splitterPos") = SplitContainer3.SplitterDistance
    ParaService.Glob.para("frmTB_fileExplorer__searchType") = If(rbSearchEverywhere.Checked, "everything", "local")
    ParaService.Glob.para("frmTB_fileExplorer__searchLocalFolder") = txtSearchRoot.Text
  End Sub

  Private Sub frmTB_fileExplorer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    tshelper = New ToolStrip_DontEatClickEvent(ToolStrip1)
    sortMan = New EV.Windows.Forms.ListViewSortManager(ListView1, New Type() { _
                 GetType(EV.Windows.Forms.ListViewTextCaseInsensitiveSort), _
                 GetType(EV.Windows.Forms.ListViewTextCaseInsensitiveSort), _
                 GetType(EV.Windows.Forms.ListViewTextCaseInsensitiveSort)}, _
                 0, SortOrder.Ascending)

    SplitContainer3.SplitterDistance = paraservice.Glob.para("frmTB_fileExplorer__splitterPos", "200")
    rbSearchEverywhere.Checked = ParaService.Glob.para("frmTB_fileExplorer__searchType", "local") = "everything"
    rbSearchLocal.Checked = ParaService.Glob.para("frmTB_fileExplorer__searchType", "local") = "local"
    txtSearchRoot.Text = ParaService.Glob.para("frmTB_fileExplorer__searchLocalFolder", "C:\")

    refreshProperties()
    AxFolderTreeview2.SelectedFolder.Name = ParaService.Glob.para("frmTB_fileExplorer__selectedFolder", "C:\")

    refreshBookmarksMenu()
  End Sub

  Private Sub tsbNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbNewFile.Click
    Using sfd As New SaveFileDialog
      sfd.Title = "Neue Datei anlegen ..."
      sfd.Filter = "Alle Dateien (*.*)|*.*"
      sfd.DefaultExt = ".txt"
      sfd.InitialDirectory = AxFolderTreeview2.SelectedFolder.name

      If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
        IO.File.WriteAllText(sfd.FileName, "")
        onNavigate(sfd.FileName)
      End If
    End Using
  End Sub

  Private Sub tsbExplorer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExplore.Click
    Process.Start("explorer.exe", "/e," + AxFolderTreeview2.SelectedFolder.name)
  End Sub

  Private Sub tsbScriptDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    AxFolderTreeview2.SelectedFolder.name = ParaService.SettingsFolder + "scriptClass\"
  End Sub

  Private Sub ListView1_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles ListView1.AfterLabelEdit
    If ListView1.VirtualMode Then Exit Sub
    If String.IsNullOrEmpty(e.Label) Then e.CancelEdit = True : Exit Sub
    Try
      Dim oldFilename As String = ListView1.Items(e.Item).Tag
      Dim newFilename As String = IO.Path.Combine(IO.Path.GetDirectoryName(oldFilename), e.Label)
      IO.File.Move(oldFilename, newFilename)

      ListView1.Items(e.Item).Tag = newFilename
    Catch ex As Exception
      e.CancelEdit = True
      MsgBox("Fehler beim Umbenennen der Datei..." + vbNewLine + ex.ToString, MsgBoxStyle.Exclamation)
    End Try
  End Sub

  Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
    If ListView1.VirtualMode Then Exit Sub
    If ListView1.SelectedItems.Count <> 1 Then Exit Sub

    Dim fileSpec As String = ListView1.SelectedItems(0).Tag

    If IO.Directory.Exists(fileSpec) Then
      AxFolderTreeview2.SelectedFolder.name = fileSpec
      'ElseIf fileSpec.ToUpper.EndsWith(".RTF") Then
      'gotoNote("file:/" + fileSpec)
    Else
      onNavigate(fileSpec)
      'gotoNote("loc:/" + fileSpec)
    End If
  End Sub

  Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbDirUp.Click
    On Error Resume Next
    AxFolderTreeview2.SelectedFolder.name = AxFolderTreeview2.SelectedFolder.parent.fullPath
  End Sub

  Private Sub ListView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles ListView1.ItemDrag
    Dim dat As New DataObject
    Dim out(ListView1.SelectedItems.Count - 1) As String
    For i = 0 To out.Length - 1
      out(i) = ListView1.SelectedItems(i).Tag
    Next
    dat.SetData("FileDrop", out)
    dat.SetData("siURLDrop", out)
    dat.SetText(Join(out, vbNewLine))
    ListView1.DoDragDrop(dat, DragDropEffects.Copy Or DragDropEffects.Link Or DragDropEffects.Move Or DragDropEffects.Scroll)
  End Sub


  Private Sub AxFolderTreeview2_FolderClick(ByVal sender As System.Object, ByVal e As AxCCRPFolderTV6.__FolderTreeview_FolderClickEvent) Handles AxFolderTreeview2.FolderClick
    On Error Resume Next
    txtLocFolder.Text = e.folder.Name
    
    refreshFolderList()
  End Sub

  Sub refreshFolderList()
    If ListView1.VirtualMode Then Exit Sub
    If txtLocFolder.Text = "Desktop" Then Exit Sub
    ParaService.Glob.para("frmTB_fileExplorer__selectedFolder") = txtLocFolder.Text
    Dim files() = IO.Directory.GetFileSystemEntries(txtLocFolder.Text)
    ListView1.Items.Clear()
    imlLocalFolder.Images.Clear()
    For i = 0 To files.Length - 1
      files(i) = If(IO.Directory.Exists(files(i)), "D" + vbTab + " ", "F" + vbTab) + IO.Path.GetFileName(files(i)) + vbTab + files(i)
    Next
    'Array.Sort(files)
    For i = 0 To files.Length - 1
      Dim parts() = Split(files(i), vbTab)
      imlLocalFolder.Images.Add(parts(2), FileTypeAndIcon.RegisteredFileType.GetAssociatedIcon(parts(2), FileTypeAndIcon.RegisteredFileType.assoc_iconSize.SHGFI_SMALLICON))
      Dim ir = ListView1.Items.Add(parts(1), parts(2))
      Try
        ir.SubItems.Add(IO.File.GetLastWriteTime(parts(2)).ToString("yy-MM-dd HH:mm"))
        ir.SubItems.Add(FileLen(parts(2)))

      Catch :
      End Try
      ir.Tag = parts(2)
    Next
  End Sub

  Private Sub tsbView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbView.Click
    ListView1.View = (ListView1.View + 1) Mod 5

  End Sub

  Private Sub txtLocFolder_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLocFolder.KeyUp
    If e.KeyCode = Keys.Enter Or (e.Control And e.KeyCode = Keys.V) Then
      AxFolderTreeview2.SelectedFolder.name = txtLocFolder.Text
    End If
  End Sub

  Private Sub txtLocFolder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocFolder.TextChanged

  End Sub

  Private Sub ListView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseUp
    If e.Button = Windows.Forms.MouseButtons.Right Then
      Dim item = ListView1.GetItemAt(e.X, e.Y)
      If item Is Nothing Then Exit Sub

      If pnlSearch.Visible AndAlso Not String.IsNullOrEmpty(item.SubItems(3).Text) Then
        AxFolderTreeview2.SelectedFolder.name = item.SubItems(3).Text
        CurrentMode = expMode.folder
      Else

        Dim fileSpec As String = item.Tag

        onNavigate(fileSpec)
      End If
    End If
  End Sub

  Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

  End Sub

  Sub switchToSearchMode()
    If rbSearchEverywhere.Checked Then CurrentMode = expMode.everything Else CurrentMode = expMode.locSearch
  End Sub

  Private Sub tspShowHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tspShowHide.Click
    If tspShowHide.Text = "p" Then
      switchToSearchMode()
    Else
      CurrentMode = expMode.folder
    End If
  End Sub

  Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    txtSearchRoot.Text = AxFolderTreeview2.SelectedFolder.Name
  End Sub

  Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
    If e.KeyCode = Keys.Enter Then
      e.Handled = True : e.SuppressKeyPress = True
    End If
  End Sub

  Dim evSearch As EveryThingIPCWindow

  Private Sub rbSearchEverywhere_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSearchEverywhere.CheckedChanged
    If CurrentMode <> expMode.folder Then switchToSearchMode()
  End Sub

  Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
    If CurrentMode = expMode.everything Then
      evSearch.StartSearch(txtSearch.Text, ListView1)
    ElseIf CurrentMode = expMode.locSearch Then
      If e.KeyCode = Keys.Enter Then

        If evSearch IsNot Nothing Then
          evSearch.Dispose()
          evSearch = Nothing
        End If

        ListView1.Hide()
        ListView1.Items.Clear()
        outIdx = 0
        Dim filter() = Split(txtSearch.Text.ToUpper, " ")
        Dim searchRoot = txtSearchRoot.Text
        If String.IsNullOrEmpty(searchRoot) Then searchRoot = txtLocFolder.Text
        recursiveFileSearch("", ParaService.FP(searchRoot), filter, True)

        ListView1.Show()
      End If
    End If
  End Sub

  Dim outIdx As Integer = 0
  Sub recursiveFileSearch(ByVal startFolder As String, ByVal rootFolder As String, ByVal fileFilter() As String, ByVal recursiv As Boolean)
    Dim hFind As IntPtr, lineData(10) As String
    Dim wfd As New WIN32_FIND_DATA
    ' ff_folderCount += 1
    Application.DoEvents()
    hFind = FindFirstFile(ParaService.FP(rootFolder + startFolder, "*.*"), wfd)
    Do
      If wfd.cFileName = "." Or wfd.cFileName = ".." Then Continue Do
      Dim relFileSpec As String = ParaService.FP(startFolder, wfd.cFileName)
      Dim fileSpec As String = rootFolder + relFileSpec
      Dim fileSpecUpper = fileSpec.ToUpper
      Dim fileNameUpper = wfd.cFileName.ToUpper
      ' Debug.Print(fileSpec & outIdx & "  " & outMaxIdx)
      If outIdx Mod 347 = 0 Then Application.DoEvents() 'onStatusEvent(startFolder, fileSpec)
      outIdx += 1

      'If p_cancel = True Then Exit Sub 'wird vom statusEvent gesetzt
      If (wfd.dwFileAttributes And FileAttribute.Directory) > 0 Then

        If recursiv = True Then recursiveFileSearch(relFileSpec, rootFolder, fileFilter, True)

      Else
        Dim fileExt = IO.Path.GetExtension(fileNameUpper)

        For Each word In fileFilter
          If word.Contains("\") Then
            If fileSpecUpper.Contains(word) = False Then Continue Do
          Else
            If fileNameUpper.Contains(word) = False Then Continue Do
          End If
        Next

        lineData(1) = wfd.cFileName
        lineData(2) = fileExt
        lineData(3) = wfd.nFileSizeHigh * MAXDWORD + wfd.nFileSizeLow

        Dim lmDate = DateTime.FromFileTime(((CType(wfd.ftLastWriteTime.dwHighDateTime, Long) << 32) + wfd.ftLastWriteTime.dwLowDateTime))
        lineData(4) = lmDate.ToString("yyyy-MM-dd HH-mm-ss")
        Dim crDate = DateTime.FromFileTime(((CType(wfd.ftCreationTime.dwHighDateTime, Long) << 32) + wfd.ftCreationTime.dwLowDateTime))
        lineData(5) = crDate.ToString("yyyy-MM-dd HH-mm-ss")

        lineData(7) = fileSpec
        lineData(8) = relFileSpec

        Dim item = ListView1.Items.Add(lineData(1), 1)
        item.SubItems.Add(lineData(4))
        item.SubItems.Add(lineData(3))
        item.SubItems.Add(rootFolder + startFolder)
        item.Tag = fileSpec

        'lineData(0) = lineData(SortCriteria)

        'ff_fileCount += 1
      End If
    Loop While FindNextFile(hFind, wfd) = True
  End Sub

  Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
    Process.Start("http://voidtools.com")
  End Sub


  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    txtSearchRoot.Text = txtLocFolder.Text
  End Sub

  Sub refreshProperties()

    AxFolderTreeview2.AutoUpdate = ParaService.Glob.para("ctl_local_options__chkAutoUpdate", "TRUE") = "TRUE"
    AxFolderTreeview2.VirtualFolders = ParaService.Glob.para("ctl_local_options__chkVirtualFolders", "TRUE") = "TRUE"
    Dim rootDir As String = ParaService.Glob.para("ctl_local_options__txtRoot")
    If Not String.IsNullOrEmpty(rootDir) Then _
      AxFolderTreeview2.RootFolder.name = rootDir

  End Sub

  Sub refreshBookmarksMenu()
    Dim el As ToolStripMenuItem
    If IO.File.Exists(ParaService.SettingsFolder + "fileExplorer_bookmarks.txt") = False Then IO.File.WriteAllText(ParaService.SettingsFolder + "fileExplorer_bookmarks.txt", "")

    Dim data As String = IO.File.ReadAllText(ParaService.SettingsFolder + "fileExplorer_bookmarks.txt")
    Dim LINES() As String = Split(data, vbNewLine)
    cmFavoriten.Items.Clear()

    el = cmFavoriten.Items.Add("Verwalten ...", My.Resources.settings)
    cmFavoriten.Items.Add("-")
    AddHandler el.Click, AddressOf ctxSelServerClick

    For Each line In LINES
      Dim parts() = Split(line, vbTab)
      If parts.Length < 2 Then Continue For
      If parts(0) = "-" Then
        cmFavoriten.Items.Add("-")
      Else
        el = cmFavoriten.Items.Add(parts(0), imlSearchResult.Images(0))
        el.Tag = parts(1)
        AddHandler el.Click, AddressOf ctxSelServerClick
      End If
    Next
  End Sub

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    
    cmFavoriten.Show(sender, 0, sender.height)
  End Sub

  Private Sub ctxSelServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    If sender.text = "Verwalten ..." Then
      If bmWindow Is Nothing OrElse bmWindow.IsDisposed Then
        bmWindow = New frm_fileExplorerFavorites
        bmWindow.Location = Me.Location + New Size(10, 200)
      End If
      bmWindow.Show()
      bmWindow.Activate()
    Else
      AxFolderTreeview2.SelectedFolder.name = sender.tag
      txtLocFolder.Text = sender.tag
      refreshFolderList()
    End If
  End Sub

  Private Sub frmTB_fileExplorer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    
  End Sub
End Class