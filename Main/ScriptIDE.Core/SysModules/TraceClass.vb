Public Class TT
  Shared m_isIde As Byte = 0
  Private Const S = "|##|"

  Private Sub New()
  End Sub

  Public Shared Event TraceWrite(ByVal para1 As String, ByVal para2 As String, ByVal type As String, ByVal codeLink As String)
  Public Shared Event PrintLineChanged(ByVal lineNumber As Integer, ByVal para1 As String, ByVal para2 As String, ByVal codeLink As String)


  'Sub trace(ByVal ParamArray para() As Object)
  '  Dim codeLink = getCodelinkFromStack()
  '  Dim out = ""
  '  For Each p In para : out += p + vbTab : Next
  '  out += StrDup(8, vbTab)
  '  out += codeLink '+ "_|°|_" + typ
  '  oIntWin.SendCommand("tracemonitor", "Trace", out)
  'End Sub

  Shared Function isIDE() As Boolean
    If m_isIde = 2 Then Return True
    If m_isIde = 1 Then Return False
    If m_isIde = 0 Then
      Dim procName = Process.GetCurrentProcess().ProcessName
      m_isIde = If(procName.EndsWith(".vshost"), 2, 1)
    End If
  End Function

  Public Shared Sub DumpException(ByVal para1 As String, ByVal ex As Exception, Optional ByVal typ As String = "err")
    Dim out As New System.Text.StringBuilder
    out.AppendLine(ex.ToString)
    If ex.InnerException IsNot Nothing Then
      out.AppendLine("InnerException:")
      out.AppendLine(ex.InnerException.ToString)

    End If
    If TypeOf ex Is TypeLoadException Then
      out.AppendLine("TypeLoaded:" + CType(ex, TypeLoadException).TypeName)
    End If
    Write(para1, out.ToString, typ, getCodelinkFromStack)
  End Sub

  Public Shared Sub Write(ByVal para1 As String, Optional ByVal para2 As String = "", Optional ByVal typ As String = "trace", Optional ByVal pcodeLink As String = "")
    Dim codeLink As String
    If pcodeLink <> "" Then
      codeLink = pcodeLink
    Else
      codeLink = getCodelinkFromStack()
    End If

    Dim out As String
    out = typ + S + Replace(para1, "|##|", "| # # |") + S + Replace(para2, "|##|", "| # # |") + S + codeLink
    Interproc.SendCommand("tracemonitor", "Trace", out)

    RaiseEvent TraceWrite(para1, para2, typ, codeLink)
  End Sub

  Shared Sub printLine(ByVal index As Integer, ByVal title As String, ByVal data As String, Optional ByVal pcodeLink As String = "")
    Dim codeLink As String
    If pcodeLink <> "" Then
      codeLink = pcodeLink
    Else
      codeLink = getCodelinkFromStack()
    End If
    '  index += 1

    Dim out = index.ToString("00") + S + title + S + codeLink + S + data
    Interproc.SendCommand("tracemonitor", "PrintLine", out)

    RaiseEvent PrintLineChanged(index, title, data, codeLink)
  End Sub

  Shared Function getCodelinkFromStack() As String
    Dim st = New System.Diagnostics.StackTrace(True)
    Dim mp_MethodName As String
    Dim sf As System.Diagnostics.StackFrame = st.GetFrame(2)
    mp_MethodName = sf.GetMethod().Name()
    Dim typ = sf.GetMethod.DeclaringType
    'Dim info = "TRACE " & sf.GetFileName() & " " & sf.GetFileLineNumber() & " " & sf.GetFileColumnNumber() & " " & typ.Assembly.GetName.FullName
    'Debug.Print(info)

    Dim appName = Reflection.Assembly.GetExecutingAssembly().GetName.Name
    Dim codeLink = "_|°|_cLink_|°|_" + typ.FullName & "_|°|_" & sf.GetFileName() & "?" & sf.GetFileLineNumber() & "_|°|_" & sf.GetMethod().Name()
    Return codeLink
  End Function


End Class
