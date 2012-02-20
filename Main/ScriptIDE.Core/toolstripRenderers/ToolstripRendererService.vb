Public Class ToolstripRendererService

  Public Shared Function GetRenderer() As ToolStripRenderer
    Return New ToolStripProfessionalRenderer()
    Return New Office2007Renderer.Office2007Renderer()
  End Function

End Class
