' Compress/decompress byte array

Imports System
Imports System.Collections.Generic
Imports System.IO.Compression
Imports System.IO
Imports System.Collections

Namespace Utilities
  Class Compression
    Public Shared Function Compress(ByVal data As Byte()) As Byte()
      Dim ms As New MemoryStream()
      Dim ds As New DeflateStream(ms, CompressionMode.Compress)
      ds.Write(data, 0, data.Length)
      ds.Flush()
      ds.Close()
      Return ms.ToArray()
    End Function
    Public Shared Function Decompress(ByVal data As Byte()) As Byte()
      Const BUFFER_SIZE As Integer = 256
      Dim tempArray As Byte() = New Byte(BUFFER_SIZE - 1) {}
      Dim tempList As New List(Of Byte())()
      Dim count As Integer = 0, length As Integer = 0

      Dim ms As New MemoryStream(data)
      Dim ds As New DeflateStream(ms, CompressionMode.Decompress)

      count = ds.Read(tempArray, 0, BUFFER_SIZE)
      While count > 0
        If count = BUFFER_SIZE Then
          tempList.Add(tempArray)
          tempArray = New Byte(BUFFER_SIZE - 1) {}
        Else
          Dim temp As Byte() = New Byte(count - 1) {}
          Array.Copy(tempArray, 0, temp, 0, count)
          tempList.Add(temp)
        End If
        length += count

        count = ds.Read(tempArray, 0, BUFFER_SIZE)
      End While

      Dim retVal As Byte() = New Byte(length - 1) {}

      count = 0
      For Each temp As Byte() In tempList
        Array.Copy(temp, 0, retVal, count, temp.Length)
        count += temp.Length
      Next

      Return retVal
    End Function
  End Class
End Namespace
