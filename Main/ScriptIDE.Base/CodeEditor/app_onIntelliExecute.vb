Module app_onIntelliExecute

  Public fileAssocTab As New Dictionary(Of String, String)
  'Public activeIntelliPanelClass As String = ""

  Sub createFileAssocTab()
    Dim files() = IO.Directory.GetFiles(ParaService.SettingsFolder + "scintillaConfig\", "*.xml")
    Dim writer As New System.IO.StreamWriter(ParaService.SettingsFolder + "scintillaConfig\fileAssoc.txt")
    For Each fileSpec In files
      Dim assocTyp = IO.Path.GetFileNameWithoutExtension(fileSpec)
      writer.WriteLine()
      writer.WriteLine(assocTyp)
      Dim xml As New Xml.XmlTextReader(fileSpec)
      While xml.Read()
        If xml.IsStartElement() Then
          If xml.Name = "ScintillaLanguageConfig" Then
            If xml.AttributeCount > 0 Then
              Try
                Dim fileExts() = Split(xml.GetAttribute("filefilter"), ";")
                For Each ext In fileExts
                  If ext.StartsWith("*.") Then
                    fileAssocTab.Add(ext.Substring(1).ToUpper, assocTyp)
                    writer.WriteLine(ext.Substring(1).ToUpper)
                  End If
                Next
              Catch ex As Exception
                TT.Write("createFileAssocTab - error parsing Attribute 'filefilter'", ex.ToString, "warn")
                Continue For
              End Try
            End If
          Else
            TT.Write("createFileAssocTab - invalid RootNode", "Name:" + xml.Name + "  fileSpec:" + fileSpec, "warn")
            Continue For
          End If
        End If
      End While
    Next
  End Sub

  Sub loadFileAssocTab()
    If IO.File.Exists(ParaService.SettingsFolder + "scintillaConfig\fileassoc.txt") = False Then IO.File.WriteAllText(ParaService.SettingsFolder + "scintillaConfig\fileassoc.txt", "//bitte starte das Skript sys_createFileAssoc")
    Dim cont() = IO.File.ReadAllLines(ParaService.SettingsFolder + "scintillaConfig\fileassoc.txt")
    Dim assocTyp As String = ""
    For Each line In cont
      line = line.Trim
      If line = "" Or line.StartsWith("//") Then Continue For
      If line.StartsWith(".") Then
        fileAssocTab.Add(line.ToUpper, assocTyp)
        Debug.Print(line + vbTab + assocTyp)
      Else
        assocTyp = line
        Debug.Print(">>>" + line)
      End If
    Next
  End Sub

  Function getIntelliPanelClass(ByVal fileExt As String)
    fileExt = IO.Path.GetExtension(fileExt).ToUpper
    If fileAssocTab.ContainsKey(fileExt) Then
      getIntelliPanelClass = fileAssocTab(fileExt)
    Else
      getIntelliPanelClass = ""
    End If
  End Function
  'Sub showIntelliPanelByFileExt(ByVal fileExt As String)
  '  Dim panelName As String = getIntelliPanelClass(fileExt)
  '  showIntelliPanel(panelName)
  'End Sub


  'Sub showIntelliPanel(ByVal className As String, ByVal frm As frmDC_scintilla)
  '  'If className = activeIntelliPanelClass Then Exit Sub
  '  'activeIntelliPanelClass = className

  '  Dim hasVisiblePanel As Boolean = False
  '  Dim hlp As New ScriptHelper_intelliBar(frm.flpIntelliPanel, className)

  '  Dim ref = sh.scriptClass("lang_" + className)
  '  If ref IsNot Nothing Then
  '    hasVisiblePanel = ref.createIntelliPanel(hlp)
  '  End If


  '  If hasVisiblePanel Then
  '    frm.sc1.Top = 35 : frm.flpIntelliPanel.Show()
  '  Else
  '    frm.sc1.Top = 0 : frm.flpIntelliPanel.Hide()
  '  End If

  '  frm.sc1.Height = frm.Height - frm.sc1.Top - 0
  'End Sub

  'Sub intellibarCallback(ByVal clsName As String, ByVal funcName As String, ByVal para As Object)
  '  Dim ref = sh.scriptClass("lang_" + clsName)
  '  Try
  '    CallByName(ref, funcName, CallType.Method, para)

  '  Catch ex As Exception
  '    Dim fileSpec = sh.expandScriptClassName("lang_" + clsName)
  '    gotoNote("loc:/" + fileSpec)
  '    MsgBox("Fehler beim Aufrufen des Event-Handlers " + funcName + vbNewLine + "Funktion nicht definiert oder falsche Parameter")

  '  End Try

  'End Sub

  'Sub onIntelliButtonMouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
  '  If isKeyPressed(Keys.ControlKey) Then
  '    MsgBox("Button info:" + vbNewLine + "* " + Join(sender.Tag, vbNewLine + "* "))
  '  Else

  '    Dim clsName As String = sender.Tag(3)
  '    Dim funcName As String = "onBtnClick_" + sender.Tag(0)

  '    Select Case (clsName + "__" + funcName).ToLower
  '      Case "vbscript__onbtnclick_scriptclass", "vbnet__onbtnclick_scriptclass"
  '        autostartActiveFile(True)

  '      Case "vbscript__onbtnclick_autostart", "vbnet__onbtnclick_autostart"
  '        autostartActiveFile()

  '      Case Else
  '        intellibarCallback(clsName, funcName, e.Button.ToString)

  '    End Select


  '  End If
  'End Sub

  Sub initScriptHelperMethodsList(ByVal typ As String)
    Dim cls = System.Reflection.Assembly.GetExecutingAssembly().GetType(typ)
    Dim meth = cls.GetMethods()
    Dim out(meth.Length - 1) As String
    For i = 0 To meth.Length - 1
      out(i) = meth(i).Name
    Next
    Array.Sort(out)
    scriptHelperMethods.Add(typ, out)
  End Sub



End Module

'<Microsoft.VisualBasic.ComClass()> Public Class ScriptHelper_intelliBar
'  Private myPanel As FlowLayoutPanel
'  Private fileClass As String

'  Public ReadOnly Property intelliPanel()
'    Get
'      Return myPanel
'    End Get
'  End Property
'  Public ReadOnly Property className()
'    Get
'      Return fileClass
'    End Get
'  End Property

'  Public Sub New(ByVal pnl As FlowLayoutPanel, ByVal className As String)
'    myPanel = pnl
'    fileClass = className
'  End Sub

'  Sub addButton(ByVal id, ByVal text, ByVal bgColor)
'    Dim btn As New Button
'    btn.Name = myPanel.Name + "_btn" + id
'    btn.Height = 21
'    btn.Margin = New Padding(3, 5, 3, 0)
'    btn.AutoSizeMode = AutoSizeMode.GrowOnly
'    btn.AutoSize = True
'    btn.Text = text
'    btn.BackColor = ColorTranslator.FromHtml(bgColor)
'    btn.Tag = New String() {id, text, bgColor, fileClass, "", "", "", "", "", ""}
'    'AddHandler btn.MouseUp, AddressOf onIntelliButtonMouseUp
'    myPanel.Controls.Add(btn)

'  End Sub
'  Sub addLabel(ByVal text, ByVal foreColor, ByVal fontSize)
'    Dim lbl As New Label
'    'lbl.Name = myPanel.Name + "_btn" + id
'    'lbl.Height = 28
'    'lbl.AutoSizeMode = AutoSizeMode.GrowOnly
'    lbl.Margin = New Padding(3, 8, 5, 0)
'    lbl.AutoSize = True
'    lbl.Text = text
'    lbl.Font = New Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Point)
'    lbl.ForeColor = ColorTranslator.FromHtml(foreColor)
'    lbl.Tag = New String() {"", text, "", fileClass, foreColor, "", "", "", "", ""}

'    myPanel.Controls.Add(lbl)

'  End Sub

'Sub resetControls()
'  myPanel.Controls.Clear()

'End Sub

'Sub resetPanel()
'  myPanel.Controls.Clear()

'End Sub

'End Class

