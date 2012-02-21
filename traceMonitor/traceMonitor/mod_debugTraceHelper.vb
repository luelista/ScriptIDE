Imports System.IO

Module mod_debugTraceHelper
  Private appName As String = "traceMonitor"
  Public lastLineCount As Integer

  Private tracePath As String = settingsFolder + "traceLog.txt"

  Private lastTraceFileDate As Date

  Private grwl As Growl.Connector.GrowlConnector
  Private grwlAvailable As Boolean = False
  Dim sampleNotificationPrefix As String = "TRACE_"
  Dim notifications(2) As Growl.Connector.NotificationType
  Dim grwlApp As Growl.Connector.Application

  Function tmask(ByVal out As String) As String
    Return out.Replace("\", "\\").Replace(vbCr, "\r").Replace(vbLf, "\n").Replace("|", "\s")
  End Function
  Function tunmask(ByVal out As String) As String
    Return out.Replace("\r", vbCr).Replace("\n", vbLf).Replace("\s", "|").Replace("\\", "\")
  End Function

  Sub initGrowl()
    ' If Not Growl.Connector.GrowlConnector.IsGrowlRunningLocally() Then Return
    grwl = New Growl.Connector.GrowlConnector()

    Dim g As Graphics

    Dim bmp1 As Bitmap = MAIN.Icon.ToBitmap
    g = Graphics.FromImage(bmp1)
    g.DrawImage(MAIN.iml_TraceTypes.Images("err"), 4, 4)
    g.Dispose()
    notifications(0) = New Growl.Connector.NotificationType(sampleNotificationPrefix + "err", "An error was traced", bmp1, True)

    Dim bmp2 As Bitmap = MAIN.Icon.ToBitmap
    g = Graphics.FromImage(bmp2)
    g.DrawImage(MAIN.iml_TraceTypes.Images("ini"), 4, 4)
    g.Dispose()
    notifications(1) = New Growl.Connector.NotificationType(sampleNotificationPrefix + "ini", "Trace: Initialize", bmp2, True)

    Dim bmp3 As Bitmap = MAIN.Icon.ToBitmap
    g = Graphics.FromImage(bmp3)
    g.DrawImage(MAIN.iml_TraceTypes.Images("shutdown"), 4, 4)
    g.Dispose()
    notifications(2) = New Growl.Connector.NotificationType(sampleNotificationPrefix + "shutdown", "Trace: Shutdown", bmp3, True)

    grwlApp = New Growl.Connector.Application("TraceMonitor")
    grwl.Register(grwlApp, notifications)

    grwlAvailable = True
  End Sub
  Sub stopGrowl()
    grwlAvailable = False
  End Sub

  Sub igridOnNextPage()
    Dim visibleRows As Integer = MAIN.IGrid1.Height / MAIN.IGrid1.DefaultRow.Height

    MAIN.IGrid1.Rows.Count += visibleRows * 0.8
    If MAIN.checkAutoscroll.Checked Then
      MAIN.IGrid1.Rows(MAIN.IGrid1.Rows.Count - 1).EnsureVisible()
      MAIN.IGrid1.Refresh()
    End If
  End Sub

  Sub setPrintLine(ByVal strId As String, ByVal col1 As String, ByVal codeLink As String, ByVal col2 As String)
    Try
      Dim id As Integer = Val(strId)
      With MAIN.IGrid2.Rows(id)
        .Cells(1).Value = col1
        .Cells(2).Value = col2
        .Tag = codeLink
        Dim codelink2() = Split(codeLink, "_|°|_")
        .Cells(3).Value = codelink2(2) 'app
        .Cells(4).Value = Now.ToShortDateString
        .Cells(5).Value = Now.ToString("HH:mm:ss.ffff")
      End With
    Catch ex As Exception
      trace("Unable to print line #" + strId, ex.Message, "warn")
    End Try
  End Sub

  Private ddel_addTraceItem As New del_addTraceItem(AddressOf addTraceItem)
  Private Delegate Sub del_addTraceItem(ByVal typ As String, ByVal col1 As String, ByVal col2 As String, ByVal tag As String)
  Sub addTraceItem(ByVal typ As String, ByVal col1 As String, ByVal col2 As String, ByVal tag As String)
    Try
      With MAIN
        If .InvokeRequired Then
          .Invoke(ddel_addTraceItem, typ, col1, col2, tag)
          Return
        End If

        If .IGrid1.Rows.Count = lastLineCount Then igridOnNextPage()

        Dim netSendLine As String = "Trace:" & tmask(typ) & "|" & tmask(col1) & "|" & tmask(col2) & "|" & tmask(tag)
        For Each subs In traceSubscribers
          subs.send(netSendLine)
        Next

        '.IGrid1.BeginUpdate()
        Dim ir = .IGrid1.Rows(lastLineCount)
        typ = LCase(typ)

        ir.Cells(0).Value = typ
        Select Case typ
          Case "dump" : ir.Cells(0).ImageIndex = 0 : ir.ForeColor = Color.LightGray
          Case "event" : ir.Cells(0).ImageIndex = 1
          Case "trace" : ir.Cells(0).ImageIndex = 2
          Case "info" : ir.Cells(0).ImageIndex = 3
          Case "warn" : ir.Cells(0).ImageIndex = 4
          Case "err" : ir.Cells(0).ImageIndex = 5
          Case "ok" : ir.Cells(0).ImageIndex = 6
          Case "sys" : ir.Cells(0).ImageIndex = 7
          Case "ini" : ir.Cells(0).ImageIndex = 8
          Case "shutdown" : ir.Cells(0).ImageIndex = 9
          Case Else : ir.Cells(0).ImageIndex = -1
        End Select
        ir.Cells(1).Value = col1
        ir.Cells(2).Value = col2
        Dim codelink2() = Split(tag, "_|°|_")
        Try
          ir.Cells(3).Value = codelink2(2) 'app
          If String.IsNullOrEmpty(.actTraceFilter) = False AndAlso .actTraceFilter <> LCase(codelink2(2)) AndAlso .traceFilter(typ) Then
            ir.Visible = False
          Else
            'If .checkAutoscroll.Checked Then ir.EnsureVisible()
            'If .chk_Autoselect.Checked Then ir.Selected = True
          End If
        Catch : End Try
        ir.Cells(4).Value = Now.ToShortDateString
        ir.Cells(5).Value = Now.ToString("HH:mm:ss.ffff")

        If typ.ToLower = "err" Then ir.BackColor = Color.Maroon
        ir.Tag = tag

        Dim vis As Boolean
        If .traceFilter.TryGetValue(typ, vis) AndAlso vis = False Then
          ir.Visible = False
        End If

        lastLineCount += 1

        If grwlAvailable And (typ = "ini" Or typ = "shutdown" Or typ = "err") Then
          Dim n As New Growl.Connector.Notification(grwlApp.Name, sampleNotificationPrefix + typ, DateTime.Now.Ticks.ToString, col1, col2)
          grwl.Notify(n)
        End If
        '.IGrid1.EndUpdate()
      End With
    Catch ex As Exception

    End Try
  End Sub

  Sub trace222(ByVal para1, ByVal para2)
    On Error Resume Next
    Dim st = New System.Diagnostics.StackTrace(True)
    Dim mp_MethodName As String
    Dim sf As System.Diagnostics.StackFrame = st.GetFrame(1)    ' just going one level
    ' above in the stacktrace
    mp_MethodName = sf.GetMethod().Name()
    mp_MethodName = appName & "_|°|_" & sf.GetMethod().DeclaringType().Name() & ".vb" & "_|°|_" & sf.GetMethod().Name()

    para1 = Replace(para1, vbCrLf, "|LF|")
    para2 = Replace(para2, vbCrLf, "|LF|")
    'Stop
    Dim out = ""
    out += para1 + " -->"
    out += para2
    out += "<--" + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab
    out += "_|°|_cLink_|°|_" + mp_MethodName
    Debug.WriteLine("send...:" + out)

    IO.File.AppendAllText(tracePath, out + vbNewLine)

    'Dim sw As System.IO.StreamWriter
    'sw = System.IO.File.AppendText(tracePath)
    'sw.Write(para1 + " -->")
    'sw.Write(para2)
    'sw.WriteLine("<--" + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + "_|°|_cLink_|°|_" + mp_MethodName)
    'sw.Flush()
    'sw.Close()
  End Sub


  Sub traceFileAppend()
    On Error Resume Next
    Dim sw As System.IO.StreamWriter
    sw = System.IO.File.AppendText(tracePath)
    sw.Write("This ")
    sw.Write("is Extra xxxxxxxxxxxxx ")
    sw.Write("Text ")
    sw.Flush()
    sw.Close()


  End Sub

  Function binRead(ByVal fileSpec As String, Optional ByRef errMes As String = "", Optional ByVal startPos As Integer = 0) As String
    On Error Resume Next
    Dim fs = File.Open(fileSpec, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite)
    Dim sr As New StreamReader(fs)

    If startPos > 0 Then
      sr.BaseStream.Seek(startPos, SeekOrigin.Begin)
    End If

    binRead = sr.ReadToEnd()

  End Function
  Function getFileLen(ByVal fileSpec As String) As Integer
    getFileLen = My.Computer.FileSystem.GetFileInfo(fileSpec).Length
  End Function

  Sub traceFileRead()
    'On Error Resume Next
    'Debug.Write("traceFileRead" + vbCrLf)

    ' Stop
    Dim LINES() As String = File.ReadAllLines(tracePath)
    For Each li In LINES
      Dim parts() = Split(li, "|##|", 4)
      addTraceItem(parts(0), parts(1), parts(2), parts(3))

    Next

    'Dim fileDate As Date = My.Computer.FileSystem.GetFileInfo(tracePath).LastWriteTime
    'If fileDate <= lastTraceFileDate Then
    '  Exit Sub
    'End If
    'Debug.Print("### -> traceFile changed...")

    'lastTraceFileDate = fileDate

    'Dim traceContent As String

    'traceContent = binRead(tracePath)

    'Try

    '  'Dim intFileLen As Integer = getFileLen(tracePath)
    '  'If intFileLen > lastTraceFilePos Then
    '  '  lastTraceFilePos = intFileLen
    '  'End If

    '  Dim lines() As String
    '  Dim count As Long
    '  Dim newIndex As Integer
    '  lines = Split(traceContent, vbCrLf)
    '  count = lines.Length
    '  If count > 1 Then
    '    If count > lastLineCount Then
    '      For i = lastLineCount To count - 1
    '        If i > 0 Then

    '          newIndex = AA_frmTraceMonitor.ListBox1.Items.Add(lines(i - 1))
    '        End If
    '      Next
    '      lastLineCount = count
    '    End If
    '  End If
    '  AA_frmTraceMonitor.ListBox1.SelectedIndex = newIndex
    'Catch
    '  Debug.Print("ERR: Fehler beim Lesen - path/file NotFound ?")
    'End Try
  End Sub

  Sub traceFileClear()
    On Error Resume Next
    ' Create a file to write to.
    Dim sw As System.IO.StreamWriter = System.IO.File.CreateText(tracePath)
    sw.WriteLine("info|##|closed - " + Now.ToString + "|##||##|")
    sw.Flush()
    sw.Close()

    MAIN.IGrid1.Rows.Clear()
    lastLineCount = 0

    traceFileRead()

    MAIN.tsslCountItems.Text = "Anzahl: " & MAIN.IGrid1.Rows.Count
  End Sub

End Module
