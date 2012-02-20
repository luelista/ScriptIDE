<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class idx_default
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
    Me.ListBox1 = New System.Windows.Forms.ListBox
    Me.SuspendLayout()
    '
    'ListBox1
    '
    Me.ListBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(181, Byte), Integer))
    Me.ListBox1.ForeColor = System.Drawing.Color.Black
    Me.ListBox1.FormattingEnabled = True
    Me.ListBox1.IntegralHeight = False
    Me.ListBox1.Location = New System.Drawing.Point(0, 0)
    Me.ListBox1.Name = "ListBox1"
    Me.ListBox1.Size = New System.Drawing.Size(209, 417)
    Me.ListBox1.TabIndex = 8
    '
    'idx_default
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.ListBox1)
    Me.Name = "idx_default"
    Me.Size = New System.Drawing.Size(209, 417)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ListBox1 As System.Windows.Forms.ListBox

End Class
