Imports System.Threading

Public Class ConsoleRun

  Private Shared p_currentInstance As ConsoleRun
  Public Shared Property CurrentInstance() As ConsoleRun
    Get
      Return p_currentInstance
    End Get
    Set(ByVal value As ConsoleRun)
      p_currentInstance = value
      If value IsNot Nothing Then _
      p_currentInstance.rtf.BringToFront()
      tbConsole.updateButtons()
    End Set
  End Property

  Dim console_cmd, console_para, console_workDir As String
  Delegate Sub dlg_paraLessSub()
  Delegate Sub dlg_addTextToOutWindow(ByVal txt As String, ByVal colorName As String)
  Dim procObject As Process
  Dim procObjectReady As Boolean

  Public WithEvents rtf As RichTextBox

  Private Sub rtf_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rtf.KeyPress
    writeToConsoleProc(e.KeyChar)
  End Sub

  Public Overrides Function ToString() As String
    Return console_cmd & If(String.IsNullOrEmpty(console_para), "", " " & console_para)
  End Function

  Function consoleProcGetProcess() As Process
    While Not procObjectReady
      Thread.Sleep(10)
    End While
    Return procObject
  End Function
  Function consoleProcRunning() As Boolean
    If procObject Is Nothing Then Return False
    Return Not procObject.HasExited
  End Function
  Sub killConsoleProc()
    If consoleProcRunning() = False Then Exit Sub
    procObject.Kill()
  End Sub
  Sub writeToConsoleProc(ByVal txt As String)
    If consoleProcRunning() = False Then Exit Sub
    Debug.WriteLine(txt.ToString)
    Debug.WriteLine("... " & Hex(Asc(txt)))
    If txt = vbCr Then txt = vbCrLf
    procObject.StandardInput.Write(txt)

    procObject.StandardInput.Flush()
  End Sub

  Sub consoleThread()
    Dim psi As New ProcessStartInfo(console_cmd, console_para)
    psi.CreateNoWindow = True
    psi.UseShellExecute = False
    psi.RedirectStandardInput = True : psi.RedirectStandardOutput = True : psi.RedirectStandardError = True
    psi.StandardOutputEncoding = System.Text.Encoding.ASCII
    psi.StandardErrorEncoding = System.Text.Encoding.ASCII
    'If console_workDir = "" Then console_workDir=IO.Path.GetDirectoryName(console_cmd)
    If console_workDir <> "" Then psi.WorkingDirectory = console_workDir
    Try

      procObject = Process.Start(psi)
      procObjectReady = True
      'AddHandler procObject.ErrorDataReceived, AddressOf handleConsoleStdErr
      'AddHandler procObject.OutputDataReceived, AddressOf handleConsoleStdOut

      'procObject.BeginErrorReadLine()
      'procObject.
      procObject.StandardInput.AutoFlush = True
      Dim d As New dlg_addTextToOutWindow(AddressOf addTextToOutWindow)
      Dim threadStdErr As New Thread(AddressOf consoleErrorThread)
      threadStdErr.Start()

      Dim buf(100) As Char, cnt As Integer
      While procObject.HasExited = False
        If procObject.StandardOutput.EndOfStream = False Then
          cnt = procObject.StandardOutput.Read(buf, 0, 100)
          Workbench.Instance.Invoke(d, New String(buf, 0, cnt), "black")
        End If
        Threading.Thread.Sleep(10)
      End While
      Workbench.Instance.Invoke(d, procObject.StandardOutput.ReadToEnd, "black")
      ' procObject.WaitForExit()
      'procObject.Kill()
      threadStdErr.Abort()
      procObject = Nothing
    Catch ex As Exception
      procObjectReady = True
      Dim d As New dlg_addTextToOutWindow(AddressOf addTextToOutWindow)
      Workbench.Instance.Invoke(d, "Fehler beim Starten des Prozesses:" + vbNewLine + ex.ToString + vbNewLine + "--------------------------" + vbNewLine, "red")

    End Try
    Dim d2 As New dlg_paraLessSub(AddressOf finishedConsoleRun)
    Workbench.Instance.Invoke(d2)
  End Sub

  Sub consoleErrorThread()
    Dim d As New dlg_addTextToOutWindow(AddressOf addTextToOutWindow)
    Dim buf(100) As Char, cnt As Integer
    While procObject IsNot Nothing AndAlso procObject.HasExited = False
      If procObject.StandardError.EndOfStream = False Then
        cnt = procObject.StandardError.Read(buf, 0, 100)
        Workbench.Instance.Invoke(d, New String(buf, 0, cnt), "red")
      End If
      Threading.Thread.Sleep(10)
    End While
    Try

      Workbench.Instance.Invoke(d, procObject.StandardError.ReadToEnd, "red")
    Catch : End Try
  End Sub

  Sub handleConsoleStdOut(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
    Dim d As New dlg_addTextToOutWindow(AddressOf addTextToOutWindow)
    Workbench.Instance.Invoke(d, e.Data, "black")
  End Sub
  Sub handleConsoleStdErr(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
    Dim d As New dlg_addTextToOutWindow(AddressOf addTextToOutWindow)
    Workbench.Instance.Invoke(d, e.Data, "red")

  End Sub

  Sub finishedConsoleRun()
    'tbConsole.btnStop.Text = "Run"
    'tbConsole.Timer1.Stop()
    'tbConsole.btnStop.BackColor = Color.FromKnownColor(KnownColor.ButtonFace)
    tbConsole.updateButtons()
    cls_IDEHelper.GetSingleton.OnConsoleEvent(2, Nothing)
  End Sub

  Sub addTextToOutWindow(ByVal txt As String, ByVal colorName As String)
    With rtf
      Select Case colorName
        Case "red" : .SelectionBackColor = Color.Transparent : .SelectionColor = Color.DarkRed
        Case "black" : .SelectionBackColor = Color.Transparent : .SelectionColor = Color.Black
        Case "blue" : .SelectionBackColor = Color.LightBlue : .SelectionColor = Color.Black
      End Select

      .SelectionStart = .TextLength : .ScrollToCaret()
      .AppendText(txt)
      .SelectionStart = .TextLength : .ScrollToCaret()
    End With
  End Sub


  Sub runConsoleProgram(ByVal cmd As String, Optional ByVal workDir As String = "")
    procObjectReady = False
    Dim th As New Threading.Thread(AddressOf consoleThread)
    Dim paraStartPos = -1
    If cmd.StartsWith("""") Then
      paraStartPos = cmd.IndexOf("""", 1) + 1
    Else
      paraStartPos = cmd.IndexOf(" ")
    End If
    If paraStartPos > -1 Then
      If paraStartPos >= cmd.Length Then console_para = "" Else console_para = cmd.Substring(paraStartPos + 1)
      console_cmd = cmd.Substring(0, paraStartPos)
    Else
      console_para = ""
      console_cmd = """" & cmd & """"
    End If
    console_workDir = workDir
    'tbConsole.cmbCommand.Text = cmd
    tbConsole.txtWorkDir.Text = workDir
    'tbConsole.btnStop.Text = "KILL"
    'tbConsole.Timer1.Start()
    tbConsole.updateButtons()
    addTextToOutWindow(vbNewLine + "---------------------" + vbNewLine + "Command: " + cmd + vbNewLine, "blue")
    th.Start()

  End Sub


End Class
