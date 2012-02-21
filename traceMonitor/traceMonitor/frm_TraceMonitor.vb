Imports System.Drawing.Drawing2D



Public Class frm_TraceMonitor

  Public zoomEnabled As Boolean

  Public traceFilter As New Dictionary(Of String, Boolean)

  Dim m_formBorder As MVPS.clsFormBorder

  Public actTraceFilter As String

  Dim tsp_quickLaunch_dontEat, _
       tsp_menu2_dontEat, _
       tsp_StartButtons_dontEat _
       As ToolStrip_DontEatClickEvent

  Const WM_NCLBUTTONDOWN = &HA1


  Private curApp, curModItem, curFunc As String

  'Private WithEvents wmData As New vbAccelerator.Components.Win32.CopyData


  Private Sub frm_explorer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    On Error Resume Next
    glob.saveFormPos(Me)
    glob.saveTuttiFrutti(Me)
    glob.para("frm_TraceMonitor__IGrid1__Layout") = IGrid1.LayoutObject.Text
    glob.saveParaFile()

    killServer()

    'Process.GetCurrentProcess.Kill()
  End Sub

  Sub createPrintLines()
    On Error Resume Next
    IGrid2.Rows.Count = 50
    For i As Integer = 0 To 49
      IGrid2.Cells(i, 0).Value = i + 1
    Next
    'tblLayout_printLine.Height = 21 * 50
    'tblLayout_printLine.RowCount = 50
    'tblLayout_printLine.RowStyles.Item(0).Height = 21

    'Dim lab As Label, txt As TextBox
    'For i As Integer = 0 To 49
    '  lab = New Label()
    '  txt = New TextBox()

    '  lab.AutoSize = False
    '  lab.TextAlign = ContentAlignment.MiddleRight
    '  lab.Dock = DockStyle.Fill
    '  lab.Text = "printLine " + CStr(i + 1) + ""
    '  lab.ForeColor = Color.White
    '  lab.Name = "labPrintLine_" + i.ToString("00")

    '  txt.Dock = DockStyle.Fill
    '  txt.Name = "txtPrintLine_" + i.ToString("00")

    '  tblLayout_printLine.Controls.Add(lab, 0, i)
    '  tblLayout_printLine.Controls.Add(txt, 1, i)

    '  tblLayout_printLine.RowStyles.Add(New RowStyle(SizeType.Absolute, 21))


    'Next

  End Sub

  Private Sub frm_explorer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    On Error Resume Next
    MAIN = Me

    'lab_Eile_mit_Weile.BringToFront()
    Me.Show()
    Application.DoEvents()

    glob.readFormPos(Me)


    m_formBorder = New MVPS.clsFormBorder()
    m_formBorder.client = Me
    m_formBorder.Titlebar = False


    initInterprocWindow()

    glob.readTuttiFrutti(Me)
    IGrid1.LayoutObject.Text = glob.para("frm_TraceMonitor__IGrid1__Layout")

    mwRegisterSelf()
    IO.Directory.CreateDirectory("c:\yPara\globDebugTrace\")

    ' traceFileRead()
    createPrintLines()

    IDEconnect()

    startWebUpdate("tracemonitor", True)

    loadFilterBookmarks()
    ToolStrip1.ImageList = iml_TraceTypes
    Dim btn As ToolStripButton
    For i = 0 To iml_TraceTypes.Images.Count - 1
      btn = ToolStrip1.Items.Add(iml_TraceTypes.Images.Keys(i))
      btn.ImageIndex = i : btn.Margin = New Padding(0) : btn.Checked = True : btn.DisplayStyle = ToolStripItemDisplayStyle.Image
    Next
    refreshFilter()

    'initialize growl and networking:
    initFromSettings()

    setZoomEnabled(glob.para("zoomEnabled", "TRUE") = "TRUE")

    'lab_Eile_mit_Weile.Hide()
  End Sub


  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    IDEconnect()
  End Sub

  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    IDEdisconnect()
  End Sub

  'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
  '  SimpleInterprocessCommunications.frmInterProcessComms.Show()

  'End Sub

  'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
  '  Dim mes As String : mes = "ich bin eine neue Nachricht: und zwar soll dies einen etwas längeren Text ergeben, um die Funktion wordwrap live zu testen"
  '  mes = mes + vbCrLf + "!!! auserdem können Nachrichten einen Zeilenumbruch enthalten: entweder als normale vbcrlf oder codiert als '| L F |' "
  '  mes = mes + vbCrLf + "!!! dazu ebenfalls ein Beispiel |LF| ...neue Zeile ENDE NACHRICHT"
  '  trace("ERR: ...nur Test", mes, "err")

  'End Sub

  'Private Sub Button2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button2.KeyDown
  '  Dim mes As String : mes = "blaaaaaaa"
  '  trace("INFO: abcdefg", mes)
  'End Sub

  'Private Sub Button2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button2.MouseDown
  '  Dim mes As String : mes = "blaaaaaaa"
  '  trace("INFO: abcdefg", "xxxxxxxxxxxxx")
  'End Sub


  Dim timedAutoSelect_lastRow As Integer = -1, timedZoomRefresh_lastRow As Integer = -1
  Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    'onTimerEvent()
    'traceFileRead()
    If chk_Autoselect.Checked Then
      If lastLineCount > 0 AndAlso timedAutoSelect_lastRow <> lastLineCount Then
        timedAutoSelect_lastRow = lastLineCount
        IGrid1.Rows(lastLineCount - 1).Selected = True
      End If
    End If
    If IGrid1.CurRow IsNot Nothing AndAlso timedZoomRefresh_lastRow <> IGrid1.CurRow.Index Then
      timedZoomRefresh_lastRow = IGrid1.CurRow.Index
      updateTraceZoom()
    End If

    tsslCountItems.Text = "Anzahl: " & lastLineCount
  End Sub

  Private Sub IGrid1_CellDoubleClick(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellDoubleClickEventArgs) Handles IGrid1.CellDoubleClick
    Dim ir = IGrid1.Rows(e.RowIndex)

    frm_hugeZoom.TextBox1.Text = "       Type: " + ir.Cells(0).Value + vbNewLine + _
                                 "     Para01: " + ir.Cells(1).Value + vbNewLine + _
                                 "   CodeLink: " + ir.Tag + vbNewLine + _
                                 vbNewLine + Replace(ir.Cells(2).Value, "|LF|", vbNewLine)


    frm_hugeZoom.Show()
    frm_hugeZoom.Activate()

    frm_hugeZoom.TextBox1.SelectionLength = 0
    frm_hugeZoom.TextBox1.SelectionStart = 0
  End Sub


  'Private Sub ListBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
  '  ''trace("ListBox1_MouseUp", e.Button.ToString)
  '  If e.Button = MouseButtons.Right Then
  '    Dim idx As Integer = ListBox1.IndexFromPoint(e.Location)
  '    If idx > -1 Then
  '      ListBox1.SelectedIndex = idx

  '      'trace("ListBox1_MouseUp", "TREFFER (rClick)")
  '      navigateCodeLink(curApp, curModItem, curFunc)
  '      navigateCodeLink(curApp, curModItem, curFunc)

  '    End If

  '  End If
  'End Sub

  Private Sub IGrid1_CellMouseUp(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellMouseUpEventArgs) Handles IGrid1.CellMouseUp
    If e.Button = Windows.Forms.MouseButtons.Right Then
      IGrid1.SetCurRow(e.RowIndex)
      navigateCodeLink(curApp, curModItem, curFunc)

    End If
  End Sub

  Sub updateTraceZoom()
    On Error Resume Next
    If IGrid1.CurRow Is Nothing Then
      Me.txt_zoom.Text = ""
      Me.lab_title.Text = ""
      Me.txt_func.Text = ""
      Me.txt_mod.Text = ""
      Me.lab_appName.Text = ""
      Exit Sub
    End If

    Dim ir = IGrid1.CurRow

    Dim para2 As String
    para2 = ir.Cells(2).Value
    para2 = para2.Replace("|LF|", vbCrLf)
    para2 = para2.Replace(vbTab, vbCrLf)

    Me.txt_zoom.Text = para2.Trim
    Me.lab_title.Text = ir.Cells(1).Value

    Dim codeLink As String = ir.Tag
    Dim codelink2() As String
    codelink2 = Split(codeLink, "_|°|_")
    Dim app As String = codelink2(2)
    Dim modItem As String = codelink2(3)
    Dim func As String = codelink2(4)
    Me.txt_func.Text = func
    Me.txt_mod.Text = modItem
    Me.lab_appName.Text = app
    curApp = app
    curModItem = modItem
    curFunc = func
  End Sub


  Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    traceTester() '' ... hier wird der trace aufgerufen !!!
  End Sub


  Private Sub chk_topmost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_topmost.CheckedChanged
    Me.TopMost = chk_topmost.Checked
    chk_topmost.Image = If(chk_topmost.Checked, My.Resources.top_en, My.Resources.top_dis)
  End Sub

  Private Sub pnlTitlebar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
   Handles pnlTitlebar.MouseDown, lab_moveBar.MouseDown
    If e.Button = Windows.Forms.MouseButtons.Left Then
      m_formBorder.MoveMe()
    Else
      Static oldHeight As Integer = -1
      If oldHeight = -1 Then
        oldHeight = Me.Height
        Me.Height = 40
        ' Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
      Else
        Me.Height = oldHeight : oldHeight = -1
        ' m_formBorder.Sizeable = True
        ' Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
      End If
    End If
  End Sub


  'Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
  '  printLine(2, "test111", "test222 333 444 555 666 777 888 999")

  'End Sub


  Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
    Me.Close()
  End Sub


  Private Sub IProcViewerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Process.Start(ExePath("iprocviewer"))

  End Sub

  Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Process.Start(ExePath("iprocviewer"))

  End Sub


  Private Sub lnkEditBookmarks_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lnkEditBookmarks.MouseUp
    Process.Start(settingsFolder + "filterbookmarks.txt")

  End Sub

  Sub loadFilterBookmarks()
    Const fileSpec = settingsFolder + "filterbookmarks.txt"
    FlowLayoutPanel1.Controls.Clear()
    If IO.File.Exists(fileSpec) = False Then IO.File.WriteAllText(fileSpec, "")
    Dim lines() = IO.File.ReadAllLines(fileSpec)
    For Each n In lines
      Dim l As New LinkLabel
      l.Text = n
      l.Tag = n
      l.LinkColor = Color.FromArgb(255, 133, 133, 133)
      AddHandler l.MouseUp, AddressOf lnkFilterBookmarkMouseUp
      FlowLayoutPanel1.Controls.Add(l)
    Next
  End Sub
  Private Sub lnkReloadBookmarks_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lnkReloadBookmarks.MouseUp
    loadFilterBookmarks()
  End Sub

  Private Sub lnkFilterBookmarkMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    If e.Button = Windows.Forms.MouseButtons.Left Then
      Dim filterApp As String = LCase(sender.Tag)
      actTraceFilter = filterApp
      For i = 0 To IGrid1.Rows.Count - 1
        Dim app As String = If(IGrid1.Cells(i, 3).Value, "")
        IGrid1.Rows(i).Visible = app.ToLower = filterApp
      Next
    Else
      actTraceFilter = ""
      For i = 0 To IGrid1.Rows.Count - 1
        IGrid1.Rows(i).Visible = True
      Next
    End If
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
    Dim vis As Boolean
    For i = 0 To lastLineCount - 1
      If traceFilter.TryGetValue(IGrid1.Cells(i, 0).Value, vis) Then
        IGrid1.Rows(i).Visible = vis
      End If
    Next
    If IGrid1.CurRow IsNot Nothing Then IGrid1.CurRow.EnsureVisible()
  End Sub

  Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
    traceFileClear()
  End Sub

  Private Sub StatusStrip1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
  Handles StatusStrip1.MouseUp, tsslCountItems.MouseUp, ToolStripStatusLabel2.MouseUp
    If e.Button = Windows.Forms.MouseButtons.Right Then
      SplitContainer3.Panel2Collapsed = Not SplitContainer3.Panel2Collapsed
    End If
  End Sub

  Private Sub chk_Autoselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_Autoselect.Click
    On Error Resume Next
    If chk_Autoselect.Checked Then
      IGrid1.CurRow = IGrid1.Rows(IGrid1.Rows.Count - 1)
    End If
    chk_Autoselect.Image = If(chk_Autoselect.Checked, My.Resources.autosel_en, My.Resources.autosel_dis)
    ' Timer1.Enabled = chk_Autoselect.Checked
  End Sub


  Dim splitter1MoverStartCursor, splitter1MoverStartDist As Integer
  Private Sub Label2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label2.MouseDown
    splitter1MoverStartCursor = Cursor.Position.Y
    splitter1MoverStartDist = SplitContainer1.SplitterDistance
    'Debug.Print("MouseDown!!  Y = " & splitter1MoverStartCursor & " Dist = " & splitter1MoverStartDist)
  End Sub

  Private Sub Label2_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label2.MouseMove
    If e.Button = Windows.Forms.MouseButtons.Left Then
      Dim newVal = splitter1MoverStartDist - (splitter1MoverStartCursor - Cursor.Position.Y)
      SplitContainer1.SplitterDistance = If(newVal < 0, 0, newVal)
      'Debug.Print("start    Y = " & splitter1MoverStartCursor & " Dist = " & splitter1MoverStartDist)
      'Debug.Print("New      Y = " & Cursor.Position.Y & " Dist = " & SplitContainer1.SplitterDistance)
    End If
  End Sub

  Private Sub checkAutoscroll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkAutoscroll.CheckedChanged
    checkAutoscroll.Image = If(checkAutoscroll.Checked, My.Resources.autoscroll_en, My.Resources.autoscroll_dis)
  End Sub

  Private Sub btnToggleTrace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggleTrace.Click
    setZoomEnabled(btnToggleTrace.Text = "r")
  End Sub

  Sub setZoomEnabled(ByVal s As Boolean)
    If s Then
      btnToggleTrace.Text = "q"
      zoomEnabled = True
      glob.para("zoomEnabled") = "TRUE"
      SplitContainer3.Panel2Collapsed = False

    Else
      btnToggleTrace.Text = "r"
      zoomEnabled = False
      glob.para("zoomEnabled") = "FALSE"
      SplitContainer3.Panel2Collapsed = True

    End If
  End Sub

  Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettings.Click
    frm_settings.ShowDialog()
  End Sub
End Class