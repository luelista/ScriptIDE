<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_ftpExplorer
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_ftpExplorer))
    Me.btnFtpUpload = New System.Windows.Forms.Button
    Me.btnFtpDownload = New System.Windows.Forms.Button
    Me.btnFtpDelete = New System.Windows.Forms.Button
    Me.btnFtpRenameFile = New System.Windows.Forms.Button
    Me.btnFtpCreateFile = New System.Windows.Forms.Button
    Me.btnSelFtpCon = New System.Windows.Forms.Button
    Me.btnFtpRefresh = New System.Windows.Forms.Button
    Me.txtFtpCurDir = New System.Windows.Forms.TextBox
    Me.imlIgrid = New System.Windows.Forms.ImageList(Me.components)
    Me.btnFtpUp = New System.Windows.Forms.Button
    Me.flpBookmarks = New System.Windows.Forms.FlowLayoutPanel
    Me.cmFtpFilelist = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.OeffnenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.SpeichernUnterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.UmbenennenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.LoeschenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
    Me.NeueDateiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.HochladenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
    Me.NameKopierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.PfadKopierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.AusschneidenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.EinfuegenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.AlleDateienCachenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
    Me.AufwaertsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ObersteEbeneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
    Me.btnNavFile = New System.Windows.Forms.Button
    Me.tvwFolders = New System.Windows.Forms.TreeView
    Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
    Me.pbIndicator = New System.Windows.Forms.PictureBox
    Me.chkShowTreeview = New System.Windows.Forms.CheckBox
    Me.chkUseFtpProxy = New System.Windows.Forms.CheckBox
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.cmFtpFilelist.SuspendLayout()
    Me.SplitContainer1.Panel1.SuspendLayout()
    Me.SplitContainer1.Panel2.SuspendLayout()
    Me.SplitContainer1.SuspendLayout()
    CType(Me.pbIndicator, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'btnFtpUpload
    '
    Me.btnFtpUpload.Image = CType(resources.GetObject("btnFtpUpload.Image"), System.Drawing.Image)
    Me.btnFtpUpload.Location = New System.Drawing.Point(181, 1)
    Me.btnFtpUpload.Name = "btnFtpUpload"
    Me.btnFtpUpload.Size = New System.Drawing.Size(25, 24)
    Me.btnFtpUpload.TabIndex = 13
    Me.ToolTip1.SetToolTip(Me.btnFtpUpload, "Datei hochladen")
    Me.btnFtpUpload.UseVisualStyleBackColor = True
    '
    'btnFtpDownload
    '
    Me.btnFtpDownload.Image = CType(resources.GetObject("btnFtpDownload.Image"), System.Drawing.Image)
    Me.btnFtpDownload.Location = New System.Drawing.Point(157, 1)
    Me.btnFtpDownload.Name = "btnFtpDownload"
    Me.btnFtpDownload.Size = New System.Drawing.Size(25, 24)
    Me.btnFtpDownload.TabIndex = 12
    Me.ToolTip1.SetToolTip(Me.btnFtpDownload, "Herunterladen")
    Me.btnFtpDownload.UseVisualStyleBackColor = True
    '
    'btnFtpDelete
    '
    Me.btnFtpDelete.Image = CType(resources.GetObject("btnFtpDelete.Image"), System.Drawing.Image)
    Me.btnFtpDelete.Location = New System.Drawing.Point(129, 1)
    Me.btnFtpDelete.Name = "btnFtpDelete"
    Me.btnFtpDelete.Size = New System.Drawing.Size(25, 24)
    Me.btnFtpDelete.TabIndex = 14
    Me.ToolTip1.SetToolTip(Me.btnFtpDelete, "Löschen")
    Me.btnFtpDelete.UseVisualStyleBackColor = True
    '
    'btnFtpRenameFile
    '
    Me.btnFtpRenameFile.Image = CType(resources.GetObject("btnFtpRenameFile.Image"), System.Drawing.Image)
    Me.btnFtpRenameFile.Location = New System.Drawing.Point(105, 1)
    Me.btnFtpRenameFile.Name = "btnFtpRenameFile"
    Me.btnFtpRenameFile.Size = New System.Drawing.Size(25, 24)
    Me.btnFtpRenameFile.TabIndex = 11
    Me.ToolTip1.SetToolTip(Me.btnFtpRenameFile, "Umbenennen")
    Me.btnFtpRenameFile.UseVisualStyleBackColor = True
    '
    'btnFtpCreateFile
    '
    Me.btnFtpCreateFile.Image = CType(resources.GetObject("btnFtpCreateFile.Image"), System.Drawing.Image)
    Me.btnFtpCreateFile.Location = New System.Drawing.Point(81, 1)
    Me.btnFtpCreateFile.Name = "btnFtpCreateFile"
    Me.btnFtpCreateFile.Size = New System.Drawing.Size(25, 24)
    Me.btnFtpCreateFile.TabIndex = 10
    Me.ToolTip1.SetToolTip(Me.btnFtpCreateFile, "Neue Datei/Ordner erstellen")
    Me.btnFtpCreateFile.UseVisualStyleBackColor = True
    '
    'btnSelFtpCon
    '
    Me.btnSelFtpCon.Image = CType(resources.GetObject("btnSelFtpCon.Image"), System.Drawing.Image)
    Me.btnSelFtpCon.Location = New System.Drawing.Point(1, 1)
    Me.btnSelFtpCon.Name = "btnSelFtpCon"
    Me.btnSelFtpCon.Size = New System.Drawing.Size(25, 24)
    Me.btnSelFtpCon.TabIndex = 7
    Me.ToolTip1.SetToolTip(Me.btnSelFtpCon, "Verbindung")
    Me.btnSelFtpCon.UseVisualStyleBackColor = True
    '
    'btnFtpRefresh
    '
    Me.btnFtpRefresh.Image = CType(resources.GetObject("btnFtpRefresh.Image"), System.Drawing.Image)
    Me.btnFtpRefresh.Location = New System.Drawing.Point(53, 1)
    Me.btnFtpRefresh.Name = "btnFtpRefresh"
    Me.btnFtpRefresh.Size = New System.Drawing.Size(25, 24)
    Me.btnFtpRefresh.TabIndex = 6
    Me.ToolTip1.SetToolTip(Me.btnFtpRefresh, "Aktualisieren")
    Me.btnFtpRefresh.UseVisualStyleBackColor = True
    '
    'txtFtpCurDir
    '
    Me.txtFtpCurDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFtpCurDir.Location = New System.Drawing.Point(1, 28)
    Me.txtFtpCurDir.Name = "txtFtpCurDir"
    Me.txtFtpCurDir.Size = New System.Drawing.Size(303, 20)
    Me.txtFtpCurDir.TabIndex = 4
    '
    'imlIgrid
    '
    Me.imlIgrid.ImageStream = CType(resources.GetObject("imlIgrid.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.imlIgrid.TransparentColor = System.Drawing.Color.Transparent
    Me.imlIgrid.Images.SetKeyName(0, "folder")
    Me.imlIgrid.Images.SetKeyName(1, "folderup")
    '
    'btnFtpUp
    '
    Me.btnFtpUp.Image = CType(resources.GetObject("btnFtpUp.Image"), System.Drawing.Image)
    Me.btnFtpUp.Location = New System.Drawing.Point(29, 1)
    Me.btnFtpUp.Name = "btnFtpUp"
    Me.btnFtpUp.Size = New System.Drawing.Size(25, 24)
    Me.btnFtpUp.TabIndex = 0
    Me.ToolTip1.SetToolTip(Me.btnFtpUp, "Aufwärts")
    Me.btnFtpUp.UseVisualStyleBackColor = True
    '
    'flpBookmarks
    '
    Me.flpBookmarks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.flpBookmarks.Location = New System.Drawing.Point(0, 470)
    Me.flpBookmarks.Name = "flpBookmarks"
    Me.flpBookmarks.Size = New System.Drawing.Size(304, 29)
    Me.flpBookmarks.TabIndex = 8
    '
    'cmFtpFilelist
    '
    Me.cmFtpFilelist.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OeffnenToolStripMenuItem, Me.SpeichernUnterToolStripMenuItem, Me.UmbenennenToolStripMenuItem, Me.LoeschenToolStripMenuItem, Me.ToolStripMenuItem1, Me.NeueDateiToolStripMenuItem, Me.HochladenToolStripMenuItem, Me.ToolStripMenuItem3, Me.NameKopierenToolStripMenuItem, Me.PfadKopierenToolStripMenuItem, Me.AusschneidenToolStripMenuItem, Me.EinfuegenToolStripMenuItem, Me.AlleDateienCachenToolStripMenuItem, Me.ToolStripMenuItem2, Me.AufwaertsToolStripMenuItem, Me.ObersteEbeneToolStripMenuItem})
    Me.cmFtpFilelist.Name = "cmFtpFilelist"
    Me.cmFtpFilelist.Size = New System.Drawing.Size(179, 308)
    '
    'OeffnenToolStripMenuItem
    '
    Me.OeffnenToolStripMenuItem.Name = "OeffnenToolStripMenuItem"
    Me.OeffnenToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.OeffnenToolStripMenuItem.Text = "Öffnen"
    '
    'SpeichernUnterToolStripMenuItem
    '
    Me.SpeichernUnterToolStripMenuItem.Image = CType(resources.GetObject("SpeichernUnterToolStripMenuItem.Image"), System.Drawing.Image)
    Me.SpeichernUnterToolStripMenuItem.Name = "SpeichernUnterToolStripMenuItem"
    Me.SpeichernUnterToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.SpeichernUnterToolStripMenuItem.Text = "Speichern unter ..."
    '
    'UmbenennenToolStripMenuItem
    '
    Me.UmbenennenToolStripMenuItem.Image = CType(resources.GetObject("UmbenennenToolStripMenuItem.Image"), System.Drawing.Image)
    Me.UmbenennenToolStripMenuItem.Name = "UmbenennenToolStripMenuItem"
    Me.UmbenennenToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.UmbenennenToolStripMenuItem.Text = "Umbenennen"
    '
    'LoeschenToolStripMenuItem
    '
    Me.LoeschenToolStripMenuItem.Image = CType(resources.GetObject("LoeschenToolStripMenuItem.Image"), System.Drawing.Image)
    Me.LoeschenToolStripMenuItem.Name = "LoeschenToolStripMenuItem"
    Me.LoeschenToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.LoeschenToolStripMenuItem.Text = "Löschen"
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(175, 6)
    '
    'NeueDateiToolStripMenuItem
    '
    Me.NeueDateiToolStripMenuItem.Image = CType(resources.GetObject("NeueDateiToolStripMenuItem.Image"), System.Drawing.Image)
    Me.NeueDateiToolStripMenuItem.Name = "NeueDateiToolStripMenuItem"
    Me.NeueDateiToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.NeueDateiToolStripMenuItem.Text = "Neues Element ..."
    '
    'HochladenToolStripMenuItem
    '
    Me.HochladenToolStripMenuItem.Image = CType(resources.GetObject("HochladenToolStripMenuItem.Image"), System.Drawing.Image)
    Me.HochladenToolStripMenuItem.Name = "HochladenToolStripMenuItem"
    Me.HochladenToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.HochladenToolStripMenuItem.Text = "Hochladen ..."
    '
    'ToolStripMenuItem3
    '
    Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
    Me.ToolStripMenuItem3.Size = New System.Drawing.Size(175, 6)
    '
    'NameKopierenToolStripMenuItem
    '
    Me.NameKopierenToolStripMenuItem.Name = "NameKopierenToolStripMenuItem"
    Me.NameKopierenToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.NameKopierenToolStripMenuItem.Text = "Name kopieren"
    '
    'PfadKopierenToolStripMenuItem
    '
    Me.PfadKopierenToolStripMenuItem.Name = "PfadKopierenToolStripMenuItem"
    Me.PfadKopierenToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.PfadKopierenToolStripMenuItem.Text = "Pfad kopieren"
    '
    'AusschneidenToolStripMenuItem
    '
    Me.AusschneidenToolStripMenuItem.Image = CType(resources.GetObject("AusschneidenToolStripMenuItem.Image"), System.Drawing.Image)
    Me.AusschneidenToolStripMenuItem.Name = "AusschneidenToolStripMenuItem"
    Me.AusschneidenToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.AusschneidenToolStripMenuItem.Text = "Ausschneiden"
    '
    'EinfuegenToolStripMenuItem
    '
    Me.EinfuegenToolStripMenuItem.Enabled = False
    Me.EinfuegenToolStripMenuItem.Name = "EinfuegenToolStripMenuItem"
    Me.EinfuegenToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.EinfuegenToolStripMenuItem.Text = "Einfügen"
    '
    'AlleDateienCachenToolStripMenuItem
    '
    Me.AlleDateienCachenToolStripMenuItem.Name = "AlleDateienCachenToolStripMenuItem"
    Me.AlleDateienCachenToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.AlleDateienCachenToolStripMenuItem.Text = "Alle Dateien cachen"
    '
    'ToolStripMenuItem2
    '
    Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
    Me.ToolStripMenuItem2.Size = New System.Drawing.Size(175, 6)
    '
    'AufwaertsToolStripMenuItem
    '
    Me.AufwaertsToolStripMenuItem.Image = CType(resources.GetObject("AufwaertsToolStripMenuItem.Image"), System.Drawing.Image)
    Me.AufwaertsToolStripMenuItem.Name = "AufwaertsToolStripMenuItem"
    Me.AufwaertsToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.AufwaertsToolStripMenuItem.Text = "Aufwärts"
    '
    'ObersteEbeneToolStripMenuItem
    '
    Me.ObersteEbeneToolStripMenuItem.Image = CType(resources.GetObject("ObersteEbeneToolStripMenuItem.Image"), System.Drawing.Image)
    Me.ObersteEbeneToolStripMenuItem.Name = "ObersteEbeneToolStripMenuItem"
    Me.ObersteEbeneToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
    Me.ObersteEbeneToolStripMenuItem.Text = "oberste Ebene"
    '
    'ListView1
    '
    Me.ListView1.AllowDrop = True
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
    Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ListView1.Location = New System.Drawing.Point(0, 0)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(303, 266)
    Me.ListView1.SmallImageList = Me.imlIgrid
    Me.ListView1.TabIndex = 15
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "Name"
    Me.ColumnHeader1.Width = 215
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Size"
    '
    'ColumnHeader3
    '
    Me.ColumnHeader3.Text = "Perms"
    '
    'ColumnHeader4
    '
    Me.ColumnHeader4.Text = "Owner"
    '
    'ColumnHeader5
    '
    Me.ColumnHeader5.Text = "LastMod"
    '
    'btnNavFile
    '
    Me.btnNavFile.Image = CType(resources.GetObject("btnNavFile.Image"), System.Drawing.Image)
    Me.btnNavFile.Location = New System.Drawing.Point(266, 1)
    Me.btnNavFile.Name = "btnNavFile"
    Me.btnNavFile.Size = New System.Drawing.Size(23, 24)
    Me.btnNavFile.TabIndex = 17
    Me.ToolTip1.SetToolTip(Me.btnNavFile, "Gehe zu URL")
    Me.btnNavFile.UseVisualStyleBackColor = True
    '
    'tvwFolders
    '
    Me.tvwFolders.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tvwFolders.HideSelection = False
    Me.tvwFolders.ImageIndex = 0
    Me.tvwFolders.ImageList = Me.imlIgrid
    Me.tvwFolders.Location = New System.Drawing.Point(0, 0)
    Me.tvwFolders.Name = "tvwFolders"
    Me.tvwFolders.PathSeparator = "/"
    Me.tvwFolders.SelectedImageIndex = 0
    Me.tvwFolders.Size = New System.Drawing.Size(303, 147)
    Me.tvwFolders.TabIndex = 18
    '
    'SplitContainer1
    '
    Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SplitContainer1.Location = New System.Drawing.Point(1, 52)
    Me.SplitContainer1.Name = "SplitContainer1"
    Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplitContainer1.Panel1
    '
    Me.SplitContainer1.Panel1.Controls.Add(Me.tvwFolders)
    '
    'SplitContainer1.Panel2
    '
    Me.SplitContainer1.Panel2.Controls.Add(Me.pbIndicator)
    Me.SplitContainer1.Panel2.Controls.Add(Me.ListView1)
    Me.SplitContainer1.Size = New System.Drawing.Size(303, 417)
    Me.SplitContainer1.SplitterDistance = 147
    Me.SplitContainer1.TabIndex = 19
    '
    'pbIndicator
    '
    Me.pbIndicator.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.pbIndicator.BackColor = System.Drawing.Color.White
    Me.pbIndicator.Image = CType(resources.GetObject("pbIndicator.Image"), System.Drawing.Image)
    Me.pbIndicator.Location = New System.Drawing.Point(135, 114)
    Me.pbIndicator.Name = "pbIndicator"
    Me.pbIndicator.Size = New System.Drawing.Size(32, 32)
    Me.pbIndicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.pbIndicator.TabIndex = 16
    Me.pbIndicator.TabStop = False
    Me.pbIndicator.Visible = False
    '
    'chkShowTreeview
    '
    Me.chkShowTreeview.Appearance = System.Windows.Forms.Appearance.Button
    Me.chkShowTreeview.Checked = True
    Me.chkShowTreeview.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkShowTreeview.Image = CType(resources.GetObject("chkShowTreeview.Image"), System.Drawing.Image)
    Me.chkShowTreeview.Location = New System.Drawing.Point(208, 1)
    Me.chkShowTreeview.Name = "chkShowTreeview"
    Me.chkShowTreeview.Size = New System.Drawing.Size(25, 24)
    Me.chkShowTreeview.TabIndex = 20
    Me.ToolTip1.SetToolTip(Me.chkShowTreeview, "TreeView anzeigen")
    Me.chkShowTreeview.UseVisualStyleBackColor = True
    '
    'chkUseFtpProxy
    '
    Me.chkUseFtpProxy.Appearance = System.Windows.Forms.Appearance.Button
    Me.chkUseFtpProxy.Image = CType(resources.GetObject("chkUseFtpProxy.Image"), System.Drawing.Image)
    Me.chkUseFtpProxy.Location = New System.Drawing.Point(235, 1)
    Me.chkUseFtpProxy.Name = "chkUseFtpProxy"
    Me.chkUseFtpProxy.Size = New System.Drawing.Size(25, 24)
    Me.chkUseFtpProxy.TabIndex = 21
    Me.ToolTip1.SetToolTip(Me.chkUseFtpProxy, "FTP-Proxy nutzen")
    Me.chkUseFtpProxy.UseVisualStyleBackColor = True
    '
    'frmTB_ftpExplorer
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(304, 499)
    Me.Controls.Add(Me.chkUseFtpProxy)
    Me.Controls.Add(Me.chkShowTreeview)
    Me.Controls.Add(Me.btnFtpRefresh)
    Me.Controls.Add(Me.btnNavFile)
    Me.Controls.Add(Me.btnFtpUpload)
    Me.Controls.Add(Me.btnFtpDownload)
    Me.Controls.Add(Me.btnFtpUp)
    Me.Controls.Add(Me.btnFtpDelete)
    Me.Controls.Add(Me.flpBookmarks)
    Me.Controls.Add(Me.btnFtpRenameFile)
    Me.Controls.Add(Me.btnFtpCreateFile)
    Me.Controls.Add(Me.txtFtpCurDir)
    Me.Controls.Add(Me.btnSelFtpCon)
    Me.Controls.Add(Me.SplitContainer1)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_ftpExplorer"
    Me.Text = "FTP-Explorer"
    Me.cmFtpFilelist.ResumeLayout(False)
    Me.SplitContainer1.Panel1.ResumeLayout(False)
    Me.SplitContainer1.Panel2.ResumeLayout(False)
    Me.SplitContainer1.Panel2.PerformLayout()
    Me.SplitContainer1.ResumeLayout(False)
    CType(Me.pbIndicator, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents btnFtpUpload As System.Windows.Forms.Button
  Friend WithEvents btnFtpDownload As System.Windows.Forms.Button
  Friend WithEvents btnFtpDelete As System.Windows.Forms.Button
  Friend WithEvents btnFtpRenameFile As System.Windows.Forms.Button
  Friend WithEvents btnFtpCreateFile As System.Windows.Forms.Button
  Friend WithEvents btnSelFtpCon As System.Windows.Forms.Button
  Friend WithEvents btnFtpRefresh As System.Windows.Forms.Button
  Friend WithEvents txtFtpCurDir As System.Windows.Forms.TextBox
  Friend WithEvents btnFtpUp As System.Windows.Forms.Button
  Friend WithEvents flpBookmarks As System.Windows.Forms.FlowLayoutPanel
  Friend WithEvents cmFtpFilelist As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents OeffnenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SpeichernUnterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents UmbenennenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents LoeschenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents NeueDateiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents HochladenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents NameKopierenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents PfadKopierenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AusschneidenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents EinfuegenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents AlleDateienCachenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents AufwaertsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ObersteEbeneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents imlIgrid As System.Windows.Forms.ImageList
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnNavFile As System.Windows.Forms.Button
  Friend WithEvents tvwFolders As System.Windows.Forms.TreeView
  Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents chkShowTreeview As System.Windows.Forms.CheckBox
  Friend WithEvents pbIndicator As System.Windows.Forms.PictureBox
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
  Friend WithEvents chkUseFtpProxy As System.Windows.Forms.CheckBox
  Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
