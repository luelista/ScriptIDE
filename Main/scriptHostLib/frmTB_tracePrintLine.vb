Public Class frmTB_tracePrintLine

  Dim plCont(10) As Label

  Public Overrides Function GetPersistString() As String
    Return "Addin|##|siaCodeCompiler|##|SHPrintline"
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    IdeHelper.BeforeShowAddinWindow(Me.GetPersistString(), Me)
    MyBase.Show()
  End Sub

  Sub createPrintlineArea()
    IGrid1.Rows.Count = 50
    FlowLayoutPanel1.Controls.Clear()
    For i = 1 To 9
      plCont(i) = makeLabel(DockStyle.None)
      plCont(i).BackColor = Color.LightGray
      plCont(i).ForeColor = Color.Black
      plCont(i).Text = "line" & i : plCont(i).Width = 63
      plCont(i).Margin = New Padding(0, 0, 2, 0)
      FlowLayoutPanel1.Controls.Add(plCont(i))
    Next
  End Sub
  Function makeLabel(ByVal dockst As DockStyle) As Label
    makeLabel = New Label
    makeLabel.Dock = dockst
    makeLabel.AutoSize = False
    makeLabel.TextAlign = ContentAlignment.MiddleLeft
    makeLabel.Margin = New Padding(0, 0, 0, 0)
  End Function

  Sub setPrintLine(ByVal i As Integer, ByVal content As String, Optional ByVal label As String = "")
    If i < 11 Or i > 20 Then
      'iGrid
      If i > 20 Then i -= 10
      If Not String.IsNullOrEmpty(label) Then IGrid1.Cells(i - 1, 1).Value = label
      IGrid1.Cells(i - 1, 2).Value = content
    Else
      plCont(i - 11).Text = content
    End If
  End Sub


  Private Sub frmTB_tracePrintLine_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    createPrintlineArea()
    plCont(0) = labPrintLine11
    resetPrintlines()
  End Sub

  Private Sub IGrid1_CellMouseDown(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellMouseDownEventArgs) Handles IGrid1.CellMouseDown
    If e.Button = Windows.Forms.MouseButtons.Right Then
      IGrid1.SetCurRow(e.RowIndex)
    End If
  End Sub

  Private Sub IGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IGrid1.Click

  End Sub

  Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
    KopierenToolStripMenuItem.Enabled = IGrid1.CurRow IsNot Nothing
  End Sub

  Private Sub KopierenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KopierenToolStripMenuItem.Click
    Clipboard.Clear()
    Clipboard.SetText(ZZ.JoinIGridLine(IGrid1.CurRow, vbTab))
  End Sub

  Sub resetPrintLines()
    For i = 0 To 9
      plCont(i).Text = "line " & (i + 11)
    Next
    Dim delta As Integer = 1
    For i = 0 To 49
      If i = 10 Then delta = 11
      IGrid1.Cells(i, 0).Value = CStr(i + delta)
      IGrid1.Cells(i, 1).Value = ""
      IGrid1.Cells(i, 2).Value = ""
    Next
  End Sub

  Private Sub resetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles resetToolStripMenuItem.Click
    resetPrintLines()
  End Sub
End Class