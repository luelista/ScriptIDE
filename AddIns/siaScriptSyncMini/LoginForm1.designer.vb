<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")> _
Partial Class LoginForm1
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
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm1))
    Me.LogoPictureBox = New System.Windows.Forms.PictureBox
    Me.UsernameLabel = New System.Windows.Forms.Label
    Me.PasswordLabel = New System.Windows.Forms.Label
    Me.UsernameTextBox = New System.Windows.Forms.TextBox
    Me.PasswordTextBox = New System.Windows.Forms.TextBox
    Me.OK = New System.Windows.Forms.Button
    Me.Cancel = New System.Windows.Forms.Button
    Me.Label1 = New System.Windows.Forms.Label
    Me.btnLogoff = New System.Windows.Forms.Button
    CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'LogoPictureBox
    '
    Me.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Top
    Me.LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), System.Drawing.Image)
    Me.LogoPictureBox.Location = New System.Drawing.Point(0, 0)
    Me.LogoPictureBox.Name = "LogoPictureBox"
    Me.LogoPictureBox.Size = New System.Drawing.Size(322, 60)
    Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.LogoPictureBox.TabIndex = 0
    Me.LogoPictureBox.TabStop = False
    '
    'UsernameLabel
    '
    Me.UsernameLabel.Location = New System.Drawing.Point(12, 151)
    Me.UsernameLabel.Name = "UsernameLabel"
    Me.UsernameLabel.Size = New System.Drawing.Size(79, 23)
    Me.UsernameLabel.TabIndex = 0
    Me.UsernameLabel.Text = "&Benutzername:"
    Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'PasswordLabel
    '
    Me.PasswordLabel.Location = New System.Drawing.Point(12, 184)
    Me.PasswordLabel.Name = "PasswordLabel"
    Me.PasswordLabel.Size = New System.Drawing.Size(79, 23)
    Me.PasswordLabel.TabIndex = 2
    Me.PasswordLabel.Text = "&Kennwort:"
    Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'UsernameTextBox
    '
    Me.UsernameTextBox.Location = New System.Drawing.Point(97, 153)
    Me.UsernameTextBox.Name = "UsernameTextBox"
    Me.UsernameTextBox.Size = New System.Drawing.Size(212, 20)
    Me.UsernameTextBox.TabIndex = 1
    '
    'PasswordTextBox
    '
    Me.PasswordTextBox.Location = New System.Drawing.Point(97, 186)
    Me.PasswordTextBox.Name = "PasswordTextBox"
    Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.PasswordTextBox.Size = New System.Drawing.Size(212, 20)
    Me.PasswordTextBox.TabIndex = 3
    '
    'OK
    '
    Me.OK.Location = New System.Drawing.Point(137, 227)
    Me.OK.Name = "OK"
    Me.OK.Size = New System.Drawing.Size(83, 23)
    Me.OK.TabIndex = 4
    Me.OK.Text = "OK"
    '
    'Cancel
    '
    Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel.Location = New System.Drawing.Point(226, 227)
    Me.Cancel.Name = "Cancel"
    Me.Cancel.Size = New System.Drawing.Size(83, 23)
    Me.Cancel.TabIndex = 5
    Me.Cancel.Text = "Abbrechen"
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(10, 76)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(298, 56)
    Me.Label1.TabIndex = 6
    Me.Label1.Text = "Bitte logge dich mit deinen TeamWiki.net-Zugangsdaten ein, um ScreenGrab 5.0 zu n" & _
        "utzen." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Als Benutzername kann auch die E-Mail-Adresse oder die Domain des Wikis " & _
        "verwendet werden."
    '
    'btnLogoff
    '
    Me.btnLogoff.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnLogoff.Location = New System.Drawing.Point(8, 227)
    Me.btnLogoff.Name = "btnLogoff"
    Me.btnLogoff.Size = New System.Drawing.Size(98, 23)
    Me.btnLogoff.TabIndex = 7
    Me.btnLogoff.Text = "Ausloggen"
    '
    'LoginForm1
    '
    Me.AcceptButton = Me.OK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel
    Me.ClientSize = New System.Drawing.Size(322, 260)
    Me.Controls.Add(Me.btnLogoff)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Cancel)
    Me.Controls.Add(Me.OK)
    Me.Controls.Add(Me.PasswordTextBox)
    Me.Controls.Add(Me.UsernameTextBox)
    Me.Controls.Add(Me.PasswordLabel)
    Me.Controls.Add(Me.UsernameLabel)
    Me.Controls.Add(Me.LogoPictureBox)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.HelpButton = True
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "LoginForm1"
    Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "TeamWiki.net Login"
    CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents btnLogoff As System.Windows.Forms.Button

End Class
