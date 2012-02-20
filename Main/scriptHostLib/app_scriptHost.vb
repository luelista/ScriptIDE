Module app_scriptHost
  'Public WithEvents scriptControl As New MSScriptControl.ScriptControl
  Public scriptClassDict As New Dictionary(Of String, IScriptClassHost)
  Public scriptClassFolder As String = ZZ.IDEHelper.GetSettingsFolder() + "scriptClass\"
  Public traceStack As New Stack(Of String)

  Public dumpStackToTrace As Boolean

  Public scriptClassSearchCache As New Dictionary(Of String, String)


  Sub moveScriptRunningLabelSpace()
    Static i As Integer = 0
    i = (i + 1) Mod 10
    MAIN.Text = "ScriptHostLib   " + If(i = 0, " ", "|") + " " + If(i = 1, " ", "|") + " " + If(i = 2, " ", "|") + " " + If(i = 3, " ", "|") + " " + If(i = 4, " ", "|") + " " + If(i = 5, " ", "|") + " " + If(i = 6, " ", "|") + " " + If(i = 7, " ", "|") + " " + If(i = 8, " ", "|") + " " + If(i = 9, " ", "|")
  End Sub

  'Function scriptClass(ByVal className As String) As Object
  '  Dim scriptFile = ScriptHost.Instance.expandScriptClassName(className)
  '  If String.IsNullOrEmpty(scriptFile) Then Return Nothing

  '  scriptClassSearchCache(className.ToLower) = scriptFile

  '  Dim sc As IScriptClassHost
  '  If scriptClassDict.TryGetValue(scriptFile.ToUpper, sc) Then
  '    If sc.checkForNewerFile() = False Then
  '      Return sc.getClassRef()
  '    End If
  '    scriptClassDict.Remove(scriptFile.ToUpper)
  '    sc.terminateScript()
  '  End If

  '  ' Dim sc As IScriptClassHost
  '  If scriptFile.EndsWith(".vbs") Then
  '    sc = New scHostVBS(scriptFile)
  '  Else
  '    sc = New scHostNET(scriptFile)
  '  End If
  '  sc.initScriptHost()
  '  If sc.isIniDone() Then
  '    scriptClassDict.Add(scriptFile.ToUpper, sc)
  '    Return sc.getClassRef()
  '  Else
  '    Return Nothing
  '  End If
  'End Function




  ''' <remarks>
  ''' ACHTUNG: NUR LAUFZEITFEHLER HIER MELDEN!!!!
  ''' </remarks>
  Sub onScriptError(ByVal errFile As String, ByVal errLine As Integer, ByVal text As String, Optional ByVal beSilent As Boolean = False)

    Dim className = IO.Path.GetFileNameWithoutExtension(errFile)
    Dim codeLink = "_|°|_cLink_|°|_" + "scriptClass" & "_|°|_" & errFile & "?" & errLine & "_|°|_"
    Dim mode = "err"

    'If ScriptHost.Instance.SilentMode = False And beSilent = False Then mode = "err,highlight"
    TT.Write("Line " & errLine & ": ", text, mode, codeLink)



    ' "Function: " & funcName '& vbNewLine & _
    ' vbNewLine & ex.ToString
    If isIDEMode And ScriptHost.Instance.SilentMode = False And beSilent = False Then
      'ScriptHost.Instance.OnHighlightLineRequested(className, "", errLine, HighlightLineReason.RuntimeError)

      ScriptHost.Instance.ActivateInformationWindow("trace/printLine")
      'Dim sc As frmDC_scintilla = gotoNote("loc:/" + errFile)
      'sc.RTF.GoTo.Line(errLine - 1)
      'If Not dontSetMarker Then sc.highlightErrorLine(errLine - 1)
    End If
  End Sub
  Sub onScriptError2(ByVal er As MSScriptControl.Error, ByVal fileName As String, Optional ByVal callStack As String = "")
    Dim errLine = er.Line - 200, errCol = er.Column
    Dim errText = "Line " & errLine & ", Col " & errCol & vbNewLine & _
                              er.Description & vbNewLine & _
                              er.Text & vbNewLine & _
                              "Filename: " & fileName & vbNewLine & callStack
    onScriptError(fileName, errLine, errText)
  End Sub


End Module
