<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_compileErrors
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
    Dim IGColPattern4 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern5 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_compileErrors))
    Me.igCompileErrors = New TenTec.Windows.iGridLib.iGrid
    Me.iml_TraceTypes = New System.Windows.Forms.ImageList(Me.components)
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.lblClassName = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.lblWarnCount = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.lblErrCount = New System.Windows.Forms.Label
    Me.Panel2 = New System.Windows.Forms.Panel
    Me.PictureBox2 = New System.Windows.Forms.PictureBox
    Me.Label11 = New System.Windows.Forms.Label
    Me.Label10 = New System.Windows.Forms.Label
    Me.lblErrType = New System.Windows.Forms.Label
    Me.lblLine = New System.Windows.Forms.Label
    Me.TextBox1 = New System.Windows.Forms.TextBox
    CType(Me.igCompileErrors, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel2.SuspendLayout()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'igCompileErrors
    '
    Me.igCompileErrors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.igCompileErrors.AutoResizeCols = True
    IGColPattern1.Key = "typ"
    IGColPattern1.MaxWidth = 80
    IGColPattern1.Text = "Typ"
    IGColPattern1.Width = 27
    IGColPattern2.Key = "desc"
    IGColPattern2.Text = "Description"
    IGColPattern2.Width = 293
    IGColPattern3.Key = "file"
    IGColPattern3.MaxWidth = 300
    IGColPattern3.Text = "File"
    IGColPattern3.Width = 133
    IGColPattern4.Key = "line"
    IGColPattern4.MaxWidth = 80
    IGColPattern4.Text = "Line"
    IGColPattern4.Width = 80
    IGColPattern5.Key = "col"
    IGColPattern5.MaxWidth = 80
    IGColPattern5.Text = "Typ"
    IGColPattern5.Visible = False
    IGColPattern5.Width = 45
    Me.igCompileErrors.Cols.AddRange(New TenTec.Windows.iGridLib.iGColPattern() {IGColPattern1, IGColPattern2, IGColPattern3, IGColPattern4, IGColPattern5})
    Me.igCompileErrors.DefaultRow.Height = 20
    Me.igCompileErrors.DefaultRow.NormalCellHeight = 20
    Me.igCompileErrors.ImageList = Me.iml_TraceTypes
    Me.igCompileErrors.Location = New System.Drawing.Point(1, 127)
    Me.igCompileErrors.Name = "igCompileErrors"
    Me.igCompileErrors.ReadOnly = True
    Me.igCompileErrors.RowMode = True
    Me.igCompileErrors.Size = New System.Drawing.Size(537, 290)
    Me.igCompileErrors.TabIndex = 28
    '
    'iml_TraceTypes
    '
    Me.iml_TraceTypes.ImageStream = CType(resources.GetObject("iml_TraceTypes.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.iml_TraceTypes.TransparentColor = System.Drawing.Color.Transparent
    Me.iml_TraceTypes.Images.SetKeyName(0, "info")
    Me.iml_TraceTypes.Images.SetKeyName(1, "warn")
    Me.iml_TraceTypes.Images.SetKeyName(2, "err")
    Me.iml_TraceTypes.Images.SetKeyName(3, "ok")
    Me.iml_TraceTypes.Images.SetKeyName(4, "ini")
    Me.iml_TraceTypes.Images.SetKeyName(5, "shutdown")
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(84, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
    Me.Panel1.Controls.Add(Me.PictureBox1)
    Me.Panel1.Controls.Add(Me.lblClassName)
    Me.Panel1.Controls.Add(Me.Label3)
    Me.Panel1.Controls.Add(Me.lblWarnCount)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.lblErrCount)
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(538, 41)
    Me.Panel1.TabIndex = 29
    '
    'PictureBox1
    '
    Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(12, 1)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(64, 64)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 0
    Me.PictureBox1.TabStop = False
    '
    'lblClassName
    '
    Me.lblClassName.AutoSize = True
    Me.lblClassName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblClassName.ForeColor = System.Drawing.Color.Gainsboro
    Me.lblClassName.Location = New System.Drawing.Point(314, 9)
    Me.lblClassName.Name = "lblClassName"
    Me.lblClassName.Size = New System.Drawing.Size(194, 24)
    Me.lblClassName.TabIndex = 5
    Me.lblClassName.Text = "mw_regexTester.vb"
    '
    'Label3
    '
    Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(67, Byte), Integer))
    Me.Label3.ForeColor = System.Drawing.Color.LightGray
    Me.Label3.Location = New System.Drawing.Point(241, 8)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(63, 23)
    Me.Label3.TabIndex = 4
    Me.Label3.Text = "Warnungen"
    Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblWarnCount
    '
    Me.lblWarnCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
    Me.lblWarnCount.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblWarnCount.ForeColor = System.Drawing.Color.White
    Me.lblWarnCount.Location = New System.Drawing.Point(195, 8)
    Me.lblWarnCount.Name = "lblWarnCount"
    Me.lblWarnCount.Size = New System.Drawing.Size(44, 23)
    Me.lblWarnCount.TabIndex = 3
    Me.lblWarnCount.Text = "7"
    Me.lblWarnCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label2
    '
    Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(67, Byte), Integer), CType(CType(67, Byte), Integer))
    Me.Label2.ForeColor = System.Drawing.Color.LightGray
    Me.Label2.Location = New System.Drawing.Point(148, 8)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(39, 24)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Fehler"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblErrCount
    '
    Me.lblErrCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(119, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
    Me.lblErrCount.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblErrCount.ForeColor = System.Drawing.Color.White
    Me.lblErrCount.Location = New System.Drawing.Point(95, 8)
    Me.lblErrCount.Name = "lblErrCount"
    Me.lblErrCount.Size = New System.Drawing.Size(51, 24)
    Me.lblErrCount.TabIndex = 1
    Me.lblErrCount.Text = "7"
    Me.lblErrCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Panel2
    '
    Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(111, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
    Me.Panel2.Controls.Add(Me.PictureBox2)
    Me.Panel2.Controls.Add(Me.Label11)
    Me.Panel2.Controls.Add(Me.Label10)
    Me.Panel2.Controls.Add(Me.lblErrType)
    Me.Panel2.Controls.Add(Me.lblLine)
    Me.Panel2.Controls.Add(Me.TextBox1)
    Me.Panel2.Location = New System.Drawing.Point(0, 41)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(537, 86)
    Me.Panel2.TabIndex = 30
    '
    'PictureBox2
    '
    Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Hand
    Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
    Me.PictureBox2.Location = New System.Drawing.Point(12, -40)
    Me.PictureBox2.Name = "PictureBox2"
    Me.PictureBox2.Size = New System.Drawing.Size(64, 64)
    Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox2.TabIndex = 6
    Me.PictureBox2.TabStop = False
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.ForeColor = System.Drawing.Color.LightGray
    Me.Label11.Location = New System.Drawing.Point(61, 46)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(25, 13)
    Me.Label11.TabIndex = 5
    Me.Label11.Text = "Col:"
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.ForeColor = System.Drawing.Color.LightGray
    Me.Label10.Location = New System.Drawing.Point(10, 46)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(30, 13)
    Me.Label10.TabIndex = 4
    Me.Label10.Text = "Line:"
    '
    'lblErrType
    '
    Me.lblErrType.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblErrType.ForeColor = System.Drawing.Color.LightGray
    Me.lblErrType.Location = New System.Drawing.Point(10, 12)
    Me.lblErrType.Name = "lblErrType"
    Me.lblErrType.Size = New System.Drawing.Size(79, 36)
    Me.lblErrType.TabIndex = 2
    Me.lblErrType.Text = "BC14499"
    Me.lblErrType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblLine
    '
    Me.lblLine.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblLine.ForeColor = System.Drawing.Color.WhiteSmoke
    Me.lblLine.Location = New System.Drawing.Point(3, 59)
    Me.lblLine.Name = "lblLine"
    Me.lblLine.Size = New System.Drawing.Size(91, 21)
    Me.lblLine.TabIndex = 1
    Me.lblLine.Text = "*1234 : 0000"
    Me.lblLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'TextBox1
    '
    Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBox1.Location = New System.Drawing.Point(95, 6)
    Me.TextBox1.Multiline = True
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(432, 72)
    Me.TextBox1.TabIndex = 0
    '
    'frmTB_compileErrors
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(538, 418)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.igCompileErrors)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_compileErrors"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
    Me.Text = "Error List"
    CType(Me.igCompileErrors, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents igCompileErrors As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents iml_TraceTypes As System.Windows.Forms.ImageList
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents lblErrCount As System.Windows.Forms.Label
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents lblWarnCount As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents lblClassName As System.Windows.Forms.Label
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents lblErrType As System.Windows.Forms.Label
  Friend WithEvents lblLine As System.Windows.Forms.Label
  Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
End Class
