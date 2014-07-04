Public Class ctl_options
  Implements IPropertyPage

  Private Sub btnAddFtpCred_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSFtpCred.Click
    With igFtpCred.Rows.Add()
      .Cells(3).Value = "22"
      .Cells(4).Value = "sftp"
      .Cells(2).Value = Environ("APPDATA") + "\.ssh\id_rsa.pub"
    End With
  End Sub
  Private Sub btnAddFtpCred_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFtpCred.Click
    With igFtpCred.Rows.Add()
      .Cells(3).Value = "21"
      .Cells(4).Value = "ftp"
    End With
  End Sub

  Private Sub btnDelFtpCred_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelFtpCred.Click
    On Error Resume Next
    igFtpCred.Rows.RemoveAt(igFtpCred.CurRow.Index)
  End Sub

  Private Sub igFtpCred_CurRowChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles igFtpCred.CurRowChanged
    If igFtpCred.CurRow Is Nothing Then Exit Sub

  End Sub

  Public Sub readProperties() Implements IPropertyPage.readProperties
    Try
      Dim sdata = IO.File.ReadAllText(ParaService.SettingsFolder + "ftp_hosts.txt") ' IDE.Glob.para("frm_main__thefile")
      Igrid_put(igFtpCred, sdata, "$$", "§§")
    Catch ex As Exception
      TT.Write("Exception in ctl_options.readProperties (ftp options)", ex.ToString)
    End Try

    cmbSaveSound.Text = ParaService.Glob.para("ftp_options__savesound")
    Dim dirList() As String = IO.Directory.GetFiles("C:\Windows\Media\", "*.wav", IO.SearchOption.TopDirectoryOnly)
    For Each fileSpec As String In dirList
      cmbSaveSound.Items.Add(IO.Path.GetFileName(fileSpec))
    Next
  End Sub

  Public Sub saveProperties() Implements IPropertyPage.saveProperties
    Dim sdata As String
    Igrid_get(igFtpCred, sdata, "$$", "§§")
    'IDE.Glob.para("frm_main__thefile") = sdata
    IO.File.WriteAllText(ParaService.SettingsFolder + "ftp_hosts.txt", sdata)
    readFtpConnections()

    ParaService.Glob.para("ftp_options__savesound") = cmbSaveSound.Text
  End Sub

  Private Sub cmbSaveSound_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSaveSound.SelectedIndexChanged
    If cmbSaveSound.SelectedIndex > -1 Then
      cmbSaveSound.Text = "C:\Windows\Media\" + cmbSaveSound.SelectedItem
      cmbSaveSound.SelectedIndex = -1
    End If
  End Sub

  Private Sub btnPreviewSound_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviewSound.Click
    Try
      My.Computer.Audio.Play(cmbSaveSound.Text)
    Catch ex As Exception
      MsgBox("Sound konnte nicht abgespielt werden.")
    End Try
  End Sub

  Private Sub btnSelPrivKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelPrivKey.Click
    If Not igFtpCred.SelectedRows.Count = 1 Then Return
    Using ofd As New OpenFileDialog
      ofd.Title = "Private key file auswählen..."

      ofd.FileName = "id_rsa"
      If Not String.IsNullOrEmpty(igFtpCred.CurRow.Cells(2).Value) Then ofd.FileName = igFtpCred.CurRow.Cells(2).Value

      ofd.Filter = "Private Key Files (id_rsa, *.ppk)|id_rsa;id_dsa;*.ppk|Alle Dateien|*.*"
      If ofd.ShowDialog = DialogResult.OK Then
        igFtpCred.CurRow.Cells(2).Value = ofd.FileName
      End If
    End Using
  End Sub
End Class
