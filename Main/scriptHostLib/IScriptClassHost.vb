Public Interface IScriptClassHost

  ReadOnly Property scriptClassName() As String
  Property className() As String
  ReadOnly Property assemblyRef() As Reflection.Assembly
  ReadOnly Property assemblyFilespec() As String
  Property debugMode() As RuntimeMode
  Function targetFolder() As String
  ReadOnly Property scriptPara() As ScriptParameters
  ReadOnly Property fileSpec() As String
  ReadOnly Property lastModified() As Date
  Function getClassRef() As Object
  Function getNewClassRef(ByVal parentRef As WeakReference) As Object
  Function getScriptHelper() As Object
  Function isIniDone() As Boolean
  Function checkForNewerFile() As Boolean

  Sub invalidate()

  Sub initScriptHost()
  Sub terminateScript()

End Interface

'Public Enum DebugMode
'  Internal
'  External
'End Enum