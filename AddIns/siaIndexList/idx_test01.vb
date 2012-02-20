Public Class idx_test01
  Implements IIndexList
  Dim skipNavIndexList As Boolean

  Public Event ItemClicked(ByVal lineNumber As Integer) Implements Core.IIndexList.ItemClicked


  Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
    If skipNavIndexList Then Exit Sub
    If ListView1.SelectedItems.Count <> 1 Then Exit Sub
    RaiseEvent ItemClicked(ListView1.SelectedItems(0).Tag)
  End Sub

  Function stripVBKeywords(ByVal sourceLineNetto As String) As String
    sourceLineNetto = UCase(Trim(sourceLineNetto))
    sourceLineNetto = sourceLineNetto.Replace("COMEXPORT", "")
    sourceLineNetto = sourceLineNetto.Replace("PRIVATE", "")
    sourceLineNetto = sourceLineNetto.Replace("PROTECTED", "")
    sourceLineNetto = sourceLineNetto.Replace("FRIEND", "")
    sourceLineNetto = sourceLineNetto.Replace("PUBLIC", "")
    sourceLineNetto = sourceLineNetto.Replace("PARTIAL", "")
    sourceLineNetto = sourceLineNetto.Replace("SHADOWS", "")
    sourceLineNetto = sourceLineNetto.Replace("OVERLOADS", "")
    sourceLineNetto = sourceLineNetto.Replace("OVERRIDABLE", "")
    sourceLineNetto = sourceLineNetto.Replace("OVERRIDES", "")
    sourceLineNetto = sourceLineNetto.Replace("MUSTOVERRIDE", "")
    sourceLineNetto = sourceLineNetto.Replace("DEFAULT", "")
    sourceLineNetto = sourceLineNetto.Replace("SHARED", "")
    sourceLineNetto = sourceLineNetto.Replace("READONLY", "")
    sourceLineNetto = sourceLineNetto.Replace("WRITEONLY", "")
    Return Trim(sourceLineNetto)
  End Function

  Public Sub buildList(ByVal Tab As Core.IDockContentForm) Implements Core.IIndexList.buildList
    Dim tab10 = vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab + vbTab
    With ListView1
      Dim lines() = Tab.getLines()

      skipNavIndexList = True
      Dim selIndex = Me.selectedIndex
      .Hide()
      .Items.Clear()

      .Items.Add(Tab.getViewFilename, "title").Tag = 0
      Dim tx, li, li2 As String, firstChar As Integer = 0
      For i = 0 To lines.Length - 1
        li = lines(i).Trim() + " "
        li2 = stripVBKeywords(li)
        If li2.StartsWith("FUNCTION ") Then
          .Items.Add("#" & i, li, "func").Tag = i
        ElseIf li2.StartsWith("SUB ") Then
          .Items.Add("#" & i, li, "sub").Tag = i
        ElseIf li2.StartsWith("PROPERTY ") Then
          .Items.Add("#" & i, li, "prop").Tag = i
        ElseIf li2.StartsWith("CLASS ") Then
          .Items.Add("#" & i, li, "class").Tag = i
        ElseIf li2.StartsWith("ENUM ") Then
          .Items.Add("#" & i, li, "enum").Tag = i

        ElseIf li2.StartsWith("//-->") Or li2.StartsWith("#-->") Or li2.StartsWith(";-->") Or li2.StartsWith("'-->") Then
          tx = li.Substring(5).Trim
          .Items.Add("#" & i, tx, "").Tag = i

        End If
        firstChar += lines(i).Length + 1
      Next

      selectedIndex = selIndex

      .Show()
      skipNavIndexList = False
    End With
  End Sub

  Property selectedIndex() As Integer
    Get
      Return If(ListView1.SelectedIndices.Count = 0, 0, ListView1.SelectedIndices(0))
    End Get
    Set(ByVal value As Integer)
      On Error Resume Next
      ListView1.Items(value).Selected = True
      ListView1.Items(value).EnsureVisible()
    End Set
  End Property

  Public Sub onKeyHandler(ByVal key As System.Windows.Forms.KeyEventArgs) Implements Core.IIndexList.onKeyHandler
    On Error Resume Next
    If key.KeyCode = Keys.Up And key.Control Then
      Me.selectedIndex -= 1
    End If
    If key.KeyCode = Keys.Down And key.Control Then
      Me.selectedIndex += 1
    End If
  End Sub

  Public Sub onPositionChanged(ByVal lineNumber As Integer) Implements Core.IIndexList.onPositionChanged
    With ListView1
      skipNavIndexList = True
      For i = .Items.Count - 1 To 0 Step -1
        If Val(.Items(i).Tag) < lineNumber Then .Items(i).Selected = True : Exit For
      Next
      skipNavIndexList = False
    End With
  End Sub

  Private Sub idx_test01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Private Sub idx_test01_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    ListView1.Columns(0).Width = ListView1.Width - 20
  End Sub
End Class
