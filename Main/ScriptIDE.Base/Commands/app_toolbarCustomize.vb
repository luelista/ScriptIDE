Imports TenTec.Windows.iGridLib

Public Class ToolbarService

  Public Const TBP_COMMAND = 0
  Public Const TBP_OWNER = 1
  Public Const TBP_TEXT = 2
  Public Const TBP_VIEW = 3
  Public Const TBP_KIND = 4
  Public Const TBP_ICON = 5
  Public Const TBP_minLength = 6

  Public Const TBUF_ENABLED = 1
  Public Const TBUF_VISIBLE = 2
  Public Const TBUF_TEXT = 4
  Public Const TBUF_CHECKURL = 8
  ' Public Const TBUF_CLEARADDINCACHE = 16

  Delegate Sub UpdateToolbarItemStateDelegate(ByVal command As String, ByVal mask As Integer, ByVal enabled As Boolean, ByVal visible As Boolean, ByVal text As String, ByVal forURL As String)
  Public Shared Event UpdateToolbarItemState As UpdateToolbarItemStateDelegate

  'Private Shared update_del As New UpdateToolbarItemStateDelegate(AddressOf onUpdateToolbarItem)

  Public Shared Sub UpdateToolbarItem(ByVal command As String, ByVal mask As Integer, ByVal enabled As Boolean, ByVal visible As Boolean, ByVal text As String)
    '  Return
    UpdateToolbarItem(command, mask, enabled, visible, text, Nothing)
  End Sub

  Public Shared Sub UpdateToolbarItem(ByVal command As String, ByVal mask As Integer, ByVal enabled As Boolean, ByVal visible As Boolean, ByVal text As String, ByVal url As String)
    If Not String.IsNullOrEmpty(url) Then mask = mask Or TBUF_CHECKURL : url = LCase(url)
    RaiseEvent UpdateToolbarItemState(command, mask, enabled, visible, text, url)
  End Sub

  'Private Shared Sub onUpdateToolbarItem(ByVal command As String, ByVal mask As Integer, ByVal enabled As Boolean, ByVal visible As Boolean, ByVal text As String)
  '  RaiseEvent UpdateToolbarItemState(command, mask, enabled, visible, text)
  'End Sub

  Public Shared Function ToolbarFolder() As String
    Return ParaService.ProfileFolder + "toolbars\"
  End Function

  Public Shared Sub toolbar_loadFromFile(ByVal fileName As String)
    toolbar_loadFromFile(fileName, "top", -1, -1)
  End Sub

  Public Shared Sub BuildMainMenu(ByVal mns As MenuStrip)
    mns.Items.Clear()
    Dim path2 = AddInTree.GetTreeNode("/Workspace/ToolbarCommands")
    Dim addedCommands As New List(Of String)
    BuildMainMenu(mns, "", path2, mns.Items, addedCommands)

    ' übergangsweise ...
    Dim mnuOther As ToolStripMenuItem
    For Each cod In path2.Codons
      If addedCommands.Contains(cod.Id) = False Then
        If mnuOther Is Nothing Then mnuOther = mns.Items.Add("Sonstige")
        Dim mnu As New AddinMenuItem(cod.Id, cod.AddIn, cod.Properties("text"), cod.Properties("icon"))
        mnuOther.DropDownItems.Add(mnu)
      End If
    Next
  End Sub

  Public Shared Sub BuildMainMenu(ByVal mns As MenuStrip, ByVal subPath As String, ByVal cmdPath As AddInTreeNode, ByVal parentNode As ToolStripItemCollection, ByVal addedCommands As List(Of String))
    'mns.Items.Add("Datei").Name = "mn_File"
    'mns.Items.Add("Ansicht").Name = "mn_View"

    Dim path = AddInTree.GetTreeNode("/Workspace/MainMenu" + subPath)
    path.EnsureSorted()
    For Each cod In path.Codons
      'Dim flags As String = cod.Properties("flags")
      'If flags.Contains("SKIPMENU") Then Continue For
      If cod.Name = "MenuSeparator" Then
        parentNode.Add("-")
        Continue For
      End If

      'Dim rootNode = GetRootMenuNode(mns, cod)
      If cod.Properties.Contains("refid") Then
        Dim id, text, icon As String
        id = cod.Properties("refid")
        Dim command As Codon = cmdPath.GetChildItem(id, False)
        If cod.Properties.Contains("text") Then text = cod.Properties("text") Else text = command.Properties("text")
        If cod.Properties.Contains("icon") Then icon = cod.Properties("icon") Else icon = command.Properties("icon")
        Dim mnu As New AddinMenuItem(id, command.AddIn, text, icon)
        parentNode.Add(mnu)
        addedCommands.Add(id)
      Else
        Dim mnu As New ToolStripMenuItem(cod.Properties("text"))
        parentNode.Add(mnu)
        If cod.HasSubItems Then
          BuildMainMenu(mns, subPath + "/" + cod.Id, cmdPath, mnu.DropDownItems, addedCommands)
        End If
      End If
    Next


  End Sub

  Private Shared Function GetRootMenuNode(ByVal mns As MenuStrip, ByVal cod As Codon) As ToolStripMenuItem
    Dim parts() = Split(cod.Id, ".", 2)
    Dim nam = parts(0)
    If parts.Length = 1 Then
      nam = "Tools"
    End If
    If nam = "Window" Then nam = "View"

    If mns.Items.ContainsKey("mn_" + nam) Then
      Return mns.Items("mn_" + nam)
    Else
      GetRootMenuNode = mns.Items.Add(nam)
      GetRootMenuNode.Name = "mn_" + nam
    End If

  End Function


  Public Shared Sub toolbar_loadFromFile(ByVal fileName As String, ByVal container As String, ByVal left As Integer, ByVal top As Integer)
    Dim fileSpec = ToolbarFolder() + fileName + ".txt"
    Dim LINES() = IO.File.ReadAllLines(fileSpec)

    Dim tb As ScriptWindowHelper.ScriptedToolstrip = cls_IDEHelper.Instance.CreateToolbar("." + fileName, container, left, top)
    If tb Is Nothing Then Stop
    tb.resetControls()
    For Each lin In LINES
      Dim p() = Split(lin, vbTab)
      If p.Length < TBP_minLength Then Continue For
      Select Case LCase(p(TBP_KIND))
        Case "button"
          tb.Items.Add(New AddinToolstripButton(p))

        Case "label"
          tb.Items.Add(New AddinToolstripLabel(p))

        Case "separator"
          tb.addLabel("", "-")

        Case Else
          TT.Write("TB " + fileName + " Error", "Invalid Kind: """ + p(TBP_KIND) + """", "info")
      End Select
    Next
  End Sub

  Public Shared Sub saveToolbarPositions()
    On Error Resume Next
    Dim out(-1) As String
    Dim ref = Workbench.Instance.ToolStripContainer1
    addToolstripsToList(ref.TopToolStripPanel, "t", out)
    addToolstripsToList(ref.BottomToolStripPanel, "b", out)
    addToolstripsToList(ref.LeftToolStripPanel, "l", out)
    addToolstripsToList(ref.RightToolStripPanel, "r", out)
    ParaService.Glob.para("toolbarPositions") = Join(out, "|##|")
  End Sub

  Public Shared Sub RestoreToolbars()
    On Error Resume Next
    Dim data() = Split(ParaService.Glob.para("toolbarPositions"), "|##|")
    For Each item In data
      Dim para() = Split(item, "|°|")
      If para.Length < 3 Then Continue For
      'TT.Write("Restore Toolbar ", para(0))
      If para(0).StartsWith(".") Then
        toolbar_loadFromFile(para(0).Substring(1), para(1), para(2), para(3))
      Else
        cls_IDEHelper.Instance.CreateToolbar(para(0), para(1), para(2), para(3))
      End If
    Next
  End Sub

  Public Shared Sub RestoreToolbarPositions()
    On Error Resume Next
    Dim data() = Split(ParaService.Glob.para("toolbarPositions"), "|##|")
    For Each item In data
      Dim para() = Split(item, "|°|")
      If para.Length < 3 Then Continue For
      'TT.Write("Restore ToolbarPos ", para(0))
      Dim tsRef = cls_IDEHelper.Instance.GetToolbar(para(0))

      If tsRef IsNot Nothing Then
        cls_IDEHelper.Instance.GetToolbarContainer(para(1)).Join(tsRef, para(2), para(3))
      End If
    Next
  End Sub

  Private Shared Sub addToolstripsToList(ByVal container As Control, ByVal containerName As String, ByRef list() As String)
    On Error Resume Next
    For Each ctrl As Control In container.Controls
      If TypeOf ctrl Is ToolStrip And ctrl.Name.StartsWith("userToolbar_") Then
        ReDim Preserve list(list.Length)
        list(list.Length - 1) = ctrl.Name.Substring(12) & "|°|" & containerName & "|°|" & ctrl.Left & "|°|" & ctrl.Top & "|°|" & ctrl.Width & "|°|" & ctrl.Height
      End If
    Next
  End Sub


  Public Shared Sub createNew(ByVal fileName As String)
    Dim fileSpec = ToolbarFolder() + fileName + ".txt"
    IO.File.WriteAllText(fileSpec, "")
  End Sub

  Public Shared Sub readToolbarData(ByVal ig As iGrid, ByVal fileName As String)
    Dim fileSpec = ToolbarFolder() + fileName + ".txt"
    Dim tx = IO.File.ReadAllText(fileSpec)
    Igrid_put(ig, tx)
  End Sub

  Public Shared Sub saveToolbarData(ByVal ig As iGrid, ByVal fileName As String)
    Dim fileSpec = ToolbarFolder() + fileName + ".txt"
    Dim tx As String
    Igrid_get(ig, tx)
    IO.File.WriteAllText(fileSpec, tx)
  End Sub


End Class
Class AddinMenuItem
  Inherits ToolStripMenuItem

  Private m_command, m_owner, m_text, m_icon As String
  Private m_addinInst As AddinInstance

  Public ReadOnly Property Addin() As AddinInstance
    Get
      'If m_addinInst Is Nothing Then
      '  m_addinInst = AddinInstance.GetAddinInstance(m_owner)
      'End If
      Return AddinInstance.GetAddinInstance(m_owner)
    End Get
  End Property

  Public Property IconURL() As String
    Get
      Return m_icon
    End Get
    Set(ByVal value As String)
      m_icon = value
      Me.Image = ResourceLoader.GetImageCached(value)
    End Set
  End Property

  Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
    Try
      Addin.ConnectRef.OnNavigate(NavigationKind.ToolbarCommand, "", m_command, "", "")
    Catch ex As Exception
      TT.Write("AddinMenuItem.OnClick", ex.ToString, "err")
    End Try
    MyBase.OnClick(e)
  End Sub

  Sub New(ByVal command As String, ByVal addin As AddinInstance, ByVal txt As String, ByVal iconUrl As String)
    'TT.Write("Addinmenuitem construct", command)
    m_command = command
    m_owner = addin.ID
    m_addinInst = addin
    Me.Text = txt
    Me.IconURL = iconUrl
    'Integer.TryParse(para(ToolbarService.TBP_VIEW), Me.DisplayStyle)
    AddHandler ToolbarService.UpdateToolbarItemState, AddressOf update_status
  End Sub

  Private Sub update_status(ByVal command As String, ByVal mask As Integer, ByVal enabled As Boolean, ByVal visible As Boolean, ByVal text As String, ByVal forURL As String)
    If command <> m_command Then Exit Sub
    If (mask And ToolbarService.TBUF_CHECKURL) <> 0 Then Exit Sub
    If (mask And ToolbarService.TBUF_ENABLED) <> 0 Then Me.Enabled = enabled
    If (mask And ToolbarService.TBUF_VISIBLE) <> 0 Then Me.Visible = visible
    If (mask And ToolbarService.TBUF_TEXT) <> 0 Then Me.Text = text
  End Sub

End Class
Class AddinToolstripButton
  Inherits ToolStripButton

  Private m_command, m_commandPara, m_owner, m_text, m_icon As String
  Private m_addinInst As AddinInstance
  Public FileURL As String

  Public ReadOnly Property Addin() As AddinInstance
    Get
      'If m_addinInst Is Nothing Then
      '  m_addinInst = AddinInstance.GetAddinInstance(m_owner)
      'End If
      Return AddinInstance.GetAddinInstance(m_owner)
    End Get
  End Property

  Public Property IconURL() As String
    Get
      Return m_icon
    End Get
    Set(ByVal value As String)
      m_icon = value
      Me.Image = ResourceLoader.GetImageCached(value)
    End Set
  End Property

  Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
    Try
      Addin.ConnectRef.OnNavigate(NavigationKind.ToolbarCommand, "", m_command, "", "")
    Catch ex As Exception
      TT.Write("AddinToolstripButton.OnClick", ex.ToString, "err")
    End Try
    MyBase.OnClick(e)
  End Sub

  Sub New(ByVal para() As String)
    'TT.Write("Addintsb construct", para(0))
    m_command = para(ToolbarService.TBP_COMMAND)
    m_owner = para(ToolbarService.TBP_OWNER)
    Me.Text = para(ToolbarService.TBP_TEXT)
    Me.IconURL = para(ToolbarService.TBP_ICON)
    Integer.TryParse(para(ToolbarService.TBP_VIEW), Me.DisplayStyle)
    AddHandler ToolbarService.UpdateToolbarItemState, AddressOf update_status
  End Sub
  Sub New(ByVal COMMAND As String, ByVal ADDIN As AddinInstance, ByVal TEXT As String, ByVal ICON As String)
    'TT.Write("Addintsb construct", para(0))
    m_command = COMMAND
    m_owner = ADDIN.ID : m_addinInst = ADDIN
    Me.Text = TEXT
    Me.IconURL = ICON
    Me.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
    AddHandler ToolbarService.UpdateToolbarItemState, AddressOf update_status
  End Sub
  Sub New(ByVal cod As Codon)
    'TT.Write("Addintsb construct", para(0))
    m_command = cod.Id
    m_owner = cod.AddIn.ID : m_addinInst = cod.AddIn
    Me.Text = cod.Properties("text")
    Me.IconURL = cod.Properties("icon")
    Me.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
    AddHandler ToolbarService.UpdateToolbarItemState, AddressOf update_status
  End Sub

  Private Sub update_status(ByVal command As String, ByVal mask As Integer, ByVal enabled As Boolean, ByVal visible As Boolean, ByVal text As String, ByVal forURL As String)
    If command <> m_command Then Exit Sub
    If ((mask And ToolbarService.TBUF_CHECKURL) <> 0) AndAlso FileURL <> forURL Then Exit Sub
    If (mask And ToolbarService.TBUF_ENABLED) <> 0 Then Me.Enabled = enabled
    If (mask And ToolbarService.TBUF_VISIBLE) <> 0 Then Me.Visible = visible
    If (mask And ToolbarService.TBUF_TEXT) <> 0 Then Me.Text = text
  End Sub

End Class
Class AddinToolstripLabel
  Inherits ToolStripLabel

  Private m_command, m_commandPara, m_owner, m_text, m_icon As String
  Private m_addinInst As AddinInstance
  Public FileURL As String

  Public ReadOnly Property Addin() As AddinInstance
    Get
      'If m_addinInst Is Nothing Then
      '  m_addinInst = AddinInstance.GetAddinInstance(m_owner)
      'End If
      Return AddinInstance.GetAddinInstance(m_owner)
    End Get
  End Property

  Public Property IconURL() As String
    Get
      Return m_icon
    End Get
    Set(ByVal value As String)
      m_icon = value
      If String.IsNullOrEmpty(value) Then
        Me.Image = Nothing
      Else
        Me.Image = ResourceLoader.GetImageCached(value)
      End If
    End Set
  End Property

  Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
    Try
      Addin.ConnectRef.OnNavigate(NavigationKind.ToolbarCommand, "", m_command, "", "")
    Catch ex As Exception
      TT.Write("AddinToolstripLabel.OnClick", ex.ToString, "err")
    End Try
    MyBase.OnClick(e)
  End Sub

  Sub New(ByVal para() As String)
    m_command = para(ToolbarService.TBP_COMMAND)
    m_owner = para(ToolbarService.TBP_OWNER)
    Me.Text = para(ToolbarService.TBP_TEXT)
    Me.IconURL = para(ToolbarService.TBP_ICON)
    Integer.TryParse(para(ToolbarService.TBP_VIEW), Me.DisplayStyle)
    AddHandler ToolbarService.UpdateToolbarItemState, AddressOf update_status
  End Sub
  Sub New(ByVal cod As Codon)
    m_command = cod.Id
    m_owner = cod.AddIn.ID : m_addinInst = cod.AddIn
    Me.Text = cod.Properties("text")
    Me.IconURL = cod.Properties("icon")
    Me.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
    AddHandler ToolbarService.UpdateToolbarItemState, AddressOf update_status
  End Sub

  Private Sub update_status(ByVal command As String, ByVal mask As Integer, ByVal enabled As Boolean, ByVal visible As Boolean, ByVal text As String, ByVal forURL As String)
    If command <> m_command Then Exit Sub
    If ((mask And ToolbarService.TBUF_CHECKURL) <> 0) AndAlso FileURL <> forURL Then Exit Sub
    If (mask And ToolbarService.TBUF_ENABLED) <> 0 Then Me.Enabled = enabled
    If (mask And ToolbarService.TBUF_VISIBLE) <> 0 Then Me.Visible = visible
    If (mask And ToolbarService.TBUF_TEXT) <> 0 Then Me.Text = text
  End Sub

End Class