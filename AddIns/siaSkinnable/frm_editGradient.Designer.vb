<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_editGradient
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_editGradient))
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.OK_Button = New System.Windows.Forms.Button
    Me.Cancel_Button = New System.Windows.Forms.Button
    Me.Label1 = New System.Windows.Forms.Label
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.Button1 = New System.Windows.Forms.Button
    Me.Button2 = New System.Windows.Forms.Button
    Me.TextBox2 = New System.Windows.Forms.TextBox
    Me.Label2 = New System.Windows.Forms.Label
    Me.Button3 = New System.Windows.Forms.Button
    Me.TextBox3 = New System.Windows.Forms.TextBox
    Me.Label3 = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.ComboBox1 = New System.Windows.Forms.ComboBox
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
    Me.TableLayoutPanel1.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(205, 189)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(180, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(84, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(93, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(84, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Abbrechen"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(87, 59)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(56, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Startfarbe:"
    '
    'TextBox1
    '
    Me.TextBox1.Location = New System.Drawing.Point(188, 56)
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(103, 20)
    Me.TextBox1.TabIndex = 2
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(304, 56)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(41, 20)
    Me.Button1.TabIndex = 3
    Me.Button1.Text = "..."
    Me.Button1.UseVisualStyleBackColor = True
    '
    'Button2
    '
    Me.Button2.Location = New System.Drawing.Point(304, 90)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(41, 20)
    Me.Button2.TabIndex = 6
    Me.Button2.Text = "..."
    Me.Button2.UseVisualStyleBackColor = True
    '
    'TextBox2
    '
    Me.TextBox2.Location = New System.Drawing.Point(188, 90)
    Me.TextBox2.Name = "TextBox2"
    Me.TextBox2.Size = New System.Drawing.Size(103, 20)
    Me.TextBox2.TabIndex = 5
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(87, 93)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(53, 13)
    Me.Label2.TabIndex = 4
    Me.Label2.Text = "Endfarbe:"
    '
    'Button3
    '
    Me.Button3.Location = New System.Drawing.Point(304, 124)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(41, 20)
    Me.Button3.TabIndex = 9
    Me.Button3.Text = "..."
    Me.Button3.UseVisualStyleBackColor = True
    '
    'TextBox3
    '
    Me.TextBox3.Location = New System.Drawing.Point(188, 124)
    Me.TextBox3.Name = "TextBox3"
    Me.TextBox3.Size = New System.Drawing.Size(103, 20)
    Me.TextBox3.TabIndex = 8
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(87, 127)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(55, 13)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "Textfarbe:"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(87, 25)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(60, 13)
    Me.Label4.TabIndex = 10
    Me.Label4.Text = "Verlaufsart:"
    '
    'ComboBox1
    '
    Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBox1.FormattingEnabled = True
    Me.ComboBox1.Items.AddRange(New Object() {"Horizontal", "Vertical", "ForwardDiagonal", "BackwardDiagonal"})
    Me.ComboBox1.Location = New System.Drawing.Point(188, 22)
    Me.ComboBox1.Name = "ComboBox1"
    Me.ComboBox1.Size = New System.Drawing.Size(157, 21)
    Me.ComboBox1.TabIndex = 11
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.PictureBox1)
    Me.GroupBox1.Controls.Add(Me.ComboBox1)
    Me.GroupBox1.Controls.Add(Me.Label4)
    Me.GroupBox1.Controls.Add(Me.Button3)
    Me.GroupBox1.Controls.Add(Me.TextBox3)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.Button2)
    Me.GroupBox1.Controls.Add(Me.TextBox2)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Button1)
    Me.GroupBox1.Controls.Add(Me.TextBox1)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(17, 15)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(363, 160)
    Me.GroupBox1.TabIndex = 12
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Farbe für Element :"
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(17, 25)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 12
    Me.PictureBox1.TabStop = False
    '
    'ColorDialog1
    '
    Me.ColorDialog1.FullOpen = True
    '
    'frm_editGradient
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(397, 230)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frm_editGradient"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Farbverlauf bearbeiten"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Button3 As System.Windows.Forms.Button
  Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog

End Class
