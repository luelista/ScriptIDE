Imports System.Runtime.InteropServices

Module sys_rtfAPI
#Region "Api für rtfBox"
  <StructLayout(LayoutKind.Sequential)> _
  Public Structure STRUCT_CHARFORMAT
    Public cbSize As Integer
    Public dwMask As UInt32
    Public dwEffects As UInt32
    Public yHeight As Int32
    Public yOffset As Int32
    Public crTextColor As Int32
    Public bCharSet As Byte
    Public bPitchAndFamily As Byte
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
    Public szFaceName() As Char
  End Structure

  <DllImport("user32.dll")> _
  Public Function SendMessage( _
    ByVal hWnd As IntPtr, _
    ByVal msg As Int32, _
    ByVal wParam As Int32, _
    ByVal lParam As IntPtr) As Int32
  End Function
  ' Defines for STRUCT_CHARFORMAT member dwMask
  ' (Long because UInt32 is not an intrinsic type)
  Public Const CFM_BOLD As Long = &H1&
  Public Const CFM_ITALIC As Long = &H2&
  Public Const CFM_UNDERLINE As Long = &H4&
  Public Const CFM_STRIKEOUT As Long = &H8&
  Public Const CFM_PROTECTED As Long = &H10&
  Public Const CFM_LINK As Long = &H20&
  Public Const CFM_SIZE As Long = &H80000000&
  Public Const CFM_COLOR As Long = &H40000000&
  Public Const CFM_FACE As Long = &H20000000&
  Public Const CFM_OFFSET As Long = &H10000000&
  Public Const CFM_CHARSET As Long = &H8000000&

  ' Defines for STRUCT_CHARFORMAT member dwEffects
  Public Const CFE_BOLD As Long = &H1&
  Public Const CFE_ITALIC As Long = &H2&
  Public Const CFE_UNDERLINE As Long = &H4&
  Public Const CFE_STRIKEOUT As Long = &H8&
  Public Const CFE_PROTECTED As Long = &H10&
  Public Const CFE_LINK As Long = &H20&
  Public Const CFE_AUTOCOLOR As Long = &H40000000&

  ' Windows Message defines
  Public Const WM_USER As Int32 = &H400&
  Public Const EM_FORMATRANGE As Int32 = WM_USER + 57
  Public Const EM_GETCHARFORMAT As Int32 = WM_USER + 58
  Public Const EM_SETCHARFORMAT As Int32 = WM_USER + 68
  ' Defines for EM_GETCHARFORMAT/EM_SETCHARFORMAT
  Public SCF_SELECTION As Int32 = &H1&
  Public SCF_WORD As Int32 = &H2&
  Public SCF_ALL As Int32 = &H4&

#End Region
End Module
