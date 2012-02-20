Imports System.Windows.Forms

Public Class Connect
  Implements IAddinConnect

  Public Sub Connect(ByVal application As Object, ByVal connectMode As Core.ConnectMode, ByVal addInInst As Core.AddinInstance, ByRef custom As Object) Implements Core.IAddinConnect.Connect
    IDE = Main.cls_IDEHelper.Instance


    IDE.RegisterAddinWindow(tbDesigner_ID)

  End Sub


  Public Sub Disconnect(ByVal removeMode As Core.DisconnectMode, ByRef custom As Object) Implements Core.IAddinConnect.Disconnect
    IDE.UnregisterAddinWindow(tbDesigner_ID)
  End Sub

  Public Function GetAddinWindow(ByVal PersistString As String) As Form Implements IAddinConnect.GetAddinWindow
    Select Case PersistString.ToLower
      Case tbDesigner_ID.ToLower
        Return tbDesigner
    End Select
  End Function

  Public Sub OnNavigate(ByVal kind As Core.NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object) Implements Core.IAddinConnect.OnNavigate
    If command = "Window.Designer" Then
      tbDesigner.Show() : tbDesigner.Activate()
    End If
  End Sub

  Public Sub OnAddinUpdate(ByVal addinChanged As String, ByRef custom As Object) Implements Core.IAddinConnect.OnAddinUpdate

  End Sub

  Public Sub OnBeforeShutdown(ByRef cancel As Boolean, ByRef custom As Object) Implements Core.IAddinConnect.OnBeforeShutdown

  End Sub

  Public Sub OnStartupComplete(ByRef custom As Object) Implements Core.IAddinConnect.OnStartupComplete

  End Sub


End Class
