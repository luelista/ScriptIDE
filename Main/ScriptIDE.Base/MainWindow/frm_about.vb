Public NotInheritable Class frm_about

  Private Sub frm_about_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' Set the title of the form.
    Dim ApplicationTitle As String
    ApplicationTitle = My.Application.Info.Title
    Me.Text = "Info zu ScriptIDE " + My.Application.Info.Version.ToString(2)

    Me.LabelProductName.Text = My.Application.Info.ProductName
    Me.LabelVersion.Text = "Version " + My.Application.Info.Version.ToString
    Me.LabelCopyright.Text = My.Application.Info.Copyright

    Dim sb As New System.Text.StringBuilder
    sb.Append("<b>Geladene Add-ins:</b><ul>")
    For Each inst In AddinInstance.Addins
      If inst.Loaded Then
        sb.Append("<li>" + inst.DisplayName + " by " + inst.Properties("author") + "<line><a>" + inst.Properties("url") + "</a><br>")
      End If
    Next
    sb.Append("</ul>")

    sb.Append("<br><br>")
    sb.Append("<b>Verwendete Module:</b><br>")
    sb.Append("WeifenLuo's DockPanel-Library<br>")
    sb.Append("Teile der SharpDevelop-Addinverwaltung (AddinTree)<br>")
    sb.Append("Office 2007 Toolstrip Renderer (by Phil Wright, <a>www.componentfactory.com</a>)<br>")
    sb.Append("TenTec IGrid<br>")
    sb.Append("Managed WinApi Application-wide keyboard hook<br>")
    sb.Append("VS2008ImageLibrary<br>")
    sb.Append("<br>")

    RichTextPlus1.HTMLCode = sb.ToString
  End Sub

  Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
    Me.Close()
  End Sub

End Class
