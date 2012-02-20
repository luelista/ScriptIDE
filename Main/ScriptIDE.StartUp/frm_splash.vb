Public Class frm_splash

  Private Sub frm_splash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Label4.Text = "Aktives Profil: " + Core.ParaService.ProfileName
    Label3.Text = "scriptIDE " + My.Application.Info.Version.ToString(3)
  End Sub

End Class