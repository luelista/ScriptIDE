Module app_main
  Public Declare Function ReleaseCapture Lib "user32" () As Integer

  Public Const apo = Chr(34)

  Public zz As New cls_scriptHelper()


  'Unterscheidung, als was der ScriptHost läuft
  
  Function getKeyString(ByVal e As Object) As String
    getKeyString = If(e.Control, "CTRL-", "") + If(e.Alt, "ALT-", "") + If(e.Shift, "SHIFT-", "") + _
                   e.KeyCode.ToString.ToUpper
  End Function



  'Private Sub IdeHelper_BreakPointSet(ByVal DocURL As String, ByVal lineNumber As Integer, ByVal state As Boolean) Handles IdeHelper.BreakPointSet
  '  Dim fn = IO.Path.GetFileNameWithoutExtension(DocURL)
  '  If Not isScriptClassLoaded(fn) Then Exit Sub
  '  Dim ref = scriptClass(fn)
  '  ref.zz_BBsetLine(lineNumber, state)
  '  ref.zz_BBtrace()
  'End Sub

  Public Function FP(ByVal path As String, Optional ByVal fileName As String = "")
    FP = path + IIf(path.EndsWith("\"), "", "\") + If(fileName.StartsWith("\"), fileName.Substring(1), fileName)
  End Function

End Module
