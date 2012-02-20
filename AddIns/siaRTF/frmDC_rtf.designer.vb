<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDC_rtf
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDC_rtf))
    Me.mRTF = New siaRTF.MyExtRichTextBox.MyExtRichTextBox
    Me.tsIntellibar = New System.Windows.Forms.ToolStrip
    Me.tsbSave = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
    Me.tsddbZoom = New System.Windows.Forms.ToolStripDropDownButton
    Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
    Me.txtSearch = New System.Windows.Forms.ToolStripTextBox
    Me.tsIntellibar.SuspendLayout()
    Me.SuspendLayout()
    '
    'mRTF
    '
    Me.mRTF.Dock = System.Windows.Forms.DockStyle.Fill
    Me.mRTF.HideSelection = False
    Me.mRTF.Location = New System.Drawing.Point(0, 25)
    Me.mRTF.Name = "mRTF"
    Me.mRTF.ShowSelectionMargin = True
    Me.mRTF.Size = New System.Drawing.Size(638, 409)
    Me.mRTF.TabIndex = 0
    Me.mRTF.Text = ""
    '
    'tsIntellibar
    '
    Me.tsIntellibar.AllowDrop = True
    Me.tsIntellibar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSave, Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator2, Me.tsddbZoom, Me.ToolStripSeparator1, Me.ToolStripLabel1, Me.txtSearch})
    Me.tsIntellibar.Location = New System.Drawing.Point(0, 0)
    Me.tsIntellibar.Name = "tsIntellibar"
    Me.tsIntellibar.Size = New System.Drawing.Size(638, 25)
    Me.tsIntellibar.TabIndex = 2
    Me.tsIntellibar.Text = "ToolStrip1"
    '
    'tsbSave
    '
    Me.tsbSave.Image = CType(resources.GetObject("tsbSave.Image"), System.Drawing.Image)
    Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsbSave.Name = "tsbSave"
    Me.tsbSave.Size = New System.Drawing.Size(79, 22)
    Me.tsbSave.Text = "Speichern"
    '
    'ToolStripButton1
    '
    Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
    Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton1.Name = "ToolStripButton1"
    Me.ToolStripButton1.Size = New System.Drawing.Size(112, 22)
    Me.ToolStripButton1.Text = "Objekt einfügen"
    '
    'ToolStripButton2
    '
    Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
    Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.ToolStripButton2.Name = "ToolStripButton2"
    Me.ToolStripButton2.Size = New System.Drawing.Size(64, 22)
    Me.ToolStripButton2.Text = "Formel"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
    '
    'tsddbZoom
    '
    Me.tsddbZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
    Me.tsddbZoom.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5, Me.ToolStripMenuItem6, Me.ToolStripMenuItem7, Me.ToolStripMenuItem8, Me.ToolStripMenuItem9, Me.ToolStripMenuItem10})
    Me.tsddbZoom.Image = CType(resources.GetObject("tsddbZoom.Image"), System.Drawing.Image)
    Me.tsddbZoom.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.tsddbZoom.Name = "tsddbZoom"
    Me.tsddbZoom.Size = New System.Drawing.Size(48, 22)
    Me.tsddbZoom.Text = "100%"
    '
    'ToolStripMenuItem2
    '
    Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
    Me.ToolStripMenuItem2.Size = New System.Drawing.Size(102, 22)
    Me.ToolStripMenuItem2.Text = "25%"
    '
    'ToolStripMenuItem3
    '
    Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
    Me.ToolStripMenuItem3.Size = New System.Drawing.Size(102, 22)
    Me.ToolStripMenuItem3.Text = "50%"
    '
    'ToolStripMenuItem4
    '
    Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
    Me.ToolStripMenuItem4.Size = New System.Drawing.Size(102, 22)
    Me.ToolStripMenuItem4.Text = "75%"
    '
    'ToolStripMenuItem5
    '
    Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
    Me.ToolStripMenuItem5.Size = New System.Drawing.Size(102, 22)
    Me.ToolStripMenuItem5.Text = "83%"
    '
    'ToolStripMenuItem6
    '
    Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
    Me.ToolStripMenuItem6.Size = New System.Drawing.Size(102, 22)
    Me.ToolStripMenuItem6.Text = "100%"
    '
    'ToolStripMenuItem7
    '
    Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
    Me.ToolStripMenuItem7.Size = New System.Drawing.Size(102, 22)
    Me.ToolStripMenuItem7.Text = "133%"
    '
    'ToolStripMenuItem8
    '
    Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
    Me.ToolStripMenuItem8.Size = New System.Drawing.Size(102, 22)
    Me.ToolStripMenuItem8.Text = "150%"
    '
    'ToolStripMenuItem9
    '
    Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
    Me.ToolStripMenuItem9.Size = New System.Drawing.Size(102, 22)
    Me.ToolStripMenuItem9.Text = "200%"
    '
    'ToolStripMenuItem10
    '
    Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
    Me.ToolStripMenuItem10.Size = New System.Drawing.Size(102, 22)
    Me.ToolStripMenuItem10.Text = "500%"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
    '
    'ToolStripLabel1
    '
    Me.ToolStripLabel1.Name = "ToolStripLabel1"
    Me.ToolStripLabel1.Size = New System.Drawing.Size(41, 22)
    Me.ToolStripLabel1.Text = "suche:"
    '
    'txtSearch
    '
    Me.txtSearch.Name = "txtSearch"
    Me.txtSearch.Size = New System.Drawing.Size(100, 25)
    '
    'frmDC_rtf
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(638, 434)
    Me.Controls.Add(Me.mRTF)
    Me.Controls.Add(Me.tsIntellibar)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmDC_rtf"
    Me.Text = "frmDC_rtf"
    Me.tsIntellibar.ResumeLayout(False)
    Me.tsIntellibar.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents mRTF As MyExtRichTextBox.MyExtRichTextBox
  Friend WithEvents tsIntellibar As System.Windows.Forms.ToolStrip
  Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
  Friend WithEvents txtSearch As System.Windows.Forms.ToolStripTextBox
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents tsddbZoom As System.Windows.Forms.ToolStripDropDownButton
  Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
  Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
End Class
