Public Class Connect
  Implements IAddinConnect

  Public Sub Connect(ByVal application As Object, ByVal connectMode As Core.ConnectMode, ByVal addInInst As Core.AddinInstance, ByRef custom As Object) Implements Core.IAddinConnect.Connect
    IDE = Main.cls_IDEHelper.Instance

    'IDE.RegisterAddinWindow(tbTwajaxExplorer_ID)

    'Dim tb = IDE.CreateToolbar("siaTestAddin.infoTB")
    'tb.addIcon("scr_run", "http://mw.teamwiki.net/docs/img/win-icons/debug-run.png")
    'tb.addIcon("scr_singlestep", "http://mw.teamwiki.net/docs/img/win-icons/debug-single.png")
    'tb.addIcon("scr_pause", "http://mw.teamwiki.net/docs/img/win-icons/debug-pause.png")
    'tb.addIcon("scr_stop", "http://mw.teamwiki.net/docs/img/win-icons/debug-stop.png")
    'tb.addTextbox("actfile", 200)
  End Sub


  Public Sub Disconnect(ByVal removeMode As Core.DisconnectMode, ByRef custom As Object) Implements Core.IAddinConnect.Disconnect
    'MsgBox("IDEDisconnect - siaTestAddin")
    'IDE.RemoveToolbar("siaTestAddin.infoTB")
    'tbTwajaxExplorer.Close()
    'tbTwajaxExplorer = Nothing
  End Sub

  Public Function GetAddinWindow(ByVal PersistString As String) As Form Implements IAddinConnect.GetAddinWindow
    'Select Case PersistString.ToLower
    '  Case tbTwajaxExplorer_ID.ToLower
    '    Return tbTwajaxExplorer
    'End Select
    Return Nothing
  End Function


  Public Sub OnNavigate(ByVal kind As Core.NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object) Implements Core.IAddinConnect.OnNavigate
    If command = "HexEdit.ShowActiveFile" Then
      ShowHexViewer(IDE.getActiveTab())
    End If
    If command = "HexEdit.FileOpen" Then
      Using ofd As New OpenFileDialog
        ofd.Title = "Datei im HEX-Editor öffnen ..."
        ofd.Filter = "Alle Dateien (*.*)|*.*"
        If ofd.ShowDialog = DialogResult.OK Then
          IDE.NavigateFile("[.hex]" + ofd.FileName)
        End If
      End Using
    End If
  End Sub

  Public Sub OnAddinUpdate(ByVal addinChanged As String, ByRef custom As Object) Implements Core.IAddinConnect.OnAddinUpdate

  End Sub

  Public Sub OnBeforeShutdown(ByRef cancel As Boolean, ByRef custom As Object) Implements Core.IAddinConnect.OnBeforeShutdown

  End Sub

  Public Sub OnStartupComplete(ByRef custom As Object) Implements Core.IAddinConnect.OnStartupComplete

  End Sub

End Class
