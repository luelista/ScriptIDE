Public Class frmTB_globSearch

  Public Overrides Function GetPersistString() As String
    Return tbPrefix + "GlobSearch"
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    If cls_IDEHelper.GetSingleton().Glob.para("frmTB_infoTips__isDockWin", "TRUE") = "TRUE" Then
      cls_IDEHelper.GetSingleton().BeforeShowAddinWindow(Me.GetPersistString(), Me)
    End If
    MyBase.Show()
  End Sub



  Private Sub DateiImEditorOeffnenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateiImEditorOeffnenToolStripMenuItem.Click
    navigateActFile("local")
  End Sub

  Private Sub WindowsExplorerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WindowsExplorerToolStripMenuItem.Click
    Dim fileSpec As String = igGlobsearch.CurRow.Tag
    Process.Start("explorer.exe", "/select," + fileSpec)
  End Sub

  Private Sub DateimanagerNavigierenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateimanagerNavigierenToolStripMenuItem.Click
    Dim fileSpec As String = igGlobsearch.CurRow.Tag
    'Dim dir = IO.Path.GetDirectoryName(fileSpec)
    'tbFileExplorer.ftvLocalbrowser.SelectedFolder.name = dir
    'tbFileExplorer.Show()
    ProtocolService.NavigateFilelistToURL(fileSpec)
  End Sub

  Private Sub FTPDateiOeffnenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTPDateiOeffnenToolStripMenuItem.Click
    navigateActFile("ftp")
  End Sub

  Sub navigateActFile(ByVal modus As String)
    Dim fileSpec As String = igGlobsearch.CurRow.Tag

    If fileSpec.StartsWith("|##|") Then
      'suche in akt. datei
      Dim parts() = Split(fileSpec, "|##|")
      onNavigate(parts(1) + "?" + parts(2))
      Exit Sub

    End If

    If IO.Directory.Exists(fileSpec) Then
      'ist ein Ordner
      Process.Start("explorer.exe", "/select," + fileSpec)
      Exit Sub
    End If


    If modus <> "local" And fileSpec.StartsWith(ParaService.SettingsFolder + "ftp_", StringComparison.CurrentCultureIgnoreCase) Then
      'ist eine FTP-Datei
      fileSpec = fileSpec.Replace("\", "/")

      'tbFtpExplorer.Show()
      Application.DoEvents()
      Dim fileParts() = Split(fileSpec, "/", 5)
      ' connectToServer(fileParts(3).Substring(4))
      Dim lineNr As String = igGlobsearch.CurRow.Cells(1).Value
      If lineNr.Contains(":") Then
        lineNr = "?" + lineNr.Substring(0, lineNr.IndexOf(":"))
      Else
        lineNr = ""
      End If

      Dim fileUrl = "ftp:/" + fileParts(3).Substring(4) + "/" + fileParts(4)
      onNavigate(fileUrl + lineNr)

      ' fillFtpFilelist(activeFtpCon, fileParts(4))
      Exit Sub
    End If
    If modus <> "ftp" Then
      'ist keine FTP-Datei oder Modus=lokal

      Dim lineNr As String = igGlobsearch.CurRow.Cells(1).Value
      If lineNr.Contains(":") Then
        lineNr = "?" + lineNr.Substring(0, lineNr.IndexOf(":"))
      Else
        lineNr = ""
      End If

      onNavigate("loc:/" + fileSpec + lineNr)
    End If
  End Sub

  Private Sub FTPOrdnerNavigierenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTPOrdnerNavigierenToolStripMenuItem.Click
    Dim fileSpec As String = igGlobsearch.CurRow.Tag
    If IO.File.Exists(fileSpec) Then fileSpec = IO.Path.GetDirectoryName(fileSpec)
    If fileSpec.StartsWith(ParaService.SettingsFolder + "ftp_", StringComparison.CurrentCultureIgnoreCase) Then
      fileSpec = fileSpec.Replace("\", "/")

      'tbFtpExplorer.Show()
      'Application.DoEvents()
      Dim fileParts() = Split(fileSpec, "/", 5)
      'connectToServer(fileParts(3).Substring(4))

      'fillFtpFilelist(activeFtpCon, fileParts(4))
      ProtocolService.NavigateFilelistToURL("ftp:/" + fileParts(3).Substring(4) + fileParts(4))

    End If
  End Sub

  Private Sub TrefferlisteLoeschenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrefferlisteLoeschenToolStripMenuItem.Click
    igGlobsearch.Rows.Clear()
  End Sub

  Private Sub igGlobsearch_CellDoubleClick(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellDoubleClickEventArgs) Handles igGlobsearch.CellDoubleClick
    navigateActFile("")
  End Sub


  Private Sub igGlobsearch_CurRowChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles igGlobsearch.CurRowChanged
    Dim fileSpec As String = igGlobsearch.CurRow.Tag
    Dim filePos As String = igGlobsearch.CurRow.Cells("pos").Value
    If IO.File.Exists(fileSpec) Then
      Dim abPos = filePos.IndexOf(":")
      Dim startLine = 0, startLine2 As Integer
      If abPos > -1 Then startLine = filePos.Substring(0, abPos)
      startLine2 = Math.Max(0, startLine - 4)

      Dim strRead = IO.File.OpenText(fileSpec)
      Dim line As String
      Scintilla1.Text = ""
      For i = 0 To startLine2 + 14
        If strRead.EndOfStream Then Exit For
        line = strRead.ReadLine()
        If i < startLine2 Then Continue For
        Scintilla1.AppendText((i + 1).ToString + vbTab + line + vbNewLine)
      Next
      strRead.Close()

      Scintilla1.Markers.DeleteAll()
      With Scintilla1.Lines(startLine - startLine2 - 1).AddMarker(2).Marker
        .Symbol = ScintillaNet.MarkerSymbol.PixMap
        '.BackColor = Color.Red
        '.ForeColor = Color.Blue
        '.SetImage( "48 4 2 1" + Chr(0) + "a c #ffffff" + Chr(0) + "b c #000000" + Chr(0) + "abaabaababaaabaabababaabaabaababaabaaababaabaaab" + Chr(0) + "abaabaababaaabaabababaabaabaababaabaaababaabaaab" + Chr(0) + "abaabaababaaabaabababaabaabaababaabaaababaabaaab" + Chr(0) + "abaabaababaaabaabababaabaabaababaabaaababaabaaab" + Chr(0))
        .Alpha = 255
        ' .SetImage(Me.Icon.ToBitmap)
        'New Bitmap("D:\icons\VS2008ImageLibrary\VS2008ImageLibrary\Annotations&Buttons\bmp_format\BuilderDialog_AddAll.bmp")
        .SetImage(My.Resources.executing5, Color.White)
      End With
      With Scintilla1.Lines(startLine - startLine2 - 1).AddMarker(3).Marker
        .BackColor = Color.FromArgb(255, 180, 228, 180)
        .ForeColor = Color.FromArgb(255, 180, 228, 180)
      End With
      Scintilla1.Margins(0).Width = 0
      Scintilla1.Margins(1).Width = 24
      Scintilla1.Margins(1).Mask = 2 ^ 2
      Scintilla1.Scrolling.ScrollBars = ScrollBars.None
      Scintilla1.Margins(2).Width = 0

      Scintilla1.Font = New Font("Courier New", 10, FontStyle.Regular, GraphicsUnit.Point)
      Scintilla1.Styles.Default.FontName = "Courier New"
      Scintilla1.Styles.Default.Font = New Font("Courier New", 10, FontStyle.Regular, GraphicsUnit.Point)
      'For i = 0 To 100
      Scintilla1.Styles(33).BackColor = Color.FromArgb(255, 166, 166, 166)
      Scintilla1.Styles(0).FontName = "Courier New"
      'Next

    End If

  End Sub

  Private Sub igGlobsearch_CellMouseUp(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellMouseUpEventArgs) Handles igGlobsearch.CellMouseUp
    If e.Button = Windows.Forms.MouseButtons.Right Then
      igGlobsearch.SetCurCell(e.RowIndex, e.ColIndex)
      Application.DoEvents()
      Dim fileSpec As String = igGlobsearch.CurRow.Tag
      If fileSpec.StartsWith("|##|") Then Exit Sub 'suche in akt. datei

      Dim ftpFile = fileSpec.StartsWith(ParaService.SettingsFolder + "ftp_", StringComparison.CurrentCultureIgnoreCase)

      FTPDateiOeffnenToolStripMenuItem.Visible = ftpFile
      FTPOrdnerNavigierenToolStripMenuItem.Visible = ftpFile

      cmenuGlobTreffer.Show(sender, e.MousePos)

    End If
  End Sub
  Private Sub igGlobsearch_CellMouseDown(ByVal sender As Object, ByVal e As TenTec.Windows.iGridLib.iGCellMouseDownEventArgs) Handles igGlobsearch.CellMouseDown
    If e.Button = Windows.Forms.MouseButtons.Right Then
      igGlobsearch.SetCurCell(e.RowIndex, e.ColIndex)

    End If
  End Sub

  Private Sub txtGlobsearchSuch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGlobsearchSuch.KeyDown
    If e.KeyCode = Keys.Enter Then
      onSearchButton()
    End If
  End Sub

  Sub onSearchButton()
    cmbGlobsearchFilefilter.Text = System.Text.RegularExpressions.Regex.Replace(cmbGlobsearchFilefilter.Text.ToUpper, "/[A-Z.]/", "")

    ListBox1.Items.Add(txtGlobsearchSuch.Text + vbTab + chkActFile.Checked.ToString + vbTab + checkRekursiv.Checked.ToString + vbTab + cmbGlobsearchFilefilter.Text + vbTab + ftvGlobSearch.SelectedFolder.name)


    If chkActFile.Checked Then
      If TypeOf getActiveRTF() Is IDockContentForm Then
        Dim frm As IDockContentForm = getActiveRTF()
        Dim fileSpec = frm.getFileTag(), fileName = frm.getViewFilename()
        Dim findText = txtGlobsearchSuch.Text.ToUpper()
        igGlobsearch.Rows.Clear()
        Dim rank As Integer, rColor As Color, lineText As String, max = frm.getLineCount() - 1
        For i = 0 To max
          lineText = frm.getLineContent(i)
          FileSearchScanLine(fileSpec, i, lineText, findText, rank, rColor)
          If rank > -1 Then
            Dim ir = tbGlobSearch.igGlobsearch.Rows.Add
            ir.Cells(0).Value = fileName
            ir.Cells(1).Value = CStr(i + 1)
            ir.Cells(2).Value = lineText
            ir.Cells(3).Value = rank
            ir.Tag = "|##|" + fileSpec + "|##|" + CStr(i + 1)
            ir.ForeColor = rColor
          End If
        Next
      End If
      With tbGlobSearch.igGlobsearch
        .SortObject.Clear()
        .SortObject.Add(3, TenTec.Windows.iGridLib.iGSortOrder.Descending)
        .Sort()
      End With
    Else
      startSearch(ftvGlobSearch.SelectedFolder.name, cmbGlobsearchFilefilter.Text, txtGlobsearchSuch.Text)

    End If


  End Sub

  Private Sub btnDoGlobSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDoGlobSearch.Click
    onSearchButton()

  End Sub


  Private Sub frmTB_globSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ParaService.Glob.readTuttiFrutti(Me) 'speichern in main_Closing
    ' Me.IsFloat = True

    chkDockWin.Checked = cls_IDEHelper.GetSingleton.Glob.para("frmTB_infoTips__isDockWin", "TRUE") = "TRUE"

  End Sub

  Private Sub btnZoomOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomOnOff.Click
    If SplitContainer1.SplitterDistance < 40 Then
      SplitContainer1.SplitterDistance = ParaService.Glob.para("splitterBigPos", "200")
    Else
      ParaService.Glob.para("splitterBigPos") = SplitContainer1.SplitterDistance
      SplitContainer1.SplitterDistance = 32
    End If
  End Sub

  Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
    Dim ctx As New ContextMenuStrip
    Dim el As ToolStripMenuItem
    For Each kvp In ftpConnections
      el = ctx.Items.Add(kvp.Key)
      AddHandler el.Click, AddressOf ctxSelServerClick
    Next
    AddHandler el.Click, AddressOf ctxSelServerClick

    ctx.Show(sender, 0, sender.height)
  End Sub

  Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
    
  End Sub

  Private Sub ctxSelServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    If sender.text = "Bearbeiten ..." Then
      Workbench.ShowOptionsDialog("ftphosts")
    Else
      DirectCast(ftvGlobSearch.SelectedFolder, CCRPFolderTV6.Folder).Name = ParaService.SettingsFolder + "ftp_" + sender.text + ""
      DirectCast(ftvGlobSearch.SelectedFolder, CCRPFolderTV6.Folder).Expanded = True
      DirectCast(ftvGlobSearch.SelectedFolder, CCRPFolderTV6.Folder).EnsureVisible()
    End If
  End Sub

  Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
    If ListBox1.SelectedIndex = -1 Then Exit Sub
    Dim p() = Split(ListBox1.SelectedItem, vbTab)

    txtGlobsearchSuch.Text = p(0)
    chkActFile.Checked = p(1)
    checkRekursiv.Checked = p(2)
    cmbGlobsearchFilefilter.Text = p(3)
    ftvGlobSearch.SelectedFolder.name = p(4)

    onSearchButton()
  End Sub

  Private Sub chkTopmost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTopmost.CheckedChanged
    Me.TopMost = chkTopmost.Checked
  End Sub

  Private Sub CheckBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDockWin.Click
    cls_IDEHelper.GetSingleton().Glob.para("frmTB_infoTips__isDockWin") = If(chkDockWin.Checked, "TRUE", "FALSE")
    Me.Close() : Me.Dispose()
    tbGlobSearch = Nothing
    tbGlobSearch = New frmTB_globSearch()
    tbGlobSearch.Show()
  End Sub

End Class