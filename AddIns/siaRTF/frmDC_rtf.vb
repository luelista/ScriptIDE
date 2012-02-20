Imports System.Runtime.InteropServices

<FiletypeHandler(New String() {".rtf", ".zrtf"}, False)> _
Public Class frmDC_rtf
  Implements IDockContentForm

  Public Event CurrentIndexLineChanged(ByVal lineNr As Integer) Implements IDockContentForm.CurrentIndexLineChanged

  Public Overrides Function GetPersistString() As String
    Return "RtfHelper|##|" + Me.getFileTag()
  End Function

  Private p_parameters As New Hashtable
  ReadOnly Property Parameters() As Hashtable Implements IDockContentForm.Parameters
    Get
      Return p_parameters
    End Get
  End Property

  Private Sub frmDC_rtf_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

  End Sub

  Private Sub frmDC_rtf_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    If e.CloseReason = CloseReason.MdiFormClosing Then Exit Sub
    IDE.ContentHelper._internalCloseTab(Me, e.Cancel)
  End Sub

  Private Sub frmDC_rtf_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    With mRTF
      .Font = New Font("Courier New", 10, FontStyle.Regular, GraphicsUnit.Point)
      .HideSelection = False
      .AcceptsTab = True
    End With

    tsIntellibar.Renderer = ToolstripRendererService.GetRenderer()
  End Sub


  Private p_tabRowKey As String
  'Public tabButton As Button
  Dim m_isDirty As Boolean
  Dim lastselLine As Integer = -1
  Private p_isLazy As Boolean
  Private p_indexListCtrl As IIndexList
  Dim skipNavIndexList As Boolean = False

#Region "ehemals öffentliche Variablen"
  Private m_url As String
  Public Property URL() As String Implements IDockContentForm.URL
    Get
      Return m_url
    End Get
    Set(ByVal value As String)
      m_url = value
    End Set
  End Property

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

