<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.ListBox1 = New System.Windows.Forms.ListBox
    Me.tmrRefreshList = New System.Windows.Forms.Timer(Me.components)
    Me.Button1 = New System.Windows.Forms.Button
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(10, 38)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 0
    Me.PictureBox1.TabStop = False
    '
    'ListBox1
    '
    Me.ListBox1.FormattingEnabled = True
    Me.ListBox1.Location = New System.Drawing.Point(51, 3)
    Me.ListBox1.Name = "ListBox1"
    Me.ListBox1.Size = New System.Drawing.Size(232, 82)
    Me.ListBox1.TabIndex = 1
    '
    'tmrRefreshList
    '
    Me.tmrRefreshList.Enabled = True
    '
    'Button1
    '
    Me.Button1.BackColor = System.Drawing.Color.LightCoral
    Me.Button1.Location = New System.Drawing.Point(5, 3)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(42, 25)
    Me.Button1.TabIndex = 2
    Me.Button1.Text = "- X -"
    Me.Button1.UseVisualStyleBackColor = False
    '
    'Form1
    '
    Me.AllowDrop = True
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(284, 88)
    Me.ControlBox = False
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.ListBox1)
    Me.Controls.Add(Me.PictureBox1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "Form1"
    Me.Text = " ScriptIDE Runtime Hosting Process"
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
  Friend WithEvents tmrRefreshList As System.Windows.Forms.Timer
  Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
