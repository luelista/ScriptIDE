Module app_formCodeCreator

  Enum FormInfoCols
    indent
    left
    top
    width
    height
    type
    name
    Properties
    Length
  End Enum

  'Function getMethodList(ByVal scriptCode As String) As List(Of String)
  '  Dim LINES() = Split(scriptCode, vbNewLine)
  '  getMethodList = New List(Of String)
  '  Dim sourceLineNetto2 As String, result As String
  '  Dim abPos, abPos2 As Integer
  '  Dim subFuncName As String
  '  For i = 0 To LINES.Length - 1
  '    sourceLineNetto2 = trimKeywords(LINES(i)) + " "
  '    Dim firstWord As String = ""
  '    firstWord = sourceLineNetto2.Substring(0, (sourceLineNetto2 + " ").IndexOf(" "))

  '    If firstWord = "SUB" Or firstWord = "FUNCTION" Then
  '      abPos = (sourceLineNetto2 + " ").IndexOf(" ", firstWord.Length)
  '      abPos2 = (sourceLineNetto2 + "(").IndexOf("(", firstWord.Length)
  '      If abPos2 < abPos Then abPos = abPos2
  '      subFuncName = sourceLineNetto2.Substring(abPos, abPos2 - abPos)
  '      getMethodList.Add(subFuncName.Trim)
  '    End If

  '  Next

  'End Function

  Function findEventHandler(ByVal methods As List(Of String), ByVal objName As String)
    objName = objName.ToUpper
    Dim parts() = Split(objName, "_")
    For i = 0 To parts.Length - 2
      parts(i + 1) = parts(i) + "_" + parts(i + 1)
    Next
    Dim foundMethod As String, foundMethodRank As Integer = -1
    Debug.Print("--> Event Search: " + objName)
    For Each methodName In methods
      Dim bisPos = methodName.LastIndexOf("_")
      If bisPos = -1 Then Continue For
      Dim methodName2 = methodName.Substring(0, bisPos)
      If objName.StartsWith(methodName2 + "_") Or objName = methodName2 Then
        Debug.Print("Found: " + methodName + " " + methodName2)
        foundMethod = methodName

      End If
    Next
    'For Each methodName In methods
    '  For i = foundMethodRank + 1 To parts.Length - 1
    '    If methodName.StartsWith(parts(i)) Then
    '      Debug.Print("Found: " + methodName + " " + parts(i))
    '      foundMethod = methodName : foundMethodRank = i
    '    Else
    '      Exit For
    '    End If
    '  Next
    'Next

    Debug.Print("...search Result for " + objName + ": " + foundMethod + " (" + foundMethodRank.ToString + ")")
    Return foundMethod
  End Function


  Function compileFormCode(ByVal windowData As String, ByVal methods As List(Of String), ByRef lC As Integer) As String
    'Dim scriptText = IO.File.ReadAllText(TextBox1.Text)
    'Dim methods = getMethodList(scriptText)

    'MsgBox(Join(methods.ToArray, vbNewLine))

    Dim LINES() = Split(windowData, vbNewLine)
    If LINES(0).Trim.ToUpper.StartsWith("#WINDOWDATA ") = False Then Exit Function
    Dim formName = LINES(0).Trim.Substring(11).Trim

    Dim formText As String = formName

    Dim out As New System.Text.StringBuilder
    lC += 1 : out.AppendLine("Public Class " + formName + " : Inherits WeifenLuo.WinFormsUI.Docking.DockContent 'System.Windows.Forms.Form")
    lC += 1 : out.AppendLine()
    lC += 1 : out.AppendLine("'-----------------------------------------")
    lC += 1 : out.AppendLine("Public parentScript As __CLASSNAME__")
    lC += 1 : out.AppendLine()
    For i = 0 To LINES.Length - 1
      Dim parts() = Split(LINES(i), vbTab)
      If parts.Length < FormInfoCols.Length Or parts(0).StartsWith("//") Then Continue For
      If parts(FormInfoCols.name) = "" Or parts(FormInfoCols.type) = "" Then Continue For
      lC += 1 : out.AppendLine("Public WithEvents " + parts(FormInfoCols.name) + " As " + parts(FormInfoCols.type))
    Next
    lC += 1 : out.AppendLine("'-----------------------------------------")
    lC += 1 : out.AppendLine()
    lC += 1 : out.AppendLine("Sub New(parentClass As __CLASSNAME__)")
    lC += 1 : out.AppendLine("  parentScript = parentClass")
    lC += 1 : out.AppendLine("  InitializeComponents()")
    lC += 1 : out.AppendLine("End Sub")
    lC += 1 : out.AppendLine("")
    lC += 1 : out.AppendLine("Shadows Sub Show()")
    lC += 1 : out.AppendLine("  'hier wird das Fenster ins Dockpanel eingefügt")
    lC += 1 : out.AppendLine("  'ohne diesen Aufruf wäre es eine normale Form:")
    lC += 1 : out.AppendLine("  ZZ.IdeHelper.BeforeShowAddinWindow(Me.GetPersistString(), Me)")
    lC += 1 : out.AppendLine("  MyBase.Show()")
    lC += 1 : out.AppendLine("End Sub")
    lC += 1 : out.AppendLine("")
    lC += 1 : out.AppendLine("Public Overrides Function GetPersistString() As String")
    lC += 1 : out.AppendLine("  Return ""Toolbar|##|tbScriptWin|##|__CLASSNAME__." + formName + """")
    lC += 1 : out.AppendLine("End Function")
    lC += 1 : out.AppendLine("")
    lC += 1 : out.AppendLine()
    lC += 1 : out.AppendLine("Sub InitializeComponents()")
    lC += 1 : out.AppendLine("  Me.Text = """ + formText + """")

    Dim ehName As String = findEventHandler(methods, "On" + formName)
    If Not String.IsNullOrEmpty(ehName) Then
      Dim eventName = ehName.Substring(ehName.LastIndexOf("_") + 1)
      lC += 1 : out.AppendLine("  AddHandler Me." + eventName + ", AddressOf parentScript." + ehName + "")
    End If
    Dim parents(50) As String
    parents(0) = "Me"
    Dim props() As String
    For i = 0 To LINES.Length - 1
      Dim parts() = Split(LINES(i), vbTab)
      If parts.Length < FormInfoCols.Length Or parts(0).StartsWith("//") Then Continue For
      If parts(FormInfoCols.type) = "Form" Then
        props = splitProps(parts(FormInfoCols.Properties))
        For Each prop In props
          lC += 1 : out.AppendLine("  Me." + prop)
        Next
        Continue For
      End If
      Dim nam As String = parts(FormInfoCols.name)
      Dim parentCtrl As String, indent As Integer
      getParentControl(parents, parts(FormInfoCols.indent), parentCtrl, indent)
      parents(indent + 1) = nam
      lC += 1 : out.AppendLine("  " + nam + " = New " + parts(FormInfoCols.type))
      lC += 1 : out.AppendLine("  " & nam & ".Bounds = New Rectangle(" & parts(FormInfoCols.left) & ", " & parts(FormInfoCols.top) & ", " & parts(FormInfoCols.width) & ", " & parts(FormInfoCols.height) & ")")
      lC += 1 : out.AppendLine("  " + nam + ".Name = """ + nam + """")
      props = splitProps(parts(FormInfoCols.Properties))
      For Each prop In props
        lC += 1 : out.AppendLine("  " + nam + "." + prop)
      Next
      ehName = findEventHandler(methods, nam)
      If Not String.IsNullOrEmpty(ehName) Then
        Dim eventName = ehName.Substring(ehName.LastIndexOf("_") + 1)
        lC += 1 : out.AppendLine("  AddHandler " + nam + "." + eventName + ", AddressOf parentScript." + ehName + "")
      End If
      '  out.AppendLine("  AddHandler " + nam + ".Click, AddressOf parentScript.globalEvent")
      lC += 1 : out.AppendLine("  " + parentCtrl + ".Controls.Add(" + nam + ")")
      lC += 1 : out.AppendLine()
    Next
    lC += 1 : out.AppendLine("End Sub")
    lC += 1 : out.AppendLine("End Class")

    Return out.ToString
  End Function

  Sub getParentControl(ByVal indents() As String, ByVal data As String, ByRef parentCtrl As String, ByRef indent As Integer)
    Dim parts() = Split(data, ".", 2)
    indent = Val(parts(0))
    If parts.Length = 1 Then
      parentCtrl = indents(indent)
    Else
      parentCtrl = indents(indent) + "." + parts(1)
    End If
  End Sub

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
