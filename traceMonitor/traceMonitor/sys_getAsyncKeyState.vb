Imports System.Runtime.InteropServices
'Private isControlState As Boolean
Module sys_getAsyncKeyState
  <DllImport("user32.dll")> _
  Public Function GetAsyncKeyState(ByVal vKey As Integer) As Short
  End Function

  Function isKeyPressed(ByVal key As Keys) As Boolean
    isKeyPressed = False
    Dim stat As Short
    'GetAsyncKeyState(key) 'puffer leeren
    stat = GetAsyncKeyState(key)

    If stat <> 0 Then
      isKeyPressed = True
    End If
  End Function

  Function isShiftControl() As Boolean
    isShiftControl = False
    If isKeyPressed(Keys.ShiftKey) And isKeyPressed(Keys.ControlKey) Then
      isShiftControl = True
    End If
  End Function


  Function isControl() As Boolean
    isControl = False
    If isKeyPressed(Keys.ControlKey) Then
      isControl = True
    End If
  End Function



  Function isLeftMouseButton() As Boolean
    isLeftMouseButton = False
    If isKeyPressed(Keys.LButton) Then
      isLeftMouseButton = True
    End If
  End Function


End Module
