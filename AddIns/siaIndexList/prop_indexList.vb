Public Class prop_indexList
  Implements IPropertyPage


  Public Sub readProperties() Implements Core.IPropertyPage.readProperties

  End Sub

  Public Sub saveProperties() Implements Core.IPropertyPage.saveProperties

  End Sub

  Private Sub prop_indexList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ListView1.Items.Clear()
    Dim test As New idx_test01
    ListView1.SmallImageList = test.ImageList1
    For i = 0 To test.ImageList1.Images.Count - 1
      Dim key = test.ImageList1.Images.Keys(i)
      ListView1.Items.Add(key, key)
    Next
  End Sub
End Class
