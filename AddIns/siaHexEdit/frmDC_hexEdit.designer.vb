<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDC_hexEdit
  Inherits WeifenLuo.WinFormsUI.Docking.DockContent

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.HexBox1 = New Be.Windows.Forms.HexBox
    Me.SuspendLayout()
    '
    'HexBox1
    '
    Me.HexBox1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.HexBox1.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HexBox1.LineInfoForeColor = System.Drawing.Color.Gray
    Me.HexBox1.LineInfoVisible = True
    Me.HexBox1.Location = New System.Drawing.Point(0, 0)
    Me.HexBox1.Name = "HexBox1"
    Me.HexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.HexBox1.Size = New System.Drawing.Size(567, 431)
    Me.HexBox1.StringViewVisible = True
    Me.HexBox1.TabIndex = 0
    Me.HexBox1.UseFixedBytesPerLine = True
    Me.HexBox1.VScrollBarVisible = True
    '
    'frmDC_hexEdit
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(567, 431)
    Me.Controls.Add(Me.HexBox1)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "frmDC_hexEdit"
    Me.Text = "HexEditor"
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents HexBox1 As Be.Windows.Forms.HexBox
End Class
