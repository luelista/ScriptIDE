Imports System.Runtime.InteropServices

Class LuaDebugHost
  <DllImport("user32.dll")> _
  Public Shared Function GetAsyncKeyState(ByVal vKey As Integer) As Short
  End Function

  '' Dim WithEvents interproc As sys_interproc

  Dim luaSpace As LuaInterface.Lua
  Dim listenerThread As Threading.Thread

  Dim debugSocket As Net.Sockets.TcpClient
  Dim dsender As IO.StreamWriter, dreceiver As IO.StreamReader

  Dim traceSocket As Net.Sockets.TcpClient
  Dim tsender As IO.StreamWriter, traceEnabled As Boolean

  Enum DebuggerModes
    Run
    Break
    [Step]
  End Enum
  Dim debugMode As DebuggerModes
  Dim debugDelay As Integer = 0
  Dim breakpoint As Hashtable
  Dim stackDepth As Integer

  Dim debuggerPort As Integer
  Dim scriptFilespec As String

  Public Shared Sub Main(ByVal args() As String)
    If args.Length <> 2 Then
      Console.WriteLine("Usage: {0} port file", My.Application.Info.AssemblyName)
      Return
    End If

    Dim myLuaDebugHost As New LuaDebugHost(args(0), args(1))
    myLuaDebugHost.Run()
  End Sub

  Sub handleException(ByVal ex As Exception)

    Dim errorData As New System.Text.StringBuilder()

    errorData.AppendLine("--- Exception ---")
    errorData.AppendLine("... Message")
    errorData.AppendLine(ex.ToString)
    errorData.AppendLine("... Stack Trace")
    errorData.AppendLine(ex.ToString)
    If ex.InnerException IsNot Nothing Then
      errorData.AppendLine("--- Inner Exception ---")
      errorData.AppendLine("... Message")
      errorData.AppendLine(ex.InnerException.ToString)
      errorData.AppendLine("... Stack Trace")
      errorData.AppendLine(ex.InnerException.StackTrace + vbNewLine)
    End If
    errorData.AppendLine("--- Lua stack ---")
    Dim i As Integer = 0, debug As New LuaInterface.LuaDebug
    While luaSpace.GetStack(i, debug)
      errorData.AppendFormat("{0}: {1} {2} {3}" + vbNewLine, i, debug.shortsrc, debug.currentline, debug.linedefined)
      i += 1
    End While

    If traceEnabled Then _
       _Lua_trace("Unhandled exception", errorData.ToString, "err")

    ConsoleColorStack.Push(ConsoleColor.Red, ConsoleColor.White)
    Console.WriteLine("===== Program crashed =====")
    Console.WriteLine(errorData.ToString)
    ConsoleColorStack.Pop()

    '' For Test: Display exceptions in modal dialog
    'MsgBox(ex.ToString + vbNewLine + vbNewLine + ex.StackTrace + vbNewLine + vbNewLine, MsgBoxStyle.Critical, "Fehler")
    'If ex.InnerException IsNot Nothing Then
    '  MsgBox(ex.InnerException.ToString + vbNewLine + vbNewLine + ex.InnerException.StackTrace + vbNewLine + vbNewLine, MsgBoxStyle.Critical, "Fehler Inner")
    'End If

  End Sub

  Sub New(ByVal port As Integer, ByVal file As String)
    debuggerPort = port
    scriptFilespec = file
  End Sub

  Sub Run()
    Try
      debugMode = DebuggerModes.Break

      Try
        Console.WriteLine("Trying to connect to TraceServer at 127.0.0.1:10777 (Cancel with CTRL+C) ...")
        traceSocket = New Net.Sockets.TcpClient()
        traceSocket.Connect(New Net.IPEndPoint(Net.IPAddress.Loopback, 10777))
        tsender = New IO.StreamWriter(traceSocket.GetStream)
        tsender.AutoFlush = True
        tsender.WriteLine("Register: LuaDebugHost; " + scriptFilespec)
        traceEnabled = True
        Console.WriteLine("OK, Trace enabled.")
      Catch ex As Exception
        Console.WriteLine("ERR: " + ex.Message)
      End Try


      Console.WriteLine("Waiting for debugger to attach (Cancel with CTRL+C) ...")
      debugSocket = New Net.Sockets.TcpClient()
      debugSocket.Connect(New Net.IPEndPoint(Net.IPAddress.Loopback, debuggerPort))
      dsender = New IO.StreamWriter(debugSocket.GetStream)
      dsender.AutoFlush = True
      dreceiver = New IO.StreamReader(debugSocket.GetStream)

      listenerThread = New Threading.Thread(AddressOf loopThread)
      listenerThread.Start()

      initluaspace()

      Dim r = luaSpace.SetDebugHook(LuaInterface.EventMasks.LUA_MASKLINE Or LuaInterface.EventMasks.LUA_MASKCALL Or LuaInterface.EventMasks.LUA_MASKRET, 0)
      Console.WriteLine("SetDebugHook ret " & r)

      Console.WriteLine()

      AddHandler luaSpace.DebugHook, AddressOf luaSpace_DebugHook
      AddHandler luaSpace.HookException, AddressOf luaSpace_HookException
      luaSpace.DoFile(scriptFilespec)
      luaSpace.RemoveDebugHook()

      QuitDebugger()

    Catch ex As Exception
      handleException(ex)
      QuitDebugger()
    End Try
  End Sub

  Sub QuitDebugger()
    ''interproc.SendCommand("scriptIDE", "LuaDebugger_Detach", "")
    'Process.GetCurrentProcess.Kill()
    If traceEnabled Then
      twrite("Exit")
      tsender.Close()
    End If
    luaSpace.Close()
    'luaSpace.Dispose()
    dsender.Write("Detach")
    dreceiver.Close()
    dsender.Close()
    listenerThread.Abort()
  End Sub

  Private Sub luaSpace_HookException(ByVal sender As Object, ByVal e As LuaInterface.HookExceptionEventArgs)
    Console.WriteLine("Panic: Hookexception")
    Console.WriteLine(e.Exception.ToString)
  End Sub
  Private Sub luaSpace_DebugHook(ByVal sender As Object, ByVal e As LuaInterface.DebugHookEventArgs)
    Try
      Select Case e.EventCode
        Case LuaInterface.EventCodes.LUA_HOOKCALL
          ' Console.WriteLine("+++ " & e.LuaDebug.shortsrc & " : " & e.LuaDebug.currentline & " -- " & e.LuaDebug.what & " " & e.LuaDebug.namewhat & " " & e.LuaDebug.name)
          stackDepth += 1

        Case LuaInterface.EventCodes.LUA_HOOKRET
          ' Console.WriteLine("--- " & e.LuaDebug.shortsrc & " : " & e.LuaDebug.currentline & " -- " & e.LuaDebug.what & " " & e.LuaDebug.namewhat & " " & e.LuaDebug.name)
          stackDepth -= 1

        Case LuaInterface.EventCodes.LUA_HOOKLINE
          'Dim debug As LuaInterface.LuaDebug = e.LuaDebug
          Dim ret = luaSpace.GetInfo("Sn", e.LuaDebug)
          '' Console.WriteLine("Getinfo {0}", ret)
          Dim debug As LuaInterface.LuaDebug = luaSpace.GetDebugFromPointer(e.LuaDebug)
          If debug.shortsrc.StartsWith("[") Then Return
          '' Console.ForegroundColor = ConsoleColor.Red
          '' Console.WriteLine("luaSpace_DebugHook: " & debug.eventCode.ToString)
          'Console.WriteLine("src: " + debug.source)
          '' Console.WriteLine(debug.shortsrc & " : " & debug.currentline & " -- " & debug.what & " " & debug.namewhat & " " & debug.name)
          '' Console.ForegroundColor = ConsoleColor.White
          'e.LuaDebug.

          If debugDelay > 0 Then Threading.Thread.Sleep(debugDelay)

          'While is_pause_button()
          '  Threading.Thread.Sleep(100)
          '  debugMode = DebuggerModes.Break
          'End While

          If debugMode = DebuggerModes.Step Then debugMode = DebuggerModes.Break

          If debugMode = DebuggerModes.Break Then
            dsender.WriteLine("OnBreak|" & debug.shortsrc & "|" & debug.currentline)

            While debugMode = DebuggerModes.Break
              'If GetAsyncKeyState(Windows.Forms.Keys.F10) <> 0 Then debugMode = DebuggerModes.Run
              'If GetAsyncKeyState(Windows.Forms.Keys.F9) <> 0 Then debugMode = DebuggerModes.Step : Threading.Thread.Sleep(debugDelay)
              'If is_pause_button() Then QuitDebugger()
              Threading.Thread.Sleep(100)
            End While

            dsender.WriteLine("OnRun")
          End If
      End Select

    Catch ex As Exception
      Console.WriteLine("Exception in debugger: " + ex.ToString)
    End Try
  End Sub

  ''Function is_pause_button() As Boolean
  ''  Return GetAsyncKeyState(Windows.Forms.Keys.Pause) <> 0 Or GetAsyncKeyState(Windows.Forms.Keys.F12)
  ''End Function

  Private Sub initluaspace()
    luaSpace = New LuaInterface.Lua
    luaSpace.RegisterFunction("trace", Me, Me.GetType().GetMethod("_Lua_trace"))
    luaSpace.RegisterFunction("printline", Me, Me.GetType().GetMethod("_Lua_printLine"))
    'lua("ide") = 
    luaSpace("package")("path") += ";C:/yPara/scriptIDE4/luaLibs/?.lua"
    luaSpace("package")("cpath") += ";C:\yPara\scriptIDE4\luaLibs\?.dll"
  End Sub

  Sub loopThread()
    Try
      Do
        Dim line As String = dreceiver.ReadLine
        Dim parts() As String = Split(line, "|")

        Select Case parts(0)
          Case "Run"
            debugMode = DebuggerModes.Run
          Case "Break"
            debugMode = DebuggerModes.Break
          Case "Step"
            debugMode = DebuggerModes.Step

        End Select
      Loop

    Catch ex As Exception
      Console.WriteLine("Listenerthread aborted")
      Console.WriteLine(ex.ToString)
    End Try
  End Sub

  Private Const TraceSep = "|"
  Sub _Lua_printLine(ByVal line As Integer, ByVal str1 As String, ByVal str2 As String)
    If Not traceEnabled Then Return
    Dim stackframe As LuaInterface.LuaDebug
    luaSpace.GetStack(0, stackframe)
    Dim codeLink = "_|°|_cLink_|°|_" + "lua" & "_|°|_" & stackframe.shortsrc & "?" & stackframe.currentline & "_|°|_" & stackframe.name
    Dim out = line.ToString("00") + TraceSep + tmask(str1) + TraceSep + tmask(codeLink) + TraceSep + tmask(str2)
    twrite("PrintLine:" + out)
  End Sub
  Sub _Lua_trace(ByVal para1 As String, Optional ByVal para2 As String = "", Optional ByVal typ As String = "trace")
    If Not traceEnabled Then Return
    Dim stackframe As LuaInterface.LuaDebug
    luaSpace.GetStack(0, stackframe)
    Dim codeLink = "_|°|_cLink_|°|_" + "lua" & "_|°|_" & stackframe.shortsrc & "?" & stackframe.currentline & "_|°|_" & stackframe.name
    Dim out = tmask(typ) + TraceSep + tmask(para1) + TraceSep + tmask(para2) + TraceSep + tmask(codeLink)
    twrite("Trace:" + out)
  End Sub
  Sub twrite(ByVal out As String)
    tsender.WriteLine(out)
  End Sub
  Function tmask(ByVal out As String) As String
    Return out.Replace("\", "\\").Replace(vbCr, "\r").Replace(vbLf, "\n").Replace("|", "\s")
  End Function


  ''Sub loopThread()
  ''  Try
  ''    Windows.Forms.Application.Run()
  ''  Catch ex As Exception
  ''    handleException(ex)
  ''  End Try
  ''End Sub

  ''Private Sub interproc_DataRequest(ByVal source As String, ByVal cmdString As String, ByVal para As String, ByRef returnValue As String) Handles interproc.DataRequest

  ''End Sub

  ''Private Sub interproc_Message(ByVal source As String, ByVal cmdString As String, ByVal para As String) Handles interproc.Message
  ''  Select Case cmdString
  ''    Case "BREAK"
  ''      debugMode = DebuggerModes.Break
  ''    Case "RUN"
  ''      debugMode = DebuggerModes.Run
  ''    Case "SINGLESTEP"
  ''      debugMode = DebuggerModes.Step
  ''    Case "SETBREAKPOINT"
  ''      breakpoint(para) = True
  ''    Case "REMOVEBREAKPOINT"
  ''      breakpoint.Remove(para)

  ''  End Select
  ''End Sub

End Class
