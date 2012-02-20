<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_windowManager
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_windowManager))
    Dim IGColPattern1 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern2 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern3 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern4 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern5 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Me.IGrid1Col3CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1Col3ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.igAddInsCol4CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igAddInsCol4ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.igAddInsCol2CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igAddInsCol2ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.IGrid1Col1CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.IGrid1Col1ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.igAddInsCol3CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igAddInsCol3ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.TreeView1 = New System.Windows.Forms.TreeView
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.btnOK = New System.Windows.Forms.Button
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.Button2 = New System.Windows.Forms.Button
    Me.TabControl1 = New System.Windows.Forms.TabControl
    Me.TabPage1 = New System.Windows.Forms.TabPage
    Me.TabControl2 = New System.Windows.Forms.TabControl
    Me.TabPage8 = New System.Windows.Forms.TabPage
    Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
    Me.lnkOpenOwnWindow = New System.Windows.Forms.LinkLabel
    Me.TabPage9 = New System.Windows.Forms.TabPage
    Me.lvOpenedWins = New System.Windows.Forms.ListView
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
    Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
    Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
    Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
    Me.TabPage2 = New System.Windows.Forms.TabPage
    Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
    Me.labAddinAuthor = New System.Windows.Forms.Label
    Me.grpAddinDetails = New System.Windows.Forms.GroupBox
    Me.labAddinDetails = New System.Windows.Forms.Label
    Me.lnkAddAddin = New System.Windows.Forms.LinkLabel
    Me.igAddIns = New TenTec.Windows.iGridLib.iGrid
    Me.imlAddinIcons = New System.Windows.Forms.ImageList(Me.components)
    Me.PictureBox2 = New System.Windows.Forms.PictureBox
    Me.TabPage3 = New System.Windows.Forms.TabPage
    Me.TabPage5 = New System.Windows.Forms.TabPage
    Me.tbpAddinOptions = New System.Windows.Forms.TabPage
    Me.ScriptedPanel1 = New ScriptIDE.ScriptWindowHelper.ScriptedPanel
    Me.TabPage4 = New System.Windows.Forms.TabPage
    Me.lvGroupHome = New System.Windows.Forms.ListView
    Me.TabPage6 = New System.Windows.Forms.TabPage
    Me.lnkHelpWinSwitcher = New System.Windows.Forms.LinkLabel
    Me.Button1 = New System.Windows.Forms.Button
    Me.scWinSwitch = New ScintillaNet.Scintilla
    Me.TabPage7 = New System.Windows.Forms.TabPage
    Me.Label1 = New System.Windows.Forms.Label
    Me.GroupBox2 = New System.Windows.Forms.GroupBox
    Me.lvHome = New System.Windows.Forms.ListView
    Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
    Me.imlTabImages = New System.Windows.Forms.ImageList(Me.components)
    Me.Label3 = New System.Windows.Forms.Label
    Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
    Me.TreeView2 = New System.Windows.Forms.TreeView
    Me.pnlTitleBar = New System.Windows.Forms.Panel
    Me.LinkLabel3 = New System.Windows.Forms.LinkLabel
    Me.Panel1.SuspendLayout()
    Me.TabControl1.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    Me.TabControl2.SuspendLayout()
    Me.TabPage8.SuspendLayout()
    Me.TabPage9.SuspendLayout()
    Me.ToolStrip1.SuspendLayout()
    Me.TabPage2.SuspendLayout()
    Me.grpAddinDetails.SuspendLayout()
    CType(Me.igAddIns, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tbpAddinOptions.SuspendLayout()
    Me.TabPage4.SuspendLayout()
    Me.TabPage6.SuspendLayout()
    CType(Me.scWinSwitch, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage7.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.SuspendLayout()
    '
    'IGrid1Col3CellStyle
    '
    Me.IGrid1Col3CellStyle.ReadOnly = TenTec.Windows.iGridLib.iGBool.[True]
    '
    'IGrid1Col1CellStyle
    '
    Me.IGrid1Col1CellStyle.Type = TenTec.Windows.iGridLib.iGCellType.Check
    '
    'TreeView1
    '
    Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeView1.ImageIndex = 0
    Me.TreeView1.ImageList = Me.ImageList1
    Me.TreeView1.Location = New System.Drawing.Point(9, 8)
    Me.TreeView1.Name = "TreeView1"
    Me.TreeView1.SelectedImageIndex = 0
    Me.TreeView1.Size = New System.Drawing.Size(412, 186)
    Me.TreeView1.TabIndex = 0
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList1.Images.SetKeyName(0, "mwsidebar")
    Me.ImageList1.Images.SetKeyName(1, "msscript_101.ico")
    Me.ImageList1.Images.SetKeyName(2, "devenv")
    Me.ImageList1.Images.SetKeyName(3, "toolbox")
    Me.ImageList1.Images.SetKeyName(4, "dev")
    Me.ImageList1.Images.SetKeyName(5, "form")
    Me.ImageList1.Images.SetKeyName(6, "class")
    Me.ImageList1.Images.SetKeyName(7, "win")
    Me.ImageList1.Images.SetKeyName(8, "exc")
    Me.ImageList1.Images.SetKeyName(9, "dll")
    Me.ImageList1.Images.SetKeyName(10, "wscript")
    Me.ImageList1.Images.SetKeyName(11, "scriptwindows")
    Me.ImageList1.Images.SetKeyName(12, "scriptwindows2")
    Me.ImageList1.Images.SetKeyName(13, "defaultwindow")
    Me.ImageList1.Images.SetKeyName(14, "explore")
    Me.ImageList1.Images.SetKeyName(15, "cmd")
    Me.ImageList1.Images.SetKeyName(16, "table")
    '
    'btnOK
    '
    Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOK.Location = New System.Drawing.Point(328, 274)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.Size = New System.Drawing.Size(93, 25)
    Me.btnOK.TabIndex = 1
    Me.btnOK.Text = "Anzeigen"
    Me.btnOK.UseVisualStyleBackColor = True
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.BackColor = System.Drawing.SystemColors.Control
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.Panel1.Controls.Add(Me.TextBox1)
    Me.Panel1.Location = New System.Drawing.Point(9, 201)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(412, 66)
    Me.Panel1.TabIndex = 3
    '
    'TextBox1
    '
    Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.TextBox1.Location = New System.Drawing.Point(9, 5)
    Me.TextBox1.Multiline = True
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.ReadOnly = True
    Me.TextBox1.Size = New System.Drawing.Size(393, 54)
    Me.TextBox1.TabIndex = 0
    '
    'Button2
    '
    Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Button2.Location = New System.Drawing.Point(9, 274)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(82, 25)
    Me.Button2.TabIndex = 5
    Me.Button2.Text = "Neu laden"
    Me.Button2.UseVisualStyleBackColor = True
    '
    'TabControl1
    '
    Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TabControl1.Controls.Add(Me.TabPage1)
    Me.TabControl1.Controls.Add(Me.TabPage2)
    Me.TabControl1.Controls.Add(Me.TabPage3)
    Me.TabControl1.Controls.Add(Me.TabPage5)
    Me.TabControl1.Controls.Add(Me.tbpAddinOptions)
    Me.TabControl1.Controls.Add(Me.TabPage4)
    Me.TabControl1.Controls.Add(Me.TabPage6)
    Me.TabControl1.Controls.Add(Me.TabPage7)
    Me.TabControl1.Location = New System.Drawing.Point(149, 18)
    Me.TabControl1.Name = "TabControl1"
    Me.TabControl1.SelectedIndex = 0
    Me.TabControl1.Size = New System.Drawing.Size(469, 362)
    Me.TabControl1.TabIndex = 6
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.TabControl2)
    Me.TabPage1.Location = New System.Drawing.Point(4, 22)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(461, 336)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "Fenster"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'TabControl2
    '
    Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TabControl2.Controls.Add(Me.TabPage8)
    Me.TabControl2.Controls.Add(Me.TabPage9)
    Me.TabControl2.Location = New System.Drawing.Point(14, 8)
    Me.TabControl2.Name = "TabControl2"
    Me.TabControl2.SelectedIndex = 0
    Me.TabControl2.Size = New System.Drawing.Size(443, 330)
    Me.TabControl2.TabIndex = 8
    '
    'TabPage8
    '
    Me.TabPage8.Controls.Add(Me.Panel1)
    Me.TabPage8.Controls.Add(Me.LinkLabel2)
    Me.TabPage8.Controls.Add(Me.TreeView1)
    Me.TabPage8.Controls.Add(Me.lnkOpenOwnWindow)
    Me.TabPage8.Controls.Add(Me.btnOK)
    Me.TabPage8.Controls.Add(Me.Button2)
    Me.TabPage8.Location = New System.Drawing.Point(4, 22)
    Me.TabPage8.Name = "TabPage8"
    Me.TabPage8.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage8.Size = New System.Drawing.Size(435, 304)
    Me.TabPage8.TabIndex = 0
    Me.TabPage8.Text = "Neue Fenster anzeigen"
    Me.TabPage8.UseVisualStyleBackColor = True
    '
    'LinkLabel2
    '
    Me.LinkLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LinkLabel2.AutoSize = True
    Me.LinkLabel2.LinkColor = System.Drawing.Color.SteelBlue
    Me.LinkLabel2.Location = New System.Drawing.Point(213, 280)
    Me.LinkLabel2.Name = "LinkLabel2"
    Me.LinkLabel2.Size = New System.Drawing.Size(67, 13)
    Me.LinkLabel2.TabIndex = 7
    Me.LinkLabel2.TabStop = True
    Me.LinkLabel2.Text = "Zurückholen"
    '
    'lnkOpenOwnWindow
    '
    Me.lnkOpenOwnWindow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lnkOpenOwnWindow.AutoSize = True
    Me.lnkOpenOwnWindow.LinkColor = System.Drawing.Color.SteelBlue
    Me.lnkOpenOwnWindow.Location = New System.Drawing.Point(286, 280)
    Me.lnkOpenOwnWindow.Name = "lnkOpenOwnWindow"
    Me.lnkOpenOwnWindow.Size = New System.Drawing.Size(36, 13)
    Me.lnkOpenOwnWindow.TabIndex = 6
    Me.lnkOpenOwnWindow.TabStop = True
    Me.lnkOpenOwnWindow.Text = "test(2)"
    '
    'TabPage9
    '
    Me.TabPage9.Controls.Add(Me.lvOpenedWins)
    Me.TabPage9.Controls.Add(Me.ToolStrip1)
    Me.TabPage9.Controls.Add(Me.PropertyGrid1)
    Me.TabPage9.Location = New System.Drawing.Point(4, 22)
    Me.TabPage9.Name = "TabPage9"
    Me.TabPage9.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage9.Size = New System.Drawing.Size(435, 304)
    Me.TabPage9.TabIndex = 1
    Me.TabPage9.Text = "Geöffnete Fenster"
    Me.TabPage9.UseVisualStyleBackColor = True
    '
    'lvOpenedWins
    '
    Me.lvOpenedWins.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvOpenedWins.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader5, Me.ColumnHeader4, Me.ColumnHeader6})
    Me.lvOpenedWins.Location = New System.Drawing.Point(6, 29)
    Me.lvOpenedWins.MultiSelect = False
    Me.lvOpenedWins.Name = "lvOpenedWins"
    Me.lvOpenedWins.Size = New System.Drawing.Size(206, 268)
    Me.lvOpenedWins.TabIndex = 0
    Me.lvOpenedWins.UseCompatibleStateImageBehavior = False
    Me.lvOpenedWins.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Titel"
    Me.ColumnHeader2.Width = 83
    '
    'ColumnHeader5
    '
    Me.ColumnHeader5.Text = "ID"
    Me.ColumnHeader5.Width = 140
    '
    'ColumnHeader4
    '
    Me.ColumnHeader4.Text = "DockState"
    Me.ColumnHeader4.Width = 71
    '
    'ColumnHeader6
    '
    Me.ColumnHeader6.Text = "Type"
    '
    'ToolStrip1
    '
    Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ToolStrip1.AutoSize = False
    Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
    Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton2, Me.ToolStripButton4, Me.ToolStripButton1, Me.ToolStripButton3})
    Me.ToolStrip1.Location = New System.Drawing.Point(6, 6)
    Me.ToolStrip1.Name = "ToolStrip1"
    Me.ToolStrip1.Size = New System.Drawing.Size(206, 25)
    Me.ToolStrip1.TabIndex = 2
    Me.ToolStrip1.Text = "ToolStrip1"
    '
    'ToolStripButton2
    '
    Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
    Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton2.Name = "ToolStripButton2"
    Me.ToolStripButton2.Size = New System.Drawing.Size(70, 22)
    Me.ToolStripButton2.Text = "Sichtbar"
    '
    'ToolStripButton4
    '
    Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
    Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton4.Name = "ToolStripButton4"
    Me.ToolStripButton4.Size = New System.Drawing.Size(87, 22)
    Me.ToolStripButton4.Text = "ID kopieren"
    '
    'ToolStripButton1
    '
    Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
    Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton1.Name = "ToolStripButton1"
    Me.ToolStripButton1.Size = New System.Drawing.Size(185, 20)
    Me.ToolStripButton1.Text = "An Standardposition anzeigen"
    '
    'ToolStripButton3
    '
    Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
    Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton3.Name = "ToolStripButton3"
    Me.ToolStripButton3.Size = New System.Drawing.Size(78, 20)
    Me.ToolStripButton3.Text = "Schließen"
    '
    'PropertyGrid1
    '
    Me.PropertyGrid1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PropertyGrid1.Location = New System.Drawing.Point(218, 5)
    Me.PropertyGrid1.Name = "PropertyGrid1"
    Me.PropertyGrid1.Size = New System.Drawing.Size(207, 292)
    Me.PropertyGrid1.TabIndex = 1
    '
    'TabPage2
    '
    Me.TabPage2.Controls.Add(Me.LinkLabel3)
    Me.TabPage2.Controls.Add(Me.LinkLabel1)
    Me.TabPage2.Controls.Add(Me.labAddinAuthor)
    Me.TabPage2.Controls.Add(Me.grpAddinDetails)
    Me.TabPage2.Controls.Add(Me.lnkAddAddin)
    Me.TabPage2.Controls.Add(Me.igAddIns)
    Me.TabPage2.Controls.Add(Me.PictureBox2)
    Me.TabPage2.Location = New System.Drawing.Point(4, 22)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage2.Size = New System.Drawing.Size(461, 336)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "AddIns"
    Me.TabPage2.UseVisualStyleBackColor = True
    '
    'LinkLabel1
    '
    Me.LinkLabel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LinkLabel1.LinkArea = New System.Windows.Forms.LinkArea(206, 5)
    Me.LinkLabel1.Location = New System.Drawing.Point(92, 12)
    Me.LinkLabel1.Name = "LinkLabel1"
    Me.LinkLabel1.Size = New System.Drawing.Size(356, 51)
    Me.LinkLabel1.TabIndex = 8
    Me.LinkLabel1.TabStop = True
    Me.LinkLabel1.Text = resources.GetString("LinkLabel1.Text")
    Me.LinkLabel1.UseCompatibleTextRendering = True
    '
    'labAddinAuthor
    '
    Me.labAddinAuthor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labAddinAuthor.AutoSize = True
    Me.labAddinAuthor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.labAddinAuthor.Location = New System.Drawing.Point(358, 258)
    Me.labAddinAuthor.Name = "labAddinAuthor"
    Me.labAddinAuthor.Size = New System.Drawing.Size(77, 13)
    Me.labAddinAuthor.TabIndex = 6
    Me.labAddinAuthor.Text = "Autor: abcdefg"
    Me.labAddinAuthor.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'grpAddinDetails
    '
    Me.grpAddinDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpAddinDetails.Controls.Add(Me.labAddinDetails)
    Me.grpAddinDetails.Location = New System.Drawing.Point(18, 258)
    Me.grpAddinDetails.Name = "grpAddinDetails"
    Me.grpAddinDetails.Size = New System.Drawing.Size(430, 79)
    Me.grpAddinDetails.TabIndex = 7
    Me.grpAddinDetails.TabStop = False
    Me.grpAddinDetails.Text = "GroupBox2"
    '
    'labAddinDetails
    '
    Me.labAddinDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labAddinDetails.Location = New System.Drawing.Point(8, 18)
    Me.labAddinDetails.Name = "labAddinDetails"
    Me.labAddinDetails.Size = New System.Drawing.Size(402, 54)
    Me.labAddinDetails.TabIndex = 5
    Me.labAddinDetails.Text = "Label5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "bbb" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ccc" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ddd"
    '
    'lnkAddAddin
    '
    Me.lnkAddAddin.AutoSize = True
    Me.lnkAddAddin.Location = New System.Drawing.Point(92, 70)
    Me.lnkAddAddin.Name = "lnkAddAddin"
    Me.lnkAddAddin.Size = New System.Drawing.Size(105, 13)
    Me.lnkAddAddin.TabIndex = 3
    Me.lnkAddAddin.TabStop = True
    Me.lnkAddAddin.Text = "Add-in-Ordner öffnen"
    '
    'igAddIns
    '
    Me.igAddIns.AllowDrop = True
    Me.igAddIns.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    IGColPattern1.CellStyle = Me.IGrid1Col3CellStyle
    IGColPattern1.ColHdrStyle = Me.IGrid1Col3ColHdrStyle
    IGColPattern1.Text = "Name"
    IGColPattern1.Width = 242
    IGColPattern2.CellStyle = Me.igAddInsCol4CellStyle
    IGColPattern2.ColHdrStyle = Me.igAddInsCol4ColHdrStyle
    IGColPattern2.Text = "ID"
    IGColPattern2.Width = 0
    IGColPattern3.CellStyle = Me.igAddInsCol2CellStyle
    IGColPattern3.ColHdrStyle = Me.igAddInsCol2ColHdrStyle
    IGColPattern3.Text = "Typ"
    IGColPattern3.Width = 49
    IGColPattern4.CellStyle = Me.IGrid1Col1CellStyle
    IGColPattern4.ColHdrStyle = Me.IGrid1Col1ColHdrStyle
    IGColPattern4.MaxWidth = 53
    IGColPattern4.MinWidth = 20
    IGColPattern4.Text = "Beim Start laden"
    IGColPattern4.Width = 53
    IGColPattern5.CellStyle = Me.igAddInsCol3CellStyle
    IGColPattern5.ColHdrStyle = Me.igAddInsCol3ColHdrStyle
    IGColPattern5.Text = "Status"
    Me.igAddIns.Cols.AddRange(New TenTec.Windows.iGridLib.iGColPattern() {IGColPattern1, IGColPattern2, IGColPattern3, IGColPattern4, IGColPattern5})
    Me.igAddIns.DefaultRow.Height = 20
    Me.igAddIns.DefaultRow.NormalCellHeight = 20
    Me.igAddIns.Header.Height = 19
    Me.igAddIns.ImageList = Me.imlAddinIcons
    Me.igAddIns.Location = New System.Drawing.Point(18, 91)
    Me.igAddIns.Name = "igAddIns"
    Me.igAddIns.RowMode = True
    Me.igAddIns.Size = New System.Drawing.Size(430, 158)
    Me.igAddIns.TabIndex = 2
    '
    'imlAddinIcons
    '
    Me.imlAddinIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.imlAddinIcons.ImageSize = New System.Drawing.Size(16, 16)
    Me.imlAddinIcons.TransparentColor = System.Drawing.Color.Transparent
    '
    'PictureBox2
    '
    Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
    Me.PictureBox2.Location = New System.Drawing.Point(18, 20)
    Me.PictureBox2.Name = "PictureBox2"
    Me.PictureBox2.Size = New System.Drawing.Size(64, 64)
    Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox2.TabIndex = 1
    Me.PictureBox2.TabStop = False
    '
    'TabPage3
    '
    Me.TabPage3.Location = New System.Drawing.Point(4, 22)
    Me.TabPage3.Name = "TabPage3"
    Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage3.Size = New System.Drawing.Size(461, 336)
    Me.TabPage3.TabIndex = 8
    Me.TabPage3.Text = "xx"
    Me.TabPage3.UseVisualStyleBackColor = True
    '
    'TabPage5
    '
    Me.TabPage5.Location = New System.Drawing.Point(4, 22)
    Me.TabPage5.Name = "TabPage5"
    Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage5.Size = New System.Drawing.Size(461, 336)
    Me.TabPage5.TabIndex = 4
    Me.TabPage5.Text = "xx"
    Me.TabPage5.UseVisualStyleBackColor = True
    '
    'tbpAddinOptions
    '
    Me.tbpAddinOptions.Controls.Add(Me.ScriptedPanel1)
    Me.tbpAddinOptions.Location = New System.Drawing.Point(4, 22)
    Me.tbpAddinOptions.Name = "tbpAddinOptions"
    Me.tbpAddinOptions.Padding = New System.Windows.Forms.Padding(3)
    Me.tbpAddinOptions.Size = New System.Drawing.Size(461, 336)
    Me.tbpAddinOptions.TabIndex = 5
    Me.tbpAddinOptions.Text = "addinUC"
    Me.tbpAddinOptions.UseVisualStyleBackColor = True
    '
    'ScriptedPanel1
    '
    Me.ScriptedPanel1.activateEvents = ""
    Me.ScriptedPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ScriptedPanel1.AutoScroll = True
    Me.ScriptedPanel1.ClassName = Nothing
    Me.ScriptedPanel1.direction = 1
    Me.ScriptedPanel1.insertX = 0
    Me.ScriptedPanel1.insertY = 0
    Me.ScriptedPanel1.Location = New System.Drawing.Point(18, 8)
    Me.ScriptedPanel1.Name = "ScriptedPanel1"
    Me.ScriptedPanel1.offsetX = 0
    Me.ScriptedPanel1.Size = New System.Drawing.Size(430, 331)
    Me.ScriptedPanel1.TabIndex = 1
    Me.ScriptedPanel1.WinID = Nothing
    '
    'TabPage4
    '
    Me.TabPage4.Controls.Add(Me.lvGroupHome)
    Me.TabPage4.Location = New System.Drawing.Point(4, 22)
    Me.TabPage4.Name = "TabPage4"
    Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage4.Size = New System.Drawing.Size(461, 336)
    Me.TabPage4.TabIndex = 3
    Me.TabPage4.Text = "group"
    Me.TabPage4.UseVisualStyleBackColor = True
    '
    'lvGroupHome
    '
    Me.lvGroupHome.Activation = System.Windows.Forms.ItemActivation.OneClick
    Me.lvGroupHome.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvGroupHome.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.lvGroupHome.HotTracking = True
    Me.lvGroupHome.HoverSelection = True
    Me.lvGroupHome.Location = New System.Drawing.Point(21, 12)
    Me.lvGroupHome.MultiSelect = False
    Me.lvGroupHome.Name = "lvGroupHome"
    Me.lvGroupHome.Size = New System.Drawing.Size(424, 316)
    Me.lvGroupHome.TabIndex = 0
    Me.lvGroupHome.UseCompatibleStateImageBehavior = False
    '
    'TabPage6
    '
    Me.TabPage6.Controls.Add(Me.lnkHelpWinSwitcher)
    Me.TabPage6.Controls.Add(Me.Button1)
    Me.TabPage6.Controls.Add(Me.scWinSwitch)
    Me.TabPage6.Location = New System.Drawing.Point(4, 22)
    Me.TabPage6.Name = "TabPage6"
    Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage6.Size = New System.Drawing.Size(461, 336)
    Me.TabPage6.TabIndex = 6
    Me.TabPage6.Text = "winSwitch"
    Me.TabPage6.UseVisualStyleBackColor = True
    '
    'lnkHelpWinSwitcher
    '
    Me.lnkHelpWinSwitcher.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lnkHelpWinSwitcher.AutoSize = True
    Me.lnkHelpWinSwitcher.Location = New System.Drawing.Point(253, 310)
    Me.lnkHelpWinSwitcher.Name = "lnkHelpWinSwitcher"
    Me.lnkHelpWinSwitcher.Size = New System.Drawing.Size(176, 13)
    Me.lnkHelpWinSwitcher.TabIndex = 4
    Me.lnkHelpWinSwitcher.TabStop = True
    Me.lnkHelpWinSwitcher.Text = "Hilfe zur Fensterschnellumschaltung"
    '
    'Button1
    '
    Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Button1.Location = New System.Drawing.Point(13, 305)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(75, 23)
    Me.Button1.TabIndex = 3
    Me.Button1.Text = "Apply"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'scWinSwitch
    '
    Me.scWinSwitch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.scWinSwitch.Indentation.SmartIndentType = ScintillaNet.SmartIndent.Simple
    Me.scWinSwitch.Indentation.TabWidth = 20
    Me.scWinSwitch.Location = New System.Drawing.Point(14, 8)
    Me.scWinSwitch.Name = "scWinSwitch"
    Me.scWinSwitch.Size = New System.Drawing.Size(415, 291)
    Me.scWinSwitch.Styles.BraceBad.FontName = "Verdana"
    Me.scWinSwitch.Styles.BraceLight.FontName = "Verdana"
    Me.scWinSwitch.Styles.CallTip.FontName = "Tahoma"
    Me.scWinSwitch.Styles.CallTip.Size = 8.25!
    Me.scWinSwitch.Styles.ControlChar.FontName = "Verdana"
    Me.scWinSwitch.Styles.Default.FontName = "Verdana"
    Me.scWinSwitch.Styles.IndentGuide.FontName = "Verdana"
    Me.scWinSwitch.Styles.LastPredefined.FontName = "Verdana"
    Me.scWinSwitch.Styles.LineNumber.FontName = "Verdana"
    Me.scWinSwitch.Styles.Max.FontName = "Verdana"
    Me.scWinSwitch.TabIndex = 2
    '
    'TabPage7
    '
    Me.TabPage7.Controls.Add(Me.Label1)
    Me.TabPage7.Controls.Add(Me.GroupBox2)
    Me.TabPage7.Location = New System.Drawing.Point(4, 22)
    Me.TabPage7.Name = "TabPage7"
    Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage7.Size = New System.Drawing.Size(461, 336)
    Me.TabPage7.TabIndex = 7
    Me.TabPage7.Text = "home"
    Me.TabPage7.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.Location = New System.Drawing.Point(9, 302)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(147, 18)
    Me.Label1.TabIndex = 7
    Me.Label1.Text = "Programmversion:"
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.lvHome)
    Me.GroupBox2.Controls.Add(Me.Label3)
    Me.GroupBox2.Location = New System.Drawing.Point(12, 15)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(378, 271)
    Me.GroupBox2.TabIndex = 5
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Willkommen!"
    '
    'lvHome
    '
    Me.lvHome.Activation = System.Windows.Forms.ItemActivation.OneClick
    Me.lvHome.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lvHome.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader3})
    Me.lvHome.LargeImageList = Me.imlTabImages
    Me.lvHome.Location = New System.Drawing.Point(15, 86)
    Me.lvHome.MultiSelect = False
    Me.lvHome.Name = "lvHome"
    Me.lvHome.Size = New System.Drawing.Size(350, 166)
    Me.lvHome.TabIndex = 3
    Me.lvHome.TileSize = New System.Drawing.Size(300, 36)
    Me.lvHome.UseCompatibleStateImageBehavior = False
    Me.lvHome.View = System.Windows.Forms.View.Tile
    '
    'imlTabImages
    '
    Me.imlTabImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.imlTabImages.ImageSize = New System.Drawing.Size(32, 32)
    Me.imlTabImages.TransparentColor = System.Drawing.Color.Transparent
    '
    'Label3
    '
    Me.Label3.Location = New System.Drawing.Point(15, 30)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(338, 66)
    Me.Label3.TabIndex = 0
    Me.Label3.Text = "Auf den folgenden Seiten kannst du die ScriptIDE konfigurieren." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Hier siehst du d" & _
        "ie Kategorien, in die die Einstellungsseiten unterteilt sind:"
    '
    'imlToolbarIcons
    '
    Me.imlToolbarIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.imlToolbarIcons.ImageSize = New System.Drawing.Size(16, 16)
    Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.Transparent
    '
    'TreeView2
    '
    Me.TreeView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.TreeView2.HideSelection = False
    Me.TreeView2.HotTracking = True
    Me.TreeView2.Location = New System.Drawing.Point(8, 48)
    Me.TreeView2.Name = "TreeView2"
    Me.TreeView2.ShowRootLines = False
    Me.TreeView2.Size = New System.Drawing.Size(132, 322)
    Me.TreeView2.TabIndex = 7
    '
    'pnlTitleBar
    '
    Me.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnlTitleBar.Location = New System.Drawing.Point(0, 0)
    Me.pnlTitleBar.Name = "pnlTitleBar"
    Me.pnlTitleBar.Size = New System.Drawing.Size(614, 42)
    Me.pnlTitleBar.TabIndex = 8
    '
    'LinkLabel3
    '
    Me.LinkLabel3.AutoSize = True
    Me.LinkLabel3.Location = New System.Drawing.Point(242, 71)
    Me.LinkLabel3.Name = "LinkLabel3"
    Me.LinkLabel3.Size = New System.Drawing.Size(105, 13)
    Me.LinkLabel3.TabIndex = 9
    Me.LinkLabel3.TabStop = True
    Me.LinkLabel3.Text = "Add-in-Ordner öffnen"
    '
    'frm_windowManager
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(614, 384)
    Me.Controls.Add(Me.TabControl1)
    Me.Controls.Add(Me.TreeView2)
    Me.Controls.Add(Me.pnlTitleBar)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(590, 420)
    Me.Name = "frm_windowManager"
    Me.Text = "Konfiguration"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.TabControl1.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    Me.TabControl2.ResumeLayout(False)
    Me.TabPage8.ResumeLayout(False)
    Me.TabPage8.PerformLayout()
    Me.TabPage9.ResumeLayout(False)
    Me.ToolStrip1.ResumeLayout(False)
    Me.ToolStrip1.PerformLayout()
    Me.TabPage2.ResumeLayout(False)
    Me.TabPage2.PerformLayout()
    Me.grpAddinDetails.ResumeLayout(False)
    CType(Me.igAddIns, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tbpAddinOptions.ResumeLayout(False)
    Me.TabPage4.ResumeLayout(False)
    Me.TabPage6.ResumeLayout(False)
    Me.TabPage6.PerformLayout()
    CType(Me.scWinSwitch, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage7.ResumeLayout(False)
    Me.TabPage7.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
  Friend WithEvents btnOK As System.Windows.Forms.Button
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
  Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
  Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
  Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
  Friend WithEvents igAddIns As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents IGrid1Col3CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1Col3ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents IGrid1Col1CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents IGrid1Col1ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
  Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
  Friend WithEvents tbpAddinOptions As System.Windows.Forms.TabPage
  Friend WithEvents TreeView2 As System.Windows.Forms.TreeView
  Friend WithEvents ScriptedPanel1 As ScriptWindowHelper.ScriptedPanel
  Friend WithEvents imlAddinIcons As System.Windows.Forms.ImageList
  Friend WithEvents lnkOpenOwnWindow As System.Windows.Forms.LinkLabel
  Friend WithEvents lnkAddAddin As System.Windows.Forms.LinkLabel
  Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
  Friend WithEvents scWinSwitch As ScintillaNet.Scintilla
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents lnkHelpWinSwitcher As System.Windows.Forms.LinkLabel
  Friend WithEvents pnlTitleBar As System.Windows.Forms.Panel
  Friend WithEvents lvGroupHome As System.Windows.Forms.ListView
  Friend WithEvents imlTabImages As System.Windows.Forms.ImageList
  Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents lvHome As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
  Friend WithEvents labAddinDetails As System.Windows.Forms.Label
  Friend WithEvents grpAddinDetails As System.Windows.Forms.GroupBox
  Friend WithEvents labAddinAuthor As System.Windows.Forms.Label
  Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
  Friend WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
  Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
  Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
  Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
  Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
  Friend WithEvents lvOpenedWins As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
  Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
  Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
  Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents igAddInsCol2CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igAddInsCol2ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents igAddInsCol3CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igAddInsCol3ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents igAddInsCol4CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igAddInsCol4ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
End Class
