Imports System.Windows.Forms
Imports System.Drawing

Public Class frm_editGradient

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    selectColor(TextBox1)
  End Sub

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    selectColor(TextBox2)
  End Sub

  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    selectColor(TextBox3)
  End Sub

  Sub selectColor(ByVal txt As TextBox)
    On Error Resume Next
    ColorDialog1.Color = ColorTranslator.FromHtml(txt.Text)
    If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
      txt.Text = ColorTranslator.ToHtml(ColorDialog1.Color)
    End If
  End Sub

  Private Sub frm_editGradient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub
End Class
