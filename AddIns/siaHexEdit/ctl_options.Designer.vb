<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctl_options
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
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.RadioButton1 = New System.Windows.Forms.RadioButton
    Me.RadioButton2 = New System.Windows.Forms.RadioButton
    Me.nudBytesPerLine = New System.Windows.Forms.NumericUpDown
    Me.checkAscii = New System.Windows.Forms.CheckBox
    Me.checkLinenumbers = New System.Windows.Forms.CheckBox
    Me.txtLinenumbersColor = New System.Windows.Forms.TextBox
    Me.GroupBox1.SuspendLayout()
    CType(Me.nudBytesPerLine, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.txtLinenumbersColor)
    Me.GroupBox1.Controls.Add(Me.checkLinenumbers)
    Me.GroupBox1.Controls.Add(Me.checkAscii)
    Me.GroupBox1.Controls.Add(Me.nudBytesPerLine)
    Me.GroupBox1.Controls.Add(Me.RadioButton2)
    Me.GroupBox1.Controls.Add(Me.RadioButton1)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(0, 18)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(364, 229)
    Me.GroupBox1.TabIndex = 1
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Hex Editor"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(18, 23)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(80, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Bytes pro Zeile:"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(18, 49)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(70, 13)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Hex-Anzeige:"
    '
    'RadioButton1
    '
    Me.RadioButton1.AutoSize = True
    Me.RadioButton1.Location = New System.Drawing.Point(117, 47)
    Me.RadioButton1.Name = "RadioButton1"
    Me.RadioButton1.Size = New System.Drawing.Size(104, 17)
    Me.RadioButton1.TabIndex = 2
    Me.RadioButton1.TabStop = True
    Me.RadioButton1.Text = "Großbuchstaben"
    Me.RadioButton1.UseVisualStyleBackColor = True
    '
    'RadioButton2
    '
    Me.RadioButton2.AutoSize = True
    Me.RadioButton2.Location = New System.Drawing.Point(227, 47)
    Me.RadioButton2.Name = "RadioButton2"
    Me.RadioButton2.Size = New System.Drawing.Size(104, 17)
    Me.RadioButton2.TabIndex = 3
    Me.RadioButton2.TabStop = True
    Me.RadioButton2.Text = "Kleinbuchstaben"
    Me.RadioButton2.UseVisualStyleBackColor = True
    '
    'nudBytesPerLine
    '
    Me.nudBytesPerLine.Location = New System.Drawing.Point(117, 21)
    Me.nudBytesPerLine.Name = "nudBytesPerLine"
    Me.nudBytesPerLine.Size = New System.Drawing.Size(80, 20)
    Me.nudBytesPerLine.TabIndex = 4
    '
    'checkAscii
    '
    Me.checkAscii.AutoSize = True
    Me.checkAscii.Location = New System.Drawing.Point(21, 87)
    Me.checkAscii.Name = "checkAscii"
    Me.checkAscii.Size = New System.Drawing.Size(91, 17)
    Me.checkAscii.TabIndex = 5
    Me.checkAscii.Text = "ASCII-Ansicht"
    Me.checkAscii.UseVisualStyleBackColor = True
    '
    'checkLinenumbers
    '
    Me.checkLinenumbers.AutoSize = True
    Me.checkLinenumbers.Location = New System.Drawing.Point(21, 114)
    Me.checkLinenumbers.Name = "checkLinenumbers"
    Me.checkLinenumbers.Size = New System.Drawing.Size(98, 17)
    Me.checkLinenumbers.TabIndex = 6
    Me.checkLinenumbers.Text = "Zeilennummern"
    Me.checkLinenumbers.UseVisualStyleBackColor = True
    '
    'txtLinenumbersColor
    '
    Me.txtLinenumbersColor.Location = New System.Drawing.Point(146, 112)
    Me.txtLinenumbersColor.Name = "txtLinenumbersColor"
    Me.txtLinenumbersColor.Size = New System.Drawing.Size(75, 20)
    Me.txtLinenumbersColor.TabIndex = 7
    '
    'ctl_options
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.GroupBox1)
    Me.Name = "ctl_options"
    Me.Size = New System.Drawing.Size(365, 307)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.nudBytesPerLine, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
  Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents checkLinenumbers As System.Windows.Forms.CheckBox
  Friend WithEvents checkAscii As System.Windows.Forms.CheckBox
  Friend WithEvents nudBytesPerLine As System.Windows.Forms.NumericUpDown
  Friend WithEvents txtLinenumbersColor As System.Windows.Forms.TextBox

End Class
