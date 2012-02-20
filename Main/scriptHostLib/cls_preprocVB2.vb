Imports System.Text.RegularExpressions

Public Class cls_preprocVB2

  Public mClassName, scriptClassName As String
  Public globPara As New Dictionary(Of String, String)
  Public defines As New Dictionary(Of String, String)
  Public referenceList As New Dictionary(Of String, String)

  Public targetRuntime As RuntimeMode 'ACHTUNG: release wird hier NICHT verwendet - das entscheidet der scHost/siaCodeCompiler
  Public DebugMode As Integer         'PPC_DEBUG or PPC_RELEASE                      ...anhand diesem aus dem Code extrahierten Flag

  Public preprocErrors As New CodeDom.Compiler.CompilerErrorCollection()

  Private _files As New List(Of PPFile)

  Public errLine As Integer, errText As String

  Public DefaultIncludeFolder As String = ParaService.SettingsFolder + "scriptModules\"

  Public winData As New List(Of String), addinData As String
  Public globMethodList As New List(Of String)

  'Code Injection
  Public Const PPC_DEFAULT = 1
  Public Const PPC_NOINCETION = 2

  'Debug Mode
  Public Const PPD_DEBUG = 4
  Public Const PPD_RELEASE = 8


  'File Type .. scriptPrefix
  Public Const PPT_MAINCLASS = 16
  Public Const PPT_WINMAIN = 32
  Public Const PPT_MODULE = 64

  Public Const PPF_SILENTMODE = 128
  Public Const PPF_STOPCODE = 256

  Property className() As String
    Get
      Return mClassName
    End Get
    Protected Set(ByVal value As String)
      mClassName = value
      scriptClassName = System.Text.RegularExpressions.Regex.Replace(value, "[^a-zA-Z0-9_]", "_")
      'mCacheDllFileSpec = ParaService.SettingsFolder + "dllCache\q_scriptClass." + mScriptClassName + ".dll"
    End Set
  End Property

  Public Property para(ByVal paraName As String, Optional ByVal defaultValue As String = "") As String
    Get
      If defines.TryGetValue(paraName.ToLower, para) = False Then
        Return defaultValue
      End If
    End Get
    Set(ByVal value As String)
      defines(LCase(paraName)) = value
    End Set
  End Property

  Public ReadOnly Property Files() As ObjectModel.ReadOnlyCollection(Of PPFile)
    Get
      Return _files.AsReadOnly()
    End Get
  End Property

  Sub initDefaultReferences()
    Dim fn As String
    fn = "System.dll" : referenceList(LCase(fn)) = fn
    fn = "mscorlib.dll" : referenceList(LCase(fn)) = fn
    fn = "ScriptIDE.Core.dll" : referenceList(LCase(fn)) = fn
    fn = "ScriptIDE.ScriptWindowHelper.dll" : referenceList(LCase(fn)) = fn
    fn = "Microsoft.VisualBasic.dll" : referenceList(LCase(fn)) = fn
    fn = "System.Windows.Forms.dll" : referenceList(LCase(fn)) = fn
    fn = "System.Drawing.dll" : referenceList(LCase(fn)) = fn
  End Sub

  Function initScriptcompiler(ByVal fileSpec As String, ByVal debugMode As Integer)
    Me.className = IO.Path.GetFileNameWithoutExtension(fileSpec)
    Me.targetRuntime = RuntimeMode.IDE : Me.para("Runtime") = "IDE"
    If debugMode = PPD_DEBUG Then
      para("debug") = "true" : para("debugmode") = "DEBUG"
    Else
      para("release") = "true" : para("debugmode") = "RELEASE"
    End If
    Me.DebugMode = debugMode

    Me.AddFile(fileSpec, 0, PPC_DEFAULT, PPT_MAINCLASS)

    Me.AddFile(ParaService.SettingsFolder + "preprocTest\mainModule.vb", 0, PPC_NOINCETION, PPT_WINMAIN)

    '...nachdem die targetRuntime feststeht, muss der ermittelt werden
    ' If targetRuntime = RuntimeMode.IDE Then Me.DebugMode = PPD_DEBUG


  End Function

  Function AddFile(ByVal fileName As String, ByVal flags As Integer, ByVal codeInj As Integer, ByVal fileType As Integer) As Boolean
    Dim file As New PPFile()
    file.className = Me.className
    file.CodeInj = codeInj : file.FileType = fileType : file.Flags = flags
    file.Parent = Me
    file.FileSpec = fileName 'ACHTUNG: hier wird der Inhalt eingelesen!
    file.procFile()
    file.tempFileSpec = _files.Count.ToString("00") + "_" + IO.Path.GetFileName(file.FileSpec)
    _files.Add(file)
    Return True
  End Function

  Sub injectCodeHelperForAll()
    For Each file In _files
      If file.CodeInj <> PPC_NOINCETION Then
        'If para("tracestatements", "0") <> "0" Then 
        file.replaceLegacyTraceCommands()
        file.injectCodeHelper()
      End If
    Next
  End Sub

  Sub SaveAll(ByVal folderSpec As String)
    Dim idx As Integer = 0
    For Each file In _files
      file.LINES(4) = "' 004 References:       " + Join(Me.referenceList.Values.ToArray(), "; ")
      file.tempFileSpec = IO.Path.Combine(folderSpec, idx.ToString("00") + "_" + IO.Path.GetFileName(file.FileSpec))
      IO.File.WriteAllLines(file.tempFileSpec, file.LINES)
      idx += 1
    Next

  End Sub

  Function getTargetType() As String
    If targetRuntime = RuntimeMode.IDE Or targetRuntime = RuntimeMode.HostedDebug Or targetRuntime = RuntimeMode.DLL Then
      Return "library"
    Else
      If defines.TryGetValue("AssemblyType", getTargetType) = False Then Return "winexe"
      If getTargetType = "winexe2" Then getTargetType = "winexe"
    End If
  End Function

  Sub copyReferencedAssemblies(ByVal targetFolder As String)
    Dim scriptPath = ParaService.FP(IO.Path.GetDirectoryName(_files(0).FileSpec))
    For Each ref In referenceList
      Dim path As String = "", fn = IO.Path.GetFileName(ref.Value)
      If IO.File.Exists(scriptPath + fn) Then : path = scriptPath + fn
      ElseIf IO.File.Exists(ref.Value) Then : path = ref.Value
      ElseIf IO.File.Exists(ParaService.SettingsFolder + "extLibs\" + fn) Then : path = ParaService.SettingsFolder + "extLibs\" + fn
      End If
      If path = "" Then TT.Write("Reference NOT found", ref.Value, "dump") : Continue For
      Try
        IO.File.Copy(path, targetFolder + fn)
        TT.Write("Reference copied SUCCESSFULLY", path + " -> " + targetFolder, "dump")
      Catch ex As Exception
        TT.Write("EXCEPTION while copying reference", path + " " + ex.Message, "warn")
      End Try
    Next
  End Sub

  Function runCompiler() As CodeDom.Compiler.CompilerResults
    Dim targetFolder = ParaService.SettingsFolder + "preprocTest\" + className + "\"
    IO.Directory.CreateDirectory(targetFolder)
    Me.SaveAll(targetFolder)

    Dim options As New CodeDom.Compiler.CompilerParameters
    Dim fileNames(_files.Count - 1) As String
    For i = 0 To _files.Count - 1
      fileNames(i) = _files(i).tempFileSpec
    Next

    'mpara.References.Add("scripthostlib.dll")
    For Each kvp In Me.referenceList
      options.ReferencedAssemblies.Add(kvp.Value)
    Next

    options.GenerateExecutable = True : options.GenerateInMemory = False

    Dim libType = getTargetType()

    Dim outputFolder As String, outputFilename = className + If(libType = "library", ".dll", ".exe")
    If targetRuntime = RuntimeMode.Debug And DebugMode = PPD_RELEASE Then
      outputFolder = "C:\yEXE\scriptEXE\" + className + "\"
    ElseIf targetRuntime = RuntimeMode.Debug And DebugMode = PPD_DEBUG Then
      outputFolder = ParaService.SettingsFolder + "compiledScripts\" + className + "\debug\"
    ElseIf targetRuntime = RuntimeMode.IDE Then
      'options.OutputAssembly = targetFolder + outputFilename
      outputFolder = ParaService.SettingsFolder + "dllCache\"
      options.GenerateInMemory = True
    ElseIf targetRuntime = RuntimeMode.HostedDebug Then
      outputFolder = ParaService.SettingsFolder + "dllCache\"
    ElseIf targetRuntime = RuntimeMode.DLL Then
      outputFolder = ParaService.SettingsFolder + "compiledScripts\" + className + "\lib\"
    End If
    options.OutputAssembly = outputFolder + outputFilename
    IO.Directory.CreateDirectory(outputFolder)

    If libType = "library" Then disposeOldFile(options.OutputAssembly)
    If targetRuntime <> RuntimeMode.IDE Then _
      copyReferencedAssemblies(outputFolder)

    options.CompilerOptions = " /optioninfer+ /optionexplicit+ " + _
                              " /t:" + getTargetType() + _
                              " /doc:""" + IO.Path.ChangeExtension(options.OutputAssembly, ".xml") + """" + _
                              " " + para("CompilerOptions")
    Dim iconFileName = ResourceLoader.GetFileCached(Trim(Me.para("IconFile")))
    If iconFileName <> "" Then
      options.CompilerOptions += " /win32icon:" + iconFileName ' + """"
    End If

    'Dim x As New CodeDom.Compiler.CompilerResults(New CodeDom.Compiler.TempFileCollection())
    'x.Errors.
    

    Dim compiler As CodeDom.Compiler.CodeDomProvider
    compiler = New Microsoft.VisualBasic.VBCodeProvider()
    Dim RESULT = compiler.CompileAssemblyFromFile(options, fileNames)
    Dim out = ""
    For Each x In RESULT.Output : out += x + vbNewLine : Next

    ZZ.setOutMonitor(out)
    Return RESULT
  End Function

  Sub disposeOldFile(ByVal fileSpec As String)
    If IO.File.Exists(fileSpec) = False Then Exit Sub
    Try
      IO.File.Delete(fileSpec)
    Catch ex As Exception
      Try
        IO.File.Move(fileSpec, ParaService.SettingsFolder + "temp\" & IO.Path.GetFileName(fileSpec) & Hex(Now.Ticks) & ".deleteMe")
      Catch ex2 As Exception
        MsgBox("Fehler: konnte die temporäre Datei nicht entsorgen" + vbNewLine + fileSpec + vbNewLine + vbNewLine + ex2.Message, MsgBoxStyle.Exclamation)
      End Try
    End Try
  End Sub

  Public Class PPFile
    Public className As String
    Public CodeInj As Integer
    Public FileType As Integer
    Public Flags As Integer

    Public filePara As New Dictionary(Of String, String)
    Public ImportList As New Dictionary(Of String, String)
    Public ImplementsList As New Dictionary(Of String, String)
    Public Parent As cls_preprocVB2

    Public LINES() As String

    Public lastProcLine As Integer

    Public tempFileSpec As String
    Private _fileSpec As String
    Public Property FileSpec() As String
      Get
        Return _fileSpec
      End Get
      Set(ByVal value As String)
        _fileSpec = value
        If IO.File.Exists(_fileSpec) Then
          Dim mC, iC As Integer
          mC = 1000
          ReDim LINES(mC)
          insertPrefix()
          iC = 200
          Dim sr As New IO.StreamReader(_fileSpec)
          While Not sr.EndOfStream
            If iC > mC Then mC += 1000 : ReDim Preserve LINES(mC)
            LINES(iC) = sr.ReadLine
            iC += 1
          End While
          sr.Close()
          ReDim Preserve LINES(iC + 2)
          If FileType = PPT_MAINCLASS Or FileType = PPT_MODULE Then LINES(iC) = "  End Class"
          LINES(iC + 1) = "End NameSpace"

        Else
          ReDim LINES(0)
        End If
      End Set
    End Property


    Private Function getInsertHinten(ByVal flags As Integer, ByVal lineNum As Integer, ByVal methodType As String)
      Dim zLN As String = lineNum.ToString
      If (Parent.DebugMode And PPD_RELEASE) <> 0 And (flags And PPF_STOPCODE) <> 0 Then
        Return "ZZ.trace(" + zLN + ",zNN,""STOP-Anweisung aufgetreten"")"
      ElseIf (Parent.DebugMode And PPD_RELEASE) <> 0 And (flags And PPF_STOPCODE) = 0 Then
        Return ":If Err.number<>0 Then ZZ.CB(" + zLN + ",zNN,zC,zC2,Err,ziI,ziE,ziQ,false)"

      ElseIf (Parent.DebugMode And PPD_DEBUG) <> 0 And (flags And PPF_STOPCODE) <> 0 Then
        Return ":zC+=1:If true Then ZZ.CB(" + zLN + ",zNN,zC,zC2,Err,ziI,ziE,ziQ,true):if ziQ Then Exit " + methodType
      ElseIf (Parent.DebugMode And PPD_DEBUG) <> 0 And (flags And PPF_STOPCODE) = 0 Then
        Return ":zC+=1:If Err.number<>0 Or zC>zC2 Or zBB(" + zLN + ") Then ZZ.CB(" + zLN + ",zNN,zC,zC2,Err,ziI,ziE,ziQ,zBB(" + zLN + ")):if ziQ Then Exit " + methodType

      Else
        Return ""
      End If
    End Function


    Sub bogusInsertData(ByVal myLines() As String)
      ReDim Preserve LINES(204 + myLines.Length)
      For i = 0 To myLines.Length - 1
        LINES(200 + i) = Trim(myLines(i))
        If LINES(200 + i).StartsWith("#") Then
          LINES(200 + i) = "'" + LINES(200 + i)
        End If


      Next
    End Sub

    Function evalStat(ByVal stat As String) As Boolean
      stat = Trim(stat)
      Dim startPos = 0
      If stat.StartsWith("(") Then startPos = stat.IndexOf(")")
      If startPos = -1 Then Parent.preprocErrors.Add(New CodeDom.Compiler.CompilerError(FileSpec, Parent.errLine, 0, "PP8003", "Klammer zu erwartet """ + stat + """")) : Return False
      Dim orPos, andPos As Integer
      orPos = stat.IndexOf("|", startPos)
      andPos = stat.IndexOf("&", startPos)
      If orPos > -1 AndAlso (orPos < andPos Or andPos = 0) Then
        Dim parts() = Split(stat, "|", 2)
        Return evalStat(parts(0)) Or evalStat(parts(1))
      ElseIf andPos > -1 AndAlso (andPos < orPos Or orPos = 0) Then
        Dim parts() = Split(stat, "&", 2)
        Return evalStat(parts(0)) And evalStat(parts(1))
      ElseIf stat.StartsWith("!") Then
        Return Not evalStat(stat.Substring(1))
      ElseIf startPos > 0 Then
        Return evalStat(stat.Substring(1, stat.Length - 1))
      Else
        Return Parent.defines.ContainsKey(LCase(stat))
      End If
    End Function


    Sub replaceLegacyTraceCommands()
      'Dim reg As New Regex("^[\s:@]*(Trace|PrintLine)[\s(]+(.*?)[)]?[\s]*$", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
      'Dim reg As New Regex("^[\s:@]*(Trace|PrintLine)[\s]+(.*?)[\s]*$", RegexOptions.IgnoreCase Or RegexOptions.Multiline)
      'For i = 0 To LINES.Length - 1
      '  LINES(i) = reg.Replace(LINES(i), "$1( $2 )")
      'Next
    End Sub


    Sub procFile()
      Dim nettoLine, firstWord, para, words() As String
      Dim handled As Boolean
      Dim isInDataBlock As Integer, dataOut As System.Text.StringBuilder
      Dim condStack As New Stack(Of Boolean), notEvaldCondCounter As Integer
      For i = 0 To LINES.Length - 1
        If isInDataBlock <> 0 Then
          If Trim(LINES(i)).StartsWith("#EndData", StringComparison.InvariantCultureIgnoreCase) Then
            Select Case isInDataBlock
              Case 1 : LINES(i) = "  """" ' End of Data Block"
              Case 2 : dataOut.AppendLine(LINES(i)) : LINES(i) = "' EndWinData " + LINES(i) : Parent.winData.Add(dataOut.ToString)
              Case 3 : LINES(i) = "' EndAddinData " + LINES(i) : Parent.addinData = dataOut.ToString 'dataOut.AppendLine(LINES(i))
            End Select
            isInDataBlock = 0 : dataOut = Nothing
          Else
            If isInDataBlock = 1 Then : LINES(i) = "  """ + Replace(LINES(i), """", """""") + """ + vbNewLine + _"
            Else : dataOut.AppendLine(LINES(i)) : LINES(i) = "' data " + LINES(i)
            End If
          End If
          Continue For
        End If

        LINES(i) = Trim(LINES(i)).Replace("__LINE__", i.ToString("0000")).Replace("__FILE__", """" + FileSpec + """").Replace("__CLASSNAME__", className)
        Parent.errLine = i
        handled = False

        nettoLine = LINES(i) + " "
        firstWord = LCase(nettoLine.Substring(0, nettoLine.IndexOf(" ")))

        If notEvaldCondCounter > 0 OrElse (condStack.Count > 0 AndAlso condStack.Peek() = False AndAlso firstWord <> "#endif" AndAlso firstWord <> "#else") Then
          If firstWord = "#ifcond" Or firstWord = "#ifdef" Or firstWord = "#ifndef" Then notEvaldCondCounter += 1
          If notEvaldCondCounter > 0 AndAlso (firstWord = "#endif") Then notEvaldCondCounter -= 1
          LINES(i) = "'" & condStack.Count & "-" & notEvaldCondCounter & "-" & LINES(i)
          Continue For
        End If

        If nettoLine.StartsWith("#") Then
          handled = True
          Select Case firstWord
            Case "#imports"
              para = Trim(Split(nettoLine, " ", 2)(1))
              ImportList(LCase(para)) = para

            Case "#implements"
              para = Trim(Split(nettoLine, " ", 2)(1))
              ImplementsList(LCase(para)) = para

            Case "#ref", "#reference"
              para = Trim(Split(nettoLine, " ", 2)(1))
              Parent.referenceList(LCase(IO.Path.GetFileName(para))) = para

            Case "#inc", "#include", "#merge"
              para = Trim(Split(nettoLine, " ", 2)(1))
              Dim incFileName As String
              If para(0) = "<"c And para(para.Length - 1) = ">"c Then _
                incFileName = IO.Path.Combine(Parent.DefaultIncludeFolder, para.Substring(1, para.Length - 2) + ".nsm")
              If para(0) = """"c And para(para.Length - 1) = """"c Then _
                incFileName = IO.Path.Combine(IO.Path.GetDirectoryName(FileSpec), para.Substring(1, para.Length - 2))
              If String.IsNullOrEmpty(incFileName) OrElse IO.File.Exists(incFileName) = False Then _
                Parent.preprocErrors.Add(New CodeDom.Compiler.CompilerError(FileSpec, i, 0, "PP8001", "Includefile " + para + " nicht gefunden")) : Continue For

              Parent.AddFile(incFileName, 0, CodeInj, PPT_MODULE)

            Case "#silentmodeon"
              Flags = Flags And PPF_SILENTMODE

            Case "#codeinjection"
              para = UCase(Split(nettoLine, " ", 2)(1))
              If para.StartsWith("N") Then : CodeInj = PPC_NOINCETION
              ElseIf para.StartsWith("D") Then : CodeInj = PPC_DEFAULT
              Else : Parent.preprocErrors.Add(New CodeDom.Compiler.CompilerError(FileSpec, i, 0, "PP8002", "Ungültiger Werte für Flag #CodeInjection - bitte verwende 'NOTHING', 'DEFAULT'")) : Continue For
              End If

            Case "#runtime"
              para = UCase(Split(nettoLine, " ", 2)(1))
              If para.StartsWith("I") Then : Parent.targetRuntime = RuntimeMode.IDE : Parent.para("runtime") = "IDE"
              ElseIf para.StartsWith("E") Then : Parent.targetRuntime = RuntimeMode.Debug : Parent.para("runtime") = "EXE"
              ElseIf para.StartsWith("H") Then : Parent.targetRuntime = RuntimeMode.HostedDebug : Parent.para("runtime") = "HOSTED"
              ElseIf para.StartsWith("D") Then : Parent.targetRuntime = RuntimeMode.DLL : Parent.para("runtime") = "DLL"
              Else : Parent.preprocErrors.Add(New CodeDom.Compiler.CompilerError(FileSpec, i, 0, "PP8002", "Ungültiger Werte für Flag #Runtime - bitte verwende 'IDE', 'EXE' oder 'HOSTED'")) : Continue For
              End If

            Case "#data"
              LINES(i) = Replace(LINES(i), "#Data", "Dim ", , , CompareMethod.Text) + " = _"
              isInDataBlock = 1 : handled = False

            Case "#windowdata"
              dataOut = New System.Text.StringBuilder() : dataOut.AppendLine(LINES(i))
              LINES(i) = "' data=2 " + LINES(i)
              isInDataBlock = 2 : handled = True

            Case "#addindata"
              dataOut = New System.Text.StringBuilder() ': dataOut.AppendLine(LINES(i))
              LINES(i) = "' data=3 " + LINES(i)
              isInDataBlock = 3 : handled = True

            Case "#def", "#define", "#para"
              words = Split(nettoLine, " ", 3)
              Parent.para(words(1)) = words(2)

            Case "#undef", "#undefine"
              words = Split(nettoLine, " ", 3)
              Parent.defines.Remove(LCase(words(1)))

            Case "#ifcond"
              Dim match = Regex.Match(nettoLine, "#ifcond\s+([a-zA-Z0-9_-]+)\s+(==|=|\!=|cnt|\!cnt|like|\!like)\s+""(.*)""")
              If match.Success = False Then Parent.preprocErrors.Add(New CodeDom.Compiler.CompilerError(FileSpec, i, 0, "PP8005", "Syntax für erweiterte PreProc-Bedingungen: #ifcond VARNAME ==|=|!=|cnt|!cnt|like|!like ""varcont""")) : Continue For
              Dim val = Parent.para(match.Groups(1).Value)
              Dim bRes As Boolean
              Select Case match.Groups(2).Value
                Case "==", "=" : bRes = val = match.Groups(3).Value
                Case "!=" : bRes = val <> match.Groups(3).Value
                Case "cnt" : bRes = val.Contains(match.Groups(3).Value)
                Case "!cnt" : bRes = Not val.Contains(match.Groups(3).Value)
                Case "like" : bRes = val Like match.Groups(3).Value
                Case "!like" : bRes = Not val Like match.Groups(3).Value
              End Select
              condStack.Push(bRes)

            Case "#ifdef"
              words = Split(nettoLine, " ", 2)
              condStack.Push(evalStat(words(1)))

            Case "#ifndef"
              words = Split(nettoLine, " ", 2)
              condStack.Push(Not evalStat(words(1)))

            Case "#else"
              condStack.Push(Not condStack.Pop())

            Case "#endif"
              condStack.Pop()

            Case Else : handled = False
          End Select

        End If
        If handled Then LINES(i) = "'+" + LINES(i)
      Next
      Parent.errLine = -1


      'Insert Imports+Implements
      If ImportList.Count > 0 Then LINES(10) = "Imports " + Join(ImportList.Values.ToArray(), ", ")
      If ImplementsList.Count > 0 Then LINES(14) = "Implements " + Join(ImplementsList.Values.ToArray(), ", ")
    End Sub

    Enum VBScope
      NS
      Cls
      PropOuter
      MethodHead
      SelectHead
      CodeLine
    End Enum

    Enum ColonMode
      None
      errClear
      skipComplete
    End Enum

    Sub injectCodeHelper()

      Dim abPos, abPos2 As Integer
      Dim nettoLine, nettoLineU, firstWord, secondWord, para, words() As String

      Dim regRemoveWords As New Regex("(PRIVATE|PROTECTED|FRIEND|PUBLIC|PARTIAL|SHADOWS|OVERLOADS|OVERRIDABLE|OVERRIDES|MUSTOVERRIDE|DEFAULT|SHARED|READONLY|WRITEONLY)", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
      Dim regEmptyFuncs As New Regex("(PARTIAL|MUSTOVERRIDE)", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
      Dim regTrimInner As New Regex("\s+", RegexOptions.Compiled)
      Dim regSubFunc As New Regex("(FUNCTION|SUB|PROPERTY)\s+(.*)\s", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
      Dim regTrace As New Regex("^[\s:@]*(Trace|PrintLine)\s+(.*)\s*$", RegexOptions.IgnoreCase Or RegexOptions.Compiled)

      Dim prevScope = VBScope.NS, scope As VBScope = VBScope.NS
      Dim methodType, methodName As String
      Dim insertFirstLine As Boolean
      Dim lineColon, blockColon, funcColon As ColonMode
      Dim isInInterface As Boolean

      For i = 12 To LINES.Length - 1
        abPos = LINES(i).IndexOf("'")
        While abPos <> -1
          If (StringHelper.CountChars(LINES(i).Substring(0, abPos), """"c) Mod 2) = 0 Then
            LINES(i) = LINES(i).Substring(0, abPos) : Exit While
          End If
          abPos = LINES(i).IndexOf("'", abPos + 1)
        End While
        LINES(i) = regTrace.Replace(LINES(i), "ZZ.$1(" & i & ",zNN,$2)")

        'If abPos > -1 Then
        '  'Dim matches = System.Text.RegularExpressions.Regex.Matches(LINES(i), "(?<!"")"".*?(?<!"")""|'.*")
        '  'If i = 1077 Then Stop
        '  For Each match As System.Text.RegularExpressions.Match In matches
        '    If match.Value.StartsWith("'") Then LINES(i) = LINES(i).Substring(0, match.Index) : Exit For
        '  Next
        'End If

        lineColon = ColonMode.None
        If LINES(i).StartsWith("::") Then lineColon = ColonMode.errClear : LINES(i) = LINES(i).Substring(2)
        If LINES(i).StartsWith(":") Then lineColon = ColonMode.skipComplete : LINES(i) = LINES(i).Substring(1)
        If LINES(i).StartsWith("@") Then lineColon = ColonMode.errClear : LINES(i) = LINES(i).Substring(1)

        nettoLine = regTrimInner.Replace(Trim(regRemoveWords.Replace(LINES(i), "")), " ")
        If nettoLine = "" Then Continue For

        'nettoLineU = UCase(nettoLine)

        words = nettoLine.Split("("c, " "c)
        firstWord = UCase(words(0))
        Select Case firstWord
          Case "BEGIN"
            blockColon = lineColon : LINES(i) = ""
            If blockColon = ColonMode.errClear Then LINES(i) = "On Error Resume Next"
            Continue For

          Case "INTERFACE"
            isInInterface = True

          Case "CLASS"
            scope = VBScope.Cls

          Case "SUB", "FUNCTION", "PROPERTY"
            If isInInterface Or regEmptyFuncs.IsMatch(LINES(i)) Then Continue For
            methodType = firstWord : methodName = words(1)
            scope = VBScope.MethodHead
            funcColon = lineColon
            If firstWord = "PROPERTY" Then scope = VBScope.PropOuter
            Parent.globMethodList.Add(UCase(methodName))
          Case "GET", "SET"
            If scope = VBScope.PropOuter Then
              scope = VBScope.MethodHead
            End If

          Case "SELECT"
            scope = VBScope.SelectHead

          Case "CASE"
            scope = VBScope.CodeLine

          Case "END"
            If words.Length > 1 Then secondWord = UCase(words(1)) Else secondWord = ""
            If secondWord = "GET" Or secondWord = "SET" Then scope = VBScope.PropOuter
            If secondWord = "SUB" Or secondWord = "FUNCTION" Or secondWord = "PROPERTY" Then scope = VBScope.Cls
            If secondWord = "CLASS" Then scope = VBScope.NS
            If secondWord = "SELECT" Then scope = VBScope.CodeLine
            If secondWord = "INTERFACE" Then isInInterface = False

            If funcColon = ColonMode.errClear AndAlso (secondWord = "GET" Or secondWord = "SET" Or secondWord = "SUB" Or secondWord = "FUNCTION") Then
              LINES(i - 1) += ":Err.Clear()"
            End If

            If secondWord = "" And lineColon <> ColonMode.None Then
              If blockColon = ColonMode.errClear Then LINES(i) = "Err.clear()" Else LINES(i) = ""
              blockColon = ColonMode.None : Continue For
            End If
        End Select
        'If methodName = "AutoStart" Then Stop

        Dim insertVorne = "", insertHinten As String = ""
        If scope >= VBScope.MethodHead Then
          'insertVorne = "zLN=" & i.ToString & ":zC=zC+1:"
          If firstWord = "STOP" Then
            LINES(i) = getInsertHinten(Parent.DebugMode Or PPF_STOPCODE, i, methodType)
            insertHinten = ""
          Else
            insertHinten = getInsertHinten(Parent.DebugMode, i, methodType)
          End If

          If lineColon = ColonMode.errClear Then insertVorne += "On Error Resume Next: " : insertHinten = ":Err.clear()"
          If blockColon = ColonMode.errClear Or funcColon = ColonMode.errClear Then insertHinten = " 'colon:err"

          If scope = VBScope.SelectHead Then insertHinten = " 'selHead"

          If scope <> VBScope.MethodHead And insertFirstLine Then
            scope = VBScope.CodeLine : insertFirstLine = False
            insertVorne = "On Error Resume Next : zNN=""" + methodName + """ : " + insertVorne
          End If
          If scope = VBScope.MethodHead Then insertFirstLine = True : scope = VBScope.CodeLine : insertVorne = "" : insertHinten = " 'methHead"

          If lineColon = ColonMode.skipComplete Or blockColon = ColonMode.skipComplete Or funcColon = ColonMode.skipComplete _
              Then insertVorne = "" : insertHinten = " 'colon=skip"

          If nettoLine.EndsWith(" _") Then insertHinten = ""
        End If
        Dim tabCnt = Math.Max(0, (80 - LINES(i).Length) \ 4) '\ 4
        LINES(i) = insertVorne + LINES(i) + StrDup(tabCnt, vbTab) + insertHinten
        LINES(i) = LINES(i).Replace("__LINE__", i.ToString("0000"))
        prevScope = scope
      Next


    End Sub

    Private Sub insertPrefix()
      Dim i As Integer = -1
      i += 1 : LINES(i) = "' " + i.ToString("000") + " This file was generated by the ScriptIDE2 sh06 preprocessor"
      i += 1 : LINES(i) = "' " + i.ToString("000") + " Compilation date: " + Now.ToString
      i += 1 : LINES(i) = "' " + i.ToString("000") + " Source File:      " + FileSpec
      i += 1 : LINES(i) = "' " + i.ToString("000") + " Script Class:     " + className
      i += 1 : LINES(i) = "' " + i.ToString("000") + " References:       ... to be replaced - this should never be visible" '+ Join(Parent.referenceList.Values.ToArray(), "; ")
      i += 1 : LINES(i) = "' " + i.ToString("000") + " Flags: 0x" + Hex(CodeInj) + "       Code Injection: 0x" + Hex(CodeInj) + "   File Type: 0x" + Hex(FileType)
      i += 1 : LINES(i) = "' " + i.ToString("000") + " ---------- Import Statements ------------"
      i += 1 ' : LINES(i) = "Imports System, System.Windows.Forms"
      i += 1 ' : LINES(i) = "Imports Microsoft.VisualBasic"
      i += 1 ' : LINES(i) = "Imports System.Windows.Forms"
      'For Each kvp In ImportList
      'i += 1 : LINES(i) = "Imports " + kvp.Value
      'Next
      i = 10 : LINES(i) = "' RESERVED FOR IMPORT STATEMENTS"
      i += 1 : LINES(i) = "' " + i.ToString("000") + " ---------- Script Prefix ------------"
      i += 1 : LINES(i) = "Namespace ScriptClass"
      If FileType = PPT_MAINCLASS Then
        'scriptHelper einfügen
        i += 1 : LINES(i) = "  Partial Class " + className
        i = 14 : LINES(i) = "' RESERVED FOR IMPLEMENTS STATEMENTS"
        Dim sr = IO.File.OpenText(ParaService.SettingsFolder + "preprocTest\vbnetPrefix2.vb")
        While Not sr.EndOfStream
          i += 1 : LINES(i) = sr.ReadLine()
        End While
      End If
      If FileType = PPT_MODULE Then
        'Klassenkopf einfügen
        i += 1 : LINES(i) = "  Partial Class " + className

      End If
      i = 198 : LINES(i) = "' " + i.ToString("000") + " ---------- Main Script Content ------------"

    End Sub
    Sub initDefaultImports()
      Dim fn As String
      fn = "System" : ImportList(LCase(fn)) = fn
      fn = "ScriptIDE" : ImportList(LCase(fn)) = fn
      fn = "ScriptIDE.Core" : ImportList(LCase(fn)) = fn
      fn = "Microsoft.VisualBasic" : ImportList(LCase(fn)) = fn
      fn = "System.Windows.Forms" : ImportList(LCase(fn)) = fn
      fn = "System.Drawing" : ImportList(LCase(fn)) = fn
    End Sub

    Public Sub New()
      initDefaultImports()
    End Sub
  End Class

End Class
