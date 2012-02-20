Imports TenTec.Windows.iGridLib

Public Class prop_General
  Implements IPropertyPage



  Private Sub prop_Hotkeys_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub




  Public Sub readProperties() Implements Core.IPropertyPage.readProperties
    chkTitlebar.Checked = ParaService.Glob.para("titleBarVisible", "TRUE") = "TRUE"
    chkSubtitlebar.Checked = ParaService.Glob.para("subTitleBarVisible", "TRUE") = "TRUE"
    chkMenu.Checked = ParaService.Glob.para("menuVisible", "TRUE") = "TRUE"
    chkBookmarks.Checked = ParaService.Glob.para("bookmarksVisible", "TRUE") = "TRUE"

  End Sub

  Public Sub saveProperties() Implements Core.IPropertyPage.saveProperties
    ParaService.Glob.para("titleBarVisible") = If(chkTitlebar.Checked, "TRUE", "FALSE")
    ParaService.Glob.para("subTitleBarVisible") = If(chkSubtitlebar.Checked, "TRUE", "FALSE")
    ParaService.Glob.para("menuVisible") = If(chkMenu.Checked, "TRUE", "FALSE")
    ParaService.Glob.para("bookmarksVisible") = If(chkBookmarks.Checked, "TRUE", "FALSE")
    Workbench.Instance.showHideToolbar()
    makeVistaForm()
  End Sub


End Class
