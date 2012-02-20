Imports System.IO
Imports System.Reflection
Imports System.Xml

Public Class AddinInstance

  Private luaSpace As LuaInterface.Lua

  Public AssemblyFileName, ConnectClassName As String
  Public XMLFileSpec As String
  'Private p_LoadOnStart As Boolean
  Private p_Loaded As Boolean
  Private p_icon As Icon = My.Resources.invalidicon, p_hasIcon As Boolean = False
  Private p_assembly As Assembly
  Private p_id As String = ""
  
  Public ConnectRef As IAddinConnect
  Public Description As String
  Public IsScriptAddin As Boolean

  'Public hlp As cls_scriptHelper

  Public HandlesProtocols As Dictionary(Of String, Type)
  Public HandlesFiletypes As Dictionary(Of String, Type)

  Public Properties As New Properties
  Private m_paths As New Dictionary(Of String, ExtensionPath)()
  Private m_definedDoozers As IList(Of LazyLoadDoozer) = New List(Of LazyLoadDoozer)()

  Public ReadOnly Property DisplayName() As String
    Get
      Return Properties("displayname")
    End Get
  End Property
  Public ReadOnly Property ID() As String
    Get
      Return p_id 'Properties("name")
    End Get
  End Property

  Public ReadOnly Property Paths() As Dictionary(Of String, ExtensionPath)
    Get
      Return m_paths
    End Get
  End Property

  Public ReadOnly Property DefinedDoozers() As IList(Of LazyLoadDoozer)
    Get
      Return m_definedDoozers
    End Get
  End Property

  Public Function GetExtensionPath(ByVal pathName As String) As ExtensionPath
    If Not m_paths.ContainsKey(pathName) Then
      m_paths.Add(pathName, New ExtensionPath(pathName, Me))
      Return m_paths(pathName)
    End If
    Return m_paths(pathName)
  End Function

  Public Function CreateObject(ByVal className As String) As Object
    'If p_assembly Is Nothing Then Return Nothing
    'Dim typ() = p_assembly.GetTypes
    Try
      Return p_assembly.CreateInstance(className)
    Catch ex As Exception
      TT.Write("Addin-CreateObject " + className, ex.ToString, "err")
    End Try
  End Function

  Public ReadOnly Property HasIcon() As Boolean
    Get
      Return p_hasIcon
    End Get
  End Property
  Public ReadOnly Property Icon() As Icon
    Get
      If p_icon Is Nothing Then
        initAssembly()
        loadInfoFromAssembly(p_assembly)
      End If
      Return p_icon
    End Get
  End Property

  Public ReadOnly Property Loaded() As Boolean
    Get
      Return p_Loaded
    End Get
  End Property

  'Public Property LoadOnStart() As Boolean
  '  Get
  '    Return p_LoadOnStart
  '  End Get
  '  Set(ByVal value As Boolean)
  '    p_LoadOnStart = value
  '  End Set
  'End Property

  Private Sub initAssembly()
    If IsScriptAddin Then Return
    If p_assembly Is Nothing Then
      If AssemblyFileName(0) = ":" Then
        p_assembly = Assembly.Load(AssemblyFileName.Substring(1))
      Else
        p_assembly = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(XMLFileSpec), AssemblyFileName))
      End If
      If p_assembly Is Nothing Then MsgBox("Fehler beim Laden des AddIns: Ungültige .NET-Assembly" + vbNewLine + AssemblyFileName, , "Add-in-Verwaltung") : Exit Sub
    End If
  End Sub

  Private Sub loadInfoFromAssembly(ByVal a As Assembly)
    Try
      For Each typ In a.GetTypes()
        If typ.FullName.ToUpper.EndsWith(".CONNECT") Then
          ConnectClassName = typ.FullName
          Return
        End If
      Next
    Catch ex As ReflectionTypeLoadException
      Dim ex2 As String = ""
      For Each ex3 In ex.LoaderExceptions
        ex2 += "* " + ex3.Message + vbNewLine
      Next
      MsgBox("Fehler beim Laden des Add-ins: Mindestens ein Typ in der Assembly kann nicht geladen werden." + vbNewLine + vbNewLine + "Details:" + vbNewLine + ex2, MsgBoxStyle.Critical, "Add-in-Verwaltung: " + XMLFileSpec)
    Catch ex As Exception
      TT.DumpException("loadInfoFromAssembly", ex)
    End Try
  End Sub

  Public Sub ConnectAddin()
    If p_Loaded Then Exit Sub
    If Not String.IsNullOrEmpty(AssemblyFileName) Then
      initAssembly()

      If String.IsNullOrEmpty(ConnectClassName) Then
        loadInfoFromAssembly(p_assembly)
      End If

      If Not String.IsNullOrEmpty(ConnectClassName) Then
        ConnectRef = Me.CreateObject(ConnectClassName)
        ConnectRef.Connect(Nothing, ConnectMode.Startup, Me, Nothing)
      End If
    End If
    p_Loaded = True
    AddInTree.InsertAddIn(Me)
  End Sub

  Public Sub ConnectScript(ByVal assemblyRef As Assembly, ByVal ref As Object, ByVal mode As ConnectMode)
    ConnectRef = ref
    ConnectRef.Connect(Nothing, mode, Me, Nothing)
    p_assembly = assemblyRef

    p_Loaded = True
  End Sub

  Public Sub DisconnectAddin()
    On Error Resume Next
    If ConnectRef IsNot Nothing Then
      ConnectRef.Disconnect(DisconnectMode.IdeShutdown, Nothing)
      ConnectRef = Nothing
    End If
    p_Loaded = False
  End Sub

  Protected Overrides Sub Finalize()
    DisconnectAddin()
    MyBase.Finalize()
  End Sub

  Private Sub New()

  End Sub

  'Private Sub New(ByVal ser As String)
  '  'On Error Resume Next
  '  'Dim parts() = Split(ser, vbTab)
  '  'If parts.Length < 5 Then Exit Sub

  '  'AddinName = parts(0)
  '  'FileSpec = parts(1)
  '  'p_LoadOnStart = parts(2) = "True"
  '  'Description = parts(3)

  '  'If p_LoadOnStart Then Me.ConnectAddin()
  'End Sub

  Public Overrides Function ToString() As String
    Return Join(New String() {XMLFileSpec}, vbTab)
  End Function


  Private Shared instances As New List(Of AddinInstance)

  Public Shared Function ConnectFromScriptData(ByVal scriptFileSpec As String, ByVal xmlReader As XmlTextReader, ByVal assemblyRef As Assembly, ByVal ref As IAddinConnect, ByVal mode As ConnectMode) As AddinInstance
    'Addin doublettenCheck
    Dim checkFor = IO.Path.GetFileNameWithoutExtension(LCase(scriptFileSpec))
    Dim wasReloaded As Boolean = False
    If IsAddinLoaded(checkFor) Then RemoveAddIn(checkFor) : wasReloaded = True
    
    Try
      Dim inst As New AddinInstance
      inst.XMLFileSpec = scriptFileSpec
      inst.IsScriptAddin = True
      inst.p_id = checkFor
      While xmlReader.Read()
        If xmlReader.IsStartElement() Then
          Select Case xmlReader.LocalName
            Case "AddIn"
              inst.Properties = Properties.ReadFromAttributes(xmlReader)
              inst.SetupAddIn(xmlReader, Path.GetDirectoryName(scriptFileSpec))
              Exit Select
            Case Else
              TT.Write("Unknown add-in file.", xmlReader.LocalName, "err")
          End Select
        End If
      End While

      inst.ConnectScript(assemblyRef, ref, mode)
      instances.Add(inst)

      If wasReloaded Then
        AddInTree.RebuildTree()
      Else
        AddInTree.InsertAddIn(inst)
      End If

    Catch ex As Exception
      TT.DumpException("Error loading (script)Add-in " + scriptFileSpec, ex)
      MsgBox("Fehler beim Verbinden des Add-ins!" + vbNewLine + ex.Message + vbNewLine + vbNewLine + "(Weitere Informationen sind im TraceMonitor zu finden)", MsgBoxStyle.Critical, "Add-in-Verwaltung: " + scriptFileSpec)
    End Try
  End Function

