Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports TenTec.Windows.iGridLib

Public Class iGDragDropManager

  Public Event DragDropDone(ByVal MovedGridRow As iGRow)


#Region "Public Methods"

	Public Sub New()

		MyBase.New()

	End Sub

	Public Sub Manage(ByVal grid As iGrid, ByVal canDragFrom As Boolean, ByVal canDropTo As Boolean)

		' Implement start of dragging.
		If canDragFrom Then
			AddHandler grid.StartDragCell, AddressOf Me.grid_StartDragCell
		End If

		' Implement dropping.
		If canDropTo Then
			grid.AllowDrop = True
			AddHandler grid.DragDrop, AddressOf Me.grid_DragDrop
			AddHandler grid.DragOver, AddressOf Me.grid_DragOver
			AddHandler grid.DragLeave, AddressOf Me.grid_DragLeave
		End If

		' Implement destination indication.
		If canDropTo Then
			AddHandler grid.Paint, AddressOf Me.grid_Paint
		End If

	End Sub

#End Region

#Region "Fields"

	''' <summary>
	''' Stores the index of the row which to insert
	''' the rows being dragged before.
	''' </summary>
	Private fDstIndex As Integer

	''' <summary>
	''' The grid where to insert the rows being dragged.
	''' </summary>
	Private fDstGrid As iGrid

	''' <summary>
	''' Index of the row used to start dragging.
	''' </summary>
	Private fSrcIndex As Integer

	''' <summary>
	''' The grid which cells are being dragged.
	''' </summary>
	Private fSrcGrid As iGrid

	''' <summary>
	''' Indicates whether rows can be dropped between other rows,
	''' or can only be added to the end.
	''' </summary>
	Private fAllowInsert As Boolean = True

	''' <summary>
	''' Indicates whether rows can only be copied.
	''' </summary>
	Private fAllowCopy As Boolean = True

	''' <summary>
	''' Indicates whether rows can only be moved.
	''' </summary>
	Private fAllowMove As Boolean = True

	''' <summary>
	''' Indicates whether rows can be moved within a grid.
	''' </summary>
	Private fAllowMoveWithinOneGrid As Boolean

#End Region

