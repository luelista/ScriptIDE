' <file>
'     <copyright see="prj:///doc/copyright.txt"/>
'     <license see="prj:///doc/license.txt"/>
'     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
'     <version>$Revision: 4498 $</version>
' </file>

Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports System.Resources

''' <summary>
''' Static class containing the AddInTree. Contains methods for accessing tree nodes and building items.
''' </summary>
Public NotInheritable Class AddInTree
  Private Sub New()
  End Sub

  Public Shared Event Changed()

  ' Shared m_addIns As New List(Of AddinInstance)()
  Shared rootNode As New AddInTreeNode()

  Shared m_doozers As New Dictionary(Of String, IDoozer)()
  'Shared m_conditionEvaluators As New Dictionary(Of String, IConditionEvaluator)()

  Shared Sub New()
    m_doozers.Add("Class", New ClassDoozer())
    m_doozers.Add("LuaClass", New LuaClassDoozer())
    'm_doozers.Add("FileFilter", New FileFilterDoozer())
    'm_doozers.Add("String", New StringDoozer())
    'm_doozers.Add("Icon", New IconDoozer())
    'm_doozers.Add("MenuItem", New MenuItemDoozer())
    'm_doozers.Add("ToolbarItem", New ToolbarItemDoozer())
    m_doozers.Add("Include", New IncludeDoozer())

    'AddinInstance.ConnectFromFile(ParaService.AppPath + "siaIDEMain.AddIn", True, True)


    'm_conditionEvaluators.Add("Compare", New CompareConditionEvaluator())
    'm_conditionEvaluators.Add("Ownerstate", New OwnerStateConditionEvaluator())

    'ApplicationStateInfoService.RegisterStateGetter("Installed 3rd party AddIns", AddressOf GetInstalledThirdPartyAddInsListAsString)
  End Sub

  'Private Shared Function GetInstalledThirdPartyAddInsListAsString() As Object
  '	Dim sb As New System.Text.StringBuilder()
  '    For Each addIn As AddinInstance In AddIns
  '      ' Skip preinstalled AddIns (show only third party AddIns)
  '      If FileUtility.IsBaseDirectory(FileUtility.ApplicationRootPath, addIn.FileSpec) Then
  '        Dim hidden As String = addIn.Properties("addInManagerHidden")
  '        If String.Equals(hidden, "true", StringComparison.OrdinalIgnoreCase) OrElse String.Equals(hidden, "preinstalled", StringComparison.OrdinalIgnoreCase) Then
  '          Continue For
  '        End If
  '      End If
  '      If sb.Length > 0 Then
  '        sb.Append(", ")
  '      End If
  '      sb.Append("[")
  '      sb.Append(addIn.Name)
  '      If addIn.Version IsNot Nothing Then
  '        sb.Append(" "c)
  '        sb.Append(addIn.Version.ToString())
  '      End If
  '      If Not addIn.Enabled Then
  '        sb.Append(", Enabled=")
  '        sb.Append(addIn.Enabled)
  '      End If
  '      If addIn.Action <> AddInAction.Enable Then
  '        sb.Append(", Action=")
  '        sb.Append(addIn.Action.ToString())
  '      End If
  '      sb.Append("]")
  '    Next
  '	Return sb.ToString()
  'End Function

  '''' <summary>
  '''' Gets the list of loaded AddIns.
  '''' </summary>
  'Public Shared ReadOnly Property AddIns() As IList(Of AddinInstance)
  '  Get
  '    Return m_addIns.AsReadOnly()
  '  End Get
  'End Property

  ''' <summary>
  ''' Gets a dictionary of registered doozers.
  ''' </summary>
  Public Shared ReadOnly Property Doozers() As Dictionary(Of String, IDoozer)
    Get
      Return m_doozers
    End Get
  End Property

  '''' <summary>
  '''' Gets a dictionary of registered condition evaluators.
  '''' </summary>
  'Public Shared ReadOnly Property ConditionEvaluators() As Dictionary(Of String, IConditionEvaluator)
  '	Get
  '		Return m_conditionEvaluators
  '	End Get
  'End Property

  ''' <summary>
  ''' Checks whether the specified path exists in the AddIn tree.
  ''' </summary>
  Public Shared Function ExistsTreeNode(ByVal path As String) As Boolean
    If path Is Nothing OrElse path.Length = 0 Then
      Return True
    End If

    Dim splittedPath As String() = path.Split("/"c)
    Dim curPath As AddInTreeNode = rootNode
    Dim i As Integer = 0
    While i < splittedPath.Length
      ' curPath = curPath.ChildNodes[splittedPath[i]] - check if child path exists
      If Not curPath.ChildNodes.TryGetValue(splittedPath(i), curPath) Then
        Return False
      End If
      i += 1
    End While
    Return True
  End Function

  ''' <summary>
  ''' Gets the <see cref="AddInTreeNode"/> representing the specified path.
  ''' This method throws a <see cref="TreePathNotFoundException"/> when the
  ''' path does not exist.
  ''' </summary>
  Public Shared Function GetTreeNode(ByVal path As String) As AddInTreeNode
    Return GetTreeNode(path, True)
  End Function

  ''' <summary>
  ''' Gets the <see cref="AddInTreeNode"/> representing the specified path.
  ''' </summary>
  ''' <param name="path">The path of the AddIn tree node</param>
  ''' <param name="throwOnNotFound">
  ''' If set to <c>true</c>, this method throws a
  ''' <see cref="TreePathNotFoundException"/> when the path does not exist.
  ''' If set to <c>false</c>, <c>null</c> is returned for non-existing paths.
  ''' </param>
  ''' <exception cref="FileNotFoundException" />
  Public Shared Function GetTreeNode(ByVal path As String, ByVal throwOnNotFound As Boolean) As AddInTreeNode
    If path Is Nothing OrElse path.Length = 0 Then
      Return rootNode
    End If
    Dim splittedPath As String() = path.Split("/"c)
    Dim curPath As AddInTreeNode = rootNode
    Dim i As Integer = 0
    While i < splittedPath.Length
      If Not curPath.ChildNodes.TryGetValue(splittedPath(i), curPath) Then
        If throwOnNotFound Then
          Throw New FileNotFoundException("Addin path " & i & " not found", path)
        Else
          Return Nothing
        End If
      End If
      ' curPath = curPath.ChildNodes[splittedPath[i]]; already done by TryGetValue
      i += 1
    End While
    Return curPath
  End Function


  Public Shared Function GetCodon(ByVal path As String) As Codon
    If path Is Nothing OrElse path.Length = 0 Then
      Return Nothing
    End If
    Dim splittedPath As String() = path.Split("/"c)
    Dim curPath As AddInTreeNode = rootNode
    Dim i As Integer = 0
    While i < splittedPath.Length - 1
      If Not curPath.ChildNodes.TryGetValue(splittedPath(i), curPath) Then
        Return Nothing
      End If
      ' curPath = curPath.ChildNodes[splittedPath[i]]; already done by TryGetValue
      i += 1
    End While
    Return curPath.GetChildItem(splittedPath(splittedPath.Length - 1))
  End Function

  ''' <summary>
  ''' Builds a single item in the addin tree.
  ''' </summary>
  ''' <param name="path">A path to the item in the addin tree.</param>
  ''' <param name="caller">The owner used to create the objects.</param>
  ''' <exception cref="TreePathNotFoundException">The path does not
  ''' exist or does not point to an item.</exception>
  Public Shared Function BuildItem(ByVal path As String, ByVal caller As Object) As Object
    Dim pos As Integer = path.LastIndexOf("/"c)
    Dim parent As String = path.Substring(0, pos)
    Dim child As String = path.Substring(pos + 1)
    Dim node As AddInTreeNode = GetTreeNode(parent)
    Return node.BuildChildItem(child, caller, New ArrayList(BuildItems(Of Object)(path, caller, False)))
  End Function

  ''' <summary>
  ''' Builds the items in the path. Ensures that all items have the type T.
  ''' Throws a <see cref="TreePathNotFoundException"/> if the path is not found.
  ''' </summary>
  ''' <param name="path">A path in the addin tree.</param>
  ''' <param name="caller">The owner used to create the objects.</param>
  Public Shared Function BuildItems(Of T)(ByVal path As String, ByVal caller As Object) As List(Of T)
    Return BuildItems(Of T)(path, caller, True)
  End Function

  ''' <summary>
  ''' Builds the items in the path. Ensures that all items have the type T.
  ''' </summary>
  ''' <param name="path">A path in the addin tree.</param>
  ''' <param name="caller">The owner used to create the objects.</param>
  ''' <param name="throwOnNotFound">If true, throws a <see cref="TreePathNotFoundException"/>
  ''' if the path is not found. If false, an empty ArrayList is returned when the
  ''' path is not found.</param>
  Public Shared Function BuildItems(Of T)(ByVal path As String, ByVal caller As Object, ByVal throwOnNotFound As Boolean) As List(Of T)
    Dim node As AddInTreeNode = GetTreeNode(path, throwOnNotFound)
    If node Is Nothing Then
      Return New List(Of T)()
    Else
      Return node.BuildChildItems(Of T)(caller)
    End If
  End Function

  Private Shared Function CreatePath(ByVal localRoot As AddInTreeNode, ByVal path As String) As AddInTreeNode
    If path Is Nothing OrElse path.Length = 0 Then
      Return localRoot
    End If
    Dim splittedPath As String() = path.Split("/"c)
    Dim curPath As AddInTreeNode = localRoot
    Dim i As Integer = 0
    While i < splittedPath.Length
      If Not curPath.ChildNodes.ContainsKey(splittedPath(i)) Then
        curPath.ChildNodes(splittedPath(i)) = New AddInTreeNode()
      End If
      curPath = curPath.ChildNodes(splittedPath(i))
      i += 1
    End While

    Return curPath
  End Function

  Private Shared Sub AddExtensionPath(ByVal path As ExtensionPath)
    Dim treePath As AddInTreeNode = CreatePath(rootNode, path.Name)
    For Each codon As Codon In path.Codons
      treePath.Codons.Add(codon)
    Next
  End Sub

  Public Shared Sub RebuildTree()
    AddInTree.rootNode = New AddInTreeNode()
    For Each addin In AddinInstance.Addins
      If addin.Loaded Then
        For Each path__1 As ExtensionPath In addin.Paths.Values
          AddExtensionPath(path__1)
        Next
      End If
    Next
    RaiseEvent Changed()
  End Sub

  ''' <summary>
  ''' The specified AddIn is added to the <see cref="AddIns"/> collection.
  ''' If the AddIn is enabled, its doozers, condition evaluators and extension
  ''' paths are added to the AddInTree and its resources are added to the
  ''' <see cref="ResourceService"/>.
  ''' </summary>
  Public Shared Sub InsertAddIn(ByVal addIn As AddinInstance)
    If addIn.Loaded Then
      For Each path__1 As ExtensionPath In addIn.Paths.Values
        AddExtensionPath(path__1)
      Next

      'For Each runtime As Runtime In addIn.Runtimes
      'If Runtime.IsActive Then
      For Each doozer As LazyLoadDoozer In addIn.DefinedDoozers
        If AddInTree.Doozers.ContainsKey(doozer.Name) Then
          Throw New Exception("Duplicate doozer: " + doozer.Name)
        End If
        AddInTree.Doozers.Add(doozer.Name, doozer)
      Next
      'For Each condition As LazyConditionEvaluator In Runtime.DefinedConditionEvaluators
      '  If AddInTree.ConditionEvaluators.ContainsKey(condition.Name) Then
      '    Throw New AddInLoadException("Duplicate condition evaluator: " + condition.Name)
      '  End If
      '  AddInTree.ConditionEvaluators.Add(condition.Name, condition)
      'Next
      'End If
      'Next

      'Dim addInRoot As String = Path.GetDirectoryName(addIn.FileName)
      'For Each bitmapResource As String In addIn.BitmapResources
      '  Dim path__1 As String = Path.Combine(addInRoot, bitmapResource)
      '  Dim resourceManager__2 As ResourceManager = ResourceManager.CreateFileBasedResourceManager(Path.GetFileNameWithoutExtension(path__1), Path.GetDirectoryName(path__1), Nothing)
      '  ResourceService.RegisterNeutralImages(resourceManager__2)
      'Next

      'For Each stringResource As String In addIn.StringResources
      '  Dim path__1 As String = Path.Combine(addInRoot, stringResource)
      '  Dim resourceManager__2 As ResourceManager = ResourceManager.CreateFileBasedResourceManager(Path.GetFileNameWithoutExtension(path__1), Path.GetDirectoryName(path__1), Nothing)
      '  ResourceService.RegisterNeutralStrings(resourceManager__2)
      'Next
    End If
    RaiseEvent Changed()
    ' m_addIns.Add(addIn)
  End Sub

  '''' <summary>
  '''' The specified AddIn is removed to the <see cref="AddIns"/> collection.
  '''' This is only possible for disabled AddIns, enabled AddIns require
  '''' a restart of the application to be removed.
  '''' </summary>
  '''' <exception cref="ArgumentException">Occurs when trying to remove an enabled AddIn.</exception>
  'Public Shared Sub RemoveAddIn(addIn As AddIn)
  '	If addIn.Enabled Then
  '		Throw New ArgumentException("Cannot remove enabled AddIns at runtime.")
  '	End If
  '	m_addIns.Remove(addIn)
  'End Sub

  ' As long as the show form takes 10 times of loading the xml representation I'm not implementing
  ' binary serialization.
  '		static Dictionary<string, ushort> nameLookupTable = new Dictionary<string, ushort>();
  '		static Dictionary<AddIn, ushort> addInLookupTable = new Dictionary<AddIn, ushort>();
  '
  '		public static ushort GetAddInOffset(AddIn addIn)
  '		{
  '			return addInLookupTable[addIn];
  '		}
  '
  '		public static ushort GetNameOffset(string name)
  '		{
  '			if (!nameLookupTable.ContainsKey(name)) {
  '				nameLookupTable[name] = (ushort)nameLookupTable.Count;
  '			}
  '			return nameLookupTable[name];
  '		}
  '
  '		public static void BinarySerialize(string fileName)
  '		{
  '			for (int i = 0; i < addIns.Count; ++i) {
  '				addInLookupTable[addIns] = (ushort)i;
  '			}
  '			using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(fileName))) {
  '				rootNode.BinarySerialize(writer);
  '				writer.Write((ushort)addIns.Count);
  '				for (int i = 0; i < addIns.Count; ++i) {
  '					addIns[i].BinarySerialize(writer);
  '				}
  '				writer.Write((ushort)nameLookupTable.Count);
  '				foreach (string name in nameLookupTable.Keys) {
  '					writer.Write(name);
  '				}
  '			}
  '		}

  '' used by Load(): disables an addin and removes it from the dictionaries.
  'Private Shared Sub DisableAddin(addIn As AddIn, dict As Dictionary(Of String, Version), addInDict As Dictionary(Of String, AddIn))
  '	addIn.Enabled = False
  '	addIn.Action = AddInAction.DependencyError
  '	For Each name As String In addIn.Manifest.Identities.Keys
  '		dict.Remove(name)
  '		addInDict.Remove(name)
  '	Next
  'End Sub

  '''' <summary>
  '''' Loads a list of .addin files, ensuring that dependencies are satisfied.
  '''' This method is normally called by <see cref="CoreStartup.RunInitialization"/>.
  '''' </summary>
  '''' <param name="addInFiles">
  '''' The list of .addin file names to load.
  '''' </param>
  '''' <param name="disabledAddIns">
  '''' The list of disabled AddIn identity names.
  '''' </param>
  'Public Shared Sub Load(addInFiles As List(Of String), disabledAddIns As List(Of String))
  '	Dim list As New List(Of AddIn)()
  '	Dim dict As New Dictionary(Of String, Version)()
  '	Dim addInDict As New Dictionary(Of String, AddIn)()
  '	For Each fileName As String In addInFiles
  '		Dim addIn__1 As AddIn
  '		Try
  '			addIn__1 = AddIn.Load(fileName)
  '		Catch ex As AddInLoadException
  '			LoggingService.[Error](ex)
  '			If ex.InnerException IsNot Nothing Then
  '				MessageService.ShowError(("Error loading AddIn " & fileName & ":" & vbLf) + ex.InnerException.Message)
  '			Else
  '				MessageService.ShowError(("Error loading AddIn " & fileName & ":" & vbLf) + ex.Message)
  '			End If
  '			addIn__1 = New AddIn()
  '			addIn__1.addInFileName = fileName
  '			addIn__1.CustomErrorMessage = ex.Message
  '		End Try
  '		If addIn__1.Action = AddInAction.CustomError Then
  '			list.Add(addIn__1)
  '			Continue For
  '		End If
  '		addIn__1.Enabled = True
  '		If disabledAddIns IsNot Nothing AndAlso disabledAddIns.Count > 0 Then
  '			For Each name As String In addIn__1.Manifest.Identities.Keys
  '				If disabledAddIns.Contains(name) Then
  '					addIn__1.Enabled = False
  '					Exit For
  '				End If
  '			Next
  '		End If
  '		If addIn__1.Enabled Then
  '			For Each pair As KeyValuePair(Of String, Version) In addIn__1.Manifest.Identities
  '				If dict.ContainsKey(pair.Key) Then
  '					MessageService.ShowError("Name '" & pair.Key & "' is used by " & "'" & Convert.ToString(addInDict(pair.Key).FileName) & "' and '" & fileName & "'")
  '					addIn__1.Enabled = False
  '					addIn__1.Action = AddInAction.InstalledTwice
  '					Exit For
  '				Else
  '					dict.Add(pair.Key, pair.Value)
  '					addInDict.Add(pair.Key, addIn__1)
  '				End If
  '			Next
  '		End If
  '		list.Add(addIn__1)
  '	Next
  '	checkDependencies:
  '	For i As Integer = 0 To list.Count - 1
  '		Dim addIn__1 As AddIn = list(i)
  '		If Not addIn__1.Enabled Then
  '			Continue For
  '		End If

  '		Dim versionFound As Version

  '		For Each reference As AddInReference In addIn__1.Manifest.Conflicts
  '			If reference.Check(dict, versionFound) Then
  '				MessageService.ShowError(addIn__1.Name + " conflicts with " + reference.ToString() & " and has been disabled.")
  '				DisableAddin(addIn__1, dict, addInDict)
  '					' after removing one addin, others could break
  '				GoTo checkDependencies
  '			End If
  '		Next
  '		For Each reference As AddInReference In addIn__1.Manifest.Dependencies
  '			If Not reference.Check(dict, versionFound) Then
  '				If versionFound IsNot Nothing Then
  '					MessageService.ShowError(addIn__1.Name + " has not been loaded because it requires " + reference.ToString() & ", but version " & versionFound.ToString() & " is installed.")
  '				Else
  '					MessageService.ShowError(addIn__1.Name + " has not been loaded because it requires " + reference.ToString() & ".")
  '				End If
  '				DisableAddin(addIn__1, dict, addInDict)
  '					' after removing one addin, others could break
  '				GoTo checkDependencies
  '			End If
  '		Next
  '	Next
  '	For Each addIn__1 As AddIn In list
  '		Try
  '			InsertAddIn(addIn__1)
  '		Catch ex As AddInLoadException
  '			LoggingService.[Error](ex)
  '			MessageService.ShowError(("Error loading AddIn " + addIn__1.FileName & ":" & vbLf) + ex.Message)
  '		End Try
  '	Next
  'End Sub
End Class