#Region "Lua Addin"
  Private Function initluaspace() As LuaInterface.Lua
    Dim lua As New LuaInterface.Lua
    lua.RegisterFunction("trace", Me, Me.GetType().GetMethod("_Lua_trace"))
    lua.RegisterFunction("printline", Me, Me.GetType().GetMethod("_Lua_printLine"))
    lua.RegisterFunction("addin_path", Me, Me.GetType().GetMethod("_Lua_addinpath"))
    'lua("ide") = 
    lua("package")("path") += ";C:/yPara/scriptIDE4/luaLibs/?.lua"
    lua("package")("cpath") += ";C:\yPara\scriptIDE4\luaLibs\?.dll"
    Return lua
  End Function

  'Function Lua_getIdeHelper()
  '  TT.printLine(line,str1,str2)
  'end function
  Sub _Lua_printLine(ByVal line As Integer, ByVal str1 As String, ByVal str2 As String)
    TT.printLine(line, str1, str2)
  End Sub
  Sub _Lua_trace(ByVal para1 As String, Optional ByVal para2 As String = "", Optional ByVal typ As String = "trace")
    TT.Write(para1, para2, typ)
  End Sub
  Sub _Lua_addinpath(ByVal path As String, ByVal elName As String, ByVal props As LuaInterface.LuaTable, Optional ByVal hasSub As Boolean = False)
    Dim ext As ExtensionPath = GetExtensionPath(path)
    Dim _props As New Properties()
    For Each k In props.Keys
      _props.Set(k, props(k))
    Next
    ext.AddCodon(elName, _props, hasSub)
  End Sub

  Public Shared Function ConnectLuaAddin(ByVal fileSpec As String, ByVal mode As ConnectMode) As AddinInstance
    Dim inst As New AddinInstance
    inst.p_id = IO.Path.GetFileNameWithoutExtension(fileSpec)
    inst.luaSpace = inst.initluaspace()
    inst.IsScriptAddin = True
    inst.luaSpace.DoFile(fileSpec)
    inst.ConnectScript(Nothing, New LuaAddinWrapper(inst.luaSpace), mode)

    instances.Add(inst)

    AddInTree.RebuildTree()

    Return inst
  End Function

  Public Class LuaAddinWrapper
    Implements IAddinConnect
    Private luaSpace As LuaInterface.Lua
    Sub New(ByVal luaSpace As LuaInterface.Lua)
      Me.luaSpace = luaSpace
    End Sub
    Public Sub Connect(ByVal application As Object, ByVal connectMode As ConnectMode, ByVal addInInst As AddinInstance, ByRef custom As Object) Implements IAddinConnect.Connect
      luaSpace.GetFunction("addin_connect").Call(application, connectMode, addInInst, custom)
    End Sub
    Public Sub Disconnect(ByVal removeMode As DisconnectMode, ByRef custom As Object) Implements IAddinConnect.Disconnect
      luaSpace.GetFunction("addin_disconnect").Call(removeMode, custom)
    End Sub
    Public Function GetAddinWindow(ByVal PersistString As String) As System.Windows.Forms.Form Implements IAddinConnect.GetAddinWindow
      Return luaSpace.GetFunction("addin_getaddinwindow").Call(PersistString)(0)
    End Function
    Public Sub OnAddinUpdate(ByVal addinChanged As String, ByRef custom As Object) Implements IAddinConnect.OnAddinUpdate
      luaSpace.GetFunction("addin_onaddinupdate").Call(addinChanged, custom)
    End Sub
    Public Sub OnBeforeShutdown(ByRef cancel As Boolean, ByRef custom As Object) Implements IAddinConnect.OnBeforeShutdown
      luaSpace.GetFunction("addin_onbeforeshutdown").Call(cancel, custom)
    End Sub
    Public Sub OnNavigate(ByVal kind As NavigationKind, ByVal source As String, ByVal command As String, ByVal args As Object, ByRef returnValue As Object) Implements IAddinConnect.OnNavigate
      returnValue = luaSpace.GetFunction("addin_onnavigate").Call(kind, source, command, args)(0)
    End Sub
    Public Sub OnStartupComplete(ByRef custom As Object) Implements IAddinConnect.OnStartupComplete
      luaSpace.GetFunction("addin_onstartupcomplete").Call(custom)
    End Sub
  End Class

