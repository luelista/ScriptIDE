Public Class frmTB_debug

  Public traceFilter As New Dictionary(Of String, Boolean)

  'Dim plCont(100) As Label
  'Dim plCapt(100) As Label
  Public autoScrollFlag As Boolean

  Public Overrides Function GetPersistString() As String
    Return "Addin|##|siaCodeCompiler|##|SHDebug"
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    IdeHelper.BeforeShowAddinWindow(Me.GetPersistString(), Me)
    MyBase.Show()
  End Sub

  Sub setOUT(ByVal source As String, ByVal outText As String)
    txtOutSource.Text = source
    qq_txtOutMonitor.Text = outText
  End Sub

  Private Sub IGrid1_CellMouseDown(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellMouseDownEventArgs) _
  Handles IGrid2.CellMouseDown
    If e.Button = Windows.Forms.MouseButtons.Right Then
      sender.SetCurRow(e.RowIndex)
      ContextMenuStrip1.Tag = sender
    End If
  End Sub

  Sub navigateCodeLink(ByVal codeLink As String)
    Dim link() = Split(codeLink, "_|°|_")
    If link(0) <> "cLink" Then TT.Write("Invalid cLink-Format", codeLink, "warn") : Exit Sub
    If link(1) = "scriptClass" Then
      'scriptIDE 


    Else
      'Visual Studio


    End If
  End Sub

  Private Sub IGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

  End Sub

  Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
    On Error Resume Next
    KopierenToolStripMenuItem.Enabled = ContextMenuStrip1.Tag <> "pl" AndAlso ContextMenuStrip1.Tag.CurRow IsNot Nothing
  End Sub

  Private Sub KopierenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KopierenToolStripMenuItem.Click
    Dim ig As iGrid = ContextMenuStrip1.Tag
    Clipboard.Clear()
    Clipboard.SetText(ZZ.JoinIGridLine(ig.CurRow, vbTab))
  End Sub

  Private Sub resetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles resetToolStripMenuItem.Click
    'trace
    IGrid2.Rows.Clear()
  End Sub
  Private Sub frmTB_debug_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    'glob.saveTuttiFrutti(Me)
  End Sub

  'Sub resetPrintLines()
  '  For i = 1 To plCont.Length - 1
  '    If plCapt(i) IsNot Nothing Then
  '      plCapt(i).Text = i.ToString
  '      plCont(i).Text = "<--"
  '    ElseIf plCont(i) IsNot Nothing Then
  '      plCont(i).Text = i.ToString
  '    End If
  '  Next
  'End Sub

  Private Sub frmTB_debug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'glob.readTuttiFrutti(Me)
    
    'printLines
    'createPrintlineArea()
    'plCont(11) = labPrintLine11
    'resetPrintLines()

    TabControl1.Left = -25 : TabControl1.Width += 25

    If IdeHelper IsNot Nothing Then AddHandler IdeHelper.DocumentTabActivated, AddressOf ideHelper_DocumentTabActivated
    AddHandler ScriptHost.Instance.BreakModeChanged, AddressOf scriptHost_BreakModeChanged

    SplitContainer2.Orientation = ParaService.Glob.para("frmTB_debug__SplitOrientation", "0")
    'IGrid1.Rows.Count = 50
    'For i = 1 To 50
    '  IGrid1.Cells(i - 1, 0).Value = i
    'Next

    ToolStrip1.ImageList = iml_TraceTypes
    Dim btn As ToolStripButton
    For i = 0 To iml_TraceTypes.Images.Count - 1
      btn = ToolStrip1.Items.Add(iml_TraceTypes.Images.Keys(i))
      btn.ImageIndex = i : btn.Margin = New Padding(0) : btn.Checked = True : btn.DisplayStyle = ToolStripItemDisplayStyle.Image
    Next
    refreshFilter()
  End Sub

  Sub scriptHost_BreakModeChanged(ByVal className As String, ByRef breakState As String)
    If className = LCase(getActiveTabClass()) Then
      setCurrentBreakState(className, breakState = "BREAK")
    End If
  End Sub
  Sub ideHelper_DocumentTabActivated(ByVal Tab As Object, ByVal Key As String)
    Dim className = IO.Path.GetFileNameWithoutExtension(Key)
    Dim breakState = ScriptHost.Instance.globBreakMode(className)
    setCurrentBreakState(className, breakState = "BREAK")
  End Sub

  Function getActiveTabClass() As String
    Dim url = IdeHelper.getActiveTabFilespec()
    Return IO.Path.GetFileNameWithoutExtension(url)
  End Function

  Sub setCurrentBreakState(ByVal className As String, ByVal isBreak As Boolean)
    Try
      'MAIN.labScriptBreak2.Visible = isBreak
      'MAIN.lblStatus.Visible = isBreak
      MAIN.lblRunning.Text = className + ": " + If(isBreak, "[F9] Step    [F11] Go    [Strg+F12] Exit", "[F5] Run   [Strg+S] Save")
      MAIN.lblRunning.BackColor = If(isBreak, Color.Gold, Color.FromKnownColor(KnownColor.Control))
      If isBreak = False Then MAIN.lblStatus.Text = ""
      'frm_scriptDebugging.Visible = value = "BREAK"
    Catch : End Try
  End Sub


  'Sub setPrintLine(ByVal i As Integer, ByVal content As String, Optional ByVal label As String = "")
  '  If i < plCont.Length AndAlso plCont(i) IsNot Nothing Then
  '    plCont(i).Text = content + "<--"
  '  End If
  '  If label <> "" AndAlso i < plCapt.Length AndAlso plCapt(i) IsNot Nothing Then
  '    plCapt(i).Text = label
  '  End If
  'End Sub

  'Sub createPrintlineArea()
  '  FlowLayoutPanel1.Controls.Clear()
  '  TableLayoutPanel2.Controls.Clear()

  '  Dim tmp As Label
  '  For i = 1 To 9
  '    tmp = makeLabel(DockStyle.Fill)
  '    tmp.BackColor = Color.FromArgb(255, 64, 64, 64)
  '    tmp.ForeColor = Color.Gainsboro
  '    tmp.Text = Chr(&H80 + i)
  '    tmp.Font = New Font("Wingdings", 12, FontStyle.Regular, GraphicsUnit.Point)
  '    TableLayoutPanel2.Controls.Add(tmp)

  '    plCapt(i) = makeLabel(DockStyle.Fill) : plCapt(i).Tag = i
  '    plCapt(i).BackColor = Color.FromArgb(255, 64, 64, 64)
  '    plCapt(i).ForeColor = Color.WhiteSmoke
  '    TableLayoutPanel2.Controls.Add(plCapt(i))
  '    AddHandler plCapt(i).Click, AddressOf printLineLabelClicked

  '    plCont(i) = makeLabel(DockStyle.Fill) : plCont(i).Tag = i
  '    plCont(i).BackColor = Color.FromArgb(255, 224, 224, 224)
  '    plCont(i).ForeColor = Color.Black
  '    TableLayoutPanel2.Controls.Add(plCont(i))
  '    AddHandler plCont(i).Click, AddressOf printLineLabelClicked
  '  Next
  '  For i = 12 To 20
  '    plCont(i) = makeLabel(DockStyle.None)
  '    plCont(i).BackColor = Color.LightGray
  '    plCont(i).ForeColor = Color.Black
  '    plCont(i).Text = "line" & i : plCont(i).Width = 63
  '    plCont(i).Margin = New Padding(0, 0, 2, 0)
  '    FlowLayoutPanel1.Controls.Add(plCont(i))
  '  Next
  '  For i = 21 To 99
  '    tmp = makeLabel(DockStyle.Fill)
  '    tmp.BackColor = Color.LightBlue
  '    TableLayoutPanel2.Controls.Add(tmp)

  '    plCapt(i) = makeLabel(DockStyle.Fill)
  '    plCapt(i).BackColor = Color.LightBlue
  '    TableLayoutPanel2.Controls.Add(plCapt(i)) : plCapt(i).Tag = i
  '    AddHandler plCapt(i).Click, AddressOf printLineLabelClicked

  '    plCont(i) = makeLabel(DockStyle.Fill)
  '    plCont(i).BackColor = Color.FromArgb(255, 240, 240, 240)
  '    TableLayoutPanel2.Controls.Add(plCont(i)) : plCont(i).Tag = i
  '    AddHandler plCont(i).Click, AddressOf printLineLabelClicked
  '  Next

  'End Sub

  'Sub printLineLabelClicked(ByVal sender As Object, ByVal e As System.EventArgs)
  '  On Error Resume Next
  '  TextBox2.Text = sender.tag & ": """ & plCapt(sender.tag).Text & """" & vbNewLine & plCont(sender.tag).Text


  'End Sub

  'Function makeLabel(ByVal dockst As DockStyle) As Label
  '  makeLabel = New Label
  '  makeLabel.Dock = dockst
  '  makeLabel.AutoSize = False
  '  makeLabel.TextAlign = ContentAlignment.MiddleLeft
  '  makeLabel.Margin = New Padding(0, 0, 0, 0)
  'End Function

  Private Sub timer_fadeOutErrBox_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_fadeOutErrBox.Tick
    Dim delta As Integer = TextBox2.BackColor.G
    delta = 255 - delta
    delta -= 3
    If delta < 0 Then timer_fadeOutErrBox.Stop() : TextBox2.BackColor = Color.White : Exit Sub
    TextBox2.BackColor = Color.FromArgb(255, 255, 255 - delta, CInt((40 - delta) * 5.5))
    'TextBox2.BackColor = Color.FromArgb(255, 255 - delta, 255 - delta, 182 + delta)

  End Sub


  Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
    ListView2.Items.Clear()
    For Each kvp In scriptClassDict
      Dim lvi = ListView2.Items.Add(kvp.Key)
      lvi.SubItems.Add(kvp.Value.assemblyFilespec)
      lvi.SubItems.Add(kvp.Value.fileSpec)


    Next
    ListView1.Items.Clear()
    For Each kvp In scriptClassSearchCache
      ListView1.Items.Add(kvp.Key).SubItems.Add(kvp.Value)
    Next
  End Sub
  Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
    If ListView2.SelectedItems.Count <> 1 Then Exit Sub
    Dim classname As String = ListView2.SelectedItems(0).Text

    ScriptHost.Instance.getScriptClassHost(classname, True)
  End Sub

  Private Sub checkTraceStack_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkTraceStack.CheckedChanged
    dumpStackToTrace = checkTraceStack.Checked
  End Sub


  Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles qq_check_silentMode.CheckedChanged
    ScriptHost.Instance.SilentMode = qq_check_silentMode.Checked
  End Sub

  Private Sub btnStdInPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStdInPaste.Click
    If Clipboard.ContainsText() Then qq_txtInTextbox.Text = Clipboard.GetText
  End Sub

  Private Sub btnStdInCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStdInCopy.Click
    Clipboard.Clear()
    Clipboard.SetText(qq_txtInTextbox.Text)
  End Sub


  Private Sub btnStdOutPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStdOutPaste.Click
    If Clipboard.ContainsText() Then qq_txtOutMonitor.Text = Clipboard.GetText
  End Sub

  Private Sub btnStdOutCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStdOutCopy.Click
    Clipboard.Clear()
    Clipboard.SetText(qq_txtOutMonitor.Text)
  End Sub


  Private Sub IGrid2_CurRowChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles IGrid2.CurRowChanged
    If IGrid2.CurRow Is Nothing Then Exit Sub

    TextBox2.Text = IGrid2.CurRow.Cells(1).Value + vbNewLine + IGrid2.CurRow.Cells(2).Value

  End Sub



  Private Sub lnkTraceMonitor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkTraceMonitor.Click
    On Error Resume Next
    sys_interproc.EnsureAppRunning("tracemonitor")
    AppActivate("traceMonitor")
  End Sub

  Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

  End Sub

  Private Sub SplitContainer1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    If e.Button = Windows.Forms.MouseButtons.Right Then
      ContextMenuStrip1.Tag = "pl"

    End If
  End Sub

  Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

  End Sub

  Private Sub checkTraceAutoscroll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkTraceAutoscroll.CheckedChanged
    timer_autoScroll.Enabled = checkTraceAutoscroll.Checked
  End Sub

  Private Sub btnStdOutClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStdOutClear.Click
    qq_txtOutMonitor.Text = ""
  End Sub

  Private Sub timer_autoScroll_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_autoScroll.Tick
    If Not autoScrollFlag Then Exit Sub
    IGrid2.SetCurRow(IGrid2.Rows.Count - 1)
    autoScrollFlag = False
  End Sub

  Private Sub tsbTab_0_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
  Handles tsbTab_3.MouseUp, tsbTab_2.MouseUp, tsbTab_1.MouseUp, tsbTab_0.MouseUp
    TabControl1.SelectedIndex = sender.tag

  End Sub

  Private Sub tsbClearTrace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ZZ.traceClear()
    '  ZZ.printLineReset()

  End Sub

  Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
    Dim selIndex = TabControl1.SelectedIndex
    setElActivatedColor(tsbTab_0, selIndex = 0)
    setElActivatedColor(tsbTab_1, selIndex = 1)
    setElActivatedColor(tsbTab_2, selIndex = 2)
    setElActivatedColor(tsbTab_3, selIndex = 3)
  End Sub
  Sub setElActivatedColor(ByVal el As ToolStripStatusLabel, ByVal sel As Boolean)
    el.BackColor = If(sel, Color.RoyalBlue, Color.Transparent)
    el.LinkColor = If(sel, Color.White, Color.Blue)
  End Sub

  Private Sub frmTB_debug_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    'Static oldOrientation As Boolean
    'If oldOrientation = (Me.Width > Me.Height) Then Exit Sub
    'oldOrientation = (Me.Width > Me.Height)
    'If oldOrientation Then
    '  ' SplitContainer1.Orientation = Orientation.Vertical
    '  SplitContainer2.Orientation = Orientation.Vertical
    'Else
    '  ' SplitContainer1.Orientation = Orientation.Horizontal
    '  SplitContainer2.Orientation = Orientation.Horizontal
    'End If
  End Sub



  Private Sub SplitContainer2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SplitContainer2.DoubleClick
    If SplitContainer2.Orientation = Orientation.Horizontal Then
      SplitContainer2.Orientation = Orientation.Vertical
    Else
      SplitContainer2.Orientation = Orientation.Horizontal
    End If
    ParaService.Glob.para("frmTB_debug__SplitOrientation") = CInt(SplitContainer2.Orientation).ToString
  End Sub

  Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
    Dim btn As ToolStripButton = e.ClickedItem
    btn.Checked = Not btn.Checked
    refreshFilter()
  End Sub

  Sub refreshFilter()
    For Each btn As ToolStripButton In ToolStrip1.Items
      traceFilter(btn.Text) = btn.Checked
    Next
    For i = 0 To IGrid2.Rows.Count - 1
      IGrid2.Rows(i).Visible = traceFilter(IGrid2.Cells(i, 0).Value)
    Next
  End Sub

  Private Sub tsbClearTrace2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClearTrace2.Click
    IGrid2.Rows.Clear()
    TT.Write("--- TraceClear ---", "by user", "ini")
  End Sub
End Class