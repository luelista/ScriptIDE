Imports System
Imports EnvDTE80
Imports EnvDTE


Module app_main

  Public mainWindowHandle As IntPtr = IntPtr.Zero
  Public ActCodePosition As CodePosition

  Public Const settingsFolder = "C:\yPara\traceMonitor\"
  Public glob As New cls_globPara(settingsFolder + "para.txt")
  Public globAktDiz As String

  Public globAktProjectFile As String

  Public MAIN As frm_TraceMonitor

  Property configPara(ByVal para As String) As String
    Get
      configPara = glob.para("frm_config__" + para)
    End Get
    Set(ByVal value As String)
      'achtung, problem wenn konfigdialog offen ist
      glob.para("frm_config__" + para) = value
    End Set
  End Property


  Structure CodePosition
    Public Line As Integer
    Public Column As Integer
    Public moduleName As String
    Public procedureName As String
    Public codeElement As EnvDTE.CodeElement
    Public textSelection As EnvDTE.TextSelection
  End Structure

  Sub initFromSettings()

    
    If glob.para("frm_settings__chkEnableGrowl", "FALSE") = "TRUE" Then
      initGrowl()
    Else
      stopGrowl()
    End If

    If glob.para("frm_settings__chkTcpListen", "TRUE") = "TRUE" Then
      initTraceServer()
    End If

    If glob.para("frm_settings__chkRegisterMe", "FALSE") = "TRUE" Then
      registerAndForwardTo(glob.para("frm_settings__txtRegisterIP"), glob.para("frm_settings__txtRegisterPort"))
    End If

    If glob.para("frm_settings__chkSubscribeMe", "FALSE") = "TRUE" Then
      subscribeTo(glob.para("frm_settings__txtSubscribeIP"), glob.para("frm_settings__txtSubscribePort"))
    End If

  End Sub

  Sub startupComplete()
    On Error Resume Next
    'find main window handle
    Dim windowTitle As String = IDE.MainWindow.Caption
    Const className As String = "wndclass_desked_gsk"
    mainWindowHandle = FindWindowByCaption(windowTitle)
  End Sub

  Sub traceTester()
    trace("HINWEIS: test aus app_main", "bla bla bla bla .....")
  End Sub

End Module
