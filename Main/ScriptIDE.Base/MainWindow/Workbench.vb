Public Class Workbench

  Private Shared maininstance As frm_main
  'Private Shared splash As frm_splash
  Private Shared options As frm_windowManager

  Public Shared Event SplashScreenEvent(ByVal mode As Integer, ByVal newText As String)

  Public Shared ReadOnly Property Instance() As frm_main
    Get
      Return maininstance
    End Get
  End Property

  'Public Shared ReadOnly Property SplashScreen() As frm_splash
  '  Get
  '    Return splash
  '  End Get
  'End Property

  Public Shared ReadOnly Property OptionsDialog() As frm_windowManager
    Get
      If options Is Nothing OrElse options.IsDisposed Then options = New frm_windowManager
      Return options
    End Get
  End Property

  Public Shared Sub Initialize()
    'splash = New frm_splash
    'splash.Show()
    Interproc.Initialize()

    'TT.Write("VisStyle State", Application.VisualStyleState.ToString)
    'Application.EnableVisualStyles()
    'TT.Write("VisStyle State", Application.VisualStyleState.ToString)

    maininstance = New frm_main

    '  RegisterIDE()

    AddHandler Application.ThreadException, AddressOf Application_ThreadException


    Application.Run(maininstance)

  End Sub

  Public Shared Sub RegisterIDE()
    Try
      Dim hlpRef = CreateObject("IdeHelper.Application")
      Dim res = hlpRef.addSIRef(ParaService.ProfileName, "scriptide_" + ParaService.ProfileName, maininstance.Handle.ToInt32, cls_IDEHelper.Instance)
      If res = -1 Then
        TT.Write("FEHLER: Es läuft bereits eine Instanz mit dem Profil " + ParaService.ProfileName + "! Bitte verwenden Sie die SIDE4.exe um die ScriptIDE aufzurufen.", , "err") : Exit Sub
      End If
    Catch : End Try
  End Sub

  Private Shared Sub Application_ThreadException(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
    MsgBox("An unhandled Exception occured in ScriptIDE - if you see this dialog more frequently, please contact the developer." + vbNewLine + e.Exception.ToString, MsgBoxStyle.Exclamation, "Unhandled Exception")
    TT.Write("Unhandled Exception", e.Exception.ToString, "err")

  End Sub

  Public Shared Sub SetSplashText(ByVal newText As String, ByVal increment As Boolean)

    RaiseEvent SplashScreenEvent(If(increment, 1, 2), newText)

  End Sub

  Public Shared Sub HideSplashScreen()
    RaiseEvent SplashScreenEvent(3, Nothing)
  End Sub

  Public Shared Sub ShowUnloadScreen()
    RaiseEvent SplashScreenEvent(4, Nothing)
  End Sub

  Public Shared Sub ShowOptionsDialog(Optional ByVal page As String = Nothing)
    OptionsDialog.Show()
    OptionsDialog.Activate()
    If page <> Nothing Then _
      OptionsDialog.navigateOptions(page)
  End Sub


End Class
