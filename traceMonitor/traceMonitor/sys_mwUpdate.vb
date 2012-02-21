Module sys_mwUpdate

  '' 
  '' --> Aufruf:
  '' startWebUpdate("<Appname aus Updater>", True)
  '' startWebUpdate("<Appname aus Updater>", False)
  '' 
  '' (bei False wird eine MsgBox ausgegeben, wenn der Updater nicht gefunden wurde, bei True nicht)
  '' 

  Private m_isIde As Integer = 0

  Sub startWebUpdate(ByVal UpdaterAppID As String, ByVal autoUpdate As Boolean)
    If isIDE() Then Exit Sub
    
    Dim updaterFilespec As String = ExePath("mwwebupdate")

    If updaterFilespec = "" OrElse My.Computer.FileSystem.FileExists(updaterFilespec) = False OrElse FileLen(updaterFilespec) < 40000 Then
      If Not autoUpdate Then MsgBox("Der Updater wurde nicht gefunden bzw. du hast eine veraltete Version. " + vbNewLine + "Wenn du die neuste Version hast, starte den Updater einmal von Hand und versuche es nochmals. Ansonsten melde dich bei Max Weller ;-)")
      Exit Sub
    End If

    Dim updaterPara(5) As String
    updaterPara(0) = UpdaterAppID 'appID
    updaterPara(1) = glob.appPath 'localFolder
    updaterPara(2) = "/autostart" 'autostart?
    updaterPara(3) = "/autoclose" 'autoclose?
    updaterPara(4) = Application.ExecutablePath 'startAfterUpdate(1)
    updaterPara(5) = "" 'startAfterUpdate(2)
    Dim updaterArguments As String = Join(updaterPara, "#²#")

    'MsgBox(updaterFilespec + " " + updaterArguments, , "Updater wird aufgerufen...")

    glob.saveParaFile()
    Process.Start(updaterFilespec, updaterArguments)

  End Sub

  Function isIDE() As Boolean
    If m_isIde = 0 Then
      Dim procName = Process.GetCurrentProcess().ProcessName
      m_isIde = If(procName.EndsWith(".vshost"), 2, 1)
    End If
    If m_isIde = 2 Then Return True
    If m_isIde = 1 Then Return False
  End Function

End Module
