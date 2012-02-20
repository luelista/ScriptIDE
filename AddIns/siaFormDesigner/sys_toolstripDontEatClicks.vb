Imports System.Windows.Forms

' hält den Toolstrip davon ab, bei nicht-focus die ClickEvents zu futtern

Public Class ToolStrip_DontEatClickEvent
  Inherits System.Windows.Forms.NativeWindow



  Public Const WM_MOUSEACTIVATE = &H21
  Public Const MA_ACTIVATEANDEAT = 2
  Public Const MA_ACTIVATE = 1
  Public Event CallBackProc(ByRef m As Message)

  Public Sub New(ByVal tsp As ToolStrip)
    MyBase.AssignHandle(tsp.Handle)
  End Sub


  Protected Overrides Sub WndProc(ByRef m As Message)
    MyBase.WndProc(m) 

    If m.Msg = WM_MOUSEACTIVATE And m.Result = MA_ACTIVATEANDEAT Then
      m.Result = MA_ACTIVATE

    End If
  End Sub

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub
End Class
