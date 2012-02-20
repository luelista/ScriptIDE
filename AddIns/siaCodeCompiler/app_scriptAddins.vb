Imports System.Xml
Imports ScriptIDE.Main

Module app_scriptAddins

  Public Sub ConnectFromScript(ByVal fileSpec As String, ByVal mode As ConnectMode)
    'Addin doublettenCheck
    ' Dim checkFor = IO.Path.GetFileNameWithoutExtension(LCase(fileSpec))
    ' If AddinInstance.IsAddinLoaded(checkFor) Then AddinInstance.RemoveAddIn(checkFor)

    'Dim host As New scHostNET(fileSpec)
    Dim host = ScriptHost.Instance.getScriptClassHost(fileSpec)
    Dim reader As XmlTextReader = CType(host, scHostNET2).getAddInDataXmlReader()

    'host.initScriptHost()
    If host.isIniDone() = False Then
      MsgBox("Fehler beim Verbinden des Add-ins!" + vbNewLine + "Die Initialisierung des ScriptHosts wurde nicht abgeschlossen.", MsgBoxStyle.Critical, "Add-in-Verwaltung: " + fileSpec)
      Exit Sub
    End If

    Dim connectRef = host.getClassRef()
    If connectRef Is Nothing OrElse Not TypeOf connectRef Is IAddinConnect Then
      MsgBox("Fehler beim Verbinden des Add-ins!" + vbNewLine + "Die ScriptKlasse muss das Interface IAddinConnect implementieren.", MsgBoxStyle.Critical, "Add-in-Verwaltung: " + fileSpec)
      Exit Sub
    End If

    AddinInstance.ConnectFromScriptData(fileSpec, reader, host.assemblyRef, connectRef, mode)
  End Sub

  Sub LoadAllScriptAddins()
    On Error Resume Next
    Dim fileCont = IO.File.ReadAllText(ParaService.ProfileFolder + "AddinList3.txt")
    Dim LINES() = Split(fileCont, vbNewLine)

    For Each fileSpec In LINES
      If IO.File.Exists(fileSpec) Then
        ConnectFromScript(fileSpec, ConnectMode.Startup)
      End If
    Next
  End Sub

End Module
