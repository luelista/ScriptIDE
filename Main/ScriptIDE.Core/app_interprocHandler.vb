Public Class Interproc

  Public Shared WithEvents oIntWin As sys_interproc

  Public Shared Function SendCommand(ByVal titleName As String, ByVal cmdString As String, ByVal para As String) As Boolean
    If oIntWin IsNot Nothing Then Return oIntWin.SendCommand(titleName, cmdString, para)
  End Function
  Public Shared Function GetData(ByVal titleName As String, ByVal cmdString As String, ByVal para As String, Optional ByVal timeout As Integer = 5000) As String
    If oIntWin IsNot Nothing Then Return oIntWin.GetData(titleName, cmdString, para, timeout)
  End Function

  Public Shared Sub Initialize()
    oIntWin = New sys_interproc("scriptide_" + ParaService.ProfileName)


  End Sub

  Public Shared Sub UpdateCommandDefinition()
    Dim path = AddInTree.GetTreeNode("/Core/Interproc/Commands")

    For Each cod In path.Codons
      Dim typ As String
      Select Case cod.Name
        Case "QUERY", "DATAREQUEST", "GETCOMMAND", "FETCH"
          typ = "QUERY"
        Case "CMD", "COMMAND", "MESSAGE"
          typ = "CMD"
        Case Else
          Continue For
      End Select
      Dim cmd = oIntWin.Commands.Add(typ, cod.Id, cod.Properties("parameters"), cod.Properties("help"))
      cmd.Tag = cod.AddIn
    Next

    'With oIntWin.Commands
    '  .Add("QUERY", "GetActiveURL", "-", "gibt die URL des akt. Tab zurück")
    '  .Add("QUERY", "GetActiveFileSpec", "-", "gibt den Dateipfad des akt. Tab zurück")

    '  .Add("CMD  ", "Navigate", "FileSpec[?lineNr]", "FileSpec=Lokaler Dateiname oder URI, lineNr(optional)=zeilennummer")
    '  .Add("CMD  ", "NavigateScriptClassLine", "className?lineNr", "")
    'End With

  End Sub



  Private Shared Sub oIntWin_DataRequest(ByVal source As String, ByVal cmdString As String, ByVal para As String, ByRef returnValue As String) Handles oIntWin.DataRequest
    Dim addin As AddinInstance = oIntWin.Commands("QUERY:" + cmdString).Tag
    If addin Is Nothing Then Exit Sub

    Dim rValue As String = ""
    addin.ConnectRef.OnNavigate(NavigationKind.InterprocDataRequest, source, cmdString, para, rValue)

    returnValue = rValue


    ''trace("INTERPROC - DataRequest: " + vbTab + cmdString + vbTab + source + vbTab + para)
    'Select Case cmdString.ToUpper
    '  Case "GETACTIVEURL"
    '    returnValue = ZZ.getActiveTabFilespec()

    '  Case "GETACTIVEFILESPEC"
    '    returnValue = cls_IDEHelper.Instance.ProtocolManager.MapToLocalFilename(ZZ.getActiveTabFilespec())
    'End Select
    ''trace("INTERPROC - returnValue: " + vbTab + returnValue)
  End Sub

  Private Shared Sub oIntWin_Message(ByVal source As String, ByVal cmdString As String, ByVal para As String) Handles oIntWin.Message
    ''trace("INTERPROC - Message: " + vbTab + cmdString + vbTab + source + vbTab + para)

    'Select Case cmdString.ToUpper
    '  Case "NAVIGATE"
    '    onNavigate(para)

    '  Case "NAVIGATESCRIPTCLASSLINE"
    '    Dim abPos = para.LastIndexOf("?")
    '    If abPos = -1 Then Exit Sub
    '    Dim parts() = Split(para, "?")
    '    Dim fileSpec = sh.expandScriptClassName(para.Substring(0, abPos))
    '    onNavigate(fileSpec + "?" + para.Substring(abPos + 1))
    'End Select
  End Sub

End Class
