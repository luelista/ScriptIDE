Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Text

Public Class EveryThingIPCWindow
  Inherits NativeWindow
  Implements IDisposable

  Private Const MY_REPLY_ID As Integer = 0

  Dim WithEvents targetListview As ListView
  Dim keyword As String
  Dim viewPortStart As Integer

  Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    ' use the default id (0) for the nId parameter.
    If Everything.IsQueryReply(m.Msg, m.WParam, m.LParam, MY_REPLY_ID) Then
      Dim i As Integer
      Const bufsize As Integer = 260
      Dim buf As StringBuilder = New StringBuilder(bufsize)

      targetListview.VirtualListSize = Everything.GetTotResults()
      targetListview.Refresh()
      ' IPC Query reply.
      ' clear the old list of results            
      'listBox1.Items.Clear()
      ' set the window title
      'Text = (textBox1.Text + (" - " _
      '            + (Everything_GetTotResults + " Results")))
      ' loop through the results, adding each result to the listbox.
      i = 0
      'Do While (i < Everything_GetNumResults)
      '  ' get the result's full path and file name.
      '  Everything_GetResultFullPathName(i, buf, bufsize)
      '  ' add it to the list box                
      '  listBox1.Items.Insert(i, buf)
      '  i = (i + 1)
      'Loop
      'vScrollBar1.Minimum = 0
      'vScrollBar1.Maximum = (Everything_GetTotResults - 1)
      'vScrollBar1.SmallChange = 1
      'vScrollBar1.LargeChange = (listBox1.ClientRectangle.Height / listBox1.ItemHeight)
    End If
    MyBase.WndProc(m)
  End Sub

  Private Sub targetListview_RetrieveVirtualItem(ByVal sender As Object, ByVal e As System.Windows.Forms.RetrieveVirtualItemEventArgs) Handles targetListview.RetrieveVirtualItem
    If e.ItemIndex < viewPortStart Or e.ItemIndex > viewPortStart + 140 Then
      viewPortStart = Math.Max(0, e.ItemIndex - 50)
      doSearch(True)
    End If

    Dim fullPath = Everything.GetResultFullPathName(e.ItemIndex - viewPortStart)
    Dim path = Everything.GetResultPath(e.ItemIndex - viewPortStart)
    Dim fileName = Everything.GetResultFileName(e.ItemIndex - viewPortStart)

    e.Item = New ListViewItem(fileName)
    e.Item.SubItems.Add("")
    e.Item.SubItems.Add("")
    e.Item.SubItems.Add(path)
    If Everything.IsFolderResult(e.ItemIndex) Then
      e.Item.ImageIndex = 0
    Else
      e.Item.ImageIndex = 1
    End If
  End Sub

  Private Sub doSearch(ByVal wait As Boolean)

    ' set the search
    Everything.SetSearch(keyword)
    ' set the reply window (this window) [REQUIRED if not waiting for results in Everything_Query].
    Everything.SetReplyWindow(Handle)
    ' set the reply id.
    Everything.SetReplyID(MY_REPLY_ID)

    ' set up the visible result window.
    Everything.SetOffset(viewPortStart)
    Everything.SetMax(150)

    ' execute the query
    Everything.Query(wait)
  End Sub

  Sub StartSearch(ByVal searchKeyword As String, ByVal target As ListView)
    targetListview = target
    target.Items.Clear()
    target.VirtualMode = True
    target.VirtualListSize = 0
    viewPortStart = 0
    keyword = searchKeyword
    doSearch(False)
  End Sub
  Public Sub New()
    Dim cp As New CreateParams()
    cp.Caption = "mw Everything IPC Window- side4"
    Me.CreateHandle(cp)
  End Sub

  Private disposedValue As Boolean = False    ' To detect redundant calls

  ' IDisposable
  Protected Overridable Sub Dispose(ByVal disposing As Boolean)
    If Not Me.disposedValue Then
      If disposing Then
        '  free other state (managed objects).
      End If
      Everything.SetReplyWindow(IntPtr.Zero)
      Everything.Reset()

      targetListview.VirtualListSize = 0
      targetListview.VirtualMode = False
      targetListview = Nothing
      Me.DestroyHandle()
    End If
    Me.disposedValue = True
  End Sub

#Region " IDisposable Support "
  ' This code added by Visual Basic to correctly implement the disposable pattern.
  Public Sub Dispose() Implements IDisposable.Dispose
    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    Dispose(True)
    GC.SuppressFinalize(Me)
  End Sub
#End Region

  Private Sub targetListview_VirtualItemsSelectionRangeChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventArgs) Handles targetListview.VirtualItemsSelectionRangeChanged

  End Sub
End Class

