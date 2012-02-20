Module app_formCodeEdit
  Public netFrameworkDir As String

  Function getNetFrameworkDir() As String
    Dim testAssy As System.Reflection.Assembly = GetType(Object).Assembly
    '        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\.NETFramework")
    getNetFrameworkDir = IDE.Glob.fp(IO.Path.GetDirectoryName(testAssy.Location))
  End Function

  Function getActiveFormcode() As String
    Dim tab As Object = IDE.getActiveTab
    Dim content As String = tab.RTF.Text
    Dim abPos = InStr(content, "#windowdata", CompareMethod.Text) - 1
    Dim bisPos = InStr(abPos, content, "#enddata", CompareMethod.Text) - 1

    Dim content2 = content.Substring(abPos, bisPos - abPos - 2)
    'Dim winData() = Split(content2, vbNewLine, 2)
    Return content2
    'TextBox2.Text = winData(0)
    'Igrid_put(IGrid1, winData(1))

  End Function

  Function splitProps(ByVal propList As String) As String()
    Dim isEscaped As Boolean = False
    Dim startPos As Integer = 0
    Dim out(0) As String
    For i = 0 To propList.Length - 1
      'If propList.Chars(i) = "'"c And Not isEscaped Then inAPO = Not inAPO
      If propList.Chars(i) = "\"c And Not isEscaped Then isEscaped = True : Continue For
      If propList.Chars(i) = "|"c And Not isEscaped Then
        ReDim Preserve out(out.Length)
        'out(out.Length - 1) = propList.Substring(startPos, i - startPos)
        'startPos = i + 1
        Continue For
      End If
      out(out.Length - 1) += propList.Chars(i)
      isEscaped = False
    Next
    'ReDim Preserve out(out.Length)
    'out(out.Length - 1) = propList.Substring(startPos, propList.Length - startPos)
    'startPos = propList.Length
    Return out
  End Function

End Module
