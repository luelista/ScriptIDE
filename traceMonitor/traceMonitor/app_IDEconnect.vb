Imports EnvDTE

Module app_IDEconnect

  '' !!! Public evSolution As SolutionEvents
  
  Public IDE As DTE
  Public addinApp As Object
  Public actWindowText As String

  Delegate Sub setFormStatusLabel(ByVal tx As String)


  Sub IDEconnect()
    On Error Resume Next
    'Stop
    Dim objDump As Object
    objDump = CreateObject("refman.Application")

    addinApp = objDump.Obj("addinapp")
    IDE = DirectCast(addinApp.IDE, DTE)

    If IDE Is Nothing Then
      '' aa_frmMain.ToolStripStatusLabel1.Text = "ERR: ideRef is nothing!"
      Exit Sub
    End If
    '' !!! evSolution = IDE.Events.SolutionEvents
    '' !!! aa_frmMain.evWindow = IDE.Events.WindowEvents
    'Stop
    '' aa_frmMain.ToolStripStatusLabel1.Text = "Connected - waiting for Events ..."
  End Sub


  Sub IDEdisconnect()
    On Error Resume Next
    addinApp = Nothing
    IDE = Nothing
    GC.Collect()
    '' aa_frmMain.ToolStripStatusLabel1.Text = "Disconnected."
  End Sub

 

End Module
