Public Class frmTB_console

  Public Overrides Function GetPersistString() As String
    Return tbPrefix + "Console"
  End Function

  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    cls_IDEHelper.GetSingleton().BeforeShowAddinWindow(Me.GetPersistString(), Me)
    MyBase.Show()
  End Sub

  Private Sub frmTB_console_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    btnZoomOnOff_Click(Nothing, Nothing)
  End Sub

  Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
    If ConsoleRun.CurrentInstance IsNot Nothing Then
      ConsoleRun.CurrentInstance.killConsoleProc()
    End If
  End Sub

  Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
    If ConsoleRun.CurrentInstance IsNot Nothing Then
      ConsoleRun.CurrentInstance.writeToConsoleProc(TextBox1.Text + vbNewLine)
    End If
  End Sub

  Private Sub btnConsoleCls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
    If ConsoleRun.CurrentInstance IsNot Nothing Then
      ConsoleRun.CurrentInstance.rtf.Text = ""
    End If
  End Sub

  Private Sub btnZoomOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomOnOff.Click
    If pnlRtfContainer.Top = 86 Then
      pnlRtfContainer.Top = 32
      Label2.Hide()
    Else
      pnlRtfContainer.Top = 86
      Label2.Show()
    End If
    pnlRtfContainer.Height = ClientRectangle.Height - pnlRtfContainer.Top - 4
  End Sub

  Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    If btnStop.BackColor = Color.Firebrick Then
      btnStop.BackColor = Color.Coral
    Else
      btnStop.BackColor = Color.Firebrick
    End If
  End Sub

  Sub updateButtons()
    If ConsoleRun.CurrentInstance Is Nothing Then
      btnClear.Enabled = False
      btnClose.Enabled = False
      btnStop.Enabled = False
    Else
      btnClear.Enabled = True
      btnClose.Enabled = True
      btnStop.Enabled = ConsoleRun.CurrentInstance.consoleProcRunning
    End If
  End Sub

  Sub RunCommand(ByVal cmd As String, ByVal wd As String)
    cmbCommand.Text = cmd
    txtWorkDir.Text = wd
    OnRunCommand()
  End Sub

  Sub OnRunCommand()
    If ConsoleRun.CurrentInstance IsNot Nothing AndAlso Not ConsoleRun.CurrentInstance.consoleProcRunning() Then
      ConsoleRun.CurrentInstance.runConsoleProgram(cmbCommand.Text, txtWorkDir.Text)
      Return
    End If
    Dim con As New ConsoleRun
    con.rtf = New RichTextBox
    con.rtf.Dock = DockStyle.Fill
    pnlRtfContainer.Controls.Add(con.rtf)
    con.runConsoleProgram(cmbCommand.Text, txtWorkDir.Text)
    cmbCommand.Items.Add(con)
    ConsoleRun.CurrentInstance = con
  End Sub

  Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
    OnRunCommand
  End Sub

  Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
    If ConsoleRun.CurrentInstance IsNot Nothing Then
      If ConsoleRun.CurrentInstance.consoleProcRunning Then
        MsgBox("Unable to close running console. Please exit first.")
        Exit Sub
      End If
      pnlRtfContainer.Controls.Remove(ConsoleRun.CurrentInstance.rtf)
      cmbCommand.Items.Remove(ConsoleRun.CurrentInstance)

      ConsoleRun.CurrentInstance = Nothing

    End If
  End Sub

  Private Sub cmbCommand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCommand.SelectedIndexChanged
    If cmbCommand.SelectedItem IsNot Nothing Then
      If ConsoleRun.CurrentInstance IsNot Nothing AndAlso ConsoleRun.CurrentInstance.rtf IsNot Nothing Then _
        ConsoleRun.CurrentInstance.rtf.Hide()
      ConsoleRun.CurrentInstance = cmbCommand.SelectedItem
      ConsoleRun.CurrentInstance.rtf.Show()
    End If
  End Sub
End Class