Imports System.Text
Imports System.Reflection

Module app_typeFinder
  Dim references, importNS As List(Of String)

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

  Sub addRef(ByVal fileName As String)
    If IO.File.Exists(fileName) Then references.Add(fileName) : Return
    If IO.File.Exists("C:\yEXE\" + fileName) Then references.Add("C:\yEXE\" + fileName) : Return
    If IO.File.Exists(netFrameworkDir + fileName) Then references.Add(netFrameworkDir + fileName) : Return
  End Sub
  Sub fetchReferencesFromTab(ByVal tab As IDockContentForm)
    references = New List(Of String)
    importNS = New List(Of String)
    Dim sc As Object = tab.RTF
    'TextBox1.Text = ""
    For Each lin As Object In sc.Lines '...lin as ScintillaNet.Line
      If lin.Text.ToLower.StartsWith("#reference") Then  addRef(lin.Text.Substring(10).Trim)
      If lin.Text.ToLower.StartsWith("#imports") Then importNS.Add(UCase(Trim(lin.Text.Substring(8))))
    Next
    For Each lin As String In IO.File.ReadAllLines(IDE.GetSettingsFolder + "scriptClass\references.txt")
      If lin.ToLower.StartsWith("#reference") Then addRef(lin.Substring(10).Trim)
      If lin.ToLower.StartsWith("#imports") Then importNS.Add(UCase(Trim(lin.Substring(8)))) 'TextBox1.AppendText("IMP " + lin.Substring(8) + vbNewLine)
    Next
  End Sub

  Function findTypeExact(ByVal searchFor As String) As Type
    searchFor = searchFor.ToUpper()
    For Each assemblyFileName As String In references
      Try
        Dim a As Assembly = Assembly.LoadFrom(assemblyFileName)
        Dim types() As Type = a.GetTypes()

        For Each typ In types
          Dim fname = typ.FullName.ToUpper, name = typ.Name.ToUpper

          If fname = searchFor Then Return typ

          If name = searchFor Then
            For Each ns In importNS
              If fname = ns + "." + name Then Return typ
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

  'Sub findTypeList(ByVal searchFor As String, ByVal assemblies() As String, ByVal importNS() As String)
  '  searchFor = searchFor.ToUpper()
  '  For Each assemblyFileName As String In assemblies
  '    Try
  '      Dim a As Assembly = Assembly.LoadFrom(assemblyFileName)
  '      Dim types() As Type = a.GetTypes()
  '      For Each typ In types
  '        Dim name = typ.FullName.ToUpper
  '        If name.Contains(searchFor) Then
  '          Dim lvi = tbInfoTips.ListView1.Items.Add(typ.FullName, "type")
  '          lvi.SubItems.Add(typ.FullName)
  '        End If
  '      Next
  '    Catch ex As ReflectionTypeLoadException
  '      TT.Write("Unable to load types " + assemblyFileName, ex.ToString, "err")
  '    Catch ex As Exception
  '      TT.Write("Unable to load types " + assemblyFileName, ex.ToString, "err")
  '    End Try
  '  Next
  'End Sub

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

  Function getMethodModifiers(ByVal mInfo As MethodInfo) As String
    Dim modifiers As New StringBuilder()
    If mInfo.IsStatic Then
      modifiers.Append("Shared ") '("static ")
    End If
    If mInfo.IsPublic Then
      modifiers.Append("Public ") '("public ")
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
  Function getMethodParameters(ByVal mInfo As MethodInfo) As String
    Dim paraList = mInfo.GetParameters()
    Dim out(paraList.Length - 1) As String
    For i = 0 To paraList.Length - 1
      Dim para = paraList(i)
      out(i) = If(para.IsOptional, "[", "") + If(para.IsOut, "ByRef ", "") + para.Name + " As " + para.ParameterType.Name + If(para.IsOptional, "]", "")
    Next
    Return Join(out, ", ")
  End Function


End Module
