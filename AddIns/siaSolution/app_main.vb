Module app_main
  Public helper As IScriptHelper
  Public WithEvents IDE As IIDEHelper

  Public tbSolution As New frmTB_solutionExplorer

  Public Const tbSolution_ID = "Addin|##|siaSolution|##|SolutionExplorer"

  Private Sub IDE_DocumentTabActivated(ByVal Tab As Object, ByVal Key As String) Handles IDE.DocumentTabActivated
    If tbSolution.Visible Then
      tbSolution.onFileChanged(Key)
    End If
  End Sub

  Function getDefProjectFolder() As String
    Return IDE.Glob.para("siaSolution__defProjectFolder", IDE.GetSettingsFolder() + "projects\")
  End Function

End Module
