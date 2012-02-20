Public Class frmTB_legacyWidget

  Public Overrides Function GetPersistString() As String
    Return "ToolBar|##|tbLegacyWidget|##|" + txtWidgetfilename.Text + "|##|" + txtClass.Text
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    cls_IDEHelper.GetSingleton().BeforeShowAddinWindow(Me.GetPersistString(), Me)
    MyBase.Show()
  End Sub

  Property ContentBackColor() As Color
    Get
      ContentBackColor = Me.BackColor
    End Get
    Set(ByVal value As Color)
      Me.BackColor = value
    End Set
  End Property
  Property TitleHeight() As Integer
    Get
    End Get
    Set(ByVal value As Integer)
    End Set
  End Property
  Public Property TitleText() As String
    Get
      Return Me.Text
    End Get
    Set(ByVal value As String)
      Me.Text = value
    End Set
  End Property
  Sub _setExpandedState(ByVal newState As Boolean)
  End Sub
  Public Sub ToggleExpanded()
  End Sub
  Public Property Expanded() As Boolean
    Get
    End Get
    Set(ByVal value As Boolean)
    End Set
  End Property
  Public Property Icon() As Image
    Get
    End Get
    Set(ByVal value As Image)
    End Set
  End Property
  Public Property TitleBackColorLeft() As Color
    Get
    End Get
    Set(ByVal value As Color)
    End Set
  End Property
  Public Property TitleBackColorRight() As Color
    Get
    End Get
    Set(ByVal value As Color)
    End Set
  End Property
  Public Property TitleForeColor() As Color
    Get
    End Get
    Set(ByVal value As Color)
    End Set
  End Property

  Function getFileTag() As String
    Return GetPersistString()
  End Function

  Function getViewFilename() As String
    Return txtClass.Text
  End Function

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    ReloadWidget()
  End Sub

  Sub ReloadWidget()
    Me.Text = txtClass.Text
    loadUserctrlIntoWidget(Me)
  End Sub

  Private Sub frmTB_legacyWidget_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    tbLegacyWidget.Remove(GetPersistString.ToLower)
  End Sub

  Private Sub frmTB_legacyWidget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim cm As New ContextMenuStrip
    Dim mnu = cm.Items.Add("Neu laden")
    AddHandler mnu.Click, AddressOf Button1_Click
    cm.Items.Add("-").Enabled = False
    cm.Items.Add("Dateiname: " + txtWidgetfilename.Text).Enabled = False
    cm.Items.Add("Klasse: " + txtClass.Text).Enabled = False
    Me.DockHandler.TabPageContextMenuStrip = cm
  End Sub

  Private Sub Panel1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.DoubleClick
    ReloadWidget()
  End Sub

End Class
