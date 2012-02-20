Imports ScriptIDE.Main

Public Class Connect
  Implements IAddinConnect



  Public Sub Connect(ByVal application As Object, ByVal connectMode As Core.ConnectMode, ByVal addInInst As Core.AddinInstance, ByRef custom As Object) Implements Core.IAddinConnect.Connect
    'helper = pScriptHlp
    IDE = Main.cls_IDEHelper.Instance

    'Dim tb = IDE.CreateToolbar("siaCodeCompiler.compileTB")
    'tb.addButton("cc_compileScript", "Compile", , , , , , "http://mw.teamwiki.net/docs/img/icons/build.png")
    'tb.addButton("cc_debugRun", "Debug", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/debug-run.png")
    'tb.addButton("cc_debugStop", "", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/debug-stop.png")
    'tb.addButton("cc_debugInfo", "", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/msinfo32_128-16.png")
    'tb.addButton("cc_konsole", "", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/VB6_1254-16.png")
    ''tb.addButton("cc_explorer", "", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/explorer_101-16.png")
    'tb.addLabel("cc_debugMode", "Ready", "#bbb")

    IO.Directory.CreateDirectory(ParaService.SettingsFolder + "addIns\")
    IO.Directory.CreateDirectory(ParaService.SettingsFolder + "dllCache\")
    IO.Directory.CreateDirectory(ParaService.SettingsFolder + "preprocTest\")
    IO.Directory.CreateDirectory(ParaService.SettingsFolder + "temp\")
    IO.Directory.CreateDirectory("C:\yEXE\scriptEXE\")

    debugStateChange(getActiveTabClass, "")

    'interproc_init()

    sH.initIDEMode()
    sH.setIdeHelper(cls_IDEHelper.GetSingleton)

    LoadAllScriptAddins()

    If connectMode <> Core.ConnectMode.Startup Then
      AutostartScriptclsOnBoot()
    End If
  End Sub

  Sub AutostartScriptclsOnBoot()
    On Error Resume Next
    sH.SilentMode = True

    Dim ref As Object
    TT.Write("load", "777b", "dump")
    Workbench.SetSplashText("Skripte werden geladen ...", True)
    If Not Command().ToLower.Contains("/noautostart") Then
      Dim autostarts() = Split(ParaService.Glob.para("scriptClsAutostart"), "|##|")
      For i = 0 To autostarts.Length - 1
        If autostarts(i).Trim = "" Then Continue For
        TT.Write("Autostart-Skriptclass", autostarts(i), "dump")
        ref = Nothing
        ref = sH.scriptClass(autostarts(i))

        If ref IsNot Nothing Then ref.AutoStart()
      Next
    End If

    sH.resetBreakMode()
    sH.SilentMode = False
  End Sub

  Public Sub Disconnect(ByVal removeMode As Core.DisconnectMode, ByRef custom As Object) Implements Core.IAddinConnect.Disconnect
    'MsgBox("IDEDisconnect - siaTestAddin")
    'IDE.RemoveToolbar("siaCodeCompiler.compileTB")
  End Sub

  Public Sub autostartActiveFile()
    app_main.autostartActiveFile(IDE.getActiveTab())
  End Sub
  Public Sub compileActiveFile()
    app_main.compileActiveFile(IDE.getActiveTab())
  End Sub

  Public Function GetAddinWindow(ByVal PersistString As String) As Form Implements IAddinConnect.GetAddinWindow
    Select Case PersistString.ToUpper
      Case tbInfoTips_ID.ToUpper
        Return tbInfoTips
      Case tbProcInfo_ID.ToUpper
        Return tbProcInfo
      Case tbDebug_ID.ToUpper
        Return sH.getInformationWindowRef()
      Case tbCompileErr_ID.ToUpper
        Return sH.getErrorListRef()
      Case tbPrintline_ID.ToUpper
        Return sH.getPrintlineWndRef()
    End Select
  End Function

  Public Sub OnNavigate(ByVal kind As Core.NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object) Implements Core.IAddinConnect.OnNavigate

    If UCase(command) = "SCRIPTCALLBACK" Then
      Try
        Dim para() = Split(args, ",", 3)
        Dim ref = sH.scriptClass(para(0))
        Dim funcPara As String = ""
        If para.Length > 2 Then funcPara = para(2)
        returnValue = CallByName(ref, para(1), CallType.Method, kind, source, para(2))
      Catch ex As Exception
        TT.Write("Error in OnNavigate(ScriptCallback)", ex.ToString, "warn")
      End Try
    End If
    If kind = NavigationKind.ToolbarCommand Or kind = NavigationKind.FileCommand Then
      onToolbarEvent(command)
    End If
    If kind = NavigationKind.InterprocCommand Or kind = NavigationKind.InterprocDataRequest Then

    End If
  End Sub

  Public Sub OnAddinUpdate(ByVal addinChanged As String, ByRef custom As Object) Implements Core.IAddinConnect.OnAddinUpdate

  End Sub

  Public Sub OnBeforeShutdown(ByRef cancel As Boolean, ByRef custom As Object) Implements Core.IAddinConnect.OnBeforeShutdown

  End Sub

  Public Sub OnStartupComplete(ByRef custom As Object) Implements Core.IAddinConnect.OnStartupComplete
    AutostartScriptclsOnBoot()
  End Sub

End Class
