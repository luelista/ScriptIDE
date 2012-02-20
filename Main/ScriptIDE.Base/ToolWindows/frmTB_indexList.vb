Public Class frmTB_indexList
  Public skipNavIndexList As Boolean = False

  Private _isDisabled As Boolean
  Public Property isDisabled() As Boolean
    Get
      Return _isDisabled
    End Get
    Set(ByVal value As Boolean)
      _isDisabled = value
      IndexlisteDeaktivierenToolStripMenuItem.Checked = value
      ParaService.Glob.para("frmTB_indexList__isDisabled") = If(value, "TRUE", "FALSE")
      Me.Text = "Indexliste" + If(value, " [deaktiviert]", "")
      Me.Enabled = Not value
    End Set
  End Property

  Public Overrides Function GetPersistString() As String
    Return tbPrefix + "IndexList"
  End Function
  Shadows Sub Show()
    'hier wird das Fenster ins Dockpanel eingefügt
    'ohne diesen Aufruf wäre es eine normale Form:
    cls_IDEHelper.GetSingleton().BeforeShowAddinWindow(Me.GetPersistString(), Me)
    MyBase.Show()
  End Sub

  Private Sub frmTB_indexList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'Me.TabPageContextMenuStrip = ContextMenuStrip1
    isDisabled = ParaService.Glob.para("frmTB_indexList__isDisabled", "FALSE") = "TRUE"
  End Sub

  Private Sub VersteckenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersteckenToolStripMenuItem.Click
    Me.Hide()
  End Sub

  Private Sub IndexlisteDeaktivierenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IndexlisteDeaktivierenToolStripMenuItem.Click
    isDisabled = Not isDisabled
    IndexlisteDeaktivierenToolStripMenuItem.Checked = isDisabled
  End Sub
End Class