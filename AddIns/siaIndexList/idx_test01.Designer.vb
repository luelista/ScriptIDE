<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class idx_test01
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(idx_test01))
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.SuspendLayout()
    '
    'ListView1
    '
    Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
    Me.ListView1.FullRowSelect = True
    Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.ListView1.HideSelection = False
    Me.ListView1.Location = New System.Drawing.Point(0, 0)
    Me.ListView1.MultiSelect = False
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(209, 416)
    Me.ListView1.SmallImageList = Me.ImageList1
    Me.ListView1.TabIndex = 0
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Width = 179
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList1.Images.SetKeyName(0, "title")
    Me.ImageList1.Images.SetKeyName(1, "prop")
    Me.ImageList1.Images.SetKeyName(2, "sub")
    Me.ImageList1.Images.SetKeyName(3, "enum")
    Me.ImageList1.Images.SetKeyName(4, "func")
    Me.ImageList1.Images.SetKeyName(5, "class")
    '
    'idx_test01
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.ListView1)
    Me.Name = "idx_test01"
    Me.Size = New System.Drawing.Size(209, 417)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList

End Class
