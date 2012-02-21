Public Class LuaScripting

  Public Const LUA_HOST_CONNECTION As String = "luaDebugHostConnection"

  Public Const LUA_DEBUG_PORT As Integer = 10666
  Private server As Net.Sockets.TcpListener

  Private p_CurrentDebugHost As LuaDebugHostConnection
  Public Property CurrentDebugHost() As LuaDebugHostConnection
    Get
      Return p_CurrentDebugHost
    End Get
    Set(ByVal value As LuaDebugHostConnection)
      p_CurrentDebugHost = value
      If Workbench.Instance.InvokeRequired Then
        Workbench.Instance.Invoke(ddel_updateCurrentHostInfo)
      Else
        updateCurrentHostInfo()
      End If
    End Set
  End Property
  Private ddel_updateCurrentHostInfo As New Threading.ThreadStart(AddressOf updateCurrentHostInfo)
  Sub updateCurrentHostInfo()
    If p_CurrentDebugHost Is Nothing Then
      ToolbarService.UpdateToolbarItem("Lua.ContinueRun", ToolbarService.TBUF_ENABLED, False, False, Nothing)
      ToolbarService.UpdateToolbarItem("Lua.ContinueStep", ToolbarService.TBUF_ENABLED, False, False, Nothing)
      ToolbarService.UpdateToolbarItem("Lua.Break", ToolbarService.TBUF_ENABLED, False, False, Nothing)
      ToolbarService.UpdateToolbarItem("Lua.Stop", ToolbarService.TBUF_ENABLED, False, False, Nothing)
      tbLuaInstances.ListView1.SelectedItems.Clear()
    Else
      ToolbarService.UpdateToolbarItem("Lua.ContinueRun", ToolbarService.TBUF_ENABLED, p_CurrentDebugHost.DebuggerState = LuaDebugHostConnection.DebuggerModes.Break, False, Nothing)
      ToolbarService.UpdateToolbarItem("Lua.ContinueStep", ToolbarService.TBUF_ENABLED, p_CurrentDebugHost.DebuggerState = LuaDebugHostConnection.DebuggerModes.Break, False, Nothing)
      ToolbarService.UpdateToolbarItem("Lua.Break", ToolbarService.TBUF_ENABLED, p_CurrentDebugHost.DebuggerState = LuaDebugHostConnection.DebuggerModes.Run, False, Nothing)
      ToolbarService.UpdateToolbarItem("Lua.Stop", ToolbarService.TBUF_ENABLED, True, False, Nothing)
      If tbLuaInstances.ListView1.Items.ContainsKey(p_CurrentDebugHost.URL) Then tbLuaInstances.ListView1.Items(p_CurrentDebugHost.URL).Selected = True
    End If
  End Sub

  Private ddel_setDebugHostInfo As New del_setDebugHostInfo(AddressOf setDebugHostInfo)
  Private Delegate Sub del_setDebugHostInfo(ByVal host As LuaDebugHostConnection, ByVal remove As Boolean)
  Sub setDebugHostInfo(ByVal host As LuaDebugHostConnection, ByVal remove As Boolean)
    If Workbench.Instance.InvokeRequired Then
      Workbench.Instance.Invoke(ddel_setDebugHostInfo, host, remove)
    Else
      If remove Then
        tbLuaInstances.ListView1.Items.RemoveByKey(host.URL)
        ToolbarService.UpdateToolbarItem("Lua.DebugRun", ToolbarService.TBUF_ENABLED, True, False, Nothing, host.URL)
      Else
        Dim lvi As ListViewItem
        If tbLuaInstances.ListView1.Items.ContainsKey(host.URL) Then
          lvi = tbLuaInstances.ListView1.Items(host.URL)
        Else
          lvi = tbLuaInstances.ListView1.Items.Add(IO.Path.GetFileName(host.URL))
          lvi.Name = host.URL
          lvi.SubItems.Add("Waiting")
        End If
        If host.stream IsNot Nothing Then
          lvi.SubItems(1).Text = host.DebuggerState.ToString
        End If
        If host Is p_CurrentDebugHost Then tbLuaInstances.ListView1.Items(host.URL).Selected = True
      End If
    End If
  End Sub

  'Private debugHosts As Dictionary(Of Long, LuaDebugHostConnection)

  Public Class LuaDebugHostConnection
    Implements IDisposable

    Public Tab As IDockContentForm
    Public IprocID As Long

    Public MainScript As String
    Public HostProcess As Process

    Public DebugLastFile As String, DebugLastTab As Object, DebugLastLine As Integer

    Public stream As Net.Sockets.TcpClient
    Public listenerThread As Threading.Thread
    Public sender As IO.StreamWriter

    Public URL As String

    Enum DebuggerModes
      Run
      Break
      [Step]
    End Enum
    Private m_debuggerState As DebuggerModes
    Public Property DebuggerState() As DebuggerModes
      Get
        Return m_debuggerState
      End Get
      Set(ByVal value As DebuggerModes)
        If sender Is Nothing Then Throw New Exception("Debugger not attached")
        sender.WriteLine(value.ToString)
        sender.Flush()
      End Set
    End Property

    Sub StartDebug()
      listenerThread = New Threading.Thread(AddressOf listenThread)
      listenerThread.Start()

      inst.setDebugHostInfo(Me, False)
      ToolbarService.UpdateToolbarItem("Lua.DebugRun", ToolbarService.TBUF_ENABLED, False, False, Nothing, Me.URL)


      Dim Psi As New ProcessStartInfo()
      Psi.FileName = IO.Path.Combine(ParaService.AppPath, "sideluadbg.exe")
      Psi.Arguments = LUA_DEBUG_PORT & " """ & MainScript & """"
      'Psi.WorkingDirectory = IO.Path.GetDirectoryName(MainScript)
      HostProcess = Process.Start(Psi)
      HostProcess.EnableRaisingEvents = True
      AddHandler HostProcess.Exited, AddressOf HostProcess_Exited
    End Sub
    Sub HostProcess_Exited()
      Me.Dispose()
    End Sub
    Sub listenThread()
      Try
        stream = Instance.server.AcceptTcpClient()
        TT.Write("LuaDebugger attached", "", "ini")
        If inst.p_CurrentDebugHost Is Me Then inst.CurrentDebugHost = Me
        sender = New IO.StreamWriter(stream.GetStream)
        sender.AutoFlush = True
        Dim receiver As New IO.StreamReader(stream.GetStream)
        Do
          Dim line = receiver.ReadLine()
          Dim parts() = Split(line, "|")
          Select Case parts(0)
            Case "OnBreak"
              m_debuggerState = DebuggerModes.Break
              HighlightLine(parts(1), parts(2) - 1)

              If inst.p_CurrentDebugHost Is Me Then inst.CurrentDebugHost = Me
            Case "OnRun"
              m_debuggerState = DebuggerModes.Run
              HighlightLine(Nothing, -1)

              If inst.p_CurrentDebugHost Is Me Then inst.CurrentDebugHost = Me
            Case "Detach"
              Me.Dispose()
              Return
          End Select
          inst.setDebugHostInfo(Me, False)
        Loop
      Catch ex As Exception
        TT.DumpException("Exception in LuaScripting.LuaDebugHostConnection.listenThread", ex, "warn")
      Finally
        If sender IsNot Nothing Then sender.Close()
        If stream IsNot Nothing Then stream.Close()
      End Try
    End Sub

    Private ddel_HighlightLine As New del_HighlightLine(AddressOf HighlightLine)
    Private Delegate Sub del_HighlightLine(ByVal tab As String, ByVal line As Integer)
    Private Sub HighlightLine(ByVal tab As String, ByVal line As Integer)
      If Workbench.Instance.InvokeRequired Then
        Workbench.Instance.Invoke(ddel_HighlightLine, tab, line)
      Else
        If tab = DebugLastFile AndAlso DebugLastTab IsNot Nothing Then
          DebugLastTab.highlightExecutingLine(line)
          Return
        End If
        If DebugLastTab IsNot Nothing Then DebugLastTab.highlightExecutingLine(-1)
        If Not String.IsNullOrEmpty(tab) Then
          DebugLastTab = gotoNote(tab)
          DebugLastTab.highlightExecutingLine(line)
        Else
          DebugLastTab = Nothing
        End If
        DebugLastFile = tab
      End If
    End Sub


    Private disposedValue As Boolean = False    ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
      If Not Me.disposedValue Then
        If disposing Then
          ' TODO: free other state (managed objects).
        End If

        HighlightLine(Nothing, -1)
        If HostProcess IsNot Nothing Then
          If HostProcess.HasExited = False Then HostProcess.CloseMainWindow() : Threading.Thread.Sleep(200)
          If HostProcess.HasExited = False Then HostProcess.Kill()
          HostProcess = Nothing
        End If
        If listenerThread IsNot Nothing Then
          If listenerThread.IsAlive Then listenerThread.Abort()
          listenerThread = Nothing
        End If
        stream = Nothing
        sender = Nothing
        If Tab IsNot Nothing Then
          Tab.Parameters(LUA_HOST_CONNECTION) = Nothing
          Tab = Nothing
        End If
        If inst.p_CurrentDebugHost Is Me Then inst.CurrentDebugHost = Nothing
        inst.setDebugHostInfo(Me, True)

        TT.Write("LuaDebugger detached", MainScript, "shutdown")
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

  End Class

  Private Sub New()
    server = New Net.Sockets.TcpListener(LUA_DEBUG_PORT)
    server.ExclusiveAddressUse = False
    server.Start()
  End Sub
  Protected Overrides Sub Finalize()
    server.Stop()
    MyBase.Finalize()
  End Sub
  Private Shared inst As LuaScripting = New LuaScripting
  Public Shared ReadOnly Property Instance() As LuaScripting
    Get
      Return inst
    End Get
  End Property

  'alle Funktionen werden aus siaIDEMain.Connect aufgerufen

  Sub AddinRegister()
    Dim fileSpec As String = ProtocolService.MapToLocalFilename(cls_IDEHelper.Instance.getActiveTabFilespec())
    Dim id As String = IO.Path.GetFileNameWithoutExtension(fileSpec)
    If AddinInstance.IsAddinLoaded(id) Then
      MsgBox("Addin mit der ID = " + id + " ist bereits geladen.")
      Exit Sub
    End If
    AddinInstance.ConnectLuaAddin(fileSpec, ConnectMode.AfterStartup)
    ParaService.Glob.para("addinState__" + id) = "TRUE"
  End Sub

  Sub AddinUnregister()
    Dim fileSpec As String = cls_IDEHelper.Instance.getActiveTabFilespec()
    Dim id As String = IO.Path.GetFileNameWithoutExtension(fileSpec)
    If AddinInstance.IsAddinLoaded(id) = False Then
      MsgBox("Addin mit der ID = " + id + " ist nicht geladen.")
      Exit Sub
    End If
    AddinInstance.RemoveAddIn(id)
    ParaService.Glob.para("addinState__" + id) = "FALSE"
  End Sub

  Private Sub autoSave()
    Dim tab As IDockContentForm = getActiveRTF()
    If tab.Dirty Then tab.onSave()
  End Sub
  Sub stopLua()
    Dim tab As IDockContentForm = getActiveRTF()
    Dim luaSpace As LuaDebugHostConnection = tab.Parameters(LUA_HOST_CONNECTION)
    If luaSpace Is Nothing Then Exit Sub
    luaSpace.Dispose()
  End Sub

  Sub Run()
    MsgBox("...")
  End Sub

  Sub Debug()
    Dim tab As IDockContentForm = getActiveRTF()
    Dim luaSpace As LuaDebugHostConnection = tab.Parameters(LUA_HOST_CONNECTION)
    If luaSpace IsNot Nothing Then
      MsgBox("Läuft schon")
      Exit Sub
    End If

    Dim Filespec As String = ProtocolService.MapToLocalFilename(tab.URL)

    autoSave()

    Try
      luaSpace = New LuaDebugHostConnection
      luaSpace.MainScript = Filespec
      luaSpace.Tab = tab
      luaSpace.URL = tab.URL
      CurrentDebugHost = luaSpace
      luaSpace.StartDebug()

    Catch ex As Exception
      TT.DumpException("Unable to start debugging", ex)
    End Try

    tab.Parameters(LUA_HOST_CONNECTION) = luaSpace
  End Sub

  Sub Break()
    If CurrentDebugHost Is Nothing Then Exit Sub
    CurrentDebugHost.DebuggerState = LuaDebugHostConnection.DebuggerModes.Break
  End Sub

  Sub ContinueStep()
    If CurrentDebugHost Is Nothing Then Exit Sub
    CurrentDebugHost.DebuggerState = LuaDebugHostConnection.DebuggerModes.Step
  End Sub
  Sub ContinueRun()
    If CurrentDebugHost Is Nothing Then Exit Sub
    CurrentDebugHost.DebuggerState = LuaDebugHostConnection.DebuggerModes.Run
  End Sub

End Class
