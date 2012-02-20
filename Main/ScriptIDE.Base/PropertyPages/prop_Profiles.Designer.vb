<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prop_Profiles
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(prop_Profiles))
    Me.igHotkeysCol6CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igHotkeysCol6ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
    Me.Label1 = New System.Windows.Forms.Label
    Me.labProfileName = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.txtProfileDisplayName = New System.Windows.Forms.TextBox
    Me.txtFormatWinTitle = New System.Windows.Forms.TextBox
    Me.Label4 = New System.Windows.Forms.Label
    Me.txtFormatWinCaption = New System.Windows.Forms.TextBox
    Me.Label5 = New System.Windows.Forms.Label
    Me.Label6 = New System.Windows.Forms.Label
    Me.txtFormatStatusBar = New System.Windows.Forms.TextBox
    Me.Label7 = New System.Windows.Forms.Label
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.btnOpenProfileFolder = New System.Windows.Forms.Button
    Me.txtProfileFolder = New System.Windows.Forms.TextBox
    Me.Label8 = New System.Windows.Forms.Label
    Me.GroupBox2 = New System.Windows.Forms.GroupBox
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.GroupBox1.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.SuspendLayout()
    '
    'imlToolbarIcons
    '
    Me.imlToolbarIcons.ImageStream = CType(resources.GetObject("imlToolbarIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.Transparent
    Me.imlToolbarIcons.Images.SetKeyName(0, "explorer_252.ico")
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.DimGray
    Me.Label1.Location = New System.Drawing.Point(23, 21)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(120, 24)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Aktives Profil:"
    '
    'labProfileName
    '
    Me.labProfileName.AutoSize = True
    Me.labProfileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labProfileName.ForeColor = System.Drawing.Color.DimGray
    Me.labProfileName.Location = New System.Drawing.Point(185, 21)
    Me.labProfileName.Name = "labProfileName"
    Me.labProfileName.Size = New System.Drawing.Size(72, 24)
    Me.labProfileName.TabIndex = 1
    Me.labProfileName.Text = "default"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(24, 59)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(125, 13)
    Me.Label3.TabIndex = 2
    Me.Label3.Text = "Anzeigename des Profils:"
    '
    'txtProfileDisplayName
    '
    Me.txtProfileDisplayName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProfileDisplayName.Location = New System.Drawing.Point(189, 56)
    Me.txtProfileDisplayName.Name = "txtProfileDisplayName"
    Me.txtProfileDisplayName.Size = New System.Drawing.Size(308, 20)
    Me.txtProfileDisplayName.TabIndex = 3
    '
    'txtFormatWinTitle
    '
    Me.txtFormatWinTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFormatWinTitle.Location = New System.Drawing.Point(189, 114)
    Me.txtFormatWinTitle.Name = "txtFormatWinTitle"
    Me.txtFormatWinTitle.Size = New System.Drawing.Size(308, 20)
    Me.txtFormatWinTitle.TabIndex = 5
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(24, 117)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(121, 13)
    Me.Label4.TabIndex = 4
    Me.Label4.Text = "Format des Fenstertitels:"
    '
    'txtFormatWinCaption
    '
    Me.txtFormatWinCaption.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFormatWinCaption.Location = New System.Drawing.Point(189, 140)
    Me.txtFormatWinCaption.Name = "txtFormatWinCaption"
    Me.txtFormatWinCaption.Size = New System.Drawing.Size(308, 20)
    Me.txtFormatWinCaption.TabIndex = 7
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(24, 143)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(147, 13)
    Me.Label5.TabIndex = 6
    Me.Label5.Text = "Format der Fensterüberschrift:"
    '
    'Label6
    '
    Me.Label6.Location = New System.Drawing.Point(24, 199)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(306, 47)
    Me.Label6.TabIndex = 8
    Me.Label6.Text = "%fn - Dateiname                           %fs - Dateipfad" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "%pd - Anzeigename des " & _
        "Profils     %pn - Interner Profilname" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "%vs - ScriptIDE-Version (kurz)"
    '
    'txtFormatStatusBar
    '
    Me.txtFormatStatusBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFormatStatusBar.Location = New System.Drawing.Point(189, 166)
    Me.txtFormatStatusBar.Name = "txtFormatStatusBar"
    Me.txtFormatStatusBar.Size = New System.Drawing.Size(308, 20)
    Me.txtFormatStatusBar.TabIndex = 10
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(24, 169)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(114, 13)
    Me.Label7.TabIndex = 9
    Me.Label7.Text = "Format der Statuszeile:"
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.btnOpenProfileFolder)
    Me.GroupBox1.Controls.Add(Me.txtProfileFolder)
    Me.GroupBox1.Controls.Add(Me.Label8)
    Me.GroupBox1.Controls.Add(Me.txtFormatStatusBar)
    Me.GroupBox1.Controls.Add(Me.Label7)
    Me.GroupBox1.Controls.Add(Me.Label6)
    Me.GroupBox1.Controls.Add(Me.txtFormatWinCaption)
    Me.GroupBox1.Controls.Add(Me.Label5)
    Me.GroupBox1.Controls.Add(Me.txtFormatWinTitle)
    Me.GroupBox1.Controls.Add(Me.Label4)
    Me.GroupBox1.Controls.Add(Me.txtProfileDisplayName)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.labProfileName)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(0, 5)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(532, 252)
    Me.GroupBox1.TabIndex = 11
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Profil-Optionen"
    '
    'btnOpenProfileFolder
    '
    Me.btnOpenProfileFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOpenProfileFolder.Location = New System.Drawing.Point(439, 80)
    Me.btnOpenProfileFolder.Name = "btnOpenProfileFolder"
    Me.btnOpenProfileFolder.Size = New System.Drawing.Size(58, 23)
    Me.btnOpenProfileFolder.TabIndex = 13
    Me.btnOpenProfileFolder.Text = "Explorer"
    Me.btnOpenProfileFolder.UseVisualStyleBackColor = True
    '
    'txtProfileFolder
    '
    Me.txtProfileFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtProfileFolder.Location = New System.Drawing.Point(189, 82)
    Me.txtProfileFolder.Name = "txtProfileFolder"
    Me.txtProfileFolder.ReadOnly = True
    Me.txtProfileFolder.Size = New System.Drawing.Size(244, 20)
    Me.txtProfileFolder.TabIndex = 12
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Location = New System.Drawing.Point(24, 85)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(63, 13)
    Me.Label8.TabIndex = 11
    Me.Label8.Text = "Profilordner:"
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.ListView1)
    Me.GroupBox2.Location = New System.Drawing.Point(0, 263)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(532, 88)
    Me.GroupBox2.TabIndex = 12
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Liste aller existierenden Profile"
    '
    'ListView1
    '
    Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListView1.LargeImageList = Me.imlToolbarIcons
    Me.ListView1.Location = New System.Drawing.Point(27, 22)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(470, 52)
    Me.ListView1.TabIndex = 0
    Me.ListView1.UseCompatibleStateImageBehavior = False
    '
    'prop_Profiles
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.GroupBox1)
    Me.Name = "prop_Profiles"
    Me.Size = New System.Drawing.Size(532, 352)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
  Friend WithEvents igHotkeysCol6CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igHotkeysCol6ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents labProfileName As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents txtProfileDisplayName As System.Windows.Forms.TextBox
  Friend WithEvents txtFormatWinTitle As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents txtFormatWinCaption As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents txtFormatStatusBar As System.Windows.Forms.TextBox
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents txtProfileFolder As System.Windows.Forms.TextBox
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents btnOpenProfileFolder As System.Windows.Forms.Button
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents ListView1 As System.Windows.Forms.ListView

End Class
