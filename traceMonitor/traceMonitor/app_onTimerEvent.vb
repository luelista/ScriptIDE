Imports EnvDTE
Module app_onTimerEvent

 

  Public Sub onTimerEvent()
    Dim mes As String : mes = "checkForFuncListUpdate"
    trace("TEST: ...blabla", mes)

    'checkForFuncListUpdate()
  End Sub




  Private Sub checkForFuncListUpdate()

    '' On Error Resume Next
    Dim objDocument As EnvDTE.Document
    Dim objTextDocument As EnvDTE.TextDocument
    Static lastDoc As EnvDTE.TextDocument
    Static lastCodeElement As CodeElement

    Try
      objDocument = IDE.ActiveDocument
      objTextDocument = CType(objDocument.Object, EnvDTE.TextDocument)

      If IDE.ActiveWindow.ProjectItem Is Nothing Then Exit Sub
      If objTextDocument.DTE.ActiveWindow.Document.Name Is Nothing Then Exit Sub
      If objTextDocument.DTE.ActiveWindow.ProjectItem.Name Is Nothing Then Exit Sub

      If objTextDocument IsNot lastDoc Then
        Debug.Write("++++++++++++++++++++++++++++++++++++++++++++")
        Debug.Write(vbCrLf)
        Debug.Write("--> highlightSolutionExplorer: ")
        Debug.Write(objTextDocument.DTE.ActiveWindow.Document.Name)
        Debug.Write(vbCrLf)
        highlightSolutionExplorer(objDocument)

        lastDoc = objTextDocument
        Debug.Write("--> createFunctionList: ")
        Debug.Write(objTextDocument.DTE.ActiveWindow.Document.Name)
        Debug.Write(vbCrLf)
        ' !!! createFunctionList(IDE.ActiveWindow.ProjectItem, AA_frmExplorer)
      End If


      Dim codeElement As CodeElement
      codeElement = GetSelectedCodeElement()
      If lastCodeElement IsNot codeElement Then
        lastCodeElement = codeElement
        Debug.Write("-->highlightFuncList: ")
        Debug.Write(codeElement.Name)
        Debug.Write(vbCrLf)
        highlightFuncList(codeElement)
      End If

    Catch objException As System.Exception
      'MessageBox.Show(objException.ToString)
    End Try

  End Sub

  Sub highlightFuncList(ByVal codeElement As CodeElement)
    On Error Resume Next
    Dim such = codeElement.Name
    Dim lv As ListView
    ' !!! lv = AA_frmExplorer.ListView1
    'Dim el As ListView.ListViewItemCollection
    Dim el As ListView.SelectedIndexCollection

    For Each el In lv.Items '.Items ''lv.Items
      Debug.Write("lv.Items: ")
      Debug.Print(el.ToString)
      If InStr(el.ToString, such) > 0 Then
        Dim index As Integer
        'index = el.Item.

      End If
    Next

  End Sub

  Public Sub navigateFuncList(ByVal proItem As EnvDTE.ProjectItem, ByVal newMod As String)
    On Error Resume Next
    ' !!! createFunctionList(proItem, AA_frmExplorer)
    Exit Sub



    Dim objDocument As EnvDTE.Document
    Dim objTextDocument As EnvDTE.TextDocument
    Dim objTextSelection As EnvDTE.TextSelection
    Dim sMsg As String
    Dim p As EnvDTE.Project
    p = IDE.ActiveSolutionProjects

    'p = IDE.ActiveDocument
    Static lastDoc As EnvDTE.TextDocument

    newMod = Replace(newMod, ".vb", "")
    Dim newProjectItem As ProjectItem

    For Each prjItem As EnvDTE.ProjectItem In p.ProjectItems
      Dim img As String = "x"
      Debug.Print(prjItem.Name)
      If prjItem.Name.EndsWith(".vb") Then Debug.Print(prjItem.Name)
      If prjItem.Name = newMod Then
        MsgBox(prjItem.Name)
        '!!! createFunctionList(prjItem, AA_frmExplorer)
        Exit Sub
      End If
    Next
    Exit Sub


    newProjectItem = IDE.ActiveDocument.ProjectItem.FileCodeModel.CodeElements()
    ' !!! createFunctionList(newProjectItem, AA_frmExplorer)
    Exit Sub



    objDocument = IDE.ActiveDocument
    ' Get the text document
    objTextDocument = CType(objDocument.Object, EnvDTE.TextDocument)
    If objTextDocument IsNot lastDoc Then
      lastDoc = objTextDocument
      Debug.Print("checkForFuncListUpdate")
      '!!! createFunctionList(IDE.ActiveWindow.ProjectItem, AA_frmExplorer)
    End If


    ' Get the text selection object
    'objTextSelection = objTextDocument.Selection

    'Debug.Print(IDE.ActiveWindow.)
    'Dim td As TextDocument = IDE.ActiveWindow.Document.Object ', TextDocument)
    'Dim point As EditPoint = td.CreateEditPoint()

    ''Catch objException As System.Exception
    'MessageBox.Show(objException.ToString)
    ''End Try

  End Sub




End Module