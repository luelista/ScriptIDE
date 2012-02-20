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

  Dim mtimerStart As Integer
  Public Declare Function GetTime Lib "winmm.dll" Alias "timeGetTime" () As Integer
  Private _fso, _wscriptshell As Object
  'Private _idehelper As ScriptHelper_IDEHelper

  ReadOnly Property IDEHelper() As IIDEHelper Implements IScriptHelper.IDEHelper
    Get
      'If _idehelper Is Nothing Then _idehelper = New ScriptHelper_IDEHelper : _idehelper.ScriptClassName = _scriptClassName
      Return app_main.IdeHelper
    End Get
  End Property

  ReadOnly Property ScriptClassName() As String Implements IScriptHelper.ScriptClassName
    Get
      Return _scriptClassName
    End Get
  End Property
  ReadOnly Property Filespec() As String Implements IScriptHelper.Filespec
    Get
      Return _scriptFilespec
    End Get
  End Property

  Public Function appPath() As String
    Return FP(My.Computer.FileSystem.GetParentPath(Application.ExecutablePath))
  End Function


  Public Function FP(ByVal path As String, Optional ByVal fileName As String = "") Implements IScriptHelper.FP
    FP = path + IIf(path.EndsWith("\"), "", "\") + If(fileName.StartsWith("\"), fileName.Substring(1), fileName)
  End Function
  Public Function FPUNIX(ByVal path As String, Optional ByVal fileName As String = "") Implements IScriptHelper.FPUNIX
    FPUNIX = path + IIf(path.EndsWith("/"), "", "/") + If(fileName.StartsWith("/"), fileName.Substring(1), fileName)
  End Function

  Function FilePathCombine(ByVal f1, ByVal f2, Optional ByVal isUnix = False) Implements IScriptHelper.FilePathCombine
    If isUnix Then Return FPUNIX(f1, f2) Else Return FP(f1, f2)
  End Function

  Sub trace(ByVal zLN, ByVal zNN, ByVal para1, Optional ByVal para2 = "", Optional ByVal typ = "info") Implements IScriptHelper.trace
    Dim codeLink = "_|°|_cLink_|°|_" + "scriptClass" & "_|°|_" & ScriptClassName & "?" & zLN & "_|°|_" & zNN & ""
    TT.Write(para1, para2, typ, codeLink)
  End Sub

  Sub traceClear() Implements IScriptHelper.traceClear
    If MAIN IsNot Nothing Then MAIN.IGrid2.Rows.Clear()
    TT.Write("--- TraceClear ---", _scriptClassName, "ini")
  End Sub

  Sub printLine(ByVal zLN, ByVal zNN, ByVal index, ByVal title, ByVal data) Implements IScriptHelper.printLine
    Dim codeLink = "_|°|_cLink_|°|_" + "scriptClass" & "_|°|_" & ScriptClassName & "?" & zLN & "_|°|_" & zNN & ""
    TT.printLine(index, title, data, codeLink)
  End Sub

  Sub printLineReset() Implements IScriptHelper.printLineReset
    If tbPrintline IsNot Nothing Then tbPrintline.resetPrintLines()
    TT.Write("--- PrintLineReset ---", _scriptClassName, "ini")
  End Sub

  Function NavigateScript(ByVal className As String, ByVal cmdName As String, ByVal flags As String, ByVal target As String) As String
    If sys_interproc.getWindow("siDebug_" + className + "_") <> IntPtr.Zero Then
      NavigateScript = ship.GetData("siDebug_" + className + "_", "Navigate", className + "|##|" + cmdName + "|##|" + Replace(flags, "|##|", "| # # |") + "|##|" + target)
      Exit Function
    End If

    Dim cls = scriptClass(className)
    Try
      NavigateScript = cls.Navigate(cmdName, flags, target)
    Catch ex As Exception
      NavigateScript = "ERR: scriptClass doesn't provide method 'Navigate' or Program not loaded"
    End Try
  End Function


  Sub CB(<Out()> ByRef zLN, ByVal zNN, <Out()> ByRef zC, <Out()> ByRef zC2, <Out()> ByRef zErr, <Out()> ByRef ziI, <Out()> ByRef ziE, <Out()> ByRef ziQ, Optional ByVal onStopCode = False) Implements IScriptHelper.CB
    'Stop

    CB2(zLN, zNN, zC, zC2, zErr.Number, zErr.Description, ziI, ziE, ziQ, onStopCode)
    zErr.Clear()

  End Sub
  Sub CB2(<Out()> ByRef zLN As Integer, ByVal zNN As String, <Out()> ByRef zC As Integer, <Out()> ByRef zC2 As Integer, ByVal errorNum As Integer, ByVal errorDesc As String, <Out()> ByRef ziI As Integer, <Out()> ByRef ziE As String, <Out()> ByRef ziQ As Boolean, Optional ByVal onStopCode As Boolean = False) Implements IScriptHelper.CB2
    On Error Resume Next
    If dumpStackToTrace Then TT.Write(zLN, zNN, "callBack ln=" & zLN, "c=" & zC & "  c2=" & zC2 & "   err=" & errorNum & "  i=" & ziI & "  e=" & ziE & "  q=" & ziQ)
    Dim errDesc = ""
    Dim lineStr As String = If(zLN < 199, "*" & zLN, CStr(zLN - 199))
    If errorNum <> 0 Then
      Dim errText = "Line=" & lineStr & " File=" & ScriptClassName & " Func=" & zNN & vbNewLine & _
                              errorNum & " - " & errorDesc & vbNewLine & _
                              getCallStack()
      errDesc = errorDesc

      'z.Zt. nicht verwendet!
      ScriptHost.Instance.OnScriptError(ScriptClassName, zNN, zLN - 199, "Runtime", errorNum, errorDesc)

      onScriptError(ScriptHost.Instance.expandScriptClassName(ScriptClassName), zLN - 199, errText, _isLocalSilentMode)

      If ScriptHost.Instance.SilentMode = False And _isLocalSilentMode = False Then _
        ScriptHost.Instance.globBreakMode(ScriptClassName) = "BREAK"

    End If
    Application.DoEvents()
    ziI = 2
    zC2 = zC + 50
    moveScriptRunningLabelSpace()

    If onStopCode Then Me.trace(zLN, zNN, "STOP-Anweisung aufgetreten ...", , "err")
    If ScriptHost.Instance.SilentMode Or _isLocalSilentMode Then Exit Sub 'bei silent Mode alles Tracen, aber nie anhalten
    If onStopCode Then
      errDesc = "S T O P - Code"
      ScriptHost.Instance.globBreakMode(ScriptClassName) = "BREAK"
    End If
    If isKeyPressed(Keys.F12) Or ScriptHost.Instance.globBreakMode(ScriptClassName) = "EinzelSchritt" Then
      ScriptHost.Instance.globBreakMode(ScriptClassName) = "BREAK"
    End If
    ziQ = False
    If (isKeyPressed(Keys.F12) And isKeyPressed(Keys.ControlKey)) Or ScriptHost.Instance.globBreakMode(ScriptClassName) = "QUIT" Then  'isKeyPressed(Keys.F11) Then
      ziQ = True
      zC2 = zC
      ScriptHost.Instance.globBreakMode(ScriptClassName) = ""
      Exit Sub
    End If

    If ScriptHost.Instance.globBreakMode(ScriptClassName) = "BREAK" Then
      'MAIN.qq_txtOutMonitor.Text = getCallStack()
      Dim fileName = ScriptHost.Instance.expandScriptClassName(ScriptClassName)
      Debug.Print("Goto: " & fileName & "   " & lineStr)
      'gotoNote("loc:/" + fileName)
      'Dim sc = getActiveRTF()
      'Debug.Print(TypeName(sc))
      'sc.highlightExecutingLine(zLN - 1)
      If zLN > 199 Then
        ScriptHost.Instance.OnHighlightLineRequested(_scriptFilespec, zNN, zLN - 199, HighlightLineReason.BreakMode)
      End If
      Dim codePos = zLN & "    " & ScriptClassName & "." & lineStr
      MAIN.lblStatus.Text = codePos & "     [" & errDesc & "]"
      'MAIN.tssl_Filename.Text = codePos & "       " & errDesc & "            " & tbDebug.Label1.Text
      Do
        isKeyPressed(Keys.F9) : isKeyPressed(Keys.F11) : isKeyPressed(Keys.F12)
        If isKeyPressed(Keys.F9) = False Then isF9Dauerton = False
        If isKeyPressed(Keys.F9) Or ScriptHost.Instance.globBreakMode(ScriptClassName) = "EinzelSchritt" Then
          Dim tick As Integer
          Do
            tick += 1
            Threading.Thread.Sleep(33)
          Loop While isKeyPressed(Keys.F9) And tick < 25 And isF9Dauerton = False
          If tick >= 25 Then isF9Dauerton = True
          ziI = 2
          zC2 = zC
          ' Threading.Thread.Sleep(333)
          ScriptHost.Instance.globBreakMode(ScriptClassName) = "EinzelSchritt"
          ScriptHost.Instance.OnHighlightLineRequested(ScriptClassName, "", 0, HighlightLineReason.BreakMode)
          Exit Sub
        End If
        If isKeyPressed(Keys.F11) Or ScriptHost.Instance.globBreakMode(ScriptClassName) = "" Then
          zC2 = zC + 50
          ziI = 2
          ScriptHost.Instance.globBreakMode(ScriptClassName) = ""
          ScriptHost.Instance.OnHighlightLineRequested(ScriptClassName, "", 0, HighlightLineReason.BreakMode)
          Exit Sub
        End If
        If isKeyPressed(Keys.F12) And isKeyPressed(Keys.ControlKey) Then
          ziQ = True
          zC2 = zC
          ScriptHost.Instance.globBreakMode(ScriptClassName) = ""
          ScriptHost.Instance.OnHighlightLineRequested(ScriptClassName, "", 0, HighlightLineReason.BreakMode)
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
    On Error Resume Next
    If MAIN IsNot Nothing Then MAIN.setOUT(ScriptClassName, txt)
    'MAIN.TabControl1.SelectedIndex = 5
    'MAIN.SplitContainer2.Panel2Collapsed = False
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

  Public Sub invalidateScriptClass(ByVal id As String) Implements IScriptHelper.invalidateScriptClass
    ScriptHost.Instance.RecompileScriptClass(id)
  End Sub

  Public Function TryGetScriptClass(ByVal classID As String, ByRef ClassRef As Object) As Boolean Implements IScriptHelper.TryGetScriptClass
    With ScriptHost.Instance
      If .isScriptClassLoaded(classID) Then
        Return .scriptClass(classID)
      End If
    End With
  End Function

  Public Function isScriptClassLoaded(ByVal id As String) As Boolean Implements IScriptHelper.isScriptClassLoaded
    Return ScriptHost.Instance.isScriptClassLoaded(id)
  End Function

  Public Function scriptClass(ByVal id As String) As Object Implements IScriptHelper.scriptClass
    Return ScriptHost.Instance.scriptClass(id)
  End Function

  Public Function newScriptClass(ByVal id As String, ByVal ParamArray newParams() As String) As Object Implements IScriptHelper.newScriptClass
    Dim sc = ScriptHost.Instance.getScriptClassHost(id)

    Dim inst = sc.getNewClassRef(_scriptInst)
    Try
      inst.GetType().InvokeMember("OnNew", Reflection.BindingFlags.InvokeMethod, Nothing, inst, newParams)
    Catch ex As Exception
      TT.Write("" & id & " - exception in constructor", ex.ToString, "err")
    End Try

    'CallByName(inst, "OnNew", CallType.Method, newParams)

    Return inst
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

  Public Function CreateWindow(ByVal ScriptedWindowID As String, ByVal Flags As DWndFlags, Optional ByVal DefaultPosition As Integer = 5) As IScriptedPanel Implements IScriptHelper.CreateWindow
    On Error Resume Next
    Dim hasBeenCreated As Boolean
    Dim panelRef = ScriptWindowHelper.ScriptWindowManager.CreateWindow(ScriptedWindowID, _scriptFilespec, Flags, DefaultPosition, hasBeenCreated)
    Dim wnd As ScriptWindowHelper.frmTB_scriptWin = panelRef.Form

    Return wnd.PNL
  End Function

  Public Function CreateToolbar(ByVal ToolbarID As String) As IScriptedPanel Implements IScriptHelper.CreateToolbar
    Dim tb = IDEHelper.CreateToolbar(ToolbarID, "bottom", -1, -1)
    tb.className = _scriptFilespec
    Return tb
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
    traceStack.Push(ScriptClassName + vbTab + entryName)
    If dumpStackToTrace Then TT.Write("Stack: ", getCallStack(), "dump")
  End Sub
  Sub stackPop() Implements IScriptHelper.stackPop
    On Error Resume Next
    traceStack.Pop()
    If dumpStackToTrace Then TT.Write("Stack: ", getCallStack(), "dump")
  End Sub

  Function getCallStack() Implements IScriptHelper.getCallStack
    On Error Resume Next
    Dim st As New StackTrace(2)
    Dim out As New System.Text.StringBuilder
    out.AppendLine(Join(traceStack.ToArray, vbNewLine))
    out.AppendLine()
    For i = 0 To st.FrameCount - 1
      out.AppendLine(st.GetFrame(i).GetMethod.Name)
    Next

    Return out.ToString
  End Function

  Sub releaseMyRef() Implements IScriptHelper.releaseMyRef
    If scriptClassDict.ContainsKey(ScriptClassName.ToUpper) Then
      Dim obj = scriptClassDict(ScriptClassName.ToUpper)
      scriptClassDict.Remove(ScriptClassName.ToUpper)
      obj.terminateScript()
    End If
  End Sub
  Sub releaseScriptClass(ByVal className As String) Implements IScriptHelper.releaseScriptClass
    If scriptClassDict.ContainsKey(className.ToUpper) Then
      Dim obj = scriptClassDict(className.ToUpper)
      scriptClassDict.Remove(className.ToUpper)
      obj.terminateScript()
    End If
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

  ''' <summary>
  ''' Eventhandler für skript hinzufügen -- achtung: geht nur für die hier definierten EventHandlerTypen
  ''' </summary>
  ''' <param name="obj">Control</param>
  ''' <param name="eventName">String</param>
  ''' <remarks></remarks>
  Sub handleEvent(ByVal obj, ByVal eventName)
    Dim ref = scriptClass(ScriptClassName)

    Dim eventTyp = obj.GetType.GetEvent(eventName)
    Dim t As Type = GetType(EventArgs)
    Dim del As [Delegate]
    Dim handlerTyp = eventTyp.EventHandlerType.Name.ToLower
    Select Case handlerTyp
      Case "eventhandler"
        del = New EventHandler(AddressOf ref.onUnknownEvent)
      Case "mouseeventhandler"
        del = New MouseEventHandler(AddressOf ref.onUnknownEvent)
      Case "keyeventhandler"
        del = New KeyEventHandler(AddressOf ref.onUnknownEvent)
      Case "formclosingeventhandler"
        del = New FormClosingEventHandler(AddressOf ref.onUnknownEvent)
      Case "formclosedeventhandler"
        del = New FormClosedEventHandler(AddressOf ref.onUnknownEvent)
      Case "linkclickedeventhandler"
        del = New LinkClickedEventHandler(AddressOf ref.onUnknownEvent)
      Case "toolstripitemclickedeventhandler"
        del = New ToolStripItemClickedEventHandler(AddressOf ref.onUnknownEvent)
      Case "treenodemouseclickeventhandler"
        del = New TreeNodeMouseClickEventHandler(AddressOf ref.onUnknownEvent)
      Case "webbrowsernavigatingeventhandler"
        del = New WebBrowserNavigatingEventHandler(AddressOf ref.onUnknownEvent)
      Case "webbrowsernavigatedeventhandler"
        del = New WebBrowserNavigatedEventHandler(AddressOf ref.onUnknownEvent)
    End Select
    eventTyp.AddEventHandler(obj, del)

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

  Public ReadOnly Property InterProc() As Object Implements IScriptHelper.InterProc
    Get
      Return InterProc.oIntWin
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

  Dim _globObj As cls_globPara
  Public Function newGlobPara() As cls_globPara Implements IScriptHelper.newGlobPara
    If isIDEMode Then
      If _globObj Is Nothing Then
        _globObj = New cls_globPara(IDEHelper.GetSettingsFolder() + "scriptPara\" + ScriptClassName + ".para.txt")
      End If
      Return _globObj
    Else
      Return IDEHelper.Glob
    End If
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

  'Public Function JoinIGridLine(ByVal line As TenTec.Windows.iGridLib.iGRow, Optional ByVal Delimiter As String = vbTab) As String Implements IScriptHelper.JoinIGridLine
  Public Function JoinIGridLine(ByVal objline As Object, Optional ByVal Delimiter As String = vbTab) As String Implements IScriptHelper.JoinIGridLine
    Dim line As TenTec.Windows.iGridLib.iGRow = objline
    Dim max = line.Cells.Count - 1
    Dim out(max) As String
    For i As Integer = 0 To max
      out(i) = CStr(line.Cells(i).Value)
    Next
    Return Join(out, Delimiter)
  End Function

  Public Sub SplitToIGridLine(ByVal objline As Object, ByVal input As String, Optional ByVal Delimiter As String = vbTab) Implements IScriptHelper.SplitToIGridLine
    Dim line As TenTec.Windows.iGridLib.iGRow = objline
    Dim max = line.Cells.Count - 1
    Dim out() = Split(input, Delimiter)
    ReDim Preserve out(max)
    For i As Integer = 0 To max
      line.Cells(i).Value = out(i)
    Next
  End Sub

  Public Sub Igrid_get(ByVal objGrid As Object, ByRef FileContent As String, Optional ByVal LineDelim As String = vbNewLine, Optional ByVal ColDelim As String = vbTab) Implements IScriptHelper.Igrid_get
    Dim Grid As iGrid = objGrid
    Dim max = Grid.Rows.Count - 1
    Dim out(max) As String
    For i As Integer = 0 To max
      out(i) = JoinIGridLine(Grid.Rows(i), ColDelim)
    Next
    FileContent = Join(out, LineDelim)
  End Sub

  Public Sub Igrid_put(ByVal objGrid As Object, ByVal FileContent As String, Optional ByVal LineDelim As String = vbNewLine, Optional ByVal ColDelim As String = vbTab) Implements IScriptHelper.Igrid_put
    Dim Grid As iGrid = objGrid
    Dim out() = Split(FileContent, LineDelim)
    Grid.Rows.Clear()
    Grid.Rows.Count = out.Length
    For i As Integer = 0 To out.Length - 1
      SplitToIGridLine(Grid.Rows(i), out(i), ColDelim)
    Next
  End Sub

  Public ReadOnly Property RuntimeMode() As RuntimeMode Implements IScriptHelper.RuntimeMode
    Get
      Return RuntimeMode.IDE
    End Get
  End Property

End Class


