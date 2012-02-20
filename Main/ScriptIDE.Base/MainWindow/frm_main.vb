
Public Class frm_main

  Friend m_FormBorder As New MVPS.clsFormBorder()
  Dim winSwitcherGroups() As String

  Dim MW_MSG_REREGISTER_ADDIN_APP As Integer = 0

  <DebuggerStepThrough()> _
  Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    Dim msg As WindowsUtilities.WindowsMessages = m.Msg
    'Debug.Print(msg.ToString)
    If m.Msg = MW_MSG_REREGISTER_ADDIN_APP AndAlso MW_MSG_REREGISTER_ADDIN_APP <> 0 Then
      Dim tmr As New Timer With {.Interval = 300}
      AddHandler tmr.Tick, AddressOf tmrReregisterAddinTick
      tmr.Start()

    ElseIf msg = WM_DWMCOMPOSITIONCHANGED Then
      If checkIfGlassEnabled() Then
        makeVistaForm()
      Else
        resetVistaForm()
      End If
    End If
    'If msg = WindowsUtilities.WindowsMessages.WM_WININICHANGE Then
    '  imlIgrid.Images.Clear()
    '  FileTypeAndIcon.RegisteredFileType.clearCache()
    '  createTabBar()

    'End If
    MyBase.WndProc(m)
  End Sub

  Sub tmrReregisterAddinTick(ByVal sender As Object, ByVal e As System.EventArgs)
    sender.stop()
    sender.Dispose()
    Workbench.RegisterIDE()
  End Sub


  Private Sub frm_main_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
    If e.Data.GetDataPresent("FileDrop") Then
      e.Effect = DragDropEffects.Copy
      Dim files() As String = e.Data.GetData("FileDrop")
      For Each fileSpec In files
        onNavigate(fileSpec)
      Next
    End If
  End Sub

  Private Sub frm_main_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
    If e.Data.GetDataPresent("FileDrop") Then
      e.Effect = DragDropEffects.Copy
    End If
  End Sub

  Private Sub frm_main_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragOver

  End Sub

  Private Sub frm_main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    On Error Resume Next
    Workbench.ShowUnloadScreen()
    Application.DoEvents()

    'If sh.globBreakMode <> "" Then
    '  If MsgBox("Zur Zeit befindet ein Skript im Haltemodus. Wenn das Programm jetzt geschlossen wird, kann es sein, dass die Instanz im Hintergrund offenbleibt. Um es zu beenden, klicke OK und drücke dann F11. Solange eine Instanz im Hintergrund geöffnet ist, kann keine neue gestartet werden.", MsgBoxStyle.OkCancel Or MsgBoxStyle.Exclamation, "Debugmodus läuft noch") = MsgBoxResult.Cancel Then
    '    e.Cancel = True : Exit Sub
    '  End If
    'End If
    ToolbarService.saveToolbarPositions()
    ParaService.Glob.saveFormPos(Me)
    ParaService.Glob.saveTuttiFrutti(Me)
    ParaService.Glob.saveTuttiFrutti(tbGlobSearch)

    ParaService.Glob.para("lastOpenedFile") = getActiveRTF.getFileTag()
    'glob.para("frm_main__OutlookbarHeight") = OutlookBar1.Height
    'glob.para("lastOpenedTabs") = openedTabs
    'savewidgetStates()
    DockPanel1.SaveAsXml(ParaService.ProfileFolder + "dockLayout.xml")
    saveBookmarks()

    Dim cancel = closeAllTabs()
    ParaService.Glob.saveParaFile()
    If cancel = False Then e.Cancel = True : Workbench.HideSplashScreen() : Exit Sub

    AppKeyHook.unhook()

    AddinInstance.DisconnectAll()

    'Err.Clear()
    'Dim hlpRef = CreateObject("ScriptIDE.Application")
    'hlpRef._internalInstRelease(hlpRef.GetInstanceIDByProfileName(ParaService.ProfileName))
    'Dim ideHelper = CreateObject("IdeHelper.Application")
    'ideHelper.removeSIRef(ideHelper.getSIInstanceID(ParaService.ProfileName))
    'If Err.Number <> 0 Then TT.Write("releasing instance ref failed", Err.Description, "warn")

    killConsoleProc()
    'glob.para("frm_main__Collapsed1") = If(SplitContainer1.Panel1Collapsed, "TRUE", "FALSE")
    'glob.para("frm_main__Collapsed2") = If(SplitContainer2.Panel2Collapsed, "TRUE", "FALSE")
    'glob.para("frm_main__TabControl1_SelectedIndex") = TabControl1.SelectedIndex


    Process.GetCurrentProcess.Kill()
    'glob.para("frm_main__lastSidebar") = OutlookBar1.SelectedButton.Index
  End Sub

  Private Sub frm_main_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    Dim str = getKeyString(e)
    'tbDebug.labKeyboardHook.Text = "keyCode: " & e.KeyCode & "  0x" & Hex(e.KeyCode) & vbNewLine & str

    'If check_keyboardHook.Checked AndAlso IO.File.Exists(ScriptHost.ScriptHost.Instance.expandScriptClassName("userKeyboardMan")) Then
    '  Dim ref = sH.scriptClass("userKeyboardMan")
    '  Try
    '    Dim bCancel As Boolean = False
    '    bCancel = CallByName(ref, "keyMan_" & e.KeyCode, CallType.Method, str)
    '    If bCancel = True Then
    '      e.SuppressKeyPress = True
    '      e.Handled = True
    '      Return
    '    End If
    '  Catch ex As Exception

    '  End Try
    'End If

    Select Case e.KeyCode
      Case Keys.Y
        If e.Alt Then tabPrev()
      Case Keys.X
        If e.Alt Then tabNext()

        'Case Keys.F12
        'Try
        ' OutlookBar1.SetSelectionChanged(OutlookBar1.Buttons(OutlookBar1.SelectedButton.Index - 1))
        'Catch : End Try
        'Case Keys.L
        'If e.Control And e.Alt Then
        'For Each el As Object In ta
        'End If
        'Case Keys.F4
        '  Try
        '    OutlookBar1.SetSelectionChanged(OutlookBar1.Buttons(OutlookBar1.SelectedButton.Index + 1))
        '  Catch : End Try
        'Case Keys.F4 : OutlookBar1.SetSelectionChanged(OutlookBar1.Buttons(0))
        'Case Keys.F5 : OutlookBar1.SetSelectionChanged(OutlookBar1.Buttons(1))
        'Case Keys.F6 : OutlookBar1.SetSelectionChanged(OutlookBar1.Buttons(2))
        'Case Keys.F7 : OutlookBar1.SetSelectionChanged(OutlookBar1.Buttons(3))
        'Case Keys.F8 : OutlookBar1.SetSelectionChanged(OutlookBar1.Buttons(4))
        'Case Keys.F9 : OutlookBar1.SetSelectionChanged(OutlookBar1.Buttons(5))

        'Case Keys.F2
        'SplitContainer1.Panel1Collapsed = Not SplitContainer1.Panel1Collapsed

        'Case Keys.F3
        'SplitContainer2.Panel2Collapsed = Not SplitContainer2.Panel2Collapsed
        'e.SuppressKeyPress = True : e.Handled = True

        'Case Keys.Up
        '  If e.Control Then
        '    e.SuppressKeyPress = True
        '    Try
        '      If tbIndexList.ListBox1.SelectedIndex > 0 Then tbIndexList.ListBox1.SelectedIndex -= 1
        '    Catch ' : ListBox1.SelectedIndex = ListBox1.Items.Count - 1 
        '    End Try
        '  End If
        'Case Keys.Down
        '  If e.Control Then
        '    e.SuppressKeyPress = True
        '    Try
        '      tbIndexList.ListBox1.SelectedIndex += 1
        '    Catch ' : ListBox1.SelectedIndex = 0 
        '    End Try
        '  End If
      Case Keys.ShiftKey
        If e.Alt And e.Control Then
          For i = 0 To DockPanel1.Contents.Count - 1
            TT.Write("Documents", DockPanel1.Contents(i).DockHandler.TabText, "dump")
          Next

          For i = 0 To DockPanel1.Panes.Count - 1
            TT.Write("ppp" & i, DockPanel1.Panes(i).DockState.ToString + "  " + DockPanel1.Panes(i).Contents.Count.ToString, "dump")

            '  DockPanel1.Panes(i).Visible = True
            For j = 0 To DockPanel1.Panes(i).Contents.Count - 1
              TT.Write("---> cont " & j, DockPanel1.Panes(i).Contents(j).DockHandler.TabText, "dump")
            Next
          Next

          createOpenedTabList()
          '  Beep()
        End If

    End Select

  End Sub

  'Sub showHideToolbar(Optional ByVal tbTitleMode As CheckState = CheckState.Indeterminate, Optional ByVal tbBookmarksVisible As CheckState = CheckState.Indeterminate)
  '  ' height TitleBar
  '  If checkIfGlassEnabled() Then
  '    pnlTitlebar.Height = 40 : ParaService.Glob.para("frm_main__tbTitleMode") = "1"
  '    labWinTitle.Font = New Font("Segoe UI", 14, FontStyle.Regular, GraphicsUnit.Point)
  '  ElseIf tbTitleMode = CheckState.Unchecked Then
  '    pnlTitlebar.Height = 25 : ParaService.Glob.para("frm_main__tbTitleMode") = "0"
  '    labWinTitle.Font = New Font("Segoe UI", 9, FontStyle.Bold, GraphicsUnit.Point)
  '    txtGlobAktFileSpec.Top = 14
  '  ElseIf tbTitleMode = CheckState.Checked Then
  '    pnlTitlebar.Height = 32 : ParaService.Glob.para("frm_main__tbTitleMode") = "1"
  '    labWinTitle.Font = New Font("Segoe UI", 14, FontStyle.Regular, GraphicsUnit.Point)
  '    txtGlobAktFileSpec.Top = 19
  '  End If

  '  ' bookmarks
  '  If tbBookmarksVisible = CheckState.Unchecked Then
  '    flpBookmarks.Hide() : ParaService.Glob.para("frm_main__tbBookmarks") = "0"
  '    lnkAddBookmark.Hide()
  '  ElseIf tbBookmarksVisible = CheckState.Checked Then
  '    flpBookmarks.Show() : ParaService.Glob.para("frm_main__tbBookmarks") = "1"
  '    lnkAddBookmark.Show()
  '  End If

  '  ' height UserToolbar
  '  Dim usertbHeight As Integer = 0
  '  'For Each ctrl As Control In MAIN.flpToolbar.Controls
  '  '  Debug.Print("TbVisible?" + vbTab + ctrl.Name + vbTab + ctrl.Visible.ToString())
  '  '  'If ctrl.Visible Then _
  '  '  usertbHeight = Math.Max(usertbHeight, Math.Max(0, ctrl.Top) + Math.Max(25, ctrl.Height))
  '  'Next
  '  'If usertbHeight = 0 Then
  '  '  flpToolbar.Hide()
  '  'Else
  '  '  flpToolbar.Height = usertbHeight
  '  '  flpToolbar.Show()
  '  'End If

  '  'Dim bookmarksHeight = If(flpBookmarks.Visible, flpBookmarks.Height, 0)

  '  'flpBookmarks.Top = pnlTitlebar.Height : lnkAddBookmark.Top = flpBookmarks.Top + 1
  '  'flpToolbar.Top = pnlTitlebar.Height + bookmarksHeight
  '  'DockPanel1.Top = pnlTitlebar.Height + bookmarksHeight + usertbHeight
  '  'DockPanel1.Height = StatusStrip1.Top - DockPanel1.Top


  '  KleineTitelleisteToolStripMenuItem.Checked = ParaService.Glob.para("frm_main__tbTitleMode") = "0"
  '  BookmarksAnzeigenToolStripMenuItem.Checked = ParaService.Glob.para("frm_main__tbBookmarks") = "1"
  'End Sub

  Sub showHideToolbar()
    Dim titleBar As Boolean = ParaService.Glob.para("titleBarVisible", "TRUE") = "TRUE"
    Dim subTitleBar As Boolean = ParaService.Glob.para("subTitleBarVisible", "TRUE") = "TRUE"
    Dim menuBar As Boolean = ParaService.Glob.para("menuVisible", "TRUE") = "TRUE"
    Dim bookmarksBar As Boolean = ParaService.Glob.para("bookmarksVisible", "TRUE") = "TRUE"

    labMaximize.Visible = Not titleBar
    labMinimize.Visible = Not titleBar
    labClose.Visible = Not titleBar

    m_FormBorder.Titlebar = titleBar
    pnlTitlebar.Visible = subTitleBar
    MenuStrip1.Visible = menuBar
    ' pnlBookmarks.Top = If(subTitleBar, pnlTitlebar.Height, 0)
    pnlBookmarks.Visible = bookmarksBar
  End Sub


  Private Sub frm_main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    On Error Resume Next
    'Workbench.Instance = Me

    Workbench.SetSplashText("Oberfläche wird initialisiert ...", True)
    'interproc_init()

    MW_MSG_REREGISTER_ADDIN_APP = WindowsUtilities.sys_windowMessages.RegisterWindowMessage("MW_MSG_REREGISTER_ADDIN_APP")


    TT.Write("WorkingDir(1)", Environment.CurrentDirectory, "dump")
    ' Environment.CurrentDirectory = IO.Path.GetDirectoryName(Application.ExecutablePath)
    TT.Write("EXEPath(2)", Application.ExecutablePath, "dump")
    If Environment.CurrentDirectory <> IO.Path.GetDirectoryName(Application.ExecutablePath) Then
      TT.Write("WorkingDir and EXEPath are different!", , "warn")
    End If

    TT.Write("load", "111", "dump")
    Dim ref As Object

    'ref = CreateObject("refman.Application")
    'If ref Is Nothing Then
    '  MsgBox("Der COM Reference Manager ist nicht vorhanden bzw. nicht registriert.", MsgBoxStyle.Critical)
    'Else
    '  If appObject Is Nothing Then
    '    ref.Obj("rtftab") = New comtyp.application
    '  Else
    '    ref.Obj("rtftab") = appObject
    '  End If
    'End If
    TT.Write("load", "222", "dump")
    'fileSpecMerkeZeileAbruf = "C:\yPara\mw-merkeZeileRtf.txt"
    'If IO.File.Exists(fileSpecMerkeZeileAbruf) = False Then IO.File.WriteAllText(fileSpecMerkeZeileAbruf, "")

    IO.Directory.CreateDirectory(ParaService.SettingsFolder + "loc_cache\")
    IO.Directory.CreateDirectory(ParaService.SettingsFolder + "iconCache\")
    IO.Directory.CreateDirectory(ParaService.ProfileFolder + "toolbars\")
    IO.Directory.CreateDirectory(ParaService.SettingsFolder + "scintillaConfig\")
    TT.Write("load", "333", "dump")
    ParaService.Glob.readFormPos(Me)
    ParaService.Glob.readTuttiFrutti(Me)

    m_FormBorder.client = Me
    m_FormBorder.Titlebar = False

    'scriptControl.Language = "VBScript"

    'sH.initIDEMode()
    'sh.setIdeHelper(cls_IDEHelper.GetSingleton)
    'sh.SilentMode = True

    TT.Write("load", "444", "dump")
    Application.DoEvents()


    loadFileAssocTab()
    'initColors()
    TT.Write("load", "555", "dump")
    'setFarbschema() --- addin
    ScriptWindowHelper.ScriptWindowManager.IdeHelper = cls_IDEHelper.GetSingleton()
    ToolbarService.RestoreToolbars()

    showHideToolbar() '(ParaService.Glob.para("frm_main__tbTitleMode", "1"), ParaService.Glob.para("frm_main__tbBookmarks", "1"))
    makeVistaForm()

    'DockPanel1.Extender.DockPaneStripFactory=

    'Show()

    'readFtpConnections()
    TT.Write("load", "666", "dump")
    'activeFtpCon = glob.para("lastFtpHost")

    Workbench.SetSplashText("AddIns werden verbunden ...", True)
    If Not Command().ToLower.Contains("/noaddin") Then
      AddinInstance.ReadAddinList()
    End If
    Interproc.UpdateCommandDefinition()

    TT.Write("load", "777a", "dump")
    Workbench.SetSplashText("Fenster werden wiederhergestellt ...", True)
    initToolboxWindows()

    ToolbarService.RestoreToolbarPositions()
    Show()

    TT.Write("load", "777c", "dump")
    'sH.globBreakMode = ""

    Workbench.SetSplashText("Initialisierung wird abgeschlossen ...", True)

    TT.Write("load", "777d", "dump")
    'sH.SilentMode = False

    cls_IDEHelper.Instance.OnOnIniDone()
    AddinInstance.OnStartupComplete()

    ToolbarService.BuildMainMenu(Me.MenuStrip1)

    ToolbarService.RestoreToolbarPositions()
    'If activeFtpCon <> "" Then fillFtpFilelist(activeFtpCon, tbFtpExplorer.txtFtpCurDir.Text)

    'mwRegisterSelf()
    TT.Write("load", "888", "dump")
    refreshMainTitle()

    AddHandler AddInTree.Changed, AddressOf onAddinTreeChanged

    TT.Write("load", "999", "dump")
    Me.Show() ': Workbench.SplashScreen.Activate()
    Application.DoEvents()
    Workbench.HideSplashScreen() 'Workbench.SplashScreen.Close()


    AppKeyHook.Initialize()

    readBookmarks()
    createWinSwitcherList()
    initScriptHelperMethodsList("scriptIDE.cls_scriptHelper")
    initScriptHelperMethodsList("scriptIDE.ScriptHelper_IDEHelper")
    initScriptHelperMethodsList("scriptIDE.frmTB_scriptWin")

    'startWebUpdate(True)
    tssl_Filename.Text = Environment.CurrentDirectory + " /// " + Application.ExecutablePath
    txtActProfile.Text = ParaService.ProfileFolder
    TT.Write("load", "000", "dump")
    'nextInstance(My.Application.CommandLineArgs)

    If IO.File.Exists("C:\yEXE\ScriptIDE.chm") Then helpFilePath = "C:\yEXE\ScriptIDE.chm"
    If IO.File.Exists(ParaService.FP(My.Application.Info.DirectoryPath, "ScriptIDE.chm")) Then helpFilePath = ParaService.FP(My.Application.Info.DirectoryPath, "ScriptIDE.chm")

  End Sub

  Sub onAddinTreeChanged()

    ToolbarService.BuildMainMenu(Me.MenuStrip1)
  End Sub

  Sub setFarbschema()

    With DockPanel1.Skin.DockPaneStripSkin
      'DockPanel1.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveTabGradient.StartColor = Color.Crimson
      With .ToolWindowGradient
        .InactiveCaptionGradient.StartColor = Color.FromArgb(255, 111, 111, 111) 'Color.FromArgb(255, 88, 119, 173)
        .InactiveCaptionGradient.EndColor = Color.FromArgb(255, 55, 55, 55) 'Color.FromArgb(255, 14, 40, 110)
        .InactiveCaptionGradient.TextColor = Color.FromArgb(255, 188, 188, 188) 'Color.White
        '.InactiveCaptionGradient.LinearGradientMode = Drawing2D.LinearGradientMode.Horizontal
        .ActiveCaptionGradient.StartColor = Color.FromArgb(255, 88, 119, 173) ' Color.FromArgb(255, 151, 104, 181)
        .ActiveCaptionGradient.EndColor = Color.FromArgb(255, 14, 40, 110) ' Color.FromArgb(255, 84, 14, 110)
        '.InactiveCaptionGradient.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical
        .ActiveCaptionGradient.TextColor = Color.White
      End With
      'DockPanel1.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = Color.FromArgb(255, 88, 119, 173)
      'DockPanel1.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = Color.FromArgb(255, 14, 40, 110)
      .DocumentGradient.ActiveTabGradient.StartColor = Color.FromArgb(255, 255, 255, 255)
      .DocumentGradient.ActiveTabGradient.EndColor = Color.FromArgb(255, 212, 208, 200)
      .DocumentGradient.ActiveTabGradient.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical
      '.DocumentGradient.DockStripGradient.StartColor = Color.FromArgb(255, 44, 44, 44)
      .DocumentGradient.DockStripGradient.LinearGradientMode = Drawing2D.LinearGradientMode.Vertical
      'DockPanel1.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.TextColor = Color.White
    End With
  End Sub

  'Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
  '  If ListView1.SelectedItems.Count <> 1 Then Exit Sub

  '  gotoNote("note_" + ListView1.SelectedItems(0).Text)
  'End Sub





  Private Sub chkTopmost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTopmost.CheckedChanged
    Me.TopMost = chkTopmost.Checked
    labTopmost.BackColor = If(Me.TopMost, Color.MediumVioletRed, Color.MediumSeaGreen)
  End Sub


  Private Sub chkSticky_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSticky.CheckedChanged
    refreshMainTitle()
  End Sub

  Private Sub labTopmost_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles labTopmost.MouseDown
    If e.Button = Windows.Forms.MouseButtons.Right Then
      chkTopmost.Checked = Not chkTopmost.Checked
      If Not chkTopmost.Checked Then Me.SendToBack()
    End If
  End Sub

  Private Sub frm_main_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate


  End Sub


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
    If Me.WindowState = FormWindowState.Maximized Then
      labMaximize.Text = "2"
    Else
      labMaximize.Text = "1"
    End If
  End Sub




  'Private Sub flpWidgets_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

  'End Sub

  'Private Sub flpWidgets_Resize(ByVal sender As Object, ByVal e As System.EventArgs)
  '  Me.SuspendLayout()
  '  For Each ctrl In flpWidgets.Controls
  '    ctrl.width = flpWidgets.Width - 5
  '  Next
  '  Me.ResumeLayout()
  '  Me.Refresh()
  'End Sub


  Private Sub Label3_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles StatusStrip1.MouseDown, PictureBox2.MouseDown, pnlTitlebar.MouseDown, labWinTitle.MouseDown, tsslDIZ.MouseDown, tssl_nextChar.MouseDown, tssl_Filename.MouseDown, tssl_cursorPos.MouseDown, tsProgress1.MouseDown
    If e.Button = Windows.Forms.MouseButtons.Left Then
      m_FormBorder.MoveMe()
    ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
      pnlFileinfo.Visible = Not pnlFileinfo.Visible
    End If
  End Sub

  Private Sub labExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkExit.Click
    Me.Close()
    'Application.Exit()
  End Sub

  Private Sub labSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSettings.Click
    'frm_widgetMan.Show()
    cmenuMain.Show(lnkSettings, 0, 20)
  End Sub

  Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    If getActiveRTF() IsNot Nothing Then
      getActiveRTF().onSave()
    End If
  End Sub


  Sub createWinSwitcherList()
    If Not IO.File.Exists(ParaService.ProfileFolder + "winSwitcher.txt") Then Exit Sub
    While cmenuMain.Items(0).Name.StartsWith("ws_")
      cmenuMain.Items.RemoveAt(0)
    End While
    Dim LINES() = IO.File.ReadAllLines(ParaService.ProfileFolder + "winSwitcher.txt")
    Dim modus = 1
    Dim itemID = 0, groupID As Integer = 0
    ReDim winSwitcherGroups(groupID)
    For Each line In LINES
      line = line.Trim
      If line = "" Then Exit Sub
      If line.StartsWith("-") Then
        groupID += 1
        ReDim Preserve winSwitcherGroups(groupID)
        cmenuMain.Items.Insert(itemID, New ToolStripSeparator()) : itemID += 1
        Continue For
      End If
      Dim parts() = Split(line, vbTab, 2)
      If parts.Length < 2 Then Continue For
      winSwitcherGroups(groupID) += parts(1) + vbTab
      Dim ts As New ToolStripMenuItem()
      ts.Name = "ws_" & itemID
      ts.Text = parts(0) : ts.Tag = New String() {groupID, parts(1)}
      AddHandler ts.Click, AddressOf wsMenuItem_Click
      cmenuMain.Items.Insert(itemID, ts) : itemID += 1
    Next
  End Sub

  Private Sub wsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Dim groupID As Integer = sender.Tag(0)
    Dim showWnds As String = vbTab + sender.Tag(1) + vbTab
    Dim hideWnds() = Split(winSwitcherGroups(groupID), vbTab)
    For Each wndID In hideWnds
      If wndID = "" Then Continue For
      Dim item As Object = cls_IDEHelper.Instance.GetInternalDockWindow(wndID)
      If item Is Nothing Then Continue For
      If showWnds.Contains(vbTab + wndID + vbTab) Then
        item.Show()
        item.Activate()
      Else
        If item.visible Then item.hide()
      End If
    Next

  End Sub


  'Private Sub txtGlobAktFileSpec_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
  '  If e.KeyCode = Keys.Enter Then
  '    gotoNote(txtGlobAktFileSpec.Text)
  '  End If
  'End Sub

  Private Sub lnkAddBookmark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAddBookmark.Click
    Dim btext As String = getActiveRTF().getViewFilename()
    Dim btag As String = getActiveRTF().getFileTag()
    addBookmark(btext, btag)
  End Sub
  Sub lnkBookmark_mouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
    Dim lnk As Label = sender
    If KeyState.isKeyPressed(Keys.ControlKey) Then
      lnk.DoDragDrop(lnk, DragDropEffects.Move)
    ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
      onNavigate(lnk.Tag)
    ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
      Dim f As New frm_editBookmark
      f.Tag = lnk
      f.txtEditbookmark_text.Text = lnk.Text
      f.txtEditbookmark_url.Text = lnk.Tag
      f.grpEditbookmark.Show()
      f.Top = Me.Top + 100
      f.Left = Me.Left + 200
      f.ShowDialog()
      f.Dispose()
    End If
  End Sub
  Sub readBookmarks()
    If Not IO.File.Exists(ParaService.ProfileFolder + "bookmarks.txt") Then IO.File.WriteAllText(ParaService.ProfileFolder + "bookmarks.txt", "")
    Dim lines() = IO.File.ReadAllLines(ParaService.ProfileFolder + "bookmarks.txt")
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
    IO.File.WriteAllText(ParaService.ProfileFolder + "bookmarks.txt", sb.ToString)

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


  Private Sub flpBookmarks_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles flpBookmarks.DragDrop
    On Error Resume Next
    If e.Data.GetDataPresent("System.Windows.Forms.Label") Then
      e.Effect = DragDropEffects.Move

      Dim lnkSource As Label = e.Data.GetData("System.Windows.Forms.Label")
      Dim p As New Point(e.X - Me.Left - flpBookmarks.Left, 5)

      Dim hoveredCtrl As Control = flpBookmarks.GetChildAtPoint(p, GetChildAtPointSkip.None)
      Dim targetIndex As Integer = flpBookmarks.Controls.GetChildIndex(hoveredCtrl)
      Dim sourceIndex As Integer = flpBookmarks.Controls.GetChildIndex(lnkSource)
      tssl_Filename.Text = targetIndex.ToString + " -> " + sourceIndex.ToString

      Dim out(flpBookmarks.Controls.Count) As String
      For i As Integer = 0 To flpBookmarks.Controls.Count - 1
        out(i) = flpBookmarks.Controls(i).Text + vbTab + flpBookmarks.Controls(i).Tag
        If i = targetIndex Then out(i) = lnkSource.Text + vbTab + lnkSource.Tag + vbNewLine + out(i)
        If i = sourceIndex Then out(i) = ""
      Next

      IO.File.WriteAllText(ParaService.ProfileFolder + "bookmarks.txt", Join(out, vbNewLine))

      readBookmarks()
    End If
  End Sub

  Private Sub flpBookmarks_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles flpBookmarks.DragEnter
    If e.Data.GetDataPresent("System.Windows.Forms.Label") Then
      e.Effect = DragDropEffects.Move
    End If
  End Sub


  'Private Sub pnlErrMes_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
  '  If e.Button = Windows.Forms.MouseButtons.Left Then
  '    'If e.X > pnlErrMes.Width - 25 And e.Y > pnlErrMes.Height - 22 Then
  '    'MVPS.clsFormBorder.moveMeHwnd(pnlErrMes.Handle, WindowsUtilities.HitTestValues.HTBOTTOMRIGHT)
  '    'Else
  '    MVPS.clsFormBorder.moveMeHwnd(pnlErrMes.Handle)
  '    'End If

  '  End If
  'End Sub
  'Private Sub pnlErrMes_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
  '  'If e.X > pnlErrMes.Width - 25 And e.Y > pnlErrMes.Height - 22 Then
  '  'pnlErrMes.Cursor = Cursors.SizeNWSE
  '  'Else
  '  'pnlErrMes.Cursor = Cursors.SizeAll
  '  'End If
  'End Sub

  Private Sub labScriptRunning_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
  Handles labScriptRunning.MouseDown
    If e.Button = Windows.Forms.MouseButtons.Left Then
      m_FormBorder.MoveMe()
    ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
      IsSmallMode = Not IsSmallMode
    End If

  End Sub

  Property IsSmallMode() As Boolean
    Get
      Return ParaService.Glob.para("frm_main__toggleSizeMode", "big") <> "big"
    End Get
    Set(ByVal value As Boolean)
      If ParaService.Glob.para("frm_main__toggleSizeMode", "big") = "big" Then
        If value Then
          ParaService.Glob.saveFormPos(Me, "big")
          ParaService.Glob.readFormPos(Me, True, "small")
          ParaService.Glob.para("frm_main__toggleSizeMode") = "small"
          lnkSettings.Hide() : lnkWindowmanager.Hide() : lnkHelp.Hide() : txtDIZ.Hide()
          DockPanel1.Hide()
        End If
      Else
        If Not value Then
          ParaService.Glob.saveFormPos(Me, "small")
          ParaService.Glob.readFormPos(Me, True, "big")
          ParaService.Glob.para("frm_main__toggleSizeMode") = "big"
          lnkSettings.Show() : lnkWindowmanager.Show() : lnkHelp.Show() : txtDIZ.Show()
          DockPanel1.Show()
        End If
        End If
    End Set
  End Property

  Private Sub IGrid2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

  End Sub

  Private Sub cmenuMain_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles cmenuMain.ItemClicked
    'If TypeOf e.ClickedItem.Tag Is String() Then
    '  Dim dc As DockContent = getDeserializedDockContent("Toolbar|##|" + e.ClickedItem.Tag)
    '  dc.Show() '(Me.DockPanel1)
    'End If
  End Sub

  Private Sub cmenuMain_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmenuMain.Opening
    'Dim lst() = Split(glob.para("scriptWindowList"), "|||")
    'With ScriptWindowToolStripMenuItem
    '  .DropDownItems.Clear()
    '  For Each Nam In lst
    '    If Nam.Trim = "" Then Continue For
    '    .DropDownItems.Add(Nam)
    '  Next
    '  .Enabled = .DropDownItems.Count > 0
    'End With

  End Sub

  Private Sub OptionenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Workbench.ShowOptionsDialog()
  End Sub

  Private Sub lnkHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkHelp.Click
    ' Help.ShowPopup(lnkHelp, "Herzlich willkommen in der Online-Hilfe ..." + vbNewLine + "Zu finden im Programmverzeichnis unter ScriptIDE.chm", Me.PointToScreen(lnkHelp.Location))
    'Help.ShowHelp(Me, "C:\yEXE\ScriptIDE.chm")
    If helpFilePath = "" Then MsgBox("Hilfedatei nicht gefunden!", MsgBoxStyle.Critical, "Fehler") : Exit Sub
    Help.ShowHelp(Me, helpFilePath)
  End Sub

  Private Sub labScriptBreak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

  End Sub

  'Private Sub labCloseErrMes_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
  '  pnlErrMes.Hide()
  'End Sub


  Private Sub LegacyWidgetÖffnenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Dim widgetPath As String
    Using ofd As New OpenFileDialog
      ofd.Filter = "dll|*.dll"
      If ofd.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
      widgetPath = ofd.FileName
    End Using

    Dim frm As New Form With {.Text = "Bitte Typ wählen"}
    Dim lst As New ListBox With {.Dock = DockStyle.Fill}
    frm.Controls.Add(lst)
    AddHandler lst.DoubleClick, Function(sender2 As Object, e2 As EventArgs) Me.Visible = False
    Dim ass = Reflection.Assembly.LoadFile(widgetPath)
    For Each typ In ass.GetTypes
      lst.Items.Add(typ.FullName)
    Next
    frm.ShowDialog()
    Dim typName As String = lst.SelectedItem
    Dim tb As New frmTB_legacyWidget
    tb.txtWidgetfilename.Text = widgetPath
    tb.txtClass.Text = typName
    tb.Show() '(Me.DockPanel1)
    tbLegacyWidget.Add(typName.ToLower, tb)
  End Sub

  Private Sub DockLayoutAlsStandardSpeichernToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DockLayoutAlsStandardSpeichernToolStripMenuItem.Click
    DockPanel1.SaveAsXml(ParaService.ProfileFolder + "dockLayout_default.xml")
  End Sub

  Private Sub DockLayoutStandardWiederherstellenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DockLayoutStandardWiederherstellenToolStripMenuItem.Click
    DockPanel1.LoadFromXml(ParaService.ProfileFolder + "dockLayout_default.xml", AddressOf getDeserializedDockContent)
  End Sub

  Private Sub ScriptWindowToolStripMenuItem_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
    On Error Resume Next
    Dim winID = e.ClickedItem.Text
    getDeserializedDockContent("ToolBar|##|tbScriptWin|##|" + winID)
  End Sub

  Private Sub DockPanel1_ActiveDocumentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DockPanel1.ActiveDocumentChanged
    TT.Write("Event", "DockPanel1_ActiveDocumentChanged", "event")
    If skipMdiActivationEvent = False AndAlso DockPanel1.ActiveDocument IsNot Nothing Then
      Dim dockCont As DockContent = DockPanel1.ActiveDocument
      setActRtfBox(dockCont)
    End If
  End Sub


  'Private Sub DockPanel1_ActiveContentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DockPanel1.ActiveContentChanged
  '  TT.Write("Event", "DockPanel1_ActiveContentChanged")
  'End Sub

  'Private Sub DockPanel1_ActivePaneChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DockPanel1.ActivePaneChanged
  '  TT.Write("Event", "DockPanel1_ActivePaneChanged")
  'End Sub

  Private Sub lnkWindowmanager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkWindowmanager.Click
    cls_IDEHelper.Instance.ShowOptionsDialog()
  End Sub

  'Private Sub Label1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
  '  MVPS.clsFormBorder.moveMeHwnd(pnlErrMes.Handle, WindowsUtilities.HitTestValues.HTBOTTOMRIGHT)
  'End Sub

  Private Sub AddinManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    cls_IDEHelper.Instance.ShowOptionsDialog("addins")
  End Sub

  'Private Sub KleineTitelleisteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KleineTitelleisteToolStripMenuItem.Click
  '  If KleineTitelleisteToolStripMenuItem.Checked Then
  '    showHideToolbar(CheckState.Checked)
  '  Else
  '    showHideToolbar(CheckState.Unchecked)
  '  End If
  'End Sub

  'Private Sub BookmarksAnzeigenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookmarksAnzeigenToolStripMenuItem.Click
  '  If BookmarksAnzeigenToolStripMenuItem.Checked Then
  '    showHideToolbar(, CheckState.Unchecked)
  '  Else
  '    showHideToolbar(, CheckState.Checked)
  '  End If
  'End Sub

  Private Sub labClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles labClose.Click
    Me.Close()
  End Sub

  Private Sub labMaximize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles labMaximize.Click
    If Me.WindowState = FormWindowState.Maximized Then
      Me.WindowState = FormWindowState.Normal
    Else
      Me.WindowState = FormWindowState.Maximized
    End If
  End Sub

  Private Sub labMinimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles labMinimize.Click
    Me.WindowState = FormWindowState.Minimized
  End Sub

  Private Sub labClose_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles labMinimize.MouseEnter, labMaximize.MouseEnter, labClose.MouseEnter
    sender.foreColor = cls_IDEHelper.Instance.Skin.TitleBarForeColor1 ' Color.PaleTurquoise
  End Sub

  Private Sub labClose_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles labMinimize.MouseLeave, labMaximize.MouseLeave, labClose.MouseLeave
    sender.foreColor = cls_IDEHelper.Instance.Skin.TitleBarForeColor2 'Color.Gainsboro
  End Sub

  Private Sub pnlFileinfo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlFileinfo.Paint
    Dim rect As New Rectangle(0, 0, 32, pnlFileinfo.Height)
    Dim bsh As New Drawing2D.LinearGradientBrush(rect, Color.DimGray, Color.DarkGray, Drawing2D.LinearGradientMode.ForwardDiagonal)
    e.Graphics.FillRectangle(bsh, rect)

  End Sub

  'Private Sub DockPanel1_ActiveContentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DockPanel1.ActiveContentChanged
  '  TT.Write("DockPanel1_ActiveContentChanged", "", "event")

  'End Sub

  'Private Sub DockPanel1_ContentAdded(ByVal sender As Object, ByVal e As WeifenLuo.WinFormsUI.Docking.DockContentEventArgs) Handles DockPanel1.ContentAdded
  '  TT.Write("DockPanel1_ContentAdded", e.Content.DockHandler.TabText, "event")

  'End Sub

  'Private Sub DockPanel1_ContentRemoved(ByVal sender As Object, ByVal e As WeifenLuo.WinFormsUI.Docking.DockContentEventArgs) Handles DockPanel1.ContentRemoved
  '  TT.Write("DockPanel1_ContentRemoved", e.Content.DockHandler.TabText, "event")

  'End Sub

  Private Sub lnkTraceMon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkTraceMon.Click
    Try
      Process.Start("traceMonitor.exe")
    Catch ex As Exception
      MsgBox("Trace-Monitor konnte nicht gestartet werden." + vbNewLine + vbNewLine + ex.Message, MsgBoxStyle.Critical, "Fehler")
    End Try
  End Sub
End Class
