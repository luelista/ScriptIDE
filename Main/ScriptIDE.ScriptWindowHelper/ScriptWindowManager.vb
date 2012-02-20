Public NotInheritable Class ScriptWindowManager
  Private Sub New()
  End Sub

  Public Shared IdeHelper As IIDEHelper

  Private Shared tbScriptWin As New Dictionary(Of String, frmTB_scriptWin)
  Friend Declare Function ReleaseCapture Lib "user32" () As Integer

  Public Shared Event ScriptWindowEvent(ByVal WinID As String, ByVal ObjectType As String, ByVal EventArgs As ScriptEventArgs)

  Friend Shared Sub OnScriptWindowEvent(ByVal WinID As String, ByVal ObjectType As String, ByVal EventArgs As ScriptEventArgs)
    'TT.Write("ScriptEvent " + WinID + "<", EventArgs.ID + "_" + EventArgs.EventName)
    'TT.Write("ScriptEvent for Class:", EventArgs.ClassName)
    'Dim startTime = cls_scriptHelper.GetTime()
    RaiseEvent ScriptWindowEvent(WinID, ObjectType, EventArgs)
    'cls_IDEHelper.Instance.OnOnDialogEvent(WinID, ObjectType, EventArgs)
    'TT.Write("ON " + EventArgs.ID + "_" + EventArgs.EventName, cls_scriptHelper.GetTime() - startTime & "ms")
  End Sub
  Friend Shared Sub OnScriptWindowClose(ByVal scriptedWindowID As String)
    tbScriptWin.Remove(scriptedWindowID.ToLower)
  End Sub


  Public Shared Function TryGetWindow(ByVal scriptedWindowID As String, ByRef windowRef As IScriptedPanel) As Boolean
    Dim key = "toolbar|##|tbscriptwin|##|" + scriptedWindowID.ToLower
    Dim wndRef As frmTB_scriptWin
    TryGetWindow = tbScriptWin.TryGetValue(key, wndRef)
    windowRef = wndRef.PNL
  End Function

  Public Shared Function ContainsWindow(ByVal scriptedWindowID As String) As Boolean
    Dim key = "toolbar|##|tbscriptwin|##|" + scriptedWindowID.ToLower
    Return tbScriptWin.ContainsKey(key)
  End Function

  Public Shared Function IsWindowVisible(ByVal scriptedWindowID As String) As Boolean
    Dim key = "toolbar|##|tbscriptwin|##|" + scriptedWindowID.ToLower
    Dim wndRef As frmTB_scriptWin
    If Not tbScriptWin.TryGetValue(key, wndRef) Then Return False
    Return wndRef.Visible
  End Function

  Public Shared Function CreateWindow(ByVal scriptedWindowID As String, ByVal className As String, ByVal Flags As DWndFlags, ByVal showHint As Integer, ByRef HasBeenCreated As Boolean) As IScriptedPanel
    On Error Resume Next
    ' Console.WriteLine("Dic? " & (tbScriptWin Is Nothing) & "   " & TypeName(tbScriptWin))
    Dim key = LCase(scriptedWindowID)
    Dim persistString = "ToolBar|##|tbScriptWin|##|" + scriptedWindowID

    If (ParaService.Glob IsNot Nothing) AndAlso (ParaService.Glob.para("scriptWindowList").ToLower.Contains(key) = False) Then
      ParaService.Glob.para("scriptWindowList") = ParaService.Glob.para("scriptWindowList", "|||") + scriptedWindowID + "|||"
    End If

    Dim wndRef As frmTB_scriptWin
    If Not tbScriptWin.TryGetValue("toolbar|##|tbscriptwin|##|" + key, wndRef) Then
      'Console.WriteLine("ERRa?" & Err.Description)
      Dim frm = New frmTB_scriptWin(persistString, className, (Flags And DWndFlags.StdWindow) = 0, showHint)
      'Console.WriteLine("ERRx?" & Err.Description)
      tbScriptWin.Add("toolbar|##|tbscriptwin|##|" + key, frm)
      'Console.WriteLine("ERRy?" & Err.Description)
      HasBeenCreated = True
      'Console.WriteLine("ERRz?" & Err.Description)
      If (Flags And DWndFlags.NoAutoShow) = 0 Then frm.Show()
      Return frm.PNL
    Else
      'Console.WriteLine("ERRb?" & Err.Description)
      If (Flags And DWndFlags.NoAutoShow) = 0 Then wndRef.Show()
      HasBeenCreated = False
      Return wndRef.PNL
    End If
  End Function

  Friend Shared Function getKeyString(ByVal e As Object) As String
    getKeyString = If(e.Control, "CTRL-", "") + If(e.Alt, "ALT-", "") + If(e.Shift, "SHIFT-", "") + _
                   e.KeyCode.ToString.ToUpper
  End Function

  Public Shared Function GetFirstScriptWindow() As IScriptedPanel
    For Each win In tbScriptWin
      Return win.Value.PNL
    Next
    Return Nothing
  End Function

End Class

