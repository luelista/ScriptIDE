<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_openedFiles
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
    Dim IGColPattern1 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern2 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_openedFiles))
    Me.IGrid1 = New TenTec.Windows.iGridLib.iGrid
    Me.IGCellStyleDesign1 = New TenTec.Windows.iGridLib.iGCellStyleDesign
    Me.imlIgrid = New System.Windows.Forms.ImageList(Me.components)
    Me.txtSearch = New System.Windows.Forms.TextBox
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'IGrid1
    '
    Me.IGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.IGrid1.AutoResizeCols = True
    Me.IGrid1.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer))
    IGColPattern1.CellStyle = Me.IGCellStyleDesign1
    IGColPattern1.Width = 171
    IGColPattern2.Visible = False
    IGColPattern2.Width = 0
    Me.IGrid1.Cols.AddRange(New TenTec.Windows.iGridLib.iGColPattern() {IGColPattern1, IGColPattern2})
    Me.IGrid1.DefaultCol.CellStyle = Me.IGCellStyleDesign1
    Me.IGrid1.DefaultRow.Height = 15
    Me.IGrid1.DefaultRow.NormalCellHeight = 15
    Me.IGrid1.GridLines.Mode = TenTec.Windows.iGridLib.iGGridLinesMode.None
    Me.IGrid1.Header.AllowPress = False
    Me.IGrid1.Header.Height = 19
    Me.IGrid1.Header.Visible = False
    Me.IGrid1.ImageList = Me.imlIgrid
    Me.IGrid1.Location = New System.Drawing.Point(0, 29)
    Me.IGrid1.Name = "IGrid1"
    Me.IGrid1.ReadOnly = True
    Me.IGrid1.RowMode = True
    Me.IGrid1.RowModeHasCurCell = True
    Me.IGrid1.SelCellsBackColorNoFocus = System.Drawing.SystemColors.Highlight
    Me.IGrid1.SelCellsForeColorNoFocus = System.Drawing.SystemColors.HighlightText
    Me.IGrid1.Size = New System.Drawing.Size(175, 414)
    Me.IGrid1.TabIndex = 4
    '
    'IGCellStyleDesign1
    '
    Me.IGCellStyleDesign1.CustomDrawFlags = TenTec.Windows.iGridLib.iGCustomDrawFlags.Foreground
    '
    'imlIgrid
    '
    Me.imlIgrid.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.imlIgrid.ImageSize = New System.Drawing.Size(16, 16)
    Me.imlIgrid.TransparentColor = System.Drawing.Color.Transparent
    '
    'txtSearch
    '
    Me.txtSearch.Location = New System.Drawing.Point(2, 3)
    Me.txtSearch.Name = "txtSearch"
    Me.txtSearch.Size = New System.Drawing.Size(148, 20)
    Me.txtSearch.TabIndex = 5
    Me.txtSearch.Text = "suche..."
    '
    'frmTB_openedFiles
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(175, 443)
    Me.Controls.Add(Me.txtSearch)
    Me.Controls.Add(Me.IGrid1)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_openedFiles"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft
    Me.Text = "Geöffnete Tabs"
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents IGrid1 As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents imlIgrid As System.Windows.Forms.ImageList
  Friend WithEvents IGCellStyleDesign1 As TenTec.Windows.iGridLib.iGCellStyleDesign
  Friend WithEvents txtSearch As System.Windows.Forms.TextBox
End Class
