Module app_interprocHandler

  'Public WithEvents oIntWin As sys_interproc


  'Sub interproc_init()
  '  oIntWin = New sys_interproc("siaCodeCompiler")
  'End Sub

  'Private Sub oIntWin_InitCommandDefinition(ByVal e As sys_interproc.commandDefEventArgs) Handles oIntWin.InitCommandDefinition
  '  e.Add("CMD  ", "_Debug_ScriptError", "className|##|functionName|##|lineNumber|##|errorType|##|errorNumber|##|errorDescription", "")
  '  e.Add("CMD  ", "_Debug_BreakModeChanged", "breakState", "")
  '  e.Add("CMD  ", "_Debug_HighlightLineRequested", "className|##|functionName|##|lineNumber|##|reason", "")
  '  e.Add("QUERY", "_Debug_QueryBreakpoints", "className", "returns breakPoints comma-separated")

  'End Sub

  'Private Sub oIntWin_DataRequest(ByVal source As String, ByVal cmdString As String, ByVal para As String, ByRef returnValue As String) Handles oIntWin.DataRequest
  '  'trace("INTERPROC - DataRequest: " + vbTab + cmdString + vbTab + source + vbTab + para)

  '  Select Case cmdString.ToUpper
  '    Case "_DEBUG_QUERYBREAKPOINTS"

  '  End Select


  '  'trace("INTERPROC - returnValue: " + vbTab + returnValue)
  'End Sub

  'Private Sub oIntWin_Message(ByVal source As String, ByVal cmdString As String, ByVal para As String) Handles oIntWin.Message
  '  'trace("INTERPROC - Message: " + vbTab + cmdString + vbTab + source + vbTab + para)

  '  Dim data() = Split(para, "|##|")

  '  Select Case cmdString.ToUpper
  '    Case "_DEBUG_SCRIPTERROR"
  '      'ScriptHost.Instance.OnScriptError(Data(0), Data(1), Data(2), Data(3), Data(4), Data(5))


  '    Case "_DEBUG_BREAKMODECHANGED"
  '      'ScriptHost.Instance.OnBreakModeChanged(Data(0))
  '      If data(0) = "" Then debugState = "RUN"
  '      If data(0) <> "" Then debugState = "BREAK"
  '      DirectCast(IDE.getMainFormRef(), Windows.Forms.Form).Invoke(New Threading.ThreadStart(AddressOf debugStateChange))


  '    Case "_DEBUG_HIGHLIGHTLINEREQUESTED"
  '      'ScriptHost.Instance.OnHighlightLineRequested(Data(0), Data(1), Data(2), Data(3))

  '      'If data(0) <> debuggedScript Then Exit Sub
  '      highlightExecutingLine(data(2))

  '  End Select




  'End Sub

End Module
