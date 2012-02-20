Module app_compilerErrors

  Function processCompileErrors(ByVal className As String, ByVal result As CodeDom.Compiler.CompilerResults) As Boolean
    Dim firstErrLine = -1, errCount, warnCount As Integer
    If tbCompileErrors IsNot Nothing Then tbCompileErrors.clearList()
    If result.Errors.HasErrors Or result.Errors.HasWarnings Then
      ScriptHost.Instance.ErrorListVisible = True
      With tbCompileErrors
        For Each er As CodeDom.Compiler.CompilerError In result.Errors
          If er.IsWarning Then warnCount += 1 Else errCount += 1
          If er.IsWarning = False And firstErrLine = -1 Then firstErrLine = er.Line
          Dim ir = .igCompileErrors.Rows.Add
          ir.Cells("typ").Value = er.ErrorNumber
          ir.Cells("typ").ImageIndex = .iml_TraceTypes.Images.IndexOfKey(If(er.IsWarning, "warn", "err"))
          'ir.Cells(1).Value = er.ErrorNumber
          ir.Cells("desc").Value = er.ErrorText
          ir.Cells("file").Value = IO.Path.GetFileName(er.FileName)
          ir.Tag = New Object() {er.FileName, er.Line}
          ir.Cells("line").Value = If(er.Line < 200, "*" & er.Line.ToString("0000"), " " & (er.Line - 200).ToString("0000")) & "  : " & er.Column
          ir.Cells("col").Value = If(er.IsWarning, "w", "ERR") ' er.Column
        Next
        .igCompileErrors.SortObject.Clear()
        .igCompileErrors.SortObject.Add("col", iGSortOrder.Ascending)
        .igCompileErrors.SortObject.Add("line", iGSortOrder.Ascending)
        .igCompileErrors.Sort()

        .lblErrCount.Text = CStr(errCount)
        .lblWarnCount.Text = CStr(warnCount)
        .lblClassName.Text = className
        'MAIN.qq_txtOutMonitor.Text = out.ToString + vbNewLine + "----------------" + vbNewLine + sources(0)

        'onScriptError(mFileSpec, firstErrLine - 200, "Fehler beim Kompilieren - " & errCount & " Fehler, " & warnCount & " Warnungen" & vbNewLine & MAIN.qq_txtOutMonitor.Text)
        Beep()
        .igCompileErrors.SetCurRow(0)
        Dim firstLine As Integer = .igCompileErrors.CurRow.Tag(1)
        If firstLine >= 200 Then
          ScriptHost.Instance.OnScriptError(className, "", firstLine - 200, "CompileError", 0, "Fehler beim Kompilieren - " & errCount & " Fehler, " & warnCount & " Warnungen")
          ScriptHost.Instance.OnHighlightLineRequested(className, "", firstLine - 200, HighlightLineReason.CompileError)
        End If
      End With
      Return True
    Else
      If tbCompileErrors IsNot Nothing Then tbCompileErrors.Hide()
    End If
  End Function


  Sub processCompileException(ByVal ex As Exception)
    Dim firstErrLine = -1, errCount, warnCount As Integer
    ScriptHost.Instance.ErrorListVisible = True
    tbCompileErrors.clearList()
    Dim ir = tbCompileErrors.igCompileErrors.Rows.Add
    ir.Cells("typ").Value = ""
    ir.Cells("typ").ImageIndex = tbCompileErrors.iml_TraceTypes.Images.IndexOfKey("err")
    'ir.Cells(1).Value = er.ErrorNumber
    ir.Cells("desc").Value = ex.ToString
    'ir.Cells("file").Value = IO.Path.GetFileName(er.FileName)
    'ir.Cells("line").Value = If(er.Line < 200, "*" & er.Line, CStr(er.Line - 200))
    'ir.Cells("col").Value = er.Column
    tbCompileErrors.igCompileErrors.SetCurRow(0)
    Beep()
  End Sub
End Module
