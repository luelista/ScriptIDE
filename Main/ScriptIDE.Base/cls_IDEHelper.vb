<Microsoft.VisualBasic.ComClass()> Public Class cls_IDEHelper
  Implements IIDEHelper

  Public Event DocumentAfterSave(ByVal key As String) Implements IIDEHelper.DocumentAfterSave
  Public Event DocumentBeforeClose(ByVal key As String, ByRef cancel As Boolean) Implements IIDEHelper.DocumentBeforeClose
  Public Event DocumentBeforeSave(ByVal key As String, ByRef cancel As Boolean) Implements IIDEHelper.DocumentBeforeSave
  Public Event DocumentClosed(ByVal key As String) Implements IIDEHelper.DocumentClosed
  Public Event DocumentOpened(ByVal Key As String) Implements IIDEHelper.DocumentOpened
  Public Event DocumentTabActivated(ByVal rtf As Object, ByVal key As String) Implements IIDEHelper.DocumentTabActivated
  Public Event OnDialogEvent(ByVal winID As String, ByVal objectName As String, ByVal eventArgs As ScriptEventArgs) Implements IIDEHelper.OnDialogEvent
  Public Event BreakPointSet(ByVal DocURL As String, ByVal lineNumber As Integer, ByVal state As Boolean) Implements IIDEHelper.BreakPointSet
  Public Event OnIniDone() Implements IIDEHelper.OnIniDone
  Public Event ConsoleEvent(ByVal EventType As Integer, ByVal EventParam As Object) Implements IIDEHelper.ConsoleEvent

  Private Shared p_instanceCH As New IDEHelper_ContentHelper
  Private Shared p_instanceSKINS As New IDEHelper_Skins
  ' Private Shared p_instanceIL As New IDEHelper_indexList
  Private Shared p_instancePM = New ProtocolService
  Private Shared p_instanceAD As New IDEHelper_Addins
  Private Shared p_instance As New cls_IDEHelper

  Private p_isStartup As Boolean = True

  Private Sub New()
    AddHandler ScriptWindowHelper.ScriptWindowManager.ScriptWindowEvent, AddressOf OnOnDialogEvent
  End Sub

  Public Shared Function GetSingleton() As cls_IDEHelper
    Return p_instance
  End Function

  Public Shared ReadOnly Property Instance() As cls_IDEHelper
    Get
      Return p_instance
    End Get
  End Property

  'Private p_breakMode As String

  'Public Property globBreakMode() As String Implements IIDEHelper.globBreakMode
  '  Get
  '    Return p_breakMode
  '  End Get
  '  Set(ByVal value As String)
  '    p_breakMode = value
  '    Dim isBreak = value = "BREAK"
  '    MAIN.labScriptBreak2.Visible = isBreak
  '    MAIN.labScriptBreak.Visible = isBreak
  '    MAIN.labScriptBreak2.Text = If(isBreak, "[F9] Step    [F11] Go    [Strg+F12] Exit", "[F5] Run   [Strg+S] Save")
  '    If isBreak = False Then MAIN.tssl_Filename.Text = ""
  '    'frm_scriptDebugging.Visible = value = "BREAK"
  '  End Set
  'End Property


  Friend Sub OnOnIniDone()
    p_isStartup = False
    RaiseEvent OnIniDone()
  End Sub
  Friend Sub OnBreakPointSet(ByVal DocURL As String, ByVal lineNumber As Integer, ByVal state As Boolean)
    RaiseEvent BreakPointSet(DocURL, lineNumber, state)
  End Sub
  Friend Sub OnDocumentAfterSave(ByVal key As String)
    RaiseEvent DocumentAfterSave(key)
  End Sub
  Friend Sub OnDocumentBeforeClose(ByVal key As String, ByRef cancel As Boolean)
    RaiseEvent DocumentBeforeClose(key, cancel)
  End Sub
  Friend Sub OnDocumentBeforeSave(ByVal key As String, ByRef cancel As Boolean)
    RaiseEvent DocumentBeforeSave(key, cancel)
  End Sub
  Friend Sub OnDocumentClosed(ByVal key As String)
    RaiseEvent DocumentClosed(key)
  End Sub
  Friend Sub OnDocumentOpened(ByVal key As String)
    RaiseEvent DocumentOpened(key)
  End Sub
  Friend Sub OnDocumentTabActivated(ByVal rtf As Object, ByVal key As String)
    RaiseEvent DocumentTabActivated(rtf, key)
  End Sub
  Friend Sub OnOnDialogEvent(ByVal winID As String, ByVal objectName As String, ByVal eventArgs As ScriptEventArgs)
    RaiseEvent OnDialogEvent(winID, objectName, eventArgs)
  End Sub
  Friend Sub OnConsoleEvent(ByVal EventType As Integer, ByVal EventParams As Object)
    RaiseEvent ConsoleEvent(EventType, EventParams)
  End Sub




  'Friend ScriptClassName As String

  Function CreateDockWindow(ByVal scriptedWindowID As String, Optional ByVal defaultPosition As Integer = 5) As IScriptedPanel Implements IIDEHelper.CreateDockWindow
    'On Error Resume Next
    'Dim hasBeenCreated As Boolean
    'Dim panelRef = ScriptWindowHelper.ScriptWindowManager.CreateWindow(scriptedWindowID, "", True, hasBeenCreated)
    'Dim wnd As ScriptWindowHelper.frmTB_scriptWin = panelRef.Form

    'If hasBeenCreated Then
    '  If defaultPosition > -1 Then
    '    wnd.ShowHint = defaultPosition
    '    wnd.Show()
    '  End If
    'Else
    '  If defaultPosition > -1 Then wnd.Show()
    'End If
    'Return wnd.PNL
    'Throw New NotImplementedException("Please use scriptHelper (zz.CreateWindow) ;-)")
    MsgBox("Die Funktion CreateDockWindow ist obsolet. Bitte verwende die Funktion ZZ.CreateWindow aus dem scriptHelper" + vbNewLine + "scriptedWindowID: " + scriptedWindowID + vbNewLine + "ZZ.CreateWindow(String, DWndFlags, Integer)", , "cls_IDEHelper")
  End Function

  Public Function CreateForm(ByVal scriptedWindowID As String) As IScriptedWindow Implements IIDEHelper.CreateForm
    'On Error Resume Next
    'Return ScriptWindowHelper.ScriptWindowManager.CreateWindow(scriptedWindowID, False)
    'Throw New NotImplementedException("Please use scriptHelper (zz.CreateWindow) ;-)")
    MsgBox("Die Funktion CreateForm ist obsolet. Bitte verwende die Funktion ZZ.CreateWindow aus dem scriptHelper" + vbNewLine + "scriptedWindowID: " + scriptedWindowID + vbNewLine + "ZZ.CreateWindow(String, DWndFlags, Integer)", , "cls_IDEHelper")
  End Function

  Public Function GetInternalDockWindow(ByVal persistString As String) As System.Windows.Forms.Form Implements IIDEHelper.getInternalDockWindow
    Return getDeserializedDockContent(persistString)
  End Function


  Public Function GetToolbarList() As String() Implements IIDEHelper.GetToolbarList
    Dim out(-1) As String
    Dim ref = Workbench.Instance.ToolStripContainer1
    addToolstripsToList(ref.TopToolStripPanel, out)
    addToolstripsToList(ref.BottomToolStripPanel, out)
    addToolstripsToList(ref.LeftToolStripPanel, out)
    addToolstripsToList(ref.RightToolStripPanel, out)
    Return out
  End Function

  Private Sub addToolstripsToList(ByVal container As Control, ByRef list() As String)
    For Each ctrl As Control In container.Controls
      If TypeOf ctrl Is ToolStrip Then
        ReDim Preserve list(list.Length)
        list(list.Length - 1) = ctrl.Name ' + vbTab + ctrl.Bounds.ToString
      End If
    Next
  End Sub

  Public Function GetToolbar(ByVal ToolbarID As String, Optional ByRef containerName As String = Nothing) As IScriptedPanel Implements IIDEHelper.GetToolbar
    Dim elName = "userToolbar_" + ToolbarID
    TT.Write("Getting toolbar???", ToolbarID, "dump")
    Dim ref = Workbench.Instance.ToolStripContainer1
    If ref.TopToolStripPanel.Controls.ContainsKey(elName) Then
      containerName = "Top" : Return ref.TopToolStripPanel.Controls(elName)
    End If
    If ref.BottomToolStripPanel.Controls.ContainsKey(elName) Then
      containerName = "Bottom" : Return ref.BottomToolStripPanel.Controls(elName)
    End If
    If ref.LeftToolStripPanel.Controls.ContainsKey(elName) Then
      containerName = "Left" : Return ref.LeftToolStripPanel.Controls(elName)
    End If
    If ref.RightToolStripPanel.Controls.ContainsKey(elName) Then
      containerName = "Right" : Return ref.RightToolStripPanel.Controls(elName)
    End If
  End Function

  Public Function GetToolbarContainer(ByVal name As String) As ToolStripPanel
    Select Case LCase(Left(name, 1))
      Case "t" : Return Workbench.Instance.ToolStripContainer1.TopToolStripPanel
      Case "b" : Return Workbench.Instance.ToolStripContainer1.BottomToolStripPanel
      Case "l" : Return Workbench.Instance.ToolStripContainer1.LeftToolStripPanel
      Case "r" : Return Workbench.Instance.ToolStripContainer1.RightToolStripPanel
    End Select
  End Function

  Public Function CreateToolbar(ByVal ToolbarID As String, ByVal container As String, ByVal left As Integer, ByVal top As Integer) As IScriptedPanel Implements IIDEHelper.CreateToolbar
    Dim tsRef = GetToolbar(ToolbarID)

    If tsRef Is Nothing Then
      TT.Write("Creating toolbar", ToolbarID)
      Dim ts = New ScriptWindowHelper.ScriptedToolstrip() With {.Renderer = ToolstripRendererService.GetRenderer()}

      ts.Name = "userToolbar_" + ToolbarID
      'ts.Height = 25
      ts.WinID = ToolbarID
      If ToolbarID.Contains(".") Then ts.ClassName = ToolbarID.Substring(0, ToolbarID.IndexOf("."))

      If left > -1 And top > -1 Then
        GetToolbarContainer(container).Join(ts, left, top)
      Else
        GetToolbarContainer(container).Controls.Add(ts)
      End If

      ' Workbench.Instance.showHideToolbar()
      Return ts
    Else
      TT.Write("Getting toolbar", ToolbarID, "dump")
      'Dim ts = Workbench.Instance.ToolStripContainer1.TopToolStripPanel.Controls("userToolbar_" + toolbarID)
      DirectCast(tsRef, Control).Show()
      ' Workbench.Instance.showHideToolbar()
      Return tsRef
    End If
  End Function
  Public Function CreateToolbar(ByVal toolbarID As String, Optional ByVal tbStyle As ToolBarStyle = ToolBarStyle.ToolStrip) As IScriptedPanel Implements IIDEHelper.CreateToolbar
    Return CreateToolbar(toolbarID, "top", -1, -1)
  End Function
  Public Sub RemoveToolbar(ByVal toolbarID As String) Implements IIDEHelper.RemoveToolbar
    Dim rContainer As String
    Dim ctrl As Control = GetToolbar(toolbarID, rContainer)
    GetToolbarContainer(rContainer).Controls.RemoveByKey("userToolbar_" + toolbarID)
    Workbench.Instance.showHideToolbar()
  End Sub
  Public Sub RefreshToolbarSize() Implements IIDEHelper.RefreshToolbarSize
    Workbench.Instance.showHideToolbar()
  End Sub



  Public Function getMainFormRef() As Object Implements IIDEHelper.getMainFormRef
    Return Workbench.Instance
  End Function

  'Public Function getMainToolbar() As Object Implements IIDEHelper.getMainToolbar
  '  Return MAIN.pnlToolbar
  'End Function

  Public ReadOnly Property MainWindow() As Object Implements IIDEHelper.MainWindow
    Get
      Return Workbench.Instance
    End Get
  End Property

  'Public Sub showHideMainToolbar(ByVal visible As Boolean, Optional ByVal height As Integer = -1) Implements IIDEHelper.showHideMainToolbar
  '  If visible Then
  '    If height = -1 Then height = MAIN.pnlToolbar.Height
  '    MAIN.showHideToolbar(height)
  '  Else
  '    MAIN.showHideToolbar(0)
  '  End If
  'End Sub

  Public Sub ShowOptionsDialog(Optional ByVal page As String = Nothing) Implements IIDEHelper.ShowOptionsDialog
    ScriptIDE.Main.Workbench.ShowOptionsDialog(page)
  End Sub


  Public Function NavigateFile(ByVal filePara As String) As System.Windows.Forms.Form Implements IIDEHelper.NavigateFile
    onNavigate(filePara)
    Return getActiveRTF()
  End Function
  Public Function IsFileOpened(ByVal filePara As String) As Boolean Implements IIDEHelper.IsFileOpened
    Return isTabOpened(filePara)
  End Function
  Public Function GetTabByURL(ByVal filePara As String) As System.Windows.Forms.Form Implements IIDEHelper.GetTabByURL
    Dim dummy1, dummy2, dummy3 As String
    Dim documentRef As Object
    parseDocumentURL(filePara, dummy1, dummy2, dummy3, documentRef)
    Return documentRef
  End Function

  Public Function GetTabByPersistString(ByVal persistString As String) As System.Windows.Forms.Form Implements IIDEHelper.GetTabByPersistString
    For Each doc In Workbench.Instance.DockPanel1.Documents
      If doc.DockHandler.GetPersistStringCallback.Invoke() = persistString Then Return doc.DockHandler.Form
    Next
    Return Nothing
  End Function

  Public Function GetTabList() As String() Implements IIDEHelper.GetTabList
    Dim out(999) As String, idx As Integer = 0
    For Each doc In Workbench.Instance.DockPanel1.Documents
      out(idx) = doc.DockHandler.GetPersistStringCallback.Invoke() : idx += 1
    Next
    ReDim Preserve out(idx)
    Return out
  End Function


  Public Function Exec(ByVal Command As String, ByVal userPara As Object, ByVal source As Object) As Object Implements IIDEHelper.Exec
    '--> AddinCommand
    Dim item As Codon
    If Command.Substring(0, 1) = "/" Then
      item = AddInTree.GetCodon(Command)
    Else

      item = AddInTree.GetCodon("/Workspace/ToolbarCommands/" + Command)
      If item Is Nothing Then
        Dim tn = AddInTree.GetTreeNode("/Workspace/FileCommands")
        item = tn.GetChildItem(Command, True)
      End If
    End If
    If item Is Nothing Then Throw New ArgumentException(Command + " is not a valid command name")

    Dim ref = item.AddIn.ConnectRef
    ref.OnNavigate(NavigationKind.ToolbarCommand, source, Command, userPara, Exec)
  End Function


  Public Function Navigate(ByVal Target As String, ByVal Command As String, ByVal userPara As String) As String Implements IIDEHelper.Navigate
    '--> Interproc
    Return Interproc.GetData(Target, Command, userPara)
  End Function


  'TODO !!!

  Public Overloads Sub executeProgramInConsole(ByVal commandLine As String, Optional ByVal workDir As String = "") Implements IIDEHelper.executeProgramInConsole
    'runConsoleProgram(commandLine, workDir)
  End Sub
  Public Overloads Sub executeProgramInConsole(ByVal commandLine As String, ByRef procInfo As Process, ByVal workDir As String) Implements IIDEHelper.executeProgramInConsole
    'runConsoleProgram(commandLine, workDir)

    'procInfo = consoleProcGetProcess()
  End Sub

  Function Console_GetProcess() As Process Implements IIDEHelper.Console_GetProcess
    ' Return consoleProcGetProcess()
  End Function
  Function Console_ProcRunning() As Boolean Implements IIDEHelper.Console_ProcRunning
    ' Return consoleProcRunning()
  End Function
  Sub Console_Kill() Implements IIDEHelper.Console_Kill
    ' killConsoleProc()
  End Sub

  Public Function getActiveTab() As Form Implements IIDEHelper.getActiveTab
    Return getActiveRTF()
  End Function

  Public Function getActiveTabFilespec() As String Implements IIDEHelper.getActiveTabFilespec
    Dim act = getActiveRTF()
    If act Is Nothing Then Return ""
    'Dim typ = getActiveTabType()
    'If typ = "nothing" Or typ = "" Then Return ""
    'If act.Filesource = "_loc" Then Return act.filename
    'If typ = "scintilla" Or typ = "picture" Then Return ftpGetLocalAlias(act.Filesource, act.FileName)
    'If typ = "rtf" Then Return ""
    Return act.URL
  End Function

  Public Function getActiveTabType() As String Implements IIDEHelper.getActiveTabType
    Dim act = getActiveTab()
    If act Is Nothing Then Return "nothing"
    If TypeOf act Is frmDC_scintilla Then Return "scintilla"
    'If TypeOf act Is frmDC_rtf Then Return "rtf"
    'If TypeOf act Is frmDC_picture Then Return "picture"
    Return act.GetType.Name
  End Function

  Public ReadOnly Property Glob() As Object Implements IIDEHelper.Glob
    Get
      Return ParaService.Glob
    End Get
  End Property

  Public Function GetSettingsFolder() As String Implements IIDEHelper.GetSettingsFolder
    Return ParaService.SettingsFolder
  End Function
  Public Function GetProfileFolder() As String Implements IIDEHelper.GetProfileFolder
    Return ParaService.ProfileFolder
  End Function

  Public Sub RegisterAddinWindow(ByVal WindowID As String) Implements IIDEHelper.RegisterAddinWindow
    tbAddinWin.Add(WindowID)
  End Sub
  Public Sub UnregisterAddinWindow(ByVal WindowID As String) Implements IIDEHelper.UnregisterAddinWindow
    tbAddinWin.Remove(WindowID)
  End Sub



  Public Sub BeforeShowAddinWindow(ByVal WindowID As String, ByVal Ref As Object) Implements IIDEHelper.BeforeShowAddinWindow
    'Visual studio ist zu dumm, um den Parameter direkt im richtigen Typ anzugeben
    '...da kommen nur irgendwelche völlig unzutreffenden Fehlermeldungen --> Scheißding
    Dim ref2 As WeifenLuo.WinFormsUI.Docking.DockContent = Ref
    Debug.Print("BeforeShowAddinWindow  " + WindowID)

    ref2.DockPanel = Workbench.Instance.DockPanel1

    'ref2.Show(MAIN.DockPanel1)
  End Sub


  'Public Function GetProtocolHandler(ByVal Protocol As String) As IProtocolHandler Implements IIDEHelper.GetProtocolHandler
  '  Return app_protocolHandler.GetProtocolHandler(Protocol)
  'End Function

  'Public Function GetURLProtocolHandler(ByVal InternalURL As String) As IProtocolHandler Implements IIDEHelper.GetURLProtocolHandler
  '  Return app_protocolHandler.GetURLProtocolHandler(InternalURL)
  'End Function

