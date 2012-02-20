Imports System.Reflection

<Microsoft.VisualBasic.ComClass()> Public Class frmTB_scriptWin
  Implements IScriptedWindow

  Private p_className As String
  Private p_winID As String

  Private p_offsetX As Integer = 0, p_offsetY As Integer = 0
  Private actX As Integer = 0, actY As Integer = 0
  Private insertDir As Integer = 1 ' As insertDirection
  Private lastRowHeight As Integer = 0

  Private eventHandlerTypes As String = ""
  Private isDockContent As Boolean = True

  Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    isDockContent = True
  End Sub

  Sub New(ByVal persistString As String, ByVal className As String, ByVal isDockForm As Boolean, ByVal dockPosHint As Integer)

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Me.ShowHint = dockPosHint
    WinID = persistString
    Me.ClassName = className
    isDockContent = isDockForm
    Me.Text = Replace(persistString, "|##|", " - ")
  End Sub

  ReadOnly Property PNL() As IScriptedPanel Implements IScriptedWindow.PNL
    Get
      Return pnlMain
    End Get
  End Property

  Public Property WinID() As String Implements IScriptedWindow.winID
    Get
      Return p_winID
    End Get
    Set(ByVal value As String)
      p_winID = value
      PNL.winID = value
      TT.Write("Set WinID=", value)
      'If value IsNot Nothing AndAlso value.Contains(".") Then
      '  Dim clsName = value.Substring(0, value.IndexOf("."))
      '  'If ScriptHost.Instance.expandScriptClassName(clsName) <> "" Then
      '  ScriptBearbeitenToolStripMenuItem.Visible = True
      '  ScriptneuladenToolStripMenuItem.Visible = True
      '  'AddInManagerToolStripMenuItem.Visible = False
      '  btnLoadScript.Enabled = True
      '  p_className = clsName
      '  'End If
      'End If
    End Set
  End Property

  'Private p_winIDPrefix As String = "Script|##|"
  'Public Property WinIDPrefix() As String
  '  Get
  '    Return p_winIDPrefix
  '  End Get
  '  Set(ByVal value As String)
  '    p_winIDPrefix = value
  '  End Set
  'End Property


  Public Property ClassName() As String Implements IScriptedWindow.className
    Get
      Return p_className
    End Get
    Set(ByVal value As String)
      p_className = value
      PNL.className = value
      If String.IsNullOrEmpty(value) = False Then
        ScriptBearbeitenToolStripMenuItem.Visible = True
        ScriptneuladenToolStripMenuItem.Visible = True
        btnLoadScript.Enabled = True

      End If
    End Set
  End Property

  Function getFileTag() As String Implements IScriptedWindow.getFileTag
    Return WinID
  End Function

  Function getViewFilename() As String Implements IScriptedWindow.getViewFilename
    Return Text
  End Function

  Public Overrides Function GetPersistString() As String Implements IScriptedWindow.GetPersistString
    Return WinID
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    If isDockContent Then
      If ScriptWindowManager.IdeHelper IsNot Nothing Then ScriptWindowManager.IdeHelper.BeforeShowAddinWindow(Me.GetPersistString(), Me)
      'ScriptHost.Instance.OnBeforeScriptWindowShow(WinID, Me, False)
    End If
    MyBase.Show()
  End Sub

  Private Sub frmTB_scriptedContent_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    On Error Resume Next
    'Dim para() = Split(WinID, "|##|")
    'Dim key = para(2).ToLower

    ScriptWindowManager.OnScriptWindowClose(Me.WinID)
  End Sub

  Private Sub frmTB_scriptedContent_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    'On Error Resume Next
    'If eventHandlerTypes.Contains("|FORMKEYDOWN|") Then
    'Dim ref = scriptClass(ClassName)
    'Dim eventArgs As New ScriptEventArgs("KeyDown", sender, , , , getKeyString(e), ClassName)
    'eventArgs.ID = WinID.Substring(WinID.IndexOf(".") + 1)
    'ScriptHost.Instance.OnScriptWindowEvent(WinID, "Form", eventArgs)
    'End If
  End Sub

  Private Sub frmTB_scriptedContent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    On Error Resume Next
    Me.DockHandler.TabPageContextMenuStrip = cmCaption

    If ClassName <> "" Then
      Dim eventArgs As New ScriptEventArgs("Load", sender, , , , , ClassName)
      eventArgs.ID = WinID.Substring(WinID.IndexOf(".") + 1)
      'ScriptHost.Instance.OnScriptWindowEvent(WinID, "Form", eventArgs)
      ScriptWindowManager.OnScriptWindowEvent(WinID, "Form", eventArgs)
    End If
  End Sub

  Sub setVisible(ByVal stat As Boolean) Implements IScriptedWindow.setVisible
    Me.Visible = stat
  End Sub

  Private Sub AddInManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddInManagerToolStripMenuItem.Click
    'frm_windowManager.Show()
    'frm_windowManager.Activate()
    'frm_windowManager.TabControl1.SelectedIndex = 1
    If ScriptWindowManager.IdeHelper IsNot Nothing Then ScriptWindowManager.IdeHelper.ShowOptionsDialog("addins")
  End Sub

  Private Sub ScriptneuladenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles ScriptneuladenToolStripMenuItem.Click, btnLoadScript.Click
    MsgBox("Coming soon....")
    'TODO: wie soll der die Klasse laden ???
    'If ClassName <> "" Then
    '  Try
    '    Dim ref = scriptClass(ClassName)
    '    ref.AutoStart()
    '  Catch ex As Exception
    '  End Try
    'End If
  End Sub


  Private Sub ScriptBearbeitenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScriptBearbeitenToolStripMenuItem.Click
    MsgBox("Coming soon....")
    'TODO: Name ermitteln
    'Dim fileSpec = ScriptHost.Instance.expandScriptClassName(ClassName)
    ''onNavigate(fileSpec)
    'ScriptWindowManager.IdeHelper.NavigateFile(fileSpec)
  End Sub

End Class