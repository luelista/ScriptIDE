
Public Structure ScriptParameters
  Public Language As String
  Public References As List(Of String)
  Public Includes As List(Of String)
  Public MergePartialFiles As List(Of String)
  Public ImportLines As List(Of String)
  Public ComExport As List(Of String)
  'Public SilentMode As String
  'Public IconFile As String
  Public DefPara As Dictionary(Of String, String)
  Property Para(ByVal paraName As String, Optional ByVal defValue As String = "") As String
    Get
      If Not DefPara.ContainsKey(paraName.ToLower) Then Return defValue
      Return DefPara(paraName.ToLower)
    End Get
    Set(ByVal value As String)
      If DefPara.ContainsKey(paraName.ToLower) Then
        DefPara(paraName.ToLower) = value
      Else
        DefPara.Add(paraName.ToLower, value)
      End If
    End Set
  End Property

  Public Sub GetFromCode(ByVal codeText As String)
    References = New List(Of String)
    Includes = New List(Of String)
    MergePartialFiles = New List(Of String)
    ImportLines = New List(Of String)
    ComExport = New List(Of String)
    DefPara = New Dictionary(Of String, String)
    Dim code() = Split(codeText, vbNewLine)
    Dim line As String, handled As Boolean
    For i = 0 To code.Length - 1
      line = code(i).Trim
      If line.StartsWith("#") Then
        Dim data = Split(line, " ", 3)
        Dim paraName = data(0).ToUpper

        handled = True
        Select Case paraName
          Case "#LANGUAGE" : Language = data(1)
          Case "#PARA", "#DEFINE"
            ReDim Preserve data(3)
            Me.Para(data(1)) = data(2)
            'Case "#SILENT" : SilentMode = para(1)
            'Case "#ICONFILE" : IconFile = para(1)

          Case "#REFERENCE"
            If References.Contains(data(1).ToLower) = False Then References.Add(data(1).ToLower)

          Case "#INCLUDE", "#INCLUDEFILE"
            If Includes.Contains(data(1).ToLower) = False Then Includes.Add(data(1).ToLower)

          Case "#IMPORTS"
            If ImportLines.Contains(data(1)) = False Then ImportLines.Add(data(1))

          Case "#MERGE"
            'MergePartialFiles

            'Case Else
            ' handled = False
        End Select
        ' If handled Then code(i) = "'' verarbeitet -- " + code(i)
      End If

    Next
  End Sub

End Structure
Interface IScriptPrecompiler
  Sub scriptPreproc(ByRef scriptCode As String, ByVal myimports As System.Collections.Generic.List(Of String), ByVal className As String, ByRef lineCount As Integer)
  Function getMainModuleForCompile(ByVal className As String) As String
  Property IsReleaseMode() As Boolean
  Property HostSite() As IScriptClassHost
End Interface
