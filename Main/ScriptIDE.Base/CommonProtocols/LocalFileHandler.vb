Public Class LocalFileHandler
  Implements IProtocolHandler
  Implements IProtocolHandler2

  Public Sub NavigateFilelistTo(ByVal internalURL As String) Implements IProtocolHandler.NavigateFilelistTo
    Dim fileSpec = internalURL.Substring(5)
    Dim folder = IO.Path.GetDirectoryName(fileSpec)
    tbFileExplorer.AxFolderTreeview2.SelectedFolder.name = folder
    ShowFilelist()
  End Sub

  Public Function ReadFile(ByVal internalURL As String) As String Implements IProtocolHandler.ReadFile
    Dim fileSpec = internalURL.Substring(5)
    Dim fs As New IO.StreamReader(New IO.FileStream(fileSpec, IO.FileMode.OpenOrCreate, IO.FileAccess.Read, IO.FileShare.ReadWrite))
    ReadFile = fs.ReadToEnd
    fs.Close()
    'Return IO.File.ReadAllText(fileSpec)
  End Function

  Public Sub SaveFile(ByVal internalURL As String, ByVal data As String) Implements IProtocolHandler.SaveFile
    Dim fileSpec = internalURL.Substring(5)
    IO.File.WriteAllText(fileSpec, data)
  End Sub

  Public Sub ShowFilelist() Implements IProtocolHandler.ShowFilelist
    tbFileExplorer.Show()
    tbFileExplorer.Activate()
  End Sub

  Public Function MapToLocalFilename(ByVal internalURL As String) As String Implements IProtocolHandler.MapToLocalFilename
    Return internalURL.Substring(5).Replace("/", "\")
  End Function

  Public Function DeleteFile(ByVal internalURL As String) As Boolean Implements ScriptIDE.Core.IProtocolHandler2.DeleteFile
    Dim fileSpec = internalURL.Substring(5)
    IO.File.Delete(fileSpec)
  End Function

  Public Function FileExists(ByVal internalURL As String) As Boolean Implements ScriptIDE.Core.IProtocolHandler2.FileExists
    Dim fileSpec = internalURL.Substring(5)
    Return IO.File.Exists(fileSpec)
  End Function

  Public Function FolderExists(ByVal internalFolderURL As String) As Boolean Implements ScriptIDE.Core.IProtocolHandler2.FolderExists
    Dim fileSpec = internalFolderURL.Substring(5)
    Return IO.Directory.Exists(fileSpec)
  End Function

  Public Function GetFileList(ByVal internalFolderURL As String) As String() Implements ScriptIDE.Core.IProtocolHandler2.GetFileList
    Dim fileSpec = internalFolderURL.Substring(5)
    Return IO.Directory.GetFileSystemEntries(fileSpec)
  End Function

  Public Function GetFilelistForm() As System.Windows.Forms.Form Implements ScriptIDE.Core.IProtocolHandler2.GetFilelistForm
    Return tbFileExplorer
  End Function

  Public Function RenameFile(ByVal oldInternalURL As String, ByVal newInternalURL As String) As Boolean Implements ScriptIDE.Core.IProtocolHandler2.RenameFile
    Dim fileSpec = oldInternalURL.Substring(5)
    Dim fileSpec2 = newInternalURL.Substring(5)
    IO.File.Move(fileSpec, fileSpec2)
  End Function

  Public Function ShowSaveAsDialog(ByVal windowTitle As String, ByVal preselectURL As String, ByRef saveAsURL As String, ByVal custom As Object, ByVal flags As Integer) As Boolean Implements ScriptIDE.Core.IProtocolHandler2.ShowSaveAsDialog
    Using sfd As New SaveFileDialog()
      sfd.Title = windowTitle
      Dim fileSpec = preselectURL.Substring(5)
      If fileSpec.EndsWith("\") Or fileSpec.EndsWith("/") Then
        sfd.InitialDirectory = fileSpec
      Else
        sfd.FileName = fileSpec
      End If
      If sfd.ShowDialog() = DialogResult.OK Then
        saveAsURL = "loc:/" + sfd.FileName.Replace("\", "/")
        Return True
      Else
        Return False
      End If
    End Using
  End Function
End Class
