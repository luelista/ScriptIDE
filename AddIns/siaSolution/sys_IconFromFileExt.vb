Imports System
Imports System.Collections.Generic
Imports System.Collections
Imports System.Text
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports System.Drawing

''' <summary>
''' Structure that encapsulates basic information of icon embedded in a file.
''' </summary>
Public Structure EmbeddedIconInfo
  Public FileName As String
  Public IconIndex As Integer
End Structure

Public Class RegisteredFileType
  Private Shared iconCache As New Dictionary(Of String, Icon())
  Shared Sub clearCache()
    iconCache.Clear()
  End Sub

  Shared Function getImageIndexForFileSpec(ByVal iml As ImageList, ByVal fileSpec As String) As Integer

  End Function


  Shared Function getImageIndexForFileExt(ByVal iml As ImageList, ByVal fileSpec As String) As String
    Dim ext = IO.Path.GetExtension(fileSpec).ToUpper
    If iml.Images.ContainsKey(ext) Then Return ext
    If iml.ImageSize.Height < 32 Then
      iml.Images.Add(ext, RegisteredFileType.GetFileIconByExt(ext, 0)(0))
    Else
      iml.Images.Add(ext, RegisteredFileType.GetFileIconByExt(ext, 1)(1))
    End If
    Return ext
  End Function

  Shared Function getImageIndexForFileThumb(ByVal iml As ImageList, ByVal fileSpec As String) As Integer
    Dim fsUpper = fileSpec.ToUpper
    If iml.Images.ContainsKey(fsUpper) Then Return iml.Images.IndexOfKey(fsUpper)

    Dim ext = IO.Path.GetExtension(fsUpper)

    Select Case ext
      Case ".EXE" 'eigenes Icon


      Case ".PNG", ".JPG", ".BMP", ".GIF" 'das bild selbst


      Case Else
        If iml.Images.ContainsKey("EXT_" + ext) Then Return iml.Images.IndexOfKey("EXT_" + ext)


    End Select

  End Function


#Region "APIs"

  Private Structure SHFILEINFO
    Public hIcon As IntPtr            ' : icon
    Public iIcon As Integer           ' : icondex
    Public dwAttributes As Integer    ' : SFGAO_ flags
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
    Public szDisplayName As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
    Public szTypeName As String
  End Structure

  Private Declare Auto Function SHGetFileInfo Lib "shell32.dll" _
          (ByVal pszPath As String, _
           ByVal dwFileAttributes As Integer, _
           ByRef psfi As SHFILEINFO, _
           ByVal cbFileInfo As Integer, _
           ByVal uFlags As Integer) As IntPtr

  Private Const SHGFI_ICON = &H100
  Enum assoc_iconSize
    SHGFI_SMALLICON = &H1
    SHGFI_LARGEICON = &H0    ' Large icon
  End Enum

  Private Declare Ansi Function ExtractIcon Lib "shell32.dll" Alias "ExtractIconA" (ByVal hInst As Integer, ByVal lpszExeFileName As String, ByVal nIconIndex As Integer) As IntPtr

  <DllImport("shell32.dll", CharSet:=CharSet.Auto)> _
  Private Shared Function ExtractIconEx(ByVal szFileName As String, ByVal nIconIndex As Integer, ByVal phiconLarge As IntPtr(), ByVal phiconSmall As IntPtr(), ByVal nIcons As UInteger) As UInteger
  End Function

  <DllImport("user32.dll", EntryPoint:="DestroyIcon", SetLastError:=True)> _
  Private Shared Function DestroyIcon(ByVal hIcon As IntPtr) As Integer
  End Function

#End Region

#Region "CORE METHODS"

  ''' <summary>
  ''' Gets registered file types and their associated icon in the system.
  ''' </summary>
  ''' <returns>Returns a hash table which contains the file extension as keys, the icon file and param as values.</returns>
  Public Shared Function GetAllFileTypesAndIcons() As Hashtable
    Try
      ' Create a registry key object to represent the HKEY_CLASSES_ROOT registry section
      Dim rkRoot As RegistryKey = Registry.ClassesRoot

      'Gets all sub keys' names.
      Dim keyNames As String() = rkRoot.GetSubKeyNames()
      Dim iconsInfo As New Hashtable()

      'Find the file icon.
      For Each keyName As String In keyNames
        If [String].IsNullOrEmpty(keyName) Then
          Continue For
        End If
        Dim indexOfPoint As Integer = keyName.IndexOf(".")

        'If this key is not a file exttension(eg, .zip), skip it.
        If indexOfPoint <> 0 Then
          Continue For
        End If

        Dim rkFileType As RegistryKey = rkRoot.OpenSubKey(keyName)
        If rkFileType Is Nothing Then
          Continue For
        End If

        'Gets the default value of this key that contains the information of file type.
        Dim defaultValue As Object = rkFileType.GetValue("")
        If defaultValue Is Nothing Then
          Continue For
        End If

        'Go to the key that specifies the default icon associates with this file type.
        Dim defaultIcon As String = defaultValue.ToString() & "\DefaultIcon"
        Dim rkFileIcon As RegistryKey = rkRoot.OpenSubKey(defaultIcon)
        If rkFileIcon IsNot Nothing Then
          'Get the file contains the icon and the index of the icon in that file.
          Dim value As Object = rkFileIcon.GetValue("")
          If value IsNot Nothing Then
            'Clear all unecessary " sign in the string to avoid error.
            Dim fileParam As String = value.ToString().Replace("""", "")
            iconsInfo.Add(keyName, fileParam)
          End If
          rkFileIcon.Close()
        End If
        rkFileType.Close()
      Next
      rkRoot.Close()
      Return iconsInfo
    Catch exc As Exception
      Throw exc
    End Try
  End Function

  ''' <summary>
  ''' Gets a registered file types and their associated icon in the system by its file ext
  ''' </summary>
  ''' <returns>Returns a hash table which contains the file extension as keys, the icon file and param as values.</returns>
  Public Shared Function GetFileIconByExt(ByVal fileExt As String, Optional ByVal icons As Integer = 2) As Icon()
    Try
      fileExt = fileExt.ToLower
      If fileExt.StartsWith(".") = False And fileExt <> "folder" Then fileExt = "." + fileExt
      'If iconCache.ContainsKey(fileExt) AndAlso iconCache(fileExt)(0) IsNot Nothing Then Return iconCache(fileExt)
      ' Create a registry key object to represent the HKEY_CLASSES_ROOT registry section
      Dim rkRoot As RegistryKey = Registry.ClassesRoot

      Dim rkFileType As RegistryKey = rkRoot.OpenSubKey(fileExt)
      If rkFileType Is Nothing Then
        Return New Icon() {My.Resources.invalidicon, My.Resources.invalidicon}
      End If

      'Gets the default value of this key that contains the information of file type.
      Dim defaultValue As Object = rkFileType.GetValue("")
      If defaultValue Is Nothing Then
        Return New Icon() {My.Resources.invalidicon, My.Resources.invalidicon}
      End If

      'Go to the key that specifies the default icon associates with this file type.
      Dim defaultIcon As String = defaultValue.ToString() & "\DefaultIcon"
      Dim rkFileIcon As RegistryKey = rkRoot.OpenSubKey(defaultIcon)
      If rkFileIcon IsNot Nothing Then
        'Get the file contains the icon and the index of the icon in that file.
        Dim value As Object = rkFileIcon.GetValue("")
        If value IsNot Nothing Then
          'Clear all unecessary " sign in the string to avoid error.
          Dim fileParam As String = value.ToString().Replace("""", "")
          Dim data(1) As Icon
          If icons <> 1 Then data(0) = ExtractIconFromFile(fileParam, False)
          If icons <> 0 Then data(1) = ExtractIconFromFile(fileParam, True)
          'iconCache.Add(fileExt, data)
          Return data
        End If
        rkFileIcon.Close()
      Else
        Return New Icon() {My.Resources.invalidicon, My.Resources.invalidicon}
      End If
      rkFileType.Close()

      rkRoot.Close()
    Catch exc As Exception
      Return New Icon() {My.Resources.invalidicon, My.Resources.invalidicon}
    End Try
  End Function

  ''' <summary>
  ''' Extract the icon from file.
  ''' </summary>
  ''' <param name="fileAndParam">The params string, 
  ''' such as ex: "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
  ''' <returns>This method always returns the large size of the icon (may be 32x32 px).</returns>
  Public Shared Function ExtractIconFromFile(ByVal fileAndParam As String) As Icon
    Try
      Dim embeddedIcon As EmbeddedIconInfo = getEmbeddedIconInfo(fileAndParam)

      'Gets the handle of the icon.
      Dim lIcon As IntPtr = ExtractIcon(0, embeddedIcon.FileName, embeddedIcon.IconIndex)

      'Gets the real icon.
      Return Icon.FromHandle(lIcon)
    Catch exc As Exception
      Throw exc
    End Try
  End Function

  ''' <summary>
  ''' Extract the icon from file.
  ''' </summary>
  ''' <param name="fileAndParam">The params string, 
  ''' such as ex: "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
  ''' <param name="isLarge">
  ''' Determines the returned icon is a large (may be 32x32 px) 
  ''' or small icon (16x16 px).</param>
  Public Shared Function ExtractIconFromFile(ByVal fileAndParam As String, ByVal isLarge As Boolean) As Icon
    Dim readIconCount As UInteger = 0
    Dim hDummy As IntPtr() = New IntPtr(0) {IntPtr.Zero}
    Dim hIconEx As IntPtr() = New IntPtr(0) {IntPtr.Zero}

    Try
      Dim embeddedIcon As EmbeddedIconInfo = getEmbeddedIconInfo(fileAndParam)

      If isLarge Then
        readIconCount = ExtractIconEx(embeddedIcon.FileName, embeddedIcon.IconIndex, hIconEx, hDummy, 1)
      Else
        readIconCount = ExtractIconEx(embeddedIcon.FileName, embeddedIcon.IconIndex, hDummy, hIconEx, 1)
      End If

      If readIconCount > 0 AndAlso hIconEx(0) <> IntPtr.Zero Then
        ' Get first icon.
        Dim extractedIcon As Icon = DirectCast(Icon.FromHandle(hIconEx(0)).Clone(), Icon)

        Return extractedIcon
      Else
        ' No icon read
        Return My.Resources.invalidicon
      End If
    Catch exc As Exception
      ' Extract icon error.
      Throw New ApplicationException("Could not extract icon", exc)
    Finally
      ' Release resources.
      For Each ptr As IntPtr In hIconEx
        If ptr <> IntPtr.Zero Then
          DestroyIcon(ptr)
        End If
      Next

      For Each ptr As IntPtr In hDummy
        If ptr <> IntPtr.Zero Then
          DestroyIcon(ptr)
        End If
      Next
    End Try

  End Function

#End Region


  Shared Function GetAssociatedIcon(ByVal fileSpec As String, _
                             Optional ByVal iconSize As assoc_iconSize = assoc_iconSize.SHGFI_LARGEICON _
                             ) As Icon

    Dim hImg As IntPtr  'The handle to the system image list.
    Dim shinfo As SHFILEINFO
    shinfo = New SHFILEINFO()

    hImg = SHGetFileInfo(fileSpec, 0, shinfo, _
                    Marshal.SizeOf(shinfo), _
                    SHGFI_ICON Or iconSize)

    'The icon is returned in the hIcon member of the
    'shinfo struct.
    Dim myIcon As System.Drawing.Icon
    If shinfo.hIcon = 0 Then Return My.Resources.invalidicon
    myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon)

    Return myIcon
  End Function
  Shared Function GetAssociatedIconAsImage(ByVal fileSpec As String, _
                             Optional ByVal iconSize As assoc_iconSize = assoc_iconSize.SHGFI_LARGEICON _
                             ) As Image


    Return GetAssociatedIcon(fileSpec, iconSize).ToBitmap
  End Function



#Region "UTILITY METHODS"

  ''' <summary>
  ''' Parses the parameters string to the structure of EmbeddedIconInfo.
  ''' </summary>
  ''' <param name="fileAndParam">The params string, 
  ''' such as ex: "C:\\Program Files\\NetMeeting\\conf.exe,1".</param>
  ''' <returns></returns>
  Protected Shared Function getEmbeddedIconInfo(ByVal fileAndParam As String) As EmbeddedIconInfo
    Dim embeddedIcon As New EmbeddedIconInfo()

    If [String].IsNullOrEmpty(fileAndParam) Then
      Return embeddedIcon
    End If

    'Use to store the file contains icon.
    Dim fileName As String = [String].Empty

    'The index of the icon in the file.
    Dim iconIndex As Integer = 0
    Dim iconIndexString As String = [String].Empty

    Dim commaIndex As Integer = fileAndParam.IndexOf(",")
    'if fileAndParam is some thing likes that: "C:\\Program Files\\NetMeeting\\conf.exe,1".
    If commaIndex > 0 Then
      fileName = fileAndParam.Substring(0, commaIndex)
      iconIndexString = fileAndParam.Substring(commaIndex + 1)
    Else
      fileName = fileAndParam
    End If

    If Not [String].IsNullOrEmpty(iconIndexString) Then
      'Get the index of icon.
      iconIndex = Integer.Parse(iconIndexString) 'negativ positiv machen
      'If iconIndex < 0 Then
      '  iconIndex = 0
      '  'To avoid the invalid index.
      'End If
    End If

    embeddedIcon.FileName = fileName
    embeddedIcon.IconIndex = iconIndex

    Return embeddedIcon
  End Function

#End Region
End Class
