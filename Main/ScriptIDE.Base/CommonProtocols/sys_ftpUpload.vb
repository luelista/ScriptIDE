Imports System.Runtime.InteropServices

Module sys_ftpUpload
  Dim ftpHandle As IntPtr
  Dim hInet As IntPtr

  Private Declare Function InternetOpen Lib "wininet.dll" _
   Alias "InternetOpenA" _
  (ByVal lpszAgent As String, _
   ByVal dwAccessType As Integer, _
   ByVal lpszProxyName As IntPtr, _
   ByVal lpszProxyBypass As IntPtr, _
   ByVal dwFlags As Integer) As Integer

  Private Declare Function InternetCloseHandle Lib "wininet.dll" ( _
  ByVal hInet As IntPtr) As Boolean

  Private Declare Auto Function InternetConnect Lib "wininet.dll" ( _
      ByVal hInternetSession As System.IntPtr, _
      ByVal sServerName As String, _
      ByVal nServerPort As Integer, _
      ByVal sUsername As String, _
      ByVal sPassword As String, _
      ByVal lService As Int32, _
      ByVal lFlags As Int32, _
      ByVal lContext As System.IntPtr) As System.IntPtr

  'Public Declare Function FtpGetFile Lib "wininet.dll" (ByVal hConnect As IntPtr, _
  '   ByVal remoteFile As String, ByVal newFile As String, _
  '   <MarshalAs(UnmanagedType.Bool)> ByVal failIfExists As Boolean, _
  '   ByVal flagsAndAttributes As Integer, ByVal flags As Integer, _
  '   ByVal context As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
  <DllImport("wininet.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
  Private Function FtpGetFile(ByVal hConnect As IntPtr, ByVal remoteFile As String, ByVal newFile As String, <MarshalAs(UnmanagedType.Bool)> _
  ByVal failIfExists As Boolean, ByVal flagsAndAttributes As Integer, ByVal flags As Integer, _
  ByVal context As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
  End Function

  Private Declare Function FtpPutFile Lib "wininet.dll" Alias "FtpPutFileA" (ByVal hFtpSession As IntPtr, ByVal lpszLocalFile As String, ByVal lpszRemoteFile As String, ByVal dwFlags As Integer, ByVal dwContext As Integer) As Boolean

  Private Declare Function FtpFindFirstFile Lib "wininet.dll" Alias "FtpFindFirstFileW" _
   (ByVal hConnect As IntPtr, ByVal searchFile As String, _
   ByRef findFileData As WIN32_FIND_DATA, ByVal flags As Integer, _
   ByVal context As IntPtr) As IntPtr

  Private Declare Function InternetFindNextFile Lib "wininet.dll" Alias "InternetFindNextFileW" _
   (ByVal hFind As IntPtr, ByRef findFileData As WIN32_FIND_DATA) _
   As <MarshalAs(UnmanagedType.Bool)> Boolean

  ' The CharSet must match the CharSet of the corresponding PInvoke signature
  <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
 Private Structure WIN32_FIND_DATA
    Public dwFileAttributes As UInteger
    Public ftCreationTime As System.Runtime.InteropServices.ComTypes.FILETIME
    Public ftLastAccessTime As System.Runtime.InteropServices.ComTypes.FILETIME
    Public ftLastWriteTime As System.Runtime.InteropServices.ComTypes.FILETIME
    Public nFileSizeHigh As UInteger
    Public nFileSizeLow As UInteger
    Public dwReserved0 As UInteger
    Public dwReserved1 As UInteger
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> Public cFileName As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> Public cAlternateFileName As String
  End Structure

  Private Declare Function FtpSetCurrentDirectory Lib "wininet.dll" Alias "FtpSetCurrentDirectoryW" (ByVal hConnect As IntPtr, ByVal lpszDirectory As String) As Boolean

  <DllImport("wininet.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
  Private Function FtpCreateDirectory(ByVal hConnect As IntPtr, ByVal lpszDirectory As String) As Boolean
  End Function

  Private Declare Function FtpRenameFile Lib "wininet.dll" Alias "FtpRenameFileA" _
              (ByVal hConnect As Integer, ByVal lpszExisting As String, _
              ByVal lpszNew As String) As Boolean


  Sub ftpConnect(ByVal server As String, ByVal port As Integer, ByVal user As String, ByVal pass As String)
    hInet = WinInet.InternetOpen("Mozilla/mw/ftp/SIDE4...", WinInet.INTERNET_OPEN_TYPE_DIRECT, IntPtr.Zero, IntPtr.Zero, WinInet.INTERNET_FLAG_NO_CACHE_WRITE)
    '    WinInet.INTERNET_DEFAULT_FTP_PORT
    ftpHandle = WinInet.InternetConnect(hInet, server, port, _
              user, pass, WinInet.INTERNET_SERVICE_FTP, _
              WinInet.INTERNET_FLAG_EXISTING_CONNECT Or WinInet.INTERNET_FLAG_PASSIVE, IntPtr.Zero)

  End Sub

  Function ftpDelete(ByVal path As String, ByVal isDir As Boolean) As Integer
    If isDir Then
      Return WinInet.FtpRemoveDirectory(ftpHandle, path)
    Else
      Return WinInet.FtpDeleteFile(ftpHandle, path)
    End If
  End Function

  Function ftpCreateDir(ByVal directoryPath As String) As Integer
    Return FtpCreateDirectory(ftpHandle, directoryPath)
  End Function

  Function ftpRename(ByVal oldFile As String, ByVal newFile As String) As Integer
    Return FtpRenameFile(ftpHandle, oldFile, newFile)
  End Function

  Function ftpUpload(ByVal locFile As String, ByVal remoteFile As String) As Integer
    Dim res As Integer
    res = WinInet.FtpPutFile(ftpHandle, locFile, remoteFile, WinInet.FTP_TRANSFER_TYPE_BINARY, 0)

    Return res
  End Function
  Function ftpDownload(ByVal locFile As String, ByVal remoteFile As String) As Boolean
    Dim res As Boolean
    If IO.File.Exists(locFile) Then IO.File.Delete(locFile)
    res = FtpGetFile(ftpHandle, remoteFile, locFile, True, WinInet.FILE_ATTRIBUTE_ARCHIVE, WinInet.FTP_TRANSFER_TYPE_BINARY Or &H80000000, 0)
    Return res
  End Function

  Function ftpFileList(ByVal folder As String) As String
    Dim pData As WIN32_FIND_DATA
    Dim bRet As Boolean
    FtpSetCurrentDirectory(ftpHandle, folder)
    Dim hFind As IntPtr = FtpFindFirstFile(ftpHandle, folder + "*.*", pData, WinInet.INTERNET_FLAG_RELOAD Or _
                               WinInet.INTERNET_FLAG_NO_CACHE_WRITE, IntPtr.Zero)
    Dim out As New System.Text.StringBuilder()
    out.AppendLine("--> " + pData.cFileName + pData.cAlternateFileName)
    Do
      bRet = InternetFindNextFile(hFind, pData)

      If Not bRet Then
        Exit Do
      Else
        out.AppendLine("..." + pData.cFileName + pData.cAlternateFileName)
        'Pos = InStr(pData.cFileName, " ") ' The file name should be at the start of the line
        'If Pos > 0 Then
        '  strItemName = Trim(Left(pData.cFileName, Pos)) ' Get the file name
        'End If
        ' Add the name to a list - Could be a list box as below or Text box
        'Form1.List_Files.Items.Add(strItemName)
      End If
    Loop
    InternetCloseHandle(hFind) ' close the handle.
    Return out.ToString
  End Function


  Sub ftpClose()
    WinInet.InternetCloseHandle(ftpHandle)
    WinInet.InternetCloseHandle(hInet)
  End Sub

End Module
