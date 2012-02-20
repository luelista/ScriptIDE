Imports System.Text.RegularExpressions

Module sys_htmlToRTF

#Region "Interop-Defines"
  <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
  Private Structure CHARFORMAT2_STRUCT
    Public cbSize As UInt32
    Public dwMask As UInt32
    Public dwEffects As UInt32
    Public yHeight As Int32
    Public yOffset As Int32
    Public crTextColor As Int32
    Public bCharSet As Byte
    Public bPitchAndFamily As Byte
    <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=32)> _
    Public szFaceName As Char()
    Public wWeight As UInt16
    Public sSpacing As UInt16
    Public crBackColor As Integer
    ' Color.ToArgb() -> int
    Public lcid As Integer
    Public dwReserved As Integer
    Public sStyle As Int16
    Public wKerning As Int16
    Public bUnderlineType As Byte
    Public bAnimation As Byte
    Public bRevAuthor As Byte
    Public bReserved1 As Byte
  End Structure

  <Runtime.InteropServices.DllImport("user32.dll", CharSet:=Runtime.InteropServices.CharSet.Auto)> _
  Private Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
  End Function

  Private Const WM_USER As Integer = &H400
  Private Const EM_GETCHARFORMAT As Integer = WM_USER + 58
  Private Const EM_SETCHARFORMAT As Integer = WM_USER + 68

  Private Const SCF_SELECTION As Integer = &H1
  Private Const SCF_WORD As Integer = &H2
  Private Const SCF_ALL As Integer = &H4

#Region "CHARFORMAT2 Flags"
  Private Const CFE_BOLD As UInt32 = &H1
  Private Const CFE_ITALIC As UInt32 = &H2
  Private Const CFE_UNDERLINE As UInt32 = &H4
  Private Const CFE_STRIKEOUT As UInt32 = &H8
  Private Const CFE_PROTECTED As UInt32 = &H10
  Private Const CFE_LINK As UInt32 = &H20
  Private Const CFE_AUTOCOLOR As UInt32 = &H40000000
  Private Const CFE_SUBSCRIPT As UInt32 = &H10000
  ' Superscript and subscript are 
  Private Const CFE_SUPERSCRIPT As UInt32 = &H20000
  '  mutually exclusive			 

  Private Const CFM_SMALLCAPS As Integer = &H40
  ' (*)	
  Private Const CFM_ALLCAPS As Integer = &H80
  ' Displayed by 3.0	
  Private Const CFM_HIDDEN As Integer = &H100
  ' Hidden by 3.0 
  Private Const CFM_OUTLINE As Integer = &H200
  ' (*)	
  Private Const CFM_SHADOW As Integer = &H400
  ' (*)	
  Private Const CFM_EMBOSS As Integer = &H800
  ' (*)	
  Private Const CFM_IMPRINT As Integer = &H1000
  ' (*)	
  Private Const CFM_DISABLED As Integer = &H2000
  Private Const CFM_REVISED As Integer = &H4000

  Private Const CFM_BACKCOLOR As Integer = &H4000000
  Private Const CFM_LCID As Integer = &H2000000
  Private Const CFM_UNDERLINETYPE As Integer = &H800000
  ' Many displayed by 3.0 
  Private Const CFM_WEIGHT As Integer = &H400000
  Private Const CFM_SPACING As Integer = &H200000
  ' Displayed by 3.0	
  Private Const CFM_KERNING As Integer = &H100000
  ' (*)	
  Private Const CFM_STYLE As Integer = &H80000
  ' (*)	
  Private Const CFM_ANIMATION As Integer = &H40000
  ' (*)	
  Private Const CFM_REVAUTHOR As Integer = &H8000


  Private Const CFM_BOLD As UInt32 = &H1
  Private Const CFM_ITALIC As UInt32 = &H2
  Private Const CFM_UNDERLINE As UInt32 = &H4
  Private Const CFM_STRIKEOUT As UInt32 = &H8
  Private Const CFM_PROTECTED As UInt32 = &H10
  Private Const CFM_LINK As UInt32 = &H20
  Private Const CFM_SIZE As UInt32 = &H80000000L
  Private Const CFM_COLOR As UInt32 = &H40000000
  Private Const CFM_FACE As UInt32 = &H20000000
  Private Const CFM_OFFSET As UInt32 = &H10000000
  Private Const CFM_CHARSET As UInt32 = &H8000000
  Private Const CFM_SUBSCRIPT As UInt32 = CFE_SUBSCRIPT Or CFE_SUPERSCRIPT
  Private Const CFM_SUPERSCRIPT As UInt32 = CFM_SUBSCRIPT

  Private Const CFU_UNDERLINENONE As Byte = &H0
  Private Const CFU_UNDERLINE As Byte = &H1
  Private Const CFU_UNDERLINEWORD As Byte = &H2
  ' (*) displayed as ordinary underline	
  Private Const CFU_UNDERLINEDOUBLE As Byte = &H3
  ' (*) displayed as ordinary underline	
  Private Const CFU_UNDERLINEDOTTED As Byte = &H4
  Private Const CFU_UNDERLINEDASH As Byte = &H5
  Private Const CFU_UNDERLINEDASHDOT As Byte = &H6
  Private Const CFU_UNDERLINEDASHDOTDOT As Byte = &H7
  Private Const CFU_UNDERLINEWAVE As Byte = &H8
  Private Const CFU_UNDERLINETHICK As Byte = &H9
  Private Const CFU_UNDERLINEHAIRLINE As Byte = &HA
  ' (*) displayed as ordinary underline	
