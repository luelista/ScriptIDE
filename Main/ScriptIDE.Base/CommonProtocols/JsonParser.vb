Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Text

Namespace fastJSON
  ''' <summary>
  ''' This class encodes and decodes JSON strings.
  ''' Spec. details, see http://www.json.org/
  ''' 
  ''' JSON uses Arrays and Objects. These correspond here to the datatypes ArrayList and Hashtable.
  ''' All numbers are parsed to doubles.
  ''' </summary>
  Friend Class JsonParser
    Private Const TOKEN_NONE As Integer = 0
    Private Const TOKEN_CURLY_OPEN As Integer = 1
    Private Const TOKEN_CURLY_CLOSE As Integer = 2
    Private Const TOKEN_SQUARED_OPEN As Integer = 3
    Private Const TOKEN_SQUARED_CLOSE As Integer = 4
    Private Const TOKEN_COLON As Integer = 5
    Private Const TOKEN_COMMA As Integer = 6
    Private Const TOKEN_STRING As Integer = 7
    Private Const TOKEN_NUMBER As Integer = 8
    Private Const TOKEN_TRUE As Integer = 9
    Private Const TOKEN_FALSE As Integer = 10
    Private Const TOKEN_NULL As Integer = 11

    ''' <summary>
    ''' Parses the string json into a value
    ''' </summary>
    ''' <param name="json">A JSON string.</param>
    ''' <returns>An ArrayList, a dictionary, a double, a string, null, true, or false</returns>
    Friend Shared Function JsonDecode(ByVal json As String) As Object
      Dim success As Boolean = True

      Return JsonDecode(json, success)
    End Function

    ''' <summary>
    ''' Parses the string json into a value; and fills 'success' with the successfullness of the parse.
    ''' </summary>
    ''' <param name="json">A JSON string.</param>
    ''' <param name="success">Successful parse?</param>
    ''' <returns>An ArrayList, a Hashtable, a double, a string, null, true, or false</returns>
    Private Shared Function JsonDecode(ByVal json As String, ByRef success As Boolean) As Object
      success = True
      If json IsNot Nothing Then
        Dim charArray As Char() = json.ToCharArray()
        Dim index As Integer = 0
        Dim value As Object = ParseValue(charArray, index, success)
        Return value
      Else
        Return Nothing
      End If
    End Function


    Protected Shared Function ParseObject(ByVal json As Char(), ByRef index As Integer, ByRef success As Boolean) As Dictionary(Of String, Object)
      Dim table As New Dictionary(Of String, Object)()
      Dim token As Integer

      ' {
      NextToken(json, index)

      Dim done As Boolean = False
      While Not done
        token = LookAhead(json, index)
        If token = TOKEN_NONE Then
          success = False
          Return Nothing
        ElseIf token = TOKEN_COMMA Then
          NextToken(json, index)
        ElseIf token = TOKEN_CURLY_CLOSE Then
          NextToken(json, index)
          Return table
        Else

          ' name
          Dim name As String = ParseString(json, index, success)
          If Not success Then
            success = False
            Return Nothing
          End If

          ' :
          token = NextToken(json, index)
          If token <> TOKEN_COLON Then
            success = False
            Return Nothing
          End If

          ' value
          Dim value As Object = ParseValue(json, index, success)
          If Not success Then
            success = False
            Return Nothing
          End If

          table(name) = value
        End If
      End While

      Return table
    End Function

    Protected Shared Function ParseArray(ByVal json As Char(), ByRef index As Integer, ByRef success As Boolean) As ArrayList
      Dim array As New ArrayList()

      NextToken(json, index)

      Dim done As Boolean = False
      While Not done
        Dim token As Integer = LookAhead(json, index)
        If token = TOKEN_NONE Then
          success = False
          Return Nothing
        ElseIf token = TOKEN_COMMA Then
          NextToken(json, index)
        ElseIf token = TOKEN_SQUARED_CLOSE Then
          NextToken(json, index)
          Exit While
        Else
          Dim value As Object = ParseValue(json, index, success)
          If Not success Then
            Return Nothing
          End If

          array.Add(value)
        End If
      End While

      Return array
    End Function

    Protected Shared Function ParseValue(ByVal json As Char(), ByRef index As Integer, ByRef success As Boolean) As Object
      Select Case LookAhead(json, index)
        Case TOKEN_NUMBER
          Return ParseNumber(json, index, success)
        Case TOKEN_STRING
          Return ParseString(json, index, success)
        Case TOKEN_CURLY_OPEN
          Return ParseObject(json, index, success)
        Case TOKEN_SQUARED_OPEN
          Return ParseArray(json, index, success)
        Case TOKEN_TRUE
          NextToken(json, index)
          Return True
        Case TOKEN_FALSE
          NextToken(json, index)
          Return False
        Case TOKEN_NULL
          NextToken(json, index)
          Return Nothing
        Case TOKEN_NONE
          Exit Select
      End Select

      success = False
      Return Nothing
    End Function

    Protected Shared Function ParseString(ByVal json As Char(), ByRef index As Integer, ByRef success As Boolean) As String
      Dim s As New StringBuilder()
      Dim c As Char

      EatWhitespace(json, index)

      ' "
      c = json(index) : index += 1

      Dim complete As Boolean = False
      While Not complete

        If index = json.Length Then
          Exit While
        End If

        c = json(index) : index += 1
        If c = """"c Then
          complete = True
          Exit While
        ElseIf c = "\"c Then

          If index = json.Length Then
            Exit While
          End If
          c = json(index) : index += 1
          If c = """"c Then
            s.Append(""""c)
          ElseIf c = "\"c Then
            s.Append("\"c)
          ElseIf c = "/"c Then
            s.Append("/"c)
          ElseIf c = "b"c Then
            s.Append(ControlChars.Back)
          ElseIf c = "f"c Then
            s.Append(ControlChars.FormFeed)
          ElseIf c = "n"c Then
            s.Append(ControlChars.Lf)
          ElseIf c = "r"c Then
            s.Append(ControlChars.Cr)
          ElseIf c = "t"c Then
            s.Append(ControlChars.Tab)
          ElseIf c = "u"c Then
            Dim remainingLength As Integer = json.Length - index
            If remainingLength >= 4 Then
              ' parse the 32 bit hex into an integer codepoint
              Dim codePoint As UInteger
              If Not (InlineAssignHelper(success, UInt32.TryParse(New String(json, index, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, codePoint))) Then
                Return ""
              End If
              ' convert the integer codepoint to a unicode char and add to string
              s.Append([Char].ConvertFromUtf32(CInt(codePoint)))
              ' skip 4 chars
              index += 4
            Else
              Exit While
            End If

          End If
        Else
          s.Append(c)

        End If
      End While

      If Not complete Then
        success = False
        Return Nothing
      End If

      Return s.ToString()
    End Function

    Protected Shared Function ParseNumber(ByVal json As Char(), ByRef index As Integer, ByRef success As Boolean) As String
      EatWhitespace(json, index)

      Dim lastIndex As Integer = GetLastIndexOfNumber(json, index)
      Dim charLength As Integer = (lastIndex - index) + 1

      Dim number As New String(json, index, charLength)
      success = True

      index = lastIndex + 1
      Return number
    End Function

    Protected Shared Function GetLastIndexOfNumber(ByVal json As Char(), ByVal index As Integer) As Integer
      Dim lastIndex As Integer

      For lastIndex = index To json.Length - 1
        If "0123456789+-.eE".IndexOf(json(lastIndex)) = -1 Then
          Exit For
        End If
      Next
      Return lastIndex - 1
    End Function

    Protected Shared Sub EatWhitespace(ByVal json As Char(), ByRef index As Integer)
      While index < json.Length
        If " " & vbTab & vbLf & vbCr.IndexOf(json(index)) = -1 Then
          Exit While
        End If
        index += 1
      End While
    End Sub

    Protected Shared Function LookAhead(ByVal json As Char(), ByVal index As Integer) As Integer
      Dim saveIndex As Integer = index
      Return NextToken(json, saveIndex)
    End Function

    Protected Shared Function NextToken(ByVal json As Char(), ByRef index As Integer) As Integer
      EatWhitespace(json, index)

      If index = json.Length Then
        Return TOKEN_NONE
      End If

      Dim c As Char = json(index)
      index += 1
      Select Case c
        Case "{"c
          Return TOKEN_CURLY_OPEN
        Case "}"c
          Return TOKEN_CURLY_CLOSE
        Case "["c
          Return TOKEN_SQUARED_OPEN
        Case "]"c
          Return TOKEN_SQUARED_CLOSE
        Case ","c
          Return TOKEN_COMMA
        Case """"c
          Return TOKEN_STRING
        Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, _
         "6"c, "7"c, "8"c, "9"c, "-"c
          Return TOKEN_NUMBER
        Case ":"c
          Return TOKEN_COLON
      End Select
      index -= 1

      Dim remainingLength As Integer = json.Length - index

      ' false
      If remainingLength >= 5 Then
        If json(index) = "f"c AndAlso json(index + 1) = "a"c AndAlso json(index + 2) = "l"c AndAlso json(index + 3) = "s"c AndAlso json(index + 4) = "e"c Then
          index += 5
          Return TOKEN_FALSE
        End If
      End If

      ' true
      If remainingLength >= 4 Then
        If json(index) = "t"c AndAlso json(index + 1) = "r"c AndAlso json(index + 2) = "u"c AndAlso json(index + 3) = "e"c Then
          index += 4
          Return TOKEN_TRUE
        End If
      End If

      ' null
      If remainingLength >= 4 Then
        If json(index) = "n"c AndAlso json(index + 1) = "u"c AndAlso json(index + 2) = "l"c AndAlso json(index + 3) = "l"c Then
          index += 4
          Return TOKEN_NULL
        End If
      End If

      Return TOKEN_NONE
    End Function

    Protected Shared Function SerializeValue(ByVal value As Object, ByVal builder As StringBuilder) As Boolean
      Dim success As Boolean = True

      If TypeOf value Is String Then
        success = SerializeString(DirectCast(value, String), builder)
      ElseIf TypeOf value Is Hashtable Then
        success = SerializeObject(DirectCast(value, Hashtable), builder)
      ElseIf TypeOf value Is ArrayList Then
        success = SerializeArray(DirectCast(value, ArrayList), builder)
      ElseIf IsNumeric(value) Then
        success = SerializeNumber(Convert.ToDouble(value), builder)
      ElseIf (TypeOf value Is [Boolean]) AndAlso (CType(value, [Boolean]) = True) Then
        builder.Append("true")
      ElseIf (TypeOf value Is [Boolean]) AndAlso (CType(value, [Boolean]) = False) Then
        builder.Append("false")
      ElseIf value Is Nothing Then
        builder.Append("null")
      Else
        success = False
      End If
      Return success
    End Function

    Protected Shared Function SerializeObject(ByVal anObject As Hashtable, ByVal builder As StringBuilder) As Boolean
      builder.Append("{")

      Dim e As IDictionaryEnumerator = anObject.GetEnumerator()
      Dim first As Boolean = True
      While e.MoveNext()
        Dim key As String = e.Key.ToString()
        Dim value As Object = e.Value

        If Not first Then
          builder.Append(", ")
        End If

        SerializeString(key, builder)
        builder.Append(":")
        If Not SerializeValue(value, builder) Then
          Return False
        End If

        first = False
      End While

      builder.Append("}")
      Return True
    End Function

    Protected Shared Function SerializeArray(ByVal anArray As ArrayList, ByVal builder As StringBuilder) As Boolean
      builder.Append("[")

      Dim first As Boolean = True
      For i As Integer = 0 To anArray.Count - 1
        Dim value As Object = anArray(i)

        If Not first Then
          builder.Append(", ")
        End If

        If Not SerializeValue(value, builder) Then
          Return False
        End If

        first = False
      Next

      builder.Append("]")
      Return True
    End Function

    Protected Shared Function SerializeString(ByVal aString As String, ByVal builder As StringBuilder) As Boolean
      builder.Append("""")

      Dim charArray As Char() = aString.ToCharArray()
      For i As Integer = 0 To charArray.Length - 1
        Dim c As Char = charArray(i)
        If c = """"c Then
          builder.Append("\""")
        ElseIf c = "\"c Then
          builder.Append("\\")
        ElseIf c = ControlChars.Back Then
          builder.Append("\b")
        ElseIf c = ControlChars.FormFeed Then
          builder.Append("\f")
        ElseIf c = ControlChars.Lf Then
          builder.Append("\n")
        ElseIf c = ControlChars.Cr Then
          builder.Append("\r")
        ElseIf c = ControlChars.Tab Then
          builder.Append("\t")
        Else
          Dim codepoint As Integer = Convert.ToInt32(c)
          If (codepoint >= 32) AndAlso (codepoint <= 126) Then
            builder.Append(c)
          Else
            builder.Append("\u" & Convert.ToString(codepoint, 16).PadLeft(4, "0"c))
          End If
        End If
      Next

      builder.Append("""")
      Return True
    End Function

    Protected Shared Function SerializeNumber(ByVal number As Double, ByVal builder As StringBuilder) As Boolean
      builder.Append(Convert.ToString(number, CultureInfo.InvariantCulture))
      Return True
    End Function

    Protected Shared Function IsNumeric(ByVal o As Object) As Boolean
      Dim result As Double

      Return If((o Is Nothing), False, [Double].TryParse(o.ToString(), result))
    End Function
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
      target = value
      Return value
    End Function
  End Class
End Namespace
