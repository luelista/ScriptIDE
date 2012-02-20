'Test: Scripte kompilieren
<Microsoft.VisualBasic.ComClass()> Public Class cls_IDEHelperMini
  Implements IIDEHelper

  Public Event BreakPointSet(ByVal DocURL As String, ByVal lineNumber As Integer, ByVal state As Boolean) Implements IIDEHelper.BreakPointSet

  Public Event DocumentAfterSave(ByVal Key As String) Implements IIDEHelper.DocumentAfterSave

  Public Event DocumentBeforeClose(ByVal Key As String, ByRef Cancel As Boolean) Implements IIDEHelper.DocumentBeforeClose

  Public Event DocumentBeforeSave(ByVal Key As String, ByRef Cancel As Boolean) Implements IIDEHelper.DocumentBeforeSave

  Public Event DocumentClosed(ByVal Key As String) Implements IIDEHelper.DocumentClosed

  Public Event DocumentOpened(ByVal Key As String) Implements IIDEHelper.DocumentOpened

  Public Event DocumentTabActivated(ByVal Tab As Object, ByVal Key As String) Implements IIDEHelper.DocumentTabActivated

  Public Event OnDialogEvent(ByVal WinID As String, ByVal EventName As String, ByVal EventArgs As ScriptEventArgs) Implements IIDEHelper.OnDialogEvent

  'Public tbScriptWin As New Dictionary(Of String, frmTB_scriptWin)
  Private Shared p_instance As New cls_IDEHelperMini

  Private Sub New()
  End Sub

  Public Shared Function GetSingleton() As cls_IDEHelperMini
    Return p_instance
  End Function

  <Obsolete("Die Funktion CreateDockWindow ist obsolet. Bitte verwende die Funktion ZZ.CreateWindow aus dem scriptHelper")> _
  Function CreateDockWindow(ByVal scriptedWindowID As String, Optional ByVal defaultPosition As Integer = 5) As IScriptedPanel Implements IIDEHelper.CreateDockWindow
    MsgBox("Die Funktion CreateDockWindow ist obsolet. Bitte verwende die Funktion ZZ.CreateWindow aus dem scriptHelper" + vbNewLine + "scriptedWindowID: " + scriptedWindowID + vbNewLine + "ZZ.CreateWindow(String, DWndFlags, Integer)", , "cls_IDEHelper")
  End Function

  <Obsolete("Die Funktion CreateForm ist obsolet. Bitte verwende die Funktion ZZ.CreateWindow aus dem scriptHelper")> _
  Public Function CreateForm(ByVal scriptedWindowID As String) As IScriptedWindow Implements IIDEHelper.CreateForm
    MsgBox("Die Funktion CreateForm ist obsolet. Bitte verwende die Funktion ZZ.CreateWindow aus dem scriptHelper" + vbNewLine + "scriptedWindowID: " + scriptedWindowID + vbNewLine + "ZZ.CreateWindow(String, DWndFlags, Integer)", , "cls_IDEHelper")

  End Function

  Public Function GetInternalDockWindow(ByVal persistString As String) As System.Windows.Forms.Form Implements IIDEHelper.getInternalDockWindow
    If persistString.StartsWith("toolbar|##|tbscriptwin|##|", StringComparison.CurrentCultureIgnoreCase) Then
      Dim windowRef As IScriptedPanel
      If ScriptWindowManager.TryGetWindow(persistString.Substring(26), windowRef) Then
        Return windowRef.Form
      End If
    End If
    Return Nothing
  End Function

  Public Sub RemoveToolbar(ByVal toolbarID As String) Implements IIDEHelper.RemoveToolbar
  End Sub
  Public Sub RefreshToolbarSize() Implements IIDEHelper.RefreshToolbarSize
  End Sub

  Public Function getMainFormRef() As Object Implements IIDEHelper.getMainFormRef
  End Function

  'Public Function getMainToolbar() As Object Implements IIDEHelper.getMainToolbar
  '  Return MAIN.pnlToolbar
  'End Function

  Public ReadOnly Property MainWindow() As Object Implements IIDEHelper.MainWindow
    Get
    End Get
  End Property

  'Public Sub showHideMainToolbar(ByVal visible As Boolean, Optional ByVal height As Integer = -1) Implements IIDEHelper.showHideMainToolbar
  '  If visible Then
  '    If height = -1 Then height = MAIN.pnlToolbar.Height
  '    MAIN.showHideToolbar(height)
  '  Else
  '    MAIN.showHideToolbar(0)
  '  End If
  'End Sub

  Public Sub ShowOptionsDialog(Optional ByVal page As String = Nothing) Implements IIDEHelper.ShowOptionsDialog
  End Sub

  Public Function getActiveTab() As Form Implements IIDEHelper.getActiveTab
  End Function

  Public Function getActiveTabFilespec() As String Implements IIDEHelper.getActiveTabFilespec
    Return ""
  End Function

  Public Function getActiveTabType() As String Implements IIDEHelper.getActiveTabType
    Return ""
  End Function

  Sub initGlobObject(ByVal fileName As String)
    _globObj = New cls_globPara(fileName)
  End Sub

  Dim _globObj As cls_globPara
  Public ReadOnly Property Glob() As Object Implements IIDEHelper.Glob
    Get
      Return _globObj
    End Get
  End Property

  Public Function getSettingsFolder() As String Implements IIDEHelper.GetSettingsFolder
    Return ParaService.SettingsFolder
  End Function

  Public Sub RegisterAddinWindow(ByVal WindowID As String) Implements IIDEHelper.RegisterAddinWindow
  End Sub
  Public Sub UnregisterAddinWindow(ByVal WindowID As String) Implements IIDEHelper.UnregisterAddinWindow
  End Sub
  Public Sub BeforeShowAddinWindow(ByVal WindowID As String, ByVal Ref As Object) Implements IIDEHelper.BeforeShowAddinWindow
  End Sub

  Public ReadOnly Property ContentHelper() As IIDEContentHelper Implements IIDEHelper.ContentHelper
    Get
    End Get
  End Property
  Public ReadOnly Property IndexList() As IIDEIndexList Implements IIDEHelper.IndexList
    Get
    End Get
  End Property
  Public ReadOnly Property Skin() As Object Implements IIDEHelper.Skin
    Get
    End Get
  End Property
  Public ReadOnly Property ProtocolManager() As IProtocolManager ' Implements IIDEHelper.ProtocolManager
    Get
    End Get
  End Property
  Public ReadOnly Property Addins() As IIDEAddins Implements IIDEHelper.Addins
    Get
    End Get
  End Property

  Public Overloads Sub executeProgramInConsole(ByVal commandLine As String, Optional ByVal workDir As String = "") Implements IIDEHelper.executeProgramInConsole
  End Sub

  Public Overloads Sub executeProgramInConsole(ByVal commandLine As String, ByRef procInfo As System.Diagnostics.Process, ByVal workDir As String) Implements IIDEHelper.executeProgramInConsole
  End Sub


  Public Function IsFileOpened(ByVal filePara As String) As Boolean Implements IIDEHelper.IsFileOpened
    Return False
  End Function

  Public Sub RegisterFileactionHandler(ByVal ClassRef As Object) Implements IIDEHelper.RegisterFileactionHandler
  End Sub

  Public Function CreateToolbar(ByVal ToolbarID As String, Optional ByVal Style As ToolBarStyle = ToolBarStyle.ToolStrip) As IScriptedPanel Implements IIDEHelper.CreateToolbar
  End Function

  Public Function GetToolbarList() As String() Implements IIDEHelper.GetToolbarList
    Return New String() {}
  End Function

  Public ReadOnly Property IsStartup() As Boolean Implements IIDEHelper.IsStartup
    Get

    End Get
  End Property

  Public Event OnIniDone() Implements IIDEHelper.OnIniDone

  Public Function CreateToolbar(ByVal ToolbarID As String, ByVal container As String, ByVal left As Integer, ByVal top As Integer) As IScriptedPanel Implements IIDEHelper.CreateToolbar

  End Function

  Public Function GetToolbar(ByVal ToolbarID As String, Optional ByRef containerName As String = Nothing) As IScriptedPanel Implements IIDEHelper.GetToolbar

  End Function

  Public Function GetProfileFolder() As String Implements IIDEHelper.GetProfileFolder
    Return ParaService.ProfileFolder
  End Function

  Public Function Exec(ByVal Command As String, ByVal userPara As Object, ByVal userPara2 As Object) As Object Implements IIDEHelper.Exec

  End Function

  Public Function GetTabByURL(ByVal filePara As String) As System.Windows.Forms.Form Implements IIDEHelper.GetTabByURL

  End Function

  Public Function Navigate(ByVal Target As String, ByVal Command As String, ByVal userPara As String) As String Implements IIDEHelper.Navigate
    'TODO: navigate via interproc
  End Function

  Public Function NavigateFile(ByVal filePara As String) As System.Windows.Forms.Form Implements IIDEHelper.NavigateFile
    Process.Start(filePara)
  End Function

  Public Function GetTabByPersistString(ByVal persistString As String) As System.Windows.Forms.Form Implements IIDEHelper.GetTabByPersistString

  End Function

  Public Function GetTabList() As String() Implements IIDEHelper.GetTabList
    Return New String() {}
  End Function

  Public ReadOnly Property DIZ() As String Implements IIDEHelper.DIZ
    Get

    End Get
  End Property
End Class


