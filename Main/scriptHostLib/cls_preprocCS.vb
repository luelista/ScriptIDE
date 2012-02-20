Public Class cls_preprocCS
  Implements IScriptPrecompiler


  Private _host As IScriptClassHost
  Public Property HostSite() As IScriptClassHost Implements IScriptPrecompiler.HostSite
    Get
      Return _host
    End Get
    Set(ByVal value As IScriptClassHost)
      _host = value
    End Set
  End Property

  Private Shared p_inst As New cls_preprocCS
  Public Shared ReadOnly Property Instance() As cls_preprocCS
    Get
      Return p_inst
    End Get
  End Property
  Private Sub New()
  End Sub

  Private _releaseMode As Boolean
  Public Property IsReleaseMode() As Boolean Implements IScriptPrecompiler.IsReleaseMode
    Get
      Return _releaseMode
    End Get
    Set(ByVal value As Boolean)
      _releaseMode = value
    End Set
  End Property

  Public Sub scriptPreproc(ByRef scriptCode As String, ByVal myimports As System.Collections.Generic.List(Of String), ByVal className As String, ByRef lineCount As Integer) Implements IScriptPrecompiler.scriptPreproc
    Dim LINES() = Split(scriptCode, vbNewLine)

    Dim forCheck As String, result As String
    For i = 0 To LINES.Length - 1
      scriptLineModify(i + 1, LINES(i), result)
      LINES(i) = result

      forCheck = LINES(i).Trim.ToUpper
      If forCheck.StartsWith("#LANGUAGE") Or forCheck.StartsWith("#REFERENCE") Or forCheck.StartsWith("#INCLUDE") Or forCheck.StartsWith("#INCLUDEFILE") Or forCheck.StartsWith("#IMPORTS") Or forCheck.StartsWith("#PARA") Then
        LINES(i) = "// verarbeitet: " + LINES(i)
      End If
    Next

    Dim sb As New System.Text.StringBuilder
    Dim lineCounter As Integer = 0
    'sb.AppendLine("Option Explicit") : lineCounter += 1
    For i = 0 To myimports.Count - 1 'imports
      sb.AppendLine("using " + myimports(i) + ";") : lineCounter += 1
    Next
    Dim sr = IO.File.OpenText(ParaService.SettingsFolder + "scriptClass\csharpPrefix.txt")
    For lineCounter = lineCounter To 199
      If Not sr.EndOfStream Then sb.AppendLine(sr.ReadLine()) Else sb.AppendLine()
    Next
    sr.Close()
    'sb.AppendLine(IO.File.ReadAllText(settingsFolder + "scriptClass\vbnetPrefix.txt"))
    sb.AppendLine(Join(LINES, vbNewLine))
    sb.AppendLine("} //EndClass")
    sb.AppendLine("} //EndNamespace")
    lineCount = LINES.Length + 50 'aufschlag ...

    scriptCode = sb.ToString.Replace("{ScriptClass}", className)
  End Sub

  Function getMainModuleForCompile(ByVal className As String) As String Implements IScriptPrecompiler.getMainModuleForCompile
    Dim scriptCode = IO.File.ReadAllText(ParaService.SettingsFolder + "scriptClass\csharpMain.txt")
    scriptCode = scriptCode.Replace("{ScriptClass}", className)
    Return scriptCode
  End Function



  Dim subFuncTyp As String, isUnterstrich As Boolean, insertOnError As Boolean

  Sub scriptLineModify(ByVal lineNr As Integer, ByVal sourceLine As String, ByRef rResultLine As String)
    Dim abPos, abPos2 As Integer
    Dim isSubFunc, EOsubFunc As Boolean
    Dim subFuncName As String
    Dim isElseStatement, isIfStatement, isEndIfStatement, isCaseStatement, isForNext, isDoppelpunkt As Boolean

    'kommentare vernichten
    abPos = sourceLine.IndexOf("//")
    If abPos > -1 Then
      Dim matches = System.Text.RegularExpressions.Regex.Matches(sourceLine, "(?<!"")"".*?(?<!"")""|'.*")

      For Each match As System.Text.RegularExpressions.Match In matches
        If match.Value.StartsWith("//") Then sourceLine = sourceLine.Substring(0, match.Index) : Exit For
      Next
    End If

    Dim sourceLineNetto2 = sourceLine.Trim
    If sourceLineNetto2 = "" Then rResultLine = sourceLine : Exit Sub

    Dim sourceLineNetto = sourceLineNetto2.ToUpper
    sourceLineNetto = sourceLineNetto.Replace("PRIVATE", "")
    sourceLineNetto = sourceLineNetto.Replace("PROTECTED", "")
    sourceLineNetto = sourceLineNetto.Replace("FRIEND", "")
    sourceLineNetto = sourceLineNetto.Replace("PUBLIC", "")
    sourceLineNetto = sourceLineNetto.Replace("PARTIAL", "")
    sourceLineNetto = sourceLineNetto.Replace("SHADOWS", "")
    sourceLineNetto = sourceLineNetto.Replace("OVERLOADS", "")
    sourceLineNetto = sourceLineNetto.Replace("OVERRIDABLE", "")
    sourceLineNetto = sourceLineNetto.Replace("OVERRIDES", "")
    sourceLineNetto = sourceLineNetto.Replace("MUSTOVERRIDE", "")
    sourceLineNetto = sourceLineNetto.Replace("DEFAULT", "")
    sourceLineNetto = sourceLineNetto.Replace("SHARED", "")
    sourceLineNetto = sourceLineNetto.Replace("READONLY", "")
    sourceLineNetto = sourceLineNetto.Replace("WRITEONLY", "")

    Dim firstWord As String = ""
    firstWord = sourceLineNetto.Substring(0, (sourceLineNetto + " ").IndexOf(" "))

    If sourceLineNetto.StartsWith(":") Then isDoppelpunkt = True : sourceLineNetto = sourceLineNetto.Substring(1)

    Select Case firstWord
      Case "SUB", "FUNCTION", "PROPERTY"
        isSubFunc = True
        EOsubFunc = False
        subFuncTyp = firstWord
        abPos = (sourceLineNetto2 + " ").IndexOf(" ", firstWord.Length)
        abPos2 = (sourceLineNetto2 + "(").IndexOf("(", firstWord.Length)
        If abPos2 < abPos Then abPos = abPos2
        subFuncName = sourceLineNetto2.Substring(abPos, abPos2 - abPos)
      Case "END"
        If sourceLineNetto.StartsWith("END SUB") Or sourceLineNetto.StartsWith("END FUNCTION") Or sourceLineNetto.StartsWith("END PROPERTY") Then
          EOsubFunc = True : subFuncTyp = ""
        End If
        If sourceLineNetto.StartsWith("END IF") Then
          isEndIfStatement = True
        End If
        If sourceLineNetto.StartsWith("END SELECT") Then
          isCaseStatement = True
        End If

      Case "IF"
        isIfStatement = True

      Case "ELSE"
        isElseStatement = True

      Case "CASE"
        isCaseStatement = True

      Case "FOR"
        isForNext = True

      Case "SELECT"
        If sourceLineNetto.StartsWith("SELECT CASE") Then
          isCaseStatement = True
        End If

      Case "TRACE"
        sourceLine = Replace(sourceLine, "trace ", "ZZ.trace (zLN,zNN,", , , CompareMethod.Text) + ")"
      Case "PRINTLINE"
        sourceLine = Replace(sourceLine, "printLine ", "ZZ.printLine (zLN,zNN,", , , CompareMethod.Text) + ")"
    End Select


    Dim insertVorne = "zLN=" & lineNr.ToString & ":zC=zC+1:"
    Dim insertHinten As String
    insertHinten = ":If Err.number<>0 Or zC>zC2 Or zBB(zLN) Then ZZ.CB(zLN,zNN,zC,zC2,Err,ziI,ziE,ziQ,zBB(zLN)):if ziQ Then Exit " & subFuncTyp '& ":End If"
    'insertHinten = ":If Err.number<>0 Or zC>zC2  Then For ziI=0 To 1:ZZ.CB zLN,zNN,zC,zC2,Err,ziI,ziE,ziQ:If ziE<>"""" Then: Execute(ziE):End If:Next:if ziQ Then ZZ.stackPop:Exit " & subFuncTyp '& ":End If"
    If firstWord = "STOP" Then
      sourceLine = ""
      insertHinten = ":If true Then ZZ.CB(zLN,zNN,zC,zC2,Err,ziI,ziE,ziQ,true):if ziQ Then Exit " & subFuncTyp '& ":End If"
    End If

    If insertOnError Then insertOnError = False : insertVorne = "On Error Resume Next:zNN=" + apo + subFuncName + apo + ":" + insertVorne
    If isSubFunc Then insertVorne = "" : insertHinten = "" : insertOnError = True

    If subFuncTyp = "" Then insertVorne = "" : insertHinten = ""
    If EOsubFunc Then insertVorne = "" : insertHinten = "" 'ZZ.stackPop():
    If isCaseStatement Then insertVorne = "" : insertHinten = ""
    If isElseStatement Then insertVorne = ""
    If isEndIfStatement Then insertVorne = ""
    If isIfStatement Then insertHinten = ""
    If isDoppelpunkt Then insertVorne = "" : insertHinten = ""
    If isUnterstrich Then insertVorne = "" : isUnterstrich = False
    If sourceLineNetto.EndsWith(" _") Then insertHinten = "" : isUnterstrich = True


    Dim temp = insertVorne + sourceLine
    Dim lg = temp.Length
    If lg < 60 Then
      temp = temp + Space(60 - lg)
    End If

    rResultLine = temp + insertHinten
  End Sub

End Class
