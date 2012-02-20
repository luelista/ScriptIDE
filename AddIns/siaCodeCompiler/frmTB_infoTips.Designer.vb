<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_infoTips
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_infoTips))
    Me.ComboBox2 = New System.Windows.Forms.ComboBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.Button1 = New System.Windows.Forms.Button
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.Button2 = New System.Windows.Forms.Button
    Me.Button3 = New System.Windows.Forms.Button
    Me.btnOpenAssembly = New System.Windows.Forms.Button
    Me.labClassName = New System.Windows.Forms.Label
    Me.Button5 = New System.Windows.Forms.Button
    Me.Button6 = New System.Windows.Forms.Button
    Me.btnSCList = New System.Windows.Forms.Button
    Me.labActAssembly = New System.Windows.Forms.Label
    Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
    Me.Button4 = New System.Windows.Forms.Button
    Me.btnSaveRefList = New System.Windows.Forms.Button
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
    Me.lnkSidebar1 = New System.Windows.Forms.LinkLabel
    Me.lnkSidebar2 = New System.Windows.Forms.LinkLabel
    Me.pnlSB2 = New System.Windows.Forms.Panel
    Me.Button11 = New System.Windows.Forms.Button
    Me.Button10 = New System.Windows.Forms.Button
    Me.tvwQuickRef = New System.Windows.Forms.TreeView
    Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
    Me.pnlSB1 = New System.Windows.Forms.Panel
    Me.labBMHighlight = New System.Windows.Forms.Label
    Me.chkDockWin = New System.Windows.Forms.CheckBox
    Me.chkTopmost = New System.Windows.Forms.CheckBox
    Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
    Me.Button7 = New System.Windows.Forms.Button
    Me.pnlExtraOptions = New System.Windows.Forms.Panel
    Me.btnInsActTyp = New System.Windows.Forms.Button
    Me.txtEventTypeName = New System.Windows.Forms.TextBox
    Me.Label8 = New System.Windows.Forms.Label
    Me.Button9 = New System.Windows.Forms.Button
    Me.Button8 = New System.Windows.Forms.Button
    Me.txtEventCtrlName = New System.Windows.Forms.TextBox
    Me.Label7 = New System.Windows.Forms.Label
    Me.Label6 = New System.Windows.Forms.Label
    Me.Label5 = New System.Windows.Forms.Label
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.btnExpandExtraModus = New System.Windows.Forms.Button
    Me.Button12 = New System.Windows.Forms.Button
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SplitContainer1.Panel1.SuspendLayout()
    Me.SplitContainer1.Panel2.SuspendLayout()
    Me.SplitContainer1.SuspendLayout()
    Me.pnlSB2.SuspendLayout()
    Me.pnlSB1.SuspendLayout()
    Me.SplitContainer2.Panel1.SuspendLayout()
    Me.SplitContainer2.Panel2.SuspendLayout()
    Me.SplitContainer2.SuspendLayout()
    Me.pnlExtraOptions.SuspendLayout()
    Me.SuspendLayout()
    '
    'ComboBox2
    '
    Me.ComboBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBox2.FormattingEnabled = True
    Me.ComboBox2.Location = New System.Drawing.Point(97, 27)
    Me.ComboBox2.Name = "ComboBox2"
    Me.ComboBox2.Size = New System.Drawing.Size(246, 21)
    Me.ComboBox2.TabIndex = 11
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(57, 30)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(34, 13)
    Me.Label2.TabIndex = 10
    Me.Label2.Text = "Type:"
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(8, 86)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(92, 29)
    Me.Button1.TabIndex = 12
    Me.Button1.Text = "fromFile"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'TextBox1
    '
    Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBox1.Location = New System.Drawing.Point(106, 64)
    Me.TextBox1.Multiline = True
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.TextBox1.Size = New System.Drawing.Size(301, 78)
    Me.TextBox1.TabIndex = 0
    '
    'ListView1
    '
    Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
    Me.ListView1.Location = New System.Drawing.Point(0, 19)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(295, 321)
    Me.ListView1.SmallImageList = Me.ImageList1
    Me.ListView1.TabIndex = 14
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "Name"
    Me.ColumnHeader1.Width = 198
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Typ"
    Me.ColumnHeader2.Width = 90
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList1.Images.SetKeyName(0, "sub")
    Me.ImageList1.Images.SetKeyName(1, "function")
    Me.ImageList1.Images.SetKeyName(2, "property")
    Me.ImageList1.Images.SetKeyName(3, "enum")
    Me.ImageList1.Images.SetKeyName(4, "type")
    Me.ImageList1.Images.SetKeyName(5, "namespace")
    Me.ImageList1.Images.SetKeyName(6, "field")
    Me.ImageList1.Images.SetKeyName(7, "assembly")
    Me.ImageList1.Images.SetKeyName(8, "event")
    Me.ImageList1.Images.SetKeyName(9, "class")
    Me.ImageList1.Images.SetKeyName(10, "const")
    Me.ImageList1.Images.SetKeyName(11, "interface")
    Me.ImageList1.Images.SetKeyName(12, "win")
    Me.ImageList1.Images.SetKeyName(13, "tool")
    Me.ImageList1.Images.SetKeyName(14, "palette")
    Me.ImageList1.Images.SetKeyName(15, "box")
    Me.ImageList1.Images.SetKeyName(16, "wizard")
    Me.ImageList1.Images.SetKeyName(17, "lib")
    Me.ImageList1.Images.SetKeyName(18, "ide")
    Me.ImageList1.Images.SetKeyName(19, "settings")
    Me.ImageList1.Images.SetKeyName(20, "note")
    Me.ImageList1.Images.SetKeyName(21, "control")
    Me.ImageList1.Images.SetKeyName(22, "web")
    '
    'Button2
    '
    Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button2.Location = New System.Drawing.Point(346, 25)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(40, 22)
    Me.Button2.TabIndex = 15
    Me.Button2.Text = ">>"
    Me.Button2.UseVisualStyleBackColor = True
    '
    'Button3
    '
    Me.Button3.Location = New System.Drawing.Point(91, 2)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(66, 23)
    Me.Button3.TabIndex = 16
    Me.Button3.Text = "List Types"
    Me.Button3.UseVisualStyleBackColor = True
    '
    'btnOpenAssembly
    '
    Me.btnOpenAssembly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOpenAssembly.Image = CType(resources.GetObject("btnOpenAssembly.Image"), System.Drawing.Image)
    Me.btnOpenAssembly.Location = New System.Drawing.Point(376, 112)
    Me.btnOpenAssembly.Name = "btnOpenAssembly"
    Me.btnOpenAssembly.Size = New System.Drawing.Size(32, 21)
    Me.btnOpenAssembly.TabIndex = 17
    Me.btnOpenAssembly.UseVisualStyleBackColor = True
    '
    'labClassName
    '
    Me.labClassName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labClassName.BackColor = System.Drawing.Color.MidnightBlue
    Me.labClassName.ForeColor = System.Drawing.Color.WhiteSmoke
    Me.labClassName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.labClassName.Location = New System.Drawing.Point(0, 0)
    Me.labClassName.Name = "labClassName"
    Me.labClassName.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
    Me.labClassName.Size = New System.Drawing.Size(295, 19)
    Me.labClassName.TabIndex = 18
    Me.labClassName.Text = "Label1"
    Me.labClassName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Button5
    '
    Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
    Me.Button5.Location = New System.Drawing.Point(58, 2)
    Me.Button5.Name = "Button5"
    Me.Button5.Size = New System.Drawing.Size(34, 23)
    Me.Button5.TabIndex = 19
    Me.Button5.UseVisualStyleBackColor = True
    '
    'Button6
    '
    Me.Button6.Location = New System.Drawing.Point(240, 2)
    Me.Button6.Name = "Button6"
    Me.Button6.Size = New System.Drawing.Size(41, 23)
    Me.Button6.TabIndex = 20
    Me.Button6.Text = "Vars"
    Me.Button6.UseVisualStyleBackColor = True
    '
    'btnSCList
    '
    Me.btnSCList.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSCList.Location = New System.Drawing.Point(315, 112)
    Me.btnSCList.Name = "btnSCList"
    Me.btnSCList.Size = New System.Drawing.Size(59, 21)
    Me.btnSCList.TabIndex = 28
    Me.btnSCList.Text = "ScriptCls"
    Me.btnSCList.UseVisualStyleBackColor = True
    '
    'labActAssembly
    '
    Me.labActAssembly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.labActAssembly.BackColor = System.Drawing.Color.FloralWhite
    Me.labActAssembly.Location = New System.Drawing.Point(0, 113)
    Me.labActAssembly.Name = "labActAssembly"
    Me.labActAssembly.Size = New System.Drawing.Size(414, 19)
    Me.labActAssembly.TabIndex = 1
    Me.labActAssembly.TextAlign = System.Drawing.ContentAlignment.BottomLeft
    '
    'RichTextBox1
    '
    Me.RichTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.RichTextBox1.BackColor = System.Drawing.Color.FloralWhite
    Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.RichTextBox1.DetectUrls = False
    Me.RichTextBox1.Location = New System.Drawing.Point(0, -1)
    Me.RichTextBox1.Name = "RichTextBox1"
    Me.RichTextBox1.ReadOnly = True
    Me.RichTextBox1.ShowSelectionMargin = True
    Me.RichTextBox1.Size = New System.Drawing.Size(414, 114)
    Me.RichTextBox1.TabIndex = 0
    Me.RichTextBox1.Text = ""
    '
    'Button4
    '
    Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button4.Location = New System.Drawing.Point(8, 117)
    Me.Button4.Name = "Button4"
    Me.Button4.Size = New System.Drawing.Size(43, 21)
    Me.Button4.TabIndex = 14
    Me.Button4.Text = ">>>"
    Me.Button4.UseVisualStyleBackColor = True
    '
    'btnSaveRefList
    '
    Me.btnSaveRefList.Location = New System.Drawing.Point(50, 117)
    Me.btnSaveRefList.Name = "btnSaveRefList"
    Me.btnSaveRefList.Size = New System.Drawing.Size(50, 21)
    Me.btnSaveRefList.TabIndex = 13
    Me.btnSaveRefList.Text = "save"
    Me.btnSaveRefList.UseVisualStyleBackColor = True
    '
    'PictureBox1
    '
    Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(7, -1)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 21
    Me.PictureBox1.TabStop = False
    '
    'SplitContainer1
    '
    Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
    Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
    Me.SplitContainer1.Name = "SplitContainer1"
    '
    'SplitContainer1.Panel1
    '
    Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
    Me.SplitContainer1.Panel1.Controls.Add(Me.lnkSidebar1)
    Me.SplitContainer1.Panel1.Controls.Add(Me.lnkSidebar2)
    Me.SplitContainer1.Panel1.Controls.Add(Me.pnlSB2)
    Me.SplitContainer1.Panel1.Controls.Add(Me.pnlSB1)
    '
    'SplitContainer1.Panel2
    '
    Me.SplitContainer1.Panel2.Controls.Add(Me.ListView1)
    Me.SplitContainer1.Panel2.Controls.Add(Me.labClassName)
    Me.SplitContainer1.Size = New System.Drawing.Size(414, 340)
    Me.SplitContainer1.SplitterDistance = 115
    Me.SplitContainer1.TabIndex = 27
    '
    'lnkSidebar1
    '
    Me.lnkSidebar1.BackColor = System.Drawing.Color.SteelBlue
    Me.lnkSidebar1.LinkColor = System.Drawing.Color.White
    Me.lnkSidebar1.Location = New System.Drawing.Point(3, -1)
    Me.lnkSidebar1.Name = "lnkSidebar1"
    Me.lnkSidebar1.Size = New System.Drawing.Size(57, 18)
    Me.lnkSidebar1.TabIndex = 2
    Me.lnkSidebar1.TabStop = True
    Me.lnkSidebar1.Tag = "1"
    Me.lnkSidebar1.Text = "Bookmark"
    Me.lnkSidebar1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lnkSidebar2
    '
    Me.lnkSidebar2.BackColor = System.Drawing.Color.FromArgb(CType(CType(84, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(110, Byte), Integer))
    Me.lnkSidebar2.LinkColor = System.Drawing.Color.White
    Me.lnkSidebar2.Location = New System.Drawing.Point(63, -1)
    Me.lnkSidebar2.Name = "lnkSidebar2"
    Me.lnkSidebar2.Size = New System.Drawing.Size(50, 18)
    Me.lnkSidebar2.TabIndex = 3
    Me.lnkSidebar2.TabStop = True
    Me.lnkSidebar2.Tag = "2"
    Me.lnkSidebar2.Text = "quickRef"
    Me.lnkSidebar2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'pnlSB2
    '
    Me.pnlSB2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlSB2.Controls.Add(Me.Button12)
    Me.pnlSB2.Controls.Add(Me.Button11)
    Me.pnlSB2.Controls.Add(Me.Button10)
    Me.pnlSB2.Controls.Add(Me.tvwQuickRef)
    Me.pnlSB2.Location = New System.Drawing.Point(0, 17)
    Me.pnlSB2.Name = "pnlSB2"
    Me.pnlSB2.Size = New System.Drawing.Size(115, 323)
    Me.pnlSB2.TabIndex = 4
    '
    'Button11
    '
    Me.Button11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Button11.BackColor = System.Drawing.Color.Silver
    Me.Button11.Location = New System.Drawing.Point(29, 302)
    Me.Button11.Name = "Button11"
    Me.Button11.Size = New System.Drawing.Size(25, 20)
    Me.Button11.TabIndex = 2
    Me.Button11.Text = "--"
    Me.Button11.UseVisualStyleBackColor = False
    '
    'Button10
    '
    Me.Button10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Button10.BackColor = System.Drawing.Color.Silver
    Me.Button10.Location = New System.Drawing.Point(3, 302)
    Me.Button10.Name = "Button10"
    Me.Button10.Size = New System.Drawing.Size(26, 20)
    Me.Button10.TabIndex = 1
    Me.Button10.Text = "+"
    Me.Button10.UseVisualStyleBackColor = False
    '
    'tvwQuickRef
    '
    Me.tvwQuickRef.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tvwQuickRef.ImageIndex = 0
    Me.tvwQuickRef.ImageList = Me.ImageList2
    Me.tvwQuickRef.Location = New System.Drawing.Point(3, 3)
    Me.tvwQuickRef.Name = "tvwQuickRef"
    Me.tvwQuickRef.SelectedImageIndex = 2
    Me.tvwQuickRef.ShowRootLines = False
    Me.tvwQuickRef.Size = New System.Drawing.Size(114, 298)
    Me.tvwQuickRef.TabIndex = 0
    '
    'ImageList2
    '
    Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList2.Images.SetKeyName(0, "shell32_246.ico")
    Me.ImageList2.Images.SetKeyName(1, "ACCICONS_138.ico")
    Me.ImageList2.Images.SetKeyName(2, "explorer_252.ico")
    Me.ImageList2.Images.SetKeyName(3, "ieframe_17367.ico")
    Me.ImageList2.Images.SetKeyName(4, "iexplore_32542.ico")
    Me.ImageList2.Images.SetKeyName(5, "msenv_VSICON.ico")
    Me.ImageList2.Images.SetKeyName(6, "msvbprj_4530.ico")
    Me.ImageList2.Images.SetKeyName(7, "nvcpl_15027.ico")
    Me.ImageList2.Images.SetKeyName(8, "nvcpl_15028.ico")
    Me.ImageList2.Images.SetKeyName(9, "nvcpl_15029.ico")
    '
    'pnlSB1
    '
    Me.pnlSB1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlSB1.Controls.Add(Me.labBMHighlight)
    Me.pnlSB1.Location = New System.Drawing.Point(0, 17)
    Me.pnlSB1.Name = "pnlSB1"
    Me.pnlSB1.Size = New System.Drawing.Size(115, 322)
    Me.pnlSB1.TabIndex = 1
    '
    'labBMHighlight
    '
    Me.labBMHighlight.BackColor = System.Drawing.Color.LawnGreen
    Me.labBMHighlight.Location = New System.Drawing.Point(8, 10)
    Me.labBMHighlight.Name = "labBMHighlight"
    Me.labBMHighlight.Size = New System.Drawing.Size(100, 23)
    Me.labBMHighlight.TabIndex = 0
    Me.labBMHighlight.Visible = False
    '
    'chkDockWin
    '
    Me.chkDockWin.AutoSize = True
    Me.chkDockWin.Location = New System.Drawing.Point(333, 6)
    Me.chkDockWin.Name = "chkDockWin"
    Me.chkDockWin.Size = New System.Drawing.Size(50, 17)
    Me.chkDockWin.TabIndex = 29
    Me.chkDockWin.Text = "dock"
    Me.chkDockWin.UseVisualStyleBackColor = True
    '
    'chkTopmost
    '
    Me.chkTopmost.AutoSize = True
    Me.chkTopmost.Location = New System.Drawing.Point(290, 6)
    Me.chkTopmost.Name = "chkTopmost"
    Me.chkTopmost.Size = New System.Drawing.Size(41, 17)
    Me.chkTopmost.TabIndex = 30
    Me.chkTopmost.Text = "top"
    Me.chkTopmost.UseVisualStyleBackColor = True
    '
    'SplitContainer2
    '
    Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
    Me.SplitContainer2.Location = New System.Drawing.Point(0, 50)
    Me.SplitContainer2.Name = "SplitContainer2"
    Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplitContainer2.Panel1
    '
    Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
    '
    'SplitContainer2.Panel2
    '
    Me.SplitContainer2.Panel2.Controls.Add(Me.btnOpenAssembly)
    Me.SplitContainer2.Panel2.Controls.Add(Me.btnSCList)
    Me.SplitContainer2.Panel2.Controls.Add(Me.RichTextBox1)
    Me.SplitContainer2.Panel2.Controls.Add(Me.labActAssembly)
    Me.SplitContainer2.Size = New System.Drawing.Size(414, 476)
    Me.SplitContainer2.SplitterDistance = 340
    Me.SplitContainer2.TabIndex = 29
    '
    'Button7
    '
    Me.Button7.Location = New System.Drawing.Point(163, 2)
    Me.Button7.Name = "Button7"
    Me.Button7.Size = New System.Drawing.Size(78, 23)
    Me.Button7.TabIndex = 31
    Me.Button7.Text = "insert Events"
    Me.Button7.UseVisualStyleBackColor = True
    '
    'pnlExtraOptions
    '
    Me.pnlExtraOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlExtraOptions.Controls.Add(Me.btnInsActTyp)
    Me.pnlExtraOptions.Controls.Add(Me.txtEventTypeName)
    Me.pnlExtraOptions.Controls.Add(Me.Label8)
    Me.pnlExtraOptions.Controls.Add(Me.Button9)
    Me.pnlExtraOptions.Controls.Add(Me.Button8)
    Me.pnlExtraOptions.Controls.Add(Me.txtEventCtrlName)
    Me.pnlExtraOptions.Controls.Add(Me.Label7)
    Me.pnlExtraOptions.Controls.Add(Me.Label6)
    Me.pnlExtraOptions.Controls.Add(Me.Label5)
    Me.pnlExtraOptions.Controls.Add(Me.Label1)
    Me.pnlExtraOptions.Controls.Add(Me.btnSaveRefList)
    Me.pnlExtraOptions.Controls.Add(Me.Button4)
    Me.pnlExtraOptions.Controls.Add(Me.TextBox1)
    Me.pnlExtraOptions.Controls.Add(Me.Button1)
    Me.pnlExtraOptions.Controls.Add(Me.Label4)
    Me.pnlExtraOptions.Location = New System.Drawing.Point(0, 50)
    Me.pnlExtraOptions.Name = "pnlExtraOptions"
    Me.pnlExtraOptions.Size = New System.Drawing.Size(416, 161)
    Me.pnlExtraOptions.TabIndex = 32
    Me.pnlExtraOptions.Visible = False
    '
    'btnInsActTyp
    '
    Me.btnInsActTyp.Location = New System.Drawing.Point(282, 25)
    Me.btnInsActTyp.Name = "btnInsActTyp"
    Me.btnInsActTyp.Size = New System.Drawing.Size(16, 23)
    Me.btnInsActTyp.TabIndex = 25
    Me.btnInsActTyp.Text = "<"
    Me.btnInsActTyp.UseVisualStyleBackColor = True
    '
    'txtEventTypeName
    '
    Me.txtEventTypeName.Location = New System.Drawing.Point(161, 25)
    Me.txtEventTypeName.Name = "txtEventTypeName"
    Me.txtEventTypeName.Size = New System.Drawing.Size(120, 20)
    Me.txtEventTypeName.TabIndex = 24
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.BackColor = System.Drawing.Color.CadetBlue
    Me.Label8.Location = New System.Drawing.Point(106, 29)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(55, 13)
    Me.Label8.TabIndex = 23
    Me.Label8.Text = "typeName"
    '
    'Button9
    '
    Me.Button9.Location = New System.Drawing.Point(309, 26)
    Me.Button9.Name = "Button9"
    Me.Button9.Size = New System.Drawing.Size(75, 23)
    Me.Button9.TabIndex = 22
    Me.Button9.Text = "insert Code"
    Me.Button9.UseVisualStyleBackColor = True
    '
    'Button8
    '
    Me.Button8.Location = New System.Drawing.Point(309, 4)
    Me.Button8.Name = "Button8"
    Me.Button8.Size = New System.Drawing.Size(75, 23)
    Me.Button8.TabIndex = 21
    Me.Button8.Text = "copy"
    Me.Button8.UseVisualStyleBackColor = True
    '
    'txtEventCtrlName
    '
    Me.txtEventCtrlName.Location = New System.Drawing.Point(161, 3)
    Me.txtEventCtrlName.Name = "txtEventCtrlName"
    Me.txtEventCtrlName.Size = New System.Drawing.Size(120, 20)
    Me.txtEventCtrlName.TabIndex = 20
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.BackColor = System.Drawing.Color.CadetBlue
    Me.Label7.Location = New System.Drawing.Point(106, 7)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(49, 13)
    Me.Label7.TabIndex = 19
    Me.Label7.Text = "ctrlName"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.BackColor = System.Drawing.Color.CadetBlue
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label6.ForeColor = System.Drawing.Color.WhiteSmoke
    Me.Label6.Location = New System.Drawing.Point(8, 6)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(82, 13)
    Me.Label6.TabIndex = 18
    Me.Label6.Text = "Insert Events"
    '
    'Label5
    '
    Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label5.BackColor = System.Drawing.Color.CadetBlue
    Me.Label5.Location = New System.Drawing.Point(0, 0)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(416, 57)
    Me.Label5.TabIndex = 17
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.BackColor = System.Drawing.Color.SlateGray
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.WhiteSmoke
    Me.Label1.Location = New System.Drawing.Point(8, 64)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(72, 13)
    Me.Label1.TabIndex = 15
    Me.Label1.Text = "References"
    '
    'Label4
    '
    Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label4.BackColor = System.Drawing.Color.SlateGray
    Me.Label4.Location = New System.Drawing.Point(0, 57)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(416, 98)
    Me.Label4.TabIndex = 16
    '
    'btnExpandExtraModus
    '
    Me.btnExpandExtraModus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnExpandExtraModus.Font = New System.Drawing.Font("Wingdings 3", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
    Me.btnExpandExtraModus.Location = New System.Drawing.Point(386, 25)
    Me.btnExpandExtraModus.Name = "btnExpandExtraModus"
    Me.btnExpandExtraModus.Size = New System.Drawing.Size(26, 22)
    Me.btnExpandExtraModus.TabIndex = 33
    Me.btnExpandExtraModus.Text = "s"
    Me.btnExpandExtraModus.UseVisualStyleBackColor = True
    '
    'Button12
    '
    Me.Button12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Button12.BackColor = System.Drawing.Color.Silver
    Me.Button12.Location = New System.Drawing.Point(83, 302)
    Me.Button12.Name = "Button12"
    Me.Button12.Size = New System.Drawing.Size(25, 20)
    Me.Button12.TabIndex = 3
    Me.Button12.Text = "#"
    Me.Button12.UseVisualStyleBackColor = False
    '
    'frmTB_infoTips
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(414, 526)
    Me.Controls.Add(Me.btnExpandExtraModus)
    Me.Controls.Add(Me.Button6)
    Me.Controls.Add(Me.Button7)
    Me.Controls.Add(Me.Button2)
    Me.Controls.Add(Me.ComboBox2)
    Me.Controls.Add(Me.chkDockWin)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.Button3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.chkTopmost)
    Me.Controls.Add(Me.Button5)
    Me.Controls.Add(Me.SplitContainer2)
    Me.Controls.Add(Me.pnlExtraOptions)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.KeyPreview = True
    Me.Name = "frmTB_infoTips"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
    Me.Text = "Reflection"
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.SplitContainer1.Panel1.ResumeLayout(False)
    Me.SplitContainer1.Panel2.ResumeLayout(False)
    Me.SplitContainer1.ResumeLayout(False)
    Me.pnlSB2.ResumeLayout(False)
    Me.pnlSB1.ResumeLayout(False)
    Me.SplitContainer2.Panel1.ResumeLayout(False)
    Me.SplitContainer2.Panel2.ResumeLayout(False)
    Me.SplitContainer2.ResumeLayout(False)
    Me.pnlExtraOptions.ResumeLayout(False)
    Me.pnlExtraOptions.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents Button3 As System.Windows.Forms.Button
  Friend WithEvents btnOpenAssembly As System.Windows.Forms.Button
  Friend WithEvents labClassName As System.Windows.Forms.Label
  Friend WithEvents Button5 As System.Windows.Forms.Button
  Friend WithEvents Button6 As System.Windows.Forms.Button
  Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents labBMHighlight As System.Windows.Forms.Label
  Friend WithEvents btnSCList As System.Windows.Forms.Button
  Friend WithEvents btnSaveRefList As System.Windows.Forms.Button
  Friend WithEvents Button4 As System.Windows.Forms.Button
  Friend WithEvents labActAssembly As System.Windows.Forms.Label
  Friend WithEvents chkDockWin As System.Windows.Forms.CheckBox
  Friend WithEvents chkTopmost As System.Windows.Forms.CheckBox
  Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
  Friend WithEvents Button7 As System.Windows.Forms.Button
  Friend WithEvents pnlExtraOptions As System.Windows.Forms.Panel
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Button9 As System.Windows.Forms.Button
  Friend WithEvents Button8 As System.Windows.Forms.Button
  Friend WithEvents txtEventCtrlName As System.Windows.Forms.TextBox
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents btnExpandExtraModus As System.Windows.Forms.Button
  Friend WithEvents txtEventTypeName As System.Windows.Forms.TextBox
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents pnlSB1 As System.Windows.Forms.Panel
  Friend WithEvents lnkSidebar1 As System.Windows.Forms.LinkLabel
  Friend WithEvents lnkSidebar2 As System.Windows.Forms.LinkLabel
  Friend WithEvents pnlSB2 As System.Windows.Forms.Panel
  Friend WithEvents tvwQuickRef As System.Windows.Forms.TreeView
  Friend WithEvents Button11 As System.Windows.Forms.Button
  Friend WithEvents Button10 As System.Windows.Forms.Button
  Friend WithEvents btnInsActTyp As System.Windows.Forms.Button
  Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
  Friend WithEvents Button12 As System.Windows.Forms.Button
End Class
