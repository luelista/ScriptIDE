<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_editBookmark
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_editBookmark))
    Me.grpEditbookmark = New System.Windows.Forms.GroupBox
    Me.PictureBox3 = New System.Windows.Forms.PictureBox
    Me.btnEditbookmark_delete = New System.Windows.Forms.Button
    Me.btnEditbookmark_close = New System.Windows.Forms.Button
    Me.btnEditbookmark_save = New System.Windows.Forms.Button
    Me.txtEditbookmark_url = New System.Windows.Forms.TextBox
    Me.txtEditbookmark_text = New System.Windows.Forms.TextBox
    Me.Label5 = New System.Windows.Forms.Label
    Me.Label6 = New System.Windows.Forms.Label
    Me.grpEditbookmark.SuspendLayout()
    CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'grpEditbookmark
    '
    Me.grpEditbookmark.BackColor = System.Drawing.Color.SkyBlue
    Me.grpEditbookmark.Controls.Add(Me.PictureBox3)
    Me.grpEditbookmark.Controls.Add(Me.btnEditbookmark_delete)
    Me.grpEditbookmark.Controls.Add(Me.btnEditbookmark_close)
    Me.grpEditbookmark.Controls.Add(Me.btnEditbookmark_save)
    Me.grpEditbookmark.Controls.Add(Me.txtEditbookmark_url)
    Me.grpEditbookmark.Controls.Add(Me.txtEditbookmark_text)
    Me.grpEditbookmark.Controls.Add(Me.Label5)
    Me.grpEditbookmark.Controls.Add(Me.Label6)
    Me.grpEditbookmark.Location = New System.Drawing.Point(0, 0)
    Me.grpEditbookmark.Name = "grpEditbookmark"
    Me.grpEditbookmark.Size = New System.Drawing.Size(533, 199)
    Me.grpEditbookmark.TabIndex = 28
    Me.grpEditbookmark.TabStop = False
    Me.grpEditbookmark.Text = "Bookmark bearbeiten"
    Me.grpEditbookmark.Visible = False
    '
    'PictureBox3
    '
    Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
    Me.PictureBox3.Location = New System.Drawing.Point(546, 38)
    Me.PictureBox3.Name = "PictureBox3"
    Me.PictureBox3.Size = New System.Drawing.Size(67, 63)
    Me.PictureBox3.TabIndex = 8
    Me.PictureBox3.TabStop = False
    '
    'btnEditbookmark_delete
    '
    Me.btnEditbookmark_delete.Image = CType(resources.GetObject("btnEditbookmark_delete.Image"), System.Drawing.Image)
    Me.btnEditbookmark_delete.Location = New System.Drawing.Point(346, 130)
    Me.btnEditbookmark_delete.Name = "btnEditbookmark_delete"
    Me.btnEditbookmark_delete.Size = New System.Drawing.Size(142, 35)
    Me.btnEditbookmark_delete.TabIndex = 6
    Me.btnEditbookmark_delete.Text = "Löschen"
    Me.btnEditbookmark_delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnEditbookmark_delete.UseVisualStyleBackColor = True
    '
    'btnEditbookmark_close
    '
    Me.btnEditbookmark_close.Image = CType(resources.GetObject("btnEditbookmark_close.Image"), System.Drawing.Image)
    Me.btnEditbookmark_close.Location = New System.Drawing.Point(185, 130)
    Me.btnEditbookmark_close.Name = "btnEditbookmark_close"
    Me.btnEditbookmark_close.Size = New System.Drawing.Size(142, 35)
    Me.btnEditbookmark_close.TabIndex = 5
    Me.btnEditbookmark_close.Text = "   Abbrechen"
    Me.btnEditbookmark_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnEditbookmark_close.UseVisualStyleBackColor = True
    '
    'btnEditbookmark_save
    '
    Me.btnEditbookmark_save.Image = CType(resources.GetObject("btnEditbookmark_save.Image"), System.Drawing.Image)
    Me.btnEditbookmark_save.Location = New System.Drawing.Point(24, 130)
    Me.btnEditbookmark_save.Name = "btnEditbookmark_save"
    Me.btnEditbookmark_save.Size = New System.Drawing.Size(142, 35)
    Me.btnEditbookmark_save.TabIndex = 4
    Me.btnEditbookmark_save.Text = "   OK"
    Me.btnEditbookmark_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.btnEditbookmark_save.UseVisualStyleBackColor = True
    '
    'txtEditbookmark_url
    '
    Me.txtEditbookmark_url.Location = New System.Drawing.Point(24, 95)
    Me.txtEditbookmark_url.Name = "txtEditbookmark_url"
    Me.txtEditbookmark_url.Size = New System.Drawing.Size(503, 20)
    Me.txtEditbookmark_url.TabIndex = 3
    '
    'txtEditbookmark_text
    '
    Me.txtEditbookmark_text.Location = New System.Drawing.Point(24, 47)
    Me.txtEditbookmark_text.Name = "txtEditbookmark_text"
    Me.txtEditbookmark_text.Size = New System.Drawing.Size(503, 20)
    Me.txtEditbookmark_text.TabIndex = 2
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.ForeColor = System.Drawing.Color.Black
    Me.Label5.Location = New System.Drawing.Point(21, 79)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(45, 13)
    Me.Label5.TabIndex = 1
    Me.Label5.Text = "FileTag:"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.ForeColor = System.Drawing.Color.Black
    Me.Label6.Location = New System.Drawing.Point(21, 31)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(69, 13)
    Me.Label6.TabIndex = 0
    Me.Label6.Text = "Beschriftung:"
    '
    'frm_editBookmark
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(532, 198)
    Me.Controls.Add(Me.grpEditbookmark)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frm_editBookmark"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
    Me.Text = "Bookmark bearbeiten"
    Me.grpEditbookmark.ResumeLayout(False)
    Me.grpEditbookmark.PerformLayout()
    CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents grpEditbookmark As System.Windows.Forms.GroupBox
  Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
  Friend WithEvents btnEditbookmark_delete As System.Windows.Forms.Button
  Friend WithEvents btnEditbookmark_close As System.Windows.Forms.Button
  Friend WithEvents btnEditbookmark_save As System.Windows.Forms.Button
  Friend WithEvents txtEditbookmark_url As System.Windows.Forms.TextBox
  Friend WithEvents txtEditbookmark_text As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
