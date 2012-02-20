Public Class frmTB_solutionExplorer


  Public Overrides Function GetPersistString() As String
    Return tbSolution_ID
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    IDE.BeforeShowAddinWindow(Me.GetPersistString(), Me)
    MyBase.Show()
  End Sub

  Private Sub frmTB_rtfFilelist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    IO.Directory.CreateDirectory(IDE.GetSettingsFolder() + "projects\")
    Dim loadedPrjs() = Split(IDE.Glob.para("siaSolution__loadedProjects", ""), "|##|")
    For Each fileName In loadedPrjs
      If fileName.Trim = "" Then Continue For
      If fileName.StartsWith("!") Then
        Dim tvw = TreeView1.Nodes.Add("Prj_" + fileName.Substring(1), _
                                      IO.Path.GetFileName(fileName) + " (entladen)", _
                                      "UnloadedProject", "UnloadedProject")
      Else
        If IO.File.Exists(fileName) Then
          openProjectFile(fileName)
        End If
      End If
    Next
  End Sub


  '-->
  '--> Toolbar
  Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbNewProject.Click
    Using sfd As New SaveFileDialog
      sfd.Title = "Neues Projekt speichern unter ..."
      sfd.Filter = "ScriptIDE Project File (*.sip)|*.sip"
      sfd.InitialDirectory = getDefProjectFolder()
      If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
        Dim nod = TreeView1.Nodes.Add("Prj_" + sfd.FileName, "ProjektNew")
        Dim prj As New projectFile(sfd.FileName)
        prj.Para("ProjectName") = IO.Path.GetFileName(sfd.FileName)
        prj.putToTvn(nod)
        prj.saveFile()
        nod.Tag = prj
        saveLoadedPrjList()
      End If
    End Using
  End Sub
  Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbLoadProject.Click
    Using ofd As New OpenFileDialog
      ofd.Title = "Projekt öffnen ..."
      ofd.Filter = "ScriptIDE Project File (*.sip)|*.sip"
      ofd.InitialDirectory = getDefProjectFolder()
      If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then
        openProjectFile(ofd.FileName)
        saveLoadedPrjList()
      End If
    End Using
  End Sub
  Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbAddFile.Click
    If TreeView1.SelectedNode Is Nothing Then Exit Sub
    Dim url = IDE.getActiveTabFilespec()
    addItemToProject(url)
  End Sub

  Private Sub tsbExplorer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExplorer.Click
    If TreeView1.SelectedNode Is Nothing Then Exit Sub

    Select Case getNodeType(TreeView1.SelectedNode)
      Case "Project", "UnloadedProject"
        Dim fileSpec = TreeView1.SelectedNode.Name.Substring(4)
        Process.Start("explorer.exe", "/select," + fileSpec)

      Case "DefaultFile"
        Dim fileSpec As String = TreeView1.SelectedNode.Tag(1)
        If String.IsNullOrEmpty(fileSpec) Then Exit Sub
        If fileSpec.StartsWith("loc:/") Then
          Process.Start("explorer.exe", "/select," + fileSpec.Substring(5))
        ElseIf fileSpec.Substring(1, 2) = ":\" Then
          Process.Start("explorer.exe", "/select," + fileSpec)
        Else
          Dim prot = ProtocolService.GetURLProtocolHandler(fileSpec)
          If prot Is Nothing Then Exit Sub
          prot.NavigateFilelistTo(fileSpec)
        End If

      Case "Folder"
        Beep()
    End Select

  End Sub





  '-->
  '--> Helper Functions

  Sub onFileChanged(ByVal url As String)
    Dim nod() = TreeView1.Nodes.Find("File_" + url.ToLower, True)
    If nod.Length = 0 Then Exit Sub
    nod(0).EnsureVisible()
    TreeView1.SelectedNode = nod(0)
  End Sub

  Sub openProjectFile(ByVal fileName As String)
    Dim nod = TreeView1.Nodes.Add("Prj_" + fileName, "ProjektLd")
    nod.Tag = New projectFile(fileName)
    nod.Tag.putToTvn(nod)
  End Sub

  Function getNodeType(ByVal nod As TreeNode) As String
    If nod.Name.StartsWith("Prj_") Then
      If nod.Tag Is Nothing Then
        Return "UnloadedProject"
      Else
        Return "Project"
      End If
    Else
      Return nod.Tag(0)
    End If
  End Function

  Function getProjectByNode(ByVal nod As TreeNode) As projectFile
    Dim curNod As TreeNode = getRootNode(nod)
    If curNod.Tag IsNot Nothing AndAlso TypeOf curNod.Tag Is projectFile Then Return curNod.Tag
  End Function

  Function getRootNode(ByVal nod As TreeNode) As TreeNode
    Dim curNod As TreeNode = nod
    Do
      If curNod.Parent Is Nothing Then Return curNod
      curNod = curNod.Parent
    Loop
  End Function

  Sub addItemToProject(ByVal url As String)
    Dim tvn = TreeView1.SelectedNode
    If tvn Is Nothing Then Exit Sub
    If getNodeType(tvn) <> "Folder" And getNodeType(tvn) <> "Project" Then
      If tvn.Parent Is Nothing Then Exit Sub
      tvn = tvn.Parent
    End If
    Dim imageKey = siaSolution.RegisteredFileType.getImageIndexForFileExt(ImageList1, url)
    Dim newNod = tvn.Nodes.Add("File_" + url.ToLower, IO.Path.GetFileName(url), imageKey, imageKey)
    newNod.Tag = New String() {"DefaultFile", url, ""}
    saveActiveProject()
  End Sub

  Sub saveActiveProject()
    If TreeView1.SelectedNode Is Nothing Then Exit Sub
    Dim rootNod = getRootNode(TreeView1.SelectedNode)
    Dim prj As projectFile = rootNod.Tag
    prj.loadFromTvn(rootNod)
    prj.saveFile()
  End Sub

  Sub saveLoadedPrjList()
    Dim out(TreeView1.Nodes.Count) As String
    For i = 0 To TreeView1.Nodes.Count - 1
      out(i) = If(TreeView1.Nodes(i).Tag Is Nothing, "!", "") + TreeView1.Nodes(i).Name.Substring(4)
    Next
    IDE.Glob.para("siaSolution__loadedProjects") = Join(out, "|##|")
  End Sub





  '-->
  '--> tvwEvents

  Private Sub TreeView1_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView1.AfterLabelEdit
    If Not String.IsNullOrEmpty(e.Label) Then
      e.Node.Text = e.Label
      saveActiveProject()
    End If
  End Sub

  Private Sub TreeView1_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView1.BeforeLabelEdit
    Select Case getNodeType(TreeView1.SelectedNode)
      Case "Project", "Folder"
      Case Else
        e.CancelEdit = True
    End Select
  End Sub

  Private Sub TreeView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TreeView1.DragDrop
    If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", _
          True) Then

      'Get the TreeView raising the event (incase multiple on form)
      Dim selectedTreeview As TreeView = CType(sender, TreeView)

      'Get the TreeNode being dragged
      Dim dropNode As TreeNode = _
            CType(e.Data.GetData("System.Windows.Forms.TreeNode"),  _
            TreeNode)

      'The target node should be selected from the DragOver event
      Dim targetNode As TreeNode = selectedTreeview.SelectedNode

      'Remove the drop node from its current location
      dropNode.Remove()

      'If there is no targetNode add dropNode to the bottom of
      'the TreeView root nodes, otherwise add it to the end of
      'the dropNode child nodes
      If targetNode Is Nothing Then
        selectedTreeview.Nodes.Add(dropNode)
      Else
        targetNode.Nodes.Add(dropNode)
      End If

      'Ensure the newley created node is visible to
      'the user and select it
      dropNode.EnsureVisible()
      selectedTreeview.SelectedNode = dropNode

      saveActiveProject()

    ElseIf e.Data.GetDataPresent("siURLDrop", True) Then
      For Each fileName As String In e.Data.GetData("siURLDrop")
        addItemToProject(fileName)
      Next

    ElseIf e.Data.GetDataPresent("FileDrop", True) Or e.Data.GetDataPresent("siURLDrop", True) Then
      Dim files() As String
      If e.Data.GetDataPresent("siURLDrop", True) Then : files = e.Data.GetData("siURLDrop")
      ElseIf e.Data.GetDataPresent("FileDrop", True) Then : files = e.Data.GetData("FileDrop")
      End If

      For Each fileName As String In files
        If fileName.ToLower.EndsWith(".sip") Then
          openProjectFile(fileName)
          saveLoadedPrjList()
        Else
          addItemToProject(fileName)
        End If
      Next
    End If
  End Sub

  Private Sub TreeView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TreeView1.DragEnter
    If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
      e.Effect = DragDropEffects.Move
    ElseIf e.Data.GetDataPresent("FileDrop", True) Or e.Data.GetDataPresent("siURLDrop", True) Then
      e.Effect = DragDropEffects.Link
    Else
      e.Effect = DragDropEffects.None
    End If
  End Sub

  Private Sub TreeView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TreeView1.DragOver
    If e.Data.GetDataPresent("FileDrop", True) Or e.Data.GetDataPresent("siURLDrop", True) Then
      Dim selectedTreeview As TreeView = CType(sender, TreeView)

      Dim pt As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
      Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

      If Not (selectedTreeview.SelectedNode Is targetNode) Then
        selectedTreeview.SelectedNode = targetNode

        Dim typ = getNodeType(targetNode)
        If typ <> "Folder" And typ <> "Project" Then
          e.Effect = DragDropEffects.None
          Exit Sub
        End If

        e.Effect = DragDropEffects.Link
      End If


    ElseIf e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
      Dim selectedTreeview As TreeView = CType(sender, TreeView)

      Dim pt As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
      Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

      If Not (selectedTreeview.SelectedNode Is targetNode) Then
        selectedTreeview.SelectedNode = targetNode

        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        If getRootNode(dropNode) IsNot getRootNode(targetNode) Then
          e.Effect = DragDropEffects.None
          Exit Sub
        End If

        Do Until targetNode Is Nothing
          If targetNode Is dropNode Then
            e.Effect = DragDropEffects.None
            Exit Sub
          End If
          targetNode = targetNode.Parent
        Loop

        'Currently selected node is a suitable target
        e.Effect = DragDropEffects.Move
      End If
    End If
  End Sub

  Private Sub TreeView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles TreeView1.ItemDrag
    Select Case getNodeType(e.Item)
      Case "DefaultFile", "Folder"
        'Set the drag node and initiate the DragDrop 
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Select

  End Sub

  Private Sub TreeView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TreeView1.KeyUp
    If e.KeyCode = Keys.F2 Then
      Select Case getNodeType(TreeView1.SelectedNode)
        Case "Project", "Folder"
          TreeView1.SelectedNode.BeginEdit()
      End Select
    End If
  End Sub

  Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
    If e.Button = Windows.Forms.MouseButtons.Right Then
      TreeView1.SelectedNode = e.Node
      Select Case getNodeType(e.Node)
        Case "UnloadedProject" : cmProjectUnloaded.Show(sender, e.Location)
        Case "Project" : cmProject.Show(sender, e.Location)
        Case "DefaultFile" : cmFile.Show(sender, e.Location)
        Case "Folder" : cmFolder.Show(sender, e.Location)
      End Select
    End If
  End Sub

  Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
    Dim typ = getNodeType(e.Node)
    Select Case typ
      Case "DefaultFile"
        Dim Info() As String = e.Node.Tag
        IDE.NavigateFile(Info(1))
      Case "UnloadedProject"
        reloadActProject()
    End Select
  End Sub





  '-->
  '--> Projektfiles

  Private Sub UmbenennenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UmbenennenToolStripMenuItem.Click
    TreeView1.SelectedNode.BeginEdit()
  End Sub

  Private Sub ProjektEntladenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProjektEntladenToolStripMenuItem.Click
    Dim rootNod = getRootNode(TreeView1.SelectedNode)
    rootNod.Nodes.Clear()
    rootNod.Tag = Nothing : rootNod.Text = IO.Path.GetFileName(rootNod.Name) + " (entladen)"
    rootNod.ImageKey = "UnloadedProject" : rootNod.SelectedImageKey = "UnloadedProject"
    saveLoadedPrjList()
  End Sub

  Private Sub ProjektLadenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProjektLadenToolStripMenuItem.Click
    reloadActProject()
  End Sub
  Sub reloadActProject()
    Dim rootNod = getRootNode(TreeView1.SelectedNode)
    Dim fileName = rootNod.Name.Substring(4)
    rootNod.Tag = New projectFile(fileName)
    rootNod.Tag.putToTvn(rootNod)
    rootNod.Expand()
    saveLoadedPrjList()
  End Sub

  Private Sub ProjektdateiBearbeitenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProjektdateiBearbeitenToolStripMenuItem.Click
    Dim rootNod = getRootNode(TreeView1.SelectedNode)
    Dim fileName = rootNod.Name.Substring(4)
    IDE.NavigateFile("[TXT]" + fileName)
  End Sub
  Private Sub AusListeEntfernenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AusListeEntfernenToolStripMenuItem.Click
    TreeView1.SelectedNode.Remove()
    saveLoadedPrjList()
  End Sub




  '-->
  '--> Datei/Ordner löschen

  Private Sub ExcludeFromPrjToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcludeFromPrjToolStripMenuItem.Click
    TreeView1.SelectedNode.Remove()
    saveActiveProject()
  End Sub
  Private Sub Folder_LoeschenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Folder_LoeschenToolStripMenuItem.Click
    TreeView1.SelectedNode.Remove()
    saveActiveProject()
  End Sub

  Private Sub Folder_UmbenennenToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Folder_UmbenennenToolStripMenuItem1.Click
    TreeView1.SelectedNode.BeginEdit()
  End Sub



  '-->
  '--> Neuer Ordner

  Private Sub NeuerOrdnerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NeuerOrdnerToolStripMenuItem.Click
    createSubFolder()
  End Sub
  Private Sub NeuerOrdnerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Folder_NeuerOrdnerToolStripMenuItem1.Click
    createSubFolder()
  End Sub

  Sub createSubFolder()
    Dim tvn = TreeView1.SelectedNode.Nodes.Add("", "Neuer Ordner", "Folder", "Folder")
    tvn.Tag = New String() {"Folder", "", ""}
    tvn.BeginEdit()
  End Sub

End Class