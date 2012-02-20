<Microsoft.VisualBasic.ComClass()> Public Class ScriptHelper_syntaxHighlight
  Private sc As ScintillaNet.Scintilla
  Public ReadOnly Property scRef() As Object
    Get
      Return sc
    End Get
  End Property
  Public Sub setKeywords(ByVal id, ByVal words)
    sc.Lexing.SetKeywords(id, words)
  End Sub
  

  Sub resetStyles(ByVal setDefault As Boolean)
    If setDefault Then
      sc.Styles.ResetDefault()
    Else
      sc.Styles.Reset()
    End If

  End Sub

  Public Sub New(ByVal p_scRef)
    sc = p_scRef
  End Sub
End Class
Module app_syntaxHighlight
  Public Sub setSCStyle(ByVal sc As ScintillaNet.Scintilla, ByVal styleID As Integer, ByVal fore As String, ByVal back As String, Optional ByVal txtStyle As String = "")
    sc.Styles.Item(styleID).ForeColor = ColorTranslator.FromHtml(fore)
    sc.Styles.Item(styleID).BackColor = ColorTranslator.FromHtml(back)

    If Not String.IsNullOrEmpty(txtStyle) Then
      DirectCast(sc, ScintillaNet.INativeScintilla).StyleSetBold(styleID, txtStyle.Contains("bold"))
      DirectCast(sc, ScintillaNet.INativeScintilla).StyleSetItalic(styleID, txtStyle.Contains("italic"))
      DirectCast(sc, ScintillaNet.INativeScintilla).StyleSetEOLFilled(styleID, txtStyle.Contains("eol"))
      DirectCast(sc, ScintillaNet.INativeScintilla).StyleSetUnderline(styleID, txtStyle.Contains("underline"))
    End If
  End Sub

  Sub initScintillaHighlighter(ByVal mrtf As ScintillaNet.Scintilla, ByVal fileExt As String)
    Dim fileName As String = "<not found>"
    Try
      fileExt = IO.Path.GetExtension(fileExt).ToUpper
      mrtf.ConfigurationManager.Language = ""

      If fileAssocTab.ContainsKey(fileExt) Then
        fileName = ParaService.SettingsFolder + "scintillaConfig\" + fileAssocTab(fileExt) + ".xml"
        Dim xr As New Xml.XmlTextReader(fileName)

        While xr.Read
          If xr.IsStartElement() = False Then Continue While
          Select Case xr.LocalName.ToLower
            Case "scintillalanguageconfig" 'nothing ...

            Case "language" : mrtf.ConfigurationManager.Language = xr.ReadString().Trim

            Case "lexername" : mrtf.Lexing.LexerName = xr.ReadString().Trim

            Case "scstyle"
              If xr.AttributeCount < 3 Or xr.AttributeCount > 4 Then Throw New Exception("The SCStyle tag must have three or four attributes")
              Dim id As Integer
              If Not Integer.TryParse(xr.GetAttribute("id"), id) Then Throw New Exception("The SCStyle tag must have an numeric id attribute")

              setSCStyle(mrtf, id, xr.GetAttribute("fgcolor"), xr.GetAttribute("bgcolor"), xr.GetAttribute("style"))

            Case "keywords"
              If xr.AttributeCount <> 1 Then Throw New Exception("The Keywords tag must have ONE attribute")
              Dim id As Integer
              If Not Integer.TryParse(xr.GetAttribute("id"), id) Then Throw New Exception("The Keywords tag must have an numeric id attribute")
              Dim cont = Trim(xr.ReadString)
              cont = cont.Replace(vbCr, " ").Replace(vbLf, " ")
              mrtf.Lexing.SetKeywords(id, cont)

            Case "margin"
              If xr.AttributeCount <> 2 Then Throw New Exception("The margin tag must have TWO attributes.")
              Dim id As Integer
              If Not Integer.TryParse(xr.GetAttribute("id"), id) Then Throw New Exception("The Keywords tag must have an numeric id attribute")
              Dim width As Integer
              If Not Integer.TryParse(xr.GetAttribute("width"), width) Then Throw New Exception("The Keywords tag must have an numeric width attribute")
              mrtf.Margins(id).Width = width

            Case Else
              Throw New IO.InvalidDataException("At this point shouldn't be a " + xr.Name + " Element")
          End Select
        End While

        xr.Close()
      End If

      mrtf.Lexing.Colorize()

    Catch ex As Exception
      MsgBox("Beim Konfigurieren des Syntax-Highlighters für den Dateityp """ + fileExt + """ ist ein Fehler aufgetreten. Bitte überprüfe die XML-Datei auf korrekte Syntax. Weitere Informationen zu dieser Ausnahme wurden in den Trace-Monitor geschrieben." + vbNewLine + vbNewLine + "XML-Datei: " + fileName + vbNewLine + "Ausnahme: " + ex.Message, MsgBoxStyle.Exclamation, "Syntax-Highlighting")
      TT.DumpException("syntaxHighlight", ex)
    End Try
  End Sub
  'Sub initScintillaHighlighter(ByVal mrtf As ScintillaNet.Scintilla, ByVal fileExt As String)
  '  fileExt = IO.Path.GetExtension(fileExt).ToUpper
  '  Dim scriptName As String

  '  mrtf.ConfigurationManager.Language = ""
  '  If fileAssocTab.ContainsKey(fileExt) Then
  '    'scriptName = settingsFolder+"languageScripts\" + fileAssocTab(fileExt) + ".vbs"
  '    'Dim scriptText = IO.File.ReadAllText(scriptName)
  '    'scriptControl.Reset()
  '    Dim hlp As Object = New ScriptHelper_syntaxHighlight(mrtf)
  '    'executeScriptFunction(scriptName, "syntaxHighlight", False, hlp)

  '    Dim fileName = "lang_" + fileAssocTab(fileExt)

  '    'TODO: syntaxHighlight anders lösen ...
  '    'With sh.scriptClass(fileName)
  '    '  .syntaxHighlight(hlp)
  '    'End With
  '  End If


  '  mrtf.Lexing.Colorize()
  'End Sub

  'Sub syntaxHighlight_vbs(ByVal mrtf As ScintillaNet.Scintilla)
  '  mrtf.ConfigurationManager.Language = "vbscript"
  'End Sub
  'Sub syntaxHighlight_perl(ByVal mrtf As ScintillaNet.Scintilla)
  '  mrtf.ConfigurationManager.Language = "cs"
  '  mrtf.Lexing.LexerName = "perl"
  'End Sub
  'Sub syntaxHighlight_html(ByVal mrtf As ScintillaNet.Scintilla)
  '  mrtf.ConfigurationManager.Language = "html"
  '  ''
  '  'mrtf.Lexing.LoadLexerLibrary("D:\_Downloads\wscite201\wscite\html.properties")
  '  'Dim i As ScintillaNet.INativeScintilla
  '  'i = mrtf
  '  'i.LoadLexerLibrary("D:\_Downloads\wscite201\wscite\html.properties")
  '  Debug.Print(mrtf.Lexing.LexerName)
  '  Debug.Print(mrtf.ConfigurationManager.Language)

  '  ' HTML
  '  setSCStyle(mrtf, 0, "#000000", "#FFFFFF") 'txt
  '  setSCStyle(mrtf, 1, "#86016F", "#FFFFFF", "bold") 'known tag 790069#
  '  setSCStyle(mrtf, 2, "#FF00FF", "#FFFFFF", "bold") 'unknown tag
  '  setSCStyle(mrtf, 3, "#000000", "#FFFFFF", "bold") 'attribut
  '  setSCStyle(mrtf, 4, "#FF0000", "#FFFFFF", "bold") 'unknown attr
  '  'setSCStyle(mrtf, 5, "#000033", "#FFFFFF") 'num
  '  setSCStyle(mrtf, 6, "#0C0C80", "#FFFFFF") 'double quot str 0C0C80
  '  setSCStyle(mrtf, 7, "#0C0C80", "#FFFFFF") 'single quot str
  '  setSCStyle(mrtf, 8, "#000000", "#FFFFFF") 'other txt inside tag
  '  setSCStyle(mrtf, 9, "#666666", "#EFEFEF", "italic") 'kom
  '  setSCStyle(mrtf, 10, "#FF0000", "#FFFFFF") 'entity
  '  setSCStyle(mrtf, 11, "#990080", "#FFFFFF") 'xml tag end />


  '  ' JAVASCRIPT
  '  setSCStyle(mrtf, 41, "#000000", "#F3F3FF", "eol") 'script
  '  setSCStyle(mrtf, 42, "#444444", "#EFEFEF", "italic") 'kom
  '  setSCStyle(mrtf, 43, "#444444", "#EFEFEF", "italic") 'kom
  '  setSCStyle(mrtf, 44, "#444444", "#EFEFEF", "italic") 'kom
  '  setSCStyle(mrtf, 45, "#007F7F", "#F3F3FF") 'num
  '  setSCStyle(mrtf, 46, "#000000", "#F3F3FF") 'word
  '  setSCStyle(mrtf, 47, "#000000", "#F3F3FF", "bold") 'keyword
  '  setSCStyle(mrtf, 48, "#790000", "#F3F3FF") 'double quot str
  '  setSCStyle(mrtf, 49, "#790000", "#F3F3FF") 'single quot str
  '  setSCStyle(mrtf, 50, "#000000", "#F3F3FF") 'symbol
  '  setSCStyle(mrtf, 51, "#000000", "#F3F3FF") 'EndOfLine
  '  setSCStyle(mrtf, 52, "#000000", "#FFBBB0") 'Regex
  '  'setSCStyle(mrtf, 40, "#000000", "#cccccc") 'script




  '  ' PHP farben
  '  setSCStyle(mrtf, 18, "#000033", "#FFEEAA", "eol")
  '  setSCStyle(mrtf, 118, "#000033", "#FFFFFF")
  '  setSCStyle(mrtf, 119, "#008300", "#FFFFFF")
  '  setSCStyle(mrtf, 120, "#008300", "#FFFFFF")
  '  setSCStyle(mrtf, 121, "#000000", "#FFFFFF", "bold") 'keyword
  '  setSCStyle(mrtf, 122, "#CC9900", "#FFFFFF", "bold")
  '  setSCStyle(mrtf, 123, "#17179E", "#FFFFFF") 'var
  '  setSCStyle(mrtf, 124, "#666666", "#EFEFEF", "italic") 'kom
  '  setSCStyle(mrtf, 125, "#666666", "#EFEFEF", "italic") 'kom
  '  setSCStyle(mrtf, 126, "#17478E", "#FFFFFF") 'var in string
  '  setSCStyle(mrtf, 127, "#000000", "#FFFFFF")
  'End Sub

  'Sub syntaxHighlight_js(ByVal mrtf As ScintillaNet.Scintilla)
  '  mrtf.ConfigurationManager.Language = "js"
  '  ''
  '  'mrtf.Lexing.LoadLexerLibrary("D:\_Downloads\wscite201\wscite\html.properties")
  '  'Dim i As ScintillaNet.INativeScintilla
  '  'i = mrtf
  '  'i.LoadLexerLibrary("D:\_Downloads\wscite201\wscite\html.properties")
  '  Debug.Print(mrtf.Lexing.LexerName)
  '  Debug.Print(mrtf.ConfigurationManager.Language)

  '  For i = 0 To 100
  '    Debug.Print(i & ": " & ColorTranslator.ToHtml(mrtf.Styles.Item(i).ForeColor))
  '  Next

  '  ' JAVASCRIPT
  '  setSCStyle(mrtf, 6, "#790000", "#FFFFFF") 'double quot str
  '  setSCStyle(mrtf, 7, "#790000", "#FFFFFF") 'single quot str
  '  setSCStyle(mrtf, 1, "#666666", "#EFEFEF", "italic") 'kom
  '  setSCStyle(mrtf, 2, "#666666", "#EFEFEF", "italic") 'kom
  '  setSCStyle(mrtf, 3, "#666666", "#EFEFEF", "italic") 'kom
  '  setSCStyle(mrtf, 14, "#000000", "#FFBBB0") 'Regex
  '  setSCStyle(mrtf, 10, "#007F7F", "#FFFFFF", "bold") 'symbole



  'End Sub


End Module
