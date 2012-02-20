Module app_main
  Public WithEvents IDE As IIDEHelper


  'Public tbIndexList As New frmTB_indexList
  'Public Const tbIndexList_ID = "Addin|##|siaIndexList|##|IndexList"


  Sub onToolbarEvent(ByVal tbName As String)
    On Error Resume Next
    Select Case tbName
      'Case "Window.IndexList"
      '  tbIndexList.Show() : tbIndexList.Activate()


    End Select
  End Sub


End Module
