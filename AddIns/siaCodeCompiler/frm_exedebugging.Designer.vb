<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_exedebugging
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_exedebugging))
    Me.Label1 = New System.Windows.Forms.Label
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.Button1 = New System.Windows.Forms.Button
    Me.Button2 = New System.Windows.Forms.Button
    Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(3, 6)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(236, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Die folgende EXE befindet sich im Debugmodus:"
    '
    'TextBox1
    '
    Me.TextBox1.Location = New System.Drawing.Point(6, 22)
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(282, 20)
    Me.TextBox1.TabIndex = 1
    '
    'Button1
    '
    Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Button1.Location = New System.Drawing.Point(6, 358)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(133, 27)
    Me.Button1.TabIndex = 2
    Me.Button1.Text = "Scriptcode anzeigen"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'Button2
    '
    Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Button2.Location = New System.Drawing.Point(145, 358)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(133, 27)
    Me.Button2.TabIndex = 3
    Me.Button2.Text = "Prozess beenden"
    Me.Button2.UseVisualStyleBackColor = True
    '
    'PropertyGrid1
    '
    Me.PropertyGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PropertyGrid1.Location = New System.Drawing.Point(6, 48)
    Me.PropertyGrid1.Name = "PropertyGrid1"
    Me.PropertyGrid1.Size = New System.Drawing.Size(282, 304)
    Me.PropertyGrid1.TabIndex = 4
    Me.PropertyGrid1.ToolbarVisible = False
    '
    'frm_exedebugging
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(295, 387)
    Me.Controls.Add(Me.PropertyGrid1)
    Me.Controls.Add(Me.Button2)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.TextBox1)
    Me.Controls.Add(Me.Label1)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frm_exedebugging"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
    Me.Text = "frm_exedebugging"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
End Class
