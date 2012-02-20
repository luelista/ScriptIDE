Public Class ctl_options
  Implements IPropertyPage

  Private Sub btnAddFtpCred_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFtpCred.Click
    igFtpCred.Rows.Add()
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
End Class
