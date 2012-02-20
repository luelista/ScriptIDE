Imports System.Runtime.InteropServices

Module sys_formMoveTricky
  Const WM_NCLBUTTONDOWN As Integer = &HA1
  'Const HTCAPTION As Integer = 2
  Const HTCAPTION As Integer = &H2
  Enum HitTestValues
    HTERROR = -2
    HTTRANSPARENT = -1
    HTNOWHERE = 0
    HTCLIENT = 1
    HTCAPTION = 2
    HTSYSMENU = 3
    HTGROWBOX = 4
    HTMENU = 5
    HTHSCROLL = 6
    HTVSCROLL = 7
    HTMINBUTTON = 8
    HTMAXBUTTON = 9
    HTLEFT = 10
    HTRIGHT = 11
    HTTOP = 12
    HTTOPLEFT = 13
    HTTOPRIGHT = 14
    HTBOTTOM = 15
    HTBOTTOMLEFT = 16
    HTBOTTOMRIGHT = 17
    HTBORDER = 18
    HTOBJECT = 19
    HTCLOSE = 20
    HTHELP = 21
  End Enum
  Declare Function SendMessageA Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As IntPtr, _
   ByVal wParam As IntPtr, ByRef lParam As IntPtr) As IntPtr

  <DllImport("user32.dll", SetLastError:=True)> _
  Public Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInt32) As Boolean
  End Function

  <Flags()> Enum SWPFlags
    SWP_NOSIZE = &H1
    SWP_NOMOVE = &H2
    SWP_NOZORDER = &H4
    SWP_NOREDRAW = &H8
    SWP_NOACTIVATE = &H10
    SWP_FRAMECHANGED = &H20     '/* The frame changed: send WM_NCCALCSIZE */
    SWP_SHOWWINDOW = &H40
    SWP_HIDEWINDOW = &H80
    SWP_NOCOPYBITS = &H100
    SWP_NOOWNERZORDER = &H200    '/* Don't do owner Z ordering */
    SWP_NOSENDCHANGING = &H400    '/* Don't send WM_WINDOWPOSCHANGING */
  End Enum

  <DllImport("user32.dll")> _
  Private Function ReleaseCapture() As Boolean
  End Function

  Sub FormMoveTricky(ByVal hWnd As IntPtr)
    ReleaseCapture()
    SendMessageA(hWnd, WM_NCLBUTTONDOWN, HTCAPTION, 0&)

  End Sub

  Sub FormResizeTricky(ByVal hWnd As IntPtr, ByVal resizeDirection As HitTestValues)
    ReleaseCapture()
    SendMessageA(hWnd, WM_NCLBUTTONDOWN, resizeDirection, 0&)

  End Sub



End Module
