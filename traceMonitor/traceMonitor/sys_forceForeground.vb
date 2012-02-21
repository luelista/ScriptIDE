Module sys_forceForeground

  ' *********************************************************************
  '  Copyright ©1998 Karl E. Peterson, All Rights Reserved
  '  http://www.mvps.org/vb
  ' *********************************************************************
  '  You are free to use this code within your own applications, but you
  '  are expressly forbidden from selling or otherwise distributing this
  '  source code without prior written consent.
  ' *********************************************************************

  '
  ' Required Win32 API Declarations
  '
  Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hWnd As Long, ByVal lpdwProcessId As Long) As Long
  Private Declare Function AttachThreadInput Lib "user32" (ByVal idAttach As Long, ByVal idAttachTo As Long, ByVal fAttach As Long) As Long
  Private Declare Function GetForegroundWindow Lib "user32" () As Long
  Private Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As Long) As Long
  Private Declare Function IsIconic Lib "user32" (ByVal hWnd As Long) As Long
  Private Declare Function ShowWindow Lib "user32" (ByVal hWnd As Long, ByVal nCmdShow As Long) As Long
  '
  ' Constants used with APIs
  '
  Private Const SW_SHOW = 5
  Private Const SW_RESTORE = 9

  Public Function ForceForegroundWindow(ByVal hWnd As Long) As Boolean
    Dim ThreadID1 As Long
    Dim ThreadID2 As Long
    Dim nRet As Long
    '
    ' Nothing to do if already in foreground.
    '
    If hWnd = GetForegroundWindow() Then
      ForceForegroundWindow = True
    Else
      '
      ' First need to get the thread responsible for this window,
      ' and the thread for the foreground window.
      '
      ThreadID1 = GetWindowThreadProcessId(GetForegroundWindow, 0&)
      ThreadID2 = GetWindowThreadProcessId(hWnd, 0&)
      '
      ' By sharing input state, threads share their concept of
      ' the active window.
      '
      If ThreadID1 <> ThreadID2 Then
        Call AttachThreadInput(ThreadID1, ThreadID2, True)
        nRet = SetForegroundWindow(hWnd)
        Call AttachThreadInput(ThreadID1, ThreadID2, False)
      Else
        nRet = SetForegroundWindow(hWnd)
      End If
      '
      ' Restore and repaint
      '
      If IsIconic(hWnd) Then
        Call ShowWindow(hWnd, SW_RESTORE)
      Else
        Call ShowWindow(hWnd, SW_SHOW)
      End If
      '
      ' SetForegroundWindow return accurately reflects success.
      '
      ForceForegroundWindow = CBool(nRet)
    End If
  End Function







End Module
