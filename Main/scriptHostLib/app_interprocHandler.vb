Imports System.Text

Module app_interprocHandler

  Public WithEvents ship As sys_interproc

  Sub interproc_init()
    ship = New sys_interproc("scripthostlib06")
    ship.Commands.Add("CMD", "RuntimeObject_trace", "zLN|##|zNN|##|type|##|para1|##|para2", "(for internal use only)")
    ship.Commands.Add("CMD", "RuntimeObject_printLine", "zLN|##|zNN|##|index|##|title|##|data", "(for internal use only)")
    ship.Commands.Add("CMD", "RuntimeObject_onScriptError", "errFile|##|errLine|##|text", "(for internal use only)")
    ship.Commands.Add("CMD", "RuntimeObject_setOutMonitor", "txt", "(for internal use only)")
    ship.Commands.Add("CMD", "RuntimeObject_printLineReset", "(none)", "(for internal use only)")
    ship.Commands.Add("CMD", "RuntimeObject_traceClear", "(none)", "(for internal use only)")
    ship.Commands.Add("CMD  ", "_Debug_BreakModeChanged", "breakState", "")
    ship.Commands.Add("CMD  ", "_Debug_HighlightLineRequested", "className|##|functionName|##|lineNumber|##|reason", "")
    ship.Commands.Add("QUERY", "_Debug_QueryBreakpoints", "className", "returns breakPoints comma-separated")

    ship.Commands.Add("QUERY", "NavigateScript", "className|##|commandName|##|Flags|##|Target", "returns 'Return Value'")

  End Sub


  Private Sub ship_DataRequest(ByVal source As String, ByVal cmdString As String, ByVal para As String, ByRef returnValue As String) Handles ship.DataRequest
    'trace("INTERPROC - DataRequest: " + vbTab + cmdString + vbTab + source + vbTab + para)

    Dim sourceClass As String = ""
    If source.StartsWith("siDebug_") Then sourceClass = source.StartsWith(8, source.LastIndexOf("_") - 8)

    Select Case cmdString.ToUpper
      Case "_Debug_QueryBreakpoints"
        Dim bb() As Boolean
        ScriptHost.Instance.OnQueryBreakpoints(sourceClass, source, bb)

        Dim out As StringBuilder
        For i = 0 To bb.Length
          If bb(i) Then out.Append(i & ",")
        Next
        returnValue = out.ToString

      Case "NavigateScript"
        Dim p() = Split(para, "|##|", 3) : ReDim Preserve p(3)
        returnValue = ZZ.NavigateScript(p(0), p(1), p(2), p(3))

    End Select


    'trace("INTERPROC - returnValue: " + vbTab + returnValue)
  End Sub

  Private Sub ship_Message(ByVal source As String, ByVal cmdString As String, ByVal para As String) Handles ship.Message
    'TT.Write("INTERPROC - Message: " + vbTab + cmdString + vbTab + source + vbTab + para)

    Dim sourceClass As String = ""
    If source.StartsWith("siDebug_") Then sourceClass = source.StartsWith(8, source.LastIndexOf("_") - 8)
    Select Case cmdString
      Case "RuntimeObject_trace"
        Dim p() = Split(para, "|##|", 5) : ReDim Preserve p(5)
        TT.Write(p(3), p(4), p(2), "_|°|_cLink_|°|_" + "scriptClass" & "_|°|_" & sourceClass & "?" & p(0) & "_|°|_" & p(1))

      Case "RuntimeObject_printLine"
        Dim p() = Split(para, "|##|", 5) : ReDim Preserve p(5)
        TT.printLine(p(2), p(3), p(4), "_|°|_cLink_|°|_" + "scriptClass" & "_|°|_" & sourceClass & "?" & p(0) & "_|°|_" & p(1))

      Case "RuntimeObject_onScriptError"
        Dim p() = Split(para, "|##|", 3) : ReDim Preserve p(3)
        onScriptError(p(0), p(1), p(2))

      Case "RuntimeObject_setOutMonitor"
        If MAIN IsNot Nothing Then MAIN.setOUT(sourceClass, para)

      Case "RuntimeObject_printLineReset"
        If tbPrintline IsNot Nothing Then tbPrintline.resetPrintLines()
        TT.Write("--- PrintLineReset ---", sourceClass, "ini")

      Case "RuntimeObject_traceClear"
        If MAIN IsNot Nothing Then MAIN.IGrid2.Rows.Clear()
        TT.Write("--- TraceClear ---", sourceClass, "ini")

      Case "_Debug_BreakModeChanged"
        'ScriptHost.Instance.OnBreakModeChanged(Data(0))
        'If data(0) = "" Then debugState = "RUN"
        'If data(0) <> "" Then debugState = "BREAK"
        'DirectCast(IDE.getMainFormRef(), Windows.Forms.Form).Invoke(New Threading.ThreadStart(AddressOf debugStateChange))
        ScriptHost.Instance.globBreakMode(sourceClass) = Trim(para)

      Case "_Debug_HighlightLineRequested"
        Dim p() = Split(para, "|##|", 5) : ReDim Preserve p(4)
        'ScriptHost.Instance.OnHighlightLineRequested(Data(0), Data(1), Data(2), Data(3))

        'If data(0) <> debuggedScript Then Exit Sub
        ScriptHost.Instance.OnHighlightLineRequested(p(0), p(1), p(2), p(3))

    End Select



  End Sub

End Module
