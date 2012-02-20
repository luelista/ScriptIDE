Module app_main
  Public Const apo = Chr(34)

  'Public settingsFolder As String = "C:\yPara\scriptIDE\"
  Public helpFilePath As String
  'Public glob As New cls_globPara(settingsFolder + "scriptIDE.para.txt")
  Public Const APP_ID = "scriptide"
  Public globGridHistory As New List(Of Integer)
  Public globGridHistoryCurIndex As String = ""
  Public globSkipNextHistoryAdd As Boolean = False
  
  'Public ZZ As New cls_scriptHelper()

  Public scriptHelperMethods As New Dictionary(Of String, String())

  Public skipMdiActivationEvent As Boolean = False


  'toolfenster
  Public Const tbPrefix As String = "Addin|##|siaIDEMain|##|"
  'Public Const tbConsole_ID As String = "Addin|##|siaIDEMain.tbConsole"
  'Public Const tbGlobSearch_ID As String = "Addin|##|siaIDEMain.tbGlobSearch"
  'Public Const tbIndexList_ID As String = "Addin|##|siaIDEMain.tbIndexList"
  'Public Const tbOpenedFiles_ID As String = "Addin|##|siaIDEMain.tbOpenedFiles"
  Public tbConsole As New frmTB_console
  Public tbLuaInstances As New frmTB_luaInstances
  'Public tbDebug As frmTB_debug = sH.getInformationWindowRef()
  'Public tbErrorList As frmTB_compileErrors = sH.getInformationWindowRef()
  'Public tbFileExplorer As New frmTB_fileExplorer
  'Public tbFtpExplorer As New frmTB_ftpExplorer
  Public tbGlobSearch As New frmTB_globSearch
  Public tbIndexList As New frmTB_indexList
  Public tbOpenedFiles As New frmTB_openedFiles
  Public tbFileExplorer As New frmTB_fileExplorer
  Public tbFtpExplorer As New frmTB_ftpExplorer

  'Public tbRtfFilelist As New frmTB_rtfFilelist
  'Public tbTracePrintLine As New frmTB_tracePrintLine
  'Public tbScriptWin As New Dictionary(Of String, frmTB_scriptWin)
  Public tbAddinWin As New List(Of String) 'New Dictionary(Of String, DockContent)
  Public tbLegacyWidget As New Dictionary(Of String, frmTB_legacyWidget)


  Sub navHelpByKeyword(ByVal hlpKeywd As String)
    If helpFilePath = "" Then MsgBox("Hilfedatei nicht gefunden!", MsgBoxStyle.Critical, "Fehler") : Exit Sub
    Help.ShowHelp(Workbench.Instance, helpFilePath, HelpNavigator.Topic, "scriptide/" + hlpKeywd.ToLower + ".htm")
  End Sub

  Function getDeserializedDockContent(ByVal persistString As String) As Object
    Try
      Debug.Print(persistString)
      'TT.Write("getDeserializedDockContent", persistString)
      Dim para() = Split(persistString, "|##|")
      Select Case para(0).ToUpper
        Case "TOOLBAR"
          Dim formKey As String = para(1).ToLower
          Debug.Print("LoadWin: " + persistString)
          'If formKey = "tbdebug" Then Return sH.getInformationWindowRef()
          'If formKey = "tberrorlist" Then Return sH.getErrorListRef()
          'If formKey = "tbfileexplorer" Then Return tbFileExplorer
          'If formKey = "tbftpexplorer" Then Return tbFtpExplorer
          'If formKey = "tbconsole" Then Return tbConsole
          'If formKey = "tbglobsearch" Then Return tbGlobSearch
          'If formKey = "tbindexlist" Then Return tbIndexList
          'If formKey = "tbopenedfiles" Then Return tbOpenedFiles
          'If formKey = "tbrtffilelist" Then Return tbRtfFilelist
          'If formKey = "tbtraceprintline" Then Return tbTracePrintLine
          If formKey = "tbscriptwin" Then
            Dim key = para(2).ToLower

            Dim clsName As String = ""
            If key.Contains(".") Then clsName = key.Substring(0, key.IndexOf("."))

            Return ScriptWindowHelper.ScriptWindowManager.CreateWindow(key, clsName, DWndFlags.DockWindow Or DWndFlags.NoAutoShow, DockState.Hidden, False).Form 'ZZ.IDEHelper.CreateDockWindow(key, -1).Form '-1= nicht SHOW aufrufen
            'If tbScriptWin.ContainsKey(key) Then
            '  Return tbScriptWin(key)
            'Else
            '  Dim ref = scriptClass(key.Substring(0, key.IndexOf(".")))
            '  ref.autoStart()
            '  If tbScriptWin.ContainsKey(key) Then Return tbScriptWin(key)
            'End If
          End If
          If formKey = "tblegacywidget" Then
            'Dim key = para(2).ToLower + "|" + para(3).ToLower
            If tbLegacyWidget.ContainsKey(persistString.ToLower) Then
            Else
              Dim win As New frmTB_legacyWidget
              tbLegacyWidget.Add(persistString.ToLower, win)
              win.txtWidgetfilename.Text = para(2)
              win.txtClass.Text = para(3)
              'win.Show(MAIN.DockPanel1)
            End If
            Return tbLegacyWidget(persistString.ToLower)
          End If
          'Case "SCRIPT"

          '  Return ScriptWindowHelper.ScriptWindowManager.CreateWindow(LCase(para(2)), para(1), DWndFlags.DockWindow Or DWndFlags.NoAutoShow, DockState.Hidden, False).Form 'ZZ.IDEHelper.CreateDockWindow(key, -1).Form '-1= nicht SHOW aufrufen

        Case "ADDIN"

          Dim addin = AddinInstance.GetAddinReference(para(1))
          If addin Is Nothing Then Return Nothing
          Return addin.GetAddinWindow(persistString)

        Case Else
          If para.Length = 2 Then
            Dim ref = gotoNote(para(1), True, True)
            Return ref
          End If
      End Select
    Catch ex As Exception
      TT.Write("Error while fetching Win:" + persistString, ex.ToString, "err")
    End Try
  End Function

  Sub initToolboxWindows()
    On Error Resume Next
    If IO.File.Exists(ParaService.ProfileFolder + "dockLayout.xml") Then
      skipMdiActivationEvent = True

      Dim del As New DeserializeDockContent(AddressOf getDeserializedDockContent)
      Workbench.Instance.DockPanel1.LoadFromXml(ParaService.ProfileFolder + "dockLayout.xml", del)

      skipMdiActivationEvent = False
      If Workbench.Instance.DockPanel1.ActiveDocument IsNot Nothing AndAlso TypeOf Workbench.Instance.DockPanel1.ActiveDocument Is IDockContentForm Then
        Dim frm As IDockContentForm = Workbench.Instance.DockPanel1.ActiveDocument
        setActRtfBox(frm)
      End If
    Else

      'tbFileExplorer.Show(MAIN.DockPanel1, DockState.DockRight)
      'tbFtpExplorer.Show(MAIN.DockPanel1, DockState.DockRight)
      tbIndexList.Show() '(MAIN.DockPanel1, DockState.DockLeft)
      tbOpenedFiles.Show() '(MAIN.DockPanel1, DockState.DockLeft)
      getDeserializedDockContent("Addin|##|siaCommonProtocols|##|LocalFileBrowser").show()
    End If
  End Sub


  Function isIDE() As Boolean
    isIDE = False
#If DEBUG Then
    isIDE = True
#End If
  End Function

  Function getKeyString(ByVal e As Object) As String
    getKeyString = If(e.Control, "CTRL-", "") + If(e.Alt, "ALT-", "") + If(e.Shift, "SHIFT-", "") + _
                   e.KeyCode.ToString.ToUpper
  End Function

  Sub nextInstance(ByVal args As String)
    Dim cmd As New CommandLineParser(args)

    If cmd.NamedArgs.ContainsKey("/terminate") Then
      TT.Write("Terminating by Command Line Parameter", , "shutdown")
      Workbench.Instance.Close()
      Exit Sub
    End If
    Workbench.Instance.Activate()
    If Workbench.Instance.WindowState = FormWindowState.Minimized Then Workbench.Instance.WindowState = FormWindowState.Normal
    Workbench.Instance.IsSmallMode = False



    Dim forceFT As String = ""
    If cmd.NamedArgs("openwith") Then forceFT = "[." + cmd.NamedArgs("openwith") + "]"
    For Each fileSpec In cmd.FreeArgs
      If IO.File.Exists(fileSpec) Then
        onNavigate(forceFT + fileSpec)
      End If
    Next


  End Sub

End Module
