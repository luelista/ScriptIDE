Public Class cls_globPara

  ': ========== Globale Variablen ==========================================

  Dim m_paraFileSpec As String
  Dim m_content As New Dictionary(Of String, String)
  Const tabDelimiter As String = "<=" + vbTab



  ': ========== Konstruktor + Destruktor ==================================

  Public Sub New(Optional ByVal fileSpec As String = "")
    m_paraFileSpec = fileSpec
    If m_paraFileSpec = "" Then
      m_paraFileSpec = fp(My.Application.Info.DirectoryPath, My.Application.Info.AssemblyName + ".para.txt")
    End If
    Dim folder As String = My.Computer.FileSystem.GetParentPath(m_paraFileSpec)
    System.IO.Directory.CreateDirectory(folder)

    readFile()
  End Sub
  Protected Overrides Sub Finalize()
    saveParaFile()
    MyBase.Finalize()
  End Sub



  ': ========== Haupteigenschaft ========================

  Public Property para(ByVal key As String, Optional ByVal def As String = "") As String
    Get
      If m_content.ContainsKey(key) Then
        para = m_content.Item(key)
      Else
        para = def
      End If
    End Get
    Set(ByVal value As String)
      If m_content.ContainsKey(key) Then
        m_content.Item(key) = value
      Else
        m_content.Add(key, value)
      End If
    End Set
  End Property


  ': ========== Hilfsfunktionen ========================

  Public Function appPath() As String
    Return fp(My.Computer.FileSystem.GetParentPath(Application.ExecutablePath))
  End Function
  Public Function fp(ByVal path As String, Optional ByVal fileName As String = "")
    fp = path + IIf(path.EndsWith("\"), "", "\") + If(fileName.StartsWith("\"), fileName.Substring(1), fileName)
  End Function

  Public Function fpUNIX(ByVal path As String, Optional ByVal fileName As String = "")
    fpUNIX = path + IIf(path.EndsWith("/"), "", "/") + If(fileName.StartsWith("/"), fileName.Substring(1), fileName)
  End Function


  Public Function Contains(ByVal key As String) As Boolean
    Contains = m_content.ContainsKey(key)

  End Function



  ': ========== Form-Tools ========================

  Public Sub readFormPos(ByVal frm As Form, Optional ByVal readSize As Boolean = True, Optional ByVal suffix As String = "")
    Try
      Dim paraName As String = frm.Name.ToLower + "__" + "Rect" + suffix
      Dim formPos() As String = Split(Me.para(paraName), ";")
      frm.Left = CInt(formPos(0))
      frm.Top = CInt(formPos(1))
      If readSize Then
        frm.Width = CInt(formPos(2))
        frm.Height = CInt(formPos(3))
      End If

    Catch ex As Exception

    End Try
  End Sub

  Public Sub saveFormPos(ByVal frm As Form, Optional ByVal suffix As String = "")
    Dim formPos As String

    With frm
      If .WindowState = FormWindowState.Minimized Then .WindowState = FormWindowState.Normal
      formPos = .Left.ToString + ";" + .Top.ToString _
              + ";" + .Width.ToString + ";" + .Height.ToString
      Dim paraName As String = frm.Name.ToLower + "__" + "Rect" + suffix
      Me.para(paraName) = formPos
    End With
  End Sub

  Public Sub readTuttiFrutti(ByVal frm As ContainerControl)
    recursive_readTuttiFrutti(frm, frm)
  End Sub
  Public Sub saveTuttiFrutti(ByVal frm As ContainerControl)
    recursive_saveTuttiFrutti(frm, frm)
  End Sub

  Public Sub recursive_readTuttiFrutti(ByVal frm As ContainerControl, ByVal ctrl As Control)
    On Error Resume Next
    Dim typ As String
    Dim prefix As String = frm.Name + "__"
    For Each subctrl As Object In ctrl.Controls
      If subctrl.Controls.Count > 0 Then recursive_readTuttiFrutti(frm, subctrl)
      typ = subctrl.GetType().ToString

      Debug.Print(subctrl.Name + vbTab + typ)
      If subctrl.Name.startswith("qq_") Then Continue For

      If typ = "System.Windows.Forms.RadioButton" Then
        Dim paras() As String = Split(subctrl.Name, "__")
        If Me.para(prefix + paras(0)) = paras(1) Then
          subctrl.Checked = True
        Else
          subctrl.checked = False
        End If
      End If

      If Not Me.Contains(prefix + subctrl.Name) Then Continue For
      Debug.Print("ja" + vbTab + subctrl.Name + vbTab + typ)

      If typ = "System.Windows.Forms.TextBox" Then
        subctrl.Text = Me.para(prefix + subctrl.Name)
      End If
      If typ = "System.Windows.Forms.ComboBox" Then
        subctrl.Text = Me.para(prefix + subctrl.Name)
      End If
      If typ = "System.Windows.Forms.CheckBox" Then
        subctrl.Checked = (Me.para(prefix + subctrl.Name) = "TRUE")
      End If
      If typ = "System.Windows.Forms.SplitContainer" Then
        subctrl.SplitterDistance = Me.para(prefix + subctrl.Name)
        subctrl.Orientation = Me.para(prefix + subctrl.Name + ".Or")
      End If
      If typ = "AxCCRPFolderTV6.AxFolderTreeview" Then
        subctrl.SelectedFolder.name = Me.para(prefix + subctrl.Name)
      End If
    Next
  End Sub
  Public Sub recursive_saveTuttiFrutti(ByVal frm As ContainerControl, ByVal ctrl As Control)
    On Error Resume Next
    Dim typ As String
    Dim prefix As String = frm.Name + "__"
    For Each subctrl As Control In ctrl.Controls
      If subctrl.Name.StartsWith("qq_") Then Continue For
      typ = subctrl.GetType().ToString

      If typ = "System.Windows.Forms.TextBox" Then
        Me.para(prefix + subctrl.Name) = CType(subctrl, TextBox).Text
      End If
      If typ = "System.Windows.Forms.ComboBox" Then
        Me.para(prefix + subctrl.Name) = CType(subctrl, ComboBox).Text
      End If
      If typ = "System.Windows.Forms.CheckBox" Then
        Me.para(prefix + subctrl.Name) = IIf(CType(subctrl, CheckBox).Checked, "TRUE", "FALSE")
      End If
      If typ = "System.Windows.Forms.SplitContainer" Then
        Me.para(prefix + subctrl.Name) = CType(subctrl, SplitContainer).SplitterDistance.ToString
        Me.para(prefix + subctrl.Name + ".Or") = CInt(CType(subctrl, SplitContainer).Orientation).ToString
      End If
      If typ = "System.Windows.Forms.RadioButton" Then
        Dim radioBox As RadioButton = subctrl
        If radioBox.Checked Then
          Dim paras() As String = Split(subctrl.Name, "__")
          Me.para(prefix + paras(0)) = paras(1)
        End If
      End If
      If typ = "AxCCRPFolderTV6.AxFolderTreeview" Then
        Me.para(prefix + subctrl.Name) = CType(subctrl, Object).SelectedFolder.name
      End If

      If subctrl.Controls.Count > 0 Then recursive_saveTuttiFrutti(frm, subctrl)
    Next
  End Sub


  ': ========== Private Funktionen ====================

  Private Sub readFile()
    On Error Resume Next
    Err.Clear()

    If Not My.Computer.FileSystem.FileExists(m_paraFileSpec) Then Exit Sub

    Dim cont() As String = _
       Split(My.Computer.FileSystem.ReadAllText(m_paraFileSpec), vbNewLine)

    Dim line(), lineString As String
    For Each lineString In cont
      line = Split(lineString, tabDelimiter)
      If line.Length < 2 Then Continue For

      m_content.Add(line(0), Replace(line(1), "|²ZS³|", vbNewLine))
      'TT.Write("ParaRead", line(0))
      'Debug.Print(lineString)
      'Stop
    Next

    If Err.Number <> 0 Then MsgBox("beim Laden der Einstellungen ist ein Fehler aufgetreten:" + vbNewLine + Err.Description + vbNewLine + "(cls_globPara)")


  End Sub

  Sub saveParaFile()
    Dim cont As String = ""
    Dim key, item As String

    For Each key In m_content.Keys
      item = m_content.Item(key)
      item = Replace(item, vbNewLine, "|²ZS³|")
      cont += key + tabDelimiter + item + tabDelimiter + vbNewLine
    Next
    'MsgBox(cont)
    My.Computer.FileSystem.WriteAllText(m_paraFileSpec, cont, False)
  End Sub




End Class
