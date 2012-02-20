Module app_fileTypeHandling

  'Public fileTypeHandlers As New Dictionary(Of String, FiletypeData)
  Public fileActionHandlers As New Dictionary(Of String, FileactionData)
  Public fileKeyboardHandlers As New Dictionary(Of String, FileactionData)

  Enum FiletypeGUIMode
    Scintilla
    DockContent
    NoGUI
  End Enum

  'Sub setAddinHandlerForExt(ByVal ext As String, ByVal fhType As Type, ByVal gui As FiletypeGUIMode)
  '  If fileTypeHandlers.ContainsKey(ext.ToLower) = False Then
  '    fileTypeHandlers.Add(ext.ToLower, New FiletypeData)
  '  End If
  '  With fileTypeHandlers(ext.ToLower)
  '    .handlerClassType = fhType
  '    .handlerMode = "ADDIN"

  '    .useGUI = gui
  '  End With
  'End Sub

  Sub addFileactionHandlerFunc(ByVal meth As Reflection.MethodInfo, ByVal classInst As Object)
    Dim attr2 As FileActionAttribute = Attribute.GetCustomAttribute(meth, GetType(FileActionAttribute))
    If attr2 Is Nothing Then Exit Sub

    Dim key = meth.DeclaringType.FullName + "::" + meth.Name
    If fileActionHandlers.ContainsKey(key) Then fileActionHandlers.Remove(key)

    Dim dat As New FileactionData
    dat.buttonText = attr2.ButtonText
    dat.iconURL = attr2.IconURL
    dat.fileTypes = attr2.FileTypes
    dat.handlerFunc = meth
    dat.handlerClassInst = classInst
    dat.handlesKey = attr2.HandlesKeyString
    If String.IsNullOrEmpty(attr2.HandlesKeyString) = False Then
      If fileKeyboardHandlers.ContainsKey(attr2.HandlesKeyString) Then _
                 fileKeyboardHandlers.Remove(attr2.HandlesKeyString)
      fileKeyboardHandlers.Add(attr2.HandlesKeyString, dat)
    End If
    fileActionHandlers.Add(key, dat)
  End Sub

  Sub createToolbaritemsForExt(ByVal ext As String, ByVal tb As ToolStrip, ByVal tabURL As String)
    On Error Resume Next
    Dim tsb As ToolStripButton
    tabURL = LCase(tabURL)
    Dim cmdPath = AddInTree.GetTreeNode("/Workspace/ToolbarCommands")
    Dim path = AddInTree.GetTreeNode("/Workspace/FileCommands")
    For Each subPath In path.ChildNodes
      Dim checkFor = "." + LCase(subPath.Key) + "."
      If checkFor.Contains(ext + ".") Then
        subPath.Value.EnsureSorted()
        For Each cod In subPath.Value.Codons
          Select Case LCase(cod.Name)
            Case "toolbutton"
              Dim id, text, icon As String
              id = cod.Properties("refid")
              Dim command As Codon = cmdPath.GetChildItem(id, False)
              If cod.Properties.Contains("text") Then text = cod.Properties("text") Else text = command.Properties("text")
              If cod.Properties.Contains("icon") Then icon = cod.Properties("icon") Else icon = command.Properties("icon")

              Dim item As New AddinToolstripButton(id, command.AddIn, text, icon)
              item.FileURL = tabURL : tb.Items.Add(item)
            Case "toollabel"
              Dim item As New AddinToolstripLabel(cod)
              item.FileURL = tabURL : tb.Items.Add(item)
            Case "toolseparator"
              tb.Items.Add("-")
            Case Else
              TT.Write("TB " + ext + " Error", "Invalid Kind: """ + cod.Properties("kind") + """", "info")
          End Select
        Next
      End If
    Next

    For Each kvp In fileActionHandlers
      If kvp.Value Is Nothing OrElse kvp.Value.fileTypes Is Nothing OrElse kvp.Value.fileTypes.Length = 0 Then Continue For
      If kvp.Value.fileTypes.Contains(ext) Or kvp.Value.fileTypes(0) = ".*" Then
        If kvp.Value.iconURL <> "" Then
          tsb = tb.Items.Add(kvp.Value.buttonText, ResourceLoader.GetImageCached(kvp.Value.iconURL))
        Else
          tsb = tb.Items.Add(kvp.Value.buttonText)
        End If
        tsb.Tag = kvp.Value
        AddHandler tsb.MouseUp, AddressOf onFileactionButtonClicked
      End If
    Next
  End Sub

  Sub onFileactionButtonClicked(ByVal sender As Object, ByVal e As MouseEventArgs)
    Dim tag As FileactionData = sender.Tag
    If e.Button = MouseButtons.Right Then
      MsgBox(tag.buttonText + vbNewLine + tag.handlerFunc.Name)
    Else
      Dim frm = CType(sender, ToolStripItem).Owner.FindForm()
      FileActionCallback(tag, frm)
    End If
  End Sub

  Sub FileActionCallback(ByVal tag As FileactionData, ByVal frm As Form)
    Try
      tag.handlerFunc.Invoke(tag.handlerClassInst, New Object() {frm})
    Catch ex As Reflection.TargetInvocationException
      MsgBox("Fehler beim Ausführen der Aktion """ + tag.buttonText + """:" + vbNewLine + ex.InnerException.ToString, MsgBoxStyle.Exclamation)
    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try
  End Sub

End Module

'Class FiletypeData
'  Public handlerClassType As Type

'  Public handlerMode As String '"INTSCRIPT", "ADDIN"
'  Public useGUI As FiletypeGUIMode
'  Public scintillaHandlerInstance As IScintillaFiletypeHandler
'  Public plainHandlerInstance As IPlainFiletypeHandler

'  Public IsHandlerInstanciated As Boolean
'End Class

Class FileactionData
  Public handlerFunc As Reflection.MethodInfo
  Public buttonText, iconURL, fileTypes(), handlesKey As String
  Public handlerClassInst As Object
End Class
