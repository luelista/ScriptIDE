Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices

Public Class ResourceLoader

  ''' <summary>
  ''' The URLMON library contains this function, URLDownloadToFile, which is a way
  ''' to download files without user prompts.
  ''' </summary>
  ''' <param name="pCaller">Pointer to caller object (AX).</param>
  ''' <param name="szURL">String of the URL.</param>
  ''' <param name="szFileName">String of the destination filename/path.</param>
  ''' <param name="dwReserved">[reserved].</param>
  ''' <param name="lpfnCB">A callback function to monitor progress or abort.</param>
  ''' <returns>throws exception if not success</returns>
  <DllImport("urlmon.dll", CharSet:=CharSet.Auto, SetLastError:=True, PreserveSig:=False)> _
  Public Shared Function URLDownloadToFile(<MarshalAs(UnmanagedType.IUnknown)> ByVal pAxCaller As Object, _
                                      <MarshalAs(UnmanagedType.LPWStr)> ByVal szURL As String, _
                                      <MarshalAs(UnmanagedType.LPWStr)> ByVal szFileName As String, _
                                      <MarshalAs(UnmanagedType.U4)> ByVal dwReserved As UInteger, _
                                      ByVal lpfnCB As IntPtr) As Int32
  End Function

  Public Shared Function GetImageCached(ByVal strURL As String) As System.Drawing.Image
    Try
      If strURL.StartsWith("http") Then
        Dim cacheFile = ParaService.SettingsFolder + "iconCache\" + GetMD5Hash(strURL) + ".png"
        If IO.File.Exists(cacheFile) Then
          GetImageCached = Image.FromFile(cacheFile)
        Else
          GetImageCached = ImageFromWeb(strURL)
          GetImageCached.Save(cacheFile)
        End If
      Else
        GetImageCached = Image.FromFile(strURL)
      End If
    Catch ex As Exception
    End Try
  End Function

  Public Shared Function GetFileCached(ByVal strURL As String, Optional ByVal fileExt As String = Nothing) As String
    If String.IsNullOrEmpty(strURL) Then Return ""
    Try
      If strURL.StartsWith("http") Then
        If fileExt = Nothing Then fileExt = IO.Path.GetExtension(strURL)
        Dim cacheFile = ParaService.SettingsFolder + "iconCache\" + GetMD5Hash(strURL) + fileExt
        If Not IO.File.Exists(cacheFile) Then
          If URLDownloadToFile(IntPtr.Zero, strURL, cacheFile, Nothing, IntPtr.Zero) <> 0 Then
            Return "" 'fehler
          End If
        End If
        Return cacheFile
      Else
        Return strURL
      End If
    Catch ex As Exception
      Return ""
    End Try
  End Function

  ' Bild von Webserver laden
  Public Shared Function ImageFromWeb(ByVal sURL As String) As Image
    Try
      ' Web-Anfrage mit vorgegebener URL zur Bilddatei
      Dim oRequest As WebRequest = WebRequest.Create(sURL)
      oRequest.Method = "GET"

      ' Antwort unserer Anfrage...
      Dim oResponse As WebResponse = oRequest.GetResponse()
      Application.DoEvents()

      ' Stream-Objekt mit den Bilddaten erstellen
      Dim oStream As New StreamReader(oResponse.GetResponseStream())

      ' Bild aus dem Stream-Objekt in ein Image-Objekt kopieren
      Dim oImg As Image = Image.FromStream(oStream.BaseStream)

      ' Objekte zerstören
      oStream.Close()
      oResponse.Close()

      ' Image-Objekt zurückgeben
      Return oImg

    Catch ex As Exception
      ' Fehler: Nothing zurückgeben
      Return Nothing
    End Try
  End Function
  ''' <summary>
  ''' Liefert den MD5-Hash-Wert eines Strings als Byte-Array zurück
  ''' </summary>
  ''' <param name="Text">Text zu dem ein Byte-Array ermittelt werden soll</param>
  ''' <returns></returns>
  ''' <remarks>Als Encoding wird der System.Default verwendet;</remarks>
  Public Shared Function MD5HashForString(ByVal Text As String) As Byte()
    Dim enc As System.Text.Encoding = System.Text.Encoding.Default
    Dim tmpSource() As Byte = enc.GetBytes(Text)

    MD5HashForString = New MD5CryptoServiceProvider().ComputeHash(tmpSource)
  End Function

  ''' <summary>
  ''' Liefert den MD5-Hash-Wert eines Strings als String zurück
  ''' </summary>
  ''' <param name="Text">Text zu dem ein MD5-Hash ermittelt werden soll</param>
  ''' <returns></returns>
  ''' <remarks>Als Encoding wird der System.Default verwendet;</remarks>
  Public Shared Function GetMD5Hash(ByVal Text As String) As String
    Dim enc As System.Text.Encoding = System.Text.Encoding.Default
    Dim tmpSource() As Byte = enc.GetBytes(Text)

    Return ByteArrayToString(New MD5CryptoServiceProvider().ComputeHash(tmpSource))
  End Function

  ''' <summary>
  ''' Liefert ein Byte-Array als String zurück (können Sie verwenden um einen 
  ''' MD5-Hash-Wert vom Type "Byte-Array" in eine String um zu wandeln
  ''' </summary>
  ''' <param name="arrInput"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function ByteArrayToString(ByVal arrInput() As Byte) As String
    Dim i As Integer
    Dim sOutput As New StringBuilder(arrInput.Length)

    For i = 0 To arrInput.Length - 1
      sOutput.Append(arrInput(i).ToString("X2"))
    Next

    Return sOutput.ToString()
  End Function



End Class
