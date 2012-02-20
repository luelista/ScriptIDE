Imports System.Runtime.InteropServices

Public Enum HighlightLineReason
  RuntimeError
  CompileError
  BreakMode
  BreakModeExternal
End Enum

Public Class ScriptHost


  'Public Event ScriptWindowEvent(ByVal WinID As String, ByVal ObjectType As String, ByVal EventArgs As ScriptEventArgs)
  Public Event ScriptError(ByVal className As String, ByVal functionName As String, ByVal lineNumber As Integer, ByVal errorType As String, ByVal errorNumber As Integer, ByVal errorDescription As String)
  Public Event BreakModeChanged(ByVal className As String, ByRef breakState As String)
  Public Event QueryBreakpoints(ByVal className As String, ByVal interprocSourceWindow As String, ByRef bb() As Boolean)
  'Public Event ScriptWindowClosed(ByVal WinID As String, ByVal WinRef As frmTB_scriptWin)
  Public Event HighlightLineRequested(ByVal className As String, ByVal functionName As String, ByVal lineNumber As Integer, ByVal reason As HighlightLineReason)

  Private _breakMode As New Dictionary(Of String, String)


  Friend Sub OnScriptWindowEvent(ByVal WinID As String, ByVal ObjectType As String, ByVal EventArgs As ScriptEventArgs)
    TT.Write("ScriptEvent: " + WinID + " -> " + EventArgs.ClassName + " (on" + ObjectType + "Event)", "Sub " + EventArgs.ID + "_" + EventArgs.EventName + "(e)", "event")
    'TT.Write("ScriptEvent for Class:", EventArgs.ClassName, "event")
    Dim startTime = cls_scriptHelper.GetTime()
    'RaiseEvent ScriptWindowEvent(WinID, ObjectType, EventArgs)
    If Not String.IsNullOrEmpty(EventArgs.ClassName) Then
      Try
        Dim ref = scriptClass(EventArgs.ClassName)
        If ref Is Nothing Then Exit Sub
        Dim args() As Object = New Object() {EventArgs}
        Dim argTypes() As Type = New Type() {GetType(EventArgs)}
        Try
          'CallByName(ref, EventArgs.ID.Substring(4) + "_" + EventArgs.EventName, CallType.Method, EventArgs)
          Microsoft.VisualBasic.CompilerServices.LateBinding.LateCall(ref, Nothing, EventArgs.ID.Substring(4) + "_" + EventArgs.EventName, args, Nothing, Nothing)
          'TT.Write("Called:", "1")
          Exit Sub
        Catch : End Try
        Try
          'CallByName(ref, EventArgs.ID + "_" + EventArgs.EventName, CallType.Method, EventArgs)
          Microsoft.VisualBasic.CompilerServices.LateBinding.LateCall(ref, Nothing, EventArgs.ID + "_" + EventArgs.EventName, args, Nothing, Nothing)
          'TT.Write("Called:", "2")
          Exit Sub
        Catch : End Try
        Try
          'CallByName(ref, "on" + ObjectType + "Event", CallType.Method, EventArgs)
          Microsoft.VisualBasic.CompilerServices.LateBinding.LateCall(ref, Nothing, "on" + ObjectType + "Event", args, Nothing, Nothing)
          'TT.Write("Called:", "3")
          Exit Sub
        Catch : End Try
        Try
          'CallByName(ref, "globalEvent", CallType.Method, EventArgs)
          'Microsoft.VisualBasic.CompilerServices.LateBinding.LateCall(ref, Nothing, EventArgs.ID + "_" + EventArgs.EventName, args, Nothing, Nothing)
          ref.globalEvent(EventArgs)
          'TT.Write("Called:", "4")
          Exit Sub
        Catch : End Try
      Catch : End Try
    End If
    TT.Write("ON " + EventArgs.ID + "_" + EventArgs.EventName, "was unhandled " & (cls_scriptHelper.GetTime() - startTime) & "ms")
  End Sub

  Friend Sub OnScriptError(ByVal className As String, ByVal functionName As String, ByVal lineNumber As String, ByVal errorType As String, ByVal errorNumber As Integer, ByVal errorDescription As String)
    RaiseEvent ScriptError(className, functionName, lineNumber, errorType, errorNumber, errorDescription)
  End Sub

  Friend Sub OnBreakModeChanged(ByVal className As String, ByRef breakState As String)
    RaiseEvent BreakModeChanged(className, breakState)
  End Sub

  Friend Sub OnQueryBreakpoints(ByVal className As String, ByVal interprocSourceWindow As String, ByRef bb() As Boolean)
    RaiseEvent QueryBreakpoints(className, interprocSourceWindow, bb)
  End Sub

  'Friend Sub OnScriptWindowClosed(ByVal WinID As String, ByVal WinRef As frmTB_scriptWin)
  '  RaiseEvent ScriptWindowClosed(WinID, WinRef)
  'End Sub

  Friend Sub OnHighlightLineRequested(ByVal className As String, ByVal functionName As String, ByVal lineNumber As Integer, ByVal reason As HighlightLineReason)
    RaiseEvent HighlightLineRequested(className, functionName, lineNumber, reason)
  End Sub

  Sub setIdeHelper(ByVal helperObject As IIDEHelper)
    IdeHelper = helperObject
  End Sub

  Function isScriptClassLoaded(ByVal className As String) As Boolean
    Return scriptClassDict.ContainsKey(className.ToUpper)
  End Function

  Function getScriptSearchPath() As String()
    Return Split(ParaService.Glob.para("scriptSearchPath", ParaService.SettingsFolder + "scriptClass\" + vbNewLine), vbNewLine)
  End Function

  Function expandScriptClassName(ByVal className As String) As String
    If String.IsNullOrEmpty(className) Then Return ""
    Dim value As String
    If scriptClassSearchCache.TryGetValue(className.ToLower, value) = True Then Return value

    Dim retVal As String = ""
    Dim className2 = IO.Path.GetFileNameWithoutExtension(className)
    If IO.File.Exists(className) = True Then retVal = className : GoTo BreakAll

    Dim list() As String = getScriptSearchPath()
    Dim checkExt() = New String() {".nsa", ".ns", ".nsvb", ".vb", ".nscs", ".cs", ".vbs"}
    For Each folder In list
      Dim fileSpec As String = ZZ.FP(folder, className2)
      For Each ext In checkExt
        If IO.File.Exists(fileSpec + ext) Then retVal = fileSpec + ext : GoTo BreakAll
      Next
    Next

BreakAll:  'blöder GoTo  --  warum gibts in VB kein Exit For für verschachtelte schleifen ???
    If retVal <> "" Then
      scriptClassSearchCache(className.ToLower) = retVal
      scriptClassSearchCache(className2.ToLower) = retVal
    End If
    Return retVal
  End Function
  Function scriptClass(ByVal className As String, Optional ByVal forceRecompile As Boolean = False) As Object 'Implements _IApplication.scriptClass
    Dim sc = getScriptClassHost(className, forceRecompile)
    If sc IsNot Nothing AndAlso sc.isIniDone() Then
      Return sc.getClassRef()
    Else
      Return Nothing
    End If
  End Function

  Sub RecompileScriptClass(ByVal className As String)
    Dim scriptFile = ScriptHost.Instance.expandScriptClassName(className)
    Dim para As IScriptClassHost
    If scriptClassDict.TryGetValue(scriptFile.ToUpper, para) Then
      para.invalidate()
    End If
  End Sub

  'releaseMode=4=cls_preprocVB2.PPD_DEBUG
  Function getScriptClassHost(ByVal className As String, Optional ByVal forceRecompile As Boolean = False, Optional ByVal releaseMode As Integer = 4) As IScriptClassHost
    Dim scriptFile = ScriptHost.Instance.expandScriptClassName(className)
    If String.IsNullOrEmpty(scriptFile) Then Return Nothing

    Dim sc As IScriptClassHost
    If scriptClassDict.TryGetValue(scriptFile.ToUpper, sc) Then
      If forceRecompile = False AndAlso sc.checkForNewerFile() = False Then
        Return sc
      End If
      scriptClassDict.Remove(scriptFile.ToUpper)
      sc.terminateScript()
    End If

    ' Dim sc As IScriptClassHost
    If scriptFile.EndsWith(".vbs") Then
      sc = New scHostVBS(scriptFile)
    Else
      sc = New scHostNET2(scriptFile, releaseMode)
    End If
    If sc.debugMode = RuntimeMode.IDE Then
      sc.initScriptHost()
      If sc.isIniDone() Then scriptClassDict.Add(scriptFile.ToUpper, sc)
    End If
    Return sc
  End Function

  Property InformationWindowVisible() As Boolean
    Get
      If MAIN Is Nothing Then Return False
      Return MAIN.Visible
    End Get
    Set(ByVal value As Boolean)
      If IdeHelper IsNot Nothing AndAlso IdeHelper.IsStartup Then Exit Property
      If MAIN Is Nothing And value = False Then Exit Property
      If MAIN Is Nothing And value = True Then
        MAIN = New frmTB_debug()
      End If
      If value = True Then
        MAIN.Show() : MAIN.Activate()
      Else
        MAIN.Hide()
      End If
    End Set
  End Property

  Property ErrorListVisible() As Boolean
    Get
      If tbCompileErrors Is Nothing Then Return False
      Return tbCompileErrors.Visible
    End Get
    Set(ByVal value As Boolean)
      If IdeHelper IsNot Nothing AndAlso IdeHelper.IsStartup Then Exit Property
      If tbCompileErrors Is Nothing And value = False Then Exit Property
      If tbCompileErrors Is Nothing And value = True Then
        tbCompileErrors = New frmTB_compileErrors()
      End If
      If value = True Then
        tbCompileErrors.Show() : tbCompileErrors.Activate()
      Else
        tbCompileErrors.Hide()
      End If
    End Set
  End Property

  Property PrintLineWndVisible() As Boolean
    Get
      If tbPrintline Is Nothing Then Return False
      Return tbPrintline.Visible
    End Get
    Set(ByVal value As Boolean)
      If IdeHelper IsNot Nothing AndAlso IdeHelper.IsStartup Then Exit Property
      If tbPrintline Is Nothing And value = False Then Exit Property
      If tbPrintline Is Nothing And value = True Then
        tbPrintline = New frmTB_tracePrintLine()
      End If
      If value = True Then
        tbPrintline.Show() : tbPrintline.Activate()
      Else
        tbPrintline.Hide()
      End If
    End Set
  End Property

  Sub ActivateInformationWindow(Optional ByVal page As String = "")
    If IdeHelper IsNot Nothing AndAlso IdeHelper.IsStartup Then Exit Sub
    If page = "compileErrors" Then
      ErrorListVisible = True
      Exit Sub
    End If
    InformationWindowVisible = True
    MAIN.Activate()
    If page <> "" Then
      For Each tp As TabPage In MAIN.TabControl1.TabPages
        If tp.Text = page Then MAIN.TabControl1.SelectedTab = tp
      Next
    End If
  End Sub



  Function getInformationWindowRef() As frmTB_debug
    If MAIN Is Nothing Then
      MAIN = New frmTB_debug()
    End If
    Return MAIN
  End Function

  Function getErrorListRef() As frmTB_compileErrors
    If tbCompileErrors Is Nothing Then
      tbCompileErrors = New frmTB_compileErrors()
    End If
    Return tbCompileErrors
  End Function

  Function getPrintlineWndRef() As frmTB_tracePrintLine
    If tbPrintline Is Nothing Then
      tbPrintline = New frmTB_tracePrintLine()
    End If
    Return tbPrintline
  End Function

  Sub initIDEMode()
    isIDEMode = True
    initScriptHost()
  End Sub

  Private Sub initScriptHost()
    AddHandler ScriptWindowHelper.ScriptWindowManager.ScriptWindowEvent, AddressOf OnScriptWindowEvent
    interproc_init()
    CoreTraceListener.Initialize()
  End Sub

  Public ReadOnly Property HostMode() As RuntimeMode
    Get
      Return RuntimeMode.IDE
    End Get
  End Property


  Private p_SilentMode As Boolean
  Public Property SilentMode() As Boolean
    Get
      Return p_SilentMode
    End Get
    Set(ByVal value As Boolean)
      p_SilentMode = value
    End Set
  End Property


  Function getScriptRuntimeMode(ByVal fileSpec As String) As String
    If IO.File.Exists(fileSpec) = False Then Return ""
    Using sr As New System.IO.StreamReader(fileSpec)
      While Not sr.EndOfStream
        Dim rl = UCase(Trim(sr.ReadLine))
        If rl.StartsWith("#RUNTIME ") Then Return Trim(rl.Substring(9))
      End While
    End Using
    Return "IDE"
  End Function

  Sub setBreakPoint(ByVal className As String, ByVal line As Integer, ByVal isSet As Boolean)

  End Sub

  'Private p_breakMode As String

  Public Property globBreakMode(ByVal className As String) As String
    Get
      Dim val As String
      If Not _breakMode.TryGetValue(LCase(className), val) Then Return ""
      Return val
    End Get
    Set(ByVal value As String)
      OnBreakModeChanged(LCase(className), value)
      _breakMode(LCase(className)) = value
      'p_breakMode = value
      'Dim isBreak = value = "BREAK"
    End Set
  End Property

  Sub resetBreakMode()
    _breakMode.Clear()
  End Sub




#Region "Singleton"
  Private Shared m_instance As ScriptHost

  Public Shared ReadOnly Property Instance() As ScriptHost
    Get
      If m_instance Is Nothing Then
        m_instance = New ScriptHost
      End If
      Return m_instance
    End Get
  End Property

  Public Shared Function GetSingleton() As ScriptHost
    If m_instance Is Nothing Then
      m_instance = New ScriptHost
    End If
    Return m_instance
  End Function

  Private Sub New()

  End Sub
#End Region

End Class