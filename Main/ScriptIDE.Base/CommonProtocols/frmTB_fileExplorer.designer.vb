<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_fileExplorer
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_fileExplorer))
    Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
    Me.Button2 = New System.Windows.Forms.Button
    Me.AxFolderTreeview2 = New AxCCRPFolderTV6.AxFolderTreeview
    Me.txtLocFolder = New System.Windows.Forms.TextBox
    Me.pnlSearch = New System.Windows.Forms.Panel
    Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
    Me.Button1 = New System.Windows.Forms.Button
    Me.rbSearchEverywhere = New System.Windows.Forms.RadioButton
    Me.rbSearchLocal = New System.Windows.Forms.RadioButton
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.txtSearchRoot = New System.Windows.Forms.TextBox
    Me.txtSearch = New System.Windows.Forms.TextBox
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
    Me.col_Pfad = New System.Windows.Forms.ColumnHeader
    Me.imlLocalFolder = New System.Windows.Forms.ImageList(Me.components)
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
    Me.tspShowHide = New System.Windows.Forms.ToolStripButton
    Me.tsbDirUp = New System.Windows.Forms.ToolStripButton
    Me.tsbNewFile = New System.Windows.Forms.ToolStripButton
    Me.tsbExplore = New System.Windows.Forms.ToolStripButton
    Me.tsbView = New System.Windows.Forms.ToolStripButton
    Me.tsbScriptDir = New System.Windows.Forms.ToolStripButton
    Me.tsbNew = New System.Windows.Forms.ToolStripButton
    Me.tsbRename = New System.Windows.Forms.ToolStripButton
    Me.tsbExplorer = New System.Windows.Forms.ToolStripButton
    Me.imlSearchResult = New System.Windows.Forms.ImageList(Me.components)
    Me.cmFavoriten = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.SplitContainer3.Panel1.SuspendLayout()
    Me.SplitContainer3.Panel2.SuspendLayout()
    Me.SplitContainer3.SuspendLayout()
    CType(Me.AxFolderTreeview2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlSearch.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.ToolStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'SplitContainer3
    '
    Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
    Me.SplitContainer3.Name = "SplitContainer3"
    Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplitContainer3.Panel1
    '
    Me.SplitContainer3.Panel1.Controls.Add(Me.Button2)
    Me.SplitContainer3.Panel1.Controls.Add(Me.AxFolderTreeview2)
    Me.SplitContainer3.Panel1.Controls.Add(Me.txtLocFolder)
    '
    'SplitContainer3.Panel2
    '
    Me.SplitContainer3.Panel2.Controls.Add(Me.pnlSearch)
    Me.SplitContainer3.Panel2.Controls.Add(Me.ListView1)
    Me.SplitContainer3.Panel2.Controls.Add(Me.ToolStrip1)
    Me.SplitContainer3.Size = New System.Drawing.Size(338, 509)
    Me.SplitContainer3.SplitterDistance = 208
    Me.SplitContainer3.TabIndex = 4
    '
    'Button2
    '
    Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
    Me.Button2.Location = New System.Drawing.Point(0, 0)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(58, 24)
    Me.Button2.TabIndex = 2
    Me.Button2.Text = "FAV"
    Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.Button2.UseVisualStyleBackColor = True
    '
    'AxFolderTreeview2
    '
    Me.AxFolderTreeview2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.AxFolderTreeview2.Location = New System.Drawing.Point(0, 24)
    Me.AxFolderTreeview2.Name = "AxFolderTreeview2"
    Me.AxFolderTreeview2.OcxState = CType(resources.GetObject("AxFolderTreeview2.OcxState"), System.Windows.Forms.AxHost.State)
    Me.AxFolderTreeview2.Size = New System.Drawing.Size(338, 184)
    Me.AxFolderTreeview2.TabIndex = 1
    '
    'txtLocFolder
    '
    Me.txtLocFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLocFolder.Location = New System.Drawing.Point(60, 2)
    Me.txtLocFolder.Name = "txtLocFolder"
    Me.txtLocFolder.Size = New System.Drawing.Size(277, 20)
    Me.txtLocFolder.TabIndex = 0
    '
    'pnlSearch
    '
    Me.pnlSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlSearch.BackColor = System.Drawing.Color.LightSteelBlue
    Me.pnlSearch.Controls.Add(Me.LinkLabel1)
    Me.pnlSearch.Controls.Add(Me.Button1)
    Me.pnlSearch.Controls.Add(Me.rbSearchEverywhere)
    Me.pnlSearch.Controls.Add(Me.rbSearchLocal)
    Me.pnlSearch.Controls.Add(Me.PictureBox1)
    Me.pnlSearch.Controls.Add(Me.txtSearchRoot)
    Me.pnlSearch.Controls.Add(Me.txtSearch)
    Me.pnlSearch.Location = New System.Drawing.Point(0, 203)
    Me.pnlSearch.Name = "pnlSearch"
    Me.pnlSearch.Size = New System.Drawing.Size(338, 70)
    Me.pnlSearch.TabIndex = 3
    Me.pnlSearch.Visible = False
    '
    'LinkLabel1
    '
    Me.LinkLabel1.AutoSize = True
    Me.LinkLabel1.LinkArea = New System.Windows.Forms.LinkArea(1, 10)
    Me.LinkLabel1.Location = New System.Drawing.Point(147, 52)
    Me.LinkLabel1.Name = "LinkLabel1"
    Me.LinkLabel1.Size = New System.Drawing.Size(167, 17)
    Me.LinkLabel1.TabIndex = 7
    Me.LinkLabel1.TabStop = True
    Me.LinkLabel1.Text = "(Everything muss installiert sein)"
    Me.LinkLabel1.UseCompatibleTextRendering = True
    '
    'Button1
    '
    Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Button1.Location = New System.Drawing.Point(304, 29)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(29, 23)
    Me.Button1.TabIndex = 6
    Me.Button1.Text = "<<"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'rbSearchEverywhere
    '
    Me.rbSearchEverywhere.AutoSize = True
    Me.rbSearchEverywhere.Location = New System.Drawing.Point(44, 50)
    Me.rbSearchEverywhere.Name = "rbSearchEverywhere"
    Me.rbSearchEverywhere.Size = New System.Drawing.Size(96, 17)
    Me.rbSearchEverywhere.TabIndex = 5
    Me.rbSearchEverywhere.TabStop = True
    Me.rbSearchEverywhere.Text = "Überall suchen"
    Me.rbSearchEverywhere.UseVisualStyleBackColor = True
    '
    'rbSearchLocal
    '
    Me.rbSearchLocal.AutoSize = True
    Me.rbSearchLocal.Location = New System.Drawing.Point(44, 31)
    Me.rbSearchLocal.Name = "rbSearchLocal"
    Me.rbSearchLocal.Size = New System.Drawing.Size(105, 17)
    Me.rbSearchLocal.TabIndex = 4
    Me.rbSearchLocal.TabStop = True
    Me.rbSearchLocal.Text = "Lokal Suchen in:"
    Me.rbSearchLocal.UseVisualStyleBackColor = True
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(5, 10)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 3
    Me.PictureBox1.TabStop = False
    '
    'txtSearchRoot
    '
    Me.txtSearchRoot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSearchRoot.Location = New System.Drawing.Point(147, 30)
    Me.txtSearchRoot.Name = "txtSearchRoot"
    Me.txtSearchRoot.Size = New System.Drawing.Size(152, 20)
    Me.txtSearchRoot.TabIndex = 1
    '
    'txtSearch
    '
    Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSearch.Location = New System.Drawing.Point(45, 5)
    Me.txtSearch.Name = "txtSearch"
    Me.txtSearch.Size = New System.Drawing.Size(254, 20)
    Me.txtSearch.TabIndex = 0
    '
    'ListView1
    '
    Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.col_Pfad})
    Me.ListView1.LabelEdit = True
    Me.ListView1.Location = New System.Drawing.Point(0, 0)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(338, 270)
    Me.ListView1.SmallImageList = Me.imlLocalFolder
    Me.ListView1.TabIndex = 2
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "Dateiname"
    Me.ColumnHeader1.Width = 120
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Letzte Änderung"
    Me.ColumnHeader2.Width = 93
    '
    'ColumnHeader3
    '
    Me.ColumnHeader3.Text = "Größe"
    Me.ColumnHeader3.Width = 41
    '
    'col_Pfad
    '
    Me.col_Pfad.Text = "Pfad"
    Me.col_Pfad.Width = 0
    '
    'imlLocalFolder
    '
    Me.imlLocalFolder.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.imlLocalFolder.ImageSize = New System.Drawing.Size(16, 16)
    Me.imlLocalFolder.TransparentColor = System.Drawing.Color.Transparent
    '
    'ToolStrip1
    '
    Me.ToolStrip1.AutoSize = False
    Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tspShowHide, Me.tsbDirUp, Me.tsbNewFile, Me.tsbExplore, Me.tsbView})
    Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
    Me.ToolStrip1.Location = New System.Drawing.Point(0, 272)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(338, 25)
    Me.ToolStrip1.TabIndex = 1
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'tspShowHide
    '
    Me.tspShowHide.Font = New System.Drawing.Font("Wingdings 3", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
    Me.tspShowHide.Image = CType(resources.GetObject("tspShowHide.Image"), System.Drawing.Image)
    Me.tspShowHide.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tspShowHide.Name = "tspShowHide"
    Me.tspShowHide.Size = New System.Drawing.Size(40, 22)
    Me.tspShowHide.Text = "p"
    Me.tspShowHide.ToolTipText = "Suche ein/ausblenden"
    '
    'tsbDirUp
    '
    Me.tsbDirUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbDirUp.Image = CType(resources.GetObject("tsbDirUp.Image"), System.Drawing.Image)
    Me.tsbDirUp.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbDirUp.Name = "tsbDirUp"
    Me.tsbDirUp.Size = New System.Drawing.Size(23, 22)
    Me.tsbDirUp.Text = "Übergeordnetes Verzeichnis"
    Me.tsbDirUp.ToolTipText = "Übergeordnetes Verzeichnis"
    '
    'tsbNewFile
    '
    Me.tsbNewFile.Image = CType(resources.GetObject("tsbNewFile.Image"), System.Drawing.Image)
    Me.tsbNewFile.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbNewFile.Name = "tsbNewFile"
    Me.tsbNewFile.Size = New System.Drawing.Size(85, 22)
    Me.tsbNewFile.Text = "Neue Datei"
    Me.tsbNewFile.ToolTipText = "Neue Datei anlegen"
    '
    'tsbExplore
    '
    Me.tsbExplore.Image = CType(resources.GetObject("tsbExplore.Image"), System.Drawing.Image)
    Me.tsbExplore.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbExplore.Name = "tsbExplore"
    Me.tsbExplore.Size = New System.Drawing.Size(69, 22)
    Me.tsbExplore.Text = "Explorer"
    Me.tsbExplore.ToolTipText = "Explorer im aktuellen Verzeichnis öffnen"
    '
    'tsbView
    '
    Me.tsbView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbView.Image = CType(resources.GetObject("tsbView.Image"), System.Drawing.Image)
    Me.tsbView.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbView.Name = "tsbView"
    Me.tsbView.Size = New System.Drawing.Size(23, 22)
    Me.tsbView.Text = "Ansicht"
    '
    'tsbScriptDir
    '
    Me.tsbScriptDir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbScriptDir.Image = CType(resources.GetObject("tsbScriptDir.Image"), System.Drawing.Image)
    Me.tsbScriptDir.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbScriptDir.Name = "tsbScriptDir"
    Me.tsbScriptDir.Size = New System.Drawing.Size(23, 22)
    Me.tsbScriptDir.Text = "Scriptfolder anzeigen"
    '
    'tsbNew
    '
    Me.tsbNew.Image = CType(resources.GetObject("tsbNew.Image"), System.Drawing.Image)
    Me.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbNew.Name = "tsbNew"
    Me.tsbNew.Size = New System.Drawing.Size(49, 22)
    Me.tsbNew.Text = "Neu"
    '
    'tsbRename
    '
    Me.tsbRename.Image = CType(resources.GetObject("tsbRename.Image"), System.Drawing.Image)
    Me.tsbRename.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbRename.Name = "tsbRename"
    Me.tsbRename.Size = New System.Drawing.Size(99, 22)
    Me.tsbRename.Text = "Umbenennen"
    '
    'tsbExplorer
    '
    Me.tsbExplorer.Image = CType(resources.GetObject("tsbExplorer.Image"), System.Drawing.Image)
    Me.tsbExplorer.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbExplorer.Name = "tsbExplorer"
    Me.tsbExplorer.Size = New System.Drawing.Size(69, 22)
    Me.tsbExplorer.Text = "Explorer"
    '
    'imlSearchResult
    '
    Me.imlSearchResult.ImageStream = CType(resources.GetObject("imlSearchResult.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.imlSearchResult.TransparentColor = System.Drawing.Color.Transparent
    Me.imlSearchResult.Images.SetKeyName(0, "SHELL32_4-16.png")
    Me.imlSearchResult.Images.SetKeyName(1, "rundll32_100-16.png")
    '
    'cmFavoriten
    '
    Me.cmFavoriten.Name = "cmFavoriten"
    Me.cmFavoriten.Size = New System.Drawing.Size(61, 4)
    '
    'frmTB_fileExplorer
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(338, 509)
    Me.Controls.Add(Me.SplitContainer3)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_fileExplorer"
    Me.Text = "Lokale Ordner"
    Me.SplitContainer3.Panel1.ResumeLayout(False)
    Me.SplitContainer3.Panel1.PerformLayout()
    Me.SplitContainer3.Panel2.ResumeLayout(False)
    Me.SplitContainer3.ResumeLayout(False)
    CType(Me.AxFolderTreeview2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnlSearch.ResumeLayout(False)
    Me.pnlSearch.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
  Friend WithEvents txtLocFolder As System.Windows.Forms.TextBox
  Friend WithEvents imlLocalFolder As System.Windows.Forms.ImageList
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbRename As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbExplorer As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbScriptDir As System.Windows.Forms.ToolStripButton
  Friend WithEvents AxFolderTreeview2 As AxCCRPFolderTV6.AxFolderTreeview
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents tsbDirUp As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbNewFile As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbExplore As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbView As System.Windows.Forms.ToolStripButton
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
  Friend WithEvents pnlSearch As System.Windows.Forms.Panel
  Friend WithEvents tspShowHide As System.Windows.Forms.ToolStripButton
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents txtSearchRoot As System.Windows.Forms.TextBox
  Friend WithEvents txtSearch As System.Windows.Forms.TextBox
  Friend WithEvents col_Pfad As System.Windows.Forms.ColumnHeader
  Friend WithEvents rbSearchEverywhere As System.Windows.Forms.RadioButton
  Friend WithEvents rbSearchLocal As System.Windows.Forms.RadioButton
  Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents imlSearchResult As System.Windows.Forms.ImageList
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents cmFavoriten As System.Windows.Forms.ContextMenuStrip
End Class
