Imports System.Drawing

Module app_main
  Public helper As IScriptHelper
  Public WithEvents IDE As IIDEHelper

  Dim skinObjCache As New Dictionary(Of String, Object)

  'Public tbFileExplorer As New frmTB_fileExplorer

  Private Sub IDE_DocumentTabActivated(ByVal rtf As Object, ByVal key As String) Handles IDE.DocumentTabActivated
    'Dim tb = IDE.CreateToolbar("siaTestAddin.infoTB")
    'Dim hlp As IDockContentForm = rtf
    'tb.Element("txt_actfile").Text = key
  End Sub

  Private Sub IDE_OnDialogEvent(ByVal winID As String, ByVal eventName As String, ByVal eventArgs As ScriptEventArgs) Handles IDE.OnDialogEvent
    If winID = "siaTestAddin.infoTB" Then
      onToolbarEvent(eventName, eventArgs)
    End If
  End Sub

  Sub onToolbarEvent(ByVal eventName As String, ByVal eventArgs As ScriptEventArgs)
    Select Case eventArgs.Sender.Name
      Case "btn_scr_run"
        ' Windows.Forms.SendKeys.Send("{F11}")
        ' IDE.globBreakMode = BreakMode.Run

      Case "btn_scr_singlestep"
        ' Windows.Forms.SendKeys.Send("{F9}")
        ' IDE.globBreakMode = BreakMode.SingleStep

      Case "btn_scr_pause"
        ' Windows.Forms.SendKeys.Send("{F12}")
        ' IDE.globBreakMode = BreakMode.Break

      Case "btn_scr_stop"
        Windows.Forms.SendKeys.Send("^{F12}")

    End Select
  End Sub

  Sub readSkin(ByVal skinName As String)
    On Error Resume Next
    Dim sr As New System.IO.StreamReader(IDE.GetSettingsFolder() + "skins\" + skinName + ".sis")
    Dim activeObjectName As String = "", activeObject As Object
    While Not sr.EndOfStream
      Dim li = sr.ReadLine().Trim()
      If li = "" Then Continue While

      If li.StartsWith("[") And li.EndsWith("]") Then
        activeObjectName = li.Substring(1, li.Length - 2)
        activeObject = getGradientObjectByName(activeObjectName)
        Continue While
      End If
      Dim p() = Split(li, "=", 2)
      If p.Length <> 2 Then Continue While
      Select Case p(0)
        Case "ColorValue" : setGradientObjectByName(activeObjectName, ColorTranslator.FromHtml(p(1)))
        Case "Data" : setGradientObjectByName(activeObjectName, p(1))
        Case "BoolValue" : setGradientObjectByName(activeObjectName, p(1) = "TRUE")
        Case "StartColor" : activeObject.StartColor = ColorTranslator.FromHtml(p(1))
        Case "EndColor" : activeObject.EndColor = ColorTranslator.FromHtml(p(1))
        Case "TextColor" : activeObject.TextColor = ColorTranslator.FromHtml(p(1))
        Case "LinearGradientMode" : activeObject.LinearGradientMode = p(1)
        Case Else : MsgBox("Unbekannte Option " + li)
      End Select
    End While
    sr.Close()
    IDE.getMainFormRef().Refresh()
  End Sub

  Function getSkinObjectByName(ByVal objName As String, Optional ByVal addinName As String = Nothing)
    If objName = "Skin" Then Return IDE.getMainFormRef().DockPanel1.Skin
    If skinObjCache.TryGetValue(LCase(objName), getSkinObjectByName) Then Exit Function
    If addinName Is Nothing Then
      Dim cod As Codon = AddInTree.GetCodon("/OptionsDialog/SkinObjects/" + objName)
      If cod Is Nothing Then Return Nothing
      addinName = cod.Properties("addinname")
    End If
    Dim addin As IAddinConnect = AddinInstance.GetAddinReference(addinName)
    If addin Is Nothing Then Return Nothing
    If TypeOf addin Is ISkinnable Then
      Dim obj = CType(addin, ISkinnable).GetSkinObject(objName)

      skinObjCache(LCase(objName)) = obj
      Return obj
    End If
  End Function

  Function getGradientObjectByName(ByVal objPath As String)
    Dim path() = Split(objPath, ".")

    Dim dps As Object
    If path(0) = "Skin" Then
      dps = IDE.getMainFormRef().DockPanel1.Skin
    Else
      dps = getSkinObjectByName(path(0))
    End If

    Dim obj = dps
    For i = 1 To path.Length - 1
      obj = CallByName(obj, path(i), CallType.Get)
    Next
    Return obj
  End Function

  Sub setGradientObjectByName(ByVal objPath As String, ByVal newValue As Object)
    Dim path() = Split(objPath, ".")

    Dim dps As Object
    If path(0) = "Skin" Then
      dps = IDE.getMainFormRef().DockPanel1.Skin
    Else
      dps = getSkinObjectByName(path(0))
    End If

    Dim obj = dps
    For i = 1 To path.Length - 1
      CallByName(obj, path(i), CallType.Let, newValue)
    Next
  End Sub


End Module
