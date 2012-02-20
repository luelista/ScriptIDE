Imports System.Windows.Forms

Public Class Connect
  Implements IAddinConnect
  Implements ISkinnable

  Public Sub Connect(ByVal application As Object, ByVal connectMode As Core.ConnectMode, ByVal addInInst As Core.AddinInstance, ByRef custom As Object) Implements Core.IAddinConnect.Connect
    IDE = Main.cls_IDEHelper.Instance

    'Dim tb = IDE.CreateToolbar("siaScriptSyncMini.miniTB")
    'tb.addButton("ssm_showWnd", "Sync", , , , , , "http://mw.teamwiki.net/docs/img/win-icons/CSCDLL_143-16.png")

    IDE.RegisterAddinWindow(tbScriptSync_ID)


    If IDE.Glob.para("twLoginData") <> "" Then
      Dim userData = Split(RC4StringDecrypt(IDE.Glob.para("twLoginData"), kData), ":")
      If userData.Length = 2 Then doLogin(userData(0), userData(1))
    End If
    If twLoginuser = "" Then
      'If Not 
      onChangeLogin() 'Then Application.Exit()
    End If
  End Sub


  Public Sub Disconnect(ByVal removeMode As Core.DisconnectMode, ByRef custom As Object) Implements Core.IAddinConnect.Disconnect
    IDE.UnregisterAddinWindow(tbScriptSync_ID)
  End Sub


  Function UploadFile(ByVal CategoryID As Integer, ByVal FileSpec As String, ByRef errMes As String, Optional ByVal localTargetFilespec As String = "") As Boolean
    If CategoryID = 0 Then Exit Function
    doLogin(twLoginuser, twLoginPass)
    Dim rErrMes As String
    upload_file(globAktAppID, FileSpec, IO.Path.GetFileName(FileSpec), "C:\yPara\scriptIDE\scriptClass\", "", rErrMes)

    If rErrMes.StartsWith("OK") Then
      errMes = "" : Return True
    Else
      errMes = rErrMes : Return False
    End If
  End Function

  Function DownloadFile(ByVal fileInfo As String, ByVal localTargetFilespec As String, ByRef errMes As String) As Boolean
    Try
      Dim file() As String = Split(fileInfo, vbTab)
      If globAktappInfo Is Nothing Then
        globAktappInfo = New MWupd3File(getAppInfo("0"), False)
      End If

      Dim url = globAktappInfo.MorePara("RootURL") + file(FilePara.FileID) + ".dat"

      My.Computer.Network.DownloadFile(url, localTargetFilespec, "", "", True, 10000, True)

      Return True
    Catch ex As Exception
      errMes = ex.Message : Return False
    End Try
  End Function

  Function GetFilelist(ByVal CategoryID As Integer) As String
    If CategoryID = 0 Then Return "" 'Achtung!
    Dim appInfo As New MWupd3File(getAppInfo(CategoryID), False)

    Return Join(appInfo.Files.ToArray, vbNewLine)
  End Function

  Function GetCategories() As String
    Dim Lines = getAppList("10") '10=syncFiles
    Dim out As New System.Text.StringBuilder
    For i = 3 To Lines.Length - 1
      If Lines(i).Trim = "" Then Continue For
      Dim Parts() = Split(Lines(i), vbTab)

      If Parts(4).Contains("hidden2") Then Continue For '4=Tags
      out.AppendLine(Parts(2) + vbTab + Parts(0))
    Next
    Return out.ToString
  End Function

  Public Function GetAddinWindow(ByVal PersistString As String) As Form Implements IAddinConnect.GetAddinWindow
    Select Case PersistString.ToLower
      Case tbScriptSync_ID.ToLower
        Return tbScriptSync
    End Select
  End Function

  Public Sub OnNavigate(ByVal kind As Core.NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object) Implements Core.IAddinConnect.OnNavigate
    If command = "Window.ScriptSync" Then
      tbScriptSync.Show()
    End If
  End Sub

  Public Sub OnAddinUpdate(ByVal addinChanged As String, ByRef custom As Object) Implements Core.IAddinConnect.OnAddinUpdate

  End Sub

  Public Sub OnBeforeShutdown(ByRef cancel As Boolean, ByRef custom As Object) Implements Core.IAddinConnect.OnBeforeShutdown

  End Sub

  Public Sub OnStartupComplete(ByRef custom As Object) Implements Core.IAddinConnect.OnStartupComplete

  End Sub

  Public Function GetSkinObject(ByVal id As String) As Object Implements ScriptIDE.Core.ISkinnable.GetSkinObject
    Return ScriptSyncSkin.GetSingleton()
  End Function

End Class
