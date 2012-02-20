<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_solutionExplorer
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_solutionExplorer))
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
    Me.tsbNewProject = New System.Windows.Forms.ToolStripButton
    Me.tsbLoadProject = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.tsbAddFile = New System.Windows.Forms.ToolStripButton
    Me.tsbExplorer = New System.Windows.Forms.ToolStripButton
    Me.TreeView1 = New System.Windows.Forms.TreeView
    Me.cmProject = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.UmbenennenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ProjektEntladenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.AlleDateienÖffnenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.NeuerOrdnerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.cmFile = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.ExcludeFromPrjToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.cmProjectUnloaded = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.ProjektLadenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ProjektdateiBearbeitenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.AusListeEntfernenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.cmFolder = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.Folder_UmbenennenToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
    Me.Folder_LoeschenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.Folder_NeuerOrdnerToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStrip1.SuspendLayout()
    Me.cmProject.SuspendLayout()
    Me.cmFile.SuspendLayout()
    Me.cmProjectUnloaded.SuspendLayout()
    Me.cmFolder.SuspendLayout()
    Me.SuspendLayout()
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList1.Images.SetKeyName(0, "VB6_1210.ico")
    Me.ImageList1.Images.SetKeyName(1, "VB6_1251.ico")
    Me.ImageList1.Images.SetKeyName(2, "Projectx")
    Me.ImageList1.Images.SetKeyName(3, "DefaultFile")
    Me.ImageList1.Images.SetKeyName(4, "Folder")
    Me.ImageList1.Images.SetKeyName(5, "Projectx2")
    Me.ImageList1.Images.SetKeyName(6, "Project")
    Me.ImageList1.Images.SetKeyName(7, "UnloadedProject")
    '
    'ToolStrip1
    '
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNewProject, Me.tsbLoadProject, Me.ToolStripSeparator1, Me.tsbAddFile, Me.tsbExplorer})
    Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(222, 25)
    Me.ToolStrip1.TabIndex = 0
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'tsbNewProject
    '
    Me.tsbNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbNewProject.Image = CType(resources.GetObject("tsbNewProject.Image"), System.Drawing.Image)
    Me.tsbNewProject.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbNewProject.Name = "tsbNewProject"
    Me.tsbNewProject.Size = New System.Drawing.Size(23, 22)
    Me.tsbNewProject.Text = "Neu"
    '
    'tsbLoadProject
    '
    Me.tsbLoadProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbLoadProject.Image = CType(resources.GetObject("tsbLoadProject.Image"), System.Drawing.Image)
    Me.tsbLoadProject.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbLoadProject.Name = "tsbLoadProject"
    Me.tsbLoadProject.Size = New System.Drawing.Size(23, 22)
    Me.tsbLoadProject.Text = "Projekt laden ..."
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
    '
    'tsbAddFile
    '
    Me.tsbAddFile.Image = CType(resources.GetObject("tsbAddFile.Image"), System.Drawing.Image)
    Me.tsbAddFile.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbAddFile.Name = "tsbAddFile"
    Me.tsbAddFile.Size = New System.Drawing.Size(89, 22)
    Me.tsbAddFile.Text = "Hinzufügen"
    Me.tsbAddFile.ToolTipText = "Aktuelle Datei hinzufügen"
    '
    'tsbExplorer
    '
    Me.tsbExplorer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbExplorer.Image = CType(resources.GetObject("tsbExplorer.Image"), System.Drawing.Image)
    Me.tsbExplorer.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbExplorer.Name = "tsbExplorer"
    Me.tsbExplorer.Size = New System.Drawing.Size(23, 22)
    Me.tsbExplorer.Text = "Explorer öffnen"
    '
    'TreeView1
    '
    Me.TreeView1.AllowDrop = True
    Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeView1.HideSelection = False
    Me.TreeView1.ImageIndex = 0
    Me.TreeView1.ImageList = Me.ImageList1
    Me.TreeView1.LabelEdit = True
    Me.TreeView1.Location = New System.Drawing.Point(0, 25)
    Me.TreeView1.Name = "TreeView1"
    Me.TreeView1.SelectedImageIndex = 0
    Me.TreeView1.Size = New System.Drawing.Size(222, 473)
    Me.TreeView1.TabIndex = 1
    '
    'cmProject
    '
    Me.cmProject.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UmbenennenToolStripMenuItem, Me.ProjektEntladenToolStripMenuItem, Me.AlleDateienÖffnenToolStripMenuItem, Me.NeuerOrdnerToolStripMenuItem})
    Me.cmProject.Name = "cmProject"
    Me.cmProject.Size = New System.Drawing.Size(176, 92)
    '
    'UmbenennenToolStripMenuItem
    '
    Me.UmbenennenToolStripMenuItem.Name = "UmbenennenToolStripMenuItem"
    Me.UmbenennenToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.UmbenennenToolStripMenuItem.Text = "Umbenennen"
    '
    'ProjektEntladenToolStripMenuItem
    '
    Me.ProjektEntladenToolStripMenuItem.Name = "ProjektEntladenToolStripMenuItem"
    Me.ProjektEntladenToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.ProjektEntladenToolStripMenuItem.Text = "Projekt entladen"
    '
    'AlleDateienÖffnenToolStripMenuItem
    '
    Me.AlleDateienÖffnenToolStripMenuItem.Name = "AlleDateienÖffnenToolStripMenuItem"
    Me.AlleDateienÖffnenToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.AlleDateienÖffnenToolStripMenuItem.Text = "Alle Dateien öffnen"
    '
    'NeuerOrdnerToolStripMenuItem
    '
    Me.NeuerOrdnerToolStripMenuItem.Name = "NeuerOrdnerToolStripMenuItem"
    Me.NeuerOrdnerToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
    Me.NeuerOrdnerToolStripMenuItem.Text = "Neuer Ordner ..."
    '
    'cmFile
    '
    Me.cmFile.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcludeFromPrjToolStripMenuItem})
    Me.cmFile.Name = "cmFile"
    Me.cmFile.Size = New System.Drawing.Size(206, 26)
    '
    'ExcludeFromPrjToolStripMenuItem
    '
    Me.ExcludeFromPrjToolStripMenuItem.Name = "ExcludeFromPrjToolStripMenuItem"
    Me.ExcludeFromPrjToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
    Me.ExcludeFromPrjToolStripMenuItem.Text = "Aus Projekt ausschließen"
    '
    'cmProjectUnloaded
    '
    Me.cmProjectUnloaded.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProjektLadenToolStripMenuItem, Me.ProjektdateiBearbeitenToolStripMenuItem, Me.AusListeEntfernenToolStripMenuItem})
    Me.cmProjectUnloaded.Name = "cmProject"
    Me.cmProjectUnloaded.Size = New System.Drawing.Size(197, 70)
    '
    'ProjektLadenToolStripMenuItem
    '
    Me.ProjektLadenToolStripMenuItem.Name = "ProjektLadenToolStripMenuItem"
    Me.ProjektLadenToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
    Me.ProjektLadenToolStripMenuItem.Text = "Projekt laden"
    '
    'ProjektdateiBearbeitenToolStripMenuItem
    '
    Me.ProjektdateiBearbeitenToolStripMenuItem.Name = "ProjektdateiBearbeitenToolStripMenuItem"
    Me.ProjektdateiBearbeitenToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
    Me.ProjektdateiBearbeitenToolStripMenuItem.Text = "Projektdatei bearbeiten"
    '
    'AusListeEntfernenToolStripMenuItem
    '
    Me.AusListeEntfernenToolStripMenuItem.Name = "AusListeEntfernenToolStripMenuItem"
    Me.AusListeEntfernenToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
    Me.AusListeEntfernenToolStripMenuItem.Text = "Aus Liste entfernen"
    '
    'cmFolder
    '
    Me.cmFolder.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Folder_UmbenennenToolStripMenuItem1, Me.Folder_LoeschenToolStripMenuItem, Me.Folder_NeuerOrdnerToolStripMenuItem1})
    Me.cmFolder.Name = "cmProject"
    Me.cmFolder.Size = New System.Drawing.Size(159, 70)
    '
    'Folder_UmbenennenToolStripMenuItem1
    '
    Me.Folder_UmbenennenToolStripMenuItem1.Name = "Folder_UmbenennenToolStripMenuItem1"
    Me.Folder_UmbenennenToolStripMenuItem1.Size = New System.Drawing.Size(158, 22)
    Me.Folder_UmbenennenToolStripMenuItem1.Text = "Umbenennen"
    '
    'Folder_LoeschenToolStripMenuItem
    '
    Me.Folder_LoeschenToolStripMenuItem.Name = "Folder_LoeschenToolStripMenuItem"
    Me.Folder_LoeschenToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
    Me.Folder_LoeschenToolStripMenuItem.Text = "Löschen"
    '
    'Folder_NeuerOrdnerToolStripMenuItem1
    '
    Me.Folder_NeuerOrdnerToolStripMenuItem1.Name = "Folder_NeuerOrdnerToolStripMenuItem1"
    Me.Folder_NeuerOrdnerToolStripMenuItem1.Size = New System.Drawing.Size(158, 22)
    Me.Folder_NeuerOrdnerToolStripMenuItem1.Text = "Neuer Ordner ..."
    '
    'frmTB_solutionExplorer
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(222, 498)
    Me.Controls.Add(Me.TreeView1)
    Me.Controls.Add(Me.ToolStrip1)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_solutionExplorer"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
    Me.Text = "Solution Explorer"
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.cmProject.ResumeLayout(False)
    Me.cmFile.ResumeLayout(False)
    Me.cmProjectUnloaded.ResumeLayout(False)
    Me.cmFolder.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents tsbLoadProject As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbExplorer As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbAddFile As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbNewProject As System.Windows.Forms.ToolStripButton
  Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
  Friend WithEvents cmProject As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents UmbenennenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ProjektEntladenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AlleDateienÖffnenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents cmFile As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents cmProjectUnloaded As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents ProjektLadenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ProjektdateiBearbeitenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ExcludeFromPrjToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AusListeEntfernenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents NeuerOrdnerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents cmFolder As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents Folder_NeuerOrdnerToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents Folder_UmbenennenToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents Folder_LoeschenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
