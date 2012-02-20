'******************************************************************

'  Office 2007 Renderer Project                                    

'                                                                  

'  Use the Office2007Renderer class as a custom renderer by        

'  providing it to the ToolStripManager.Renderer property. Then    

'  all tool strips, menu strips, status strips etc will be drawn   

'  using the Office 2007 style renderer in your application.       

'                                                                  

'   Author: Phil Wright                                            

'  Website: www.componentfactory.com                               

'  Contact: phil.wright@componentfactory.com                       

'******************************************************************


Imports System.Drawing
Imports System.Windows.Forms

Namespace Office2007Renderer
  ''' <summary>
  ''' Provide Office 2007 Blue Theme colors
  ''' </summary>
  Public Class Office2007ColorTable
    Inherits ProfessionalColorTable
#Region "Static Fixed Colors - Blue Color Scheme"
    Private Shared _contextMenuBack As Color = Color.FromArgb(250, 250, 250)
    Private Shared _buttonPressedBegin As Color = Color.FromArgb(248, 181, 106)
    Private Shared _buttonPressedEnd As Color = Color.FromArgb(255, 208, 134)
    Private Shared _buttonPressedMiddle As Color = Color.FromArgb(251, 140, 60)
    Private Shared _buttonSelectedBegin As Color = Color.FromArgb(255, 255, 222)
    Private Shared _buttonSelectedEnd As Color = Color.FromArgb(255, 203, 136)
    Private Shared _buttonSelectedMiddle As Color = Color.FromArgb(255, 225, 172)
    Private Shared _menuItemSelectedBegin As Color = Color.FromArgb(255, 213, 103)
    Private Shared _menuItemSelectedEnd As Color = Color.FromArgb(255, 228, 145)
    Private Shared _checkBack As Color = Color.FromArgb(255, 227, 149)
    Private Shared _gripDark As Color = Color.FromArgb(111, 157, 217)
    Private Shared _gripLight As Color = Color.FromArgb(255, 255, 255)
    Private Shared _imageMargin As Color = Color.FromArgb(233, 238, 238)
    Private Shared _menuBorder As Color = Color.FromArgb(134, 134, 134)
    Private Shared _overflowBegin As Color = Color.FromArgb(167, 204, 251)
    Private Shared _overflowEnd As Color = Color.FromArgb(101, 147, 207)
    Private Shared _overflowMiddle As Color = Color.FromArgb(167, 204, 251)
    Private Shared _menuToolBack As Color = Color.FromArgb(191, 219, 255)
    Private Shared _separatorDark As Color = Color.FromArgb(154, 198, 255)
    Private Shared _separatorLight As Color = Color.FromArgb(255, 255, 255)
    Private Shared _statusStripLight As Color = Color.FromArgb(215, 229, 247)
    Private Shared _statusStripDark As Color = Color.FromArgb(172, 201, 238)
    Private Shared _toolStripBorder As Color = Color.FromArgb(111, 157, 217)
    Private Shared _toolStripContentEnd As Color = Color.FromArgb(164, 195, 235)
    Private Shared _toolStripBegin As Color = Color.FromArgb(227, 239, 255)
    Private Shared _toolStripEnd As Color = Color.FromArgb(152, 186, 230)
    Private Shared _toolStripMiddle As Color = Color.FromArgb(222, 236, 255)
    Private Shared _buttonBorder As Color = Color.FromArgb(121, 153, 194)
#End Region

#Region "Identity"
    ''' <summary>
    ''' Initialize a new instance of the Office2007ColorTable class.
    ''' </summary>
    Public Sub New()
    End Sub
    Private Shared _instance As Office2007ColorTable
    Public Shared Function GetDefault() As Office2007ColorTable
      If _instance Is Nothing Then _instance = New Office2007ColorTable
      Return _instance
    End Function
#End Region

#Region "ButtonPressed"
    ''' <summary>
    ''' Gets the starting color of the gradient used when the button is pressed down.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ButtonPressedGradientBegin() As Color
      Get
        Return _buttonPressedBegin
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used when the button is pressed down.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ButtonPressedGradientEnd() As Color
      Get
        Return _buttonPressedEnd
      End Get
    End Property

    ''' <summary>
    ''' Gets the middle color of the gradient used when the button is pressed down.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ButtonPressedGradientMiddle() As Color
      Get
        Return _buttonPressedMiddle
      End Get
    End Property
#End Region

#Region "ButtonSelected"
    ''' <summary>
    ''' Gets the starting color of the gradient used when the button is selected.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ButtonSelectedGradientBegin() As Color
      Get
        Return _buttonSelectedBegin
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used when the button is selected.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ButtonSelectedGradientEnd() As Color
      Get
        Return _buttonSelectedEnd
      End Get
    End Property

    ''' <summary>
    ''' Gets the middle color of the gradient used when the button is selected.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ButtonSelectedGradientMiddle() As Color
      Get
        Return _buttonSelectedMiddle
      End Get
    End Property

    ''' <summary>
    ''' Gets the border color to use with ButtonSelectedHighlight.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ButtonSelectedHighlightBorder() As Color
      Get
        Return _buttonBorder
      End Get
    End Property
#End Region

#Region "Check"
    ''' <summary>
    ''' Gets the solid color to use when the check box is selected and gradients are being used.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property CheckBackground() As Color
      Get
        Return _checkBack
      End Get
    End Property
#End Region

