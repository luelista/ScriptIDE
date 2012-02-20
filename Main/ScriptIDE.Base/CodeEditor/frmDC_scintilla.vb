Imports ICSharpCode
Imports ICSharpCode.SharpDevelop

<FiletypeHandler(New String() {}, False)> _
Public Class frmDC_scintilla
  Implements IDockContentForm

  Public Event CurrentIndexLineChanged(ByVal lineNr As Integer) Implements Core.IDockContentForm.CurrentIndexLineChanged


  Dim WithEvents sc1 As ScintillaNet.Scintilla


  'TODO intellisense BEGIN
  'Dim intelliSenseStartPos As Integer
  'Friend IntellisenseEnabled, IntellisenseActive, IntellisenseSkipNextKeypress As Boolean

  'Friend myProjectContent As Dom.DefaultProjectContent
  'Friend parseInformation As New Dom.ParseInformation()
  'Private lastCompilationUnits As Dictionary(Of String, Dom.ICompilationUnit)
  'Private scriptPreproc As ScriptHost.cls_preprocVB2

  'Sub initializeIntelliSense()
  '  lastCompilationUnits = New Dictionary(Of String, Dom.ICompilationUnit)
  '  scriptPreproc = New ScriptHost.cls_preprocVB2()
  '  Dim fileSpec As String = ProtocolService.MapToLocalFilename(Me.URL)
  '  ScriptHost.scHostNET2.InitializePrecompiler(scriptPreproc, fileSpec, 4) 'PPD_DEBUG=4

  '  myProjectContent = New Dom.DefaultProjectContent()
  '  addProjectRefsFromPreproc(myProjectContent, scriptPreproc)

  '  IntellisenseEnabled = True

  'End Sub

  'Private Sub sc1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles sc1.KeyPress
  '  If lstIntellisense.Visible Then
  '    TT.Write("keyPress", e.KeyChar.GetHashCode)
  '    If Char.IsLetterOrDigit(e.KeyChar) = False And e.KeyChar <> "_"c And e.KeyChar <> Chr(8) Then
  '      insertSelItem()
  '    Else
  '      searchInISList(e.KeyChar)
  '    End If
  '  Else
  '  End If
  '  If e.KeyChar = "."c Or e.KeyChar = "#"c Then onAutoComplete(e.KeyChar)
  '  If e.KeyChar = "("c Then showTooltip("klammer")
  '  'If e.KeyChar = ","c Then showTooltip("komma")
  'End Sub
  'Sub searchInISList(Optional ByVal lastChar As Char = Nothing)
  '  If Not lstIntellisense.Visible Then Exit Sub
  '  Dim range = sc1.GetRange(intelliSenseStartPos, sc1.Selection.Start)
  '  Dim txt = range.Text
  '  If lastChar <> Nothing Then
  '    If lastChar = vbBack Then 'das muss in den keyEvent
  '      If txt.Length = 0 Then lstIntellisense.Hide() : Exit Sub
  '      txt = txt.Substring(0, txt.Length - 1)
  '    Else
  '      txt += lastChar
  '    End If
  '  End If
  '  lstIntellisense.SelectedIndex = lstIntellisense.FindString(txt)
  'End Sub

  'Sub insertSelItem()
  '  If Not lstIntellisense.Visible Then Exit Sub
  '  If intelliSenseStartPos > sc1.Selection.Start Then Exit Sub
  '  Dim range = sc1.GetRange(intelliSenseStartPos, sc1.Selection.Start)
  '  Dim insText As String = lstIntellisense.SelectedItem
  '  If insText IsNot Nothing Then
  '    range.Text = insText
  '    sc1.Selection.Start += insText.Length
  '  End If
  '  lstIntellisense.Hide()
  'End Sub

  'Private Sub lstIntellisense_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstIntellisense.DoubleClick
  '  sc1.Focus()
  '  insertSelItem()
  'End Sub

  'Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
  '  initializeIntelliSense()
  'End Sub

  'Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

  '  If Not IntellisenseEnabled Then Exit Sub

  '  ParseStep(myProjectContent, scriptPreproc, lastCompilationUnits)
  '  For Each file In scriptPreproc.Files
  '    If file.FileType = ScriptHost.cls_preprocVB2.PPT_MAINCLASS Then
  '      parseInformation.SetCompilationUnit(lastCompilationUnits(file.tempFileSpec))
  '      Exit For
  '    End If
  '  Next
  'End Sub

  'Sub onAutoComplete(ByVal autoCompleteMode As String)
  '  If Not IntellisenseEnabled Then Exit Sub
  '  IntellisenseSkipNextKeypress = True

  '  Dim startPos = sc1.Selection.Start
  '  Dim ch As Char
  '  If autoCompleteMode = "BACK" Then
  '    While startPos > 0
  '      startPos -= 1
  '      ch = sc1.Text.Chars(startPos - 1)
  '      If ch = "("c Or ch = ","c Or ch = vbCr Or ch = vbLf Then Exit Sub '!!!
  '      If ch = "."c Then startPos -= 1 : Exit While
  '    End While
  '  End If
  '  If autoCompleteMode = "manuell" Then
  '    While startPos > 0
  '      startPos -= 1
  '      ch = sc1.Text.Chars(startPos)
  '      If ch = "("c Or ch = ","c Or ch = vbCr Or ch = vbLf Then Exit Sub '!!!
  '      If ch = "."c Then Exit While
  '    End While
  '  End If
  '  intelliSenseStartPos = startPos + 1
  '  TT.Write("StartPos=", sc1.Text.Substring(Math.Max(0, startPos - 20), 20))

  '  Dim finder As Dom.IExpressionFinder
  '  'If MainForm.IsVisualBasic Then
  '  finder = New Dom.VBNet.VBExpressionFinder()
  '  'Else
  '  '    finder = New Dom.CSharp.CSharpExpressionFinder(mainForm.parseInformation)
  '  'End If

  '  Dim expression As Dom.ExpressionResult = finder.FindExpression(bogusScriptPrefix + sc1.Text, bogusScriptOffset + startPos)
  '  If expression.Region.IsEmpty Then
  '    Dim selRange = sc1.Selection.Range
  '    expression.Region = New Dom.DomRegion(200 + selRange.StartingLine.Number, sc1.GetColumn(selRange.Start))
  '  End If

  '  If expression.Expression = "" Then Exit Sub

  '  Dim resolver As New Dom.NRefactoryResolver.NRefactoryResolver(myProjectContent.Language)
  '  'Dim exp As Dom.ExpressionResult = FindExpression(sc1)


  '  ' If autoCompleteMode="BACK" then 
  '  'If exp.Expression ="New" then
  '  TT.Write("expression", expression.Expression)
  '  Dim rr As Dom.ResolveResult = resolver.Resolve(expression, parseInformation, bogusScriptPrefix + sc1.Text)
  '  'Dim resultList As New List(Of ICompletionData)()
  '  If rr IsNot Nothing Then
  '    Dim completionData As Collections.ArrayList = rr.GetCompletionData(myProjectContent)
  '    If completionData IsNot Nothing Then
  '      TT.Write("completionData.Count=", completionData.Count)

  '      '   lstIntellisense.Font = New Font(sc1.Font.FontFamily, sc1.Font.SizeInPoints * sc1.Zoom, FontStyle.Regular, GraphicsUnit.Point)

  '      showListForCompletion(completionData)
  '      'AddCompletionData(resultList, completionData)
  '    Else
  '      TT.Write("completionData IS Nothing")
  '    End If

  '  Else
  '    TT.Write("ResolveResult IS Nothing")
  '  End If
  '  'Return resultList.ToArray()



  'End Sub


  'Sub showListForCompletion(ByVal completionData As Collections.ArrayList)
  '  lstIntellisense.Hide()

  '  lstIntellisense.Sorted = False
  '  lstIntellisense.Items.Clear()
  '  Dim textItem As String
  '  For Each item As Object In completionData
  '    If TypeOf item Is String Then
  '      textItem = item
  '    Else
  '      textItem = item.name
  '    End If
  '    If Not lstIntellisense.Items.Contains(textItem) Then lstIntellisense.Items.Add(textItem)
  '  Next
  '  Dim pos = intelliSenseStartPos - 1
  '  '  Dim pos As Point =sc1.po  myTextArea.GetPositionFromCharIndex()
  '  Dim topPos = sc1.PointYFromPosition(pos)
  '  Dim itemsHeight = lstIntellisense.ItemHeight * lstIntellisense.Items.Count
  '  Dim inBottomHalf As Boolean = topPos > sc1.Height \ 2
  '  Dim fitsUnderLine As Boolean = sc1.Height - topPos - 40 > itemsHeight
  '  If inBottomHalf AndAlso fitsUnderLine = False Then
  '    lstIntellisense.Height = Math.Min(itemsHeight + 4, topPos - 100)
  '    lstIntellisense.Top = topPos - lstIntellisense.Height + 25
  '  Else
  '    If fitsUnderLine Then
  '      lstIntellisense.Height = itemsHeight + 8
  '    Else
  '      lstIntellisense.Height = Math.Min(itemsHeight + 4, sc1.Height - topPos - 100)
  '    End If
  '    lstIntellisense.Top = topPos + 40
  '  End If
  '  'lstIntellisense.Top = sc1.PointYFromPosition(pos) + 40
  '  lstIntellisense.Left = sc1.PointXFromPosition(pos) + 8
  '  lstIntellisense.Show()
  '  lstIntellisense.Sorted = True
  '  lstIntellisense.BringToFront()
  'End Sub

  'Sub showTooltip(ByVal keyMode As String)
  '  If Not IntellisenseEnabled Then Exit Sub
  '  'Dim textArea As TextEditor.TextArea = editor.ActiveTextAreaControl.TextArea

  '  Dim finder As Dom.IExpressionFinder
  '  'If MainForm.IsVisualBasic Then
  '  finder = New Dom.VBNet.VBExpressionFinder()

  '  Dim commaCount As Integer = 0
  '  Dim startPos = sc1.Selection.Start
  '  If keyMode = "klammer" Then
  '    'startPos -= 1
  '    RichTextPlus1.Tag = Nothing
  '  Else
  '    Dim parenthesesCount As Integer = 0, inStr As Boolean, ch As Char
  '    While startPos > 0
  '      startPos -= 1
  '      ch = sc1.Text.Substring(startPos, 1)
  '      If ch = vbCr Or ch = vbLf Then
  '        If Not inStr Then RichTextPlus1.Hide()
  '        Exit Sub
  '      End If
  '      If Not inStr And ch = ")"c Then parenthesesCount += 1
  '      If Not inStr And ch = "("c Then parenthesesCount -= 1
  '      If Not inStr And ch = ","c Then commaCount += 1
  '      If ch = """"c Then inStr = Not inStr
  '      If parenthesesCount < 0 Then Exit While
  '    End While
  '    'If keyMode = "komma" Then commaCount += 1
  '  End If

  '  If RichTextPlus1.Visible = False Or RichTextPlus1.Tag Is Nothing Then
  '    TT.Write("StartPos=", sc1.Text.Substring(Math.Max(0, startPos - 20), 20))

  '    Dim expression As Dom.ExpressionResult = finder.FindExpression(bogusScriptPrefix + sc1.Text, bogusScriptOffset + startPos)
  '    If expression.Region.IsEmpty Then
  '      Dim selRange = sc1.Selection.Range
  '      expression.Region = New Dom.DomRegion(200 + selRange.StartingLine.Number, sc1.GetColumn(selRange.Start))
  '    End If

  '    'Dim expression As Dom.ExpressionResult = FindFullExpression(sc1)
  '    TT.Write("expression", expression.Expression)

  '    Dim resolver As New Dom.NRefactoryResolver.NRefactoryResolver(myProjectContent.Language)
  '    Dim rr As Dom.ResolveResult
  '    rr = resolver.Resolve(expression, parseInformation, bogusScriptPrefix + sc1.Text)
  '    Dim text As String = GetText(rr, commaCount)

  '    '  Dim pos As Point =sc1.po  myTextArea.GetPositionFromCharIndex()
  '    RichTextPlus1.Top = sc1.PointYFromPosition(startPos) + 18 + 25
  '    RichTextPlus1.Left = sc1.PointXFromPosition(startPos) - 10
  '    RichTextPlus1.HTMLCode = text
  '    RichTextPlus1.Tag = rr
  '    RichTextPlus1.BringToFront()
  '    RichTextPlus1.Show()
  '  Else
  '    RichTextPlus1.HTMLCode = GetText(RichTextPlus1.Tag, commaCount)
  '  End If

  '  '' Dim toolTipText As String = GetText(rr)
  '  '' If toolTipText IsNot Nothing Then
  '  ''     e.ShowToolTip(toolTipText)
  '  '' End If
  'End Sub
  'TODO intellisense END



  Public Overrides Function GetPersistString() As String
    Return "ScintillaHelper|##|" + Me.Hash
  End Function


  Private Sub frmDC_scintilla_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    If e.CloseReason = CloseReason.MdiFormClosing Then Exit Sub
    If internalCloseTab(Me) = False Then e.Cancel = True
  End Sub

  Public Sub onLazyInit() Implements IDockContentForm.onLazyInit
    sc1 = New ScintillaNet.Scintilla
    sc1.ContextMenuStrip = ContextMenuStrip1
    Me.Controls.Add(sc1)
    tsIntellibar.Dock = DockStyle.Top

    sc1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Right
    sc1.Bounds = New Rectangle(0, tsIntellibar.Height, ClientSize.Width, ClientSize.Height - tsIntellibar.Height)
    onAddinTreeChanged()

    sc1.Margins(0).Width = If(p_FileType = "vbnet" Or p_FileType = "vbscript", 20, 0)
    tsIntellibar.Renderer = ToolstripRendererService.GetRenderer()
    With sc1
      .Font = New Font("Courier New", 11, FontStyle.Regular, GraphicsUnit.Point)
      .Styles.Default.FontName = "Courier New"
      .Selection.HideSelection = False
      .AcceptsTab = True

      .Indentation.IndentWidth = 2
      .Indentation.TabWidth = 2
      .Indentation.SmartIndentType = ScintillaNet.SmartIndent.Simple
      .Indentation.UseTabs = False

      .Margins(0).Width = 0 'Spalte für Breakpoint
      .Margins(0).Type = 4

      .Margins(0).IsClickable = True
      .Margins(0).Mask = 16

      .Margins(1).Width = 40 'Zeilennummer
      .Margins(1).Type = ScintillaNet.MarginType.Number
      .Margins(1).IsMarkerMargin = False

      .Margins(2).Width = 20 'Restliche symbole
      .Margins(2).Type = ScintillaNet.MarginType.Symbol
      .Margins(2).IsClickable = True
      .Margins(2).IsMarkerMargin = True
      .Margins(2).Mask = (2 ^ 10) Or (2 ^ 12) Or (2 ^ 15) '12 und 13=executing; 15 und 13=executing2; 10 und 11=error; 14=jumpToLine

      .Margins(3).IsMarkerMargin = True
      .Margins(3).Type = ScintillaNet.MarginType.Symbol
      .Margins(3).Mask = (2 ^ 11) Or (2 ^ 13) Or (2 ^ 14)

      .Styles.LineNumber.IsVisible = True 'zeilennummern an
      .IsBraceMatching = True

    End With
    initScintillaHighlighter(sc1, IO.Path.GetExtension(URL))

    AddHandler AddInTree.Changed, AddressOf onAddinTreeChanged

  End Sub

  Sub onAddinTreeChanged()
    While tsIntellibar.Items.Count > 1
      tsIntellibar.Items.RemoveAt(1)
    End While
    createToolbaritemsForExt(IO.Path.GetExtension(URL).ToLower, tsIntellibar, Me.URL)
  End Sub

  'Private p_FileName As String
  'Private p_FileSource As String = ""
  Private p_FileType As String

  Private p_tabRowKey As String
  Private p_isLazy As Boolean
  Dim m_isDirty As Boolean
  Dim lastselLine As Integer = -1
  Private WithEvents p_indexListCtrl As IIndexList


  Private p_parameters As New Hashtable
  ReadOnly Property Parameters() As Hashtable Implements IDockContentForm.Parameters
    Get
      Return p_parameters
    End Get
  End Property

