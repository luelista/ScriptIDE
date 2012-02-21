Public Class frm_hugeZoom

  Dim m_formBorder As MVPS.clsFormBorder


  Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
    If e.KeyCode = Keys.A And e.Control Then
      TextBox1.SelectAll()
    End If
    If e.KeyCode = Keys.Escape Then
      Me.Close()
    End If
    If e.KeyCode = Keys.W And e.Control Then
      TextBox1.WordWrap = Not TextBox1.WordWrap
    End If
    If e.KeyCode = Keys.T And e.Control Then
      Me.TopMost = Not Me.TopMost
    End If
    If e.KeyCode = Keys.F11 Then
      If Me.WindowState = FormWindowState.Maximized Then
        Me.WindowState = FormWindowState.Normal
      Else
        Me.WindowState = FormWindowState.Maximized
      End If
    End If
  End Sub

  Private Sub frm_hugeZoom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    m_formBorder = New MVPS.clsFormBorder()
    m_formBorder.client = Me
    m_formBorder.Titlebar = False

  End Sub

  Private Sub frm_hugeZoom_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
  Handles MyBase.MouseDown, Label2.MouseDown, Label1.MouseDown
    m_formBorder.MoveMe()
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    Me.Close()
  End Sub
End Class