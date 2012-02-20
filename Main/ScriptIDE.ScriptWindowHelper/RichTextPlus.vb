Public Class RichTextPlus
  Inherits RichTextBox

  Private _htmlCode As String
  Public Property HTMLCode() As String
    Get
      Return _htmlCode
    End Get
    Set(ByVal value As String)
      _htmlCode = value
      Me.DetectUrls = False
      zoomRTF(Me, value)
    End Set
  End Property



End Class
