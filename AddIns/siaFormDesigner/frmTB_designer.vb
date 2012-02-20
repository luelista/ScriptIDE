Imports System.Windows.Forms
Imports TenTec.Windows.iGridLib

Public Class frmTB_designer

  Dim tshelper As ToolStrip_DontEatClickEvent

  Dim helperLabels(5) As PictureBox
  Dim selectedControl As Control

  Sub createHelperLabels()
    For i = 0 To 3
      helperLabels(i) = New PictureBox
      With helperLabels(i)
        .Size = New Size(8, 8) : .BackColor = Color.DarkGray
        .BorderStyle = BorderStyle.FixedSingle
        AddHandler .MouseDown, AddressOf resizerLbl_MouseDown
        AddHandler .MouseUp, AddressOf resizerLbl_MouseUp
        SplitContainer1.Panel1.Controls.Add(helperLabels(i))
        .BringToFront()
      End With
    Next
    helperLabels(0).Tag = 0
    helperLabels(1).Tag = 1
    helperLabels(2).Tag = 2
    helperLabels(3).Tag = 3
  End Sub

  Sub showResizersForCtrl(ByVal ctrl As Control)
    Dim pos = SplitContainer1.Panel1.PointToClient(ctrl.Parent.PointToScreen(ctrl.Location))

    helperLabels(0).Left = pos.X - 8 : helperLabels(0).Top = pos.Y - 8 'HTTOPLEFT
    helperLabels(1).Left = pos.X + ctrl.Width : helperLabels(1).Top = pos.Y - 8 'HTTOPRIGHT
    helperLabels(2).Left = pos.X - 8 : helperLabels(2).Top = pos.Y + ctrl.Height 'HTBOTTOMLEFT
    helperLabels(3).Left = pos.X + ctrl.Width : helperLabels(3).Top = pos.Y + ctrl.Height 'HTBOTTOMRIGHT
    For i = 0 To 3 : helperLabels(i).BringToFront() : Next
  End Sub

  Function getResizerIntPos(ByVal num As Integer) As Point
    Dim pt = helperLabels(num).Location
    If num = 0 Or num = 1 Then pt.Y += 8
    If num = 0 Or num = 2 Then pt.X += 8
    Return pt
  End Function

  Function getResizerIntSize(ByVal lastChanged As Integer) As Rectangle
    Dim rect As Rectangle
    rect.X = helperLabels(0).Left + 8
    rect.Y = helperLabels(0).Top + 8
    rect.Width = helperLabels(3).Left - rect.X
    rect.Height = helperLabels(3).Top - rect.Y

    If lastChanged >= 0 And lastChanged < 4 Then
      Dim pt = getResizerIntPos(lastChanged)
      If lastChanged = 1 Then rect.Height -= (pt.Y - rect.Y)
      If lastChanged = 2 Then rect.Width -= (pt.X - rect.X)
      If lastChanged = 0 Or lastChanged = 2 Then rect.X = pt.X
      If lastChanged = 0 Or lastChanged = 1 Then rect.Y = pt.Y
      If lastChanged = 1 Or lastChanged = 3 Then rect.Width = pt.X - rect.X
      If lastChanged = 2 Or lastChanged = 3 Then rect.Height = pt.Y - rect.Y
    End If

    Return SplitContainer1.Panel1.RectangleToScreen(rect)
  End Function

  Private Sub resizerLbl_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Dim ctrl As Control = sender
    FormMoveTricky(ctrl.Handle)

    Dim bounds = getResizerIntSize(ctrl.Tag)
    selectedControl.Bounds = selectedControl.Parent.RectangleToClient(bounds)
    showResizersForCtrl(selectedControl)
    Dim row = GetRowForCtrl(selectedControl.Name)
    IGrid1.Cells(row, "left").Value = CStr(selectedControl.Left)
    IGrid1.Cells(row, "top").Value = CStr(selectedControl.Top)
    IGrid1.Cells(row, "width").Value = CStr(selectedControl.Width)
    IGrid1.Cells(row, "height").Value = CStr(selectedControl.Height)
  End Sub

  Private Sub resizerLbl_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
  End Sub

  Public Overrides Function GetPersistString() As String
    Return tbDesigner_ID
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    IDE.BeforeShowAddinWindow(tbDesigner_ID, Me)
    MyBase.Show()
  End Sub

  Private Sub frmTB_scriptSync_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    With IGrid1
      .Cols.Add("parent", "Parent")
      .Cols.Add("left", "xPos")
      .Cols.Add("top", "yPos")
      .Cols.Add("width", "Width")
      .Cols.Add("height", "Height")
      .Cols.Add("type", "Type")
      .Cols.Add("name", "Name")
      .Cols.Add("_", "Properties")
    End With
    netFrameworkDir = getNetFrameworkDir()
    tshelper = New ToolStrip_DontEatClickEvent(ToolStrip1)
    createHelperLabels()
  End Sub

  Function GetRowForCtrl(ByVal ctrlName As String) As Integer
    For i = 0 To IGrid1.Rows.Count - 1
      If IGrid1.Cells(i, "name").Value = ctrlName Then Return i
    Next
    Return -1
  End Function
  Function GetCtrlByName(ByVal ctrlName As String) As Control
    Dim ctrl() = Panel1.Controls.Find(ctrlName, True)
    If ctrl.Length = 0 Then Return Nothing
    Return ctrl(0)
  End Function

  Private Sub ToolStripButton1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ToolStripButton9.MouseDown, ToolStripButton8.MouseDown, ToolStripButton7.MouseDown, ToolStripButton6.MouseDown, ToolStripButton5.MouseDown, ToolStripButton4.MouseDown, ToolStripButton3.MouseDown, ToolStripButton2.MouseDown, ToolStripButton10.MouseDown, ToolStripButton1.MouseDown
    Dim dat As New DataObject
    dat.SetData("WinFormControl", sender.Text)
    ToolStrip1.DoDragDrop(dat, DragDropEffects.Copy)

  End Sub

  Function GetNextFreeControlName(ByVal ctrlType As String)
    If ctrlType.Contains(".") Then ctrlType = ctrlType.Substring(ctrlType.LastIndexOf(".") + 1)
    Dim idx = 0
    While GetRowForCtrl(ctrlType & "_" & idx) <> -1
      idx += 1
    End While
    Return ctrlType & "_" & idx
  End Function

  Private Sub Panel1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel1.DragDrop
    If e.Data.GetDataPresent("WinFormControl") Then
      e.Effect = DragDropEffects.Copy
      Dim ctrlType = e.Data.GetData("WinFormControl")

      Dim inst As Control = createControlInstance(ctrlType)
      Dim parentEl As Control = findControlRecursiv(Panel1, New Point(e.X, e.Y)) 'Panel1
      Dim pkt = parentEl.PointToClient(New Point(e.X, e.Y))
      inst.Location = pkt
      inst.Name = GetNextFreeControlName(ctrlType)
      inst.Text = inst.Name + " in " + parentEl.Name
      'inst.Width = 100 : inst.Height = 100
      parentEl.Controls.Add(inst)
      AddHandler inst.MouseMove, AddressOf designedEl_MouseMove
      AddHandler inst.MouseDown, AddressOf designedEl_MouseDown

      'iGrid Row erzeugen
      While String.IsNullOrEmpty(parentEl.Name)
        parentEl = parentEl.Parent
      End While

      Dim ir As iGRow
      If parentEl Is Panel1 Then
        ir = IGrid1.Rows.Add
      Else
        Dim pos = GetRowForCtrl(parentEl.Name) + 1
        Dim indent As Integer = Val(IGrid1.Cells(pos, "parent").Value)
        For i = pos To IGrid1.Rows.Count - 1
          Dim indent2 As Integer = Val(IGrid1.Cells(i, "parent").Value)
          If indent2 <= indent Then
            pos = i - 1
            Exit For
          End If
        Next
        ir = IGrid1.Rows.Insert(pos)
      End If

      ir.Cells("left").Value = CStr(inst.Left)
      ir.Cells("top").Value = CStr(inst.Top)
      ir.Cells("width").Value = CStr(inst.Width)
      ir.Cells("height").Value = CStr(inst.Height)
      ir.Cells("type").Value = ctrlType
      ir.Cells("name").Value = inst.Name
    End If
  End Sub
  Function findControlRecursiv(ByVal parentCtrl As Control, ByVal childPos As Point) As Control
    Dim child = parentCtrl.GetChildAtPoint(parentCtrl.PointToClient(childPos), GetChildAtPointSkip.None)
    If child Is Nothing Then Return parentCtrl

    If child.Controls.Count = 0 Then Return child
    Return findControlRecursiv(child, childPos)
  End Function

  Function createControlInstance(ByVal ctrlName As String) As Control
    Dim typ = findTypeExact(ctrlName)
    If typ Is Nothing Then Return Nothing
    If typ.IsSubclassOf(GetType(Control)) = False Then Return Nothing

    Dim ctrl = Activator.CreateInstance(typ)
    Return ctrl
  End Function

  Private Sub Panel1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Panel1.DragEnter
    If e.Data.GetDataPresent("WinFormControl") Then
      e.Effect = DragDropEffects.Copy
    End If
  End Sub

  Sub buildFormPreviewFromGrid()
    On Error Resume Next
    Panel1.Controls.Clear()

    Dim EL(22) As Control
    EL(0) = Me.Panel1
    For i = 0 To IGrid1.Rows.Count - 1
      If IGrid1.Cells(i, "parent").Value IsNot Nothing AndAlso IGrid1.Cells(i, "parent").Value.startswith("//") Then Continue For
      Dim typeName = IGrid1.Cells(i, "type").Value
      If String.IsNullOrEmpty(typeName) Then Continue For
      If typeName = "Form" Then
        labInfo2.Text = "FormSize: " & IGrid1.Cells(i, "width").Value & "x" & IGrid1.Cells(i, "height").Value
        Panel1.Width = IGrid1.Cells(i, "width").Value
        Panel1.Height = IGrid1.Cells(i, "height").Value

      Else
        Dim indent() = Split(IGrid1.Cells(i, "parent").Value, ".", 2)
        Dim indentLevel As Integer, indentPara As String = ""
        If Integer.TryParse(indent(0), indentLevel) = False Then indentLevel = 0
        If indent.Length = 2 Then indentPara = indent(1)

        Dim ctrl = createControlInstance(typeName)
        ctrl.Left = IGrid1.Cells(i, "left").Value : ctrl.Top = IGrid1.Cells(i, "top").Value
        ctrl.Width = IGrid1.Cells(i, "width").Value : ctrl.Height = IGrid1.Cells(i, "height").Value
        ctrl.Name = IGrid1.Cells(i, "name").Value : ctrl.Text = IGrid1.Cells(i, "name").Value
        AddHandler ctrl.MouseMove, AddressOf designedEl_MouseMove
        AddHandler ctrl.MouseDown, AddressOf designedEl_MouseDown
        setPropsForControl(ctrl, IGrid1.Cells(i, "_").Value)
        ' Panel1.Controls.Add(ctrl)
        Select Case indentPara.ToLower
          Case "panel1" : CType(EL(indentLevel), SplitContainer).Panel1.Controls.Add(ctrl)
          Case "panel2" : CType(EL(indentLevel), SplitContainer).Panel2.Controls.Add(ctrl)
          Case Else : EL(indentLevel).Controls.Add(ctrl)
        End Select
        EL(indentLevel + 1) = ctrl
      End If

    Next
  End Sub

  Sub setPropsForControl(ByVal ctrl As Control, ByVal props As String)
    On Error Resume Next
    Dim data = splitProps(props)
    For i = 0 To UBound(data)
      Dim splittedVar() = Split(data(i), "=", 2)
      Dim propName = Trim(splittedVar(0)), propCont = Trim(splittedVar(1))

      If IsNumeric(propCont) Then
        CallByName(ctrl, propName, CallType.Let, CInt(propCont))
      End If
      If propCont.StartsWith("""") And propCont.EndsWith("""") Then
        CallByName(ctrl, propName, CallType.Let, propCont.Substring(1, propCont.Length - 2))
      End If
      If propCont.StartsWith("zz.getImageCached(""", StringComparison.CurrentCultureIgnoreCase) Or _
       propCont.StartsWith("ResourceLoader.getImageCached(""", StringComparison.CurrentCultureIgnoreCase) Then
        Dim abPos = propCont.IndexOf("""")
        Dim url = propCont.Substring(abPos + 1, propCont.Length - abPos - 3)
        Dim img As Image = ResourceLoader.GetImageCached(url)

        CallByName(ctrl, propName, CallType.Let, img)
      End If
      If propCont.ToLower = "true" Then CallByName(ctrl, propName, CallType.Let, True)
      If propCont.ToLower = "false" Then CallByName(ctrl, propName, CallType.Let, False)
    Next
  End Sub


  Private Sub designedEl_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    FormMoveTricky(sender.Handle)
    Dim row = GetRowForCtrl(sender.name)
    IGrid1.Cells(row, "left").Value = CStr(sender.Left)
    IGrid1.Cells(row, "top").Value = CStr(sender.Top)
    setSelectedControl(sender)
    Dim rowIdx = GetRowForCtrl(sender.Name)
    IGrid1.SetCurRow(rowIdx)
  End Sub

  Private Sub designedEl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    
  End Sub

  Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
    If e.X > sender.width - 10 And e.Y > sender.height - 10 Then
      FormResizeTricky(sender.Handle, HitTestValues.HTBOTTOMRIGHT)
      labInfo2.Text = "FormSize: " + Panel1.Size.ToString
    End If
  End Sub

  Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

  End Sub

  Private Sub tsbOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOpen.Click
    fetchReferencesFromTab(IDE.getActiveTab())

    Dim content2 = getActiveFormcode()
    Dim winData() = Split(content2, vbNewLine, 2)

    labInfo3.Text = "FormName: " + winData(0)
    Igrid_put(IGrid1, winData(1))

    buildFormPreviewFromGrid()
  End Sub

  Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

  End Sub

  Private Sub PropertyGrid1_PropertyValueChanged(ByVal s As System.Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
    On Error Resume Next
    txtZoombox.Text = ""
    txtZoombox.AppendText("PropName:" + e.ChangedItem.PropertyDescriptor.Name + vbNewLine)
    txtZoombox.AppendText("ShouldSerializeValue:" + e.ChangedItem.PropertyDescriptor.ShouldSerializeValue(PropertyGrid1.SelectedObject).ToString + vbNewLine)
    txtZoombox.AppendText("ConvertedValue:" + e.ChangedItem.PropertyDescriptor.Converter.ConvertToString(e.ChangedItem.Value) + vbNewLine)
    txtZoombox.AppendText("Value:" + e.ChangedItem.Value.ToString + vbNewLine)

  End Sub

  Private Sub tsbWriteBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbWriteBack.Click
    Dim ig As iGrid = IGrid1
    ig.Rows.Clear()
    ig.Rows.Count = 100

    ig.Cells(0, "width").Value = CStr(Panel1.Width)
    ig.Cells(0, "height").Value = CStr(Panel1.Height)
    ig.Cells(0, "type").Value = "Form"
    Dim cnt As Integer = 1
    recWalkCtrls(ig, Me.Panel1, cnt, 0)
    ig.Rows.Count = cnt + 1
  End Sub

  Sub recWalkCtrls(ByVal ig As iGrid, ByVal parentCtrl As Control, ByRef index As Integer, ByVal indent As Integer)
    For Each ctrl As Control In parentCtrl.Controls
      ig.Cells(index, "parent").Value = CStr(indent)
      ig.Cells(index, "left").Value = CStr(ctrl.Left)
      ig.Cells(index, "top").Value = CStr(ctrl.Top)
      ig.Cells(index, "width").Value = CStr(ctrl.Width)
      ig.Cells(index, "height").Value = CStr(ctrl.Height)
      ig.Cells(index, "type").Value = ctrl.GetType().FullName
      ig.Cells(index, "name").Value = ctrl.Name
      index += 1

      If ctrl.HasChildren Then
        recWalkCtrls(ig, ctrl, index, indent + 1)
      End If
    Next
  End Sub

  Sub setSelectedControl(ByVal ctrl As Control)
    On Error Resume Next
    Static nonRec As Boolean = False
    If nonRec Then Exit Sub
    nonRec = True 'nicht recursiv aufrufen

    PropertyGrid1.SelectedObject = ctrl
    labInfo4.Text = "SelectedControl: " + ctrl.Name
    Dim MySelRect = ctrl.Parent.RectangleToScreen(ctrl.Bounds)
    ControlPaint.DrawReversibleFrame(MySelRect, Color.Black, FrameStyle.Thick)
    PropertyGrid1.SelectedObject = ctrl
    selectedControl = ctrl
    showResizersForCtrl(ctrl)

    nonRec = False
  End Sub

  Private Sub IGrid1_CurRowChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles IGrid1.CurRowChanged
    On Error Resume Next
    If IGrid1.CurRow Is Nothing OrElse String.IsNullOrEmpty(IGrid1.CurRow.Cells("name").Value) Then Exit Sub

    Dim ctrlName As String = IGrid1.CurRow.Cells("name").Value
    Dim ctrl = GetCtrlByName(ctrlName)
    setSelectedControl(ctrl)
  End Sub

  Private Sub tsbCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbCopy.Click
    Dim cont As String
    Igrid_get(IGrid1, cont)
    Clipboard.Clear()
    Clipboard.SetText(cont)
  End Sub
End Class