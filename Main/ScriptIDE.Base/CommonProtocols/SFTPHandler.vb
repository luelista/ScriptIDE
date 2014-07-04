Public Class SFTPHandler
  Implements IProtocolHandler

  Public Sub NavigateFilelistTo(ByVal internalURL As String) Implements IProtocolHandler.NavigateFilelistTo
    Dim parts() = Split(internalURL, "/", 3) '1=sftp: 2=domain.tld 3=path/file
    Dim directory = "/" + parts(2).Substring(0, parts(2).LastIndexOf("/") + 1)
    fillFtpFilelist(parts(1), directory, True)
    ShowFilelist()
  End Sub

  Public Function ReadFile(ByVal internalURL As String) As String Implements IProtocolHandler.ReadFile
    Dim parts() = Split(internalURL, "/", 3) '1=sftp: 2=domain.tld 3=path/file
    Dim con = getSftpConnection(parts(1))
    Return con.ReadAllText("/" + parts(2))
  End Function

  Public Sub SaveFile(ByVal internalURL As String, ByVal data As String) Implements IProtocolHandler.SaveFile
    Dim parts() = Split(internalURL, "/", 3) '1=sftp: 2=domain.tld 3=path/file
    Dim con = getSftpConnection(parts(1))
    con.WriteAllText("/" + parts(2), data)
  End Sub

  Public Sub ShowFilelist() Implements IProtocolHandler.ShowFilelist
    tbFtpExplorer.Show()
    tbFtpExplorer.Activate()
  End Sub

  Public Sub New()
    If ftpConnections.Count = 0 Then readFtpConnections()
  End Sub

  Public Function MapToLocalFilename(ByVal internalURL As String) As String Implements IProtocolHandler.MapToLocalFilename
    Dim parts() = Split(internalURL, "/", 3) '1=sftp: 2=domain.tld 3=path/file
    Dim loc = ftpGetLocalAlias(parts(1), parts(2)) 'schummel - damit geht das tab-umschalten deutlich schneller
    If IO.File.Exists(loc) = False Then ftpReadTextFile(parts(1), parts(2))
    Return loc
  End Function

End Class
