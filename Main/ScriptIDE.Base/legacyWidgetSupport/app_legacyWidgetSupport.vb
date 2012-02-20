Imports System.Reflection

Module app_legacyWidgetSupport

  Sub loadLegacyWidget(ByVal fromFile As String)


  End Sub

  Sub loadUserctrlIntoWidget(ByVal expBar As frmTB_legacyWidget)
    '... die sub bräuchte einen Parameter, ob neuladen oder lazyLaden angesagt ist

    With expBar
      .Refresh() '!!! damit der container schon mal offen ist 
      Application.DoEvents() ' !!! geht evt auch ohne - nicht getestet
      .SuspendLayout()
      Dim asm As Assembly
      Try
        asm = Assembly.LoadFrom(.txtWidgetfilename.Text)
        Dim typ = asm.GetType(.txtClass.Text, True, True)
        Dim x = Activator.CreateInstance(typ)
        '.isLazy = False

        x.name = "sw_userctrl"
        .Panel1.Controls.Add(x)
        '!!! der muß rein, um den timer vom resize zu überbrücken (muß dazu 'public' sein)
        x.dock = DockStyle.Fill
        '.ExpandOnTitleAction = mwExplorerBar.mwExplorerBar.titleAction.LeftClick
        x.SetExplorerBarProps(expBar)
        x.BackColor = Color.DimGray
        ' setExpBarColor(expBar)
        .ResumeLayout()
        Application.DoEvents()

        Try : x.onStart("") : Catch : End Try '.txtPara.Text

        Try : Dim hlp = New cls_widgetHelper("", .Name, expBar, x) '.txtPara.Text
          x.onIniDone(hlp) : Catch : End Try

        'Dim iconFile = "C:\yPara\mwSidebar\Icons\" + Path.GetFileNameWithoutExtension(.txtWidgetfilename.Text) + "_" + .txtClass.Text + ".png"
        'Try : expBar.Icon.Save(iconFile, Imaging.ImageFormat.Png) : Catch : End Try


      Catch ex As Exception
        MsgBox("Fehler beim Laden des Widgets " + IO.Path.GetFileName(.txtWidgetfilename.Text) + " / " + .txtClass.Text + ":" + vbNewLine + ex.ToString, MsgBoxStyle.Exclamation)
      End Try

    End With
  End Sub

End Module
