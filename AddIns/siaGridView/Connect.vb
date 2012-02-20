<SIA("Dateiformathandler für IGrid", "Dateiformathandler für IGrid", _
                           IconRessourceName:="siaCodeCompiler.SHELL32_275.ico")> _
Public Class Connect
  Inherits MarshalByRefObject
  Implements IAddinConnect

  Public Sub Connect(ByVal application As Object, ByVal connectMode As Core.ConnectMode, ByVal addInInst As Core.AddinInstance, ByRef custom As Object) Implements Core.IAddinConnect.Connect
    IDE = Main.cls_IDEHelper.Instance

    'Dim tb = IDE.CreateToolbar("siaCodeCompiler.compileTB")
    'tb.addButton("cc_compileScript", "Compile", , , , , , "http://mw.teamwiki.net/docs/img/icons/build.png")
    'tb.addButton("cc_debugRun", "Debug", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/debug-run.png")
    'tb.addButton("cc_debugStop", "", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/debug-stop.png")
    'tb.addButton("cc_debugInfo", "", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/msinfo32_128-16.png")
    'tb.addButton("cc_konsole", "", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/VB6_1254-16.png")
    'tb.addButton("cc_explorer", "", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/explorer_101-16.png")
    'tb.addLabel("cc_debugMode", "Ready", "#bbb")

    'IDE.RegisterAddinWindow(tbInfoTips_ID)
    IO.Directory.CreateDirectory(IDE.GetSettingsFolder() + "gridData\")
  End Sub

  Function CreateGenericGrid(Optional ByVal id As String = Nothing)
    If id Is Nothing Then id = Now.Ticks.ToString
    Return IDE.NavigateFile("generic:/" + id + ".grid")
  End Function

  Public Sub Disconnect(ByVal removeMode As Core.DisconnectMode, ByRef custom As Object) Implements Core.IAddinConnect.Disconnect
    'MsgBox("IDEDisconnect - siaTestAddin")
    'IDE.RemoveToolbar("siaCodeCompiler.compileTB")
    'IDE.UnregisterAddinWindow(tbInfoTips_ID)
  End Sub

  Public Function GetAddinWindow(ByVal PersistString As String) As Form Implements IAddinConnect.GetAddinWindow
    'Select Case PersistString.ToUpper
    '  Case tbProcInfo_ID.ToUpper
    '    Return tbProcInfo
    'End Select
  End Function


  Public Sub OnNavigate(ByVal kind As Core.NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object) Implements Core.IAddinConnect.OnNavigate

  End Sub

  Public Sub OnAddinUpdate(ByVal addinChanged As String, ByRef custom As Object) Implements Core.IAddinConnect.OnAddinUpdate

  End Sub

  Public Sub OnBeforeShutdown(ByRef cancel As Boolean, ByRef custom As Object) Implements Core.IAddinConnect.OnBeforeShutdown

  End Sub

  Public Sub OnStartupComplete(ByRef custom As Object) Implements Core.IAddinConnect.OnStartupComplete

  End Sub
End Class
