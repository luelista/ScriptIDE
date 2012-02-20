Public Class scHostVBS
  Implements IScriptClassHost

  Public WithEvents scHost As New MSScriptControl.ScriptControl

  Dim mClassName, mScriptClassName As String
  Dim mfileSpec As String
  Dim mlastModified As Date
  Dim mref As Object
  Dim miniDone As Boolean
  Dim mscriptHelper As Object
  Dim mlineCount As Integer

  Property className() As String Implements IScriptClassHost.className
    Get
      Return mClassName
    End Get
    Protected Set(ByVal value As String)
      mClassName = value
      mScriptClassName = System.Text.RegularExpressions.Regex.Replace(value, "[^a-zA-Z0-9_]", "_")
    End Set
  End Property
  Public ReadOnly Property assemblyRef() As System.Reflection.Assembly Implements IScriptClassHost.assemblyRef
    Get
      Return Nothing
    End Get
  End Property
  ReadOnly Property scriptClassName() As String Implements IScriptClassHost.scriptClassName
    Get
      Return mScriptClassName
    End Get
  End Property
  Public Property debugMode() As RuntimeMode Implements IScriptClassHost.debugMode
    Get
      Return RuntimeMode.IDE
    End Get
    Set(ByVal value As RuntimeMode)
      Throw New NotImplementedException("DebugMode cannot be set for VBS")
    End Set
  End Property
  Public Function targetFolder() As String Implements IScriptClassHost.targetFolder
    Return ParaService.SettingsFolder + "scriptClass\"
  End Function
  ReadOnly Property scriptPara() As ScriptParameters Implements IScriptClassHost.scriptPara
    Get
      'Return mpara
    End Get
  End Property
  ReadOnly Property fileSpec() As String Implements IScriptClassHost.fileSpec
    Get
      Return mfileSpec
    End Get
  End Property
  ReadOnly Property lastModified() As Date Implements IScriptClassHost.lastModified
    Get
      Return mlastModified
    End Get
  End Property
  Function getClassRef() As Object Implements IScriptClassHost.getClassRef
    Return mref
  End Function
  Function getScriptHelper() As Object Implements IScriptClassHost.getScriptHelper
    Return mscriptHelper
  End Function
  Function isIniDone() As Boolean Implements IScriptClassHost.isIniDone
    Return miniDone
  End Function

  Sub initScriptHost() Implements IScriptClassHost.initScriptHost
    mfileSpec = ParaService.SettingsFolder + "scriptClass\" + className + ".vbs"

    scHost.Language = "VBScript"
    scHost.Reset()

    Dim scriptText = IO.File.ReadAllText(mfileSpec)
    mlastModified = IO.File.GetLastWriteTime(mfileSpec)

    mscriptHelper = New cls_scriptHelper
    mscriptHelper._scriptClassName = className
    scHost.AddObject("ZZ", mscriptHelper)
    scHost.Timeout = 1000000
    Dim hlp2 As Object = New cls_scriptHelperGlobalScope(className)
    scHost.AddObject("cls_scriptHelperGlobalScope", hlp2, True)

    scriptPreproc(scriptText)
    IO.File.WriteAllText(ParaService.SettingsFolder + "script-after-preProc.txt", scriptText)
    Try
      scHost.AddCode(scriptText)
      mref = scHost.Eval("New zz_scriptClass")
      mref.zz_BBreset(mlineCount)
      miniDone = True
    Catch ex As Exception
      'onScriptError(scHost.Error, mfileSpec)
      miniDone = False
    End Try
  End Sub

  Sub New(ByVal className As String)
    mClassName = className
    TT.Write("New scHostNET", className, "ini")
  End Sub

  Sub terminateScript() Implements IScriptClassHost.terminateScript
    Runtime.InteropServices.Marshal.ReleaseComObject(mref)
    mref = Nothing
    scHost.Reset()
    scHost = Nothing
  End Sub

  Private Sub scHost_Error() Handles scHost.Error
    Dim stack = "Stack:" + vbNewLine + mscriptHelper.getCallStack()
    onScriptError2(scHost.Error, mfileSpec, stack)
  End Sub

  Sub scriptPreproc(ByRef scriptCode As String)
    Dim result As String
    Dim data() = Split(scriptCode, vbCrLf)
    mlineCount = data.Length

    For i = 0 To mlineCount - 1
      scriptLineModify(i + 1, data(i), result)
      data(i) = result

      'Dim checkLine = data(i).ToLower
      'checkLine = checkLine.Replace("public", "").Replace("private", "")
      'checkLine = checkLine.Trim


      'If checkLine.StartsWith("sub ") Or checkLine.StartsWith("function ") Or checkLine.StartsWith("property") Then
      '  data(i) += " : On Error Resume Next : ZZ.stackPush(""" + checkLine.Replace("""", """""") + """)"
      'End If

      'If checkLine.StartsWith("end sub") Or checkLine.StartsWith("end function") Or checkLine.StartsWith("end property") Then
      '  data(i) = " ZZ.stackPop : " + data(i)
      'End If

    Next

    scriptCode = getScriptHeader() + Join(data, vbCrLf)

    If scriptCode.ToLower.Contains("end class") = False Then
      scriptCode = scriptCode + vbNewLine + vbNewLine + "End Class" + vbNewLine
    End If

    MAIN.setOUT("vbScript precompiler Output", scriptCode)
    'Dim f As New Form With {.Text = mclassName, .Height = 400}
    'f.Controls.Add(New TextBox() With {.Text = scriptCode, .Dock = DockStyle.Fill, .Multiline = True, _
    '               .WordWrap = False, .ScrollBars = ScrollBars.Both, _
    '               .Font = New Font("Courier New", 9, FontStyle.Regular, GraphicsUnit.Point)})
    'f.Show()
  End Sub

  Dim subFuncTyp As String, isUnterstrich As Boolean

  Sub scriptLineModify(ByVal lineNr As Integer, ByVal sourceLine As String, ByRef rResultLine As String)
    Dim abPos, abPos2 As Integer
    Dim isSubFunc, EOsubFunc As Boolean
    Dim subFuncName As String
    Dim isElseStatement, isIfStatement, isEndIfStatement, isCaseStatement, isForNext, isDoppelpunkt As Boolean

    'kommentare vernichten
    abPos = sourceLine.IndexOf("'")
    If abPos > -1 Then
      Dim matches = System.Text.RegularExpressions.Regex.Matches(sourceLine, "(?<!"")"".*?(?<!"")""|'.*")
      For Each match As System.Text.RegularExpressions.Match In matches
        If match.Value.StartsWith("'") Then sourceLine = sourceLine.Substring(0, match.Index) : Exit For
      Next
    End If

    Dim sourceLineNetto2 = sourceLine.Trim
    If sourceLineNetto2 = "" Then rResultLine = sourceLine : Exit Sub

    Dim sourceLineNetto = sourceLineNetto2.ToUpper
    sourceLineNetto = sourceLineNetto.Replace("PRIVATE", "")
    sourceLineNetto = sourceLineNetto.Replace("PUBLIC", "")
    sourceLineNetto = sourceLineNetto.Replace("DEFAULT", "")

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
        sourceLine = Replace(sourceLine, "trace ", "ZZ.trace zLN,zNN,", , , CompareMethod.Text)
      Case "PRINTLINE"
        sourceLine = Replace(sourceLine, "printLine ", "ZZ.printLine zLN,zNN,", , , CompareMethod.Text)
    End Select


    Dim insertVorne = "zLN=" & lineNr.ToString & ":zC=zC+1:"
    Dim insertHinten As String
    insertHinten = ":If Err.number<>0 Or zC>zC2 Or zBB(zLN) Then For ziI=0 To 1:ZZ.CB zLN,zNN,zC,zC2,Err,ziI,ziE,ziQ,zBB(zLN):If ziE<>"""" Then: Execute(ziE):End If:Next:if ziQ Then ZZ.stackPop:Exit " & subFuncTyp '& ":End If"
    'insertHinten = ":If Err.number<>0 Or zC>zC2  Then For ziI=0 To 1:ZZ.CB zLN,zNN,zC,zC2,Err,ziI,ziE,ziQ:If ziE<>"""" Then: Execute(ziE):End If:Next:if ziQ Then ZZ.stackPop:Exit " & subFuncTyp '& ":End If"
    If firstWord = "STOP" Then
      sourceLine = ""
      insertHinten = ":If true Then For ziI=0 To 1:ZZ.CB zLN,zNN,zC,zC2,Err,ziI,ziE,ziQ,true:If ziE<>"""" Then: Execute(ziE):End If:Next:if ziQ Then ZZ.stackPop:Exit " & subFuncTyp '& ":End If"
    End If

    If isSubFunc Then insertVorne = "" : insertHinten = ":On Error Resume Next:zNN=" + apo + subFuncName + apo + ":ZZ.stackPush(zNN)"
    If subFuncTyp = "" Then insertVorne = "" : insertHinten = ""
    If EOsubFunc Then insertVorne = "ZZ.stackPop:" : insertHinten = ""
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

  Function getScriptHeader() As String
    Dim headerFilespec = ParaService.SettingsFolder + "scriptClass\scriptPrefix.txt"
    Dim txt = IO.File.ReadAllLines(headerFilespec)
    ReDim Preserve txt(200)
    Dim header = Join(txt, vbNewLine).Replace("{ScriptClass}", mClassName)
    Return header
  End Function

  Public Function checkForNewerFile() As Boolean Implements IScriptClassHost.checkForNewerFile
    If mIsInvalid Then Return True
    Dim modDat = IO.File.GetLastWriteTime(mfileSpec)
    If modDat <= mlastModified Then
      Return False
    Else
      If MsgBox("Der Code der folgende ScriptClass wurde seit dem letzten Kompilieren verändert. Soll die Klasse neu erstellt werden?" + vbNewLine + vbNewLine + "Pfad: " + mfileSpec + vbNewLine + "Zuletzt kompiliert: " + mlastModified.ToShortDateString + " " + mlastModified.ToLongTimeString + vbNewLine + "Änderungsdatum der Datei: " + modDat, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Script-Host") = MsgBoxResult.No Then
        mlastModified = modDat
        Return False
      End If
    End If
    Return True
  End Function

  Private mIsInvalid As Boolean
  Public Sub invalidate() Implements IScriptClassHost.invalidate
    mIsInvalid = True
  End Sub

  Public Function getNewClassRef(ByVal parentRef As WeakReference) As Object Implements IScriptClassHost.getNewClassRef

  End Function

  Public ReadOnly Property assemblyFilespec() As String Implements IScriptClassHost.assemblyFilespec
    Get
      Return ""
    End Get
  End Property
End Class
