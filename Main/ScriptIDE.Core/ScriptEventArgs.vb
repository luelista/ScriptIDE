<Microsoft.VisualBasic.ComClass()> Public Class ScriptEventArgs

  Private _eventName As String
  Public ReadOnly Property EventName() As String
    Get
      Return _eventName
    End Get
  End Property

  Private _className As String
  Public ReadOnly Property ClassName() As String
    Get
      Return _className
    End Get
  End Property

  Private _ID As String
  Public Property ID() As String
    Get
      Return _ID
    End Get
    Set(ByVal value As String)
      _ID = value
    End Set
  End Property

  Private _controlType As String
  Public Property ControlType() As String
    Get
      Return _controlType
    End Get
    Set(ByVal value As String)
      _controlType = value
    End Set
  End Property

  Private _sender As Object
  Public ReadOnly Property Sender() As Object
    Get
      Return _sender
    End Get
  End Property

  Private _mouseButton As String
  Public ReadOnly Property MouseButton() As Object
    Get
      Return _mouseButton
    End Get
  End Property

  Private _keyString As String
  Public ReadOnly Property KeyString() As String
    Get
      Return _keyString
    End Get
  End Property

  Private _mouseX As Integer
  Public ReadOnly Property MouseX() As Object
    Get
      Return _mouseX
    End Get
  End Property

  Private _mouseY As Integer
  Public ReadOnly Property MouseY() As Object
    Get
      Return _mouseY
    End Get
  End Property

  Private _cancel As Boolean
  Public Property Cancel() As Boolean
    Get
      Return _cancel
    End Get
    Set(ByVal value As Boolean)
      _cancel = value
    End Set
  End Property

  Private _menu As Object
  Public Property Menu() As Object
    Get
      Return _menu
    End Get
    Set(ByVal value As Object)
      _menu = value
    End Set
  End Property

  Public Sub New(Optional ByVal eventN As String = Nothing, Optional ByVal send As Object = Nothing, Optional ByVal mouseBtn As String = Nothing, Optional ByVal mouseX As Integer = Nothing, Optional ByVal mouseY As Integer = Nothing, Optional ByVal keyStr As String = Nothing, Optional ByVal className As String = Nothing)
    _ID = CStr(send.Name)
    _eventName = eventN
    _sender = send
    _mouseButton = mouseBtn
    _mouseX = mouseX
    _mouseY = mouseY
    _keyString = keyStr
    _className = className
  End Sub

End Class