#Region "Private Methods"

	''' <summary>
	''' Starts dragging.
	''' </summary>
	Private Sub grid_StartDragCell(ByVal sender As Object, ByVal e As iGStartDragCellEventArgs)

		Dim myGrid As iGrid = CType(sender, iGrid)

		' Start dragging.
		ResetDst()
		fSrcGrid = myGrid
		fSrcIndex = e.RowIndex
		myGrid.DoDragDrop(myGrid.SelectedRows, DragDropEffects.Move Or DragDropEffects.Copy)

	End Sub

	''' <summary>
	''' Checks whether a grid can accept the data being dragged.
	''' </summary>
	Private Sub grid_DragOver(ByVal sender As Object, ByVal e As DragEventArgs)
    If e.Data.GetDataPresent("ToolbarCommandData") Then

      ResetDst()

      Dim myGrid As iGrid = CType(sender, iGrid)
      If Not myGrid Is Nothing Then
        Dim myDstIndex As Integer
        Dim myMousePos As Point = New Point(e.X, e.Y)
        myMousePos = myGrid.PointToClient(myMousePos)

        Dim myDeltaScroll As Integer = myGrid.DefaultRow.Height \ 2
        Dim myCellsAreaBounds As Rectangle = myGrid.CellsAreaBounds
        If myMousePos.Y - myCellsAreaBounds.Top <= myDeltaScroll Then
          myGrid.VScrollBar.Value -= myGrid.VScrollBar.SmallChange
        ElseIf myCellsAreaBounds.Bottom - myMousePos.Y <= myDeltaScroll Then
          myGrid.VScrollBar.Value += myGrid.VScrollBar.SmallChange
        End If

        If fAllowInsert Then
          myDstIndex = GetDstIndexFromPoint(myGrid, myMousePos.X, myMousePos.Y)
        Else
          myDstIndex = myGrid.Rows.Count
        End If

        If myDstIndex >= 0 AndAlso (Not fSrcGrid Is myGrid OrElse fSrcGrid.SelectedRows.Count > 1 OrElse (myDstIndex <> fSrcIndex AndAlso myDstIndex <> fSrcIndex + 1)) Then
          fDstGrid = myGrid : fDstIndex = myDstIndex
          e.Effect = DragDropEffects.Copy
          InvalidateDst()
          Return
        End If
      End If

    ElseIf e.Data.GetDataPresent(GetType(iGSelectedRowsCollection)) AndAlso Not fSrcGrid Is Nothing Then
      ' Stop

      ResetDst()

      Dim myGrid As iGrid = CType(sender, iGrid)
      If Not myGrid Is Nothing AndAlso (Not fSrcGrid Is myGrid OrElse fAllowMoveWithinOneGrid) Then

        Dim myDstIndex As Integer
        Dim myMousePos As Point = New Point(e.X, e.Y)
        myMousePos = myGrid.PointToClient(myMousePos)

        ' Scroll the grid automatically if required.
        Dim myDeltaScroll As Integer = myGrid.DefaultRow.Height \ 2
        Dim myCellsAreaBounds As Rectangle = myGrid.CellsAreaBounds
        If myMousePos.Y - myCellsAreaBounds.Top <= myDeltaScroll Then
          myGrid.VScrollBar.Value -= myGrid.VScrollBar.SmallChange
        ElseIf myCellsAreaBounds.Bottom - myMousePos.Y <= myDeltaScroll Then
          myGrid.VScrollBar.Value += myGrid.VScrollBar.SmallChange
        End If


        If fAllowInsert Then
          ' Get the cell under the mouse pointer if any.
          myDstIndex = GetDstIndexFromPoint(myGrid, myMousePos.X, myMousePos.Y)
        Else
          myDstIndex = myGrid.Rows.Count
        End If

        If myDstIndex >= 0 AndAlso (Not fSrcGrid Is myGrid OrElse fSrcGrid.SelectedRows.Count > 1 OrElse (myDstIndex <> fSrcIndex AndAlso myDstIndex <> fSrcIndex + 1)) Then

          ' Enable the drop operation.
          fDstGrid = myGrid
          fDstIndex = myDstIndex
          If fAllowMove AndAlso fAllowCopy Then
            If Control.ModifierKeys = Keys.Control Then
              e.Effect = DragDropEffects.Copy
            Else
              e.Effect = DragDropEffects.Move
            End If
          ElseIf fAllowCopy Then
            e.Effect = DragDropEffects.Copy
          ElseIf fAllowMove Then
            e.Effect = DragDropEffects.Move
          End If

          ' Redraw the destination grid.
          InvalidateDst()

          Return

        End If

      End If

    End If

		' Disable the drop operation.
    e.Effect = DragDropEffects.None

	End Sub

	''' <summary>
	''' Gets the destination row index from the specified mouse 
	''' coordinates.
	''' </summary>
	Private Function GetDstIndexFromPoint(ByVal grid As iGrid, ByVal x As Integer, ByVal y As Integer) As Integer

		' Get the row under at the point.
		Dim myRowHeight, myRowY As Integer
    Dim myRow As iGRow = grid.Rows.FromY(y, myRowY, myRowHeight)

		' If the mouse is above a row, return its or the previous row's index.
		If Not myRow Is Nothing Then
			If y < myRowY + myRowHeight / 2 Then
				Return myRow.Index
			Else
				Return myRow.Index + 1
			End If
		End If

    
		If grid.CellsAreaBounds.Contains(x, y) Then
			Return grid.Rows.Count
		End If

		Return -1

	End Function

	''' <summary>
	''' Drop the rows being dragged.
	''' </summary>
	Private Sub grid_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs)
    If fDstIndex >= 0 AndAlso Not fDstGrid Is Nothing AndAlso e.Data.GetDataPresent("ToolbarCommandData") Then

      fDstGrid.PerformAction(iGActions.DeselectAllRows)
      Dim myRow As iGRow = fDstGrid.Rows.Insert(fDstIndex)

      Dim data As Codon = e.Data.GetData("ToolbarCommandData")
      If data Is Nothing Then
        myRow.Cells("kind").Value = "Separator"
      Else
        myRow.Cells("cmd").Value = data.Id
        myRow.Cells("owner").Value = data.AddIn.ID
        myRow.Cells("text").Value = data.Properties("text")
        myRow.Cells("view").Value = 2
        myRow.Cells("kind").Value = "button" 'data.Properties("kind")
        myRow.Cells("icon").Value = data.Properties("icon")
      End If

      myRow.EnsureVisible()
      fDstGrid.Focus()
      ResetDst()
      fSrcGrid = Nothing

      RaiseEvent DragDropDone(myRow)

    ElseIf fDstIndex >= 0 AndAlso Not fDstGrid Is Nothing AndAlso Not fSrcGrid Is Nothing Then
      'Stop

      'alte Bug-Schleuder - - dieser DragDropManager

      'Eigenbau für: Einzelne Zeile im gleichen IGrid verschieben
      '...openedTabs-Liste
      Dim MovedRow As iGRow = Nothing

      'TT.printLine(1, "dstIndex", fDstIndex)

      Dim myRow As Integer = fDstGrid.Rows.Insert(fDstIndex).Index
      'TT.printLine(2, "...myRow", myRow)
      Dim oldRow As Integer = fSrcGrid.CurRow.Index
      'TT.printLine(3, "oldRow", oldRow)
      'If oldRow > myRow Then oldRow += 1




      fDstGrid.Rows(myRow).BackColor = fSrcGrid.Rows(oldRow).BackColor
      fDstGrid.Rows(myRow).ForeColor = fSrcGrid.Rows(oldRow).ForeColor
      fDstGrid.Rows(myRow).Tag = fSrcGrid.Rows(oldRow).Tag

      For myColIndex As Integer = 0 To fDstGrid.Cols.Count - 1
        fDstGrid.Cells(myRow, myColIndex).DropDownControl = fSrcGrid.Cells(oldRow, myColIndex).DropDownControl
        fDstGrid.Cells(myRow, myColIndex).Style = fSrcGrid.Cells(oldRow, myColIndex).Style
        fDstGrid.Cells(myRow, myColIndex).Value = fSrcGrid.Cells(oldRow, myColIndex).Value
        fDstGrid.Cells(myRow, myColIndex).BackColor = fSrcGrid.Cells(oldRow, myColIndex).BackColor
        fDstGrid.Cells(myRow, myColIndex).ImageIndex = fSrcGrid.Cells(oldRow, myColIndex).ImageIndex
      Next

      fDstGrid.Rows(myRow).Selected = True
      fDstGrid.Rows(myRow).EnsureVisible()

      fSrcGrid.Rows.RemoveAt(oldRow)
      'TT.printLine(4, "...myRow", myRow)
      'TT.printLine(5, "oldRow", oldRow)
      If oldRow < myRow Then myRow -= 1
      'TT.printLine(6, "...myRow", myRow)
      'TT.printLine(7, "oldRow", oldRow)

      MovedRow = fDstGrid.Rows(myRow)
      'TT.printLine(8, "MovedRow.Index", MovedRow.Index)

      fDstGrid.Focus()
      ResetDst()
      fSrcGrid = Nothing

      RaiseEvent DragDropDone(MovedRow)


      Exit Sub

      'Dim myTag As String = "Just added"

      '' Add rows and copy cell values.

      'If Not fSrcGrid Is fDstGrid Then
      '  fDstGrid.PerformAction(iGActions.DeselectAllRows)
      'End If



      'For myIndex As Integer = fSrcGrid.SelectedRows.Count - 1 To 0 Step -1
      '  Dim oldRow = fSrcGrid.SelectedRows(myIndex)
      '  Dim myRow As iGRow = fDstGrid.Rows.Insert(fDstIndex)
      '  If fSrcGrid Is fDstGrid Then
      '    'myRow.Tag = myTag
      '  Else
      '    myRow.Selected = True
      '  End If

      '  MovedRow = myRow

      '  myRow.BackColor = oldRow.BackColor
      '  myRow.ForeColor = oldRow.ForeColor
      '  myRow.Tag = oldRow.Tag


      '  Dim mySrcRowIndex As Integer = oldRow.Index
      '  For myColIndex As Integer = 0 To fDstGrid.Cols.Count - 1
      '    myRow.Cells(myColIndex).DropDownControl = oldRow.Cells(myColIndex).DropDownControl
      '    myRow.Cells(myColIndex).Style = oldRow.Cells(myColIndex).Style
      '    myRow.Cells(myColIndex).Value = oldRow.Cells(myColIndex).Value
      '    myRow.Cells(myColIndex).BackColor = oldRow.Cells(myColIndex).BackColor
      '    myRow.Cells(myColIndex).ImageIndex = oldRow.Cells(myColIndex).ImageIndex

      '  Next
      'Next


      ''For myIndex As Integer = fSrcGrid.SelectedCells.Count - 1 To 0 Step -1
      ''  Dim myRow As iGRow = fDstGrid.Rows.Insert(fDstIndex)
      ''  If fSrcGrid Is fDstGrid Then
      ''    'myRow.Tag = myTag
      ''  Else
      ''    myRow.Selected = True
      ''  End If

      ''  MovedRow = myRow
      ''  myRow.BackColor = Color.White

      ''  Dim mySrcRowIndex As Integer = fSrcGrid.SelectedCells(myIndex).RowIndex
      ''  For myColIndex As Integer = 0 To fDstGrid.Cols.Count - 1
      ''    myRow.Cells(myColIndex).DropDownControl = fSrcGrid.Cells(mySrcRowIndex, myColIndex).DropDownControl
      ''    myRow.Cells(myColIndex).Style = fSrcGrid.Cells(mySrcRowIndex, myColIndex).Style
      ''    myRow.Cells(myColIndex).Value = fSrcGrid.Cells(mySrcRowIndex, myColIndex).Value
      ''    myRow.Cells(myColIndex).BackColor = fSrcGrid.Cells(mySrcRowIndex, myColIndex).BackColor

      ''  Next
      ''Next

      '' Ensure visible dropped rows.
      'If Not fSrcGrid Is fDstGrid Then
      '  MovedRow.EnsureVisible()
      'End If

      '' Delete source rows if it is needed.
      ''If e.Effect = DragDropEffects.Move Then
      ''  For Each selCol As iGCell In fSrcGrid.SelectedCells
      ''    fSrcGrid.Rows.RemoveAt(selCol.RowIndex)
      ''  Next
      ''End If
      'If e.Effect = DragDropEffects.Move Then
      '  For Each selRow As iGRow In fSrcGrid.SelectedRows
      '    fSrcGrid.Rows.RemoveAt(selRow.Index)
      '  Next
      'End If

      '' If rows were moved within a grid, select them.
      'If e.Effect = DragDropEffects.Move Then
      '  If fSrcGrid Is fDstGrid Then
      '    'For Each myCell As iGCell In fSrcGrid.Cells
      '    '  If myCell.Row.Tag Is myTag Then
      '    '    myCell.Row.Tag = Nothing
      '    '    myCell.Selected = True
      '    '    Exit For
      '    '  End If
      '    'Next
      '  End If

      'End If

      'fDstGrid.Focus()
      'ResetDst()
      'fSrcGrid = Nothing

      'RaiseEvent DragDropDone(MovedRow)

    End If

  End Sub

	''' <summary>
	''' Resets internal fields when dragging is cancelled.
	''' </summary>
	Private Sub grid_DragLeave(ByVal sender As Object, ByVal e As EventArgs)

		ResetDst()

	End Sub

	''' <summary>
	''' Resets the fields stoting the destination.
	''' </summary>
	Private Sub ResetDst()

		InvalidateDst()

		fDstIndex = -1
		fDstGrid = Nothing

	End Sub

	''' <summary>
	''' Causes the current destination position to redrawn.
	''' </summary>
	Private Sub InvalidateDst()

		If fDstIndex >= 0 AndAlso Not fDstGrid Is Nothing Then
			If fDstIndex < fDstGrid.Rows.Count Then
				fDstGrid.Invalidate(New Rectangle(0, fDstGrid.Rows(fDstIndex).Y - 2, fDstGrid.Width, 3))
			ElseIf fDstIndex > 0 Then
				fDstGrid.Invalidate(New Rectangle(0, fDstGrid.Rows(fDstIndex - 1).Y + fDstGrid.Rows(fDstIndex - 1).Height - 2, fDstGrid.Width, 3))
			Else
				fDstGrid.Invalidate(New Rectangle(0, fDstGrid.CellsAreaBounds.Y, fDstGrid.Width, 1))
			End If

		End If

	End Sub

	''' <summary>
	''' Draws the destination.
	''' </summary>
	Private Sub grid_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)

		If Not fAllowInsert Then
			Return
		End If

		If Not fAllowCopy AndAlso Not fAllowMove Then
			Return
		End If

		If fDstIndex < 0 OrElse Not fDstGrid Is sender Then
			Return
		End If

		Dim myCellsAreaBounds As Rectangle = fDstGrid.CellsAreaBounds

		Dim myY As Integer
		If fDstIndex < fDstGrid.Rows.Count Then
			myY = fDstGrid.Rows(fDstIndex).Y - 2
		ElseIf fDstIndex > 0 Then
			myY = fDstGrid.Rows(fDstIndex - 1).Y + fDstGrid.Rows(fDstIndex - 1).Height - 2
		Else
			myY = myCellsAreaBounds.Y - 2
		End If

		Dim myOldClip As Region = e.Graphics.Clip
		e.Graphics.SetClip(myCellsAreaBounds, CombineMode.Intersect)
		Try
			e.Graphics.FillRectangle(Brushes.SandyBrown, New Rectangle(0, myY, fDstGrid.Width, 3))
		Finally
			e.Graphics.Clip = myOldClip
		End Try

	End Sub

#End Region

#Region "Public Poperties"

	''' <summary>
	''' Gets or sets a value indicating whether rows can be dropped 
	''' between other rows, or can only be added to the end.
	''' </summary>
	Public Property AllowInsert() As Boolean
		Get
			Return fAllowInsert
		End Get
		Set(ByVal Value As Boolean)
			fAllowInsert = Value
		End Set
	End Property

	''' <summary>
	''' Gets or sets a value indicating whether rows can be copied.
	''' </summary>
	Public Property AllowCopy() As Boolean
		Get
			Return fAllowCopy
		End Get
		Set(ByVal Value As Boolean)
			fAllowCopy = Value
		End Set
	End Property

	''' <summary>
	''' Gets or sets a value indicating whether rows can be moved.
	''' </summary>
	Public Property AllowMove() As Boolean
		Get
			Return fAllowMove
		End Get
		Set(ByVal Value As Boolean)
			fAllowMove = Value
		End Set
	End Property

	''' <summary>
	''' Gets or sets a value indicating whether rows can be moved within a grid.
	''' </summary>
	Public Property AllowMoveWithinOneGrid() As Boolean
		Get
			Return fAllowMoveWithinOneGrid
		End Get
		Set(ByVal Value As Boolean)
			fAllowMoveWithinOneGrid = Value
		End Set
	End Property

#End Region

End Class
