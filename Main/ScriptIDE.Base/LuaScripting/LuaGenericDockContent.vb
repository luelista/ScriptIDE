Public Class LuaGenericDockContent
  Inherits WeifenLuo.WinFormsUI.Docking.DockContent
  Implements IDockContentForm

  Private p_indexListCtrl As IIndexList
  Private dispatch As LuaInterface.LuaTable

  Private p_tabRowKey As String
  Private p_isLazy As Boolean

  Private p_parameters As New Hashtable
  ReadOnly Property Parameters() As Hashtable Implements IDockContentForm.Parameters
    Get
      Return p_parameters
    End Get
  End Property

  Public Sub New(ByVal functionTable As LuaInterface.LuaTable)
    dispatch = functionTable
  End Sub

  Private Sub Me_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    If e.CloseReason = CloseReason.MdiFormClosing Then Exit Sub
    If internalCloseTab(Me) = False Then e.Cancel = True
  End Sub

  Public Sub createIndexList() Implements Core.IDockContentForm.createIndexList
    cls_IDEHelper.Instance.ContentHelper.SimpleCreateIndexList(Me, p_indexListCtrl)
  End Sub

  Public Event CurrentIndexLineChanged(ByVal lineNr As Integer) Implements Core.IDockContentForm.CurrentIndexLineChanged

  Public Overrides Function GetPersistString() As String
    Return "LuaGenericDockContent|##|" + Me.Hash
  End Function

  Public Property Dirty() As Boolean Implements Core.IDockContentForm.Dirty
    Get
      Return dispatch("Dirty")
    End Get
    Set(ByVal value As Boolean)
      dispatch("Dirty") = value
      Me.Text = If(value, "◊", "") + getViewFilename()
      createOpenedTabList()
    End Set
  End Property

  Public ReadOnly Property Filetype() As String Implements Core.IDockContentForm.Filetype
    Get
      Return dispatch("Filetype")
    End Get
  End Property

  Public Function getActLineContent() As String Implements Core.IDockContentForm.getActLineContent
    Dim func As LuaInterface.LuaFunction = dispatch("getActLineContent")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "getActLineContent not found")
    Else
      Return func.Call()(0)
    End If
  End Function

  Public Function getCurrentLineNumber() As Integer Implements Core.IDockContentForm.getCurrentLineNumber
    Dim func As LuaInterface.LuaFunction = dispatch("getCurrentLineNumber")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "getCurrentLineNumber not found")
    Else
      Return func.Call()(0)
    End If
  End Function

  Public Function getFileTag() As String Implements Core.IDockContentForm.getFileTag
    Return dispatch("URL")
  End Function

  Public Function getIcon() As System.Drawing.Icon() Implements Core.IDockContentForm.getIcon
    Dim func As LuaInterface.LuaFunction = dispatch("getIcon")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "getIcon not found")
      Dim ext = URL.Substring(URL.LastIndexOf("."))
      Return FileTypeAndIcon.RegisteredFileType.GetFileIconByExt(ext)
    Else
      Dim result = func.Call()
      If TypeOf result(0) Is String Then
        Return FileTypeAndIcon.RegisteredFileType.GetFileIconByExt(result(0))
      Else
        Return result
      End If
    End If
  End Function

  Public Function getLineContent(ByVal lineNumber As Integer) As String Implements Core.IDockContentForm.getLineContent
    Dim func As LuaInterface.LuaFunction = dispatch("getLineContent")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "getLineContent not found")
    Else
      Return func.Call(lineNumber)(0)
    End If
  End Function

  Public Function getLineCount() As Integer Implements Core.IDockContentForm.getLineCount
    Dim func As LuaInterface.LuaFunction = dispatch("getLineCount")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "getLineCount not found")
    Else
      Return func.Call()(0)
    End If
  End Function

  Public Function getLines() As String() Implements Core.IDockContentForm.getLines
    Dim func As LuaInterface.LuaFunction = dispatch("getLines")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "getLines not found")
    Else
      Return func.Call()(0)
    End If
  End Function

  Public Function getViewFilename() As String Implements Core.IDockContentForm.getViewFilename
    Dim func As LuaInterface.LuaFunction = dispatch("getViewFilename")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "getViewFilename not found")
      Return URL.Substring(URL.LastIndexOf("/") + 1)
    Else
      Return func.Call()(0)
    End If
  End Function

  Private m_hash As String
  Public Property Hash() As String Implements IDockContentForm.Hash
    Get
      Return m_hash
    End Get
    Set(ByVal value As String)
      m_hash = value
    End Set
  End Property

  ReadOnly Property indexListCtrl() As ListBox Implements IDockContentForm.indexListCtrl
    Get
      Return p_indexListCtrl
    End Get
  End Property


  Public Sub jumpToLine(ByVal lineNr As Integer) Implements Core.IDockContentForm.jumpToLine
    Dim func As LuaInterface.LuaFunction = dispatch("lineNr")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "lineNr not found")
    Else
      func.Call(lineNr)
    End If
  End Sub

  Public Sub navigateIndexlist(ByVal line As String) Implements Core.IDockContentForm.navigateIndexlist
    Dim func As LuaInterface.LuaFunction = dispatch("navigateIndexlist")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "navigateIndexlist not found")
    Else
      func.Call(line)
    End If
  End Sub

  Public Function onCheckDirty(Optional ByVal beforeWhat As String = "vor dem Schließen ") As Boolean Implements Core.IDockContentForm.onCheckDirty
    Dim func As LuaInterface.LuaFunction = dispatch("onCheckDirty")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "onCheckDirty not found")
      If Dirty Then
        setActRtfBox(Me)
        Select Case MsgBox("Im Dokument " + URL + " befinden sich ungespeicherte Änderungen. Soll es " + beforeWhat + "gespeichert werden?", MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel, "scriptIDE - Datei speichern?")
          Case MsgBoxResult.Yes
            Me.onSave()
          Case MsgBoxResult.No
          Case MsgBoxResult.Cancel
            Return False
        End Select
      End If
      Return True
    Else
      Return func.Call(beforeWhat)(0)
    End If
  End Function

  Public Sub onLazyInit() Implements Core.IDockContentForm.onLazyInit
    Dim func As LuaInterface.LuaFunction = dispatch("onLazyInit")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "onLazyInit not found")
    Else
      func.Call()
    End If
  End Sub

  Public Sub onRead() Implements Core.IDockContentForm.onRead
    Dim func As LuaInterface.LuaFunction = dispatch("onRead")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "onRead not found")
    Else
      func.Call()
    End If
  End Sub

  Public Sub onSave() Implements Core.IDockContentForm.onSave
    Dim func As LuaInterface.LuaFunction = dispatch("onRead")
    If func Is Nothing Then
      TT.Write("LuaGenericDockContent, functionTable", "onRead not found")
    Else
      func.Call()
    End If
  End Sub


  Public ReadOnly Property RTF() As Object Implements Core.IDockContentForm.RTF
    Get
      Return dispatch("Object")
    End Get
  End Property

  Public Sub setFileTag(ByVal sName As String) Implements Core.IDockContentForm.setFileTag
    dispatch("URL") = sName
  End Sub

  Property tabRowKey() As String Implements IDockContentForm.tabRowKey
    Get
      Return p_tabRowKey
    End Get
    Set(ByVal value As String)
      p_tabRowKey = value
    End Set
  End Property
  Property isLazy() As Boolean Implements IDockContentForm.isLazy
    Get
      Return p_isLazy
    End Get
    Set(ByVal value As Boolean)
      p_isLazy = value
    End Set
  End Property

  Public Property URL() As String Implements Core.IDockContentForm.URL
    Get
      Return dispatch("URL")
    End Get
    Set(ByVal value As String)
      dispatch("URL") = value
      Me.Icon = getIcon(0)
    End Set
  End Property
End Class
