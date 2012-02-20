<FileActionClass()> Module app_fileHandlers

  <FileAction("*.*", "Hex-View", "http://www.iconfinder.net/data/icons/oxygen/16x16/mimetypes/text-x-hex.png")> _
  Sub ShowHexViewer(ByVal tab As IDockContentForm)
    IDE.NavigateFile("[.hex]" + tab.URL)

  End Sub

End Module
