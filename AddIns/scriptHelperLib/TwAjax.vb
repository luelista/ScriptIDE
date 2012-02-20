Imports System.Web

Public Class TwAjax
  Public Shared twURL As String = "http://ajax.teamwiki.net/php/vb/twajax.php"
  Public Shared version As String = "090629a"

  Public Shared Function getUrlContent(ByVal url As String, Optional ByVal cookies As String = "") As String
    Dim xmlhttp As Object = CreateObject("Msxml2.XMLHTTP.3.0") 'MSXML2.ServerXMLHTTP")
    xmlhttp.Open("GET", url, True)
    If cookies <> "" Then xmlhttp.setRequestHeader("Cookie", cookies)
    xmlhttp.send("")

    Dim timer = 0
    While xmlhttp.ReadyState <> 4
      idle()
      If timer > 1000 Then xmlhttp = Nothing : Return ""
      timer += 1
    End While

    getUrlContent = xmlhttp.ResponseText
    xmlhttp = Nothing
  End Function


  Public Shared Function postUrl(ByVal url As String, ByVal post As String, Optional ByVal cookies As String = "") As String
    Dim xmlhttp As Object = CreateObject("Msxml2.XMLHTTP.3.0") 'MSXML2.ServerXMLHTTP")
    xmlhttp.Open("POST", url, True)
    xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded")
    If cookies <> "" Then xmlhttp.setRequestHeader("Cookie", cookies)
    xmlhttp.send("" + post)

    Dim timer = 0
    While xmlhttp.ReadyState <> 4
      idle()
      If timer > 1000 Then xmlhttp = Nothing : Return ""
      timer += 1
    End While

    postUrl = xmlhttp.ResponseText
    xmlhttp = Nothing
  End Function

  Public Shared Sub idle(Optional ByVal sleepTime As Integer = 10)
    System.Threading.Thread.Sleep(sleepTime)
    Application.DoEvents()
  End Sub

  Public Shared Function listDir(ByVal actApp As String) As String()
    On Error Resume Next

    Dim strURL As String = TwAjax.twURL + "?version=" + TwAjax.version + "&m=listdir&app=" + actApp + "&_u=" + twajax.uu

    Dim response As String = TwAjax.getUrlContent(strURL)
    Dim lines() As String = Split(response, vbLf)

    Dim lst As New System.Collections.Generic.List(Of String)
    For Each fileSpec As String In lines
      If fileSpec <> "" Then lst.Add(fileSpec)
    Next
    Return lst.ToArray
  End Function


  Public Shared Function RenameFile(ByVal appName As String, ByVal fileName As String, ByVal newFileName As String) As String
    Dim strURL As String
    strURL = TwAjax.twURL + "?version=" + TwAjax.version + "&m=rename&app=" + appName + "&file=" + fileName + "&newfile=" + newFileName + "&_u=" + twajax.uu

    RenameFile = getUrlContent(strURL)
  End Function

  Public Shared Function DeleteFile(ByVal appName As String, ByVal deleteFileName As String) As String
    Dim strURL As String
    strURL = TwAjax.twURL + "?version=" + TwAjax.version + "&m=deletefile&app=" + appName + "&file=" + deleteFileName + "&_u=" + twajax.uu

    DeleteFile = getUrlContent(strURL)
  End Function

  Public Shared Sub SaveFile(ByVal appName As String, ByVal fileName As String, ByVal content As String, Optional ByRef errMes As String = "")
    Dim strURL As String
    strURL = TwAjax.twURL + "?version=" + TwAjax.version + "&m=save&app=" + appName + "&file=" + fileName + "&_u=" + twajax.uu

    errMes = postUrl(strURL, "DATA=" + HttpUtility.UrlEncode(content))
  End Sub


  Public Shared Function ReadFile(ByVal appName As String, ByVal fileName As String) As String
    Dim strURL As String
    strURL = TwAjax.twURL + "?version=" + TwAjax.version + "&m=read&app=" + appName + "&file=" + fileName + "&_u=" + twajax.uu

    ReadFile = getUrlContent(strURL)
  End Function






  Shared uu = my.user.name
End Class



