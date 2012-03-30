<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_console
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_console))
    Me.btnClear = New System.Windows.Forms.Button
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.Button7 = New System.Windows.Forms.Button
    Me.btnStop = New System.Windows.Forms.Button
    Me.btnZoomOnOff = New System.Windows.Forms.Button
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.btnStart = New System.Windows.Forms.Button
    Me.btnClose = New System.Windows.Forms.Button
    Me.Label1 = New System.Windows.Forms.Label
    Me.txtWorkDir = New System.Windows.Forms.TextBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
    Me.pnlRtfContainer = New System.Windows.Forms.Panel
    Me.cmbCommand = New System.Windows.Forms.ComboBox
    Me.SuspendLayout()
    '
    'btnClear
    '
    Me.btnClear.Enabled = False
    Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
    Me.btnClear.Location = New System.Drawing.Point(47, 4)
    Me.btnClear.Name = "btnClear"
    Me.btnClear.Size = New System.Drawing.Size(36, 24)
    Me.btnClear.TabIndex = 14
    Me.ToolTip1.SetToolTip(Me.btnClear, "Clear console")
    Me.btnClear.UseVisualStyleBackColor = True
    '
    'TextBox1
    '
    Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBox1.Location = New System.Drawing.Point(169, 34)
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(620, 20)
    Me.TextBox1.TabIndex = 12
    '
    'Button7
    '
    Me.Button7.Location = New System.Drawing.Point(86, 34)
    Me.Button7.Name = "Button7"
    Me.Button7.Size = New System.Drawing.Size(78, 21)
    Me.Button7.TabIndex = 11
    Me.Button7.Text = "sendLine:"
    Me.Button7.UseVisualStyleBackColor = True
    '
    'btnStop
    '
    Me.btnStop.Enabled = False
    Me.btnStop.Image = CType(resources.GetObject("btnStop.Image"), System.Drawing.Image)
    Me.btnStop.Location = New System.Drawing.Point(87, 4)
    Me.btnStop.Name = "btnStop"
    Me.btnStop.Size = New System.Drawing.Size(36, 24)
    Me.btnStop.TabIndex = 9
    Me.ToolTip1.SetToolTip(Me.btnStop, "Stop command")
    Me.btnStop.UseVisualStyleBackColor = True
    '
    'btnZoomOnOff
    '
    Me.btnZoomOnOff.BackColor = System.Drawing.SystemColors.Control
    Me.btnZoomOnOff.Font = New System.Drawing.Font("Wingdings 3", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
    Me.btnZoomOnOff.Location = New System.Drawing.Point(2, 4)
    Me.btnZoomOnOff.Name = "btnZoomOnOff"
    Me.btnZoomOnOff.Size = New System.Drawing.Size(41, 24)
    Me.btnZoomOnOff.TabIndex = 20
    Me.btnZoomOnOff.Text = "rq"
    Me.btnZoomOnOff.UseVisualStyleBackColor = True
    '
    'btnStart
    '
    Me.btnStart.Image = CType(resources.GetObject("btnStart.Image"), System.Drawing.Image)
    Me.btnStart.Location = New System.Drawing.Point(127, 4)
    Me.btnStart.Name = "btnStart"
    Me.btnStart.Size = New System.Drawing.Size(36, 24)
    Me.btnStart.TabIndex = 26
    Me.ToolTip1.SetToolTip(Me.btnStart, "Run command")
    Me.btnStart.UseVisualStyleBackColor = True
    '
    'btnClose
    '
    Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnClose.Enabled = False
    Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
    Me.btnClose.Location = New System.Drawing.Point(752, 4)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(36, 24)
    Me.btnClose.TabIndex = 27
    Me.ToolTip1.SetToolTip(Me.btnClose, "Close this console")
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.BackColor = System.Drawing.Color.Khaki
    Me.Label1.Location = New System.Drawing.Point(83, 63)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(81, 13)
    Me.Label1.TabIndex = 21
    Me.Label1.Text = "Work Directory:"
    '
    'txtWorkDir
    '
    Me.txtWorkDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtWorkDir.Location = New System.Drawing.Point(169, 60)
    Me.txtWorkDir.Name = "txtWorkDir"
    Me.txtWorkDir.Size = New System.Drawing.Size(620, 20)
    Me.txtWorkDir.TabIndex = 22
    '
    'Label2
    '
    Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label2.BackColor = System.Drawing.Color.Khaki
    Me.Label2.Location = New System.Drawing.Point(0, 31)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(802, 52)
    Me.Label2.TabIndex = 23
    '
    'Timer1
    '
    Me.Timer1.Interval = 1100
    '
    'pnlRtfContainer
    '
    Me.pnlRtfContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlRtfContainer.Location = New System.Drawing.Point(2, 86)
    Me.pnlRtfContainer.Name = "pnlRtfContainer"
    Me.pnlRtfContainer.Size = New System.Drawing.Size(786, 197)
    Me.pnlRtfContainer.TabIndex = 24
    '
    'cmbCommand
    '
    Me.cmbCommand.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmbCommand.FormattingEnabled = True
    Me.cmbCommand.Location = New System.Drawing.Point(169, 6)
    Me.cmbCommand.Name = "cmbCommand"
    Me.cmbCommand.Size = New System.Drawing.Size(577, 21)
    Me.cmbCommand.TabIndex = 25
    '
    'frmTB_console
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(801, 283)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnStart)
    Me.Controls.Add(Me.cmbCommand)
    Me.Controls.Add(Me.pnlRtfContainer)
    Me.Controls.Add(Me.txtWorkDir)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.btnZoomOnOff)
    Me.Controls.Add(Me.btnClear)
    Me.Controls.Add(Me.TextBox1)
    Me.Controls.Add(Me.Button7)
    Me.Controls.Add(Me.btnStop)
    Me.Controls.Add(Me.Label2)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_console"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom
    Me.Text = "Konsole"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents btnClear As System.Windows.Forms.Button
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Button7 As System.Windows.Forms.Button
  Friend WithEvents btnStop As System.Windows.Forms.Button
  Friend WithEvents btnZoomOnOff As System.Windows.Forms.Button
  Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtWorkDir As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Timer1 As System.Windows.Forms.Timer
  Friend WithEvents pnlRtfContainer As System.Windows.Forms.Panel
  Friend WithEvents cmbCommand As System.Windows.Forms.ComboBox
  Friend WithEvents btnStart As System.Windows.Forms.Button
  Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
