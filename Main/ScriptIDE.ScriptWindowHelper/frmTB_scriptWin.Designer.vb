<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_scriptWin
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
    Me.cmCaption = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.AddInManagerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ScriptneuladenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ScriptBearbeitenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
    Me.SchließenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.pnlMain = New ScriptIDE.ScriptWindowHelper.ScriptedPanel
    Me.btnLoadScript = New System.Windows.Forms.Button
    Me.Label1 = New System.Windows.Forms.Label
    Me.cmCaption.SuspendLayout()
    Me.pnlMain.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmCaption
    '
    Me.cmCaption.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddInManagerToolStripMenuItem, Me.ScriptneuladenToolStripMenuItem, Me.ScriptBearbeitenToolStripMenuItem, Me.ToolStripMenuItem1, Me.SchließenToolStripMenuItem})
    Me.cmCaption.Name = "cmCaption"
    Me.cmCaption.Size = New System.Drawing.Size(165, 98)
    '
    'AddInManagerToolStripMenuItem
    '
    Me.AddInManagerToolStripMenuItem.Name = "AddInManagerToolStripMenuItem"
    Me.AddInManagerToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    Me.AddInManagerToolStripMenuItem.Text = "AddIn-Manager"
    '
    'ScriptneuladenToolStripMenuItem
    '
    Me.ScriptneuladenToolStripMenuItem.Name = "ScriptneuladenToolStripMenuItem"
    Me.ScriptneuladenToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    Me.ScriptneuladenToolStripMenuItem.Text = "Script (neu)laden"
    Me.ScriptneuladenToolStripMenuItem.Visible = False
    '
    'ScriptBearbeitenToolStripMenuItem
    '
    Me.ScriptBearbeitenToolStripMenuItem.Name = "ScriptBearbeitenToolStripMenuItem"
    Me.ScriptBearbeitenToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    Me.ScriptBearbeitenToolStripMenuItem.Text = "Script bearbeiten"
    Me.ScriptBearbeitenToolStripMenuItem.Visible = False
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(161, 6)
    '
    'SchließenToolStripMenuItem
    '
    Me.SchließenToolStripMenuItem.Name = "SchließenToolStripMenuItem"
    Me.SchließenToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
    Me.SchließenToolStripMenuItem.Text = "Schließen"
    '
    'pnlMain
    '
    Me.pnlMain.activateEvents = ""
    Me.pnlMain.ClassName = Nothing
    Me.pnlMain.Controls.Add(Me.btnLoadScript)
    Me.pnlMain.Controls.Add(Me.Label1)
    Me.pnlMain.direction = 1
    Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnlMain.insertX = 0
    Me.pnlMain.insertY = 0
    Me.pnlMain.Location = New System.Drawing.Point(0, 0)
    Me.pnlMain.Name = "pnlMain"
    Me.pnlMain.offsetX = 0
    Me.pnlMain.Size = New System.Drawing.Size(238, 404)
    Me.pnlMain.TabIndex = 0
    Me.pnlMain.WinID = ""
    '
    'btnLoadScript
    '
    Me.btnLoadScript.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.btnLoadScript.Enabled = False
    Me.btnLoadScript.Location = New System.Drawing.Point(21, 220)
    Me.btnLoadScript.Name = "btnLoadScript"
    Me.btnLoadScript.Size = New System.Drawing.Size(197, 23)
    Me.btnLoadScript.TabIndex = 1
    Me.btnLoadScript.Text = "Skript laden"
    Me.btnLoadScript.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(255, Byte), Integer))
    Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.White
    Me.Label1.Location = New System.Drawing.Point(21, 148)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(197, 68)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "bitte zuerst scriptedPanel.resetControls() aufrufen"
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'frmTB_scriptWin
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(238, 404)
    Me.Controls.Add(Me.pnlMain)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.KeyPreview = True
    Me.Name = "frmTB_scriptWin"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
    Me.Text = "ScriptWindow"
    Me.cmCaption.ResumeLayout(False)
    Me.pnlMain.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents pnlMain As ScriptedPanel
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents cmCaption As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents AddInManagerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ScriptneuladenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ScriptBearbeitenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents SchließenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents btnLoadScript As System.Windows.Forms.Button
End Class
