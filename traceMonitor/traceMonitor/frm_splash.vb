Public Class frm_splash

  Private Sub frm_splash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Label2.Text = "Version " + My.Application.Info.Version.ToString(2)
  End Sub
End Class