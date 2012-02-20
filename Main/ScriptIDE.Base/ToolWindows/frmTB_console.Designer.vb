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
    Me.btnConsoleCls = New System.Windows.Forms.Button
    Me.rtfConsoleOut = New System.Windows.Forms.RichTextBox
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.Button7 = New System.Windows.Forms.Button
    Me.txtRunCommand = New System.Windows.Forms.TextBox
    Me.btnRunCommand = New System.Windows.Forms.Button
    Me.btnZoomOnOff = New System.Windows.Forms.Button
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.Label1 = New System.Windows.Forms.Label
    Me.txtWorkDir = New System.Windows.Forms.TextBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
    Me.SuspendLayout()
    '
    'btnConsoleCls
    '
    Me.btnConsoleCls.Image = CType(resources.GetObject("btnConsoleCls.Image"), System.Drawing.Image)
    Me.btnConsoleCls.Location = New System.Drawing.Point(47, 5)
    Me.btnConsoleCls.Name = "btnConsoleCls"
    Me.btnConsoleCls.Size = New System.Drawing.Size(35, 23)
    Me.btnConsoleCls.TabIndex = 14
    Me.ToolTip1.SetToolTip(Me.btnConsoleCls, "Konsole leeren")
    Me.btnConsoleCls.UseVisualStyleBackColor = True
    '
    'rtfConsoleOut
    '
    Me.rtfConsoleOut.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rtfConsoleOut.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.rtfConsoleOut.Location = New System.Drawing.Point(2, 86)
    Me.rtfConsoleOut.Name = "rtfConsoleOut"
    Me.rtfConsoleOut.Size = New System.Drawing.Size(798, 193)
    Me.rtfConsoleOut.TabIndex = 13
    Me.rtfConsoleOut.Text = ""
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
    Me.Button7.Location = New System.Drawing.Point(86, 32)
    Me.Button7.Name = "Button7"
    Me.Button7.Size = New System.Drawing.Size(78, 23)
    Me.Button7.TabIndex = 11
    Me.Button7.Text = "sendLine:"
    Me.Button7.UseVisualStyleBackColor = True
    '
    'txtRunCommand
    '
    Me.txtRunCommand.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtRunCommand.Location = New System.Drawing.Point(169, 7)
    Me.txtRunCommand.Name = "txtRunCommand"
    Me.txtRunCommand.Size = New System.Drawing.Size(620, 20)
    Me.txtRunCommand.TabIndex = 10
    '
    'btnRunCommand
    '
    Me.btnRunCommand.Location = New System.Drawing.Point(86, 5)
    Me.btnRunCommand.Name = "btnRunCommand"
    Me.btnRunCommand.Size = New System.Drawing.Size(78, 23)
    Me.btnRunCommand.TabIndex = 9
    Me.btnRunCommand.Text = "Run"
    Me.btnRunCommand.UseVisualStyleBackColor = True
    '
    'btnZoomOnOff
    '
    Me.btnZoomOnOff.BackColor = System.Drawing.SystemColors.Control
    Me.btnZoomOnOff.Font = New System.Drawing.Font("Wingdings 3", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
    Me.btnZoomOnOff.Location = New System.Drawing.Point(2, 5)
    Me.btnZoomOnOff.Name = "btnZoomOnOff"
    Me.btnZoomOnOff.Size = New System.Drawing.Size(41, 23)
    Me.btnZoomOnOff.TabIndex = 20
    Me.btnZoomOnOff.Text = "rq"
    Me.btnZoomOnOff.UseVisualStyleBackColor = True
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
    Me.Label2.Location = New System.Drawing.Point(2, 31)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(797, 52)
    Me.Label2.TabIndex = 23
    '
    'Timer1
    '
    Me.Timer1.Interval = 1100
    '
    'frmTB_console
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(801, 283)
    Me.Controls.Add(Me.rtfConsoleOut)
    Me.Controls.Add(Me.txtWorkDir)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.btnZoomOnOff)
    Me.Controls.Add(Me.btnConsoleCls)
    Me.Controls.Add(Me.TextBox1)
    Me.Controls.Add(Me.Button7)
    Me.Controls.Add(Me.txtRunCommand)
    Me.Controls.Add(Me.btnRunCommand)
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
  Friend WithEvents btnConsoleCls As System.Windows.Forms.Button
  Friend WithEvents rtfConsoleOut As System.Windows.Forms.RichTextBox
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Button7 As System.Windows.Forms.Button
  Friend WithEvents txtRunCommand As System.Windows.Forms.TextBox
  Friend WithEvents btnRunCommand As System.Windows.Forms.Button
  Friend WithEvents btnZoomOnOff As System.Windows.Forms.Button
  Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtWorkDir As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
