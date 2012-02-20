Public Class frm_exedebugging
   
  Public Overrides Function GetPersistString() As String
    Return tbProcInfo_ID
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    IDE.BeforeShowAddinWindow(tbProcInfo_ID, Me)
    MyBase.Show()
  End Sub

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    On Error Resume Next
    Dim p As Process = PropertyGrid1.SelectedObject
    p.Kill()
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    IDE.NavigateFile(debuggedScript)
  End Sub

  Private Sub PropertyGrid1_SelectedObjectsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PropertyGrid1.SelectedObjectsChanged
    Dim p As Process = PropertyGrid1.SelectedObject
    TextBox1.Text = p.MainModule.FileName
  End Sub

  Private Sub frm_exedebugging_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub
End Class