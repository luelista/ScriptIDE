Imports System
Imports System.IO
Imports System.Collections
Imports Be.Windows.Forms

  ''' <summary>
  ''' Byte provider for (big) files.
  ''' </summary>
  Public Class FriendlyFileByteProvider
    Implements IByteProvider
    Implements IDisposable
#Region "WriteCollection class"
    ''' <summary>
    ''' Represents the write buffer class
    ''' </summary>
    Private Class WriteCollection
      Inherits DictionaryBase
      ''' <summary>
      ''' Gets or sets a byte in the collection
      ''' </summary>
      Default Public Property Item(ByVal index As Long) As Byte
        Get
          Return CByte(Me.Dictionary(index))
        End Get
        Set(ByVal value As Byte)
          Dictionary(index) = value
        End Set
      End Property

      ''' <summary>
      ''' Adds a byte into the collection
      ''' </summary>
      ''' <param name="index">the index of the byte</param>
      ''' <param name="value">the value of the byte</param>
      Public Sub Add(ByVal index As Long, ByVal value As Byte)
        Dictionary.Add(index, value)
      End Sub

      ''' <summary>
      ''' Determines if a byte with the given index exists.
      ''' </summary>
      ''' <param name="index">the index of the byte</param>
      ''' <returns>true, if the is in the collection</returns>
      Public Function Contains(ByVal index As Long) As Boolean
        Return Dictionary.Contains(index)
      End Function

    End Class
#End Region

    ''' <summary>
    ''' Occurs, when the write buffer contains new changes.
    ''' </summary>
    Public Event Changed As EventHandler Implements IByteProvider.Changed

    ''' <summary>
    ''' Contains all changes
    ''' </summary>
    Private _writes As New WriteCollection()

    ''' <summary>
    ''' Contains the file name.
    ''' </summary>
    Private _fileName As String
    ''' <summary>
    ''' Contains the file stream.
    ''' </summary>
    Private _fileStream As FileStream
    ''' <summary>
    ''' Read-only access.
    ''' </summary>
    Private _readOnly As Boolean

    ''' <summary>
    ''' Initializes a new instance of the FileByteProvider class.
    ''' </summary>
    ''' <param name="fileName"></param>
    Public Sub New(ByVal fileName As String)
      _fileName = fileName

      Try
        ' try to open in write mode
      _fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite Or FileShare.Delete)
      Catch
        ' write mode failed, try to open in read-only and fileshare friendly mode.
        Try
        _fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite Or FileShare.Delete)
          _readOnly = True
        Catch
          Throw
        End Try
      End Try
    End Sub

    ''' <summary>
    ''' Terminates the instance of the FileByteProvider class.
    ''' </summary>
    Protected Overrides Sub Finalize()
      Try
        Dispose()
      Finally
        MyBase.Finalize()
      End Try
    End Sub

    ''' <summary>
    ''' Raises the Changed event.
    ''' </summary>
    ''' <remarks>Never used.</remarks>
    Private Sub OnChanged(ByVal e As EventArgs)
      RaiseEvent Changed(Me, e)
    End Sub

    ''' <summary>
    ''' Gets the name of the file the byte provider is using.
    ''' </summary>
    Public ReadOnly Property FileName() As String
      Get
        Return _fileName
      End Get
    End Property

    ''' <summary>
    ''' Returns a value if there are some changes.
    ''' </summary>
    ''' <returns>true, if there are some changes</returns>
    Public Function HasChanges() As Boolean Implements IByteProvider.HasChanges
      Return (_writes.Count > 0)
    End Function

    ''' <summary>
    ''' Updates the file with all changes the write buffer contains.
    ''' </summary>
    Public Sub ApplyChanges() Implements IByteProvider.ApplyChanges
      If Me._readOnly Then
        Throw New Exception("File is in read-only mode.")
      End If

      If Not HasChanges() Then
        Exit Sub
      End If

      Dim en As IDictionaryEnumerator = _writes.GetEnumerator()
      While en.MoveNext()
        Dim index As Long = CLng(en.Key)
        Dim value As Byte = CByte(en.Value)
        If _fileStream.Position <> index Then
          _fileStream.Position = index
        End If
        _fileStream.Write(New Byte() {value}, 0, 1)
      End While
      _writes.Clear()
    End Sub

    ''' <summary>
    ''' Clears the write buffer and reject all changes made.
    ''' </summary>
    Public Sub RejectChanges()
      _writes.Clear()
    End Sub

#Region "IByteProvider Members"
    ''' <summary>
    ''' Never used.
    ''' </summary>
    Public Event LengthChanged As EventHandler Implements IByteProvider.LengthChanged

    ''' <summary>
    ''' Reads a byte from the file.
    ''' </summary>
    ''' <param name="index">the index of the byte to read</param>
    ''' <returns>the byte</returns>
    Public Function ReadByte(ByVal index As Long) As Byte Implements IByteProvider.ReadByte
      If _writes.Contains(index) Then
        Return _writes(index)
      End If

      If _fileStream.Position <> index Then
        _fileStream.Position = index
      End If

      Dim res As Byte = CByte(_fileStream.ReadByte())
      Return res
    End Function

    ''' <summary>
    ''' Gets the length of the file.
    ''' </summary>
    Public ReadOnly Property Length() As Long Implements IByteProvider.Length
      Get
        Return _fileStream.Length
      End Get
    End Property

    ''' <summary>
    ''' Writes a byte into write buffer
    ''' </summary>
    Public Sub WriteByte(ByVal index As Long, ByVal value As Byte) Implements IByteProvider.WriteByte
      If _writes.Contains(index) Then
        _writes(index) = value
      Else
        _writes.Add(index, value)
      End If

      OnChanged(EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' Not supported
    ''' </summary>
    Public Sub DeleteBytes(ByVal index As Long, ByVal length As Long) Implements IByteProvider.DeleteBytes
      Throw New NotSupportedException("FileByteProvider.DeleteBytes")
    End Sub

    ''' <summary>
    ''' Not supported
    ''' </summary>
    Public Sub InsertBytes(ByVal index As Long, ByVal bs As Byte()) Implements IByteProvider.InsertBytes
      Throw New NotSupportedException("FileByteProvider.InsertBytes")
    End Sub

    ''' <summary>
    ''' Returns true
    ''' </summary>
    Public Function SupportsWriteByte() As Boolean Implements IByteProvider.SupportsWriteByte
      Return Not _readOnly
    End Function

    ''' <summary>
    ''' Returns false
    ''' </summary>
    Public Function SupportsInsertBytes() As Boolean Implements IByteProvider.SupportsInsertBytes
      Return False
    End Function

    ''' <summary>
    ''' Returns false
    ''' </summary>
    Public Function SupportsDeleteBytes() As Boolean Implements IByteProvider.SupportsDeleteBytes
      Return False
    End Function
#End Region

#Region "IDisposable Members"
    ''' <summary>
    ''' Releases the file handle used by the FileByteProvider.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
      If _fileStream IsNot Nothing Then
        _fileName = Nothing

        _fileStream.Close()
        _fileStream = Nothing
      End If

      GC.SuppressFinalize(Me)
    End Sub
#End Region
  End Class
