Module sys_rc4encryption

  Public Function RC4StringEncrypt(ByVal str As String, ByVal key As Byte()) As String
    Dim bytes = System.Text.Encoding.Default.GetBytes(str)
    RC4(bytes, key)
    Dim out As New System.Text.StringBuilder(str.Length * 2)
    For i = 0 To bytes.Length - 1
      out.Append(If(bytes(i) And &HF0, "", "0") + Hex(bytes(i)))
    Next
    Return out.ToString
  End Function
  Public Function RC4StringDecrypt(ByVal strHex As String, ByVal key As Byte()) As String
    If strHex.Length Mod 2 <> 0 Then Return ""
    Dim bytes(strHex.Length / 2 - 1) As Byte
    For i = 0 To bytes.Length - 1
      bytes(i) = Convert.ToByte(strHex.Substring(i * 2, 2), 16)
    Next
    RC4(bytes, key)
    Return System.Text.Encoding.Default.GetString(bytes)
  End Function

  Public Sub RC4(ByRef bytes As [Byte](), ByVal key As [Byte]())
    Dim s As [Byte]() = New [Byte](255) {}
    Dim k As [Byte]() = New [Byte](255) {}
    Dim temp As [Byte]
    Dim i As Integer, j As Integer

    For i = 0 To 255
      s(i) = CInt(i)
      k(i) = key(i Mod key.GetLength(0))
    Next

    j = 0
    For i = 0 To 255
      j = (j + s(i) + k(i)) Mod 256
      temp = s(i)
      s(i) = s(j)
      s(j) = temp
    Next

    i = InlineAssignHelper(j, 0)
    For x As Integer = 0 To bytes.GetLength(0) - 1
      i = (i + 1) Mod 256
      j = (j + s(i)) Mod 256
      temp = s(i)
      s(i) = s(j)
      s(j) = temp
      Dim t As Integer = (CInt(s(i)) + CInt(s(j))) Mod 256
      bytes(x) = bytes(x) Xor s(t)
    Next
  End Sub
  Private Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
    target = value
    Return value
  End Function

End Module
