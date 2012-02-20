Public Class StringHelper
  Private Sub New()
  End Sub


  Public Shared Function CountChars(ByVal str As String, ByVal chr As Char) As Integer
    CountChars = 0
    For i = 0 To str.Length - 1
      If str.Chars(i) = chr Then CountChars += 1
    Next
  End Function



End Class
