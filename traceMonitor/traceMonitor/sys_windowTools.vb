Imports System.Text
Imports System.Runtime.InteropServices

Module sys_windowTools
  

  Class UnmanagedMethods
    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack:=4)> _
    Public Structure RECT
      Public Left As Integer
      Public Top As Integer
      Public Right As Integer
      Public Bottom As Integer
    End Structure

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function FindWindow( _
       ByVal lpClassName As String, _
       ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", EntryPoint:="FindWindow", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function FindWindowByClass( _
         ByVal lpClassName As String, _
         ByVal zero As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", EntryPoint:="FindWindow", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function FindWindowByCaption( _
         ByVal zero As IntPtr, _
         ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Public Shared Sub GetClassName(ByVal hWnd As System.IntPtr, _
       ByVal lpClassName As StringBuilder, ByVal nMaxCount As Integer)
    End Sub
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function GetWindowText(ByVal hwnd As IntPtr, _
                       ByVal lpString As StringBuilder, _
                       ByVal cch As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function GetWindowTextLength(ByVal hwnd As IntPtr) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function ShowWindow(ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As Boolean
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function IsWindowVisible(ByVal hwnd As IntPtr) As Boolean
    End Function

    Public Declare Function GetWindowRect Lib "user32" ( _
          ByVal hWnd As IntPtr, _
          ByRef lpRect As RECT) As Integer

  End Class
  Public Declare Auto Function SendMessage Lib "user32.dll" ( _
    ByVal hWnd As IntPtr, _
    ByVal wMsg As Int32, _
    ByVal wParam As Int32, _
    ByVal lParam As Int32 _
    ) As Int32
  Private Const BM_CLICK = &HF5

  Public Enum WindowShowStyle As UInteger
    ''' <summary>Hides the window and activates another window.</summary>
    ''' <remarks>See SW_HIDE</remarks>
    Hide = 0
    '''<summary>Activates and displays a window. If the window is minimized
    ''' or maximized, the system restores it to its original size and
    ''' position. An application should specify this flag when displaying
    ''' the window for the first time.</summary>
    ''' <remarks>See SW_SHOWNORMAL</remarks>
    ShowNormal = 1
    ''' <summary>Activates the window and displays it as a minimized window.</summary>
    ''' <remarks>See SW_SHOWMINIMIZED</remarks>
    ShowMinimized = 2
    ''' <summary>Activates the window and displays it as a maximized window.</summary>
    ''' <remarks>See SW_SHOWMAXIMIZED</remarks>
    ShowMaximized = 3
    ''' <summary>Maximizes the specified window.</summary>
    ''' <remarks>See SW_MAXIMIZE</remarks>
    Maximize = 3
    ''' <summary>Displays a window in its most recent size and position.
    ''' This value is similar to "ShowNormal", except the window is not
    ''' actived.</summary>
    ''' <remarks>See SW_SHOWNOACTIVATE</remarks>
    ShowNormalNoActivate = 4
    ''' <summary>Activates the window and displays it in its current size
    ''' and position.</summary>
    ''' <remarks>See SW_SHOW</remarks>
    Show = 5
    ''' <summary>Minimizes the specified window and activates the next
    ''' top-level window in the Z order.</summary>
    ''' <remarks>See SW_MINIMIZE</remarks>
    Minimize = 6
    '''   <summary>Displays the window as a minimized window. This value is
    '''   similar to "ShowMinimized", except the window is not activated.</summary>
    ''' <remarks>See SW_SHOWMINNOACTIVE</remarks>
    ShowMinNoActivate = 7
    ''' <summary>Displays the window in its current size and position. This
    ''' value is similar to "Show", except the window is not activated.</summary>
    ''' <remarks>See SW_SHOWNA</remarks>
    ShowNoActivate = 8
    ''' <summary>Activates and displays the window. If the window is
    ''' minimized or maximized, the system restores it to its original size
    ''' and position. An application should specify this flag when restoring
    ''' a minimized window.</summary>
    ''' <remarks>See SW_RESTORE</remarks>
    Restore = 9
    ''' <summary>Sets the show state based on the SW_ value specified in the
    ''' STARTUPINFO structure passed to the CreateProcess function by the
    ''' program that started the application.</summary>
    ''' <remarks>See SW_SHOWDEFAULT</remarks>
    ShowDefault = 10
    ''' <summary>Windows 2000/XP: Minimizes a window, even if the thread
    ''' that owns the window is hung. This flag should only be used when
    ''' minimizing windows from a different thread.</summary>
    ''' <remarks>See SW_FORCEMINIMIZE</remarks>
    ForceMinimized = 11

  End Enum

  Function FindWindowByCaption(ByVal strCaption As String) As IntPtr
    FindWindowByCaption = UnmanagedMethods.FindWindowByCaption(IntPtr.Zero, strCaption)

  End Function

  Function FindWindowByClass(ByVal strClass As String) As IntPtr
    FindWindowByClass = UnmanagedMethods.FindWindowByClass(strClass, IntPtr.Zero)

  End Function

  Function FindWindowByBoth(ByVal strClass As String, ByVal strCaption As String) As IntPtr
    FindWindowByBoth = UnmanagedMethods.FindWindow(strClass, strCaption)

  End Function

  Public Function GetText(ByVal hWnd As IntPtr) As String
    Dim length As Integer
    If hWnd.ToInt32 <= 0 Then
      Return Nothing
    End If
    length = UnmanagedMethods.GetWindowTextLength(hWnd)
    If length = 0 Then
      Return ""
    End If
    Dim sb As New System.Text.StringBuilder("", length + 1)

    UnmanagedMethods.GetWindowText(hWnd, sb, sb.Capacity)
    Return sb.ToString()
  End Function

  Function GetWindowClassName(ByVal hWnd As IntPtr) As String
    Dim sClassName As New System.Text.StringBuilder("", 256)
    Call UnmanagedMethods.GetClassName(hWnd, sClassName, 256)
    GetWindowClassName = sClassName.ToString
  End Function
  

  Sub emulateStartButtonClick()
    Dim hStartButton As IntPtr
    hStartButton = FindWindowByBoth("Button", "Start")
    SendMessage(hStartButton, BM_CLICK, 0, 0&)
  End Sub

  Sub Window_show(ByVal hWnd As IntPtr, Optional ByVal style As WindowShowStyle = WindowShowStyle.Show)
    UnmanagedMethods.ShowWindow(hWnd, WindowShowStyle.Show)

  End Sub

  Sub Window_hide(ByVal hWnd As IntPtr)
    UnmanagedMethods.ShowWindow(hWnd, WindowShowStyle.Hide)

  End Sub

  Sub toggleWinTaskbar(Optional ByVal state As TriState = TriState.UseDefault)
    Const className = "Shell_TrayWnd"

    Dim hTaskbar As IntPtr = FindWindowByClass(className)

    Dim newState As Boolean
    If state = TriState.False Then newState = False
    If state = TriState.True Or state = 1 Then newState = True
    If state = TriState.UseDefault Then
      newState = Not UnmanagedMethods.IsWindowVisible(hTaskbar)
    End If

    If newState Then
      Window_show(hTaskbar)
    Else
      Window_hide(hTaskbar)
    End If

  End Sub

  Sub getWindowRect(ByVal hWnd As IntPtr, ByRef x As Integer, ByRef y As Integer, ByRef width As Integer, ByRef height As Integer)
    Dim rc As UnmanagedMethods.RECT
    UnmanagedMethods.GetWindowRect(hWnd, rc)
    x = rc.Left
    y = rc.Top
    width = rc.Right - rc.Left
    height = rc.Bottom - rc.Top

  End Sub

End Module
