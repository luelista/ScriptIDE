<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_legacyWidget
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.txtWidgetfilename = New System.Windows.Forms.TextBox
    Me.Button1 = New System.Windows.Forms.Button
    Me.txtClass = New System.Windows.Forms.TextBox
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.BackColor = System.Drawing.Color.LightSteelBlue
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(492, 293)
    Me.Panel1.TabIndex = 0
    '
    'txtWidgetfilename
    '
    Me.txtWidgetfilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtWidgetfilename.Location = New System.Drawing.Point(50, 1)
    Me.txtWidgetfilename.Name = "txtWidgetfilename"
    Me.txtWidgetfilename.Size = New System.Drawing.Size(218, 20)
    Me.txtWidgetfilename.TabIndex = 1
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(1, 0)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(43, 21)
    Me.Button1.TabIndex = 2
    Me.Button1.Text = "load"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'txtClass
    '
    Me.txtClass.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtClass.Location = New System.Drawing.Point(274, 1)
    Me.txtClass.Name = "txtClass"
    Me.txtClass.Size = New System.Drawing.Size(219, 20)
    Me.txtClass.TabIndex = 3
    '
    'frmTB_legacyWidget
    '
    Me.ClientSize = New System.Drawing.Size(492, 293)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.txtClass)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.txtWidgetfilename)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "frmTB_legacyWidget"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
    Me.Text = "legacyWidget"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents txtWidgetfilename As System.Windows.Forms.TextBox
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents txtClass As System.Windows.Forms.TextBox

End Class
