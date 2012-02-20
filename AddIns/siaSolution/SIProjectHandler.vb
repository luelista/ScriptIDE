<FiletypeHandler(New String() {"*.sip"}, False)> Public Class SIProjectHandler
  Implements IPlainFiletypeHandler

  Public Sub NavigateFile(ByVal InternalURL As String) Implements IPlainFiletypeHandler.NavigateFile
    'Dim ph = IDE.GetURLProtocolHandler(InternalURL)
    'If ph Is Nothing Then Exit Sub
    'tbSolution.openProjectFile(ph.MapToLocalFilename(InternalURL))
    tbSolution.openProjectFile(InternalURL)
  End Sub

End Class