#Region "Grip"
    ''' <summary>
    ''' Gets the color to use for shadow effects on the grip or move handle.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property GripDark() As Color
      Get
        Return _gripDark
      End Get
    End Property

    ''' <summary>
    ''' Gets the color to use for highlight effects on the grip or move handle.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property GripLight() As Color
      Get
        Return _gripLight
      End Get
    End Property
#End Region

#Region "ImageMargin"
    ''' <summary>
    ''' Gets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ImageMarginGradientBegin() As Color
      Get
        Return _imageMargin
      End Get
    End Property
#End Region

#Region "MenuBorder"
    ''' <summary>
    ''' Gets the border color or a MenuStrip.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property MenuBorder() As Color
      Get
        Return _menuBorder
      End Get
    End Property
#End Region

#Region "MenuItem"
    ''' <summary>
    ''' Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property MenuItemPressedGradientBegin() As Color
      Get
        Return _toolStripBegin
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property MenuItemPressedGradientEnd() As Color
      Get
        Return _toolStripEnd
      End Get
    End Property

    ''' <summary>
    ''' Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed down.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property MenuItemPressedGradientMiddle() As Color
      Get
        Return _toolStripMiddle
      End Get
    End Property

    ''' <summary>
    ''' Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property MenuItemSelectedGradientBegin() As Color
      Get
        Return _menuItemSelectedBegin
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used when the ToolStripMenuItem is selected.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property MenuItemSelectedGradientEnd() As Color
      Get
        Return _menuItemSelectedEnd
      End Get
    End Property
#End Region

#Region "MenuStrip"
    ''' <summary>
    ''' Gets the starting color of the gradient used in the MenuStrip.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property MenuStripGradientBegin() As Color
      Get
        Return _menuToolBack
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used in the MenuStrip.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property MenuStripGradientEnd() As Color
      Get
        Return _menuToolBack
      End Get
    End Property
#End Region

#Region "OverflowButton"
    ''' <summary>
    ''' Gets the starting color of the gradient used in the ToolStripOverflowButton.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property OverflowButtonGradientBegin() As Color
      Get
        Return _overflowBegin
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used in the ToolStripOverflowButton.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property OverflowButtonGradientEnd() As Color
      Get
        Return _overflowEnd
      End Get
    End Property

    ''' <summary>
    ''' Gets the middle color of the gradient used in the ToolStripOverflowButton.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property OverflowButtonGradientMiddle() As Color
      Get
        Return _overflowMiddle
      End Get
    End Property
#End Region

#Region "RaftingContainer"
    ''' <summary>
    ''' Gets the starting color of the gradient used in the ToolStripContainer.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property RaftingContainerGradientBegin() As Color
      Get
        Return _menuToolBack
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used in the ToolStripContainer.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property RaftingContainerGradientEnd() As Color
      Get
        Return _menuToolBack
      End Get
    End Property
#End Region

#Region "Separator"
    ''' <summary>
    ''' Gets the color to use to for shadow effects on the ToolStripSeparator.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property SeparatorDark() As Color
      Get
        Return _separatorDark
      End Get
    End Property

    ''' <summary>
    ''' Gets the color to use to for highlight effects on the ToolStripSeparator.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property SeparatorLight() As Color
      Get
        Return _separatorLight
      End Get
    End Property
#End Region

#Region "StatusStrip"
    ''' <summary>
    ''' Gets the starting color of the gradient used on the StatusStrip.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property StatusStripGradientBegin() As Color
      Get
        Return _statusStripLight
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used on the StatusStrip.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property StatusStripGradientEnd() As Color
      Get
        Return _statusStripDark
      End Get
    End Property
#End Region

#Region "ToolStrip"
    ''' <summary>
    ''' Gets the border color to use on the bottom edge of the ToolStrip.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ToolStripBorder() As Color
      Get
        Return _toolStripBorder
      End Get
    End Property

    ''' <summary>
    ''' Gets the starting color of the gradient used in the ToolStripContentPanel.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ToolStripContentPanelGradientBegin() As Color
      Get
        Return _toolStripContentEnd
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used in the ToolStripContentPanel.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ToolStripContentPanelGradientEnd() As Color
      Get
        Return _menuToolBack
      End Get
    End Property

    ''' <summary>
    ''' Gets the solid background color of the ToolStripDropDown.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ToolStripDropDownBackground() As Color
      Get
        Return _contextMenuBack
      End Get
    End Property

    ''' <summary>
    ''' Gets the starting color of the gradient used in the ToolStrip background.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ToolStripGradientBegin() As Color
      Get
        Return _toolStripBegin
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used in the ToolStrip background.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ToolStripGradientEnd() As Color
      Get
        Return _toolStripEnd
      End Get
    End Property

    ''' <summary>
    ''' Gets the middle color of the gradient used in the ToolStrip background.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ToolStripGradientMiddle() As Color
      Get
        Return _toolStripMiddle
      End Get
    End Property

    ''' <summary>
    ''' Gets the starting color of the gradient used in the ToolStripPanel.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ToolStripPanelGradientBegin() As Color
      Get
        Return _menuToolBack
      End Get
    End Property

    ''' <summary>
    ''' Gets the end color of the gradient used in the ToolStripPanel.
    ''' </summary>
    Public Overloads Overrides ReadOnly Property ToolStripPanelGradientEnd() As Color
      Get
        Return _menuToolBack
      End Get
    End Property
#End Region
  End Class
End Namespace
