<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_fileExplorerFavorites
    Inherits System.Windows.Forms.Form

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
    Dim IGColPattern1 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern2 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_fileExplorerFavorites))
    Me.IGrid1Col0CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1Col0ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1Col1CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1Col1ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1 = New TenTec.Windows.iGridLib.iGrid
    Me.IGrid1DefaultCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1DefaultColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1RowTextColCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.btnClose = New System.Windows.Forms.Button
    Me.btnAddDir = New System.Windows.Forms.Button
    Me.btnRemove = New System.Windows.Forms.Button
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'IGrid1Col0ColHdrStyle
    '
    Me.IGrid1Col0ColHdrStyle.ContentIndent = New TenTec.Windows.iGridLib.iGIndent(1, 4, 1, 4)
    '
    'IGrid1Col1ColHdrStyle
    '
    Me.IGrid1Col1ColHdrStyle.ContentIndent = New TenTec.Windows.iGridLib.iGIndent(1, 4, 1, 4)
    '
    'IGrid1
    '
    Me.IGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.IGrid1.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat
    Me.IGrid1.AutoResizeCols = True
    IGColPattern1.CellStyle = Me.IGrid1Col0CellStyle
    IGColPattern1.ColHdrStyle = Me.IGrid1Col0ColHdrStyle
    IGColPattern1.Text = "Beschriftung"
    IGColPattern1.Width = 133
    IGColPattern2.CellStyle = Me.IGrid1Col1CellStyle
    IGColPattern2.ColHdrStyle = Me.IGrid1Col1ColHdrStyle
    IGColPattern2.Text = "Pfad"
    IGColPattern2.Width = 200
    Me.IGrid1.Cols.AddRange(New TenTec.Windows.iGridLib.iGColPattern() {IGColPattern1, IGColPattern2})
    Me.IGrid1.DefaultCol.CellStyle = Me.IGrid1DefaultCellStyle
    Me.IGrid1.DefaultCol.ColHdrStyle = Me.IGrid1DefaultColHdrStyle
    Me.IGrid1.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat
    Me.IGrid1.Header.Height = 25
    Me.IGrid1.Location = New System.Drawing.Point(13, 10)
    Me.IGrid1.Name = "IGrid1"
    Me.IGrid1.RowMode = True
    Me.IGrid1.RowTextCol.CellStyle = Me.IGrid1RowTextColCellStyle
    Me.IGrid1.Size = New System.Drawing.Size(337, 287)
    Me.IGrid1.TabIndex = 0
    '
    'btnClose
    '
    Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnClose.Location = New System.Drawing.Point(359, 275)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(99, 23)
    Me.btnClose.TabIndex = 1
    Me.btnClose.Text = "Schließen"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'btnAddDir
    '
    Me.btnAddDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnAddDir.Location = New System.Drawing.Point(359, 10)
    Me.btnAddDir.Name = "btnAddDir"
    Me.btnAddDir.Size = New System.Drawing.Size(99, 36)
    Me.btnAddDir.TabIndex = 2
    Me.btnAddDir.Text = "Aktuellen Ordner hinzufügen"
    Me.btnAddDir.UseVisualStyleBackColor = True
    '
    'btnRemove
    '
    Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnRemove.Location = New System.Drawing.Point(359, 52)
    Me.btnRemove.Name = "btnRemove"
    Me.btnRemove.Size = New System.Drawing.Size(99, 23)
    Me.btnRemove.TabIndex = 3
    Me.btnRemove.Text = "Löschen"
    Me.btnRemove.UseVisualStyleBackColor = True
    '
    'frm_fileExplorerFavorites
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(470, 307)
    Me.Controls.Add(Me.btnRemove)
    Me.Controls.Add(Me.btnAddDir)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.IGrid1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(239, 158)
    Me.Name = "frm_fileExplorerFavorites"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
    Me.Text = "Favoriten verwalten"
    Me.TopMost = True
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents IGrid1 As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents IGrid1Col0CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1Col0ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid1Col1CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1Col1ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid1DefaultCellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1DefaultColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid1RowTextColCellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents btnClose As System.Windows.Forms.Button
  Friend WithEvents btnAddDir As System.Windows.Forms.Button
  Friend WithEvents btnRemove As System.Windows.Forms.Button
End Class
