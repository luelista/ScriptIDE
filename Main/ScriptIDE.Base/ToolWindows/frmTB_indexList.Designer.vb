<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_indexList
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
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_indexList))
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.IndexlisteDeaktivierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
    Me.VersteckenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ContextMenuStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IndexlisteDeaktivierenToolStripMenuItem, Me.ToolStripMenuItem1, Me.VersteckenToolStripMenuItem})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(184, 54)
    '
    'IndexlisteDeaktivierenToolStripMenuItem
    '
    Me.IndexlisteDeaktivierenToolStripMenuItem.Name = "IndexlisteDeaktivierenToolStripMenuItem"
    Me.IndexlisteDeaktivierenToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
    Me.IndexlisteDeaktivierenToolStripMenuItem.Text = "Indexliste deaktivieren"
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(180, 6)
    '
    'VersteckenToolStripMenuItem
    '
    Me.VersteckenToolStripMenuItem.Name = "VersteckenToolStripMenuItem"
    Me.VersteckenToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
    Me.VersteckenToolStripMenuItem.Text = "Verstecken"
    '
    'frmTB_indexList
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(207, 409)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_indexList"
    Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft
    Me.TabPageContextMenuStrip = Me.ContextMenuStrip1
    Me.Text = "Indexliste"
    Me.ContextMenuStrip1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents IndexlisteDeaktivierenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents VersteckenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
