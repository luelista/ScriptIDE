Imports WeifenLuo.WinFormsUI.Docking
Imports System.Drawing

Public Class ctl_options
  Implements IPropertyPage

  Dim selType, selPath As String

  Private Sub ctl_options_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
    If e.Data.GetDataPresent("FileDrop") Then
      Dim files() As String = e.Data.GetData("FileDrop")
      If files(0).ToLower.EndsWith(".sis") Then
        If files(0).ToLower.StartsWith(IDE.GetSettingsFolder().ToLower + "skins") Then
        Else
          IO.File.Copy(files(0), IDE.GetSettingsFolder().ToLower + "skins\")

        End If
        ComboBox1.Text = IO.Path.GetFileNameWithoutExtension(files(0))
        readSkin(ComboBox1.Text)
      End If
    End If
  End Sub

  Private Sub ctl_options_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
    If e.Data.GetDataPresent("FileDrop") Then
      Dim files() As String = e.Data.GetData("FileDrop")
      If files(0).ToLower.EndsWith(".sis") Then
        e.Effect = DragDropEffects.Copy
      End If
    End If
  End Sub

  Function imlColorAdd(ByVal col As Color) As String
    Dim html As String = ColorTranslator.ToHtml(col)
    If ImageList1.Images.ContainsKey(html) = False Then
      Dim bm As New Bitmap(16, 16)
      Dim g As Graphics = Graphics.FromImage(bm)
      g.FillRectangle(New SolidBrush(col), 0, 0, 16, 16)
      g.Dispose()
      ImageList1.Images.Add(html, bm)
    End If
    Return html
  End Function

  Function addItem(ByVal parent As TreeNodeCollection, ByVal name As String, ByVal autoCheckType As Object) As TreeNodeCollection
    Dim nod = parent.Add(name)
    If autoCheckType IsNot Nothing Then
      If TypeOf autoCheckType Is Folder Then
        nod.ImageKey = "dirc" : nod.SelectedImageKey = "dirc"
      ElseIf TypeOf autoCheckType Is String Then
        nod.ImageKey = "string" : nod.SelectedImageKey = "string"
      ElseIf TypeOf autoCheckType Is Boolean Then
        If autoCheckType = True Then
          nod.ImageKey = "bool_true" : nod.SelectedImageKey = "bool_true"
        Else
          nod.ImageKey = "bool_false" : nod.SelectedImageKey = "bool_false"
        End If
      ElseIf TypeOf autoCheckType Is Color Then
        nod.ImageKey = imlColorAdd(autoCheckType) : nod.SelectedImageKey = imlColorAdd(autoCheckType)
      End If
    Else
      nod.ImageKey = "color" : nod.SelectedImageKey = "color"
    End If
    Return nod.Nodes
  End Function

  Class Folder
  End Class

  Private Sub ctl_options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim tnc(10) As TreeNodeCollection
    Dim isFolder As New Folder

    pnlEdit_Color.Top = 35
    pnlEdit_Gradient.Top = 35
    pnlEdit_String.Top = 35


    tnc(0) = TreeView1.Nodes
    tnc(0).Clear()

    tnc(1) = addItem(tnc(0), "Skin", isFolder)

    tnc(2) = addItem(tnc(1), "AutoHideStripSkin", isFolder)
    tnc(3) = addItem(tnc(2), "DockStripGradient", Nothing)
    tnc(3) = addItem(tnc(2), "TabGradient", Nothing)


    tnc(2) = addItem(tnc(1), "DockPaneStripSkin", isFolder)

    tnc(3) = addItem(tnc(2), "DocumentGradient", isFolder)
    tnc(4) = addItem(tnc(3), "DockStripGradient", Nothing)
    tnc(4) = addItem(tnc(3), "ActiveTabGradient", Nothing)
    tnc(4) = addItem(tnc(3), "InactiveTabGradient", Nothing)

    tnc(3) = addItem(tnc(2), "ToolWindowGradient", isFolder)
    tnc(4) = addItem(tnc(3), "DockStripGradient", Nothing)
    tnc(4) = addItem(tnc(3), "ActiveCaptionGradient", Nothing)
    tnc(4) = addItem(tnc(3), "InactiveCaptionGradient", Nothing)
    tnc(4) = addItem(tnc(3), "ActiveTabGradient", Nothing)
    tnc(4) = addItem(tnc(3), "InactiveTabGradient", Nothing)


    Dim node = AddInTree.GetTreeNode("/OptionsDialog/SkinObjects")
    For Each cod As Codon In node.Codons
      tnc(1) = addItem(tnc(0), cod.Id, isFolder)
      Dim skinobj = getSkinObjectByName(cod.Id, cod.Properties("addinname"))
      Dim skintyp = skinobj.GetType()
      For Each prop In skintyp.GetProperties()
        Dim val As Object = prop.GetValue(skinobj, Nothing)
        tnc(2) = addItem(tnc(1), prop.Name, val)

      Next
    Next


    listSkins()
    ComboBox1.Text = IDE.Glob.para("siaSkinnable__lastselskin")
  End Sub
  Sub listSkins()
    ComboBox1.Items.Clear()
    For Each fileSpec In IO.Directory.GetFiles(IDE.GetSettingsFolder() + "skins\", "*.sis")
      ComboBox1.Items.Add(IO.Path.GetFileNameWithoutExtension(fileSpec))
    Next
  End Sub



  Sub saveActiveSkin()
    Dim sb As New System.Text.StringBuilder
    recWalkNodes(TreeView1.Nodes, sb)
    IO.File.WriteAllText(IDE.GetSettingsFolder() + "skins\" + ComboBox1.Text + ".sis", sb.ToString)
    Dim srvName = ComboBox1.Text + ".sis"
    If srvName.StartsWith(IDE.DIZ + "_") = False Then srvName = IDE.DIZ + "_" + srvName
    TwAjax.SaveFile("scriptide", "skins/" + srvName, sb.ToString)
    listSkins()
  End Sub

  Sub recWalkNodes(ByVal tnc As TreeNodeCollection, ByVal sb As System.Text.StringBuilder)
    On Error Resume Next
    For Each nod As TreeNode In tnc
      Dim obj = getGradientObjectByName(nod.FullPath)
      sb.AppendLine("[" + nod.FullPath + "]")
      If TypeOf obj Is String Then
        sb.AppendLine("Data=" & obj)
      ElseIf TypeOf obj Is Boolean Then
        sb.AppendLine("BoolValue=" & If(obj = True, "TRUE", "FALSE"))
      Else
        sb.AppendLine("LinearGradientMode=" & obj.LinearGradientMode)
        sb.AppendLine("StartColor=" & ColorTranslator.ToHtml(obj.StartColor))
        sb.AppendLine("EndColor=" & ColorTranslator.ToHtml(obj.EndColor))
        sb.AppendLine("TextColor=" & ColorTranslator.ToHtml(obj.TextColor))
        sb.AppendLine("ColorValue=" & ColorTranslator.ToHtml(obj))
      End If
      recWalkNodes(nod.Nodes, sb)
    Next
  End Sub

  Sub showPreviewPanel(ByVal pnlName As String)
    pnlEdit_Color.Visible = pnlName = "Color"
    pnlEdit_Gradient.Visible = pnlName = "Gradient"
    pnlEdit_String.Visible = pnlName = "String"

  End Sub

  Sub readDataFromPath(ByVal nod As TreeNode)
    Dim obj = getGradientObjectByName(selPath)

    If TypeOf obj Is TabGradient Then
      selType = "TabGradient"
      showPreviewPanel("Gradient")
      Dim grad As TabGradient = CType(obj, TabGradient)
      cmbGradientStyle.SelectedIndex = grad.LinearGradientMode
      txtGradient1.Text = ColorTranslator.ToHtml(grad.StartColor)
      txtGradient2.Text = ColorTranslator.ToHtml(grad.EndColor)
      txtGradientText.Text = ColorTranslator.ToHtml(grad.TextColor)

    ElseIf TypeOf obj Is DockPanelGradient Then
      selType = "DockPanelGradient"
      showPreviewPanel("Gradient")
      Dim grad As DockPanelGradient = CType(obj, DockPanelGradient)
      cmbGradientStyle.SelectedIndex = grad.LinearGradientMode
      txtGradient1.Text = ColorTranslator.ToHtml(grad.StartColor)
      txtGradient2.Text = ColorTranslator.ToHtml(grad.EndColor)
      txtGradientText.Enabled = False

    ElseIf TypeOf obj Is Color Then
      selType = "Color"
      showPreviewPanel("Color")
      txtColor.Text = ColorTranslator.ToHtml(obj)

    ElseIf TypeOf obj Is String Then
      selType = "String"
      showPreviewPanel("String")
      TextBox4.Text = obj

    ElseIf TypeOf obj Is Boolean Then
      showPreviewPanel("-hide-")

      setGradientObjectByName(selPath, Not obj)
      If obj Then
        nod.ImageKey = "bool_false" : nod.SelectedImageKey = "bool_false"
      Else
        nod.ImageKey = "bool_true" : nod.SelectedImageKey = "bool_true"
      End If

    End If
  End Sub

  Sub writeDataFromPreview()
    If selType = "TabGradient" Then
      Dim obj = getGradientObjectByName(selPath)
      Dim grad As TabGradient = CType(obj, TabGradient)
      grad.LinearGradientMode = cmbGradientStyle.SelectedIndex
      grad.StartColor = ColorTranslator.FromHtml(txtGradient1.Text)
      grad.EndColor = ColorTranslator.FromHtml(txtGradient2.Text)
      grad.TextColor = ColorTranslator.FromHtml(txtGradientText.Text)

    ElseIf selType = "DockPanelGradient" Then
      Dim obj = getGradientObjectByName(selPath)
      obj.LinearGradientMode = cmbGradientStyle.SelectedIndex
      obj.StartColor = ColorTranslator.FromHtml(txtGradient1.Text)
      obj.EndColor = ColorTranslator.FromHtml(txtGradient2.Text)

    ElseIf selType = "Color" Then
      setGradientObjectByName(selPath, ColorTranslator.FromHtml(txtColor.Text))

    ElseIf selType = "String" Then
      setGradientObjectByName(selPath, TextBox4.Text)

    End If
  End Sub

  Private Sub TreeView1_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterCollapse
    If e.Node.Nodes.Count > 0 Then e.Node.ImageKey = "dirc" : e.Node.SelectedImageKey = "dirc"
  End Sub

  Private Sub TreeView1_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterExpand
    If e.Node.Nodes.Count > 0 Then e.Node.ImageKey = "diro" : e.Node.SelectedImageKey = "diro"
  End Sub

  Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
    If e.Node.Nodes.Count > 0 Then Exit Sub

    selPath = e.Node.FullPath
    
    readDataFromPath(e.Node)
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    saveActiveSkin()
  End Sub

  Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
    IDE.Glob.para("siaSkinnable__lastselskin") = ComboBox1.Text

    readSkin(ComboBox1.Text)
  End Sub

  Public Sub readProperties() Implements IPropertyPage.readProperties

  End Sub

  Public Sub saveProperties() Implements IPropertyPage.saveProperties
    If frmDownload IsNot Nothing AndAlso frmDownload.IsDisposed = False Then
      frmDownload.Close()
      frmDownload = Nothing
    End If

  End Sub

  Dim frmDownload As frm_downloadSkin
  Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
    If frmDownload Is Nothing OrElse frmDownload.IsDisposed Then
      frmDownload = New frm_downloadSkin
    End If
    frmDownload.Show()
    frmDownload.Activate()
  End Sub

  Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
    Process.Start("explorer.exe", "/e," + IDE.GetSettingsFolder + "skins")
  End Sub

  Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
    TreeView1.ExpandAll()
  End Sub

  Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click, Button4.Click, Button3.Click, Button2.Click
    selectColor(Me.Controls.Find(sender.tag, True)(0))
  End Sub

  Sub selectColor(ByVal txt As TextBox)
    On Error Resume Next
    ColorDialog1.Color = ColorTranslator.FromHtml(txt.Text)
    If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
      txt.Text = ColorTranslator.ToHtml(ColorDialog1.Color)
    End If
  End Sub

  Private Sub txtColor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtColor.TextChanged
    picPreview2.Refresh()
  End Sub

  Private Sub txtGradientText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles txtGradientText.TextChanged, txtGradient2.TextChanged, txtGradient1.TextChanged, cmbGradientStyle.SelectedIndexChanged
    picPreview1.Refresh()
  End Sub

  Private Sub picPreview1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picPreview1.Paint
    On Error Resume Next
    Dim rct As New Rectangle(Point.Empty, picPreview1.Size)
    Dim brs As New Drawing2D.LinearGradientBrush(rct, ColorTranslator.FromHtml(txtGradient1.Text), ColorTranslator.FromHtml(txtGradient2.Text), cmbGradientStyle.SelectedIndex)
    e.Graphics.FillRectangle(brs, rct)
    e.Graphics.DrawString("Vorschau", Me.Font, New SolidBrush(ColorTranslator.FromHtml(txtGradientText.Text)), 50, 15)
  End Sub

  Private Sub picPreview2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles picPreview2.Paint
    On Error Resume Next
    Dim rct As New Rectangle(Point.Empty, picPreview1.Size)
    Dim brs As New SolidBrush(ColorTranslator.FromHtml(txtColor.Text))
    e.Graphics.FillRectangle(brs, rct)
    e.Graphics.DrawString("Vorschau", Me.Font, Brushes.Black, 50, 15)
  End Sub

  Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

  End Sub

  Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
    writeDataFromPreview()
  End Sub
End Class
