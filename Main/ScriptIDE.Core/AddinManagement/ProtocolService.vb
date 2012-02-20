
Public Class ProtocolService
  
  Private Shared Function urlHelper(ByVal url As String) As String
    url = Replace(url, "\", "/")
    If url.Substring(1, 2) = ":/" Then
      url = "loc:/" + url
    End If
    Return url
  End Function

  Public Shared Function MapToLocalFilename(ByVal InternalURL As String) As String
    InternalURL = urlHelper(InternalURL)
    Dim ph = GetURLProtocolHandler(InternalURL)
    If ph Is Nothing Then Throw New IO.FileNotFoundException("Couldn't create protocol handler for " + InternalURL)
    Return ph.MapToLocalFilename(InternalURL)
  End Function

  Shared Function ReadFileFromURL(ByVal InternalURL As String) As String
    InternalURL = urlHelper(InternalURL)
    Dim ph = GetURLProtocolHandler(InternalURL)
    If ph Is Nothing Then Throw New IO.FileNotFoundException("Couldn't create protocol handler for " + InternalURL)
    Return ph.ReadFile(InternalURL)
  End Function

  Shared Sub SaveFileToURL(ByVal InternalURL As String, ByVal content As String)
    InternalURL = urlHelper(InternalURL)
    Dim ph = GetURLProtocolHandler(InternalURL)
    If ph Is Nothing Then Throw New IO.FileNotFoundException("Couldn't create protocol handler for " + InternalURL)
    ph.SaveFile(InternalURL, content)

    'cls_IDEHelper.instance.OnDocumentAfterSave(InternalURL)
  End Sub

  Shared Sub NavigateFilelistToURL(ByVal InternalURL As String)
    InternalURL = urlHelper(InternalURL)
    Dim ph = GetURLProtocolHandler(InternalURL)
    If ph Is Nothing Then Throw New IO.FileNotFoundException("Couldn't create protocol handler for " + InternalURL)
    ph.NavigateFilelistTo(InternalURL)
  End Sub

  Shared Sub ShowFilelistForProtocol(ByVal Protocol As String)
    Dim ph = GetProtocolHandler(Protocol)
    If ph Is Nothing Then Throw New IO.FileNotFoundException("Couldn't create protocol handler for " + Protocol)
    ph.ShowFilelist()
  End Sub

  Public Shared Function GetURLProtocolHandler(ByVal InternalURL As String) As IProtocolHandler
    Dim pos = InternalURL.IndexOf(":")
    If pos = -1 Then Throw New IO.FileNotFoundException("Invalid URL")
    Dim protocol = InternalURL.Substring(0, pos).ToLower
    Return GetProtocolHandler(protocol)
  End Function

  Public Shared Function GetProtocolHandler(ByVal Protocol As String) As IProtocolHandler
    'For Each inst In AddinInstance.Addins
    '  If inst.Loaded = False Then Continue For
    '  If inst.HandlesProtocols.ContainsKey(Protocol) Then
    '    Return Activator.CreateInstance(inst.HandlesProtocols(Protocol))
    '  End If
    'Next
    If Protocol.EndsWith(":") = False Then Protocol += ":"
    Dim path = AddInTree.GetTreeNode("/Workspace/ProtocolHandlers", False)
    For Each cod In path.Codons
      If cod.Properties("protocol") = Protocol Then
        Return cod.BuildItem(Nothing, Nothing)
      End If
    Next
    Return Nothing
  End Function


End Class