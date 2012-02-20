<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_splash
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_splash))
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.BackColor = System.Drawing.Color.Transparent
    Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.White
    Me.Label1.Location = New System.Drawing.Point(15, 179)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(478, 57)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Anwendung wird initialisiert ..."
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomRight
    '
    'Label2
    '
    Me.Label2.BackColor = System.Drawing.Color.Transparent
    Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label2.ForeColor = System.Drawing.Color.White
    Me.Label2.Location = New System.Drawing.Point(70, 89)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(423, 45)
    Me.Label2.TabIndex = 2
    Me.Label2.Text = "Copyright © 2008-2012 Max Weller" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "http://scriptide.wikilab.de"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'Label3
    '
    Me.Label3.BackColor = System.Drawing.Color.Transparent
    Me.Label3.Font = New System.Drawing.Font("Segoe UI", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.ForeColor = System.Drawing.Color.White
    Me.Label3.Location = New System.Drawing.Point(42, 14)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(456, 82)
    Me.Label3.TabIndex = 5
    Me.Label3.Text = "scriptIDE 4"
    Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'Label4
    '
    Me.Label4.BackColor = System.Drawing.Color.Transparent
    Me.Label4.Font = New System.Drawing.Font("Segoe UI", 11.25!)
    Me.Label4.ForeColor = System.Drawing.Color.White
    Me.Label4.Location = New System.Drawing.Point(69, 141)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(424, 23)
    Me.Label4.TabIndex = 6
    Me.Label4.Text = "Aktives Profil:"
    Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
    '
    'frm_splash
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.Black
    Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
    Me.ClientSize = New System.Drawing.Size(522, 260)
    Me.ControlBox = False
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Label3)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frm_splash"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "ScriptIDE 4 - Anwendung wird initialisiert ..."
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
