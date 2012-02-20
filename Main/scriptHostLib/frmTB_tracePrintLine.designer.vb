<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_tracePrintLine
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
    Dim IGColPattern1 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern2 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern3 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_tracePrintLine))
    Me.IGrid1Col0CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1Col0ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1Col1CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1Col1ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1Col2CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1Col2ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1 = New TenTec.Windows.iGridLib.iGrid
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.KopierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.resetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.IGrid1DefaultCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1DefaultColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1RowTextColCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.labPrintLine11 = New System.Windows.Forms.Label
    Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
    Me.Label30 = New System.Windows.Forms.Label
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.ContextMenuStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'IGrid1
    '
    Me.IGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.IGrid1.AutoResizeCols = True
    IGColPattern1.AllowSizing = False
    IGColPattern1.CellStyle = Me.IGrid1Col0CellStyle
    IGColPattern1.ColHdrStyle = Me.IGrid1Col0ColHdrStyle
    IGColPattern1.Text = "ID"
    IGColPattern1.Width = 25
    IGColPattern2.CellStyle = Me.IGrid1Col1CellStyle
    IGColPattern2.ColHdrStyle = Me.IGrid1Col1ColHdrStyle
    IGColPattern2.Text = "Titel"
    IGColPattern2.Width = 181
    IGColPattern3.CellStyle = Me.IGrid1Col2CellStyle
    IGColPattern3.ColHdrStyle = Me.IGrid1Col2ColHdrStyle
    IGColPattern3.Text = "Inhalt"
    IGColPattern3.Width = 155
    Me.IGrid1.Cols.AddRange(New TenTec.Windows.iGridLib.iGColPattern() {IGColPattern1, IGColPattern2, IGColPattern3})
    Me.IGrid1.ContextMenuStrip = Me.ContextMenuStrip1
    Me.IGrid1.DefaultCol.CellStyle = Me.IGrid1DefaultCellStyle
    Me.IGrid1.DefaultCol.ColHdrStyle = Me.IGrid1DefaultColHdrStyle
    Me.IGrid1.Header.Height = 19
    Me.IGrid1.Location = New System.Drawing.Point(1, 44)
    Me.IGrid1.Name = "IGrid1"
    Me.IGrid1.ReadOnly = True
    Me.IGrid1.RowMode = True
    Me.IGrid1.RowTextCol.CellStyle = Me.IGrid1RowTextColCellStyle
    Me.IGrid1.Size = New System.Drawing.Size(365, 291)
    Me.IGrid1.TabIndex = 0
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KopierenToolStripMenuItem, Me.resetToolStripMenuItem})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(145, 48)
    '
    'KopierenToolStripMenuItem
    '
    Me.KopierenToolStripMenuItem.Name = "KopierenToolStripMenuItem"
    Me.KopierenToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
    Me.KopierenToolStripMenuItem.Text = "Kopieren"
    '
    'resetToolStripMenuItem
    '
    Me.resetToolStripMenuItem.Name = "resetToolStripMenuItem"
    Me.resetToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
    Me.resetToolStripMenuItem.Text = "Zurücksetzen"
    '
    'labPrintLine11
    '
    Me.labPrintLine11.BackColor = System.Drawing.Color.PaleGoldenrod
    Me.labPrintLine11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labPrintLine11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.labPrintLine11.Location = New System.Drawing.Point(0, 0)
    Me.labPrintLine11.Name = "labPrintLine11"
    Me.labPrintLine11.Padding = New System.Windows.Forms.Padding(44, 4, 0, 0)
    Me.labPrintLine11.Size = New System.Drawing.Size(596, 44)
    Me.labPrintLine11.TabIndex = 2
    Me.labPrintLine11.Text = "Label29"
    '
    'FlowLayoutPanel1
    '
    Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.PaleGoldenrod
    Me.FlowLayoutPanel1.Location = New System.Drawing.Point(45, 24)
    Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
    Me.FlowLayoutPanel1.Size = New System.Drawing.Size(564, 20)
    Me.FlowLayoutPanel1.TabIndex = 4
    '
    'Label30
    '
    Me.Label30.BackColor = System.Drawing.Color.PaleGoldenrod
    Me.Label30.Image = CType(resources.GetObject("Label30.Image"), System.Drawing.Image)
    Me.Label30.Location = New System.Drawing.Point(6, 6)
    Me.Label30.Name = "Label30"
    Me.Label30.Size = New System.Drawing.Size(32, 32)
    Me.Label30.TabIndex = 5
    '
    'frmTB_tracePrintLine
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(366, 335)
    Me.Controls.Add(Me.Label30)
    Me.Controls.Add(Me.FlowLayoutPanel1)
    Me.Controls.Add(Me.labPrintLine11)
    Me.Controls.Add(Me.IGrid1)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_tracePrintLine"
    Me.Text = "PrintLines"
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ContextMenuStrip1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents IGrid1 As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents IGrid1DefaultCellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1DefaultColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid1RowTextColCellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1Col0CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1Col0ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid1Col1CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1Col1ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid1Col2CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1Col2ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents KopierenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents resetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents labPrintLine11 As System.Windows.Forms.Label
  Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
  Friend WithEvents Label30 As System.Windows.Forms.Label
End Class
