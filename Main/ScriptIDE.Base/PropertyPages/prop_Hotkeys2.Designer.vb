<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prop_Hotkeys2
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(prop_Hotkeys2))
    Me.igToolbarsCol6CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igToolbarsCol6ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.lvwTbCmds = New System.Windows.Forms.ListView
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
    Me.TreeView1 = New System.Windows.Forms.TreeView
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.btnAddKey = New System.Windows.Forms.Button
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.chk_Win = New System.Windows.Forms.CheckBox
    Me.chk_Alt = New System.Windows.Forms.CheckBox
    Me.chk_Shift = New System.Windows.Forms.CheckBox
    Me.chk_Control = New System.Windows.Forms.CheckBox
    Me.txtKeyCode = New System.Windows.Forms.TextBox
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.tsm_Enabled = New System.Windows.Forms.ToolStripMenuItem
    Me.tsm_EditPara = New System.Windows.Forms.ToolStripMenuItem
    Me.tsm_ValidArea = New System.Windows.Forms.ToolStripMenuItem
    Me.GlobalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.TextEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.MainWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.AnyToolWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.tsm_Delete = New System.Windows.Forms.ToolStripMenuItem
    Me.GroupBox1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.ContextMenuStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'lvwTbCmds
    '
    Me.lvwTbCmds.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lvwTbCmds.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
    Me.lvwTbCmds.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.lvwTbCmds.Location = New System.Drawing.Point(0, 3)
    Me.lvwTbCmds.Name = "lvwTbCmds"
    Me.lvwTbCmds.Size = New System.Drawing.Size(153, 342)
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
    Me.imlToolbarIcons.ImageStream = CType(resources.GetObject("imlToolbarIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.Transparent
    Me.imlToolbarIcons.Images.SetKeyName(0, "key")
    Me.imlToolbarIcons.Images.SetKeyName(1, "area")
    '
    'TreeView1
    '
    Me.TreeView1.AllowDrop = True
    Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeView1.CheckBoxes = True
    Me.TreeView1.Location = New System.Drawing.Point(174, 76)
    Me.TreeView1.Name = "TreeView1"
    Me.TreeView1.Size = New System.Drawing.Size(273, 269)
    Me.TreeView1.TabIndex = 16
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.btnAddKey)
    Me.GroupBox1.Controls.Add(Me.PictureBox1)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.chk_Win)
    Me.GroupBox1.Controls.Add(Me.chk_Alt)
    Me.GroupBox1.Controls.Add(Me.chk_Shift)
    Me.GroupBox1.Controls.Add(Me.chk_Control)
    Me.GroupBox1.Controls.Add(Me.txtKeyCode)
    Me.GroupBox1.Location = New System.Drawing.Point(174, 3)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(273, 67)
    Me.GroupBox1.TabIndex = 18
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Tastenkombination hinzufügen"
    '
    'btnAddKey
    '
    Me.btnAddKey.Location = New System.Drawing.Point(173, 17)
    Me.btnAddKey.Name = "btnAddKey"
    Me.btnAddKey.Size = New System.Drawing.Size(89, 23)
    Me.btnAddKey.TabIndex = 19
    Me.btnAddKey.Text = "Hinzufügen"
    Me.btnAddKey.UseVisualStyleBackColor = True
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(17, 19)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 6
    Me.PictureBox1.TabStop = False
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(170, 22)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(16, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "..."
    '
    'chk_Win
    '
    Me.chk_Win.AutoSize = True
    Me.chk_Win.Location = New System.Drawing.Point(217, 43)
    Me.chk_Win.Name = "chk_Win"
    Me.chk_Win.Size = New System.Drawing.Size(45, 17)
    Me.chk_Win.TabIndex = 5
    Me.chk_Win.Text = "Win"
    Me.chk_Win.UseVisualStyleBackColor = True
    '
    'chk_Alt
    '
    Me.chk_Alt.AutoSize = True
    Me.chk_Alt.Location = New System.Drawing.Point(173, 43)
    Me.chk_Alt.Name = "chk_Alt"
    Me.chk_Alt.Size = New System.Drawing.Size(38, 17)
    Me.chk_Alt.TabIndex = 4
    Me.chk_Alt.Text = "Alt"
    Me.chk_Alt.UseVisualStyleBackColor = True
    '
    'chk_Shift
    '
    Me.chk_Shift.AutoSize = True
    Me.chk_Shift.Location = New System.Drawing.Point(120, 43)
    Me.chk_Shift.Name = "chk_Shift"
    Me.chk_Shift.Size = New System.Drawing.Size(47, 17)
    Me.chk_Shift.TabIndex = 3
    Me.chk_Shift.Text = "Shift"
    Me.chk_Shift.UseVisualStyleBackColor = True
    '
    'chk_Control
    '
    Me.chk_Control.AutoSize = True
    Me.chk_Control.Location = New System.Drawing.Point(69, 43)
    Me.chk_Control.Name = "chk_Control"
    Me.chk_Control.Size = New System.Drawing.Size(45, 17)
    Me.chk_Control.TabIndex = 2
    Me.chk_Control.Text = "Strg"
    Me.chk_Control.UseVisualStyleBackColor = True
    '
    'txtKeyCode
    '
    Me.txtKeyCode.BackColor = System.Drawing.Color.White
    Me.txtKeyCode.Location = New System.Drawing.Point(69, 19)
    Me.txtKeyCode.Name = "txtKeyCode"
    Me.txtKeyCode.ReadOnly = True
    Me.txtKeyCode.Size = New System.Drawing.Size(94, 20)
    Me.txtKeyCode.TabIndex = 0
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_Enabled, Me.tsm_EditPara, Me.tsm_ValidArea, Me.tsm_Delete})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(188, 114)
    '
    'tsm_Enabled
    '
    Me.tsm_Enabled.Checked = True
    Me.tsm_Enabled.CheckState = System.Windows.Forms.CheckState.Checked
    Me.tsm_Enabled.Name = "tsm_Enabled"
    Me.tsm_Enabled.Size = New System.Drawing.Size(187, 22)
    Me.tsm_Enabled.Text = "Aktiviert"
    '
    'tsm_EditPara
    '
    Me.tsm_EditPara.Name = "tsm_EditPara"
    Me.tsm_EditPara.Size = New System.Drawing.Size(187, 22)
    Me.tsm_EditPara.Text = "Parameter bearbeiten"
    '
    'tsm_ValidArea
    '
    Me.tsm_ValidArea.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GlobalToolStripMenuItem, Me.TextEditorToolStripMenuItem, Me.MainWindowToolStripMenuItem, Me.AnyToolWindowToolStripMenuItem})
    Me.tsm_ValidArea.Name = "tsm_ValidArea"
    Me.tsm_ValidArea.Size = New System.Drawing.Size(187, 22)
    Me.tsm_ValidArea.Text = "Gültiger Bereich"
    '
    'GlobalToolStripMenuItem
    '
    Me.GlobalToolStripMenuItem.Name = "GlobalToolStripMenuItem"
    Me.GlobalToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
    Me.GlobalToolStripMenuItem.Text = "Global"
    '
    'TextEditorToolStripMenuItem
    '
    Me.TextEditorToolStripMenuItem.Name = "TextEditorToolStripMenuItem"
    Me.TextEditorToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
    Me.TextEditorToolStripMenuItem.Text = "TextEditor"
    '
    'MainWindowToolStripMenuItem
    '
    Me.MainWindowToolStripMenuItem.Name = "MainWindowToolStripMenuItem"
    Me.MainWindowToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
    Me.MainWindowToolStripMenuItem.Text = "MainWindow"
    '
    'AnyToolWindowToolStripMenuItem
    '
    Me.AnyToolWindowToolStripMenuItem.Name = "AnyToolWindowToolStripMenuItem"
    Me.AnyToolWindowToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
    Me.AnyToolWindowToolStripMenuItem.Text = "AnyToolWindow"
    '
    'tsm_Delete
    '
    Me.tsm_Delete.Name = "tsm_Delete"
    Me.tsm_Delete.Size = New System.Drawing.Size(187, 22)
    Me.tsm_Delete.Text = "Löschen"
    '
    'prop_Hotkeys2
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.TreeView1)
    Me.Controls.Add(Me.lvwTbCmds)
    Me.Name = "prop_Hotkeys2"
    Me.Size = New System.Drawing.Size(460, 352)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ContextMenuStrip1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents lvwTbCmds As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
  Friend WithEvents igToolbarsCol6CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igToolbarsCol6ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents chk_Win As System.Windows.Forms.CheckBox
  Friend WithEvents chk_Alt As System.Windows.Forms.CheckBox
  Friend WithEvents chk_Shift As System.Windows.Forms.CheckBox
  Friend WithEvents chk_Control As System.Windows.Forms.CheckBox
  Friend WithEvents txtKeyCode As System.Windows.Forms.TextBox
  Friend WithEvents btnAddKey As System.Windows.Forms.Button
  Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents tsm_Enabled As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents tsm_EditPara As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents tsm_ValidArea As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents GlobalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TextEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents MainWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AnyToolWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents tsm_Delete As System.Windows.Forms.ToolStripMenuItem

End Class
