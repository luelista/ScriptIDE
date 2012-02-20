Public Class frmTB_compileErrors

  Public Overrides Function GetPersistString() As String
    Return "Addin|##|siaCodeCompiler|##|SHCompilerErrors"
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    IdeHelper.BeforeShowAddinWindow(Me.GetPersistString(), Me)
    MyBase.Show()
  End Sub

  Private Sub frmTB_compileErrors_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Private Sub igCompileErrors_CellDoubleClick(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellDoubleClickEventArgs) Handles igCompileErrors.CellDoubleClick

  End Sub

  Private Sub igCompileErrors_CellMouseUp(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellMouseUpEventArgs) Handles igCompileErrors.CellMouseUp

  End Sub

  Private Sub igCompileErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles igCompileErrors.Click

  End Sub

  Private Sub igCompileErrors_CurRowChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles igCompileErrors.CurRowChanged
    If igCompileErrors.CurRow Is Nothing Then Exit Sub
    Dim ir = igCompileErrors.CurRow

    lblErrType.Text = ir.Cells("typ").Value
    TextBox1.Text = ir.Cells("desc").Value
    lblLine.Text = ir.Cells("line").Value
    'lblCol.Text = ir.Cells("col").Value
    
  End Sub

  Sub clearList()
    lblErrCount.Text = "0"
    lblWarnCount.Text = "0"
    igCompileErrors.Rows.Clear()
    lblErrType.Text = ""
    TextBox1.Text = ""
    lblLine.Text = ""
  End Sub

  Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click, PictureBox1.Click
    Me.Hide()
  End Sub
End Class