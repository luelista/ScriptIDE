<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDC_gridView
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDC_gridView))
    Me.IGrid1 = New TenTec.Windows.iGridLib.iGrid
    Me.IGrid1DefaultCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1DefaultColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1RowTextColCellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
    Me.tsFileActions = New System.Windows.Forms.ToolStrip
    Me.chkStat = New System.Windows.Forms.CheckBox
    Me.grpStat = New System.Windows.Forms.GroupBox
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.Label7 = New System.Windows.Forms.Label
    Me.labSelLineCount = New System.Windows.Forms.Label
    Me.Label5 = New System.Windows.Forms.Label
    Me.labAverage = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.labSum = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.GroupBox2 = New System.Windows.Forms.GroupBox
    Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
    Me.Label1 = New System.Windows.Forms.Label
    Me.check_rowMode = New System.Windows.Forms.CheckBox
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.txtFilterScriptSel = New System.Windows.Forms.TextBox
    Me.Label3 = New System.Windows.Forms.Label
    Me.btnRunScript = New System.Windows.Forms.Button
    Me.lstScriptSel = New System.Windows.Forms.ListBox
    Me.txtReadFilename = New System.Windows.Forms.TextBox
    Me.labFilename = New System.Windows.Forms.Label
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.btnSave = New System.Windows.Forms.Button
    Me.btnOpen = New System.Windows.Forms.Button
    Me.txtSaveFilename = New System.Windows.Forms.TextBox
    Me.btnEditScript = New System.Windows.Forms.Button
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SplitContainer1.Panel1.SuspendLayout()
    Me.SplitContainer1.Panel2.SuspendLayout()
    Me.SplitContainer1.SuspendLayout()
    Me.grpStat.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'IGrid1
    '
    Me.IGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.IGrid1.DefaultCol.CellStyle = Me.IGrid1DefaultCellStyle
    Me.IGrid1.DefaultCol.ColHdrStyle = Me.IGrid1DefaultColHdrStyle
    Me.IGrid1.DefaultRow.CellStyle = Me.IGrid1DefaultCellStyle
    Me.IGrid1.GroupBox.HintBackColor = System.Drawing.Color.Transparent
    Me.IGrid1.GroupBox.HintForeColor = System.Drawing.Color.WhiteSmoke
    Me.IGrid1.GroupBox.Visible = True
    Me.IGrid1.Header.Height = 19
    Me.IGrid1.Location = New System.Drawing.Point(0, 25)
    Me.IGrid1.Name = "IGrid1"
    Me.IGrid1.RowMode = True
    Me.IGrid1.RowTextCol.CellStyle = Me.IGrid1RowTextColCellStyle
    Me.IGrid1.SelCellsBackColorNoFocus = System.Drawing.SystemColors.Highlight
    Me.IGrid1.SelCellsForeColorNoFocus = System.Drawing.SystemColors.HighlightText
    Me.IGrid1.SelectionMode = TenTec.Windows.iGridLib.iGSelectionMode.MultiExtended
    Me.IGrid1.Size = New System.Drawing.Size(461, 598)
    Me.IGrid1.TabIndex = 0
    '
    'IGrid1DefaultCellStyle
    '
    Me.IGrid1DefaultCellStyle.EmptyStringAs = TenTec.Windows.iGridLib.iGEmptyStringAs.EmptyString
    Me.IGrid1DefaultCellStyle.TextTrimming = TenTec.Windows.iGridLib.iGStringTrimming.None
    Me.IGrid1DefaultCellStyle.ValueType = GetType(String)
    '
    'SplitContainer1
    '
    Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
    Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
    Me.SplitContainer1.Name = "SplitContainer1"
    '
    'SplitContainer1.Panel1
    '
    Me.SplitContainer1.Panel1.Controls.Add(Me.tsFileActions)
    Me.SplitContainer1.Panel1.Controls.Add(Me.IGrid1)
    '
    'SplitContainer1.Panel2
    '
    Me.SplitContainer1.Panel2.Controls.Add(Me.chkStat)
    Me.SplitContainer1.Panel2.Controls.Add(Me.grpStat)
    Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
    Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
    Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
    Me.SplitContainer1.Panel2.Controls.Add(Me.txtReadFilename)
    Me.SplitContainer1.Panel2.Controls.Add(Me.labFilename)
    Me.SplitContainer1.Panel2.Controls.Add(Me.PictureBox1)
    Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
    Me.SplitContainer1.Panel2.Controls.Add(Me.btnOpen)
    Me.SplitContainer1.Panel2.Controls.Add(Me.txtSaveFilename)
    Me.SplitContainer1.Size = New System.Drawing.Size(660, 624)
    Me.SplitContainer1.SplitterDistance = 461
    Me.SplitContainer1.TabIndex = 1
    '
    'tsFileActions
    '
    Me.tsFileActions.Location = New System.Drawing.Point(0, 0)
    Me.tsFileActions.Name = "tsFileActions"
    Me.tsFileActions.Size = New System.Drawing.Size(461, 25)
    Me.tsFileActions.TabIndex = 1
    Me.tsFileActions.Text = "ToolStrip1"
    '
    'chkStat
    '
    Me.chkStat.AutoSize = True
    Me.chkStat.Location = New System.Drawing.Point(14, 400)
    Me.chkStat.Name = "chkStat"
    Me.chkStat.Size = New System.Drawing.Size(15, 14)
    Me.chkStat.TabIndex = 6
    Me.chkStat.UseVisualStyleBackColor = True
    '
    'grpStat
    '
    Me.grpStat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpStat.Controls.Add(Me.ListView1)
    Me.grpStat.Controls.Add(Me.Label7)
    Me.grpStat.Controls.Add(Me.labSelLineCount)
    Me.grpStat.Controls.Add(Me.Label5)
    Me.grpStat.Controls.Add(Me.labAverage)
    Me.grpStat.Controls.Add(Me.Label4)
    Me.grpStat.Controls.Add(Me.labSum)
    Me.grpStat.Location = New System.Drawing.Point(4, 400)
    Me.grpStat.Name = "grpStat"
    Me.grpStat.Size = New System.Drawing.Size(184, 224)
    Me.grpStat.TabIndex = 14
    Me.grpStat.TabStop = False
    Me.grpStat.Text = "      Statistik"
    '
    'ListView1
    '
    Me.ListView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
    Me.ListView1.Location = New System.Drawing.Point(7, 95)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(169, 124)
    Me.ListView1.TabIndex = 9
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "Anz."
    Me.ColumnHeader1.Width = 50
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Inhalt"
    Me.ColumnHeader2.Width = 94
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(8, 65)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(39, 26)
    Me.Label7.TabIndex = 8
    Me.Label7.Text = "Anz." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Zeilen:"
    '
    'labSelLineCount
    '
    Me.labSelLineCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labSelLineCount.BackColor = System.Drawing.Color.Thistle
    Me.labSelLineCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labSelLineCount.Location = New System.Drawing.Point(56, 68)
    Me.labSelLineCount.Name = "labSelLineCount"
    Me.labSelLineCount.Size = New System.Drawing.Size(122, 21)
    Me.labSelLineCount.TabIndex = 7
    Me.labSelLineCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(8, 39)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(41, 26)
    Me.Label5.TabIndex = 4
    Me.Label5.Text = "Durch-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "schnitt:"
    '
    'labAverage
    '
    Me.labAverage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labAverage.BackColor = System.Drawing.Color.Thistle
    Me.labAverage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labAverage.Location = New System.Drawing.Point(56, 42)
    Me.labAverage.Name = "labAverage"
    Me.labAverage.Size = New System.Drawing.Size(122, 21)
    Me.labAverage.TabIndex = 3
    Me.labAverage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(8, 20)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(45, 13)
    Me.Label4.TabIndex = 2
    Me.Label4.Text = "Summe:"
    '
    'labSum
    '
    Me.labSum.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labSum.BackColor = System.Drawing.Color.Thistle
    Me.labSum.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labSum.Location = New System.Drawing.Point(56, 16)
    Me.labSum.Name = "labSum"
    Me.labSum.Size = New System.Drawing.Size(122, 21)
    Me.labSum.TabIndex = 1
    Me.labSum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label2
    '
    Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label2.BackColor = System.Drawing.Color.LightSteelBlue
    Me.Label2.ForeColor = System.Drawing.Color.DarkSlateBlue
    Me.Label2.Location = New System.Drawing.Point(55, 9)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(133, 20)
    Me.Label2.TabIndex = 13
    Me.Label2.Text = "g r i d V i e w"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.NumericUpDown1)
    Me.GroupBox2.Controls.Add(Me.Label1)
    Me.GroupBox2.Controls.Add(Me.check_rowMode)
    Me.GroupBox2.Location = New System.Drawing.Point(4, 125)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(184, 77)
    Me.GroupBox2.TabIndex = 12
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Einstellungen"
    '
    'NumericUpDown1
    '
    Me.NumericUpDown1.Location = New System.Drawing.Point(61, 47)
    Me.NumericUpDown1.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
    Me.NumericUpDown1.Name = "NumericUpDown1"
    Me.NumericUpDown1.Size = New System.Drawing.Size(51, 20)
    Me.NumericUpDown1.TabIndex = 2
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(9, 49)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(46, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Spalten:"
    '
    'check_rowMode
    '
    Me.check_rowMode.AutoSize = True
    Me.check_rowMode.Checked = True
    Me.check_rowMode.CheckState = System.Windows.Forms.CheckState.Checked
    Me.check_rowMode.Location = New System.Drawing.Point(12, 23)
    Me.check_rowMode.Name = "check_rowMode"
    Me.check_rowMode.Size = New System.Drawing.Size(86, 17)
    Me.check_rowMode.TabIndex = 0
    Me.check_rowMode.Text = "Zeilenmodus"
    Me.check_rowMode.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.btnEditScript)
    Me.GroupBox1.Controls.Add(Me.txtFilterScriptSel)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.btnRunScript)
    Me.GroupBox1.Controls.Add(Me.lstScriptSel)
    Me.GroupBox1.Location = New System.Drawing.Point(4, 208)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(184, 186)
    Me.GroupBox1.TabIndex = 11
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "scriptAuswahl"
    '
    'txtFilterScriptSel
    '
    Me.txtFilterScriptSel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtFilterScriptSel.Location = New System.Drawing.Point(45, 19)
    Me.txtFilterScriptSel.Name = "txtFilterScriptSel"
    Me.txtFilterScriptSel.Size = New System.Drawing.Size(131, 20)
    Me.txtFilterScriptSel.TabIndex = 9
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(7, 22)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(32, 13)
    Me.Label3.TabIndex = 8
    Me.Label3.Text = "Filter:"
    '
    'btnRunScript
    '
    Me.btnRunScript.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnRunScript.Location = New System.Drawing.Point(9, 158)
    Me.btnRunScript.Name = "btnRunScript"
    Me.btnRunScript.Size = New System.Drawing.Size(106, 23)
    Me.btnRunScript.TabIndex = 7
    Me.btnRunScript.Text = "- S T A R T -"
    Me.btnRunScript.UseVisualStyleBackColor = True
    '
    'lstScriptSel
    '
    Me.lstScriptSel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lstScriptSel.FormattingEnabled = True
    Me.lstScriptSel.Location = New System.Drawing.Point(10, 44)
    Me.lstScriptSel.Name = "lstScriptSel"
    Me.lstScriptSel.Size = New System.Drawing.Size(166, 108)
    Me.lstScriptSel.TabIndex = 6
    '
    'txtReadFilename
    '
    Me.txtReadFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtReadFilename.Location = New System.Drawing.Point(4, 68)
    Me.txtReadFilename.Name = "txtReadFilename"
    Me.txtReadFilename.Size = New System.Drawing.Size(129, 20)
    Me.txtReadFilename.TabIndex = 10
    '
    'labFilename
    '
    Me.labFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labFilename.BackColor = System.Drawing.Color.LightSlateGray
    Me.labFilename.ForeColor = System.Drawing.Color.White
    Me.labFilename.Location = New System.Drawing.Point(55, 30)
    Me.labFilename.Name = "labFilename"
    Me.labFilename.Size = New System.Drawing.Size(133, 26)
    Me.labFilename.TabIndex = 9
    Me.labFilename.Text = "Label3"
    Me.labFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(4, 9)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 8
    Me.PictureBox1.TabStop = False
    '
    'btnSave
    '
    Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSave.Location = New System.Drawing.Point(136, 92)
    Me.btnSave.Name = "btnSave"
    Me.btnSave.Size = New System.Drawing.Size(52, 23)
    Me.btnSave.TabIndex = 3
    Me.btnSave.Text = "saveAs"
    Me.btnSave.UseVisualStyleBackColor = True
    '
    'btnOpen
    '
    Me.btnOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOpen.Location = New System.Drawing.Point(136, 66)
    Me.btnOpen.Name = "btnOpen"
    Me.btnOpen.Size = New System.Drawing.Size(52, 23)
    Me.btnOpen.TabIndex = 2
    Me.btnOpen.Text = "read"
    Me.btnOpen.UseVisualStyleBackColor = True
    '
    'txtSaveFilename
    '
    Me.txtSaveFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSaveFilename.Location = New System.Drawing.Point(4, 94)
    Me.txtSaveFilename.Name = "txtSaveFilename"
    Me.txtSaveFilename.Size = New System.Drawing.Size(129, 20)
    Me.txtSaveFilename.TabIndex = 0
    '
    'btnEditScript
    '
    Me.btnEditScript.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnEditScript.Location = New System.Drawing.Point(121, 158)
    Me.btnEditScript.Name = "btnEditScript"
    Me.btnEditScript.Size = New System.Drawing.Size(55, 23)
    Me.btnEditScript.TabIndex = 10
    Me.btnEditScript.Text = "Edit"
    Me.btnEditScript.UseVisualStyleBackColor = True
    '
    'frmDC_gridView
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(660, 624)
    Me.Controls.Add(Me.SplitContainer1)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "frmDC_gridView"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document
    Me.Text = "gridView"
    CType(Me.IGrid1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.SplitContainer1.Panel1.ResumeLayout(False)
    Me.SplitContainer1.Panel1.PerformLayout()
    Me.SplitContainer1.Panel2.ResumeLayout(False)
    Me.SplitContainer1.Panel2.PerformLayout()
    Me.SplitContainer1.ResumeLayout(False)
    Me.grpStat.ResumeLayout(False)
    Me.grpStat.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents IGrid1 As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents IGrid1DefaultCellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1DefaultColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid1RowTextColCellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents txtSaveFilename As System.Windows.Forms.TextBox
  Friend WithEvents btnRunScript As System.Windows.Forms.Button
  Friend WithEvents lstScriptSel As System.Windows.Forms.ListBox
  Friend WithEvents btnSave As System.Windows.Forms.Button
  Friend WithEvents btnOpen As System.Windows.Forms.Button
  Friend WithEvents labFilename As System.Windows.Forms.Label
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents txtReadFilename As System.Windows.Forms.TextBox
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents check_rowMode As System.Windows.Forms.CheckBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents tsFileActions As System.Windows.Forms.ToolStrip
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents txtFilterScriptSel As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents grpStat As System.Windows.Forms.GroupBox
  Friend WithEvents labSum As System.Windows.Forms.Label
  Friend WithEvents chkStat As System.Windows.Forms.CheckBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents labAverage As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents labSelLineCount As System.Windows.Forms.Label
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnEditScript As System.Windows.Forms.Button
End Class
