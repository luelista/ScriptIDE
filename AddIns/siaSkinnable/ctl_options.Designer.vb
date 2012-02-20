<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctl_options
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctl_options))
    Me.TreeView1 = New System.Windows.Forms.TreeView
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.Button1 = New System.Windows.Forms.Button
    Me.ComboBox1 = New System.Windows.Forms.ComboBox
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.LinkLabel3 = New System.Windows.Forms.LinkLabel
    Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
    Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
    Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
    Me.Button6 = New System.Windows.Forms.Button
    Me.pnlEdit_Color = New System.Windows.Forms.Panel
    Me.picPreview2 = New System.Windows.Forms.PictureBox
    Me.Button5 = New System.Windows.Forms.Button
    Me.txtColor = New System.Windows.Forms.TextBox
    Me.Label7 = New System.Windows.Forms.Label
    Me.pnlEdit_String = New System.Windows.Forms.Panel
    Me.TextBox4 = New System.Windows.Forms.TextBox
    Me.Label5 = New System.Windows.Forms.Label
    Me.pnlEdit_Gradient = New System.Windows.Forms.Panel
    Me.picPreview1 = New System.Windows.Forms.PictureBox
    Me.Label6 = New System.Windows.Forms.Label
    Me.cmbGradientStyle = New System.Windows.Forms.ComboBox
    Me.Label4 = New System.Windows.Forms.Label
    Me.Button3 = New System.Windows.Forms.Button
    Me.txtGradientText = New System.Windows.Forms.TextBox
    Me.Label3 = New System.Windows.Forms.Label
    Me.Button2 = New System.Windows.Forms.Button
    Me.txtGradient2 = New System.Windows.Forms.TextBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.Button4 = New System.Windows.Forms.Button
    Me.txtGradient1 = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
    Me.GroupBox1.SuspendLayout()
    Me.SplitContainer1.Panel1.SuspendLayout()
    Me.SplitContainer1.Panel2.SuspendLayout()
    Me.SplitContainer1.SuspendLayout()
    Me.pnlEdit_Color.SuspendLayout()
    CType(Me.picPreview2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlEdit_String.SuspendLayout()
    Me.pnlEdit_Gradient.SuspendLayout()
    CType(Me.picPreview1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TreeView1
    '
    Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TreeView1.ImageKey = "diro"
    Me.TreeView1.ImageList = Me.ImageList1
    Me.TreeView1.Location = New System.Drawing.Point(0, 0)
    Me.TreeView1.Name = "TreeView1"
    Me.TreeView1.PathSeparator = "."
    Me.TreeView1.SelectedImageKey = "diro"
    Me.TreeView1.Size = New System.Drawing.Size(212, 455)
    Me.TreeView1.TabIndex = 0
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList1.Images.SetKeyName(0, "dirc")
    Me.ImageList1.Images.SetKeyName(1, "diro")
    Me.ImageList1.Images.SetKeyName(2, "bool_false")
    Me.ImageList1.Images.SetKeyName(3, "bool_true")
    Me.ImageList1.Images.SetKeyName(4, "color")
    Me.ImageList1.Images.SetKeyName(5, "string")
    '
    'Button1
    '
    Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Button1.Location = New System.Drawing.Point(344, 16)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(75, 23)
    Me.Button1.TabIndex = 1
    Me.Button1.Text = "Speichern"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'ComboBox1
    '
    Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ComboBox1.FormattingEnabled = True
    Me.ComboBox1.Location = New System.Drawing.Point(12, 18)
    Me.ComboBox1.Name = "ComboBox1"
    Me.ComboBox1.Size = New System.Drawing.Size(326, 21)
    Me.ComboBox1.TabIndex = 2
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.LinkLabel3)
    Me.GroupBox1.Controls.Add(Me.LinkLabel2)
    Me.GroupBox1.Controls.Add(Me.LinkLabel1)
    Me.GroupBox1.Controls.Add(Me.ComboBox1)
    Me.GroupBox1.Controls.Add(Me.Button1)
    Me.GroupBox1.Location = New System.Drawing.Point(0, 3)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(435, 65)
    Me.GroupBox1.TabIndex = 3
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Skins laden/speichern"
    '
    'LinkLabel3
    '
    Me.LinkLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LinkLabel3.AutoSize = True
    Me.LinkLabel3.Location = New System.Drawing.Point(334, 44)
    Me.LinkLabel3.Name = "LinkLabel3"
    Me.LinkLabel3.Size = New System.Drawing.Size(85, 13)
    Me.LinkLabel3.TabIndex = 5
    Me.LinkLabel3.TabStop = True
    Me.LinkLabel3.Text = "Alle expandieren"
    '
    'LinkLabel2
    '
    Me.LinkLabel2.AutoSize = True
    Me.LinkLabel2.Location = New System.Drawing.Point(93, 44)
    Me.LinkLabel2.Name = "LinkLabel2"
    Me.LinkLabel2.Size = New System.Drawing.Size(101, 13)
    Me.LinkLabel2.TabIndex = 4
    Me.LinkLabel2.TabStop = True
    Me.LinkLabel2.Text = "Skins herunterladen"
    '
    'LinkLabel1
    '
    Me.LinkLabel1.AutoSize = True
    Me.LinkLabel1.Location = New System.Drawing.Point(9, 44)
    Me.LinkLabel1.Name = "LinkLabel1"
    Me.LinkLabel1.Size = New System.Drawing.Size(78, 13)
    Me.LinkLabel1.TabIndex = 3
    Me.LinkLabel1.TabStop = True
    Me.LinkLabel1.Text = "Explorer öffnen"
    '
    'SplitContainer1
    '
    Me.SplitContainer1.AllowDrop = True
    Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SplitContainer1.Location = New System.Drawing.Point(0, 72)
    Me.SplitContainer1.Name = "SplitContainer1"
    '
    'SplitContainer1.Panel1
    '
    Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
    '
    'SplitContainer1.Panel2
    '
    Me.SplitContainer1.Panel2.Controls.Add(Me.Button6)
    Me.SplitContainer1.Panel2.Controls.Add(Me.pnlEdit_Color)
    Me.SplitContainer1.Panel2.Controls.Add(Me.pnlEdit_String)
    Me.SplitContainer1.Panel2.Controls.Add(Me.pnlEdit_Gradient)
    Me.SplitContainer1.Size = New System.Drawing.Size(435, 455)
    Me.SplitContainer1.SplitterDistance = 212
    Me.SplitContainer1.TabIndex = 4
    '
    'Button6
    '
    Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Button6.Location = New System.Drawing.Point(5, 3)
    Me.Button6.Name = "Button6"
    Me.Button6.Size = New System.Drawing.Size(118, 25)
    Me.Button6.TabIndex = 3
    Me.Button6.Text = "Übernehmen"
    Me.Button6.UseVisualStyleBackColor = True
    '
    'pnlEdit_Color
    '
    Me.pnlEdit_Color.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlEdit_Color.Controls.Add(Me.picPreview2)
    Me.pnlEdit_Color.Controls.Add(Me.Button5)
    Me.pnlEdit_Color.Controls.Add(Me.txtColor)
    Me.pnlEdit_Color.Controls.Add(Me.Label7)
    Me.pnlEdit_Color.Location = New System.Drawing.Point(5, 317)
    Me.pnlEdit_Color.Name = "pnlEdit_Color"
    Me.pnlEdit_Color.Size = New System.Drawing.Size(213, 104)
    Me.pnlEdit_Color.TabIndex = 2
    Me.pnlEdit_Color.Visible = False
    '
    'picPreview2
    '
    Me.picPreview2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.picPreview2.Location = New System.Drawing.Point(27, 56)
    Me.picPreview2.Name = "picPreview2"
    Me.picPreview2.Size = New System.Drawing.Size(149, 39)
    Me.picPreview2.TabIndex = 25
    Me.picPreview2.TabStop = False
    '
    'Button5
    '
    Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Button5.Location = New System.Drawing.Point(170, 28)
    Me.Button5.Name = "Button5"
    Me.Button5.Size = New System.Drawing.Size(40, 20)
    Me.Button5.TabIndex = 21
    Me.Button5.Tag = "txtColor"
    Me.Button5.Text = "..."
    Me.Button5.UseVisualStyleBackColor = True
    '
    'txtColor
    '
    Me.txtColor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtColor.Location = New System.Drawing.Point(69, 28)
    Me.txtColor.Name = "txtColor"
    Me.txtColor.Size = New System.Drawing.Size(97, 20)
    Me.txtColor.TabIndex = 1
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(1, 7)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(90, 13)
    Me.Label7.TabIndex = 0
    Me.Label7.Text = "Farbe bearbeiten:"
    '
    'pnlEdit_String
    '
    Me.pnlEdit_String.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlEdit_String.Controls.Add(Me.TextBox4)
    Me.pnlEdit_String.Controls.Add(Me.Label5)
    Me.pnlEdit_String.Location = New System.Drawing.Point(5, 219)
    Me.pnlEdit_String.Name = "pnlEdit_String"
    Me.pnlEdit_String.Size = New System.Drawing.Size(213, 97)
    Me.pnlEdit_String.TabIndex = 1
    Me.pnlEdit_String.Visible = False
    '
    'TextBox4
    '
    Me.TextBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBox4.Location = New System.Drawing.Point(4, 27)
    Me.TextBox4.Multiline = True
    Me.TextBox4.Name = "TextBox4"
    Me.TextBox4.Size = New System.Drawing.Size(206, 65)
    Me.TextBox4.TabIndex = 1
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(1, 7)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(84, 13)
    Me.Label5.TabIndex = 0
    Me.Label5.Text = "Text bearbeiten:"
    '
    'pnlEdit_Gradient
    '
    Me.pnlEdit_Gradient.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnlEdit_Gradient.Controls.Add(Me.picPreview1)
    Me.pnlEdit_Gradient.Controls.Add(Me.Label6)
    Me.pnlEdit_Gradient.Controls.Add(Me.cmbGradientStyle)
    Me.pnlEdit_Gradient.Controls.Add(Me.Label4)
    Me.pnlEdit_Gradient.Controls.Add(Me.Button3)
    Me.pnlEdit_Gradient.Controls.Add(Me.txtGradientText)
    Me.pnlEdit_Gradient.Controls.Add(Me.Label3)
    Me.pnlEdit_Gradient.Controls.Add(Me.Button2)
    Me.pnlEdit_Gradient.Controls.Add(Me.txtGradient2)
    Me.pnlEdit_Gradient.Controls.Add(Me.Label2)
    Me.pnlEdit_Gradient.Controls.Add(Me.Button4)
    Me.pnlEdit_Gradient.Controls.Add(Me.txtGradient1)
    Me.pnlEdit_Gradient.Controls.Add(Me.Label1)
    Me.pnlEdit_Gradient.Location = New System.Drawing.Point(5, 33)
    Me.pnlEdit_Gradient.Name = "pnlEdit_Gradient"
    Me.pnlEdit_Gradient.Size = New System.Drawing.Size(213, 185)
    Me.pnlEdit_Gradient.TabIndex = 0
    Me.pnlEdit_Gradient.Visible = False
    '
    'picPreview1
    '
    Me.picPreview1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.picPreview1.Location = New System.Drawing.Point(27, 141)
    Me.picPreview1.Name = "picPreview1"
    Me.picPreview1.Size = New System.Drawing.Size(149, 39)
    Me.picPreview1.TabIndex = 24
    Me.picPreview1.TabStop = False
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(1, 6)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(96, 13)
    Me.Label6.TabIndex = 23
    Me.Label6.Text = "Verlauf bearbeiten:"
    '
    'cmbGradientStyle
    '
    Me.cmbGradientStyle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmbGradientStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cmbGradientStyle.FormattingEnabled = True
    Me.cmbGradientStyle.Items.AddRange(New Object() {"Horizontal", "Vertical", "ForwardDiagonal", "BackwardDiagonal"})
    Me.cmbGradientStyle.Location = New System.Drawing.Point(69, 28)
    Me.cmbGradientStyle.Name = "cmbGradientStyle"
    Me.cmbGradientStyle.Size = New System.Drawing.Size(142, 21)
    Me.cmbGradientStyle.TabIndex = 22
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(1, 31)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(60, 13)
    Me.Label4.TabIndex = 21
    Me.Label4.Text = "Verlaufsart:"
    '
    'Button3
    '
    Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Button3.Location = New System.Drawing.Point(170, 115)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(40, 20)
    Me.Button3.TabIndex = 20
    Me.Button3.Tag = "txtGradientText"
    Me.Button3.Text = "..."
    Me.Button3.UseVisualStyleBackColor = True
    '
    'txtGradientText
    '
    Me.txtGradientText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtGradientText.Location = New System.Drawing.Point(69, 115)
    Me.txtGradientText.Name = "txtGradientText"
    Me.txtGradientText.Size = New System.Drawing.Size(97, 20)
    Me.txtGradientText.TabIndex = 19
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(1, 118)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(55, 13)
    Me.Label3.TabIndex = 18
    Me.Label3.Text = "Textfarbe:"
    '
    'Button2
    '
    Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Button2.Location = New System.Drawing.Point(170, 86)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(40, 20)
    Me.Button2.TabIndex = 17
    Me.Button2.Tag = "txtGradient2"
    Me.Button2.Text = "..."
    Me.Button2.UseVisualStyleBackColor = True
    '
    'txtGradient2
    '
    Me.txtGradient2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtGradient2.Location = New System.Drawing.Point(69, 86)
    Me.txtGradient2.Name = "txtGradient2"
    Me.txtGradient2.Size = New System.Drawing.Size(97, 20)
    Me.txtGradient2.TabIndex = 16
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(1, 89)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(53, 13)
    Me.Label2.TabIndex = 15
    Me.Label2.Text = "Endfarbe:"
    '
    'Button4
    '
    Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Button4.Location = New System.Drawing.Point(170, 57)
    Me.Button4.Name = "Button4"
    Me.Button4.Size = New System.Drawing.Size(40, 20)
    Me.Button4.TabIndex = 14
    Me.Button4.Tag = "txtGradient1"
    Me.Button4.Text = "..."
    Me.Button4.UseVisualStyleBackColor = True
    '
    'txtGradient1
    '
    Me.txtGradient1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtGradient1.Location = New System.Drawing.Point(69, 57)
    Me.txtGradient1.Name = "txtGradient1"
    Me.txtGradient1.Size = New System.Drawing.Size(97, 20)
    Me.txtGradient1.TabIndex = 13
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(1, 60)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(56, 13)
    Me.Label1.TabIndex = 12
    Me.Label1.Text = "Startfarbe:"
    '
    'ctl_options
    '
    Me.AllowDrop = True
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.SplitContainer1)
    Me.Name = "ctl_options"
    Me.Size = New System.Drawing.Size(436, 528)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.SplitContainer1.Panel1.ResumeLayout(False)
    Me.SplitContainer1.Panel2.ResumeLayout(False)
    Me.SplitContainer1.ResumeLayout(False)
    Me.pnlEdit_Color.ResumeLayout(False)
    Me.pnlEdit_Color.PerformLayout()
    CType(Me.picPreview2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnlEdit_String.ResumeLayout(False)
    Me.pnlEdit_String.PerformLayout()
    Me.pnlEdit_Gradient.ResumeLayout(False)
    Me.pnlEdit_Gradient.PerformLayout()
    CType(Me.picPreview1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
  Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
  Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
  Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents pnlEdit_Gradient As System.Windows.Forms.Panel
  Friend WithEvents cmbGradientStyle As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Button3 As System.Windows.Forms.Button
  Friend WithEvents txtGradientText As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents txtGradient2 As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Button4 As System.Windows.Forms.Button
  Friend WithEvents txtGradient1 As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents pnlEdit_Color As System.Windows.Forms.Panel
  Friend WithEvents Button5 As System.Windows.Forms.Button
  Friend WithEvents txtColor As System.Windows.Forms.TextBox
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents pnlEdit_String As System.Windows.Forms.Panel
  Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
  Friend WithEvents Button6 As System.Windows.Forms.Button
  Friend WithEvents picPreview2 As System.Windows.Forms.PictureBox
  Friend WithEvents picPreview1 As System.Windows.Forms.PictureBox

End Class
