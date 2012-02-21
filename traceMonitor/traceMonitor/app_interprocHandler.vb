Module app_interprocHandler

  Public WithEvents oIntWin As sys_interproc

  Dim traceListenerList As New List(Of String)

  Sub initInterprocWindow()
    oIntWin = New sys_interproc("tracemonitor")
  End Sub

  Private Sub oIntWin_InitCommandDefinition(ByVal e As sys_interproc.commandDefEventArgs) Handles oIntWin.InitCommandDefinition
    e.Add("CMD  ", "GetLine", "LineNumber", "gibt eingetragenen Text in der angegebenen Zeile zurück")
    e.Add("CMD  ", "PrintLine", "LineNumber|##|Data1|##|Data2", "gibt einen beliebigen (einzeiligen) Text in der angegebenen Zeile aus")
    e.Add("CMD  ", "Trace", "Type|##|Data2|##|Data2|##|CodeLink", "hängt eine Zeile an die Trace-Ausgabe an")
    'e.Add("CMD  ", "RegisterTraceListener", "-", "trägt den Absender der Nachricht als Trace-Listener ein")
    'e.Add("CMD  ", "UnregisterTraceListener", "-", "entfernt den Absender aus der Liste der Trace-Listener")
  End Sub

  Private Sub oIntWin_DataRequest(ByVal source As String, ByVal cmdString As String, ByVal para As String, ByRef returnValue As String) Handles oIntWin.DataRequest
    Select Case cmdString
      Case "GETLINE"
        Dim id As Integer = Val(para)
        With frm_TraceMonitor.IGrid2.Rows(id)
          returnValue = .Cells(2).Value
        End With
    End Select
  End Sub

  'Sub broadcastTraceItem(ByVal cmd As String, ByVal para As String)
  '  For Each winID In traceListenerList
  '    oIntWin.SendCommand(winID, "TraceBroadcastMessage", cmd + "|##|" + para)
  '  Next
  'End Sub

  Private Sub oIntWin_Message(ByVal source As String, ByVal cmdString As String, ByVal para As String) Handles oIntWin.Message
    ' On Error Resume Next

    Const tracePath As String = "c:\yPara\globDebugTrace\traceLog.txt"

    'broadcastTraceItem(cmdString, para)

    Select Case cmdString
      'Case "REGISTERTRACELISTENER"
      '  traceListenerList.Add(source)
      'Case "UNREGISTERTRACELISTENER"
      '  If traceListenerList.Contains(source) Then traceListenerList.Remove(source)

      Case "PRINTLINE"
        Dim parts() = Split(para, "|##|", 4)
        'printLine(Val(parts(0)), parts(1), parts(2))

        'Dim id As String = Val(parts(0)).ToString("00")
        'With frm_TraceMonitor.tblLayout_printLine.Controls("txtPrintLine_" + id)
        '  .Text = parts(3)
        '  .Tag = parts(2)
        'End With
        'frm_TraceMonitor.tblLayout_printLine.Controls("labPrintLine_" + id).Text = parts(1)
        setPrintLine(parts(0), parts(1), parts(2), parts(3))

      Case "TRACE"
        'Dim out = ""
        'out += source + " -->"
        'out += para
        'out += "<--" + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab
        'out += "_|°|_cLink_|°|_" + source + "@interproc_|°|_via sys_interproc"
        'Debug.WriteLine("send...:" + out)
        para = para.Replace(vbCrLf, "|LF|")
        Dim parts() = Split(para, "|##|", 4)
        addTraceItem(parts(0), parts(1), parts(2), parts(3))

        'Try
        'Err.Clear()
        'Dim fs = IO.File.Open(tracePath, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.ReadWrite)
        'Dim sw As New IO.StreamWriter(fs)
        'sw.WriteLine(para)
        'sw.Close() : fs.Close()
        'sw.Dispose() : fs.Dispose()
        'Debug.Print("TraceFile append - Err? " + Err.Description)
        'Catch ex As Exception
        'trace("Fehler beim speichern", "", "err")
        'End Try

        'Dim newIndex As Integer
        'newIndex = AA_frmTraceMonitor.ListBox1.Items.Add(para)
        'lastLineCount = newIndex
        'AA_frmTraceMonitor.ListBox1.SelectedIndex = newIndex
        ' IO.File.AppendAllText(tracePath, out + vbNewLine)
    End Select
  End Sub


End Module
