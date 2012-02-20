<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prop_Debug
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
    Me.TreeView1 = New System.Windows.Forms.TreeView
    Me.Label1 = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'TreeView1
    '
    Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeView1.Location = New System.Drawing.Point(7, 43)
    Me.TreeView1.Name = "TreeView1"
    Me.TreeView1.Size = New System.Drawing.Size(436, 306)
    Me.TreeView1.TabIndex = 0
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(4, 5)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(59, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Addin-Tree"
    '
    'prop_Debug
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TreeView1)
    Me.Name = "prop_Debug"
    Me.Size = New System.Drawing.Size(460, 352)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
  Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
