<PropertyPage("Tools", "Solutions & Projects", "Solutions & Projects", "")> Public Class ctl_options
  Implements IPropertyPage

  Private Sub ctl_options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  End Sub

  Private Sub btnChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChoose.Click
    FolderBrowserDialog1.SelectedPath = txtDefProjectDir.Text
    If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
      txtDefProjectDir.Text = FolderBrowserDialog1.SelectedPath
    End If
  End Sub

  Public Sub readProperties() Implements IPropertyPage.readProperties
    txtDefProjectDir.Text = IDE.Glob.para("siaSolution__defProjectFolder", IDE.GetSettingsFolder() + "projects\")
  End Sub

  Public Sub saveProperties() Implements IPropertyPage.saveProperties
    IDE.Glob.para("siaSolution__defProjectFolder") = txtDefProjectDir.Text
  End Sub
End Class
