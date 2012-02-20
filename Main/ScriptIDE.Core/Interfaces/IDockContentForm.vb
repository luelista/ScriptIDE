Public Interface IDockContentForm
  'Inherits WeifenLuo.WinFormsUI.Docking.IDockContent
  Event CurrentIndexLineChanged(ByVal lineNr As Integer)

  Property URL() As String
  Property Hash() As String
  ReadOnly Property Filetype() As String

  Property tabRowKey() As String
  Property isLazy() As Boolean
  ReadOnly Property indexListCtrl() As Windows.Forms.ListBox

  Function getFileTag() As String
  Sub setFileTag(ByVal sName As String)

  Function getViewFilename() As String

  Function getIcon() As drawing.Icon()

  Property Dirty() As Boolean
  ReadOnly Property RTF() As Object
  ReadOnly Property Parameters() As Hashtable

  Sub navigateIndexlist(ByVal line As String)
  Sub jumpToLine(ByVal lineNr As Integer)

  Sub onSave()
  Sub onRead()
  Sub onLazyInit()

  Function getLineContent(ByVal lineNumber As Integer) As String
  Function getLineCount() As Integer
  Function getLines() As String()
  Function getActLineContent() As String
  Function getCurrentLineNumber() As Integer

  Function onCheckDirty(Optional ByVal beforeWhat As String = "vor dem Schließen ") As Boolean

  Sub createIndexList()


End Interface
