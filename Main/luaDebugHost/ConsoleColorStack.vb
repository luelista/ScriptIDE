Public Class ConsoleColorStack
  Private Shared m_stack As New Stack(Of ConsoleColorSet)

  Private Sub New()
  End Sub

  Private Structure ConsoleColorSet
    Dim bg, fg As ConsoleColor
  End Structure

  ''' <summary>
  ''' Stores the current ConsoleColor set on a stack and assigns the given
  ''' Colors to the console.
  ''' </summary>
  ''' <param name="backGround">New background color</param>
  ''' <param name="foreGround">New foreground color</param>
  ''' <remarks></remarks>
  Public Shared Sub Push(ByVal backGround As ConsoleColor, ByVal foreGround As ConsoleColor)
    Dim c As New ConsoleColorSet
    c.bg = Console.BackgroundColor : c.fg = Console.ForegroundColor
    m_stack.Push(c)
    Console.BackgroundColor = backGround : Console.ForegroundColor = foreGround
  End Sub

  ''' <summary>
  ''' Restores the last ConsoleColor set from the stack.
  ''' </summary>
  ''' <remarks></remarks>
  Public Shared Sub Pop()
    Dim c As ConsoleColorSet = m_stack.Pop()
    Console.BackgroundColor = c.bg : Console.ForegroundColor = c.fg
  End Sub

End Class
