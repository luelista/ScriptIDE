Imports TenTec.Windows.iGridLib

Public Class prop_Hotkeys
  Implements IPropertyPage

  Structure actionCmdItem
    Public Icon As Image, text, addin As String
    Public Sub New(ByVal cod As Codon, ByVal path As String)
      If Not String.IsNullOrEmpty(cod.Properties("icon")) Then
        Icon = ResourceLoader.GetImageCached(cod.Properties("icon"))
      End If
      text = cod.Properties("text") : addin = cod.Id ' "Command ||| " + cod.AddIn.ID + " ||| " + path
    End Sub
    Public Overrides Function ToString() As String
      Return text
    End Function
  End Structure
  Sub readCommandList(ByVal path As String)
    'ComboBox1.Items.Clear() ': imlToolbarIcons.Images.Clear()
    Dim node = AddInTree.GetTreeNode(path)
    'Dim lvi As ListViewItem
    'lvi = lvwTbCmds.Items.Add("-", "<Trennzeichen>", "")

    For Each cod In node.Codons
      ComboBox1.Items.Add(New actionCmdItem(cod, path + "/" + cod.Id))
    Next
    For Each childNode In node.ChildNodes
      For Each cod In childNode.Value.Codons
        ComboBox1.Items.Add(New actionCmdItem(cod, path + "/" + childNode.Key + "/" + cod.Id))
      Next
    Next
  End Sub

  Private Sub prop_Hotkeys_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    With igHotkeys
      .AutoResizeCols = False
      .Cols.Add("userKey", "Taste", 70)
      .Cols.Add("userMod", "Modifier", 90)
      .Cols.Add("userAction", "Aktion", 140)
      .Cols.Add("keyCode", "keyCode", 0).Visible = False
      .Cols.Add("mod", "mod", 0).Visible = False
      .Cols.Add("scanCode", "scanCode", 40)
      .Cols.Add("data3", "data3", 0) '.Visible = False
      .Cols.Add("data4", "Parameter", 0) '.Visible = False
      .ReadOnly = True
    End With

  End Sub


  Private Sub txtKeyCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKeyCode.GotFocus
    AppKeyHook.HandleAllRef = AddressOf txtKeyCode_KeyIntercepted
  End Sub

  Private Sub txtKeyCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKeyCode.LostFocus
    AppKeyHook.HandleAllRef = Nothing
  End Sub

  Public Sub txtKeyCode_KeyIntercepted(ByVal key As Keys, ByVal scanCode As Integer)
    txtKeyCode.Text = key.ToString
    Label1.Text = CInt(key).ToString
    'GroupBox1.Text = Now.ToLongTimeString

    'scanCode
    If chk_scanCode.Checked Then txtScanCode.Text = scanCode

    'Modifier uebernehmen
    chk_Control.Checked = KeyState.isKeyPressed(Keys.LControlKey) OrElse KeyState.isKeyPressed(Keys.RControlKey)
    chk_Alt.Checked = KeyState.isKeyPressed(Keys.LMenu) OrElse KeyState.isKeyPressed(Keys.RMenu)
    chk_Shift.Checked = KeyState.isKeyPressed(Keys.LShiftKey) OrElse KeyState.isKeyPressed(Keys.RShiftKey)
    chk_Win.Checked = KeyState.isKeyPressed(Keys.LWin) OrElse KeyState.isKeyPressed(Keys.RWin)
    applyData()
  End Sub

  Sub applyData()
    Dim ir = igHotkeys.CurRow
    If ir Is Nothing Then Exit Sub

    ir.Cells("userKey").Value = txtKeyCode.Text '+ " ||| " + Label1.Text
    ir.Cells("userMod").Value = getModifierString("", "STRG ", "SHIFT ", "ALT ", "WIN ", "")
    ' ir.Cells(2).Value = getActionData()
    ir.Cells("keyCode").Value = Label1.Text
    ir.Cells("mod").Value = getModifierString("°", "c", "s", "a", "w", "_") & txtScanCode.Text & "°"
    ir.Cells("scanCode").Value = txtScanCode.Text
  End Sub

  Private Sub chk_Win_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_Win.Click, chk_Shift.Click, chk_Control.Click, chk_Alt.Click
    applyData()
  End Sub

  Private Function getModifierString(ByVal splitter As String, ByVal ctrlTrue As String, ByVal shiftTrue As String, ByVal altTrue As String, ByVal winTrue As String, ByVal falseString As String)
    Return splitter + If(chk_Control.Checked, ctrlTrue, falseString) + splitter + If(chk_Shift.Checked, shiftTrue, falseString) + splitter + If(chk_Alt.Checked, altTrue, falseString) + splitter + If(chk_Win.Checked, winTrue, falseString) + splitter
  End Function


  Public Sub readProperties() Implements Core.IPropertyPage.readProperties
    readCommandList("/Workspace/ToolbarCommands")
    readCommandList("/Workspace/FileCommands")

    Dim fileSpec As String = ParaService.ProfileFolder + "hotkeys.txt"
    Dim igCont As String = IO.File.ReadAllText(fileSpec)
    'igCont = TwAjax.ReadFile("appbar", fileSpec)
    'If igCont = "" Then ' IO.File.Exists("C:\yPara\appBar\hotkeys.txt") Then
    '  fileSpec = "defaulthotkeys.txt"
    '  igCont = TwAjax.ReadFile("appbar", fileSpec)
    'End If
    'Label26.Text = "globale Hotkey-Verwaltung -- webFileSpec: " + fileSpec
    Igrid_put(igHotkeys, igCont)

    onEditHotkeyRow()
  End Sub

  Public Sub saveProperties() Implements Core.IPropertyPage.saveProperties
   
  End Sub


  Private Sub btnHotkeyNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHotkeyNew.Click
    On Error Resume Next
    Dim ir As iGRow
    If igHotkeys.CurRow IsNot Nothing Then ir = igHotkeys.Rows.Insert(igHotkeys.CurRow.Index) _
                                      Else ir = igHotkeys.Rows.Add()

    ir.Selected = True : ir.EnsureVisible()
  End Sub

  Private Sub ComboBox1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles ComboBox1.DrawItem
    e.DrawBackground()
    If e.Index = -1 Then Exit Sub
    Dim item As actionCmdItem = ComboBox1.Items(e.Index)
    If item.Icon IsNot Nothing Then e.Graphics.DrawImage(item.Icon, 1, e.Bounds.Y + 1, 16, 16)
    e.Graphics.DrawString(item.text, e.Font, New SolidBrush(e.ForeColor), 25, e.Bounds.Y + 3)
  End Sub



  Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles ComboBox1.SelectedIndexChanged, TextBox1.TextChanged
    Dim ir = igHotkeys.CurRow
    If ir Is Nothing Or ComboBox1.SelectedIndex = -1 Then Exit Sub

    Dim item As actionCmdItem = ComboBox1.Items(ComboBox1.SelectedIndex)
    ir.Cells("userAction").Value = item.text
    ir.Cells("data3").Value = item.addin

    ir.Cells("data4").Value = TextBox1.Text
  End Sub

  Private Sub igHotkeys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles igHotkeys.Click

  End Sub

  Sub onEditHotkeyRow()
    Dim ir = igHotkeys.CurRow
    If ir Is Nothing Then GroupBox1.Enabled = False : GroupBox2.Enabled = False : Exit Sub
    GroupBox1.Enabled = True : GroupBox2.Enabled = True


    'Stop
    Dim keyNr As String = ir.Cells("keyCode").Value
    'keyNr = keyNr.Substring(keyNr.IndexOf(" ||| ") + 5)
    Label1.Text = keyNr
    Dim vk As Keys = Val(keyNr)
    txtKeyCode.Text = vk.ToString
    'Stop
    Dim mods As String = If(ir.Cells("mod").Value, "")
    chk_Control.Checked = mods.Contains("°c°")
    chk_Shift.Checked = mods.Contains("°s°")
    chk_Alt.Checked = mods.Contains("°a°")
    chk_Win.Checked = mods.Contains("°w°")

    ComboBox1.Text = ir.Cells("userAction").Value
    TextBox1.Text = ir.Cells("data4").Value
  End Sub

  Private Sub igHotkeys_CurRowChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles igHotkeys.CurRowChanged
    onEditHotkeyRow()
  End Sub

  Private Sub igHotkeys_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles igHotkeys.KeyDown
    On Error Resume Next
    If e.KeyCode = Keys.Delete Then
      igHotkeys.Rows.RemoveAt(igHotkeys.CurRow.Index)
    End If
  End Sub

  Private Sub txtKeyCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKeyCode.TextChanged

  End Sub

  Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    Dim igCont As String
    Igrid_get(igHotkeys, igCont)
    Dim fileSpec As String = ParaService.ProfileFolder + "hotkeys.txt"
    IO.File.WriteAllText(fileSpec, igCont)
    'TwAjax.SaveFile("appbar", "hotkeys_" + getDIZ() + ".txt", igCont)

    AppKeyHook.compileHotkeyFile(Me.igHotkeys)
    ' --> Arrayfreundliches Format erstellen!
  End Sub
End Class
