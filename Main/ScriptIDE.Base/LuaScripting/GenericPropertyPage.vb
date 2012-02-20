Public Class GenericPropertyPage
  Inherits UserControl
  Implements IPropertyPage

  Public Event OnReadProperties()
  Public Event OnSaveProperties()

  Public Sub readProperties() Implements Core.IPropertyPage.readProperties
    RaiseEvent OnReadProperties()
  End Sub

  Public Sub saveProperties() Implements Core.IPropertyPage.saveProperties
    RaiseEvent OnSaveProperties()
  End Sub
End Class
