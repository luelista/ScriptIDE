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
    Me.btnChoose = New System.Windows.Forms.Button
    Me.txtDefProjectDir = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.btnChoose)
    Me.GroupBox1.Controls.Add(Me.txtDefProjectDir)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(0, 16)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(367, 90)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Pfade"
    '
    'btnChoose
    '
    Me.btnChoose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnChoose.Location = New System.Drawing.Point(294, 41)
    Me.btnChoose.Name = "btnChoose"
    Me.btnChoose.Size = New System.Drawing.Size(29, 23)
    Me.btnChoose.TabIndex = 2
    Me.btnChoose.Text = "..."
    Me.btnChoose.UseVisualStyleBackColor = True
    '
    'txtDefProjectDir
    '
    Me.txtDefProjectDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtDefProjectDir.Location = New System.Drawing.Point(14, 43)
    Me.txtDefProjectDir.Name = "txtDefProjectDir"
    Me.txtDefProjectDir.Size = New System.Drawing.Size(274, 20)
    Me.txtDefProjectDir.TabIndex = 1
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(11, 27)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(169, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Standardordner für Projektdateien:"
    '
    'ctl_options
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.GroupBox1)
    Me.Name = "ctl_options"
    Me.Size = New System.Drawing.Size(367, 283)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents btnChoose As System.Windows.Forms.Button
  Friend WithEvents txtDefProjectDir As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog

End Class
