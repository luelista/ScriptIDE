Public Interface IScriptedWindow

  Property className() As String
  Property winID() As String
  
  Function getFileTag() As String
   
  Function getViewFilename() As String
 
  Function GetPersistString() As String
 
  Sub setVisible(ByVal stat As Boolean)

  ReadOnly Property PNL() As IScriptedPanel


End Interface
