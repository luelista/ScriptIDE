Public Class ContentViewerService

  Shared Function GetDocumentViewer(ByVal origFileExtension As String) As Object
    origFileExtension = origFileExtension.ToLower
    Dim fileExtension = ";*" + origFileExtension + ";"
    Dim path = AddInTree.GetTreeNode("/Workspace/ContentViewers")
    path.EnsureSorted()
    Dim defaultViewer As Codon = Nothing
    For Each cod In path.Codons
      Dim checkVar = ";" + cod.Properties("filefilter") + ";"
      If defaultViewer Is Nothing And checkVar = ";*.*;" Then defaultViewer = cod
      If checkVar.Contains(fileExtension) Or LCase(cod.Properties("id")) = origFileExtension Then
        Select Case cod.Properties("mode").ToLower
          Case "dockcontent"
            Return cod.BuildItem(Nothing, Nothing)
          Case "nogui"
            Return False 'STOP
        End Select
      End If
    Next
    If defaultViewer IsNot Nothing Then
      Return defaultViewer.BuildItem(Nothing, Nothing)
    End If
    Return Nothing
  End Function

  Public Shared Function ShowChooser(ByVal InternalURL As String) As String
    Using f As New frm_ProtocolOpenAsDialog
      f.lblFilespec.Text = InternalURL
      Dim path = AddInTree.GetTreeNode("/Workspace/ContentViewers", False)
      For Each cod In path.Codons
        Dim checkVar = cod.Properties("filefilter")
        Dim lvi = f.ListView1.Items.Add(cod.Properties("id"))
        lvi.SubItems.Add(checkVar)
      Next
      If f.ShowDialog = DialogResult.OK Then
        Return "[" + LCase(f.ListView1.SelectedItems(0).Text) + "]" + InternalURL
      Else
        Return Nothing
      End If
    End Using
  End Function

End Class
