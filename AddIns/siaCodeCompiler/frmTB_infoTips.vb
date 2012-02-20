Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Reflection

Public Class frmTB_infoTips
  Public skipNavIndexList As Boolean = False

  Dim referencesLoadedFrom As String
  Dim netFrameworkDir As String

  Dim varTypeDict As Dictionary(Of String, String)
  Dim actType As Type ', actAssembly As String

  Dim sortMan As EV.Windows.Forms.ListViewSortManager

  Dim bmNum(bmCount + 1), bmCont(bmCount + 1) As Label, bmTag(bmCount + 1) As String

  Const bmCount As Integer = 30

  Private _actAssembly As String
  Public Property actAssembly() As String
    Get
      Return _actAssembly
    End Get
    Set(ByVal value As String)
      _actAssembly = value
      labActAssembly.Text = value
    End Set
  End Property

  Public Overrides Function GetPersistString() As String
    Return tbInfoTips_ID
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    If IDE.Glob.para("frmTB_infoTips__isDockWin", "TRUE") = "TRUE" Then
      IDE.BeforeShowAddinWindow(Me.GetPersistString(), Me)
    End If
    MyBase.Show()
  End Sub

  Private Sub ComboBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyUp
    If e.KeyCode = Windows.Forms.Keys.Enter Then
      startTypeSearch()
    End If
  End Sub

  Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
    startTypeSearch(True)
  End Sub

  Function getActScintilla() As ScintillaNet.Scintilla
    Return CType(IDE.getActiveTab(), Object).RTF

  End Function

  Function getAssemblies() As ArrayList
    Return getLinesByKeyword("REF")
  End Function

  Function getIncludeNS() As ArrayList
    Return getLinesByKeyword("IMP")
  End Function

  Function getLinesByKeyword(ByVal kw As String) As ArrayList
    Dim abpos = 3 'kw.Length
    'kw = kw.ToLower
    Dim lines() As String = Split(TextBox1.Text, vbNewLine)
    Dim out As New ArrayList(lines.Length)
    Dim outCounter As Integer = -1
    For i = 0 To lines.Length - 1
      'If lines(i).ToLower.StartsWith(kw) Then
      If lines(i).Length < abpos Then Continue For
      If lines(i).Substring(0, abpos) = kw Then
        out.Add(lines(i).Substring(abpos).Trim)
      End If
    Next
    Return out
  End Function

  Sub addRef(ByVal fileName As String)
    If fileName.ToLower.StartsWith("#reference") Then
      fileName = fileName.Substring(10).Trim

      If IO.File.Exists(fileName) Then TextBox1.AppendText("REF " + fileName + vbNewLine) : Return
      If IO.File.Exists("C:\yEXE\" + fileName) Then TextBox1.AppendText("REF C:\yEXE\" + fileName + vbNewLine) : Return
      If IO.File.Exists(netFrameworkDir + fileName) Then TextBox1.AppendText("REF " + netFrameworkDir + fileName + vbNewLine) : Return
    End If

    If fileName.ToLower.StartsWith("#imports") Then TextBox1.AppendText("IMP " + fileName.Substring(8) + vbNewLine)
  End Sub
  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    fetchReferencesFromTab(IDE.getActiveTab)
  End Sub
  Sub fetchReferencesFromTab(ByVal tab As IDockContentForm)
    referencesLoadedFrom = tab.URL
    Dim sc As ScintillaNet.Scintilla = tab.RTF
    TextBox1.Text = ""
    For Each lin As ScintillaNet.Line In sc.Lines
      addRef(lin.Text)
    Next
    For Each lin As String In Split(getInternalReferenceList(), vbNewLine)
      addRef(lin)
    Next
    For Each lin As String In IO.File.ReadAllLines(IDE.GetSettingsFolder + "scriptClass\references.txt")
      addRef(lin)
    Next
  End Sub

  Private Function getInternalReferenceList() As String
    Return "#Reference System.dll" + vbNewLine + _
           "#Reference mscorlib.dll" + vbNewLine + _
           "#Reference ScriptIDE.Core.dll" + vbNewLine + _
           "#Reference ScriptIDE.ScriptWindowHelper.dll" + vbNewLine + _
           "#Reference Microsoft.VisualBasic.dll" + vbNewLine + _
           "#Reference System.Windows.Forms.dll" + vbNewLine + _
           "#Imports System" + vbNewLine + _
           "#Imports ScriptIDE" + vbNewLine + _
           "#Imports Microsoft.VisualBasic" + vbNewLine + _
           "#Imports System.Windows.Forms" + vbNewLine + _
           ""
  End Function

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    startTypeSearch()
  End Sub

  Function getTypeType(ByVal typ As Type) As String
    Return If(typ.IsNotPublic, "NotPublic ", "Public ") + _
           If(typ.IsClass, "Class ", "") + If(typ.IsInterface, "Interface ", "") + _
           If(typ.IsEnum, "Enum ", "")
  End Function

  Sub startTypeSearch(Optional ByVal fromHistory As Boolean = False)
    Dim suchWort = ComboBox2.Text
    If suchWort.Length < 3 Then Beep() : Exit Sub

    If Not fromHistory Then
      If ComboBox2.Items.Contains(suchWort) Then
        ComboBox2.Items.Remove(suchWort)
      End If

      ComboBox2.Items.Add(suchWort)
      ComboBox2.SelectedIndex = ComboBox2.Items.Count - 1
    End If

    ListView1.Hide()
    ListView1.Items.Clear()
    ListView1.Groups.Clear()

    Dim assemblyList As ArrayList = getAssemblies()

    If suchWort.StartsWith("FILE:", StringComparison.CurrentCultureIgnoreCase) Then
      actAssembly = suchWort.Substring(5)
      Dim dat As New ArrayList() : dat.Add(actAssembly)
      findTypeList("", dat, New ArrayList())
      labClassName.Text = "Typenliste"
      ListView1.Show()
      Exit Sub
    End If
    If Not String.IsNullOrEmpty(actAssembly) Then
      assemblyList.Insert(0, actAssembly)
    End If

    Dim typ = findTypeExact(suchWort, assemblyList, getIncludeNS())
    If typ Is Nothing Then
      'ListView1.Items.Add("(Typ nicht gefunden)")
      findTypeList(suchWort, assemblyList, getIncludeNS())
      labClassName.Text = "kein exakter Treffer, " & ListView1.Items.Count & " ähnliche"
      zoomRTF(RichTextBox1, "Zum eingegebenen Suchwort konnte kein exakter Treffer ermittelt werden.<br><br><f fg='#999' size='15'>Evtl. ist die Assembly musst du in den <a>Optionen</a> einen Verweis auf die Assembly hinzufügen.</color><f size='17'>")
      ListView1.Show()
      Exit Sub
    End If
    labClassName.Text = getTypeType(typ) + typ.FullName
    actType = typ
    zoomTypeInfo(actType)

    Dim grpMethod = ListView1.Groups.Add("sub", "Methoden")
    Dim grpProps = ListView1.Groups.Add("prop", "Eigenschaften")
    Dim grpEvents = ListView1.Groups.Add("event", "Ereignisse")
    Dim grpFields = ListView1.Groups.Add("fields", "Öffentliche Felder")
    Dim grpTypes = ListView1.Groups.Add("fields", "Typen")

    Dim methods() = typ.GetMethods
    For Each item In methods
      If item.IsSpecialName Or item.DeclaringType IsNot typ Then Continue For

      Dim mods = getMethodModifiers(item)
      Dim paras = getMethodParameters(item, False, False)
      Dim methodTyp As String = "Function"
      If item.ReturnType.FullName = "System.Void" Then methodTyp = "Sub"

      Dim lvi = ListView1.Items.Add(mods + methodTyp + " " + item.Name + "(" + paras + ")", "function")

      lvi.Group = grpMethod
      lvi.Tag = item
      If methodTyp = "Sub" Then
        lvi.ImageKey = "sub"
        lvi.SubItems.Add("-")
        'lvi.Tag = mods + " Sub " + item.Name + "(" + paras.ToString + ")"
      Else
        lvi.SubItems.Add(item.ReturnType.FullName)
        'lvi.Tag = mods + " Function " + item.Name + "(" + paras.ToString + ") As " + item.ReturnType.FullName
      End If
    Next

    Dim props() = typ.GetProperties
    For Each item In props
      If item.DeclaringType IsNot typ Then Continue For
      Dim mods As String = If(item.CanRead, "", " (WriteOnly)") + If(item.CanWrite, "", " (ReadOnly)")

      Dim lvi = ListView1.Items.Add(item.Name + mods, "property")
      lvi.Group = grpProps : lvi.SubItems.Add(item.PropertyType.FullName)
      lvi.Tag = item ' lvi.Tag = mods + " Property " + item.ToString + " As " + item.PropertyType.FullName
    Next

    Dim events() = typ.GetEvents
    For Each item In events
      If item.DeclaringType IsNot typ Then Continue For

      Dim lvi = ListView1.Items.Add(item.Name, "event")
      lvi.Group = grpEvents : lvi.SubItems.Add("-")
      lvi.Tag = item 'item.ToString + vbNewLine + item.EventHandlerType.ToString
    Next

    Dim fields() = typ.GetFields
    For Each item In fields
      If item.DeclaringType IsNot typ Then Continue For
      Dim txt As String = item.Name
      Try : txt &= " = " & CStr(item.GetRawConstantValue()) : Catch : End Try
      Dim lvi = ListView1.Items.Add(txt, "field")
      If item.IsLiteral Then lvi.ImageKey = "const"
      lvi.Group = grpFields : lvi.SubItems.Add(item.FieldType.FullName)
      lvi.Tag = item 'item.ToString + vbNewLine + item.EventHandlerType.ToString
    Next

    Dim base = typ.BaseType
    Dim idx As Integer = 1
    While base IsNot Nothing
      Dim lvi = ListView1.Items.Add(idx.ToString("00") + " " + base.Name, "type")
      lvi.Group = grpTypes : lvi.SubItems.Add(base.FullName)
      lvi.Tag = base 'item.ToString + vbNewLine + item.EventHandlerType.ToString
      base = base.BaseType
      idx += 1
    End While
    ListView1.Show()
  End Sub

  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    'listTypes
    ListView1.Items.Clear()
    ListView1.Hide()
    findTypeList(ComboBox2.Text, getAssemblies(), getIncludeNS())
    ListView1.Show()
  End Sub

  Private Sub btnOpenAssembly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenAssembly.Click
    Using ofd As New OpenFileDialog
      ofd.Title = "Assembly laden ..."
      ofd.Filter = "DLL- und EXE-Dateien|*.exe;*.dll|Alle Dateien|*.*"
      If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
        ComboBox2.Text = "FILE:" + ofd.FileName
        startTypeSearch()
      End If
    End Using
  End Sub

  Private Sub btnSCList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSCList.Click
    ListView1.Items.Clear()
    Dim fileList() = IO.Directory.GetFiles(ParaService.SettingsFolder + "dllCache\", "*.dll")
    For Each fileSpec In fileList
      Dim lvi = ListView1.Items.Add(IO.Path.GetFileNameWithoutExtension(fileSpec), "namespace")
      lvi.SubItems.Add("FILE:" + fileSpec)
      
    Next
  End Sub

  Sub fetchFromCode(ByVal tab As IDockContentForm)
    If referencesLoadedFrom <> tab.URL Then
      '   fetchReferencesFromTab(tab)
      loadVarsAndTypes()
    End If
    Dim sc As ScintillaNet.Scintilla = tab.RTF

    Dim wordStart, wordEnd As Integer
    Dim line = sc.Selection.Range.StartingLine
    Dim selStart As Integer = line.SelectionStartPosition - line.StartPosition
    For wordStart = selStart To 0 Step -1
      If Char.IsLetterOrDigit(line.Text.Substring(wordStart - 1, 1)) = False Then Exit For
    Next
    For wordEnd = selStart To line.Text.Length - 2
      If Char.IsLetterOrDigit(line.Text.Substring(wordEnd, 1)) = False Then Exit For
    Next

    Dim wordUnderCursor = line.Text.Substring(wordStart, wordEnd - wordStart)
    If wordUnderCursor.Length < 2 Then Exit Sub
    ComboBox2.Text = wordUnderCursor

    If varTypeDict.ContainsKey(wordUnderCursor.ToLower) Then
      ComboBox2.Text = varTypeDict(wordUnderCursor.ToLower)
      txtEventCtrlName.Text = wordUnderCursor : txtEventTypeName.Text = ComboBox2.Text
    End If
    startTypeSearch()
  End Sub

  Private Sub ListView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles ListView1.ItemDrag
    On Error Resume Next
    Dim out As String = ""
    If ListView1.SelectedItems.Count = 0 Then Exit Sub

    For Each lvi In ListView1.SelectedItems
      If TypeOf lvi.Tag Is MemberInfo Then
        Dim item As MemberInfo = lvi.Tag

        Dim ctrlName As String = actType.Name

        If IsShared(item) = False And txtEventCtrlName.Text <> "" And actType.FullName.EndsWith(txtEventTypeName.Text, StringComparison.InvariantCultureIgnoreCase) Then _
          ctrlName = txtEventCtrlName.Text

        Select Case item.MemberType
          Case MemberTypes.Method
            Dim method As MethodInfo = item
            Dim paras = getMethodParameters(item, False, False)
            If method.ReturnType.FullName <> "System.Void" Then
              out &= "Dim RESULT As " & method.ReturnType.FullName & vbNewLine
              out &= "RESULT = "
            End If
            ' out &= Microsoft.VisualBasic.CompilerServices.Utils.MethodToString(method)
            out &= ctrlName & "." & method.Name & "(" & getMethodParameters(method, False, False) & ")"
            out &= vbNewLine

            'If method.ReturnType.FullName = "System.Void" Then
            '  RichTextBox1.Text = getMethodModifiers(method) + " Sub " + method.Name + "(" + getMethodParameters(method) + ")"
            'Else
            '  RichTextBox1.Text = getMethodModifiers(method) + " Function " + method.Name + "(" + getMethodParameters(method) + ") As " + method.ReturnType.FullName
            'End If

          Case MemberTypes.Property
            Dim prop As PropertyInfo = item

            out &= ctrlName & "." & prop.Name & "(" & getMethodParameters(prop.GetGetMethod(), False, False) & ")"
            out &= vbNewLine
            'out += "<a>[+]</a> " + PropertyToString(prop) + "<br>"
            'Dim mods As String = If(prop.CanRead, "", "WriteOnly ") + If(prop.CanWrite, "", "ReadOnly ")
            'RichTextBox1.Text = mods + " Property " + prop.ToString + " As " + prop.PropertyType.FullName


          Case MemberTypes.Event
            Dim evnt As EventInfo = item
            Dim method As MethodInfo = evnt.EventHandlerType.GetMethod("Invoke")


            out += "AddHandler " + ctrlName + "." + evnt.Name + ", AddressOf " + ctrlName + "_" + evnt.Name + vbNewLine
            out += "RemoveHandler " + ctrlName + "." + evnt.Name + ", AddressOf " + ctrlName + "_" + evnt.Name + vbNewLine
            If method IsNot Nothing Then
              out &= vbNewLine
              out += "Sub " + ctrlName + "_" + evnt.Name + ""
              out += "(" + getMethodParameters(method, True, False) + ")" + " ''... Handles " + ctrlName + "." + evnt.Name + vbNewLine
              out += "  " + vbNewLine
              out += "End Sub" + vbNewLine
            End If


          Case MemberTypes.Field
            Dim fld As FieldInfo = item

            out &= actType.FullName & "." & fld.Name
            out &= " '=" & CStr(fld.GetRawConstantValue())
            out &= vbNewLine

        End Select
      End If
    Next

    Dim d As New DataObject()
    d.SetText(out)
    ListView1.DoDragDrop(d, DragDropEffects.All)

  End Sub

  Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
    On Error Resume Next
    If ListView1.SelectedItems.Count = 0 Then Exit Sub

    Dim elName As String = ListView1.SelectedItems(0).SubItems(0).Text
    Dim typName As String = ListView1.SelectedItems(0).SubItems(1).Text
    If typName = "-" Then Exit Sub

    If elName.StartsWith("withEvents", StringComparison.InvariantCultureIgnoreCase) Then
      txtEventCtrlName.Text = elName.Substring(10).Trim
      txtEventTypeName.Text = typName
    End If

    ComboBox2.Text = typName
    startTypeSearch()
  End Sub

  Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
    On Error Resume Next

    Dim out As String = ""
    If ListView1.SelectedItems.Count = 0 Then _
      zoomRTF(RichTextBox1, "<b>Willkommen im Reflection-Browser</b><br><br>...bitte wähle einen Eintrag aus") : Exit Sub

    If TypeOf ListView1.SelectedItems(0).Tag Is MemberInfo Then
      Dim item As MemberInfo = ListView1.SelectedItems(0).Tag
      out &= "<h1> " + item.Name + " </h1><br>"
      Select Case item.MemberType
        Case MemberTypes.Method
          Dim method As MethodInfo = item
          ' out &= Microsoft.VisualBasic.CompilerServices.Utils.MethodToString(method)
          out &= "<a>[+]</a> " + MethodToString(method)
          'If method.ReturnType.FullName = "System.Void" Then
          '  RichTextBox1.Text = getMethodModifiers(method) + " Sub " + method.Name + "(" + getMethodParameters(method) + ")"
          'Else
          '  RichTextBox1.Text = getMethodModifiers(method) + " Function " + method.Name + "(" + getMethodParameters(method) + ") As " + method.ReturnType.FullName
          'End If

        Case MemberTypes.Property
          Dim prop As PropertyInfo = item

          out += "<a>[+]</a> " + PropertyToString(prop) + "<br>"
          'Dim mods As String = If(prop.CanRead, "", "WriteOnly ") + If(prop.CanWrite, "", "ReadOnly ")
          'RichTextBox1.Text = mods + " Property " + prop.ToString + " As " + prop.PropertyType.FullName


        Case MemberTypes.Event
          Dim evnt As EventInfo = item
          Dim method As MethodInfo = evnt.EventHandlerType.GetMethod("Invoke")
          out += "<f fg='#080'>'' " + evnt.ToString + "</color><br>"

          Dim ctrlName As String = actType.Name

          If txtEventCtrlName.Text <> "" And actType.FullName.EndsWith(txtEventTypeName.Text, StringComparison.InvariantCultureIgnoreCase) Then _
            ctrlName = txtEventCtrlName.Text

          If method IsNot Nothing Then
            out += "<a>[+]</a> <f fg='#008'>Sub</color> <font size='20'><b>" + ctrlName + "_" + evnt.Name + "</b><font size='17'>"
            out += "(" + getMethodParameters(method, True, True) + ")" + " <f fg='#080'>''... Handles " + ctrlName + "." + evnt.Name + "<br>"
            out += "  " + "<br>"
            out += "<f fg='#008'>End Sub</color>" + "<br>"
          End If
          out += "<a>[+]</a> <f fg='#008'>AddHandler</color> " + ctrlName + "." + evnt.Name + ", <f fg='#008'>AddressOf</color> " + ctrlName + "_" + evnt.Name + "<br>"
          out += "<a>[+]</a> <f fg='#008'>RemoveHandler</color> " + ctrlName + "." + evnt.Name + ", <f fg='#008'>AddressOf</color> " + ctrlName + "_" + evnt.Name + "<br>"


        Case MemberTypes.Field
          Dim fld As FieldInfo = item
          out &= "<a>[+]</a> " + FieldToString(fld)
          out &= " = <b>" & CStr(fld.GetRawConstantValue()) & "</b>"



      End Select
      out &= getInfoForMember(item)
    End If

    If TypeOf ListView1.SelectedItems(0).Tag Is Type Then
      zoomTypeInfo(ListView1.SelectedItems(0).Tag)
      Exit Sub
    End If

    If TypeOf ListView1.SelectedItems(0).Tag Is String Then
      out &= "<h1>" + ListView1.SelectedItems(0).Tag + "</h1><br>"

    End If

    If TypeOf ListView1.SelectedItems(0).Tag Is String() Then
      Dim item As String() = ListView1.SelectedItems(0).Tag
      out &= "<h1>" + item(0) + "</h1><br>"
      out &= item(1)
    End If

    zoomRTF(RichTextBox1, out)
    ' txtZoom.Text = lvi.Tag
  End Sub

  Sub zoomTypeInfo(ByVal typ As Type)
    Dim out As String = ""
    out &= "<h1> " + getTypeType(typ) + typ.FullName + " </h1><br>"
    out &= "<b>Assembly:</b><br>FileSpec: <a>" + typ.Assembly.Location + "</a>" + "<br>DisplayName: " + typ.Assembly.FullName + "<br>"
    out &= getInfoForType(typ)
    zoomRTF(RichTextBox1, out)
  End Sub

  Function getInfoForType(ByVal typ As Type) As String
    On Error Resume Next
    Dim info = JimBlackler.DocsByReflection.DocsByReflection.XMLFromType(typ)
    If Err.Number <> 0 Then Return "<br><f fg='#999'>XML Doc not available: " + Err.Description + "</color>"

    Dim out = ""
    out += "<br><b>Summary: </b><br>" + info("summary").InnerText.Trim
    out += "<br><br><b>Remarks: </b><br>" + info("remarks").InnerText.Trim
    Return out
  End Function

  Function getInfoForMember(ByVal typ As MemberInfo) As String
    On Error Resume Next
    Dim info = JimBlackler.DocsByReflection.DocsByReflection.XMLFromMember(typ)
    If Err.Number <> 0 Then Return "<br><f fg='#999'>XML Doc not available: " + Err.Description + "</color>"

    Dim out = ""
    out += "<br><b>Return Value: </b>" + info("returns").InnerText.Trim
    out += "<br><br><b>Summary: </b><br>" + info("summary").InnerText.Trim
    Return out
  End Function

  Sub addBMLabel(ByVal num As Integer, ByVal top As Integer, ByVal txt As String)
    Dim lab1 As New Label
    lab1.ForeColor = Drawing.Color.FromArgb(221, 221, 221)
    lab1.Font = New Drawing.Font("Courier New", 8, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point)
    lab1.Bounds = New Drawing.Rectangle(2, top + 2, 20, 18)
    lab1.Text = num.ToString("#0")
    AddHandler lab1.MouseClick, AddressOf onBMLab_MouseClick
    pnlSB1.Controls.Add(lab1)
    lab1.Tag = num : bmNum(num) = lab1
    '----------------------------------------
    lab1 = New Label
    lab1.ForeColor = Drawing.Color.FromArgb(221, 221, 221)
    lab1.BackColor = Drawing.Color.FromArgb(68, 68, 68)
    ' lab1.Font = New Drawing.Font("Courier New", 9, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point)
    lab1.Bounds = New Drawing.Rectangle(24, top, pnlSB1.Width - 26, 18)
    lab1.Text = txt : lab1.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right
    AddHandler lab1.MouseClick, AddressOf onBMLab_MouseClick
    pnlSB1.Controls.Add(lab1)
    lab1.Tag = num : bmCont(num) = lab1
  End Sub

  Sub onBMLab_MouseClick(ByVal sender As System.Object, ByVal e As MouseEventArgs)
    Dim btnIdx As Integer = sender.tag
    If e.Button = Windows.Forms.MouseButtons.Left Then
      Dim spl() = Split(bmTag(btnIdx), "|||")
      If spl.Length > 1 AndAlso spl(1) <> "" Then actAssembly = spl(1)
      ComboBox2.Text = spl(0)
      startTypeSearch()
    End If
    If e.Button = Windows.Forms.MouseButtons.Right Then
      bmTag(btnIdx) = actType.FullName & "|||" & actAssembly
      bmCont(btnIdx).Text = actType.Name
      saveBookmark(btnIdx)
    End If
    labBMHighlight.Bounds = padRectangle(bmCont(btnIdx).Bounds, 1)
    labBMHighlight.SendToBack() : labBMHighlight.Show()
  End Sub
  Function padRectangle(ByVal rect As Drawing.Rectangle, ByVal padAmount As Integer) As Drawing.Rectangle
    Return New Drawing.Rectangle(rect.X - padAmount, rect.Y - padAmount, rect.Width + (2 * padAmount), rect.Height + (2 * padAmount))
  End Function

  Sub readBookmarks()
    On Error Resume Next
    Dim bmFile() = IO.File.ReadAllLines(ParaService.SettingsFolder + "reflection_Bookmarks.txt")
    ReDim Preserve bmFile(bmCount)
    For i = 0 To 19
      Dim spl() = Split(bmFile(i), vbTab)
      ReDim Preserve spl(3)
      addBMLabel(i + 1, (i * 21) + 4, spl(0))
      bmTag(i + 1) = spl(1)
    Next
  End Sub

  Sub saveBookmark(ByVal i As Integer)
    On Error Resume Next
    Dim bmFile() = IO.File.ReadAllLines(ParaService.SettingsFolder + "reflection_Bookmarks.txt")
    ReDim Preserve bmFile(bmCount)
    bmFile(i - 1) = bmCont(i).Text & vbTab & bmTag(i)
    IO.File.WriteAllLines(ParaService.SettingsFolder + "reflection_Bookmarks.txt", bmFile)
  End Sub

  Private Sub frmTB_infoTips_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    IDE.Glob.saveFormPos(Me)
  End Sub

  Private Sub frmTB_infoTips_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    If e.KeyCode = Keys.Escape Then
      Me.Hide()
    End If
  End Sub

  Private Sub frmTB_infoTips_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    On Error Resume Next
    IDE.Glob.readFormPos(Me)

    Dim testAssembly As System.Reflection.Assembly = GetType(Object).Assembly
    netFrameworkDir = IDE.Glob.fp(Path.GetDirectoryName(testAssembly.Location))

    sortMan = New EV.Windows.Forms.ListViewSortManager(ListView1, New Type() { _
                 GetType(EV.Windows.Forms.ListViewTextCaseInsensitiveSort), _
                 GetType(EV.Windows.Forms.ListViewTextCaseInsensitiveSort)}, _
                 0, SortOrder.Ascending)

    TextBox1.Text = IO.File.ReadAllText(ParaService.AppPath + "reflection_ReferenceList.txt")

    chkDockWin.Checked = IDE.Glob.para("frmTB_infoTips__isDockWin", "TRUE") = "TRUE"

    zoomRTF(RichTextBox1, "<h1>Willkommen im Reflection-Browser</h1><br><u>Bitte beachten:</u> beim ersten Start muss in den <a>Optionen</a> die Referenzliste erstellt und gespeichert werden.<br><br><b>...ansonsten:</b> einfach einen Suchbegriff eingeben und ENTER drücken!")
    showSidebarPane("1")

    readBookmarks()
  End Sub

  Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
    On Error Resume Next
    ComboBox2.SelectedIndex -= 1
    ' startTypeSearch()

  End Sub

  Function getVarTypeMatches() As MatchCollection
    Dim scriptCode = IO.File.ReadAllText(IDE.GetSettingsFolder + "scriptClass\vbnetPrefix.txt")
    scriptCode += vbNewLine + getActScintilla.Text
    Return Regex.Matches(scriptCode, "(dim|public|function|private|property|friend|shared|protected|withevents) ([a-zA-Z0-9_]+)([ ]*\([^\)]*\))? As ([a-zA-Z0-9_.]+)", RegexOptions.IgnoreCase)
  End Function

  Sub loadVarsAndTypes()
    On Error Resume Next
    varTypeDict = New Dictionary(Of String, String)

    'ListView1.Items.Clear() : ListView1.Groups.Clear()
    Dim matches = getVarTypeMatches()
    For Each m As Match In matches
      'Dim lvi = ListView1.Items.Add(m.Groups(1).Value + " " + m.Groups(2).Value, "enum")
      'lvi.SubItems.Add(m.Groups(4).Value)
      varTypeDict.Add(m.Groups(2).Value.ToLower, m.Groups(4).Value)
    Next

  End Sub

  Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
    ListView1.Items.Clear() : ListView1.Groups.Clear()
    Dim matches = getVarTypeMatches()
    For Each m As Match In matches
      Dim lvi = ListView1.Items.Add(m.Groups(1).Value + " " + m.Groups(2).Value, "enum")
      lvi.SubItems.Add(m.Groups(4).Value)
      lvi.Tag = New String() {m.Groups(2).Value, m.Value}

    Next
  End Sub
  Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
    ListView1.Items.Clear() : ListView1.Groups.Clear()
    Dim matches = getVarTypeMatches()
    For Each m As Match In matches
      If LCase(m.Groups(1).Value) = "withevents" Then
        Dim lvi = ListView1.Items.Add("WithEvents " + m.Groups(2).Value, "field")
        lvi.SubItems.Add(m.Groups(4).Value)
        lvi.Tag = New String() {m.Groups(2).Value, m.Value}
      End If
    Next
  End Sub


  Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
    ScriptIDE.Main.MVPS.clsFormBorder.moveMeHwnd(Me.Handle)
  End Sub

  Private Sub btnSaveRefList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRefList.Click
    IO.File.WriteAllText(ParaService.AppPath + "reflection_ReferenceList.txt", TextBox1.Text)

  End Sub

  Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    On Error Resume Next
    TextBox1.Text = IO.File.ReadAllText(ParaService.AppPath + "reflection_ReferenceList.txt")

  End Sub


  Private Sub chkTopmost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTopmost.CheckedChanged
    Me.TopMost = chkTopmost.Checked
  End Sub

  Private Sub CheckBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDockWin.Click
    IDE.Glob.para("frmTB_infoTips__isDockWin") = If(chkDockWin.Checked, "TRUE", "FALSE")
    recreateReflectorWindow()
  End Sub

  Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

  End Sub

  Private Sub btnExpandExtraModus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpandExtraModus.Click
    If pnlExtraOptions.Visible Then
      pnlExtraOptions.Hide()
      btnExpandExtraModus.Text = "s"
      SplitContainer2.Top = pnlExtraOptions.Top
    Else
      pnlExtraOptions.Show()
      btnExpandExtraModus.Text = "p"
      SplitContainer2.Top = pnlExtraOptions.Top + pnlExtraOptions.Height
    End If
    SplitContainer2.Height = Me.ClientSize.Height - SplitContainer2.Top
  End Sub




  Private Sub RichTextBox1_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles RichTextBox1.LinkClicked
    If e.LinkText = "Optionen" Then
      btnExpandExtraModus_Click(Nothing, Nothing)
      Exit Sub
    End If

    If e.LinkText.Substring(1, 2) = ":\" Then
      Process.Start("explorer.exe", "/select," + e.LinkText)
      Exit Sub
    End If

    If e.LinkText = "[+]" Then
      Dim mousePos = RichTextBox1.PointToClient(Cursor.Position)

      Dim pos = RichTextBox1.GetCharIndexFromPosition(mousePos)
      Dim line = RichTextBox1.GetLineFromCharIndex(pos)
      Dim lines() = Split(RichTextBox1.Text, vbLf)
      Dim lineStr = lines(line)
      lineStr = lineStr.Substring(4)
      'Clipboard.Clear()

      Dim tab As IDockContentForm = IDE.getActiveTab()
      If TypeOf tab.RTF Is ScintillaNet.Scintilla Then
        Dim sc As ScintillaNet.Scintilla = tab.RTF
        sc.InsertText(vbNewLine + lineStr + vbNewLine)
      Else
        MsgBox(lineStr, , "Code Generator:")
      End If

      Exit Sub
    End If

    ComboBox2.Text = e.LinkText
    startTypeSearch()

  End Sub


  Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged

  End Sub

  'toggle Sidebar
  Private Sub lnkSidebar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles lnkSidebar2.Click, lnkSidebar1.Click
    Dim id As String = sender.Tag
    showSidebarPane(id)
  End Sub

  Sub showSidebarPane(ByVal id As String)
    IDE.Glob.para("frmTB_infoTips__sidebarPane") = id
    Dim sidebar As Panel = SplitContainer1.Panel1
    For Each ctrl As Control In sidebar.Controls
      If TypeOf ctrl Is LinkLabel Then
        ctrl.BackColor = If(ctrl.Name = "lnkSidebar" + id, Drawing.Color.SteelBlue, Drawing.Color.FromArgb(84, 97, 110))
      End If
      If TypeOf ctrl Is Panel Then
        ctrl.Visible = ctrl.Name = "pnlSB" + id
      End If
    Next
  End Sub

  Private Sub btnInsActTyp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsActTyp.Click
    txtEventTypeName.Text = actType.FullName
  End Sub

  Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
    Dim txt = IO.File.ReadAllText(ParaService.SettingsFolder + "Reflection_qRef.txt")
    Dim LINES() = Split(txt, vbNewLine)

    Dim n(10) As TreeNodeCollection
    n(0) = tvwQuickRef.Nodes
    For i = 0 To LINES.Length - 1
      Dim data() = Split(LINES(i), "|")
      Dim indent As Integer = data.Length - 3
      If indent < 0 Then Continue For

      Dim key As String = Trim(data(indent + 0))
      Dim text As String = Trim(data(indent + 1))
      Dim imageKey As String = Trim(data(indent + 2))

      n(indent).Add(key, text, imageKey)

    Next


  End Sub


  Private Sub pnlSB2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlSB2.Paint
    Dim x = 0, y = 0
    For i = 0 To ImageList1.Images.Count - 1
      e.Graphics.DrawImage(ImageList1.Images(i), x, y)
      x += 18
      If x > 80 Then x = 0 : y += 18

    Next
  End Sub

  Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click

  End Sub

  Private Sub tvwQuickRef_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwQuickRef.AfterSelect

  End Sub

  Private Sub tvwQuickRef_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwQuickRef.NodeMouseClick
    If e.Button = Windows.Forms.MouseButtons.Right Then
      ComboBox2.Text = e.Node.Name
      startTypeSearch()

    End If
  End Sub

End Class