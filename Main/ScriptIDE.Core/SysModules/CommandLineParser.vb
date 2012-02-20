Public Class CommandLineParser

  Public NamedArgs As New Dictionary(Of String, String)
  Public FreeArgs As New List(Of String)


  Public Overrides Function ToString() As String
    Dim out As New System.Text.StringBuilder
    out.AppendLine("NamedArgs:")
    For Each kvp As KeyValuePair(Of String, String) In NamedArgs
      out.AppendLine("[" + kvp.Key + "]=" + kvp.Value + "<<<")
    Next
    out.AppendLine()
    out.AppendLine()
    out.AppendLine("FreeArgs:")
    out.AppendLine(">>>" + Join(FreeArgs.ToArray, "<<<" + vbNewLine + ">>>") + "<<<")
    out.AppendLine()

    Return out.ToString()
  End Function

  Public Const mode_nN = 1
  Public Const mode_nC = 2
  Public Const mode_DEF = 3


  Sub New(ByVal cmdLine As String)
    cmdLine += " "
    Dim inApo As Boolean, doubleApo As Boolean
    Dim argName As String = "", argCont As String = ""
    Dim mode As Integer = mode_DEF
    For i As Integer = 0 To cmdLine.Length - 1
      'If Not isEscaped AndAlso cmdLine(i) = "\"c Then isEscaped = True : Continue For
      If cmdLine(i) = """"c Then
        inApo = Not inApo : If Not doubleApo Then doubleApo = True : Continue For
      End If
      doubleApo = False

      If Not inApo AndAlso cmdLine(i) = "/"c Then argName = "" : mode = mode_nN : Continue For


      If mode = mode_nN Then
        'trace("nN", cmdLine(i))
        If cmdLine(i) = "="c Or cmdLine(i) = ":"c Then mode = mode_nC : argCont = "" : Continue For
        If Not inApo AndAlso cmdLine(i) = " "c Then
          mode = mode_DEF : NamedArgs.Add(LCase(argName), "")
          Continue For
        End If
        argName += cmdLine(i)
      End If

      If mode = mode_nC Then
        If Not inApo AndAlso cmdLine(i) = " "c Then
          NamedArgs.Add(LCase(argName), argCont) : argName = "" : argCont = "" : mode = mode_DEF
          Continue For
        End If

        argCont += cmdLine(i)
      End If

      If mode = mode_DEF Then
        If Not inApo AndAlso cmdLine(i) = " "c Then
          FreeArgs.Add(argCont) : argCont = ""
          Continue For
        End If

        argCont += cmdLine(i)
      End If

    Next


  End Sub


End Class