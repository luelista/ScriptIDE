


Public Class BreakMode
  Public Const Break = "BREAK"
  Public Const Run = ""
  Public Const SingleStep = "EinzelSchritt"

End Class

Public Enum NavigationKind
  InterprocCommand
  InterprocDataRequest
  CommandLine
  Script
  ToolbarCommand
  FileCommand
  RequestCommandHelp
  Area
End Enum

<Flags()> Public Enum ButtonFlags
  None = 0
  StartMenu = 1
  EndMenu = 2
  IsSplitButton = 4
End Enum

Public Enum ToolBarStyle
  Panel
  ToolStrip
End Enum



<Flags()> Public Enum DWndFlags
  DockWindow = 0
  StdWindow = 1
  NoAutoShow = 2
  DisableCallback = 4


End Enum