Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Runtime.InteropServices.ComTypes

Public Class ScriptedObjectHelper
  Private Sub New()
  End Sub

  Public Shared idCounter As Integer = 1

  Public Shared Function GenerateElementId(ByVal typePrefix As String, ByVal givenID As String, ByVal rootPanel As IScriptedPanel) As String
    If givenID.StartsWith(typePrefix + "_", StringComparison.CurrentCultureIgnoreCase) = False Then _
      givenID = typePrefix + "_" + givenID
    If rootPanel.HasElement(givenID) = True Then givenID &= "_" & idCounter : idCounter += 1
    Return givenID
  End Function

End Class

Public Class ScriptedToolstrip
  Inherits ToolStrip
  Implements IScriptedPanel

  Private p_className As String
  Private p_winID As String
  Private p_actMenu As ToolStripItem

  Sub New()
    MyBase.New()
  End Sub

  Public Const WM_MOUSEACTIVATE = &H21
  Public Const MA_ACTIVATEANDEAT = 2
  Public Const MA_ACTIVATE = 1
  Protected Overrides Sub WndProc(ByRef m As Message)
    MyBase.WndProc(m)
    If m.Msg = WM_MOUSEACTIVATE And m.Result = MA_ACTIVATEANDEAT Then m.Result = MA_ACTIVATE
  End Sub


  Public Property WinID() As String Implements IScriptedPanel.winID
    Get
      Return p_winID
    End Get
    Set(ByVal value As String)
      p_winID = value

      'If value.Contains(".") Then
      '  Dim clsName = value.Substring(0, value.IndexOf("."))
      '  'If ScriptHost.Instance.expandScriptClassName(clsName) <> "" Then
      '  p_className = clsName
      '  'End If
      'End If
    End Set
  End Property

  Public Property ClassName() As String Implements IScriptedPanel.className
    Get
      Return p_className
    End Get
    Set(ByVal value As String)
      p_className = value
    End Set
  End Property

  Property ActiveMenu() As ToolStripItem
    Get
      Return p_actMenu
    End Get
    Set(ByVal value As ToolStripItem)
      If TypeOf value Is ToolStripDropDownButton Or TypeOf value Is ToolStripSplitButton Then
        p_actMenu = value
      Else
        p_actMenu = Nothing
      End If

    End Set
  End Property

  Sub BR(Optional ByVal rowHeight As Integer = -1) Implements IScriptedPanel.BR

  End Sub

  Sub addControl(ByVal strID, ByVal strProgID, Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1) Implements IScriptedPanel.addControl
    'Try
    '  Dim typ = Type.GetType(CStr(strProgID), True)
    '  Dim ctrl As Control = Activator.CreateInstance(typ)
    '  ctrl.Name = strID

    '  If intLeft > -1 Then ctrl.Left = intLeft Else ctrl.Left = actX
    '  If intWidth > 0 Then ctrl.Width = intWidth
    '  If intWidth = -2 Then ctrl.Width = Me.Width - ctrl.Left - 10 : ctrl.Anchor = 13
    '  If intHeight > 0 Then ctrl.Height = intHeight

    '  If intLeft = -1 Then actX += ctrl.Width + 5 ' If insertDir = insertDirection.Horizontal Then
    '  If intTop > -1 Then ctrl.Top = intTop Else ctrl.Top = actY ': If insertDir = insert Direction.Vertical Then actY += ctrl.Height + 5
    '  If ctrl.Height > lastRowHeight Then lastRowHeight = ctrl.Height

    '  Me.Controls.Add(ctrl)
    'Catch ex As Exception
    '  MsgBox("Fehler" + vbNewLine + ex.ToString)
    'End Try
  End Sub

  'Sub setVisible(ByVal stat)
  '  Me.Visible = stat
  'End Sub
  Public Sub setBackColor(ByVal col As String) Implements IScriptedPanel.setBackColor
    Me.BackColor = ColorTranslator.FromHtml(col)
  End Sub

  Default Public ReadOnly Property Element(ByVal id) Implements IScriptedPanel.Element
    Get
      Return Me.Items(id)
      Return Nothing
    End Get
  End Property
  Public ReadOnly Property HasElement(ByVal id) Implements IScriptedPanel.HasElement
    Get
      On Error Resume Next
      If Me.Controls.Find(id, True).Length > 0 Then Return True
      ' If Me.Controls.Find("txt_" + id, True).Length > 0 Then Return True
      ' If Me.Controls.Find("btn_" + id, True).Length > 0 Then Return True
      Return False
    End Get
  End Property


  Sub resetControls(Optional ByVal startX = 0, Optional ByVal startY = 0, Optional ByVal dir = 1) Implements IScriptedPanel.resetControls
    Me.Items.Clear()
    p_actMenu = Nothing
  End Sub

  Function addLabel(ByVal strID, ByVal strText, Optional ByVal bgColor = "", Optional ByVal fgColor = "", Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1) Implements IScriptedPanel.addLabel
    If strText = "-" Then 'Trennzeichen
      Return Me.Items.Add("-")
    End If

    Dim lab As New ToolStripLabel
    strID = ScriptedObjectHelper.GenerateElementId("lab", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Items.Add(lab)
    lab.Name = strID ' "lab_" + strID
    lab.Text = strText
    lab.TextAlign = ContentAlignment.MiddleLeft
    lab.Tag = New Object() {ClassName, strID, "", "", "", "", "", "", "", ""}
    If bgColor <> "" Then lab.BackColor = ColorTranslator.FromHtml(bgColor)
    If fgColor <> "" Then lab.ForeColor = ColorTranslator.FromHtml(fgColor)

    'Positionierung u. Größe
    lab.AutoSize = True
    If intWidth <> -1 Or intHeight <> -1 Then lab.AutoSize = False
    If intWidth > 0 Then lab.Width = intWidth
    If intHeight > 0 Then lab.Height = intHeight

    AddHandler lab.MouseUp, AddressOf lab_Mouseclick
    'ZZ.IDEHelper.RefreshToolbarSize()
    Return lab
  End Function
  Private Sub lab_Mouseclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("MouseClick", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    'ref.onLabelEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Label", eventArgs)
  End Sub

  Function addSubMenuItem(ByVal Typ, ByVal strID, ByVal btnText, Optional ByVal bgColor = "", Optional ByVal iconUrl = "", Optional ByVal flags = 0, Optional ByVal handler = Nothing)
    Dim item As ToolStripDropDownItem = DirectCast(p_actMenu, Object).DropDownItems.Add(btnText, ResourceLoader.GetImageCached(iconUrl), handler)
    If Typ = "Label" Then item.Enabled = False
    If bgColor <> "" Then item.BackColor = ColorTranslator.FromHtml(bgColor)
    item.Name = "ddi_" + strID
    AddHandler item.Click, AddressOf btn_Mouseclick
    Return item
  End Function

#Region "Button"
  Function addButton(ByVal strID, ByVal btnText, Optional ByVal bgColor = "", Optional ByVal btnLeft = -1, Optional ByVal btnTop = -1, Optional ByVal btnWidth = -1, Optional ByVal btnHeight = -1, Optional ByVal iconUrl = "", Optional ByVal flags = 0, Optional ByVal handler = Nothing) Implements IScriptedPanel.addButton
    If p_actMenu IsNot Nothing Then
      Return addSubMenuItem("Button", strID, btnText, bgColor, iconUrl, flags, handler)
    End If

    Dim btn As ToolStripItem
    If flags And ButtonFlags.StartMenu Then
      If flags And ButtonFlags.IsSplitButton Then
        Dim btn2 As New ToolStripSplitButton
        AddHandler btn2.DropDownItemClicked, AddressOf mnu_ItemClicked
        AddHandler btn2.DropDownOpening, AddressOf mnu_DropDownOpening
        AddHandler btn2.ButtonClick, AddressOf btn_Mouseclick
        btn = btn2
      Else
        Dim btn2 As New ToolStripDropDownButton
        AddHandler btn2.DropDownItemClicked, AddressOf mnu_ItemClicked
        AddHandler btn2.DropDownOpening, AddressOf mnu_DropDownOpening
        btn = btn2
      End If
      p_actMenu = btn
    Else
      btn = New ToolStripButton
      AddHandler btn.MouseUp, AddressOf btn_Mouseclick
    End If

    strID = ScriptedObjectHelper.GenerateElementId("btn", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Items.Add(btn)
    btn.Name = strID
    If iconUrl <> "" Then
      btn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
      btn.Image = ResourceLoader.GetImageCached(iconUrl)
    End If
    btn.Text = btnText
    btn.Tag = New Object() {ClassName, strID, "", "", "", "", "", "", "", ""}
    If bgColor <> "" Then btn.BackColor = ColorTranslator.FromHtml(bgColor)
    If btnHeight <= 0 And btnWidth <= 0 Then btn.AutoSize = True Else btn.Height = btnHeight : btn.Width = btnWidth

    'IdeHelper.RefreshToolbarSize()
    Return btn
  End Function

  Function addButtonEx(ByVal btnID, ByVal btnText, Optional ByVal bgColor = "", Optional ByVal btnLeft = -1, Optional ByVal btnTop = -1, Optional ByVal btnWidth = -1, Optional ByVal btnHeight = -1, Optional ByVal handler = Nothing) Implements IScriptedPanel.addButtonEx
    Dim btn As Button = addButton(btnID, btnText, bgColor, btnLeft, btnTop, btnWidth, btnHeight, , , handler)
    Return btn
  End Function

  Private Sub btn_Mouseclick(ByVal sender As Object, ByVal e As Object)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim EventArgs As ScriptEventArgs
    If TypeOf e Is System.Windows.Forms.MouseEventArgs Then
      EventArgs = New ScriptEventArgs("MouseClick", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    Else
      EventArgs = New ScriptEventArgs("MouseClick", sender, , , , , ClassName)
    End If
    'ref.onButtonEvent(EventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Button", EventArgs)
  End Sub

#End Region



#Region "Menu"
  Function addMenu(ByVal strMenuID, ByVal strButtonID, ByVal strMouseButton, ByVal ParamArray menuItems()) Implements IScriptedPanel.addMenu
    Dim mnu As New ToolStripDropDownButton
    mnu.Name = "ddb_" + strMenuID
    mnu.Text = strButtonID
    Me.Items.Add(mnu)
    AddHandler mnu.DropDownItemClicked, AddressOf mnu_ItemClicked
    AddHandler mnu.DropDownOpening, AddressOf mnu_DropDownOpening

    If strMouseButton <> "" Then
      mnu.Image = ResourceLoader.GetImageCached(strMouseButton)
    End If

    For Each txt In menuItems
      mnu.DropDownItems.Add(txt)
    Next

    Return mnu
  End Function
  Private Sub mnu_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim mnu As ContextMenuStrip = sender.Tag(8) 'Element(sender.Tag(8))
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("BeforeOpen", sender, "", -1, -1, , ClassName)
    eventArgs.Menu = sender
    'Dim result = ref.onMenuEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Menu", eventArgs)
    'If eventArgs.Cancel = True Then
    'mnu.Show(sender, e.Location)
  End Sub
  Sub mnu_ItemClicked(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs2 As New ScriptEventArgs("ItemClicked", e.ClickedItem, , , , e.ClickedItem.Text, ClassName)
    eventArgs2.Menu = sender
    eventArgs2.ID = sender.Name
    'ref.onMenuEvent(eventArgs2)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Menu", eventArgs2)
  End Sub

#End Region



#Region "Icon"
  Function addIcon(ByVal strID, ByVal strURL, Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1) Implements IScriptedPanel.addIcon
    Dim btn As New ToolStripButton
    strID = ScriptedObjectHelper.GenerateElementId("btn", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Items.Add(btn)
    btn.Name = strID
    btn.DisplayStyle = ToolStripItemDisplayStyle.Image
    'btn.Text = btnText
    btn.Image = ResourceLoader.GetImageCached(strURL)
    btn.Tag = New Object() {ClassName, strID, "", "", "", "", "", "", "", ""}
    'If bgColor <> "" Then btn.BackColor = ColorTranslator.FromHtml(bgColor)
    'If btnHeight <= 0 And btnWidth <= 0 Then btn.AutoSize = True Else btn.Height = btnHeight : btn.Width = btnWidth
    'If btnLeft > -1 Then btn.Left = btnLeft Else btn.Left = actX : actX += btn.Width + 5
    'If btnTop > -1 Then btn.Top = btnTop Else btn.Top = actY ': If insertDir = insertDirection.Vertical Then actY += btn.Height + 5
    'If btn.Height > lastRowHeight Then lastRowHeight = btn.Height

    'If eventHandlerTypes.Contains("|BUTTONMOUSECLICK|") Then
    AddHandler btn.MouseUp, AddressOf btn_Mouseclick
    '    IdeHelper.RefreshToolbarSize()
    Return btn
    'Dim pic As New PictureBox
    'If Me.Controls.ContainsKey("pic_" + strID) Then Me.Controls.RemoveByKey("pic_" + strID)
    'Me.Controls.Add(pic)
    'pic.Name = "pic_" + strID
    'Dim hash = cls_scriptHelper.getMD5Hash(strURL)
    'If strURL.startswith("http") Then
    '  If IO.File.Exists(settingsFolder + "iconCache\" + hash + ".png") Then
    '    pic.Image = Image.FromFile(settingsFolder + "iconCache\" + hash + ".png")
    '  Else
    '    AddHandler pic.LoadCompleted, AddressOf pic_LoadCompleted
    '    pic.LoadAsync(strURL)
    '  End If
    'Else
    '  pic.Image = Image.FromFile(strURL)
    'End If
    'pic.Tag = New Object() {ClassName, strID, strURL, hash, "", "", "", "", "", ""}
    'If intHeight <= 0 And intWidth <= 0 Then pic.SizeMode = PictureBoxSizeMode.AutoSize Else pic.Height = intHeight : pic.Width = intWidth : pic.SizeMode = PictureBoxSizeMode.StretchImage
    'If intLeft > -1 Then pic.Left = intLeft Else pic.Left = actX : actX += pic.Width + 5
    'If intTop > -1 Then pic.Top = intTop Else pic.Top = actY ': If insertDir = insertDirection.Vertical Then actY += btn.Height + 5
    'If pic.Height > lastRowHeight Then lastRowHeight = pic.Height

    ''If eventHandlerTypes.Contains("|ICONMOUSECLICK|") Then 
    'AddHandler pic.MouseClick, AddressOf pic_Mouseclick
    'Return pic
  End Function
  Sub pic_LoadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    DirectCast(sender, PictureBox).Image.Save(ParaService.SettingsFolder + "iconCache\" + sender.Tag(3) + ".png")
  End Sub
  Private Sub pic_Mouseclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("MouseClick", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    'ref.onIconEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Icon", eventArgs)
  End Sub
#End Region



#Region "Textbox"
  Sub addTextbox(ByVal ID, ByVal XX, Optional ByVal labelText = "", Optional ByVal labelXX = 0, Optional ByVal labelBgColor = "", Optional ByVal x = -1, Optional ByVal y = -1, Optional ByVal flags = "") Implements IScriptedPanel.addTextbox
    Dim txt As New ToolStripTextBox
    ID = ScriptedObjectHelper.GenerateElementId("txt", ID, Me)
    If Me.Controls.ContainsKey(ID) Then Me.Controls.RemoveByKey(ID)
    Me.Items.Add(txt)
    txt.Name = ID

    'If x > -1 Then txt.Left = x + labelXX Else txt.Left = actX + labelXX
    'If XX = -2 Then
    '  txt.Width = Me.Width - txt.Left - 10 : txt.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right
    'Else : 
    txt.Width = XX
    'End If

    txt.Tag = New Object() {ClassName, ID, "", "", "", "", "", "", "", ""}

    'Dim abPos = flags.indexof("multiline=")
    'If abPos > -1 Then
    '  txt.Multiline = True : txt.Height = flags.substring(abPos + 10, (flags + " ").indexof(" ", abPos) - abPos - 10)
    'End If
    'If x = -1 Then actX += labelXX + txt.Width + 10
    'If y > -1 Then txt.Top = y Else txt.Top = actY ': If insertDir = insertDirection.Vertical And noLineBreak = False Then actY += txt.Height + 5
    'If txt.Height > lastRowHeight Then lastRowHeight = txt.Height

    'If labelText <> "" Then
    '  Dim lab As New Label
    '  Me.Controls.Add(lab)
    '  lab.AutoSize = False
    '  lab.Text = labelText
    '  lab.Height = txt.Height
    '  lab.Width = labelXX
    '  lab.Name = "txtDesc_" + ID
    '  lab.TextAlign = ContentAlignment.MiddleLeft
    '  txt.Left += 5
    '  lab.Left = txt.Left - labelXX - 5
    '  'If x > -1 Then lab.Left = x Else lab.Left = insertX
    '  lab.Top = txt.Top

    '  If labelBgColor <> "" Then lab.BackColor = ColorTranslator.FromHtml(labelBgColor)
    'End If

    'If eventHandlerTypes.Contains("|TEXTBOXGOTFOCUS|") Then
    AddHandler txt.GotFocus, AddressOf txt_GotFocus
    'If eventHandlerTypes.Contains("|TEXTBOXKEYDOWN|") Then
    AddHandler txt.KeyDown, AddressOf txt_KeyDown
    'If eventHandlerTypes.Contains("|TEXTBOXLOSTFOCUS|") Then
    AddHandler txt.LostFocus, AddressOf txt_LostFocus
    'If eventHandlerTypes.Contains("|TEXTBOXMOUSEUP|") Then
    AddHandler txt.MouseUp, AddressOf txt_MouseUp
    'If eventHandlerTypes.Contains("|TEXTBOXTEXTCHANGED|") Then
    AddHandler txt.TextChanged, AddressOf txt_TextChanged
  End Sub

  Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("GotFocus", sender, , , , , ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
  Private Sub txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("KeyDown", sender, , , , ScriptWindowManager.getKeyString(e), ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
  Private Sub txt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("LostFocus", sender, , , , , ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
  Private Sub txt_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("MouseUp", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
  Private Sub txt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("TextChanged", sender, , , , , ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
#End Region

  Public Property activateEvents() As String Implements IScriptedPanel.activateEvents
    Get
    End Get
    Set(ByVal value As String)
    End Set
  End Property

  Public Property direction() As Integer Implements IScriptedPanel.direction
    Get
    End Get
    Set(ByVal value As Integer)
    End Set
  End Property

  Public Property insertX() As Integer Implements IScriptedPanel.insertX
    Get
    End Get
    Set(ByVal value As Integer)
    End Set
  End Property

  Public Property insertY() As Integer Implements IScriptedPanel.insertY
    Get
    End Get
    Set(ByVal value As Integer)
    End Set
  End Property

  Public Property offsetX() As Integer Implements IScriptedPanel.offsetX
    Get
    End Get
    Set(ByVal value As Integer)
    End Set
  End Property

  Public Sub FinishMenu() Implements IScriptedPanel.FinishMenu
    p_actMenu = Nothing
  End Sub

  Public ReadOnly Property Form() As System.Windows.Forms.Form Implements IScriptedPanel.Form
    Get
      Return Me.FindForm()
    End Get
  End Property

  Public ReadOnly Property Window() As IScriptedWindow Implements IScriptedPanel.Window
    Get
      Return Me.FindForm()
    End Get
  End Property

  Public Function addCheckbox(ByVal strID As Object, ByVal strText As Object, Optional ByVal bgColor As Object = "", Optional ByVal fgColor As Object = "", Optional ByVal intLeft As Object = -1, Optional ByVal intTop As Object = -1, Optional ByVal intWidth As Object = -1, Optional ByVal intHeight As Object = -1) As Object Implements IScriptedPanel.addCheckbox

  End Function
End Class






Public Class ScriptedPanel
  Inherits Panel
  Implements IScriptedPanel


  Private p_className As String
  Private p_winID As String

  Public Property WinID() As String Implements IScriptedPanel.winID
    Get
      Return p_winID
    End Get
    Set(ByVal value As String)
      p_winID = value

      'If (Not String.IsNullOrEmpty(value)) AndAlso value.Contains(".") Then
      '  Dim clsName = value.Substring(0, value.IndexOf("."))
      '  ' If ScriptHost.Instance.expandScriptClassName(clsName) <> "" Then
      '  p_className = clsName
      '  'End If
      'End If
    End Set
  End Property

  Public Property ClassName() As String Implements IScriptedPanel.className
    Get
      Return p_className
    End Get
    Set(ByVal value As String)
      p_className = value
    End Set
  End Property

  Private p_offsetX As Integer = 0, p_offsetY As Integer = 0
  Private actX As Integer = 0, actY As Integer = 0
  Private insertDir As Integer = 1 ' As insertDirection
  Private lastRowHeight As Integer = 0

  Private eventHandlerTypes As String = ""


  Property activateEvents() As String Implements IScriptedPanel.activateEvents
    Get
      Return eventHandlerTypes
    End Get
    Set(ByVal value As String)
      eventHandlerTypes = value.ToUpper
    End Set
  End Property

  Property offsetX() As Integer Implements IScriptedPanel.offsetX
    Get
      Return p_offsetX
    End Get
    Set(ByVal value As Integer)
      p_offsetX = value
      actX = value
    End Set
  End Property
  Property insertX() As Integer Implements IScriptedPanel.insertX
    Get
      Return actX
    End Get
    Set(ByVal value As Integer)
      actX = value
    End Set
  End Property
  Property insertY() As Integer Implements IScriptedPanel.insertY
    Get
      Return actY
    End Get
    Set(ByVal value As Integer)
      actY = value
    End Set
  End Property
  Property direction() As Integer Implements IScriptedPanel.direction
    Get
      Return insertDir
    End Get
    Set(ByVal value As Integer)
      insertDir = value
    End Set
  End Property

  Public Enum insertDirection
    Horizontal = 1
    Vertical = 2
  End Enum

  'Private Sub frmTB_scriptedContent_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
  '  If eventHandlerTypes.Contains("|FORMKEYDOWN|") Then
  '    Dim ref = scriptClass(className)
  '    ref.onFormEvent(New cls_scriptEventObject("KeyDown", sender, , , , getKeyString(e)))
  '  End If
  'End Sub

  Sub BR(Optional ByVal rowHeight As Integer = -1) Implements IScriptedPanel.BR
    actX = offsetX
    If rowHeight > -1 Then actY += rowHeight Else actY += lastRowHeight + 5
    lastRowHeight = 0
  End Sub

  Sub addControl(ByVal strID, ByVal strProgID, Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1) Implements IScriptedPanel.addControl
    Try
      Dim typ = Type.GetType(CStr(strProgID), True)
      Dim ctrl As Control = Activator.CreateInstance(typ)
      ctrl.Name = strID

      If intLeft > -1 Then ctrl.Left = intLeft Else ctrl.Left = actX
      If intWidth > 0 Then ctrl.Width = intWidth
      If intWidth = -2 Then ctrl.Width = Me.Width - ctrl.Left - 10 : ctrl.Anchor = 13
      If intHeight > 0 Then ctrl.Height = intHeight

      If intLeft = -1 Then actX += ctrl.Width + 5 ' If insertDir = insertDirection.Horizontal Then
      If intTop > -1 Then ctrl.Top = intTop Else ctrl.Top = actY ': If insertDir = insert Direction.Vertical Then actY += ctrl.Height + 5
      If ctrl.Height > lastRowHeight Then lastRowHeight = ctrl.Height

      Me.Controls.Add(ctrl)
    Catch ex As Exception
      MsgBox("Fehler" + vbNewLine + ex.ToString)
    End Try
  End Sub

  Function addToolstrip(ByVal strID, Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1) As ToolStrip
    Dim ts As New ScriptedToolstrip
    strID = ScriptedObjectHelper.GenerateElementId("ts", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Controls.Add(ts)
    ts.Name = strID : ts.AutoSize = True : ts.LayoutStyle = ToolStripLayoutStyle.Flow
    ts.WinID = Me.WinID
    'ts.Renderer=New 

    If intLeft > -1 Then ts.Left = intLeft Else ts.Left = actX
    If intWidth > 0 Then ts.Width = intWidth
    If intWidth = -2 Then ts.Width = Me.Width - ts.Left - 10 : ts.Anchor = 13
    If intLeft = -1 Then actX += ts.Width + 5 ' If insertDir = insertDirection.Horizontal Then
    If intTop > -1 Then ts.Top = intTop Else ts.Top = actY ': If insertDir = insert Direction.Vertical Then actY += ctrl.Height + 5
    If ts.Height > lastRowHeight Then lastRowHeight = ts.Height

    Return ts
  End Function

  Function addPanel(ByVal strID As String, Optional ByVal dock As DockStyle = DockStyle.None, Optional ByVal intLeft As Integer = -1, Optional ByVal intTop As Integer = -1, Optional ByVal intWidth As Integer = -1, Optional ByVal intHeight As Integer = -1) As ScriptedPanel
    Dim pnl As New ScriptedPanel
    strID = ScriptedObjectHelper.GenerateElementId("pnl", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Controls.Add(pnl)
    pnl.Name = strID
    pnl.Dock = dock
    pnl.WinID = Me.WinID : pnl.ClassName = Me.ClassName

    If dock = DockStyle.None Then
      If intLeft > -1 Then pnl.Left = intLeft Else pnl.Left = actX
    End If
    If intWidth > 0 Then pnl.Width = intWidth
    If intWidth = -2 Then pnl.Width = Me.Width - pnl.Left - 10 : pnl.Anchor = 13
    If intHeight > 0 Then pnl.Height = intHeight

    If dock = DockStyle.None Then
      If intLeft = -1 Then actX += pnl.Width + 5 ' If insertDir = insertDirection.Horizontal Then
      If intTop > -1 Then pnl.Top = intTop Else pnl.Top = actY ': If insertDir = insert Direction.Vertical Then actY += ctrl.Height + 5
    End If
    If pnl.Height > lastRowHeight Then lastRowHeight = pnl.Height

    Return pnl
  End Function


  Function addSplitcontainer(ByVal strID As String, ByVal strPnl1ID As String, ByRef pnl1Ref As ScriptedPanel, ByVal strPnl2ID As String, ByRef pnl2Ref As ScriptedPanel, Optional ByVal dock As DockStyle = DockStyle.None, Optional ByVal intLeft As Integer = -1, Optional ByVal intTop As Integer = -1, Optional ByVal intWidth As Integer = -1, Optional ByVal intHeight As Integer = -1) As SplitContainer
    Dim pnl As New SplitContainer
    strID = ScriptedObjectHelper.GenerateElementId("pnl", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Controls.Add(pnl)
    pnl.Name = strID
    pnl.Dock = dock

    pnl1Ref = New ScriptedPanel
    pnl.Panel1.Controls.Add(pnl1Ref)
    pnl1Ref.Name = strPnl1ID
    pnl1Ref.Dock = DockStyle.Fill
    pnl1Ref.WinID = Me.WinID : pnl1Ref.ClassName = Me.ClassName

    pnl2Ref = New ScriptedPanel
    pnl.Panel2.Controls.Add(pnl2Ref)
    pnl2Ref.Name = strPnl2ID
    pnl2Ref.Dock = DockStyle.Fill
    pnl2Ref.WinID = Me.WinID : pnl2Ref.ClassName = Me.ClassName

    If dock = DockStyle.None Then
      If intLeft > -1 Then pnl.Left = intLeft Else pnl.Left = actX
    End If
    If intWidth > 0 Then pnl.Width = intWidth
    If intWidth = -2 Then pnl.Width = Me.Width - pnl.Left - 10 : pnl.Anchor = 13
    If intHeight > 0 Then pnl.Height = intHeight

    If dock = DockStyle.None Then
      If intLeft = -1 Then actX += pnl.Width + 5 ' If insertDir = insertDirection.Horizontal Then
      If intTop > -1 Then pnl.Top = intTop Else pnl.Top = actY ': If insertDir = insert Direction.Vertical Then actY += ctrl.Height + 5
    End If
    If pnl.Height > lastRowHeight Then lastRowHeight = pnl.Height


    Return pnl
  End Function


  'Sub setVisible(ByVal stat)
  '  Me.Visible = stat
  'End Sub
  Public Sub setBackColor(ByVal col As String) Implements IScriptedPanel.setBackColor
    Me.BackColor = ColorTranslator.FromHtml(col)
  End Sub

  Default Public ReadOnly Property Element(ByVal id) Implements IScriptedPanel.Element
    Get
      On Error Resume Next
      Dim ctrls() As Control
      ctrls = Me.Controls.Find(id, True) : If ctrls.Length > 0 Then Return ctrls(0)
      ctrls = Me.Controls.Find("txt_" + id, True) : If ctrls.Length > 0 Then Return ctrls(0)
      ctrls = Me.Controls.Find("btn_" + id, True) : If ctrls.Length > 0 Then Return ctrls(0)
      ctrls = Me.Controls.Find("lab_" + id, True) : If ctrls.Length > 0 Then Return ctrls(0)
      ctrls = Me.Controls.Find("pic_" + id, True) : If ctrls.Length > 0 Then Return ctrls(0)
      Return Nothing
    End Get
  End Property
  Public ReadOnly Property HasElement(ByVal id) Implements IScriptedPanel.HasElement
    Get
      On Error Resume Next
      If Me.Controls.Find(id, True).Length > 0 Then Return True
      If Me.Controls.Find("txt_" + id, True).Length > 0 Then Return True
      If Me.Controls.Find("btn_" + id, True).Length > 0 Then Return True
      If Me.Controls.Find("lab_" + id, True).Length > 0 Then Return True
      If Me.Controls.Find("pic_" + id, True).Length > 0 Then Return True
      Return False
    End Get
  End Property


  Sub resetControls(Optional ByVal startX = 0, Optional ByVal startY = 0, Optional ByVal dir = 1) Implements IScriptedPanel.resetControls
    Me.Controls.Clear()
    actX = startX : actY = startY ': insertDir = dir
    offsetX = startX : lastRowHeight = 0
  End Sub

  Public Function addCheckbox(ByVal strID As Object, ByVal strText As Object, Optional ByVal bgColor As Object = "", Optional ByVal fgColor As Object = "", Optional ByVal intLeft As Object = -1, Optional ByVal intTop As Object = -1, Optional ByVal intWidth As Object = -1, Optional ByVal intHeight As Object = -1) As Object Implements IScriptedPanel.addCheckbox
    Dim chk As New CheckBox
    strID = ScriptedObjectHelper.GenerateElementId("chk", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Controls.Add(chk)
    chk.Name = strID
    chk.Text = strText
    chk.TextAlign = ContentAlignment.MiddleLeft
    chk.Tag = New Object() {ClassName, strID, "", "", "", "", "", "", "", ""}
    If bgColor <> "" Then chk.BackColor = ColorTranslator.FromHtml(bgColor)
    If fgColor <> "" Then chk.ForeColor = ColorTranslator.FromHtml(fgColor)

    'Positionierung u. Größe
    chk.AutoSize = True
    If intWidth <> -1 Or intHeight <> -1 Then chk.AutoSize = False
    If intLeft > -1 Then chk.Left = intLeft Else chk.Left = actX
    If intWidth = -2 Then chk.Width = Me.Width - chk.Left - 10 : chk.Anchor = 13
    If intWidth > 0 Then chk.Width = intWidth
    If intHeight > 0 Then chk.Height = intHeight
    If intLeft = -1 Then actX += chk.Width + 5
    If intTop > -1 Then chk.Top = intTop Else chk.Top = actY ': If insertDir = insertDirection.Vertical Then actY += btn.Height + 5
    If chk.Height > lastRowHeight Then lastRowHeight = chk.Height

    'If eventHandlerTypes.Contains("|LABELMOUSECLICK|") Then 
    AddHandler chk.CheckedChanged, AddressOf chk_CheckedChanged
    AddHandler chk.MouseClick, AddressOf chk_MouseClick
    Return chk
  End Function
  Private Sub chk_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("CheckedChanged", sender, , , , , ClassName)
    'ref.onLabelEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "CheckBox", eventArgs)
  End Sub
  Private Sub chk_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("MouseClick", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    'ref.onLabelEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "CheckBox", eventArgs)
  End Sub

  Function addLabel(ByVal strID, ByVal strText, Optional ByVal bgColor = "", Optional ByVal fgColor = "", Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1) Implements IScriptedPanel.addLabel
    Dim lab As New Label
    strID = ScriptedObjectHelper.GenerateElementId("lab", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Controls.Add(lab)
    lab.Name = strID
    lab.Text = strText
    lab.TextAlign = ContentAlignment.MiddleLeft
    lab.Tag = New Object() {ClassName, strID, "", "", "", "", "", "", "", ""}
    If bgColor <> "" Then lab.BackColor = ColorTranslator.FromHtml(bgColor)
    If fgColor <> "" Then lab.ForeColor = ColorTranslator.FromHtml(fgColor)

    'Positionierung u. Größe
    lab.AutoSize = True
    If intWidth <> -1 Or intHeight <> -1 Then lab.AutoSize = False
    If intLeft > -1 Then lab.Left = intLeft Else lab.Left = actX
    If intWidth = -2 Then lab.Width = Me.Width - lab.Left - 10 : lab.Anchor = 13
    If intWidth > 0 Then lab.Width = intWidth
    If intHeight > 0 Then lab.Height = intHeight
    If intLeft = -1 Then actX += lab.Width + 5
    If intTop > -1 Then lab.Top = intTop Else lab.Top = actY ': If insertDir = insertDirection.Vertical Then actY += btn.Height + 5
    If lab.Height > lastRowHeight Then lastRowHeight = lab.Height

    'If eventHandlerTypes.Contains("|LABELMOUSECLICK|") Then 
    AddHandler lab.MouseClick, AddressOf lab_Mouseclick
    Return lab
  End Function
  Private Sub lab_Mouseclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("MouseClick", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    'ref.onLabelEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Label", eventArgs)
  End Sub


#Region "Button"
  Public Function addButton(ByVal strID As Object, ByVal btnText As Object, Optional ByVal bgColor As Object = "", Optional ByVal btnLeft As Object = -1, Optional ByVal btnTop As Object = -1, Optional ByVal btnWidth As Object = -1, Optional ByVal btnHeight As Object = -1, Optional ByVal iconUrl As Object = "", Optional ByVal flags As Object = 0, Optional ByVal handler As Object = Nothing) As Object Implements IScriptedPanel.addButton
    Dim btn As New Button
    strID = ScriptedObjectHelper.GenerateElementId("btn", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Controls.Add(btn)
    btn.Name = strID
    btn.Text = btnText
    If iconUrl <> "" Then
      btn.TextImageRelation = TextImageRelation.ImageBeforeText
      btn.Image = ResourceLoader.GetImageCached(iconUrl)
    End If
    btn.Tag = New Object() {ClassName, strID, "", "", "", "", "", "", "", ""}
    If bgColor <> "" Then btn.BackColor = ColorTranslator.FromHtml(bgColor)
    If btnHeight <= 0 And btnWidth <= 0 Then btn.AutoSize = True Else btn.Height = btnHeight : btn.Width = btnWidth
    If btnLeft > -1 Then btn.Left = btnLeft Else btn.Left = actX : actX += btn.Width + 5
    If btnTop > -1 Then btn.Top = btnTop Else btn.Top = actY ': If insertDir = insertDirection.Vertical Then actY += btn.Height + 5
    If btn.Height > lastRowHeight Then lastRowHeight = btn.Height

    'If eventHandlerTypes.Contains("|BUTTONMOUSECLICK|") Then
    If handler IsNot Nothing Then AddHandler btn.Click, handler
    AddHandler btn.MouseClick, AddressOf btn_Mouseclick
    Return btn
  End Function
  Function addButtonEx(ByVal btnID, ByVal btnText, Optional ByVal bgColor = "", Optional ByVal btnLeft = -1, Optional ByVal btnTop = -1, Optional ByVal btnWidth = -1, Optional ByVal btnHeight = -1, Optional ByVal handler = Nothing) Implements IScriptedPanel.addButtonEx
    Return addButton(btnID, btnText, bgColor, btnLeft, btnTop, btnWidth, btnHeight)
  End Function
  Private Sub btn_Mouseclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("MouseClick", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    'ref.onButtonEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Button", eventArgs)
  End Sub
#End Region



#Region "Menu"
  Function addMenu(ByVal strMenuID, ByVal strButtonID, ByVal strMouseButton, ByVal ParamArray menuItems()) Implements IScriptedPanel.addMenu
    Dim appendTo As Control = Element(strButtonID)
    If appendTo Is Nothing Then Throw New System.Collections.Generic.KeyNotFoundException("Control " + strButtonID + " existiert nicht")

    Dim mnu As New ContextMenuStrip
    mnu.Name = "menu_" + strMenuID
    AddHandler mnu.ItemClicked, AddressOf mnu_ItemClicked
    'Me.Controls.Add(mnu)
    If strMouseButton.tolower.startswith("r") Then 'rechte Taste -- ganz einfach
      appendTo.ContextMenuStrip = mnu
    Else
      AddHandler appendTo.MouseClick, AddressOf menubtn_Mouseclick
      appendTo.Tag(8) = mnu
    End If
    For Each txt In menuItems
      mnu.Items.Add(txt)
    Next

    Return mnu
  End Function
  Private Sub menubtn_Mouseclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    Dim mnu As ContextMenuStrip = sender.Tag(8) 'Element(sender.Tag(8))
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("BeforeOpen", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    eventArgs.Menu = mnu
    'Dim result = ref.onMenuEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Menu", eventArgs)
    If eventArgs.Cancel = True Then Exit Sub
    mnu.Show(sender, e.Location)
  End Sub
  Sub mnu_ItemClicked(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs2 As New ScriptEventArgs("ItemClicked", e.ClickedItem, , , , e.ClickedItem.Text, ClassName)
    eventArgs2.Menu = sender
    'ref.onMenuEvent(eventArgs2)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Menu", eventArgs2)
  End Sub
#End Region




#Region "Icon"
  Function addIcon(ByVal strID, ByVal strURL, Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1) Implements IScriptedPanel.addIcon
    Dim pic As New PictureBox
    strID = ScriptedObjectHelper.GenerateElementId("pic", strID, Me)
    If Me.Controls.ContainsKey(strID) Then Me.Controls.RemoveByKey(strID)
    Me.Controls.Add(pic)
    pic.Name = strID
    Dim hash = ResourceLoader.GetMD5Hash(strURL)
    If strURL.startswith("http") Then
      If IO.File.Exists(ParaService.SettingsFolder + "iconCache\" + hash + ".png") Then
        pic.Image = Image.FromFile(ParaService.SettingsFolder + "iconCache\" + hash + ".png")
      Else
        AddHandler pic.LoadCompleted, AddressOf pic_LoadCompleted
        pic.LoadAsync(strURL)
      End If
    Else
      pic.Image = Image.FromFile(strURL)
    End If
    pic.Tag = New Object() {ClassName, strID, strURL, hash, "", "", "", "", "", ""}
    If intHeight <= 0 And intWidth <= 0 Then pic.SizeMode = PictureBoxSizeMode.AutoSize Else pic.Height = intHeight : pic.Width = intWidth : pic.SizeMode = PictureBoxSizeMode.StretchImage
    If intLeft > -1 Then pic.Left = intLeft Else pic.Left = actX : actX += pic.Width + 5
    If intTop > -1 Then pic.Top = intTop Else pic.Top = actY ': If insertDir = insertDirection.Vertical Then actY += btn.Height + 5
    If pic.Height > lastRowHeight Then lastRowHeight = pic.Height

    'If eventHandlerTypes.Contains("|ICONMOUSECLICK|") Then 
    AddHandler pic.MouseClick, AddressOf pic_Mouseclick
    Return pic
  End Function
  Sub pic_LoadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    DirectCast(sender, PictureBox).Image.Save(ParaService.SettingsFolder + "iconCache\" + sender.Tag(3) + ".png")
  End Sub
  Private Sub pic_Mouseclick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    On Error Resume Next
    ScriptWindowManager.ReleaseCapture()
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("MouseClick", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    'ref.onIconEvent(eventArgs)
    'ref.onButtonEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Button", eventArgs)
  End Sub
#End Region



#Region "Textbox"
  Sub addTextbox(ByVal ID, ByVal XX, Optional ByVal labelText = "", Optional ByVal labelXX = 0, Optional ByVal labelBgColor = "", Optional ByVal x = -1, Optional ByVal y = -1, Optional ByVal flags = "") Implements IScriptedPanel.addTextbox
    Dim txt As New TextBox
    If Me.Controls.ContainsKey("txt_" + ID) Then Me.Controls.RemoveByKey("txt_" + ID)
    Me.Controls.Add(txt)
    txt.Name = "txt_" + ID

    If x > -1 Then txt.Left = x + labelXX Else txt.Left = actX + labelXX
    If XX = -2 Then
      txt.Width = Me.Width - txt.Left - 10 : txt.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right
    Else : txt.Width = XX
    End If

    txt.Tag = New Object() {ClassName, ID, "", "", "", "", "", "", "", ""}

    Dim abPos = flags.indexof("multiline=")
    If abPos > -1 Then
      txt.Multiline = True : txt.Height = flags.substring(abPos + 10, (flags + " ").indexof(" ", abPos) - abPos - 10)
    End If
    If x = -1 Then actX += labelXX + txt.Width + 10
    If y > -1 Then txt.Top = y Else txt.Top = actY ': If insertDir = insertDirection.Vertical And noLineBreak = False Then actY += txt.Height + 5
    If txt.Height > lastRowHeight Then lastRowHeight = txt.Height

    If labelText <> "" Then
      Dim lab As New Label
      Me.Controls.Add(lab)
      lab.AutoSize = False
      lab.Text = labelText
      lab.Height = txt.Height
      lab.Width = labelXX
      lab.Name = "txtDesc_" + ID
      lab.TextAlign = ContentAlignment.MiddleLeft
      txt.Left += 5
      lab.Left = txt.Left - labelXX - 5
      'If x > -1 Then lab.Left = x Else lab.Left = insertX
      lab.Top = txt.Top

      If labelBgColor <> "" Then lab.BackColor = ColorTranslator.FromHtml(labelBgColor)
    End If

    'If eventHandlerTypes.Contains("|TEXTBOXGOTFOCUS|") Then
    AddHandler txt.GotFocus, AddressOf txt_GotFocus
    'If eventHandlerTypes.Contains("|TEXTBOXKEYDOWN|") Then 
    AddHandler txt.KeyDown, AddressOf txt_KeyDown
    'If eventHandlerTypes.Contains("|TEXTBOXLOSTFOCUS|") Then 
    AddHandler txt.LostFocus, AddressOf txt_LostFocus
    'If eventHandlerTypes.Contains("|TEXTBOXMOUSEUP|") Then 
    AddHandler txt.MouseUp, AddressOf txt_MouseUp
    'If eventHandlerTypes.Contains("|TEXTBOXTEXTCHANGED|") Then 
    AddHandler txt.TextChanged, AddressOf txt_TextChanged
  End Sub

  Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("GotFocus", sender, , , , , ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
  Private Sub txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("KeyDown", sender, , , , ScriptWindowManager.getKeyString(e), ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
  Private Sub txt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("LostFocus", sender, , , , , ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
  Private Sub txt_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("MouseUp", sender, e.Button.ToString, e.X, e.Y, , ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
  Private Sub txt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    On Error Resume Next
    'Dim ref = scriptClass(ClassName)
    Dim eventArgs As New ScriptEventArgs("TextChanged", sender, , , , , ClassName)
    'ref.onTextboxEvent(eventArgs)
    ScriptWindowManager.OnScriptWindowEvent(WinID, "Textbox", eventArgs)
  End Sub
#End Region




  Public Sub FinishMenu() Implements IScriptedPanel.FinishMenu
    Throw New NotImplementedException("Only implemented in ScriptedToolstrip")
  End Sub


  Public ReadOnly Property Form() As System.Windows.Forms.Form Implements IScriptedPanel.Form
    Get
      Return Me.FindForm()
    End Get
  End Property

  Public ReadOnly Property Window() As IScriptedWindow Implements IScriptedPanel.Window
    Get
      Return Me.FindForm()
    End Get
  End Property

End Class

Public Class RtfBox
  Inherits RichTextBox


End Class
Public Class WebBrws
  Inherits WebBrowser


End Class


Public Class IpSocket
  Inherits Net.Sockets.Socket

  Private p_IPAddress As Net.IPAddress
  Private p_Port As Integer
  Private myEndPoint As Net.EndPoint
  Private cancelListening As Boolean = False

  Event SocketAccepted(ByVal sock As Net.Sockets.Socket)
  Event DataReceived(ByVal data As String)

  ReadOnly Property IPAddress() As Net.IPAddress
    Get
      Return p_IPAddress
    End Get
  End Property
  ReadOnly Property Port() As Integer
    Get
      Return p_Port
    End Get
  End Property

  Sub New(ByVal _ipAdress As String, ByVal _port As Integer)
    MyBase.New(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.IPv4)
    p_IPAddress = Net.IPAddress.Parse(_ipAdress)
    p_Port = _port
    myEndPoint = New Net.IPEndPoint(IPAddress, Port)
    Dim th As New Threading.Thread(AddressOf listenerThread)
    th.Start()

  End Sub

  Sub quit()
    cancelListening = True
  End Sub

  Sub listenerThread()
    Me.Bind(myEndPoint)
    Me.Listen(10)

    Dim buf(1024) As Byte, receiveLen As Integer
    Do
      Dim acceptSock = Me.Accept()
      receiveLen = acceptSock.Receive(buf, 1024, Net.Sockets.SocketFlags.Partial)

    Loop While cancelListening = False

  End Sub

End Class


Public Class LstBox
  Inherits ListBox


  Sub itemAdd(ByVal it)
    Me.Items.Add(it)
  End Sub

  Sub itemClear()
    Me.Items.Clear()
  End Sub

  ReadOnly Property itemCount()
    Get
      Return Me.Items.Count
    End Get
  End Property

  Property ItemText(ByVal idx) As String
    Get
      Return Me.Items(idx)
    End Get
    Set(ByVal value As String)
      Me.Items(idx) = value
    End Set
  End Property

  Sub itemRemove(ByVal idx)
    Me.Items.RemoveAt(idx)
  End Sub

  'Delegate Sub xxdel(ByVal obj1 As Object, ByVal obj2 As Object)

  'Sub setChangeEventHandler()
  '  Dim ref = scriptClass("tb_testWindow")
  '  ' Stop
  '  Dim typ = ref.GetType()
  '  For Each mem In typ.GetMembers()
  '    TT.Write("member: ", mem.Name)
  '  Next
  '  'Dim th = Type.GetTypeHandle(ref)
  '  'Dim typ2 = Type.GetTypeFromHandle(th)
  '  'Dim obj = Activator.CreateInstance(typ2)


  '  'Dim x() As System.Reflection.MemberInfo
  '  'x = ref.GetType.GetMember("event111", Reflection.BindingFlags.InvokeMethod)
  '  ' Dim disp = Marshal.GetIDispatchForObject(ref)
  '  Dim unk = Marshal.GetIUnknownForObject(ref)
  '  'Dim disp2 As IntPtr
  '  'Marshal.QueryInterface(unk, New Guid("{00020400-0000-0000-C000-000000000046}"), disp2)
  '  'Dim dispwrap As New DispatchWrapper(ref)
  '  'Dim count = dispwrap.WrappedObject.GetTypeInfoCount()

  '  Dim comobj As New SolidEdgeSpy.InteropServices.ComObject(ref)
  '  Dim memIDs(1) As Integer
  '  comobj.TypeInfo.GetIDsOfNames(New String() {"event111"}, 1, memIDs)

  '  Dim d As EventHandler
  '  d = AddressOf ref.event111

  '  'Dim refPtr As IntPtr
  '  'Dim handle = Marshal.GetFunctionPointerForDelegate(d)
  '  '' Marshal.StructureToPtr(ref, refPtr, True)
  '  'Dim pVTable As IntPtr = Marshal.ReadIntPtr(unk, 0)
  '  'Dim pFunc, pFunc2 As IntPtr
  '  'pFunc = Marshal.ReadIntPtr(pVTable, 0)
  '  'pFunc2 = Marshal.ReadIntPtr(pVTable, 9 * 4)

  '  MsgBox(Join(comobj.GetAllMembers, vbNewLine) + vbNewLine + _
  '         "Unk:" + unk.ToString + "  FuncHandle: " + Handle.ToString)


  '  '   Dim idisp = Marshal.

  '  ' Marshal.GetUnmanagedThunkForManagedMethodPtr()
  '  ' Marshal.GetDelegateForFunctionPointer()
  '  'd = [Delegate].CreateDelegate(GetType(xxdel), ref, "event111", True)
  '  AddHandler Me.SelectedIndexChanged, d
  'End Sub

End Class


<Guid("00020400-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
Public Interface IDispatch
  Function GetTypeInfoCount() As Integer
  Function GetTypeInfo(<MarshalAs(UnmanagedType.U4)> ByVal iTInfo As Integer, <MarshalAs(UnmanagedType.U4)> ByVal lcid As Integer) As System.Runtime.InteropServices.ComTypes.ITypeInfo
  <PreserveSig()> _
  Function GetIDsOfNames(ByRef riid As Guid, <MarshalAs(UnmanagedType.LPArray, ArraySubType:=UnmanagedType.LPWStr)> ByVal rgsNames As String(), ByVal cNames As Integer, ByVal lcid As Integer, <MarshalAs(UnmanagedType.LPArray)> ByVal rgDispId As Integer()) As Integer
  <PreserveSig()> _
  Function Invoke(ByVal dispIdMember As Integer, ByRef riid As Guid, <MarshalAs(UnmanagedType.U4)> ByVal lcid As Integer, <MarshalAs(UnmanagedType.U4)> ByVal dwFlags As Integer, ByRef pDispParams As System.Runtime.InteropServices.ComTypes.DISPPARAMS, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pVarResult As Object(), _
   ByRef pExcepInfo As System.Runtime.InteropServices.ComTypes.EXCEPINFO, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pArgErr As IntPtr()) As Integer
End Interface


Public Class TreeVw
  Inherits TreeView


  Sub nodeAdd(ByVal rootKey, ByVal key, ByVal text)
    Dim root 'As TreeNode
    Dim res = Me.Nodes.Find(rootKey, True)
    If res.Length = 0 Then root = Me Else root = res(0)
    'If String.IsNullOrEmpty(rootKey) Then root = Me Else root = Me.Nodes.Find(rootKey, True)(0)
    root.Nodes.Add(key, text)
  End Sub

  Sub nodeClear()
    Me.Nodes.Clear()
  End Sub

  Sub selectNode(ByVal key)

  End Sub

End Class