#Region "Sub Objects"
  Public ReadOnly Property ContentHelper() As IIDEContentHelper Implements IIDEHelper.ContentHelper
    Get
      Return p_instanceCH
    End Get
  End Property
  Public ReadOnly Property IndexList() As IIDEIndexList Implements IIDEHelper.IndexList
    Get
      ' Return p_instanceIL
    End Get
  End Property
  Public ReadOnly Property Skin() As Object Implements IIDEHelper.Skin
    Get
      Return p_instanceSKINS
    End Get
  End Property
  'Public ReadOnly Property ProtocolManager() As IProtocolManager Implements IIDEHelper.ProtocolManager
  '  Get
  '    Return p_instancePM
  '  End Get
  'End Property
  Public ReadOnly Property Addins() As IIDEAddins Implements IIDEHelper.Addins
    Get
      Return p_instanceAD
    End Get
  End Property
#End Region

  Public Sub RegisterFileactionHandler(ByVal ClassRef As Object) Implements IIDEHelper.RegisterFileactionHandler
    Dim typ = ClassRef.GetType() 'Reflection.BindingFlags.Static Or 
    For Each meth In typ.GetMembers()
      Debug.Print(TypeName(meth) + vbTab + meth.Name)
      If TypeOf meth Is Reflection.MethodInfo Then
        addFileactionHandlerFunc(meth, ClassRef)
      End If
    Next
  End Sub

  Public ReadOnly Property IsStartup() As Boolean Implements IIDEHelper.IsStartup
    Get
      Return p_isStartup
    End Get
  End Property

  Public ReadOnly Property DIZ() As String Implements IIDEHelper.DIZ
    Get
      Return Workbench.Instance.txtDIZ.Text
    End Get
  End Property

