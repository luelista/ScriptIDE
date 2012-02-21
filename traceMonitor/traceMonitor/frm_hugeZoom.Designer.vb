<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_hugeZoom
    Inherits System.Windows.Forms.Form

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
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.Button1 = New System.Windows.Forms.Button
    Me.SuspendLayout()
    '
    'TextBox1
    '
    Me.TextBox1.BackColor = System.Drawing.Color.Black
    Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.TextBox1.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TextBox1.ForeColor = System.Drawing.Color.White
    Me.TextBox1.Location = New System.Drawing.Point(0, 25)
    Me.TextBox1.Multiline = True
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.TextBox1.Size = New System.Drawing.Size(744, 589)
    Me.TextBox1.TabIndex = 0
    Me.TextBox1.WordWrap = False
    '
    'Label1
    '
    Me.Label1.ForeColor = System.Drawing.Color.LightGray
    Me.Label1.Location = New System.Drawing.Point(108, 6)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(329, 19)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "WordWrap: Ctrl+W           TopMost: Ctrl+T           Maximize: F11"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.ForeColor = System.Drawing.Color.White
    Me.Label2.Location = New System.Drawing.Point(12, 6)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(71, 13)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "TraceZoom"
    '
    'Button1
    '
    Me.Button1.BackColor = System.Drawing.SystemColors.Control
    Me.Button1.Location = New System.Drawing.Point(649, 2)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(92, 21)
    Me.Button1.TabIndex = 3
    Me.Button1.Text = "Close (ESC)"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'frm_hugeZoom
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.Black
    Me.ClientSize = New System.Drawing.Size(744, 614)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TextBox1)
    Me.Name = "frm_hugeZoom"
    Me.ShowInTaskbar = False
    Me.Text = "TraceMonitorZoom"
    Me.TopMost = True
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
