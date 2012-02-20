<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_about
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

    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents LabelProductName As System.Windows.Forms.Label
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
  Friend WithEvents OKButton As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_about))
    Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
    Me.LabelCopyright = New System.Windows.Forms.Label
    Me.LabelProductName = New System.Windows.Forms.Label
    Me.LabelVersion = New System.Windows.Forms.Label
    Me.RichTextPlus1 = New ScriptIDE.ScriptWindowHelper.RichTextPlus
    Me.OKButton = New System.Windows.Forms.Button
    Me.LogoPictureBox = New System.Windows.Forms.PictureBox
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.TableLayoutPanel.SuspendLayout()
    CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel
    '
    Me.TableLayoutPanel.BackColor = System.Drawing.Color.Transparent
    Me.TableLayoutPanel.ColumnCount = 1
    Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
    Me.TableLayoutPanel.Controls.Add(Me.LabelCopyright, 0, 3)
    Me.TableLayoutPanel.Controls.Add(Me.LabelProductName, 0, 0)
    Me.TableLayoutPanel.Controls.Add(Me.LabelVersion, 0, 1)
    Me.TableLayoutPanel.Controls.Add(Me.RichTextPlus1, 0, 2)
    Me.TableLayoutPanel.Controls.Add(Me.OKButton, 0, 4)
    Me.TableLayoutPanel.Location = New System.Drawing.Point(124, 9)
    Me.TableLayoutPanel.Name = "TableLayoutPanel"
    Me.TableLayoutPanel.RowCount = 5
    Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 139.0!))
    Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
    Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    Me.TableLayoutPanel.Size = New System.Drawing.Size(354, 243)
    Me.TableLayoutPanel.TabIndex = 0
    '
    'LabelCopyright
    '
    Me.LabelCopyright.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelCopyright.Location = New System.Drawing.Point(6, 189)
    Me.LabelCopyright.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
    Me.LabelCopyright.MaximumSize = New System.Drawing.Size(0, 17)
    Me.LabelCopyright.Name = "LabelCopyright"
    Me.LabelCopyright.Size = New System.Drawing.Size(345, 17)
    Me.LabelCopyright.TabIndex = 2
    Me.LabelCopyright.Text = "Copyright"
    Me.LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'LabelProductName
    '
    Me.LabelProductName.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelProductName.Location = New System.Drawing.Point(6, 0)
    Me.LabelProductName.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
    Me.LabelProductName.MaximumSize = New System.Drawing.Size(0, 17)
    Me.LabelProductName.Name = "LabelProductName"
    Me.LabelProductName.Size = New System.Drawing.Size(345, 17)
    Me.LabelProductName.TabIndex = 0
    Me.LabelProductName.Text = "Product Name"
    Me.LabelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'LabelVersion
    '
    Me.LabelVersion.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelVersion.Location = New System.Drawing.Point(6, 25)
    Me.LabelVersion.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
    Me.LabelVersion.MaximumSize = New System.Drawing.Size(0, 17)
    Me.LabelVersion.Name = "LabelVersion"
    Me.LabelVersion.Size = New System.Drawing.Size(345, 17)
    Me.LabelVersion.TabIndex = 0
    Me.LabelVersion.Text = "Version"
    Me.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'RichTextPlus1
    '
    Me.RichTextPlus1.BackColor = System.Drawing.Color.White
    Me.RichTextPlus1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.RichTextPlus1.DetectUrls = False
    Me.RichTextPlus1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.RichTextPlus1.HTMLCode = Nothing
    Me.RichTextPlus1.Location = New System.Drawing.Point(10, 53)
    Me.RichTextPlus1.Margin = New System.Windows.Forms.Padding(10, 3, 3, 3)
    Me.RichTextPlus1.Name = "RichTextPlus1"
    Me.RichTextPlus1.ReadOnly = True
    Me.RichTextPlus1.Size = New System.Drawing.Size(341, 133)
    Me.RichTextPlus1.TabIndex = 1
    Me.RichTextPlus1.Text = "zzzzzzzzz"
    '
    'OKButton
    '
    Me.OKButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.OKButton.Location = New System.Drawing.Point(253, 217)
    Me.OKButton.Name = "OKButton"
    Me.OKButton.Size = New System.Drawing.Size(98, 23)
    Me.OKButton.TabIndex = 0
    Me.OKButton.Text = "&OK"
    Me.OKButton.UseVisualStyleBackColor = True
    '
    'LogoPictureBox
    '
    Me.LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), System.Drawing.Image)
    Me.LogoPictureBox.Location = New System.Drawing.Point(0, 0)
    Me.LogoPictureBox.Name = "LogoPictureBox"
    Me.LogoPictureBox.Size = New System.Drawing.Size(120, 262)
    Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.LogoPictureBox.TabIndex = 0
    Me.LogoPictureBox.TabStop = False
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(423, 6)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(52, 52)
    Me.PictureBox1.TabIndex = 1
    Me.PictureBox1.TabStop = False
    '
    'frm_about
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.White
    Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
    Me.CancelButton = Me.OKButton
    Me.ClientSize = New System.Drawing.Size(486, 262)
    Me.Controls.Add(Me.LogoPictureBox)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.TableLayoutPanel)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frm_about"
    Me.Padding = New System.Windows.Forms.Padding(9)
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "frm_about"
    Me.TableLayoutPanel.ResumeLayout(False)
    CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents LabelCopyright As System.Windows.Forms.Label
  Friend WithEvents RichTextPlus1 As ScriptIDE.ScriptWindowHelper.RichTextPlus

End Class
