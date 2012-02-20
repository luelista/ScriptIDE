Imports System.Runtime.InteropServices

<FiletypeHandler(New String() {".grid"}, False)> _
Public Class frmDC_gridView
  Implements IDockContentForm

  Dim defaultColHeads() As String = {"aaa", "bbb", "ccc", "ddd", "eee", "fff", "ggg", "hhh", "iii", "jjj", "kkk", "lll", "mmm", "nnn", "ooo", "ppp", "qqq", "rrr", "sss", "ttt", "uuu", "vvv", "www", "xxx", "yyy", "zzz", "Col(27)", "Col(28)", "Col(29)", "Col(30)", "Col(31)", "Col(32)", "Col(33)", "Col(34)", "Col(35)", "Col(36)", "Col(37)", "Col(38)", "Col(39)", "Col(40)", "Col(41)", "Col(42)", "Col(43)", "Col(44)", "Col(45)"}

  Event CurrentIndexLineChanged(ByVal lineNr As Integer) Implements IDockContentForm.CurrentIndexLineChanged

  Private p_parameters As New Hashtable
  Public ReadOnly Property Parameters() As Hashtable Implements IDockContentForm.Parameters
    Get
      Return p_parameters
    End Get
  End Property

  Sub setDefaultColumnHeaders()
    ColumnHeaders = Join(defaultColHeads, vbTab)
  End Sub

  Public Overrides Function GetPersistString() As String
    Return "GridView|##|" + Me.getFileTag()
  End Function


  Private Sub frmDC_rtf_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

  End Sub

  Private Sub frmDC_rtf_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    If e.CloseReason = CloseReason.MdiFormClosing Then Exit Sub
    IDE.ContentHelper._internalCloseTab(Me, e.Cancel)
  End Sub

  Private Sub frmDC_rtf_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub


  Private p_tabRowKey As String
  'Public tabButton As Button
  Dim m_isDirty As Boolean
  Dim lastselLine As Integer = -1
  Private p_isLazy As Boolean
  Private p_indexListCtrl As ListBox
  Dim skipNavIndexList As Boolean = False
  Private m_url As String
  Public Property URL() As String Implements IDockContentForm.URL
    Get
      Return m_url
    End Get
    Set(ByVal value As String)
      m_url = value
      If String.IsNullOrEmpty(value) = False AndAlso value.StartsWith("generic:") Then
        labFilename.Text = IO.Path.GetFileNameWithoutExtension(value)
        labFilename.BackColor = Drawing.Color.Firebrick
      Else
        labFilename.Text = IO.Path.GetFileName(value)
        txtSaveFilename.Text = IO.Path.GetFileNameWithoutExtension(value)
      End If
    End Set
  End Property
#Region "ehemals öffentliche Variablen"


  Private m_hash As String
  Public Property Hash() As String Implements IDockContentForm.Hash
    Get
      Return m_hash
    End Get
    Set(ByVal value As String)
      m_hash = value
    End Set
  End Property

  ReadOnly Property FileType() As String Implements IDockContentForm.Filetype
    Get
      Return "rtf"
    End Get
  End Property
  ReadOnly Property indexListCtrl() As ListBox Implements IDockContentForm.indexListCtrl
    Get
      Return p_indexListCtrl
    End Get
  End Property
  Property tabRowKey() As String Implements IDockContentForm.tabRowKey
    Get
      Return p_tabRowKey
    End Get
    Set(ByVal value As String)
      p_tabRowKey = value
    End Set
  End Property
  Property isLazy() As Boolean Implements IDockContentForm.isLazy
    Get
      Return p_isLazy
    End Get
    Set(ByVal value As Boolean)
      p_isLazy = value
    End Set
  End Property
  Function getFileTag() As String Implements IDockContentForm.getFileTag
    Return URL
  End Function
  Sub setFileTag(ByVal sName As String) Implements IDockContentForm.setFileTag
    URL = sName
  End Sub


