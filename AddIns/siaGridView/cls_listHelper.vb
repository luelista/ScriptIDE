Imports System.Text
Imports TenTec.Windows.iGridLib

<Microsoft.VisualBasic.ComClass()> Public Class cls_listHelper

  Dim sb As StringBuilder

  Dim m_igRef As iGrid
  Property Grid() As iGrid
    Get
      Return m_igRef
    End Get
    Set(ByVal value As iGrid)
      m_igRef = value
    End Set
  End Property


  Sub iniPicUpLine()
    sb = New StringBuilder

  End Sub

  Sub picUpTab(ByVal tabText As String)
    sb.Append(tabText + vbTab)
  End Sub
  Sub picUpTabComplete()
    sb.AppendLine()
  End Sub

  Sub picUpLine(ByVal lineText As String)
    sb.AppendLine(lineText)
  End Sub

  Function getOutData() As String
    Return sb.ToString
  End Function


  Property Col(ByVal index As Integer) As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(index).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(index).Value = value
    End Set
  End Property


#Region "LetterCols AAA-PPP"
  Property aaa() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(0).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(0).Value = value
    End Set
  End Property

  Property bbb() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(1).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(1).Value = value
    End Set
  End Property

  Property ccc() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(2).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(2).Value = value
    End Set
  End Property

  Property ddd() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(3).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(3).Value = value
    End Set
  End Property

  Property eee() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(4).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(4).Value = value
    End Set
  End Property

  Property fff() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(5).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(5).Value = value
    End Set
  End Property

  Property ggg() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(6).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(6).Value = value
    End Set
  End Property

  Property hhh() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(7).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(7).Value = value
    End Set
  End Property

  Property iii() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(8).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(8).Value = value
    End Set
  End Property

  Property jjj() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(9).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(9).Value = value
    End Set
  End Property

  Property kkk() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(10).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(10).Value = value
    End Set
  End Property

  Property lll() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(11).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(11).Value = value
    End Set
  End Property

  Property mmm() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(12).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(12).Value = value
    End Set
  End Property

  Property nnn() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(13).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(13).Value = value
    End Set
  End Property

  Property ooo() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(14).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(14).Value = value
    End Set
  End Property

  Property ppp() As String
    Get
      On Error Resume Next
      Return m_igRef.CurRow.Cells(15).Value
    End Get
    Set(ByVal value As String)
      On Error Resume Next
      m_igRef.CurRow.Cells(15).Value = value
    End Set
  End Property
#End Region

End Class
