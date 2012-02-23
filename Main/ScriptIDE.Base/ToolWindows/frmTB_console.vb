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
    Else
      pnlRtfContainer.Top = 86
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

  Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
    If ConsoleRun.CurrentInstance Is Nothing Then
      If ConsoleRun.CurrentInstance.consoleProcRunning() Then
        Dim con As New ConsoleRun
        con.rtf = New RichTextBox
        con.rtf.Dock = DockStyle.Fill
        pnlRtfContainer.Controls.Add(con.rtf)
        con.runConsoleProgram(cmbCommand.Text, txtWorkDir.Text)
        ConsoleRun.CurrentInstance = con
        Return
      End If
    End If
    ConsoleRun.CurrentInstance.runConsoleProgram(cmbCommand.Text, txtWorkDir.Text)
  End Sub

  Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
    If ConsoleRun.CurrentInstance Is Nothing Then
      pnlRtfContainer.Controls.Remove(ConsoleRun.CurrentInstance.rtf)
      cmbCommand.Items.Remove(ConsoleRun.CurrentInstance)

      ConsoleRun.CurrentInstance = Nothing

    End If
  End Sub

  Private Sub cmbCommand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCommand.SelectedIndexChanged
    If cmbCommand.SelectedItem IsNot Nothing Then
      ConsoleRun.CurrentInstance.rtf.Hide()
      ConsoleRun.CurrentInstance = cmbCommand.SelectedItem
      ConsoleRun.CurrentInstance.rtf.Show()
    End If
  End Sub
End Class