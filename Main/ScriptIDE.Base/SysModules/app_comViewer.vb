Imports Microsoft.Win32
Module app_comViewer
  Structure componentInfo
    Public ProgID As String
    Public CLSID As String
    Public Path As String
    Public Sub New(ByVal pid As String, ByVal cid As String, ByVal pth As String)
      ProgID = pid : CLSID = cid : Path = pth
    End Sub
  End Structure

  Public COMcomponentNames() As String
  Public COMcomponentList() As String


  Function getNetTypeInfo(ByVal typeString As String) As String
    On Error Resume Next
    Dim parts = Split(typeString, ",")
    Dim ref = System.Reflection.Assembly.LoadWithPartialName(parts(0).Trim)
    Dim typ = ref.GetType(parts(1).Trim)
    Dim allMembers = typ.GetMembers
    Dim out(allMembers.Length) As String
    For i = 0 To allMembers.Length - 1
      out(i) = allMembers(i).MemberType.ToString + "__" + allMembers(i).Name '+allMembers(i).
    Next
    Return Join(out, vbNewLine)
  End Function


  Sub readCOMComponentList()
    If COMcomponentList IsNot Nothing AndAlso COMcomponentList.Length > 0 Then Exit Sub
    If IO.File.Exists(ParaService.SettingsFolder + "COMcomponents.txt") = False Then refreshCOMComponentList()
    COMcomponentList = IO.File.ReadAllLines(ParaService.SettingsFolder + "COMcomponents.txt")
    ReDim COMcomponentNames(COMcomponentList.Length - 1)
    For i = 0 To COMcomponentList.Length - 1
      If COMcomponentList(i) = "" Then Continue For
      COMcomponentNames(i) = COMcomponentList(i).Substring(0, COMcomponentList(i).IndexOf(vbTab))
    Next
  End Sub

  Sub refreshCOMComponentList()

    ' Open the HKEY_CLASSES_ROOT\CLSID key
    Dim regClsid As RegistryKey = Registry.ClassesRoot.OpenSubKey("")

    ' Iterate over all the subkeys.
    Dim outC As Integer = 0
    Dim subkeys() = regClsid.GetSubKeyNames()
    ReDim COMcomponentList(subkeys.Length - 1)
    ReDim COMcomponentNames(subkeys.Length - 1)

    Dim clsid As String
    For Each clsid In subkeys
      ' Open the subkey.
      Try
        Dim regClsidKey As RegistryKey = regClsid.OpenSubKey(clsid + "\CLSID")
        If regClsidKey Is Nothing Then Continue For
        COMcomponentList(outC) = clsid & vbTab & regClsidKey.GetValue("")
        COMcomponentNames(outC) = clsid
        outC += 1
        regClsidKey.Close()

      Catch : End Try
    Next
    ReDim Preserve COMcomponentList(outC - 1)
    ReDim Preserve COMcomponentNames(outC - 1)
    IO.File.WriteAllLines(ParaService.SettingsFolder + "COMcomponents.txt", COMcomponentList)
  End Sub

End Module
