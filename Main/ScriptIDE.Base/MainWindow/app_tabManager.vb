Module app_tabManager

  Private rtfControls As New Dictionary(Of String, Object)
  Private actRtfBox As Object

  Sub saveAll()
    For Each frm In rtfControls
      If TypeOf frm.Value Is IDockContentForm Then
        Dim tab As IDockContentForm = frm.Value
        If tab.Dirty Then tab.onSave()
      End If
    Next
  End Sub



  'Property openedTabs() As String
  '  Get
  '    'Return Join(rtfControls.Keys.ToArray, "|##|")
  '    Dim max = tbOpenedFiles.IGrid1.Rows.Count - 1
  '    Dim out(max) As String
  '    For i = 0 To max
  '      out(i) = tbOpenedFiles.IGrid1.Rows(i).Tag.getFileTag()
  '    Next
  '    Dim res = Join(out, "|##|")
  '    Return res
  '  End Get
  '  Set(ByVal value As String)
  '    Dim files() = Split(Trim(value), "|##|")
  '    'frm_pleasewait.ProgressBar1.Maximum = files.Length
  '    For Each f In files
  '      If f <> "" Then gotoNote(f, True)
  '      'frm_pleasewait.ProgressBar1.Increment(1)
  '    Next
  '    ' createTabBar()
  '  End Set
  'End Property

  Function tabNext() As Boolean
    Dim dingdong As Boolean = False
    For Each kp In rtfControls
      If dingdong Then setActRtfBox(kp.Value) : Return True
      If kp.Value Is actRtfBox Then dingdong = True
    Next
  End Function
  Function tabPrev() As Boolean
    Dim lastTab As Object
    For Each kp In rtfControls
      If kp.Value Is actRtfBox And lastTab IsNot Nothing Then setActRtfBox(lastTab) : Return True
      lastTab = kp.Value
    Next
  End Function

  Function isTabOpened(ByVal sName As String) As Boolean
    sName = sName.Replace("\", "/").Replace("//", "/")
    If rtfControls.ContainsKey(sName.ToLower) Or rtfControls.ContainsKey("loc:/" + sName.ToLower) Then
      Return True
    End If
    Return False
  End Function

  Sub parseDocumentURL(ByVal sName As String, ByRef sURL As String, ByRef forceFTHandler As String, ByRef forceFTHandler2 As String, ByRef documentRef As Object)
    sURL = sName
    sURL = sURL.Replace("\", "/").Replace("//", "/")
    'If sName.ToUpper.StartsWith("LOC:/") Then sName = "ftp:/_loc/" + sName.Substring(5)

    If sURL.StartsWith("[") And sURL.Contains("]") Then
      Dim abPos = sURL.IndexOf("]")
      forceFTHandler = sURL.Substring(1, abPos - 1)
      forceFTHandler2 = "[" + forceFTHandler.ToLower + "]"
      sURL = sURL.Substring(abPos + 1)
    End If

    If sURL.Substring(1, 2) = ":/" Then sURL = "loc:/" + sURL

    rtfControls.TryGetValue(forceFTHandler + sURL.ToLower, documentRef)

  End Sub

  Public Sub onNavigate(ByVal location As String)
    'On Error Resume Next
    Try
      If String.IsNullOrEmpty(location) Then Exit Sub
      Dim linenrpos = location.LastIndexOf("?")
      Dim lineNr As Integer = -1
      If linenrpos > -1 Then
        If IsNumeric(location.Substring(linenrpos + 1)) Then
          lineNr = CInt(location.Substring(linenrpos + 1))
          location = location.Substring(0, linenrpos)
        End If
      End If

      If location.Chars(1) = ":"c Then
        If IO.File.Exists(location) = False Then Exit Sub
        Dim fileExt = IO.Path.GetExtension(location).ToUpper
        'If fileExt = ".RTF" Then
        '  gotoNote("file:/" + location.Replace("\", "/"))
        'Else
        gotoNote("loc:/" + location.Replace("\", "/"))
        'End If
      Else
        gotoNote(location)
      End If

      If lineNr > -1 Then
        Dim rtf As IDockContentForm = getActiveRTF()
        rtf.jumpToLine(lineNr - 1)

      End If
    Catch ex As Exception
      TT.DumpException("onNavigate:" + location + "<<<", ex)
    End Try
  End Sub


  Function gotoNote(ByVal sName As String, Optional ByVal openLazy As Boolean = False, Optional ByVal dontShowForm As Boolean = False) As IDockContentForm
    Dim forceFTHandler = "", forceFTHandler2 As String = "", urlName As String, documentRef As Object

    parseDocumentURL(sName, urlName, forceFTHandler, forceFTHandler2, documentRef)

    If documentRef IsNot Nothing Then
      ' gibts schon -- navigieren
      If dontShowForm Then Throw New InvalidOperationException("Tab is already opened, unable to create with 'dontShowForm'")

      setActRtfBox(documentRef)
      Return documentRef
    End If

    Dim protocol = urlName.Substring(0, urlName.IndexOf("/"))

    Dim hlp As Object 'IDockContentForm

    Dim fileExt = IO.Path.GetExtension(urlName)
    If forceFTHandler <> "" Then fileExt = forceFTHandler

    hlp = ContentViewerService.GetDocumentViewer(fileExt)
    If hlp Is Nothing Then 'kein Viewer gefunden
      MsgBox("Für den Dateityp " + fileExt + " ist kein DocumentViewer hinterlegt. Die Datei kann nicht angezeigt werden.", MsgBoxStyle.Critical)
      Return Nothing
    End If
    If TypeOf hlp Is Boolean AndAlso hlp = False Then Return Nothing '...kümmert sich selber um die Oberfläche

    If Not dontShowForm Then hlp.Show(Workbench.Instance.DockPanel1, DockState.Document)

    rtfControls.Add(forceFTHandler2 + urlName.ToLower, hlp)
    hlp.url = urlName
    hlp.Hash = forceFTHandler2 + urlName '.ToLower --> DON'T! URLs might be case sensitive!!!
    hlp.Text = hlp.getViewFilename()

    'createGridLineForHelperObject(tbOpenedFiles.IGrid1, sName, hlp)

    If Not openLazy Then
      setActRtfBox(hlp)
      hlp.onLazyInit()
      hlp.onRead()
      hlp.createIndexList()
    End If
    hlp.isLazy = openLazy
    Return hlp
  End Function

  Sub renameTab(ByVal oldKey As String, ByVal newKey As String)
    oldKey = LCase(oldKey) : newKey = LCase(newKey)
    Dim el As Object
    If rtfControls.ContainsKey(newKey) Or rtfControls.TryGetValue(oldKey, el) = False Then _
      TT.Write("Rename Tab failed", oldKey + "->" + newKey, "err") : Exit Sub
    rtfControls.Remove(oldKey)
    rtfControls.Add(newKey, el)
  End Sub

  'Function createImageControl() As PictureboxHelper
  '  Dim rb As New PictureBox
  '  rb.Dock = DockStyle.Fill
  '  rb.SizeMode = PictureBoxSizeMode.Zoom

  '  'MAIN.pnlMainContainer.Controls.Add(rb)
  '  Dim cont As New DockContent
  '  cont.Controls.Add(rb)
  '  cont.Show(MAIN.DockPanel1, DockState.Document)

  '  Dim hlp As New PictureboxHelper(rb)
  '  hlp.dockContainer = cont : cont.Tag = hlp
  '  hlp.indexListCtrl = tbIndexList.ListBox1
  '  Return hlp
  'End Function

  'Function createCodeControl() As ScintillaHelper
  '  Dim rb As New ScintillaNet.Scintilla
  '  rb.Dock = DockStyle.Fill
  '  rb.Font = New Font("Courier New", 11, FontStyle.Regular, GraphicsUnit.Point)
  '  rb.Styles.Default.FontName = "Courier New"
  '  rb.Selection.HideSelection = False
  '  rb.AcceptsTab = True
  '  rb.Indentation.IndentWidth = 2
  '  rb.Indentation.TabWidth = 2
  '  rb.Indentation.SmartIndentType = ScintillaNet.SmartIndent.Simple
  '  rb.Indentation.UseTabs = False
  '  rb.Margins(0).Width = 40 'zeilennummer sichtbar machen
  '  rb.Margins(1).Width = 20 'spalte dahinter
  '  rb.Styles.LineNumber.IsVisible = True 'zeilennummern an
  '  rb.IsBraceMatching = True

  '  'MAIN.pnlMainContainer.Controls.Add(rb)
  '  Dim cont As New DockContent
  '  cont.Controls.Add(rb)
  '  cont.Show(MAIN.DockPanel1)

  '  Dim hlp As New ScintillaHelper(rb)
  '  hlp.dockContainer = cont : cont.Tag = hlp
  '  hlp.indexListCtrl = tbIndexList.ListBox1
  '  Return hlp
  'End Function

  'Function createRtfControl() As RtfHelper
  '  Dim rb As New RichTextBox
  '  rb.ShowSelectionMargin = True
  '  rb.Dock = DockStyle.Fill
  '  rb.Font = New Font("Courier New", 10, FontStyle.Regular, GraphicsUnit.Point)
  '  rb.HideSelection = False
  '  rb.AcceptsTab = True

  '  'MAIN.pnlMainContainer.Controls.Add(rb)
  '  Dim cont As DockContent = New DockContent
  '  cont.Controls.Add(rb)
  '  cont.Show(MAIN.DockPanel1, DockState.Document)

  '  Dim hlp As New RtfHelper(rb)
  '  hlp.dockContainer = cont : cont.Tag = hlp
  '  hlp.indexListCtrl = tbIndexList.ListBox1
  '  Return hlp
  'End Function

  Function getTitleFormatString(ByVal name As String) As String
    Dim formatString As String = ""
    Select Case name
      Case "mainWinTitle" : formatString = ParaService.Glob.para("format_" + name, "%fn - ScriptIDE %vs - %pd")
      Case "mainWinCaption" : formatString = ParaService.Glob.para("format_" + name, "%fn")
      Case "mainWinStatusBar" : formatString = ParaService.Glob.para("format_" + name, "%pd [%pn] %fs")
    End Select
    Return formatString
  End Function

  Function formatTitleString(ByVal text As String, ByVal fn As String, ByVal fs As String)
    text = text.Replace("%pd", ParaService.ProfileDisplayName)
    text = text.Replace("%pn", ParaService.ProfileName)
    text = text.Replace("%vs", My.Application.Info.Version.ToString(2))
    text = text.Replace("%fn", fn)
    text = text.Replace("%fs", fs)
    Return text
  End Function

  Sub refreshMainTitle()
    On Error Resume Next
    Dim prefix = ""
    If Workbench.Instance.chkSticky.Checked Then prefix = "* "
    Dim filename = "", filespec = ""
    Dim win As Form = actRtfBox 'Workbench.Instance.DockPanel1.ActiveDocument

    If win IsNot Nothing Then
      Workbench.Instance.txtAktFormType.Text = win.GetType().FullName
      Workbench.Instance.txtActPersistString.Text = CType(win, DockContent).GetPersistString()
      If TypeOf win Is IDockContentForm Then
        filespec = CType(win, IDockContentForm).getFileTag()
        Workbench.Instance.PictureBox2.Image = CType(win, IDockContentForm).getIcon()(1).ToBitmap
      Else
        filespec = ""
        Workbench.Instance.PictureBox2.Image = win.Icon.ToBitmap
      End If
      filename = win.Text
      'Workbench.Instance.labWinTitle.Text = win.Text

    Else
      Workbench.Instance.PictureBox2.Image = Workbench.Instance.Icon.ToBitmap
      'Workbench.Instance.labWinTitle.Text = "ScriptIDE 4"
      filename = "(keine Datei geöffnet)"
      filespec = ""
    End If

    Workbench.Instance.Text = formatTitleString(getTitleFormatString("mainWinTitle"), filename, filespec)
    Workbench.Instance.labWinTitle.Text = formatTitleString(getTitleFormatString("mainWinCaption"), filename, filespec)
    Workbench.Instance.txtGlobAktFileSpec.Text = filespec

    Workbench.Instance.pnlTitlebar.Invalidate()
  End Sub

  Sub setActRtfBox(ByVal hlp As Object)
    If hlp Is Nothing Then Exit Sub
    On Error Resume Next
    'If Not TypeOf hlp Is IDockContentForm Then Exit Sub

    If hlp.isLazy = False And actRtfBox Is hlp And Workbench.Instance.DockPanel1.ActiveDocument Is hlp Then Exit Sub

    hlp.Activate()
    '  Workbench.Instance.DockPanel1.SetPaneIndex(Workbench.Instance.DockPanel1.ActiveDocumentPane, 1)
    'Dim hlp3 As DockContent = hlp

    'Workbench.Instance.DockPanel1.ActiveDocumentPane.ActiveContent = hlp
    ' Application.DoEvents()

    actRtfBox = hlp
    'HACK ___ Debug.Print("setActRtfBox " + hlp.getFileTag())
    'setButtonColor(hlp.tabButton, True)
    ' hlp.tabRow.selected = True
    ' hlp.tabRow.ensureVisible()

    'If hlp.tabRowKey <> "" And tbOpenedFiles.IGrid1.Rows.KeyExists(hlp.tabRowKey) Then _
    '   tbOpenedFiles.IGrid1.SetCurRow(hlp.tabRowKey)
    'titelzeile ändern ...
    'HACK ___ refreshMainTitle()


    If TypeOf hlp Is IDockContentForm Then
      Dim hlp2 As IDockContentForm = hlp
      If hlp2.isLazy Then hlp2.onLazyInit() : hlp2.onRead()
      'indexList erstellen ...
      hlp2.createIndexList()

      hlp2.RTF.Focus()
    End If

    'showIntelliPanelByFileExt(hlp.fileName)

    createOpenedTabList()

    Dim fileTag As String = ""
    fileTag = hlp.Text
    fileTag = hlp.getFileTag()

    cls_IDEHelper.GetSingleton.OnDocumentTabActivated(hlp, fileTag)
    refreshMainTitle()
  End Sub
  Function getActiveRTF() As Object
    Return actRtfBox
  End Function

  Function closeAllTabs() As Boolean
    If rtfControls.Count = 0 Then Return True
    For i = rtfControls.Count - 1 To 0 Step -1
      Dim hlp = rtfControls.Values(i)
      If hlp.Dirty Then
        setActRtfBox(hlp)
        Dim cancel = hlp.onCheckDirty()
        If cancel = False Then Return False
      End If
    Next
    Return True
  End Function

  Sub setButtonColor(ByVal btn As Button, ByVal selected As Boolean)
    If selected Then
      btn.ForeColor = Color.Black
      btn.BackColor = Color.Gold
      btn.FlatAppearance.MouseOverBackColor = Color.Orange

    Else
      btn.BackColor = Color.Transparent ' Color.FromKnownColor(KnownColor.ButtonFace) '
      btn.ForeColor = Color.White ' Color.Black '
      btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 33, 66, 177)
    End If
    btn.FlatAppearance.BorderColor = Color.SteelBlue
  End Sub

  Sub setIconToFileext(ByVal iml As ImageList, ByVal cl As TenTec.Windows.iGridLib.iGCell, ByVal fileSpec As String)
    Dim fileExt = IO.Path.GetExtension(fileSpec).ToLower
    If fileSpec = "folder" Then fileExt = "folder"
    If iml.Images.ContainsKey(fileExt) = False Then
      Dim icons() = FileTypeAndIcon.RegisteredFileType.GetFileIconByExt(fileExt)

      iml.Images.Add(fileExt, icons(0))
    End If
    cl.ImageIndex = iml.Images.IndexOfKey(fileExt)
  End Sub

  Function getDocumentPane() As DockPane
    For Each p In Workbench.Instance.DockPanel1.Panes
      If p.DockState = DockState.Document Then
        Return p
      End If
    Next
  End Function

  Sub createOpenedTabList()
    Dim doc = Workbench.Instance.DockPanel1.ActiveDocumentPane 'getDocumentPane()
    If doc Is Nothing Then Exit Sub
    With tbOpenedFiles.IGrid1
      .SuspendLayout()
      tbOpenedFiles.imlIgrid.Images.Clear()
      Dim scrollPos = .VScrollBar.Value
      .Rows.Clear()
      '.Rows.Count = Workbench.Instance.DockPanel1.Documents.Count
      .Rows.Count = doc.Contents.Count
      For i = 0 To doc.Contents.Count - 1
        Dim document As IDockContent = doc.Contents(i)
        tbOpenedFiles.imlIgrid.Images.Add(document.DockHandler.Form.Icon)
        'Dim i = 0
        'For Each document In Workbench.Instance.DockPanel1.Documents
        .Cells(i, 0).Value = document.DockHandler.TabText 'doc.Contents(i).DockHandler.TabText
        .Cells(i, 0).ImageIndex = i

        .Rows(i).Tag = document 'DirectCast(document.DockHandler.Form, DockContent).GetPersistString() 'DirectCast(doc.Contents(i).DockHandler.Form, DockContent).GetPersistString()
        'If document Is Workbench.Instance.DockPanel1.ActiveDocument Then .SetCurRow(i)
        If document Is actRtfBox Then .SetCurRow(i)
        '  i += 1
      Next
      .VScrollBar.Value = scrollPos
      .ResumeLayout()
    End With
  End Sub

  Sub createGridLineForHelperObject(ByVal grid As TenTec.Windows.iGridLib.iGrid, ByVal dictKey As String, ByVal hlp As Object)
    Dim row = grid.Rows.Add
    'tab.AutoSize = True
    Dim protocol = dictKey.Substring(0, dictKey.IndexOf("/")).ToUpper
    row.Cells(0).Value = hlp.getViewFilename()
    If hlp.Dirty = True Then row.Cells(1).Value = "%"
    row.Key = "rk" & Now.Ticks
    setIconToFileext(tbOpenedFiles.imlIgrid, row.Cells(0), dictKey)
    'tab.TextImageRelation = TextImageRelation.ImageBeforeText
    'tab.Padding = New Padding(0, 0, 0, 0)
    'tab.Margin = New Padding(-2, 0, -12, 0)

    'tab.Size = New Size(20, 15)

    'tab.Image = MAIN.ImageList1.Images(getImageKey(kp.Value.Filename))

    row.Tag = hlp
    hlp.tabRowKey = row.Key

    'tab.FlatStyle = FlatStyle.Standard
    'tab.FlatAppearance.BorderSize = 0
    If actRtfBox Is hlp Then
      row.EnsureVisible()
      tbOpenedFiles.IGrid1.SetCurRow(row.Index)
    End If
    'setButtonColor(tab, actRtfBox Is kp.Value)
    'tab.Refresh()
    'Dim width = tab.Width
    ' tab.AutoSize = False
    'tab.Size = New Size(width, 15)

    'AddHandler tab.MouseUp, AddressOf tabButton_MouseUp

  End Sub
  Sub createTabBar()
    Stop
    'Dim tabBar = MAIN.pnlTabbar
    'tabBar.Controls.Clear()
    Dim tabList = tbOpenedFiles.IGrid1
    Dim scrollPos = tabList.VScrollBar.Value
    tabList.Rows.Clear()
    Dim idx As Integer = 0
    For Each kp In rtfControls
      idx += 1
      Dim row = tabList.Rows.Add
      'tab.AutoSize = True
      Dim protocol = kp.Key.Substring(0, kp.Key.IndexOf("/")).ToUpper
      row.Cells(0).Value = kp.Value.getViewFilename()
      If kp.Value.Dirty = True Then row.Cells(1).Value = "%"
      row.Key = "rk" & idx
      setIconToFileext(tbOpenedFiles.imlIgrid, row.Cells(0), kp.Key)
      'tab.TextImageRelation = TextImageRelation.ImageBeforeText
      'tab.Padding = New Padding(0, 0, 0, 0)
      'tab.Margin = New Padding(-2, 0, -12, 0)

      'tab.Size = New Size(20, 15)

      'tab.Image = MAIN.ImageList1.Images(getImageKey(kp.Value.Filename))
      row.Tag = kp.Value
      'tab.FlatStyle = FlatStyle.Standard
      'tab.FlatAppearance.BorderSize = 0
      If actRtfBox Is kp.Value Then
        row.EnsureVisible()
        tbOpenedFiles.IGrid1.SetCurRow(row.Index)
      End If
      'setButtonColor(tab, actRtfBox Is kp.Value)
      'tab.Refresh()
      'Dim width = tab.Width
      ' tab.AutoSize = False
      'tab.Size = New Size(width, 15)

      'AddHandler tab.MouseUp, AddressOf tabButton_MouseUp
      kp.Value.tabRowKey = row.Key
      'tabBar.Controls.Add(tab)
    Next
    Try
      tabList.VScrollBar.Value = scrollPos
    Catch : End Try
    'MAIN.resizeTabBarPanel()
    refreshMainTitle()
  End Sub

  Sub closeTab(ByVal hlp As Object) 'IDockContentForm
    On Error Resume Next
    hlp.Close()

  End Sub
  Function internalCloseTab(ByVal hlp As IDockContentForm) As Boolean
    On Error Resume Next
    If hlp Is Nothing Then Return True
    If hlp.onCheckDirty() = False Then Return False
    'MAIN.pnlMainContainer.Controls.Remove(hlp.RTF)
    'hlp.RTF.Dispose()

    'Dim rowIdx As Integer = tbOpenedFiles.IGrid1.Rows(hlp.tabRowKey).Index
    'tbOpenedFiles.IGrid1.Rows.RemoveAt(rowIdx)
    'tbOpenedFiles.IGrid1.SetCurRow(rowIdx)


    'If hlp Is actRtfBox Then
    'If tabNext() = False Then If tabPrev() = False Then actRtfBox = Nothing
    'End If

    rtfControls.Remove(hlp.Hash.ToLower)
    Return True
    ' createTabBar()
  End Function


  Sub tabButton_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Select Case e.Button
      Case MouseButtons.Left
        setActRtfBox(sender.Tag)
      Case MouseButtons.Right
        closeTab(sender.Tag)

    End Select
  End Sub

  Function getImageKey(ByVal filename As String) As String
    Return "file"
  End Function


End Module