End Class



<Microsoft.VisualBasic.ComClass()> Public Class IDEHelper_Addins
  Implements IIDEAddins

  Friend Sub New()
  End Sub

  Public Function ContainsKey(ByVal addinName As String) As Boolean Implements IIDEAddins.ContainsKey
    Return AddinInstance.GetAddinInstance(addinName) IsNot Nothing
  End Function
  Public Function IsLoaded(ByVal addinName As String) As Boolean Implements IIDEAddins.IsLoaded
    Return AddinInstance.GetAddinReference(addinName) IsNot Nothing
  End Function

  Public Sub InstallAddin(ByVal fileSpec As String) Implements IIDEAddins.InstallAddin

  End Sub

  Default Public ReadOnly Property Item(ByVal addinName As String) As IAddinConnect Implements IIDEAddins.Item
    Get
      Return AddinInstance.GetAddinReference(addinName)
    End Get
  End Property

  Public Function GetAddinInfo(ByVal addinName As String) As Object Implements IIDEAddins.GetAddinInfo
    Return AddinInstance.GetAddinInstance(addinName)
  End Function

  Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.Collections.Generic.KeyValuePair(Of String, IAddinConnect)) Implements System.Collections.Generic.IEnumerable(Of System.Collections.Generic.KeyValuePair(Of String, IAddinConnect)).GetEnumerator
    Return New addinEnumerator
  End Function

  Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    Return New addinEnumerator
  End Function

  Class addinEnumerator
    Implements IEnumerator(Of KeyValuePair(Of String, IAddinConnect))
    Dim pos As Integer

    Public ReadOnly Property Current() As System.Collections.Generic.KeyValuePair(Of String, IAddinConnect) Implements System.Collections.Generic.IEnumerator(Of System.Collections.Generic.KeyValuePair(Of String, IAddinConnect)).Current
      Get
        If pos >= AddinInstance.Addins.Count Then Return Nothing
        Dim inst = AddinInstance.Addins(pos)
        Return New KeyValuePair(Of String, IAddinConnect)(inst.ID, inst.ConnectRef)
      End Get
    End Property

    Public ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
      Get
        Return Current
      End Get
    End Property

    Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
      pos += 1
    End Function

    Public Sub Reset() Implements System.Collections.IEnumerator.Reset
      pos = 0
    End Sub

    Private disposedValue As Boolean = False    ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
      If Not Me.disposedValue Then
        If disposing Then
          ' TODO: free other state (managed objects).
        End If

        ' TODO: free your own state (unmanaged objects).
        ' TODO: set large fields to null.
      End If
      Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
      ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
      Dispose(True)
      GC.SuppressFinalize(Me)
    End Sub