#End Region

  Function getFileTag() As String Implements IDockContentForm.getFileTag
    Return URL
  End Function
  Sub setFileTag(ByVal sName As String) Implements IDockContentForm.setFileTag
    URL = sName
  End Sub

  Function getViewFilename() As String Implements IDockContentForm.getViewFilename
    Return URL.Substring(URL.LastIndexOf("/") + 1, URL.Length - URL.LastIndexOf("/") - 5)
  End Function
  Function getIcon() As Icon() Implements IDockContentForm.getIcon
    'Dim ext = IO.Path.GetExtension(Filename)
    Return FileTypeAndIcon.RegisteredFileType.GetFileIconByExt(".doc")
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
      Return mRTF
    End Get
  End Property

  Sub navigateIndexlist(ByVal line As String) Implements IDockContentForm.navigateIndexlist
    If skipNavIndexList Then Exit Sub
    Dim findString = Split(line, "|##|")(1)
    Dim pos = mRTF.Text.IndexOf(findString.Trim)
    If pos = -1 Then Exit Sub

    'Dim pos = mrtf.Find(line, RichTextBoxFinds.None)

    mRTF.SelectionStart = pos
    mRTF.ScrollToCaret()


  End Sub
  Sub jumpToLine(ByVal lineNr As Integer) Implements IDockContentForm.jumpToLine
    Dim firstChar = mRTF.GetFirstCharIndexFromLine(lineNr)
    mRTF.SelectionLength = 0 : mRTF.SelectionStart = lineNr

    mRTF.ScrollToCaret()
  End Sub

  Sub onSave() Implements IDockContentForm.onSave
    'isBUSY = True
    'If p_Filesource = "_loc" Then
    '  Me.mRTF.SaveFile(Me.FileName)
    'Else
    '  Me.mRTF.SaveFile(settingsFolder + "loc_cache\" + Me.FileName.Replace("/", ","))
    '  TwAjax.SaveFile("memo", Me.FileName, Me.mRTF.Rtf)
    'End If
    'Dim ph = IDE.ProtocolManager.GetURLProtocolHandler(Me.URL)
    'ph.SaveFile(Me.URL, mRTF.Rtf)
    If Me.URL.ToUpper.EndsWith(".ZRTF") Then
      Dim rtfBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(mRTF.Rtf)
      Dim rtfText As String = mRTF.Text
      Dim compressedText As String = System.Text.Encoding.Default.GetString(Utilities.Compression.Compress(rtfBytes))
      Dim compressed As String = "zRTF" & _
      " *** plainTextLength=" & rtfText.Length & _
      " *** date=" & Now.ToString("yyyy-MM-dd-HH-mm-ss") & _
      " *** cursorPos=" & mRTF.SelectionStart & _
      " *** zoom=" & CInt(mRTF.ZoomFactor * 100) & _
         vbNewLine & rtfText & vbNewLine & _
      "zRTF *** compressedRTF *** " & _
         vbNewLine & compressedText

      ProtocolService.SaveFileToURL(Me.URL, compressed)

      IDE.ContentHelper.StatusText = "zRTF save     Plain-Text: " & rtfText.Length & "     RTF-Text: " & mRTF.Rtf.Length & "     RTF-Compressed: " & compressedText.Length
    Else
      ProtocolService.SaveFileToURL(Me.URL, mRTF.Rtf)
      IDE.ContentHelper.StatusText = "default RTF save     Plain-Text: " & mRTF.TextLength & "     RTF-Text: " & mRTF.Rtf.Length
    End If
    'isBUSY = False
    Dirty = False
    If IDE.getActiveTab() Is Me Then createIndexList()
  End Sub

  Sub onRead() Implements IDockContentForm.onRead
    'isBUSY = True
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
      If fileCont Is Nothing Then
        'closeTab(Me)
        Me.Close()
        Throw New IO.FileNotFoundException("RTF file not found", URL)
        'MsgBox("Fehler: Datei existiert nicht")
        'GoTo end_of_function
      End If

      If fileCont.StartsWith("zRTF ***") Then
        Dim splitter As String = vbNewLine & "zRTF *** compressedRTF *** " & vbNewLine
        Dim pos = fileCont.IndexOf(splitter)
        If pos = -1 Then Throw New IO.InvalidDataException("zRTF Format mismatch")
        Dim zippedBytes = System.Text.Encoding.Default.GetBytes(fileCont.Substring(pos + splitter.Length))
        Dim rtfText As String = System.Text.Encoding.UTF8.GetString(Utilities.Compression.Decompress(zippedBytes))
        mRTF.Rtf = rtfText

        Dim firstLine As String = fileCont.Substring(0, fileCont.IndexOf(vbNewLine))

        IDE.ContentHelper.StatusText = "zRTF read success     firstLine=""" & firstLine & """"
      Else
        mRTF.Rtf = fileCont
      End If
      'MAIN.PerformLayout()
      mRTF.ScrollBars = RichTextBoxScrollBars.ForcedBoth

    Catch ex As Exception
      mRTF.Text = " --- Fehler beim Lesen der Datei ---" + vbNewLine + _
      "    Nachricht: " + ex.Message + vbNewLine + _
      "        Datum: " + Now.ToShortDateString + " " + Now.ToLongTimeString + vbNewLine + _
      "    Dateiname: " + URL + vbNewLine + vbNewLine + _
      ex.ToString
    End Try
end_of_function:
    'isBUSY = False
    Dirty = False
    p_isLazy = False
  End Sub

  Function getLineContent(ByVal lineNumber As Integer) As String Implements IDockContentForm.getLineContent
    Try
      Return mRTF.Lines(lineNumber)
    Catch ex As Exception
      Return ""
    End Try
  End Function
  Public Function getLineCount() As Integer Implements IDockContentForm.getLineCount
    Return mRTF.Lines.Length
  End Function

  Function getActLineContent() As String Implements IDockContentForm.getActLineContent
    Dim selStart = mRTF.SelectionStart
    Dim lineIndex = mRTF.GetLineFromCharIndex(selStart)
    Return getLineContent(lineIndex)
  End Function
  Sub selectCurLine()
    Dim selStart = mRTF.SelectionStart
    Dim lineIndex = mRTF.GetLineFromCharIndex(selStart)
    Dim firstCharCurLine = mRTF.GetFirstCharIndexOfCurrentLine
    mRTF.Select(firstCharCurLine, mRTF.GetFirstCharIndexFromLine(lineIndex + 1) - firstCharCurLine)
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
    If p_indexListCtrl Is Nothing Then
      p_indexListCtrl = IDE.ContentHelper.GetIndexList(IO.Path.GetExtension(m_url))
    End If

    If p_indexListCtrl IsNot Nothing Then
      p_indexListCtrl.buildList(Me)
      IDE.ContentHelper.ShowIndexListControl(p_indexListCtrl)

    End If

    'Dim il1, il2 As Integer : IDE.IndexList.RestorePos(il1, il2)
    'IDE.IndexList.Reset()

    'Dim tx, li As String, firstChar As Integer = 0
    'Dim LINES = Split(mRTF.Text, vbLf)
    'For i = 0 To LINES.Length - 1
    '  li = LINES(i).Trim + " "
    '  If li.StartsWith("!") Then
    '    ' Stop
    '    Dim count = getCharCountFromStart(li, "!"c)
    '    If count = 1 Then
    '      tx = li.Substring(1).Trim
    '    Else
    '      tx = StrDup(count * 4 - 4, " ") + "" + li.Substring(count).Trim
    '    End If

    '    IDE.IndexList.AddItem(tx, firstChar.ToString("000000000"), i.ToString("000000"))
    '  End If
    '  firstChar += LINES(i).Length + 1
    'Next
    'IDE.IndexList.RestorePos(il1, il2)
  End Sub


  Dim highlightBgColors() As Color = New Color() {Color.Gold, Color.LightGray, Color.FromArgb(255, 177, 33, 77), Color.FromArgb(255, 66, 166, 66), Color.White}
  Dim highlightFgColors() As Color = New Color() {Color.Black, Color.Black, Color.White, Color.White, Color.Black}
  Dim highlightCyclePos, highlightSelStart, highlightSelLen As Integer
  Sub toggleCycleHighlight()
    Dirty = True
    If highlightSelStart <> mRTF.SelectionStart Or mRTF.SelectionLength > 0 Then
      highlightSelStart = mRTF.SelectionStart
      highlightSelLen = mRTF.SelectionLength
      highlightCyclePos = 0
    Else
      mRTF.SelectionLength = highlightSelLen
    End If

    mRTF.SelectionBackColor = highlightBgColors(highlightCyclePos)
    mRTF.SelectionColor = highlightFgColors(highlightCyclePos)
    mRTF.SelectionLength = 0

    highlightCyclePos += 1
    If highlightCyclePos >= highlightBgColors.Length Then highlightCyclePos = 0
  End Sub
  Sub toggleTitle()
    Dirty = True
    If mRTF.SelectionBackColor = Color.RoyalBlue Then
      mRTF.SelectionFont = mRTF.Font
      mRTF.SelectionBackColor = Color.White : mRTF.SelectionColor = Color.Black
      If mRTF.SelectedText.StartsWith("! ") Then mRTF.SelectedText = mRTF.SelectedText.Substring(2)
    Else
      Dim selNew As Boolean = mRTF.SelectionLength = 0
      If selNew Then
        mRTF.SelectionStart = mRTF.GetFirstCharIndexOfCurrentLine
        mRTF.SelectionLength = mRTF.GetFirstCharIndexFromLine(mRTF.GetLineFromCharIndex(mRTF.SelectionStart) + 1) - 1 - mRTF.SelectionStart
      End If
      mRTF.SelectionFont = New Font("Courier New", 13, FontStyle.Bold, GraphicsUnit.Point)
      mRTF.SelectionBackColor = Color.RoyalBlue : mRTF.SelectionColor = Color.White
      If selNew Then
        mRTF.SelectedText = "! " + mRTF.SelectedText + StrDup(Math.Max(0, 60 - mRTF.SelectionLength), " ")
      Else
        mRTF.SelectedText = "! " + mRTF.SelectedText
      End If
    End If

  End Sub
  Sub toggleBold()
    Dirty = True
    Dim isBold As Boolean
    If mRTF.SelectionFont IsNot Nothing Then isBold = mRTF.SelectionFont.Bold
    If isBold Then
      mRTF.SetSelectionBold(False)
    Else
      mRTF.SetSelectionBold(True)
    End If
  End Sub
  Sub toggleItalic()
    Dirty = True
    Dim isItalic As Boolean
    If mRTF.SelectionFont IsNot Nothing Then isItalic = mRTF.SelectionFont.Italic
    If isItalic Then
      mRTF.SetSelectionItalic(False)
    Else
      mRTF.SetSelectionItalic(True)
    End If
  End Sub
  Sub toggleUnderlined()
    Dirty = True
    Dim isUnderline As Boolean
    If mRTF.SelectionFont IsNot Nothing Then isUnderline = mRTF.SelectionFont.Underline
    If isUnderline Then
      mRTF.SetSelectionUnderlined(False)
    Else
      mRTF.SetSelectionUnderlined(True)
    End If
  End Sub
  Sub toggleList()
    Dirty = True
    mRTF.SelectionBullet = Not mRTF.SelectionBullet
    mRTF.SelectionIndent = If(mRTF.SelectionBullet, 20, 0)
  End Sub
  Sub toggleIndent()
    Dirty = True
    mRTF.SelectionIndent = If(mRTF.SelectionIndent = 30, 0, 30)
  End Sub
  Sub showFontDialog()
    Using fd As New FontDialog
      fd.Font = mRTF.SelectionFont
      fd.ShowEffects = False : fd.AllowScriptChange = False : fd.ShowApply = True
      AddHandler fd.Apply, AddressOf onFontDialogApply
      If fd.ShowDialog = DialogResult.OK Then
        If mRTF.SelectionFont Is Nothing OrElse fd.Font.FontFamily IsNot mRTF.SelectionFont.FontFamily Then mRTF.SetSelectionFont(fd.Font.FontFamily.Name)
        If mRTF.SelectionFont Is Nothing OrElse fd.Font.Size <> mRTF.SelectionFont.Size Then mRTF.SetSelectionSize(fd.Font.Size)
      End If
    End Using
  End Sub

  Private Sub mrtf_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles mRTF.KeyDown
    'Dim keyString = getKeyString(e)
    'Debug.Print("rtf Keydown " + keyString)

    If e.Control Then
      Select Case e.KeyCode
        Case Keys.H : toggleCycleHighlight()
        Case Keys.T : Me.toggleTitle()
        Case Keys.B : Me.toggleBold()
        Case Keys.I : Me.toggleItalic()
        Case Keys.U : Me.toggleUnderlined()
        Case Keys.L : Me.toggleList()
        Case Keys.J : Me.toggleIndent() : e.SuppressKeyPress = True
        Case Keys.S, Keys.Enter : Me.onSave() : e.SuppressKeyPress = True
        Case Keys.Y : showFontDialog()
        Case Keys.E : ProtocolService.NavigateFilelistToURL(URL) : e.SuppressKeyPress = True : e.Handled = True
        Case Keys.F : txtSearch.Focus() : txtSearch.SelectAll()
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

    If e.KeyCode = Keys.F10 Then mRTF.SetSelectionFont("Segoe UI")
    If e.KeyCode = Keys.F11 Then mRTF.SetSelectionFont("Courier New")
    If e.KeyCode = Keys.F12 Then mRTF.SetSelectionFont("Comic Sans MS")

    If e.KeyCode = 226 And IDE.Glob.para("siaRTF__merkeZeile", "TRUE") = "TRUE" Then ' kleiner Zeichen - <
      Dim selStart = mRTF.SelectionStart
      Dim firstCharInLine = mRTF.GetFirstCharIndexOfCurrentLine()
      If firstCharInLine = selStart Then
        e.SuppressKeyPress = True
        If e.Shift Then
          mRTF.SelectionLength = 0
          Dim merkeData() As String = IDE.ContentHelper.merkeZeileAbruf()
          For Each line In merkeData
            mRTF.SelectedRtf = line
            mRTF.Select(mRTF.SelectionStart + mRTF.SelectionLength, 0)
          Next
        Else
          selectCurLine()
          IDE.ContentHelper.merkeZeile(mRTF.SelectedRtf)
          If e.Control Or e.Alt Then
            mRTF.SelectionLength = 0
          Else
            mRTF.SelectedText = ""
          End If
        End If
      End If
    End If

  End Sub
  Sub onFontDialogApply(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim fd As FontDialog = sender
    If fd.Font.FontFamily.Name <> mRTF.SelectionFont.FontFamily.Name Then mRTF.SetSelectionFont(fd.Font.FontFamily.Name)
    If fd.Font.Size <> mRTF.SelectionFont.Size Then mRTF.SetSelectionSize(fd.Font.Size)
  End Sub


  Private Sub mrtf_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles mRTF.LinkClicked

    If e.LinkText.StartsWith("\\") Then

      If e.LinkText.ToLower.StartsWith("\\note_") Or e.LinkText.ToLower.StartsWith("\\note-") _
          Or e.LinkText.ToLower.StartsWith("\\note/") Then
        Dim nam = "twajax:/memo/note/" + e.LinkText.Substring(7)
        IDE.NavigateFile(nam)
      Else
        Dim linkText = "mailto:/" + e.LinkText.Substring(2)
        Dim appbarFileSpec = helper.GetExePath("appbar")
        If appbarFileSpec = "" Then
          MsgBox("um dieses Feature zu nutzen, musst du die Appbar installiert haben!", MsgBoxStyle.Critical)
        Else
          Process.Start(appbarFileSpec, "/mailto_protocol """ + linkText + """ fromScriptIDE " + URL)
        End If
      End If
    ElseIf e.LinkText.StartsWith("http://") Then
      Dim lnk As String = e.LinkText
      lnk = Replace(lnk, "|TW|", ".teamwiki.de/")
      lnk = Replace(lnk, "|TWH|", ".html?designType=1")
      Process.Start(lnk)
    Else
      Process.Start(e.LinkText)
    End If
  End Sub


  Private Sub mrtf_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mRTF.SelectionChanged
    Dim x = mRTF.GetLineFromCharIndex(mRTF.SelectionStart)
    Dim first = mRTF.GetFirstCharIndexOfCurrentLine
    IDE.ContentHelper.StatusText = "" & x & " : " & (mRTF.SelectionStart - first) & "   SelStart: " & mRTF.SelectionStart & "   SelLength: " & mRTF.SelectionLength
    If x = lastselLine Then Exit Sub
    lastselLine = x
    Dim row = mRTF.GetLineFromCharIndex(mRTF.SelectionStart)
    'IDE.IndexList.OnLinenumberChanged(pos)
    If p_indexListCtrl IsNot Nothing Then
      p_indexListCtrl.onPositionChanged(row)

    End If

    RaiseEvent CurrentIndexLineChanged(row)

    'skipNavIndexList = True
    'For i = p_indexListCtrl.Items.Count - 1 To 0 Step -1
    '  Dim line = CInt(Microsoft.VisualBasic.Right(p_indexListCtrl.Items(i), 8))
    '  If line <= pos Then p_indexListCtrl.SelectedIndex = i : Exit For
    'Next
    'skipNavIndexList = False
  End Sub

  Private Sub mrtf_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mRTF.TextChanged

    Dirty = True
  End Sub

  Public Sub onLazyInit() Implements IDockContentForm.onLazyInit

    IDE.ContentHelper.CreateFileactionToolbar(Me.URL, IO.Path.GetExtension(URL).ToLower, tsIntellibar)

  End Sub

  Public Function getCurrentLineNumber() As Integer Implements ScriptIDE.Core.IDockContentForm.getCurrentLineNumber
    Return mRTF.GetLineFromCharIndex(mRTF.SelectionStart)
  End Function

  Public Function getLines() As String() Implements ScriptIDE.Core.IDockContentForm.getLines
    Return mRTF.Lines
  End Function

  Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
    onSave()
  End Sub

  Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
    If e.KeyCode = Keys.Enter Then
      Dim startPos As Integer = mRTF.SelectionStart + 1
      Dim findPos As Integer = mRTF.Find(txtSearch.Text, startPos, RichTextBoxFinds.None)
      If findPos = -1 Then
        MsgBox("Keine weiteren Treffer. Suche beginnt von oben.", MsgBoxStyle.Information, "RTF-Suche")
        findPos = mRTF.Find(txtSearch.Text, 0, RichTextBoxFinds.None)

      End If


      e.SuppressKeyPress = True
    End If
  End Sub

  Private Sub tsddbZoom_DropDownItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsddbZoom.DropDownItemClicked
    Dim zoom As Integer = e.ClickedItem.Text.Substring(0, e.ClickedItem.Text.Length - 1)
    mRTF.ZoomFactor = zoom / 100
    tsddbZoom.Text = e.ClickedItem.Text
  End Sub

  Private Sub tsIntellibar_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tsIntellibar.DragDrop
    e.Effect = DragDropEffects.Link
    If e.Data.GetDataPresent("FileDrop") Then
      '  mRTF.InsertImageFromFile(e.Data.GetData("FileDrop")(0))
      mRTF.InsertImage(e.Data.GetData("FileDrop")(0), mRTF.SelectionStart)
    End If
  End Sub

  Private Sub tsIntellibar_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tsIntellibar.DragEnter
    e.Effect = DragDropEffects.Link
  End Sub

  Private Sub tsIntellibar_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsIntellibar.ItemClicked

  End Sub


  Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
    Dim io = mRTF.InsertObjectDialog()
    IDE.ContentHelper.StatusText = "Object inserted     ClassID=" + io.clsid.ToString + "    File=" + io.lpszFile
    mRTF.SelectionLength = 0
    mRTF.SelectedText = IDE.ContentHelper.StatusText
  End Sub

  Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
    Dim wb As New WebBrowser()

    mRTF.InsertControl(wb)
    wb.Navigate("http://www.teamwiki.net")

  End Sub

End Class