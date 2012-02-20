<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_designer
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_designer))
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.DownloadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.SpeichernUnterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.LoeschenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
    Me.IGrid1 = New TenTec.Windows.iGridLib.iGrid
    Me.IGrid1DefaultCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1DefaultColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1RowTextColCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
    Me.labInfo4 = New System.Windows.Forms.Label
    Me.labInfo3 = New System.Windows.Forms.Label
    Me.labInfo2 = New System.Windows.Forms.Label
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.labInfo1 = New System.Windows.Forms.Label
    Me.txtZoombox = New System.Windows.Forms.TextBox
    Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
    Me.tsbOpen = New System.Windows.Forms.ToolStripButton
    Me.tsbWriteBack = New System.Windows.Forms.ToolStripButton
    Me.tsbCopy = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton
    Me.ContextMenuStrip1.SuspendLayout()
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SplitContainer1.Panel1.SuspendLayout()
    Me.SplitContainer1.Panel2.SuspendLayout()
    Me.SplitContainer1.SuspendLayout()
    Me.ToolStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'ImageList1
    '
    Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
    Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DownloadToolStripMenuItem, Me.SpeichernUnterToolStripMenuItem, Me.LoeschenToolStripMenuItem})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(170, 70)
    '
    'DownloadToolStripMenuItem
    '
    Me.DownloadToolStripMenuItem.Name = "DownloadToolStripMenuItem"
    Me.DownloadToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
    Me.DownloadToolStripMenuItem.Text = "Download"
    '
    'SpeichernUnterToolStripMenuItem
    '
    Me.SpeichernUnterToolStripMenuItem.Name = "SpeichernUnterToolStripMenuItem"
    Me.SpeichernUnterToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
    Me.SpeichernUnterToolStripMenuItem.Text = "Speichern unter ..."
    '
    'LoeschenToolStripMenuItem
    '
    Me.LoeschenToolStripMenuItem.Name = "LoeschenToolStripMenuItem"
    Me.LoeschenToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
    Me.LoeschenToolStripMenuItem.Text = "Löschen"
    '
    'IGrid1
    '
    Me.IGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.IGrid1.DefaultCol.CellStyle = Me.IGrid1DefaultCellStyle
    Me.IGrid1.DefaultCol.ColHdrStyle = Me.IGrid1DefaultColHdrStyle
    Me.IGrid1.Header.AllowPress = False
    Me.IGrid1.Header.Height = 19
    Me.IGrid1.Location = New System.Drawing.Point(0, 0)
    Me.IGrid1.Name = "IGrid1"
    Me.IGrid1.RowTextCol.CellStyle = Me.IGrid1RowTextColCellStyle
    Me.IGrid1.Size = New System.Drawing.Size(581, 291)
    Me.IGrid1.TabIndex = 2
    '
    'SplitContainer1
    '
    Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
    Me.SplitContainer1.Location = New System.Drawing.Point(2, 27)
    Me.SplitContainer1.Name = "SplitContainer1"
    Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplitContainer1.Panel1
    '
    Me.SplitContainer1.Panel1.Controls.Add(Me.labInfo4)
    Me.SplitContainer1.Panel1.Controls.Add(Me.labInfo3)
    Me.SplitContainer1.Panel1.Controls.Add(Me.labInfo2)
    Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
    Me.SplitContainer1.Panel1.Controls.Add(Me.labInfo1)
    '
    'SplitContainer1.Panel2
    '
    Me.SplitContainer1.Panel2.Controls.Add(Me.txtZoombox)
    Me.SplitContainer1.Panel2.Controls.Add(Me.PropertyGrid1)
    Me.SplitContainer1.Panel2.Controls.Add(Me.IGrid1)
    Me.SplitContainer1.Size = New System.Drawing.Size(849, 706)
    Me.SplitContainer1.SplitterDistance = 345
    Me.SplitContainer1.TabIndex = 3
    '
    'labInfo4
    '
    Me.labInfo4.BackColor = System.Drawing.Color.SteelBlue
    Me.labInfo4.ForeColor = System.Drawing.Color.White
    Me.labInfo4.Location = New System.Drawing.Point(377, 0)
    Me.labInfo4.Name = "labInfo4"
    Me.labInfo4.Size = New System.Drawing.Size(210, 21)
    Me.labInfo4.TabIndex = 4
    Me.labInfo4.Text = "ActiveControl   lab_xyzabc"
    Me.labInfo4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'labInfo3
    '
    Me.labInfo3.AutoSize = True
    Me.labInfo3.Location = New System.Drawing.Point(204, 4)
    Me.labInfo3.Name = "labInfo3"
    Me.labInfo3.Size = New System.Drawing.Size(115, 13)
    Me.labInfo3.TabIndex = 3
    Me.labInfo3.Text = "FormName aaabbbccc"
    '
    'labInfo2
    '
    Me.labInfo2.AutoSize = True
    Me.labInfo2.Location = New System.Drawing.Point(85, 4)
    Me.labInfo2.Name = "labInfo2"
    Me.labInfo2.Size = New System.Drawing.Size(94, 13)
    Me.labInfo2.TabIndex = 2
    Me.labInfo2.Text = "FormSize 111x222"
    '
    'Panel1
    '
    Me.Panel1.AllowDrop = True
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel1.Location = New System.Drawing.Point(4, 21)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(626, 303)
    Me.Panel1.TabIndex = 1
    '
    'labInfo1
    '
    Me.labInfo1.AutoSize = True
    Me.labInfo1.Location = New System.Drawing.Point(5, 4)
    Me.labInfo1.Name = "labInfo1"
    Me.labInfo1.Size = New System.Drawing.Size(43, 13)
    Me.labInfo1.TabIndex = 0
    Me.labInfo1.Text = "Design:"
    '
    'txtZoombox
    '
    Me.txtZoombox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtZoombox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.txtZoombox.Location = New System.Drawing.Point(3, 293)
    Me.txtZoombox.Multiline = True
    Me.txtZoombox.Name = "txtZoombox"
    Me.txtZoombox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtZoombox.Size = New System.Drawing.Size(584, 62)
    Me.txtZoombox.TabIndex = 4
    Me.txtZoombox.Text = "11" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "22" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "33" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "44"
    '
    'PropertyGrid1
    '
    Me.PropertyGrid1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PropertyGrid1.Location = New System.Drawing.Point(587, 2)
    Me.PropertyGrid1.Name = "PropertyGrid1"
    Me.PropertyGrid1.Size = New System.Drawing.Size(261, 351)
    Me.PropertyGrid1.TabIndex = 3
    '
    'ToolStrip1
    '
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpen, Me.tsbWriteBack, Me.tsbCopy, Me.ToolStripSeparator1, Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripButton6, Me.ToolStripButton7, Me.ToolStripButton8, Me.ToolStripButton9, Me.ToolStripButton10})
    Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(853, 25)
    Me.ToolStrip1.TabIndex = 4
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'tsbOpen
    '
    Me.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbOpen.Image = CType(resources.GetObject("tsbOpen.Image"), System.Drawing.Image)
    Me.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbOpen.Name = "tsbOpen"
    Me.tsbOpen.Size = New System.Drawing.Size(23, 22)
    Me.tsbOpen.Text = "ToolStripButton11"
    '
    'tsbWriteBack
    '
    Me.tsbWriteBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbWriteBack.Image = CType(resources.GetObject("tsbWriteBack.Image"), System.Drawing.Image)
    Me.tsbWriteBack.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbWriteBack.Name = "tsbWriteBack"
    Me.tsbWriteBack.Size = New System.Drawing.Size(23, 22)
    Me.tsbWriteBack.Text = "ToolStripButton12"
    '
    'tsbCopy
    '
    Me.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.tsbCopy.Image = CType(resources.GetObject("tsbCopy.Image"), System.Drawing.Image)
    Me.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbCopy.Name = "tsbCopy"
    Me.tsbCopy.Size = New System.Drawing.Size(23, 22)
    Me.tsbCopy.Text = "kopieren"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
    '
    'ToolStripButton1
    '
    Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
    Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton1.Name = "ToolStripButton1"
    Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton1.Text = "Button"
    '
    'ToolStripButton2
    '
    Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
    Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton2.Name = "ToolStripButton2"
    Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton2.Text = "Label"
    '
    'ToolStripButton3
    '
    Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
    Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton3.Name = "ToolStripButton3"
    Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton3.Text = "CheckBox"
    '
    'ToolStripButton4
    '
    Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
    Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton4.Name = "ToolStripButton4"
    Me.ToolStripButton4.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton4.Text = "PictureBox"
    '
    'ToolStripButton5
    '
    Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
    Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton5.Name = "ToolStripButton5"
    Me.ToolStripButton5.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton5.Text = "TextBox"
    '
    'ToolStripButton6
    '
    Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
    Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton6.Name = "ToolStripButton6"
    Me.ToolStripButton6.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton6.Text = "ListBox"
    '
    'ToolStripButton7
    '
    Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
    Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton7.Name = "ToolStripButton7"
    Me.ToolStripButton7.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton7.Text = "GroupBox"
    '
    'ToolStripButton8
    '
    Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
    Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton8.Name = "ToolStripButton8"
    Me.ToolStripButton8.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton8.Text = "SplitContainer"
    '
    'ToolStripButton9
    '
    Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton9.Image = CType(resources.GetObject("ToolStripButton9.Image"), System.Drawing.Image)
    Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton9.Name = "ToolStripButton9"
    Me.ToolStripButton9.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton9.Text = "Panel"
    '
    'ToolStripButton10
    '
    Me.ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
    Me.ToolStripButton10.Image = CType(resources.GetObject("ToolStripButton10.Image"), System.Drawing.Image)
    Me.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton10.Name = "ToolStripButton10"
    Me.ToolStripButton10.Size = New System.Drawing.Size(23, 22)
    Me.ToolStripButton10.Text = "Tentec.Windows.iGridLib.iGrid"
    '
    'frmTB_designer
    '
    Me.AllowDrop = True
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(853, 734)
    Me.Controls.Add(Me.ToolStrip1)
    Me.Controls.Add(Me.SplitContainer1)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmTB_designer"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
    Me.Text = "Design"
    Me.ContextMenuStrip1.ResumeLayout(False)
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.SplitContainer1.Panel1.ResumeLayout(False)
    Me.SplitContainer1.Panel1.PerformLayout()
    Me.SplitContainer1.Panel2.ResumeLayout(False)
    Me.SplitContainer1.Panel2.PerformLayout()
    Me.SplitContainer1.ResumeLayout(False)
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents DownloadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SpeichernUnterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents LoeschenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents IGrid1 As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents IGrid1DefaultCellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1DefaultColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid1RowTextColCellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents labInfo1 As System.Windows.Forms.Label
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton7 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton9 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton10 As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbOpen As System.Windows.Forms.ToolStripButton
  Friend WithEvents tsbWriteBack As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents labInfo3 As System.Windows.Forms.Label
  Friend WithEvents labInfo2 As System.Windows.Forms.Label
  Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
  Friend WithEvents tsbCopy As System.Windows.Forms.ToolStripButton
  Friend WithEvents labInfo4 As System.Windows.Forms.Label
  Friend WithEvents txtZoombox As System.Windows.Forms.TextBox
End Class
