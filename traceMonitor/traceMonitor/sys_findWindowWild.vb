Module sys_findWindowWild

  'If you use this code, please mention "www.TheScarms.com"

  Private Declare Function EnumWindows& Lib "user32" (ByVal lpEnumFunc As d_EnumWinProc, ByVal lParam As Long)
  Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String, ByVal cch As Long) As Long
  Public Declare Function IsWindowVisible& Lib "user32" (ByVal hwnd As Long)
  Public Declare Function GetParent Lib "user32" (ByVal hwnd As Long) As Long

  Delegate Function d_EnumWinProc(ByVal hwnd As Long, ByVal lParam As Long) As Long
  Private Declare Function GetWindowTextLength Lib "user32" _
  Alias "GetWindowTextLengthA" (ByVal hwnd As Long) As Long



  Dim sPattern As String, hFind As Long

  Function EnumWinProc(ByVal hwnd As Long, ByVal lParam As Long) As Long

    Static lastFound As String
    Dim k As Long, sName As String
    'If IsWindowVisible(hwnd) And GetParent(hwnd) = 0 Then
    sName = Space$(128)
    k = GetWindowText(hwnd, sName, 128)
    If k > 0 Then
      sName = Left$(sName, k)
      If lParam = 0 Then sName = UCase(sName)
      If sName Like sPattern Then
        hFind = hwnd
        EnumWinProc = 0
        If sName <> lastFound Then
          Debug.Print("Found: " + sName)
          lastFound = sName
        End If
        'Exit Function
      End If
    End If
    'End If
    EnumWinProc = 1
  End Function

  Public Function FindWindowWild(ByVal sWild As String, Optional ByVal bMatchCase As Boolean = True) As Long
    'hwnd = FindWindowWild(sWild, False)
    sPattern = sWild
    If Not bMatchCase Then sPattern = UCase(sPattern)
    EnumWindows(AddressOf EnumWinProc, bMatchCase)
    FindWindowWild = hFind
  End Function

  Sub ideMainWindowAcivate()
    On Error Resume Next
    Dim hwnd As Long
    Dim temp As String
    hwnd = FindWindowWild("* - Microsoft Visual Basic *")
    temp = GetCaption(hwnd)
    Debug.Print(temp)
    '' Stop

    AppActivate(temp) '    "SimpelBrowse - Microsoft"

  End Sub


  Public Function GetCaption(ByVal lhWnd As Long) As String
    Dim sA As String, lLen As Long
    lLen& = GetWindowTextLength(lhWnd&)
    sA$ = New String(vbNullString, lLen&)
    Call GetWindowText(lhWnd&, sA$, lLen& + 1)
    GetCaption$ = sA$
  End Function


End Module
