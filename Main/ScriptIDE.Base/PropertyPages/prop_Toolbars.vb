Public Class prop_Toolbars
  Implements IPropertyPage

  Dim WithEvents igDragDrop1 As New iGDragDropManager

  Sub readCommandList()
    lvwTbCmds.Items.Clear() : imlToolbarIcons.Images.Clear()
    Dim path = AddInTree.GetTreeNode("/Workspace/ToolbarCommands")
    Dim lvi As ListViewItem
    lvi = lvwTbCmds.Items.Add("-", "<Trennzeichen>", "")

    For Each cod In path.Codons
      lvi = lvwTbCmds.Items.Add(cod.Id, cod.Id, "")
      If Not String.IsNullOrEmpty(cod.Properties("icon")) Then
        Try
          imlToolbarIcons.Images.Add(cod.Id, ResourceLoader.GetImageCached(cod.Properties("icon")))
          lvi.ImageKey = cod.Id
        Catch ex As Exception

        End Try
      End If
      lvi.Tag = cod
    Next

  End Sub

  Sub readToolbarList()
    cmbTbList.Items.Clear()
    Dim tbList() = cls_IDEHelper.Instance.GetToolbarList()
    For Each tb In tbList
      If Not tb.StartsWith("userToolbar_") Then Continue For
      Dim name = tb.Substring(12)
      cmbTbList.Items.Add(name)
    Next
    Dim files() = IO.Directory.GetFiles(ToolbarService.ToolbarFolder(), "*.txt", IO.SearchOption.TopDirectoryOnly)
    For Each file In files
      Dim fileName = "." + IO.Path.GetFileNameWithoutExtension(file)
      If Not cmbTbList.Items.Contains(fileName) Then
        cmbTbList.Items.Add(fileName)
      End If
    Next
  End Sub

  Private Sub ListView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lvwTbCmds.ItemDrag
    Dim dat As New DataObject
    dat.SetText(e.Item.text)
    dat.SetData("ToolbarCommandData", e.Item.tag)
    lvwTbCmds.DoDragDrop(dat, DragDropEffects.Copy Or DragDropEffects.Link Or DragDropEffects.Scroll)

  End Sub

  Function getActiveToolbarFilename() As String
    Dim fileName As String = cmbTbList.SelectedItem
    If String.IsNullOrEmpty(fileName) OrElse fileName.StartsWith(".") = False Then Return ""

    Return fileName.Substring(1)
  End Function

  Private Sub igDragDrop1_DragDropDone(ByVal MovedGridRow As TenTec.Windows.iGridLib.iGRow) Handles igDragDrop1.DragDropDone
    saveActiveToolbarData()
  End Sub

  Sub saveActiveToolbarData()
    Dim fileName As String = getActiveToolbarFilename()
    If fileName = "" Then Exit Sub

    ToolbarService.saveToolbarData(igToolbars, fileName)
    ToolbarService.toolbar_loadFromFile(fileName)
  End Sub

  Private Sub btnTB_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTB_new.Click
    Dim dlg As New frm_createToolbar
    If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
      'IO.File.WriteAllText(ParaService.SettingsFolder + "toolbars\" + dlg.TextBox1.Text + ".txt", "")
      ToolbarService.createNew(dlg.TextBox1.Text)
      ToolbarService.toolbar_loadFromFile(dlg.TextBox1.Text)

      'cmbTbList.Items.Add(dlg.TextBox1.Text)
      readToolbarList()
    End If
  End Sub

  Private Sub btnTB_rename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTB_rename.Click
    Dim fileName As String = getActiveToolbarFilename()
    If fileName = "" Then Exit Sub

    Dim dlg As New frm_createToolbar
    dlg.ComboBox1.Enabled = False : dlg.Text = "Toolbar umbenennen"
    dlg.TextBox1.Text = fileName
    If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
      Dim newFilename As String = dlg.TextBox1.Text

      'Dim tbCont As String
      Dim tb As Control = cls_IDEHelper.Instance.GetToolbar("." + fileName) ', tbCont)
      tb.Name = "userToolbar_." + newFilename
      'Dim tbLeft = tb.Left, tbTop = tb.Top
      'tb = Nothing
      'cls_IDEHelper.Instance.RemoveToolbar("." + fileName)

      Dim fileSpec = ToolbarService.ToolbarFolder() + fileName + ".txt"
      IO.File.Move(fileSpec, ToolbarService.ToolbarFolder() + newFilename + ".txt")

      'ToolbarService.toolbar_loadFromFile(fileName, tbCont, tbLeft, tbTop)
    End If
  End Sub

  Private Sub btnTB_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTB_delete.Click
    Dim fileName As String = cmbTbList.SelectedItem
    If String.IsNullOrEmpty(fileName) Then Return

    If MsgBox("Möchten Sie die Toolbar """ + fileName + """ wirklich löschen ?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub

    If fileName.StartsWith(".") Then
      Dim fileSpec = ToolbarService.ToolbarFolder() + fileName.Substring(1) + ".txt"
      IO.File.Move(fileSpec, IO.Path.ChangeExtension(fileSpec, ".bak"))
    End If

    cls_IDEHelper.Instance.RemoveToolbar(fileName)
  End Sub

  Private Sub chkTB_visible_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTB_visible.Click
    Dim fileName As String = cmbTbList.SelectedItem
    If String.IsNullOrEmpty(fileName) Then Return

    If chkTB_visible.Checked = True Then
      If fileName.StartsWith(".") Then
        ToolbarService.toolbar_loadFromFile(fileName.Substring(1))
      Else
        cls_IDEHelper.Instance.CreateToolbar(fileName)
      End If
    Else
      Dim tb = cls_IDEHelper.Instance.GetToolbar(fileName)
      If tb IsNot Nothing Then
        DirectCast(tb, Control).Visible = False
      End If
    End If
  End Sub

  Private Sub btnTB_removeLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTB_removeLine.Click
    On Error Resume Next
    Dim idx = igToolbars.CurRow.Index
    igToolbars.Rows.RemoveAt(idx)
    igToolbars.SetCurRow(idx)
    saveActiveToolbarData()
  End Sub

  Private Sub cmbTbList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTbList.SelectedIndexChanged
    Dim fileName As String = getActiveToolbarFilename()
    If fileName = "" Then
      labTB_isScriptedErr.Show() : labTB_isScriptedErr.BringToFront()
    Else
      labTB_isScriptedErr.Hide()
      ToolbarService.readToolbarData(igToolbars, fileName)
    End If
    Dim tb = cls_IDEHelper.Instance.GetToolbar(cmbTbList.SelectedItem)
    chkTB_visible.Checked = tb IsNot Nothing AndAlso CType(tb, Control).Visible
  End Sub

  Public Sub readProperties() Implements Core.IPropertyPage.readProperties
    readCommandList()
    readToolbarList()
    If cmbTbList.Items.Count > 0 Then cmbTbList.SelectedIndex = 0
  End Sub

  Public Sub saveProperties() Implements Core.IPropertyPage.saveProperties

  End Sub

  Private Sub propPag_Toolbars_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim ddc As New TenTec.Windows.iGridLib.iGDropDownList
    ddc.Items.Add(1, "Text")
    ddc.Items.Add(2, "Image")
    ddc.Items.Add(3, "ImageAndText")
    igToolbars.Cols("view").CellStyle.DropDownControl = ddc

    igDragDrop1.AllowMoveWithinOneGrid = True
    igDragDrop1.Manage(igToolbars, True, True)
  End Sub

  Private Sub igToolbars_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles igToolbars.Click

  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

  End Sub

  Private Sub igToolbars_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles igToolbars.KeyUp
    If e.KeyCode = Keys.S And e.Control Then
      saveActiveToolbarData()
    End If
  End Sub
End Class