#End Region

  End Class
End Class


<Microsoft.VisualBasic.ComClass()> Public Class IDEHelper_Skins

  'Property UserToolbarBackColor() As Color
  '  Get
  '    Return MAIN.flpToolbar.BackColor
  '  End Get
  '  Set(ByVal value As Color)
  '    MAIN.flpToolbar.BackColor = value
  '  End Set
  'End Property

  Property BookmarksBackColor() As Color
    Get
      Return Workbench.Instance.pnlBookmarks.BackColor
    End Get
    Set(ByVal value As Color)
      Workbench.Instance.pnlBookmarks.BackColor = value
      Workbench.Instance.lnkAddBookmark.BackColor = value
    End Set
  End Property

  Property BookmarksLinkColor() As Color
    Get
      Return Workbench.Instance.pnlBookmarks.ForeColor
    End Get
    Set(ByVal value As Color)
      Workbench.Instance.pnlBookmarks.ForeColor = value
    End Set
  End Property

  Property StatusbarBackColor() As Color
    Get
      Return Workbench.Instance.StatusStrip1.BackColor
    End Get
    Set(ByVal value As Color)
      Workbench.Instance.StatusStrip1.BackColor = value
    End Set
  End Property

  Property StatusbarForeColor() As Color
    Get
      Return Workbench.Instance.StatusStrip1.ForeColor
    End Get
    Set(ByVal value As Color)
      Workbench.Instance.StatusStrip1.ForeColor = value
    End Set
  End Property

  Property TitleBarForeColor1() As Color
    Get
      Return Workbench.Instance.labWinTitle.ForeColor
    End Get
    Set(ByVal value As Color)
      Workbench.Instance.labWinTitle.ForeColor = value
    End Set
  End Property

  Property TitleBarForeColor2() As Color
    Get
      Return Workbench.Instance.txtGlobAktFileSpec.ForeColor
    End Get
    Set(ByVal value As Color)
      Workbench.Instance.txtGlobAktFileSpec.ForeColor = value
      Workbench.Instance.labScriptRunning.ForeColor = value
      Workbench.Instance.pnlTitlebar.ForeColor = value
      Workbench.Instance.chkSticky.ForeColor = value
      Workbench.Instance.chkTopmost.ForeColor = value
      Workbench.Instance.check_merkeZeile.ForeColor = value
    End Set
  End Property

  Property TitleBarBackColor() As Color
    Get
      Return Workbench.Instance.pnlTitlebar.BackColor
    End Get
    Set(ByVal value As Color)
      If vistaStyleOn Then Exit Property
      Workbench.Instance.pnlTitlebar.BackColor = value
    End Set
  End Property

  Private _TitleBarImageURL As String = ""
  Property TitleBarImageURL() As String
    Get
      Return _TitleBarImageURL
    End Get
    Set(ByVal value As String)
      If vistaStyleOn Then Exit Property
      If String.IsNullOrEmpty(value) Then
        _TitleBarImageURL = ""
        Workbench.Instance.pnlTitlebar.BackgroundImage = Nothing
      Else
        _TitleBarImageURL = value
        Workbench.Instance.pnlTitlebar.BackgroundImage = ResourceLoader.GetImageCached(value)
      End If
    End Set
  End Property

  'Property MenuVisible() As Boolean
  '  Get
  '    Return Workbench.Instance.MenuStrip1.Visible
  '  End Get
  '  Set(ByVal value As Boolean)
  '    Workbench.Instance.MenuStrip1.Visible = value
  '    Workbench.Instance.MenuAnzeigenToolStripMenuItem.Checked = value
  '  End Set
  'End Property

  'Property TitleBarVisible() As Boolean
  '  Get
  '    Return Workbench.Instance.m_FormBorder.Titlebar
  '  End Get
  '  Set(ByVal value As Boolean)
  '    Workbench.Instance.m_FormBorder.Titlebar = value
  '  End Set
  'End Property

  Property OpenedFilesBackColor() As Color
    Get
      Return tbOpenedFiles.IGrid1.BackColor
    End Get
    Set(ByVal value As Color)
      tbOpenedFiles.IGrid1.BackColor = value
    End Set
  End Property

  Friend Sub New()
  End Sub
