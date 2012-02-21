Public Class idx_default
  Implements IIndexList
  Dim skipNavIndexList As Boolean

  Public Event ItemClicked(ByVal lineNumber As Integer) Implements Core.IIndexList.ItemClicked


  Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
    If skipNavIndexList Then Exit Sub
    If ListBox1.SelectedIndex > -1 Then
      'Dim lineNr As Integer
      'Integer.TryParse(ListBox1.SelectedItem, lineNr)
      Dim gotoLine As Integer
      If Not Integer.TryParse(Split(ListBox1.SelectedItem, "|##|")(2), gotoLine) Then Exit Sub
      RaiseEvent ItemClicked(gotoLine)
    End If
  End Sub

  Public Sub buildList(ByVal Tab As Core.IDockContentForm) Implements Core.IIndexList.buildList
    
    Dim tab10 = vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab
    Dim lines() = Tab.getLines()

    Dim out(lines.Length) As String, c As Integer
    Dim tx, li, li2 As String, firstChar As Integer = 0
    For i = 0 To lines.Length - 1
      li = lines(i).Trim() + " "
      li2 = li.ToLower()
      If li2.StartsWith("function ") Or li2.StartsWith("sub ") Then
        out(c) = li + tab10 + "|##|" + "|##|" + i.ToString("000000") + "|##|" + firstChar.ToString("000000000") : c += 1

      ElseIf li2.StartsWith("//-->") Or li2.StartsWith("#-->") Or li2.StartsWith(";-->") Or li2.StartsWith("'-->") Then
        tx = li.Substring(5).Trim
        out(c) = tx + tab10 + "|##|" + "|##|" + i.ToString("000000") + "|##|" + firstChar.ToString("000000000") : c += 1

      ElseIf li2.Contains("function") AndAlso li2.Contains("//@@-") = False AndAlso li2.Contains("end function") = False AndAlso li2.Contains("//endfunction") = False Then
        tx = li.Replace("=", "").Replace("function", "").Replace("window.", "").Replace("{", "").Trim
        out(c) = tx + tab10 + "|##|" + "|##|" + i.ToString("000000") + "|##|" + firstChar.ToString("000000000") : c += 1
      End If
      firstChar += lines(i).Length + 1
    Next
    ReDim Preserve out(c - 1)

    With ListBox1
      skipNavIndexList = True
      .Hide()
      Dim selIndex = .SelectedIndex
      Dim topIndex = .TopIndex
      .Items.Clear()
      .Items.AddRange(out)

      Try 'Restore last selection, may fail
        .SelectedIndex = selIndex
        .TopIndex = topIndex
      Catch : End Try
      .Show()
      skipNavIndexList = False
    End With
  End Sub

  Public Sub onKeyHandler(ByVal key As System.Windows.Forms.KeyEventArgs) Implements Core.IIndexList.onKeyHandler
    On Error Resume Next
    If key.KeyCode = Keys.Up And key.Control Then
      ListBox1.SelectedIndex -= 1
    End If
    If key.KeyCode = Keys.Down And key.Control Then
      ListBox1.SelectedIndex += 1
    End If
  End Sub

  Public Sub onPositionChanged(ByVal lineNumber As Integer) Implements Core.IIndexList.onPositionChanged
    With ListBox1
      skipNavIndexList = True
      For i = .Items.Count - 1 To 0 Step -1
        Dim parts() = Split(.Items(i), "|##|")
        If Val(parts(2)) < lineNumber Then .SelectedIndex = i : Exit For
      Next
      skipNavIndexList = False
    End With
  End Sub
End Class
