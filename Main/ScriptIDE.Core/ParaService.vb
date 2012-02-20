Imports System.IO

Public Class ParaService

  Private Shared _settingsFolder As String = "C:\yPara\scriptIDE4\"
  Public Shared Property SettingsFolder() As String
    Get
      Return _settingsFolder
    End Get
    Set(ByVal value As String)
      _settingsFolder = FP(value)
    End Set
  End Property

  Private Shared _profileName As String = "default"
  Public Shared Property ProfileName() As String
    Get
      Return _profileName
    End Get
    Set(ByVal value As String)
      _profileName = value
    End Set
  End Property

  Public Shared Property ProfileDisplayName() As String
    Get
      Return Glob.para("profileDisplayName", _profileName)
    End Get
    Set(ByVal value As String)
      Glob.para("profileDisplayName") = value
    End Set
  End Property

  Public Shared ReadOnly Property ProfileFolder() As String
    Get
      Return _settingsFolder + "profiles\" + _profileName + "\"
    End Get
  End Property

  Private Shared _glob As cls_globPara
  Public Shared ReadOnly Property Glob() As cls_globPara
    Get
      Return _glob
    End Get
  End Property

  Public Shared Sub Initialize()
    _glob = New cls_globPara(Path.Combine(ProfileFolder, "side4para.txt"))
  End Sub

  Public Shared ReadOnly Property AppPath() As String
    Get
      'Return FP(Path.GetDirectoryName(Application.ExecutablePath))
      Return FP(Environment.CurrentDirectory)
    End Get
  End Property

  Public Shared Function FP(ByVal Path As String, Optional ByVal FileName As String = "")
    FP = path + IIf(path.EndsWith("\"), "", "\") + If(fileName.StartsWith("\"), fileName.Substring(1), fileName)
  End Function

  Public Shared Function FP_unix(ByVal Path As String, Optional ByVal FileName As String = "")
    FP_unix = Path + IIf(Path.EndsWith("/"), "", "/") + If(FileName.StartsWith("/"), FileName.Substring(1), FileName)
  End Function

End Class
