Public Class Connect
  Implements IAddinConnect
  Implements ISkinnable

  Public Function GetAddinWindow(ByVal PersistString As String) As System.Windows.Forms.Form Implements Core.IAddinConnect.GetAddinWindow
    If PersistString.ToLower() = tbPrefix.ToLower + "console" Then Return tbConsole
    If PersistString.ToLower() = tbPrefix.ToLower + "globsearch" Then Return tbGlobSearch
    If PersistString.ToLower() = tbPrefix.ToLower + "indexlist" Then Return tbIndexList
    If PersistString.ToLower() = tbPrefix.ToLower + "openedfiles" Then Return tbOpenedFiles
    If PersistString.ToLower() = tbPrefix.ToLower + "localfilebrowser" Then Return tbFileExplorer
    If PersistString.ToLower() = tbPrefix.ToLower + "ftpbrowser" Then Return tbFtpExplorer
    If PersistString.ToLower() = tbPrefix.ToLower + "luainstances" Then Return tbLuaInstances
  End Function

  Public Sub OnNavigate(ByVal kind As Core.NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object) Implements Core.IAddinConnect.OnNavigate
    Select Case command.ToUpper
      Case "GETACTIVEURL"
        returnValue = cls_IDEHelper.Instance.getActiveTabFilespec()

      Case "GETACTIVEFILESPEC"
        returnValue = ProtocolService.MapToLocalFilename(cls_IDEHelper.Instance.getActiveTabFilespec())

      Case "NAVIGATE"
        app_tabManager.onNavigate(args)

      Case "ISFILELOADED"
        Dim tabRef As Object
        parseDocumentURL(args, Nothing, Nothing, Nothing, tabRef)
        If tabRef Is Nothing Then
          returnValue = "FALSE"
        Else
          returnValue = "TRUE"
        End If

      Case "_NEXTINSTANCE"
        nextInstance(args)

      Case "NAVIGATESCRIPTCLASSLINE"
        'TODO
        'Dim abPos = args.LastIndexOf("?")
        'If abPos = -1 Then Exit Sub
        ''Dim parts() = Split(args, "?")
        'Dim fileSpec = sh.expandScriptClassName(args.Substring(0, abPos))
        'app_main.onNavigate(fileSpec + "?" + args.Substring(abPos + 1))

      Case "FILE.NEW"


      Case "FILE.OPEN"
        Using ofd As New OpenFileDialog
          ofd.Title = "Datei öffnen ..."
          ofd.Filter = "Alle Dateien (*.*)|*.*"
          If ofd.ShowDialog = DialogResult.OK Then
            cls_IDEHelper.Instance.NavigateFile(ofd.FileName)
          End If
        End Using

      Case "FILE.OPENURL"
        Dim url = InputBox("Gib eine URL ein, die geöffnet werden soll...", , ParaService.Glob.para("siaIDEMain__Connect__lastUrlnav"))
        If url = "" Then Exit Sub
        ParaService.Glob.para("siaIDEMain__Connect__lastUrlnav") = url
        cls_IDEHelper.GetSingleton.NavigateFile(url)

      Case "FILE.SAVE"
        Try
          Dim tab As Object = cls_IDEHelper.Instance.getActiveTab()
          tab.onSave()
        Catch : End Try

      Case "FILE.SAVEALL"
        app_tabManager.saveAll()

      Case "FILE.RENAME"
        Dim tab As IDockContentForm = TryCast(cls_IDEHelper.Instance.getActiveTab(), IDockContentForm)
        If tab IsNot Nothing Then
          Dim curURL = tab.URL
          Dim ph As IProtocolHandler2 = TryCast(ProtocolService.GetURLProtocolHandler(curURL), IProtocolHandler2)

          If ph Is Nothing Then
            MsgBox("Diese Aktion wird vom Protokollhandler für diese Datei nicht unterstützt.", MsgBoxStyle.Critical, "Datei umbenennen")
          End If
          Dim newURL As String
          If ph.ShowSaveAsDialog("Datei umbenennen ...", curURL, newURL, Nothing, 0) Then
            ph.RenameFile(curURL, newURL)

            Dim ft As String = "" 'Syntax mit z.b. [.hex] am Anfang berücksichtigen
            parseDocumentURL(tab.Hash, Nothing, Nothing, ft, Nothing)
            renameTab(tab.Hash, ft + newURL)
            tab.Hash = ft + newURL
            tab.URL = newURL
          End If
        End If

      Case "FILE.SAVEAS"
        Dim tab As IDockContentForm = TryCast(cls_IDEHelper.Instance.getActiveTab(), IDockContentForm)
        If tab IsNot Nothing Then
          Dim curURL = tab.URL
          Dim ph As IProtocolHandler2 = TryCast(ProtocolService.GetURLProtocolHandler(curURL), IProtocolHandler2)

          If ph Is Nothing Then
            MsgBox("Diese Aktion wird vom Protokollhandler für diese Datei nicht unterstützt.", MsgBoxStyle.Critical, "Datei speichern unter")
          End If
          Dim newURL As String
          If ph.ShowSaveAsDialog("Datei speichern unter ...", curURL, newURL, Nothing, 0) Then
            Dim ft As String = "" 'Syntax mit z.b. [.hex] am Anfang berücksichtigen
            parseDocumentURL(tab.Hash, Nothing, Nothing, ft, Nothing)
            renameTab(tab.Hash, ft + newURL)
            tab.Hash = ft + newURL
            tab.URL = newURL
            tab.onSave()
          End If
        End If

      Case "FILE.CLOSE"
        closeTab(cls_IDEHelper.Instance.getActiveTab())

      Case "FILE.CLOSEALL"
        If MsgBox("Alle geöffneten Tabs schließen?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "") = MsgBoxResult.Yes Then
          closeAllTabs()
        End If

      Case "FILE.OPENFILEAS"
        Dim tab As IDockContentForm = TryCast(cls_IDEHelper.Instance.getActiveTab(), IDockContentForm)
        If tab IsNot Nothing Then
          Dim curURL = tab.URL
          Dim newURL = ContentViewerService.ShowChooser(curURL)
          If Not String.IsNullOrEmpty(newURL) Then
            gotoNote(newURL)
          End If
        End If

      Case "FILE.EXITPROGRAM"
        Workbench.Instance.Close()

      Case "WINDOW.CONSOLE"
        tbConsole.Show() : tbConsole.Activate()

      Case "WINDOW.INDEXLIST"
        tbIndexList.Show() : tbIndexList.Activate()

      Case "WINDOW.OPENEDFILES"
        tbOpenedFiles.Show() : tbOpenedFiles.Activate()

      Case "WINDOW.GLOBSEARCH"
        tbGlobSearch.Show() : tbGlobSearch.Activate()

      Case "WINDOW.LUA.INSTANCES"
        tbLuaInstances.Show() : tbLuaInstances.Activate()

      Case "WINDOW.SHOWLOCALBROWSER"
        tbFileExplorer.Show() : tbFileExplorer.Activate()

      Case "WINDOW.SHOWFTPBROWSER"
        tbFtpExplorer.Show() : tbFtpExplorer.Activate()

      Case "HELP.ABOUT"
        Using f As New frm_about
          f.ShowDialog()
        End Using

      Case "HELP.WEBSITE"
        Process.Start("http://vbnet.teamwiki.net/scriptide/scriptide.html")

      Case "HELP.TOPICS"
        If helpFilePath = "" Then MsgBox("Hilfedatei nicht gefunden!", MsgBoxStyle.Critical, "Fehler") : Exit Sub
        Help.ShowHelp(Workbench.Instance, helpFilePath)

      Case "TOOLS.OPTIONS"
        Workbench.ShowOptionsDialog()

      Case "LUA.ADDINREGISTER"
        LuaScripting.Instance.AddinRegister()

      Case "LUA.ADDINUNREGISTER"
        LuaScripting.Instance.AddinUnregister()

      Case "LUA.RUN"
        LuaScripting.Instance.Run()

      Case "LUA.DEBUGRUN"
        LuaScripting.Instance.Debug()

      Case "LUA.CONTINUERUN"
        LuaScripting.Instance.ContinueRun()

      Case "LUA.CONTINUESTEP"
        LuaScripting.Instance.ContinueStep()

      Case "LUA.BREAK"
        LuaScripting.Instance.Break()

      Case "LUA.STOP"
        LuaScripting.Instance.stopLua()

      Case Else
        TT.Write("siaIDEMain.Connect.OnNavigate", "command not recognized: " + command, "err")
    End Select
  End Sub

  Public Sub Connect(ByVal application As Object, ByVal connectMode As Core.ConnectMode, ByVal addInInst As Core.AddinInstance, ByRef custom As Object) Implements Core.IAddinConnect.Connect

  End Sub

  Public Sub Disconnect(ByVal removeMode As Core.DisconnectMode, ByRef custom As Object) Implements Core.IAddinConnect.Disconnect

  End Sub

  Public Sub OnAddinUpdate(ByVal addinChanged As String, ByRef custom As Object) Implements Core.IAddinConnect.OnAddinUpdate

  End Sub

  Public Sub OnBeforeShutdown(ByRef cancel As Boolean, ByRef custom As Object) Implements Core.IAddinConnect.OnBeforeShutdown

  End Sub

  Public Sub OnStartupComplete(ByRef custom As Object) Implements Core.IAddinConnect.OnStartupComplete

  End Sub

  Public Function GetSkinObject(ByVal id As String) As Object Implements Core.ISkinnable.GetSkinObject
    Select Case id
      Case "MainWindow" : Return cls_IDEHelper.Instance.Skin
      Case "Toolstrips" : Return Office2007Renderer.Office2007ColorTable.GetDefault()
    End Select
  End Function
End Class
