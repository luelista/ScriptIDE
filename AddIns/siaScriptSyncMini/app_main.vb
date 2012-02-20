'Imports System.Drawing

Module app_main
  Public helper As IScriptHelper
  Public WithEvents IDE As IIDEHelper


  Public tbScriptSync As New frmTB_scriptSync
  Public Const tbScriptSync_ID = "Addin|##|siaScriptSyncMini|##|ScriptSyncMini"

  Public Const filesCatID = 10
  Public globAktAppID As Integer
  Public globAktappInfo As MWupd3File


  Function getAppInfo(ByVal appID As String) As String
    Dim url = "http://mwupd3.teamwiki.net/request.php?c=get_true_app_info&appid=" + appID
    Dim RES = TwAjax.getUrlContent(url, "twnetSID=" + twSessID)
    Dim LINES() = Split(RES, vbNewLine, 4)
    If checkIfErrorResult(LINES) = False Then Return ""
    Return LINES(3)
  End Function

  Function getAppList(Optional ByVal appID As String = Nothing) As String()
    Dim url = "http://mwupd3.teamwiki.net/request.php?c=list_apps"
    If Not String.IsNullOrEmpty(appID) Then url += "&catid=" + appID
    Dim RES = TwAjax.getUrlContent(url, "twnetSID=" + twSessID)
    Dim LINES() = Split(RES, vbNewLine) ', 4)
    If checkIfErrorResult(LINES) = False Then Return LINES
    Return LINES '(3)
  End Function



End Module
