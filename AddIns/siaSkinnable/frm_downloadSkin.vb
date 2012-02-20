Imports System.Windows.Forms

Public Class frm_downloadSkin

  Sub downloadSelItems()
    For Each lvi In ListView1.SelectedItems
      Dim skinData = TwAjax.ReadFile("scriptide", "skins/" + lvi.text)
      IO.File.WriteAllText(IDE.GetSettingsFolder + "skins\" + lvi.text, skinData)

    Next
  End Sub

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    downloadSelItems
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub frm_downloadSkin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim list() = TwAjax.listDir("scriptide")
    For Each fileName In list
      If fileName.StartsWith("skins/") Then
        ListView1.Items.Add(fileName.Substring(6), 0)
      End If
    Next
  End Sub

  Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
    downloadSelItems
  End Sub

End Class
