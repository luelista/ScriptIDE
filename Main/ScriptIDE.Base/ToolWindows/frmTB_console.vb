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

  Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunCommand.Click
    If consoleProcRunning() Then
      killConsoleProc()
    Else
      runConsoleProgram(txtRunCommand.Text, txtWorkDir.Text)
    End If
  End Sub

  Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
    writeToConsoleProc(TextBox1.Text + vbNewLine)
  End Sub

  Private Sub rtfConsoleOut_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rtfConsoleOut.KeyPress
    writeToConsoleProc(e.KeyChar)
  End Sub
  Private Sub btnConsoleCls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsoleCls.Click
    rtfConsoleOut.Text = ""
  End Sub

  Private Sub btnZoomOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomOnOff.Click
    If rtfConsoleOut.Top = 86 Then
      rtfConsoleOut.Top = 32
    Else
      rtfConsoleOut.Top = 86
    End If
    rtfConsoleOut.Height = ClientRectangle.Height - rtfConsoleOut.Top - 4
  End Sub

  Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    If btnRunCommand.BackColor = Color.Firebrick Then
      btnRunCommand.BackColor = Color.Coral
    Else
      btnRunCommand.BackColor = Color.Firebrick
    End If
  End Sub
End Class