Public Class prop_autorunClasses
  Implements IPropertyPage

  Dim reloadClbOnMouseUp As Boolean = False


  Private Sub clbScriptClsAutostart_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clbScriptClsAutostart.ItemCheck
    Static skipMe As Boolean = False
    If skipMe Then Exit Sub

    Dim out(clbScriptClsAutostart.CheckedItems.Count) As String
    For i = 0 To clbScriptClsAutostart.CheckedItems.Count - 1
      out(i) = clbScriptClsAutostart.CheckedItems(i)
    Next
    If e.NewValue Then
      out(clbScriptClsAutostart.CheckedItems.Count) = clbScriptClsAutostart.Items(e.Index)
    Else
      For i = 0 To out.Length - 1
        If out(i) = clbScriptClsAutostart.Items(e.Index) Then out(i) = ""
      Next
    End If
    ParaService.Glob.para("scriptClsAutostart") = Join(out, "|##|")
    reloadClbOnMouseUp = True

  End Sub

  Sub readAutostartList()
    clbScriptClsAutostart.Items.Clear()
    Dim autostarts() = Split(ParaService.Glob.para("scriptClsAutostart"), "|##|")

    For Each fileName In autostarts
      If fileName <> "" Then _
      clbScriptClsAutostart.Items.Add(fileName, True)
    Next

    For Each fileSpec In IO.Directory.GetFiles(ParaService.SettingsFolder + "scriptClass")
      Dim fileExt = IO.Path.GetExtension(fileSpec).ToUpper
      If fileExt = ".VBS" Or fileExt = ".VB" Or fileExt = ".CS" Then
        Dim fileName = IO.Path.GetFileNameWithoutExtension(fileSpec)
        If clbScriptClsAutostart.Items.Contains(fileName) = False Then
          clbScriptClsAutostart.Items.Add(fileName)
        End If
      End If
    Next

  End Sub

  Private Sub clbScriptClsAutostart_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles clbScriptClsAutostart.MouseUp
    If reloadClbOnMouseUp Then
      Dim selFilename = clbScriptClsAutostart.SelectedItem
      readAutostartList()
      For i = 0 To clbScriptClsAutostart.Items.Count - 1
        If clbScriptClsAutostart.Items(i) = selFilename Then clbScriptClsAutostart.SelectedIndex = i : Exit For
      Next
      reloadClbOnMouseUp = False
    End If
  End Sub

  Private Sub clbScriptClsAutostart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clbScriptClsAutostart.SelectedIndexChanged
    Button3.Enabled = clbScriptClsAutostart.SelectedIndex > 0 AndAlso _
                      clbScriptClsAutostart.GetItemChecked(clbScriptClsAutostart.SelectedIndex)
    Button4.Enabled = clbScriptClsAutostart.SelectedIndex > -1 AndAlso _
                      clbScriptClsAutostart.SelectedIndex < clbScriptClsAutostart.Items.Count - 1 AndAlso _
                      clbScriptClsAutostart.GetItemChecked(clbScriptClsAutostart.SelectedIndex) AndAlso _
                      clbScriptClsAutostart.GetItemChecked(clbScriptClsAutostart.SelectedIndex + 1)
  End Sub

  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    If clbScriptClsAutostart.SelectedIndex < 1 Then Exit Sub
    swapItems(clbScriptClsAutostart.SelectedIndex, clbScriptClsAutostart.SelectedIndex - 1)
    clbScriptClsAutostart.SelectedIndex -= 1
  End Sub
  Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    If clbScriptClsAutostart.SelectedIndex < 0 Or clbScriptClsAutostart.SelectedIndex > clbScriptClsAutostart.Items.Count - 2 Then Exit Sub
    swapItems(clbScriptClsAutostart.SelectedIndex, clbScriptClsAutostart.SelectedIndex + 1)
    clbScriptClsAutostart.SelectedIndex += 1
  End Sub

  Sub swapItems(ByVal idx1 As Integer, ByVal idx2 As Integer)
    Dim t1 As String ', c1 As Boolean
    If clbScriptClsAutostart.GetItemChecked(idx1) = False Or clbScriptClsAutostart.GetItemChecked(idx2) = False Then Exit Sub

    ' c1 = clbScriptClsAutostart.GetItemChecked(idx1)
    t1 = clbScriptClsAutostart.Items(idx1)
    ' clbScriptClsAutostart.SetItemChecked(idx1, clbScriptClsAutostart.GetItemChecked(idx2))
    clbScriptClsAutostart.Items(idx1) = clbScriptClsAutostart.Items(idx2)
    ' clbScriptClsAutostart.SetItemChecked(idx2, c1)
    clbScriptClsAutostart.Items(idx2) = t1
  End Sub


  Public Sub readProperties() Implements IPropertyPage.readProperties

    readAutostartList()
  End Sub

  Public Sub saveProperties() Implements IPropertyPage.saveProperties
    On Error Resume Next

  End Sub

End Class
