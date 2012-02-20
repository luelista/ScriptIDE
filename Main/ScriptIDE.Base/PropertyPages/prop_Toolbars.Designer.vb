<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prop_Toolbars
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
    Dim IGColPattern15 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern16 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern17 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern18 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern19 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern20 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern21 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Me.btnTB_delete = New System.Windows.Forms.Button
    Me.Panel3 = New System.Windows.Forms.Panel
    Me.btnTB_rename = New System.Windows.Forms.Button
    Me.btnTB_removeLine = New System.Windows.Forms.Button
    Me.chkTB_visible = New System.Windows.Forms.CheckBox
    Me.igToolbars = New TenTec.Windows.iGridLib.iGrid
    Me.lvwTbCmds = New System.Windows.Forms.ListView
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
    Me.labTB_isScriptedErr = New System.Windows.Forms.Label
    Me.Label1 = New System.Windows.Forms.Label
    Me.btnTB_new = New System.Windows.Forms.Button
    Me.cmbTbList = New System.Windows.Forms.ComboBox
    Me.igToolbarsCol6CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igToolbarsCol6ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.Button1 = New System.Windows.Forms.Button
    Me.Panel3.SuspendLayout()
    CType(Me.igToolbars, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'btnTB_delete
    '
    Me.btnTB_delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnTB_delete.Location = New System.Drawing.Point(322, 8)
    Me.btnTB_delete.Name = "btnTB_delete"
    Me.btnTB_delete.Size = New System.Drawing.Size(67, 23)
    Me.btnTB_delete.TabIndex = 16
    Me.btnTB_delete.Text = "Löschen"
    Me.btnTB_delete.UseVisualStyleBackColor = True
    '
    'Panel3
    '
    Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel3.Controls.Add(Me.Button1)
    Me.Panel3.Controls.Add(Me.btnTB_rename)
    Me.Panel3.Controls.Add(Me.btnTB_removeLine)
    Me.Panel3.Location = New System.Drawing.Point(158, 322)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Size = New System.Drawing.Size(296, 35)
    Me.Panel3.TabIndex = 18
    '
    'btnTB_rename
    '
    Me.btnTB_rename.Location = New System.Drawing.Point(98, 0)
    Me.btnTB_rename.Name = "btnTB_rename"
    Me.btnTB_rename.Size = New System.Drawing.Size(99, 23)
    Me.btnTB_rename.TabIndex = 5
    Me.btnTB_rename.Text = "Umbenennen"
    Me.btnTB_rename.UseVisualStyleBackColor = True
    '
    'btnTB_removeLine
    '
    Me.btnTB_removeLine.Location = New System.Drawing.Point(1, 0)
    Me.btnTB_removeLine.Name = "btnTB_removeLine"
    Me.btnTB_removeLine.Size = New System.Drawing.Size(91, 23)
    Me.btnTB_removeLine.TabIndex = 3
    Me.btnTB_removeLine.Text = "<< Entfernen"
    Me.btnTB_removeLine.UseVisualStyleBackColor = True
    '
    'chkTB_visible
    '
    Me.chkTB_visible.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.chkTB_visible.AutoSize = True
    Me.chkTB_visible.Location = New System.Drawing.Point(398, 12)
    Me.chkTB_visible.Name = "chkTB_visible"
    Me.chkTB_visible.Size = New System.Drawing.Size(63, 17)
    Me.chkTB_visible.TabIndex = 14
    Me.chkTB_visible.Text = "sichtbar"
    Me.chkTB_visible.UseVisualStyleBackColor = True
    '
    'igToolbars
    '
    Me.igToolbars.AllowDrop = True
    Me.igToolbars.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    IGColPattern15.Key = "cmd"
    IGColPattern15.Text = "Command"
    IGColPattern15.Width = 73
    IGColPattern16.Key = "owner"
    IGColPattern16.Text = "Owner"
    IGColPattern17.Key = "text"
    IGColPattern17.Text = "Text"
    IGColPattern17.Width = 69
    IGColPattern18.Key = "view"
    IGColPattern18.Text = "View"
    IGColPattern18.Width = 57
    IGColPattern19.Key = "kind"
    IGColPattern19.Text = "kind"
    IGColPattern19.Width = 0
    IGColPattern20.Key = "icon"
    IGColPattern20.Text = "IconURL"
    IGColPattern20.Width = 10
    IGColPattern21.CellStyle = Me.igToolbarsCol6CellStyle
    IGColPattern21.ColHdrStyle = Me.igToolbarsCol6ColHdrStyle
    IGColPattern21.Text = "para"
    Me.igToolbars.Cols.AddRange(New TenTec.Windows.iGridLib.iGColPattern() {IGColPattern15, IGColPattern16, IGColPattern17, IGColPattern18, IGColPattern19, IGColPattern20, IGColPattern21})
    Me.igToolbars.Header.AllowPress = False
    Me.igToolbars.Header.Height = 19
    Me.igToolbars.Location = New System.Drawing.Point(159, 41)
    Me.igToolbars.Name = "igToolbars"
    Me.igToolbars.Size = New System.Drawing.Size(299, 276)
    Me.igToolbars.TabIndex = 17
    '
    'lvwTbCmds
    '
    Me.lvwTbCmds.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lvwTbCmds.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
    Me.lvwTbCmds.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.lvwTbCmds.Location = New System.Drawing.Point(0, 41)
    Me.lvwTbCmds.Name = "lvwTbCmds"
    Me.lvwTbCmds.Size = New System.Drawing.Size(153, 304)
    Me.lvwTbCmds.SmallImageList = Me.imlToolbarIcons
    Me.lvwTbCmds.TabIndex = 15
    Me.lvwTbCmds.UseCompatibleStateImageBehavior = False
    Me.lvwTbCmds.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Width = 131
    '
    'imlToolbarIcons
    '
    Me.imlToolbarIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.imlToolbarIcons.ImageSize = New System.Drawing.Size(16, 16)
    Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.Transparent
    '
    'labTB_isScriptedErr
    '
    Me.labTB_isScriptedErr.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labTB_isScriptedErr.Enabled = False
    Me.labTB_isScriptedErr.Location = New System.Drawing.Point(-4, 41)
    Me.labTB_isScriptedErr.Name = "labTB_isScriptedErr"
    Me.labTB_isScriptedErr.Size = New System.Drawing.Size(456, 312)
    Me.labTB_isScriptedErr.TabIndex = 13
    Me.labTB_isScriptedErr.Text = "Die ausgewählte Toolbar wird von einem Skript oder Addin intern verwaltet und kan" & _
        "n daher nicht bearbeitet werden."
    Me.labTB_isScriptedErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.labTB_isScriptedErr.Visible = False
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(-1, 13)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(46, 13)
    Me.Label1.TabIndex = 12
    Me.Label1.Text = "Toolbar:"
    '
    'btnTB_new
    '
    Me.btnTB_new.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnTB_new.Location = New System.Drawing.Point(251, 8)
    Me.btnTB_new.Name = "btnTB_new"
    Me.btnTB_new.Size = New System.Drawing.Size(67, 23)
    Me.btnTB_new.TabIndex = 11
    Me.btnTB_new.Text = "Neu ..."
    Me.btnTB_new.UseVisualStyleBackColor = True
    '
    'cmbTbList
    '
    Me.cmbTbList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmbTbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbTbList.FormattingEnabled = True
    Me.cmbTbList.Location = New System.Drawing.Point(50, 9)
    Me.cmbTbList.Name = "cmbTbList"
    Me.cmbTbList.Size = New System.Drawing.Size(195, 21)
    Me.cmbTbList.TabIndex = 10
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(203, 0)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(61, 23)
    Me.Button1.TabIndex = 6
    Me.Button1.Text = "Help"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'prop_Toolbars
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.btnTB_delete)
    Me.Controls.Add(Me.Panel3)
    Me.Controls.Add(Me.chkTB_visible)
    Me.Controls.Add(Me.igToolbars)
    Me.Controls.Add(Me.lvwTbCmds)
    Me.Controls.Add(Me.labTB_isScriptedErr)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.btnTB_new)
    Me.Controls.Add(Me.cmbTbList)
    Me.Name = "prop_Toolbars"
    Me.Size = New System.Drawing.Size(460, 352)
    Me.Panel3.ResumeLayout(False)
    CType(Me.igToolbars, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents btnTB_delete As System.Windows.Forms.Button
  Friend WithEvents Panel3 As System.Windows.Forms.Panel
  Friend WithEvents btnTB_rename As System.Windows.Forms.Button
  Friend WithEvents btnTB_removeLine As System.Windows.Forms.Button
  Friend WithEvents chkTB_visible As System.Windows.Forms.CheckBox
  Friend WithEvents igToolbars As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents lvwTbCmds As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents labTB_isScriptedErr As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents btnTB_new As System.Windows.Forms.Button
  Friend WithEvents cmbTbList As System.Windows.Forms.ComboBox
  Friend WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
  Friend WithEvents igToolbarsCol6CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igToolbarsCol6ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
