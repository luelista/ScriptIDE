Imports System.Text

Public Class projectFile
  Dim PrjFileParts As Dictionary(Of String, String)
  Dim fileURL As String

  Public Sub New(ByVal FileSpec As String)

    fileURL = FileSpec
    'Dim st As New IO.FileStream(FileSpec, IO.FileMode.OpenOrCreate)
    'Dim ph = IDE.GetURLProtocolHandler(fileURL)
    'If ph Is Nothing Then Beep() : Exit Sub
    'parse_ProjectFile(ph.ReadFile(fileURL))
    parse_ProjectFile(ProtocolService.ReadFileFromURL(fileURL))
  End Sub

  Sub saveFile()
    Dim sw As New StringBuilder 'IO.StreamWriter(fileURL)
    For Each part In PrjFileParts
      sw.AppendLine("NewPart: " + part.Key)
      sw.AppendLine(part.Value)
    Next
    'Dim ph = IDE.GetURLProtocolHandler(fileURL)
    'If ph Is Nothing Then Beep() : Exit Sub
    'ph.SaveFile(fileURL, sw.ToString())
    ProtocolService.SaveFileToURL(fileURL, sw.ToString())
  End Sub

  Property Para(ByVal paraName As String, Optional ByVal defValue As String = "") As String
    Get
      If Not PrjFileParts.ContainsKey(paraName.ToLower) Then Return defValue
      Return PrjFileParts(paraName.ToLower)
    End Get
    Set(ByVal value As String)
      If PrjFileParts.ContainsKey(paraName.ToLower) Then
        PrjFileParts(paraName.ToLower) = value
      Else
        PrjFileParts.Add(paraName.ToLower, value)
      End If
    End Set
  End Property

  Sub loadFromTvn(ByVal tn As TreeNode)
    Dim sb As New StringBuilder
    recWalkTree(tn.Nodes, 0, sb)
    Para("ProjectTree") = sb.ToString
    Para("ProjectName") = tn.Text
    Para("Expanded") = If(tn.IsExpanded, "True", "False")
  End Sub

  Sub recWalkTree(ByVal tnc As TreeNodeCollection, ByVal ind As Integer, ByVal out As StringBuilder)
    For Each nod As TreeNode In tnc
      out.AppendLine(ind.ToString("00") + vbTab + nod.Name + vbTab + nod.Text + vbTab + nod.ImageKey + vbTab + If(nod.IsExpanded, "E", "C") + vbTab + vbTab + Join(nod.Tag, vbTab))
      If nod.Nodes.Count > 0 Then
        recWalkTree(nod.Nodes, ind + 1, out)
      End If
    Next
  End Sub

  Sub putToTvn(ByVal tn As TreeNode)
    tn.Text = Para("ProjectName", "Unnamed Project").Trim
    tn.ImageKey = "Project" : tn.SelectedImageKey = "Project"
    If Para("Expanded", "True").Trim <> "True" Then tn.Collapse() Else tn.Expand()
    'tn.Tag = New String() {"Project", fileName, ""}
    Dim Lines() = Split(Para("ProjectTree"), vbNewLine)
    Dim tnc(20) As TreeNodeCollection
    tnc(0) = tn.Nodes
    For Each line In Lines
      Dim parts() = Split(line, vbTab, 7)
      Dim indent As Integer
      If Integer.TryParse(parts(0), indent) = False Then Continue For
      Dim imageKey As String = parts(3)
      If imageKey.StartsWith(".") Then
        imageKey = siaSolution.RegisteredFileType.getImageIndexForFileExt(tbSolution.ImageList1, imageKey)
      End If
      With tnc(indent).Add(parts(1), parts(2), imageKey, imageKey)
        If parts(4) = "E" Then .Expand()
        .Tag = Split(parts(6), vbTab)
        tnc(indent + 1) = .Nodes
      End With
    Next
  End Sub

  Sub parse_ProjectFile(ByVal fs As String)
    'Dim read As New IO.StreamReader(fs)
    Dim Lines() As String = Split(fs, vbNewLine)
    Dim partName As String
    Dim lastPartCont As StringBuilder
    PrjFileParts = New Dictionary(Of String, String)
    'While Not read.EndOfStream
    'Dim line = read.ReadLine()
    For Each line In Lines
      Dim checkLine = line.Trim.ToLower()
      If checkLine.StartsWith("newpart:") Then
        If lastPartCont IsNot Nothing Then
          PrjFileParts.Add(partName, lastPartCont.ToString())
        End If
        partName = checkLine.Substring(8).Trim
        lastPartCont = New StringBuilder
        Continue For 'While
      End If
      lastPartCont.AppendLine(line)
    Next
    'End While

    If lastPartCont IsNot Nothing Then
      PrjFileParts.Add(partName, lastPartCont.ToString())
    End If
    'fs.Close()
  End Sub

End Class
