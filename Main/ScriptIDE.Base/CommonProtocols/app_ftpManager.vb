Imports System.Drawing

Module app_ftpManager

  'Public WithEvents ftpCtrl As New DotNetRemoting.FTPClientCtl

  Public fileList() As String
  Public nextUploadFile As String = ""
  Public skipFileChange As Boolean = False
  'Public fMain As frm_main

  Public useFtpProxy As Boolean = False
  Public ftpProxyURL As String = "https://labs.max-weller.de/json_ftp_proxy.php?m="

  'Public ftpServer, ftpUser, ftpPass, ftpDirlistURL As String
  Public ftpConnections As New Dictionary(Of String, String())
  Public activeFtpCon As String
  Public activeFtpProto As String

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
      ReDim Preserve parts(5)
      ftpConnections.Add(parts(0), New String() {parts(1), parts(2), parts(3), parts(4)})
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
    activeFtpProto = If(String.IsNullOrEmpty(ftpCon(3)), "ftp", ftpCon(3)) + ":/"

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

  Private lastsshclient As Renci.SshNet.SftpClient, lastsshclientdomain As String
  Function getSftpConnection(ByVal serverDomain As String) As Renci.SshNet.SftpClient
    ''cache lookup
    If lastsshclientdomain = serverDomain AndAlso lastsshclient IsNot Nothing AndAlso lastsshclient.IsConnected Then Return lastsshclient

    ''collect connection info
    Dim ftpCon As String() = ftpConnections(serverDomain)
    Dim authMethods(0) As Renci.SshNet.AuthenticationMethod
    authMethods(0) = New Renci.SshNet.PrivateKeyAuthenticationMethod(ftpCon(0), New Renci.SshNet.PrivateKeyFile(ftpCon(1)))
    Dim ci As New Renci.SshNet.ConnectionInfo(serverDomain, ftpCon(2), ftpCon(0), authMethods)

    '' connect
    Dim client As New Renci.SshNet.SftpClient(ci)
    client.Connect()

    '' cache connection
    lastsshclient = client : lastsshclientdomain = serverDomain

    Return client
  End Function

  Function sshNetGetPerms(ByVal file As Renci.SshNet.Sftp.SftpFileAttributes)
    Return If(file.OwnerCanRead, "r", "-") + If(file.OwnerCanWrite, "w", "-") + If(file.OwnerCanExecute, "x", "-") + _
          If(file.GroupCanRead, "r", "-") + If(file.GroupCanWrite, "w", "-") + If(file.GroupCanExecute, "x", "-") + _
          If(file.OthersCanRead, "r", "-") + If(file.OthersCanWrite, "w", "-") + If(file.OthersCanExecute, "x", "-")
  End Function

  Sub fillFtpFilelist_loadData(ByVal para As Object)
    Dim dFinish As New delfillFtpFilelist_insertList(AddressOf fillFtpFilelist_insertList)

    Dim serverDomain As String, folder As String, dontReload As Boolean
    serverDomain = para(0) : folder = para(1) : dontReload = para(2)

    Dim ftpCon As String() = ftpConnections(serverDomain)
    activeFtpProto = If(String.IsNullOrEmpty(ftpCon(3)), "ftp", ftpCon(3)) + ":/"
    If ftpCon(3) = "sftp" Then
      Dim c = getSftpConnection(serverDomain)
      Dim dirlist = c.ListDirectory(folder)

      Dim arr(dirlist.Count - 1) As String, i As Integer = 0
      For Each item In dirlist
        arr(i) = If(item.IsDirectory, "DIR", "FILE") + vbTab + item.Name + vbTab + Str(item.Length) + vbTab + sshNetGetPerms(item.Attributes) + vbTab + Str(item.UserId) + "/" + Str(item.GroupId) + vbTab + item.LastWriteTime.ToString
        i += 1
      Next
      Array.Sort(arr)
      tbFtpExplorer.Invoke(dFinish, arr, folder)

    ElseIf useFtpProxy Then
      Dim OPTIONS As New Hashtable
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
    ParaService.Glob.para("frmTB_ftpExplorer__ftpCurDir__" + activeFtpCon, "/") = folder

    Dim item As Object = getTreeViewItem(folder)
    If TypeOf item Is TreeNode Then
      CType(item, TreeNode).EnsureVisible()
      CType(item, TreeNode).TreeView.SelectedNode = item
    End If

    tbFtpExplorer.txtFtpCurDir.Text = folder
    Dim th As New Threading.Thread(AddressOf fillFtpFilelist_loadData)
    th.Start(New Object() {serverDomain, folder, dontReload})

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


End Module
