Imports System.Drawing

Module app_ftpManager

  'Public WithEvents ftpCtrl As New DotNetRemoting.FTPClientCtl

  Public fileList() As String
  Public nextUploadFile As String = ""
  Public skipFileChange As Boolean = False
  'Public fMain As frm_main

  Public useFtpProxy As Boolean = False
  Public ftpProxyURL As String = "https://secure.teamwiki.net/json_ftp_proxy.php?m="

  'Public ftpServer, ftpUser, ftpPass, ftpDirlistURL As String
  Public ftpConnections As New Dictionary(Of String, String())
  Public activeFtpCon As String

  Dim ftpctrl As New DotNetRemoting.FTPClientCtl

  Sub readFtpConnections()
    If IO.File.Exists(ParaService.SettingsFolder + "ftp_hosts.txt") = False Then IO.File.WriteAllText(ParaService.SettingsFolder + "ftp_hosts.txt", "")
    ' Dim lines() = Split(IDE.Glob.para("frm_main__thefile"), "$$")
    Dim lines() = Split(IO.File.ReadAllText(ParaService.SettingsFolder + "ftp_hosts.txt"), "$$")
    If lines.Length = 0 Then Return

    ftpConnections.Clear()
    For Each li In lines
      li = li.Trim
      If li = "" Then Continue For
      Dim parts() = Split(li, "§§")
      ReDim Preserve parts(4)
      ftpConnections.Add(parts(0), New String() {parts(1), parts(2), parts(3)})
    Next
  End Sub

  Function getFtpPort(ByVal ftpCon() As String) As Integer
    Return If(ftpCon.Length > 2 AndAlso Val(ftpCon(2)) > 0, Val(ftpCon(2)), 21)
  End Function

  Sub connectToServer(Optional ByVal serverDomain As String = "")
    Dim changedHost As Boolean = False
    changedHost = Not String.IsNullOrEmpty(serverDomain)
    If String.IsNullOrEmpty(serverDomain) Then serverDomain = activeFtpCon
    If String.IsNullOrEmpty(serverDomain) Then Exit Sub
    activeFtpCon = serverDomain
    Dim ftpCon As String() = ftpConnections(serverDomain)
    ParaService.Glob.para("lastFtpHost") = serverDomain
    ftpConnect(serverDomain, getFtpPort(ftpCon), ftpCon(0), ftpCon(1))

    If changedHost Then
      If tbFtpExplorer.InvokeRequired Then
        tbFtpExplorer.Invoke(New Threading.ThreadStart(AddressOf tbFtpExplorer.readBookmarks))
      Else
        tbFtpExplorer.readBookmarks()
      End If
    End If

    'With tbFtpExplorer.flpBookmarks
    '  .Controls.Clear()
    '  Dim favs() = Split(ftpCon(3), ":")
    '  For Each fav In favs
    '    Dim parts() = Split(fav, vbTab)
    '    Dim lnk As New LinkLabel With {.Text = parts(0), .Tag = parts(0)}
    '    If parts.Length > 1 Then lnk.Text = parts(1)
    '    .Controls.Add(lnk)
    '  Next
    'End With
  End Sub

  Function ftpReadImage(ByVal serverDomain As String, ByVal fileName As String) As Image
    connectToServer(serverDomain)
    Dim locFileSpec As String = ParaService.fp(IO.Path.GetTempPath, "ftpUpDown" + IO.Path.GetExtension(fileName))
    Dim res = ftpDownload(locFileSpec, fileName)
    If res = False Then
      MsgBox("FEHLER beim Download" + vbNewLine + _
      "Loc: " + locFileSpec + vbNewLine + "Srv: " + fileName + vbNewLine + showFtpError())
      Return New Bitmap(0, 0)
    Else
      Dim str As New System.IO.FileStream(locFileSpec, IO.FileMode.Open)
      ftpReadImage = Image.FromStream(str)
      str.Close()
    End If
  End Function

  Function ftpGetLocalAlias(ByVal serverDomain As String, ByVal fileName As String) As String
    Dim fld = ParaService.SettingsFolder + "ftp_" + serverDomain + "/"
    IO.Directory.CreateDirectory(fld)
    Return fld + "/" + fileName
  End Function

  Function ftpReadTextFile(ByVal serverDomain As String, ByVal fileName As String) As String
    connectToServer(serverDomain)
    'Dim locFileSpec = glob.fp(IO.Path.GetTempPath, "ftpUpDown.txt")
    Dim locFileSpec = ftpGetLocalAlias(serverDomain, fileName)
    IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(locFileSpec))
    If useFtpProxy Then
      Dim OPTIONS As New Hashtable
      Dim ftpCon As String() = ftpConnections(serverDomain)
      OPTIONS("server") = serverDomain : OPTIONS("port") = getFtpPort(ftpCon)
      OPTIONS("user") = ftpCon(0) : OPTIONS("password") = ftpCon(1)
      OPTIONS("file") = fileName
      Dim resultString As String = TwAjax.postUrl(ftpProxyURL + "get_file", TwAjax.EncodeUrlParaList(OPTIONS))
      Dim RESULT As System.Collections.Generic.Dictionary(Of String, Object) = fastJSON.JsonParser.JsonDecode(resultString)
      If RESULT Is Nothing OrElse RESULT("status") <> "1" Then
        MsgBox("FEHLER beim Download" + vbNewLine + _
        "Srv: " + fileName + vbNewLine + resultString)
        Return ""
      Else
        IO.File.WriteAllText(locFileSpec, RESULT("result"))
        Return RESULT("result")
      End If
    Else

      Dim res = ftpDownload(locFileSpec, fileName)
      ftpClose()
      If res = False Then
        MsgBox("FEHLER beim Download" + vbNewLine + _
        "Loc: " + locFileSpec + vbNewLine + "Srv: " + fileName + vbNewLine + showFtpError())
        Return ""
      Else
        Return IO.File.ReadAllText(locFileSpec)
      End If
    End If
  End Function

  Sub ftpSaveTextFile(ByVal serverDomain As String, ByVal fileName As String, ByVal content As String)
    'MAIN.labSaved.Visible = True
    Application.DoEvents()
    connectToServer(serverDomain)
    Dim locFileSpec = ftpGetLocalAlias(serverDomain, fileName)

    Dim path = IO.Path.GetDirectoryName(locFileSpec)
    If IO.Directory.Exists(path) = False Then IO.Directory.CreateDirectory(path)
    IO.File.WriteAllText(locFileSpec, content)

    If useFtpProxy Then
      Dim OPTIONS As New Hashtable
      Dim ftpCon As String() = ftpConnections(serverDomain)
      OPTIONS("server") = serverDomain : OPTIONS("port") = getFtpPort(ftpCon)
      OPTIONS("user") = ftpCon(0) : OPTIONS("password") = ftpCon(1)
      OPTIONS("file") = fileName : OPTIONS("content") = content
      Dim resultString As String = TwAjax.postUrl(ftpProxyURL + "put_file", TwAjax.EncodeUrlParaList(OPTIONS))
      Dim RESULT As System.Collections.Generic.Dictionary(Of String, Object) = fastJSON.JsonParser.JsonDecode(resultString)
      If RESULT Is Nothing OrElse RESULT("status") <> "1" Then
        MsgBox("FEHLER beim Upload" + vbNewLine + _
        "Srv: " + fileName + vbNewLine + resultString)
      Else
        ftpPlaySaveSound()
      End If
    Else
      Dim res As WinInet.InternetError = ftpUpload(locFileSpec, fileName)
      ftpClose()
      If res = 1 Then
        ftpPlaySaveSound()
      Else
        MsgBox("FEHLER beim Upload" + vbNewLine + _
        "Loc: " + locFileSpec + vbNewLine + _
        "Srv: " + fileName + vbNewLine + showFtpError(), MsgBoxStyle.Critical)
      End If
    End If
    'MAIN.labSaved.Visible = False
  End Sub

  Sub ftpPlaySaveSound()
    On Error Resume Next
    Dim sound = ParaService.Glob.para("ftp_options__savesound")
    If IO.File.Exists(sound) Then
      My.Computer.Audio.Play(sound)
    End If
  End Sub

  Function showFtpError() As String
    Dim errName As WinInet.InternetError = Err.LastDllError
    Return WinInet.TranslateErrorCode(errName)

    'Dim errorDesc As String = errName.ToString
    'If errName = WinInet.InternetError.EXTENDED_ERROR Then
    '  Dim respErrNo As Integer, respErrStr As New System.Text.StringBuilder(1000)
    '  WinInet.InternetGetLastResponseInfo(respErrNo, respErrStr, 999)
    '  errorDesc += " (" & respErrNo & ") " & vbNewLine & vbNewLine & respErrStr.ToString
    'End If
    'Return errorDesc
  End Function

  Function getTreeViewItem(ByVal path As String) As Object
    Dim item As Object = tbFtpExplorer.tvwFolders
    Dim names() = Split(path, "/")
    For I = 0 To names.Length - 1
      If names(I) = "" Then Continue For
      Dim nc As TreeNodeCollection = item.nodes
      Dim key As String = LCase(names(I))
      If nc.ContainsKey(key) Then
        item = nc(key)
      Else
        item = nc.Add(key, names(I))
      End If
    Next
    Return item
  End Function

  Sub fillFtpFilelist_loadData(ByVal para As Object)
    Dim dFinish As New delfillFtpFilelist_insertList(AddressOf fillFtpFilelist_insertList)

    Dim serverDomain As String, folder As String, dontReload As Boolean
    serverDomain = para(0) : folder = para(1) : dontReload = para(2)
    If useFtpProxy Then
      Dim OPTIONS As New Hashtable
      Dim ftpCon As String() = ftpConnections(serverDomain)
      OPTIONS("server") = serverDomain : OPTIONS("port") = getFtpPort(ftpCon)
      OPTIONS("user") = ftpCon(0) : OPTIONS("password") = ftpCon(1)
      OPTIONS("directory") = folder
      Dim resultString As String = TwAjax.postUrl(ftpProxyURL + "list_folder", TwAjax.EncodeUrlParaList(OPTIONS))
      Dim RESULT As System.Collections.Generic.Dictionary(Of String, Object) = fastJSON.JsonParser.JsonDecode(resultString)
      If RESULT Is Nothing Then
        MsgBox("Timeout!")
        tbFtpExplorer.Invoke(dFinish, Nothing, folder)
      ElseIf RESULT("status") <> "1" Then
        tbFtpExplorer.Invoke(dFinish, Nothing, folder)
      Else
        Dim list As ArrayList = RESULT("result")

        Dim arr(list.Count - 1) As String
        For i = 0 To list.Count - 1
          arr(i) = If(list(i)("is_dir"), "DIR", "FILE") + vbTab + list(i)("name") + vbTab + list(i)("size") + vbTab + list(i)("perms") + vbTab + list(i)("user") + "/" + list(i)("group") + vbTab + list(i)("date") + " " + "00:00"
        Next
        Array.Sort(arr)
        tbFtpExplorer.Invoke(dFinish, arr, folder)

      End If
    Else
      Try
        Dim ftpCon As String() = ftpConnections(serverDomain)
        ftpctrl.Quit()
        ftpctrl.RemoteHost = serverDomain
        ftpctrl.ControlPort = getFtpPort(ftpCon)

        ftpctrl.Connect()
        ftpctrl.Login(ftpCon(0), ftpCon(1))
      Catch ex As Exception
        tbFtpExplorer.Invoke(dFinish, Nothing, folder)
        MsgBox("Falsche Login-Daten oder Server nicht erreichbar" + vbNewLine + vbNewLine + ex.Message + vbNewLine + showFtpError())
        Exit Sub
      End Try
      If ftpctrl.IsConnected = False Then
      End If
      Dim filelist = ftpctrl.GetDetails(folder)
      If filelist Is Nothing Then
        tbFtpExplorer.Invoke(dFinish, Nothing, folder)
        MsgBox("Ermitteln der Dateiliste nicht möglich!" + vbNewLine + "Server: " + serverDomain + vbNewLine + "Ordner: " + folder + vbNewLine + showFtpError(), MsgBoxStyle.Exclamation)
        Exit Sub
      End If
      Dim arr(filelist.Length - 1) As String
      For i = 0 To filelist.Length - 1
        arr(i) = If(filelist(i).Dir, "DIR", "FILE") + vbTab + filelist(i).Name + vbTab + filelist(i).Size.ToString + vbTab + filelist(i).Permissions + vbTab + filelist(i).Owner + "/" + filelist(i).Group + vbTab + filelist(i).LastModified.ToShortDateString + " " + filelist(i).LastModified.ToLongTimeString
      Next
      Array.Sort(arr)
      tbFtpExplorer.Invoke(dFinish, arr, folder)
    End If

  End Sub

  Private Delegate Sub delfillFtpFilelist_insertList(ByVal arr() As String, ByVal folder As String)
  Sub fillFtpFilelist_insertList(ByVal arr() As String, ByVal folder As String)
    Dim ir As ListViewItem
    If folder.Length > 1 Then
      ir = tbFtpExplorer.ListView1.Items.Add("[..] Level up")
      ir.Tag = "LEVELUP"
      ir.ImageKey = "folderup"
    End If
    If arr IsNot Nothing Then
      Dim item As Object = getTreeViewItem(folder)
      Dim nc As TreeNodeCollection
      If TypeOf item Is TreeView Then
        nc = CType(item, TreeView).Nodes
      Else
        nc = CType(item, TreeNode).Nodes
        'CType(item, TreeNode).EnsureVisible()
        'CType(item, TreeNode).TreeView.SelectedNode = item
      End If

      For Each item In arr
        Dim D() = Split(item, vbTab)
        If D(1) = "." Or D(1) = ".." Then Continue For
        ir = tbFtpExplorer.ListView1.Items.Add(D(1))
        ir.Tag = D(0)
        If D(0) = "DIR" Then
          ir.ImageKey = getImageKeyForFileext(tbFtpExplorer.imlIgrid, "folder")
          If Not nc.ContainsKey(LCase(D(1))) Then nc.Add(LCase(D(1)), D(1))
        Else
          ir.ImageKey = getImageKeyForFileext(tbFtpExplorer.imlIgrid, D(1))
          ir.SubItems.Add(D(2))
          ir.SubItems.Add(D(3))
          ir.SubItems.Add(D(4))
          ir.SubItems.Add(D(5))

        End If
      Next
    End If
    tbFtpExplorer.pbIndicator.Hide()
  End Sub
  Sub fillFtpFilelist(ByVal serverDomain As String, ByVal folder As String, Optional ByVal dontReload As Boolean = False)
    If tbFtpExplorer.txtFtpCurDir.Text = folder And dontReload Then Exit Sub
    If tbFtpExplorer.pbIndicator.Visible Then Exit Sub
    tbFtpExplorer.pbIndicator.Show()
    tbFtpExplorer.ListView1.Items.Clear()
    If ftpctrl.Parent Is Nothing Then tbFtpExplorer.Controls.Add(ftpctrl)

    If folder.StartsWith("/") = False Then folder = "/" + folder
    If folder.EndsWith("/") = False Then folder += "/"
    activeFtpCon = serverDomain

    Dim item As Object = getTreeViewItem(folder)
    If TypeOf item Is TreeNode Then
      CType(item, TreeNode).EnsureVisible()
      CType(item, TreeNode).TreeView.SelectedNode = item
    End If

    tbFtpExplorer.txtFtpCurDir.Text = folder
    Dim th As New Threading.Thread(AddressOf fillFtpFilelist_loadData)
    th.Start(New Object() {serverDomain, folder, dontReload})

    'If MAIN.FtpClientCtl1.RemoteHost <> serverDomain Or MAIN.FtpClientCtl1. = False Then
    'Try
    '  Dim ftpCon As String() = ftpConnections(serverDomain)
    '  tbFtpExplorer.ftpCtrl.Quit()
    '  tbFtpExplorer.ftpCtrl.RemoteHost = serverDomain
    '  tbFtpExplorer.ftpCtrl.ControlPort = 21

    '  tbFtpExplorer.ftpCtrl.Connect()
    '  tbFtpExplorer.ftpCtrl.Login(ftpCon(0), ftpCon(1))
    'Catch ex As Exception
    '  MsgBox("Falsche Login-Daten oder Server nicht erreichbar" + vbNewLine + vbNewLine + ex.Message + vbNewLine + showFtpError())
    '  Exit Sub
    'End Try
    'If tbFtpExplorer.ftpCtrl.IsConnected = False Then
    'End If
    'Dim filelist = tbFtpExplorer.ftpCtrl.GetDetails(folder)
    'If filelist Is Nothing Then
    '  MsgBox("Ermitteln der Dateiliste nicht möglich!" + vbNewLine + "Server: " + serverDomain + vbNewLine + "Ordner: " + folder + vbNewLine + showFtpError(), MsgBoxStyle.Exclamation)
    '  Exit Sub
    'End If
    '' tbFtpExplorer.imlIgrid.Images.Clear()
    'Dim arr(filelist.Length - 1) As String
    'For i = 0 To filelist.Length - 1
    '  arr(i) = If(filelist(i).Dir, "DIR", "FILE") + vbTab + filelist(i).Name
    'Next
    'Array.Sort(arr)
    'Dim item As Object = getTreeViewItem(folder)
    'Dim nc As TreeNodeCollection
    'If TypeOf item Is TreeView Then
    '  nc = CType(item, TreeView).Nodes
    'Else
    '  nc = CType(item, TreeNode).Nodes
    '  CType(item, TreeNode).EnsureVisible()
    '  CType(item, TreeNode).TreeView.SelectedNode = item
    'End If

    'For Each Item In arr
    '  Dim D() = Split(Item, vbTab)
    '  Dim ir = tbFtpExplorer.ListView1.Items.Add(D(1))
    '  ir.Tag = D(0)
    '  If D(0) = "DIR" Then
    '    ir.ImageKey = getImageKeyForFileext(tbFtpExplorer.imlIgrid, "folder")
    '    If Not nc.ContainsKey(LCase(D(1))) Then nc.Add(LCase(D(1)), D(1))
    '  Else
    '    ir.ImageKey = getImageKeyForFileext(tbFtpExplorer.imlIgrid, D(1))
    '  End If
    'Next
    'tbFtpExplorer.pbIndicator.Hide()
  End Sub

  Function getImageKeyForFileext(ByVal iml As ImageList, ByVal fileSpec As String) As String
    Dim fileExt = IO.Path.GetExtension(fileSpec).ToLower
    If fileSpec = "folder" Then fileExt = "folder"
    If iml.Images.ContainsKey(fileExt) = False Then
      Dim icons() = FileTypeAndIcon.RegisteredFileType.GetFileIconByExt(fileExt)

      iml.Images.Add(fileExt, icons(0))

    End If
    ' cl.ImageIndex = iml.Images.IndexOfKey(fileExt)
    Return fileExt
  End Function

  'Sub refreshFolderList()
  '  fMain.ToolStrip1.Enabled = False
  '  Dim listFilespec = "C:\yPara\FtpWebFileSync\dirList_" + ftpServer + ".txt"
  '  IO.File.WriteAllText(listFilespec, TwAjax.getUrlContent(ftpDirlistURL + "?mode=dirlist_r&dir=&format=v2"))
  '  MsgBox("download fertig" & vbNewLine & "...Länge=" & FileLen(listFilespec), MsgBoxStyle.Information)
  '  fMain.ToolStrip1.Enabled = True
  '  readFilelist()
  'End Sub

  'Sub readFilelist()
  '  If IO.File.Exists("C:\yPara\FtpWebFileSync\dirList_" + ftpServer + ".txt") = False Then
  '    refreshFolderList()
  '  End If
  '  fileList = IO.File.ReadAllLines("C:\yPara\FtpWebFileSync\dirList_" + ftpServer + ".txt")

  '  fMain.TreeView1.Nodes.Clear()
  '  Dim root As TreeNode = fMain.TreeView1.Nodes.Add(ftpServer)
  '  root.Tag = New String() {"0", "/", ""}
  '  root.ImageKey = "root"
  '  root.SelectedImageKey = "root"

  '  loadToTreeview(root, 0)

  'End Sub

  'Function countChars(ByVal origStr As String, ByVal charToCount As Char) As Integer
  '  countChars = 0
  '  For i As Integer = 0 To origStr.Length - 1
  '    If origStr.Chars(i) = charToCount Then countChars += 1
  '  Next
  'End Function

  'Sub loadToTreeview(ByVal rootNode As TreeNode, ByVal startIndex As Integer)
  '  'On Error Resume Next
  '  Dim i As Integer = startIndex - 1
  '  Dim rootPath As String = rootNode.FullPath.Substring(ftpServer.Length)
  '  Dim subNode(15) As TreeNode
  '  subNode(0) = rootNode
  '  Dim rootPathL As Integer = rootPath.Length
  '  Dim rootLevel As Integer = rootNode.Tag(0)
  '  rootNode.Nodes.Clear()
  '  'Stop
  '  Do
  '    i += 1
  '    If i > fileList.Length - 1 Then Exit Do
  '    Dim reclevel As Integer = Val(fileList(i))
  '    If reclevel > rootLevel + 15 Then Continue Do
  '    Dim parts() As String = Split(fileList(i), vbTab)
  '    subNode(reclevel) = subNode(reclevel - 1).Nodes.Add(parts(1), parts(2))

  '    subNode(reclevel).Tag = parts
  '    If reclevel < rootLevel Then Exit Do
  '    If parts(0).Substring(0, rootPathL) <> rootPath Then Exit Do

  '  Loop

  'End Sub

  'Sub showFolderInGrid(ByVal folder As String, Optional ByVal ig As TenTec.Windows.iGridLib.iGrid = Nothing, Optional ByVal listStyle As Integer = 1)
  '  Dim filelist() As String = Split(TwAjax.getUrlContent(ftpDirlistURL + "?mode=dirlist&dir=" + folder), vbLf)
  '  If ig Is Nothing Then ig = fMain.IGrid1

  '  For Each LINE As String In filelist
  '    Dim PARTS() As String = Split(LINE, vbTab)
  '    If PARTS.Length < 4 Then Continue For
  '    If listStyle = 2 And PARTS(0) = "<DIR>" Then Continue For
  '    With ig.Rows.Add()
  '      .Tag = PARTS
  '      If listStyle = 1 Then .Cells(0).Value = PARTS(1) Else .Cells(0).Value = glob.fpUNIX(folder, PARTS(1))
  '      .Cells(1).Value = PARTS(5)

  '      If listStyle = 1 Then
  '        .Cells(2).Value = PARTS(2)
  '        Dim fileExt As String
  '        If PARTS(0) = "<DIR>" Then
  '          fileExt = "...FOLDER"
  '        ElseIf PARTS(1).LastIndexOf(".") = -1 Then
  '          fileExt = "---"
  '        Else
  '          fileExt = PARTS(1).Substring(PARTS(1).LastIndexOf(".")).ToUpper
  '        End If
  '        If fMain.imlFilelist.Images.ContainsKey(fileExt) = False Then
  '          Dim errmes As String = ""
  '          Dim img As Image
  '          Try
  '            img = GetIconFromFileExt(fileExt, assoc_iconSize.SHGFI_SMALLICON, errmes)
  '          Catch : img = frm_main.Icon.ToBitmap : End Try
  '          If errmes <> "" Then fileExt = "---"
  '          If fMain.imlFilelist.Images.ContainsKey(fileExt) = False Then fMain.imlFilelist.Images.Add(fileExt, img)

  '        End If
  '        .Cells(0).ImageIndex = fMain.imlFilelist.Images.IndexOfKey(fileExt)
  '      End If
  '    End With
  '  Next
  'End Sub

  'Sub navigateToPath(ByVal path As String)
  '  Dim n() = fMain.TreeView1.Nodes.Find(path, True)
  '  Application.DoEvents()
  '  If n.Length > 0 Then fMain.TreeView1.SelectedNode = n(0)
  'End Sub
  'Sub saveFavorites()
  '  Dim favList(fMain.tsbFavorites.DropDownItems.Count - 1) As String
  '  For i = 0 To favList.Length - 1
  '    favList(i) = fMain.tsbFavorites.DropDownItems(i).Text
  '  Next
  '  glob.para("favoritesList") = Join(favList, vbTab)
  'End Sub
  'Sub readFavorites()
  '  fMain.tsbFavorites.DropDownItems.Clear()
  '  Dim favList() As String = Split(glob.para("favoritesList"), vbTab)
  '  For Each path As String In favList
  '    If path = "" Then Continue For
  '    fMain.tsbFavorites.DropDownItems.Add(path)
  '  Next
  'End Sub

  'Function downloadFile(ByVal filespec As String, Optional ByVal targetRoot As String = "") As String
  '  Dim cacheFolder As String = glob.para("frm_options__txtLocCacheFolder")
  '  If targetRoot <> "" Then cacheFolder = targetRoot
  '  If cacheFolder = "" Then My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand) : Return ""
  '  fMain.FileSystemWatcher1.Path = cacheFolder

  '  Dim locFilename As String = filespec
  '  locFilename = locFilename.Substring(1)
  '  locFilename = Replace(locFilename, "/", "\")

  '  Dim locFileSpec As String = glob.fp(cacheFolder, ftpServer + "\" + locFilename)

  '  Dim res = ftpDownload(locFileSpec, "/httpdocs" + filespec)
  '  If res = False Then
  '    'MsgBox("ERR: " & System.Runtime.InteropServices.Marshal.GetLastWin32Error())
  '    'Dim exc As New System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error())

  '    frm_main.TextBox1.Text = "FEHLER beim Download" + vbNewLine + _
  '    "Loc: " + locFileSpec + vbNewLine + "Srv: " + "/httpdocs" + filespec
  '    My.Computer.Audio.Play("C:\windows\media\chord.wav")
  '    connectTeamwikiServer()
  '    'MsgBox("ERR: die Datei konnte nicht heruntergeladen werden:" + vbNewLine + "Loc: " + locFileSpec + vbNewLine + "Srv: " + "/httpdocs" + filespec, MsgBoxStyle.Exclamation)
  '    Return ""
  '  End If

  '  Return locFileSpec
  'End Function
  'Sub openFile(ByVal filespec As String)
  '  Dim editorFilespec As String = glob.para("frm_options__txtStandardEdit")
  '  Dim customEditors() As String = Split(glob.para("frm_options__txtCustomEdit"), vbNewLine)

  '  Dim fileExt As String = IO.Path.GetExtension(filespec).Substring(1).ToLower
  '  For Each custEdit In customEditors
  '    If custEdit.StartsWith(fileExt) Then editorFilespec = custEdit.Substring(custEdit.IndexOf("=") + 1)
  '  Next

  '  Process.Start(editorFilespec, filespec)


  'End Sub

  'Sub uploadFile(ByVal locFile As String)
  '  Dim cacheFolder As String = glob.fp(glob.para("frm_options__txtLocCacheFolder"))
  '  Dim serverFile As String = Replace(locFile, cacheFolder, "")
  '  serverFile = Replace(serverFile, "\", "/")
  '  serverFile = "/httpdocs/" + serverFile

  '  'MsgBox("folgende Datei könnte man jetzt hochladen:" + vbNewLine + "-> " + nextUploadFile + vbNewLine + "-> " + serverFile, MsgBoxStyle.Exclamation)
  '  Dim res As WinInet.InternetError = ftpUpload(locFile, serverFile)

  '  If res = 1 Then
  '    My.Computer.Audio.Play("C:\windows\media\windows xp-informationsleiste.wav")
  '  Else
  '    frm_main.TextBox1.Text = "FEHLER beim Upload" + vbNewLine + _
  '    "Loc: " + nextUploadFile + vbNewLine + _
  '    "Srv: " + serverFile + vbNewLine + res.ToString
  '    My.Computer.Audio.Play("C:\windows\media\chord.wav")
  '    connectTeamwikiServer()
  '  End If


  'End Sub


End Module
