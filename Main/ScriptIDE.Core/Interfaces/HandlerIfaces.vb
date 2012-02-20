
Public Interface IIndexList
  Event ItemClicked(ByVal lineNumber As Integer)
  Sub buildList(ByVal Tab As IDockContentForm)
  Sub onPositionChanged(ByVal lineNumber As Integer)
  Sub onKeyHandler(ByVal key As KeyEventArgs)


End Interface


'-->
'--> PropertyPage

Public Interface IPropertyPage
  Sub readProperties()
  Sub saveProperties()

End Interface

<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False)> Public Class PropertyPageAttribute
  Inherits Attribute

  'Private _pageKey As String
  'Public ReadOnly Property PageKey() As String
  '  Get
  '    Return _pageKey
  '  End Get
  'End Property
  Private _treePath As String
  Public ReadOnly Property TreePath() As String
    Get
      Return _treePath
    End Get
  End Property
  Private _shortTitle As String
  Public ReadOnly Property ShortTitle() As String
    Get
      Return _shortTitle
    End Get
  End Property
  Private _longTitle As String
  Public ReadOnly Property LongTitle() As String
    Get
      Return _longTitle
    End Get
  End Property
  Private _iconUrl As String
  Public ReadOnly Property IconURL() As String
    Get
      Return _iconUrl
    End Get
  End Property

  Public Sub New(ByVal pTreePath As String, ByVal pShortTitle As String, ByVal pLongTitle As String, ByVal pIconURL As String)
    ' _pageKey = pPageKey
    _treePath = pTreePath
    _shortTitle = pShortTitle
    _longTitle = pLongTitle
    _iconUrl = pIconURL
  End Sub

  Public Overrides Function ToString() As String
    Return "PropertyPage: " + _shortTitle
  End Function
End Class




'-->
'--> ActionHandler

<AttributeUsage(AttributeTargets.Class Or AttributeTargets.Module, AllowMultiple:=False)> Public Class FileActionClassAttribute
  Inherits Attribute
End Class

<AttributeUsage(AttributeTargets.Method, AllowMultiple:=False)> Public Class FileActionAttribute
  Inherits Attribute
  Private _buttonText, _iconURL, _fileTypes() As String

  ''' <param name="fileTypes">Dateitypen, für die diese Aktion angezeigt wird. Bsp.: "*.txt;*.rtf;*.vb" ODER alle Dateien "*"</param>
  ''' <param name="ButtonText">Beschriftung</param>
  ''' <param name="IconURL">WebURL (http://...), Lokale Datei, Ressource (projektname.iconname.png)</param>
  Public Sub New(ByVal fileTypes As String, ByVal ButtonText As String, ByVal IconURL As String)
    Me._buttonText = ButtonText
    Me._iconURL = IconURL
    Dim parts() = Split(fileTypes, ";")
    ReDim _fileTypes(-1)
    For Each part In parts
      If part.StartsWith("*.") = False Then Continue For
      ReDim Preserve _fileTypes(_fileTypes.Length)
      _fileTypes(_fileTypes.Length - 1) = part.Substring(1).ToLower
    Next
  End Sub


  Private _keyString As String
  Public Property HandlesKeyString() As String
    Get
      Return _keyString
    End Get
    Set(ByVal value As String)
      _keyString = value
    End Set
  End Property


  Public ReadOnly Property ButtonText() As String
    Get
      Return _buttonText
    End Get
  End Property
  Public ReadOnly Property IconURL() As String
    Get
      Return _iconURL
    End Get
  End Property
  Public ReadOnly Property FileTypes() As String()
    Get
      Return _fileTypes
    End Get
  End Property

  Public Overrides Function ToString() As String
    Return "FileAction: " + ButtonText + " (" + Join(FileTypes, "|") + ")"
  End Function
End Class





'-->
'--> ProtocolHandler

Public Interface IProtocolHandler

  Function ReadFile(ByVal internalURL As String) As String
  Sub SaveFile(ByVal internalURL As String, ByVal data As String)

  Sub NavigateFilelistTo(ByVal internalURL As String)
  Sub ShowFilelist()

  Function MapToLocalFilename(ByVal internalURL As String) As String

End Interface

Public Interface IProtocolHandler2
  Inherits IProtocolHandler

  Function ShowSaveAsDialog(ByVal windowTitle As String, ByVal preselectURL As String, ByRef saveAsURL As String, ByVal custom As Object, ByVal flags As Integer) As Boolean
  Function GetFilelistForm() As System.Windows.Forms.Form
  Function RenameFile(ByVal oldInternalURL As String, ByVal newInternalURL As String) As Boolean
  Function DeleteFile(ByVal internalURL As String) As Boolean
  Function FileExists(ByVal internalURL As String) As Boolean
  Function FolderExists(ByVal internalFolderURL As String) As Boolean
  Function GetFileList(ByVal internalFolderURL As String) As String()

End Interface

<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False)> Public Class ProtocolHandlerAttribute
  Inherits Attribute

  Public ProtocolName As String

  Public Sub New(ByVal pProtocolName As String)
    Me.ProtocolName = pProtocolName
  End Sub

  Private p_ForceFileHandler As Type
  Public Property ForceFileHandler() As Type
    Get
      Return p_ForceFileHandler
    End Get
    Set(ByVal value As Type)
      p_ForceFileHandler = value
    End Set
  End Property

  Public Overrides Function ToString() As String
    Return "ProtocolHandler: " + ProtocolName
  End Function
End Class



''-->
''--> FiletypeHandler

'''' <summary>
'''' Interface für FiletypeHandler, die auf frmDC_scintilla aufsetzen
'''' </summary>
'''' <remarks>siehe FiletypeHandlerAttribute</remarks>
'Public Interface IScintillaFiletypeHandler

'  Sub OpenedWithScintilla(ByVal InternalURI As String, ByVal sc As ScintillaNet.Scintilla, ByVal dockContentForm As IDockContentForm)

'End Interface

''' <summary>
''' Interface für FiletypeHandler ohne DockContent
''' </summary>
''' <remarks>siehe FiletypeHandlerAttribute</remarks>
Public Interface IPlainFiletypeHandler

  Sub NavigateFile(ByVal InternalURI As String)

End Interface


''' <summary>
''' frmDC_scintilla: dieses Attr. und Implements IFiletypeHandler
''' Eigene Oberfläche: dieses Attr., Inherits DockContent und Implements IDockContentForm
''' ohne Oberfläche: dieses Attr., Inherits DockContent und Implements IDockContentForm
''' </summary>
<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False)> Public Class FiletypeHandlerAttribute
  Inherits Attribute


  Private _fileExtensions As String()
  Public ReadOnly Property FileExtensions() As String()
    Get
      Return _fileExtensions
    End Get
  End Property


  Private _useScintilla As Boolean
  Public ReadOnly Property UseScintilla() As Boolean
    Get
      Return _useScintilla
    End Get
  End Property


  Public Sub New(ByVal pFileExtensions() As String, ByVal pUseScintilla As Boolean)
    For i = 0 To pFileExtensions.Length - 1
      pFileExtensions(i) = pFileExtensions(i).Trim("*", " ").ToLower
    Next
    _fileExtensions = pFileExtensions
    _useScintilla = pUseScintilla
  End Sub

  Public Overrides Function ToString() As String
    Return "FiletypeHandler: " + Join(FileExtensions, ";")
  End Function
End Class

