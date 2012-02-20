Public Class ctl_local_options
  Implements IPropertyPage


  Private Sub ctl_options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub


  Public Sub readProperties() Implements IPropertyPage.readProperties
    ParaService.Glob.readTuttiFrutti(Me)
  End Sub

  Public Sub saveProperties() Implements IPropertyPage.saveProperties
    ParaService.Glob.saveTuttiFrutti(Me)
    tbFileExplorer.refreshProperties()
  End Sub
End Class