End Class


<Microsoft.VisualBasic.ComClass()> Public Class IDEHelper_ContentHelper
  Implements IIDEContentHelper

  Dim indexListCtrls As New Dictionary(Of String, IIndexList)

  Public Event CurrentDocumentLineChanged(ByVal Tab As Object, ByVal lineNr As Integer) Implements IIDEContentHelper.CurrentDocumentLineChanged
  
  Public Sub OnCurrentDocumentLineChanged(ByVal Tab As Object, ByVal lineNr As Integer) Implements IIDEContentHelper.OnCurrentDocumentLineChanged
    RaiseEvent CurrentDocumentLineChanged(Tab, lineNr)
  End Sub

  Function GetIndexList(ByVal fileExtension As String) As IIndexList Implements IIDEContentHelper.GetIndexList
    If tbIndexList.isDisabled Then Return Nothing
    fileExtension = ";*" + fileExtension.ToLower + ";"
    Dim path = AddInTree.GetTreeNode("/Workspace/IndexList")
    path.EnsureSorted()
    For Each cod In path.Codons
      Dim checkVar = ";" + cod.Properties("filefilter") + ";"
      If checkVar.Contains(fileExtension) Or checkVar = ";*.*;" Then
        If indexListCtrls.ContainsKey(cod.Id) Then Return indexListCtrls(cod.Id)

        Dim ctrl = cod.BuildItem(Nothing, Nothing)
        If TypeOf ctrl Is IIndexList AndAlso TypeOf ctrl Is Control Then
          indexListCtrls.Add(cod.Id, ctrl)
          Return ctrl
        Else
          MsgBox("Indexlisten-Controls müssen von Control erben und das Interface IIndexList implementieren!", MsgBoxStyle.Exclamation)
          Return Nothing
        End If
      End If
    Next
    Return Nothing
  End Function

  Sub ShowIndexListControl(ByVal ctrl As IIndexList) Implements IIDEContentHelper.ShowIndexListControl
    If tbIndexList.Controls.Count > 0 AndAlso tbIndexList.Controls(0) Is ctrl Then Exit Sub

    tbIndexList.Controls.Clear()
    If ctrl Is Nothing Then Exit Sub

    tbIndexList.Controls.Add(ctrl)
    CType(ctrl, Control).Dock = DockStyle.Fill
    CType(ctrl, Control).Show()
  End Sub

  Friend Sub New()
  End Sub

  Public Sub _internalCloseTab(ByVal frm As IDockContentForm, ByRef cancel As Boolean) Implements IIDEContentHelper._internalCloseTab
    cancel = Not internalCloseTab(frm)
  End Sub

  Public Sub merkeZeile(ByVal merkeData As String) Implements IIDEContentHelper.merkeZeile
    sys_merkeZeileAbruf.merkeZeile(merkeData)
  End Sub

  Public Function merkeZeileAbruf() As String() Implements IIDEContentHelper.merkeZeileAbruf
    Return sys_merkeZeileAbruf.merkeZeileAbruf()
  End Function

  Public Property StatusText() As String Implements IIDEContentHelper.StatusText
    Get
      Return Workbench.Instance.tssl_Filename.Text
    End Get
    Set(ByVal value As String)
      Workbench.Instance.tssl_Filename.Text = value
    End Set
  End Property

  Public Sub CreateFileactionToolbar(ByVal tabURL As String, ByVal ext As String, ByVal tb As System.Windows.Forms.ToolStrip) Implements IIDEContentHelper.CreateFileactionToolbar
    app_fileTypeHandling.createToolbaritemsForExt(ext, tb, tabURL)
  End Sub

  Public Sub _internalRenameDocument(ByVal oldKey As String, ByVal newKey As String) Implements IIDEContentHelper._internalRenameDocument
    app_tabManager.renameTab(oldKey, newKey)
  End Sub

  Public Sub SimpleCreateIndexList(ByVal windowRef As IDockContentForm, ByRef indexListRefCache As IIndexList) Implements IIDEContentHelper.SimpleCreateIndexList
    If tbIndexList.isDisabled Then Return

    If indexListRefCache Is Nothing Then
      indexListRefCache = Me.GetIndexList(IO.Path.GetExtension(windowRef.URL))
    End If

    If indexListRefCache IsNot Nothing Then
      indexListRefCache.buildList(windowRef)
      Me.ShowIndexListControl(indexListRefCache)
    End If
  End Sub

  End Class




