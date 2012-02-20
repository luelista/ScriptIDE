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

End Module

<Microsoft.VisualBasic.ComClass()> Public Class cls_fileSearcher
  Dim proot, pfilter() As String

  Private p_cancel As Boolean

  Private ff_searchedFileCount, ff_fileCount, ff_folderCount, ff_elapsed As Integer, ff_actFolder As String
  Private out() As String, outIdx, outMaxIdx As Integer

  Private p_scriptClass As String, clsCallback As Object

  Event OnDoEvents(ByVal FolderCount, ByVal FileCount, ByVal ActFolder, ByVal ActFileSpec, ByRef Cancel)

  Private sortCrit As Integer
  Property SortCriteria() As Integer
    Get
      Return sortCrit
    End Get
    Set(ByVal value As Integer)
      sortCrit = value
    End Set
  End Property


  Private p_raiseStatusEvent As Boolean
  Public Property RaiseStatusEvent() As Boolean
    Get
      Return p_raiseStatusEvent
    End Get
    Set(ByVal value As Boolean)
      p_raiseStatusEvent = value
    End Set
  End Property


  Private p_callBackFunc As String
  Public Property CallbackScriptClassFunc() As String
    Get
      Return p_callBackFunc
    End Get
    Set(ByVal value As String)
      If p_scriptClass = "" Then
        Throw New InvalidOperationException("Um eine Callbackfunktion einzustellen, muss cls_fileSearcher durch eine Skriptklasse erzeugt worden sein.")
      Else
        If clsCallback Is Nothing Then clsCallback = ScriptHost.Instance.scriptClass(p_scriptClass)
      End If

      p_callBackFunc = value
    End Set
  End Property

  Private Function onStatusEvent(ByVal myActFolder As String, ByVal myActFile As String) As Boolean
    Dim cancel As Boolean
    If p_raiseStatusEvent Then
      RaiseEvent OnDoEvents(ff_folderCount, ff_fileCount, myActFolder, myActFile, cancel)
    End If
    If p_callBackFunc <> "" Then
      Try
        CallByName(clsCallback, p_callBackFunc, CallType.Method, ff_folderCount, ff_fileCount, myActFolder, myActFile, cancel)


      Catch ex As Exception
        MsgBox("Beim Aufrufen der Status-Callback-Funktion " + p_callBackFunc + " in der ScriptClass " + p_scriptClass + " ist ein Fehler aufgetreten." + vbNewLine + vbNewLine + "Möglicherweise existiert sie nicht mit der benötigten Methodensignatur:" + vbNewLine + _
               "Sub " + p_callBackFunc + "(FolderCount, FileCount, ActFolder, ActFileSpec, ByRef Cancel)" + vbNewLine + vbNewLine + "Exception: " + ex.Message, MsgBoxStyle.Information)
        p_cancel = True
      End Try
    End If
    If cancel = True Then p_cancel = True
  End Function

  ReadOnly Property ElapsedTime() As Integer
    Get
      Return ff_elapsed
    End Get
  End Property
  'ReadOnly Property RootFolder() As String
  '  Get
  '    Return proot
  '  End Get
  'End Property
  'ReadOnly Property Filter() As String
  '  Get
  '    Return pfilter
  '  End Get
  'End Property
  'ReadOnly Property IsDirectory() As Boolean
  '  Get
  '    Return (wfd.dwFileAttributes And FileAttribute.Directory) > 0
  '  End Get
  'End Property
  'ReadOnly Property FileName() As String
  '  Get
  '    Return wfd.cFileName
  '  End Get
  'End Property
  'ReadOnly Property FileSpec() As String
  '  Get
  '    Return glob.fp(proot, wfd.cFileName)
  '  End Get
  'End Property
  'ReadOnly Property FileSize() As Int64
  '  Get
  '    Return wfd.nFileSizeHigh * MAXDWORD + wfd.nFileSizeLow
  '  End Get
  'End Property



  Sub New(ByVal RootFolder As String, ByVal Filter As String, Optional ByVal fromScriptClass As String = "")
    proot = RootFolder
    pfilter = Split(UCase(Filter), " ")

    p_scriptClass = fromScriptClass

    'wfd = New WIN32_FIND_DATA

    'hsearch = FindFirstFile(glob.fp(proot, pfilter), wfd)
  End Sub

  'Function Find() As Boolean
  '  Return FindNextFile(hsearch, wfd)
  'End Function

  Public ReadOnly Property FoundFilesCount()
    Get
      Return ff_fileCount
    End Get
  End Property
  Public ReadOnly Property FoundDirectoriesCount() As Integer
    Get
      Return ff_folderCount
    End Get
  End Property

  Sub ResetVars()
    ff_searchedFileCount = 0 : ff_fileCount = 0 : ff_folderCount = 0 : outIdx = 0 : outMaxIdx = 0
    ff_elapsed = cls_scriptHelper.GetTime()
    p_cancel = False
    ReDim out(0)
  End Sub

  Function FindFiles() As String
    ResetVars()
    recursiveFileSearch("", proot, pfilter, "", False)
    ReDim Preserve out(outIdx)
    If sortCrit > 0 Then Array.Sort(out)
    FindFiles = Join(out, vbNewLine)
    ff_elapsed = cls_scriptHelper.GetTime() - ff_elapsed
  End Function

  Function FindRecursive() As String
    ResetVars()
    recursiveFileSearch("", proot, pfilter, "", True)
    ReDim Preserve out(outIdx)
    If sortCrit > 0 Then Array.Sort(out)
    FindRecursive = Join(out, vbNewLine)
    ff_elapsed = cls_scriptHelper.GetTime() - ff_elapsed
  End Function

  Sub recursiveFileSearch(ByVal startFolder As String, ByVal rootFolder As String, ByVal fileFilter() As String, ByVal findText As String, ByVal recursiv As Boolean)
    Dim hFind As IntPtr, lineData(10) As String
    Dim wfd As New WIN32_FIND_DATA
    Dim ii As Integer
    ff_folderCount += 1
    Application.DoEvents()
    hFind = FindFirstFile(ZZ.FP(rootFolder + startFolder, "*.*"), wfd)
    Do
      If wfd.cFileName = "." Or wfd.cFileName = ".." Then Continue Do
      Dim relFileSpec As String = ZZ.FP(startFolder, wfd.cFileName)
      Dim fileSpec As String = ZZ.FP(rootFolder, relFileSpec)
      Dim fileSpecUpper = fileSpec.ToUpper
      Dim fileNameUpper = wfd.cFileName.ToUpper
      ' Debug.Print(fileSpec & outIdx & "  " & outMaxIdx)
      If ff_searchedFileCount Mod 347 = 0 Then onStatusEvent(startFolder, fileSpec)

      ff_searchedFileCount += 1

      If p_cancel = True Then Exit Sub 'wird vom statusEvent gesetzt
      If (wfd.dwFileAttributes And FileAttribute.Directory) > 0 Then

        If recursiv = True Then recursiveFileSearch(relFileSpec, rootFolder, fileFilter, findText, True)

      Else
        For ii = 0 To pfilter.Length - 1
          If fileNameUpper.Contains(pfilter(ii)) = False Then Continue Do
        Next

        Dim fileExt = IO.Path.GetExtension(fileNameUpper)

        lineData(1) = wfd.cFileName
        lineData(2) = fileExt
        lineData(3) = wfd.nFileSizeHigh * MAXDWORD + wfd.nFileSizeLow

        Dim lmDate = DateTime.FromFileTime(((CType(wfd.ftLastWriteTime.dwHighDateTime, Long) << 32) + wfd.ftLastWriteTime.dwLowDateTime))
        lineData(4) = lmDate.ToString("yyyy-MM-dd HH-mm-ss")
        Dim crDate = DateTime.FromFileTime(((CType(wfd.ftCreationTime.dwHighDateTime, Long) << 32) + wfd.ftCreationTime.dwLowDateTime))
        lineData(5) = crDate.ToString("yyyy-MM-dd HH-mm-ss")

        lineData(7) = fileSpec
        lineData(8) = relFileSpec

        lineData(0) = lineData(SortCriteria)

        ff_fileCount += 1
        addToOut(lineData)
      End If
    Loop While FindNextFile(hFind, wfd) = True
  End Sub

  Private Sub addToOut(ByVal str() As String)
    If outIdx >= outMaxIdx Then
      outMaxIdx = outMaxIdx * 2 + 1
      ReDim Preserve out(outMaxIdx)
    End If
    out(outIdx) = Join(str, vbTab)
    outIdx += 1
  End Sub

End Class
