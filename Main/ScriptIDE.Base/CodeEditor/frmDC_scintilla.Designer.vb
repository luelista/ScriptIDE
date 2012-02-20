<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDC_scintilla
  Inherits DockContent

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDC_scintilla))
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.DumpVarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
    Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
    Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.tsIntellibar = New System.Windows.Forms.ToolStrip
    Me.tsbSave = New System.Windows.Forms.ToolStripButton
    Me.lstIntellisense = New System.Windows.Forms.ListBox
    Me.pnlLoadIndicator = New System.Windows.Forms.PictureBox
    Me.bwReadSave = New System.ComponentModel.BackgroundWorker
    Me.RichTextPlus1 = New ScriptIDE.ScriptWindowHelper.RichTextPlus
    Me.ContextMenuStrip1.SuspendLayout()
    Me.tsIntellibar.SuspendLayout()
    CType(Me.pnlLoadIndicator, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DumpVarToolStripMenuItem, Me.ToolStripMenuItem1, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ToolStripMenuItem2, Me.SelectAllToolStripMenuItem})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(128, 126)
    '
    'DumpVarToolStripMenuItem
    '
    Me.DumpVarToolStripMenuItem.Name = "DumpVarToolStripMenuItem"
    Me.DumpVarToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    Me.DumpVarToolStripMenuItem.Text = "Dump Var"
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(124, 6)
    '
    'CutToolStripMenuItem
    '
    Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
    Me.CutToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    Me.CutToolStripMenuItem.Text = "Cut"
    '
    'CopyToolStripMenuItem
    '
    Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
    Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    Me.CopyToolStripMenuItem.Text = "Copy"
    '
    'PasteToolStripMenuItem
    '
    Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
    Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    Me.PasteToolStripMenuItem.Text = "Paste"
    '
    'ToolStripMenuItem2
    '
    Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
    Me.ToolStripMenuItem2.Size = New System.Drawing.Size(124, 6)
    '
    'SelectAllToolStripMenuItem
    '
    Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
    Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
    Me.SelectAllToolStripMenuItem.Text = "Select All"
    '
    'tsIntellibar
    '
    Me.tsIntellibar.Dock = System.Windows.Forms.DockStyle.None
    Me.tsIntellibar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave})
    Me.tsIntellibar.Location = New System.Drawing.Point(0, 0)
    Me.tsIntellibar.Name = "tsIntellibar"
    Me.tsIntellibar.Size = New System.Drawing.Size(91, 25)
    Me.tsIntellibar.TabIndex = 1
    Me.tsIntellibar.Text = "ToolStrip1"
    '
    'tsbSave
    '
    Me.tsbSave.Image = CType(resources.GetObject("tsbSave.Image"), System.Drawing.Image)
    Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbSave.Name = "tsbSave"
    Me.tsbSave.Size = New System.Drawing.Size(79, 22)
    Me.tsbSave.Text = "Speichern"
    '
    'lstIntellisense
    '
    Me.lstIntellisense.FormattingEnabled = True
    Me.lstIntellisense.Location = New System.Drawing.Point(62, 65)
    Me.lstIntellisense.Name = "lstIntellisense"
    Me.lstIntellisense.Size = New System.Drawing.Size(154, 290)
    Me.lstIntellisense.TabIndex = 2
    Me.lstIntellisense.Visible = False
    '
    'pnlLoadIndicator
    '
    Me.pnlLoadIndicator.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.pnlLoadIndicator.BackColor = System.Drawing.Color.White
    Me.pnlLoadIndicator.Image = CType(resources.GetObject("pnlLoadIndicator.Image"), System.Drawing.Image)
    Me.pnlLoadIndicator.Location = New System.Drawing.Point(192, 157)
    Me.pnlLoadIndicator.Name = "pnlLoadIndicator"
    Me.pnlLoadIndicator.Size = New System.Drawing.Size(118, 88)
    Me.pnlLoadIndicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
    Me.pnlLoadIndicator.TabIndex = 4
    Me.pnlLoadIndicator.TabStop = False
    '
    'bwReadSave
    '
    Me.bwReadSave.WorkerSupportsCancellation = True
    '
    'RichTextPlus1
    '
    Me.RichTextPlus1.BackColor = System.Drawing.SystemColors.Info
    Me.RichTextPlus1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.RichTextPlus1.Cursor = System.Windows.Forms.Cursors.Default
    Me.RichTextPlus1.DetectUrls = False
    Me.RichTextPlus1.ForeColor = System.Drawing.SystemColors.InfoText
    Me.RichTextPlus1.HTMLCode = "ich bin <b>ein Info-Tip</b>!"
    Me.RichTextPlus1.Location = New System.Drawing.Point(127, 69)
    Me.RichTextPlus1.Name = "RichTextPlus1"
    Me.RichTextPlus1.ReadOnly = True
    Me.RichTextPlus1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
    Me.RichTextPlus1.Size = New System.Drawing.Size(336, 67)
    Me.RichTextPlus1.TabIndex = 3
    Me.RichTextPlus1.Text = "ich bin ein Info-Tip!"
    Me.RichTextPlus1.Visible = False
    Me.RichTextPlus1.WordWrap = False
    '
    'frmDC_scintilla
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(503, 402)
    Me.Controls.Add(Me.pnlLoadIndicator)
    Me.Controls.Add(Me.RichTextPlus1)
    Me.Controls.Add(Me.lstIntellisense)
    Me.Controls.Add(Me.tsIntellibar)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "frmDC_scintilla"
    Me.Text = "SCINTILLA"
    Me.ContextMenuStrip1.ResumeLayout(False)
    Me.tsIntellibar.ResumeLayout(False)
    Me.tsIntellibar.PerformLayout()
    CType(Me.pnlLoadIndicator, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents DumpVarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents tsIntellibar As System.Windows.Forms.ToolStrip
  Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
  Friend WithEvents lstIntellisense As System.Windows.Forms.ListBox
  Friend WithEvents RichTextPlus1 As ScriptIDE.ScriptWindowHelper.RichTextPlus
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents pnlLoadIndicator As System.Windows.Forms.PictureBox
  Friend WithEvents bwReadSave As System.ComponentModel.BackgroundWorker
End Class