'<Microsoft.VisualBasic.ComClass()> Public Class IDEHelper_indexList
'  Implements IIDEIndexList

'  Sub Reset() Implements IIDEIndexList.Reset
'    tbIndexList.ListBox1.Items.Clear()
'  End Sub

'  Sub AddItem(ByVal Text As String, ByVal Position As String, ByVal Details As String) Implements IIDEIndexList.AddItem
'    Const tabs = vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab
'    tbIndexList.ListBox1.Items.Add(Text + tabs + "|##|" + Details + "|##|" + Position + "|##|" + Position)
'  End Sub

'  Sub RestorePos(ByRef selIndex As Integer, ByRef topIndex As Integer) Implements IIDEIndexList.RestorePos
'    tbIndexList.skipNavIndexList = True
'    If selIndex = 0 And topIndex = 0 Then
'      selIndex = tbIndexList.ListBox1.SelectedIndex
'      topIndex = tbIndexList.ListBox1.TopIndex
'      tbIndexList.ListBox1.SuspendLayout()
'    Else
'      tbIndexList.ListBox1.SelectedIndex = selIndex
'      tbIndexList.ListBox1.TopIndex = topIndex
'      tbIndexList.ListBox1.ResumeLayout()
'    End If
'    tbIndexList.skipNavIndexList = False
'  End Sub

'  Public Sub OnLinenumberChanged(ByVal lineNr As Integer) Implements IIDEIndexList.OnLinenumberChanged
'    tbIndexList.skipNavIndexList = True
'    For i = tbIndexList.ListBox1.Items.Count - 1 To 0 Step -1
'      Dim txt = tbIndexList.ListBox1.Items(i)
'      Dim line = CInt(Mid(txt, InStrRev(txt, "|##|") + 4))
'      If line <= lineNr Then tbIndexList.ListBox1.SelectedIndex = i : Exit For
'    Next
'    tbIndexList.skipNavIndexList = False
'  End Sub

'  Friend Sub New()
'  End Sub
'End Class
