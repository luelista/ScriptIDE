Imports System.Runtime.InteropServices

<FiletypeHandler(New String() {".hex", ".exe", ".dll"}, False)> _
Public Class frmDC_hexEdit
  Implements IDockContentForm

  Private p_parameters As New Hashtable
  Public ReadOnly Property Parameters() As Hashtable Implements IDockContentForm.Parameters
    Get
      Return p_parameters
    End Get
  End Property

  Public Overrides Function GetPersistString() As String
    Return "HexEditor|##|[.hex]" + Me.getFileTag()
  End Function

  Private Sub frmDC_rtf_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

  End Sub

  Private Sub frmDC_rtf_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    If e.CloseReason = CloseReason.MdiFormClosing Then Exit Sub
    IDE.ContentHelper._internalCloseTab(Me, e.Cancel)
    DirectCast(HexBox1.ByteProvider, IDisposable).Dispose()

    HexBox1.ByteProvider = Nothing

  End Sub

  Private Sub frmDC_rtf_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    If IDE.Glob.para("siaHexEdit__HexCase", "Upper") = "Lower" Then _
      HexBox1.HexCasing = Be.Windows.Forms.HexCasing.Lower
    HexBox1.LineInfoForeColor = ColorTranslator.FromHtml(IDE.Glob.para("siaHexEdit__LnColor", "#7A7A7A"))
    HexBox1.LineInfoVisible = IDE.Glob.para("siaHexEdit__showLN", "TRUE") = "TRUE"
    HexBox1.StringViewVisible = IDE.Glob.para("siaHexEdit__showAscii", "TRUE") = "TRUE"
    HexBox1.UseFixedBytesPerLine = IDE.Glob.para("siaHexEdit__showAscii", "TRUE") = "TRUE"
    Integer.TryParse(IDE.Glob.para("siaHexEdit__ByteCount", "16"), HexBox1.BytesPerLine)

  End Sub


  Private p_tabRowKey As String
  'Public tabButton As Button
  Dim m_isDirty As Boolean
  Dim lastselLine As Integer = -1
  Private p_isLazy As Boolean
  Private p_indexListCtrl As ListBox
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
    Return "$" + IO.Path.GetFileName(URL)
  End Function
  Function getIcon() As Icon() Implements IDockContentForm.getIcon
    'Dim ext = IO.Path.GetExtension(Filename)
    Return FileTypeAndIcon.RegisteredFileType.GetFileIconByExt(".bin")
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
      Return HexBox1
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
    'IDE.ProtocolManager.SaveFile(Me.URL, mRTF.Rtf)
    ''isBUSY = False
    'Dirty = False
    'If IDE.getActiveTab() Is Me Then createIndexList()
  End Sub

  Sub onRead() Implements IDockContentForm.onRead
    Try
      HexBox1.ReadOnly = True
      HexBox1.ByteProvider = New FriendlyFileByteProvider(ProtocolService.MapToLocalFilename(Me.URL))
    Catch ex As Exception
      MsgBox("Fehler beim Einlesen ..." + vbNewLine + "evtl. neue Datei" + vbNewLine + vbNewLine + ex.ToString)
    End Try
end_of_function:
    Dirty = False
    p_isLazy = False
  End Sub

  Function getLineContent(ByVal lineNumber As Integer) As String Implements IDockContentForm.getLineContent

  End Function
  Public Function getLineCount() As Integer Implements IDockContentForm.getLineCount
    ' Return mRTF.Lines.Length
  End Function

  Function getActLineContent() As String Implements IDockContentForm.getActLineContent
    'Dim selStart = mRTF.SelectionStart
    'Dim lineIndex = mRTF.GetLineFromCharIndex(selStart)
    'Return getLineContent(lineIndex)
  End Function
  Sub selectCurLine()
    'Dim selStart = mRTF.SelectionStart
    'Dim lineIndex = mRTF.GetLineFromCharIndex(selStart)
    'Dim firstCharCurLine = mRTF.GetFirstCharIndexOfCurrentLine
    'mRTF.Select(firstCharCurLine, mRTF.GetFirstCharIndexFromLine(lineIndex + 1) - firstCharCurLine)
  End Sub

  Function onCheckDirty(Optional ByVal beforeWhat As String = "vor dem Schließen ") As Boolean Implements IDockContentForm.onCheckDirty
    'If Dirty Then
    '  Me.Activate()
    '  Select Case MsgBox("Im Dokument " + Me.URL + " befinden sich ungespeicherte Änderungen. Soll es vor dem " + beforeWhat + "gespeichert werden?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel, "scriptIDE - Datei speichern?")
    '    Case MsgBoxResult.Yes
    '      Me.onSave()
    '    Case MsgBoxResult.No
    '    Case MsgBoxResult.Cancel
    '      Return False
    '  End Select
    'End If
    Return True
  End Function

  Function getCharCountFromStart(ByVal text As String, ByVal c As Char) As Integer
    'Dim idx = 0
    'Do While text.Substring(idx, 1) = c
    '  getCharCountFromStart += 1
    'Loop
    'Return idx
  End Function


  Sub createIndexList() Implements IDockContentForm.createIndexList
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


  Private Sub mrtf_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    'Dim keyString = getKeyString(e)
    'Debug.Print("rtf Keydown " + keyString)

    If e.Control Then
      Select Case e.KeyCode

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

        Case Keys.E
          ProtocolService.NavigateFilelistToURL(URL)
      End Select
    End If

  End Sub

  Public Sub onLazyInit() Implements IDockContentForm.onLazyInit

  End Sub

  Public Function getCurrentLineNumber() As Integer Implements ScriptIDE.Core.IDockContentForm.getCurrentLineNumber
    Return 0
  End Function

  Public Function getLines() As String() Implements ScriptIDE.Core.IDockContentForm.getLines
    Return New String() {""}
  End Function

  Public Event CurrentIndexLineChanged(ByVal lineNr As Integer) Implements ScriptIDE.Core.IDockContentForm.CurrentIndexLineChanged

End Class