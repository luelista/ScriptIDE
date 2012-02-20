Imports System.Web
Imports System.Text

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
    Windows.Forms.Application.DoEvents()
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

    errMes = postUrl(strURL, "DATA=" + UrlEncode(content))
  End Sub


  Public Shared Function ReadFile(ByVal appName As String, ByVal fileName As String) As String
    Dim strURL As String
    strURL = TwAjax.twURL + "?version=" + TwAjax.version + "&m=read&app=" + appName + "&file=" + fileName + "&_u=" + twajax.uu

    ReadFile = getUrlContent(strURL)
  End Function




#Region "URL Encoding"
  Public Shared Function UrlEncode(ByVal str As String) As String
    If (str Is Nothing) Then
      Return Nothing
    End If
    Return UrlEncode(str, Encoding.UTF8)
  End Function
  Public Shared Function UrlEncode(ByVal str As String, ByVal e As Encoding) As String
    If (str Is Nothing) Then
      Return Nothing
    End If
    Return Encoding.ASCII.GetString(UrlEncodeToBytes(str, e))
  End Function
  Public Shared Function UrlEncodeToBytes(ByVal str As String, ByVal e As Encoding) As Byte()
    If (str Is Nothing) Then
      Return Nothing
    End If
    Dim bytes As Byte() = e.GetBytes(str)
    Return UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, False)
  End Function
  Private Shared Function UrlEncodeBytesToBytesInternal(ByVal bytes As Byte(), ByVal offset As Integer, ByVal count As Integer, ByVal alwaysCreateReturnValue As Boolean) As Byte()
    Dim num As Integer = 0
    Dim num2 As Integer = 0
    Dim i As Integer
    For i = 0 To count - 1
      Dim ch As Char = ChrW(bytes((offset + i)))
      If (ch = " "c) Then
        num += 1
      ElseIf Not IsSafe(ch) Then
        num2 += 1
      End If
    Next i
    If ((Not alwaysCreateReturnValue AndAlso (num = 0)) AndAlso (num2 = 0)) Then
      Return bytes
    End If
    Dim buffer As Byte() = New Byte((count + (num2 * 2)) - 1) {}
    Dim num4 As Integer = 0
    Dim j As Integer
    For j = 0 To count - 1
      Dim num6 As Byte = bytes((offset + j))
      Dim ch2 As Char = ChrW(num6)
      If IsSafe(ch2) Then
        buffer(num4) = num6 : num4 += 1
      ElseIf (ch2 = " "c) Then
        buffer(num4) = &H2B : num4 += 1
      Else
        buffer(num4) = &H25 : num4 += 1
        buffer(num4) = IntToHex(((num6 >> 4) And 15)) : num4 += 1
        buffer(num4) = IntToHex((num6 And 15)) : num4 += 1
      End If
    Next j
    Return buffer
  End Function
  Friend Shared Function IntToHex(ByVal n As Integer) As Byte
    If (n <= 9) Then
      Return (n + &H30)
    End If
    Return ((n - 10) + &H61)
  End Function
  Friend Shared Function IsSafe(ByVal ch As Char) As Boolean
    If ((((ch >= "a"c) AndAlso (ch <= "z"c)) OrElse ((ch >= "A"c) AndAlso (ch <= "Z"c))) OrElse ((ch >= "0"c) AndAlso (ch <= "9"c))) Then
      Return True
    End If
    Select Case ch
      Case "'"c, "("c, ")"c, "*"c, "-"c, "."c, "_"c, "!"c
        Return True
    End Select
    Return False
  End Function
#End Region





  Shared uu = my.user.name
End Class



