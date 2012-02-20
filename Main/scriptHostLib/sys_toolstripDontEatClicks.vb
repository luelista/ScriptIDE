Imports System.Windows.Forms

' hält den Toolstrip davon ab, bei nicht-focus die ClickEvents zu futtern

Public Class sys_toolstripDontEatClicks
  Inherits System.Windows.Forms.NativeWindow

  Public Event CallBackProc(ByRef m As Message)

  Private m_Subclassed As Boolean = False

  Public Sub New(ByVal handle As IntPtr)
    MyBase.AssignHandle(handle)
  End Sub

  Public Property SubClass() As Boolean
    Get
      Return m_Subclassed
    End Get
    Set(ByVal Value As Boolean)
      m_Subclassed = Value
    End Set
  End Property

  Protected Overrides Sub WndProc(ByRef m As Message)
    If m_Subclassed Then 'If Subclassing is Enabled 

      RaiseEvent CallBackProc(m) 'then RaiseEvent

    End If
    MyBase.WndProc(m)
  End Sub

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub
End Class


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