#End Region

  Public Shared Function ConnectFromFile(ByVal fileSpec As String) As AddinInstance
    'Addin doublettenCheck
    Dim checkFor = IO.Path.GetFileNameWithoutExtension(LCase(fileSpec))
    If checkFor = "siaidemain" AndAlso IsAddinLoaded(checkFor) Then Exit Function
    If IsAddinLoaded(checkFor) Then MsgBox("Fehler beim Verbinden des Add-ins!" + vbNewLine + "Es ist bereits ein Add-in mit der Bezeichnung '" + checkFor + "' geladen.", MsgBoxStyle.Critical, "Add-in-Verwaltung: " + fileSpec) : Exit Function

    Try
      Dim inst As New AddinInstance
      inst.XMLFileSpec = fileSpec
      inst.p_id = checkFor
      Using reader As New XmlTextReader(fileSpec)
        While reader.Read()
          If reader.IsStartElement() Then
            Select Case reader.LocalName
              Case "AddIn"
                inst.Properties = Properties.ReadFromAttributes(reader)
                inst.SetupAddIn(reader, Path.GetDirectoryName(fileSpec))
                Exit Select
              Case Else
                TT.Write("Unknown add-in file.", reader.LocalName, "err")
            End Select
          End If
        End While
      End Using
      'If inst.Properties("addInManagerHidden") = "true" Then connectNow = True

      'inst.LoadOnStart = connectNow
      'If connectNow Then 
      inst.ConnectAddin()
      instances.Add(inst)

      Return inst
    Catch ex As Exception
      TT.DumpException("Error loading Add-in " + fileSpec, ex)
      MsgBox("Fehler beim Verbinden des Add-ins!" + vbNewLine + ex.Message + vbNewLine + vbNewLine + "Details:" + vbNewLine + ex.ToString, MsgBoxStyle.Critical, "Add-in-Verwaltung: " + fileSpec)
    End Try
  End Function

  Sub SetupAddIn(ByVal reader As XmlReader, ByVal hintPath As String)
    While reader.Read()
      If reader.NodeType = XmlNodeType.Element AndAlso reader.IsStartElement() Then
        TT.Write("SetupAddIn - node", reader.LocalName, "dump")
        Select Case reader.LocalName
          Case "StringResources", "BitmapResources"
            'If reader.AttributeCount <> 1 Then
            '  Throw New AddInLoadException("BitmapResources requires ONE attribute.")
            'End If

            'Dim filename__1 As String = StringParser.Parse(reader.GetAttribute("file"))

            'If reader.LocalName = "BitmapResources" Then
            '  addIn.BitmapResources.Add(filename__1)
            'Else
            '  addIn.StringResources.Add(filename__1)
            'End If
          Case "Runtime", "Identity", "Dependency"
            TT.Write("Not yet implemented", "NodeType:" + reader.Name, "trace")
            'If Not reader.IsEmptyElement Then
            '  Runtime.ReadSection(reader, addIn, hintPath)
            'End If
          Case "Import"
            If Me.IsScriptAddin Then Continue While
            If reader.AttributeCount = 1 Then
              Me.AssemblyFileName = reader.GetAttribute(0)
            Else
              Me.AssemblyFileName = reader.GetAttribute("assembly")
              Me.ConnectClassName = reader.GetAttribute("class")
            End If
          Case "Doozer"
            If Not reader.IsEmptyElement Then
              Throw New AddInLoadException("Doozer nodes must be empty!")
            End If
            Dim doozerProp As Properties = Properties.ReadFromAttributes(reader)
            Me.DefinedDoozers.Add(New LazyLoadDoozer(Me, doozerProp))

          Case "Include"
            If reader.AttributeCount <> 1 Then
              Throw New AddInLoadException("Include requires ONE attribute.")
            End If
            If Not reader.IsEmptyElement Then
              Throw New AddInLoadException("Include nodes must be empty!")
            End If
            If hintPath Is Nothing Then
              Throw New AddInLoadException("Cannot use include nodes when hintPath was not specified (e.g. when AddInManager reads a .addin file)!")
            End If
            Dim fileName__2 As String = Path.Combine(hintPath, reader.GetAttribute(0))
            Dim xrs As New XmlReaderSettings()
            xrs.ConformanceLevel = ConformanceLevel.Fragment
            Using includeReader As XmlReader = XmlTextReader.Create(fileName__2, xrs)
              SetupAddIn(includeReader, Path.GetDirectoryName(fileName__2))
            End Using

          Case "Path"
            If reader.AttributeCount <> 1 Then
              Throw New AddInLoadException("Import node requires ONE attribute.")
            End If
            Dim pathName As String = reader.GetAttribute(0)
            Dim extPath As ExtensionPath = Me.GetExtensionPath(pathName)
            If Not reader.IsEmptyElement Then
              ExtensionPath.SetUp(extPath, reader, "Path")
            End If

          Case "Manifest"
            'addIn.Manifest.ReadManifestSection(reader, hintPath)

          Case Else
            'Throw New AddInLoadException("Unknown root path node:" & reader.LocalName)
            TT.Write("Unknown root path node:", reader.LocalName, "warn")
        End Select
      End If
    End While
  End Sub

  Public Shared Sub RemoveAddIn(ByVal AddInName As String)
    For Each inst In Addins
      If inst.ID = AddInName Then
        inst.DisconnectAddin() : instances.Remove(inst)
        Exit For
      End If
    Next
    AddInTree.RebuildTree()
  End Sub

  Public Shared Function IsAddinLoaded(ByVal AddinName As String) As Boolean
    AddinName = AddinName.ToLower
    For Each inst In Addins
      If inst.ID.ToLower = AddinName Then Return True
    Next
    Return False
  End Function

  Public Shared Function GetAddinReference(ByVal AddinName As String) As IAddinConnect
    AddinName = AddinName.ToLower
    For Each inst In Addins
      If inst.Loaded = False Then Continue For
      If inst.ID.ToLower = AddinName Then Return inst.ConnectRef
    Next
  End Function

  Public Shared Function GetAddinInstance(ByVal AddinName As String) As AddinInstance
    AddinName = AddinName.ToLower
    For Each inst In Addins
      'If inst.Loaded = False Then Continue For
      If LCase(inst.ID) = AddinName Then Return inst
    Next
  End Function

  Public Shared ReadOnly Property Addins() As List(Of AddinInstance)
    Get
      Return instances
    End Get
  End Property

  Public Shared Sub ReadAddinList()
    Const mainAddin = "siaIDEMain.AddIn"

    ConnectFromFile(ParaService.AppPath + mainAddin)

    Dim dirList() = IO.Directory.GetFiles(ParaService.AppPath, "*.addin")
    For i = 0 To dirList.Length - 1
      Dim id As String = LCase(IO.Path.GetFileNameWithoutExtension(dirList(i)))
      If ParaService.Glob.para("addinState__" + id, "FALSE") <> "TRUE" Then Continue For
      ConnectFromFile(dirList(i))
      Application.DoEvents()
    Next

    'Dim dirList() = IO.Directory.GetFiles(ParaService.AppPath, "*.addin")
    'Dim frm As frm_newAddinsFound
    'For i = 0 To dirList.Length - 1
    '  If nameList.Contains(IO.Path.GetFileNameWithoutExtension(dirList(i).ToLower)) = False Then
    '    If frm Is Nothing Then
    '      frm = New frm_newAddinsFound
    '    End If
    '    frm.ListView1.Items.Add("Ja").SubItems.Add(dirList(i))
    '  End If
    'Next
    'If frm IsNot Nothing Then
    '  If frm.ShowDialog = DialogResult.OK Then
    '    For Each lvi As ListViewItem In frm.ListView1.Items
    '      ConnectFromFile(lvi.Text, lvi.Checked)
    '    Next
    '    SaveAddinList()
    '  End If
    'End If

    dirList = IO.Directory.GetFiles(ParaService.SettingsFolder + "addIns/", "*.nsla")
    For i = 0 To dirList.Length - 1
      Dim id As String = LCase(IO.Path.GetFileNameWithoutExtension(dirList(i)))
      If ParaService.Glob.para("addinState__" + id, "FALSE") = "TRUE" Then
        ConnectLuaAddin(dirList(i), ConnectMode.Startup)
      End If
    Next

  End Sub

  'Public Shared Sub SaveAddinList()
  '  Dim out As New System.Text.StringBuilder
  '  For Each inst In Addins
  '    If inst.IsScriptAddin = False Then
  '      out.AppendLine(inst.ToString)
  '    End If
  '  Next
  '  IO.File.WriteAllText(ParaService.ProfileFolder + "AddinList2.txt", out.ToString)
  'End Sub

  Public Shared Sub OnStartupComplete()
    For Each inst In Addins
      If inst.Loaded AndAlso (inst.ConnectRef IsNot Nothing) Then
        inst.ConnectRef.OnStartupComplete(Nothing)
      End If
    Next
  End Sub

  Public Shared Sub DisconnectAll()
    For Each inst In Addins
      If inst.Loaded AndAlso (inst.ConnectRef IsNot Nothing) Then
        inst.ConnectRef.OnBeforeShutdown(Nothing, Nothing)
      End If
      inst.DisconnectAddin()
    Next
    Addins.Clear()
  End Sub


End Class
