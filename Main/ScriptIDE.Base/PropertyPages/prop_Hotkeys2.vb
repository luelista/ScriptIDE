Public Class prop_Hotkeys2
  Implements IPropertyPage

  Structure hotKeyCommand
    Public cmdID As String
    Public ValidArea As String
    Public KeyString As String
    Dim mods As String, keyCode As Integer
    Public IconURL As String
    Public para As String
    Public isTemplate As Boolean
    Public disabled As Boolean
    Public Sub New(ByVal ID As String, ByVal area As String, ByVal keyString As String, ByVal url As String, ByVal parameter As String, ByVal disable As String)
      cmdID = ID : ValidArea = area : Me.KeyString = keyString : IconURL = url : para = parameter
      Dim ctrl, shift, alt, win As Boolean
      parseModList(True, keyString, ctrl, shift, alt, win)
      keyString = "-" + keyString
      keyString = keyString.Substring(keyString.LastIndexOf("-") + 1)
      Try
        keyCode = [Enum].Parse(GetType(Keys), keyString, True)
      Catch ex As Exception
        keyCode = Keys.None
      End Try
      mods = getModifierString("°", ctrl, "c", shift, "s", alt, "a", win, "w", "_")
      If Not String.IsNullOrEmpty(disable) Then disabled = True
    End Sub
    Public Sub New(ByVal ID As String, ByVal area As String, ByVal mods As String, ByVal keyCode As String, ByVal url As String, ByVal parameter As String, ByVal disable As String)
      Dim ctrl, shift, alt, win As Boolean
      parseModList(False, mods, ctrl, shift, alt, win)
      KeyString = getModifierString("", ctrl, "CTRL-", shift, "SHIFT-", alt, "ALT-", win, "WIN-", "")
      KeyString += CType(keyCode, Keys).ToString()
      cmdID = ID : ValidArea = area : IconURL = url : para = parameter
      Me.mods = mods : Me.keyCode = keyCode
      If Not String.IsNullOrEmpty(disable) Then disabled = True
    End Sub
  End Structure

  Sub readCommandList()
    lvwTbCmds.Items.Clear() ' : imlToolbarIcons.Images.Clear()
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
          lvi.ImageKey = "none"
        End Try
      Else
        lvi.ImageKey = "none"
      End If
      lvi.Tag = cod
    Next

  End Sub


  Private Sub ListView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lvwTbCmds.ItemDrag
    Dim dat As New DataObject
    dat.SetText(e.Item.text)
    dat.SetData("ToolbarCommandData", e.Item.tag)
    lvwTbCmds.DoDragDrop(dat, DragDropEffects.Copy Or DragDropEffects.Link Or DragDropEffects.Scroll)
  End Sub

  Private Sub lvwTbCmds_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvwTbCmds.MouseDoubleClick
    If lvwTbCmds.SelectedItems.Count <> 1 Then Exit Sub
    If TreeView1.SelectedNode Is Nothing Then Exit Sub
    Dim lvi As ListViewItem = lvwTbCmds.SelectedItems(0)
    Dim cod As Codon = lvi.Tag
    Dim keyNode As TreeNode = TreeView1.SelectedNode
    If keyNode.Parent IsNot Nothing Then keyNode = keyNode.Parent

    Dim v As hotKeyCommand = keyNode.Tag
    v.cmdID = cod.Id
    v.ValidArea = "Global"
    v.IconURL = cod.Properties("icon")

    Dim tvn As TreeNode
    If keyNode Is TreeView1.SelectedNode Then
      tvn = keyNode.Nodes.Add(v.cmdID + " [" + v.ValidArea + "]")
    Else
      tvn = keyNode.Nodes.Insert(TreeView1.SelectedNode.Index, v.cmdID + " [" + v.ValidArea + "]")
    End If
    tvn.Tag = v : tvn.Checked = True
    tvn.Name = v.cmdID + "_" + v.ValidArea
    tvn.ImageKey = lvi.ImageKey : tvn.SelectedImageKey = tvn.ImageKey
  End Sub

  Public Shared Sub parseModList(ByVal userView As Boolean, ByVal keyString As String, ByRef ctrl As Boolean, ByRef shift As Boolean, ByRef alt As Boolean, ByRef win As Boolean)
    If String.IsNullOrEmpty(keyString) Then Return
    If userView Then
      ctrl = keyString.Contains("CTRL-")
      shift = keyString.Contains("SHIFT-")
      alt = keyString.Contains("ALT-")
      win = keyString.Contains("WIN-")
    Else
      ctrl = keyString.Contains("°c°")
      shift = keyString.Contains("°s°")
      alt = keyString.Contains("°a°")
      win = keyString.Contains("°w°")
    End If
  End Sub

  Function getKeyNode(ByVal keyString As String) As TreeNode
    If TreeView1.Nodes.ContainsKey(UCase(keyString)) Then
      Return TreeView1.Nodes(UCase(keyString))
    Else
      Dim tvn = TreeView1.Nodes.Add(UCase(keyString), keyString, "key", "key")
      Dim v As New hotKeyCommand("", "", keyString, "", "", "")
      v.isTemplate = True
      tvn.Tag = v
      Return tvn
    End If
  End Function

  Sub readHotkeyList()
    On Error Resume Next
    Dim cmds(), parts() As String
    If IO.File.Exists(ParaService.ProfileFolder + "hotkeys_compiled.txt") Then
      Dim data() As String
      data = IO.File.ReadAllLines(ParaService.ProfileFolder + "hotkeys_compiled.txt")
      ReDim Preserve data(256)

      For i = 0 To data.Length
        If Trim(data(i)) = "" Then Continue For
        cmds = Split(data(i), vbTab)
        For j = 0 To cmds.Length - 1
          parts = Split(cmds(j), "|^°|")
          If parts.Length < 5 Then Continue For

          Dim v As New hotKeyCommand(parts(3), parts(1), parts(0), i, "", parts(4), parts(2))

          Dim keyNode = getKeyNode(v.KeyString)
          Dim tvn = keyNode.Nodes.Add(UCase(v.ValidArea + "_" + v.cmdID), v.cmdID + " [" + v.ValidArea + "]", v.cmdID, v.cmdID)
          tvn.Tag = v : tvn.Checked = Not v.disabled
        Next
      Next
    End If

    Dim path = AddInTree.GetTreeNode("/Workspace/ToolbarCommands")
    For Each cod In path.Codons
      If cod.Properties.Contains("defkey") Then
        parts = Split(cod.Properties("defkey"), ",")
        Dim area, keyString As String
        If parts.Length = 2 Then
          area = parts(0) : keyString = Trim(parts(1))
        Else
          area = "MainWindow" : keyString = Trim(parts(0))
        End If
        Dim keyNode = getKeyNode(keyString)
        Dim id = UCase(area + "_" + cod.Id)
        If keyNode.Nodes.ContainsKey(id) = False Then
          Dim tvn = keyNode.Nodes.Add(id, cod.Id + " [" + area + "]", cod.Id, cod.Id)
          tvn.Tag = New hotKeyCommand(cod.Id, area, keyString, cod.Properties("icon"), "", "")
          tvn.Checked = True
        End If
      End If
    Next

  End Sub

  Sub saveHotkeys()

    Dim out(256) As String

    For Each keyNode As TreeNode In TreeView1.Nodes
      For Each itemNode As TreeNode In keyNode.Nodes
        Dim v As hotKeyCommand = itemNode.Tag

        out(v.keyCode) += v.mods + "|^°|" + v.ValidArea + "|^°|" + If(v.disabled, "DISABLE", "") + "|^°|" + v.cmdID + "|^°|" + v.para + vbTab
      Next
    Next

    IO.File.WriteAllLines(ParaService.ProfileFolder + "hotkeys_compiled.txt", out)
    AppKeyHook.readHotKeyList()

  End Sub

  Public Sub readProperties() Implements Core.IPropertyPage.readProperties
    TreeView1.ImageList = imlToolbarIcons
    imlToolbarIcons.Images.Clear()
    imlToolbarIcons.Images.Add("none", ResourceLoader.GetImageCached("http://mw.teamwiki.net/docs/img/win-icons/ieframe_1_31026-16.png"))
    imlToolbarIcons.Images.Add("key", ResourceLoader.GetImageCached("http://mw.teamwiki.net/docs/img/win-icons/charmap_111-16.png"))

    readCommandList()
    readHotkeyList()

    TreeView1.ExpandAll()
    Err.Clear()
  End Sub

  Public Sub saveProperties() Implements Core.IPropertyPage.saveProperties
    saveHotkeys()
  End Sub


  Private Sub prop_Hotkeys2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub
  'Dim v As New hotKeyCommand
  '  v.KeyString = ""
  '  v.KeyString += getModifierString("", "CTRL-", "SHIFT-", "ALT-", "WIN-", "")
  '  v.KeyString += txtKeyCode.Text
  '  v.mods = getModifierString("°", "c", "s", "a", "w", "_")
  '  v.keyCode = Label1.Text
  Private Sub btnAddKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddKey.Click
    Dim KeyString As String = ""
    KeyString += getModifierString("", "CTRL-", "SHIFT-", "ALT-", "WIN-", "")
    KeyString += txtKeyCode.Text
    Dim nod = getKeyNode(KeyString)
    nod.EnsureVisible()
    TreeView1.SelectedNode = nod
  End Sub

  Private Function getModifierString(ByVal splitter As String, ByVal ctrlTrue As String, ByVal shiftTrue As String, ByVal altTrue As String, ByVal winTrue As String, ByVal falseString As String)
    Return getModifierString(splitter, chk_Control.Checked, ctrlTrue, chk_Shift.Checked, shiftTrue, chk_Alt.Checked, altTrue, chk_Win.Checked, winTrue, falseString)
  End Function

  Public Shared Function getModifierString(ByVal splitter As String, ByVal ctrl As Boolean, ByVal ctrlTrue As String, ByVal shift As Boolean, ByVal shiftTrue As String, ByVal alt As Boolean, ByVal altTrue As String, ByVal win As Boolean, ByVal winTrue As String, ByVal falseString As String)
    Return splitter + If(ctrl, ctrlTrue, falseString) + splitter + If(shift, shiftTrue, falseString) + splitter + If(alt, altTrue, falseString) + splitter + If(win, winTrue, falseString) + splitter
  End Function


  Private Sub txtKeyCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKeyCode.GotFocus
    AppKeyHook.HandleAllRef = AddressOf txtKeyCode_KeyIntercepted
  End Sub

  Private Sub txtKeyCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKeyCode.LostFocus
    AppKeyHook.HandleAllRef = Nothing
  End Sub

  Public Sub txtKeyCode_KeyIntercepted(ByVal key As Keys, ByVal scanCode As Integer)
    If (key >= Keys.LShiftKey And key <= Keys.RMenu) Or key = Keys.LWin Or key = Keys.RWin Then Exit Sub


    txtKeyCode.Text = key.ToString
    Label1.Text = CInt(key).ToString
    'GroupBox1.Text = Now.ToLongTimeString

    'scanCode
    'If chk_scanCode.Checked Then txtScanCode.Text = scanCode

    'Modifier uebernehmen
    chk_Control.Checked = AppKeyHook.isCtrl 'KeyState.isKeyPressed(Keys.LControlKey) OrElse KeyState.isKeyPressed(Keys.RControlKey)
    chk_Alt.Checked = AppKeyHook.isAlt 'KeyState.isKeyPressed(Keys.LMenu) OrElse KeyState.isKeyPressed(Keys.RMenu)
    chk_Shift.Checked = AppKeyHook.isShift ' KeyState.isKeyPressed(Keys.LShiftKey) OrElse KeyState.isKeyPressed(Keys.RShiftKey)
    chk_Win.Checked = AppKeyHook.isWin ' KeyState.isKeyPressed(Keys.LWin) OrElse KeyState.isKeyPressed(Keys.RWin)
    ' applyData()
  End Sub


  Private Sub lvwTbCmds_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwTbCmds.SelectedIndexChanged

  End Sub

  Private Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterCheck
    Dim v As hotKeyCommand = e.Node.Tag
    v.disabled = Not e.Node.Checked
    e.Node.Tag = v
  End Sub

  Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

  End Sub

  Private Sub TreeView1_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView1.BeforeCheck
    Dim v As hotKeyCommand = e.Node.Tag
    If v.isTemplate Then e.Cancel = True
  End Sub

  Private Sub TreeView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TreeView1.DragDrop
    If e.Data.GetDataPresent("siCommand") Then
      Dim pt As Point = TreeView1.PointToClient(New Point(e.X, e.Y))
      Dim nod As TreeNode = TreeView1.GetNodeAt(pt)
      If nod Is Nothing Then e.Effect = DragDropEffects.None : Exit Sub
      Dim v As hotKeyCommand = nod.Tag

      Dim targetNode As TreeNode = nod
      If v.isTemplate = False Then targetNode = nod.Parent : v = targetNode.Tag

      Dim newNode As TreeNode, enabled As Boolean = True

      If e.Data.GetDataPresent("sourceTreeNode") AndAlso KeyState.isKeyPressed(Keys.LControlKey) = False AndAlso KeyState.isKeyPressed(Keys.RControlKey) = False Then
        e.Effect = DragDropEffects.Move
        Dim sourceNode As TreeNode = e.Data.GetData("sourceTreeNode")
        If sourceNode.Checked = False Then enabled = False
        sourceNode.Remove()
      Else
        e.Effect = DragDropEffects.Copy
      End If

      Dim data() As String = e.Data.GetData("siCommand")
      Dim v2 As New hotKeyCommand(data(0), data(1), v.mods, v.keyCode, "", data(2))
      If targetNode Is nod Then
        newNode = targetNode.Nodes.Add(v2.cmdID + " [" + v2.ValidArea + "]")
      Else
        newNode = targetNode.Nodes.Insert(nod.Index, v2.cmdID + " [" + v2.ValidArea + "]")
      End If
      newNode.Tag = v2 : newNode.Name = UCase(v2.ValidArea + "_" + v2.cmdID)
      newNode.ImageKey = v2.cmdID : newNode.SelectedImageKey = v2.cmdID
      newNode.Checked = enabled

    End If
  End Sub

  Private Sub TreeView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TreeView1.DragOver
    If e.Data.GetDataPresent("siCommand") Then
      Dim pt As Point = TreeView1.PointToClient(New Point(e.X, e.Y))
      Dim nod As TreeNode = TreeView1.GetNodeAt(pt)
      If nod Is Nothing Then e.Effect = DragDropEffects.None : Exit Sub
      If e.Data.GetDataPresent("sourceTreeNode") AndAlso KeyState.isKeyPressed(Keys.LControlKey) = False AndAlso KeyState.isKeyPressed(Keys.RControlKey) = False Then
        e.Effect = DragDropEffects.Move
      Else
        e.Effect = DragDropEffects.Copy
      End If
    End If
  End Sub

  Private Sub TreeView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles TreeView1.ItemDrag
    Dim dat As New DataObject()
    Dim v As hotKeyCommand = e.Item.tag
    If v.isTemplate Then
      dat.SetText(v.KeyString)
    Else
      dat.SetText(v.cmdID)
      dat.SetData("siCommand", New String() {v.cmdID, v.ValidArea, v.para})
      dat.SetData("sourceTreeNode", e.Item)
    End If
    TreeView1.DoDragDrop(dat, DragDropEffects.Copy Or DragDropEffects.Move Or DragDropEffects.Scroll)

  End Sub

  Private Sub TreeView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TreeView1.KeyDown

  End Sub

  Private Sub TreeView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TreeView1.KeyUp

  End Sub

  Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
    If e.Button = Windows.Forms.MouseButtons.Right Then
      Dim v As hotKeyCommand = e.Node.Tag
      tsm_Enabled.Enabled = Not v.isTemplate
      tsm_EditPara.Enabled = Not v.isTemplate
      tsm_ValidArea.Enabled = Not v.isTemplate

      ContextMenuStrip1.Tag = e.Node
      ContextMenuStrip1.Show(sender, e.Location)

    End If
  End Sub

  Private Sub tsm_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Delete.Click
    Dim nod As TreeNode = ContextMenuStrip1.Tag
    If nod.Nodes.Count > 0 Then
      If MsgBox("Möchten Sie wirklich diesen Hotkey einschließlich aller zugewiesenen Befehle löschen?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
    End If
    nod.Remove()
  End Sub

  Private Sub tsm_Enabled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Enabled.Click
    Dim nod As TreeNode = ContextMenuStrip1.Tag
    Dim v As hotKeyCommand = nod.Tag

  End Sub

  Private Sub tsm_EditPara_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_EditPara.Click

  End Sub
End Class
