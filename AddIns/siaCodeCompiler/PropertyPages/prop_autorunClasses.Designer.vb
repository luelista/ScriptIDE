<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prop_autorunClasses
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(prop_autorunClasses))
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.Button4 = New System.Windows.Forms.Button
    Me.Button3 = New System.Windows.Forms.Button
    Me.clbScriptClsAutostart = New System.Windows.Forms.CheckedListBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.Button4)
    Me.GroupBox1.Controls.Add(Me.Button3)
    Me.GroupBox1.Controls.Add(Me.clbScriptClsAutostart)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(0, 4)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(364, 333)
    Me.GroupBox1.TabIndex = 32
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Skriptklassen automatisch starten"
    '
    'Button4
    '
    Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
    Me.Button4.Location = New System.Drawing.Point(274, 98)
    Me.Button4.Name = "Button4"
    Me.Button4.Size = New System.Drawing.Size(32, 29)
    Me.Button4.TabIndex = 9
    Me.Button4.UseVisualStyleBackColor = True
    '
    'Button3
    '
    Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
    Me.Button3.Location = New System.Drawing.Point(274, 63)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(32, 29)
    Me.Button3.TabIndex = 8
    Me.Button3.UseVisualStyleBackColor = True
    '
    'clbScriptClsAutostart
    '
    Me.clbScriptClsAutostart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.clbScriptClsAutostart.FormattingEnabled = True
    Me.clbScriptClsAutostart.IntegralHeight = False
    Me.clbScriptClsAutostart.Location = New System.Drawing.Point(19, 63)
    Me.clbScriptClsAutostart.Name = "clbScriptClsAutostart"
    Me.clbScriptClsAutostart.Size = New System.Drawing.Size(246, 256)
    Me.clbScriptClsAutostart.TabIndex = 7
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(16, 26)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(323, 34)
    Me.Label1.TabIndex = 6
    Me.Label1.Text = "Alle ScriptKlassen, die mit einem Häkchen versehen sind, werden beim Start automa" & _
        "tisch aufgerufen ..."
    '
    'prop_autorunClasses
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoScroll = True
    Me.Controls.Add(Me.GroupBox1)
    Me.Name = "prop_autorunClasses"
    Me.Size = New System.Drawing.Size(365, 340)
    Me.GroupBox1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents Button4 As System.Windows.Forms.Button
  Friend WithEvents Button3 As System.Windows.Forms.Button
  Friend WithEvents clbScriptClsAutostart As System.Windows.Forms.CheckedListBox
  Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
