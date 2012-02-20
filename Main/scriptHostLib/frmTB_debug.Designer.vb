<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_debug
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
    Dim IGColPattern3 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_debug))
    Me.IGrid3Col3CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid3Col3ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid3Col2CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid3Col2ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid3Col3CellStyle1 = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid3Col3ColHdrStyle1 = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid3Col4CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid3Col4ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid3Col5CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid3Col5ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.Button8 = New System.Windows.Forms.Button
    Me.qq_txtOutMonitor = New System.Windows.Forms.TextBox
    Me.Button6 = New System.Windows.Forms.Button
    Me.checkTraceStack = New System.Windows.Forms.CheckBox
    Me.qq_check_silentMode = New System.Windows.Forms.CheckBox
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
    Me.tsbTab_0 = New System.Windows.Forms.ToolStripStatusLabel
    Me.tsbTab_1 = New System.Windows.Forms.ToolStripStatusLabel
    Me.tsbTab_2 = New System.Windows.Forms.ToolStripStatusLabel
    Me.tsbTab_3 = New System.Windows.Forms.ToolStripStatusLabel
    Me.lnkTraceMonitor = New System.Windows.Forms.ToolStripStatusLabel
    Me.lblRunning = New System.Windows.Forms.ToolStripStatusLabel
    Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.NavigateCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
    Me.KopierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.resetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
    Me.TextBox2 = New System.Windows.Forms.TextBox
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
    Me.tsbClearTrace2 = New System.Windows.Forms.ToolStripButton
    Me.IGrid2 = New TenTec.Windows.iGridLib.iGrid
    Me.iml_TraceTypes = New System.Windows.Forms.ImageList(Me.components)
    Me.TabControl1 = New System.Windows.Forms.TabControl
    Me.TabPage1 = New System.Windows.Forms.TabPage
    Me.TabPage3 = New System.Windows.Forms.TabPage
    Me.btnStdOutClear = New System.Windows.Forms.Button
    Me.txtOutSource = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.btnStdOutPaste = New System.Windows.Forms.Button
    Me.btnStdOutCopy = New System.Windows.Forms.Button
    Me.TabPage4 = New System.Windows.Forms.TabPage
    Me.btnStdInPaste = New System.Windows.Forms.Button
    Me.btnStdInCopy = New System.Windows.Forms.Button
    Me.qq_txtInTextbox = New System.Windows.Forms.TextBox
    Me.TabPage5 = New System.Windows.Forms.TabPage
    Me.ListView2 = New System.Windows.Forms.ListView
    Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.timer_fadeOutErrBox = New System.Windows.Forms.Timer(Me.components)
    Me.checkTraceAutoscroll = New System.Windows.Forms.CheckBox
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.timer_autoScroll = New System.Windows.Forms.Timer(Me.components)
    Me.StatusStrip1.SuspendLayout()
    Me.ContextMenuStrip1.SuspendLayout()
    Me.SplitContainer2.Panel1.SuspendLayout()
    Me.SplitContainer2.Panel2.SuspendLayout()
    Me.SplitContainer2.SuspendLayout()
    Me.ToolStrip1.SuspendLayout()
    CType(Me.IGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabControl1.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    Me.TabPage3.SuspendLayout()
    Me.TabPage4.SuspendLayout()
    Me.TabPage5.SuspendLayout()
    Me.SuspendLayout()
    '
    'Button8
    '
    Me.Button8.BackColor = System.Drawing.Color.PaleGoldenrod
    Me.Button8.Location = New System.Drawing.Point(75, 14)
    Me.Button8.Name = "Button8"
    Me.Button8.Size = New System.Drawing.Size(70, 23)
    Me.Button8.TabIndex = 8
    Me.Button8.Text = "recompile"
    Me.Button8.UseVisualStyleBackColor = False
    '
    'qq_txtOutMonitor
    '
    Me.qq_txtOutMonitor.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.qq_txtOutMonitor.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.qq_txtOutMonitor.Location = New System.Drawing.Point(8, 30)
    Me.qq_txtOutMonitor.Multiline = True
    Me.qq_txtOutMonitor.Name = "qq_txtOutMonitor"
    Me.qq_txtOutMonitor.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.qq_txtOutMonitor.Size = New System.Drawing.Size(792, 163)
    Me.qq_txtOutMonitor.TabIndex = 7
    Me.qq_txtOutMonitor.WordWrap = False
    '
    'Button6
    '
    Me.Button6.BackColor = System.Drawing.Color.PaleGoldenrod
    Me.Button6.Location = New System.Drawing.Point(6, 14)
    Me.Button6.Name = "Button6"
    Me.Button6.Size = New System.Drawing.Size(70, 23)
    Me.Button6.TabIndex = 6
    Me.Button6.Text = "listClasses"
    Me.Button6.UseVisualStyleBackColor = False
    '
    'checkTraceStack
    '
    Me.checkTraceStack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.checkTraceStack.AutoSize = True
    Me.checkTraceStack.Location = New System.Drawing.Point(696, 200)
    Me.checkTraceStack.Name = "checkTraceStack"
    Me.checkTraceStack.Size = New System.Drawing.Size(78, 17)
    Me.checkTraceStack.TabIndex = 18
    Me.checkTraceStack.Text = "traceStack"
    Me.ToolTip1.SetToolTip(Me.checkTraceStack, "Bei jedem Funktionsaufruf kompletten Callstack im Trace anzeigen")
    Me.checkTraceStack.UseVisualStyleBackColor = True
    '
    'qq_check_silentMode
    '
    Me.qq_check_silentMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.qq_check_silentMode.AutoSize = True
    Me.qq_check_silentMode.Location = New System.Drawing.Point(772, 200)
    Me.qq_check_silentMode.Name = "qq_check_silentMode"
    Me.qq_check_silentMode.Size = New System.Drawing.Size(50, 17)
    Me.qq_check_silentMode.TabIndex = 22
    Me.qq_check_silentMode.Text = "silent"
    Me.ToolTip1.SetToolTip(Me.qq_check_silentMode, "Bei Skriptfehlern nicht anhalten")
    Me.qq_check_silentMode.UseVisualStyleBackColor = True
    '
    'StatusStrip1
    '
    Me.StatusStrip1.AutoSize = False
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbTab_0, Me.tsbTab_1, Me.tsbTab_2, Me.tsbTab_3, Me.lnkTraceMonitor, Me.lblRunning, Me.lblStatus})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 196)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(835, 22)
    Me.StatusStrip1.TabIndex = 25
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'tsbTab_0
    '
    Me.tsbTab_0.BackColor = System.Drawing.Color.RoyalBlue
    Me.tsbTab_0.IsLink = True
    Me.tsbTab_0.LinkColor = System.Drawing.Color.White
    Me.tsbTab_0.Margin = New System.Windows.Forms.Padding(22, 3, 0, 2)
    Me.tsbTab_0.Name = "tsbTab_0"
    Me.tsbTab_0.Size = New System.Drawing.Size(35, 17)
    Me.tsbTab_0.Tag = "0"
    Me.tsbTab_0.Text = "Trace"
    '
    'tsbTab_1
    '
    Me.tsbTab_1.IsLink = True
    Me.tsbTab_1.Name = "tsbTab_1"
    Me.tsbTab_1.Size = New System.Drawing.Size(44, 17)
    Me.tsbTab_1.Tag = "1"
    Me.tsbTab_1.Text = "stdOUT"
    Me.tsbTab_1.ToolTipText = "Standard-Ausgabe"
    '
    'tsbTab_2
    '
    Me.tsbTab_2.IsLink = True
    Me.tsbTab_2.Name = "tsbTab_2"
    Me.tsbTab_2.Size = New System.Drawing.Size(32, 17)
    Me.tsbTab_2.Tag = "2"
    Me.tsbTab_2.Text = "stdIN"
    Me.tsbTab_2.ToolTipText = "Standard-Eingabe"
    '
    'tsbTab_3
    '
    Me.tsbTab_3.IsLink = True
    Me.tsbTab_3.Name = "tsbTab_3"
    Me.tsbTab_3.Size = New System.Drawing.Size(31, 17)
    Me.tsbTab_3.Tag = "3"
    Me.tsbTab_3.Text = "class"
    Me.tsbTab_3.ToolTipText = "Skriptklassen-Liste"
    '
    'lnkTraceMonitor
    '
    Me.lnkTraceMonitor.IsLink = True
    Me.lnkTraceMonitor.Margin = New System.Windows.Forms.Padding(12, 3, 0, 2)
    Me.lnkTraceMonitor.Name = "lnkTraceMonitor"
    Me.lnkTraceMonitor.Size = New System.Drawing.Size(55, 17)
    Me.lnkTraceMonitor.Text = "ext. Trace"
    Me.lnkTraceMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'lblRunning
    '
    Me.lblRunning.AutoSize = False
    Me.lblRunning.Name = "lblRunning"
    Me.lblRunning.Size = New System.Drawing.Size(200, 17)
    Me.lblRunning.Text = "..."
    Me.lblRunning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblStatus
    '
    Me.lblStatus.Name = "lblStatus"
    Me.lblStatus.Size = New System.Drawing.Size(389, 17)
    Me.lblStatus.Spring = True
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NavigateCodeToolStripMenuItem, Me.ToolStripMenuItem1, Me.KopierenToolStripMenuItem, Me.resetToolStripMenuItem})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(214, 76)
    '
    'NavigateCodeToolStripMenuItem
    '
    Me.NavigateCodeToolStripMenuItem.Name = "NavigateCodeToolStripMenuItem"
    Me.NavigateCodeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
    Me.NavigateCodeToolStripMenuItem.Text = "Auslösende Zeile anzeigen"
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(210, 6)
    '
    'KopierenToolStripMenuItem
    '
    Me.KopierenToolStripMenuItem.Name = "KopierenToolStripMenuItem"
    Me.KopierenToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
    Me.KopierenToolStripMenuItem.Text = "Kopieren"
    '
    'resetToolStripMenuItem
    '
    Me.resetToolStripMenuItem.Name = "resetToolStripMenuItem"
    Me.resetToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
    Me.resetToolStripMenuItem.Text = "Zurücksetzen"
    '
    'SplitContainer2
    '
    Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
    Me.SplitContainer2.Location = New System.Drawing.Point(0, 1)
    Me.SplitContainer2.Name = "SplitContainer2"
    '
    'SplitContainer2.Panel1
    '
    Me.SplitContainer2.Panel1.Controls.Add(Me.TextBox2)
    '
    'SplitContainer2.Panel2
    '
    Me.SplitContainer2.Panel2.Controls.Add(Me.ToolStrip1)
    Me.SplitContainer2.Panel2.Controls.Add(Me.IGrid2)
    Me.SplitContainer2.Size = New System.Drawing.Size(811, 196)
    Me.SplitContainer2.SplitterDistance = 215
    Me.SplitContainer2.TabIndex = 29
    '
    'TextBox2
    '
    Me.TextBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBox2.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TextBox2.Location = New System.Drawing.Point(2, 0)
    Me.TextBox2.Multiline = True
    Me.TextBox2.Name = "TextBox2"
    Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.TextBox2.Size = New System.Drawing.Size(211, 196)
    Me.TextBox2.TabIndex = 28
    '
    'ToolStrip1
    '
    Me.ToolStrip1.AutoSize = False
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbClearTrace2})
    Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0)
    Me.ToolStrip1.Size = New System.Drawing.Size(592, 20)
    Me.ToolStrip1.TabIndex = 28
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'tsbClearTrace2
    '
    Me.tsbClearTrace2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.tsbClearTrace2.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbClearTrace2.Name = "tsbClearTrace2"
    Me.tsbClearTrace2.Size = New System.Drawing.Size(24, 17)
    Me.tsbClearTrace2.Text = "-X-"
    '
    'IGrid2
    '
    Me.IGrid2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.IGrid2.AutoResizeCols = True
    Me.IGrid2.BackColor = System.Drawing.Color.SlateGray
    IGColPattern1.AllowMoving = False
    IGColPattern1.AllowSizing = False
    IGColPattern1.Text = "Typ"
    IGColPattern1.Width = 20
    IGColPattern2.Text = "Para1"
    IGColPattern2.Width = 281
    IGColPattern3.Text = "Para2"
    IGColPattern3.Width = 286
    Me.IGrid2.Cols.AddRange(New TenTec.Windows.iGridLib.iGColPattern() {IGColPattern1, IGColPattern2, IGColPattern3})
    Me.IGrid2.ContextMenuStrip = Me.ContextMenuStrip1
    Me.IGrid2.DefaultRow.Height = 19
    Me.IGrid2.DefaultRow.NormalCellHeight = 19
    Me.IGrid2.ForeColor = System.Drawing.Color.White
    Me.IGrid2.ForeColorDisabled = System.Drawing.Color.Gainsboro
    Me.IGrid2.GridLines.Mode = TenTec.Windows.iGridLib.iGGridLinesMode.None
    Me.IGrid2.Header.AllowPress = False
    Me.IGrid2.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat
    Me.IGrid2.Header.Height = 16
    Me.IGrid2.Header.UseXPStyles = False
    Me.IGrid2.Header.Visible = False
    Me.IGrid2.ImageList = Me.iml_TraceTypes
    Me.IGrid2.Location = New System.Drawing.Point(1, 20)
    Me.IGrid2.Name = "IGrid2"
    Me.IGrid2.ReadOnly = True
    Me.IGrid2.RowMode = True
    Me.IGrid2.SelCellsBackColor = System.Drawing.Color.SteelBlue
    Me.IGrid2.SelCellsBackColorNoFocus = System.Drawing.Color.SteelBlue
    Me.IGrid2.SelCellsForeColorNoFocus = System.Drawing.SystemColors.HighlightText
    Me.IGrid2.Size = New System.Drawing.Size(591, 177)
    Me.IGrid2.TabIndex = 27
    '
    'iml_TraceTypes
    '
    Me.iml_TraceTypes.ImageStream = CType(resources.GetObject("iml_TraceTypes.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.iml_TraceTypes.TransparentColor = System.Drawing.Color.Transparent
    Me.iml_TraceTypes.Images.SetKeyName(0, "dump")
    Me.iml_TraceTypes.Images.SetKeyName(1, "event")
    Me.iml_TraceTypes.Images.SetKeyName(2, "trace")
    Me.iml_TraceTypes.Images.SetKeyName(3, "info")
    Me.iml_TraceTypes.Images.SetKeyName(4, "warn")
    Me.iml_TraceTypes.Images.SetKeyName(5, "err")
    Me.iml_TraceTypes.Images.SetKeyName(6, "ok")
    Me.iml_TraceTypes.Images.SetKeyName(7, "sys")
    Me.iml_TraceTypes.Images.SetKeyName(8, "ini")
    Me.iml_TraceTypes.Images.SetKeyName(9, "shutdown")
    '
    'TabControl1
    '
    Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left
    Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TabControl1.Controls.Add(Me.TabPage1)
    Me.TabControl1.Controls.Add(Me.TabPage3)
    Me.TabControl1.Controls.Add(Me.TabPage4)
    Me.TabControl1.Controls.Add(Me.TabPage5)
    Me.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
    Me.TabControl1.ItemSize = New System.Drawing.Size(5, 19)
    Me.TabControl1.Location = New System.Drawing.Point(0, -5)
    Me.TabControl1.Multiline = True
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.Padding = New System.Drawing.Point(1, 0)
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(838, 206)
    Me.TabControl1.TabIndex = 28
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.SplitContainer2)
    Me.TabPage1.Location = New System.Drawing.Point(23, 4)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(811, 198)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "Trace"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'TabPage3
    '
    Me.TabPage3.Controls.Add(Me.btnStdOutClear)
    Me.TabPage3.Controls.Add(Me.txtOutSource)
    Me.TabPage3.Controls.Add(Me.Label1)
    Me.TabPage3.Controls.Add(Me.btnStdOutPaste)
    Me.TabPage3.Controls.Add(Me.btnStdOutCopy)
    Me.TabPage3.Controls.Add(Me.qq_txtOutMonitor)
    Me.TabPage3.Location = New System.Drawing.Point(23, 4)
    Me.TabPage3.Name = "TabPage3"
    Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage3.Size = New System.Drawing.Size(811, 198)
    Me.TabPage3.TabIndex = 2
    Me.TabPage3.Text = "stdOUT"
    Me.TabPage3.UseVisualStyleBackColor = True
    '
    'btnStdOutClear
    '
    Me.btnStdOutClear.Image = CType(resources.GetObject("btnStdOutClear.Image"), System.Drawing.Image)
    Me.btnStdOutClear.Location = New System.Drawing.Point(200, 3)
    Me.btnStdOutClear.Name = "btnStdOutClear"
    Me.btnStdOutClear.Size = New System.Drawing.Size(90, 23)
    Me.btnStdOutClear.TabIndex = 15
    Me.btnStdOutClear.Text = "Leeren"
    Me.btnStdOutClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnStdOutClear.UseVisualStyleBackColor = True
    '
    'txtOutSource
    '
    Me.txtOutSource.Location = New System.Drawing.Point(395, 5)
    Me.txtOutSource.Name = "txtOutSource"
    Me.txtOutSource.Size = New System.Drawing.Size(240, 20)
    Me.txtOutSource.TabIndex = 14
    '
    'Label1
    '
    Me.Label1.BackColor = System.Drawing.Color.LightGray
    Me.Label1.Location = New System.Drawing.Point(301, 5)
    Me.Label1.Name = "Label1"
    Me.Label1.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
    Me.Label1.Size = New System.Drawing.Size(88, 20)
    Me.Label1.TabIndex = 13
    Me.Label1.Text = "Ausgabe von:"
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'btnStdOutPaste
    '
    Me.btnStdOutPaste.Image = CType(resources.GetObject("btnStdOutPaste.Image"), System.Drawing.Image)
    Me.btnStdOutPaste.Location = New System.Drawing.Point(104, 3)
    Me.btnStdOutPaste.Name = "btnStdOutPaste"
    Me.btnStdOutPaste.Size = New System.Drawing.Size(90, 23)
    Me.btnStdOutPaste.TabIndex = 12
    Me.btnStdOutPaste.Text = "Einfügen"
    Me.btnStdOutPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnStdOutPaste.UseVisualStyleBackColor = True
    '
    'btnStdOutCopy
    '
    Me.btnStdOutCopy.Image = CType(resources.GetObject("btnStdOutCopy.Image"), System.Drawing.Image)
    Me.btnStdOutCopy.Location = New System.Drawing.Point(8, 3)
    Me.btnStdOutCopy.Name = "btnStdOutCopy"
    Me.btnStdOutCopy.Size = New System.Drawing.Size(90, 23)
    Me.btnStdOutCopy.TabIndex = 11
    Me.btnStdOutCopy.Text = "Kopieren"
    Me.btnStdOutCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnStdOutCopy.UseVisualStyleBackColor = True
    '
    'TabPage4
    '
    Me.TabPage4.Controls.Add(Me.btnStdInPaste)
    Me.TabPage4.Controls.Add(Me.btnStdInCopy)
    Me.TabPage4.Controls.Add(Me.qq_txtInTextbox)
    Me.TabPage4.Location = New System.Drawing.Point(23, 4)
    Me.TabPage4.Name = "TabPage4"
    Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage4.Size = New System.Drawing.Size(811, 198)
    Me.TabPage4.TabIndex = 3
    Me.TabPage4.Text = "stdIN"
    Me.TabPage4.UseVisualStyleBackColor = True
    '
    'btnStdInPaste
    '
    Me.btnStdInPaste.Image = CType(resources.GetObject("btnStdInPaste.Image"), System.Drawing.Image)
    Me.btnStdInPaste.Location = New System.Drawing.Point(104, 3)
    Me.btnStdInPaste.Name = "btnStdInPaste"
    Me.btnStdInPaste.Size = New System.Drawing.Size(90, 23)
    Me.btnStdInPaste.TabIndex = 10
    Me.btnStdInPaste.Text = "Einfügen"
    Me.btnStdInPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnStdInPaste.UseVisualStyleBackColor = True
    '
    'btnStdInCopy
    '
    Me.btnStdInCopy.Image = CType(resources.GetObject("btnStdInCopy.Image"), System.Drawing.Image)
    Me.btnStdInCopy.Location = New System.Drawing.Point(8, 3)
    Me.btnStdInCopy.Name = "btnStdInCopy"
    Me.btnStdInCopy.Size = New System.Drawing.Size(90, 23)
    Me.btnStdInCopy.TabIndex = 9
    Me.btnStdInCopy.Text = "Kopieren"
    Me.btnStdInCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnStdInCopy.UseVisualStyleBackColor = True
    '
    'qq_txtInTextbox
    '
    Me.qq_txtInTextbox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.qq_txtInTextbox.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.qq_txtInTextbox.Location = New System.Drawing.Point(8, 30)
    Me.qq_txtInTextbox.Multiline = True
    Me.qq_txtInTextbox.Name = "qq_txtInTextbox"
    Me.qq_txtInTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.qq_txtInTextbox.Size = New System.Drawing.Size(792, 163)
    Me.qq_txtInTextbox.TabIndex = 8
    Me.qq_txtInTextbox.WordWrap = False
    '
    'TabPage5
    '
    Me.TabPage5.Controls.Add(Me.ListView2)
    Me.TabPage5.Controls.Add(Me.ListView1)
    Me.TabPage5.Controls.Add(Me.Button6)
    Me.TabPage5.Controls.Add(Me.Button8)
    Me.TabPage5.Location = New System.Drawing.Point(23, 4)
    Me.TabPage5.Name = "TabPage5"
    Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage5.Size = New System.Drawing.Size(811, 198)
    Me.TabPage5.TabIndex = 4
    Me.TabPage5.Text = "class"
    Me.TabPage5.UseVisualStyleBackColor = True
    '
    'ListView2
    '
    Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader6})
    Me.ListView2.Location = New System.Drawing.Point(7, 38)
    Me.ListView2.Name = "ListView2"
    Me.ListView2.Size = New System.Drawing.Size(414, 154)
    Me.ListView2.TabIndex = 10
    Me.ListView2.UseCompatibleStateImageBehavior = False
    Me.ListView2.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader3
    '
    Me.ColumnHeader3.Text = "className"
    Me.ColumnHeader3.Width = 84
    '
    'ColumnHeader4
    '
    Me.ColumnHeader4.Text = "multiInst"
    '
    'ColumnHeader6
    '
    Me.ColumnHeader6.Text = "path"
    Me.ColumnHeader6.Width = 241
    '
    'ListView1
    '
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
    Me.ListView1.Location = New System.Drawing.Point(7, 207)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(414, 149)
    Me.ListView1.TabIndex = 9
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "className"
    Me.ColumnHeader1.Width = 101
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "path"
    Me.ColumnHeader2.Width = 205
    '
    'timer_fadeOutErrBox
    '
    '
    'checkTraceAutoscroll
    '
    Me.checkTraceAutoscroll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.checkTraceAutoscroll.AutoSize = True
    Me.checkTraceAutoscroll.Checked = True
    Me.checkTraceAutoscroll.CheckState = System.Windows.Forms.CheckState.Checked
    Me.checkTraceAutoscroll.Location = New System.Drawing.Point(5, 200)
    Me.checkTraceAutoscroll.Name = "checkTraceAutoscroll"
    Me.checkTraceAutoscroll.Size = New System.Drawing.Size(15, 14)
    Me.checkTraceAutoscroll.TabIndex = 29
    Me.ToolTip1.SetToolTip(Me.checkTraceAutoscroll, "Trace automatisch scrollen")
    Me.checkTraceAutoscroll.UseVisualStyleBackColor = True
    '
    'timer_autoScroll
    '
    Me.timer_autoScroll.Enabled = True
    '
    'frmTB_debug
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(835, 218)
    Me.Controls.Add(Me.qq_check_silentMode)
    Me.Controls.Add(Me.checkTraceStack)
    Me.Controls.Add(Me.checkTraceAutoscroll)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.TabControl1)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_debug"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
    Me.Text = "ScriptHostLib"
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.ContextMenuStrip1.ResumeLayout(False)
    Me.SplitContainer2.Panel1.ResumeLayout(False)
    Me.SplitContainer2.Panel1.PerformLayout()
    Me.SplitContainer2.Panel2.ResumeLayout(False)
    Me.SplitContainer2.ResumeLayout(False)
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    CType(Me.IGrid2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabControl1.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    Me.TabPage3.ResumeLayout(False)
    Me.TabPage3.PerformLayout()
    Me.TabPage4.ResumeLayout(False)
    Me.TabPage4.PerformLayout()
    Me.TabPage5.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Button8 As System.Windows.Forms.Button
  Friend WithEvents Button6 As System.Windows.Forms.Button
  Friend WithEvents checkTraceStack As System.Windows.Forms.CheckBox
  Friend WithEvents qq_check_silentMode As System.Windows.Forms.CheckBox
  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents lblRunning As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents KopierenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents resetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents IGrid2 As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
  Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
  Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
  Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
  Friend WithEvents IGrid3Col3CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid3Col3ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid3Col2CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid3Col2ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid3Col3CellStyle1 As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid3Col3ColHdrStyle1 As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid3Col4CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid3Col4ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid3Col5CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid3Col5ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents txtOutSource As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents btnStdOutPaste As System.Windows.Forms.Button
  Friend WithEvents btnStdOutCopy As System.Windows.Forms.Button
  Friend WithEvents btnStdInPaste As System.Windows.Forms.Button
  Friend WithEvents btnStdInCopy As System.Windows.Forms.Button
  Friend WithEvents qq_txtInTextbox As System.Windows.Forms.TextBox
  Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
  Private WithEvents qq_txtOutMonitor As System.Windows.Forms.TextBox
  Friend WithEvents iml_TraceTypes As System.Windows.Forms.ImageList
  Friend WithEvents timer_fadeOutErrBox As System.Windows.Forms.Timer
  Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
  Friend WithEvents lnkTraceMonitor As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents checkTraceAutoscroll As System.Windows.Forms.CheckBox
  Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
  Friend WithEvents btnStdOutClear As System.Windows.Forms.Button
  Friend WithEvents timer_autoScroll As System.Windows.Forms.Timer
  Friend WithEvents tsbTab_0 As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents tsbTab_1 As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents tsbTab_2 As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents tsbTab_3 As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ListView2 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Public WithEvents TabControl1 As System.Windows.Forms.TabControl
  Friend WithEvents NavigateCodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents tsbClearTrace2 As System.Windows.Forms.ToolStripButton
End Class
