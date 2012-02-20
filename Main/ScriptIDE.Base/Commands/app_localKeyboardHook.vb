Imports TenTec.Windows.iGridLib
Imports System.Runtime.InteropServices

Public Class AppKeyHook

  Public Shared Event OnKeyDown(ByVal key As Keys, ByRef handled As Boolean)

  Private Shared WithEvents hookClass As New ManagedWinapi.Hooks.LowLevelKeyboardHook()
  Const LLKHFLAG_AltPressed = &H20
  Const LLKHFLAG_KeyReleased = &H80

  Private Shared lastPressedKey As Keys = Keys.None
  Private Shared handledLastKey As Boolean = False

  Public Shared isCtrl, isAlt, isShift, isWin As Boolean
  Public Shared isHookDebugging As Boolean = False
  Private Shared hotkeyList() As String

  ''' <summary>The GetForegroundWindow function returns a handle to the foreground window.</summary>
  <DllImport("user32.dll", SetLastError:=True)> _
  Private Shared Function GetForegroundWindow() As IntPtr
  End Function
  <DllImport("user32.dll", SetLastError:=True)> _
  Private Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, _
                            ByRef lpdwProcessId As Integer) As Integer
  End Function
  Private Shared lastForegroundWindow As IntPtr, lastForegroundState As Boolean, currentProcessID As Integer

  Public Delegate Sub KeyInterceptedDelegate(ByVal key As Keys, ByVal scanCode As Integer)

  Private Shared WithEvents tmr_hookAction As New Timer With {.Interval = 10, .Tag = ""}

  Private Shared p_handleAllRef As KeyInterceptedDelegate, p_handleAll As Boolean

  Public Shared Property HandleAllRef() As KeyInterceptedDelegate
    Get
      Return p_handleAllRef
    End Get
    Set(ByVal value As KeyInterceptedDelegate)
      p_handleAllRef = value
      p_handleAll = value IsNot Nothing
    End Set
  End Property

  Public Shared Function IsOwnProcessActive() As Boolean
    Dim fgHWnd = GetForegroundWindow()
    If fgHWnd <> lastForegroundWindow Then
      Dim fgProcID As Integer
      GetWindowThreadProcessId(fgHWnd, fgProcID)

      lastForegroundWindow = fgHWnd
      lastForegroundState = fgProcID = currentProcessID
    End If
    Return lastForegroundState
  End Function

  Public Shared Sub Initialize()
    'trace("hook init")
    ' hookClass.Type = ManagedWinapi.Hooks.HookType.WH_KEYBOARD
    currentProcessID = Process.GetCurrentProcess().Id
    readHotKeyList()
    hookClass.StartHook()
    TT.Write("hook init", "hooked?" & hookClass.Hooked & "  " & currentProcessID, "trace")

  End Sub
  Public Shared Sub unhook()
    hookClass.Unhook()
    TT.Write("hook stopped", "hooked?" & hookClass.Hooked, "trace")

  End Sub

  Public Shared Sub readHotKeyList()
    TT.Write("hook", "START readHotKeyList", "dump")

    Dim fileSpec = ParaService.ProfileFolder + "hotkeys_compiled.txt"
    'Dim RES = IO.File.ReadAllLines(fileSpec) 'TwAjax.ReadFile("appbar", getDIZ() + "_hotkeys_compiled.txt")
    If IO.File.Exists(fileSpec) = False Then
      'traceColor(ConsoleColor.Magenta, "hook", "ERR: hotkeyList not found!", "err")
      ReDim hotkeyList(256)
    Else
      hotkeyList = IO.File.ReadAllLines(fileSpec) 'Split(RES, vbNewLine)
      ReDim Preserve hotkeyList(256)
      For i As Keys = 0 To 255
        If hotkeyList(i) <> "" Then TT.Write("hotkeyList " & CInt(i) & " " & i.ToString, hotkeyList(i), "dump")
      Next
      TT.Write("hook", "FINISH readHotKeyList", "dump")
    End If
  End Sub

  Private Shared Sub hookClass_KeyIntercepted(ByVal msg As Integer, ByVal vkCode As Integer, ByVal scanCode As Integer, ByVal flags As Integer, ByVal time As Integer, ByVal dwExtraInfo As System.IntPtr, ByRef handled As Boolean) Handles hookClass.KeyIntercepted
    Try
      Dim keyCode As Keys = vkCode
      Dim isAltPressed As Boolean = flags And LLKHFLAG_AltPressed ' = LLKHFLAG_AltPressed
      Dim isKeyRelease As Boolean = flags And LLKHFLAG_KeyReleased '= LLKHFLAG_KeyReleased

      Select Case keyCode
        Case Keys.LControlKey, Keys.RControlKey : isCtrl = Not isKeyRelease
        Case Keys.LMenu, Keys.RMenu : isAlt = Not isKeyRelease
        Case Keys.LShiftKey, Keys.RShiftKey : isShift = Not isKeyRelease
        Case Keys.LWin, Keys.RWin : isWin = Not isKeyRelease
      End Select

      If isHookDebugging Then TT.Write("hook " & If(isKeyRelease, "UP ", "dn ") & keyCode.ToString, If(isAltPressed, "pALT", "") & _
            If(isCtrl, "isCTRL ", "") & If(isAlt, "isALT ", "") & If(isShift, "isSHIFT ", "") & If(isWin, "isWIN ", "") & "FLAGS=" & flags, "dump")

      If isKeyRelease Then
        If isShift And isAlt And keyCode = Keys.Pause Then isHookDebugging = Not isHookDebugging
        onGlobalHook_keyUp(keyCode, isAltPressed, flags, handled)
      Else
        onGlobalHook_keyDown(keyCode, isAltPressed, scanCode, flags, handled)
      End If

    Catch ex As Exception
      TT.Write("HotKey failed - Unhandled EXCEPTION in hookClass_KeyIntercepted", ex.ToString, "warn")
    End Try
  End Sub

  Private Shared Sub onGlobalHook_keyDown(ByVal keyCode As Keys, ByVal altPressed As Boolean, ByVal scanCode As Integer, ByVal flag As Integer, ByRef handled As Boolean)
    'If lastPressedKey = keyCode Then trace("hook", "ignored:", keyCode, handledLastKey) : Return handledLastKey
    lastPressedKey = keyCode
    handledLastKey = False
    If p_handleAll Then
      handled = True
      handledLastKey = True
      p_handleAllRef(keyCode, scanCode)
      ' handled = True
      Exit Sub
    End If

    If keyCode = Keys.S And altPressed Then
      handled = True
      Workbench.ShowOptionsDialog()
      Exit Sub
    End If

    RaiseEvent OnKeyDown(keyCode, handled)
    Dim hotkeyData = hotkeyList(keyCode)
    If hotkeyData <> "" AndAlso IsOwnProcessActive() Then
      isCtrl = KeyState.isKeyPressed(Keys.LControlKey) OrElse KeyState.isKeyPressed(Keys.RControlKey)
      isAlt = KeyState.isKeyPressed(Keys.LMenu) OrElse KeyState.isKeyPressed(Keys.RMenu)
      isShift = KeyState.isKeyPressed(Keys.LShiftKey) OrElse KeyState.isKeyPressed(Keys.RShiftKey)
      isWin = KeyState.isKeyPressed(Keys.LWin) OrElse KeyState.isKeyPressed(Keys.RWin)
      TT.Write("HotKey??? ", hotkeyData, "event")

      Dim mods = "°" + If(isCtrl, "c", "_") + "°" + If(isShift, "s", "_") + "°" + If(isAlt, "a", "_") + "°" + If(isWin, "w", "_") + "°"
      Dim startPos, endPos As Integer, checkFor As String
      checkFor = mods '& scanCode & "°"
      Do

        startPos = hotkeyData.IndexOf(checkFor, endPos)
        ' If startPos = -1 Then checkFor = mods & "°" : startPos = hotkeyData.IndexOf(checkFor, endPos)
        If startPos = -1 Then Return

        endPos = hotkeyData.IndexOf(vbTab, startPos)
        Dim data() = Split(hotkeyData.Substring(startPos, endPos - startPos), "|^°|")
        If data(2) <> "" Then Continue Do

        Dim actionData = data(3)
        handledLastKey = True

        TT.Write("HotKey Pressed! ", actionData, "event")
        'If tmr_hookAction.Tag <> "" Then Exit Sub

        Dim keyString = If(isCtrl, "CTRL-", "") + If(isAlt, "ALT-", "") + If(isShift, "SHIFT-", "") + _
                        If(isWin, "WIN-", "") + keyCode.ToString.ToUpper



        'TT.Write("HotKey Action_Type:", data(3) & "   " & data(4), "dump")

        Dim cod = AddInTree.GetCodon("/Workspace/ToolbarCommands/" + data(3)) '"/Workspace/ToolbarCommands/" 
        If cod Is Nothing Then TT.Write("HotKey failed - Codon not found ", data(3), "err") : Continue Do
        Dim returnValue As Object
        Dim cmdID = data(3) 'action(2).Substring(action(2).LastIndexOf("/") + 1)
        Dim ref = cod.AddIn.ConnectRef
        If ref Is Nothing Then TT.Write("HotKey failed - AddInReference is nothing", cod.AddIn.ID, "err") : Continue Do
        Dim paraList As String = data(4)
        'If action.Length >= 3 Then paraList = action(2)

        Try
          ref.OnNavigate(NavigationKind.ToolbarCommand, "HOOK: " + keyString, cmdID, paraList, returnValue)

          handled = True
          If TypeOf returnValue Is String AndAlso returnValue IsNot Nothing AndAlso returnValue <> "HANDLED" Then handled = False
        Catch ex As Exception
          TT.Write("HotKey failed - Unhandled Exception in OnNavigate", ex.ToString, "err")
        End Try

        If handled Then Return

        'handled = True
        'tmr_hookAction.Tag = New Object() {data, keyString}
        'tmr_hookAction.Start()

      Loop
    End If

  End Sub

  Private Shared Sub tmr_hookAction_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmr_hookAction.Tick
    tmr_hookAction.Stop()
    If tmr_hookAction.Tag IsNot Nothing Then
      onHotkeyAction(tmr_hookAction.Tag(0), tmr_hookAction.Tag(1))
      tmr_hookAction.Tag = Nothing
    End If
  End Sub

  Shared Sub onHotkeyAction(ByVal hotKeyData() As String, ByVal actionSource As String)
    If hotKeyData Is Nothing Or hotKeyData.Length < 5 Then Exit Sub
    
    TT.Write("HotKey Action_Type:", hotKeyData(3) & "   " & hotKeyData(4), "dump")

    Dim cod = AddInTree.GetCodon("/Workspace/ToolbarCommands/" + hotKeyData(3)) '"/Workspace/ToolbarCommands/" 
    If cod Is Nothing Then TT.Write("HotKey failed - Codon not found ", hotKeyData(3), "err") : Exit Sub
    Dim returnValue As String = ""
    Dim cmdID = hotKeyData(3) 'action(2).Substring(action(2).LastIndexOf("/") + 1)
    Dim ref = cod.AddIn.ConnectRef
    If ref Is Nothing Then TT.Write("HotKey failed - AddInReference is nothing", cod.AddIn.ID, "err") : Exit Sub
    Dim paraList As String = hotKeyData(4)
    'If action.Length >= 3 Then paraList = action(2)

    ref.OnNavigate(NavigationKind.ToolbarCommand, "HOOK: " + actionSource, cmdID, paraList, returnValue)

    'Select Case action(0)
    '  Case "INTFUNC"
    '    Dim parts(8) As String
    '    parts(BPARA.Para) = action(2)
    '    internalFunction(action(1), parts)
    '  Case "SHEX"

    '    Dim hwnd As Integer
    '    Dim success As Boolean
    '    If action(3) <> "" Then
    '      success = WindowTools.GetHandleFromPartialCaption(hwnd, action(3))
    '    Else
    '      success = False
    '    End If

    '    If success Then
    '      If action(4) = "T" AndAlso WindowTools.IsWindowVisible(hwnd) Then
    '        WindowTools.ShowWindow(hwnd, WindowShowStyle.Hide)
    '      Else
    '        WindowTools.ShowWindow(hwnd, WindowShowStyle.Show)
    '        ForceForegroundWindow(hwnd)
    '      End If
    '    Else
    '      shellExFile(action(1), action(2), , , True)
    '    End If
    '  Case "IPROC"
    '    oIntWin.EnsureAppRunning(action(1))
    '    oIntWin.SendCommand(action(1), action(2), action(3))

    '  Case "VBS"
    '    script_onScriptHotkey(action(1), lastPressedKey)

    '  Case Else
    '    Msg("Hotkey-Aktion konnte nicht ausgeführt werden!", "Parameter: " + actiondata, "AppBar", VDialogIcon.Error)
    'End Select
  End Sub



  Private Shared Sub onGlobalHook_keyUp(ByVal keyCode As Keys, ByVal altPressed As Boolean, ByVal flag As Integer, ByRef handled As Boolean)
    lastPressedKey = Keys.None
  End Sub

  Public Shared Sub compileHotkeyFile(ByVal ig As iGrid)
    Dim out(256) As String
    For Each ir As iGRow In ig.Rows
      Dim keyNr As Integer = Val(ir.Cells("keyCode").Value)
      out(keyNr) += ir.Cells("mod").Value + "|^°|" + "Global" + "|^°|" + "" + "|^°|" + ir.Cells("data3").Value + "|^°|" + ir.Cells("data4").Value + vbTab
    Next

    IO.File.WriteAllLines(ParaService.ProfileFolder + "hotkeys_compiled.txt", out)
    readHotKeyList()
  End Sub

  Private Sub New()
  End Sub

End Class
