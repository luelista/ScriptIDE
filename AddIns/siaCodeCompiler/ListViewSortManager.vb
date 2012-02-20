'''//////////////////////////////////////////////////////////////////////////
' ListViewSortManager.cs | Provides sortColumn sorting for the ListView control
'
' Copyright (c) 2002 by Eddie Velasquez
'
' Date:	Monday, April 22, 2002
' Autor:	Eddie Velasquez
'
'''////////////

Imports System
Imports System.Collections
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Globalization
Imports System.Runtime.InteropServices

Namespace EV.Windows.Forms
#Region "Comparers"

  ''' <summary>
  ''' Provides text sorting (case sensitive)
  ''' </summary>
  Public Class ListViewTextSort
    Implements IComparer
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="sortColumn">Column to be sorted</param>
    ''' <param name="ascending">true, if ascending order, false otherwise</param>
    Public Sub New(ByVal sortColumn As Int32, ByVal ascending As [Boolean])
      m_column = sortColumn
      m_ascending = ascending
    End Sub

    ''' <summary>
    ''' Implementation of IComparer.Compare
    ''' </summary>
    ''' <param name="lhs">First Object to compare</param>
    ''' <param name="rhs">Second Object to compare</param>
    ''' <returns>Less that zero if lhs is less than rhs. Greater than zero if lhs greater that rhs. Zero if they are equal</returns>
    Public Function Compare(ByVal lhs As [Object], ByVal rhs As [Object]) As Int32 Implements IComparer.Compare
      Dim lhsLvi As ListViewItem = TryCast(lhs, ListViewItem)
      Dim rhsLvi As ListViewItem = TryCast(rhs, ListViewItem)

      If lhsLvi Is Nothing OrElse rhsLvi Is Nothing Then
        ' We only know how to sort ListViewItems, so return equal
        Return 0
      End If

      Dim lhsItems As ListViewItem.ListViewSubItemCollection = lhsLvi.SubItems
      Dim rhsItems As ListViewItem.ListViewSubItemCollection = rhsLvi.SubItems

      Dim lhsText As [String] = If((lhsItems.Count > m_column), lhsItems(m_column).Text, [String].Empty)
      Dim rhsText As [String] = If((rhsItems.Count > m_column), rhsItems(m_column).Text, [String].Empty)

      Dim result As Int32 = 0
      If lhsText.Length = 0 OrElse rhsText.Length = 0 Then
        result = lhsText.CompareTo(rhsText)
      Else

        result = OnCompare(lhsText, rhsText)
      End If

      If Not m_ascending Then
        result = -result
      End If

      Return result
    End Function

    ''' <summary>
    ''' Overridden to do type-specific comparision.
    ''' </summary>
    ''' <param name="lhs">First Object to compare</param>
    ''' <param name="rhs">Second Object to compare</param>
    ''' <returns>Less that zero if lhs is less than rhs. Greater than zero if lhs greater that rhs. Zero if they are equal</returns>
    Protected Overridable Function OnCompare(ByVal lhs As [String], ByVal rhs As [String]) As Int32
      Return [String].Compare(lhs, rhs, False)
    End Function

    Private m_column As Int32
    Private m_ascending As [Boolean]
  End Class

  ''' <summary>
  ''' Provides text sorting (case insensitive)
  ''' </summary>
  Public Class ListViewTextCaseInsensitiveSort
    Inherits ListViewTextSort
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="sortColumn">Column to be sorted</param>
    ''' <param name="ascending">true, if ascending order, false otherwise</param>
    Public Sub New(ByVal sortColumn As Int32, ByVal ascending As [Boolean])
      MyBase.New(sortColumn, ascending)
    End Sub

    ''' <summary>
    ''' Case-insensitive compare
    ''' </summary>
    Protected Overloads Overrides Function OnCompare(ByVal lhs As [String], ByVal rhs As [String]) As Int32
      Return [String].Compare(lhs, rhs, True)
    End Function
  End Class

  ''' <summary>
  ''' Provides date sorting
  ''' </summary>
  Public Class ListViewDateSort
    Inherits ListViewTextSort
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="sortColumn">Column to be sorted</param>
    ''' <param name="ascending">true, if ascending order, false otherwise</param>
    Public Sub New(ByVal sortColumn As Int32, ByVal ascending As [Boolean])
      MyBase.New(sortColumn, ascending)
    End Sub

    ''' <summary>
    ''' Date compare
    ''' </summary>
    Protected Overloads Overrides Function OnCompare(ByVal lhs As [String], ByVal rhs As [String]) As Int32
      Return DateTime.Parse(lhs).CompareTo(DateTime.Parse(rhs))
    End Function
  End Class

  ''' <summary>
  ''' Provides integer (32 bits) sorting
  ''' </summary>
  Public Class ListViewInt32Sort
    Inherits ListViewTextSort
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="sortColumn">Column to be sorted</param>
    ''' <param name="ascending">true, if ascending order, false otherwise</param>
    Public Sub New(ByVal sortColumn As Int32, ByVal ascending As [Boolean])
      MyBase.New(sortColumn, ascending)
    End Sub

    ''' <summary>
    ''' Integer compare
    ''' </summary>
    Protected Overloads Overrides Function OnCompare(ByVal lhs As [String], ByVal rhs As [String]) As Int32
      Return Int32.Parse(lhs, NumberStyles.Number) - Int32.Parse(rhs, NumberStyles.Number)
    End Function
  End Class

  ''' <summary>
  ''' Provides integer (64 bits) sorting
  ''' </summary>
  Public Class ListViewInt64Sort
    Inherits ListViewTextSort
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="sortColumn">Column to be sorted</param>
    ''' <param name="ascending">true, if ascending order, false otherwise</param>
    Public Sub New(ByVal sortColumn As Int32, ByVal ascending As [Boolean])
      MyBase.New(sortColumn, ascending)
    End Sub

    ''' <summary>
    ''' Integer compare
    ''' </summary>
    Protected Overloads Overrides Function OnCompare(ByVal lhs As [String], ByVal rhs As [String]) As Int32
      Return (Int64.Parse(lhs, NumberStyles.Number) - Int64.Parse(rhs, NumberStyles.Number))
    End Function
  End Class

  ''' <summary>
  ''' Provides floating-point sorting
  ''' </summary>
  Public Class ListViewDoubleSort
    Inherits ListViewTextSort
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="sortColumn">Column to be sorted</param>
    ''' <param name="ascending">true, if ascending order, false otherwise</param>
    Public Sub New(ByVal sortColumn As Int32, ByVal ascending As [Boolean])
      MyBase.New(sortColumn, ascending)
    End Sub

    ''' <summary>
    ''' Floating-point compare
    ''' </summary>
    Protected Overloads Overrides Function OnCompare(ByVal lhs As [String], ByVal rhs As [String]) As Int32
      Dim result As [Double] = [Double].Parse(lhs) - [Double].Parse(rhs)

      If result > 0 Then
        Return 1

      ElseIf result < 0 Then
        Return -1
      Else

        Return 0
      End If
    End Function
  End Class

#End Region
  '
  '#Region "ListViewSortManager"

  ''' <summary>
  ''' Provides sorting of ListView columns 
  ''' </summary>
  Public Class ListViewSortManager
    Private Shared s_useNativeArrows As Boolean = ComCtlDllSupportsArrows()

    ''' <summary>
    ''' Creates the ListView Sort Manager
    ''' </summary>
    ''' <param name="list">ListView that this manager will provide sorting to</param>
    ''' <param name="comparers">Array of Types of comparers (One for each column)</param>
    ''' <param name="column">Initial column to sort</param>
    ''' <param name="order">Initial sort order</param>
    Public Sub New(ByVal list As ListView, ByVal comparers As Type(), ByVal column As Int32, ByVal order As SortOrder)
      m_column = -1
      m_sortOrder = SortOrder.None

      m_list = list
      m_comparers = comparers

      If Not s_useNativeArrows Then
        m_imgList = New ImageList()
        m_imgList.ImageSize = New Size(8, 8)
        m_imgList.TransparentColor = System.Drawing.Color.Magenta

        m_imgList.Images.Add(GetArrowBitmap(ArrowType.Ascending))
        ' Add ascending arrow
        m_imgList.Images.Add(GetArrowBitmap(ArrowType.Descending))
        ' Add descending arrow
        SetHeaderImageList(m_list, m_imgList)
      End If

      AddHandler list.ColumnClick, AddressOf ColumnClick

      If column <> -1 Then
        Sort(column, order)
      End If
    End Sub

    ''' <summary>
    ''' Creates the ListView Sort Manager
    ''' </summary>
    ''' <param name="list">ListView that this manager will provide sorting to</param>
    ''' <param name="comparers">Array of Types of comparers (One for each column)</param>
    Public Sub New(ByVal list As ListView, ByVal comparers As Type())
      Me.New(list, comparers, -1, SortOrder.None)
    End Sub

    ''' <summary>
    ''' Returns the current sort column
    ''' </summary>
    Public ReadOnly Property Column() As Int32
      Get
        Return m_column
      End Get
    End Property

    ''' <summary>
    ''' Returns the current sort order
    ''' </summary>
    Public ReadOnly Property SortOrder() As SortOrder
      Get
        Return m_sortOrder
      End Get
    End Property

    ''' <summary>
    ''' Returns the type of the comparer for the given column
    ''' </summary>
    ''' <param name="column">Column index</param>
    ''' <returns></returns>
    Public Function GetColumnComparerType(ByVal column As Int32) As Type
      Return m_comparers(column)
    End Function

    ''' <summary>
    ''' Sets the type of the comparer for the given column
    ''' </summary>
    ''' <param name="column">Column index</param>
    ''' <param name="comparerType">Comparer type</param>
    Public Sub SetColumnComparerType(ByVal column As Int32, ByVal comparerType As Type)
      m_comparers(column) = comparerType
    End Sub

    ''' <summary>
    ''' Reassigns the comparer types for all the columns
    ''' </summary>
    ''' <param name="comparers">Array of Types of comparers (One for each column)</param>
    Public Sub SetComparerTypes(ByVal comparers As Type())
      m_comparers = comparers
    End Sub

    ''' <summary>
    ''' Sorts the rows based on the given column and the current sort order
    ''' </summary>
    ''' <param name="sortColumn">Column to be sorted</param>
    Public Sub Sort(ByVal column As Int32)
      Dim order As SortOrder = SortOrder.Ascending

      If column = m_column Then
        order = If((m_sortOrder = SortOrder.Ascending), SortOrder.Descending, SortOrder.Ascending)
      End If

      Sort(column, order)
    End Sub

    ''' <summary>
    ''' Sorts the rows based on the given column and sort order
    ''' </summary>
    ''' <param name="column">Column to be sorted</param>
    ''' <param name="order">Sort order</param>
    Public Sub Sort(ByVal column As Int32, ByVal order As SortOrder)
      If column < 0 OrElse column >= m_comparers.Length Then
        Throw New IndexOutOfRangeException()
      End If

      If column <> m_column Then
        ShowHeaderIcon(m_list, m_column, SortOrder.None)
        m_column = column
      End If

      ShowHeaderIcon(m_list, m_column, order)
      m_sortOrder = order

      If m_sortOrder <> SortOrder.None Then
        Dim comp As ListViewTextSort = DirectCast(Activator.CreateInstance(m_comparers(m_column), New [Object]() {m_column, m_sortOrder = SortOrder.Ascending}), ListViewTextSort)
        m_list.ListViewItemSorter = comp
      Else

        m_list.ListViewItemSorter = Nothing
      End If
    End Sub

    ''' <summary>
    ''' Enables/Disables list sorting
    ''' </summary>
    Public Property SortEnabled() As [Boolean]
      Get
        Return m_list.ListViewItemSorter IsNot Nothing
      End Get

      Set(ByVal value As [Boolean])
        If value Then
          If Not Me.SortEnabled Then
            AddHandler m_list.ColumnClick, AddressOf ColumnClick
            m_list.ListViewItemSorter = DirectCast(Activator.CreateInstance(m_comparers(m_column), New [Object]() {m_column, m_sortOrder = SortOrder.Ascending}), ListViewTextSort)
            ShowHeaderIcon(m_list, m_column, m_sortOrder)
          End If
        Else

          If Me.SortEnabled Then
            RemoveHandler m_list.ColumnClick, AddressOf ColumnClick
            m_list.ListViewItemSorter = Nothing
            ShowHeaderIcon(m_list, m_column, SortOrder.None)
          End If
        End If
      End Set
    End Property

    ''' <summary>
    ''' ColumnClick event handler
    ''' </summary>
    ''' <param name="sender">Event sender</param>
    ''' <param name="e">Event arguments</param>
    Private Sub ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs)
      Me.Sort(e.Column)
    End Sub

    Private m_column As Int32
    Private m_sortOrder As SortOrder
    Private m_list As ListView
    Private m_comparers As Type()
    Private m_imgList As ImageList

#Region "Graphics"

    Private Enum ArrowType
      Ascending
      Descending
    End Enum

    Private Function GetArrowBitmap(ByVal type As ArrowType) As Bitmap
      Dim bmp As New Bitmap(8, 8)
      Dim gfx As Graphics = Graphics.FromImage(bmp)

      Dim lightPen As Pen = SystemPens.ControlLightLight
      Dim shadowPen As Pen = SystemPens.ControlDark

      gfx.FillRectangle(System.Drawing.Brushes.Magenta, 0, 0, 8, 8)

      If type = ArrowType.Ascending Then
        gfx.DrawLine(lightPen, 0, 7, 7, 7)
        gfx.DrawLine(lightPen, 7, 7, 4, 0)
        gfx.DrawLine(shadowPen, 3, 0, 0, 7)

      ElseIf type = ArrowType.Descending Then
        gfx.DrawLine(lightPen, 4, 7, 7, 0)
        gfx.DrawLine(shadowPen, 3, 7, 0, 0)
        gfx.DrawLine(shadowPen, 0, 0, 7, 0)
      End If

      gfx.Dispose()

      Return bmp
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure HDITEM
      Public mask As Int32
      Public cxy As Int32
      <MarshalAs(UnmanagedType.LPTStr)> _
      Public pszText As [String]
      Public hbm As IntPtr
      Public cchTextMax As Int32
      Public fmt As Int32
      Public lParam As Int32
      Public iImage As Int32
      Public iOrder As Int32
    End Structure

    <DllImport("user32")> _
    Private Shared Function SendMessage(ByVal Handle As IntPtr, ByVal msg As Int32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32", EntryPoint:="SendMessage")> _
    Private Shared Function SendMessage2(ByVal Handle As IntPtr, ByVal msg As Int32, ByVal wParam As IntPtr, ByRef lParam As HDITEM) As IntPtr
    End Function

    Const HDI_WIDTH As Int32 = &H1
    Const HDI_HEIGHT As Int32 = HDI_WIDTH
    Const HDI_TEXT As Int32 = &H2
    Const HDI_FORMAT As Int32 = &H4
    Const HDI_LPARAM As Int32 = &H8
    Const HDI_BITMAP As Int32 = &H10
    Const HDI_IMAGE As Int32 = &H20
    Const HDI_DI_SETITEM As Int32 = &H40
    Const HDI_ORDER As Int32 = &H80
    Const HDI_FILTER As Int32 = &H100
    ' 0x0500
    Const HDF_LEFT As Int32 = &H0
    Const HDF_RIGHT As Int32 = &H1
    Const HDF_CENTER As Int32 = &H2
    Const HDF_JUSTIFYMASK As Int32 = &H3
    Const HDF_RTLREADING As Int32 = &H4
    Const HDF_OWNERDRAW As Int32 = &H8000
    Const HDF_STRING As Int32 = &H4000
    Const HDF_BITMAP As Int32 = &H2000
    Const HDF_BITMAP_ON_RIGHT As Int32 = &H1000
    Const HDF_IMAGE As Int32 = &H800
    Const HDF_SORTUP As Int32 = &H400
    ' 0x0501
    Const HDF_SORTDOWN As Int32 = &H200
    ' 0x0501
    Const LVM_FIRST As Int32 = &H1000
    ' List messages
    Const LVM_GETHEADER As Int32 = LVM_FIRST + 31

    Const HDM_FIRST As Int32 = &H1200
    ' Header messages
    Const HDM_SETIMAGELIST As Int32 = HDM_FIRST + 8
    Const HDM_GETIMAGELIST As Int32 = HDM_FIRST + 9
    Const HDM_GETITEM As Int32 = HDM_FIRST + 11
    Const HDM_SETITEM As Int32 = HDM_FIRST + 12

    Private Sub ShowHeaderIcon(ByVal list As ListView, ByVal columnIndex As Integer, ByVal sortOrder__1 As SortOrder)
      If columnIndex < 0 OrElse columnIndex >= list.Columns.Count Then
        Exit Sub
      End If

      Dim hHeader As IntPtr = SendMessage(list.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero)

      Dim colHdr As ColumnHeader = list.Columns(columnIndex)

      Dim hd As New HDITEM()
      hd.mask = HDI_FORMAT

      Dim align As HorizontalAlignment = colHdr.TextAlign

      If align = HorizontalAlignment.Left Then
        hd.fmt = HDF_LEFT Or HDF_STRING Or HDF_BITMAP_ON_RIGHT

      ElseIf align = HorizontalAlignment.Center Then
        hd.fmt = HDF_CENTER Or HDF_STRING Or HDF_BITMAP_ON_RIGHT
      Else

        ' HorizontalAlignment.Right
        hd.fmt = HDF_RIGHT Or HDF_STRING
      End If

      If s_useNativeArrows Then
        If sortOrder__1 = SortOrder.Ascending Then
          hd.fmt = hd.fmt Or HDF_SORTUP

        ElseIf sortOrder__1 = SortOrder.Descending Then
          hd.fmt = hd.fmt Or HDF_SORTDOWN
        End If
      Else
        hd.mask = hd.mask Or HDI_IMAGE

        If sortOrder__1 <> SortOrder.None Then
          hd.fmt = hd.fmt Or HDF_IMAGE
        End If

        hd.iImage = CInt(sortOrder__1) - 1
      End If

      SendMessage2(hHeader, HDM_SETITEM, New IntPtr(columnIndex), hd)
    End Sub

    Private Sub SetHeaderImageList(ByVal list As ListView, ByVal imgList As ImageList)
      Dim hHeader As IntPtr = SendMessage(list.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero)
      SendMessage(hHeader, HDM_SETIMAGELIST, IntPtr.Zero, imgList.Handle)
    End Sub
#End Region

#Region "ComCtrl information"

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure DLLVERSIONINFO
      Public cbSize As Integer
      Public dwMajorVersion As Integer
      Public dwMinorVersion As Integer
      Public dwBuildNumber As Integer
      Public dwPlatformID As Integer
    End Structure

    <DllImport("kernel32.dll")> _
    Private Shared Function LoadLibrary(ByVal fileName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Ansi, ExactSpelling:=True)> _
    Public Shared Function GetProcAddress(ByVal hModule As IntPtr, ByVal procName As String) As UIntPtr
    End Function

    <DllImport("kernel32.dll")> _
    Private Shared Function FreeLibrary(ByVal hModule As IntPtr) As Boolean
    End Function

    <DllImport("comctl32.dll")> _
    Private Shared Function DllGetVersion(ByRef pdvi As DLLVERSIONINFO) As Integer
    End Function

    Private Shared Function ComCtlDllSupportsArrows() As Boolean
      Dim hModule As IntPtr = IntPtr.Zero

      Try
        hModule = LoadLibrary("comctl32.dll")
        If hModule <> IntPtr.Zero Then
          Dim proc As UIntPtr = GetProcAddress(hModule, "DllGetVersion")
          If proc = UIntPtr.Zero Then
            ' Old versions don't support this method
            Return False
          End If
        End If

        Dim vi As New DLLVERSIONINFO()
        vi.cbSize = Marshal.SizeOf(GetType(DLLVERSIONINFO))

        DllGetVersion(vi)

        Return vi.dwMajorVersion >= 6
      Finally
        If hModule <> IntPtr.Zero Then
          FreeLibrary(hModule)
        End If
      End Try
    End Function

#End Region

  End Class
End Namespace
