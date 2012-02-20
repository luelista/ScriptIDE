Imports ScriptIDE.Core
Imports System.Runtime.InteropServices

Public Enum HighlightLineReason
  RuntimeError
  CompileError
  BreakMode
  BreakModeExternal
End Enum


Public Class ScriptHost
  Implements IDisposable

  Private _hostMode As RuntimeMode
  Private p_SilentMode As Boolean
  Private p_breakMode As String
  Public WithEvents oIntWin As sys_interproc
  Private Shared m_instance As ScriptHost

  Public RunningAsCompiledClass As String, CompiledClassRef As Object
  Public CompiledClassTypes As Dictionary(Of String, Type)

  Public traceStack As New Stack(Of String)
  Public dumpStackToTrace As Boolean


  Public Sub New(ByVal mode As RuntimeMode, ByVal debuggedClassName As String, ByVal debuggedClassRef As Object)
    RunningAsCompiledClass = debuggedClassName
    CompiledClassRef = debuggedClassRef
    _hostMode = mode
    If mode = RuntimeMode.Release Then SilentMode = True
    AddHandler ScriptWindowHelper.ScriptWindowManager.ScriptWindowEvent, AddressOf OnScriptWindowEvent
    interproc_init()
    ScriptWindowHelper.ScriptWindowManager.IdeHelper = cls_IDEHelperMini.GetSingleton()
    'CoreTraceListener.Initialize()
  End Sub


  Friend Sub OnScriptWindowEvent(ByVal WinID As String, ByVal ObjectType As String, ByVal EventArgs As ScriptEventArgs)
    Dim startTime = cls_scriptHelper.GetTime()
    If Not String.IsNullOrEmpty(EventArgs.ClassName) Then
      Try
        Dim ref = scriptClass(EventArgs.ClassName)
        If ref Is Nothing Then Exit Sub
        Dim args() As Object = New Object() {EventArgs}
        Dim argTypes() As Type = New Type() {GetType(EventArgs)}
        Try
          Microsoft.VisualBasic.CompilerServices.LateBinding.LateCall(ref, Nothing, EventArgs.ID.Substring(4) + "_" + EventArgs.EventName, args, Nothing, Nothing)
          Exit Sub
        Catch : End Try
        Try
          Microsoft.VisualBasic.CompilerServices.LateBinding.LateCall(ref, Nothing, EventArgs.ID + "_" + EventArgs.EventName, args, Nothing, Nothing)
          Exit Sub
        Catch : End Try
        Try
          Microsoft.VisualBasic.CompilerServices.LateBinding.LateCall(ref, Nothing, "on" + ObjectType + "Event", args, Nothing, Nothing)
          'TT.Write("Called:", "3")
          Exit Sub
        Catch : End Try
        Try
          ref.globalEvent(EventArgs)
          Exit Sub
        Catch : End Try
      Catch : End Try
    End If
  End Sub

  Friend Sub OnHighlightLineRequested(ByVal className As String, ByVal functionName As String, ByVal lineNumber As Integer, ByVal reason As HighlightLineReason)
    oIntWin.SendCommand("scripthostlib06", "_Debug_HighlightLineRequested", className & "|##|" & functionName & "|##|" & lineNumber & "|##|" & reason & "|##|")
  End Sub

  <Obsolete("Not implemented")> Function expandScriptClassName(ByVal className As String) As String
    Return ""
  End Function
  Function scriptClass(ByVal className As String) As Object 'Implements _IApplication.scriptClass
    If className = RunningAsCompiledClass Then Return CompiledClassRef 'Test: Scripte kompilieren
  End Function

  Property InformationWindowVisible() As Boolean
    Get
      Return False
    End Get
    Set(ByVal value As Boolean)
    End Set
  End Property

  Property ErrorListVisible() As Boolean
    Get
      Return False
    End Get
    Set(ByVal value As Boolean)
    End Set
  End Property

  Sub ActivateInformationWindow(Optional ByVal page As String = "")
  End Sub

  Function getInformationWindowRef() As Object
  End Function

  Function getErrorListRef() As Object
  End Function



  Public ReadOnly Property HostMode() As RuntimeMode
    Get
      Return _hostMode
    End Get
  End Property



  Public Property SilentMode() As Boolean
    Get
      Return p_SilentMode
    End Get
    Set(ByVal value As Boolean)
      p_SilentMode = value
    End Set
  End Property




  Public Property globBreakMode() As String
    Get
      If _hostMode = RuntimeMode.Release Then Return ""
      Return p_breakMode
    End Get
    Set(ByVal value As String)
      If _hostMode = RuntimeMode.Release Then Return

      oIntWin.SendCommand("scripthostlib06", "_Debug_BreakModeChanged", value)
      p_breakMode = value
      Dim isBreak = value = "BREAK"
    End Set
  End Property



  Sub interproc_init()
    Dim wndName As String = "siDebug_" + RunningAsCompiledClass + "_"
    If sys_interproc.getWindow(wndName) <> IntPtr.Zero Then wndName += Hex(Now.Ticks)

    oIntWin = New sys_interproc(wndName)
    oIntWin.Commands.Add("CMD  ", "_Debug_SetBreakPoint", "line|##|state", "line: 1-basiert  state: leer=False, ungleich leer=True")
    oIntWin.Commands.Add("QUERY", "Navigate", "className|##|commandName|##|Flags|##|Target", "returns 'Return Value'")
  End Sub

  Private Sub oIntWin_DataRequest(ByVal source As String, ByVal cmdString As String, ByVal para As String, ByRef returnValue As String) Handles oIntWin.DataRequest
    'trace("INTERPROC - DataRequest: " + vbTab + cmdString + vbTab + source + vbTab + para)

    Select Case cmdString.ToUpper
      Case "Navigate"
        Dim p() = Split(para, "|##|", 3) : ReDim Preserve p(3)
        returnValue = zz.NavigateScript(p(0), p(1), p(2), p(3))
    End Select


    'trace("INTERPROC - returnValue: " + vbTab + returnValue)
  End Sub

  Private Sub oIntWin_Message(ByVal source As String, ByVal cmdString As String, ByVal para As String) Handles oIntWin.Message
    TT.Write("INTERPROC - Message: " + vbTab + cmdString + vbTab + source + vbTab + para, "dump")
    Dim data() = Split(para, "|##|")

    Select Case cmdString.ToUpper
      Case "_Debug_SetBreakPoint"
        CompiledClassRef.zz_BBsetLine(data(0), data(1) <> "")
        CompiledClassRef.zz_BBtrace()

    End Select



  End Sub


  'ACHTUNG: die Klasse ist KEIN SingleTon - diese Property dient nur der kompatibilität zum int. Script
  'Public Shared Property Instance() As ScriptHost
  '  Get
  '    Return m_instance
  '  End Get
  '  Set(ByVal value As ScriptHost)
  '    m_instance = value
  '  End Set
  'End Property
  'Public Shared Function GetSingleton() As ScriptHost
  '  Return m_instance
  'End Function

  '#Region "Singleton"

  '  Public Shared ReadOnly Property Instance() As ScriptHost
  '    Get
  '      If m_instance Is Nothing Then
  '        m_instance = New ScriptHost
  '      End If
  '      Return m_instance
  '    End Get
  '  End Property

  '  Public Shared Function GetSingleton() As ScriptHost
  '    If m_instance Is Nothing Then
  '      m_instance = New ScriptHost
  '    End If
  '    Return m_instance
  '  End Function

  '  Private Sub New()

  '  End Sub
  '#End Region

  Private disposedValue As Boolean = False    ' To detect redundant calls

  ' IDisposable
  Protected Overridable Sub Dispose(ByVal disposing As Boolean)
    If Not Me.disposedValue Then
      If disposing Then
        ' TODO: free other state (managed objects).
      End If

      Try
        CompiledClassRef.Dispose()
      Catch ex As Exception
        TT.Write("Unhandled Exception in " + RunningAsCompiledClass + ".Dispose", ex.ToString, "err")
      End Try
      CompiledClassRef = Nothing
    End If
    Me.disposedValue = True
  End Sub

#Region " IDisposable Support "
  ' This code added by Visual Basic to correctly implement the disposable pattern.
  Public Sub Dispose() Implements IDisposable.Dispose
    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    Dispose(True)
    GC.SuppressFinalize(Me)
  End Sub
#End Region

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub
End Class