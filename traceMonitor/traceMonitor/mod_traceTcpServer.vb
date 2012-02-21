Imports System.Net.Sockets
Module mod_traceTcpServer

  Public traceServerListenLoopThread As Threading.Thread
  Public traceConnections As New List(Of TraceClientHandler)
  Public traceRegClients As New List(Of TraceClientHandler)
  Public traceSubscribers As New List(Of TraceClientHandler)
  Public traceServerSocket As TcpListener

  Sub killServer()
    On Error Resume Next
    traceServerSocket.Stop()
    traceServerListenLoopThread.Abort()

    For Each c In traceConnections
      c.kill()
    Next
    trace("killServer", "rem. connections: " & traceConnections.Count & vbNewLine & "rem. reg. clients: " & traceRegClients.Count & vbNewLine & "rem. subscribers: " & traceSubscribers.Count, "info")
  End Sub

  Sub registerAndForwardTo(ByVal ip As String, ByVal port As Integer)
    Try
      trace("Forwarding (registering) to TraceServer at", ip & ":" & port)
      Dim c As New TraceClientHandler
      c.client = New TcpClient()
      c.client.Connect(ip, port)
      c.listen()
      c.isSubscribed = True
      c.send("Register: TraceMonitor " & My.Application.Info.Version.ToString & " on " & My.Computer.Name)
      traceConnections.Add(c)
      traceSubscribers.Add(c)
    Catch ex As Exception
      trace("registering failed", ip & ":" & port & vbNewLine & ex.Message, "warn")
    End Try
  End Sub

  Sub subscribeTo(ByVal ip As String, ByVal port As Integer)
    Try
      trace("Attaching (subscribing) to TraceServer at", ip & ":" & port)
      Dim c As New TraceClientHandler
      c.client = New TcpClient()
      c.client.Connect(ip, port)
      c.listen()
      c.isRegistered = True
      c.send("Subscribe: TraceMonitor " & My.Application.Info.Version.ToString & " on " & My.Computer.Name)
      traceConnections.Add(c)
      traceRegClients.Add(c)
    Catch ex As Exception
      trace("subscribing failed", ip & ":" & port & vbNewLine & ex.Message, "warn")
    End Try
  End Sub

  Sub initTraceServer()
    traceServerListenLoopThread = New Threading.Thread(AddressOf traceServerListenLoop)
    traceServerListenLoopThread.Start()
  End Sub

  Sub traceServerListenLoop()
    Try
      traceServerSocket = New TcpListener(glob.para("frm_settings__txtTcpListenPort", "10777"))
      traceServerSocket.Start()
      Do
        Dim c As New TraceClientHandler
        traceConnections.Add(c)
        c.client = traceServerSocket.AcceptTcpClient()
        c.listen()
      Loop
    Catch ex As Exception
      trace("traceServerListenLoop interrupted", ex.Message, "warn")
    End Try
  End Sub

  Public Class TraceClientHandler
    Public client As TcpClient
    Dim sender As IO.StreamWriter
    Dim clientReceiveLoopThread As Threading.Thread
    Public isSubscribed, isRegistered As Boolean
    Public subscribeName As String, registerName As String

    Sub kill()
      On Error Resume Next
      client.Close()
      clientReceiveLoopThread.Abort()
    End Sub

    Sub clientReceiveLoop()
      Dim strEndpoint As String
      strEndpoint = client.Client.RemoteEndPoint.ToString
      trace("TraceClient connected", strEndpoint, "dump")
      Dim receiver As New IO.StreamReader(client.GetStream)
      Try
        Do
          Dim line As String = receiver.ReadLine
          '  Debug.Print("""" & line & """")
          If line.StartsWith("Subscribe:") Then
            If isSubscribed Then
              send("Error: Already subscribed")
            Else
              traceSubscribers.Add(Me)
              isSubscribed = True
              subscribeName = line.Substring(10).Trim()
              trace("TraceListener subscribed", subscribeName, "dump")
              send("OK: Subscribed as " + subscribeName)
            End If

          ElseIf line.StartsWith("Register:") Then
            If isRegistered Then
              send("Error: Already registered")
            Else
              traceRegClients.Add(Me)
              isRegistered = True
              registerName = line.Substring(9).Trim()
              trace("TraceClient registered", registerName, "dump")
              send("OK: Registered as " + registerName)
            End If

          ElseIf line.StartsWith("Trace:") Then
            Dim parts() As String = Split(line.Substring(6), "|")
            If parts.Length <> 4 Then
              send("Error: Invalid parameter count")
              trace("Received invalid trace item from " & strEndpoint & " (" & parts.Length & " cols received; 4 required)", line, "warn")
            Else
              addTraceItem(tunmask(parts(0)), tunmask(parts(1)), tunmask(parts(2)), tunmask(parts(3)))
            End If

          ElseIf line.StartsWith("PrintLine:") Then
            Dim parts() As String = Split(line.Substring(6), "|")
            If parts.Length <> 4 Then
              send("Error: Invalid parameter count")
            Else
              setPrintLine(tunmask(parts(0)), tunmask(parts(1)), tunmask(parts(2)), tunmask(parts(3)))
            End If

          ElseIf line.StartsWith("GetLine:") Then
            Dim id As String = Val(line.Substring(8))
            Try
              send("OK: GetLine")
              send("OK: " & id)
              send("OK: " & MAIN.IGrid2.Cells(id, 1).Value)
              send("OK: " & MAIN.IGrid2.Cells(id, 2).Value)

            Catch ex As Exception

            End Try

          ElseIf line.StartsWith("About: Version") Then
            send("Info: Name=TraceMonitor")
            send("Info: Path=" & Application.ExecutablePath)
            send("Info: Version=" & My.Application.Info.ToString)

          ElseIf line.StartsWith("Exit") Then
            send("OK: Bye.")
            Return

          Else
            send("Error: Invalid command")

          End If
        Loop
      Catch ex As Exception
        Try
          trace("Exception in TraceClientHandler", ex.Message, "err")
          send("Error: Exception in ClientHandler")
          send("Error: " + ex.Message)
        Catch ex2 As Exception
        End Try
      Finally
        trace("TraceClient disconnected", strEndPoint + If(isRegistered, "; was registered (" & registerName & ")", "") + If(isSubscribed, "; was subscribed (" & subscribeName & ")", ""), "dump")
        traceRegClients.Remove(Me)
        traceSubscribers.Remove(Me)
        Try
          sender.Close()
        Catch : End Try
        Try
          receiver.Close()
        Catch : End Try
        Try
          client.Close()
        Catch : End Try
      End Try
    End Sub
    '192.168.111.134
    Sub listen()
      sender = New IO.StreamWriter(client.GetStream)
      sender.AutoFlush = True
      clientReceiveLoopThread = New Threading.Thread(AddressOf clientReceiveLoop)
      clientReceiveLoopThread.Start()
    End Sub

    Sub send(ByVal str As String)
      sender.WriteLine(str)
    End Sub

  End Class


End Module
