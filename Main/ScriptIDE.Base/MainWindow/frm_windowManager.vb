
Public Class frm_windowManager
  Dim reloadClbOnMouseUp As Boolean = False

  Dim loadedPropPag As IPropertyPage
  Dim actPageHeader, actPageBreadcrump As String, actPageIcon As Integer
  Dim pageHeadFont1, pageHeadFont2, pageHeadFont3 As Font

  Dim isVistaDesign As Boolean

  Enum OptTabs
    WindowMan
    AddIns
    ScriptAutoStart
    Toolbars
    AddInOptions
    WinSwitcher = 6
  End Enum

  Private Sub frm_windowManager_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    savePropertyPage()
    ParaService.Glob.saveFormPos(Me)
  End Sub

  Private Sub frm_configLegacyWidget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    On Error Resume Next
    ParaService.Glob.readFormPos(Me)
    readWindowList()
    'TabControl1.Top = -21
    refreshOptionTree()
    TreeView2.SelectedNode = TreeView2.Nodes(0)
    pnlTitleBar.BringToFront()
    If IsGlassEnabled() And Not KeyState.isKeyPressed(Keys.Menu) Then
      pnlTitleBar.BackColor = Color.Black
      AddGlassToWin(Me.Handle, 0, 0, Me.pnlTitleBar.Height, 0)
      hideTitleBarContents(Me.Handle)
      isVistaDesign = True
      pageHeadFont1 = New Font("Segoe UI", 14, FontStyle.Bold, GraphicsUnit.Point)
      pageHeadFont2 = New Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point)
      pageHeadFont3 = New Font("Segoe UI", 10, FontStyle.Underline, GraphicsUnit.Point)
    Else
      pageHeadFont1 = New Font("Microsoft Sans Serif", 14, FontStyle.Bold, GraphicsUnit.Point)
      pageHeadFont2 = New Font("Microsoft Sans Serif", 9, FontStyle.Regular, GraphicsUnit.Point)
      pageHeadFont3 = New Font("Microsoft Sans Serif", 10, FontStyle.Underline, GraphicsUnit.Point)
    End If

    scWinSwitch.Text = IO.File.ReadAllText(ParaService.ProfileFolder + "winSwitcher.txt")

    Label1.Text = "Programmversion:    scriptIDE " + My.Application.Info.Version.ToString(3)

  End Sub

  Private Sub pnlTitleBar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlTitleBar.MouseDown
    If e.X > 18 And e.X < 100 And e.Y < 29 Then
      'navigateOptions("home")
      If TreeView2.SelectedNode Is Nothing OrElse TreeView2.SelectedNode.Name = "home" Then
      ElseIf TreeView2.SelectedNode.Parent Is Nothing Then
        navigateOptions("home")
      Else
        TreeView2.SelectedNode = TreeView2.SelectedNode.Parent
      End If
    Else
      MVPS.clsFormBorder.moveMeHwnd(Me.Handle)
    End If
  End Sub

  Private Sub pnlTitleBar_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlTitleBar.MouseMove
    Dim isBtnHover = e.X > 18 AndAlso e.X < 100 AndAlso e.Y > 2 AndAlso e.Y < 29 AndAlso Not String.IsNullOrEmpty(actPageBreadcrump)
    Dim newCursor = If(isBtnHover, Cursors.Hand, Cursors.Default)
    If pnlTitleBar.Cursor <> newCursor Then

      pnlTitleBar.Cursor = newCursor
      pnlTitleBar.Invalidate(New Rectangle(0, 0, 105, 30))
    End If
  End Sub

  Private Sub pnlTitleBar_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlTitleBar.Paint
    'e.Graphics.DrawImage(Me.Icon, New Rectangle(4, 6, 16, 16))
    On Error Resume Next
    If Not isVistaDesign Then
      Dim sizeRect As New Rectangle(0, 0, pnlTitleBar.Width, pnlTitleBar.Height)
      e.Graphics.FillRectangle(New Drawing2D.LinearGradientBrush(sizeRect, _
        Color.FromArgb(255, 82, 134, 161), Color.FromArgb(255, 82, 159, 88), Drawing2D.LinearGradientMode.Horizontal), sizeRect)
    End If

    If actPageIcon > -1 Then imlTabImages.Draw(e.Graphics, 160, 1, actPageIcon)

    'Dim rend As New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Button.PushButton.Default)
    Dim pbs = VisualStyles.PushButtonState.Normal
    If pnlTitleBar.Cursor = Cursors.Hand Then pbs = VisualStyles.PushButtonState.Hot
    If String.IsNullOrEmpty(actPageBreadcrump) Then pbs = VisualStyles.PushButtonState.Disabled

    ButtonRenderer.DrawButton(e.Graphics, New Rectangle(20, 2, 85, 27), False, pbs)
    e.Graphics.DrawString("Zurück", pageHeadFont2, If(pbs = VisualStyles.PushButtonState.Disabled, New SolidBrush(Color.FromArgb(255, 111, 111, 111)), Brushes.Black), 51, 7)

    e.Graphics.DrawImage(My.Resources.Back, 30, 6, 18, 19)
    'If actPageIcon IsNot Nothing Then _
    '  e.Graphics.DrawImage(actPageIcon, 160, 1, 32, 32)

    'DrawTextGlow(e.Graphics, MAIN.lblGlobAktFileSpec.Text, sender.Font, New Rectangle(Left + 15, 9, MAIN.Width - 100, 40), Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine, False)

    If isVistaDesign Then
      'DrawTextGlow(e.Graphics, "Home", pageHeadFont3, New Rectangle(55, 4, 50, 10), Color.Blue, TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine, False)
      If String.IsNullOrEmpty(actPageHeader) Then
        DrawTextGlow(e.Graphics, "ScriptIDE2 Konfiguration > " + actPageBreadcrump, pageHeadFont2, New Rectangle(200, 18, Me.Width - 210, 10), Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine, False)
      Else
        DrawTextGlow(e.Graphics, actPageHeader, pageHeadFont1, New Rectangle(200, 12, Me.Width - 210, 18), Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine, False)
        DrawTextGlow(e.Graphics, "ScriptIDE2 Konfiguration > " + actPageBreadcrump, pageHeadFont2, New Rectangle(200, -1, Me.Width - 210, 10), Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine, False)
      End If
    Else
      'e.Graphics.DrawString("Home", pageHeadFont3, Brushes.Blue, 50, 10)
      e.Graphics.DrawString("ScriptIDE2 Konfiguration > " + actPageBreadcrump, pageHeadFont2, Brushes.Black, 200, 1)
      e.Graphics.DrawString(actPageHeader, pageHeadFont1, Brushes.Black, 200, 14)
    End If

  End Sub

  Sub addPropertyPage(ByVal parentPath As String, ByVal id As String, ByVal caption As String, ByVal tag As Object, ByVal icon As Image)
    Dim path() = Split(parentPath, "/")
    Dim mainNode = getNodeByPath(TreeView2, path)
    mainNode.Nodes.Add(id.ToLower, path(path.Length - 1), id.ToLower).Tag = tag
    imlTabImages.Images.Add(id.ToLower, icon)
  End Sub

  Sub refreshOptionTree()
    Dim mainNode As TreeNode
    TreeView2.Nodes.Clear() : imlTabImages.Images.Clear()

    imlTabImages.Images.Add("FOLDER", My.Resources.Folder32)

    addPropertyPage("Home", "Home", "", "intTab|##|7", My.Resources.home)

    addPropertyPage("Allgemein/Fensterverwaltung", "Fensterverwaltung", "", "intTab|##|0", My.Resources.optTab0)
    addPropertyPage("Allgemein/WinSwitcher", "WinSwitcher", "", "intTab|##|6", My.Resources.optTab6)
    
    addPropertyPage("Erweiterungen/AddIns", "Add-ins", "", "intTab|##|1", My.Resources.optTab1)


    'For Each inst In AddinInstance.Addins
    '  addPropertyPage("AddIn-Optionen", inst.AddinName, inst.AddinName, "legPropPag|##|" + inst.AddinName, inst.Icon.ToBitmap())
    'Next
    Dim img As Image
    Dim propPages = AddInTree.GetTreeNode("/OptionsDialog/Pages", False)
    If propPages IsNot Nothing Then
      For Each propPag In propPages.Codons
        Try
          TT.Write("PropPage", propPag.Id, "dump")
          If String.IsNullOrEmpty(propPag.Properties("icon")) = False Then
            img = ResourceLoader.GetImageCached(propPag.Properties("icon"))
          ElseIf propPag.AddIn.HasIcon Then
            img = propPag.AddIn.Icon.ToBitmap()
          Else
            img = My.Resources.propertyPage
          End If

          addPropertyPage(propPag.Properties("path"), propPag.Id, propPag.Properties("name"), propPag, img)
        Catch ex As Exception
          TT.Write("Error loading PropertyPage ", propPag.Id, "err")
        End Try
      Next
    End If
    lvHome.Items.Clear()
    lvHome.SmallImageList = imlTabImages
    lvHome.LargeImageList = imlTabImages
    For Each nod As TreeNode In TreeView2.Nodes
      If nod.Nodes.Count = 0 Then Continue For
      Dim lvi = lvHome.Items.Add(nod.Name, nod.Text, nod.Nodes(0).Name)
      Select Case nod.Nodes.Count
        Case 0
        Case 1 : lvi.SubItems.Add(nod.Nodes(0).Text)
        Case 2 : lvi.SubItems.Add(nod.Nodes(0).Text + ", " + nod.Nodes(1).Text)
        Case Else : lvi.SubItems.Add(nod.Nodes(0).Text + ", " + nod.Nodes(1).Text + ", ...")
      End Select

    Next

    TreeView2.ExpandAll()
  End Sub
  Function getNodeByPath(ByVal lst As TreeView, ByVal path() As String) As Object
    Dim nc As Object = lst
    If path.Length = 0 OrElse path(0) = "" Then Return nc
    For i = 0 To path.Length - 2 'ACHTUNG - der letzte ITEM wird geskippt
      If nc.Nodes.ContainsKey(path(i).ToLower) Then
        nc = nc.Nodes(path(i).ToLower)
      Else
        nc = nc.Nodes.Add(path(i).ToLower, path(i), "FOLDER")
      End If
    Next
    Return nc
  End Function
  Private Sub TreeView2_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView2.AfterSelect
    On Error Resume Next

    savePropertyPage()
    actPageIcon = imlTabImages.Images.IndexOfKey(e.Node.ImageKey)
    Me.Text = "Konfiguration    " + e.Node.Name
    actPageBreadcrump = ""
    Dim nod = e.Node.Parent
    While nod IsNot Nothing
      actPageBreadcrump = nod.Text + " > " + actPageBreadcrump
      nod = nod.Parent
    End While

    If e.Node.Tag Is Nothing AndAlso e.Node.Nodes.Count > 0 Then
      TabControl1.SelectedIndex = 5
      lvGroupHome.Items.Clear()
      lvGroupHome.LargeImageList = imlTabImages
      For Each nod In e.Node.Nodes
        lvGroupHome.Items.Add(nod.Name, nod.Text, nod.ImageKey)
      Next
      actPageHeader = ""
      actPageBreadcrump += e.Node.Text + " > "
      pnlTitleBar.Refresh()
      Exit Sub
    End If

    If TypeOf e.Node.Tag Is Codon Then
      Dim propPag As Codon = e.Node.Tag
      TabControl1.SelectedIndex = OptTabs.AddInOptions
      actPageHeader = propPag.Properties("name")

      ScriptedPanel1.resetControls()
      Dim uc As IPropertyPage = propPag.BuildItem(Me, Nothing) 'Activator.CreateInstance(propPag.ucType)
      ScriptedPanel1.Controls.Add(uc)
      DirectCast(uc, Control).Dock = DockStyle.Fill
      Err.Clear()
      uc.readProperties()
      If Err.Number <> 0 Then MsgBox("Fehler beim Laden der Eigenschaftenseite " + propPag.Id + ", in readProperties ist eine unbehandelte Ausnahme aufgetreten:" + vbNewLine + Err.Description, MsgBoxStyle.Critical, "Konfiguration")
      loadedPropPag = uc
      pnlTitleBar.Refresh()
      Exit Sub
    End If

    Dim para() = Split(e.Node.Tag, "|##|")
    If para(0) = "intTab" Then
      TabControl1.SelectedIndex = para(1)
      onInternalTabSelected()
    End If

    'If para(0) = "legPropPag" Then
    '  TabControl1.SelectedIndex = OptTabs.AddInOptions
    '  actPageHeader = "Addinoptionen für " + para(1)

    '  Dim inst = AddinInstance.GetAddinInstance(para(1))
    '  ScriptedPanel1.resetControls()
    '  If inst.Loaded Then
    '    inst.ConnectRef.PopulateOptionPanel(ScriptedPanel1)
    '  Else
    '    ScriptedPanel1.addLabel("laberror", "Das Addin ist zur Zeit nicht geladen. Um es zu laden, klicke auf AddIns, und markiere die Checkbox hinter dem entsprechenden AddIn.", "#fea", , 10, 10, 300, 200)
    '  End If
    '  pnlTitleBar.Refresh()
    'End If

  End Sub

  Sub onInternalTabSelected()
    Select Case CType(TabControl1.SelectedIndex, OptTabs)
      Case 7
        actPageHeader = "Willkommen!"

      Case OptTabs.WindowMan
        readWindowList()
        actPageHeader = "Fenster-Manager"

      Case OptTabs.AddIns
        readAddinList()
        actPageHeader = "Erweiterungen: AddIns"

        'Case OptTabs.ScriptAutoStart
        '  'readAutostartList()
        '  readCommandList()
        '  readToolbarList()
        '  actPageHeader = "Toolbars"

        'Case OptTabs.Toolbars
        '  actPageHeader = "Toolbars"
        '  'ListBox1.Items.Clear()
        '  'For Each tb As Control In MAIN.flpToolbar.Controls
        '  '  If TypeOf tb Is IScriptedPanel Then
        '  '    If TypeOf tb Is ScriptedPanel Then
        '  '      ListBox1.Items.Add("Panel" + vbTab + DirectCast(tb, IScriptedPanel).winID)
        '  '    End If
        '  '    If TypeOf tb Is ScriptedToolstrip Then
        '  '      ListBox1.Items.Add("ToolStrip" + vbTab + DirectCast(tb, IScriptedPanel).winID)
        '  '    End If
        '  '  End If
        '  'Next

      Case OptTabs.WinSwitcher
        actPageHeader = "Fenster-Schnellumschaltung"

    End Select
    pnlTitleBar.Refresh()
  End Sub

  Sub savePropertyPage()
    If loadedPropPag IsNot Nothing Then
      loadedPropPag.saveProperties()
      loadedPropPag = Nothing
    End If
  End Sub

  Sub readWindowList()
    On Error Resume Next
    TreeView1.Nodes.Clear()

    Dim mainNode, subNode1 As TreeNode
    mainNode = TreeView1.Nodes.Add("", "Legacy Widgets", "mwsidebar", "mwsidebar")
    mainNode.Nodes.Add("legacyWidgetDummy")

    'mainNode = TreeView1.Nodes.Add("", "Addin Windows", "devenv", "devenv")
    'For Each winID In tbAddinWin
    '  If winID = "" Then Continue For
    '  subNode1 = mainNode.Nodes.Add(winID, winID.Substring(winID.IndexOf("|##|") + 4), "defaultwindow", "defaultwindow")
    'Next

    mainNode = TreeView1.Nodes.Add("", "Script Windows", "scriptwindows2", "scriptwindows2")
    For Each winID In Split(ParaService.Glob.para("scriptWindowList"), "|||")
      If winID = "" Then Continue For
      subNode1 = mainNode.Nodes.Add("ToolBar|##|tbScriptWin|##|" + winID, winID, "form", "form")
    Next

    TreeView1.Nodes.Add("ToolBar|##|tbDebug", "Debug/Test", "exc", "exc")
    TreeView1.Nodes.Add("ToolBar|##|tbErrorList", "Kompilerfehler", "exc", "exc")

    'mainNode = TreeView1.Nodes.Add("", "Addin Windows", "devenv", "devenv")
    Dim path = AddInTree.GetTreeNode("/Workspace/ToolWindows")
    If path IsNot Nothing Then
      For Each cod In path.Codons
        Dim text = cod.Properties("name")
        If String.IsNullOrEmpty(text) Then text = cod.Id
        mainNode = TreeView1.Nodes.Add("Addin|##|" + cod.AddIn.ID + "|##|" + cod.Id, text, "defaultwindow", "defaultwindow")
      Next
    End If


    lvOpenedWins.Items.Clear()
    For Each win As Object In Workbench.Instance.DockPanel1.Contents
      Dim lvi = lvOpenedWins.Items.Add(win.Text)
      lvi.subitems.add(win.GetPersistString())
      lvi.subitems.add(win.DockState)
      lvi.subitems.add(win.GetType.Name)
      lvi.tag = win
    Next




    'TreeView1.Nodes.Add("ToolBar|##|tbConsole", "Konsole", "cmd", "cmd")
    ''TreeView1.Nodes.Add("ToolBar|##|tbfileexplorer", "Lokale Ordner", "explore", "explore")
    ''TreeView1.Nodes.Add("ToolBar|##|tbftpexplorer", "FTB-Explorer", "explore", "explore")
    'TreeView1.Nodes.Add("ToolBar|##|tbGlobSearch", "GlobSearch", "defaultwindow", "defaultwindow")
    'TreeView1.Nodes.Add("ToolBar|##|tbIndexList", "IndexList", "defaultwindow", "defaultwindow")
    'TreeView1.Nodes.Add("ToolBar|##|tbOpenedFiles", "OpenedFiles", "defaultwindow", "defaultwindow")
    'TreeView1.Nodes.Add("ToolBar|##|tbRtfFilelist", "RtfFilelist", "explore", "explore")
    'TreeView1.Nodes.Add("ToolBar|##|tbtraceprintline", "TracePrintLine", "table", "table")
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
    openSelectedWindow()
    'Me.Hide()
  End Sub


  Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
    TextBox1.Text = TreeView1.SelectedNode.Name

  End Sub

  Private Sub TreeView1_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView1.BeforeExpand
    On Error Resume Next
    If e.Node.Text = "Legacy Widgets" AndAlso e.Node.Nodes(0).Text = "legacyWidgetDummy" Then

      Dim files() = IO.Directory.GetFiles("C:\yPara\mwSidebar\widgets\", "*.dll")
      Dim mainNode, subNode1 As TreeNode
      mainNode = e.Node
      mainNode.Nodes.Clear()

      Dim ass As Reflection.Assembly
      For Each fileSpec In files
        subNode1 = Nothing : ass = Nothing
        ass = Reflection.Assembly.LoadFile(fileSpec)
        If ass Is Nothing Then Continue For
        For Each typ In ass.GetTypes
          If typ.Name.StartsWith("sw_") = False And typ.Name.StartsWith("sg_") = False Then Continue For
          If subNode1 Is Nothing Then
            subNode1 = mainNode.Nodes.Add("", IO.Path.GetFileName(fileSpec), "dll", "dll")
          End If
          Dim key = "ToolBar|##|tbLegacyWidget|##|" + fileSpec + "|##|" + typ.FullName
          subNode1.Nodes.Add(key, typ.FullName, "class", "class")
        Next
      Next
    End If
  End Sub

  Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
    If e.Button = Windows.Forms.MouseButtons.Right Then
      TreeView1.SelectedNode = e.Node
      openSelectedWindow()
    End If
  End Sub

  Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
    If TextBox1.Text = "" Then Exit Sub
    openSelectedWindow()
    'Me.Hide()
  End Sub

  Private Sub lnkOpenOwnWindow_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkOpenOwnWindow.LinkClicked
    On Error Resume Next
    ' Dim frm As DockContent = getDeserializedDockContent(TextBox1.Text)
    'frm.Close()
    'frm.Dispose()

    Dim frm2 As DockContent = getDeserializedDockContent(TextBox1.Text)
    frm2.Show()
    frm2.DockHandler.IsFloat = True
    '  MAIN.DockPanel1.
  End Sub

  Private Sub LinkLabel2_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
    Dim frm2 As DockContent = getDeserializedDockContent(TextBox1.Text)
    frm2.Show()
    frm2.DockHandler.IsFloat = True
    frm2.DockState = DockState.Float
    frm2.DockPanel = Workbench.Instance.DockPanel1
    frm2.Visible = True
    frm2.Top = 100
    frm2.Left = 300
    frm2.Width = 200
    frm2.Height = 450
    frm2.Activate()
  End Sub

  Sub openSelectedWindow()
    On Error Resume Next
    Dim frm = getDeserializedDockContent(TextBox1.Text)

    frm.Show() '(MAIN.DockPanel1)

  End Sub

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    readWindowList()

  End Sub




  'Private Sub btnAddFtpCred_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFtpCred.Click
  '  igFtpCred.Rows.Add()

  'End Sub

  'Private Sub btnDelFtpCred_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelFtpCred.Click
  '  On Error Resume Next
  '  igFtpCred.Rows.RemoveAt(igFtpCred.CurRow.Index)
  'End Sub

  'Private Sub btnSaveFtpCred_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFtpCred.Click
  '  On Error Resume Next

  '  Dim sdata As String
  '  Igrid_get(igFtpCred, sdata, "$$", "§§")
  '  glob.para("frm_main__thefile") = sdata

  '  readFtpConnections()
  '  Me.Close()

  'End Sub


  Sub readAddinList()
    igAddIns.Rows.Clear()
    imlAddinIcons.Images.Clear()

    Dim dirList() As String

    dirList = IO.Directory.GetFiles(ParaService.AppPath, "*.AddIn", IO.SearchOption.TopDirectoryOnly)
    For Each fileSpec As String In dirList
      Dim ir = igAddIns.Rows.Add()
      Dim addin As AddinInstance
      Dim id As String = LCase(IO.Path.GetFileNameWithoutExtension(fileSpec))
      ir.Cells(1).Value = fileSpec
      addin = AddinInstance.GetAddinInstance(id)
      If addin Is Nothing Then
        ir.Cells(0).Value = IO.Path.GetFileName(fileSpec)
        ir.Cells(2).Value = ".Net"
        ir.Cells(4).Value = "Nicht geladen"
      Else
        ir.Cells(0).Value = addin.DisplayName + " (" + IO.Path.GetFileName(fileSpec) + ")"
        ir.Cells(2).Value = If(addin.Properties("addInManagerHidden") = "true", "System", ".Net")
        ir.Cells(4).Value = "Geladen"
        ir.Tag = addin
      End If
      ir.Cells(3).Value = ParaService.Glob.para("addinState__" + id, "FALSE") = "TRUE"
    Next

    dirList = IO.Directory.GetFiles(ParaService.SettingsFolder + "addIns", "*.nsla", IO.SearchOption.TopDirectoryOnly)
    For Each fileSpec As String In dirList
      Dim ir = igAddIns.Rows.Add()
      Dim addin As AddinInstance
      Dim id As String = LCase(IO.Path.GetFileNameWithoutExtension(fileSpec))
      ir.Cells(1).Value = fileSpec
      addin = AddinInstance.GetAddinInstance(id)
      ir.Cells(0).Value = IO.Path.GetFileName(fileSpec)
      ir.Cells(2).Value = "Lua"
      If addin Is Nothing Then
        ir.Cells(4).Value = "Nicht geladen"
      Else
        ir.Cells(4).Value = "Geladen"
        ir.Tag = addin
      End If
      ir.Cells(3).Value = ParaService.Glob.para("addinState__" + id, "FALSE") = "TRUE"
    Next

  End Sub

  Private Sub btnAddAddin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAddAddin.Click
    
  End Sub

  Private Sub igAddIns_AfterCommitEdit(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGAfterCommitEditEventArgs) Handles igAddIns.AfterCommitEdit
    commitEdit(e.RowIndex, e.ColIndex)
  End Sub

  Sub commitEdit(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
    If ColIndex = 3 Then
      'laden/entladen
      Dim newVal As Boolean = igAddIns.Cells(RowIndex, 3).Value
      Dim fileSpec As String = igAddIns.Cells(RowIndex, 1).Value
      Dim id As String = LCase(IO.Path.GetFileNameWithoutExtension(fileSpec))

      ParaService.Glob.para("addinState__" + id) = If(newVal, "TRUE", "FALSE")

      Dim inst As AddinInstance = igAddIns.Rows(RowIndex).Tag

      If igAddIns.Cells(RowIndex, 2).Value = "Lua" Then
        If newVal Then
          AddinInstance.ConnectLuaAddin(fileSpec, ConnectMode.AfterStartup)
        Else
          AddinInstance.RemoveAddIn(id)
        End If
      End If

      readAddinList()
    End If
  End Sub

  Private Sub igAddIns_CellMouseUp(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellMouseUpEventArgs) Handles igAddIns.CellMouseUp
    'If e.Button = Windows.Forms.MouseButtons.Right Then
    '  igAddIns.SetCurRow(e.RowIndex)
    '  If igAddIns.Cells(e.RowIndex, 1).Value = True Then
    '    AktivierenToolStripMenuItem.Text = "Addin deaktivieren"
    '    LoeschenToolStripMenuItem.Enabled = False
    '  Else
    '    AktivierenToolStripMenuItem.Text = "Addin aktivieren"
    '    LoeschenToolStripMenuItem.Enabled = True
    '  End If
    '  cmenu_Addin.Show(sender, e.MousePos)

    'End If
  End Sub


  'Private Sub clbScriptClsAutostart_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs)
  '  Static skipMe As Boolean = False
  '  If skipMe Then Exit Sub

  '  Dim out(clbScriptClsAutostart.CheckedItems.Count) As String
  '  For i = 0 To clbScriptClsAutostart.CheckedItems.Count - 1
  '    out(i) = clbScriptClsAutostart.CheckedItems(i)
  '  Next
  '  If e.NewValue Then
  '    out(clbScriptClsAutostart.CheckedItems.Count) = clbScriptClsAutostart.Items(e.Index)
  '  Else
  '    For i = 0 To out.Length - 1
  '      If out(i) = clbScriptClsAutostart.Items(e.Index) Then out(i) = ""
  '    Next
  '  End If
  '  glob.para("scriptClsAutostart") = Join(out, "|##|")
  '  reloadClbOnMouseUp = True

  'End Sub

  'Private Sub clbScriptClsAutostart_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
  '  If reloadClbOnMouseUp Then
  '    Dim selFilename = clbScriptClsAutostart.SelectedItem
  '    readAutostartList()
  '    For i = 0 To clbScriptClsAutostart.Items.Count - 1
  '      If clbScriptClsAutostart.Items(i) = selFilename Then clbScriptClsAutostart.SelectedIndex = i : Exit For
  '    Next
  '    reloadClbOnMouseUp = False
  '  End If
  'End Sub

  'Private Sub clbScriptClsAutostart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  Button3.Enabled = clbScriptClsAutostart.SelectedIndex > 0 AndAlso _
  '                    clbScriptClsAutostart.GetItemChecked(clbScriptClsAutostart.SelectedIndex)
  '  Button4.Enabled = clbScriptClsAutostart.SelectedIndex > -1 AndAlso _
  '                    clbScriptClsAutostart.SelectedIndex < clbScriptClsAutostart.Items.Count - 1 AndAlso _
  '                    clbScriptClsAutostart.GetItemChecked(clbScriptClsAutostart.SelectedIndex) AndAlso _
  '                    clbScriptClsAutostart.GetItemChecked(clbScriptClsAutostart.SelectedIndex + 1)
  'End Sub

  'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  If clbScriptClsAutostart.SelectedIndex < 1 Then Exit Sub
  '  swapItems(clbScriptClsAutostart.SelectedIndex, clbScriptClsAutostart.SelectedIndex - 1)
  '  clbScriptClsAutostart.SelectedIndex -= 1
  'End Sub
  'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  If clbScriptClsAutostart.SelectedIndex < 0 Or clbScriptClsAutostart.SelectedIndex > clbScriptClsAutostart.Items.Count - 2 Then Exit Sub
  '  swapItems(clbScriptClsAutostart.SelectedIndex, clbScriptClsAutostart.SelectedIndex + 1)
  '  clbScriptClsAutostart.SelectedIndex += 1
  'End Sub

  'Sub swapItems(ByVal idx1 As Integer, ByVal idx2 As Integer)
  '  Dim t1 As String ', c1 As Boolean
  '  If clbScriptClsAutostart.GetItemChecked(idx1) = False Or clbScriptClsAutostart.GetItemChecked(idx2) = False Then Exit Sub

  '  ' c1 = clbScriptClsAutostart.GetItemChecked(idx1)
  '  t1 = clbScriptClsAutostart.Items(idx1)
  '  ' clbScriptClsAutostart.SetItemChecked(idx1, clbScriptClsAutostart.GetItemChecked(idx2))
  '  clbScriptClsAutostart.Items(idx1) = clbScriptClsAutostart.Items(idx2)
  '  ' clbScriptClsAutostart.SetItemChecked(idx2, c1)
  '  clbScriptClsAutostart.Items(idx2) = t1
  'End Sub

  'Private Sub btnClosetb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  Dim parts() = Split(ListBox1.SelectedItem, vbTab)
  '  If parts.Length < 2 Then Exit Sub

  '  cls_IDEHelper.Instance.RemoveToolbar(parts(1))

  'End Sub

  'Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  Dim parts() = Split(ListBox1.SelectedItem, vbTab)
  '  If parts.Length < 2 Then Exit Sub

  '  Dim tb = cls_IDEHelper.Instance.CreateToolbar(parts(1))
  '  CType(tb, Control).Show()
  '  Workbench.Instance.showHideToolbar()
  'End Sub

  'Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  Dim parts() = Split(ListBox1.SelectedItem, vbTab)
  '  If parts.Length < 2 Then Exit Sub

  '  Dim tb = cls_IDEHelper.Instance.CreateToolbar(parts(1))
  '  CType(tb, Control).Visible = False
  '  Workbench.Instance.showHideToolbar()
  'End Sub

  Private Sub igAddIns_CurRowChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles igAddIns.CurRowChanged
    If igAddIns.CurRow Is Nothing Then Exit Sub
    Dim addin As AddinInstance = igAddIns.CurRow.Tag
    If addin Is Nothing Then
      grpAddinDetails.Visible = False
    Else
      grpAddinDetails.Visible = True
      grpAddinDetails.Text = addin.DisplayName
      labAddinAuthor.Text = "Autor: " + addin.Properties("author")
      labAddinAuthor.Left = grpAddinDetails.Width - labAddinAuthor.Width - 30
      labAddinDetails.Text = addin.Properties("description") + vbNewLine + _
                             "XMLFileSpec: " + vbTab + addin.XMLFileSpec + vbNewLine + _
                             "Homepage: " + vbTab + addin.Properties("url") + vbNewLine

    End If
  End Sub


  'Private Sub igAddIns_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles igAddIns.DragDrop
  '  If e.Data.GetDataPresent("FileDrop") Then
  '    Dim fileSpecs() As String = e.Data.GetData("FileDrop")
  '    For Each file In fileSpecs
  '      Dim inst = AddinInstance.ConnectFromFile(file, False)
  '      If inst IsNot Nothing Then inst.LoadOnStart = True
  '      AddinInstance.SaveAddinList()
  '      readAddinList()
  '      refreshOptionTree()
  '    Next
  '  End If
  'End Sub

  'Private Sub igAddIns_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles igAddIns.DragEnter
  '  If e.Data.GetDataPresent("FileDrop") Then
  '    e.Effect = DragDropEffects.Link

  '  End If
  'End Sub

  'Private Sub LoeschenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  AddinInstance.Addins.Remove(igAddIns.CurRow.Tag)
  '  'For i = 0 To AddinInstance.Addins.Count - 1
  '  '  If AddinInstance.Addins(i).AddinName = igAddIns.CurRow.Tag Then
  '  'Next
  '  igAddIns.Rows.RemoveAt(igAddIns.CurRow.Index)
  '  AddinInstance.SaveAddinList()
  '  refreshOptionTree()
  'End Sub

  'Private Sub AktivierenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  igAddIns.CurRow.Cells(1).Value = Not igAddIns.CurRow.Cells(1).Value
  '  commitEdit(igAddIns.CurRow.Index, 1)
  '  refreshOptionTree()
  'End Sub


  Sub recWalkNodesList(ByVal nc As TreeNodeCollection, ByRef out As String, ByVal ind As String)
    For Each nod As TreeNode In nc
      out += ind + nod.Name + vbNewLine
      If nod.Nodes.Count > 0 Then recWalkNodesList(nod.Nodes, out, ind + "   ")
    Next
  End Sub
  Sub navigateOptions(ByVal key As String)
    'Dim cat As String = "Tab|##|" & index
    'If Not String.IsNullOrEmpty(subCat) Then cat += "|##|" + subCat
    Dim tvn = TreeView2.Nodes.Find(key, True)
    If tvn.Length = 0 Then
      Dim out As String = "" : recWalkNodesList(TreeView2.Nodes, out, "")
      MsgBox("Treenode """ + key + """ nicht gefunden. Verfügbare Knoten:" + vbNewLine + out, , "Konfiguration")
    Else
      TreeView2.SelectedNode = tvn(0)
    End If
  End Sub

  Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    navHelpByKeyword("addins")
  End Sub

  Private Sub tbpAddinOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbpAddinOptions.Click

  End Sub

  Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    IO.File.WriteAllText(ParaService.ProfileFolder + "winSwitcher.txt", scWinSwitch.Text)
    Workbench.Instance.createWinSwitcherList()

  End Sub

  Private Sub lnkHelpWinSwitcher_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelpWinSwitcher.LinkClicked
    navHelpByKeyword("winswitcher")
  End Sub

  Private Sub ScriptedPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles ScriptedPanel1.Paint

  End Sub

  Private Sub ListView1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvGroupHome.MouseClick
    Dim lvi = lvGroupHome.GetItemAt(e.X, e.Y)
    If lvi Is Nothing Then Exit Sub
    Dim tn() = TreeView2.Nodes.Find(lvi.Name, True)
    If tn.Length > 0 Then TreeView2.SelectedNode = tn(0)
  End Sub

  Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvGroupHome.SelectedIndexChanged

  End Sub


  Private Sub lvHome_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHome.MouseClick
    Dim lvi = lvHome.GetItemAt(e.X, e.Y)
    If lvi Is Nothing Then Exit Sub
    Dim tn() = TreeView2.Nodes.Find(lvi.Name, True)
    If tn.Length > 0 Then TreeView2.SelectedNode = tn(0)
  End Sub

  Private Sub AddinEigenschaftenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'If igAddIns.CurRow Is Nothing OrElse igAddIns.CurRow.Tag Is Nothing Then Exit Sub
    'Dim inst As AddinInstance = igAddIns.CurRow.Tag
    'Dim psi As New ProcessStartInfo(inst.AssemblyFileName)
    'psi.Verb = "properties"
    ''psi.UseShellExecute = True

    'Process.Start(psi)
  End Sub

  Private Sub check_hideSysAddins_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    readAddinList()
  End Sub



  Private Sub IGrid1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)

  End Sub

  Private Sub IGrid1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
    If e.Data.GetDataPresent("ToolbarCommandData") Then
      e.Effect = DragDropEffects.Copy
    End If
  End Sub

  Private Sub lvwTbCmds_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

  End Sub

  Private Sub lvOpenedWins_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvOpenedWins.SelectedIndexChanged
    On Error Resume Next
    'Dim obj(lvOpenedWins.SelectedItems.Count - 1) As Object
    'For i = 0 To lvOpenedWins.SelectedItems.Count - 1
    '  obj(i) = lvOpenedWins.SelectedItems(i).Tag
    'Next
    'PropertyGrid1.SelectedObjects = obj
    Dim win = lvOpenedWins.SelectedItems(0).Tag
    PropertyGrid1.SelectedObject = win
    ToolStripButton2.Checked = win.visible
  End Sub

  Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
    On Error Resume Next
    If lvOpenedWins.SelectedItems.Count = 0 Then Exit Sub
    Dim win = lvOpenedWins.SelectedItems(0).Tag
    'win.visible = Not win.visible
    If win.visible Then
      win.dockHandler.hide()
      win.hide()
    Else
      win.dockHandler.show()
      win.show()
      win.activate()
    End If
    ToolStripButton2.Checked = win.visible
  End Sub

  Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
    On Error Resume Next
    If lvOpenedWins.SelectedItems.Count = 0 Then Exit Sub
    Dim win = lvOpenedWins.SelectedItems(0).Tag
    Clipboard.Clear()
    Clipboard.SetText(win.GetPersistString())
  End Sub

  Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
    On Error Resume Next
    If lvOpenedWins.SelectedItems.Count = 0 Then Exit Sub
    Dim win = lvOpenedWins.SelectedItems(0).Tag 'As IDockContent 
    win.show()
    win.activate()
    'win.top = 100
    'win.left = 300
    'win.width = 300 : win.height = 300
    'win.dockstate = 1 'Float
    With win.DockHandler
      .Activate()
      .Show()
      .DockState = 1 'float
      ' .FloatPane.Bounds = New Rectangle(100, 300, 300, 300)
      .FloatAt(New Rectangle(100, 300, 300, 300))
    End With

  End Sub

  Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
    On Error Resume Next
    If lvOpenedWins.SelectedItems.Count = 0 Then Exit Sub
    Dim win = lvOpenedWins.SelectedItems(0).Tag
    win.dockHandler.close()
    win.close()
    readWindowList()
  End Sub

End Class

'Sub readCommandList()
'  lvwTbCmds.Items.Clear() : imlToolbarIcons.Images.Clear()
'  Dim path = AddInTree.GetTreeNode("/Workspace/ToolbarCommands")
'  Dim lvi As ListViewItem
'  lvi = lvwTbCmds.Items.Add("-", "<Trennzeichen>", "")

'  For Each cod In path.Codons
'    lvi = lvwTbCmds.Items.Add(cod.Id, cod.Id, "")
'    If Not String.IsNullOrEmpty(cod.Properties("icon")) Then
'      imlToolbarIcons.Images.Add(cod.Id, ResourceLoader.GetImageCached(cod.Properties("icon")))
'      lvi.ImageKey = cod.Id
'    End If
'    lvi.Tag = cod
'  Next

'End Sub

'Sub readToolbarList()
'  cmbTbList.Items.Clear()
'  Dim tbList() = cls_IDEHelper.Instance.GetToolbarList()
'  For Each tb In tbList
'    Dim name = tb.Substring(12)
'    cmbTbList.Items.Add(name)

'  Next
'  Dim files() = IO.Directory.GetFiles(ParaService.SettingsFolder + "toolbars", "*.txt", IO.SearchOption.TopDirectoryOnly)
'  For Each file In files
'    Dim fileName = "." + IO.Path.GetFileNameWithoutExtension(file)
'    If Not cmbTbList.Items.Contains(fileName) Then
'      cmbTbList.Items.Add(fileName)
'    End If
'  Next
'End Sub

'Function getActiveToolbarFilename() As String
'  Dim fileName As String = cmbTbList.SelectedItem
'  If String.IsNullOrEmpty(fileName) OrElse fileName.StartsWith(".") = False Then Return ""

'  Return fileName.Substring(1)
'End Function

'Private Sub igDragDrop1_DragDropDone(ByVal MovedGridRow As TenTec.Windows.iGridLib.iGRow) Handles igDragDrop1.DragDropDone
'  saveActiveToolbarData()
'End Sub

'Sub saveActiveToolbarData()
'  Dim fileName As String = getActiveToolbarFilename()
'  If fileName = "" Then Exit Sub

'  ToolbarService.saveToolbarData(igToolbars, fileName)
'  ToolbarService.toolbar_loadFromFile(fileName)
'End Sub

'Private Sub btnTB_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'  Dim dlg As New frm_createToolbar
'  If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
'    'IO.File.WriteAllText(ParaService.SettingsFolder + "toolbars\" + dlg.TextBox1.Text + ".txt", "")
'    ToolbarService.createNew(dlg.TextBox1.Text)
'    ToolbarService.toolbar_loadFromFile(dlg.TextBox1.Text)

'    'cmbTbList.Items.Add(dlg.TextBox1.Text)
'    readToolbarList()
'  End If
'End Sub

'Private Sub btnTB_rename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'  Dim fileName As String = getActiveToolbarFilename()
'  If fileName = "" Then Exit Sub

'  Dim dlg As New frm_createToolbar
'  dlg.ComboBox1.Enabled = False : dlg.Text = "Toolbar umbenennen"
'  dlg.TextBox1.Text = fileName
'  If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
'    Dim newFilename As String = dlg.TextBox1.Text

'    'Dim tbCont As String
'    Dim tb As Control = cls_IDEHelper.Instance.GetToolbar("." + fileName) ', tbCont)
'    tb.Name = "userToolbar_" + newFilename
'    'Dim tbLeft = tb.Left, tbTop = tb.Top
'    'tb = Nothing
'    'cls_IDEHelper.Instance.RemoveToolbar("." + fileName)

'    Dim fileSpec = ToolbarService.ToolbarFolder() + fileName + ".txt"
'    IO.File.Move(fileSpec, ToolbarService.ToolbarFolder() + newFilename + ".txt")

'    'ToolbarService.toolbar_loadFromFile(fileName, tbCont, tbLeft, tbTop)
'  End If
'End Sub

'Private Sub btnTB_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'  Dim fileName As String = cmbTbList.SelectedItem
'  If String.IsNullOrEmpty(fileName) Then Return

'  If MsgBox("Möchten Sie die Toolbar """ + fileName + """ wirklich löschen ?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub

'  If fileName.StartsWith(".") Then
'    Dim fileSpec = ToolbarService.ToolbarFolder() + fileName.Substring(1) + ".txt"
'    IO.File.Move(fileSpec, IO.Path.ChangeExtension(fileSpec, ".bak"))
'  End If

'  cls_IDEHelper.Instance.RemoveToolbar(fileName)
'End Sub

'Private Sub chkTB_visible_Click(ByVal sender As Object, ByVal e As System.EventArgs)
'  Dim fileName As String = cmbTbList.SelectedItem
'  If String.IsNullOrEmpty(fileName) Then Return

'  If chkTB_visible.Checked = True Then
'    If fileName.StartsWith(".") Then
'      ToolbarService.toolbar_loadFromFile(fileName.Substring(1))
'    Else
'      cls_IDEHelper.Instance.CreateToolbar(fileName)
'    End If
'  Else
'    Dim tb = cls_IDEHelper.Instance.GetToolbar(fileName)
'    If tb IsNot Nothing Then
'      DirectCast(tb, Control).Visible = False
'    End If
'  End If
'End Sub

'Private Sub btnTB_removeLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'  On Error Resume Next
'  Dim idx = igToolbars.CurRow.Index
'  igToolbars.Rows.RemoveAt(idx)
'  igToolbars.SetCurRow(idx)
'  saveActiveToolbarData()
'End Sub

'Private Sub cmbTbList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
'  Dim fileName As String = getActiveToolbarFilename()
'  If fileName = "" Then
'    labTB_isScriptedErr.Show() : labTB_isScriptedErr.BringToFront()
'  Else
'    labTB_isScriptedErr.Hide()
'    ToolbarService.readToolbarData(igToolbars, fileName)
'  End If
'End Sub
