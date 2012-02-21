Imports EnvDTE

Module sys_ideHelper2


  Sub navigateCodeLink(ByVal app As String, ByVal modItem As String, ByVal func As String)
    On Error Resume Next
    If app = "scriptClass" Or app = "lua" Or app = "script" Then
      oIntWin.SendCommand("scriptide", "NavigateFile", modItem)


    ElseIf modItem = "via sys_interproc" Then
      Dim winTitle = app.Substring(0, app.IndexOf("@"))
      oIntWin.EnsureAppRunning("iprocviewer")
      '' zeigt die interprocInfos zu einem geöffneten Fenster an
      oIntWin.SendCommand("iprocviewer", "NavigateWinTitle", winTitle)

    Else
      Dim prjItem As EnvDTE.ProjectItem
      For Each pro In IDE.Solution.Projects
        prjItem = FindProjectItemByName(pro, modItem)
      Next

      Dim codeElement As EnvDTE.CodeElement
      Dim found As Boolean = False

      codeElement = FindCodeElemntByName(prjItem, func, found)
      codeElement.ProjectItem.Open()

      Dim myLine As Integer = codeElement.StartPoint.Line
      Dim sel As TextSelection = IDE.ActiveDocument.Selection

      sel.MoveToPoint(codeElement.StartPoint)
      codeElement.StartPoint.TryToShow(vsPaneShowHow.vsPaneShowTop, -80)
      IDE.MainWindow.Activate()
    End If

  End Sub

  Function FindCodeElemntByName(ByVal prjItem As EnvDTE.ProjectItem, ByVal func As String, ByRef found As Boolean) As EnvDTE.CodeElement
    For Each cElement As EnvDTE.CodeElement In prjItem.FileCodeModel.CodeElements
      '' Debug.Print("main:" + cElement.Name)
      FindCodeElemntByName = XXXrecursive_funclist(cElement, func, 0, found)
      If found = True Then Exit Function
    Next

  End Function


  Function XXXrecursive_funclist(ByVal cElemet As EnvDTE.CodeElement, ByVal func As String, ByVal indent As Integer, ByRef found As Boolean) As EnvDTE.CodeElement
    ''On Error Resume Next

    '' Debug.Print("funcListItem: " + cElemet.Name)
    If cElemet.Children.Count = 0 Then Exit Function

    For Each el As EnvDTE.CodeElement In cElemet.Children
      If el.IsCodeType Then
        XXXrecursive_funclist = XXXrecursive_funclist(el, func, indent + 2, found)
        If found = True Then Exit Function
      Else
        Select Case el.Kind
          Case EnvDTE.vsCMElement.vsCMElementClass, _
               EnvDTE.vsCMElement.vsCMElementModule, _
               EnvDTE.vsCMElement.vsCMElementEnum, _
               EnvDTE.vsCMElement.vsCMElementEvent, _
               EnvDTE.vsCMElement.vsCMElementFunction, _
               EnvDTE.vsCMElement.vsCMElementProperty, _
               EnvDTE.vsCMElement.vsCMElementStruct, _
               EnvDTE.vsCMElement.vsCMElementNamespace, _
               EnvDTE.vsCMElement.vsCMElementDeclareDecl
            '' Debug.Print(el.Name)

            If el.Name = func Then
              Debug.Print("found 2")
              XXXrecursive_funclist = el
              found = True
              Exit Function
            End If

            Dim item As String
            Dim sTyp As String = el.Kind.ToString.Replace("vsCMElement", "") + " "
            Dim sIndent As String = Space((indent) * 3)

            item = sIndent + sTyp + el.Name
            item = Replace(item, "Function", "")
            item = Replace(item, "Sub", " s")

            '' Debug.Print(el.FullName)
            '' !!! lvi.Tag = el
        End Select
      End If
    Next
  End Function


  Sub XXXaddCodeElementItem(ByVal cEl As EnvDTE.CodeElement, ByVal indent As Integer, ByVal frm As Form) ' frmExplorer)
    On Error Resume Next
    Dim item As String
    Dim lvi As ListViewItem
    Dim sTyp As String = cEl.Kind.ToString.Replace("vsCMElement", "") + " "
    Dim sIndent As String = Space((indent) * 3)

    item = sIndent + sTyp + cEl.Name
    item = Replace(item, "Function", "")
    item = Replace(item, "Sub", " s")

    Stop

    ' !!! lvi = frm.ListView1.Items.Add(cEl.FullName, item, "")
    lvi.Tag = cEl


  End Sub


  Function FindProjectItemByName(ByVal pro As EnvDTE.Project, ByVal modItem As String)
    On Error Resume Next
    Dim node_prj, node_item, node_el, node_subEl As TreeNode
    Dim kPrj As String = "prj" + pro.UniqueName
    '' Debug.Print(pro.UniqueName)
    '' Debug.Print(pro.Name)

    For Each prjItem As EnvDTE.ProjectItem In pro.ProjectItems

      'Debug.Print("-->" + prjItem.Name)
      If prjItem.Name = modItem Then
        FindProjectItemByName = prjItem
        Debug.Print("found !!!")
        Exit Function

      End If
      ''img = "form"
    Next
  End Function


  Sub waitForMouseUp()
    Dim isMouseDown As Boolean = False
    Do
      isMouseDown = isLeftMouseButton()
      'Debug.Print(isMouseDown.ToString)
      'Application.DoEvents()
      If isMouseDown = False Then Exit Do
    Loop
    Debug.Print("waitForMouseUp: done")
  End Sub
  Private Function getCurrentCodeWindowText() As String
    GetSelectedInformation()
    On Error Resume Next
    'Debug.Print(IDE.ActiveWindow.)
    Dim td As TextDocument = IDE.ActiveWindow.Document.Object ', TextDocument)
    Dim point As EditPoint = td.CreateEditPoint()
    Dim RESULT As String = point.GetText(5555555)
    Debug.Print(RESULT)
    getCurrentCodeWindowText = RESULT
  End Function

  Function getCurrentCodeLine() As String
    Dim objDocument As EnvDTE.Document
    Dim objTextDocument As EnvDTE.TextDocument
    Dim objTextSelection As EnvDTE.TextSelection
    Dim sMsg As String

    Try
      objDocument = IDE.ActiveDocument
      ' Get the text document
      objTextDocument = CType(objDocument.Object, EnvDTE.TextDocument)
      ' Get the text selection object
      objTextSelection = objTextDocument.Selection

      Dim topLine As Integer
      Dim topOffset As Integer
      Dim endLine As Integer
      Dim endOffset As Integer
      topLine = objTextSelection.TopPoint.Line
      topOffset = objTextSelection.TopPoint.LineCharOffset
      endLine = objTextSelection.BottomPoint.Line
      endOffset = objTextSelection.BottomPoint.LineCharOffset
      '' aa_frmMain.txt_startSel.Text = topLine.ToString & ", " & topOffset.ToString
      '' aa_frmMain.txt_endSel.Text = endLine.ToString & ", " & endOffset.ToString
      Dim lines() As String
      Dim modText As String
      Dim curLine As String
      modText = getCurrentCodeWindowText()
      lines = Split(modText, vbCrLf)
      curLine = lines(topLine - 1)
      getCurrentCodeLine = curLine
    Catch objException As System.Exception
      'MessageBox.Show(objException.ToString)
    End Try

  End Function

  Sub GetSelectedInformation()
    Dim objDocument As EnvDTE.Document
    Dim objTextDocument As EnvDTE.TextDocument
    Dim objTextSelection As EnvDTE.TextSelection
    Dim sMsg As String

    Try
      ' Get the active document
      objDocument = IDE.ActiveDocument
      ' Get the text document
      objTextDocument = CType(objDocument.Object, EnvDTE.TextDocument)
      ' Get the text selection object
      objTextSelection = objTextDocument.Selection
      ' Show some properties of the text selection
      sMsg = "1 ActivePoint: " & objTextSelection.ActivePoint.Line.ToString & ", " & _
          objTextSelection.ActivePoint.LineCharOffset.ToString
      sMsg &= vbCrLf
      sMsg &= "2 TopPoint: " & objTextSelection.TopPoint.Line.ToString & ", " & _
          objTextSelection.TopPoint.LineCharOffset.ToString
      sMsg &= vbCrLf
      sMsg &= "3 BottomPoint: " & objTextSelection.BottomPoint.Line.ToString & ", " & _
         objTextSelection.BottomPoint.LineCharOffset.ToString
      sMsg &= vbCrLf
      sMsg &= "4 Text: " & objTextSelection.Text
      sMsg &= vbCrLf
      'MessageBox.Show(sMsg)
    Catch objException As System.Exception
      MessageBox.Show(objException.ToString)
    End Try

  End Sub

  Sub checkAktCodePos()
    On Error Resume Next
    'Stop
    If IDE.ActiveDocument Is Nothing Then Exit Sub

    Dim textSel As TextSelection = IDE.ActiveDocument.Selection
    Dim myCodeLine As Integer = textSel.ActivePoint.Line

    If ActCodePosition.Line = myCodeLine Then Exit Sub
    'Stop
    ActCodePosition.Line = myCodeLine

    Dim myCodeEl As CodeElement = GetSelectedCodeElement()

    If myCodeEl.Name = ActCodePosition.procedureName Then Exit Sub
    'Stop

    ActCodePosition.procedureName = ActCodePosition.codeElement.Name
    ActCodePosition.moduleName = IDE.ActiveDocument.FullName

    ActCodePosition.codeElement = myCodeEl

    '' frm_funcList.Label1.Text = "..."
    '' frm_funcList.Label1.Text = ActCodePosition.codeElement.Name
  End Sub



  Function GetSelectedCodeElement() As CodeElement

    Dim myScopes() As vsCMElement = {vsCMElement.vsCMElementFunction, _
                                        vsCMElement.vsCMElementEvent, _
                                        vsCMElement.vsCMElementProperty, _
                                        vsCMElement.vsCMElementDeclareDecl, _
                                        vsCMElement.vsCMElementEnum, _
                                        vsCMElement.vsCMElementStruct, _
                                        vsCMElement.vsCMElementClass, _
                                        vsCMElement.vsCMElementModule, _
                                        vsCMElement.vsCMElementNamespace}


    ' Before running this example, open a code document from a project
    ' and place the insertion point anywhere inside the source code.
    Try
      Dim actDoc As Document = IDE.ActiveDocument
      Dim sel As TextSelection = _
          CType(actDoc.Selection, TextSelection)
      Dim pnt As TextPoint = CType(sel.ActivePoint, TextPoint)

      ' Discover every code element containing the insertion point.
      Dim fcm As FileCodeModel = actDoc.ProjectItem.FileCodeModel
      Dim elem As CodeElement
      For Each scope As vsCMElement In myScopes
        elem = fcm.CodeElementFromPoint(pnt, scope)
        If IsNothing(elem) = False Then
          Return elem
        End If
      Next

      Return Nothing
    Catch ex As Exception
      Return Nothing
    End Try

  End Function







End Module
