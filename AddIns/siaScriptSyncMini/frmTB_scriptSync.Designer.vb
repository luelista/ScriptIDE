<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_scriptSync
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_scriptSync))
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
    Me.tsbRefresh = New System.Windows.Forms.ToolStripButton
    Me.tsbLogin = New System.Windows.Forms.ToolStripButton
    Me.tsbUploadScript = New System.Windows.Forms.ToolStripButton
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.SpeichernUnterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.DownloadNachToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.LoeschenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
    Me.SpeichernInCyParascriptIDE4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
    Me.flpBookmarks = New System.Windows.Forms.FlowLayoutPanel
    Me.ToolStrip1.SuspendLayout()
    Me.ContextMenuStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'ListView1
    '
    Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
    Me.ListView1.Location = New System.Drawing.Point(2, 44)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(244, 385)
    Me.ListView1.SmallImageList = Me.ImageList1
    Me.ListView1.TabIndex = 0
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "Scriptname"
    Me.ColumnHeader1.Width = 134
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Von"
    Me.ColumnHeader2.Width = 43
    '
    'ColumnHeader3
    '
    Me.ColumnHeader3.Text = "LastMod"
    Me.ColumnHeader3.Width = 84
    '
    'ImageList1
    '
    Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
    Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'ToolStrip1
    '
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbRefresh, Me.tsbLogin, Me.tsbUploadScript})
    Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(249, 25)
    Me.ToolStrip1.TabIndex = 1
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'tsbRefresh
    '
    Me.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbRefresh.Image = CType(resources.GetObject("tsbRefresh.Image"), System.Drawing.Image)
    Me.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbRefresh.Name = "tsbRefresh"
    Me.tsbRefresh.Size = New System.Drawing.Size(23, 22)
    Me.tsbRefresh.Text = "Aktualisieren"
    '
    'tsbLogin
    '
    Me.tsbLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbLogin.Image = CType(resources.GetObject("tsbLogin.Image"), System.Drawing.Image)
    Me.tsbLogin.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbLogin.Name = "tsbLogin"
    Me.tsbLogin.Size = New System.Drawing.Size(23, 22)
    Me.tsbLogin.Text = "Login/Logout"
    '
    'tsbUploadScript
    '
    Me.tsbUploadScript.Image = CType(resources.GetObject("tsbUploadScript.Image"), System.Drawing.Image)
    Me.tsbUploadScript.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbUploadScript.Name = "tsbUploadScript"
    Me.tsbUploadScript.Size = New System.Drawing.Size(110, 22)
    Me.tsbUploadScript.Text = "Skript uploaden"
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SpeichernUnterToolStripMenuItem, Me.DownloadNachToolStripMenuItem, Me.LoeschenToolStripMenuItem, Me.ToolStripMenuItem1, Me.SpeichernInCyParascriptIDE4ToolStripMenuItem})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(259, 98)
    '
    'SpeichernUnterToolStripMenuItem
    '
    Me.SpeichernUnterToolStripMenuItem.Name = "SpeichernUnterToolStripMenuItem"
    Me.SpeichernUnterToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
    Me.SpeichernUnterToolStripMenuItem.Text = "Speichern unter ..."
    '
    'DownloadNachToolStripMenuItem
    '
    Me.DownloadNachToolStripMenuItem.Name = "DownloadNachToolStripMenuItem"
    Me.DownloadNachToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
    Me.DownloadNachToolStripMenuItem.Text = "Download nach: "
    Me.DownloadNachToolStripMenuItem.Visible = False
    '
    'LoeschenToolStripMenuItem
    '
    Me.LoeschenToolStripMenuItem.Name = "LoeschenToolStripMenuItem"
    Me.LoeschenToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
    Me.LoeschenToolStripMenuItem.Text = "Löschen"
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(255, 6)
    '
    'SpeichernInCyParascriptIDE4ToolStripMenuItem
    '
    Me.SpeichernInCyParascriptIDE4ToolStripMenuItem.Enabled = False
    Me.SpeichernInCyParascriptIDE4ToolStripMenuItem.Name = "SpeichernInCyParascriptIDE4ToolStripMenuItem"
    Me.SpeichernInCyParascriptIDE4ToolStripMenuItem.Size = New System.Drawing.Size(258, 22)
    Me.SpeichernInCyParascriptIDE4ToolStripMenuItem.Text = "Speichern in C:\yPara\scriptIDE4\..."
    '
    'flpBookmarks
    '
    Me.flpBookmarks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.flpBookmarks.Location = New System.Drawing.Point(2, 26)
    Me.flpBookmarks.Name = "flpBookmarks"
    Me.flpBookmarks.Size = New System.Drawing.Size(246, 18)
    Me.flpBookmarks.TabIndex = 2
    '
    'frmTB_scriptSync
    '
    Me.AllowDrop = True
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(249, 430)
    Me.Controls.Add(Me.flpBookmarks)
    Me.Controls.Add(Me.ToolStrip1)
    Me.Controls.Add(Me.ListView1)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmTB_scriptSync"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
    Me.Text = "Sync"
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.ContextMenuStrip1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents tsbUploadScript As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbRefresh As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbLogin As System.Windows.Forms.ToolStripButton
  Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents SpeichernUnterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents LoeschenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents flpBookmarks As System.Windows.Forms.FlowLayoutPanel
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents SpeichernInCyParascriptIDE4ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents DownloadNachToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
