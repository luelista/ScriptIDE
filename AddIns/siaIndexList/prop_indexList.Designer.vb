<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prop_indexList
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
    Me.Label1 = New System.Windows.Forms.Label
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(47, 49)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(321, 24)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "...ich bin eine Eigenschaftenseite"
    '
    'ListView1
    '
    Me.ListView1.Location = New System.Drawing.Point(51, 106)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(306, 153)
    Me.ListView1.TabIndex = 1
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.SmallIcon
    '
    'prop_indexList
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.ListView1)
    Me.Controls.Add(Me.Label1)
    Me.Name = "prop_indexList"
    Me.Size = New System.Drawing.Size(460, 352)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ListView1 As System.Windows.Forms.ListView

End Class
