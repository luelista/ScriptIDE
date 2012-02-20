<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prop_TextEditor
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(prop_TextEditor))
    Me.igHotkeysCol6CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igHotkeysCol6ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
    Me.ListBox1 = New System.Windows.Forms.ListBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.GroupBox2 = New System.Windows.Forms.GroupBox
    Me.btnSaveXML = New System.Windows.Forms.Button
    Me.scXML = New ScintillaNet.Scintilla
    Me.btnHelp = New System.Windows.Forms.Button
    Me.GroupBox2.SuspendLayout()
    CType(Me.scXML, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'imlToolbarIcons
    '
    Me.imlToolbarIcons.ImageStream = CType(resources.GetObject("imlToolbarIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.Transparent
    Me.imlToolbarIcons.Images.SetKeyName(0, "explorer_252.ico")
    '
    'ListBox1
    '
    Me.ListBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ListBox1.FormattingEnabled = True
    Me.ListBox1.IntegralHeight = False
    Me.ListBox1.Location = New System.Drawing.Point(8, 25)
    Me.ListBox1.Name = "ListBox1"
    Me.ListBox1.Size = New System.Drawing.Size(127, 311)
    Me.ListBox1.TabIndex = 0
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(5, 9)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(61, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Dateitypen:"
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.btnHelp)
    Me.GroupBox2.Controls.Add(Me.btnSaveXML)
    Me.GroupBox2.Controls.Add(Me.scXML)
    Me.GroupBox2.Location = New System.Drawing.Point(153, 9)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(363, 327)
    Me.GroupBox2.TabIndex = 4
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Editor"
    '
    'btnSaveXML
    '
    Me.btnSaveXML.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSaveXML.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnSaveXML.Location = New System.Drawing.Point(249, 296)
    Me.btnSaveXML.Name = "btnSaveXML"
    Me.btnSaveXML.Size = New System.Drawing.Size(96, 23)
    Me.btnSaveXML.TabIndex = 4
    Me.btnSaveXML.Text = "Speichern"
    Me.btnSaveXML.UseVisualStyleBackColor = True
    '
    'scXML
    '
    Me.scXML.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.scXML.Indentation.SmartIndentType = ScintillaNet.SmartIndent.Simple
    Me.scXML.Indentation.TabWidth = 20
    Me.scXML.Location = New System.Drawing.Point(6, 19)
    Me.scXML.Name = "scXML"
    Me.scXML.Size = New System.Drawing.Size(339, 268)
    Me.scXML.Styles.BraceBad.FontName = "Verdana"
    Me.scXML.Styles.BraceLight.FontName = "Verdana"
    Me.scXML.Styles.ControlChar.FontName = "Verdana"
    Me.scXML.Styles.Default.FontName = "Verdana"
    Me.scXML.Styles.IndentGuide.FontName = "Verdana"
    Me.scXML.Styles.LastPredefined.FontName = "Verdana"
    Me.scXML.Styles.LineNumber.FontName = "Verdana"
    Me.scXML.Styles.Max.FontName = "Verdana"
    Me.scXML.TabIndex = 3
    '
    'btnHelp
    '
    Me.btnHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnHelp.Location = New System.Drawing.Point(6, 296)
    Me.btnHelp.Name = "btnHelp"
    Me.btnHelp.Size = New System.Drawing.Size(78, 23)
    Me.btnHelp.TabIndex = 5
    Me.btnHelp.Text = "Hilfe"
    Me.btnHelp.UseVisualStyleBackColor = True
    '
    'prop_TextEditor
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.ListBox1)
    Me.Name = "prop_TextEditor"
    Me.Size = New System.Drawing.Size(532, 352)
    Me.GroupBox2.ResumeLayout(False)
    CType(Me.scXML, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
  Friend WithEvents igHotkeysCol6CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igHotkeysCol6ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents scXML As ScintillaNet.Scintilla
  Friend WithEvents btnSaveXML As System.Windows.Forms.Button
  Friend WithEvents btnHelp As System.Windows.Forms.Button

End Class
