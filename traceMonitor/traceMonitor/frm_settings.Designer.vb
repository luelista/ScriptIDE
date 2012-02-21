<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_settings
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.OK_Button = New System.Windows.Forms.Button
    Me.Cancel_Button = New System.Windows.Forms.Button
    Me.Label2 = New System.Windows.Forms.Label
    Me.txtTcpListenPort = New System.Windows.Forms.TextBox
    Me.txtSubscribePort = New System.Windows.Forms.TextBox
    Me.chkSubscribeMe = New System.Windows.Forms.CheckBox
    Me.txtSubscribeIP = New System.Windows.Forms.TextBox
    Me.txtRegisterIP = New System.Windows.Forms.TextBox
    Me.txtRegisterPort = New System.Windows.Forms.TextBox
    Me.chkRegisterMe = New System.Windows.Forms.CheckBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.chkEnableGrowl = New System.Windows.Forms.CheckBox
    Me.chkTcpListen = New System.Windows.Forms.CheckBox
    Me.Label4 = New System.Windows.Forms.Label
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 360)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Cancel"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(49, 213)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(98, 13)
    Me.Label2.TabIndex = 4
    Me.Label2.Text = "Listen on TCP port:"
    '
    'txtTcpListenPort
    '
    Me.txtTcpListenPort.Location = New System.Drawing.Point(153, 210)
    Me.txtTcpListenPort.Name = "txtTcpListenPort"
    Me.txtTcpListenPort.Size = New System.Drawing.Size(76, 20)
    Me.txtTcpListenPort.TabIndex = 5
    Me.txtTcpListenPort.Text = "10777"
    '
    'txtSubscribePort
    '
    Me.txtSubscribePort.Location = New System.Drawing.Point(239, 267)
    Me.txtSubscribePort.Name = "txtSubscribePort"
    Me.txtSubscribePort.Size = New System.Drawing.Size(76, 20)
    Me.txtSubscribePort.TabIndex = 8
    Me.txtSubscribePort.Text = "10777"
    '
    'chkSubscribeMe
    '
    Me.chkSubscribeMe.AutoSize = True
    Me.chkSubscribeMe.Location = New System.Drawing.Point(32, 248)
    Me.chkSubscribeMe.Name = "chkSubscribeMe"
    Me.chkSubscribeMe.Size = New System.Drawing.Size(147, 17)
    Me.chkSubscribeMe.TabIndex = 6
    Me.chkSubscribeMe.Text = "Subscribe to TraceServer"
    Me.chkSubscribeMe.UseVisualStyleBackColor = True
    '
    'txtSubscribeIP
    '
    Me.txtSubscribeIP.Location = New System.Drawing.Point(104, 267)
    Me.txtSubscribeIP.Name = "txtSubscribeIP"
    Me.txtSubscribeIP.Size = New System.Drawing.Size(100, 20)
    Me.txtSubscribeIP.TabIndex = 9
    '
    'txtRegisterIP
    '
    Me.txtRegisterIP.Location = New System.Drawing.Point(104, 325)
    Me.txtRegisterIP.Name = "txtRegisterIP"
    Me.txtRegisterIP.Size = New System.Drawing.Size(100, 20)
    Me.txtRegisterIP.TabIndex = 12
    '
    'txtRegisterPort
    '
    Me.txtRegisterPort.Location = New System.Drawing.Point(235, 325)
    Me.txtRegisterPort.Name = "txtRegisterPort"
    Me.txtRegisterPort.Size = New System.Drawing.Size(76, 20)
    Me.txtRegisterPort.TabIndex = 11
    Me.txtRegisterPort.Text = "10777"
    '
    'chkRegisterMe
    '
    Me.chkRegisterMe.AutoSize = True
    Me.chkRegisterMe.Location = New System.Drawing.Point(32, 306)
    Me.chkRegisterMe.Name = "chkRegisterMe"
    Me.chkRegisterMe.Size = New System.Drawing.Size(198, 17)
    Me.chkRegisterMe.TabIndex = 10
    Me.chkRegisterMe.Text = "Register and forward to TraceServer"
    Me.chkRegisterMe.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(49, 328)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(186, 13)
    Me.Label1.TabIndex = 13
    Me.Label1.Text = "at IP/Host                                     Port"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(49, 270)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(186, 13)
    Me.Label3.TabIndex = 14
    Me.Label3.Text = "at IP/Host                                     Port"
    '
    'Panel1
    '
    Me.Panel1.BackgroundImage = Global.ScriptIDE.TraceMonitor.My.Resources.Resources.splashscreen3
    Me.Panel1.Location = New System.Drawing.Point(-5, -43)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(440, 179)
    Me.Panel1.TabIndex = 15
    '
    'chkEnableGrowl
    '
    Me.chkEnableGrowl.AutoSize = True
    Me.chkEnableGrowl.BackColor = System.Drawing.Color.Transparent
    Me.chkEnableGrowl.Location = New System.Drawing.Point(32, 155)
    Me.chkEnableGrowl.Name = "chkEnableGrowl"
    Me.chkEnableGrowl.Size = New System.Drawing.Size(89, 17)
    Me.chkEnableGrowl.TabIndex = 1
    Me.chkEnableGrowl.Text = "Enable Growl"
    Me.chkEnableGrowl.UseVisualStyleBackColor = False
    '
    'chkTcpListen
    '
    Me.chkTcpListen.AutoSize = True
    Me.chkTcpListen.BackColor = System.Drawing.Color.Transparent
    Me.chkTcpListen.Checked = True
    Me.chkTcpListen.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkTcpListen.Location = New System.Drawing.Point(32, 189)
    Me.chkTcpListen.Name = "chkTcpListen"
    Me.chkTcpListen.Size = New System.Drawing.Size(114, 17)
    Me.chkTcpListen.TabIndex = 2
    Me.chkTcpListen.Text = "Enable networking"
    Me.chkTcpListen.UseVisualStyleBackColor = False
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.Location = New System.Drawing.Point(29, 366)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(212, 15)
    Me.Label4.TabIndex = 16
    Me.Label4.Text = "Applying settings, please wait ..."
    Me.Label4.Visible = False
    '
    'frm_settings
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(435, 401)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.chkEnableGrowl)
    Me.Controls.Add(Me.chkTcpListen)
    Me.Controls.Add(Me.txtRegisterIP)
    Me.Controls.Add(Me.txtRegisterPort)
    Me.Controls.Add(Me.chkRegisterMe)
    Me.Controls.Add(Me.txtSubscribeIP)
    Me.Controls.Add(Me.txtSubscribePort)
    Me.Controls.Add(Me.chkSubscribeMe)
    Me.Controls.Add(Me.txtTcpListenPort)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Panel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frm_settings"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Settings"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents chkEnableGrowl As System.Windows.Forms.CheckBox
  Friend WithEvents chkTcpListen As System.Windows.Forms.CheckBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txtTcpListenPort As System.Windows.Forms.TextBox
  Friend WithEvents txtSubscribePort As System.Windows.Forms.TextBox
  Friend WithEvents chkSubscribeMe As System.Windows.Forms.CheckBox
  Friend WithEvents txtSubscribeIP As System.Windows.Forms.TextBox
  Friend WithEvents txtRegisterIP As System.Windows.Forms.TextBox
  Friend WithEvents txtRegisterPort As System.Windows.Forms.TextBox
  Friend WithEvents chkRegisterMe As System.Windows.Forms.CheckBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
