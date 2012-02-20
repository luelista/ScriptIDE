Imports System.Windows.Forms

Public Class frm_createNew

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

  Private Sub frm_createNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Public Enum ElementType
    None
    File
    Folder
  End Enum

  WriteOnly Property CreateIn() As String
    Set(ByVal value As String)
      Label1.Text = "Um ein neues Element unter """ + value + """ anzulegen, wähle den Typ aus und gib einen Namen ein:"
    End Set
  End Property

  Property SelectedElementType() As ElementType
    Get
      If rbFile.Checked Then Return ElementType.File
      If rbFolder.Checked Then Return ElementType.Folder
      Return ElementType.None
    End Get
    Set(ByVal value As ElementType)
      rbFile.Checked = value = ElementType.File
      rbFolder.Checked = value = ElementType.Folder
    End Set
  End Property
  Property ElementName() As String
    Get
      Return TextBox1.Text
    End Get
    Set(ByVal value As String)
      TextBox1.Text = value
    End Set
  End Property
  Property NavigateAfter() As Boolean
    Get
      Return CheckBox1.Checked
    End Get
    Set(ByVal value As Boolean)
      CheckBox1.Checked = value
    End Set
  End Property

  Private Sub onCheckEnabledState(ByVal sender As System.Object, ByVal e As System.EventArgs) _
  Handles rbFolder.CheckedChanged, rbFile.CheckedChanged, TextBox1.TextChanged
    OK_Button.Enabled = SelectedElementType <> ElementType.None And TextBox1.Text <> ""
  End Sub

  Private Sub rbFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFolder.Click, rbFile.Click
    TextBox1.Focus()

  End Sub

  Private Sub frm_createNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    TextBox1.Focus()
    CheckBox1.Checked = ParaService.Glob.para("frmTB_ftpExplorer__chkLoadAfterCreation", "FALSE") = "TRUE"
  End Sub

  Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
    ParaService.Glob.para("frmTB_ftpExplorer__chkLoadAfterCreation") = If(CheckBox1.Checked, "TRUE", "FALSE")
  End Sub
End Class
