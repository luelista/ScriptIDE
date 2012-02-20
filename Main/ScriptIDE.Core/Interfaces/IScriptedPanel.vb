
Public Enum insertDirection
  Horizontal = 1
  Vertical = 2
End Enum

Public Interface IScriptedPanel
  ' Inherits System.Windows.Forms.Panel

  Property className() As String
  Property winID() As String

  ReadOnly Property Form() As System.Windows.Forms.Form
  ReadOnly Property Window() As IScriptedWindow

  Property activateEvents() As String

  Property offsetX() As Integer
  Property insertX() As Integer
  Property insertY() As Integer

  Sub BR(Optional ByVal rowHeight As Integer = -1)

  Default ReadOnly Property Element(ByVal id)
  ReadOnly Property HasElement(ByVal id)

  Sub resetControls(Optional ByVal startX = 0, Optional ByVal startY = 0, Optional ByVal dir = 1)

  Function addLabel(ByVal strID, ByVal strText, Optional ByVal bgColor = "", Optional ByVal fgColor = "", Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1)
  Function addCheckbox(ByVal strID, ByVal strText, Optional ByVal bgColor = "", Optional ByVal fgColor = "", Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1)
  Function addButton(ByVal strID, ByVal btnText, Optional ByVal bgColor = "", Optional ByVal btnLeft = -1, Optional ByVal btnTop = -1, Optional ByVal btnWidth = -1, Optional ByVal btnHeight = -1, Optional ByVal iconUrl = "", Optional ByVal flags = 0, Optional ByVal handler = Nothing)
  <Obsolete("Use addButton instead")> Function addButtonEx(ByVal btnID, ByVal btnText, Optional ByVal bgColor = "", Optional ByVal btnLeft = -1, Optional ByVal btnTop = -1, Optional ByVal btnWidth = -1, Optional ByVal btnHeight = -1, Optional ByVal handler = Nothing)
  Function addMenu(ByVal strMenuID, ByVal strButtonID, ByVal strMouseButton, ByVal ParamArray menuItems())
  Function addIcon(ByVal strID, ByVal strURL, Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1)
  Sub addTextbox(ByVal ID, ByVal XX, Optional ByVal labelText = "", Optional ByVal labelXX = 0, Optional ByVal labelBgColor = "", Optional ByVal x = -1, Optional ByVal y = -1, Optional ByVal flags = "")
  Sub addControl(ByVal strID, ByVal strProgID, Optional ByVal intLeft = -1, Optional ByVal intTop = -1, Optional ByVal intWidth = -1, Optional ByVal intHeight = -1)

  Property direction() As Integer
  Sub setBackColor(ByVal col As String)
  Sub FinishMenu()

End Interface