Public MustInherit Class Everything


  Public Declare Unicode Function SetSearch Lib "Everything.dll" Alias "Everything_SetSearchW" (ByVal lpSearchString As String) As Integer

  Public Declare Sub SetMatchPath Lib "Everything.dll" Alias "Everything_SetMatchPath" (ByVal bEnable As Boolean)

  Public Declare Sub SetMatchCase Lib "Everything.dll" Alias "Everything_SetMatchCase" (ByVal bEnable As Boolean)

  Public Declare Sub SetMatchWholeWord Lib "Everything.dll" Alias "Everything_SetMatchWholeWord" (ByVal bEnable As Boolean)

  Public Declare Sub SetRegex Lib "Everything.dll" Alias "Everything_SetRegex" (ByVal bEnable As Boolean)

  Public Declare Sub SetMax Lib "Everything.dll" Alias "Everything_SetMax" (ByVal dwMax As Integer)

  Public Declare Sub SetOffset Lib "Everything.dll" Alias "Everything_SetOffset" (ByVal dwOffset As Integer)

  Public Declare Sub SetReplyWindow Lib "Everything.dll" Alias "_Everything_SetReplyWindow@4" (ByVal hWnd As IntPtr)

  Public Declare Sub SetReplyID Lib "Everything.dll" Alias "_Everything_SetReplyID@4" (ByVal nId As Integer)

  Public Declare Function GetMatchPath Lib "Everything.dll" Alias "Everything_GetMatchPath" () As Boolean

  Public Declare Function GetMatchCase Lib "Everything.dll" Alias "Everything_GetMatchCase" () As Boolean

  Public Declare Function GetMatchWholeWord Lib "Everything.dll" Alias "Everything_GetMatchWholeWord" () As Boolean

  Public Declare Function GetRegex Lib "Everything.dll" Alias "Everything_GetRegex" () As Boolean

  Public Declare Function GetMax Lib "Everything.dll" Alias "Everything_GetMax" () As UInt32

  Public Declare Function GetOffset Lib "Everything.dll" Alias "Everything_GetOffset" () As UInt32

  Public Declare Unicode Function GetSearch Lib "Everything.dll" Alias "Everything_GetSearchW" () As String

  Public Declare Function GetLastError Lib "Everything.dll" Alias "Everything_GetLastError" () As Integer

  Public Declare Function GetReplyWindow Lib "Everything.dll" Alias "_Everything_GetReplyWindow@0" () As IntPtr

  Public Declare Function GetReplyID Lib "Everything.dll" Alias "_Everything_GetReplyID@0" () As Integer

  Public Declare Unicode Function Query Lib "Everything.dll" Alias "Everything_QueryW" (ByVal bWait As Boolean) As Boolean

  Public Declare Function IsQueryReply Lib "Everything.dll" Alias "Everything_IsQueryReply" (ByVal message As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr, ByVal nId As UInteger) As Boolean

  Public Declare Sub SortResultsByPath Lib "Everything.dll" Alias "Everything_SortResultsByPath" ()

  Public Declare Function GetNumFileResults Lib "Everything.dll" Alias "Everything_GetNumFileResults" () As Integer

  Public Declare Function GetNumFolderResults Lib "Everything.dll" Alias "Everything_GetNumFolderResults" () As Integer

  Public Declare Function GetNumResults Lib "Everything.dll" Alias "Everything_GetNumResults" () As Integer

  Public Declare Function GetTotFileResults Lib "Everything.dll" Alias "Everything_GetTotFileResults" () As Integer

  Public Declare Function GetTotFolderResults Lib "Everything.dll" Alias "Everything_GetTotFolderResults" () As Integer

  Public Declare Function GetTotResults Lib "Everything.dll" Alias "Everything_GetTotResults" () As Integer

  Public Declare Function IsVolumeResult Lib "Everything.dll" Alias "Everything_IsVolumeResult" (ByVal nIndex As Integer) As Boolean

  Public Declare Function IsFolderResult Lib "Everything.dll" Alias "Everything_IsFolderResult" (ByVal nIndex As Integer) As Boolean

  Public Declare Function IsFileResult Lib "Everything.dll" Alias "Everything_IsFileResult" (ByVal nIndex As Integer) As Boolean

  Private Declare Unicode Sub GetResultFullPathName Lib "Everything.dll" Alias "Everything_GetResultFullPathNameW" (ByVal nIndex As Integer, ByVal lpString As StringBuilder, ByVal nMaxCount As Integer)
  Private Declare Unicode Function GetResultFileNameW Lib "Everything.dll" Alias "Everything_GetResultFileNameW" (ByVal nIndex As Integer) As Integer
  Private Declare Unicode Function GetResultPathW Lib "Everything.dll" Alias "Everything_GetResultPathW" (ByVal nIndex As Integer) As Integer

  Public Shared Function GetResultFullPathName(ByVal nIndex As Integer) As String
    Const bufsize As Integer = 260
    Dim buf As StringBuilder = New StringBuilder(bufsize)
    Everything.GetResultFullPathName(nIndex, buf, bufsize)
    Return buf.ToString
  End Function

  Public Shared Function GetResultFileName(ByVal nIndex As Integer) As String
    Dim ptr = Everything.GetResultFileNameW(nIndex)
    Return Marshal.PtrToStringUni(ptr)
  End Function

  Public Shared Function GetResultPath(ByVal nIndex As Integer) As String
    Dim ptr = Everything.GetResultPathW(nIndex)
    Return Marshal.PtrToStringUni(ptr)
  End Function

  Public Declare Sub Reset Lib "Everything.dll" Alias "Everything_Reset" ()
End Class
