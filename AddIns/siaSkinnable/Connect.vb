Public Class Connect
  Implements IAddinConnect

  Public Sub Connect(ByVal application As Object, ByVal connectMode As Core.ConnectMode, ByVal addInInst As Core.AddinInstance, ByRef custom As Object) Implements Core.IAddinConnect.Connect
    IDE = Main.cls_IDEHelper.Instance

    IO.Directory.CreateDirectory(IDE.GetSettingsFolder() + "skins\")

    'Dim tb = IDE.CreateToolbar("siaSkinnable.infoTB")
    'tb.addIcon("scr_run", "http://mw.teamwiki.net/docs/img/win-icons/debug-run.png")
    'tb.addIcon("scr_singlestep", "http://mw.teamwiki.net/docs/img/win-icons/debug-single.png")
    'tb.addIcon("scr_pause", "http://mw.teamwiki.net/docs/img/win-icons/debug-pause.png")
    'tb.addIcon("scr_stop", "http://mw.teamwiki.net/docs/img/win-icons/debug-stop.png")

    'tb.addTextbox("actfile", 200)

    
  End Sub


  Public Sub Disconnect(ByVal removeMode As Core.DisconnectMode, ByRef custom As Object) Implements Core.IAddinConnect.Disconnect
    'MsgBox("IDEDisconnect - siaTestAddin")
    'IDE.RemoveToolbar("siaSkinnable.infoTB")
  End Sub

  Public Function GetAddinWindow(ByVal PersistString As String) As Form Implements IAddinConnect.GetAddinWindow
    Return Nothing 'hat keine Fenster
  End Function

  Public Sub OnNavigate(ByVal kind As Core.NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object) Implements Core.IAddinConnect.OnNavigate
    If command = "Window.SkinOptions" Then
      IDE.ShowOptionsDialog("skinmanager")
    End If
  End Sub

  Public Sub OnAddinUpdate(ByVal addinChanged As String, ByRef custom As Object) Implements Core.IAddinConnect.OnAddinUpdate

  End Sub

  Public Sub OnBeforeShutdown(ByRef cancel As Boolean, ByRef custom As Object) Implements Core.IAddinConnect.OnBeforeShutdown

  End Sub

  Public Sub OnStartupComplete(ByRef custom As Object) Implements Core.IAddinConnect.OnStartupComplete
    If IDE.Glob.para("siaSkinnable__lastselskin") <> "" Then
      Try
        readSkin(IDE.Glob.para("siaSkinnable__lastselskin"))
      Catch ex As Exception
      End Try
    End If
  End Sub


End Class
