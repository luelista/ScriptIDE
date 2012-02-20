Imports scriptIDE.Core
Imports scriptIDE.Main

Public Class Program
  Private Shared splash As frm_splash

  Shared Function Main(ByVal args() As String) As Integer
    Application.DoEvents()
    Application.DoEvents()
    Application.DoEvents()
    Application.EnableVisualStyles()
    Application.DoEvents()
    Application.DoEvents()
    Application.DoEvents()

    'If e.CommandLine.Count > 0 AndAlso e.CommandLine(0) = "/hidesplash" Then Exit Function
    Dim cmdLine As String = Command()

    Dim m = System.Text.RegularExpressions.Regex.Match(cmdLine, "/profile=([-a-zA-Z0-9]+)")
    If Not m.Success Then
      'MsgBox("Dieses Programm kann nicht direkt gestartet werden. Bitte verwenden Sie die SIDE4.exe, um die ScriptIDE zu starten.", MsgBoxStyle.Critical, "ScriptIDE Profilverwaltung")
      ' Exit Function
    Else
      ParaService.ProfileName = m.Groups(1).Value
    End If

    Dim prevInst = sys_interproc.getWindow("scriptide_" + ParaService.ProfileName)
    If prevInst <> 0 Then
      Dim ipc = New sys_interproc("si_temp_ipc_win_" + Hex(Now.Ticks))
      ipc.SendDataHwnd(prevInst, "CMD  ", "_nextInstance", cmdLine, Now.Ticks)
      ipc.DestroyHandle()
      Return 1
    End If

    splash = New frm_splash
    splash.Show()
    Application.DoEvents()

    If System.Diagnostics.Process.GetCurrentProcess().ProcessName.EndsWith(".vshost") = False Then _
      Environment.CurrentDirectory = IO.Path.GetDirectoryName(Application.ExecutablePath)
    'ParaService.ProfileName = m.Groups(1).Value

    If IO.Directory.Exists(ParaService.ProfileFolder) = False Then
      If ParaService.ProfileName = "default" Then
        'MsgBox("Herzlich Willkommen in der ScriptIDE 4.0"+vbNewLine+"Dies ist die FirstStart-Meldung .........")
      Else
        Select Case MsgBox("Der Profilordner """ + ParaService.ProfileName + """ existiert noch nicht. Sollen die Daten aus dem Profil ""default"" übernommen werden?" + vbNewLine + vbNewLine + "Klicke JA, um das neue Profil als Kopie von default zu erstellen." + vbNewLine + "Klicke NEIN, um mit einer leeren IDE zu starten." + vbNewLine + "Klicke ABBRECHEN, um die IDE zu beenden.", 32 + 3, "ScriptIDE Profilverwaltung")
          Case MsgBoxResult.Yes 'copyDir
          Case MsgBoxResult.Cancel : Return 2
        End Select
      End If
    End If

    Interproc.Initialize()
    ParaService.Initialize()

    AddHandler Workbench.SplashScreenEvent, AddressOf SplashScreenEvent
    Workbench.Initialize()
    'AddinInstance.ConnectFromFile(IO.Path.Combine(ParaService.AppPath,"siaIDEMain.AddIn"),true

    Return 0
  End Function

  Private Shared Sub SplashScreenEvent(ByVal mode As Integer, ByVal newText As String)
    If mode = 3 Then splash.Close()
    If mode = 4 Then splash = New frm_splash : splash.Show() : splash.Label1.Text = "Programm wird entladen ..." ' : splash.ProgressBar1.Hide()
    If mode = 1 Or mode = 2 Then
      On Error Resume Next
      splash.Label1.Text = newText
      splash.Text = "ScriptIDE 4 - " + newText
      'If mode = 1 Then splash.ProgressBar1.Increment(1)
      Application.DoEvents()
    End If
  End Sub



End Class
