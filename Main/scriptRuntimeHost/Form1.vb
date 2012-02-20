Public Class Form1

  Private Sub tmrRefreshList_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRefreshList.Tick
    If Not classListChanged Then Exit Sub

    ListBox1.Items.Clear()
    For Each kvp In classes
      ListBox1.Items.Add(kvp.Key)
    Next

    classListChanged = False
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    For Each kvp In classes
      kvp.Value.Dispose()
    Next
    glob.saveFormPos(Me)
    glob.saveParaFile()
    Process.GetCurrentProcess.Kill()
  End Sub

  Private Sub Form1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
    Dim files() As String = e.Data.GetData("FileDrop")
    loadScript(files(0))
  End Sub

  Private Sub Form1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
    If e.Data.GetDataPresent("FileDrop") Then
      Dim files() As String = e.Data.GetData("FileDrop")
      If UCase(files(0)).EndsWith(".DLL") Then
        e.Effect = DragDropEffects.Link
      End If
    End If
  End Sub

  Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    glob.readFormPos(Me, False)
  End Sub
End Class
