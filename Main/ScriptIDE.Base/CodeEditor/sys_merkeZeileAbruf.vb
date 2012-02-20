Imports System.IO
Module sys_merkeZeileAbruf

  Public fileSpecMerkeZeileAbruf As String = "C:\yPara\mw-merkeZeileRtf.txt"
  Private Const trennMerkeZeileAbruf As String = vbCrLf & ":[°$§" & "%!&]:-->"

  Sub merkeZeile(ByVal merkeData As String)
    Dim fileCont As String = File.ReadAllText(fileSpecMerkeZeileAbruf)
    Dim forSave As String
    If fileCont.StartsWith("MERKE") Then
      forSave = trennMerkeZeileAbruf + merkeData
      File.AppendAllText(fileSpecMerkeZeileAbruf, forSave)
    Else
      forSave = "MERKE" + trennMerkeZeileAbruf + merkeData
      File.WriteAllText(fileSpecMerkeZeileAbruf, forSave)
    End If
  End Sub

  Function merkeZeileAbruf() As String()
    Dim fileCont As String = File.ReadAllText(fileSpecMerkeZeileAbruf)

    If fileCont.StartsWith("MERKE") Then _
      File.WriteAllText(fileSpecMerkeZeileAbruf, "ABRUF" + fileCont.Substring(5))

    Dim dataPart = fileCont.Substring(5 + trennMerkeZeileAbruf.Length)
    'dataPart = dataPart.Replace(trennMerkeZeileAbruf, vbNewLine)
    Return Split(dataPart, trennMerkeZeileAbruf)
  End Function

  '


  ''fileSpec muß noch abhängig von Diz gemacht werden...
  'Sub OLD_merkeZeile()
  '  Dim getAlias As Object
  '  Dim merkeData As String
  '  'Stop
  '  'getAktLine
  '  'UPGRADE_WARNING: Couldn't resolve default property of object getAktLineData(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
  '  merkeData = OLD_getAktLineData() + vbCrLf 'rtf_getAktLine()  'CStr(Time())
  '  Debug.Print(merkeData & "<--")

  '  Dim fileSpecRead As String
  '  Dim fileSpecSave As String
  '  'UPGRADE_WARNING: Couldn't resolve default property of object getAlias(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
  '  fileSpecRead = getAlias(fileSpecMerkeZeileAbruf)
  '  fileSpecSave = fileSpecRead 'fileSpecMerkeZeileAbruf
  '  Dim result As String
  '  Dim errMes As String
  '  Dim forSave As String
  '  binRead(fileSpecRead, result, errMes)
  '  'UPGRADE_ISSUE: The preceding line couldn't be parsed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
  '  If Mid(result, 1, 5) = "MERKE" Then
  '    forSave = result & trennMerkeZeileAbruf & merkeData
  '  Else
  '    forSave = "MERKE" & trennMerkeZeileAbruf & merkeData
  '  End If
  '  Kill((fileSpecSave))
  '  binSave(fileSpecSave, forSave)
  '  'UPGRADE_ISSUE: The preceding line couldn't be parsed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
  '  If Err.Number Then Debug.Print("ERR???  " & Err.Description & errMes)
  'End Sub
  'Function OLD_getAktLineData() As Object
  '  Dim getRtfRef As Object
  '  Dim rtf_getAktLine As Object
  '  ' ACHTUNG: erkennt keine echten Zeilen, sondern nur ganze Absätze
  '  On Error Resume Next
  '  Dim lineStart As Integer
  '  Dim lineEnd As Integer
  '  Dim aktLine As String

  '  'UPGRADE_WARNING: Couldn't resolve default property of object rtf_getAktLine(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
  '  aktLine = rtf_getAktLine(lineStart, lineEnd)
  '  'UPGRADE_WARNING: Couldn't resolve default property of object getAktLineData. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
  '  getAktLineData = aktLine ' + charPlus

  '  'ifMerkeLöschen...
  '  'UPGRADE_WARNING: Couldn't resolve default property of object getRtfRef.Range. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
  '  getRtfRef.Range(lineStart, lineEnd).Text = ""

  '  If Err.Number Then Debug.Print("ERR: " & Err.Description)
  'End Function
  'Function OLD_merkeZeileAbruf() As Object
  '  Dim getRtfRef As Object
  '  Dim getAlias As Object
  '  Dim abrufData() As String
  '  Dim fileSpecRead As String
  '  Dim fileSpecSave As String
  '  ''fileSpecRead = fileSpecMerkeZeileAbruf
  '  ''fileSpecSave = fileSpecMerkeZeileAbruf
  '  'UPGRADE_WARNING: Couldn't resolve default property of object getAlias(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
  '  fileSpecRead = getAlias(fileSpecMerkeZeileAbruf)
  '  fileSpecSave = fileSpecRead 'fileSpecMerkeZeileAbruf
  '  Dim errMes As String
  '  Dim labelAsABRUF As Boolean
  '  Dim result As String
  '  Dim forInsert As String

  '  'getData
  '  binRead(fileSpecRead, result, errMes)
  '  'UPGRADE_ISSUE: The preceding line couldn't be parsed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
  '  If Mid(result, 1, 5) = "MERKE" Then labelAsABRUF = True
  '  abrufData = Split(result, trennMerkeZeileAbruf)

  '  '___ChangeLogFileType...
  '  If labelAsABRUF = True Then
  '    forInsert = Mid(result, 6)
  '    Kill((fileSpecSave))
  '    binSave(fileSpecSave, "ABRUF" + forInsert)
  '    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="82EBB1AE-1FCB-4FEF-9E6C-8736A316F8A7"'
  '    If Err.Number Then Debug.Print("ERR???  " & Err.Description & errMes)
  '  End If

  '  'splitData
  '  Dim i As Integer
  '  Dim max As Integer
  '  Dim insertData As String
  '  max = UBound(abrufData)
  '  'Stop
  '  For i = 1 To max
  '    insertData = insertData & abrufData(i) & "" '...Trenner???
  '    Debug.Print(abrufData(i))
  '  Next
  '  'UPGRADE_WARNING: Couldn't resolve default property of object getRtfRef.selection. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
  '  getRtfRef.selection.Range.Text = insertData
  '  'UPGRADE_WARNING: Couldn't resolve default property of object getRtfRef.selection. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
  '  getRtfRef.selection.Range.startPos = getRtfRef.selection.Range.endPos
  '  Debug.Print(forInsert)



  'End Function

  'Sub OLD_testMerke()
  '  ''  merkeZeile ("test1")
  '  ''  merkeZeile ("test2")
  '  ''  merkeZeile ("test3")
  'End Sub
  'Sub OLD_testAbruf()
  '  Dim i As Integer
  '  Dim max As Integer
  '  Dim Data() As String
  '  'UPGRADE_WARNING: Couldn't resolve default property of object merkeZeileAbruf(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
  '  Data = OLD_merkeZeileAbruf()
  '  max = UBound(Data)
  '  Debug.Print("++++++++++++++++++++++++++")
  '  For i = 1 To max
  '    Debug.Print(Data(i))
  '  Next
  '  Debug.Print("++++++++++++++++++++++++++")
  'End Sub


End Module
