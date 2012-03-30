Public Class GenericDockContent
  Inherits WeifenLuo.WinFormsUI.Docking.DockContent
  Implements IDockContentForm

  Protected p_indexListCtrl As IIndexList

  Protected p_tabRowKey As String
  Protected p_isLazy As Boolean
  Protected p_URL As String

  Private p_parameters As New Hashtable
  ReadOnly Property Parameters() As Hashtable Implements IDockContentForm.Parameters
    Get
      Return p_parameters
    End Get
  End Property

  Private Sub Me_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    If e.CloseReason = CloseReason.MdiFormClosing Then Exit Sub
    If internalCloseTab(Me) = False Then e.Cancel = True
  End Sub

  Public Overridable Sub createIndexList() Implements Core.IDockContentForm.createIndexList
    cls_IDEHelper.Instance.ContentHelper.SimpleCreateIndexList(Me, p_indexListCtrl)
  End Sub

  Public Event CurrentIndexLineChanged(ByVal lineNr As Integer) Implements Core.IDockContentForm.CurrentIndexLineChanged

  Public Overrides Function GetPersistString() As String
    Return "GenericDockContent|##|" + Me.Hash
  End Function

  Protected m_Dirty As Boolean
  Public Overridable Property Dirty() As Boolean Implements Core.IDockContentForm.Dirty
    Get
      Return m_Dirty
    End Get
    Set(ByVal value As Boolean)
      m_Dirty = value
      Me.Text = If(value, "◊", "") + getViewFilename()
      createOpenedTabList()
    End Set
  End Property

  Protected m_Filetype As String
  Public ReadOnly Property Filetype() As String Implements Core.IDockContentForm.Filetype
    Get
      Return m_Filetype
    End Get
  End Property

  Public Overridable Function getActLineContent() As String Implements Core.IDockContentForm.getActLineContent

  End Function

  Public Overridable Function getCurrentLineNumber() As Integer Implements Core.IDockContentForm.getCurrentLineNumber
    
  End Function

  Public Function getFileTag() As String Implements Core.IDockContentForm.getFileTag
    Return URL
  End Function

  Public Overridable Function getIcon() As System.Drawing.Icon() Implements Core.IDockContentForm.getIcon
    Dim ext = URL.Substring(URL.LastIndexOf("."))
    Return FileTypeAndIcon.RegisteredFileType.GetFileIconByExt(ext)
  End Function

  Public Overridable Function getLineContent(ByVal lineNumber As Integer) As String Implements Core.IDockContentForm.getLineContent

  End Function

  Public Overridable Function getLineCount() As Integer Implements Core.IDockContentForm.getLineCount
    
  End Function

  Public Overridable Function getLines() As String() Implements Core.IDockContentForm.getLines
    
  End Function

  Public Overridable Function getViewFilename() As String Implements Core.IDockContentForm.getViewFilename
    Return URL.Substring(URL.LastIndexOf("/") + 1)
  End Function

  Private m_hash As String
  Public Overridable Property Hash() As String Implements IDockContentForm.Hash
    Get
      Return m_hash
    End Get
    Set(ByVal value As String)
      m_hash = value
    End Set
  End Property

  Overridable ReadOnly Property indexListCtrl() As ListBox Implements IDockContentForm.indexListCtrl
    Get
      Return p_indexListCtrl
    End Get
  End Property


  Public Overridable Sub jumpToLine(ByVal lineNr As Integer) Implements Core.IDockContentForm.jumpToLine
    
  End Sub

  Public Overridable Sub navigateIndexlist(ByVal line As String) Implements Core.IDockContentForm.navigateIndexlist

  End Sub

  Public Overridable Function onCheckDirty(Optional ByVal beforeWhat As String = "vor dem Schließen ") As Boolean Implements Core.IDockContentForm.onCheckDirty
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
  End Function

  Public Overridable Sub onLazyInit() Implements Core.IDockContentForm.onLazyInit

  End Sub

  ''' <summary>
  ''' Override <see>Read</see> instead!
  ''' </summary>
  Public Sub onRead() Implements Core.IDockContentForm.onRead
    Me.Read()
  End Sub
  Protected Overridable Sub Read()

  End Sub

  ''' <summary>
  ''' Override <see>Save</see> instead!
  ''' </summary>
  Public Sub onSave() Implements Core.IDockContentForm.onSave
    Dim cancel As Boolean = False
    cls_IDEHelper.GetSingleton.OnDocumentBeforeSave(Hash, cancel)
    If cancel Then Return

    Me.Save()

    cls_IDEHelper.GetSingleton.OnDocumentAfterSave(Hash)
  End Sub
  Protected Overridable Sub Save()

  End Sub

  Public Overridable ReadOnly Property RTF() As Object Implements Core.IDockContentForm.RTF
    Get

    End Get
  End Property

  Public Sub setFileTag(ByVal sName As String) Implements Core.IDockContentForm.setFileTag
    URL = sName
  End Sub

  Overridable Property tabRowKey() As String Implements IDockContentForm.tabRowKey
    Get
      Return p_tabRowKey
    End Get
    Set(ByVal value As String)
      p_tabRowKey = value
    End Set
  End Property
  Overridable Property isLazy() As Boolean Implements IDockContentForm.isLazy
    Get
      Return p_isLazy
    End Get
    Set(ByVal value As Boolean)
      p_isLazy = value
    End Set
  End Property

  Public Overridable Property URL() As String Implements Core.IDockContentForm.URL
    Get
      Return p_URL
    End Get
    Set(ByVal value As String)
      p_URL = value
      Me.Icon = getIcon(0)
    End Set
  End Property
End Class
