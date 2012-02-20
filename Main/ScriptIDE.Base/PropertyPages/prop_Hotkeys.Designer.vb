<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prop_Hotkeys
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(prop_Hotkeys))
    Me.igHotkeysCol6CellStyle = New TenTec.Windows.iGridLib.iGCellStyle(True)
    Me.igHotkeysCol6ColHdrStyle = New TenTec.Windows.iGridLib.iGColHdrStyle(True)
    Me.imlToolbarIcons = New System.Windows.Forms.ImageList(Me.components)
    Me.txtSuchHotkey = New System.Windows.Forms.TextBox
    Me.btnHotkeyNew = New System.Windows.Forms.Button
    Me.igHotkeys = New TenTec.Windows.iGridLib.iGrid
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.txtScanCode = New System.Windows.Forms.TextBox
    Me.chk_scanCode = New System.Windows.Forms.CheckBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.chk_Win = New System.Windows.Forms.CheckBox
    Me.chk_Alt = New System.Windows.Forms.CheckBox
    Me.chk_Shift = New System.Windows.Forms.CheckBox
    Me.chk_Control = New System.Windows.Forms.CheckBox
    Me.txtKeyCode = New System.Windows.Forms.TextBox
    Me.GroupBox2 = New System.Windows.Forms.GroupBox
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.Label3 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.ComboBox1 = New System.Windows.Forms.ComboBox
    Me.PictureBox1 = New System.Windows.Forms.PictureBox
    Me.Panel1 = New System.Windows.Forms.Panel
    Me.btnSave = New System.Windows.Forms.Button
    Me.Label4 = New System.Windows.Forms.Label
    Me.PictureBox2 = New System.Windows.Forms.PictureBox
    CType(Me.igHotkeys, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox1.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'imlToolbarIcons
    '
    Me.imlToolbarIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.imlToolbarIcons.ImageSize = New System.Drawing.Size(16, 16)
    Me.imlToolbarIcons.TransparentColor = System.Drawing.Color.Transparent
    '
    'txtSuchHotkey
    '
    Me.txtSuchHotkey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSuchHotkey.BackColor = System.Drawing.Color.White
    Me.txtSuchHotkey.Location = New System.Drawing.Point(104, 47)
    Me.txtSuchHotkey.Name = "txtSuchHotkey"
    Me.txtSuchHotkey.ReadOnly = True
    Me.txtSuchHotkey.Size = New System.Drawing.Size(300, 20)
    Me.txtSuchHotkey.TabIndex = 14
    Me.txtSuchHotkey.Text = "     [  Hotkey-Suche: hier klicken und Taste drücken ...  ]"
    '
    'btnHotkeyNew
    '
    Me.btnHotkeyNew.Location = New System.Drawing.Point(3, 45)
    Me.btnHotkeyNew.Name = "btnHotkeyNew"
    Me.btnHotkeyNew.Size = New System.Drawing.Size(75, 23)
    Me.btnHotkeyNew.TabIndex = 13
    Me.btnHotkeyNew.Text = "Neu"
    Me.btnHotkeyNew.UseVisualStyleBackColor = True
    '
    'igHotkeys
    '
    Me.igHotkeys.AllowDrop = True
    Me.igHotkeys.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.igHotkeys.AutoResizeCols = True
    Me.igHotkeys.Header.AllowPress = False
    Me.igHotkeys.Location = New System.Drawing.Point(3, 74)
    Me.igHotkeys.Name = "igHotkeys"
    Me.igHotkeys.RowMode = True
    Me.igHotkeys.RowModeHasCurCell = True
    Me.igHotkeys.Size = New System.Drawing.Size(454, 122)
    Me.igHotkeys.TabIndex = 11
    Me.igHotkeys.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.txtScanCode)
    Me.GroupBox1.Controls.Add(Me.chk_scanCode)
    Me.GroupBox1.Controls.Add(Me.PictureBox1)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.chk_Win)
    Me.GroupBox1.Controls.Add(Me.chk_Alt)
    Me.GroupBox1.Controls.Add(Me.chk_Shift)
    Me.GroupBox1.Controls.Add(Me.chk_Control)
    Me.GroupBox1.Controls.Add(Me.txtKeyCode)
    Me.GroupBox1.Location = New System.Drawing.Point(3, 201)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(454, 67)
    Me.GroupBox1.TabIndex = 17
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Tastenkombination"
    '
    'txtScanCode
    '
    Me.txtScanCode.Enabled = False
    Me.txtScanCode.Location = New System.Drawing.Point(301, 41)
    Me.txtScanCode.Name = "txtScanCode"
    Me.txtScanCode.Size = New System.Drawing.Size(100, 20)
    Me.txtScanCode.TabIndex = 7
    '
    'chk_scanCode
    '
    Me.chk_scanCode.AutoSize = True
    Me.chk_scanCode.Location = New System.Drawing.Point(301, 21)
    Me.chk_scanCode.Name = "chk_scanCode"
    Me.chk_scanCode.Size = New System.Drawing.Size(77, 17)
    Me.chk_scanCode.TabIndex = 6
    Me.chk_scanCode.Text = "scanCode:"
    Me.chk_scanCode.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(170, 22)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(16, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "..."
    '
    'chk_Win
    '
    Me.chk_Win.AutoSize = True
    Me.chk_Win.Location = New System.Drawing.Point(217, 43)
    Me.chk_Win.Name = "chk_Win"
    Me.chk_Win.Size = New System.Drawing.Size(45, 17)
    Me.chk_Win.TabIndex = 5
    Me.chk_Win.Text = "Win"
    Me.chk_Win.UseVisualStyleBackColor = True
    '
    'chk_Alt
    '
    Me.chk_Alt.AutoSize = True
    Me.chk_Alt.Location = New System.Drawing.Point(173, 43)
    Me.chk_Alt.Name = "chk_Alt"
    Me.chk_Alt.Size = New System.Drawing.Size(38, 17)
    Me.chk_Alt.TabIndex = 4
    Me.chk_Alt.Text = "Alt"
    Me.chk_Alt.UseVisualStyleBackColor = True
    '
    'chk_Shift
    '
    Me.chk_Shift.AutoSize = True
    Me.chk_Shift.Location = New System.Drawing.Point(120, 43)
    Me.chk_Shift.Name = "chk_Shift"
    Me.chk_Shift.Size = New System.Drawing.Size(47, 17)
    Me.chk_Shift.TabIndex = 3
    Me.chk_Shift.Text = "Shift"
    Me.chk_Shift.UseVisualStyleBackColor = True
    '
    'chk_Control
    '
    Me.chk_Control.AutoSize = True
    Me.chk_Control.Location = New System.Drawing.Point(69, 43)
    Me.chk_Control.Name = "chk_Control"
    Me.chk_Control.Size = New System.Drawing.Size(45, 17)
    Me.chk_Control.TabIndex = 2
    Me.chk_Control.Text = "Strg"
    Me.chk_Control.UseVisualStyleBackColor = True
    '
    'txtKeyCode
    '
    Me.txtKeyCode.BackColor = System.Drawing.Color.White
    Me.txtKeyCode.Location = New System.Drawing.Point(69, 19)
    Me.txtKeyCode.Name = "txtKeyCode"
    Me.txtKeyCode.ReadOnly = True
    Me.txtKeyCode.Size = New System.Drawing.Size(94, 20)
    Me.txtKeyCode.TabIndex = 0
    '
    'GroupBox2
    '
    Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox2.Controls.Add(Me.TextBox1)
    Me.GroupBox2.Controls.Add(Me.Label3)
    Me.GroupBox2.Controls.Add(Me.Label2)
    Me.GroupBox2.Controls.Add(Me.ComboBox1)
    Me.GroupBox2.Location = New System.Drawing.Point(3, 275)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(453, 75)
    Me.GroupBox2.TabIndex = 18
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Verbundene Aktion"
    '
    'TextBox1
    '
    Me.TextBox1.Location = New System.Drawing.Point(103, 48)
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(275, 20)
    Me.TextBox1.TabIndex = 3
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(14, 51)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(58, 13)
    Me.Label3.TabIndex = 2
    Me.Label3.Text = "Parameter:"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(14, 22)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(30, 13)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Item:"
    '
    'ComboBox1
    '
    Me.ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
    Me.ComboBox1.DropDownHeight = 120
    Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBox1.FormattingEnabled = True
    Me.ComboBox1.IntegralHeight = False
    Me.ComboBox1.ItemHeight = 18
    Me.ComboBox1.Location = New System.Drawing.Point(103, 19)
    Me.ComboBox1.Name = "ComboBox1"
    Me.ComboBox1.Size = New System.Drawing.Size(275, 24)
    Me.ComboBox1.TabIndex = 0
    '
    'PictureBox1
    '
    Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
    Me.PictureBox1.Location = New System.Drawing.Point(17, 19)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox1.TabIndex = 6
    Me.PictureBox1.TabStop = False
    '
    'Panel1
    '
    Me.Panel1.BackColor = System.Drawing.SystemColors.Info
    Me.Panel1.Controls.Add(Me.PictureBox2)
    Me.Panel1.Controls.Add(Me.Label4)
    Me.Panel1.Controls.Add(Me.btnSave)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(460, 40)
    Me.Panel1.TabIndex = 19
    '
    'btnSave
    '
    Me.btnSave.BackColor = System.Drawing.SystemColors.Control
    Me.btnSave.Location = New System.Drawing.Point(54, 7)
    Me.btnSave.Name = "btnSave"
    Me.btnSave.Size = New System.Drawing.Size(97, 24)
    Me.btnSave.TabIndex = 0
    Me.btnSave.Text = "Speichern"
    Me.btnSave.UseVisualStyleBackColor = False
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(161, 6)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(288, 26)
    Me.Label4.TabIndex = 1
    Me.Label4.Text = "Achtung: Änderungen aus den neuen Hotkey-Einstellungen" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "gehen verloren!"
    '
    'PictureBox2
    '
    Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
    Me.PictureBox2.Location = New System.Drawing.Point(10, 4)
    Me.PictureBox2.Name = "PictureBox2"
    Me.PictureBox2.Size = New System.Drawing.Size(32, 32)
    Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.PictureBox2.TabIndex = 2
    Me.PictureBox2.TabStop = False
    '
    'prop_Hotkeys
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.txtSuchHotkey)
    Me.Controls.Add(Me.btnHotkeyNew)
    Me.Controls.Add(Me.igHotkeys)
    Me.Name = "prop_Hotkeys"
    Me.Size = New System.Drawing.Size(460, 352)
    CType(Me.igHotkeys, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.GroupBox2.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents imlToolbarIcons As System.Windows.Forms.ImageList
  Friend WithEvents txtSuchHotkey As System.Windows.Forms.TextBox
  Friend WithEvents btnHotkeyNew As System.Windows.Forms.Button
  Friend WithEvents igHotkeys As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents txtScanCode As System.Windows.Forms.TextBox
  Friend WithEvents chk_scanCode As System.Windows.Forms.CheckBox
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents chk_Win As System.Windows.Forms.CheckBox
  Friend WithEvents chk_Alt As System.Windows.Forms.CheckBox
  Friend WithEvents chk_Shift As System.Windows.Forms.CheckBox
  Friend WithEvents chk_Control As System.Windows.Forms.CheckBox
  Friend WithEvents txtKeyCode As System.Windows.Forms.TextBox
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
  Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
  Friend WithEvents igHotkeysCol6CellStyle As TenTec.Windows.iGridLib.iGCellStyle
  Friend WithEvents igHotkeysCol6ColHdrStyle As TenTec.Windows.iGridLib.iGColHdrStyle
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents btnSave As System.Windows.Forms.Button
  Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox

End Class
