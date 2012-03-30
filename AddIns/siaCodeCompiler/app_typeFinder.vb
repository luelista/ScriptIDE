Imports System.Text
Imports System.Reflection

Module app_typeFinder

  Sub searchAssembly(ByVal searchFor As String, ByVal fileSpec As String, ByVal exact As Boolean)
    searchFor = searchFor.ToUpper
    Dim a As Assembly = Assembly.LoadFrom(fileSpec)
    Dim modules() As [Module] = a.GetModules()
    For i = 0 To modules.Length - 1
      Dim types() As Type = modules(i).GetTypes()
      For Each typ In types
        Dim name = typ.FullName.ToUpper
        If exact Then
          If searchFor = name Then

          End If
        End If

      Next

    Next
  End Sub

  Function findTypeExact(ByVal searchFor As String, ByVal assemblies As ArrayList, ByVal importNS As ArrayList) As Type
    For i = 0 To importNS.Count - 1
      importNS(i) = importNS(i).ToUpper
    Next
    searchFor = searchFor.ToUpper()
    For Each assemblyFileName As String In assemblies
      Try
        Dim a As Assembly = Assembly.LoadFrom(assemblyFileName)
        Dim types() As Type = a.GetTypes()

        For Each typ In types
          Dim fname = typ.FullName.ToUpper, name = typ.Name.ToUpper

          If fname = searchFor Then Return typ

          If name = searchFor Then
            For Each ns In importNS
              If fname.StartsWith(ns) Then Return typ
            Next
          End If
        Next
      Catch ex As ReflectionTypeLoadException
        TT.Write("Unable to load types " + assemblyFileName, ex.ToString, "err")
      Catch ex As Exception
        TT.Write("Unable to load types " + assemblyFileName, ex.ToString, "err")
      End Try
    Next
    Return Nothing
  End Function

  Sub findTypeList(ByVal searchFor As String, ByVal assemblies As ArrayList, ByVal importNS As ArrayList)
    searchFor = searchFor.ToUpper()
    Dim i As Integer = 0
    For Each assemblyFileName As String In assemblies
      Try
        Dim a As Assembly = Assembly.LoadFrom(assemblyFileName)
        Dim types() As Type = a.GetTypes()
        For Each typ In types
          Dim name = typ.FullName.ToUpper
          If name.Contains(searchFor) Then
            Dim lvi = tbInfoTips.ListView1.Items.Add(typ.FullName, "type")
            If typ.IsClass Then lvi.ImageKey = "class"
            If typ.IsInterface Then lvi.ImageKey = "interface"
            If typ.IsEnum Then lvi.ImageKey = "enum"
            lvi.SubItems.Add(typ.FullName)
            i += 1
          End If
          If i Mod 20 = 0 AndAlso KeyState.isKeyPressed(Keys.Pause) Then
            Dim lvi = tbInfoTips.ListView1.Items.Add("abgebrochen...")
            Exit Sub
          End If
        Next
      Catch ex As ReflectionTypeLoadException
        TT.Write("Unable to load types " + assemblyFileName, ex.ToString, "err")
      Catch ex As Exception
        TT.Write("Unable to load types " + assemblyFileName, ex.ToString, "err")
      End Try
    Next
  End Sub

  Public Sub Search(ByVal theSearchString As String, ByVal theModule As String)
    'Try
    '  '
    '  ' Load the module - expect to fail if it is not a .NET Framework 
    '  ' module.
    '  '
    '  Dim a As [Assembly] = [Assembly].LoadFrom(theModule)
    '  Dim m As [Module]() = a.GetModules()

    '  ' We are case insensitive
    '  theSearchString = theSearchString.ToUpper(CultureInfo.InvariantCulture)

    '  Dim j As Integer
    '  For j = 0 To m.Length - 1

    '    myVerboseWriter.WriteLine("Searching Module {0}", theModule)

    '    If Not (m Is Nothing) Then
    '      '
    '      ' Get all the types from the module
    '      '
    '      Dim types As Type() = m(j).GetTypes()

    '      Dim i As Integer
    '      For i = 0 To types.Length - 1
    '        Dim curType As Type = types(i)

    '        ' Case insensitive
    '        Dim name As String = curType.FullName.ToUpper(CultureInfo.InvariantCulture)

    '        '
    '        ' How has the user indicated they want to search. Note that 
    '        ' even if the user has specified several ways to search we only 
    '        ' respect one of them (from most specific to most general)
    '        '                  
    '        If ExactMatchOnly Then
    '          If name = theSearchString Then
    '            DumpType(curType)
    '          End If
    '        Else
    '          If MatchOnlyNamespace Then
    '            If Not (curType.Namespace Is Nothing) Then
    '              If curType.Namespace.ToUpper(CultureInfo.InvariantCulture) = theSearchString Then
    '                DumpType(curType)
    '              End If
    '            End If
    '          Else
    '            If WideSearch Then
    '              If curType.Namespace.ToUpper(CultureInfo.InvariantCulture).IndexOf(theSearchString) <> -1 Then
    '                DumpType(curType)
    '              End If
    '            Else
    '              '
    '              ' User has not specified a search criteria - so we have some 
    '              ' defaults:
    '              '   (1) If the search string supplied matches a complete type
    '              '       name then assume they want to get all information about 
    '              '       the type. If they have actually set display options then 
    '              '       respect them.
    '              '   (2) If we are going to dump all information save the show 
    '              '       properties so they can be reset after this type (in case the 
    '              '       user is searching for multiple types).
    '              '
    '              If name = theSearchString Then
    '                Dim oldOptions As Integer = showOptions

    '                If showOptions = 0 Then
    '                  ShowAll()
    '                End If

    '                DumpType(curType)

    '                showOptions = oldOptions
    '              Else
    '                If name.IndexOf(theSearchString) <> -1 Then
    '                  DumpType(curType)
    '                End If
    '              End If
    '            End If
    '          End If
    '        End If
    '      Next i
    '    End If
    '  Next j
    'Catch rcle As ReflectionTypeLoadException

    '  Dim loadedTypes As Type() = rcle.Types
    '  Dim exceptions As Exception() = rcle.LoaderExceptions

    '  Dim exceptionCount As Integer = 0

    '  Dim i As Integer
    '  For i = 0 To loadedTypes.Length - 1
    '    If loadedTypes(i) Is Nothing Then
    '      ' The following line would output the TypeLoadException.
    '      ' myWriter.WriteLine("Unable to load a type because {0}", exceptions(exceptionCount))
    '      exceptionCount += 1
    '    End If
    '  Next i
    'Catch fnfe As FileNotFoundException
    '  myVerboseWriter.WriteLine(fnfe.Message)
    'Catch
    'End Try
  End Sub 'Search 


  ' A short description of the type.   
  Function GetTypeDescription(ByVal aType As Type) As String
    Dim str As String = Nothing

    If aType.IsClass Then
      str = "class"
    End If

    If aType.IsInterface Then
      str = "interface"
    End If

    If aType.IsValueType Then
      str = "struct"
    End If

    If aType.IsArray Then
      str = "array"
    End If

    Return str
  End Function 'GetTypeDescription


  ' Dumps information about the specified type.   
  Private Sub DumpType(ByVal aType As Type, ByVal out As StringBuilder, ByVal recursive As Boolean)
    Dim baseType As Type = aType.BaseType

    out.AppendLine(GetTypeDescription(aType) + vbTab + aType.ToString)

    out.AppendLine("Module:" + vbTab + aType.Module.FullyQualifiedName)

    DumpInterfaces(aType, out)
    DumpFields(aType, out)
    DumpProperties(aType, out)
    DumpEvents(aType, out)
    DumpMethods(aType, out)

    If recursive Then
      out.AppendLine()
    End If

    '
    ' If recursing then pop the indent on the writers so we 
    ' can easily see the nesting for the base type information.
    '                                       
    If recursive And Not (baseType Is Nothing) Then
      DumpType(baseType, out, recursive)
    End If
  End Sub 'DumpType


  ' Dumps the interfaces implemented by the specified type.   
  Private Sub DumpInterfaces(ByVal aType As Type, ByVal out As StringBuilder)
    Dim info As Type() = aType.GetInterfaces()

    If info.Length <> 0 Then
      out.AppendLine("# Interfaces: " & info.Length)

      Dim i As Integer
      For i = 0 To info.Length - 1

        'myWriter.PushIndent()
        out.AppendLine("interface " + info(i).FullName)

        '
        ' Only show method information only if requested
        '
        'myWriter.PushIndent()
        DumpType(info(i), out, False)
        'myWriter.PopIndent()
        ' myWriter.PopIndent()
      Next i
    End If
  End Sub 'DumpInterfaces


  ' Dumps the public properties directly contained in the specified type   
  Private Sub DumpProperties(ByVal aType As Type, ByVal out As StringBuilder)
    Dim pInfo As PropertyInfo() = aType.GetProperties()
    out.AppendLine("Properties")

    Dim found As Boolean = False

    If pInfo.Length <> 0 Then
      Dim curInfo As PropertyInfo = Nothing

      Dim i As Integer
      For i = 0 To pInfo.Length - 1
        curInfo = pInfo(i)

        '
        ' Only display properties declared in this type.
        '          
        If curInfo.DeclaringType Is aType Then
          found = True

          Dim flags As String = Nothing

          If curInfo.CanRead And curInfo.CanWrite Then
            flags = "get; set;"
          Else
            If curInfo.CanRead Then
              flags = "get"
            Else
              If curInfo.CanWrite Then
                flags = "set"
              End If
            End If
          End If
          out.AppendLine("  " + curInfo.ToString + " '" + flags + "'")
        End If
      Next i
    End If

    If Not found Then
      out.AppendLine("  (none)")
    End If
  End Sub 'DumpProperties


  ' Dumps the public events directly contained in the specified type   
  Private Sub DumpEvents(ByVal aType As Type, ByVal out As StringBuilder)
    Dim eInfo As EventInfo() = aType.GetEvents()

    out.AppendLine("Events:")
    Dim found As Boolean = False

    If eInfo.Length <> 0 Then
      Dim i As Integer
      For i = 0 To eInfo.Length - 1
        '
        ' Only display events declared in this type.
        '          
        If eInfo(i).DeclaringType Is aType Then
          found = True
          out.AppendLine("  " + eInfo(i).ToString)
        End If
      Next i
    End If

    If Not found Then
      out.AppendLine("  (none)")
    End If
  End Sub 'DumpEvents


  ' Dumps the public fields directly contained in the specified type   
  Private Sub DumpFields(ByVal aType As Type, ByVal out As StringBuilder)
    Dim info As FieldInfo() = aType.GetFields()

    out.AppendLine("Fields:")

    Dim found As Boolean = False

    If info.Length <> 0 Then
      Dim i As Integer
      For i = 0 To info.Length - 1
        '
        ' Only display fields declared in this type.
        '          
        If info(i).DeclaringType Is aType Then
          out.AppendLine("  " + info(i).ToString)
          found = True
        End If
      Next i
    End If

    If Not found Then
      out.AppendLine("  (none)")
    End If
  End Sub 'DumpFields


  ' Dumps the public methods directly contained in the specified type. 
  ' Note "special name" methods are not displayed.   
  Sub DumpMethods(ByVal aType As Type, ByVal out As StringBuilder)
    Dim mInfo As MethodInfo() = aType.GetMethods()

    out.AppendLine("Methods")

    Dim found As Boolean = False

    If mInfo.Length <> 0 Then
      Dim i As Integer
      For i = 0 To mInfo.Length - 1
        '
        ' Only display methods declared in this type. Also 
        ' filter out any methods with special names - these
        ' cannot be generally called by the user (i.e their 
        ' functionality is usually exposed in other ways e.g
        ' property get/set methods are exposed as properties.
        '          
        If mInfo(i).DeclaringType Is aType And Not mInfo(i).IsSpecialName Then
          found = True

          Dim modifiers As New StringBuilder()

          If mInfo(i).IsStatic Then
            modifiers.Append("static ")
          End If
          If mInfo(i).IsPublic Then
            modifiers.Append("public ")
          End If
          If mInfo(i).IsFamily Then
            modifiers.Append("protected ")
          End If
          If mInfo(i).IsAssembly Then
            modifiers.Append("internal ")
          End If
          If mInfo(i).IsPrivate Then
            modifiers.Append("private ")
          End If
          out.AppendLine("  " + modifiers.ToString + mInfo(i).ToString)
        End If
      Next i
    End If

    If Not found Then
      out.AppendLine("  (none)")
    End If
  End Sub 'DumpMethods

  Function getMethodModifiers(ByVal mInfo As MethodBase) As String
    Dim modifiers As New StringBuilder()
    If mInfo.IsStatic Then
      modifiers.Append("Shared ") '("static ")
    End If
    If mInfo.IsPublic Then
      'modifiers.Append("Public ") '("public ")
    End If
    If mInfo.IsFamily Then
      modifiers.Append("Protected ") '("protected ")
    End If
    If mInfo.IsAssembly Then
      modifiers.Append("Friend ") '("internal ")
    End If
    If mInfo.IsPrivate Then
      modifiers.Append("Private ") '("private ")
    End If
    Return modifiers.ToString
  End Function
  Function getMethodParameters(ByVal mInfo As MethodBase, ByVal longParas As Boolean, ByVal formatted As Boolean) As String
    Dim paraList = mInfo.GetParameters()
    Dim out(paraList.Length - 1) As String
    For i = 0 To paraList.Length - 1
      Dim para = paraList(i)
      If longParas Then
        'out(i) = If(para.IsOptional, "[", "") + If(para.IsOut, "ByRef ", "") + para.Name + " As " + para.ParameterType.Name + If(para.IsOptional, "]", "")
        out(i) = ParameterToString(para, formatted)
      Else
        out(i) = If(para.IsOptional, "[", "") + If(para.IsOut, "ref ", "") + para.Name + If(para.IsOptional, "]", "")
      End If
    Next
    Return Join(out, ", ")
  End Function
  Function MethodToString(ByVal Method As MethodBase) As String
    Dim flag As Boolean
    Dim typ As Type = Nothing
    Dim str As String = "<f fg='#008'>"
    If (Method.MemberType = MemberTypes.Method) Then
      typ = DirectCast(Method, MethodInfo).ReturnType
    End If
    If Method.IsPublic Then
      str = (str & "Public ")
    ElseIf Method.IsPrivate Then
      str = (str & "Private ")
    ElseIf Method.IsAssembly Then
      str = (str & "Friend ")
    End If
    If ((Method.Attributes And MethodAttributes.Virtual) <> MethodAttributes.ReuseSlot) Then
      If Not Method.DeclaringType.IsInterface Then
        str = (str & "Overrides ")
      End If
    ElseIf IsShared(Method) Then
      str = (str & "Shared ")
    End If
    Dim uNDEF As UserDefinedOperator = UserDefinedOperator.UNDEF
    If IsUserDefinedOperator(Method) Then
      uNDEF = MapToUserDefinedOperator(Method)
    End If
    If (uNDEF <> UserDefinedOperator.UNDEF) Then
      If (uNDEF = UserDefinedOperator.Narrow) Then
        str = (str & "Narrowing ")
      ElseIf (uNDEF = UserDefinedOperator.Widen) Then
        str = (str & "Widening ")
      End If
      str = (str & "Operator ")
    ElseIf ((typ Is Nothing) OrElse (typ.FullName = "System.Void")) Then
      str = (str & "Sub ")
    Else
      str = (str & "Function ")
    End If
    str &= "</color><b>"
    If (uNDEF <> UserDefinedOperator.UNDEF) Then
      str = (str & OperatorNames(CInt(uNDEF)))
    ElseIf (Method.MemberType = MemberTypes.Constructor) Then
      str = (str & "New")
    Else
      str = (str & Method.Name)
    End If
    str &= "</b>"
    If Method.IsGenericMethod Then
      str = (str & "(Of ")
      flag = True
      Dim type2 As Type
      For Each type2 In Method.GetGenericArguments() 'GetTypeParameters(Method)
        If Not flag Then
          str = (str & ", ")
        Else
          flag = False
        End If
        str = (str & VBFriendlyNameOfType(type2, False))
      Next
      str = (str & ")")
    End If
    str = (str & "(")
    flag = True
    Dim info As ParameterInfo
    For Each info In Method.GetParameters
      If Not flag Then
        str = (str & ", ")
      Else
        flag = False
      End If
      str = (str & ParameterToString(info))
    Next
    str = (str & ")")
    If ((typ Is Nothing) OrElse (typ.FullName = "System.Void")) Then
      Return str
    End If
    Return (str & " <f fg='#008'>As</color> " & VBFriendlyNameOfType(typ, True))
  End Function





  Public Function PropertyToString(ByVal Prop As PropertyInfo) As String
    Dim parameters As ParameterInfo()
    Dim returnType As Type
    Dim str2 As String = "<f fg='#008'>"
    'Dim readWrite As PropertyKind = PropertyKind.ReadWrite
    Dim getMethod As MethodInfo = Prop.GetGetMethod
    If (Not getMethod Is Nothing) Then
      'If (Not Prop.GetSetMethod Is Nothing) Then
      '  readWrite = PropertyKind.ReadWrite
      'Else
      '  readWrite = PropertyKind.ReadOnly
      'End If
      parameters = getMethod.GetParameters
      returnType = getMethod.ReturnType
    Else
      'readWrite = PropertyKind.WriteOnly
      getMethod = Prop.GetSetMethod
      Dim sourceArray As ParameterInfo() = getMethod.GetParameters
      parameters = New ParameterInfo(((sourceArray.Length - 2) + 1) - 1) {}
      Array.Copy(sourceArray, parameters, parameters.Length)
      returnType = sourceArray((sourceArray.Length - 1)).ParameterType
    End If
    str2 = (str2 & "Public ")
    If ((getMethod.Attributes And MethodAttributes.Virtual) <> MethodAttributes.ReuseSlot) Then
      If Not Prop.DeclaringType.IsInterface Then
        str2 = (str2 & "Overrides ")
      End If
    ElseIf getMethod.IsStatic Then
      str2 = (str2 & "Shared ")
    End If
    If Prop.CanRead = False Then str2 = (str2 & "ReadOnly ")
    If Prop.CanWrite = False Then str2 = (str2 & "WriteOnly ")
    str2 = (str2 & "Property</color> <b>" & Prop.Name & "</b>(")
    Dim flag As Boolean = True
    Dim info2 As ParameterInfo
    For Each info2 In parameters
      If Not flag Then
        str2 = (str2 & ", ")
      Else
        flag = False
      End If
      str2 = (str2 & ParameterToString(info2))
    Next
    Return (str2 & ") <f fg='#008'>As</color> " & VBFriendlyNameOfType(returnType, True))
  End Function

  Function ParameterToString(ByVal Parameter As ParameterInfo, Optional ByVal formatted As Boolean = True) As String
    Dim beginKW = "<f fg='#008'>", endKW = "</color>"
    If Not formatted Then beginKW = "" : endKW = ""

    Dim str2 As String = ""
    Dim parameterType As Type = Parameter.ParameterType
    If Parameter.IsOptional Then
      str2 = (str2 & "[")
    End If
    If parameterType.IsByRef Then
      str2 = (str2 & beginKW & "ByRef" + endKW + " ")
      parameterType = parameterType.GetElementType
    ElseIf IsParamArray(Parameter) Then
      str2 = (str2 & beginKW & "ParamArray" + endKW + " ")
    End If
    str2 = (str2 & Parameter.Name & beginKW & " As" + endKW + " " & VBFriendlyNameOfType(parameterType, True))
    If Not Parameter.IsOptional Then
      Return str2
    End If
    Dim defaultValue As Object = Parameter.DefaultValue
    If (defaultValue Is Nothing) Then
      str2 = (str2 & " = Nothing")
    Else
      Dim type As Type = defaultValue.GetType
      If (type.FullName <> "System.Void") Then
        If type.IsEnum Then
          str2 = (str2 & " = " & System.Enum.GetName(type, defaultValue))
        Else
          str2 = (str2 & " = " & Microsoft.VisualBasic.CompilerServices.Conversions.ToString(defaultValue))
        End If
      End If
    End If
    Return (str2 & "]")
  End Function

  Function IsArrayType(ByVal Type As Type) As Boolean
    Return Type.IsArray
  End Function

  Function IsParamArray(ByVal Parameter As ParameterInfo) As Boolean
    Return (IsArrayType(Parameter.ParameterType) AndAlso Parameter.IsDefined(GetType(ParamArrayAttribute), False))
  End Function

  Function VBFriendlyNameOfType(ByVal typ As Type, Optional ByVal FullName As Boolean = False) As String
    Dim A = "<a>", xA = "</a>"
    Dim name As String
    Dim typeCode As TypeCode
    Dim arraySuffixAndElementType As String = GetArraySuffixAndElementType((typ))
    If typ.IsEnum Then
      typeCode = typeCode.Object
    Else
      typeCode = Type.GetTypeCode(typ)
    End If
    Select Case typeCode
      Case typeCode.DBNull
        name = A + "DBNull" + xA
        Exit Select
      Case typeCode.Boolean
        name = A + "Boolean" + xA
        Exit Select
      Case typeCode.Char
        name = A + "Char" + xA
        Exit Select
      Case typeCode.SByte
        name = A + "SByte" + xA
        Exit Select
      Case typeCode.Byte
        name = A + "Byte" + xA
        Exit Select
      Case typeCode.Int16
        name = A + "Short" + xA
        Exit Select
      Case typeCode.UInt16
        name = A + "UShort" + xA
        Exit Select
      Case typeCode.Int32
        name = A + "Integer" + xA
        Exit Select
      Case typeCode.UInt32
        name = A + "UInteger" + xA
        Exit Select
      Case typeCode.Int64
        name = A + "Long" + xA
        Exit Select
      Case typeCode.UInt64
        name = A + "ULong" + xA
        Exit Select
      Case typeCode.Single
        name = A + "Single" + xA
        Exit Select
      Case typeCode.Double
        name = A + "Double" + xA
        Exit Select
      Case typeCode.Decimal
        name = A + "Decimal" + xA
        Exit Select
      Case typeCode.DateTime
        name = A + "Date" + xA
        Exit Select
      Case typeCode.String
        name = A + "String</a>"
        Exit Select
      Case Else
        If IsGenericParameter(typ) Then
          name = typ.Name
        Else
          Dim _fullName As String
          Dim str6 As String = Nothing
          Dim genericArgsSuffix As String = GetGenericArgsSuffix(typ)
          If FullName Then
            If typ.IsNested Then
              str6 = VBFriendlyNameOfType(typ.DeclaringType, True)
              _fullName = A + typ.Name + xA
            Else
              _fullName = A + typ.FullName + xA
            End If
          Else
            _fullName = A + typ.Name + xA
          End If
          If (Not genericArgsSuffix Is Nothing) Then
            Dim length As Integer = _fullName.LastIndexOf("`"c)
            If (length <> -1) Then
              _fullName = _fullName.Substring(0, length)
            End If
            name = (_fullName & genericArgsSuffix)
          Else
            name = _fullName
          End If
          If (Not str6 Is Nothing) Then
            name = (str6 & "." & name)
          End If
        End If
        Exit Select
    End Select
    'name = "<a>" & name & "</a>"
    If (Not arraySuffixAndElementType Is Nothing) Then
      name = (name & arraySuffixAndElementType)
    End If
    Return name
  End Function

  Function FieldToString(ByVal Field As FieldInfo) As String
    Dim str As String = "<font color='#00A'>"
    Dim fieldType As Type = Field.FieldType
    If Field.IsPublic Then
      str = (str & "Public ")
    ElseIf Field.IsPrivate Then
      str = (str & "Private ")
    ElseIf Field.IsAssembly Then
      str = (str & "Friend ")
    ElseIf Field.IsFamily Then
      str = (str & "Protected ")
    ElseIf Field.IsFamilyOrAssembly Then
      str = (str & "Protected Friend ")
    End If
    Return "</color><b>"((str & Field.Name) & "</b> <font color='#00A'>As</color> " & VBFriendlyNameOfType(fieldType, True))
  End Function

  Function IsGenericParameter(ByVal Type As Type) As Boolean
    Return Type.IsGenericParameter
  End Function
  Function IsShared(ByVal Member As MemberInfo) As Boolean
    Select Case Member.MemberType
      Case MemberTypes.Constructor
        Return DirectCast(Member, ConstructorInfo).IsStatic
      Case MemberTypes.Field
        Return DirectCast(Member, FieldInfo).IsStatic
      Case MemberTypes.Method
        Return DirectCast(Member, MethodInfo).IsStatic
      Case MemberTypes.Property
        Return DirectCast(Member, PropertyInfo).GetGetMethod.IsStatic
    End Select
    Return False
  End Function
  Function IsUserDefinedOperator(ByVal Method As MethodBase) As Boolean
    Return (Method.IsSpecialName AndAlso Method.Name.StartsWith("op_", StringComparison.Ordinal))
  End Function
  Function MapToUserDefinedOperator(ByVal Method As MethodBase) As UserDefinedOperator
    Dim index As Integer = 1
    Do
      If Method.Name.Equals(OperatorCLSNames(index)) Then
        Dim length As Integer = Method.GetParameters.Length
        Dim op As UserDefinedOperator = DirectCast(CSByte(index), UserDefinedOperator)
        If ((length = 1) AndAlso IsUnaryOperator(op)) Then
          Return op
        End If
        If ((length = 2) AndAlso IsBinaryOperator(op)) Then
          Return op
        End If
      End If
      index += 1
    Loop While (index <= &H1B)
    Return UserDefinedOperator.UNDEF
  End Function
  Function IsUnaryOperator(ByVal Op As UserDefinedOperator) As Boolean
    Select Case Op
      Case UserDefinedOperator.Narrow, UserDefinedOperator.Widen, UserDefinedOperator.IsTrue, UserDefinedOperator.IsFalse, UserDefinedOperator.Negate, UserDefinedOperator.Not, UserDefinedOperator.UnaryPlus
        Return True
    End Select
    Return False
  End Function
  Function IsBinaryOperator(ByVal Op As UserDefinedOperator) As Boolean
    Select Case Op
      Case UserDefinedOperator.Plus, UserDefinedOperator.Minus, UserDefinedOperator.Multiply, UserDefinedOperator.Divide, UserDefinedOperator.Power, UserDefinedOperator.IntegralDivide, UserDefinedOperator.Concatenate, UserDefinedOperator.ShiftLeft, UserDefinedOperator.ShiftRight, UserDefinedOperator.Modulus, UserDefinedOperator.Or, UserDefinedOperator.Xor, UserDefinedOperator.And, UserDefinedOperator.Like, UserDefinedOperator.Equal, UserDefinedOperator.NotEqual, UserDefinedOperator.Less, UserDefinedOperator.LessEqual, UserDefinedOperator.GreaterEqual, UserDefinedOperator.Greater
        Return True
    End Select
    Return False
  End Function









  ReadOnly OperatorCLSNames As String()
  ReadOnly OperatorNames As String()








  Sub New()
    'Symbols.NoArguments = New Object(0 - 1) {}
    'Symbols.NoArgumentNames = New String(0 - 1) {}
    'Symbols.NoTypeArguments = New Type(0 - 1) {}
    'Symbols.NoTypeParameters = New Type(0 - 1) {}
    OperatorCLSNames = New String(&H1C - 1) {}
    OperatorCLSNames(1) = "op_Explicit"
    OperatorCLSNames(2) = "op_Implicit"
    OperatorCLSNames(3) = "op_True"
    OperatorCLSNames(4) = "op_False"
    OperatorCLSNames(5) = "op_UnaryNegation"
    OperatorCLSNames(6) = "op_OnesComplement"
    OperatorCLSNames(7) = "op_UnaryPlus"
    OperatorCLSNames(8) = "op_Addition"
    OperatorCLSNames(9) = "op_Subtraction"
    OperatorCLSNames(10) = "op_Multiply"
    OperatorCLSNames(11) = "op_Division"
    OperatorCLSNames(12) = "op_Exponent"
    OperatorCLSNames(13) = "op_IntegerDivision"
    OperatorCLSNames(14) = "op_Concatenate"
    OperatorCLSNames(15) = "op_LeftShift"
    OperatorCLSNames(&H10) = "op_RightShift"
    OperatorCLSNames(&H11) = "op_Modulus"
    OperatorCLSNames(&H12) = "op_BitwiseOr"
    OperatorCLSNames(&H13) = "op_ExclusiveOr"
    OperatorCLSNames(20) = "op_BitwiseAnd"
    OperatorCLSNames(&H15) = "op_Like"
    OperatorCLSNames(&H16) = "op_Equality"
    OperatorCLSNames(&H17) = "op_Inequality"
    OperatorCLSNames(&H18) = "op_LessThan"
    OperatorCLSNames(&H19) = "op_LessThanOrEqual"
    OperatorCLSNames(&H1A) = "op_GreaterThanOrEqual"
    OperatorCLSNames(&H1B) = "op_GreaterThan"
    OperatorNames = New String(&H1C - 1) {}
    OperatorNames(1) = "CType"
    OperatorNames(2) = "CType"
    OperatorNames(3) = "IsTrue"
    OperatorNames(4) = "IsFalse"
    OperatorNames(5) = "-"
    OperatorNames(6) = "Not"
    OperatorNames(7) = "+"
    OperatorNames(8) = "+"
    OperatorNames(9) = "-"
    OperatorNames(10) = "*"
    OperatorNames(11) = "/"
    OperatorNames(12) = "^"
    OperatorNames(13) = "\"
    OperatorNames(14) = "&"
    OperatorNames(15) = "<<"
    OperatorNames(&H10) = ">>"
    OperatorNames(&H11) = "Mod"
    OperatorNames(&H12) = "Or"
    OperatorNames(&H13) = "Xor"
    OperatorNames(20) = "And"
    OperatorNames(&H15) = "Like"
    OperatorNames(&H16) = "="
    OperatorNames(&H17) = "<>"
    OperatorNames(&H18) = "<"
    OperatorNames(&H19) = "<="
    OperatorNames(&H1A) = ">="
    OperatorNames(&H1B) = ">"
  End Sub













  Function GetGenericArgsSuffix(ByVal typ As Type) As String
    If Not typ.IsGenericType Then
      Return Nothing
    End If
    Dim genericArguments As Type() = typ.GetGenericArguments
    Dim length As Integer = genericArguments.Length
    Dim num2 As Integer = length
    If (typ.IsNested AndAlso typ.DeclaringType.IsGenericType) Then
      num2 = (num2 - typ.DeclaringType.GetGenericArguments.Length)
    End If
    If (num2 = 0) Then
      Return Nothing
    End If
    Dim builder As New StringBuilder
    builder.Append("(<font color='#00A'>Of</color> ")
    Dim num4 As Integer = (length - 1)
    Dim i As Integer = (length - num2)
    Do While (i <= num4)
      builder.Append(VBFriendlyNameOfType(genericArguments(i), False))
      If (i <> (length - 1)) Then
        builder.Append(","c)
      End If
      i += 1
    Loop
    builder.Append(")")
    Return builder.ToString
  End Function

  Function GetArraySuffixAndElementType(ByRef typ As Type) As String
    If Not typ.IsArray Then
      Return Nothing
    End If
    Dim builder As New StringBuilder
    Do
      builder.Append("(")
      builder.Append(","c, (typ.GetArrayRank - 1))
      builder.Append(")")
      typ = typ.GetElementType
    Loop While typ.IsArray
    Return builder.ToString
  End Function




  Friend Enum UserDefinedOperator As SByte
    ' Fields
    [And] = 20
    Concatenate = 14
    Divide = 11
    Equal = &H16
    Greater = &H1B
    GreaterEqual = &H1A
    IntegralDivide = 13
    IsFalse = 4
    IsTrue = 3
    Less = &H18
    LessEqual = &H19
    [Like] = &H15
    MAX = &H1C
    Minus = 9
    Modulus = &H11
    Multiply = 10
    Narrow = 1
    Negate = 5
    [Not] = 6
    NotEqual = &H17
    [Or] = &H12
    Plus = 8
    Power = 12
    ShiftLeft = 15
    ShiftRight = &H10
    UnaryPlus = 7
    UNDEF = 0
    Widen = 2
    [Xor] = &H13
  End Enum







End Module
