Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Module sys_imageFromWeb

  ' Bild von Webserver laden
  Public Function ImageFromWeb(ByVal sURL As String) As Image
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
  Public Function MD5HashForString(ByVal Text As String) As Byte()
    Dim enc As System.Text.Encoding = System.Text.Encoding.Default
    Dim tmpSource() As Byte = enc.GetBytes(Text)

    MD5HashForString = New MD5CryptoServiceProvider().ComputeHash(tmpSource)
  End Function

  ''' <summary>
  ''' Liefert ein Byte-Array als String zurück (können Sie verwenden um einen 
  ''' MD5-Hash-Wert vom Type "Byte-Array" in eine String um zu wandeln
  ''' </summary>
  ''' <param name="arrInput"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function ByteArrayToString(ByVal arrInput() As Byte) As String
    Dim i As Integer
    Dim sOutput As New StringBuilder(arrInput.Length)

    For i = 0 To arrInput.Length - 1
      sOutput.Append(arrInput(i).ToString("X2"))
    Next

    Return sOutput.ToString()
  End Function
End Module
