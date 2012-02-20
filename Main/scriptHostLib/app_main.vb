Module app_main
  Public Declare Function ReleaseCapture Lib "user32" () As Integer

  Public Const apo = Chr(34)

  Public helpFilePath As String
  'Public glob As New cls_globPara(settingsFolder + "scriptIDE.para.txt")
  Public Const APP_ID = "scriptide"
  Public globGridHistory As New List(Of Integer)
  Public globGridHistoryCurIndex As String = ""
  Public globSkipNextHistoryAdd As Boolean = False
  Public MAIN As frmTB_debug
  Public tbCompileErrors As frmTB_compileErrors
  Public tbPrintline As frmTB_tracePrintLine

  Public ZZ As New cls_scriptHelper()
  Public WithEvents IdeHelper As IIDEHelper

  Public scriptHelperMethods As New Dictionary(Of String, String())

  'Unterscheidung, als was der ScriptHost läuft
  Public isIDEMode As Boolean
  Public RunningAsCompiledClass As String, CompiledClassRef As Object
  Public CompiledClassTypes As Dictionary(Of String, Type)
  'Public CompiledClassDebugging As Boolean

  Public Const tbPrintline_ID = "Addin|##|siaCodeCompiler|##|SHPrintline"

  Public skipMdiActivationEvent As Boolean = False


  

  Function isIDE() As Boolean
    isIDE = False
#If DEBUG Then
    isIDE = True
#End If
  End Function

  Function getKeyString(ByVal e As Object) As String
    getKeyString = If(e.Control, "CTRL-", "") + If(e.Alt, "ALT-", "") + If(e.Shift, "SHIFT-", "") + _
                   e.KeyCode.ToString.ToUpper
  End Function



  Sub startHelpBrowser(Optional ByVal helpPara As String = "")
    ''If isIDE() Then Exit Sub

    Dim helpBrowserFilespec As String = ExePath("helpbrowser")

    If helpBrowserFilespec = "" Then
      MsgBox("Der Help-Browser hat noch kein mwRegistry drinnen !!! ")
      ''MsgBox("Der Help-Browser wurde nicht gefunden bzw")
      ''Exit Sub
      helpBrowserFilespec = "C:\yEXE\helpBrowser.exe"
    End If

    ''Dim updaterPara(5) As String
    ''updaterPara(0) = APP_ID
    ''updaterPara(1) = glob.appPath 'localFolder
    ''updaterPara(2) = "/autostart" 'autostart?
    ''updaterPara(3) = "/autoclose" 'autoclose?
    ''updaterPara(4) = Application.ExecutablePath 'startAfterUpdate(1)
    ''updaterPara(5) = "" 'startAfterUpdate(2)
    ''Dim updaterArguments As String = Join(updaterPara, "#²#")

    ' ''MsgBox(updaterFilespec + " " + updaterArguments, , "Updater wird aufgerufen...")

    ''glob.saveParaFile()
    Process.Start(helpBrowserFilespec, helpPara)

  End Sub

  Private Sub IdeHelper_BreakPointSet(ByVal DocURL As String, ByVal lineNumber As Integer, ByVal state As Boolean) Handles IdeHelper.BreakPointSet
    Dim fn = IO.Path.GetFileNameWithoutExtension(DocURL)
    If Not ScriptHost.Instance.isScriptClassLoaded(fn) Then Exit Sub
    Dim ref = ScriptHost.Instance.scriptClass(fn)
    ref.zz_BBsetLine(lineNumber, state)
    ref.zz_BBtrace()
  End Sub


End Module
