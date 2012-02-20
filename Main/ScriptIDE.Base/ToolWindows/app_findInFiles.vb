Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Module app_findInFiles
  Dim WithEvents workFind As New System.ComponentModel.BackgroundWorker() _
      With {.WorkerReportsProgress = True, .WorkerSupportsCancellation = True}

  Public Const MAXDWORD = &HFFFF
  Public Const MAX_PATH = 260
  Public Const INVALID_HANDLE_VALUE = -1

  ' The CharSet must match the CharSet of the corresponding PInvoke signature
  <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
  Structure WIN32_FIND_DATA
    Public dwFileAttributes As UInteger
    Public ftCreationTime As System.Runtime.InteropServices.ComTypes.FILETIME
    Public ftLastAccessTime As System.Runtime.InteropServices.ComTypes.FILETIME
    Public ftLastWriteTime As System.Runtime.InteropServices.ComTypes.FILETIME
    Public nFileSizeHigh As UInteger
    Public nFileSizeLow As UInteger
    Public dwReserved0 As UInteger
    Public dwReserved1 As UInteger
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> Public cFileName As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> Public cAlternateFileName As String
  End Structure

  <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
  Public Function FindFirstFile(ByVal lpFileName As String, ByRef lpFindFileData As WIN32_FIND_DATA) As IntPtr
  End Function

  <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
  Public Function FindNextFile(ByVal hFindFile As IntPtr, ByRef lpFindFileData As WIN32_FIND_DATA) As Boolean
  End Function

  <DllImport("kernel32.dll")> _
  Public Function FindClose(ByVal hFindFile As IntPtr) As Boolean
  End Function

  Dim workerStartTime As Date

  Sub startSearch(ByVal startFolder As String, ByVal fileFilter As String, ByVal findText As String)
    tbGlobSearch.igGlobsearch.Rows.Clear()
    workerStartTime = Now
    findText = findText.ToUpper
    workFind.RunWorkerAsync(New String() {startFolder, fileFilter, findText, tbGlobSearch.checkRekursiv.Checked})
  End Sub

  Sub recursiveFileSearch(ByVal startFolder As String, ByVal rootFolder As String, ByVal fileFilter As String, ByVal findText As String, ByVal recursiv As Boolean)
    Dim hFind As IntPtr
    Dim wfd As New WIN32_FIND_DATA
    Dim rank As Integer
    workFind.ReportProgress(0, New Object() {"ORDNER", rootFolder + startFolder})
    hFind = FindFirstFile(ParaService.FP(rootFolder + startFolder, "*.*"), wfd)
    Do
      If wfd.cFileName = "." Or wfd.cFileName = ".." Then Continue Do
      Dim relFileSpec As String = ParaService.FP(startFolder, wfd.cFileName)
      Dim fileSpec As String = ParaService.FP(rootFolder, relFileSpec)
      Dim fileSpecUpper = fileSpec.ToUpper
      Dim fileNameUpper = wfd.cFileName.ToUpper

      'Debug.Print(fileSpec)
      If (wfd.dwFileAttributes And FileAttribute.Directory) > 0 Then
        If fileNameUpper.Contains(findText) Then
          rank = 100
          workFind.ReportProgress(0, New Object() {"TREFFER", fileSpec, relFileSpec, "", "", rank, Color.OrangeRed})

        End If
        If recursiv = True Then recursiveFileSearch(relFileSpec, rootFolder, fileFilter, findText, True)

      Else
        Dim fileExt = IO.Path.GetExtension(fileNameUpper)
        If fileFilter.Contains(fileExt) = False Then Continue Do

        If fileNameUpper.Contains(findText) Then
          rank = 80
          workFind.ReportProgress(0, New Object() {"TREFFER", fileSpec, relFileSpec, "", wfd.cFileName, rank, Color.Blue})
        End If


        Dim reader = IO.File.OpenText(fileSpec)
        Dim lineCont, upperCont As String, lineNr As Integer = 0
        Dim rColor As Color
        While Not reader.EndOfStream
          lineCont = reader.ReadLine() : lineNr += 1

          FileSearchScanLine(fileSpec, lineNr, lineCont, findText, rank, rColor)
          If rank > -1 Then workFind.ReportProgress(0, New Object() {"TREFFER", fileSpec, relFileSpec, lineNr.ToString + ":" + wfd.cFileName, lineCont, rank, rColor})

        End While
        reader.Close()


      End If
    Loop While FindNextFile(hFind, wfd) = True


  End Sub


  Sub FileSearchScanLine(ByVal fileSpec As String, ByVal lineNr As Integer, ByVal lineTxt As String, ByVal findText As String, ByRef rank As Integer, ByRef foreColor As Color)
    rank = -1
    Dim upperCont As String = lineTxt.ToUpper
    If upperCont.Contains(findText) Then
      rank = 40
      foreColor = Color.Black
      If upperCont.Contains("FUNCTION") Then rank += 5
      'If upperCont.Contains("CLASS") Then rank += 6
      If upperCont.Contains("//") Then rank -= 20 : foreColor = Color.Green

      'Gesamtes Wort finden
      upperCont = " " + Regex.Replace(upperCont, "[^A-Z0-9_]", " ") + " "
      If upperCont.Contains(" " + findText + " ") Then rank += 5

    End If

  End Sub


  Private Sub workFind_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles workFind.DoWork
    Dim para() As String = e.Argument
    recursiveFileSearch("", ParaService.Glob.fp(para(0)), para(1), para(2), para(3))
  End Sub

  Private Sub workFind_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles workFind.ProgressChanged
    Dim stat() As Object = e.UserState
    Select Case stat(0)
      Case "TREFFER"
        Dim ir = tbGlobSearch.igGlobsearch.Rows.Add
        ir.Cells(0).Value = stat(2)
        ir.Cells(1).Value = stat(3)
        ir.Cells(2).Value = stat(4)
        ir.Cells(3).Value = stat(5)
        ir.Tag = stat(1)
        ir.ForeColor = stat(6)
      Case "ORDNER"
        Workbench.Instance.tssl_Filename.Text = "Suche läuft ...        " & stat(1)
    End Select
  End Sub

  Private Sub workFind_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles workFind.RunWorkerCompleted
    Dim worked = Now - workerStartTime

    Workbench.Instance.tssl_Filename.Text = "Suche abgeschlossen      Treffer: " & tbGlobSearch.igGlobsearch.Rows.Count & "        benötigte Zeit: " & worked.TotalSeconds.ToString("0.000") & " sek"
    With tbGlobSearch.igGlobsearch
      .SortObject.Clear()
      .SortObject.Add(3, TenTec.Windows.iGridLib.iGSortOrder.Descending)
      .Sort()
    End With
  End Sub
End Module
