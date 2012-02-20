<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prop_General
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
    Me.components = New System.ComponentModel.Container
    Me.igHotkeysCol6CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igHotkeysCol6ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
    Me.Label1 = New System.Windows.Forms.Label
    Me.chkTitlebar = New System.Windows.Forms.CheckBox
    Me.chkMenu = New System.Windows.Forms.CheckBox
    Me.chkSubtitlebar = New System.Windows.Forms.CheckBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.chkBookmarks = New System.Windows.Forms.CheckBox
    Me.SuspendLayout()
    '
    'imlToolbarIcons
    '
    Me.imlToolbarIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.imlToolbarIcons.ImageSize = New System.Drawing.Size(16, 16)
    Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.Transparent
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(66, 45)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(338, 25)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Allgemeine Einstellungen . . . . . . ."
    '
    'chkTitlebar
    '
    Me.chkTitlebar.AutoSize = True
    Me.chkTitlebar.Location = New System.Drawing.Point(71, 98)
    Me.chkTitlebar.Name = "chkTitlebar"
    Me.chkTitlebar.Size = New System.Drawing.Size(113, 17)
    Me.chkTitlebar.TabIndex = 1
    Me.chkTitlebar.Text = "Titelzeile anzeigen"
    Me.chkTitlebar.UseVisualStyleBackColor = True
    '
    'chkMenu
    '
    Me.chkMenu.AutoSize = True
    Me.chkMenu.Location = New System.Drawing.Point(71, 144)
    Me.chkMenu.Name = "chkMenu"
    Me.chkMenu.Size = New System.Drawing.Size(99, 17)
    Me.chkMenu.TabIndex = 2
    Me.chkMenu.Text = "Menü anzeigen"
    Me.chkMenu.UseVisualStyleBackColor = True
    '
    'chkSubtitlebar
    '
    Me.chkSubtitlebar.AutoSize = True
    Me.chkSubtitlebar.Location = New System.Drawing.Point(71, 121)
    Me.chkSubtitlebar.Name = "chkSubtitlebar"
    Me.chkSubtitlebar.Size = New System.Drawing.Size(135, 17)
    Me.chkSubtitlebar.TabIndex = 3
    Me.chkSubtitlebar.Text = "Untertitelzeile anzeigen"
    Me.chkSubtitlebar.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(68, 239)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(318, 13)
    Me.Label2.TabIndex = 4
    Me.Label2.Text = "Tipp: die Einstellungen können immer mit ALT+S geöffnet werden."
    '
    'chkBookmarks
    '
    Me.chkBookmarks.AutoSize = True
    Me.chkBookmarks.Location = New System.Drawing.Point(71, 167)
    Me.chkBookmarks.Name = "chkBookmarks"
    Me.chkBookmarks.Size = New System.Drawing.Size(79, 17)
    Me.chkBookmarks.TabIndex = 5
    Me.chkBookmarks.Text = "Bookmarks"
    Me.chkBookmarks.UseVisualStyleBackColor = True
    '
    'prop_General
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.chkBookmarks)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.chkSubtitlebar)
    Me.Controls.Add(Me.chkMenu)
    Me.Controls.Add(Me.chkTitlebar)
    Me.Controls.Add(Me.Label1)
    Me.Name = "prop_General"
    Me.Size = New System.Drawing.Size(460, 352)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
  Friend WithEvents igHotkeysCol6CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igHotkeysCol6ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents chkTitlebar As System.Windows.Forms.CheckBox
  Friend WithEvents chkMenu As System.Windows.Forms.CheckBox
  Friend WithEvents chkSubtitlebar As System.Windows.Forms.CheckBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents chkBookmarks As System.Windows.Forms.CheckBox

End Class
