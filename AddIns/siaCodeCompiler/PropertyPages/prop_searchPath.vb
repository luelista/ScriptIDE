Public Class prop_searchPath
  Implements IPropertyPage

  'Dim reloadClbOnMouseUp As Boolean = False

  'Private Sub clbScriptClsAutostart_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clbScriptAddins.ItemCheck
  '  Static skipMe As Boolean = False
  '  If skipMe Then Exit Sub

  '  Dim out(clbScriptAddins.CheckedItems.Count) As String
  '  For i = 0 To clbScriptAddins.CheckedItems.Count - 1
  '    out(i) = clbScriptAddins.CheckedItems(i)
  '  Next
  '  If e.NewValue Then
  '    out(clbScriptAddins.CheckedItems.Count) = clbScriptAddins.Items(e.Index)
  '  Else
  '    For i = 0 To out.Length - 1
  '      If out(i) = clbScriptAddins.Items(e.Index) Then out(i) = ""
  '    Next
  '  End If
  '  ParaService.Glob.para("scriptAddins") = Join(out, "|##|")
  '  reloadClbOnMouseUp = True

  'End Sub

  'Sub readAutostartList()
  '  clbScriptAddins.Items.Clear()
  '  Dim autostarts() = Split(ParaService.Glob.para("scriptAddins"), "|##|")

  '  For Each fileName In autostarts
  '    If fileName <> "" Then _
  '    clbScriptAddins.Items.Add(fileName, True)
  '  Next

  '  For Each fileSpec In IO.Directory.GetFiles(ParaService.SettingsFolder + "addIns\")
  '    Dim fileExt = IO.Path.GetExtension(fileSpec).ToUpper
  '    If fileExt = ".NSA" Then
  '      Dim fileName = IO.Path.GetFileNameWithoutExtension(fileSpec)
  '      If clbScriptAddins.Items.Contains(fileName) = False Then
  '        clbScriptAddins.Items.Add(fileName)
  '      End If
  '    End If
  '  Next

  'End Sub

  'Private Sub clbScriptClsAutostart_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles clbScriptAddins.MouseUp
  '  If reloadClbOnMouseUp Then
  '    Dim selFilename = clbScriptAddins.SelectedItem
  '    readAutostartList()
  '    For i = 0 To clbScriptAddins.Items.Count - 1
  '      If clbScriptAddins.Items(i) = selFilename Then clbScriptAddins.SelectedIndex = i : Exit For
  '    Next
  '    reloadClbOnMouseUp = False
  '  End If
  'End Sub

  'Private Sub clbScriptClsAutostart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clbScriptAddins.SelectedIndexChanged
  '  Button3.Enabled = clbScriptAddins.SelectedIndex > 0 AndAlso _
  '                    clbScriptAddins.GetItemChecked(clbScriptAddins.SelectedIndex)
  '  Button4.Enabled = clbScriptAddins.SelectedIndex > -1 AndAlso _
  '                    clbScriptAddins.SelectedIndex < clbScriptAddins.Items.Count - 1 AndAlso _
  '                    clbScriptAddins.GetItemChecked(clbScriptAddins.SelectedIndex) AndAlso _
  '                    clbScriptAddins.GetItemChecked(clbScriptAddins.SelectedIndex + 1)
  'End Sub

  'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  If clbScriptAddins.SelectedIndex < 1 Then Exit Sub
  '  swapItems(clbScriptAddins.SelectedIndex, clbScriptAddins.SelectedIndex - 1)
  '  clbScriptAddins.SelectedIndex -= 1
  'End Sub
  'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  '  If clbScriptAddins.SelectedIndex < 0 Or clbScriptAddins.SelectedIndex > clbScriptAddins.Items.Count - 2 Then Exit Sub
  '  swapItems(clbScriptAddins.SelectedIndex, clbScriptAddins.SelectedIndex + 1)
  '  clbScriptAddins.SelectedIndex += 1
  'End Sub

  'Sub swapItems(ByVal idx1 As Integer, ByVal idx2 As Integer)
  '  Dim t1 As String ', c1 As Boolean
  '  If clbScriptAddins.GetItemChecked(idx1) = False Or clbScriptAddins.GetItemChecked(idx2) = False Then Exit Sub

  '  ' c1 = clbScriptClsAutostart.GetItemChecked(idx1)
  '  t1 = clbScriptAddins.Items(idx1)
  '  ' clbScriptClsAutostart.SetItemChecked(idx1, clbScriptClsAutostart.GetItemChecked(idx2))
  '  clbScriptAddins.Items(idx1) = clbScriptAddins.Items(idx2)
  '  ' clbScriptClsAutostart.SetItemChecked(idx2, c1)
  '  clbScriptAddins.Items(idx2) = t1

  '  'save Settings
  '  Dim out(clbScriptAddins.CheckedItems.Count) As String
  '  For i = 0 To clbScriptAddins.CheckedItems.Count - 1
  '    out(i) = clbScriptAddins.CheckedItems(i)
  '  Next
  '  ParaService.Glob.para("scriptAddins") = Join(out, "|##|")
  'End Sub


  Public Sub readProperties() Implements IPropertyPage.readProperties
    On Error Resume Next
    TextBox1.Text = ParaService.Glob.para("scriptSearchPath", ParaService.SettingsFolder + "scriptClass\" + vbNewLine)

    'readAutostartList()
  End Sub

  Public Sub saveProperties() Implements IPropertyPage.saveProperties
    On Error Resume Next
    ParaService.Glob.para("scriptSearchPath") = TextBox1.Text

  End Sub

  Private Sub prop_scriptAddins_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
    If e.Data.GetDataPresent("FileDrop") Then
      Dim files() As String = e.Data.GetData("FileDrop")
      For Each file In files
        If IO.Directory.Exists(file) = False Then Continue For
        TextBox1.AppendText(file + vbNewLine)
        ' ConnectFromScript(file, ConnectMode.AfterStartup)
      Next
    End If
  End Sub

  Private Sub prop_scriptAddins_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
    If e.Data.GetDataPresent("FileDrop") Then
      e.Effect = DragDropEffects.Link
    End If
  End Sub

  Private Sub prop_autorunClasses_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub
End Class
