'<PropertyPage("Protokolle", "TwAjax", "TeamWiki Ajax Protokoll", "")> _
<PropertyPage("Editoren", "Hex-Editor", "Hex-Editor", "")> _
Public Class ctl_options
  Implements IPropertyPage

  Private Sub ctl_options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Public Sub readProperties() Implements IPropertyPage.readProperties
    RadioButton1.Checked = IDE.Glob.para("siaHexEdit__HexCase", "Upper") = "Upper"
    RadioButton2.Checked = IDE.Glob.para("siaHexEdit__HexCase", "Upper") = "Lower"
    checkAscii.Checked = IDE.Glob.para("siaHexEdit__showAscii", "TRUE") = "TRUE"
    checkLinenumbers.Checked = IDE.Glob.para("siaHexEdit__showLN", "TRUE") = "TRUE"
    txtLinenumbersColor.Text = IDE.Glob.para("siaHexEdit__LnColor", "#7A7A7A")
    Integer.TryParse(IDE.Glob.para("siaHexEdit__ByteCount", "16"), nudBytesPerLine.Value)
  End Sub

  Public Sub saveProperties() Implements IPropertyPage.saveProperties
    IDE.Glob.para("siaHexEdit__HexCase") = If(RadioButton1.Checked, "Upper", "Lower")
    IDE.Glob.para("siaHexEdit__showAscii") = If(checkAscii.Checked, "TRUE", "FALSE")
    IDE.Glob.para("siaHexEdit__showLN") = If(checkLinenumbers.Checked, "TRUE", "FALSE")
    IDE.Glob.para("siaHexEdit__LnColor") = txtLinenumbersColor.Text
    IDE.Glob.para("siaHexEdit__ByteCount") = nudBytesPerLine.Value.ToString

  End Sub

End Class
