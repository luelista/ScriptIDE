' <file>
'     <copyright see="prj:///doc/copyright.txt"/>
'     <license see="prj:///doc/license.txt"/>
'     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
'     <version>$Revision: 3786 $</version>
' </file>

Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization

''' <summary>
''' This interface flags an object beeing "mementocapable". This means that the
''' state of the object could be saved to an <see cref="Properties"/> object
''' and set from a object from the same class.
''' This is used to save and restore the state of GUI objects.
''' </summary>
Public Interface IMementoCapable
  ''' <summary>
  ''' Creates a new memento from the state.
  ''' </summary>
  Function CreateMemento() As Properties

  ''' <summary>
  ''' Sets the state to the given memento.
  ''' </summary>
  Sub SetMemento(ByVal memento As Properties)
End Interface

''' <summary>
''' Description of PropertyGroup.
''' </summary>
Public Class Properties
  ''' <summary> Needed for support of late deserialization </summary>
  Private Class SerializedValue
    Private m_content As String

    Public ReadOnly Property Content() As String
      Get
        Return m_content
      End Get
    End Property

    Public Function Deserialize(Of T)() As T
      Dim serializer As New XmlSerializer(GetType(T))
      Return DirectCast(serializer.Deserialize(New StringReader(m_content)), T)
    End Function

    Public Sub New(ByVal content As String)
      Me.m_content = content
    End Sub
  End Class

  Private properties As New Dictionary(Of String, Object)()

  Default Public Property Item(ByVal [property] As String) As String
    Get
      Return Convert.ToString([Get]([property]), CultureInfo.InvariantCulture)
    End Get
    Set(ByVal value As String)
      [Set]([property], Value)
    End Set
  End Property

  Public ReadOnly Property Elements() As String()
    Get
      SyncLock properties
        Dim ret As New List(Of String)()
        For Each [property] As KeyValuePair(Of String, Object) In properties
          ret.Add([property].Key)
        Next
        Return ret.ToArray()
      End SyncLock
    End Get
  End Property

  Public Function [Get](ByVal [property] As String) As Object
    SyncLock properties
      Dim val As Object
      properties.TryGetValue([property], val)
      Return val
    End SyncLock
  End Function

  Public Sub [Set](Of T)(ByVal [property] As String, ByVal value As T)
    Dim oldValue As T = Nothing
    SyncLock properties
      If Not properties.ContainsKey([property]) Then
        properties.Add([property], value)
      Else
        oldValue = [Get](Of T)([property], value)
        properties([property]) = value
      End If
    End SyncLock
    'OnPropertyChanged(New PropertyChangedEventArgs(Me, [property], oldValue, value))
  End Sub

  Public Function Contains(ByVal [property] As String) As Boolean
    SyncLock properties
      Return properties.ContainsKey([property])
    End SyncLock
  End Function

  Public ReadOnly Property Count() As Integer
    Get
      SyncLock properties
        Return properties.Count
      End SyncLock
    End Get
  End Property

  Public Function Remove(ByVal [property] As String) As Boolean
    SyncLock properties
      Return properties.Remove([property])
    End SyncLock
  End Function

  Public Overloads Overrides Function ToString() As String
    SyncLock properties
      Dim sb As New StringBuilder()
      sb.Append("[Properties:{")
      For Each entry As KeyValuePair(Of String, Object) In properties
        sb.Append(entry.Key)
        sb.Append("=")
        sb.Append(entry.Value)
        sb.Append(",")
      Next
      sb.Append("}]")
      Return sb.ToString()
    End SyncLock
  End Function

  Public Shared Function ReadFromAttributes(ByVal reader As XmlReader) As Properties
    Dim properties As New Properties()
    If reader.HasAttributes Then
      For i As Integer = 0 To reader.AttributeCount - 1
        reader.MoveToAttribute(i)
        properties(reader.Name) = reader.Value
      Next
      'Moves the reader back to the element node.
      reader.MoveToElement()
    End If
    Return properties
  End Function

  Friend Sub ReadProperties(ByVal reader As XmlReader, ByVal endElement As String)
    If reader.IsEmptyElement Then
      Return
    End If
    While reader.Read()
      Select Case reader.NodeType
        Case XmlNodeType.EndElement
          If reader.LocalName = endElement Then
            Return
          End If
          Exit Select
        Case XmlNodeType.Element
          Dim propertyName As String = reader.LocalName
          If propertyName = "Properties" Then
            propertyName = reader.GetAttribute(0)
            Dim p As New Properties()
            p.ReadProperties(reader, "Properties")
            properties(propertyName) = p
          ElseIf propertyName = "Array" Then
            propertyName = reader.GetAttribute(0)
            properties(propertyName) = ReadArray(reader)
          ElseIf propertyName = "SerializedValue" Then
            propertyName = reader.GetAttribute(0)
            properties(propertyName) = New SerializedValue(reader.ReadInnerXml())
          Else
            properties(propertyName) = If(reader.HasAttributes, reader.GetAttribute(0), Nothing)
          End If
          Exit Select
      End Select
    End While
  End Sub

  Private Function ReadArray(ByVal reader As XmlReader) As ArrayList
    If reader.IsEmptyElement Then
      Return New ArrayList(0)
    End If
    Dim l As New ArrayList()
    While reader.Read()
      Select Case reader.NodeType
        Case XmlNodeType.EndElement
          If reader.LocalName = "Array" Then
            Return l
          End If
          Exit Select
        Case XmlNodeType.Element
          l.Add(If(reader.HasAttributes, reader.GetAttribute(0), Nothing))
          Exit Select
      End Select
    End While
    Return l
  End Function

  Public Sub WriteProperties(ByVal writer As XmlWriter)
    SyncLock properties
      Dim sortedProperties As New List(Of KeyValuePair(Of String, Object))(properties)
      sortedProperties.Sort(Function(a, b) StringComparer.OrdinalIgnoreCase.Compare(a.Key, b.Key))
      For Each entry As KeyValuePair(Of String, Object) In sortedProperties
        Dim val As Object = entry.Value
        If TypeOf val Is Properties Then
          writer.WriteStartElement("Properties")
          writer.WriteAttributeString("name", entry.Key)
          DirectCast(val, Properties).WriteProperties(writer)
          writer.WriteEndElement()
        ElseIf TypeOf val Is Array OrElse TypeOf val Is ArrayList Then
          writer.WriteStartElement("Array")
          writer.WriteAttributeString("name", entry.Key)
          For Each o As Object In DirectCast(val, IEnumerable)
            writer.WriteStartElement("Element")
            WriteValue(writer, o)
            writer.WriteEndElement()
          Next
          writer.WriteEndElement()
        ElseIf TypeDescriptor.GetConverter(val).CanConvertFrom(GetType(String)) Then
          writer.WriteStartElement(entry.Key)
          WriteValue(writer, val)
          writer.WriteEndElement()
        ElseIf TypeOf val Is SerializedValue Then
          writer.WriteStartElement("SerializedValue")
          writer.WriteAttributeString("name", entry.Key)
          writer.WriteRaw(DirectCast(val, SerializedValue).Content)
          writer.WriteEndElement()
        Else
          writer.WriteStartElement("SerializedValue")
          writer.WriteAttributeString("name", entry.Key)
          Dim serializer As New XmlSerializer(val.[GetType]())
          serializer.Serialize(writer, val, Nothing)
          writer.WriteEndElement()
        End If
      Next
    End SyncLock
  End Sub

  Private Sub WriteValue(ByVal writer As XmlWriter, ByVal val As Object)
    If val IsNot Nothing Then
      If TypeOf val Is String Then
        writer.WriteAttributeString("value", val.ToString())
      Else
        Dim c As TypeConverter = TypeDescriptor.GetConverter(val.[GetType]())
        writer.WriteAttributeString("value", c.ConvertToInvariantString(val))
      End If
    End If
  End Sub

  Public Sub Save(ByVal fileName As String)
    Using writer As New XmlTextWriter(fileName, Encoding.UTF8)
      writer.Formatting = Formatting.Indented
      writer.WriteStartElement("Properties")
      WriteProperties(writer)
      writer.WriteEndElement()
    End Using
  End Sub

  '		public void BinarySerialize(BinaryWriter writer)
  '		{
  '			writer.Write((byte)properties.Count);
  '			foreach (KeyValuePair<string, object> entry in properties) {
  '				writer.Write(AddInTree.GetNameOffset(entry.Key));
  '				writer.Write(AddInTree.GetNameOffset(entry.Value.ToString()));
  '			}
  '		}

  Public Shared Function Load(ByVal fileName As String) As Properties
    If Not File.Exists(fileName) Then
      Return Nothing
    End If
    Using reader As New XmlTextReader(fileName)
      While reader.Read()
        If reader.IsStartElement() Then
          Select Case reader.LocalName
            Case "Properties"
              Dim properties As New Properties()
              properties.ReadProperties(reader, "Properties")
              Return properties
          End Select
        End If
      End While
    End Using
    Return Nothing
  End Function

  Public Function [Get](Of T)(ByVal [property] As String, ByVal defaultValue As T) As T
    SyncLock properties
      Dim o As Object
      If Not properties.TryGetValue([property], o) Then
        properties.Add([property], defaultValue)
        Return defaultValue
      End If

      If TypeOf o Is String AndAlso GetType(T) IsNot GetType(String) Then
        Dim c As TypeConverter = TypeDescriptor.GetConverter(GetType(T))
        Try
          o = c.ConvertFromInvariantString(o.ToString())
        Catch ex As Exception
          MsgBox("Error loading property '" & [property] & "': " & ex.Message)
          o = defaultValue
        End Try
        ' store for future look up
        properties([property]) = o
      ElseIf TypeOf o Is ArrayList AndAlso GetType(T).IsArray Then
        Dim list As ArrayList = DirectCast(o, ArrayList)
        Dim elementType As Type = GetType(T).GetElementType()
        Dim arr As Array = System.Array.CreateInstance(elementType, list.Count)
        Dim c As TypeConverter = TypeDescriptor.GetConverter(elementType)
        Try
          For i As Integer = 0 To arr.Length - 1
            If list(i) IsNot Nothing Then
              arr.SetValue(c.ConvertFromInvariantString(list(i).ToString()), i)
            End If
          Next
          o = arr
        Catch ex As Exception
          MsgBox("Error loading property '" & [property] & "': " & ex.Message)
          o = defaultValue
        End Try
        ' store for future look up
        properties([property]) = o
      ElseIf Not (TypeOf o Is String) AndAlso GetType(T) Is GetType(String) Then
        Dim c As TypeConverter = TypeDescriptor.GetConverter(GetType(T))
        If c.CanConvertTo(GetType(String)) Then
          o = c.ConvertToInvariantString(o)
        Else
          o = o.ToString()
        End If
      ElseIf TypeOf o Is SerializedValue Then
        Try
          o = DirectCast(o, SerializedValue).Deserialize(Of T)()
        Catch ex As Exception
          MsgBox("Error loading property '" & [property] & "': " & ex.Message)
          o = defaultValue
        End Try
        ' store for future look up
        properties([property]) = o
      End If
      Try
        Return DirectCast(o, T)
      Catch generatedExceptionName As NullReferenceException
        ' can happen when configuration is invalid -> o is null and a value type is expected
        Return defaultValue
      End Try
    End SyncLock
  End Function

  Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
    RaiseEvent PropertyChanged(Me, e)
  End Sub

  Public Event PropertyChanged As PropertyChangedEventHandler
End Class

