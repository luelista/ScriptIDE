Public Interface IIDEHelper

  Event DocumentTabActivated(ByVal Tab As Object, ByVal Key As String)
  Event DocumentOpened(ByVal Key As String)
  Event DocumentBeforeClose(ByVal Key As String, ByRef Cancel As Boolean)
  Event DocumentClosed(ByVal Key As String)
  Event DocumentBeforeSave(ByVal Key As String, ByRef Cancel As Boolean)
  Event DocumentAfterSave(ByVal Key As String)
  Event BreakPointSet(ByVal DocURL As String, ByVal lineNumber As Integer, ByVal state As Boolean)
  Event OnDialogEvent(ByVal WinID As String, ByVal EventName As String, ByVal EventArgs As ScriptEventArgs)
  Event OnIniDone()

  ''' <param name="EventType">1=Start 2=Stop</param>
  Event ConsoleEvent(ByVal EventType As Integer, ByVal EventParam As Object)
  Function Console_GetProcess() As Process
  Function Console_ProcRunning() As Boolean
  Sub Console_Kill()

  ReadOnly Property IsStartup() As Boolean
  ReadOnly Property DIZ() As String

  Function GetToolbarList() As String()
  Function GetToolbar(ByVal ToolbarID As String, Optional ByRef containerName As String = Nothing) As IScriptedPanel
  Function CreateToolbar(ByVal ToolbarID As String, Optional ByVal Style As ToolBarStyle = ToolBarStyle.ToolStrip) As IScriptedPanel
  Function CreateToolbar(ByVal ToolbarID As String, ByVal container As String, ByVal left As Integer, ByVal top As Integer) As IScriptedPanel
  Sub RemoveToolbar(ByVal ToolbarID As String)
  Sub RefreshToolbarSize()

  Function getInternalDockWindow(ByVal persistString As String) As Windows.Forms.Form
  Function CreateDockWindow(ByVal ScriptedWindowID As String, Optional ByVal DefaultPosition As Integer = 5) As IScriptedPanel
  ' WeifenLuo.WinFormsUI.Docking.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockRightAutoHide

  Function CreateForm(ByVal WindowID As String) As IScriptedWindow

  Sub RegisterAddinWindow(ByVal WindowID As String) 'WeifenLuo.WinFormsUI.Docking.IDockContent
  Sub UnregisterAddinWindow(ByVal WindowID As String) 'WeifenLuo.WinFormsUI.Docking.IDockContent
  Sub BeforeShowAddinWindow(ByVal WindowID As String, ByVal Ref As Object) 'WeifenLuo.WinFormsUI.Docking.IDockContent

  Sub RegisterFileactionHandler(ByVal ClassRef As Object)

  ReadOnly Property MainWindow() As Object
  Function getMainFormRef() As Object
  'Function getMainToolbar() As Object

  'Sub showHideMainToolbar(ByVal visible As Boolean, Optional ByVal height As Integer = -1)

  'Property globBreakMode() As String

  Sub ShowOptionsDialog(Optional ByVal page As String = Nothing)

  Function Exec(ByVal Command As String, ByVal userPara As Object, ByVal userPara2 As Object) As Object
  Function Navigate(ByVal Target As String, ByVal Command As String, ByVal userPara As String) As String

  Function NavigateFile(ByVal filePara As String) As Form
  Function IsFileOpened(ByVal filePara As String) As Boolean

  Function GetTabByURL(ByVal filePara As String) As Form
  Function GetTabByPersistString(ByVal persistString As String) As Form

  Overloads Sub executeProgramInConsole(ByVal commandLine As String, Optional ByVal workDir As String = "")
  Overloads Sub executeProgramInConsole(ByVal commandLine As String, ByRef procInfo As Process, ByVal workDir As String)

  Function getActiveTab() As Form
  Function getActiveTabType() As String
  Function getActiveTabFilespec() As String
  Function GetTabList() As String()

  ReadOnly Property Glob() As Object 'cls_globPara

  Function GetSettingsFolder() As String
  Function GetProfileFolder() As String

  ReadOnly Property ContentHelper() As IIDEContentHelper
  'ReadOnly Property ProtocolManager() As IProtocolManager
  ReadOnly Property IndexList() As IIDEIndexList
  ReadOnly Property Skin() As Object
  ReadOnly Property Addins() As IIDEAddins

End Interface
Public Interface IIDEContentHelper
  Event CurrentDocumentLineChanged(ByVal Tab As Object, ByVal lineNr As Integer)
  Sub OnCurrentDocumentLineChanged(ByVal Tab As Object, ByVal lineNr As Integer)

  Function GetIndexList(ByVal fileExtension As String) As IIndexList
  Sub ShowIndexListControl(ByVal ctrl As IIndexList)
  Sub SimpleCreateIndexList(ByVal windowRef As IDockContentForm, ByRef indexListRefCache As IIndexList)

  Sub _internalCloseTab(ByVal frm As IDockContentForm, ByRef cancel As Boolean)

  Sub merkeZeile(ByVal merkeData As String)
  Function merkeZeileAbruf() As String()

  Property StatusText() As String
  Sub CreateFileactionToolbar(ByVal tabURL As String, ByVal ext As String, ByVal tb As ToolStrip)

  Sub _internalRenameDocument(ByVal oldKey As String, ByVal newKey As String)

End Interface
Public Interface IIDEIndexList

  Sub Reset()
  Sub AddItem(ByVal Text As String, ByVal Position As String, ByVal Details As String)
  Sub RestorePos(ByRef selIndex As Integer, ByRef topIndex As Integer)
  Sub OnLinenumberChanged(ByVal lineNr As Integer)

End Interface

Public Interface IIDEAddins
  Inherits IEnumerable(Of KeyValuePair(Of String, IAddinConnect))
  Function ContainsKey(ByVal addinName As String) As Boolean
  Function IsLoaded(ByVal addinName As String) As Boolean
  Default ReadOnly Property Item(ByVal addinName As String) As IAddinConnect
  Sub InstallAddin(ByVal fileSpec As String)
  Function GetAddinInfo(ByVal addinName As String) As Object
End Interface

Public Interface IProtocolManager
  Function ReadFile(ByVal InternalURL As String) As String
  Sub SaveFile(ByVal InternalURL As String, ByVal content As String)
  Sub NavigateFilelistToURL(ByVal InternalURL As String)
  Sub ShowFilelistForProtocol(ByVal Protocol As String)
  Function GetURLProtocolHandler(ByVal InternalURL As String) As IProtocolHandler
  Function GetProtocolHandler(ByVal Protocol As String) As IProtocolHandler
  Function MapToLocalFilename(ByVal internalURL As String) As String
End Interface