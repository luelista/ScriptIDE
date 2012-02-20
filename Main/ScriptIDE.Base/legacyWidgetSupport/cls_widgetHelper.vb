Public Class cls_widgetHelper

  Private _para As String
  Private expBar As Object
  Private ax As Object
  Private axName As String

  Public Class colorInfoID
    Public Const MainBG = "mainbg"
    Public Const MainMenuBG = "mainmenubg"
    Public Const OpenedWidgetsBG1 = "openedwidgetsbg1"
    Public Const OpenedWidgetsBG2 = "openedwidgetsbg2"
    Public Const ClosedWidgetsBG1 = "closedwidgetsbg1"
    Public Const ClosedWidgetsBG2 = "closedwidgetsbg2"
    Public Const TopmostSwitchColor = "topmostswitchcolor"
    Public Const WidgetBG = "widgetbg"
    Public Const WidgetText = "widgettext"
  End Class

  Friend Sub New(ByVal parameter As String, ByVal className As String, ByVal expBarControl As Object, ByVal axObject As Object)
    _para = parameter
    expBar = expBarControl
    ax = axObject
    axName = className
  End Sub
  Sub onInfoMessageLinkClicked(ByVal linkText As String, ByVal msgText As String, ByVal tag As String)

  End Sub
  Public ReadOnly Property ColorInfo(ByVal colorID As Object) As Color
    Get
      On Error Resume Next
      If TypeOf colorID Is String Then colorID = UCase(colorID)
      Select Case colorID
        Case colorInfoID.MainBG : Return ColorTranslator.FromHtml(ParaService.Glob.para("frm_widgetMan__txtMainBG"))
        Case colorInfoID.MainMenuBG : Return ColorTranslator.FromHtml(ParaService.Glob.para("frm_widgetMan__txtMainMenuBG"))
        Case colorInfoID.OpenedWidgetsBG1 : Return ColorTranslator.FromHtml(ParaService.Glob.para("frm_widgetMan__txtOpenedWidgets_BG1"))
        Case colorInfoID.OpenedWidgetsBG2 : Return ColorTranslator.FromHtml(ParaService.Glob.para("frm_widgetMan__txtOpenedWidgets_BG2"))
        Case colorInfoID.ClosedWidgetsBG1 : Return ColorTranslator.FromHtml(ParaService.Glob.para("frm_widgetMan__txtClosedWidgets_BG1"))
        Case colorInfoID.ClosedWidgetsBG2 : Return ColorTranslator.FromHtml(ParaService.Glob.para("frm_widgetMan__txtClosedWidgets_BG2"))
        Case colorInfoID.TopmostSwitchColor : Return ColorTranslator.FromHtml(ParaService.Glob.para("frm_widgetMan__txtTopmostSwitchColor"))
        Case colorInfoID.WidgetBG : Return ColorTranslator.FromHtml(ParaService.Glob.para("frm_widgetMan__txtWidgetBG"))
        Case colorInfoID.WidgetText : Return ColorTranslator.FromHtml(ParaService.Glob.para("frm_widgetMan__txtWidgetFG"))
      End Select
    End Get
  End Property

  Friend Sub infoMessageCallback(ByVal linkText As String, ByVal msgText As String, ByVal tag As String)
    ax.onInfoMessageLinkClicked(linkText, msgText, tag)
  End Sub
  Public Sub infoMessage(ByVal scolor As String, ByVal msg As String, Optional ByVal tag As Object = "")
    'showInfoMessage(scolor, msg, tag, New callback_infoMessageLink(AddressOf Me.infoMessageCallback))
    MsgBox(scolor + vbNewLine + vbNewLine + msg + vbNewLine + vbNewLine + tag.ToString)
  End Sub

  Public Property Title() As String
    Get
      Return expBar.TitleText
    End Get
    Set(ByVal value As String)
      expBar.TitleText = value
    End Set
  End Property

  Public ReadOnly Property ID() As String
    Get
      Return axName
    End Get
  End Property

  Public Property GlobPara(ByVal section As String, ByVal paraName As String) As String
    Get
      Return ParaService.Glob.para("widgetGlob__" + section + "__" + paraName)
    End Get
    Set(ByVal value As String)
      ParaService.Glob.para("widgetGlob__" + section + "__" + paraName) = value
    End Set
  End Property
  Public Property WidgetPara(ByVal paraName As String) As String
    Get
      Return ParaService.Glob.para(axName + "__" + paraName)
    End Get
    Set(ByVal value As String)
      ParaService.Glob.para(axName + "__" + paraName) = value
    End Set
  End Property

  Public ReadOnly Property Para() As String
    Get
      Return _para
    End Get
  End Property

  ReadOnly Property DIZ() As String
    Get
      Return "noDiz"
    End Get
  End Property

  Public Sub Run(ByVal fileSpec As String, Optional ByVal arguments As String = "")
    Process.Start(fileSpec, arguments)
  End Sub

  Public Sub webSaveFile(ByVal appName As String, ByVal fileName As String, ByVal content As String)
    TwAjax.SaveFile(appName, fileName, content)
  End Sub
  Public Function webReadFile(ByVal appName As String, ByVal fileName As String) As String
    Return TwAjax.ReadFile(appName, fileName)
  End Function

  Public Sub navigateQView(ByVal fileSpec As String)
    'oIntWin.SendCommand("qview", "NAVIGATE", fileSpec)
  End Sub
  Public Sub navigateDefaultBrowserExtern(ByVal url As String)
    'oIntWin.SendCommand("qview", "NAVIGATE", url)
  End Sub

  Public Function GetInterprocRef(ByVal winTitle As String) As sys_interproc
    Return New sys_interproc(winTitle)
  End Function



End Class
