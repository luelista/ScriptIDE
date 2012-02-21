Module sys_mwTrace

  'Sub trace(ByVal ParamArray para() As Object)
  '  Dim codeLink = getCodelinkFromStack()
  '  Dim out = ""
  '  For Each p In para : out += p + vbTab : Next
  '  out += StrDup(8, vbTab)
  '  out += codeLink '+ "_|°|_" + typ
  '  oIntWin.SendCommand("tracemonitor", "Trace", out)
  'End Sub

  Sub trace(ByVal para1 As String, ByVal para2 As String, Optional ByVal typ As String = "info")
    Dim codeLink = getCodelinkFromStack()

    Dim out As String
    out = typ + "|##|" + para1 + "|##|" + para2 + "|##|" + codeLink

    addTraceItem(typ, para1, para2, codeLink)
    '' oIntWin.SendCommand("tracemonitor", "Trace", out)
  End Sub

  Sub printLine(ByVal index As Integer, ByVal title As String, ByVal data As String)
    Dim codeLink = getCodelinkFromStack()
    Const S = "|##|"
    Dim out = index.ToString("00") + S + title + S + codeLink + S + data

    oIntWin.SendCommand("tracemonitor", "PrintLine", out)
  End Sub

  Function getCodelinkFromStack() As String
    Dim st = New System.Diagnostics.StackTrace(True)
    Dim mp_MethodName As String
    Dim sf As System.Diagnostics.StackFrame = st.GetFrame(2)
    mp_MethodName = sf.GetMethod().Name()
    Dim appName = Reflection.Assembly.GetExecutingAssembly().GetName.Name
    Dim codeLink = "_|°|_cLink_|°|_" + appName & "_|°|_" & sf.GetMethod().DeclaringType().Name() & ".vb" & "_|°|_" & sf.GetMethod().Name()
    Return codeLink
  End Function


End Module
