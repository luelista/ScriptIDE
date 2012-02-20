Imports System.Runtime.InteropServices
Imports ScriptIDE.Core.WindowsUtilities

Module app_winVista
  <DllImport("dwmapi.dll")> _
  Public Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
  End Function
  <System.Runtime.InteropServices.DllImport("dwmapi.dll")> _
  Public Function DwmIsCompositionEnabled(ByRef en As Integer) As Integer
  End Function

  <StructLayout(LayoutKind.Sequential)> _
  Public Structure MARGINS
    Public cxLeftWidth As Integer
    Public cxRightWidth As Integer
    Public cyTopHeight As Integer
    Public cyBottomHeight As Integer
  End Structure

  Enum BP_BUFFERFORMAT
    BPBF_COMPATIBLEBITMAP
    BPBF_DIB
    BPBF_TOPDOWNDIB
    BPBF_TOPDOWNMONODI
  End Enum

  <StructLayout(LayoutKind.Sequential)> _
  Public Structure RECT
    Public Left As Integer
    Public Top As Integer
    Public Right As Integer
    Public Bottom As Integer

    Public Sub New(ByVal pLeft As Integer, ByVal pTop As Integer, ByVal pRight As Integer, ByVal pBottom As Integer)
      Left = pLeft
      Top = pTop
      Right = pRight
      Bottom = pBottom
    End Sub
    Public Sub New(ByVal managedRect As Rectangle)
      Left = managedRect.Left
      Top = managedRect.Top
      Right = managedRect.Right
      Bottom = managedRect.Bottom
    End Sub
  End Structure

  ' JUHU -- ich hatte es richtig ...
  '<System.Runtime.InteropServices.DllImport("dwmapi.dll")> _
  'Public Function BeginBufferedPaint(ByRef hdcTarget As IntPtr, ByRef prcTarget As RECT, ByVal dwFormat As BP_BUFFERFORMAT, ByVal pPaintParams As IntPtr, ByRef pHdc As IntPtr) As Integer
  'End Function

  'http://www.arca-eclipse.com/modules.php?name=Forums&file=viewtopic&t=162
  'Start a new Buffered Painting session 
  <DllImport("uxtheme.dll")> _
  Public Function BeginBufferedPaint(ByVal hdc As IntPtr, ByRef prcTarget As RECT, ByVal dwFormat As BP_BUFFERFORMAT, ByVal pPaintParams As IntPtr, ByRef phdc As IntPtr) As IntPtr
  End Function

  'End a buffered painting session (hBufferedPaint is returned from BeginBufferedPaint) 
  <DllImport("uxtheme.dll")> _
  Public Function EndBufferedPaint(ByVal hBufferedPaint As IntPtr, ByVal fUpdateTarget As Boolean) As IntPtr
  End Function

  'Set the alpha level of the device context(0 being fully transparent and 255 being fully opaque 
  <DllImport("uxtheme.dll")> _
  Public Function BufferedPaintSetAlpha(ByVal targetDC As IntPtr, ByVal prcTarget As IntPtr, ByVal Alpha As Byte) As IntPtr
  End Function

  'Set the alpha level of the device context(0 being fully transparent and 255 being fully opaque 
  <DllImport("uxtheme.dll", EntryPoint:="BufferedPaintSetAlpha")> _
  Public Function BufferedPaintSetAlphaRect(ByVal targetDC As IntPtr, ByRef prcTarget As RECT, ByVal Alpha As Byte) As IntPtr
  End Function

  'Init the buffered painting, call this on each thread that uses Buffered Painting functions 
  <DllImport("uxtheme.dll")> _
  Public Sub BufferedPaintInit()
  End Sub

  'clean up the buffered painting, call this on any thread that has called BufferedPAintInit() when your finished 
  <DllImport("uxtheme.dll")> _
  Public Sub BufferedPaintUnInit()
  End Sub

  <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
  Public Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
  End Function

  <DllImport("user32.dll")> _
  Public Function GetComboBoxInfo(ByVal hWnd As IntPtr, ByRef pcbi As COMBOBOXINFO) As Boolean
  End Function

  <StructLayout(LayoutKind.Sequential)> _
  Public Structure COMBOBOXINFO
    Public cbSize As Int32
    Public rcItem As RECT
    Public rcButton As RECT
    Public buttonState As ComboBoxButtonState
    Public hwndCombo As IntPtr
    Public hwndEdit As IntPtr
    Public hwndList As IntPtr
  End Structure

  Public Enum ComboBoxButtonState
    STATE_SYSTEM_NONE = 0
    STATE_SYSTEM_INVISIBLE = &H8000
    STATE_SYSTEM_PRESSED = &H8
  End Enum

  Public Const PRF_CLIENT = &H4& ' Draw the window's client area
  Public Const PRF_CHILDREN = &H10
  Public Const PRF_OWNED = &H20
  Public Const PRF_NONCLIENT = &H2


  <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
  Public Function Rectangle(ByVal hdc As IntPtr, ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer) As Boolean
  End Function
  <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
  Public Function MoveToEx(ByVal hdc As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal lpPoint As IntPtr) As IntPtr
  End Function
  <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
  Public Function LineTo(ByVal hdc As IntPtr, ByVal x As Integer, ByVal y As Integer) As Boolean
  End Function
  <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
  Public Function CreatePen(ByVal enPenStyle As System.Drawing.Drawing2D.PenType, ByVal nWidth As Integer, ByVal crColor As Integer) As IntPtr
  End Function
  '<System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
  'Public Function CreateSolidBrush(ByVal enBrushStyle As System.Drawing.Drawing2D.PenType, ByVal crColor As Integer) As IntPtr
  'End Function
  <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
  Public Function CreateSolidBrush(ByVal crColor As Integer) As IntPtr
  End Function
  <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
  Public Function DeleteObject(ByVal hObject As IntPtr) As Boolean
  End Function
  <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
  Public Function SelectObject(ByVal hdc As IntPtr, ByVal hObject As IntPtr) As IntPtr
  End Function

  ''' <summary>
  ''' Do Not Draw The Caption (Text)
  ''' </summary>
  Public WTNCA_NODRAWCAPTION As UInteger = &H1
  ''' <summary>
  ''' Do Not Draw the Icon
  ''' </summary>
  Public WTNCA_NODRAWICON As UInteger = &H2
  ''' <summary>
  ''' Do Not Show the System Menu
  ''' </summary>
  Public WTNCA_NOSYSMENU As UInteger = &H4
  ''' <summary>
  ''' Do Not Mirror the Question mark Symbol
  ''' </summary>
  Public WTNCA_NOMIRRORHELP As UInteger = &H8

  ''' <summary>
  ''' The Options of What Attributes to Add/Remove
  ''' </summary>
  <StructLayout(LayoutKind.Sequential)> _
  Public Structure WTA_OPTIONS
    Public Flags As UInteger
    Public Mask As UInteger
  End Structure

  ''' <summary>
  ''' What Type of Attributes? (Only One is Currently Defined)
  ''' </summary>
  Public Enum WindowThemeAttributeType
    WTA_NONCLIENT = 1
  End Enum

  ''' <summary>
  ''' Set The Window's Theme Attributes
  ''' </summary>
  ''' <param name="hWnd">The Handle to the Window</param>
  ''' <param name="wtype">What Type of Attributes</param>
  ''' <param name="attributes">The Attributes to Add/Remove</param>
  ''' <param name="size">The Size of the Attributes Struct</param>
  ''' <returns>If The Call Was Successful or Not</returns>
  <DllImport("UxTheme.dll")> _
  Public Function SetWindowThemeAttribute(ByVal hWnd As IntPtr, ByVal wtype As WindowThemeAttributeType, ByRef attributes As WTA_OPTIONS, ByVal size As UInteger) As Integer
  End Function

  ''' <summary>Values to pass to the GetDCEx method.</summary>
  <Flags()> _
  Public Enum DeviceContextValues As Integer
    ''' <summary>DCX_WINDOW: Returns a DC that corresponds to the window rectangle rather
    ''' than the client rectangle.</summary>
    Window = &H1
    ''' <summary>DCX_CACHE: Returns a DC from the cache, rather than the OWNDC or CLASSDC
    ''' window. Essentially overrides CS_OWNDC and CS_CLASSDC.</summary>
    Cache = &H2
    ''' <summary>DCX_NORESETATTRS: Does not reset the attributes of this DC to the
    ''' default attributes when this DC is released.</summary>
    NoResetAttrs = &H4
    ''' <summary>DCX_CLIPCHILDREN: Excludes the visible regions of all child windows
    ''' below the window identified by hWnd.</summary>
    ClipChildren = &H8
    ''' <summary>DCX_CLIPSIBLINGS: Excludes the visible regions of all sibling windows
    ''' above the window identified by hWnd.</summary>
    ClipSiblings = &H10
    ''' <summary>DCX_PARENTCLIP: Uses the visible region of the parent window. The
    ''' parent's WS_CLIPCHILDREN and CS_PARENTDC style bits are ignored. The origin is
    ''' set to the upper-left corner of the window identified by hWnd.</summary>
    ParentClip = &H20
    ''' <summary>DCX_EXCLUDERGN: The clipping region identified by hrgnClip is excluded
    ''' from the visible region of the returned DC.</summary>
    ExcludeRgn = &H40
    ''' <summary>DCX_INTERSECTRGN: The clipping region identified by hrgnClip is
    ''' intersected with the visible region of the returned DC.</summary>
    IntersectRgn = &H80
    ''' <summary>DCX_EXCLUDEUPDATE: Unknown...Undocumented</summary>
    ExcludeUpdate = &H100
    ''' <summary>DCX_INTERSECTUPDATE: Unknown...Undocumented</summary>
    IntersectUpdate = &H200
    ''' <summary>DCX_LOCKWINDOWUPDATE: Allows drawing even if there is a LockWindowUpdate
    ''' call in effect that would otherwise exclude this window. Used for drawing during
    ''' tracking.</summary>
    LockWindowUpdate = &H400
    ''' <summary>DCX_VALIDATE When specified with DCX_INTERSECTUPDATE, causes the DC to
    ''' be completely validated. Using this function with both DCX_INTERSECTUPDATE and
    ''' DCX_VALIDATE is identical to using the BeginPaint function.</summary>
    Validate = &H200000
  End Enum

  <DllImport("user32.dll")> _
  Public Function GetDCEx(ByVal hWnd As IntPtr, ByVal hrgnClip As IntPtr, ByVal DeviceContextValues As DeviceContextValues) As IntPtr
  End Function

  <DllImport("user32.dll")> _
  Public Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
  End Function

  Sub setLabelText(ByVal lab As Label, ByVal txt As String)
    'If ownerDrawSettings IsNot Nothing Then
    '  ownerDrawSettings(lab.Name) = txt
    'Else
    lab.Text = txt
    If vistaStyleOn Then
      With Workbench.Instance
        .pnlTitlebar.Invalidate(New Rectangle(.PictureBox2.Left, 0, .Width - .PictureBox2.Left - 100, 40))

      End With
    End If

    'End If
  End Sub

  Public Const WM_DWMCOMPOSITIONCHANGED As Long = &H31E

  Dim ownerDrawSettings As Dictionary(Of String, Object)
  Public vistaStyleOn As Boolean

  Sub makeVistaForm()
    If vistaStyleOn = False And checkIfGlassEnabled() And Workbench.Instance.pnlTitlebar.Visible Then
      vistaStyleOn = True
      'hideTitleBarContents(MAIN.Handle)
      Workbench.Instance.pnlTitlebar.BackColor = Color.Black
      Workbench.Instance.pnlTitlebar.BackgroundImage = Nothing
      'tbrZoom.BackColor = Color.Black
      Workbench.Instance.pnlTitlebar.Height = 50
      AddGlassToWin(Workbench.Instance.Handle, 0, 0, Workbench.Instance.pnlTitlebar.Height, 0)
      'pnlTitlebar.Left = -3 : 

      ownerDrawSettings = New Dictionary(Of String, Object)
      Workbench.Instance.labWinTitle.Hide() ': Workbench.Instance.txtGlobAktFileSpec.Hide()
      AddHandler Workbench.Instance.pnlTitlebar.Paint, AddressOf pnlTitlebar_Paint
      For Each ctrl As Control In Workbench.Instance.pnlTitlebar.Controls
        If TypeOf ctrl Is Button Or TypeOf ctrl Is CheckBox Then 'Or TypeOf ctrl Is Label Then
          AddHandler ctrl.Paint, AddressOf ownerDrawForGlass
          ownerDrawSettings.Add(ctrl.Name, ctrl.Text)
          'ctrl.Text = ""
          'ctrl.BackColor = Color.Black
        End If
        If TypeOf ctrl Is ComboBox Or TypeOf ctrl Is TextBox Or TypeOf ctrl Is ListBox Then
          Debug.Print("GlassWin: " + ctrl.Name)
          ownerDrawSettings.Add(ctrl.Name, New TextBoxGlassDrawer(ctrl))
        End If
      Next
    End If
    If vistaStyleOn = True And Workbench.Instance.pnlTitlebar.Visible = False Then
      resetVistaForm()
    End If
  End Sub
  Sub resetVistaForm()
    vistaStyleOn = False
    Workbench.Instance.pnlTitlebar.BackColor = Color.FromArgb(255, 33, 33, 33)
    AddGlassToWin(Workbench.Instance.Handle, 0, 0, 0, 0)
    Workbench.Instance.pnlTitlebar.Width = 32

    Workbench.Instance.labWinTitle.Show() ': Workbench.Instance.txtGlobAktFileSpec.Show()
    ownerDrawSettings = Nothing
  End Sub


  Private Sub pnlTitlebar_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    If vistaStyleOn = False Then Exit Sub
    Dim left As Integer = Workbench.Instance.labWinTitle.Left

    DrawTextGlow(e.Graphics, Workbench.Instance.txtGlobAktFileSpec.Text, sender.Font, New Rectangle(left + 15, 9, Workbench.Instance.Width - 100, 40), Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine, False)
    DrawTextGlow(e.Graphics, Workbench.Instance.labWinTitle.Text, Workbench.Instance.labWinTitle.Font, New Rectangle(left + 15, -1, Workbench.Instance.Width - 100, 18), Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine, False)

  End Sub


  Sub ownerDrawForGlass(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    If vistaStyleOn = False Then Exit Sub
    Dim xpos, ypos As Integer
    Dim txt = sender.Text 'ownerDrawSettings(sender.Name)



    Dim strf As New StringFormat()
    strf.Alignment = StringAlignment.Center
    Dim txtSize = e.Graphics.MeasureString(txt, sender.Font, sender.Width, strf)

    ypos = (sender.Height - txtSize.Height) / 2
    xpos = (sender.Width) / 2
    If Not TypeOf sender Is Label Then
      If sender.TextImageRelation = TextImageRelation.ImageBeforeText Then
        xpos = sender.Padding.Left + sender.Image.Width + 4
        strf.Alignment = StringAlignment.Near
      ElseIf sender.TextImageRelation = TextImageRelation.ImageAboveText Then
        ypos = sender.Padding.Top + sender.Image.Height + 3
        xpos = (sender.Width - txtSize.Width) / 2
        strf.Alignment = StringAlignment.Near
      End If
    End If
    If TypeOf sender Is CheckBox Then
      Dim rect As New Rectangle(20, 2, sender.Width - 25, sender.height - 4)
      Dim stat = VisualStyles.CheckBoxState.UncheckedNormal
      If Workbench.Instance.RectangleToScreen(CType(sender, CheckBox).Bounds).Contains(Cursor.Position) Then
        stat += 1
      End If
      If sender.Checked Then stat = VisualStyles.CheckBoxState.CheckedNormal

      DrawTextGlow(e.Graphics, txt, sender.Font, rect, Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine, True)
      'CheckBoxRenderer.DrawCheckBox(e.Graphics, New Point(0, 0), stat)
    ElseIf TypeOf sender Is Button Then
      e.Graphics.DrawString(txt, sender.font, Brushes.Black, xpos, ypos, strf)
    ElseIf TypeOf sender Is Label Then
      Dim rect As New Rectangle(20, 0, sender.Width, sender.height)
      DrawTextGlow(e.Graphics, txt, sender.Font, rect, Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine, True)
      If sender.Image IsNot Nothing Then
        e.Graphics.DrawImage(sender.Image, 0, 0)

      End If
    ElseIf TypeOf sender Is PictureBox Then
      'e.Graphics.DrawImage(sender.Image, 0, 0)

    End If
  End Sub
  Sub hideTitleBarContents(ByVal hWnd As IntPtr)
    Dim ops As New WTA_OPTIONS
    '// We Want To Hide the Caption and the Icon

    ops.Flags = WTNCA_NODRAWICON Or WTNCA_NODRAWCAPTION
    '// If we set the Mask to the same value as the Flags, the Flags are Added. 

    '// If not they are Removed

    ops.Mask = WTNCA_NODRAWICON Or WTNCA_NODRAWCAPTION
    '// Set It, The Marshal.Sizeof() stuff is to get the right size of the 

    '// custom struct, and in UINT/DWORD Form

    SetWindowThemeAttribute(hWnd, WindowThemeAttributeType.WTA_NONCLIENT, ops, Marshal.SizeOf(GetType(WTA_OPTIONS)))

  End Sub

  Sub drawRectOnHDC(ByVal hdc As Integer, ByVal rect As Rectangle, ByVal fillColor As Color)

    Dim gdiPen = CreatePen(Drawing2D.PenType.SolidColor, 1, ColorTranslator.ToWin32(fillColor))
    Dim gdiBrush = CreateSolidBrush(fillColor.ToArgb)
    'If PenColor = Color.Transparent Then
    '  SetROP2(hdc, CInt(RasterOps.R2_XORPEN))
    'End If
    Dim oldPen = SelectObject(hdc, gdiPen)
    Dim oldBrush = SelectObject(hdc, gdiBrush)

    Rectangle(hdc, rect.X, rect.Y, rect.Right, rect.Bottom)

    SelectObject(hdc, oldBrush)
    SelectObject(hdc, oldPen)
    DeleteObject(gdiPen)
    DeleteObject(gdiBrush)

  End Sub


  'HRESULT EndBufferedPaint(      
  '  HPAINTBUFFER hBufferedPaint,
  '  BOOL fUpdateTarget
  ');
  '  HRESULT BufferedPaintSetAlpha(      
  '    HPAINTBUFFER hBufferedPaint,
  '    const RECT *prc,
  '    BYTE alpha
  ');
  'HPAINTBUFFER BeginBufferedPaint(      
  '  HDC hdcTarget,
  '  const RECT *prcTarget,
  '  BP_BUFFERFORMAT dwFormat,
  '  BP_PAINTPARAMS *pPaintParams,
  '  HDC *phdc
  ');
  Public Class ComboBoxClientGlassDrawer
    Inherits NativeWindow

    Public graph As Graphics
    Public ctrl As Control
    Public editrect As RECT
    Public Sub New(ByVal pCmb As ComboBox)
      ctrl = pCmb
      Dim cmbinfo As New COMBOBOXINFO
      cmbinfo.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(cmbinfo)
      GetComboBoxInfo(pCmb.Handle, cmbinfo)
      editrect = cmbinfo.rcItem
      editrect.Top = 0 : editrect.Left = 0
      'graph = Graphics.FromHwnd(ctrl.Handle)
      Me.AssignHandle(cmbinfo.hwndEdit)
    End Sub
    'override the default WindowProc and hijack the WM_PAINT command 
    Protected Overloads Overrides Sub WndProc(ByRef m As Message)
      'let windows handle the message by default 
      MyBase.WndProc(m)

      If m.Msg = WindowsMessages.WM_PAINT Then
        'create a graphics object for this control 
        Dim graph As Graphics = Graphics.FromHwnd(Me.Handle)

        'obtain the controls native device context 
        Dim hdc As IntPtr = graph.GetHdc()

        'create a empty device context 
        Dim BufferedDC As IntPtr = IntPtr.Zero

        Dim messageToUse = WindowsMessages.WM_PRINTCLIENT
        'Cast the ClientRectangle to a native RECT              
        'Dim ClientRect As New RECT(ctrl.ClientRectangle)

        'obtain the buffered device context from BeginBufferedPaint 
        Dim BuffDCHandle As IntPtr = BeginBufferedPaint(hdc, editrect, BP_BUFFERFORMAT.BPBF_TOPDOWNDIB, IntPtr.Zero, BufferedDC)

        'paint the client to the buffered device context 
        SendMessage(Handle, messageToUse, BufferedDC, PRF_CLIENT Or PRF_CHILDREN Or PRF_OWNED)

        'set the ALPHA level to fully opaque 
        BufferedPaintSetAlpha(BuffDCHandle, IntPtr.Zero, 255)

        'end the buffered painting session 
        EndBufferedPaint(BuffDCHandle, True)

        'release the controls device context 
        graph.ReleaseHdc(hdc)
        graph.Dispose()
        ' m.Result = -1
      Else

      End If
    End Sub
  End Class


  Public Class TextBoxGlassDrawer
    Inherits NativeWindow

    Public graph As Graphics
    Public ctrl As Control
    Public subctrl As ComboBoxClientGlassDrawer

    Public Sub New(ByVal pControl As Control)
      ctrl = pControl
      'graph = Graphics.FromHwnd(ctrl.Handle)
      Me.AssignHandle(ctrl.Handle)

      If TypeOf pControl Is ComboBox Then
        subctrl = New ComboBoxClientGlassDrawer(pControl)
      End If
    End Sub
    'override the default WindowProc and hijack the WM_PAINT command 
    Protected Overloads Overrides Sub WndProc(ByRef m As Message)
      'let windows handle the message by default 
      MyBase.WndProc(m)

      If m.Msg = WindowsMessages.WM_PAINT Then
        'create a graphics object for this control 
        Dim graph As Graphics = ctrl.CreateGraphics()
        'obtain the controls native device context 
        Dim hdc As IntPtr = graph.GetHdc()

        'create a empty device context 
        Dim BufferedDC As IntPtr = IntPtr.Zero

        If TypeOf ctrl Is ListBox Then
          'Stop
        End If
        'Cast the ClientRectangle to a native RECT              
        Dim ClientRect As New RECT(ctrl.ClientRectangle)

        'obtain the buffered device context from BeginBufferedPaint 
        Dim BuffDCHandle As IntPtr = BeginBufferedPaint(hdc, ClientRect, BP_BUFFERFORMAT.BPBF_TOPDOWNDIB, IntPtr.Zero, BufferedDC)
        drawRectOnHDC(hdc, ctrl.ClientRectangle, Color.White)

        'paint the client to the buffered device context 
        SendMessage(Handle, WindowsMessages.WM_PRINTCLIENT, BufferedDC, PRF_NONCLIENT Or PRF_CLIENT Or PRF_CHILDREN Or PRF_OWNED)

        If TypeOf ctrl Is ListBox Then
          '  Dim cmbinfo As New COMBOBOXINFO
          '  cmbinfo.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(cmbinfo)
          '  GetComboBoxInfo(ctrl.Handle, cmbinfo)
          '  SendMessage(cmbinfo.hwndEdit, WindowsMessages.WM_PRINTCLIENT, BufferedDC, PRF_CLIENT Or PRF_CHILDREN Or PRF_OWNED)
        End If

        'set the ALPHA level to fully opaque 
        BufferedPaintSetAlpha(BuffDCHandle, IntPtr.Zero, 255)

        If TypeOf ctrl Is ComboBox Then
          BufferedPaintSetAlphaRect(BuffDCHandle, New RECT(0, 0, 1, 1), 0)
          BufferedPaintSetAlphaRect(BuffDCHandle, New RECT(0, ctrl.Height - 1, 1, ctrl.Height), 0)
          BufferedPaintSetAlphaRect(BuffDCHandle, New RECT(ctrl.Width - 1, 0, ctrl.Width, 1), 0)
          BufferedPaintSetAlphaRect(BuffDCHandle, New RECT(ctrl.Width - 1, ctrl.Height - 1, ctrl.Width, ctrl.Height), 0)
          '  Dim cmbinfo As New COMBOBOXINFO
          '  cmbinfo.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(cmbinfo)
          '  GetComboBoxInfo(ctrl.Handle, cmbinfo)
          '  SendMessage(cmbinfo.hwndEdit, WindowsMessages.WM_PRINTCLIENT, BufferedDC, PRF_CLIENT Or PRF_CHILDREN Or PRF_OWNED)
        End If

        'end the buffered painting session 
        EndBufferedPaint(BuffDCHandle, True)

        'release the controls device context 
        graph.ReleaseHdc(hdc)
      End If
    End Sub

    'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    '  Static recursionBlocker As Boolean = False


    '  Select Case m.Msg
    '    Case WindowsMessages.WM_PAINT, WindowsMessages.WM_CHAR, WindowsMessages.WM_KEYDOWN, _
    '         WindowsMessages.WM_MOUSEMOVE, WindowsMessages.WM_PRINT

    '      If recursionBlocker = False Then
    '        recursionBlocker = True
    '        RedrawControlAsBitmap(Me.Handle)
    '        recursionBlocker = False
    '      End If

    '  End Select

    '  MyBase.WndProc(m)
    'End Sub

    ''' <summary>
    ''' Redraws a given control as a bitmap ontop of itself.
    ''' </summary>
    ''' <param name="hwnd"></param>
    ''' <remarks></remarks>
    Public Sub RedrawControlAsBitmap(ByVal hwnd As IntPtr)


      If ctrl IsNot Nothing Then
        Using bm As New Bitmap(ctrl.Width, ctrl.Height)
          ctrl.DrawToBitmap(bm, ctrl.ClientRectangle)

          Using g As Graphics = ctrl.CreateGraphics
            g.DrawImage(bm, New Point(0, 0))
          End Using

        End Using
      End If

    End Sub
    'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    '  'Debug.Print(m.ToString)
    '  If m.Msg = 15 Then
    '    ctrl.Invalidate()
    '    MyBase.WndProc(m)
    '    DoMyPaint()
    '    NativeMethods.BeginBufferedPaint()
    '  Else
    '    MyBase.WndProc(m)
    '  End If
    'End Sub

    Sub DoMyPaint()
      Dim txt As String, rect As Rectangle
      txt = ctrl.Text
      'rect = New Rectangle(2, 2, ctrl.Width, ctrl.Height)
      'DrawTextGlow(graph, txt, ctrl.Font, rect, Color.Black, TextFormatFlags.VerticalCenter Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine)
      Dim ff As TextFormatFlags = TextFormatFlags.TextBoxControl
      rect = New Rectangle(0, 0, ctrl.Width, ctrl.Height)
      TextBoxRenderer.DrawTextBox(graph, rect, txt, ctrl.Font, 0, VisualStyles.TextBoxState.Disabled)

      'graph.DrawString(txt, ctrl.Font, Brushes.Black, 2, 2)


    End Sub

  End Class

  Public Sub DrawTextGlow(ByVal Graphics As Graphics, ByVal text As String, ByVal fnt As Font, ByVal bounds As Rectangle, ByVal Clr As Color, ByVal flags As TextFormatFlags, ByVal small As Boolean)

    ' Variables used later.
    Dim SavedBitmap As IntPtr = IntPtr.Zero
    Dim SavedFont As IntPtr = IntPtr.Zero
    Dim MainHDC As IntPtr = Graphics.GetHdc
    Dim MemHDC As IntPtr = APIs.CreateCompatibleDC(MainHDC)
    Dim BtmInfo As New APIs.BITMAPINFO
    Dim TextRect As APIs.RECT
    If small Then
      TextRect = New APIs.RECT(0, 0, bounds.Right - bounds.Left + 2 * 8, bounds.Bottom - bounds.Top + 2 * 8)
    Else
      TextRect = New APIs.RECT(8, 8, bounds.Right - bounds.Left + 2 * 8, bounds.Bottom - bounds.Top + 2 * 8)
    End If
    Dim ScreenRect As New APIs.RECT(bounds.Left - 8, bounds.Top - 8, bounds.Right + 8, bounds.Bottom + 8)
    Dim hFont As IntPtr = fnt.ToHfont

    Try
      Dim Renderer As VisualStyles.VisualStyleRenderer = New VisualStyles.VisualStyleRenderer(System.Windows.Forms.VisualStyles.VisualStyleElement.Window.Caption.Active)

      ' Memory bitmap to hold the drawn glowed text.
      BtmInfo.bmiHeader.biSize = Marshal.SizeOf(BtmInfo.bmiHeader)

      With BtmInfo
        .bmiHeader.biWidth = bounds.Width + 30
        .bmiHeader.biHeight = -bounds.Height - 30
        .bmiHeader.biPlanes = 1
        .bmiHeader.biBitCount = 32
      End With

      ' Create a DIB Section for this bitmap from the graphics object.
      Dim dibSection As IntPtr = APIs.CreateDIBSection(MainHDC, BtmInfo, 0, 0, IntPtr.Zero, 0)

      ' Save the current handles temporarily.
      SavedBitmap = APIs.SelectObject(MemHDC, dibSection)
      SavedFont = APIs.SelectObject(MemHDC, hFont)

      ' Holds the properties of the text (size and color , ...etc).
      Dim TextOptions As APIs.S_DTTOPTS = New APIs.S_DTTOPTS

      With TextOptions
        .dwSize = Marshal.SizeOf(TextOptions)
        .dwFlags = APIs.DTT_COMPOSITED Or APIs.DTT_GLOWSIZE Or APIs.DTT_TEXTCOLOR
        .crText = ColorTranslator.ToWin32(Clr)
        .iGlowSize = 8
      End With

      ' Draw The text on the memory surface.
      APIs.DrawThemeTextEx(Renderer.Handle, MemHDC, 0, 0, text, -1, flags, TextRect, TextOptions)

      ' Reflecting the image on the primary surface of the graphics object.
      With ScreenRect
        APIs.BitBlt(MainHDC, .Left, .Top, .Right - .Left, .Bottom - .Top, MemHDC, 0, 0, APIs.SRCCOPY)
      End With

      ' Resources Cleaning.
      APIs.SelectObject(MemHDC, SavedFont)
      APIs.SelectObject(MemHDC, SavedBitmap)

      APIs.DeleteDC(MemHDC)
      APIs.DeleteObject(hFont)
      APIs.DeleteObject(dibSection)

      Graphics.ReleaseHdc(MainHDC)
    Catch ex As Exception

    End Try
  End Sub


#Region "    APIs Declaration Section          "

  Public Class APIs

    Public Declare Function CreateDIBSection Lib "gdi32.dll" (ByVal hdc As IntPtr, ByRef pbmi As BITMAPINFO, ByVal iUsage As UInt32, ByVal ppvBits As Integer, ByVal hSection As IntPtr, ByVal dwOffset As UInt32) As IntPtr
    Public Declare Function CreateCompatibleDC Lib "gdi32.dll" (ByVal hDC As IntPtr) As IntPtr
    Public Declare Function SelectObject Lib "gdi32.dll" (ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
    Public Declare Function DeleteObject Lib "gdi32.dll" (ByVal hObject As IntPtr) As Boolean
    Public Declare Function DeleteDC Lib "gdi32.dll" (ByVal hdc As IntPtr) As Boolean
    Public Declare Function BitBlt Lib "gdi32.dll" (ByVal hdc As IntPtr, ByVal nXDest As Integer, ByVal nYDest As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hdcSrc As IntPtr, ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As Int32) As Boolean

    Public Declare Function DwmExtendFrameIntoClientArea Lib "dwmapi.dll" (ByVal hWnd As IntPtr, ByRef margins As MARGINS) As Integer
    Public Declare Sub DwmIsCompositionEnabled Lib "dwmapi.dll" (ByRef IsIt As Boolean)
    <DllImport("UxTheme.dll", ExactSpelling:=True, SetLastError:=True, CharSet:=CharSet.Unicode)> Shared Function DrawThemeTextEx(ByVal hTheme As IntPtr, ByVal hdc As IntPtr, ByVal iPartId As Integer, ByVal iStateId As Integer, ByVal text As String, ByVal iCharCount As Integer, ByVal dwFlags As Integer, ByRef pRect As RECT, ByRef pOptions As S_DTTOPTS) As Integer
    End Function

    Public Const DTT_COMPOSITED As Integer = 8192
    Public Const DTT_GLOWSIZE As Integer = 2048
    Public Const DTT_TEXTCOLOR As Integer = 1
    Public Const SRCCOPY As Integer = &HCC0020
    Public Const WM_SYSCOLORCHANGE As Int32 = &H15

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MARGINS
      Public Left As Integer
      Public Right As Integer
      Public Top As Integer
      Public Bottom As Integer
    End Structure

    Public Structure RECT

      Public Sub New(ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iRight As Integer, ByVal iBottom As Integer)
        Left = iLeft
        Top = iTop
        Right = iRight
        Bottom = iBottom
      End Sub

      Public Left As Integer
      Public Top As Integer
      Public Right As Integer
      Public Bottom As Integer
    End Structure

    Public Structure BITMAPINFOHEADER
      Dim biSize As Integer
      Dim biWidth As Integer
      Dim biHeight As Integer
      Dim biPlanes As Short
      Dim biBitCount As Short
      Dim biCompression As Integer
      Dim biSizeImage As Integer
      Dim biXPelsPerMeter As Integer
      Dim biYPelsPerMeter As Integer
      Dim biClrUsed As Integer
      Dim biClrImportant As Integer
    End Structure

    Public Structure RGBQUAD
      Dim rgbBlue As Byte
      Dim rgbGreen As Byte
      Dim rgbRed As Byte
      Dim rgbReserved As Byte
    End Structure

    Public Structure BITMAPINFO
      Dim bmiHeader As BITMAPINFOHEADER
      Dim bmiColors As RGBQUAD
    End Structure

    Public Structure S_DTTOPTS
      Dim dwSize As Integer
      Dim dwFlags As Integer
      Dim crText As Integer
      Dim crBorder As Integer
      Dim crShadow As Integer
      Dim iTextShadowType As Integer
      Dim ptShadowOffset As Point
      Dim iBorderSize As Integer
      Dim iFontPropId As Integer
      Dim iColorPropId As Integer
      Dim iStateId As Integer
      Dim fApplyOverlay As Boolean
      Dim iGlowSize As Integer
      Dim pfnDrawTextCallback As Integer
      Dim lParam As IntPtr
    End Structure


  End Class

#End Region

  Function checkIfGlassEnabled() As Boolean
    If Environment.OSVersion.Version.Major < 6 Then
      Return False
    End If

    'Check if DWM is enabled
    Dim isGlassSupported As Boolean = False
    DwmIsCompositionEnabled(isGlassSupported)
    Return isGlassSupported
  End Function

  Function IsGlassEnabled() As Boolean
    'If Cfg("checkVistaEffects") = "FALSE" Then Return False
    Return checkIfGlassEnabled()
  End Function

  Sub AddGlassToWin(ByVal hWnd As IntPtr, ByVal left As Integer, ByVal right As Integer, ByVal top As Integer, ByVal bottom As Integer)
    Dim margins As New MARGINS()
    margins.cxLeftWidth = left
    margins.cxRightWidth = right
    margins.cyTopHeight = top
    margins.cyBottomHeight = bottom

    Dim result As Integer = DwmExtendFrameIntoClientArea(hWnd, margins)

  End Sub

End Module
