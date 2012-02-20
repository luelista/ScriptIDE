
'Imports System.Drawing
<FileActionClass()> _
Module app_main
  Public helper As IScriptHelper
  Public WithEvents IDE As IIDEHelper

  Public debuggedProc As New Dictionary(Of String, debugInfo)
  Public debuggedScript As String, debuggedIntWin As String

  'Public debugState As String
  Public tbProcInfo As New frm_exedebugging
  Public tbInfoTips As New frmTB_infoTips
  Public Const tbProcInfo_ID = "Addin|##|siaCodeCompiler|##|EXEDebuggingInformation"
  Public Const tbInfoTips_ID = "Addin|##|siaCodeCompiler|##|ReflectionInfo"
  Public Const tbDebug_ID = "Addin|##|siaCodeCompiler|##|SHDebug"
  Public Const tbCompileErr_ID = "Addin|##|siaCodeCompiler|##|SHCompilerErrors"
  Public Const tbPrintline_ID = "Addin|##|siaCodeCompiler|##|SHPrintline"

  Public WithEvents sH As ScriptIDE.ScriptHost.ScriptHost = ScriptIDE.ScriptHost.ScriptHost.Instance

  Structure debugInfo
    Public Sub New(ByVal file As String, ByVal docURL As String, ByVal proc As Process)
      Me.ScriptFile = file : Me.DocURL = docURL : Me.proc = proc
    End Sub
    Public DocURL, ScriptFile As String, proc As Process
  End Structure

  Sub recreateReflectorWindow()
    tbInfoTips.Close() : tbInfoTips.Dispose()
    tbInfoTips = Nothing
    tbInfoTips = New frmTB_infoTips()
    tbInfoTips.Show()
  End Sub

  Private Sub sH_BreakModeChanged(ByVal className As String, ByRef breakState As String) Handles sH.BreakModeChanged
    If className = LCase(getActiveTabClass()) Then
      debugStateChange(className, breakState)
    End If
  End Sub

  Function getActiveTabClass() As String
    Dim url = IDE.getActiveTabFilespec()
    If String.IsNullOrEmpty(url) Then Return ""
    Return ProtocolService.MapToLocalFilename(url)
  End Function

  Sub debugStateChange(ByVal className As String, ByVal state As String)
    'Dim tb = IDE.CreateToolbar("siaCodeCompiler.compileTB")
    'tb.Element("btn_cc_debugRun").Enabled = debugState = ""
    Dim debugging = Not String.IsNullOrEmpty(state) ' state = "RUN" Or state = "BREAK"

    Main.ToolbarService.UpdateToolbarItem("Debug.Run", Main.ToolbarService.TBUF_ENABLED, Not debugging, False, Nothing)
    Main.ToolbarService.UpdateToolbarItem("Debug.Compile", Main.ToolbarService.TBUF_ENABLED, Not debugging, False, Nothing)
    Main.ToolbarService.UpdateToolbarItem("Debug.Stop", Main.ToolbarService.TBUF_ENABLED, debugging, False, Nothing)
    Main.ToolbarService.UpdateToolbarItem("Debug.Info", Main.ToolbarService.TBUF_ENABLED, debugging, False, Nothing)

    If debugging Then
      'tb.Element("lab_cc_debugMode").text = debugState
      Main.ToolbarService.UpdateToolbarItem("Debug.ModeLbl", Main.ToolbarService.TBUF_TEXT, False, False, className + ": " + state)
    Else
      Main.ToolbarService.UpdateToolbarItem("Debug.ModeLbl", Main.ToolbarService.TBUF_TEXT, False, False, className + ": -----")
      'tb.Element("lab_cc_debugMode").text = "Ready"

      tbProcInfo.Hide()
      If Not String.IsNullOrEmpty(className) Then
        'Dim scriptFile = sH.expandScriptClassName(className)
        'If Not String.IsNullOrEmpty(scriptFile) Then
        Try
          Dim sc As Main.frmDC_scintilla = IDE.GetTabByURL(className)
          sc.highlightExecutingLine(-1)
        Catch ex As Exception
        End Try
         'End If
      End If
    End If
  End Sub

  Private Sub sH_HighlightLineRequested(ByVal className As String, ByVal functionName As String, ByVal lineNumber As Integer, ByVal reason As HighlightLineReason) Handles sH.HighlightLineRequested
    Dim fileSpec = sH.expandScriptClassName(className)
    If fileSpec = "" Then TT.Write("!!! highlightLineRequest for not-existing script", className, "warn") : Return
    Dim sc As Main.frmDC_scintilla = IDE.GetTabByURL("loc:/" + fileSpec)

    If reason = HighlightLineReason.RuntimeError Or reason = HighlightLineReason.BreakMode Then _
      sc.highlightExecutingLine(lineNumber - 1)

    If reason = HighlightLineReason.BreakModeExternal Then _
          sc.highlightExecutingLine2(lineNumber - 1)

    If reason = HighlightLineReason.CompileError Then sc.highlightErrorLine(lineNumber - 1)
  End Sub


  Private Sub sH_QueryBreakpoints(ByVal className As String, ByVal interprocSourceWindow As String, ByRef bb() As Boolean) Handles sH.QueryBreakpoints
    Try
      Dim fileSpec = sH.expandScriptClassName(className)
      If fileSpec = "" Then TT.Write("!!! queryBreakpoints for not-existing script", className, "warn") : Return

      Dim tab As IDockContentForm = IDE.GetTabByURL("loc:/" + fileSpec)
      If tab Is Nothing OrElse Not TypeOf tab.RTF Is ScintillaNet.Scintilla Then TT.Write("!!! queryBreakpoints - file must be opened in scintilla control", className + " _ " + tab.GetType.FullName, "warn") : Return

      Dim sc As ScintillaNet.Scintilla = tab.RTF
      
      ReDim bb(sc.Lines.Count)
      For i = 0 To sc.Lines.Count - 1
        If (sc.Lines(i).GetMarkerMask() And 16) = 16 Then
          bb(i) = True
        End If
      Next
    Catch : End Try
  End Sub


  Private Sub IDE_BreakPointSet(ByVal DocURL As String, ByVal lineNumber As Integer, ByVal state As Boolean) Handles IDE.BreakPointSet
    Dim className = LCase(IO.Path.GetFileNameWithoutExtension(DocURL))
    sH.setBreakPoint(className, lineNumber, state)

    'If debugState <> "" And DocURL = debuggedScript Then
    '  oIntWin.SendCommand(debuggedIntWin, "_Debug_SetBreakPoint", lineNumber & "|##|" & If(state, "True", ""))
    'End If
  End Sub

  Private Sub IDE_DocumentTabActivated(ByVal rtf As Object, ByVal DocURL As String) Handles IDE.DocumentTabActivated
    Dim className = ProtocolService.MapToLocalFilename(DocURL)
    Dim breakState = ScriptHost.Instance.globBreakMode(className)
    debugStateChange(className, breakState)
  End Sub

  Private Sub IDE_OnDialogEvent(ByVal winID As String, ByVal eventName As String, ByVal eventArgs As ScriptEventArgs) Handles IDE.OnDialogEvent
    If winID = "siaCodeCompiler.compileTB" Then
      onToolbarEvent(eventArgs.Sender.name)
    End If
  End Sub

  Sub onToolbarEvent(ByVal tbName As String)
    On Error Resume Next
    Select Case tbName
      Case "Debug.LoadScriptAddin"
        Dim tab As Object = IDE.getActiveTab
        If TypeOf tab Is Main.frmDC_scintilla Then
          If tab Is Nothing Then Exit Sub
          If tab.Dirty Then tab.onSave()

          Dim fileName = ProtocolService.MapToLocalFilename(tab.URL)

          ConnectFromScript(fileName, ConnectMode.AfterStartup)
        End If

      Case "Debug.Compile"
        Dim tab = IDE.getActiveTab
        If TypeOf tab Is Main.frmDC_scintilla Then
          btn_compileScript(tab)
        End If

      Case "Debug.Run"
        Dim tab = IDE.getActiveTab
        If TypeOf tab Is Main.frmDC_scintilla Then
          btn_autostartScript(tab)
        End If

      Case "Debug.Stop"
        Dim info As debugInfo
        Dim className = LCase(getActiveTabClass())
        If debuggedProc.TryGetValue(className, info) Then
          TT.Write("Kill Process " & info.proc.Id, info.proc.MainModule.FileName, "ok")
          info.proc.Kill()
        End If
        sH.globBreakMode(className) = ""

      Case "Debug.Info"
        Dim info As debugInfo
        Dim className = LCase(getActiveTabClass())
        If debuggedProc.TryGetValue(className, info) Then
          tbProcInfo.Show() : tbProcInfo.Activate()
          tbProcInfo.PropertyGrid1.SelectedObject = info.proc
        End If

      Case "Debug.OpenExplorer"
        Dim tab As Object = IDE.getActiveTab
        If tab Is Nothing Then Exit Sub

        Dim className = IO.Path.GetFileNameWithoutExtension(tab.URL)

        Dim sh As New scHostNET2(className, 0)
        Process.Start("explorer.exe", "/e," + sH.targetFolder)
        'Process.Start("explorer.exe", "/e," + ParaService.SettingsFolder)

      Case "Window.Reflection"
        tbInfoTips.Show() : tbInfoTips.Activate()

      Case "Window.SHCompilerErrors"
        sH.ErrorListVisible = True

      Case "Window.SHDebug"
        sH.InformationWindowVisible = True

      Case "Window.SHPrintline"
        sH.PrintLineWndVisible = True

      Case "Reflection.Lookup"
        Dim tab = IDE.getActiveTab
        tbInfoTips.Show() : tbInfoTips.Activate()
        If tab IsNot Nothing Then btn_lookupType(tab)

    End Select
  End Sub

  <FileAction("*.vb;*.cs;*.vbs", "FindType", "", HandlesKeyString:="F2")> _
  Public Sub btn_lookupType(ByVal tab As Object)
    tbInfoTips.Show()
    tbInfoTips.fetchFromCode(tab)
  End Sub
  <FileAction("*.vb;*.cs;*.vbs", "Kompilieren", "http://mw.teamwiki.net/docs/img/icons/build.png", HandlesKeyString:="CTRL-F5")> _
  Public Sub btn_compileScript(ByVal tab As Object)
    compileActiveFile(tab)
  End Sub
  <FileAction("*.vb;*.cs;*.vbs", "Debug", "http://mw.teamwiki.net/docs/img/win-icons/debug-run.png", HandlesKeyString:="F5")> _
  Public Sub btn_autostartScript(ByVal tab As Object)
    autostartActiveFile(tab)
  End Sub

  '<FileAction("*.vb;*.cs", "", "http://mw.teamwiki.net/docs/img/win-icons/debug-stop.png", HandlesKeyString:="CTRL-F12")> _
  'Public Sub btn_debugStop(ByVal tab As Object)
  '  If debuggedProc IsNot Nothing Then
  '    debuggedProc.Kill()
  '  End If
  '  debugStateChange()
  'End Sub
  '<FileAction("*.vb;*.cs", "", "http://mw.teamwiki.net/docs/img/win-icons/msinfo32_128-16.png")> _
  'Public Sub btn_debugInfo(ByVal tab As Object)
  '  If debuggedProc IsNot Nothing Then
  '    tbProcInfo.Show() : tbProcInfo.Activate()
  '    tbProcInfo.PropertyGrid1.SelectedObject = debuggedProc
  '  End If
  'End Sub

  <FileAction("*.vb;*.cs;*.vbs", "", "http://mw.teamwiki.net/docs/img/win-icons/explorer_101-16.png")> _
  Public Sub btn_openExplorer(ByVal tab As Object)
    If tab Is Nothing Then Exit Sub
    If tab.Dirty Then tab.onSave()

    Dim className = IO.Path.GetFileNameWithoutExtension(tab.URL)

    Dim sh As New scHostNET2(className, 0)
    Process.Start("explorer.exe", "/e," + sh.targetFolder)
  End Sub

  Function getScriptRuntimeMode(ByVal fileSpec As String) As String
    If IO.File.Exists(fileSpec) = False Then Return ""
    Using sr As New System.IO.StreamReader(fileSpec)
      While Not sr.EndOfStream
        Dim rl = UCase(Trim(sr.ReadLine))
        If rl.StartsWith("#RUNTIME ") Then Return Trim(rl.Substring(9))
      End While
    End Using
    Return ""
  End Function

  Sub autostartActiveFileInt(ByVal fileName As String, ByVal onlyCompile As Boolean)
    On Error Resume Next
    ScriptHost.Instance.RecompileScriptClass(fileName)
    If onlyCompile Then
      ScriptHost.Instance.scriptClass(fileName)
    Else
      Dim ref = ScriptHost.Instance.scriptClass(fileName)
      ref.autoStart()
    End If
  End Sub
  Public Sub autostartActiveFile(ByVal tab As Object)
    If tab Is Nothing Then Exit Sub
    If tab.Dirty Then tab.onSave()

    Dim fileName = ProtocolService.MapToLocalFilename(tab.URL)

    Dim break = sH.globBreakMode(fileName)

    Dim host As IScriptClassHost = sH.getScriptClassHost(fileName, True)
    If host.debugMode = RuntimeMode.IDE Then
      If break = "EinzelSchritt" Or break = "BREAK" Then sH.globBreakMode(fileName) = "" : Exit Sub
      ' autostartActiveFileInt(fileName, False)
      Dim cls = host.getClassRef()
      cls.autoStart()
      Exit Sub
    End If

    If break <> "" Then Exit Sub

    'host.compileScript(True, exeName)

    If host.assemblyFilespec <> "" Then
      Dim proc As Process
      IDE.executeProgramInConsole(host.assemblyFilespec, proc, IO.Path.GetDirectoryName(host.assemblyFilespec))
      AddHandler proc.Exited, AddressOf debuggedProc_Exited
      'debuggedProc = proc

      debuggedProc(LCase(fileName)) = New debugInfo(fileName, tab.URL, proc)
      sH.globBreakMode(fileName) = "RUN"
      'debuggedScript = IDE.getActiveTabFilespec()
      'debugStateChange()

      If tbProcInfo.Visible Then
        tbProcInfo.Text = "Debug: " + proc.ProcessName
        tbProcInfo.PropertyGrid1.SelectedObject = proc
      End If
    End If
  End Sub
  Public Sub compileActiveFile(ByVal tab As Object)
    If tab Is Nothing Then Exit Sub
    If tab.Dirty Then tab.onSave()

    Dim fileName = ProtocolService.MapToLocalFilename(tab.URL)

    Dim host As IScriptClassHost = sH.getScriptClassHost(fileName, True, cls_preprocVB2.PPD_RELEASE)
    If host.debugMode <> RuntimeMode.Debug Then
      MsgBox("Als Zielumgebung für dieses Script ist '" + host.debugMode.ToString + "' eingetragen. Verwende das Flag #Runtime EXE, um das Script zu einer eigenständig ausführbaren Datei kompilieren zu können.", MsgBoxStyle.Exclamation, "Script kompilieren")
      Exit Sub
    End If
    If sH.globBreakMode(fileName) <> "" Then Exit Sub
    ' Dim exeName As String
    'host.compileScript(False, exeName)
    If host.assemblyFilespec <> "" Then
      If MsgBox("Das Script " + host.className + " wurde erfolgreich kompiliert. Soll die EXE-Datei jetzt ausgeführt werden?" + _
                vbNewLine + vbNewLine + host.assemblyFilespec, MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = MsgBoxResult.Yes Then
        Process.Start(host.assemblyFilespec)
      End If
    End If
  End Sub


  Private Delegate Sub debuggedProcExitedDelegate(ByVal info As debugInfo)
  Private Sub debuggedProcExited(ByVal info As debugInfo)
    debuggedProc.Remove(info.ScriptFile)
    sH.globBreakMode(info.ScriptFile) = ""
  End Sub

  Private Sub debuggedProc_Exited(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles debuggedProc.Exited
    'debuggedProc = Nothing
    Dim info As debugInfo
    For Each el In debuggedProc
      If el.Value.proc Is sender Then info = el.Value
    Next
    'If debuggedProc.TryGetValue(sender.ProcessName, info) = False Then Exit Sub
    If info.proc Is Nothing Then Exit Sub
    'Nötig, da die in der Process-Klasse geschlurt haben und das Exited-Event in irgendeinem fremden
    'Thread ausgeführt wird
    DirectCast(IDE.getMainFormRef(), Windows.Forms.Form).Invoke(New debuggedProcExitedDelegate(AddressOf debuggedProcExited), info)
    'DirectCast(IDE.getMainFormRef(), Windows.Forms.Form).Invoke(New Threading.ThreadStart(AddressOf debugStateChange))

  End Sub



End Module
