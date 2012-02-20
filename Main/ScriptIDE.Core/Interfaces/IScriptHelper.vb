Imports System.Runtime.InteropServices
Public Enum RuntimeMode
  IDE
  Debug
  Release
  HostedDebug
  DLL
End Enum
Public Interface IScriptHelper

  ReadOnly Property ScriptClassName() As String
  ReadOnly Property Filespec() As String
  ReadOnly Property RuntimeMode() As RuntimeMode

  Function FP(ByVal path As String, Optional ByVal fileName As String = "")
  Function FPUNIX(ByVal path As String, Optional ByVal fileName As String = "")
  Function FilePathCombine(ByVal f1, ByVal f2, Optional ByVal isUnix = False)

  Sub trace(ByVal zLN, ByVal zNN, ByVal para1, Optional ByVal para2 = "", Optional ByVal typ = "info")
  Sub traceClear()

  Sub printLine(ByVal zLN, ByVal zNN, ByVal index, ByVal title, ByVal data)
  Sub printLineReset()

  Sub CB(ByRef zLN, ByVal zNN, <Out()> ByRef zC, ByRef zC2, ByRef zErr, <Out()> ByRef ziI, <Out()> ByRef ziE, <Out()> ByRef ziQ, Optional ByVal onStopCode = False)
  Sub CB2(ByRef zLN As Integer, ByVal zNN As String, <Out()> ByRef zC As Integer, ByRef zC2 As Integer, ByVal errorNum As Integer, ByVal errorDesc As String, <Out()> ByRef ziI As Integer, <Out()> ByRef ziE As String, <Out()> ByRef ziQ As Boolean, Optional ByVal onStopCode As Boolean = False)

  Sub Idle(Optional ByVal milliseconds As Integer = 10)
  Sub DoEvents()

  ReadOnly Property IDEHelper() As IIDEHelper
  ReadOnly Property FSO() As Object
  ReadOnly Property Shell() As Object
  ReadOnly Property InterProc() As Object

  Sub setOutMonitor(ByVal txt)

  Sub shellExecute(ByVal filename, Optional ByVal args = "", Optional ByVal wait = -1)
  Sub shellConsole(ByVal commandLine, Optional ByVal workDir = "")

  <Obsolete("Use getActiveTab instead")> Function getActiveRTF()
  Function getActiveTab()
  Function getActiveTabType()
  Function getActiveTabFilespec()

  Function OpenFileFinder(ByVal RootFolder, Optional ByVal Filter = "*.*")
  Function fileGetContents(ByVal filespec)
  Function filePutContents(ByVal filespec, ByVal content, Optional ByVal append = False)
  Sub splitFilespecData(ByVal filespec, ByRef path, ByRef name, ByRef ext)
  Function fileExists(ByVal FileSpec)
  Sub killFile(ByVal FileSpec)

  Sub setClipboardText(ByVal txt)
  Function getClipboardText()

  Function getCompName()
  Function getUserName()

  Sub playSoundWAV(ByVal fileSpec, Optional ByVal mode = 1)

  Sub TimerStart()
  Function TimerGetElapsed()

  Sub sleep(ByVal ms)

  Function isScriptClassLoaded(ByVal classID As String) As Boolean
  Function TryGetScriptClass(ByVal classID As String, ByRef ClassRef As Object) As Boolean
  Function scriptClass(ByVal classID As String) As Object
  Sub invalidateScriptClass(ByVal classID As String)
  Function newScriptClass(ByVal classID As String, ByVal ParamArray newParams() As String) As Object
  Function CreateObject(ByVal objName)
  Function getExeObject(ByVal exeName) As Object

  Function GetExePath(ByVal exeName As String) As String

  <Obsolete("Use IDEHelper.CreateDockWindow instead")> Function getDialogFormRef(ByVal id) As Object
  <Obsolete("Use IDEHelper.GetInternalDockWindow instead")> Function getDockPanelRef(ByVal strPanelType, Optional ByVal strPanelName = "") As Object

  Function CreateToolbar(ByVal ToolbarID As String) As IScriptedPanel
  Function CreateWindow(ByVal ScriptedWindowID As String, ByVal Flags As DWndFlags, Optional ByVal DefaultPosition As Integer = 5) As IScriptedPanel


  Sub SendKeys(ByVal key, Optional ByVal wait = True)

  Sub stackPush(ByVal entryName)
  Sub stackPop()

  Function getCallStack()

  Sub releaseMyRef()
  Sub releaseScriptClass(ByVal className As String)

  Function http_get(ByVal strURL, Optional ByVal cookies = "")
  Function http_post(ByVal strURL, ByVal strPostData, Optional ByVal cookies = "")

  Function TwGetFileList(ByVal strAppName, ByVal strFileName)
  Function TwReadFile(ByVal strAppName, ByVal strFileName)
  Function TwSaveFile(ByVal strAppName, ByVal strFileName, ByVal strContent)

  Function UrlEncode(ByVal str)
  Function UrlDecode(ByVal str)

  Function getAsyncKeyState(ByVal nVirtKey As Windows.Forms.Keys) As Boolean

  Function GetArgbColor(ByVal htmlColor) As Object

  Sub setBgColor(ByVal el, ByVal htmlColor)
  Sub setFgColor(ByVal el, ByVal htmlColor)

  Function MD5(ByVal strToHash As String) As String
  Function GetImageCached(ByVal strURL As String) As Drawing.Image

  Function newGlobPara() As cls_globPara

  Function HexDump(ByVal bytes As Byte()) As String

  Function JoinIGridLine(ByVal objline As Object, Optional ByVal Delimiter As String = vbTab) As String
  Sub SplitToIGridLine(ByVal objline As Object, ByVal input As String, Optional ByVal Delimiter As String = vbTab)
  Sub Igrid_get(ByVal objGrid As Object, ByRef FileContent As String, Optional ByVal LineDelim As String = vbNewLine, Optional ByVal ColDelim As String = vbTab)
  Sub Igrid_put(ByVal objGrid As Object, ByVal FileContent As String, Optional ByVal LineDelim As String = vbNewLine, Optional ByVal ColDelim As String = vbTab)

End Interface
