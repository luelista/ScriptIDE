<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_rtfFilelist
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_rtfFilelist))
    Me.Panel2 = New System.Windows.Forms.Panel
    Me.ToolStrip2 = New System.Windows.Forms.ToolStrip
    Me.tsb_FilelistRefresh = New System.Windows.Forms.ToolStripButton
    Me.tsb_FilelistNewfile = New System.Windows.Forms.ToolStripButton
    Me.tsb_FilelistRename = New System.Windows.Forms.ToolStripButton
    Me.TreeView1 = New System.Windows.Forms.TreeView
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.Panel2.SuspendLayout()
    Me.ToolStrip2.SuspendLayout()
    Me.SuspendLayout()
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.ToolStrip2)
    Me.Panel2.Controls.Add(Me.TreeView1)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel2.Location = New System.Drawing.Point(0, 0)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(219, 462)
    Me.Panel2.TabIndex = 4
    '
    'ToolStrip2
    '
    Me.ToolStrip2.AutoSize = False
    Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
    Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_FilelistRefresh, Me.tsb_FilelistNewfile, Me.tsb_FilelistRename})
    Me.ToolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
    Me.ToolStrip2.Location = New System.Drawing.Point(2, 2)
    Me.ToolStrip2.Name = "ToolStrip2"
    Me.ToolStrip2.Size = New System.Drawing.Size(257, 23)
    Me.ToolStrip2.TabIndex = 6
    Me.ToolStrip2.Text = "ToolStrip2"
    '
    'tsb_FilelistRefresh
    '
    Me.tsb_FilelistRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsb_FilelistRefresh.Image = CType(resources.GetObject("tsb_FilelistRefresh.Image"), System.Drawing.Image)
    Me.tsb_FilelistRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsb_FilelistRefresh.Name = "tsb_FilelistRefresh"
    Me.tsb_FilelistRefresh.Size = New System.Drawing.Size(23, 20)
    Me.tsb_FilelistRefresh.Text = "Liste neuladen"
    '
    'tsb_FilelistNewfile
    '
    Me.tsb_FilelistNewfile.Image = CType(resources.GetObject("tsb_FilelistNewfile.Image"), System.Drawing.Image)
    Me.tsb_FilelistNewfile.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsb_FilelistNewfile.Name = "tsb_FilelistNewfile"
    Me.tsb_FilelistNewfile.Size = New System.Drawing.Size(85, 20)
    Me.tsb_FilelistNewfile.Text = "Neue Datei"
    '
    'tsb_FilelistRename
    '
    Me.tsb_FilelistRename.Image = CType(resources.GetObject("tsb_FilelistRename.Image"), System.Drawing.Image)
    Me.tsb_FilelistRename.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsb_FilelistRename.Name = "tsb_FilelistRename"
    Me.tsb_FilelistRename.Size = New System.Drawing.Size(99, 20)
    Me.tsb_FilelistRename.Text = "Umbenennen"
    '
    'TreeView1
    '
    Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(254, Byte), Integer))
    Me.TreeView1.HideSelection = False
    Me.TreeView1.HotTracking = True
    Me.TreeView1.ImageKey = "file"
    Me.TreeView1.ImageList = Me.ImageList1
    Me.TreeView1.Location = New System.Drawing.Point(2, 26)
    Me.TreeView1.Name = "TreeView1"
    Me.TreeView1.PathSeparator = "/"
    Me.TreeView1.SelectedImageKey = "file"
    Me.TreeView1.ShowPlusMinus = False
    Me.TreeView1.ShowRootLines = False
    Me.TreeView1.Size = New System.Drawing.Size(214, 435)
    Me.TreeView1.TabIndex = 4
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList1.Images.SetKeyName(0, "home")
    Me.ImageList1.Images.SetKeyName(1, "help")
    Me.ImageList1.Images.SetKeyName(2, "file")
    Me.ImageList1.Images.SetKeyName(3, "act")
    Me.ImageList1.Images.SetKeyName(4, "priv")
    Me.ImageList1.Images.SetKeyName(5, "folderclose")
    Me.ImageList1.Images.SetKeyName(6, "folderopen")
    Me.ImageList1.Images.SetKeyName(7, "doc")
    '
    'frmTB_rtfFilelist
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(219, 462)
    Me.Controls.Add(Me.Panel2)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_rtfFilelist"
    Me.Text = "RTF-Dateiliste"
    Me.Panel2.ResumeLayout(False)
    Me.ToolStrip2.ResumeLayout(False)
    Me.ToolStrip2.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
  Friend WithEvents tsb_FilelistRefresh As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsb_FilelistNewfile As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsb_FilelistRename As System.Windows.Forms.ToolStripButton
  Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
End Class
