<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTB_globSearch
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
    Dim IGColPattern1 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern2 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern3 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim IGColPattern4 As TenTec.Windows.iGridLib.iGColPattern = New TenTec.Windows.iGridLib.iGColPattern
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTB_globSearch))
    Me.Label10 = New System.Windows.Forms.Label
    Me.txtGlobsearchUndnicht = New System.Windows.Forms.TextBox
    Me.cmbGlobsearchFilefilter = New System.Windows.Forms.ComboBox
    Me.Label4 = New System.Windows.Forms.Label
    Me.btnDoGlobSearch = New System.Windows.Forms.Button
    Me.txtGlobsearchSuch = New System.Windows.Forms.TextBox
    Me.igGlobsearch = New TenTec.Windows.iGridLib.iGrid
    Me.cmenuGlobTreffer = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.DateiImEditorOeffnenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.WindowsExplorerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.DateimanagerNavigierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
    Me.FTPDateiOeffnenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.FTPOrdnerNavigierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
    Me.TrefferlisteLoeschenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.checkRekursiv = New System.Windows.Forms.CheckBox
    Me.Scintilla1 = New ScintillaNet.Scintilla
    Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
    Me.chkDockWin = New System.Windows.Forms.CheckBox
    Me.chkTopmost = New System.Windows.Forms.CheckBox
    Me.chkActFile = New System.Windows.Forms.CheckBox
    Me.btnZoomOnOff = New System.Windows.Forms.Button
    Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
    Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
    Me.ListBox1 = New System.Windows.Forms.ListBox
    Me.ftvGlobSearch = New AxCCRPFolderTV6.AxFolderTreeview
    CType(Me.igGlobsearch, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.cmenuGlobTreffer.SuspendLayout()
    CType(Me.Scintilla1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SplitContainer1.Panel1.SuspendLayout()
    Me.SplitContainer1.Panel2.SuspendLayout()
    Me.SplitContainer1.SuspendLayout()
    CType(Me.ftvGlobSearch, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Label10
    '
    Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label10.AutoSize = True
    Me.Label10.Location = New System.Drawing.Point(1062, 10)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(67, 13)
    Me.Label10.TabIndex = 15
    Me.Label10.Text = "UND NICHT"
    '
    'txtGlobsearchUndnicht
    '
    Me.txtGlobsearchUndnicht.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.txtGlobsearchUndnicht.Location = New System.Drawing.Point(1131, 7)
    Me.txtGlobsearchUndnicht.Name = "txtGlobsearchUndnicht"
    Me.txtGlobsearchUndnicht.Size = New System.Drawing.Size(155, 20)
    Me.txtGlobsearchUndnicht.TabIndex = 14
    '
    'cmbGlobsearchFilefilter
    '
    Me.cmbGlobsearchFilefilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmbGlobsearchFilefilter.FormattingEnabled = True
    Me.cmbGlobsearchFilefilter.Items.AddRange(New Object() {"*.php;*.inc;*.ttt;*.dmlc", "*.js", "*.tpl;*.html", "*.css", "*.php;*.inc;*.ttt;*.js;*.tpl;*.html;*.css;*.txt"})
    Me.cmbGlobsearchFilefilter.Location = New System.Drawing.Point(474, 7)
    Me.cmbGlobsearchFilefilter.Name = "cmbGlobsearchFilefilter"
    Me.cmbGlobsearchFilefilter.Size = New System.Drawing.Size(121, 21)
    Me.cmbGlobsearchFilefilter.TabIndex = 13
    '
    'Label4
    '
    Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(423, 10)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(45, 13)
    Me.Label4.TabIndex = 12
    Me.Label4.Text = "Filefilter:"
    '
    'btnDoGlobSearch
    '
    Me.btnDoGlobSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnDoGlobSearch.BackColor = System.Drawing.SystemColors.Control
    Me.btnDoGlobSearch.Location = New System.Drawing.Point(211, 5)
    Me.btnDoGlobSearch.Name = "btnDoGlobSearch"
    Me.btnDoGlobSearch.Size = New System.Drawing.Size(54, 23)
    Me.btnDoGlobSearch.TabIndex = 11
    Me.btnDoGlobSearch.Text = "such..."
    Me.btnDoGlobSearch.UseVisualStyleBackColor = True
    '
    'txtGlobsearchSuch
    '
    Me.txtGlobsearchSuch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.txtGlobsearchSuch.Location = New System.Drawing.Point(50, 6)
    Me.txtGlobsearchSuch.Name = "txtGlobsearchSuch"
    Me.txtGlobsearchSuch.Size = New System.Drawing.Size(155, 20)
    Me.txtGlobsearchSuch.TabIndex = 10
    '
    'igGlobsearch
    '
    Me.igGlobsearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.igGlobsearch.AutoResizeCols = True
    IGColPattern1.Key = "filename"
    IGColPattern1.Text = "File"
    IGColPattern1.Width = 234
    IGColPattern2.Key = "pos"
    IGColPattern2.Text = "Pos"
    IGColPattern2.Width = 176
    IGColPattern3.Key = "linecont"
    IGColPattern3.Text = "Line content"
    IGColPattern3.Width = 349
    IGColPattern4.Key = "rank"
    IGColPattern4.Text = "Rank"
    IGColPattern4.Width = 178
    Me.igGlobsearch.Cols.AddRange(New TenTec.Windows.iGridLib.iGColPattern() {IGColPattern1, IGColPattern2, IGColPattern3, IGColPattern4})
    Me.igGlobsearch.Header.Height = 19
    Me.igGlobsearch.Location = New System.Drawing.Point(3, 0)
    Me.igGlobsearch.Name = "igGlobsearch"
    Me.igGlobsearch.ReadOnly = True
    Me.igGlobsearch.Size = New System.Drawing.Size(941, 305)
    Me.igGlobsearch.TabIndex = 9
    '
    'cmenuGlobTreffer
    '
    Me.cmenuGlobTreffer.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DateiImEditorOeffnenToolStripMenuItem, Me.WindowsExplorerToolStripMenuItem, Me.DateimanagerNavigierenToolStripMenuItem, Me.ToolStripSeparator1, Me.FTPDateiOeffnenToolStripMenuItem, Me.FTPOrdnerNavigierenToolStripMenuItem, Me.ToolStripSeparator2, Me.TrefferlisteLoeschenToolStripMenuItem})
    Me.cmenuGlobTreffer.Name = "cmenuGlobTreffer"
    Me.cmenuGlobTreffer.Size = New System.Drawing.Size(207, 148)
    '
    'DateiImEditorOeffnenToolStripMenuItem
    '
    Me.DateiImEditorOeffnenToolStripMenuItem.Name = "DateiImEditorOeffnenToolStripMenuItem"
    Me.DateiImEditorOeffnenToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
    Me.DateiImEditorOeffnenToolStripMenuItem.Text = "Datei im Editor öffnen"
    '
    'WindowsExplorerToolStripMenuItem
    '
    Me.WindowsExplorerToolStripMenuItem.Name = "WindowsExplorerToolStripMenuItem"
    Me.WindowsExplorerToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
    Me.WindowsExplorerToolStripMenuItem.Text = "Windows-Explorer"
    '
    'DateimanagerNavigierenToolStripMenuItem
    '
    Me.DateimanagerNavigierenToolStripMenuItem.Name = "DateimanagerNavigierenToolStripMenuItem"
    Me.DateimanagerNavigierenToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
    Me.DateimanagerNavigierenToolStripMenuItem.Text = "Dateimanager navigieren"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(203, 6)
    '
    'FTPDateiOeffnenToolStripMenuItem
    '
    Me.FTPDateiOeffnenToolStripMenuItem.Name = "FTPDateiOeffnenToolStripMenuItem"
    Me.FTPDateiOeffnenToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
    Me.FTPDateiOeffnenToolStripMenuItem.Text = "FTP - Datei öffnen"
    '
    'FTPOrdnerNavigierenToolStripMenuItem
    '
    Me.FTPOrdnerNavigierenToolStripMenuItem.Name = "FTPOrdnerNavigierenToolStripMenuItem"
    Me.FTPOrdnerNavigierenToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
    Me.FTPOrdnerNavigierenToolStripMenuItem.Text = "FTP - Ordner navigieren"
    '
    'ToolStripSeparator2
    '
    Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
    Me.ToolStripSeparator2.Size = New System.Drawing.Size(203, 6)
    '
    'TrefferlisteLoeschenToolStripMenuItem
    '
    Me.TrefferlisteLoeschenToolStripMenuItem.Name = "TrefferlisteLoeschenToolStripMenuItem"
    Me.TrefferlisteLoeschenToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
    Me.TrefferlisteLoeschenToolStripMenuItem.Text = "Trefferliste löschen"
    '
    'checkRekursiv
    '
    Me.checkRekursiv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.checkRekursiv.AutoSize = True
    Me.checkRekursiv.Checked = True
    Me.checkRekursiv.CheckState = System.Windows.Forms.CheckState.Checked
    Me.checkRekursiv.Location = New System.Drawing.Point(267, 9)
    Me.checkRekursiv.Name = "checkRekursiv"
    Me.checkRekursiv.Size = New System.Drawing.Size(63, 17)
    Me.checkRekursiv.TabIndex = 16
    Me.checkRekursiv.Text = "rekursiv"
    Me.checkRekursiv.UseVisualStyleBackColor = True
    '
    'Scintilla1
    '
    Me.Scintilla1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Scintilla1.Location = New System.Drawing.Point(0, 0)
    Me.Scintilla1.Name = "Scintilla1"
    Me.Scintilla1.Size = New System.Drawing.Size(951, 1)
    Me.Scintilla1.Styles.BraceBad.FontName = "Verdana"
    Me.Scintilla1.Styles.BraceLight.FontName = "Verdana"
    Me.Scintilla1.Styles.ControlChar.FontName = "Verdana"
    Me.Scintilla1.Styles.Default.FontName = "Verdana"
    Me.Scintilla1.Styles.IndentGuide.FontName = "Verdana"
    Me.Scintilla1.Styles.LastPredefined.FontName = "Verdana"
    Me.Scintilla1.Styles.LineNumber.FontName = "Verdana"
    Me.Scintilla1.Styles.Max.FontName = "Verdana"
    Me.Scintilla1.TabIndex = 18
    '
    'SplitContainer1
    '
    Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
    Me.SplitContainer1.Location = New System.Drawing.Point(217, 0)
    Me.SplitContainer1.Name = "SplitContainer1"
    Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplitContainer1.Panel1
    '
    Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.MediumSlateBlue
    Me.SplitContainer1.Panel1.Controls.Add(Me.chkDockWin)
    Me.SplitContainer1.Panel1.Controls.Add(Me.chkTopmost)
    Me.SplitContainer1.Panel1.Controls.Add(Me.chkActFile)
    Me.SplitContainer1.Panel1.Controls.Add(Me.btnZoomOnOff)
    Me.SplitContainer1.Panel1.Controls.Add(Me.Scintilla1)
    Me.SplitContainer1.Panel1.Controls.Add(Me.Label10)
    Me.SplitContainer1.Panel1.Controls.Add(Me.txtGlobsearchSuch)
    Me.SplitContainer1.Panel1.Controls.Add(Me.checkRekursiv)
    Me.SplitContainer1.Panel1.Controls.Add(Me.btnDoGlobSearch)
    Me.SplitContainer1.Panel1.Controls.Add(Me.cmbGlobsearchFilefilter)
    Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
    Me.SplitContainer1.Panel1.Controls.Add(Me.txtGlobsearchUndnicht)
    '
    'SplitContainer1.Panel2
    '
    Me.SplitContainer1.Panel2.Controls.Add(Me.igGlobsearch)
    Me.SplitContainer1.Size = New System.Drawing.Size(951, 343)
    Me.SplitContainer1.SplitterDistance = 32
    Me.SplitContainer1.TabIndex = 19
    '
    'chkDockWin
    '
    Me.chkDockWin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkDockWin.AutoSize = True
    Me.chkDockWin.Location = New System.Drawing.Point(654, 9)
    Me.chkDockWin.Name = "chkDockWin"
    Me.chkDockWin.Size = New System.Drawing.Size(50, 17)
    Me.chkDockWin.TabIndex = 31
    Me.chkDockWin.Text = "dock"
    Me.chkDockWin.UseVisualStyleBackColor = True
    '
    'chkTopmost
    '
    Me.chkTopmost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkTopmost.AutoSize = True
    Me.chkTopmost.Location = New System.Drawing.Point(611, 9)
    Me.chkTopmost.Name = "chkTopmost"
    Me.chkTopmost.Size = New System.Drawing.Size(41, 17)
    Me.chkTopmost.TabIndex = 32
    Me.chkTopmost.Text = "top"
    Me.chkTopmost.UseVisualStyleBackColor = True
    '
    'chkActFile
    '
    Me.chkActFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkActFile.AutoSize = True
    Me.chkActFile.Location = New System.Drawing.Point(332, 9)
    Me.chkActFile.Name = "chkActFile"
    Me.chkActFile.Size = New System.Drawing.Size(90, 17)
    Me.chkActFile.TabIndex = 20
    Me.chkActFile.Text = "nur akt. Datei"
    Me.chkActFile.UseVisualStyleBackColor = True
    '
    'btnZoomOnOff
    '
    Me.btnZoomOnOff.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnZoomOnOff.BackColor = System.Drawing.SystemColors.Control
    Me.btnZoomOnOff.Font = New System.Drawing.Font("Wingdings 3", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
    Me.btnZoomOnOff.Location = New System.Drawing.Point(3, 6)
    Me.btnZoomOnOff.Name = "btnZoomOnOff"
    Me.btnZoomOnOff.Size = New System.Drawing.Size(41, 23)
    Me.btnZoomOnOff.TabIndex = 19
    Me.btnZoomOnOff.Text = "rq"
    Me.btnZoomOnOff.UseVisualStyleBackColor = True
    '
    'LinkLabel1
    '
    Me.LinkLabel1.BackColor = System.Drawing.Color.DimGray
    Me.LinkLabel1.LinkColor = System.Drawing.Color.White
    Me.LinkLabel1.Location = New System.Drawing.Point(8, 3)
    Me.LinkLabel1.Name = "LinkLabel1"
    Me.LinkLabel1.Size = New System.Drawing.Size(85, 17)
    Me.LinkLabel1.TabIndex = 20
    Me.LinkLabel1.TabStop = True
    Me.LinkLabel1.Text = "FTP - Suche"
    Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'LinkLabel2
    '
    Me.LinkLabel2.BackColor = System.Drawing.Color.DimGray
    Me.LinkLabel2.LinkColor = System.Drawing.Color.White
    Me.LinkLabel2.Location = New System.Drawing.Point(101, 3)
    Me.LinkLabel2.Name = "LinkLabel2"
    Me.LinkLabel2.Size = New System.Drawing.Size(85, 17)
    Me.LinkLabel2.TabIndex = 21
    Me.LinkLabel2.TabStop = True
    Me.LinkLabel2.Text = "Dateien cachen"
    Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'ListBox1
    '
    Me.ListBox1.FormattingEnabled = True
    Me.ListBox1.IntegralHeight = False
    Me.ListBox1.Location = New System.Drawing.Point(4, 22)
    Me.ListBox1.Name = "ListBox1"
    Me.ListBox1.Size = New System.Drawing.Size(157, 84)
    Me.ListBox1.TabIndex = 23
    '
    'ftvGlobSearch
    '
    Me.ftvGlobSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ftvGlobSearch.Location = New System.Drawing.Point(4, 22)
    Me.ftvGlobSearch.Name = "ftvGlobSearch"
    Me.ftvGlobSearch.OcxState = CType(resources.GetObject("ftvGlobSearch.OcxState"), System.Windows.Forms.AxHost.State)
    Me.ftvGlobSearch.Size = New System.Drawing.Size(207, 320)
    Me.ftvGlobSearch.TabIndex = 8
    '
    'frmTB_globSearch
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1174, 346)
    Me.Controls.Add(Me.LinkLabel2)
    Me.Controls.Add(Me.LinkLabel1)
    Me.Controls.Add(Me.ftvGlobSearch)
    Me.Controls.Add(Me.SplitContainer1)
    Me.Controls.Add(Me.ListBox1)
    Me.DockAreas = CType(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) _
                Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom), WeifenLuo.WinFormsUI.Docking.DockAreas)
    Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.HideOnClose = True
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmTB_globSearch"
    Me.Text = "Suche"
    CType(Me.igGlobsearch, System.ComponentModel.ISupportInitialize).EndInit()
    Me.cmenuGlobTreffer.ResumeLayout(False)
    CType(Me.Scintilla1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.SplitContainer1.Panel1.ResumeLayout(False)
    Me.SplitContainer1.Panel1.PerformLayout()
    Me.SplitContainer1.Panel2.ResumeLayout(False)
    Me.SplitContainer1.ResumeLayout(False)
    CType(Me.ftvGlobSearch, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents txtGlobsearchUndnicht As System.Windows.Forms.TextBox
  Friend WithEvents cmbGlobsearchFilefilter As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents btnDoGlobSearch As System.Windows.Forms.Button
  Friend WithEvents txtGlobsearchSuch As System.Windows.Forms.TextBox
  Friend WithEvents igGlobsearch As TenTec.Windows.iGridLib.iGrid
  Friend WithEvents ftvGlobSearch As AxCCRPFolderTV6.AxFolderTreeview
  Friend WithEvents cmenuGlobTreffer As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents DateiImEditorOeffnenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents WindowsExplorerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents DateimanagerNavigierenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents FTPDateiOeffnenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents FTPOrdnerNavigierenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents TrefferlisteLoeschenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents checkRekursiv As System.Windows.Forms.CheckBox
  Friend WithEvents Scintilla1 As ScintillaNet.Scintilla
  Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents btnZoomOnOff As System.Windows.Forms.Button
  Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
  Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
  Friend WithEvents chkActFile As System.Windows.Forms.CheckBox
  Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
  Friend WithEvents chkDockWin As System.Windows.Forms.CheckBox
  Friend WithEvents chkTopmost As System.Windows.Forms.CheckBox
End Class
