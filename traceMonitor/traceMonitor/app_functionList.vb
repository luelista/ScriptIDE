Imports EnvDTE

Module app_functionList
  Dim oWin As EnvDTE.Window
  Dim oPrjItem As EnvDTE.ProjectItem

  Sub highlightSolutionExplorer(ByVal objDocument As EnvDTE.Document)
    Debug.Print("--> highlightSolutionExplorer")
  End Sub

  Sub refreshSolutionExplorer()
    ''AA_frmExplorer.TreeView1.Nodes.Clear()
    ''For Each pro In IDE.Solution.Projects
    ''buildProjectTree(pro, AA_frmExplorer.TreeView1)
    ''Next

  End Sub



  Sub buildProjectTree(ByVal p As EnvDTE.Project, ByVal tv As TreeView)
    On Error Resume Next
    Dim node_prj, node_item, node_el, node_subEl As TreeNode
    Dim kPrj As String = "prj" + p.UniqueName
    node_prj = tv.Nodes.Add(kPrj, p.Name, "prj")

    For Each prjItem As EnvDTE.ProjectItem In p.ProjectItems
      Dim img As String = "x"
      If prjItem.Name.EndsWith(".vb") Then img = "form"
      node_item = node_prj.Nodes.Add(kPrj + prjItem.Name, prjItem.Name, "module")
      node_item.Tag = prjItem
    Next
    tv.ExpandAll()
  End Sub


  'Sub createFunctionList(ByVal prjItem As Object, ByVal frm As AA_frmExplorer) ' EnvDTE.ProjectItem)
  '  On Error Resume Next
  '  Debug.Print("createFunctionList ")
  '  Debug.Print(prjItem.ToString)
  ' '' Button1.Enabled = prjItem IsNot Nothing
  '  oPrjItem = prjItem


  ' '' !!! frm.Text = "FuncList ???"

  ' '' !!! frm.Text = "FuncList:" + prjItem.Name

  '  frm.ListView1.Items.Clear()

  '  For Each cel As EnvDTE.CodeElement In prjItem.FileCodeModel.CodeElements
  '    Debug.Print("main:" + cel.Name)

  '    recursive_funclist(cel, 0, frm)
  '  Next

  'End Sub

  'Sub recursive_funclist(ByVal cel As EnvDTE.CodeElement, ByVal indent As Integer, ByVal frm As AA_frmExplorer)
  ' On Error Resume Next
  '  Debug.Print("recfunclist: " + cel.Name)

  '  addCodeElementItem(cel, indent, frm)

  ' If cel.Children.Count = 0 Then Exit Sub

  '  For Each el As EnvDTE.CodeElement In cel.Children


  '   If el.IsCodeType Then
  '     recursive_funclist(el, indent + 2, frm)
  '   Else
  '      Select Case el.Kind
  '       Case EnvDTE.vsCMElement.vsCMElementClass, _
  '            EnvDTE.vsCMElement.vsCMElementModule, _
  '            EnvDTE.vsCMElement.vsCMElementEnum, _
  '            EnvDTE.vsCMElement.vsCMElementEvent, _
  '            EnvDTE.vsCMElement.vsCMElementFunction, _
  '            EnvDTE.vsCMElement.vsCMElementProperty, _
  '            EnvDTE.vsCMElement.vsCMElementStruct, _
  '            EnvDTE.vsCMElement.vsCMElementNamespace, _
  '            EnvDTE.vsCMElement.vsCMElementDeclareDecl

  '         addCodeElementItem(el, indent + 1, frm)
  '     End Select
  '
  '    End If




  ' Next


  'End Sub

  'Sub addCodeElementItem(ByVal cEl As EnvDTE.CodeElement, ByVal indent As Integer, ByVal frm As AA_frmExplorer)
  '  On Error Resume Next
  'Dim item As String
  'Dim lvi As ListViewItem

  'Dim sTyp As String = cEl.Kind.ToString.Replace("vsCMElement", "") + " "
  'Dim sIndent As String = Space((indent) * 3)

  '  item = sIndent + sTyp + cEl.Name
  '  item = Replace(item, "Function", "")
  '  item = Replace(item, "Sub", " s")

  '  lvi = frm.ListView1.Items.Add(cEl.FullName, item, "")
  '  lvi.Tag = cEl


  'End Sub


End Module
