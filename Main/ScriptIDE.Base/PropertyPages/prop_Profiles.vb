Imports TenTec.Windows.iGridLib

Public Class prop_Profiles
  Implements IPropertyPage


  Private Sub prop_Hotkeys_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub



  Public Sub readProperties() Implements Core.IPropertyPage.readProperties
    txtProfileFolder.Text = ParaService.ProfileFolder
    labProfileName.Text = ParaService.ProfileName
    txtProfileDisplayName.Text = ParaService.ProfileDisplayName

    txtFormatWinTitle.Text = getTitleFormatString("mainWinTitle")
    txtFormatWinCaption.Text = getTitleFormatString("mainWinCaption")
    txtFormatStatusBar.Text = getTitleFormatString("mainWinStatusBar")

    Dim dirs() = IO.Directory.GetDirectories(ParaService.SettingsFolder + "profiles")
    For Each dirSpec As String In dirs
      ListView1.Items.Add(IO.Path.GetFileName(dirSpec), 0)
    Next
  End Sub

  Public Sub saveProperties() Implements Core.IPropertyPage.saveProperties
    ParaService.ProfileDisplayName = txtProfileDisplayName.Text

    ParaService.Glob.para("format_mainWinTitle") = txtFormatWinTitle.Text
    ParaService.Glob.para("format_mainWinCaption") = txtFormatWinCaption.Text
    ParaService.Glob.para("format_mainWinStatusBar") = txtFormatStatusBar.Text
    refreshMainTitle()
  End Sub


  Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

  End Sub
End Class
