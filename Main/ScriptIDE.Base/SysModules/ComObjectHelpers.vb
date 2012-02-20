
Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes
Imports System.Text
Imports System.IO
Imports System.Reflection
Imports System.Reflection.Emit

' IDispatch.cs 


Namespace SolidEdgeSpy.InteropServices
  <Guid("00020400-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IDispatch
    Function GetTypeInfoCount() As Integer
    Function GetTypeInfo(<MarshalAs(UnmanagedType.U4)> ByVal iTInfo As Integer, <MarshalAs(UnmanagedType.U4)> ByVal lcid As Integer) As System.Runtime.InteropServices.ComTypes.ITypeInfo
    <PreserveSig()> _
    Function GetIDsOfNames(ByRef riid As Guid, <MarshalAs(UnmanagedType.LPArray, ArraySubType:=UnmanagedType.LPWStr)> ByVal rgsNames As String(), ByVal cNames As Integer, ByVal lcid As Integer, <MarshalAs(UnmanagedType.LPArray)> ByVal rgDispId As Integer()) As Integer
    <PreserveSig()> _
    Function Invoke(ByVal dispIdMember As Integer, ByRef riid As Guid, <MarshalAs(UnmanagedType.U4)> ByVal lcid As Integer, <MarshalAs(UnmanagedType.U4)> ByVal dwFlags As Integer, ByRef pDispParams As System.Runtime.InteropServices.ComTypes.DISPPARAMS, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pVarResult As Object(), _
     ByRef pExcepInfo As System.Runtime.InteropServices.ComTypes.EXCEPINFO, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pArgErr As IntPtr()) As Integer
  End Interface
End Namespace

' ComObject.cs 


' ComObject.cs 


Namespace SolidEdgeSpy.InteropServices
  Public Class ComObject
    Implements IDisposable
    Private _object As Object
    Private _dispatch As IDispatch
    Private _pTypeAttr As IntPtr = IntPtr.Zero
    Private _typeInfo As System.Runtime.InteropServices.ComTypes.ITypeInfo
    Private _typeName As String, _typeDescription As String, _typeHelpFile As String
    Private _typeHelpContext As Integer
    Private _comTypeLibrary As SolidEdgeSpy.InteropServices.ComTypeLibrary

    Public Sub New(ByVal comObject As Object)
      Dim ppTLB As System.Runtime.InteropServices.ComTypes.ITypeLib = Nothing
      Dim pIndex As Integer = 0

      _dispatch = TryCast(comObject, IDispatch)

      If _dispatch IsNot Nothing Then
        _object = comObject
        _typeInfo = _dispatch.GetTypeInfo(0, 0)
        _typeInfo.GetTypeAttr(_pTypeAttr)

        _typeInfo.GetDocumentation(-1, _typeName, _typeDescription, _typeHelpContext, _typeHelpFile)
        _typeInfo.GetContainingTypeLib(ppTLB, pIndex)
        _comTypeLibrary = New ComTypeLibrary(ppTLB)
      Else
        Throw New InvalidComObjectException()
      End If
    End Sub

    Protected Overrides Sub Finalize()
      Try
        Dispose()
      Finally
        MyBase.Finalize()
      End Try
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
      Try
        If _typeInfo IsNot Nothing Then
          _typeInfo.ReleaseTypeAttr(_pTypeAttr)
        End If

        If _object IsNot Nothing Then
          Marshal.ReleaseComObject(_object)
          _object = Nothing
        End If
        If _dispatch IsNot Nothing Then
          Marshal.ReleaseComObject(_dispatch)
          _dispatch = Nothing
        End If
      Catch
      End Try
    End Sub

    Public Function GetPropertyNames() As String()
      Dim list As New System.Collections.ArrayList()

      Try
        For i As Integer = 0 To Me.TypeAttr.cFuncs - 1
          Dim pFuncDesc As IntPtr = IntPtr.Zero
          Dim funcDesc As System.Runtime.InteropServices.ComTypes.FUNCDESC
          Dim strName As String, strDocString As String, strHelpFile As String
          Dim dwHelpContext As Integer

          _typeInfo.GetFuncDesc(i, pFuncDesc)
          funcDesc = DirectCast(Marshal.PtrToStructure(pFuncDesc, GetType(System.Runtime.InteropServices.ComTypes.FUNCDESC)), System.Runtime.InteropServices.ComTypes.FUNCDESC)

          Select Case funcDesc.invkind
            Case System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_PROPERTYGET
              _typeInfo.GetDocumentation(funcDesc.memid, strName, strDocString, dwHelpContext, strHelpFile)
              list.Add(strName)
              Exit Select
          End Select
        Next
      Catch ex As System.Exception
        Throw ex
      End Try

      Return DirectCast(list.ToArray(GetType(String)), String())
    End Function

    Public Function GetAllMembers() As String()
      Dim list As New System.Collections.ArrayList()

      Try

        For i As Integer = 0 To Me.TypeAttr.cFuncs - 1
          Dim pFuncDesc As IntPtr = IntPtr.Zero
          Dim funcDesc As System.Runtime.InteropServices.ComTypes.FUNCDESC
          Dim strName As String, strDocString As String, strHelpFile As String
          Dim dwHelpContext As Integer

          _typeInfo.GetFuncDesc(i, pFuncDesc)
          funcDesc = DirectCast(Marshal.PtrToStructure(pFuncDesc, GetType(System.Runtime.InteropServices.ComTypes.FUNCDESC)), System.Runtime.InteropServices.ComTypes.FUNCDESC)
          'funcDesc.oVft


          'Select Case funcDesc.invkind
          '  Case System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_PROPERTYGET
          _typeInfo.GetDocumentation(funcDesc.memid, strName, strDocString, dwHelpContext, strHelpFile)
          Debug.Print(strName)
          Dim ppv As IntPtr
          ' _typeInfo.GetMops
          '_typeInfo.(funcDesc.memid, funcDesc.invkind, ppv)
          list.Add(i & "-" & strName & "-" & funcDesc.oVft & "-" & funcDesc.memid & "-" & pFuncDesc.ToString & "-" & ppv.ToString & "-" & funcDesc.elemdescFunc.tdesc.vt)
          'Exit Select
          'End Select
        Next
      Catch ex As System.Exception
        MsgBox(ex.ToString)
        '  Throw ex
      End Try

      Return DirectCast(list.ToArray(GetType(String)), String())
    End Function

    Public ReadOnly Property TypeInfo() As ITypeInfo
      Get
        Return Me._typeInfo
      End Get
    End Property
    Public ReadOnly Property TypeName() As String
      Get
        Return Me._typeName
      End Get
    End Property
    Public ReadOnly Property TypeFullName() As String
      Get
        Return (Me.ComTypeLibrary.Name & ".") + Me._typeName
      End Get
    End Property
    Public ReadOnly Property TypeDescription() As String
      Get
        Return Me._typeDescription
      End Get
    End Property
    Public ReadOnly Property TypeHelpContext() As Integer
      Get
        Return Me._typeHelpContext
      End Get
    End Property
    Public ReadOnly Property TypeHelpFile() As String
      Get
        Return Me._typeHelpFile
      End Get
    End Property
    Public ReadOnly Property WrappedComObject() As Object
      Get
        Return _dispatch
      End Get
    End Property
    Public ReadOnly Property ComTypeLibrary() As ComTypeLibrary
      Get
        Return _comTypeLibrary
      End Get
    End Property
    Public ReadOnly Property TypeAttr() As System.Runtime.InteropServices.ComTypes.TYPEATTR
      Get
        Return DirectCast(Marshal.PtrToStructure(_pTypeAttr, GetType(System.Runtime.InteropServices.ComTypes.TYPEATTR)), System.Runtime.InteropServices.ComTypes.TYPEATTR)
      End Get
    End Property
    Public ReadOnly Property TypeVersion() As String
      Get
        Dim version As String = [String].Empty
        Try
          version = (TypeAttr.wMajorVerNum.ToString() & ".") + TypeAttr.wMinorVerNum.ToString()
        Catch
        End Try
        Return version
      End Get
    End Property
  End Class
End Namespace


' ComTypeLibrary.cs 



Namespace SolidEdgeSpy.InteropServices
  Public Class ComTypeLibrary
    Private _typeLib As System.Runtime.InteropServices.ComTypes.ITypeLib
    Private _pTypeLibAttr As IntPtr = IntPtr.Zero
    Private _Name As String, _Description As String, _HelpFile As String
    Private _HelpContext As Integer

    Public Sub New(ByVal typeLib As System.Runtime.InteropServices.ComTypes.ITypeLib)
      _typeLib = typeLib
      _typeLib.GetLibAttr(_pTypeLibAttr)
      _typeLib.GetDocumentation(-1, _Name, _Description, _HelpContext, _HelpFile)
    End Sub

    Public ReadOnly Property Name() As String
      Get
        Return Me._Name
      End Get
    End Property
    Public ReadOnly Property Description() As String
      Get
        Return Me._Description
      End Get
    End Property
    Public ReadOnly Property HelpContext() As Integer
      Get
        Return Me._HelpContext
      End Get
    End Property
    Public ReadOnly Property HelpFile() As String
      Get
        Return Me._HelpFile
      End Get
    End Property
    Public ReadOnly Property TypeLibAttr() As System.Runtime.InteropServices.ComTypes.TYPELIBATTR
      Get
        Return DirectCast(Marshal.PtrToStructure(_pTypeLibAttr, GetType(System.Runtime.InteropServices.ComTypes.TYPELIBATTR)), System.Runtime.InteropServices.ComTypes.TYPELIBATTR)
      End Get
    End Property
    Public ReadOnly Property TypeLibVersion() As String
      Get
        Dim version As String = [String].Empty
        Try
          version = (TypeLibAttr.wMajorVerNum.ToString() & ".") + TypeLibAttr.wMinorVerNum.ToString()
        Catch
        End Try
        Return version
      End Get
    End Property

    Public ReadOnly Property Version() As Version
      Get
        Return New Version(TypeLibAttr.wMajorVerNum, TypeLibAttr.wMinorVerNum, 0, 0)
      End Get
    End Property

    Public Overloads Overrides Function GetHashCode() As Integer
      Return Me.TypeLibAttr.guid.GetHashCode() + Me.TypeLibAttr.wMajorVerNum.GetHashCode() + Me.TypeLibAttr.wMinorVerNum.GetHashCode()
    End Function

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
      Dim comTypeLibrary As ComTypeLibrary = TryCast(obj, ComTypeLibrary)
      If comTypeLibrary IsNot Nothing Then
        If Me.TypeLibAttr.guid.Equals(comTypeLibrary.TypeLibAttr.guid) Then
          If Me.TypeLibAttr.wMajorVerNum.Equals(comTypeLibrary.TypeLibAttr.wMajorVerNum) Then
            If Me.TypeLibAttr.wMinorVerNum.Equals(comTypeLibrary.TypeLibAttr.wMinorVerNum) Then
              Return True
            End If
          End If
        End If
        Return MyBase.Equals(obj)
      Else
        Return MyBase.Equals(obj)
      End If
    End Function

    Public Overloads Overrides Function ToString() As String
      Return (Me.Name & " - ") + Me.Description
    End Function
  End Class
End Namespace
