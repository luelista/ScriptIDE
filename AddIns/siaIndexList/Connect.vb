Imports ScriptIDE.Main

Public Class Connect
  Implements IAddinConnect

  Sub onIniDone()
    'hier Aktionen ausführen, bei denen die IDE vollständig geladen sein muss
  End Sub

  Public Sub Connect(ByVal application As Object, ByVal connectMode As Core.ConnectMode, ByVal addInInst As Core.AddinInstance, ByRef custom As Object) Implements Core.IAddinConnect.Connect
    IDE = Main.cls_IDEHelper.Instance

    If connectMode <> Core.ConnectMode.Startup Then
      onIniDone()
    End If
  End Sub

  Public Sub Disconnect(ByVal removeMode As Core.DisconnectMode, ByRef custom As Object) Implements Core.IAddinConnect.Disconnect
    
  End Sub

  Public Function GetAddinWindow(ByVal PersistString As String) As Form Implements IAddinConnect.GetAddinWindow
    Select Case PersistString.ToUpper
      'Case tbIndexList_ID.ToUpper
      '  Return tbIndexList

    End Select
  End Function

  Public Sub OnNavigate(ByVal kind As Core.NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object) Implements Core.IAddinConnect.OnNavigate
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
    onIniDone()

  End Sub

End Class
