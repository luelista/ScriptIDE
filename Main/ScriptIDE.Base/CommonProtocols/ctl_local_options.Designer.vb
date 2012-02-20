<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctl_local_options
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
    Me.igFtpCredCol3CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igFtpCredCol3ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.chkAutoUpdate = New System.Windows.Forms.CheckBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.chkVirtualFolders = New System.Windows.Forms.CheckBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.txtRoot = New System.Windows.Forms.TextBox
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.txtRoot)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.chkVirtualFolders)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.chkAutoUpdate)
    Me.GroupBox1.Location = New System.Drawing.Point(0, 10)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(364, 163)
    Me.GroupBox1.TabIndex = 32
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "FolderTreeView"
    '
    'chkAutoUpdate
    '
    Me.chkAutoUpdate.AutoSize = True
    Me.chkAutoUpdate.Checked = True
    Me.chkAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkAutoUpdate.Location = New System.Drawing.Point(13, 23)
    Me.chkAutoUpdate.Name = "chkAutoUpdate"
    Me.chkAutoUpdate.Size = New System.Drawing.Size(159, 17)
    Me.chkAutoUpdate.TabIndex = 0
    Me.chkAutoUpdate.Text = "Automatische Aktualisierung"
    Me.chkAutoUpdate.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(30, 44)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(321, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "(kann bei sehr häufigen Dateisystemzugriffen zu Problemen führen)"
    '
    'chkVirtualFolders
    '
    Me.chkVirtualFolders.AutoSize = True
    Me.chkVirtualFolders.Checked = True
    Me.chkVirtualFolders.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkVirtualFolders.Location = New System.Drawing.Point(13, 74)
    Me.chkVirtualFolders.Name = "chkVirtualFolders"
    Me.chkVirtualFolders.Size = New System.Drawing.Size(144, 17)
    Me.chkVirtualFolders.TabIndex = 2
    Me.chkVirtualFolders.Text = "Virtuelle Ordner anzeigen"
    Me.chkVirtualFolders.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(10, 109)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(95, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Stammverzeichnis:"
    '
    'txtRoot
    '
    Me.txtRoot.Location = New System.Drawing.Point(13, 125)
    Me.txtRoot.Name = "txtRoot"
    Me.txtRoot.Size = New System.Drawing.Size(338, 20)
    Me.txtRoot.TabIndex = 4
    '
    'ctl_local_options
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoScroll = True
    Me.Controls.Add(Me.GroupBox1)
    Me.Name = "ctl_local_options"
    Me.Size = New System.Drawing.Size(365, 340)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents igFtpCredCol3CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igFtpCredCol3ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents chkAutoUpdate As System.Windows.Forms.CheckBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents chkVirtualFolders As System.Windows.Forms.CheckBox
  Friend WithEvents txtRoot As System.Windows.Forms.TextBox

End Class
