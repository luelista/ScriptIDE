Public Class CoreTraceListener
  Private Sub New()
  End Sub

  Public Shared Sub Initialize()
    AddHandler TT.TraceWrite, AddressOf OnTrace
    AddHandler TT.PrintLineChanged, AddressOf OnPrintLine
  End Sub

  Public Shared Sub OnTrace(ByVal para1 As String, Optional ByVal para2 As String = "", Optional ByVal typ As String = "info", Optional ByVal pcodeLink As String = "")
    Dim highlightLine As Boolean = False
    typ = LCase(typ)
    If typ.EndsWith(",highlight") Then
      highlightLine = True
      typ = Replace(typ, ",highlight", "")
    End If
    If isIDEMode And ScriptHost.Instance.InformationWindowVisible Then
      Dim ir = MAIN.IGrid2.Rows.Add
      ir.Visible = MAIN.traceFilter(typ)
      ir.Cells(0).Value = typ
      If MAIN.iml_TraceTypes.Images.ContainsKey(typ) Then ir.Cells(0).ImageIndex = MAIN.iml_TraceTypes.Images.IndexOfKey(typ)
      ir.Cells(1).Value = para1
      ir.Cells(2).Value = para2
      ir.Tag = pcodeLink

      MAIN.autoScrollFlag = True
      'If MAIN.checkTraceAutoscroll.Checked Then MAIN.IGrid2.SetCurRow(ir.Index)
      If highlightLine Then
        MAIN.TextBox2.BackColor = Color.Gold
        MAIN.timer_fadeOutErrBox.Start()
      End If
    End If
  End Sub

  Public Shared Sub OnPrintLine(ByVal index As Integer, ByVal title As String, ByVal data As String, Optional ByVal pcodeLink As String = "")
    With ScriptHost.Instance
      If isIDEMode And .PrintLineWndVisible Then
        .getPrintlineWndRef().setPrintLine(index, data, title)
      End If
    End With
  End Sub


End Class
