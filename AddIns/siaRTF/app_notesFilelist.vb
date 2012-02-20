Imports System.Windows.Forms

Module app_notesFilelist

  Sub iniNotesFileList(ByVal lst As TreeView)
    On Error Resume Next
    Dim list = TwAjax.listDir("memo")
    lst.Nodes.Clear()

    For Each fileSpec In list
      ' If Not it.StartsWith("note") Then Continue For
      Dim p() = Split(fileSpec, "/")
      Dim nc = getNodeColByPath(lst, p)
      nc.Add(fileSpec, p(p.Length - 1)).Tag = fileSpec

      'indent(p.Length - 1).Add(fileSpec, p(p.Length - 1))
      'If lst.Nodes.ContainsKey(p(0)) = False Then
      '  Dim fldNode = lst.Nodes.Add(p(0), p(0), "folderclose")
      '  fldNode.SelectedImageKey = "folderopen"
      '  'fldNode.NodeFont = New Font(lst.Font, FontStyle.Bold)
      'End If
      ' lst.Nodes(p(0)).Nodes.Add(it, p(1).Substring(0, p(1).Length - 4)).Tag = it
    Next
    lst.ExpandAll()
    lst.Sort()
  End Sub

  Function getNodeColByPath(ByVal lst As TreeView, ByVal path() As String) As TreeNodeCollection
    Dim nc As TreeNodeCollection = lst.Nodes
    For i = 0 To path.Length - 2
      If nc.ContainsKey(path(i)) Then
        nc = nc(path(i)).Nodes
      Else
        nc = nc.Add(path(i), path(i), "folderclose", "folderopen").Nodes
      End If
    Next
    Return nc
  End Function

End Module
