Public Class LoginForm1


  Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

    DialogResult = Windows.Forms.DialogResult.OK
  End Sub

  Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
    DialogResult = Windows.Forms.DialogResult.Cancel
  End Sub

  Private Sub LoginForm1_HelpButtonClicked(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.HelpButtonClicked
    e.Cancel = True
    ' If helpfilePath = "" Then MsgBox("Hilfedatei nicht gefunden!", MsgBoxStyle.Critical, "Fehler") : Exit Sub
    ' Help.ShowHelp(Me, helpfilePath, "mwupd3-upload/login.htm")
  End Sub

  Private Sub LoginForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    If IDE.Glob.para("twLoginData") = "" Then
      btnLogoff.Enabled = False

    Else
      Cancel.Text = "Beenden"
    End If
  End Sub

  Private Sub btnLogoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogoff.Click
    twLoginuser = "" : twLoginPass = "" : twLoginFullname = "" : twSessID = ""
    IDE.Glob.para("twLoginData") = ""
    'AppTerminate("btnLogoff")
    tbScriptSync.Hide()
  End Sub
End Class
