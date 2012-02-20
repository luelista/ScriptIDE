Imports TenTec.Windows.iGridLib

Public Class prop_TextEditor
  Implements IPropertyPage

  Dim actFilespec As String


  Private _isdirty As Boolean
  Public Property isDirty() As Boolean
    Get
      Return _isdirty
    End Get
    Set(ByVal value As Boolean)
      _isdirty = value

      btnSaveXML.Font = New Font(Me.Font, If(_isdirty, FontStyle.Bold, FontStyle.Regular))
    End Set
  End Property


  Private Sub prop_Hotkeys_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    With scXML
      .Font = New Font("Courier New", 11, FontStyle.Regular, GraphicsUnit.Point)
      .Styles.Default.FontName = "Courier New"
      .Selection.HideSelection = False
      .AcceptsTab = True

      .Indentation.IndentWidth = 2
      .Indentation.TabWidth = 2
      .Indentation.SmartIndentType = ScintillaNet.SmartIndent.Simple
      .Indentation.UseTabs = False

      .Margins(0).Width = 0 'Spalte für Breakpoint

      .Margins(1).Width = 0 'Zeilennummer

      .Margins(2).Width = 10 'Restliche symbole
      .Margins(2).Type = ScintillaNet.MarginType.Symbol
      .Margins(2).IsClickable = True
      .Margins(2).IsMarkerMargin = True
      .Margins(2).Mask = (2 ^ 10) Or (2 ^ 12) Or (2 ^ 15) '12 und 13=executing; 15 und 13=executing2; 10 und 11=error; 14=jumpToLine

      .Margins(3).IsMarkerMargin = True
      .Margins(3).Type = ScintillaNet.MarginType.Symbol
      .Margins(3).Mask = (2 ^ 11) Or (2 ^ 13) Or (2 ^ 14)

      .Styles.LineNumber.IsVisible = False 'zeilennummern an
      .IsBraceMatching = True

      .ConfigurationManager.Language = "xml"
      .Lexing.Colorize()
    End With


  End Sub



  Public Sub readProperties() Implements Core.IPropertyPage.readProperties
    For Each kvp In fileAssocTab
      If ListBox1.Items.Contains(kvp.Value) = False Then
        ListBox1.Items.Add(kvp.Value)
      End If
    Next
  End Sub

  Public Sub saveProperties() Implements Core.IPropertyPage.saveProperties
    If isDirty Then
      If MsgBox("Sollen die Änderungen in der Datei " + actFilespec + " übernommen werden?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        onsave()
      End If
    End If
  End Sub

  Sub onsave()
    IO.File.WriteAllText(actFilespec, scXML.Text)
    isDirty = False
  End Sub
  Private Sub btnSaveXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveXML.Click
    onsave()
  End Sub

  Private Sub ListBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDown
    If e.Button = Windows.Forms.MouseButtons.Right Then
      If isDirty Then
        If MsgBox("Sollen die Änderungen in der Datei " + actFilespec + " übernommen werden?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
          onsave()
        End If
      End If
      Dim idx = ListBox1.IndexFromPoint(e.Location)
      ListBox1.SelectedIndex = idx
      If idx > -1 Then
        actFilespec = ParaService.SettingsFolder + "scintillaConfig\" + ListBox1.SelectedItem + ".xml"
        scXML.Text = IO.File.ReadAllText(actFilespec)
        isDirty = False
      End If
    End If
  End Sub

  Private Sub scXML_TextDeleted(ByVal sender As Object, ByVal e As ScintillaNet.TextModifiedEventArgs) Handles scXML.TextDeleted
    If e.IsUserChange And Not isDirty Then isDirty = True
  End Sub

  Private Sub scXML_TextInserted(ByVal sender As Object, ByVal e As ScintillaNet.TextModifiedEventArgs) Handles scXML.TextInserted
    If e.IsUserChange And Not isDirty Then isDirty = True
  End Sub

End Class
