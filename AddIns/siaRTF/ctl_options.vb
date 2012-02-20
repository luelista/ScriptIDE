'<PropertyPage("Protokolle", "TwAjax", "TeamWiki Ajax Protokoll", "")> _
<PropertyPage("Editoren", "RTF", "Editor für RTF-Dateien (Rich Text Format)", "")> _
Public Class ctl_options
  Implements IPropertyPage

  Private Sub ctl_options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub


  Public Sub readProperties() Implements IPropertyPage.readProperties
    CheckBox1.Checked = IDE.Glob.para("siaRTF__merkeZeile", "TRUE") = "TRUE"
  End Sub

  Public Sub saveProperties() Implements IPropertyPage.saveProperties
    IDE.Glob.para("siaRTF__merkeZeile") = If(CheckBox1.Checked, "TRUE", "FALSE")
  End Sub
End Class
