Imports System.Runtime.InteropServices

<Microsoft.VisualBasic.ComClass()> Public Class cls_scriptHelperGlobalScope
  Dim scriptClassName As String
  Public ReadOnly Property Auto() As Integer
    Get
      Return -2
    End Get
  End Property

  Sub DoEvents()
    Application.DoEvents()
  End Sub

  Public Sub New(ByVal clsName As String)
    scriptClassName = clsName
  End Sub
End Class


<Microsoft.VisualBasic.ComClass()> Public Class cls_scriptHelper
  Implements IScriptHelper

  Public _scriptClassName As String
  Public _scriptFilespec As String
  Public _isLocalSilentMode As Boolean

  Public _scriptInst As WeakReference

  Dim isF9Dauerton As Integer

  Public sH As ScriptHost

  Dim mtimerStart As Integer
  Public Declare Function GetTime Lib "winmm.dll" Alias "timeGetTime" () As Integer
  Private _fso, _wscriptshell As Object
  'Private _idehelper As ScriptHelper_IDEHelper

  ReadOnly Property IDEHelper() As IIDEHelper Implements IScriptHelper.IDEHelper
    Get
      'If _idehelper Is Nothing Then _idehelper = New ScriptHelper_IDEHelper : _idehelper.ScriptClassName = _scriptClassName
      Return cls_IDEHelperMini.GetSingleton
    End Get
  End Property

  ReadOnly Property ScriptClassName() As String Implements IScriptHelper.ScriptClassName
    Get
      Return _scriptClassName
    End Get
  End Property

  Public Function appPath() As String
    Return FP(My.Computer.FileSystem.GetParentPath(Windows.Forms.Application.ExecutablePath))
  End Function


  Public Function FP(ByVal path As String, Optional ByVal fileName As String = "") Implements IScriptHelper.FP
    Return path + IIf(path.EndsWith("\"), "", "\") + If(fileName.StartsWith("\"), fileName.Substring(1), fileName)
  End Function
  Public Function FPUNIX(ByVal path As String, Optional ByVal fileName As String = "") Implements IScriptHelper.FPUNIX
    Return path + IIf(path.EndsWith("/"), "", "/") + If(fileName.StartsWith("/"), fileName.Substring(1), fileName)
  End Function

  Function FilePathCombine(ByVal f1, ByVal f2, Optional ByVal isUnix = False) Implements IScriptHelper.FilePathCombine
    If isUnix Then Return FPUNIX(f1, f2) Else Return FP(f1, f2)
  End Function


  Sub trace(ByVal zLN, ByVal zNN, ByVal para1, Optional ByVal para2 = "", Optional ByVal typ = "info") Implements IScriptHelper.trace
    If sH.HostMode = RuntimeMode.Release Then
      Dim codeLink = "_|°|_cLink_|°|_" + "scriptClass" & "_|°|_" & ScriptClassName & "?" & zLN & "_|°|_" & zNN & ""
      sH.oIntWin.SendCommand("tracemonitor", "Trace", typ & "|##|" & traceMaskSplitter(para1) & "|##|" & para2 & "|##|" & codeLink)
    Else
      sH.oIntWin.SendCommand("scripthostlib06", "RuntimeObject_trace", zLN & "|##|" & zNN & "|##|" & typ & "|##|" & traceMaskSplitter(para1) & "|##|" & para2)
    End If
  End Sub

  Sub printLine(ByVal zLN, ByVal zNN, ByVal index, ByVal title, ByVal data) Implements IScriptHelper.printLine
    If sH.HostMode = RuntimeMode.Release Then
      Dim codeLink = "_|°|_cLink_|°|_" + "scriptClass" & "_|°|_" & ScriptClassName & "?" & zLN & "_|°|_" & zNN & ""
      sH.oIntWin.SendCommand("tracemonitor", "PrintLine", index & "|##|" & title & "|##|" & traceMaskSplitter(data) & "|##|" & codeLink)
    Else
      sH.oIntWin.SendCommand("scripthostlib06", "RuntimeObject_printLine", zLN & "|##|" & zNN & "|##|" & index & "|##|" & title & "|##|" & data)
    End If
  End Sub
  Private Function traceMaskSplitter(ByVal t As String) As String
    Return Replace(t, "|##|", "| # # |")
  End Function


  Function NavigateScript(ByVal className As String, ByVal cmdName As String, ByVal flags As String, ByVal target As String) As String
    'Dim p() = Split(para, "|##|", 3) : ReDim Preserve p(3)

    If className = sH.RunningAsCompiledClass Then
      Dim cls = scriptClass(className)
      Try
        NavigateScript = cls.Navigate(cmdName, flags, target)
      Catch ex As Exception
        NavigateScript = "ERR: scriptClass doesn't provide method 'Navigate' or Program not loaded"
      End Try
      Exit Function
    End If

    If sys_interproc.getWindow("siDebug_" + className + "_") <> IntPtr.Zero Then
      NavigateScript = sH.oIntWin.GetData("siDebug_" + className + "_", "Navigate", className + "|##|" + cmdName + "|##|" + Replace(flags, "|##|", "| # # |") + "|##|" + target)
      Exit Function
    End If

    NavigateScript = sH.oIntWin.GetData("scripthostlib06", "NavigateScript", className + "|##|" + cmdName + "|##|" + Replace(flags, "|##|", "| # # |") + "|##|" + target)

  End Function


  Sub CB(<Out()> ByRef zLN, ByVal zNN, <Out()> ByRef zC, <Out()> ByRef zC2, <Out()> ByRef zErr, <Out()> ByRef ziI, <Out()> ByRef ziE, <Out()> ByRef ziQ, Optional ByVal onStopCode = False) Implements IScriptHelper.CB
    CB2(zLN, zNN, zC, zC2, zErr.Number, zErr.Description, ziI, ziE, ziQ, onStopCode)
    zErr.Clear()
  End Sub
  Sub CB2(<Out()> ByRef zLN As Integer, ByVal zNN As String, <Out()> ByRef zC As Integer, <Out()> ByRef zC2 As Integer, ByVal errorNum As Integer, ByVal errorDesc As String, <Out()> ByRef ziI As Integer, <Out()> ByRef ziE As String, <Out()> ByRef ziQ As Boolean, Optional ByVal onStopCode As Boolean = False) Implements IScriptHelper.CB2
    On Error Resume Next
    If sH.dumpStackToTrace Then TT.Write(zLN, zNN, "callBack ln=" & zLN, "c=" & zC & "  c2=" & zC2 & "   err=" & errorNum & "  i=" & ziI & "  e=" & ziE & "  q=" & ziQ)
    Dim errDesc = ""
    If errorNum <> 0 Then
      Dim errText = "Line=" & zLN & " File=" & ScriptClassName & " Func=" & zNN & vbNewLine & _
                              errorNum & " - " & errorDesc & vbNewLine & _
                              getCallStack()

      If sH.HostMode = RuntimeMode.Release Then
        '----- RELEASE -----
        Console.Error.WriteLine("Unhandled Exception in " + errText)


      Else
        '----- DEBUG -----
        sH.oIntWin.SendCommand("scripthostlib06", "RuntimeObject_onScriptError", ScriptClassName & "|##|" & zLN & "|##|" & errText)
        errDesc = errorDesc

        'If ScriptHost.Instance.SilentMode = False And _isLocalSilentMode = False Then _
        sH.globBreakMode = "BREAK"
      End If
    End If
    Application.DoEvents()
    ziI = 2
    If sH.HostMode = RuntimeMode.Release Then
      zC2 = zC + 100000
    Else
      zC2 = zC + 40
    End If
    'moveScriptRunningLabelSpace()

    If onStopCode Then Me.trace(zLN, zNN, "STOP-Anweisung aufgetreten ...", , "err")
    If sH.SilentMode Or _isLocalSilentMode Or sH.HostMode = RuntimeMode.Release Then Exit Sub 'bei silent Mode alles Tracen, aber nie anhalten
    If onStopCode Then
      errDesc = "S T O P - Code"
      sH.globBreakMode = "BREAK"
    End If
    If isKeyPressed(Keys.F12) Or sH.globBreakMode = "EinzelSchritt" Then
      sH.globBreakMode = "BREAK"
    End If
    ziQ = False
    If (isKeyPressed(Keys.F12) And isKeyPressed(Keys.ControlKey)) Or sH.globBreakMode = "QUIT" Then  'isKeyPressed(Keys.F11) Then
      ziQ = True
      zC2 = zC
      sH.globBreakMode = "RUN"
      Exit Sub
    End If

    If sH.globBreakMode = "BREAK" Then
      'MAIN.qq_txtOutMonitor.Text = getCallStack()
      'Dim fileName = sH.expandScriptClassName(ScriptClassName)
      'Debug.Print("Goto: " & fileName & "   " & zLN)
      'gotoNote("loc:/" + fileName)
      'Dim sc = getActiveRTF()
      'Debug.Print(TypeName(sc))
      'sc.highlightExecutingLine(zLN - 1)
      sH.OnHighlightLineRequested(ScriptClassName, zNN, zLN, HighlightLineReason.BreakModeExternal)
      Dim codePos = zLN & "    " & ScriptClassName & "." & zNN
      ' MAIN.lblStatus.Text = codePos & "     [" & errDesc & "]"
      'MAIN.tssl_Filename.Text = codePos & "       " & errDesc & "            " & tbDebug.Label1.Text
      Do
        isKeyPressed(Keys.F9) : isKeyPressed(Keys.F11) : isKeyPressed(Keys.F12)
        If isKeyPressed(Keys.F9) = False Then isF9Dauerton = False
        If isKeyPressed(Keys.F9) Or sH.globBreakMode = "EinzelSchritt" Then
          Dim tick As Integer
          Do
            tick += 1
            Threading.Thread.Sleep(33)
          Loop While isKeyPressed(Keys.F9) And tick < 25 And isF9Dauerton = False
          If tick >= 25 Then isF9Dauerton = True
          ziI = 2
          zC2 = zC
          ' Threading.Thread.Sleep(333)
          sH.globBreakMode = "EinzelSchritt"
          sH.OnHighlightLineRequested(ScriptClassName, "", 0, HighlightLineReason.BreakModeExternal)
          Exit Sub
        End If
        If isKeyPressed(Keys.F11) Or sH.globBreakMode = "RUN" Then
          zC2 = zC + 50
          ziI = 2
          sH.globBreakMode = "RUN"
          sH.OnHighlightLineRequested(ScriptClassName, "", 0, HighlightLineReason.BreakModeExternal)
          Exit Sub
        End If
        If isKeyPressed(Keys.F12) And isKeyPressed(Keys.ControlKey) Then
          ziQ = True
          zC2 = zC
          sH.globBreakMode = "RUN"
          sH.OnHighlightLineRequested(ScriptClassName, "", 0, HighlightLineReason.BreakModeExternal)
          Exit Sub
        End If
        Threading.Thread.Sleep(10)
        Application.DoEvents()
      Loop
    End If
  End Sub

  Function OpenFileFinder(ByVal RootFolder, Optional ByVal Filter = "*.*") Implements IScriptHelper.OpenFileFinder
    Return New cls_fileSearcher(RootFolder, Filter, ScriptClassName)
  End Function



  Sub Idle(Optional ByVal milliseconds As Integer = 10) Implements IScriptHelper.Idle
    Application.DoEvents()
    Threading.Thread.Sleep(milliseconds)
  End Sub

  ReadOnly Property FSO() As Object Implements IScriptHelper.FSO
    Get
      If _fso Is Nothing Then _fso = CreateObject("Scripting.FileSystemObject")
      Return _fso
    End Get
  End Property
  ReadOnly Property Shell() As Object Implements IScriptHelper.Shell
    Get
      If _wscriptshell Is Nothing Then _wscriptshell = CreateObject("WScript.Shell")
      Return _wscriptshell
    End Get
  End Property

  Sub setOutMonitor(ByVal txt) Implements IScriptHelper.setOutMonitor
    sH.oIntWin.SendCommand("scripthostlib06", "RuntimeObject_setOutMonitor", txt)
  End Sub

  Sub DoEvents() Implements IScriptHelper.DoEvents
    Application.DoEvents()
  End Sub

  Public Sub shellExecute(ByVal filename, Optional ByVal args = "", Optional ByVal wait = -1) Implements IScriptHelper.shellExecute
    Dim p = Process.Start(filename, args)
    If wait > -1 Then
      p.WaitForExit(wait)
      If p.HasExited = False Then p.Kill()
    End If
  End Sub

  Public Sub shellConsole(ByVal commandLine, Optional ByVal workDir = "") Implements IScriptHelper.shellConsole
    IDEHelper.executeProgramInConsole(commandLine, workDir)
  End Sub

  <Obsolete("Use getActiveTab instead")> Function getActiveRTF() Implements IScriptHelper.getActiveRTF
    Return IDEHelper.getActiveTab()
  End Function
  Function getActiveTab() Implements IScriptHelper.getActiveTab
    Return IDEHelper.getActiveTab()
  End Function

  Function getActiveTabType() Implements IScriptHelper.getActiveTabType
    Return IDEHelper.getActiveTabType()
  End Function

  Function getActiveTabFilespec() Implements IScriptHelper.getActiveTabFilespec
    Return IDEHelper.getActiveTabFilespec()
  End Function

  Sub splitFilespecData(ByVal filespec, ByRef path, ByRef name, ByRef ext) Implements IScriptHelper.splitFilespecData
    path = FP(IO.Path.GetDirectoryName(filespec), "")
    name = IO.Path.GetFileNameWithoutExtension(filespec)
    ext = IO.Path.GetExtension(filespec)
  End Sub

  Function fileExists(ByVal FileSpec) Implements IScriptHelper.fileExists
    Return IO.File.Exists(FileSpec)
  End Function
  Sub killFile(ByVal FileSpec) Implements IScriptHelper.killFile
    IO.File.Delete(FileSpec)
  End Sub

  Sub setClipboardText(ByVal txt) Implements IScriptHelper.setClipboardText
    Clipboard.Clear()
    Clipboard.SetText(txt)
  End Sub
  Function getClipboardText() Implements IScriptHelper.getClipboardText
    If Not Clipboard.ContainsText() Then Return ""
    Return Clipboard.GetText()
  End Function
  Function getCompName() Implements IScriptHelper.getCompName
    Return My.Computer.Name
  End Function
  Function getUserName() Implements IScriptHelper.getUserName
    Return My.User.Name
  End Function
  Sub playSoundWAV(ByVal fileSpec, Optional ByVal mode = 1) Implements IScriptHelper.playSoundWAV
    My.Computer.Audio.Play(fileSpec, mode)
  End Sub

  Sub TimerStart() Implements IScriptHelper.TimerStart
    mtimerStart = GetTime()
  End Sub
  Function TimerGetElapsed() Implements IScriptHelper.TimerGetElapsed
    Return GetTime() - mtimerStart
  End Function

  Sub sleep(ByVal ms) Implements IScriptHelper.sleep
    Threading.Thread.Sleep(ms)
  End Sub

  Function CreateObject(ByVal objName) Implements IScriptHelper.CreateObject
    Return Microsoft.VisualBasic.CreateObject(objName)
  End Function

  Function getExeObject(ByVal exeName) As Object Implements IScriptHelper.getExeObject
    On Error Resume Next
    Dim dump = Microsoft.VisualBasic.CreateObject("refman.application")
    Return dump.getExeObject(exeName)
  End Function


  Public Sub invalidateScriptClass(ByVal classID As String) Implements IScriptHelper.invalidateScriptClass

  End Sub

  Public Function isScriptClassLoaded(ByVal classID As String) As Boolean Implements IScriptHelper.isScriptClassLoaded

  End Function

  Public Function scriptClass(ByVal classID As String) As Object Implements IScriptHelper.scriptClass
    Return sH.scriptClass(classID)
  End Function

  Public Function TryGetScriptClass(ByVal classID As String, ByRef ClassRef As Object) As Boolean Implements IScriptHelper.TryGetScriptClass
    ClassRef = sH.scriptClass(classID)
  End Function
  Public Function newScriptClass(ByVal id As String, ByVal ParamArray newParams() As String) As Object Implements IScriptHelper.newScriptClass

  End Function

  'Function getTabPage(ByVal id) As Object
  '  If MAIN.TabControl1.TabPages.ContainsKey("script_" + id.tolower) Then
  '    Return MAIN.TabControl1.TabPages("script_" + id.tolower)
  '  Else
  '    MAIN.TabControl1.TabPages.Add("script_" + id, id)
  '    Return MAIN.TabControl1.TabPages("script_" + id.tolower)
  '  End If
  'End Function

  Function getDialogFormRef(ByVal id) As Object Implements IScriptHelper.getDialogFormRef
    Return IDEHelper.CreateDockWindow(id)
  End Function

  Function getDockPanelRef(ByVal strPanelType, Optional ByVal strPanelName = "") As Object Implements IScriptHelper.getDockPanelRef
    If strPanelName <> "" Then strPanelType += "|##|" + strPanelName
    Return IDEHelper.getInternalDockWindow(strPanelType)
  End Function

  Function fileGetContents(ByVal filespec) Implements IScriptHelper.fileGetContents
    On Error Resume Next
    fileGetContents = ""
    fileGetContents = IO.File.ReadAllText(filespec)
  End Function

  Function filePutContents(ByVal filespec, ByVal content, Optional ByVal append = False) Implements IScriptHelper.filePutContents
    If append Then
      IO.File.AppendAllText(filespec, content)
    Else
      IO.File.WriteAllText(filespec, content)
    End If
  End Function

  Sub SendKeys(ByVal key, Optional ByVal wait = True) Implements IScriptHelper.SendKeys
    My.Computer.Keyboard.SendKeys(key, wait)
  End Sub

  Sub stackPush(ByVal entryName) Implements IScriptHelper.stackPush
    On Error Resume Next
    sH.traceStack.Push(ScriptClassName + vbTab + entryName)
    If sH.dumpStackToTrace Then TT.Write("Stack: ", getCallStack())
  End Sub
  Sub stackPop() Implements IScriptHelper.stackPop
    On Error Resume Next
    sH.traceStack.Pop()
    If sH.dumpStackToTrace Then TT.Write("Stack: ", getCallStack())
  End Sub

  Function getCallStack() Implements IScriptHelper.getCallStack
    On Error Resume Next
    Dim st As New StackTrace(2)
    Dim out As New System.Text.StringBuilder
    out.AppendLine(Join(sH.traceStack.ToArray, vbNewLine))
    out.AppendLine()
    For i = 0 To st.FrameCount - 1
      out.AppendLine(st.GetFrame(i).GetMethod.Name)
    Next

    Return out.ToString
  End Function

  Sub releaseMyRef() Implements IScriptHelper.releaseMyRef
  End Sub
  Sub releaseScriptClass(ByVal className As String) Implements IScriptHelper.releaseScriptClass
  End Sub

  Function http_get(ByVal strURL, Optional ByVal cookies = "") Implements IScriptHelper.http_get
    Return TwAjax.getUrlContent(strURL)
  End Function
  Function http_post(ByVal strURL, ByVal strPostData, Optional ByVal cookies = "") Implements IScriptHelper.http_post
    Return TwAjax.postUrl(strURL, strPostData)
  End Function
  Function TwGetFileList(ByVal strAppName, ByVal strFileName) Implements IScriptHelper.TwGetFileList
    Return TwAjax.listDir(strAppName)
  End Function
  Function TwReadFile(ByVal strAppName, ByVal strFileName) Implements IScriptHelper.TwReadFile
    Return TwAjax.ReadFile(strAppName, strFileName)
  End Function
  Function TwSaveFile(ByVal strAppName, ByVal strFileName, ByVal strContent) Implements IScriptHelper.TwSaveFile
    Dim errMes As String = ""
    TwAjax.SaveFile(strAppName, strFileName, strContent, errMes)
    Return errMes
  End Function
  Function UrlEncode(ByVal str) Implements IScriptHelper.UrlEncode
    Return Web.HttpUtility.UrlEncode(str)
  End Function
  Function UrlDecode(ByVal str) Implements IScriptHelper.UrlDecode
    Return Web.HttpUtility.UrlDecode(str)
  End Function

  'Sub BindSocket(ByVal ip, ByVal port, ByVal callbackPrefix) 
  '  Dim s As New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.IP)
  '  s.Bind(New Net.IPEndPoint(Net.IPAddress.Parse(ip), port))
  '  s.Listen(5)
  '  Dim ref = scriptClass(ScriptClassName)
  '  Do
  '    Dim acceptSock = s.Accept()

  '    CallByName(ref, callbackPrefix + "Accepted", CallType.Method, acceptSock)
  '    acceptSock.Close()
  '  Loop
  'End Sub

  Function getAsyncKeyState(ByVal nVirtKey As Keys) As Boolean Implements IScriptHelper.getAsyncKeyState
    On Error Resume Next
    Return isKeyPressed(nVirtKey)
  End Function

  Function GetArgbColor(ByVal htmlColor) As Object Implements IScriptHelper.GetArgbColor
    On Error Resume Next
    Return ColorTranslator.FromHtml(CStr(htmlColor)).ToArgb
  End Function

  Sub setBgColor(ByVal el, ByVal htmlColor) Implements IScriptHelper.setBgColor
    On Error Resume Next
    el.backcolor = ColorTranslator.FromHtml(CStr(htmlColor))
  End Sub
  Sub setFgColor(ByVal el, ByVal htmlColor) Implements IScriptHelper.setFgColor
    On Error Resume Next
    el.Forecolor = ColorTranslator.FromHtml(CStr(htmlColor))
  End Sub

  Function MD5(ByVal strToHash As String) As String Implements IScriptHelper.MD5
    Return cls_scriptHelper.getMD5Hash(strToHash)
  End Function

  Shared Function getMD5Hash(ByVal strToHash As String) As String
    Dim md5Obj As New Security.Cryptography.MD5CryptoServiceProvider
    Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

    bytesToHash = md5Obj.ComputeHash(bytesToHash)

    Dim strResult As String = ""

    For Each b As Byte In bytesToHash
      strResult += b.ToString("x2")
    Next

    Return strResult
  End Function

  Public Sub New()
    MyBase.New()
  End Sub

  Public Function GetImageCached(ByVal strURL As String) As System.Drawing.Image Implements IScriptHelper.GetImageCached
    Try
      If strURL.StartsWith("http") Then
        Dim cacheFile = ParaService.SettingsFolder + "iconCache\" + cls_scriptHelper.getMD5Hash(strURL) + ".png"
        If IO.File.Exists(cacheFile) Then
          GetImageCached = Image.FromFile(cacheFile)
        Else
          GetImageCached = ImageFromWeb(strURL)
          GetImageCached.Save(cacheFile)
        End If
      Else
        GetImageCached = Image.FromFile(strURL)
      End If
    Catch ex As Exception
    End Try
  End Function

  Public Function GetExePath(ByVal exeName As String) As String Implements IScriptHelper.GetExePath
    Return ExePath(exeName)
  End Function

  Public Sub printLineReset() Implements IScriptHelper.printLineReset
    sH.oIntWin.SendCommand("scripthostlib06", "RuntimeObject_printLineReset", "")
  End Sub

  Public Sub traceClear() Implements IScriptHelper.traceClear
    sH.oIntWin.SendCommand("scripthostlib06", "RuntimeObject_traceClear", "")
  End Sub

  Public ReadOnly Property InterProc() As Object Implements IScriptHelper.InterProc
    Get
      Return sH.oIntWin
    End Get
  End Property

  'Public Function globPara(ByVal paraSource As String) As cls_globPara Implements IScriptHelper.globPara
  '  If paraSource.ToLower = "ide" Then
  '    If isIDEMode Then
  '      Return IDEHelper.Glob
  '    Else
  '      Throw New InvalidOperationException("Nur möglich wenn das Skript in der IDE läuft.")
  '    End If
  '  Else
  '    Return New cls_globPara(paraSource)
  '  End If
  'End Function

  Public Function newGlobPara() As cls_globPara Implements IScriptHelper.newGlobPara
    Return IDEHelper.Glob
  End Function

  '...from http://www.codeproject.com/KB/cs/HexDump.aspx
  Public Function HexDump(ByVal bytes As Byte()) As String Implements IScriptHelper.HexDump
    If bytes Is Nothing Then
      Return "<null>"
    End If
    Dim len As Integer = bytes.Length
    Dim result As New System.Text.StringBuilder(CInt((len + 15) / 16) * 78)
    Dim chars As Char() = New Char(77) {}
    ' fill all with blanks
    For i As Integer = 0 To 74
      chars(i) = " "c
    Next
    chars(76) = ControlChars.Cr
    chars(77) = ControlChars.Lf

    For i1 As Integer = 0 To len - 1 Step 16
      chars(0) = HexChar(i1 >> 28)
      chars(1) = HexChar(i1 >> 24)
      chars(2) = HexChar(i1 >> 20)
      chars(3) = HexChar(i1 >> 16)
      chars(4) = HexChar(i1 >> 12)
      chars(5) = HexChar(i1 >> 8)
      chars(6) = HexChar(i1 >> 4)
      chars(7) = HexChar(i1 >> 0)

      Dim offset1 As Integer = 11
      Dim offset2 As Integer = 60

      For i2 As Integer = 0 To 15
        If i1 + i2 >= len Then
          chars(offset1) = " "c
          chars(offset1 + 1) = " "c
          chars(offset2) = " "c
        Else
          Dim b As Byte = bytes(i1 + i2)
          chars(offset1) = HexChar(b >> 4)
          chars(offset1 + 1) = HexChar(b)
          chars(offset2) = (If(b < 32, "·"c, Chr(b)))
        End If
        offset1 += (If(i2 = 7, 4, 3))
        offset2 += 1
      Next
      result.Append(chars)
    Next
    Return result.ToString()
  End Function

  Private Function HexChar(ByVal value As Integer) As Char
    value = value And &HF
    If value >= 0 AndAlso value <= 9 Then
      Return Chr(&H30 + value) '"0"c=0x30
    Else
      Return Chr(&H41 + (value - 10)) '"A"c=0x41
    End If
  End Function

  Public Function JoinIGridLine(ByVal line As Object, Optional ByVal Delimiter As String = vbTab) As String Implements IScriptHelper.JoinIGridLine
    Dim max = line.Cells.Count - 1
    Dim out(max) As String
    For i As Integer = 0 To max
      out(i) = CStr(line.Cells(i).Value)
    Next
    Return Join(out, Delimiter)
  End Function

  Public Sub SplitToIGridLine(ByVal line As Object, ByVal input As String, Optional ByVal Delimiter As String = vbTab) Implements IScriptHelper.SplitToIGridLine
    Dim max = line.Cells.Count - 1
    Dim out() = Split(input, Delimiter)
    ReDim Preserve out(max)
    For i As Integer = 0 To max
      line.Cells(i).Value = out(i)
    Next
  End Sub

  Public Sub Igrid_get(ByVal Grid As Object, ByRef FileContent As String, Optional ByVal LineDelim As String = vbNewLine, Optional ByVal ColDelim As String = vbTab) Implements IScriptHelper.Igrid_get
    Dim max = Grid.Rows.Count - 1
    Dim out(max) As String
    For i As Integer = 0 To max
      out(i) = JoinIGridLine(Grid.Rows(i), ColDelim)
    Next
    FileContent = Join(out, LineDelim)
  End Sub

  Public Sub Igrid_put(ByVal Grid As Object, ByVal FileContent As String, Optional ByVal LineDelim As String = vbNewLine, Optional ByVal ColDelim As String = vbTab) Implements IScriptHelper.Igrid_put
    Dim out() = Split(FileContent, LineDelim)
    Grid.Rows.Clear()
    Grid.Rows.Count = out.Length
    For i As Integer = 0 To out.Length - 1
      SplitToIGridLine(Grid.Rows(i), out(i), ColDelim)
    Next
  End Sub

  Public Function CreateToolbar(ByVal ToolbarID As String) As IScriptedPanel Implements IScriptHelper.CreateToolbar
    Return Nothing
  End Function

  Public Function CreateWindow(ByVal ScriptedWindowID As String, ByVal Flags As DWndFlags, Optional ByVal DefaultPosition As Integer = 5) As IScriptedPanel Implements IScriptHelper.CreateWindow
    On Error Resume Next
    Dim wnd As ScriptWindowHelper.frmTB_scriptWin = _
          ScriptWindowHelper.ScriptWindowManager.CreateWindow(ScriptedWindowID, _scriptClassName, DWndFlags.StdWindow, 0, False).Form
    Console.WriteLine("Wnd?" & (wnd Is Nothing) & "   " & TypeName(wnd))
    If (Flags And DWndFlags.NoAutoShow) = 0 Then wnd.Show()
    wnd.Icon = FileTypeAndIcon.RegisteredFileType.GetAssociatedIcon2(Application.ExecutablePath, FileTypeAndIcon.RegisteredFileType.assoc_iconSize.SHGFI_SMALLICON)

    Return wnd.PNL
  End Function

  Public ReadOnly Property Filespec() As String Implements IScriptHelper.Filespec
    Get
      Return Application.ExecutablePath
    End Get
  End Property

  Public ReadOnly Property RuntimeMode() As RuntimeMode Implements IScriptHelper.RuntimeMode
    Get
      Return sH.HostMode
    End Get
  End Property

End Class


