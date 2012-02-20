Public Interface IAddinConnect

  Sub Connect(ByVal application As Object, ByVal connectMode As ConnectMode, ByVal addInInst As AddinInstance, ByRef custom As Object)
  Sub Disconnect(ByVal removeMode As DisconnectMode, ByRef custom As Object)

  Sub OnNavigate(ByVal kind As NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object)
  Function GetAddinWindow(ByVal PersistString As String) As Form

  Sub OnStartupComplete(ByRef custom As Object)
  Sub OnBeforeShutdown(ByRef cancel As Boolean, ByRef custom As Object)
  Sub OnAddinUpdate(ByVal addinChanged As String, ByRef custom As Object)

End Interface

Public Enum DisconnectMode
  IdeShutdown

End Enum

Public Enum ConnectMode
  AfterStartup
  Startup
  External
  CommandLine
  Solution
  UISetup
End Enum

<AttributeUsage(AttributeTargets.Class, allowmultiple:=False)> Public Class SIAAttribute
  Inherits Attribute

  Public Description, Title As String

  Public Sub New(ByVal AddinTitle As String, ByVal AddinDescription As String)
    Title = AddinTitle
    Description = AddinDescription
  End Sub

  Private p_iconResName As String
  Public Property IconRessourceName() As String
    Get
      Return p_iconResName
    End Get
    Set(ByVal value As String)
      p_iconResName = value
    End Set
  End Property

  Public Overrides Function ToString() As String
    Return Description
  End Function

End Class