#Region "ehemals öffentliche Variablen"
  Private m_url As String
  Public Property URL() As String Implements IDockContentForm.URL
    Get
      Return m_url
    End Get
    Set(ByVal value As String)
      m_url = value
      Me.Icon = getIcon(0)
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
      Return p_FileType
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
    'If p_FileSource = "_loc" Then
    '  Return "loc:/" + p_FileName
    'Else
    '  Return "ftp:/" + p_FileSource + "/" + p_FileName
    'End If
  End Function
  Sub setFileTag(ByVal sName As String) Implements IDockContentForm.setFileTag
    URL = sName
    'Dim name() = Split(sName, "/", 3)
    'If name(0).ToUpper = "LOC:" Then
    '  Me.p_FileSource = "_loc"
    '  Me.p_FileName = name(1) + "/" + name(2)
    'Else
    '  Me.p_FileSource = name(1)
    '  Me.p_FileName = name(2)
    'End If
    p_FileType = getIntelliPanelClass(URL)
    Me.Icon = Me.getIcon(0)
  End Sub

  Function getViewFilename() As String Implements IDockContentForm.getViewFilename
    Return URL.Substring(URL.LastIndexOf("/") + 1)
    'Return p_FileName.Substring(p_FileName.LastIndexOf("/") + 1)
  End Function
  Function getIcon() As Icon() Implements IDockContentForm.getIcon
    'Dim ext = p_FileName.Substring(p_FileName.LastIndexOf("."))
    Dim ext = URL.Substring(URL.LastIndexOf("."))
    Return FileTypeAndIcon.RegisteredFileType.GetFileIconByExt(ext)
  End Function

  Property Dirty() As Boolean Implements IDockContentForm.Dirty
    Get
      Return m_isDirty
    End Get
    Private Set(ByVal value As Boolean)
      If m_isDirty = value Then Exit Property
      m_isDirty = value
      'tabButton.Text = If(value, "^", "") + getViewFilename()
      'tbOpenedFiles.IGrid1.Cells(p_tabRowKey, 1).Value = If(value, "♦", "")
      Me.Text = If(value, "◊", "") + getViewFilename()
      createOpenedTabList()
    End Set
  End Property

  ReadOnly Property RTF() As Object Implements IDockContentForm.RTF
    Get
      Return sc1
    End Get
  End Property



  Private Sub p_indexListCtrl_ItemClicked(ByVal lineNumber As Integer) Handles p_indexListCtrl.ItemClicked
    If getActiveRTF() IsNot Me Then Exit Sub
    Dim actfirstvis = sc1.Lines.FirstVisible.Number
    Dim newfirstvis = Math.Max(lineNumber - 10, 0)
    sc1.Scrolling.ScrollBy(0, newfirstvis - actfirstvis)

    sc1.Selection.Start = sc1.Lines(lineNumber).StartPosition
    sc1.Selection.Length = 1
    sc1.Focus()
  End Sub

  Sub navigateIndexlist(ByVal line As String) Implements IDockContentForm.navigateIndexlist
    Dim gotoLine As Integer
    If Not Integer.TryParse(Split(line, "|##|")(2), gotoLine) Then Exit Sub

    Dim actfirstvis = sc1.Lines.FirstVisible.Number
    Dim newfirstvis = Math.Max(gotoLine - 10, 0)
    sc1.Scrolling.ScrollBy(0, newfirstvis - actfirstvis)

    sc1.Selection.Start = sc1.Lines(gotoLine).StartPosition
    sc1.Selection.Length = 1
    sc1.Focus()
  End Sub

  Sub jumpToLine(ByVal line As Integer) Implements IDockContentForm.jumpToLine
    'setLineMarker(line, 14, ScintillaNet.MarkerSymbol.RoundRectangle, Color.FromArgb(255, 204, 228, 255), Color.Black, True)
    setLineMarker(line, 14, My.Resources.bug2, Color.FromArgb(255, 204, 228, 255), Color.Black, True)

    Dim actfirstvis = sc1.Lines.FirstVisible.Number
    Dim newfirstvis = Math.Max(line - 10, 0)
    sc1.Scrolling.ScrollBy(0, newfirstvis - actfirstvis)

    sc1.Selection.Start = sc1.Lines(line).StartPosition
    sc1.Selection.Length = 1
    sc1.Focus()

    ' sc1.Markers.DeleteAll(14)
  End Sub
  Sub highlightExecutingLine(ByVal line As Integer)
    'ScintillaNet.MarkerSymbol.ShortArrow
    setLineMarker(line, 12, My.Resources.executing, Color.Yellow, Color.Orange, True)
    setLineMarker(line, 13, 0, ColorTranslator.FromHtml("#FFFF7B"), Color.Orange, True) 'vb: #FFEE62
    If line < 0 Then Exit Sub
    sc1.GoTo.Line(line)
    ' sc1.CallTip.HighlightStart = sc1.Lines(line).SelectionStartPosition
    ' sc1.CallTip.HighlightEnd = sc1.Lines(line).SelectionEndPosition
    ' sc1.CallTip.HighlightTextColor = Color.Red
    ' sc1.CallTip.Message = "Pausiert: " & line
    ' sc1.CallTip.Show()
  End Sub
  Sub highlightExecutingLine2(ByVal line As Integer)
    'ScintillaNet.MarkerSymbol.ShortArrow
    setLineMarker(line, 15, My.Resources.executing4, Color.Yellow, Color.Orange, True)
    setLineMarker(line, 13, 0, ColorTranslator.FromHtml("#AFFF7A"), Color.Orange, True) 'vb: #FFEE62
    If line < 0 Then Exit Sub
    sc1.GoTo.Line(line)
    ' sc1.CallTip.HighlightStart = sc1.Lines(line).SelectionStartPosition
    ' sc1.CallTip.HighlightEnd = sc1.Lines(line).SelectionEndPosition
    ' sc1.CallTip.HighlightTextColor = Color.Red
    ' sc1.CallTip.Message = "Pausiert: " & line
    ' sc1.CallTip.Show()
  End Sub
  Sub highlightErrorLine(ByVal line As Integer)
    setLineMarker(line, 10, ScintillaNet.MarkerSymbol.Arrows, Color.Transparent, Color.Red, True)
    setLineMarker(line, 11, 0, ColorTranslator.FromHtml("#EAB9FA"), Color.White, True)
    If line < 0 Then Exit Sub
    sc1.GoTo.Line(line)
  End Sub
  Sub setLineMarker(ByVal line As Integer, ByVal markerIndex As Integer, ByVal markerIcon As Object, ByVal bgColor As Color, ByVal fgColor As Color, Optional ByVal removeOthers As Boolean = False, Optional ByVal toggleMe As Boolean = False)
    If removeOthers Or line = -1 Then sc1.Markers.DeleteAll(markerIndex)
    If line < 0 Then Exit Sub
    If toggleMe = True And (sc1.Lines(line).GetMarkerMask And 16) = 16 Then
      sc1.Lines(line).DeleteMarker(4) : Exit Sub
    End If
    Dim m = sc1.Lines(line).AddMarker(markerIndex)
    If IsNumeric(markerIcon) Then
      m.Marker.Symbol = markerIcon
    ElseIf TypeOf markerIcon Is Image Then
      m.Marker.Symbol = ScintillaNet.MarkerSymbol.PixMap
      m.Marker.SetImage(markerIcon, Color.FromArgb(255, 255, 255, 255))
    Else
      m.Marker.Symbol = ScintillaNet.MarkerSymbol.PixMap
      m.Marker.SetImage(Image.FromFile(markerIcon), Color.FromArgb(255, 255, 255, 255))
    End If

    m.Marker.ForeColor = fgColor
    m.Marker.BackColor = bgColor

  End Sub

  Sub onSave() Implements IDockContentForm.onSave

    If bwReadSave.IsBusy Then
      Beep() : Exit Sub
    End If
    pnlLoadIndicator.Show()
    bwReadSave.RunWorkerAsync(New String() {"save", Me.sc1.Text})
    'isBUSY = True
    'If p_FileSource = "_loc" Then
    '  IO.File.WriteAllText(p_FileName, Me.sc1.Text)
    '  If p_FileName.EndsWith("fileassoc.txt") Then
    '    loadFileAssocTab()
    '  End If
    'Else
    '  ftpSaveTextFile(p_FileSource, p_FileName, Me.sc1.Text)
    '  Me.navFilelistToMe()
    'End If
    'ProtocolService.SaveFileToURL(URL, Me.sc1.Text)

    'sc1.Markers.DeleteAll(10) : sc1.Markers.DeleteAll(11) 'errors ausblenden
    ''isBUSY = False
    'Dirty = False
    'If getActiveRTF() Is Me Then createIndexList()
  End Sub

  Sub onRead() Implements IDockContentForm.onRead
    If bwReadSave.IsBusy Then
      Beep() : Exit Sub
    End If
    pnlLoadIndicator.Show()
    p_isLazy = False
    bwReadSave.RunWorkerAsync(New String() {"read", ""})

    '    'isBUSY = True
    '    ' Stop
    '    Try
    '      Dim fileCont As String = ""
    '      'If p_FileSource = "_loc" Then
    '      '  fileCont = IO.File.ReadAllText(p_FileName)
    '      'Else
    '      '  fileCont = ftpReadTextFile(p_FileSource, p_FileName)
    '      '  '   Me.navFilelistToMe()
    '      'End If
    '      fileCont = ProtocolService.ReadFileFromURL(URL)
    '      sc1.Text = fileCont

    '      'initScintillaHighlighter(sc1, URL)
    '      'showIntelliPanel(p_FileType, Me)


    '      'sc1.ConfigurationManager.CustomLocation = "D:\_Downloads\wscite201\wscite\html.properties"
    '      ' sc1.ConfigurationManager.Configure()

    '      'MAIN.PerformLayout()
    '      'sc1.ScrollBars = RichTextBoxScrollBars.ForcedBoth
    '    Catch ex As Exception
    '      MsgBox("Fehler beim Einlesen ..." + vbNewLine + "evtl. neue Datei" + vbNewLine + ex.ToString)
    '    End Try
    'end_of_function:
    '    'isBUSY = False
    '    Dirty = False
  End Sub


  Private Sub bwReadSave_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReadSave.DoWork
    If e.Argument(0) = "read" Then

      Try
        Dim fileCont As String = ""
        fileCont = ProtocolService.ReadFileFromURL(URL)
        e.Result = New String() {"", fileCont}
      Catch ex As Exception
        MsgBox("Fehler, die Datei konnte nicht gelesen werden!" + vbNewLine + "URL: " + URL + vbNewLine + vbNewLine + ex.Message, MsgBoxStyle.Exclamation, "Fehler beim Lesen")
      End Try

    ElseIf e.Argument(0) = "save" Then
      ProtocolService.SaveFileToURL(URL, e.Argument(1))
      Try
        My.Computer.Audio.Play("C:\windows\media\start.wav")
      Catch : End Try
    End If
  End Sub

  Private Sub bwReadSave_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReadSave.RunWorkerCompleted
    If TypeOf e.Result Is String() Then
      'nach lesen
      sc1.Text = e.Result(1)
    Else
      'nach speichern
      sc1.Markers.DeleteAll(10) : sc1.Markers.DeleteAll(11) 'errors ausblenden
      If getActiveRTF() Is Me Then createIndexList()
    End If
    Dirty = False
    pnlLoadIndicator.Hide()
  End Sub


  Sub navFilelistToMe()
    ProtocolService.NavigateFilelistToURL(URL)
    'If FileSource = "_loc" Then
    '  Dim folder = IO.Path.GetDirectoryName(FileName)
    '  tbFileExplorer.ftvLocalbrowser.SelectedFolder.name = folder
    '  tbFileExplorer.Show(MAIN.DockPanel1)
    'Else
    '  tbFtpExplorer.Show(MAIN.DockPanel1)
    '  Application.DoEvents()
    '  Dim directory = "/" + p_FileName.Substring(0, p_FileName.LastIndexOf("/") + 1)
    '  fillFtpFilelist(p_FileSource, directory, True)
    'End If
  End Sub

  Function getLineContent(ByVal lineNumber As Integer) As String Implements IDockContentForm.getLineContent
    Try
      Return sc1.Lines(lineNumber).Text
    Catch ex As Exception
      Return ""
    End Try
  End Function
  Public Function getLineCount() As Integer Implements IDockContentForm.getLineCount
    Return sc1.Lines.Count
  End Function

  Function getActLineContent() As String Implements IDockContentForm.getActLineContent
    Return sc1.Lines.Current.Text
  End Function

  Sub selectCurLine()
    sc1.Lines.Current.Select()
  End Sub


  Function onCheckDirty(Optional ByVal beforeWhat As String = "vor dem Schließen ") As Boolean Implements IDockContentForm.onCheckDirty
    If Dirty Then
      setActRtfBox(Me)
      Select Case MsgBox("Im Dokument " + URL + " befinden sich ungespeicherte Änderungen. Soll es " + beforeWhat + "gespeichert werden?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel, "scriptIDE - Datei speichern?")
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
    getCharCountFromStart = 0
    Do While text.Substring(getCharCountFromStart, 1) = c
      getCharCountFromStart += 1
    Loop
  End Function



  Sub createIndexList() Implements IDockContentForm.createIndexList
    cls_IDEHelper.Instance.ContentHelper.SimpleCreateIndexList(Me, p_indexListCtrl)
  End Sub

  Sub commentBlock()
    Dim startLine = sc1.Selection.Range.StartingLine.Number, endLine = sc1.Selection.Range.EndingLine.Number
    Dim insertPos = sc1.Lines(startLine).Text.TrimEnd.Length - sc1.Lines(startLine).Text.Trim.Length
    For i = startLine To endLine
      Dim txt = sc1.Lines(i).Text.TrimEnd(vbCr, vbLf)
      ' If txt.Trim = "" Then Continue For
      If txt.Length < insertPos Then txt = Space(insertPos) + txt
      Dim prevText = txt.Substring(0, insertPos)
      If prevText.Trim <> "" Then insertPos = 0 : prevText = ""
      txt = prevText + "'' " + txt.Substring(insertPos)
      sc1.Lines(i).Text = txt
    Next
  End Sub
  Sub uncommentBlock()
    Dim startLine = sc1.Selection.Range.StartingLine.Number, endLine = sc1.Selection.Range.EndingLine.Number
    For i = startLine To endLine
      Dim txt = sc1.Lines(i).Text.TrimEnd(vbCr, vbLf)
      Dim abPos = txt.IndexOf("'' ")
      If abPos > -1 AndAlso txt.TrimStart.StartsWith("'' ") Then
        txt = txt.Substring(0, abPos) + txt.Substring(abPos + 3)
      End If
      sc1.Lines(i).Text = txt
    Next
  End Sub


  Private Sub sc1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles sc1.KeyDown
    On Error Resume Next
    Dim keyString = getKeyString(e)
    If keyString.Length < 2 Then Exit Sub
    Debug.Print("scintilla Keydown " + keyString)

    If lstIntellisense.Visible Then
      If keyString = "UP" Then lstIntellisense.SelectedIndex -= 1 : e.Handled = True
      If keyString = "DOWN" Then lstIntellisense.SelectedIndex += 1 : e.Handled = True
      

    End If


    Select Case keyString
      Case "CTRL-S", "CTRL-ENTER", "CTRL-RETURN"
        Me.onSave()
        e.SuppressKeyPress = True

      Case "CTRL-E"
        Me.navFilelistToMe()

      Case "CTRL-R"
        Dim doReload = True
        If Dirty Then
          If MsgBox("Im aktuellen Dokument befinden sich ungespeicherte Änderungen. Diese gehen verloren, wenn du das Dokument jetzt neu lädst.", MsgBoxStyle.OkCancel Or MsgBoxStyle.Exclamation) = MsgBoxResult.Cancel Then
            doReload = False
          End If
        End If
        If doReload Then Me.onRead()

      Case "CTRL-W", "CTRL-F4"
        closeTab(Me)

      Case "F4"
        sc1.FindReplace.IncrementalSearch()

      Case "F5"
        'If p_FileType = "vbscript" Or p_FileType = "vbnet" Then
        '  If sH.globBreakMode <> "" Then
        '    'wenn sich ein Skript im Haltemodus befindet, keine neue Instanz aufrufen, sondern Fortsetzen
        '    sH.globBreakMode = ""
        '  Else
        '    autostartActiveFile()
        '  End If
        'End If

      Case "OEMPERIOD"
        'If p_FileType = "vbscript" Or p_FileType = "vbnet" Then
        'onAutoComplete()
        'End If

      Case "CTRL-UP", "CTRL-DOWN"
        If p_indexListCtrl IsNot Nothing Then
          p_indexListCtrl.onKeyHandler(e)

        End If


      Case "CTRL-OEMQUESTION", "CTRL-ALT-C"
        e.SuppressKeyPress = True
        Me.commentBlock()

      Case "CTRL-SHIFT-OEMQUESTION", "CTRL-ALT-U"
        e.SuppressKeyPress = True
        uncommentBlock()

      Case "SHIFT-OEMQUESTION"
        If sc1.Selection.Range.StartingLine.Number <> sc1.Selection.Range.EndingLine.Number Then
          e.SuppressKeyPress = True
          e.Handled = True
          uncommentBlock()
        End If

      Case "OEMQUESTION"
        If sc1.Selection.Range.StartingLine.Number <> sc1.Selection.Range.EndingLine.Number Then
          e.SuppressKeyPress = True
          e.Handled = True
          commentBlock()
        End If

        'TODO intellisense BEGIN
        'Case "CTRL-SPACE"
        '  onAutoComplete("manuell")
        '  If lstIntellisense.Visible Then
        '    searchInISList()
        '  Else
        '    showTooltip("manuell")
        '  End If

        'Case "BACK"
        '  If lstIntellisense.Visible = False Then
        '    onAutoComplete("BACK")
        '  End If
        '  searchInISList(Chr(8))

        'Case "ENTER", "RETURN", "TAB"
        '  If lstIntellisense.Visible Then
        '    insertSelItem()
        '    e.Handled = True
        '  End If
        'TODO intellisense END

      Case "F2"
        'If p_FileType = "vbscript" Or p_FileType = "vbnet" Then
        '  If helpFilePath = "" Then MsgBox("Hilfedatei nicht gefunden!", MsgBoxStyle.Critical, "Fehler") : Exit Sub

        '  Dim wordStart, wordEnd As Integer
        '  Dim line = sc1.Selection.Range.StartingLine
        '  Dim selStart As Integer = line.SelectionStartPosition - line.StartPosition
        '  For wordStart = selStart To 0 Step -1
        '    If Char.IsLetterOrDigit(line.Text.Substring(wordStart - 1, 1)) = False Then Exit For
        '  Next
        '  For wordEnd = selStart To line.Text.Length - 2
        '    If Char.IsLetterOrDigit(line.Text.Substring(wordEnd, 1)) = False Then Exit For
        '  Next

        '  Dim wordUnderCursor = line.Text.Substring(wordStart, wordEnd - wordStart)
        '  If wordUnderCursor.Length < 2 Then Exit Sub
        '  navHelpByKeyword(wordUnderCursor.ToLower)

        'End If

    End Select



    'If e.Alt And keyString <> "ALT-MENU" Then
    '  keyEventCallback(keyString)
    'End If
    'If fileKeyboardHandlers.ContainsKey(keyString) Then
    '  FileActionCallback(fileKeyboardHandlers(keyString), Me)
    'End If

    If e.KeyCode = 226 And Workbench.Instance.check_merkeZeile.Checked Then ' kleiner Zeichen - <

      Dim selStart = sc1.Selection.Start
      Dim firstCharInLine = sc1.Lines.Current.StartPosition
      If firstCharInLine = selStart Then
        e.SuppressKeyPress = True
        If e.Shift Then
          sc1.Selection.Length = 0
          Dim merkeData() As String = merkeZeileAbruf()
          For Each line In merkeData
            sc1.Selection.Text = line
            sc1.Selection.Start += sc1.Selection.Length
          Next
        Else
          merkeZeile(sc1.Lines.Current.Text)
          If e.Control Or e.Alt Then

          Else
            sc1.Commands.Execute(ScintillaNet.BindableCommand.LineDelete)
          End If
        End If
      End If

    End If

  End Sub

  'Sub keyEventCallback(ByVal keystring As String)
  '  Dim clsName As String = getIntelliPanelClass(URL)
  '  If clsName = "" Then Exit Sub
  '  Dim funcName As String = "onKeyDown"
  '  Dim ref = sH.scriptClass("lang_" + clsName)
  '  CallByName(ref, funcName, CallType.Method, keystring)
  'End Sub

  Sub onFontDialogApply(ByVal sender As Object, ByVal e As System.EventArgs)
    Throw New NotImplementedException("Nur für RTF verfügbar")
  End Sub


  Private Sub sc1_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs)

    If e.LinkText.StartsWith("\\") Then

      If e.LinkText.ToLower.StartsWith("\\note_") Or e.LinkText.ToLower.StartsWith("\\note-") _
          Or e.LinkText.ToLower.StartsWith("\\note/") Then
        Dim nam = e.LinkText.Substring(7)
        gotoNote(nam)
      Else
        Dim linkText = "mailto:/" + e.LinkText.Substring(2)
        Dim appbarFileSpec = MwRegistry.ExePath("appbar")
        If appbarFileSpec = "" Then
          MsgBox("um dieses Feature zu nutzen, musst du die Appbar installiert haben!", MsgBoxStyle.Critical)
        Else
          Process.Start(appbarFileSpec, "/mailto_protocol """ + linkText + """ frosc1Tab " + URL)
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

  Private Sub sc1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sc1.SelectionChanged
    Static lastselLine As Integer

    Dim row = sc1.Lines.Current.Number + 1
    Dim col = sc1.Selection.Start - sc1.Lines.Current.StartPosition + 1

    Try
      Dim nextChar = sc1.Text.Substring(sc1.Selection.Start, 1)
      Dim dispChar = If(Char.IsControl(nextChar), " ", nextChar)
      Workbench.Instance.tssl_nextChar.Text = " " & Asc(nextChar) & "  0x" & Hex(Asc(nextChar)) & "  " & dispChar

    Catch : Workbench.Instance.tssl_nextChar.Text = "" : End Try

    Workbench.Instance.tssl_cursorPos.Text = "" & row & " : " & col & "       Länge: " & sc1.TextLength

    If row <> lastselLine Then
      lastselLine = row


      If p_indexListCtrl IsNot Nothing Then
        p_indexListCtrl.onPositionChanged(row)

      End If


      RaiseEvent CurrentIndexLineChanged(row)
      '  '  Dim pos = mRTF.SelectionStart
      '  tbIndexList.skipNavIndexList = True
      '  For i = p_indexListCtrl.Items.Count - 1 To 0 Step -1
      '    Dim parts() = Split(p_indexListCtrl.Items(i), "|##|")
      '    If Val(parts(2)) < row Then p_indexListCtrl.SelectedIndex = i : Exit For
      '  Next
      '  tbIndexList.skipNavIndexList = False


      If lstIntellisense.Visible Then
        lstIntellisense.Hide() 'Zeilenwechsel schließt IntelliSense
      End If
      If RichTextPlus1.Visible Then
        RichTextPlus1.Hide()
      End If
    End If

    'TODO intellisense BEGIN
    ''If lstIntellisense.Visible Then
    ''  If sc1.Selection.Start < intelliSenseStartPos Then
    ''    lstIntellisense.Hide() 'Cursor vor Startpos bewegen schließt IntelliSense
    ''  End If
    ''End If

    ''If RichTextPlus1.Visible Then
    ''  showTooltip("poschanged")
    ''End If
    'TODO intellisense END

  End Sub

  'Sub onAutoComplete()
  '  On Error Resume Next
  '  Dim lineText = sc1.Lines.Current.Text
  '  Dim lineCurPos = sc1.Lines.Current.SelectionStartPosition - sc1.Lines.Current.StartPosition - 1
  '  Dim temp As String = "", ch As String, modus As Integer = 2
  '  For i = lineCurPos To 0 Step -1
  '    ch = lineText.Substring(i, 1)
  '    If ch = "=" Or ch = " " Or ch = "." Or ch = "(" Then modus -= 1
  '    If modus = 0 Then Exit For
  '    If modus = 1 Then temp = ch + temp

  '  Next
  '  Debug.Print(temp)
  '  If temp.ToLower.StartsWith("createobject(") Then
  '    readCOMComponentList()
  '    Application.DoEvents()

  '    With sc1.AutoComplete
  '      .AutomaticLengthEntered = True
  '      .StopCharacters = """"

  '      .Show(COMcomponentNames)
  '    End With
  '    Exit Sub
  '  End If
  '  If temp.ToLower.StartsWith("zz.") Then
  '    Application.DoEvents()

  '    With sc1.AutoComplete
  '      .AutomaticLengthEntered = True
  '      .FillUpCharacters = ".( "
  '      .StopCharacters = "."


  '      .Show(scriptHelperMethods("scriptIDE.cls_scriptHelper"))
  '    End With
  '    Exit Sub
  '  End If
  '  If temp.EndsWith(".") Then
  '    Dim varName = temp.Substring(0, temp.IndexOf(".")).ToLower
  '    For lineIdx = sc1.Lines.Current.Number To 0 Step -1
  '      Dim forCheck = sc1.Lines(lineIdx).Text.ToLower
  '      If forCheck.Contains(varName) And forCheck.Contains("'as") Then
  '        Application.DoEvents()

  '        With sc1.AutoComplete
  '          .AutomaticLengthEntered = True
  '          .FillUpCharacters = ".( "
  '          .StopCharacters = "."

  '          Dim dat = getNetTypeInfo(sc1.Lines(lineIdx).Text.Substring(forCheck.IndexOf("'as") + 3))
  '          Dim lines() = Split(dat, vbNewLine)
  '          .Show(lines)
  '        End With
  '      End If
  '    Next
  '    Exit Sub

  '  End If

  '  Dim typ As Type, mem As Reflection.MethodInfo

  '  For Each methodList In scriptHelperMethods
  '    For Each checkFor In methodList.Value
  '      If temp.ToLower.Contains(checkFor.ToLower) Then typ = Type.GetType(methodList.Key) : mem = typ.GetMethod(checkFor) : Exit For
  '    Next
  '  Next

  '  If typ IsNot Nothing Then

  '    Dim str As String = ""
  '    For Each para In mem.GetParameters
  '      str += If(para.IsOptional, "[" + para.Name + "]", para.Name) + ", "
  '    Next
  '    '      sc1.CallTip.Show("addTextbox(ID, XX, [labelText = ""], [labelXX = 0], [labelBgColor = ""], [x = -1], [y = -1], [noLineBreak = False])")
  '    sc1.CallTip.Show(str)

  '    Exit Sub
  '  End If

  'End Sub

  Private Sub sc1_CharAdded(ByVal sender As Object, ByVal e As ScintillaNet.CharAddedEventArgs) Handles sc1.CharAdded
    Dirty = True

  End Sub
  Private Sub sc1_TextDeleted(ByVal sender As Object, ByVal e As ScintillaNet.TextModifiedEventArgs) Handles sc1.TextDeleted
    If e.IsUserChange Then Dirty = True

  End Sub
  Private Sub sc1_TextInserted(ByVal sender As Object, ByVal e As ScintillaNet.TextModifiedEventArgs) Handles sc1.TextInserted
    If e.IsUserChange Then Dirty = True
    'If e.Text = """" Or e.Text = "." Then
    '  If p_FileType = "vbscript" Then
    '    'onAutoComplete()
    '  End If
    'End If
  End Sub


  Private Sub sc1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles sc1.KeyUp
    'If (e.KeyCode = Keys.D2 And e.Shift) Or (e.KeyCode = Keys.D8 And e.Shift) Or (e.KeyCode = Keys.OemPeriod And e.Shift = False) Or (e.KeyCode = Keys.Space And e.Shift = False) Then
    '  If p_FileType = "vbscript" Then
    '    onAutoComplete()
    '  End If
    '  If p_FileType = "vbnet" Then
    '    onAutoComplete()
    '  End If
    'End If
  End Sub

  Private Sub sc1_MarginClick(ByVal sender As Object, ByVal e As ScintillaNet.MarginClickEventArgs) Handles sc1.MarginClick
    On Error Resume Next
    If e.Margin.Number = 0 Then
      Dim line As ScintillaNet.Line = e.Line
      Dim forCheck As String = line.Text.Trim.ToUpper
      While forCheck = ""
        line = sc1.Lines(line.Number + 1)
        If line.Number > sc1.Lines.Count Then Exit Sub
        forCheck = line.Text.Trim.ToUpper
      End While

      If forCheck.StartsWith("SELECT CASE") Or forCheck.StartsWith("CASE ") _
         Or forCheck.StartsWith("SUB ") Or forCheck.StartsWith("FUNCTION ") Or forCheck.StartsWith("PROPERTY ") _
         Or forCheck.StartsWith("END SUB") Or forCheck.StartsWith("END FUNCTION") Or forCheck.StartsWith("END PROPERTY") Then
        MsgBox("In Zeilen mit Select-Case, Case, Sub, Function, Property sowie den zugehörigen End-Anweisungen können keine Breakpoints gesetzt werden.", MsgBoxStyle.Information)
        Exit Sub
      End If
      If forCheck.StartsWith(":") Then
        MsgBox("In dieser Zeile kann kein Breakpoint gesetzt werden, da die Codeinjection (zur Optimierung?) abgeschaltet ist." + vbNewLine + vbNewLine + "Entferne den Doppelpunkt am Zeilenanfang, um Breakpoints zuzulassen.", MsgBoxStyle.Information)
        Exit Sub
      End If

      setLineMarker(line.Number, 4, My.Resources.breakpoint, Color.Transparent, Color.Red, False, True)

      cls_IDEHelper.GetSingleton.OnBreakPointSet(Me.URL, line.Number + 1, (line.GetMarkerMask() And 16) = 16)

    End If
  End Sub

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub

  Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
    onSave()
  End Sub

  Public Function getCurrentLineNumber() As Integer Implements Core.IDockContentForm.getCurrentLineNumber
    Return sc1.Lines.Current.Number
  End Function

  Public Function getLines() As String() Implements Core.IDockContentForm.getLines
    Return Split(sc1.Text, vbNewLine)
  End Function

  Private Sub RichTextPlus1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextPlus1.Click
    RichTextPlus1.Hide()
  End Sub

  Private Sub sc1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sc1.MouseClick
    '  RichTextPlus1.Hide()
    '  lstIntellisense.Hide()
  End Sub

  Private Sub RichTextPlus1_ContentsResized(ByVal sender As Object, ByVal e As System.Windows.Forms.ContentsResizedEventArgs) Handles RichTextPlus1.ContentsResized
    RichTextPlus1.Width = e.NewRectangle.Width + 10
    RichTextPlus1.Height = e.NewRectangle.Height + 10

  End Sub

  Private Sub RichTextPlus1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextPlus1.TextChanged

  End Sub

  Private Sub lstIntellisense_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstIntellisense.GotFocus
    sc1.Focus()
  End Sub


End Class