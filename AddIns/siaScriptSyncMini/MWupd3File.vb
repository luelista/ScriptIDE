Enum FilePara
  AppID
  FileID
  FileName
  LocalTarget
  LocalSource
  EinDat
  EinDIZ
  AendDat
  AendDIZ
  Flags

  Length
End Enum

Public Class MWupd3File

  Public paraFileSpec As String
  Public AppID As String
  Public AppName As String
  Public MorePara As Dictionary(Of String, String)
  Public Files As List(Of String())

  Public Sub New(ByVal _paraFileSpec As String, ByVal fromFile As Boolean)
    MorePara = New Dictionary(Of String, String)
    Files = New List(Of String())
    If fromFile Then
      If IO.File.Exists(_paraFileSpec) = False Then Exit Sub
      Dim lines() = IO.File.ReadAllLines(_paraFileSpec)
      paraFileSpec = _paraFileSpec
      parse_MWUPD3_File(IO.File.ReadAllLines(paraFileSpec))
    Else
      parse_MWUPD3_File(Split(_paraFileSpec, vbNewLine))

    End If
  End Sub


  Sub parse_MWUPD3_File(ByVal LINES() As String)
    Dim lineMode As Integer = 0
    For Each line In LINES
      If line.Trim = "" Then Continue For
      If line.StartsWith("AppID" + vbTab) Then lineMode = 1 : Continue For

      If lineMode = 0 And line.Length >= 20 Then
        Dim FieldName = line.Substring(0, 18).Trim
        Dim FieldCont = line.Substring(20).Trim
        Select Case FieldName.ToUpper
          Case "ID" : Me.AppID = FieldCont
          Case "APPNAME" : Me.AppName = FieldCont
          Case Else : Me.MorePara.Add(FieldName, FieldCont)
        End Select
      End If
      If lineMode = 1 Then
        Dim Parts() = Split(line, vbTab)
        Me.Files.Add(Parts)
      End If
    Next

  End Sub
  Overrides Function ToString() As String
    Dim out As New System.Text.StringBuilder
    out.AppendLine("                ID: " + AppID)
    out.AppendLine("           AppName: " + AppName)
    For Each kvp In MorePara
      out.AppendLine(Space(18 - kvp.Key.Length) + kvp.Key + ": " + kvp.Value)
    Next
    out.AppendLine()
    For i As FilePara = 0 To 8
      out.Append(i.ToString + vbTab)
    Next
    out.AppendLine()
    For Each line In Me.Files
      out.AppendLine(Join(line, vbTab))
    Next

    Return out.ToString
  End Function



End Class
