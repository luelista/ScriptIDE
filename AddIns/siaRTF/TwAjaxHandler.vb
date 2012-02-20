<ProtocolHandler("twajax")> Public Class TwAjaxHandler
  Implements IProtocolHandler

  Public Sub NavigateFilelistTo(ByVal internalURL As String) Implements IProtocolHandler.NavigateFilelistTo
    Dim parts() = Split(internalURL, "/", 3)  '1=twajax: 2=app 3=path/file
    Dim directory = "/" + parts(2).Substring(0, parts(2).LastIndexOf("/") + 1)
    'fillFtpFilelist(parts(1), directory, True)
    ShowFilelist()
  End Sub

  Public Function ReadFile(ByVal internalURL As String) As String Implements IProtocolHandler.ReadFile
    Dim parts() = Split(internalURL, "/", 3)  '1=twajax: 2=app 3=path/file
    Return TwAjax.ReadFile(parts(1), parts(2))
  End Function

  Public Sub SaveFile(ByVal internalURL As String, ByVal data As String) Implements IProtocolHandler.SaveFile
    Dim parts() = Split(internalURL, "/", 3)  '1=twajax: 2=app 3=path/file
    TwAjax.SaveFile(parts(1), parts(2), data)
  End Sub

  Public Sub ShowFilelist() Implements IProtocolHandler.ShowFilelist
    tbTwajaxExplorer.Show()
    tbTwajaxExplorer.Activate()
  End Sub

  Public Sub New()
    ' If ftpConnections.Count = 0 Then readFtpConnections()
  End Sub

  Public Function MapToLocalFilename(ByVal internalURL As String) As String Implements IProtocolHandler.MapToLocalFilename
    Dim parts() = Split(internalURL, "/", 3)  '1=twajax: 2=app 3=path/file
    Dim fileName = IDE.GetSettingsFolder() + "loc_cache\" + parts(1) + "," + parts(2).Replace("\", ",")
    IO.File.WriteAllText(fileName, TwAjax.ReadFile(parts(1), parts(2)))
    Return fileName
  End Function

End Class
