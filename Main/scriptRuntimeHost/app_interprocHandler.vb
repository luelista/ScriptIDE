Module app_interprocHandler

  Public WithEvents oIntWin As sys_interproc

  Sub interproc_init()
    Dim wndName As String = "siRuntimeHost"

    oIntWin = New sys_interproc(wndName)
    oIntWin.Commands.Add("CMD", "LoadLibrary", "fileSpec", "Loads a compiled script file dynamically")

  End Sub

  Private Sub oIntWin_DataRequest(ByVal source As String, ByVal cmdString As String, ByVal para As String, ByRef returnValue As String) Handles oIntWin.DataRequest
    'trace("INTERPROC - DataRequest: " + vbTab + cmdString + vbTab + source + vbTab + para)

    Select Case cmdString.ToUpper

    End Select


    'trace("INTERPROC - returnValue: " + vbTab + returnValue)
  End Sub

  Private Sub oIntWin_Message(ByVal source As String, ByVal cmdString As String, ByVal para As String) Handles oIntWin.Message
    TT.Write("INTERPROC - Message: " + vbTab + cmdString + vbTab + source + vbTab + para)
    
    Select Case cmdString
      Case "LoadLibrary"



    End Select



  End Sub

End Module