#End Region
  Function getViewFilename() As String Implements IDockContentForm.getViewFilename
    Return IO.Path.GetFileName(URL)
  End Function
  Function getIcon() As Drawing.Icon() Implements IDockContentForm.getIcon
    'Dim ext = IO.Path.GetExtension(Filename)
    Return FileTypeAndIcon.RegisteredFileType.GetFileIconByExt(".grid")
  End Function

  Property Dirty() As Boolean Implements IDockContentForm.Dirty
    Get
      Return m_isDirty
    End Get
    Private Set(ByVal value As Boolean)
      m_isDirty = value
      'If p_tabRowKey <> "" Then tbOpenedFiles.IGrid1.Cells(p_tabRowKey, 1).Value = If(value, "%", "")
      Me.Text = If(value, "◊", "") + getViewFilename()
    End Set
  End Property

  ReadOnly Property RTF() As Object Implements IDockContentForm.RTF
    Get
      Return IGrid1
    End Get
  End Property

  Sub navigateIndexlist(ByVal line As String) Implements IDockContentForm.navigateIndexlist
    'If skipNavIndexList Then Exit Sub
    'Dim findString = Split(line, "|##|")(1)
    'Dim pos = mRTF.Text.IndexOf(findString.Trim)
    'If pos = -1 Then Exit Sub

    ''Dim pos = mrtf.Find(line, RichTextBoxFinds.None)

    'mRTF.SelectionStart = pos
    'mRTF.ScrollToCaret()


  End Sub
  Sub jumpToLine(ByVal lineNr As Integer) Implements IDockContentForm.jumpToLine
    'Dim firstChar = mRTF.GetFirstCharIndexFromLine(lineNr)
    'mRTF.SelectionLength = 0 : mRTF.SelectionStart = lineNr

    'mRTF.ScrollToCaret()
    IGrid1.SetCurRow(lineNr)
  End Sub

  Property ColumnHeaders() As String
    Get
      Dim out(IGrid1.Cols.Count - 1) As String
      For i = 0 To IGrid1.Cols.Count - 1
        out(i) = IGrid1.Cols(i).Text
      Next
      Return Join(out, vbTab)
    End Get
    Set(ByVal value As String)
      Dim inData() = Split(value, vbTab)
      IGrid1.Cols.Count = inData.Length
      For i = 0 To IGrid1.Cols.Count - 1
        IGrid1.Cols(i).Text = inData(i)
      Next
    End Set
  End Property

  Sub onSave() Implements IDockContentForm.onSave
    If txtSaveFilename.Text = "" Then MsgBox("Bitte gib unter ""save"" einen Namen ein") : Exit Sub

    saveAs(IDE.GetSettingsFolder + "gridData\" + txtSaveFilename.Text + ".grid")
  End Sub

  Sub saveAs(ByVal URL As String)
    If Me.URL.StartsWith("generic:") Then
      Dim fn = InputBox("Dieses Grid enthält von einem Skript dynamisch erzeugte Daten und hat noch keinen Dateinamen." + vbNewLine + "Um es abzuspeichern, gib hier einen Dateinamen ein:")
      If fn = "" Then Exit Sub
      changeMyName(fn)
    End If

    Dim cont As String
    Igrid_get(IGrid1, cont)
    Dim outData(16) As String
    outData(0) = "____________ dataFormat: " + "siaGridView IGrid File Format 1.0"
    outData(3) = "____________ gridLayout: " + IGrid1.LayoutObject.Text
    outData(4) = "__________ lastModified: " + Now.ToString("yyyy-MM-dd HH:mm:ss")
    outData(5) = "_______________ headers: " + ColumnHeaders()
    outData(15) = cont

    'Dim ph = IDE.ProtocolManager.GetURLProtocolHandler(Me.URL)
    ProtocolService.SaveFileToURL(URL, Join(outData, vbNewLine))
    'IDE.ProtocolManager.SaveFile(Me.URL, mRTF.Rtf)
    'isBUSY = False
    Dirty = False
    'If IDE.getActiveTab() Is Me Then createIndexList()
  End Sub

  Sub onRead() Implements IDockContentForm.onRead
    'isBUSY = True
    If Me.URL.StartsWith("generic:") Then Exit Sub
    Try
      'Dim fileCont As String
      'If p_Filesource = "_loc" Then
      '  fileCont = IO.File.ReadAllText(p_Filename)
      'Else
      '  fileCont = TwAjax.ReadFile("memo", Me.p_Filename)
      'End If
      'Dim ph = IDE.GetURLProtocolHandler(Me.URL)
      'Dim fileCont = ph.ReadFile(URL)
      Dim fileCont = ProtocolService.ReadFileFromURL(Me.URL)
      Dim inData() = Split(fileCont, vbNewLine, 16)
      If inData.Length <> 16 Then Throw New IO.InvalidDataException("Ungültiges Dateiformat oder leere Datei")
      Try : IGrid1.LayoutObject.Text = inData(3).Substring(24).Trim
      Catch : End Try

      If inData(5).Length >= 25 Then
        ColumnHeaders = inData(5).Substring(24).Trim
      Else
        setDefaultColumnHeaders()
      End If

      Igrid_put(IGrid1, inData(15), , , True)
      NumericUpDown1.Value = IGrid1.Cols.Count

    Catch ex As Exception
      MsgBox("Fehler beim Einlesen ..." + vbNewLine + "evtl. neue Datei" + vbNewLine + vbNewLine + ex.ToString)
    End Try
end_of_function:
    'isBUSY = False
    Dirty = False
    p_isLazy = False
  End Sub

  Function getLineContent(ByVal lineNumber As Integer) As String Implements IDockContentForm.getLineContent
    Try
      Return JoinIGridLine(IGrid1.Rows(lineNumber), vbTab)
    Catch ex As Exception
      Return ""
    End Try
  End Function
  Public Function getLineCount() As Integer Implements IDockContentForm.getLineCount
    Return IGrid1.Rows.Count
  End Function

  Function getActLineContent() As String Implements IDockContentForm.getActLineContent
    If IGrid1.CurRow Is Nothing Then Return ""
    Return JoinIGridLine(IGrid1.CurRow, vbTab)
  End Function
  Sub selectCurLine()
  End Sub

  Function onCheckDirty(Optional ByVal beforeWhat As String = "vor dem Schließen ") As Boolean Implements IDockContentForm.onCheckDirty
    If Dirty Then
      Me.Activate()
      Select Case MsgBox("Im Dokument " + Me.URL + " befinden sich ungespeicherte Änderungen. Soll es vor dem " + beforeWhat + "gespeichert werden?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel, "scriptIDE - Datei speichern?")
        Case MsgBoxResult.Yes
          Me.onSave()
        Case MsgBoxResult.No
        Case MsgBoxResult.Cancel
          Return False
      End Select
    End If
    Return True
  End Function

  Function getCharCountFromStart(ByVal text As String, ByVal c As Char) As Integer
    Dim idx = 0
    Do While text.Substring(idx, 1) = c
      getCharCountFromStart += 1
    Loop
    Return idx
  End Function


  Sub createIndexList() Implements IDockContentForm.createIndexList
  End Sub


  Dim isInitialized As Boolean = False
  Public Sub onLazyInit() Implements IDockContentForm.onLazyInit
    If isInitialized Then Exit Sub
    isInitialized = True

    tsFileActions.Renderer = New Office2007Renderer.Office2007Renderer()
    IDE.ContentHelper.CreateFileactionToolbar(Me.URL, ".grid", tsFileActions)
    readScriptList()

  End Sub

  Private Sub SplitContainer1_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved

  End Sub

  Public Function Grid() As TenTec.Windows.iGridLib.iGrid
    Return IGrid1
  End Function


  Private Sub check_rowMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_rowMode.CheckedChanged
    On Error Resume Next
    Dim actRowIdx = IGrid1.CurRow.Index
    IGrid1.RowMode = check_rowMode.Checked
    If IGrid1.RowMode = False Then
      unselectAllRows()
      IGrid1.Cells(actRowIdx, 0).Selected = True
    Else
      IGrid1.SetCurRow(actRowIdx)
    End If
  End Sub

  Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
    changeMyName(txtReadFilename.Text)
    Me.onRead()
    'IDE.NavigateFile("loc:/C:/yPara/scriptIDE/gridData/" + txtReadFilename.Text + ".grid")
  End Sub

  Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    changeMyName(txtSaveFilename.Text)
    Me.onSave()
  End Sub

  Sub changeMyName(ByVal newName As String)
    Dim newKey = "loc:/C:/yPara/scriptIDE/gridData/" + txtSaveFilename.Text + ".grid"
    IDE.ContentHelper._internalRenameDocument(Me.Hash, newKey.ToLower())
    Me.Hash = newKey.ToLower() : Me.URL = newKey
    Me.Text = Me.getViewFilename()
  End Sub

  Private Sub IGrid1_AfterCommitEdit(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGAfterCommitEditEventArgs) Handles IGrid1.AfterCommitEdit
    Dirty = True
  End Sub

  Private Sub IGrid1_CellMouseUp(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellMouseUpEventArgs) Handles IGrid1.CellMouseUp
    If e.Button = Windows.Forms.MouseButtons.Right Then
      If IGrid1.RowMode = False Then
        unselectAllCells()
        For i = 0 To IGrid1.Rows.Count - 1
          IGrid1.Cells(i, e.ColIndex).Selected = True
        Next
        countDiffValues()
      End If
    End If
  End Sub


  Private Sub IGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IGrid1.Click

  End Sub

  Private Sub IGrid1_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles IGrid1.KeyDown
    'STRG-X-C-V   -- clipboard
    'merkeZeileAbruf
    'entf   -- markierte Zeilen löschen
    'einfg  -- neue Zeile einfügen
    ' ...

    If e.Control Then
      Select Case e.KeyCode
        Case Keys.X : copySelectedLines()
          deleteSelectedLines()
        Case Keys.C : copySelectedLines()
        Case Keys.V
          Dim lines() = Split(Clipboard.GetText, vbNewLine)
          insertLinesAtCurPos(lines)
        Case Keys.S, Keys.Enter
          Me.onSave()
          e.SuppressKeyPress = True
        Case Keys.R
          Dim doReload = True
          If Dirty Then
            If MsgBox("Im aktuellen Dokument befinden sich ungespeicherte Änderungen. Diese gehen verloren, wenn du das Dokument jetzt neu lädst.", MsgBoxStyle.OkCancel Or MsgBoxStyle.Exclamation) = MsgBoxResult.Cancel Then
              doReload = False
            End If
          End If
          If doReload Then Me.onRead()
      End Select
    End If

    If e.KeyCode = 226 Then ' kleiner Zeichen - <

      e.SuppressKeyPress = True
      If e.Shift Then
        Dim merkeData() As String = IDE.ContentHelper.merkeZeileAbruf()
        insertLinesAtCurPos(merkeData)
      Else
        If IGrid1.CurRow Is Nothing Then Exit Sub
        IDE.ContentHelper.merkeZeile(JoinIGridLine(IGrid1.CurRow))
        If e.Control Or e.Alt Then
        Else
          IGrid1.Rows.RemoveAt(IGrid1.CurRow.Index)
        End If
      End If
    End If

    If e.KeyCode = Keys.Insert Then
      Dim ir As TenTec.Windows.iGridLib.iGRow
      If IGrid1.CurRow Is Nothing Then ir = IGrid1.Rows.Add() Else ir = IGrid1.Rows.Insert(IGrid1.CurRow.Index)
      IGrid1.SetCurRow(ir.Index)
    End If
  End Sub

  Sub insertLinesAtCurPos(ByVal merkeData() As String)
    ' Dim merkeData() As String = IDE.ContentHelper.merkeZeileAbruf()
    Dim insStartPos As Integer = 0
    If IGrid1.CurRow Is Nothing Then
      IGrid1.Rows.Count += merkeData.Length - 1
    Else
      insStartPos = IGrid1.CurRow.Index
      IGrid1.Rows.InsertRange(IGrid1.CurRow.Index, merkeData.Length)
    End If
    unselectAllRows()
    For i = 0 To merkeData.Length - 1
      SplitToIGridLine(IGrid1.Rows(insStartPos + i), merkeData(i))
      IGrid1.Rows(insStartPos + i).Selected = True
    Next
  End Sub

  Sub copySelectedLines()
    Dim i As Integer, out(IGrid1.SelectedRows.Count - 1) As String
    For Each selLine In IGrid1.SelectedRows
      out(i) = JoinIGridLine(selLine)
      i += 1
    Next
    Clipboard.Clear()
    Clipboard.SetText(Join(out, vbNewLine))
  End Sub
  Sub deleteSelectedLines()
    While IGrid1.SelectedRows.Count > 0
      IGrid1.Rows.RemoveAt(IGrid1.SelectedRows(0).Index)
    End While
  End Sub

  Sub unselectAllRows()
    While IGrid1.SelectedRows.Count > 0
      IGrid1.SelectedRows(0).Selected = False
    End While
  End Sub
  Sub unselectAllCells()
    While IGrid1.SelectedCells.Count > 0
      IGrid1.SelectedCells(0).Selected = False
    End While
  End Sub

  Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
    IGrid1.Cols.Count = NumericUpDown1.Value
  End Sub

  Private Sub txtFilterScriptSel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilterScriptSel.TextChanged
    readScriptList()
  End Sub

  Sub readScriptList()
    lstScriptSel.Items.Clear()
    Dim filter = txtFilterScriptSel.Text.ToUpper()
    Dim folder = IDE.GetSettingsFolder() + "scriptClass\"
    For Each fileSpec In IO.Directory.GetFiles(folder)
      Dim ext = IO.Path.GetExtension(fileSpec).ToUpper
      If ext = ".VB" Or ext = ".CS" Or ext = ".VBS" Then
        If fileSpec.ToUpper().Contains(filter) Then
          lstScriptSel.Items.Add(IO.Path.GetFileName(fileSpec))
        End If
      End If
    Next
  End Sub

  Private Sub btnEditScript_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditScript.Click
    On Error Resume Next
    Dim folder = IDE.GetSettingsFolder() + "scriptClass\"
    Dim scriptName = lstScriptSel.SelectedItem

    IDE.NavigateFile(folder + scriptName)
  End Sub

  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRunScript.Click
    runScriptWithList()
  End Sub

  Sub runScriptWithList()
    On Error Resume Next
    Dim scriptName = IO.Path.GetFileNameWithoutExtension(lstScriptSel.SelectedItem)
    Dim helper = ScriptIDE.ScriptHost.ScriptHost.GetSingleton()
    Dim ref = helper.scriptClass(scriptName)

    If ref Is Nothing Then MsgBox("ScriptKlasse " + scriptName + " konnte nicht erzeugt werden.") : Exit Sub

    Dim hlp = New cls_listHelper
    hlp.Grid = IGrid1

    IGrid1.Focus()

    Dim listCount = IGrid1.Rows.Count
    ref.ListHeader(hlp, Nothing, Nothing, listCount, Nothing)

    Dim cancel As Boolean
    For i = 0 To listCount - 1
      IGrid1.SetCurRow(i)

      ref.ListBodyLoop(hlp, Nothing, Nothing, i, cancel)
      If cancel Then Exit Sub

      Application.DoEvents()
    Next

    ref.ListFooter(hlp, Nothing, Nothing, listCount, Nothing)

    Dim out = hlp.getOutData()
    ref.ListOut(hlp, out, Nothing, Nothing, Nothing, cancel)


  End Sub


  Sub calculateStat()
    grpStat.Enabled = chkStat.Checked
    If chkStat.Checked = False Then Exit Sub

    If IGrid1.RowMode Then
      Dim sum As Integer = 0
      For Each row As TenTec.Windows.iGridLib.iGRow In IGrid1.SelectedRows

      Next
      labSum.Text = "---"
      labAverage.Text = "---"
      labSelLineCount.Text = IGrid1.SelectedRows.Count
    Else
      Dim sum = 0, cnt As Integer = 0, intVal As Integer
      For Each cl As TenTec.Windows.iGridLib.iGCell In IGrid1.SelectedCells
        If Integer.TryParse(cl.Value, intVal) Then
          sum += intVal : cnt += 1
        End If
      Next

      labSum.Text = sum.ToString
      labAverage.Text = CStr(sum / cnt)
      labSelLineCount.Text = IGrid1.SelectedRows.Count
    End If

  End Sub

  Sub countDiffValues()
    'ListBox1.Sorted = False : 
    ListView1.SuspendLayout()
    ListView1.Items.Clear()
    Dim pos As New Dictionary(Of String, Integer)
    Dim actValue As String
    For Each cl As TenTec.Windows.iGridLib.iGCell In IGrid1.SelectedCells
      actValue = cl.Value
      If String.IsNullOrEmpty(actValue) Then actValue = "<empty>"
      If pos.ContainsKey(actValue) Then pos(actValue) += 1 Else pos.Add(actValue, 1)
    Next
    For Each kvp In pos
      ListView1.Items.Add(kvp.Value.ToString).SubItems.Add(kvp.Key)
    Next

    'ListBox1.Sorted = True : 
    ListView1.ResumeLayout()
  End Sub

  Private Sub chkStat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStat.CheckedChanged
    calculateStat()
  End Sub

  Private Sub IGrid1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles IGrid1.SelectionChanged
    calculateStat()
  End Sub

  Private Sub IGrid1_SelectionChanging(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGSelectionChangingEventArgs) Handles IGrid1.SelectionChanging

  End Sub

  Public Function getCurrentLineNumber() As Integer Implements ScriptIDE.Core.IDockContentForm.getCurrentLineNumber
    On Error Resume Next
    Return IGrid1.CurRow.Index
  End Function

  Public Function getLines() As String() Implements ScriptIDE.Core.IDockContentForm.getLines
    Return New String() {""}
  End Function

End Class