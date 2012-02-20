Imports TenTec.Windows.iGridLib
Module sys_IGridReadSave

  Function JoinIGridLine(ByVal line As iGRow, Optional ByVal Delimiter As String = vbTab) As String
    Dim max = line.Cells.Count - 1
    Dim out(max) As String
    For i As Integer = 0 To max
      out(i) = line.Cells(i).Value.ToString
    Next
    Return Join(out, Delimiter)
  End Function

  Sub SplitToIGridLine(ByVal line As iGRow, ByVal input As String, Optional ByVal Delimiter As String = vbTab)
    Dim max = line.Cells.Count - 1
    Dim out() = Split(input, Delimiter)
    ReDim Preserve out(max)
    For i As Integer = 0 To max
      line.Cells(i).Value = out(i)
    Next
  End Sub

  Sub Igrid_get(ByVal Grid As iGrid, ByRef FileContent As String, Optional ByVal LineDelim As String = vbNewLine, Optional ByVal ColDelim As String = vbTab)
    Dim max = Grid.Rows.Count - 1
    Dim out(max) As String
    For i As Integer = 0 To max
      out(i) = JoinIGridLine(Grid.Rows(i), ColDelim)
    Next
    FileContent = Join(out, LineDelim)
  End Sub
  Sub Igrid_put(ByVal Grid As iGrid, ByVal FileContent As String, Optional ByVal LineDelim As String = vbNewLine, Optional ByVal ColDelim As String = vbTab)
    Dim out() = Split(FileContent, LineDelim)
    Grid.Rows.Clear()
    Grid.Rows.Count = out.Length
    For i As Integer = 0 To out.Length - 1
      SplitToIGridLine(Grid.Rows(i), out(i), ColDelim)
    Next
  End Sub


End Module
