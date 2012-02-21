Imports System.Windows.Forms

Public Class frm_settings

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    glob.saveTuttiFrutti(Me)
    Me.Enabled = False
    Label4.Show()
    Application.DoEvents()

    killServer()
    initFromSettings()

    Label4.Hide()
    Me.Enabled = True

    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub frm_settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    glob.readTuttiFrutti(Me)
  End Sub
End Class
