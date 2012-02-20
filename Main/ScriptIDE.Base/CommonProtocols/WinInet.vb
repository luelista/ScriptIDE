Imports System.Runtime.InteropServices
Imports System.Text

Public Class WinInet


  Const ErrFolder As String = "\ErrorLogs"   'folder Name for errorlog text files
  Const ErrFile As String = "\errlog"       'errorlog text File Name
  Private iCount As Long                    'error message counter

  Public Const sSlash = "/"

  Public Const MAX_PATH As Long = 260
  Public Const FILE_ATTRIBUTE_ARCHIVE = &H20

  Public Const INTERNET_OPEN_TYPE_PRECONFIG = 0                      ' use registry configuration
  Public Const INTERNET_OPEN_TYPE_DIRECT = 1                         ' direct to net
  Public Const INTERNET_OPEN_TYPE_PROXY = 3                          ' via named proxy
  Public Const INTERNET_OPEN_TYPE_PRECONFIG_WITH_NO_AUTOPROXY = 4    ' prevent using java/script/INS

  Public Const INTERNET_FLAG_PASSIVE = &H8000000             ' used for FTP connections
  Public Const INTERNET_FLAG_RELOAD = &H80000000

  ' additional cache flags
  Public Const INTERNET_FLAG_NO_CACHE_WRITE = &H4000000      ' don't write this item to the cache
  Public Const INTERNET_FLAG_DONT_CACHE = INTERNET_FLAG_NO_CACHE_WRITE
  Public Const INTERNET_FLAG_MAKE_PERSISTENT = &H2000000     ' make this item persistent in cache
  Public Const INTERNET_FLAG_FROM_CACHE = &H1000000          ' use offline semantics
  Public Const INTERNET_FLAG_OFFLINE = INTERNET_FLAG_FROM_CACHE

  ' additional flags
  Public Const INTERNET_FLAG_SECURE = &H800000               ' use PCT/SSL if applicable (HTTP)
  Public Const INTERNET_FLAG_KEEP_CONNECTION = &H400000      ' use keep-alive semantics
  Public Const INTERNET_FLAG_NO_AUTO_REDIRECT = &H200000     ' don't handle redirections automatically
  Public Const INTERNET_FLAG_READ_PREFETCH = &H100000        ' do background read prefetch
  Public Const INTERNET_FLAG_NO_COOKIES = &H80000            ' no automatic cookie handling
  Public Const INTERNET_FLAG_NO_AUTH = &H40000               ' no automatic authentication handling
  Public Const INTERNET_FLAG_CACHE_IF_NET_FAIL = &H10000     ' return cache file if net request fails
  Public Const INTERNET_DEFAULT_FTP_PORT = 21                ' default for FTP servers
  Public Const INTERNET_DEFAULT_GOPHER_PORT = 70             '    "     "  gopher "
  Public Const INTERNET_DEFAULT_HTTP_PORT = 80               '    "     "  HTTP   "
  Public Const INTERNET_DEFAULT_HTTPS_PORT = 443             '    "     "  HTTPS  "
  Public Const INTERNET_DEFAULT_SOCKS_PORT = 1080            ' default for SOCKS firewall servers.
  Public Const INTERNET_FLAG_EXISTING_CONNECT = &H20000000   ' FTP: use existing InternetConnect handle for server if possible
  Public Const INTERNET_SERVICE_FTP = 1
  Public Const INTERNET_SERVICE_GOPHER = 2
  Public Const INTERNET_SERVICE_HTTP = 3

  'transfer flags
  Public Const FTP_TRANSFER_TYPE_UNKNOWN = &H0
  Public Const FTP_TRANSFER_TYPE_ASCII = &H1
  Public Const FTP_TRANSFER_TYPE_BINARY = &H2
  Public Const INTERNET_FLAG_TRANSFER_ASCII = FTP_TRANSFER_TYPE_ASCII
  Public Const INTERNET_FLAG_TRANSFER_BINARY = FTP_TRANSFER_TYPE_BINARY
  Public Const FTP_TRANSFER_TYPE_MASK = (FTP_TRANSFER_TYPE_ASCII Or _
                                         FTP_TRANSFER_TYPE_BINARY)

  'internet error flags
  Public Const INTERNET_ERROR_BASE = 12000
  Public Const ERROR_INTERNET_OUT_OF_HANDLES = (INTERNET_ERROR_BASE + 1)
  Public Const ERROR_INTERNET_TIMEOUT = (INTERNET_ERROR_BASE + 2)
  Public Const ERROR_INTERNET_EXTENDED_ERROR = (INTERNET_ERROR_BASE + 3)
  Public Const ERROR_INTERNET_INTERNAL_ERROR = (INTERNET_ERROR_BASE + 4)
  Public Const ERROR_INTERNET_INVALID_URL = (INTERNET_ERROR_BASE + 5)
  Public Const ERROR_INTERNET_UNRECOGNIZED_SCHEME = (INTERNET_ERROR_BASE + 6)
  Public Const ERROR_INTERNET_NAME_NOT_RESOLVED = (INTERNET_ERROR_BASE + 7)
  Public Const ERROR_INTERNET_PROTOCOL_NOT_FOUND = (INTERNET_ERROR_BASE + 8)
  Public Const ERROR_INTERNET_INVALID_OPTION = (INTERNET_ERROR_BASE + 9)
  Public Const ERROR_INTERNET_BAD_OPTION_LENGTH = (INTERNET_ERROR_BASE + 10)
  Public Const ERROR_INTERNET_OPTION_NOT_SETTABLE = (INTERNET_ERROR_BASE + 11)
  Public Const ERROR_INTERNET_SHUTDOWN = (INTERNET_ERROR_BASE + 12)
  Public Const ERROR_INTERNET_INCORRECT_USER_NAME = (INTERNET_ERROR_BASE + 13)
  Public Const ERROR_INTERNET_INCORRECT_PASSWORD = (INTERNET_ERROR_BASE + 14)
  Public Const ERROR_INTERNET_LOGIN_FAILURE = (INTERNET_ERROR_BASE + 15)
  Public Const ERROR_INTERNET_INVALID_OPERATION = (INTERNET_ERROR_BASE + 16)
  Public Const ERROR_INTERNET_OPERATION_CANCELLED = (INTERNET_ERROR_BASE + 17)
  Public Const ERROR_INTERNET_INCORRECT_HANDLE_TYPE = (INTERNET_ERROR_BASE + 18)
  Public Const ERROR_INTERNET_INCORRECT_HANDLE_STATE = (INTERNET_ERROR_BASE + 19)
  Public Const ERROR_INTERNET_NOT_PROXY_REQUEST = (INTERNET_ERROR_BASE + 20)
  Public Const ERROR_INTERNET_REGISTRY_VALUE_NOT_FOUND = (INTERNET_ERROR_BASE + 21)
  Public Const ERROR_INTERNET_BAD_REGISTRY_PARAMETER = (INTERNET_ERROR_BASE + 22)
  Public Const ERROR_INTERNET_NO_DIRECT_ACCESS = (INTERNET_ERROR_BASE + 23)
  Public Const ERROR_INTERNET_NO_CONTEXT = (INTERNET_ERROR_BASE + 24)
  Public Const ERROR_INTERNET_NO_CALLBACK = (INTERNET_ERROR_BASE + 25)
  Public Const ERROR_INTERNET_REQUEST_PENDING = (INTERNET_ERROR_BASE + 26)
  Public Const ERROR_INTERNET_INCORRECT_FORMAT = (INTERNET_ERROR_BASE + 27)
  Public Const ERROR_INTERNET_ITEM_NOT_FOUND = (INTERNET_ERROR_BASE + 28)
  Public Const ERROR_INTERNET_CANNOT_CONNECT = (INTERNET_ERROR_BASE + 29)
  Public Const ERROR_INTERNET_CONNECTION_ABORTED = (INTERNET_ERROR_BASE + 30)
  Public Const ERROR_INTERNET_CONNECTION_RESET = (INTERNET_ERROR_BASE + 31)
  Public Const ERROR_INTERNET_FORCE_RETRY = (INTERNET_ERROR_BASE + 32)
  Public Const ERROR_INTERNET_INVALID_PROXY_REQUEST = (INTERNET_ERROR_BASE + 33)
  Public Const ERROR_INTERNET_NEED_UI = (INTERNET_ERROR_BASE + 34)
  Public Const ERROR_INTERNET_HANDLE_EXISTS = (INTERNET_ERROR_BASE + 36)
  Public Const ERROR_INTERNET_SEC_CERT_DATE_INVALID = (INTERNET_ERROR_BASE + 37)
  Public Const ERROR_INTERNET_SEC_CERT_CN_INVALID = (INTERNET_ERROR_BASE + 38)
  Public Const ERROR_INTERNET_HTTP_TO_HTTPS_ON_REDIR = (INTERNET_ERROR_BASE + 39)
  Public Const ERROR_INTERNET_HTTPS_TO_HTTP_ON_REDIR = (INTERNET_ERROR_BASE + 40)
  Public Const ERROR_INTERNET_MIXED_SECURITY = (INTERNET_ERROR_BASE + 41)
  Public Const ERROR_INTERNET_CHG_POST_IS_NON_SECURE = (INTERNET_ERROR_BASE + 42)
  Public Const ERROR_INTERNET_POST_IS_NON_SECURE = (INTERNET_ERROR_BASE + 43)
  Public Const ERROR_INTERNET_CLIENT_AUTH_CERT_NEEDED = (INTERNET_ERROR_BASE + 44)
  Public Const ERROR_INTERNET_INVALID_CA = (INTERNET_ERROR_BASE + 45)
  Public Const ERROR_INTERNET_CLIENT_AUTH_NOT_SETUP = (INTERNET_ERROR_BASE + 46)
  Public Const ERROR_INTERNET_ASYNC_THREAD_FAILED = (INTERNET_ERROR_BASE + 47)
  Public Const ERROR_INTERNET_REDIRECT_SCHEME_CHANGE = (INTERNET_ERROR_BASE + 48)
  Public Const ERROR_INTERNET_DIALOG_PENDING = (INTERNET_ERROR_BASE + 49)
  Public Const ERROR_INTERNET_RETRY_DIALOG = (INTERNET_ERROR_BASE + 50)
  Public Const ERROR_INTERNET_HTTPS_HTTP_SUBMIT_REDIR = (INTERNET_ERROR_BASE + 52)
  Public Const ERROR_INTERNET_INSERT_CDROM = (INTERNET_ERROR_BASE + 53)
  Public Const ERROR_INTERNET_FORTEZZA_LOGIN_NEEDED = (INTERNET_ERROR_BASE + 54)
  Public Const ERROR_INTERNET_SEC_CERT_ERRORS = (INTERNET_ERROR_BASE + 55)
  Public Const ERROR_INTERNET_SEC_CERT_NO_REV = (INTERNET_ERROR_BASE + 56)
  Public Const ERROR_INTERNET_SEC_CERT_REV_FAILED = (INTERNET_ERROR_BASE + 57)

  Enum InternetError
    OUT_OF_HANDLES = (INTERNET_ERROR_BASE + 1)
    TIMEOUT = (INTERNET_ERROR_BASE + 2)
    EXTENDED_ERROR = (INTERNET_ERROR_BASE + 3)
    INTERNAL_ERROR = (INTERNET_ERROR_BASE + 4)
    INVALID_URL = (INTERNET_ERROR_BASE + 5)
    UNRECOGNIZED_SCHEME = (INTERNET_ERROR_BASE + 6)
    NAME_NOT_RESOLVED = (INTERNET_ERROR_BASE + 7)
    PROTOCOL_NOT_FOUND = (INTERNET_ERROR_BASE + 8)
    INVALID_OPTION = (INTERNET_ERROR_BASE + 9)
    BAD_OPTION_LENGTH = (INTERNET_ERROR_BASE + 10)
    OPTION_NOT_SETTABLE = (INTERNET_ERROR_BASE + 11)
    SHUTDOWN = (INTERNET_ERROR_BASE + 12)
    INCORRECT_USER_NAME = (INTERNET_ERROR_BASE + 13)
    INCORRECT_PASSWORD = (INTERNET_ERROR_BASE + 14)
    LOGIN_FAILURE = (INTERNET_ERROR_BASE + 15)
    INVALID_OPERATION = (INTERNET_ERROR_BASE + 16)
    OPERATION_CANCELLED = (INTERNET_ERROR_BASE + 17)
    INCORRECT_HANDLE_TYPE = (INTERNET_ERROR_BASE + 18)
    INCORRECT_HANDLE_STATE = (INTERNET_ERROR_BASE + 19)
    NOT_PROXY_REQUEST = (INTERNET_ERROR_BASE + 20)
    REGISTRY_VALUE_NOT_FOUND = (INTERNET_ERROR_BASE + 21)
    BAD_REGISTRY_PARAMETER = (INTERNET_ERROR_BASE + 22)
    NO_DIRECT_ACCESS = (INTERNET_ERROR_BASE + 23)
    NO_CONTEXT = (INTERNET_ERROR_BASE + 24)
    NO_CALLBACK = (INTERNET_ERROR_BASE + 25)
    REQUEST_PENDING = (INTERNET_ERROR_BASE + 26)
    INCORRECT_FORMAT = (INTERNET_ERROR_BASE + 27)
    ITEM_NOT_FOUND = (INTERNET_ERROR_BASE + 28)
    CANNOT_CONNECT = (INTERNET_ERROR_BASE + 29)
    CONNECTION_ABORTED = (INTERNET_ERROR_BASE + 30)
    CONNECTION_RESET = (INTERNET_ERROR_BASE + 31)
    FORCE_RETRY = (INTERNET_ERROR_BASE + 32)
    INVALID_PROXY_REQUEST = (INTERNET_ERROR_BASE + 33)
    NEED_UI = (INTERNET_ERROR_BASE + 34)
    HANDLE_EXISTS = (INTERNET_ERROR_BASE + 36)
    SEC_CERT_DATE_INVALID = (INTERNET_ERROR_BASE + 37)
    SEC_CERT_CN_INVALID = (INTERNET_ERROR_BASE + 38)
    HTTP_TO_HTTPS_ON_REDIR = (INTERNET_ERROR_BASE + 39)
    HTTPS_TO_HTTP_ON_REDIR = (INTERNET_ERROR_BASE + 40)
    MIXED_SECURITY = (INTERNET_ERROR_BASE + 41)
    CHG_POST_IS_NON_SECURE = (INTERNET_ERROR_BASE + 42)
    POST_IS_NON_SECURE = (INTERNET_ERROR_BASE + 43)
    CLIENT_AUTH_CERT_NEEDED = (INTERNET_ERROR_BASE + 44)
    INVALID_CA = (INTERNET_ERROR_BASE + 45)
    CLIENT_AUTH_NOT_SETUP = (INTERNET_ERROR_BASE + 46)
    ASYNC_THREAD_FAILED = (INTERNET_ERROR_BASE + 47)
    REDIRECT_SCHEME_CHANGE = (INTERNET_ERROR_BASE + 48)
    DIALOG_PENDING = (INTERNET_ERROR_BASE + 49)
    RETRY_DIALOG = (INTERNET_ERROR_BASE + 50)
    HTTPS_HTTP_SUBMIT_REDIR = (INTERNET_ERROR_BASE + 52)
    INSERT_CDROM = (INTERNET_ERROR_BASE + 53)
    FORTEZZA_LOGIN_NEEDED = (INTERNET_ERROR_BASE + 54)
    SEC_CERT_ERRORS = (INTERNET_ERROR_BASE + 55)
    SEC_CERT_NO_REV = (INTERNET_ERROR_BASE + 56)
    SEC_CERT_REV_FAILED = (INTERNET_ERROR_BASE + 57)
    FTP_TRANSFER_IN_PROGRESS = (INTERNET_ERROR_BASE + 110)
    FTP_DROPPED = (INTERNET_ERROR_BASE + 111)
    FTP_NO_PASSIVE_MODE = (INTERNET_ERROR_BASE + 112)
    FTP_NO_MORE_FILES = 18
  end Enum 

  ' FTP API errors
  Public Const ERROR_FTP_TRANSFER_IN_PROGRESS = (INTERNET_ERROR_BASE + 110)
  Public Const ERROR_FTP_DROPPED = (INTERNET_ERROR_BASE + 111)
  Public Const ERROR_FTP_NO_PASSIVE_MODE = (INTERNET_ERROR_BASE + 112)
  Public Const ERROR_FTP_NO_MORE_FILES = 18

  ' gopher API errors
  Public Const ERROR_GOPHER_PROTOCOL_ERROR = (INTERNET_ERROR_BASE + 130)
  Public Const ERROR_GOPHER_NOT_FILE = (INTERNET_ERROR_BASE + 131)
  Public Const ERROR_GOPHER_DATA_ERROR = (INTERNET_ERROR_BASE + 132)
  Public Const ERROR_GOPHER_END_OF_DATA = (INTERNET_ERROR_BASE + 133)
  Public Const ERROR_GOPHER_INVALID_LOCATOR = (INTERNET_ERROR_BASE + 134)
  Public Const ERROR_GOPHER_INCORRECT_LOCATOR_TYPE = (INTERNET_ERROR_BASE + 135)
  Public Const ERROR_GOPHER_NOT_GOPHER_PLUS = (INTERNET_ERROR_BASE + 136)
  Public Const ERROR_GOPHER_ATTRIBUTE_NOT_FOUND = (INTERNET_ERROR_BASE + 137)
  Public Const ERROR_GOPHER_UNKNOWN_LOCATOR = (INTERNET_ERROR_BASE + 138)

  ' HTTP API errors
  Public Const ERROR_HTTP_HEADER_NOT_FOUND = (INTERNET_ERROR_BASE + 150)
  Public Const ERROR_HTTP_DOWNLEVEL_SERVER = (INTERNET_ERROR_BASE + 151)
  Public Const ERROR_HTTP_INVALID_SERVER_RESPONSE = (INTERNET_ERROR_BASE + 152)
  Public Const ERROR_HTTP_INVALID_HEADER = (INTERNET_ERROR_BASE + 153)
  Public Const ERROR_HTTP_INVALID_QUERY_REQUEST = (INTERNET_ERROR_BASE + 154)
  Public Const ERROR_HTTP_HEADER_ALREADY_EXISTS = (INTERNET_ERROR_BASE + 155)
  Public Const ERROR_HTTP_REDIRECT_FAILED = (INTERNET_ERROR_BASE + 156)
  Public Const ERROR_HTTP_NOT_REDIRECTED = (INTERNET_ERROR_BASE + 160)
  Public Const ERROR_HTTP_COOKIE_NEEDS_CONFIRMATION = (INTERNET_ERROR_BASE + 161)
  Public Const ERROR_HTTP_COOKIE_DECLINED = (INTERNET_ERROR_BASE + 162)
  Public Const ERROR_HTTP_REDIRECT_NEEDS_CONFIRMATION = (INTERNET_ERROR_BASE + 168)

  ' additional Internet API error codes
  Public Const ERROR_INTERNET_SECURITY_CHANNEL_ERROR = (INTERNET_ERROR_BASE + 157)
  Public Const ERROR_INTERNET_UNABLE_TO_CACHE_FILE = (INTERNET_ERROR_BASE + 158)
  Public Const ERROR_INTERNET_TCPIP_NOT_INSTALLED = (INTERNET_ERROR_BASE + 159)
  Public Const ERROR_INTERNET_DISCONNECTED = (INTERNET_ERROR_BASE + 163)
  Public Const ERROR_INTERNET_SERVER_UNREACHABLE = (INTERNET_ERROR_BASE + 164)
  Public Const ERROR_INTERNET_PROXY_SERVER_UNREACHABLE = (INTERNET_ERROR_BASE + 165)
  Public Const ERROR_INTERNET_BAD_AUTO_PROXY_SCRIPT = (INTERNET_ERROR_BASE + 166)
  Public Const ERROR_INTERNET_UNABLE_TO_DOWNLOAD_SCRIPT = (INTERNET_ERROR_BASE + 167)
  Public Const ERROR_INTERNET_SEC_INVALID_CERT = (INTERNET_ERROR_BASE + 169)
  Public Const ERROR_INTERNET_SEC_CERT_REVOKED = (INTERNET_ERROR_BASE + 170)

  ' InternetAutodial specific errors
  Public Const ERROR_INTERNET_FAILED_DUETOSECURITYCHECK = (INTERNET_ERROR_BASE + 171)
  Public Const ERROR_INTERNET_NOT_INITIALIZED = (INTERNET_ERROR_BASE + 172)
  Public Const ERROR_INTERNET_NEED_MSN_SSPI_PKG = (INTERNET_ERROR_BASE + 173)
  Public Const ERROR_INTERNET_LOGIN_FAILURE_DISPLAY_ENTITY_BODY = (INTERNET_ERROR_BASE + 174)
  Public Const INTERNET_ERROR_LAST = (INTERNET_ERROR_BASE + 174)

  Structure FILETIME
    Public dwLowDateTime As Integer
    Public dwHighDateTime As Integer
  End Structure

  Structure WIN32_FIND_DATA
    Dim dwFileAttributes As Long
    Dim ftCreationTime As FILETIME
    Dim ftLastAccessTime As FILETIME
    Dim ftLastWriteTime As FILETIME
    Dim nFileSizeHigh As Long
    Dim nFileSizeLow As Long
    Dim dwReserved0 As Long
    Dim dwReserved1 As Long
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
      Dim cFileName As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> _
        Dim cAlternate As String
  End Structure

  Public Declare Function InternetOpen Lib "wininet.dll" _
   Alias "InternetOpenA" _
  (ByVal lpszAgent As String, _
   ByVal dwAccessType As Integer, _
   ByVal lpszProxyName As IntPtr, _
   ByVal lpszProxyBypass As IntPtr, _
   ByVal dwFlags As Integer) As Integer
  'Declare Auto Function InternetConnect Lib "wininet.dll" ( _
  '    ByVal hInternetSession As System.IntPtr, _
  '    ByVal sServerName As String, _
  '    ByVal nServerPort As Integer, _
  '    ByVal sUsername As String, _
  '    ByVal sPassword As String, _
  '    ByVal lService As Int32, _
  '    ByVal lFlags As Int32, _
  '    ByVal lContext As System.IntPtr) As System.IntPtr


  Public Declare Function InternetCloseHandle Lib "wininet.dll" ( _
  ByVal hInet As IntPtr) As Boolean

  Public Declare Auto Function InternetConnect Lib "wininet.dll" ( _
      ByVal hInternetSession As System.IntPtr, _
      ByVal sServerName As String, _
      ByVal nServerPort As Integer, _
      ByVal sUsername As String, _
      ByVal sPassword As String, _
      ByVal lService As Int32, _
      ByVal lFlags As Int32, _
      ByVal lContext As System.IntPtr) As System.IntPtr

  Public Declare Function FtpFindFirstFile Lib "wininet.dll" _
     (ByVal hConnect As IntPtr, ByVal searchFile As String, _
     ByRef findFileData As WIN32_FIND_DATA, ByVal flags As Integer, _
     ByVal context As IntPtr) As IntPtr

  Public Declare Function InternetFindNextFile Lib "wininet.dll" _
     (ByVal hFind As IntPtr, ByRef findFileData As WIN32_FIND_DATA) _
     As <MarshalAs(UnmanagedType.Bool)> Boolean

  Public Declare Function InternetGetLastResponseInfo Lib "wininet.dll" Alias "InternetGetLastResponseInfoW" _
     (ByRef errorCode As Integer, <MarshalAs(UnmanagedType.LPTStr)> ByVal buffer As StringBuilder, ByRef bufferLength As Integer) _
     As <MarshalAs(UnmanagedType.Bool)> Boolean

  Public Declare Function FtpGetCurrentDirectory Lib "wininet.dll" _
     (ByVal hConnect As IntPtr, ByVal directory As StringBuilder, ByRef bufferLength As Integer) _
     As <MarshalAs(UnmanagedType.Bool)> Boolean


  Public Declare Function FtpSetCurrentDirectory Lib "wininet.dll" Alias "FtpSetCurrentDirectoryA" (ByVal hConnect As IntPtr, ByVal lpszDirectory As String) As Boolean

  <DllImport("wininet.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
  Public Shared Function FtpCreateDirectory(ByVal hConnect As IntPtr, ByVal lpszDirectory As String) As Boolean
  End Function

  Public Declare Function FtpGetFile Lib "wininet.dll" (ByVal hConnect As IntPtr, _
     ByVal remoteFile As String, ByVal newFile As String, _
     <MarshalAs(UnmanagedType.Bool)> ByVal failIfExists As Boolean, _
     ByVal flagsAndAttributes As Integer, ByVal flags As Integer, _
     ByVal context As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean

  Public Declare Function FtpCommandA Lib "wininet.dll" _
  (ByVal hFtpSession As IntPtr, _
  <MarshalAs(UnmanagedType.Bool)> ByVal fExpectResponse As Boolean, _
  ByVal dwFlags As Integer, _
  ByVal lpszCommand As String, _
  ByVal dwContext As IntPtr, _
  ByVal phFtpCommand As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean

  Public Declare Function FtpDeleteFile Lib "wininet.dll" Alias "FtpDeleteFileA" _
     (ByVal hConnect As IntPtr, ByVal fileName As String) _
     As <MarshalAs(UnmanagedType.Bool)> Boolean

  Declare Function FtpRemoveDirectory Lib "wininet.dll" Alias "FtpRemoveDirectoryA" _
     (ByVal hConnect As IntPtr, ByVal fileName As String) _
     As <MarshalAs(UnmanagedType.Bool)> Boolean

  Public Declare Function FtpPutFile Lib "wininet.dll" Alias "FtpPutFileA" _
            (ByVal hFtpSession As IntPtr, ByVal lpszLocalFile As String, ByVal lpszRemoteFile As String, ByVal dwFlags As Integer, _
             ByVal dwContext As Integer) As Integer



  Public Declare Function FtpOpenFile Lib "wininet.dll" _
      Alias "FtpOpenFileA" _
      (ByVal hFtpSession As IntPtr, _
       ByVal sFileName As String, _
       ByVal lAccess As Integer, _
       ByVal lFlags As Integer, _
       ByVal lContext As Integer) As Integer


  Public Declare Function FtpRenameFile Lib "wininet.dll" Alias "FtpRenameFileA" _
                (ByVal hConnect As Integer, ByVal lpszExisting As String, _
                ByVal lpszNew As String) As Boolean






  Public Shared Function TranslateErrorCode(ByVal lErrorCode As Long) As String

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' From       : MSDN
    ' Name       : TranslateErrorCode
    ' Purpose    : Provides message corresponding to DLL error codes
    ' Parameters : The DLL error code
    ' Return val : String containing message
    ' Algorithm : Selects the appropriate string
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Dim sBuffer As New StringBuilder(256)
    Dim nBuffer As Long

    Select Case lErrorCode
      Case 12001 : TranslateErrorCode = "No more handles could be generated at this time"
      Case 12002 : TranslateErrorCode = "The request has timed out."
      Case 12003
        'extended error. Retrieve the details using
        'the InternetGetLastResponseInfo API.

        ' sBuffer = Space$(256)
        nBuffer = 256

        If InternetGetLastResponseInfo(lErrorCode, sBuffer, nBuffer) Then
          TranslateErrorCode = sBuffer.ToString
        Else
          TranslateErrorCode = "Extended error returned from server."
        End If

      Case 12004 : TranslateErrorCode = "An internal error has occurred."
      Case 12005 : TranslateErrorCode = "The URL is invalid."
      Case 12006 : TranslateErrorCode = "The URL scheme could not be recognized, or is not supported."
      Case 12007 : TranslateErrorCode = "The server name could not be resolved."
      Case 12008 : TranslateErrorCode = "The requested protocol could not be located."
      Case 12009 : TranslateErrorCode = "A request to InternetQueryOption or InternetSetOption specified an invalid option value."
      Case 12010 : TranslateErrorCode = "The length of an option supplied to InternetQueryOption or InternetSetOption is incorrect for the type of option specified."
      Case 12011 : TranslateErrorCode = "The request option can not be set, only queried. "
      Case 12012 : TranslateErrorCode = "The Win32 Internet support is being shutdown or unloaded."
      Case 12013 : TranslateErrorCode = "The request to connect and login to an FTP server could not be completed because the supplied user name is incorrect."
      Case 12014 : TranslateErrorCode = "The request to connect and login to an FTP server could not be completed because the supplied password is incorrect. "
      Case 12015 : TranslateErrorCode = "The request to connect to and login to an FTP server failed."
      Case 12016 : TranslateErrorCode = "The requested operation is invalid. "
      Case 12017 : TranslateErrorCode = "The operation was canceled, usually because the handle on which the request was operating was closed before the operation completed."
      Case 12018 : TranslateErrorCode = "The type of handle supplied is incorrect for this operation."
      Case 12019 : TranslateErrorCode = "The requested operation can not be carried out because the handle supplied is not in the correct state."
      Case 12020 : TranslateErrorCode = "The request can not be made via a proxy."
      Case 12021 : TranslateErrorCode = "A required registry value could not be located. "
      Case 12022 : TranslateErrorCode = "A required registry value was located but is an incorrect type or has an invalid value."
      Case 12023 : TranslateErrorCode = "Direct network access cannot be made at this time. "
      Case 12024 : TranslateErrorCode = "An asynchronous request could not be made because a zero context value was supplied."
      Case 12025 : TranslateErrorCode = "An asynchronous request could not be made because a callback function has not been set."
      Case 12026 : TranslateErrorCode = "The required operation could not be completed because one or more requests are pending."
      Case 12027 : TranslateErrorCode = "The format of the request is invalid."
      Case 12028 : TranslateErrorCode = "The requested item could not be located."
      Case 12029 : TranslateErrorCode = "The attempt to connect to the server failed."
      Case 12030 : TranslateErrorCode = "The connection with the server has been terminated."
      Case 12031 : TranslateErrorCode = "The connection with the server has been reset."
      Case 12036 : TranslateErrorCode = "The request failed because the handle already exists."
      Case Else : TranslateErrorCode = "Error details not available."
    End Select

  End Function

  Function StripNull(ByVal Item As String) As String

    'Return a string without the chr$(0) terminator.
    Dim pos As Integer
    pos = InStr(Item, Chr(0))

    If pos Then
      StripNull = Left$(Item, pos - 1)
    Else
      StripNull = Item
    End If

  End Function

  Public Function RightPath(ByVal sPath As String) As String
    If (Right(sPath, 1) <> "/" And Right(sPath, 1) <> "\") Then
      sPath = sPath & "\"
    End If
    RightPath = sPath
  End Function

  Public Function BackSlash(ByVal sPath As String) As String
    BackSlash = Replace(sPath, "/", "\")
  End Function

  Public Function FwdSlash(ByVal sPath As String) As String
    FwdSlash = Replace(sPath, "\", "/")
  End Function

  '  Public Sub ErrLog(ByVal Message As String, ByVal MsgType As Long)
  '    'Dim fs As New FileSystemObject
  '    'Dim TS As TextStream
  '    'Dim sErrFile As String
  '    'Dim i As Integer
  '    Dim sMsgType As String
  '    Dim tmp As String

  '    'On Error GoTo ErrHdl

  '    ''fs = New Scripting.FileSystemObject

  '    'iCount = iCount + 1

  '    '' make sure the folder for error loggin exists
  '    'If Not fs.FolderExists(App.Path & ErrFolder) Then
  '    '  fs.CreateFolder(App.Path & ErrFolder)
  '    'End If

  '    '' include the current date into the errorlog file name
  '    'sErrFile = ErrFolder & ErrFile & Format(Now, "yymmdd") & ".txt"

  '    '' make sure the errorlog file exists and open/create the file
  '    'If fs.FileExists(App.Path & sErrFile) Then
  '    '  TS = fs.OpenTextFile(App.Path & sErrFile, ForAppending)
  '    'Else
  '    '  TS = fs.CreateTextFile(App.Path & sErrFile)
  '    'End If

  '    'define type of error message
  '    Select Case MsgType
  '      Case 1
  '        sMsgType = " *Error*: "
  '      Case 2
  '        sMsgType = " !Warning: "
  '      Case 4
  '        sMsgType = " Info: "
  '      Case Else
  '        'unknown type of message
  '        If Len(Message) = 0 Then Exit Sub
  '        sMsgType = "Message received: "
  '    End Select
  '    'put line number, type of message and time stamp into the error message
  '    tmp = CStr(iCount) & sMsgType & "<" & Format(Now, "h:Nn:Ss") & "> " & Message
  '    Debug.Print("FTP: " + tmp)
  '    IO.File.AppendAllText("C:\ypara\ftplogfile.txt", tmp + vbNewLine)
  '    'write line into the errlog text file
  '    'TS.WriteLine(tmp)
  '    ''close text stream
  '    'TS.Close()

  '    Exit Sub

  'ErrHdl:
  '    ErrLog("Unexpected Error. Source: ErrLog, Error: " & Err.Description, 1)
  '  End Sub



  '  '********************************************************************************
  '  '   Class: clsFTP
  '  '--------------------------------------------------------------------------------
  '  '   Description:
  '  '   This class allows to connect to FTP site using WinInet API calls
  '  '   and download files from FTP Directory
  '  '---------------------------------------------------------------------------------
  '  '   Methods:     OpenConnection - opens FTP connection, gets internet Handle
  '  '                CloseConnection - closes FTP connection
  '  '                DownloadFile - downloads file from FTP site to defined destination
  '  '                UploadFile - copies a file from your local machine to a FTP site
  '  '                DeleteFile - deletes a file from a FTP site
  '  '                RenameFile - renames a file on a FTP site
  '  '                CheckFile - checks if specified file exist on FTP site
  '  '                EnumDirectory - Returns a list of files and folders in a path
  '  '                ChangeDirectory - Changes the current directory
  '  '----------------------------------------------------------------------------------
  '  '!  Copyright:
  '  '!      Go America Communications, Corp., 2000
  '  '!      Author: Matthew Arnheiter
  '  '----------------------------------------------------------------------------------
  '  '**********************************************************************************

  '  Private hInternet As Long       ' The handle to the Internet
  '  Private hConnect As Long        ' The handle to the Internet Connection
  '  Private sSite As String         ' The site to which we connect to
  '  Private sUser As String         ' The user name used to connect to the site
  '  Private sPassword As String     ' The password for the user

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  OpenConnection()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Creates a connection to an internet server using the
  '  ')  InternetOpen and InternetConnect calls from the
  '  ')  WinInet API.
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  Site - The site to connect to
  '  '$              User - The user name to log in with
  '  '$              Password - The password for the user
  '  '$              Directory - The default directory
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     Logical
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Public Function OpenConnection(ByVal Site As String, ByVal User As String, ByVal Password As String, _
  '                                  ByVal Directory As String) As Boolean
  '    ' Save the site information
  '    sSite = Site
  '    sUser = User
  '    sPassword = Password

  '    OpenConnection = False

  '    hInternet = InternetOpen("Media Guide Import App", INTERNET_OPEN_TYPE_DIRECT, _
  '        vbNullString, vbNullString, INTERNET_FLAG_NO_CACHE_WRITE)

  '    If hInternet Then
  '      'Get a handle to the connection
  '      hConnect = GetInternetConnectionHandle

  '      If hConnect <> 0 Then
  '        OpenConnection = True

  '        ' Set the intial path
  '        FtpSetCurrentDirectory(hConnect, Directory)

  '        ' Check if the initial path was set
  '        If Right(FwdSlash(Directory), 1) <> "/" Then Directory = Directory & "/"
  '        If UCase(GetFTPDirectory(hConnect)) <> UCase(Directory) Then
  '          ' Raise an Error
  '          ErrLog("clsFTP.OpenConnection: wrong FTP Directory. ", 1)
  '          End
  '        End If
  '      Else
  '        ' Raise an Error
  '        ErrLog("clsFTP.OpenConnection: GetInternetConnectionHandle failed.", 1)
  '        End
  '      End If
  '    End If
  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  CloseConnection()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Closes the connection to the Ineternet site
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  NONE
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     Logical
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Public Function CloseConnection() As Boolean
  '    CloseConnection = False

  '    On Error GoTo out

  '    ' Close the handles
  '    InternetCloseHandle(hConnect)
  '    InternetCloseHandle(hInternet)

  '    ' Set the handles to 0
  '    hInternet = 0
  '    hConnect = 0
  '    CloseConnection = True
  '    Exit Function
  'out:
  '    ErrLog("clsFTP.CloseConnection method failed. Error - " & Err.Description, 1)
  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  RetrieveFile()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Downloads a file from the FTP site to a local
  '  ')  directory.
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  FileName - The name of the file to retrieve
  '  '$              Destination - The local directory to save
  '  '$                  the file to.
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     N/A
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Public Function DownloadFile(ByVal FileName As String, ByVal Destination As String) As Boolean
  '    Dim sRemoteFile As String
  '    Dim sNewFile As String
  '    Dim sCurrentDir As String
  '    'Dim oFSO As Scripting.FileSystemObject

  '    On Error GoTo out

  '    DownloadFile = False

  '    'Only if a valid connection...
  '    If hConnect Then
  '      ' Get the Current Internet directory
  '      sCurrentDir = GetFTPDirectory(hConnect)

  '      ' Setup the file name to retrieve and the destination
  '      sRemoteFile = sCurrentDir & FileName
  '      If Right$(Destination, 1) <> "\" Then
  '        Destination = Destination & sSlash
  '      End If
  '      sNewFile = Destination & FileName

  '      'make sure there is no file with the same name as specified
  '      'oFSO = New Scripting.FileSystemObject

  '      'If oFSO.FileExists(sNewFile) Then
  '      'oFSO.DeleteFile(sNewFile)
  '      'End If
  '      If IO.File.Exists(sNewFile) Then IO.File.Delete(sNewFile)

  '      ' Download file
  '      If FtpGetFile(hConnect, sRemoteFile, sNewFile, False, FILE_ATTRIBUTE_ARCHIVE, _
  '          FTP_TRANSFER_TYPE_UNKNOWN, 0&) Then
  '        ' Success
  '        DownloadFile = True
  '        ErrLog("clsFTP.DownloadFile: Download Succeeded. Time:" & CStr(Now), 4)
  '      Else
  '        ' Raise an error
  '        ErrLog("clsFTP.DownloadFile: Download Failed. " & Err.Number & ":" & _
  '                                                        Err.Description, 1)
  '      End If
  '    End If
  '    Exit Function
  'out:
  '    ErrLog("clsFTP.DownloadFile method failed. Error - " & Err.Description, 1)
  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  UploadFile()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Copies a file from your local machine to an FTP site
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  LocalFile - The name of the file to upload
  '  '$              RemoteFile - The path an file name on the FTP site to upload to
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     N/A
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Public Function UploadFile(ByVal LocalFile As String, ByVal RemoteFile As String) As Boolean
  '    Dim bRet As Long

  '    bRet = FtpPutFile(hConnection, LocalFile, RemoteFile, FTP_TRANSFER_TYPE_BINARY, 0)

  '    If bRet = False Then
  '      'App.LogEvent("Devx Sample failed to upload file " & LocalFile)
  '      Exit Function
  '    End If
  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  DeleteFile()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Deletes a file from the FTP site directory.
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  FileName - The name of the file to retrieve
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     N/A
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Public Function DeleteFile(ByVal FileName As String) As Boolean
  '    DeleteFile = False

  '    If FtpDeleteFile(hConnect, FileName) Then
  '      DeleteFile = True
  '    Else
  '      ErrLog("clsFTP.DeleteFile: Could not delete file.", 2)
  '    End If
  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  RenameFile()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Changes the name of a file on an FTP Site
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  FileName - The name of the file to rename
  '  '               NewName - The name the file is changed to
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     N/A
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Public Function RenameFile(ByVal FileName As String, ByVal NewName As String) As Boolean
  '    Dim lRet As Long

  '    lRet = FtpRenameFile(hConnect, FileName, NewName)

  '    If lRet = False Then
  '      App.LogEvent("DevX Sample failed to rename file " & FileName)
  '    End If
  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  CheckFile()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Checks if a file exists on the server that the
  '  ')  class is currently connected to
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  FileName - The name of the file to check
  '  '$                  if it exists.
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     Logical
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Public Function CheckFile(ByVal FileName As String) As Boolean
  '    Dim WFD As WIN32_FIND_DATA
  '    Dim sPath As String
  '    Dim hFindConnect As Long
  '    Dim hFind As Long

  '    On Error GoTo out

  '    CheckFile = False

  '    ' Get the current path
  '    sPath = GetFTPDirectory(hConnect)

  '    ' Set the path to the file we are searching for
  '    sPath = sPath & FileName

  '    ' Get a new connection to the site
  '    hFindConnect = GetInternetConnectionHandle()

  '    If hFindConnect Then

  '      ' Get a handle to the file we are trying to open
  '      hFind = FtpFindFirstFile(hFindConnect, sPath, WFD, _
  '          INTERNET_FLAG_RELOAD Or INTERNET_FLAG_NO_CACHE_WRITE, 0&)

  '      ' If we retrive a handle then the file is found
  '      If hFind Then CheckFile = True
  '    End If

  '    ' Close handles
  '    InternetCloseHandle(hFind)
  '    InternetCloseHandle(hFindConnect)
  '    Exit Function
  'out:
  '    ErrLog("clsFTP.CheckFile method failed. Error - " & Err.Description, 1)
  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  EnumDirectory()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Returns a list of files and folders from a directory
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  Directory - The name of the Directory to
  '  '$                  to enumerate
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     Array
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Public Function EnumDirectory(ByVal Directory As String) As Object

  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  ChangeDirectory()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Changes the current directory on the FTP Site
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  Directory - The directory to change to
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     Boolean
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Public Function ChangeDirectory(ByVal Directory As String) As Boolean

  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  GetInternetConnectionHandle()
  '  '-------------------------------------------------------
  '  ') Description:
  '  ')  Becuase the MSDN library states that you should
  '  ')  open a new connection to the server for the
  '  ')  FTPFindFirstFile methods, we create a method
  '  ')  that returns a new connection
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  NONE
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     Logical
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Private Function GetInternetConnectionHandle() As Long
  '    Dim lConn As Long

  '    On Error GoTo out

  '    If hInternet Then
  '      lConn = InternetConnect(hInternet, sSite, INTERNET_DEFAULT_FTP_PORT, _
  '          sUser, sPassword, INTERNET_SERVICE_FTP, _
  '          INTERNET_FLAG_EXISTING_CONNECT Or INTERNET_FLAG_PASSIVE, &H0)
  '    End If

  '    ' Return the handle
  '    GetInternetConnectionHandle = lConn
  '    Exit Function
  'out:
  '    ErrLog("clsFTP.GetInternetConnectionHandle method failed. Error - " & Err.Description, 1)
  '  End Function

  '  '---------------- Location Section ---------------------
  '  '} Class:   clsFTP
  '  '} Method:  GetFTPDirectory()
  '  '-------------------------------------------------------
  '  ') Description:
  '  '------------------- Usage Section ---------------------
  '  '$ Scope:       PUBLIC
  '  '$ Parameters:  hConnect = Handle to the connection
  '  '$ Usage:       N/A
  '  '$ Example:     N/A
  '  '$ Returns:     String
  '  '$ Notes:
  '  '-------------------------------------------------------
  '  Private Function GetFTPDirectory(ByVal hConnect As Long) As String
  '    Dim nCurrDir As Long
  '    Dim sCurrDir As New StringBuilder(256)

  '    On Error GoTo out

  '    'pad the requisite buffers
  '    nCurrDir = sCurrDir.Length

  '    'WinInetAPI call returns 1 if successful
  '    If FtpGetCurrentDirectory(hConnect, sCurrDir, nCurrDir) = 1 Then
  '      'return a properly qualified path
  '      'sCurrDir = StripNull(sCurrDir)

  '      If Right$(sCurrDir.ToString, 1) <> "/" Then
  '        GetFTPDirectory = sCurrDir.ToString & sSlash
  '      Else
  '        GetFTPDirectory = sCurrDir.ToString
  '      End If
  '    End If
  '    Exit Function
  'out:
  '    ErrLog("clsFTP.GetFTPDirectory method failed. Error - " & Err.Description, 1)
  '  End Function



End Class