#End Region
#End Region


  Dim rtfColorTab As List(Of String)
  Function replaceHtmlFont(ByVal m As Match) As String
    Dim res = ""
    For i = 0 To m.Groups(3).Captures.Count - 1
      Dim attrName = LCase(Trim(m.Groups(3).Captures(i).Value))
      Dim attrValue = LCase(Trim(m.Groups(4).Captures(i).Value))
      Select Case attrName
        Case "color", "fg"
          If attrValue = "#000" Or attrValue = "#000000" Or attrValue = "black" Then res &= "/cf0" : Continue For
          If rtfColorTab.Contains(attrValue) = False Then
            rtfColorTab.Add(attrValue)
          End If
          res &= "\cf" & (rtfColorTab.IndexOf(attrValue) + 1)
        Case "bg"
          If attrValue = "#FFF" Or attrValue = "#FFFFFF" Or attrValue = "white" Then res &= "/highlight0" : Continue For
          If rtfColorTab.Contains(attrValue) = False Then
            rtfColorTab.Add(attrValue)
          End If
          res &= "\highlight" & (rtfColorTab.IndexOf(attrValue) + 1)
        Case "face" : res &= "\f" & attrValue
        Case "size" : res &= "\fs" & attrValue
      End Select
    Next
    Return res & " "
  End Function

  Function getRTF(ByVal html As String) As String
    rtfColorTab = New List(Of String)
    rtfColorTab.Add("#808000") : rtfColorTab.Add("#FFFFFF")
    Dim header As String = "{\rtf1\ansi\ansicpg1252\deff0\deflang1031{\fonttbl{\f0\fmodern\fprq1\fcharset0 Courier New;}}"
    Dim header2 As String = "\viewkind4\uc1\pard\fs17\f0" + vbNewLine
    html = html.Replace(vbNewLine, "")
    html = html.Replace("\", "\\")
    html = html.Replace("<br>", "\par" + vbNewLine)
    html = html.Replace("<b>", "\b ")
    html = html.Replace("</b>", "\b0 ")
    html = html.Replace("<i>", "\i ")
    html = html.Replace("</i>", "\i0 ")
    html = html.Replace("<u>", "\ul ")
    html = html.Replace("</u>", "\ul0 ")
    html = html.Replace("<h1>", "\cf2\highlight1\fs23 ")
    html = html.Replace("</h1>", "\cf0\highlight0\fs17 ")
    'html = html.Replace("<a>", "\ul ")
    'html = html.Replace("</a>", "\ul0 ")
    html = html.Replace("</color>", "\cf0 ")

    html = Regex.Replace(html, "<f(ont)?(\s*([a-z]+)='([^']*)')*>", AddressOf replaceHtmlFont)

    'html = Regex.Replace(html, "<a>(.*?)</a>", "{\field{\*\fldinst{HYPERLINK ""$1""}}{\fldrslt{\ul" + vbNewLine + "$1" + vbNewLine + "}}}")

    Dim colorTab = "{\colortbl ;"
    For Each item In rtfColorTab
      Dim col = Drawing.ColorTranslator.FromHtml(item)
      colorTab &= "\red" & col.R & "\green" & col.G & "\blue" & col.B & ";"
    Next
    colorTab &= "}"

    Return header + colorTab + header2 + html + vbNewLine + "\par}"

  End Function
  Sub zoomRTF(ByVal rtb As RichTextBox, ByVal html As String)
    '  txtZoomHeader.Text = title
    rtb.ReadOnly = False
    rtb.Rtf = getRTF(html)
    Dim startPos, endPos As Integer
    Do
      startPos = rtb.Find("<a>", RichTextBoxFinds.MatchCase)
      If startPos = -1 Then Exit Do

      rtb.SelectedText = ""
      endPos = rtb.Find("</a>", startPos, RichTextBoxFinds.NoHighlight Or RichTextBoxFinds.MatchCase)
      If endPos = -1 Then Exit Do

      rtb.Select(startPos, endPos - startPos)
      SetSelectionStyle(rtb, CFM_LINK, CFE_LINK)
      rtb.Select(endPos, 4) : rtb.SelectedText = ""
    Loop
    rtb.ReadOnly = True
    rtb.Select(0, 0)
  End Sub

  Private Sub SetSelectionStyle(ByVal rtf As RichTextBox, ByVal mask As UInt32, ByVal effect As UInt32)
    Dim cf As New CHARFORMAT2_STRUCT()
    cf.cbSize = Runtime.InteropServices.Marshal.SizeOf(cf)
    cf.dwMask = mask
    cf.dwEffects = effect

    Dim wpar As New IntPtr(SCF_SELECTION)
    Dim lpar As IntPtr = Runtime.InteropServices.Marshal.AllocCoTaskMem(Runtime.InteropServices.Marshal.SizeOf(cf))
    Runtime.InteropServices.Marshal.StructureToPtr(cf, lpar, False)

    Dim res As IntPtr = SendMessage(rtf.Handle, EM_SETCHARFORMAT, wpar, lpar)

    Runtime.InteropServices.Marshal.FreeCoTaskMem(lpar)
  End Sub

End Module
