Imports InetCtlsObjects
Imports System.Windows.Forms

Module sys_uploadWithPost
  'Dim WithEvents inetPost As Inet
  Public kData() As Byte = {154, 15, 7, 238, 9, 165, 234, 56, 74, 235, 4, 89, 34, 65, 2, 79, 17, 51, 86, 3, 8, 14, 9, 57, 23, 4, 52, 3, 5, 5, 4, 145, 166, 83, 47, 146}

  Public uploadWithPostUrl As String = "http://mwupd3.teamwiki.net/request.php?c=upload_file"
  Public twLoginuser, twLoginPass, twLoginFullname, twSessID As String

  'Sub destroyInetCtrl()
  '  inetPost.Cancel()
  '  System.Runtime.InteropServices.Marshal.ReleaseComObject(inetPost)
  '  ' inetPost = Nothing
  'End Sub

  Function checkIfErrorResult(ByVal LINES() As String) As Boolean
    If LINES.Length < 4 Then MsgBox("Es ist ein Fehler aufgetreten. Der Server hat keine Daten zurückgeliefert.") : Return False
    If LINES(1) <> "" Then MsgBox("Es ist ein Fehler aufgetreten:" + vbNewLine + LINES(1)) : Return False
    Return True
  End Function

  Function onChangeLogin() As Boolean
    Dim frm As New LoginForm1
    frm.UsernameTextBox.Text = twLoginuser
    frm.UsernameTextBox.Focus()
    frm.UsernameTextBox.SelectAll()

    If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
      Return doLogin(frm.UsernameTextBox.Text, frm.PasswordTextBox.Text)
    Else
      Return False
    End If
  End Function

  Function doLogin(ByVal userName As String, ByVal passWord As String) As Boolean
    Dim postData As String = "u=" + userName + "&p=" + passWord
    Dim RESULT = TwAjax.postUrl("http://teamwiki.net/php/vb/app_login2.php?", postData)
    Dim lines() = Split(RESULT, vbNewLine)
    ReDim Preserve lines(4)

    If lines(0) = "Login OK" Then
      twLoginuser = userName : twLoginPass = passWord : twLoginFullname = lines(3)
      IDE.Glob.para("twLoginData") = RC4StringEncrypt(userName + ":" + passWord, kData)
      twSessID = lines(2)
      tbScriptSync.Text = "ScriptSyncMini - " + twLoginuser
      'tbScriptSync.ToolStripStatusLabel1.Text = twSessID
      'CEXWB1.Navigate("http://teamwiki.net/php/vb/app_login2.php", "sess_id=" + sessID)
      Return True
      'showLoginMenuItems()
    Else
      MsgBox("Ungültige Login-Daten!", MsgBoxStyle.Exclamation)
      Return False
    End If
  End Function


  'Sub init_inet(ByVal url As String)
  '  ' inetPost.Protocol = ProtocolConstants.icHTTP
  '  ' inetPost.URL = url

  '  ' inetPost.RemoteHost = "mwupd3.teamwiki.net"
  '  'uploadWithPostUrl = url
  'End Sub

  ' create sub for making upload post
  Sub upload_file(ByVal appID As String, ByVal sourceFilespec As String, ByVal FileName As String, ByVal targetFilespec As String, ByVal fileFlags As String, ByRef errMes As String)
    Dim try_again_count = 0

    Dim myInet As Inet = New Inet()
try_again:
    try_again_count += 1
    Application.DoEvents()

    'myInet.Cancel()
    Debug.Print(myInet.StillExecuting) 'errMes = "ERROR, inetStillExecuting" ': Exit Sub

    Dim strPOST As String = "", Header As String, strBoundary As String, uagent As String
    Dim Idboundary As String   ' id boundary for the site, it may different

    Idboundary = "657483920" & Now.Ticks    ' you can generate this number
    strBoundary = Strings.StrDup(27, "-") & Idboundary     ' mutipart post need "-" sign 

    Header = "User-Agent: Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.7.12) MWupd/3.0" & _
             vbCrLf & "Content-Type: multipart/form-data; " & "boundary=" & strBoundary & _
             vbCrLf & "Cookie : twnetSID=" & twSessID & "; "
    'ACHTUNG: Cookie mit Lücke wird benutzt um INET-Control reinzulegen, sonst
    'löscht der die raus

    'strPOST = Header & vbCrLf & vbCrLf
    strPOST &= "--" & strBoundary & vbCrLf
    strPOST &= "Content-Disposition: form-data; name=""appid""" & vbCrLf
    strPOST &= vbCrLf & appID
    strPOST &= vbCrLf & "--" & strBoundary & vbCrLf
    strPOST &= "Content-Disposition: form-data; name=""fileName""" & vbCrLf
    strPOST &= vbCrLf & FileName
    strPOST &= vbCrLf & "--" & strBoundary & vbCrLf
    strPOST &= "Content-Disposition: form-data; name=""localSource""" & vbCrLf
    strPOST &= vbCrLf & sourceFilespec
    strPOST &= vbCrLf & "--" & strBoundary & vbCrLf
    strPOST &= "Content-Disposition: form-data; name=""localTarget""" & vbCrLf
    strPOST &= vbCrLf & targetFilespec
    strPOST &= vbCrLf & "--" & strBoundary & vbCrLf
    strPOST &= "Content-Disposition: form-data; name=""flags""" & vbCrLf
    strPOST &= vbCrLf & fileFlags
    strPOST &= vbCrLf & "--" & strBoundary & vbCrLf
    strPOST &= "Content-Disposition: form-data; name=""fileContent""; filename=""" & FileName & """" & vbCrLf
    strPOST &= "Content-Type: application/octet-stream" & vbCrLf
    strPOST &= vbCrLf & IO.File.ReadAllText(sourceFilespec, System.Text.Encoding.Default)
    strPOST &= vbCrLf & "--" & strBoundary & "--"

    Try

      myInet.Execute(uploadWithPostUrl, "POST", strPOST, Header)

    Catch ex As Exception
      Debug.Print("Fehler beim Start des Uploads ... (Bug im MSINET-Control)" & try_again_count & ". Versuch")
      'If MsgBox("Fehler beim Start des Uploads ... (Bug im MSINET-Control)" + vbNewLine + vbNewLine + ex.Message, MsgBoxStyle.RetryCancel) = MsgBoxResult.Retry Then
      myInet = New Inet()
      Application.DoEvents()
      Threading.Thread.Sleep(11)
      Application.DoEvents()
      If try_again_count > 5 Then
        MsgBox("Fehler beim Start des Uploads ... (Bug im MSINET-Control)" & vbNewLine & try_again_count & ". Versuch")
        Stop

      End If
      GoTo try_again
      'End If
    End Try
    ' Wait until it finished
    Do Until myInet.StillExecuting = False
      Application.DoEvents()
      Threading.Thread.Sleep(10)

    Loop

    Debug.Print("Header: " & myInet.GetHeader)
    Try
      errMes = myInet.GetHeader("X-MWupd3-Result")
    Catch ex As Exception
      errMes = "ERROR, HEADER-DOESNT-EXIST"
    End Try

    myInet.Cancel()
    myInet = Nothing
  End Sub

  'Private Sub inetPost_StateChanged(ByVal State As Short) Handles inetPost.StateChanged
  '  Debug.Print("inet State: " & State)
  'End Sub
End Module
