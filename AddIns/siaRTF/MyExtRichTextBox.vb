Imports System.ComponentModel
Imports System.Collections
Imports System.Diagnostics
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices


Namespace MyExtRichTextBox
#Region "OLE definitions"
  ' STGM
  <Flags(), ComVisible(False)> _
  Public Enum STGM As Integer
    STGM_DIRECT = &H0
    STGM_TRANSACTED = &H10000
    STGM_SIMPLE = &H8000000
    STGM_READ = &H0
    STGM_WRITE = &H1
    STGM_READWRITE = &H2
    STGM_SHARE_DENY_NONE = &H40
    STGM_SHARE_DENY_READ = &H30
    STGM_SHARE_DENY_WRITE = &H20
    STGM_SHARE_EXCLUSIVE = &H10
    STGM_PRIORITY = &H40000
    STGM_DELETEONRELEASE = &H4000000
    STGM_NOSCRATCH = &H100000
    STGM_CREATE = &H1000
    STGM_CONVERT = &H20000
    STGM_FAILIFTHERE = &H0
    STGM_NOSNAPSHOT = &H200000
  End Enum

  ' DVASPECT
  <Flags(), ComVisible(False)> _
  Public Enum DVASPECT As Integer
    DVASPECT_CONTENT = 1
    DVASPECT_THUMBNAIL = 2
    DVASPECT_ICON = 4
    DVASPECT_DOCPRINT = 8
    DVASPECT_OPAQUE = 16
    DVASPECT_TRANSPARENT = 32
  End Enum

  ' CLIPFORMAT
  <ComVisible(False)> _
  Public Enum CLIPFORMAT As Integer
    CF_TEXT = 1
    CF_BITMAP = 2
    CF_METAFILEPICT = 3
    CF_SYLK = 4
    CF_DIF = 5
    CF_TIFF = 6
    CF_OEMTEXT = 7
    CF_DIB = 8
    CF_PALETTE = 9
    CF_PENDATA = 10
    CF_RIFF = 11
    CF_WAVE = 12
    CF_UNICODETEXT = 13
    CF_ENHMETAFILE = 14
    CF_HDROP = 15
    CF_LOCALE = 16
    CF_MAX = 17
    CF_OWNERDISPLAY = &H80
    CF_DSPTEXT = &H81
    CF_DSPBITMAP = &H82
    CF_DSPMETAFILEPICT = &H83
    CF_DSPENHMETAFILE = &H8E
  End Enum

  ' Object flags
  <Flags(), ComVisible(False)> _
  Public Enum REOOBJECTFLAGS As UInteger
    REO_NULL = &H0
    ' No flags
    REO_READWRITEMASK = &H3F
    ' Mask out RO bits
    REO_DONTNEEDPALETTE = &H20
    ' Object doesn't need palette
    REO_BLANK = &H10
    ' Object is blank
    REO_DYNAMICSIZE = &H8
    ' Object defines size always
    REO_INVERTEDSELECT = &H4
    ' Object drawn all inverted if sel
    REO_BELOWBASELINE = &H2
    ' Object sits below the baseline
    REO_RESIZABLE = &H1
    ' Object may be resized
    REO_LINK = &H80000000UI
    ' Object is a link (RO)
    REO_STATIC = &H40000000
    ' Object is static (RO)
    REO_SELECTED = &H8000000
    ' Object selected (RO)
    REO_OPEN = &H4000000
    ' Object open in its server (RO)
    REO_INPLACEACTIVE = &H2000000
    ' Object in place active (RO)
    REO_HILITED = &H1000000
    ' Object is to be hilited (RO)
    REO_LINKAVAILABLE = &H800000
    ' Link believed available (RO)
    REO_GETMETAFILE = &H400000
    ' Object requires metafile (RO)
  End Enum

  ' OLERENDER
  <ComVisible(False)> _
  Public Enum OLERENDER As Integer
    OLERENDER_NONE = 0
    OLERENDER_DRAW = 1
    OLERENDER_FORMAT = 2
    OLERENDER_ASIS = 3
  End Enum

  ' TYMED
  <Flags(), ComVisible(False)> _
  Public Enum TYMED As Integer
    TYMED_NULL = 0
    TYMED_HGLOBAL = 1
    TYMED_FILE = 2
    TYMED_ISTREAM = 4
    TYMED_ISTORAGE = 8
    TYMED_GDI = 16
    TYMED_MFPICT = 32
    TYMED_ENHMF = 64
  End Enum

  <StructLayout(LayoutKind.Sequential), ComVisible(False)> _
  Public Structure FORMATETC
    Public cfFormat As CLIPFORMAT
    Public ptd As IntPtr
    Public dwAspect As DVASPECT
    Public lindex As Integer
    Public tymed As TYMED
  End Structure

  <StructLayout(LayoutKind.Sequential), ComVisible(False)> _
  Public Structure STGMEDIUM
    '[MarshalAs(UnmanagedType.I4)]
    Public tymed As Integer
    Public unionmember As IntPtr
    Public pUnkForRelease As IntPtr
  End Structure

  <ComVisible(True), ComImport(), Guid("00000103-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IEnumFORMATETC
    <PreserveSig()> _
    Function [Next](<[In](), MarshalAs(UnmanagedType.U4)> ByVal celt As Integer, <Out()> ByVal rgelt As FORMATETC, <[In](), Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pceltFetched As Integer()) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function Skip(<[In](), MarshalAs(UnmanagedType.U4)> ByVal celt As Integer) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function Reset() As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function Clone(<Out(), MarshalAs(UnmanagedType.LPArray)> ByVal ppenum As IEnumFORMATETC()) As <MarshalAs(UnmanagedType.I4)> Integer
  End Interface

  <ComVisible(True), StructLayout(LayoutKind.Sequential)> _
  Public Class COMRECT
    Public left As Integer
    Public top As Integer
    Public right As Integer
    Public bottom As Integer

    Public Sub New()
    End Sub

    Public Sub New(ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer)
      Me.left = left
      Me.top = top
      Me.right = right
      Me.bottom = bottom
    End Sub

    Public Shared Function FromXYWH(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer) As COMRECT
      Return New COMRECT(x, y, x + width, y + height)
    End Function
  End Class

  Public Enum GETOBJECTOPTIONS
    REO_GETOBJ_NO_INTERFACES = &H0
    REO_GETOBJ_POLEOBJ = &H1
    REO_GETOBJ_PSTG = &H2
    REO_GETOBJ_POLESITE = &H4
    REO_GETOBJ_ALL_INTERFACES = &H7
  End Enum

  Public Enum GETCLIPBOARDDATAFLAGS
    RECO_PASTE = 0
    RECO_DROP = 1
    RECO_COPY = 2
    RECO_CUT = 3
    RECO_DRAG = 4
  End Enum

  <StructLayout(LayoutKind.Sequential)> _
  Public Structure CHARRANGE
    Public cpMin As Integer
    Public cpMax As Integer
  End Structure

  <StructLayout(LayoutKind.Sequential)> _
  Public Class REOBJECT
    Public cbStruct As Integer = Marshal.SizeOf(GetType(REOBJECT))
    ' Size of structure
    Public cp As Integer
    ' Character position of object
    Public clsid As Guid
    ' Class ID of object
    Public poleobj As IntPtr
    ' OLE object interface
    Public pstg As IStorage
    ' Associated storage interface
    Public polesite As IOleClientSite
    ' Associated client site interface
    Public sizel As Size
    ' Size of object (may be 0,0)
    Public dvAspect As UInteger
    ' Display aspect to use
    Public dwFlags As UInteger
    ' Object status flags
    Public dwUser As UInteger
    ' Dword for user's use
  End Class

  <ComVisible(True), Guid("0000010F-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IAdviseSink

    'C#r: UNDONE (Field in interface) public static readonly    Guid iid;
    Sub OnDataChange(<[In]()> ByVal pFormatetc As FORMATETC, <[In]()> ByVal pStgmed As STGMEDIUM)

    Sub OnViewChange(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwAspect As Integer, <[In](), MarshalAs(UnmanagedType.I4)> ByVal lindex As Integer)

    Sub OnRename(<[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pmk As Object)

    Sub OnSave()


    Sub OnClose()
  End Interface

  <ComVisible(False), StructLayout(LayoutKind.Sequential)> _
  Public NotInheritable Class STATDATA

    <MarshalAs(UnmanagedType.U4)> _
    Public advf As Integer
    <MarshalAs(UnmanagedType.U4)> _
    Public dwConnection As Integer

  End Class

  <ComVisible(False), StructLayout(LayoutKind.Sequential)> _
  Public NotInheritable Class tagOLEVERB
    <MarshalAs(UnmanagedType.I4)> _
    Public lVerb As Integer

    <MarshalAs(UnmanagedType.LPWStr)> _
    Public lpszVerbName As [String]

    <MarshalAs(UnmanagedType.U4)> _
    Public fuFlags As Integer

    <MarshalAs(UnmanagedType.U4)> _
    Public grfAttribs As Integer

  End Class

  <ComVisible(True), ComImport(), Guid("00000104-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IEnumOLEVERB

    <PreserveSig()> _
    Function [Next](<MarshalAs(UnmanagedType.U4)> ByVal celt As Integer, <Out()> ByVal rgelt As tagOLEVERB, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pceltFetched As Integer()) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function Skip(<[In](), MarshalAs(UnmanagedType.U4)> ByVal celt As Integer) As <MarshalAs(UnmanagedType.I4)> Integer

    Sub Reset()


    Sub Clone(ByRef ppenum As IEnumOLEVERB)


  End Interface

  <ComVisible(True), Guid("00000105-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IEnumSTATDATA

    'C#r: UNDONE (Field in interface) public static readonly    Guid iid;

    Sub [Next](<[In](), MarshalAs(UnmanagedType.U4)> ByVal celt As Integer, <Out()> ByVal rgelt As STATDATA, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pceltFetched As Integer())


    Sub Skip(<[In](), MarshalAs(UnmanagedType.U4)> ByVal celt As Integer)


    Sub Reset()


    Sub Clone(<Out(), MarshalAs(UnmanagedType.LPArray)> ByVal ppenum As IEnumSTATDATA())


  End Interface

  <ComVisible(True), Guid("0000011B-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IOleContainer


    Sub ParseDisplayName(<[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pbc As Object, <[In](), MarshalAs(UnmanagedType.BStr)> ByVal pszDisplayName As String, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal pchEaten As Integer(), <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal ppmkOut As Object())


    Sub EnumObjects(<[In](), MarshalAs(UnmanagedType.U4)> ByVal grfFlags As Integer, <Out(), MarshalAs(UnmanagedType.LPArray)> ByVal ppenum As Object())


    Sub LockContainer(<[In](), MarshalAs(UnmanagedType.I4)> ByVal fLock As Integer)
  End Interface

  <ComVisible(True), ComImport(), Guid("0000010E-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IDataObject
    <PreserveSig()> _
    Function GetData(ByRef a As FORMATETC, ByRef b As STGMEDIUM) As UInteger

    <PreserveSig()> _
    Function GetDataHere(ByRef pFormatetc As FORMATETC, ByRef pMedium As STGMEDIUM) As UInteger

    <PreserveSig()> _
    Function QueryGetData(ByRef pFormatetc As FORMATETC) As UInteger

    <PreserveSig()> _
    Function GetCanonicalFormatEtc(ByRef pformatectIn As FORMATETC, ByRef pformatetcOut As FORMATETC) As UInteger

    <PreserveSig()> _
    Function SetData(ByRef pFormatectIn As FORMATETC, ByRef pmedium As STGMEDIUM, <[In](), MarshalAs(UnmanagedType.Bool)> ByVal fRelease As Boolean) As UInteger

    <PreserveSig()> _
    Function EnumFormatEtc(ByVal dwDirection As UInteger, ByVal penum As IEnumFORMATETC) As UInteger

    <PreserveSig()> _
    Function DAdvise(ByRef pFormatetc As FORMATETC, ByVal advf As Integer, <[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pAdvSink As IAdviseSink, ByRef pdwConnection As UInteger) As UInteger

    <PreserveSig()> _
    Function DUnadvise(ByVal dwConnection As UInteger) As UInteger

    <PreserveSig()> _
    Function EnumDAdvise(<Out(), MarshalAs(UnmanagedType.[Interface])> ByRef ppenumAdvise As IEnumSTATDATA) As UInteger
  End Interface

  <ComVisible(True), Guid("00000118-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IOleClientSite

    <PreserveSig()> _
    Function SaveObject() As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetMoniker(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwAssign As Integer, <[In](), MarshalAs(UnmanagedType.U4)> ByVal dwWhichMoniker As Integer, <Out(), MarshalAs(UnmanagedType.[Interface])> ByRef ppmk As Object) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetContainer(<MarshalAs(UnmanagedType.[Interface])> ByRef container As IOleContainer) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function ShowObject() As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function OnShowWindow(<[In](), MarshalAs(UnmanagedType.I4)> ByVal fShow As Integer) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function RequestNewObjectLayout() As <MarshalAs(UnmanagedType.I4)> Integer
  End Interface

  <ComVisible(False), StructLayout(LayoutKind.Sequential)> _
  Public NotInheritable Class tagLOGPALETTE
    'leftover(offset=0, palVersion)
    <MarshalAs(UnmanagedType.U2)> _
    Public palVersion As Short

    'leftover(offset=2, palNumEntries)
    <MarshalAs(UnmanagedType.U2)> _
    Public palNumEntries As Short

    ' UNMAPPABLE: palPalEntry: Cannot be used as a structure field.
    '   /** @com.structmap(UNMAPPABLE palPalEntry) */
    '  public UNMAPPABLE palPalEntry;
  End Class

  <ComVisible(True), ComImport(), Guid("00000112-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IOleObject

    <PreserveSig()> _
    Function SetClientSite(<[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pClientSite As IOleClientSite) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetClientSite(ByRef site As IOleClientSite) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function SetHostNames(<[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal szContainerApp As String, <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal szContainerObj As String) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function Close(<[In](), MarshalAs(UnmanagedType.I4)> ByVal dwSaveOption As Integer) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function SetMoniker(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwWhichMoniker As Integer, <[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pmk As Object) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetMoniker(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwAssign As Integer, <[In](), MarshalAs(UnmanagedType.U4)> ByVal dwWhichMoniker As Integer, ByRef moniker As Object) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function InitFromData(<[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pDataObject As IDataObject, <[In](), MarshalAs(UnmanagedType.I4)> ByVal fCreation As Integer, <[In](), MarshalAs(UnmanagedType.U4)> ByVal dwReserved As Integer) As <MarshalAs(UnmanagedType.I4)> Integer

    Function GetClipboardData(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwReserved As Integer, ByRef data As IDataObject) As Integer

    <PreserveSig()> _
    Function DoVerb(<[In](), MarshalAs(UnmanagedType.I4)> ByVal iVerb As Integer, <[In]()> ByVal lpmsg As IntPtr, <[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pActiveSite As IOleClientSite, <[In](), MarshalAs(UnmanagedType.I4)> ByVal lindex As Integer, <[In]()> ByVal hwndParent As IntPtr, <[In]()> ByVal lprcPosRect As COMRECT) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function EnumVerbs(ByRef e As IEnumOLEVERB) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function Update() As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function IsUpToDate() As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetUserClassID(<[In](), Out()> ByRef pClsid As Guid) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetUserType(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwFormOfType As Integer, <Out(), MarshalAs(UnmanagedType.LPWStr)> ByRef userType As String) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function SetExtent(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwDrawAspect As Integer, <[In]()> ByVal pSizel As Size) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetExtent(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwDrawAspect As Integer, <Out()> ByVal pSizel As Size) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function Advise(<[In](), MarshalAs(UnmanagedType.[Interface])> ByVal pAdvSink As IAdviseSink, ByRef cookie As Integer) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function Unadvise(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwConnection As Integer) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function EnumAdvise(ByRef e As IEnumSTATDATA) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetMiscStatus(<[In](), MarshalAs(UnmanagedType.U4)> ByVal dwAspect As Integer, ByRef misc As Integer) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function SetColorScheme(<[In]()> ByVal pLogpal As tagLOGPALETTE) As <MarshalAs(UnmanagedType.I4)> Integer
  End Interface

  <ComImport()> _
  <Guid("0000000d-0000-0000-C000-000000000046")> _
  <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IEnumSTATSTG
    ' The user needs to allocate an STATSTG array whose size is celt.
    <PreserveSig()> _
    Function [Next](ByVal celt As UInteger, <MarshalAs(UnmanagedType.LPArray), Out()> ByVal rgelt As STATSTG(), ByRef pceltFetched As UInteger) As UInteger

    Sub Skip(ByVal celt As UInteger)

    Sub Reset()

    Function Clone() As <MarshalAs(UnmanagedType.[Interface])> IEnumSTATSTG
  End Interface

  <ComImport()> _
  <Guid("0000000b-0000-0000-C000-000000000046")> _
  <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IStorage
    ' [string][in] 
    ' [in] 
    ' [in] 
    ' [in] 
    ' [out] 
    Function CreateStream(ByVal pwcsName As String, ByVal grfMode As UInteger, ByVal reserved1 As UInteger, ByVal reserved2 As UInteger, ByRef ppstm As IStream) As Integer

    ' [string][in] 
    ' [unique][in] 
    ' [in] 
    ' [in] 
    ' [out] 
    Function OpenStream(ByVal pwcsName As String, ByVal reserved1 As IntPtr, ByVal grfMode As UInteger, ByVal reserved2 As UInteger, ByRef ppstm As IStream) As Integer

    ' [string][in] 
    ' [in] 
    ' [in] 
    ' [in] 
    ' [out] 
    Function CreateStorage(ByVal pwcsName As String, ByVal grfMode As UInteger, ByVal reserved1 As UInteger, ByVal reserved2 As UInteger, ByRef ppstg As IStorage) As Integer

    ' [string][unique][in] 
    ' [unique][in] 
    ' [in] 
    ' [unique][in] 
    ' [in] 
    ' [out] 
    Function OpenStorage(ByVal pwcsName As String, ByVal pstgPriority As IStorage, ByVal grfMode As UInteger, ByVal snbExclude As IntPtr, ByVal reserved As UInteger, ByRef ppstg As IStorage) As Integer

    ' [in] 
    ' [size_is][unique][in] 
    ' [unique][in] 
    ' [unique][in] 
    Function CopyTo(ByVal ciidExclude As UInteger, ByVal rgiidExclude As Guid, ByVal snbExclude As IntPtr, ByVal pstgDest As IStorage) As Integer

    ' [string][in] 
    ' [unique][in] 
    ' [string][in] 
    ' [in] 
    Function MoveElementTo(ByVal pwcsName As String, ByVal pstgDest As IStorage, ByVal pwcsNewName As String, ByVal grfFlags As UInteger) As Integer

    ' [in] 
    Function Commit(ByVal grfCommitFlags As UInteger) As Integer

    Function Revert() As Integer

    ' [in] 
    ' [size_is][unique][in] 
    ' [in] 
    ' [out] 
    Function EnumElements(ByVal reserved1 As UInteger, ByVal reserved2 As IntPtr, ByVal reserved3 As UInteger, ByRef ppenum As IEnumSTATSTG) As Integer

    ' [string][in] 
    Function DestroyElement(ByVal pwcsName As String) As Integer

    ' [string][in] 
    ' [string][in] 
    Function RenameElement(ByVal pwcsOldName As String, ByVal pwcsNewName As String) As Integer

    ' [string][unique][in] 
    ' [unique][in] 
    ' [unique][in] 
    ' [unique][in] 
    Function SetElementTimes(ByVal pwcsName As String, ByVal pctime As FILETIME, ByVal patime As FILETIME, ByVal pmtime As FILETIME) As Integer

    ' [in] 
    Function SetClass(ByVal clsid As Guid) As Integer

    ' [in] 
    ' [in] 
    Function SetStateBits(ByVal grfStateBits As UInteger, ByVal grfMask As UInteger) As Integer

    ' [out] 
    ' [in] 
    Function Stat(ByRef pstatstg As STATSTG, ByVal grfStatFlag As UInteger) As Integer

  End Interface

  <ComImport()> _
  <Guid("0000000a-0000-0000-C000-000000000046")> _
  <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface ILockBytes
    ' [in] 
    ' [unique][out] 
    ' [in] 
    ' [out] 
    Function ReadAt(ByVal ulOffset As ULong, ByVal pv As IntPtr, ByVal cb As UInteger, ByRef pcbRead As IntPtr) As Integer

    ' [in] 
    ' [size_is][in] 
    ' [in] 
    ' [out] 
    Function WriteAt(ByVal ulOffset As ULong, ByVal pv As IntPtr, ByVal cb As UInteger, ByRef pcbWritten As IntPtr) As Integer

    Function Flush() As Integer

    ' [in] 
    Function SetSize(ByVal cb As ULong) As Integer

    ' [in] 
    ' [in] 
    ' [in] 
    Function LockRegion(ByVal libOffset As ULong, ByVal cb As ULong, ByVal dwLockType As UInteger) As Integer

    ' [in] 
    ' [in] 
    ' [in] 
    Function UnlockRegion(ByVal libOffset As ULong, ByVal cb As ULong, ByVal dwLockType As UInteger) As Integer

    ' [out] 
    ' [in] 
    Function Stat(ByRef pstatstg As STATSTG, ByVal grfStatFlag As UInteger) As Integer

  End Interface

  <InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d")> _
  Public Interface ISequentialStream
    ' [length_is][size_is][out] 
    ' [in] 
    ' [out] 
    Function Read(ByVal pv As IntPtr, ByVal cb As UInteger, ByRef pcbRead As UInteger) As Integer

    ' [size_is][in] 
    ' [in] 
    ' [out] 
    Function Write(ByVal pv As IntPtr, ByVal cb As UInteger, ByRef pcbWritten As UInteger) As Integer

  End Interface

  <ComImport()> _
  <Guid("0000000c-0000-0000-C000-000000000046")> _
  <InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IStream
    Inherits ISequentialStream
    ' [in] 
    ' [in] 
    ' [out] 
    Function Seek(ByVal dlibMove As ULong, ByVal dwOrigin As UInteger, ByRef plibNewPosition As ULong) As Integer

    ' [in] 
    Function SetSize(ByVal libNewSize As ULong) As Integer

    ' [unique][in] 
    ' [in] 
    ' [out] 
    ' [out] 
    Function CopyTo(<[In]()> ByVal pstm As IStream, ByVal cb As ULong, ByRef pcbRead As ULong, ByRef pcbWritten As ULong) As Integer

    ' [in] 
    Function Commit(ByVal grfCommitFlags As UInteger) As Integer

    Function Revert() As Integer

    ' [in] 
    ' [in] 
    ' [in] 
    Function LockRegion(ByVal libOffset As ULong, ByVal cb As ULong, ByVal dwLockType As UInteger) As Integer

    ' [in] 
    ' [in] 
    ' [in] 
    Function UnlockRegion(ByVal libOffset As ULong, ByVal cb As ULong, ByVal dwLockType As UInteger) As Integer

    ' [out] 
    ' [in] 
    Function Stat(ByRef pstatstg As STATSTG, ByVal grfStatFlag As UInteger) As Integer

    ' [out] 
    Function Clone(ByRef ppstm As IStream) As Integer

  End Interface

  ''' <summary>
  ''' Definition for interface IPersist.
  ''' </summary>
  <InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000010c-0000-0000-C000-000000000046")> _
  Public Interface IPersist
    ''' <summary>
    ''' getClassID
    ''' </summary>
    ''' <param name="pClassID"></param>
    ' [out] 
    Sub GetClassID(ByRef pClassID As Guid)
  End Interface

  ''' <summary>
  ''' Definition for interface IPersistStream.
  ''' </summary>
  <InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00000109-0000-0000-C000-000000000046")> _
  Public Interface IPersistStream
    Inherits IPersist
    ''' <summary>
    ''' GetClassID
    ''' </summary>
    ''' <param name="pClassID"></param>
    Shadows Sub GetClassID(ByRef pClassID As Guid)
    ''' <summary>
    ''' isDirty
    ''' </summary>
    ''' <returns></returns>
    <PreserveSig()> _
    Function IsDirty() As Integer
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="pStm"></param>
    Sub Load(<[In]()> ByVal pStm As UCOMIStream)
    ''' <summary>
    ''' Save
    ''' </summary>
    ''' <param name="pStm"></param>
    ''' <param name="fClearDirty"></param>
    Sub Save(<[In]()> ByVal pStm As UCOMIStream, <[In](), MarshalAs(UnmanagedType.Bool)> ByVal fClearDirty As Boolean)
    ''' <summary>
    ''' GetSizeMax
    ''' </summary>
    ''' <param name="pcbSize"></param>
    Sub GetSizeMax(ByRef pcbSize As Long)
  End Interface

  <ComImport(), Guid("00020D00-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IRichEditOle
    <PreserveSig()> _
    Function GetClientSite(ByRef site As IOleClientSite) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetObjectCount() As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetLinkCount() As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetObject(ByVal iob As Integer, <[In](), Out()> ByVal lpreobject As REOBJECT, <MarshalAs(UnmanagedType.U4)> ByVal flags As GETOBJECTOPTIONS) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function InsertObject(ByVal lpreobject As REOBJECT) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function ConvertObject(ByVal iob As Integer, ByVal rclsidNew As Guid, ByVal lpstrUserTypeNew As String) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function ActivateAs(ByVal rclsid As Guid, ByVal rclsidAs As Guid) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function SetHostNames(ByVal lpstrContainerApp As String, ByVal lpstrContainerObj As String) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function SetLinkAvailable(ByVal iob As Integer, ByVal fAvailable As Boolean) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function SetDvaspect(ByVal iob As Integer, ByVal dvaspect As UInteger) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function HandsOffStorage(ByVal iob As Integer) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function SaveCompleted(ByVal iob As Integer, ByVal lpstg As IStorage) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function InPlaceDeactivate() As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function ContextSensitiveHelp(ByVal fEnterMode As Boolean) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function GetClipboardData(<[In](), Out()> ByRef lpchrg As CHARRANGE, <MarshalAs(UnmanagedType.U4)> ByVal reco As GETCLIPBOARDDATAFLAGS, ByRef lplpdataobj As IDataObject) As <MarshalAs(UnmanagedType.I4)> Integer

    <PreserveSig()> _
    Function ImportDataObject(ByVal lpdataobj As IDataObject, ByVal cf As Integer, ByVal hMetaPict As IntPtr) As <MarshalAs(UnmanagedType.I4)> Integer
  End Interface
#End Region

  Public Class myDataObject
    Implements IDataObject
    Private mBitmap As Bitmap
    Public mpFormatetc As FORMATETC

#Region "IDataObject Members"

    Private Const S_OK As UInteger = 0
    Private Const E_POINTER As UInteger = &H80004003UI
    Private Const E_NOTIMPL As UInteger = &H80004001UI
    Private Const E_FAIL As UInteger = &H80004005UI

    Public Function GetData(ByRef pFormatetc As FORMATETC, ByRef pMedium As STGMEDIUM) As UInteger Implements IDataObject.GetData
      Dim hDst As IntPtr = mBitmap.GetHbitmap()

      pMedium.tymed = CInt(TYMED.TYMED_GDI)
      pMedium.unionmember = hDst
      pMedium.pUnkForRelease = IntPtr.Zero

      Return CUInt(S_OK)
    End Function

    Public Function GetDataHere(ByRef pFormatetc As FORMATETC, ByRef pMedium As STGMEDIUM) As UInteger Implements IDataObject.GetDataHere
      Trace.WriteLine("GetDataHere")

      pMedium = New STGMEDIUM()

      Return CUInt(E_NOTIMPL)
    End Function

    Public Function QueryGetData(ByRef pFormatetc As FORMATETC) As UInteger Implements IDataObject.QueryGetData
      Trace.WriteLine("QueryGetData")

      Return CUInt(E_NOTIMPL)
    End Function

    Public Function GetCanonicalFormatEtc(ByRef pFormatetcIn As FORMATETC, ByRef pFormatetcOut As FORMATETC) As UInteger Implements IDataObject.GetCanonicalFormatEtc
      Trace.WriteLine("GetCanonicalFormatEtc")

      pFormatetcOut = New FORMATETC()

      Return CUInt(E_NOTIMPL)
    End Function

    Public Function SetData(ByRef a As FORMATETC, ByRef b As STGMEDIUM, ByVal fRelease As Boolean) As UInteger Implements IDataObject.SetData
      'mpFormatetc = pFormatectIn;
      'mpmedium = pmedium;

      Trace.WriteLine("SetData")

      Return CInt(S_OK)
    End Function

    Public Function EnumFormatEtc(ByVal dwDirection As UInteger, ByVal penum As IEnumFORMATETC) As UInteger Implements IDataObject.EnumFormatEtc
      Trace.WriteLine("EnumFormatEtc")

      Return CInt(S_OK)
    End Function

    Public Function DAdvise(ByRef a As FORMATETC, ByVal advf As Integer, ByVal pAdvSink As IAdviseSink, ByRef pdwConnection As UInteger) As UInteger Implements IDataObject.DAdvise
      Trace.WriteLine("DAdvise")

      pdwConnection = 0

      Return CUInt(E_NOTIMPL)
    End Function

    Public Function DUnadvise(ByVal dwConnection As UInteger) As UInteger Implements IDataObject.DUnadvise
      Trace.WriteLine("DUnadvise")

      Return CUInt(E_NOTIMPL)
    End Function

    Public Function EnumDAdvise(ByRef ppenumAdvise As IEnumSTATDATA) As UInteger Implements IDataObject.EnumDAdvise
      Trace.WriteLine("EnumDAdvise")

      ppenumAdvise = Nothing

      Return CUInt(E_NOTIMPL)
    End Function

#End Region

    Public Sub New()
      mBitmap = New Bitmap(16, 16)
      mpFormatetc = New FORMATETC()
    End Sub

    Public Sub SetImage(ByVal strFilename As String)
      Try
        mBitmap = DirectCast(Bitmap.FromFile(strFilename, True), Bitmap)

        mpFormatetc.cfFormat = CLIPFORMAT.CF_BITMAP
        ' Clipboard format = CF_BITMAP
        mpFormatetc.ptd = IntPtr.Zero
        ' Target Device = Screen
        mpFormatetc.dwAspect = DVASPECT.DVASPECT_CONTENT
        ' Level of detail = Full content
        mpFormatetc.lindex = -1
        ' Index = Not applicaple
        ' Storage medium = HBITMAP handle
        mpFormatetc.tymed = TYMED.TYMED_GDI
      Catch
      End Try
    End Sub

    Public Sub SetImage(ByVal image As Image)
      Try
        mBitmap = New Bitmap(image)

        mpFormatetc.cfFormat = CLIPFORMAT.CF_BITMAP
        ' Clipboard format = CF_BITMAP
        mpFormatetc.ptd = IntPtr.Zero
        ' Target Device = Screen
        mpFormatetc.dwAspect = DVASPECT.DVASPECT_CONTENT
        ' Level of detail = Full content
        mpFormatetc.lindex = -1
        ' Index = Not applicaple
        ' Storage medium = HBITMAP handle
        mpFormatetc.tymed = TYMED.TYMED_GDI
      Catch
      End Try
    End Sub
  End Class

  Public Class MyExtRichTextBox
    Inherits RichTextBox
    Public Sub New()
    End Sub

#Region "Imports and structs"

    ' It makes no difference if we use PARAFORMAT or
    ' PARAFORMAT2 here, so I have opted for PARAFORMAT2.
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure PARAFORMAT
      Public cbSize As Integer
      Public dwMask As UInteger
      Public wNumbering As Short
      Public wReserved As Short
      Public dxStartIndent As Integer
      Public dxRightIndent As Integer
      Public dxOffset As Integer
      Public wAlignment As Short
      Public cTabCount As Short
      <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
      Public rgxTabs As Integer()

      ' PARAFORMAT2 from here onwards.
      Public dySpaceBefore As Integer
      Public dySpaceAfter As Integer
      Public dyLineSpacing As Integer
      Public sStyle As Short
      Public bLineSpacingRule As Byte
      Public bOutlineLevel As Byte
      Public wShadingWeight As Short
      Public wShadingStyle As Short
      Public wNumberingStart As Short
      Public wNumberingStyle As Short
      Public wNumberingTab As Short
      Public wBorderSpace As Short
      Public wBorderWidth As Short
      Public wBorders As Short
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure CHARFORMAT
      Public cbSize As Integer
      Public dwMask As UInt32
      Public dwEffects As UInt32
      Public yHeight As Int32
      Public yOffset As Int32
      Public crTextColor As Int32
      Public bCharSet As Byte
      Public bPitchAndFamily As Byte
      <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
      Public szFaceName As Char()

      ' CHARFORMAT2 from here onwards.
      Public wWeight As Short
      Public sSpacing As Short
      Public crBackColor As Int32
      Public lcid As UInteger
      Public dwReserved As UInteger
      Public sStyle As Short
      Public wKerning As Short
      Public bUnderlineType As Byte
      Public bAnimation As Byte
      Public bRevAuthor As Byte
      Public bReserved1 As Byte
    End Structure

    'added by mw
    <DllImport("user32.dll")> _
    Public Shared Function SendMessage( _
      ByVal hWnd As IntPtr, _
      ByVal msg As Int32, _
      ByVal wParam As Int32, _
      ByVal lParam As IntPtr) As Int32
    End Function

    <DllImport("user32", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As HandleRef, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    <DllImport("user32", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As HandleRef, ByVal msg As Integer, ByVal wParam As Integer, ByRef lp As PARAFORMAT) As Integer
    End Function

    <DllImport("user32", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As HandleRef, ByVal msg As Integer, ByVal wParam As Integer, ByRef lp As CHARFORMAT) As Integer
    End Function

    Private Const EM_SETEVENTMASK As Integer = 1073
    Private Const WM_SETREDRAW As Integer = 11

    <DllImport("User32.dll", CharSet:=CharSet.Auto, PreserveSig:=False)> _
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal message As Integer, ByVal wParam As Integer) As IRichEditOle
    End Function

    <DllImport("user32.dll", ExactSpelling:=True, CharSet:=CharSet.Auto)> _
    Friend Shared Function GetClientRect(ByVal hWnd As IntPtr, <[In](), Out()> ByRef rect As Rectangle) As Boolean
    End Function

    <DllImport("user32.dll", ExactSpelling:=True, CharSet:=CharSet.Auto)> _
    Friend Shared Function GetWindowRect(ByVal hWnd As IntPtr, <[In](), Out()> ByRef rect As Rectangle) As Boolean
    End Function

    <DllImport("user32.dll", ExactSpelling:=True, CharSet:=CharSet.Auto)> _
    Friend Shared Function GetParent(ByVal hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("ole32.dll")> _
    Private Shared Function OleSetContainedObject(<MarshalAs(UnmanagedType.IUnknown)> ByVal pUnk As Object, ByVal fContained As Boolean) As Integer
    End Function

    <DllImport("ole32.dll")> _
    Private Shared Function OleLoadPicturePath(<MarshalAs(UnmanagedType.LPWStr)> ByVal lpszPicturePath As String, <MarshalAs(UnmanagedType.IUnknown)> <[In]()> ByVal pIUnknown As Object, ByVal dwReserved As UInteger, ByVal clrReserved As UInteger, ByRef riid As Guid, <MarshalAs(UnmanagedType.IUnknown)> ByRef ppvObj As Object) As Integer
    End Function

    <DllImport("ole32.dll")> _
    Private Shared Function OleCreateFromFile(<[In]()> ByRef rclsid As Guid, <MarshalAs(UnmanagedType.LPWStr)> ByVal lpszFileName As String, <[In]()> ByRef riid As Guid, ByVal renderopt As UInteger, ByRef pFormatEtc As FORMATETC, ByVal pClientSite As IOleClientSite, _
     ByVal pStg As IStorage, <MarshalAs(UnmanagedType.IUnknown)> ByRef ppvObj As Object) As Integer
    End Function

    <DllImport("ole32.dll")> _
    Private Shared Function OleCreateFromData(ByVal pSrcDataObj As IDataObject, <[In]()> ByRef riid As Guid, ByVal renderopt As UInteger, ByRef pFormatEtc As FORMATETC, ByVal pClientSite As IOleClientSite, ByVal pStg As IStorage, _
     <MarshalAs(UnmanagedType.IUnknown)> ByRef ppvObj As Object) As Integer
    End Function

    <DllImport("ole32.dll")> _
    Private Shared Function OleCreateStaticFromData(<MarshalAs(UnmanagedType.[Interface])> ByVal pSrcDataObj As IDataObject, <[In]()> ByRef riid As Guid, ByVal renderopt As UInteger, ByRef pFormatEtc As FORMATETC, ByVal pClientSite As IOleClientSite, ByVal pStg As IStorage, _
     <MarshalAs(UnmanagedType.IUnknown)> ByRef ppvObj As Object) As Integer
    End Function

    <DllImport("ole32.dll")> _
    Private Shared Function OleCreateLinkFromData(<MarshalAs(UnmanagedType.[Interface])> ByVal pSrcDataObj As IDataObject, <[In]()> ByRef riid As Guid, ByVal renderopt As UInteger, ByRef pFormatEtc As FORMATETC, ByVal pClientSite As IOleClientSite, ByVal pStg As IStorage, _
     <MarshalAs(UnmanagedType.IUnknown)> ByRef ppvObj As Object) As Integer
    End Function

#End Region



    ''' <summary>
    ''' Setzt die Schriftart für den selektierten Text
    ''' </summary>
    ''' <param name="face">Name der Schriftart</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionFont(ByVal face As String) As Boolean
      Dim cf As New STRUCT_CHARFORMAT()
      cf.cbSize = Marshal.SizeOf(cf)
      cf.dwMask = Convert.ToUInt32(CFM_FACE)

      ' ReDim face name to relevant size
      ReDim cf.szFaceName(32)
      face.CopyTo(0, cf.szFaceName, 0, Math.Min(31, face.Length))

      Dim lParam As IntPtr
      lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
      Marshal.StructureToPtr(cf, lParam, False)

      Dim res As Integer
      res = SendMessage(Me.Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam)
      Return (res = 0)
    End Function

    ''' <summary>
    ''' Setzt die Schriftgrösse des selektierten Textees
    ''' </summary>
    ''' <param name="size">Schriftgrösse</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionSize(ByVal size As Integer) As Boolean
      Dim cf As New STRUCT_CHARFORMAT()
      cf.cbSize = Marshal.SizeOf(cf)
      cf.dwMask = Convert.ToUInt32(CFM_SIZE)
      ' yHeight is in 1/20 pt
      cf.yHeight = size * 20

      Dim lParam As IntPtr
      lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
      Marshal.StructureToPtr(cf, lParam, False)

      Dim res As Integer
      res = SendMessage(Me.Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam)
      Return (res = 0)
    End Function

    ''' <summary>
    ''' Setzt Fettdruck für selektierten Text oder hebt diesen auf
    ''' </summary>
    ''' <param name="bold">True = Fettdruck; False = normal</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionBold(ByVal bold As Boolean) As Boolean
      If (bold) Then
        Return SetSelectionStyle(CFM_BOLD, CFE_BOLD)
      Else
        Return SetSelectionStyle(CFM_BOLD, 0)
      End If
    End Function

    ''' <summary>
    ''' Kurssiv-Schrift für selektierten Text setzen oder aufheben
    ''' </summary>
    ''' <param name="italic">True = kursiv; False = nicht kursiv</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionItalic(ByVal italic As Boolean) As Boolean
      If (italic) Then
        Return SetSelectionStyle(CFM_ITALIC, CFE_ITALIC)
      Else
        Return SetSelectionStyle(CFM_ITALIC, 0)
      End If
    End Function

    ''' <summary>
    ''' Unterstreicht den selektierten Text oder hebt diese auf
    ''' </summary>
    ''' <param name="underlined">True = unterstrichen; 
    ''' False = nicht unterstrichen</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionUnderlined(ByVal underlined As Boolean) As Boolean
      If (underlined) Then
        Return SetSelectionStyle(CFM_UNDERLINE, CFE_UNDERLINE)
      Else
        Return SetSelectionStyle(CFM_UNDERLINE, 0)
      End If
    End Function

    Private Function SetSelectionStyle(ByVal mask As Int32, ByVal effect As Int32) As Boolean
      Dim cf As New STRUCT_CHARFORMAT()
      cf.cbSize = Marshal.SizeOf(cf)
      cf.dwMask = Convert.ToUInt32(mask)
      cf.dwEffects = Convert.ToUInt32(effect)

      Dim lParam As IntPtr
      lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
      Marshal.StructureToPtr(cf, lParam, False)

      Dim res As Integer
      res = SendMessage(Me.Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam)
      Return (res = 0)
    End Function

    Public Sub InsertOleObject(ByVal oleObj As IOleObject)
      Dim ole As New RichEditOle(Me)
      ole.InsertOleObject(oleObj)
    End Sub

    Public Sub InsertControl(ByVal control As Control)
      Dim ole As New RichEditOle(Me)
      ole.InsertControl(control)
    End Sub

    Public Sub InsertMyDataObject(ByVal mdo As myDataObject, ByVal insertAtCharPos As Integer)
      Dim ole As New RichEditOle(Me)
      ole.InsertMyDataObject(mdo, insertAtCharPos)
    End Sub

    Public Sub UpdateObjects()
      Dim ole As New RichEditOle(Me)
      ole.UpdateObjects()
    End Sub

    Public Sub InsertImage(ByVal image As Image, ByVal insertAtCharPos As Integer)
      Dim mdo As New myDataObject()

      mdo.SetImage(image)

      Me.InsertMyDataObject(mdo, insertAtCharPos)
    End Sub

    Public Sub InsertImage(ByVal imageFile As String, ByVal insertAtCharPos As Integer)
      Dim mdo As New myDataObject()

      mdo.SetImage(imageFile)

      Me.InsertMyDataObject(mdo, insertAtCharPos)
    End Sub

    Public Sub InsertImageFromFile(ByVal strFilename As String, ByVal insertAtCharPos As Integer)
      Dim ole As New RichEditOle(Me)
      ole.InsertImageFromFile(strFilename, insertAtCharPos)
    End Sub

    Public Sub InsertActiveX(ByVal strProgID As String)
      Dim t As Type = Type.GetTypeFromProgID(strProgID)
      If t Is Nothing Then
        Return
      End If

      Dim o As Object = System.Activator.CreateInstance(t)

      Dim b As Boolean = (TypeOf o Is IOleObject)

      If b Then
        Me.InsertOleObject(DirectCast(o, IOleObject))
      End If
    End Sub

    Public Function InsertObjectDialog() As struct_OLEUIINSERTOBJECT
      Dim ole As New RichEditOle(Me)
      Return ole.InsertObjectDialog
    End Function

    '#define IOF_SHOWHELP 1
    '#define IOF_SELECTCREATENEW 2
    '#define IOF_SELECTCREATEFROMFILE 4
    '#define IOF_CHECKLINK 8
    '#define IOF_CHECKDISPLAYASICON 16
    '#define IOF_CREATENEWOBJECT 32
    '#define IOF_CREATEFILEOBJECT 64
    '#define IOF_CREATELINKOBJECT 128
    '#define IOF_DISABLELINK 256
    '#define IOF_VERIFYSERVERSEXIST 512
    '#define IOF_DISABLEDISPLAYASICON 1024
    '#define IOF_HIDECHANGEICON 2048
    '#define IOF_SHOWINSERTCONTROL 4096
    '#define IOF_SELECTCREATECONTROL 8192

    'Definitions:
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Public Structure struct_OLEUIINSERTOBJECT
      ' These IN fields are standard across all OLEUI dialog box functions.
      Public cbStruct As Integer
      Public dwFlags As Integer
      Public hWndOwner As IntPtr
      Public lpszCaption As String
      Public lpfnHook As IntPtr
      Public lCustData As IntPtr
      Public hInstance As IntPtr
      Public lpszTemplate As String
      Public hResource As IntPtr
      Public clsid As Guid

      ' Specifics for OLEUIINSERTOBJECT.
      Public lpszFile As String
      Public cchFile As Integer
      Public cClsidExclude As Integer
      Public lpClsidExclude As IntPtr
      Public iid As Guid

      ' Specific to create objects if flags say so
      Public oleRender As Integer

      '<MarshalAs(UnmanagedType.LPStruct)> _
      'Public lpFormatEtc As FORMATETC
      Public lpFormatEtc As IntPtr

      'Public lpIOleClientSite As IntPtr
      'Public lpIStorage As IntPtr
      Public lpIOleClientSite As IOleClientSite
      Public lpIStorage As IStorage

      Public ppvObj As IntPtr

      Public sc As Integer
      Public hMetaPict As IntPtr
    End Structure

    '    typedef struct tagFORMATETC {
    '  CLIPFORMAT     cfFormat;
    '  DVTARGETDEVICE *ptd;
    '  DWORD          dwAspect;
    '  LONG           lindex;
    '  DWORD          tymed;
    '} FORMATETC, *LPFORMATETC;

    <DllImport("oledlg.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function OleUIInsertObject(<[In](), Out()> ByRef lpIO As struct_OLEUIINSERTOBJECT) As Integer
    End Function


    ' RichEditOle wrapper and helper
    Private Class RichEditOle
      Public Const WM_USER As Integer = &H400
      Public Const EM_GETOLEINTERFACE As Integer = WM_USER + 60

      Private _richEdit As MyExtRichTextBox
      Private _RichEditOle As IRichEditOle

      Public Sub New(ByVal richEdit As MyExtRichTextBox)
        Me._richEdit = richEdit
      End Sub

      Public ReadOnly Property IRichEditOle() As IRichEditOle
        Get
          If Me._RichEditOle Is Nothing Then
            Me._RichEditOle = SendMessage(Me._richEdit.Handle, EM_GETOLEINTERFACE, 0)
          End If

          Return Me._RichEditOle
        End Get
      End Property


      <DllImport("ole32.dll", PreserveSig:=False)> _
      Friend Shared Function CreateILockBytesOnHGlobal(ByVal hGlobal As IntPtr, ByVal fDeleteOnRelease As Boolean, <Out()> ByRef ppLkbyt As ILockBytes) As Integer
      End Function

      <DllImport("ole32.dll")> _
      Private Shared Function StgCreateDocfileOnILockBytes(ByVal plkbyt As ILockBytes, ByVal grfMode As UInteger, ByVal reserved As UInteger, ByRef ppstgOpen As IStorage) As Integer
      End Function


      Public Function InsertObjectDialog() As struct_OLEUIINSERTOBJECT

        Dim IID_IOleObject As New Guid("{00000112-0000-0000-C000-000000000046}")

        '-----------------------
        Dim pLockBytes As ILockBytes
        CreateILockBytesOnHGlobal(IntPtr.Zero, True, pLockBytes)

        Dim pStorage As IStorage
        StgCreateDocfileOnILockBytes(pLockBytes, CUInt(STGM.STGM_SHARE_EXCLUSIVE Or STGM.STGM_CREATE Or STGM.STGM_READWRITE), 0, pStorage)

        Dim pOleClientSite As IOleClientSite
        Me.IRichEditOle.GetClientSite(pOleClientSite)
        '-----------------------


        'Call the method:
        Dim io As New struct_OLEUIINSERTOBJECT()


        io.ppvObj = Marshal.AllocHGlobal(4)
        io.iid = IID_IOleObject
        io.cbStruct = Marshal.SizeOf(io)
        io.hWndOwner = _richEdit.Handle
        io.dwFlags = &H2 + 32 + 64 + 128
        io.lpszFile = New [String](ControlChars.NullChar, 256)
        io.cchFile = io.lpszFile.Length

        Dim FormatEtcStruct As New FORMATETC
        FormatEtcStruct.cfFormat = 0
        FormatEtcStruct.ptd = IntPtr.Zero
        FormatEtcStruct.dwAspect = DVASPECT.DVASPECT_CONTENT
        FormatEtcStruct.lindex = -1
        FormatEtcStruct.tymed = TYMED.TYMED_NULL

        io.lpFormatEtc = Marshal.AllocHGlobal(Marshal.SizeOf(FormatEtcStruct))
        Marshal.StructureToPtr(FormatEtcStruct, io.lpFormatEtc, False)
        ' io.lpIOleClientSite = Marshal.GetIUnknownForObject(pOleClientSite)
        ' io.lpIStorage = Marshal.GetIUnknownForObject(pStorage)
        io.oleRender = CUInt(OLERENDER.OLERENDER_DRAW)
        io.lpIOleClientSite = pOleClientSite
        io.lpIStorage = pStorage
        'io.lpFormatEtc = New FORMATETC

        Dim ret As Integer = _
          OleUIInsertObject(io)

        Dim oleObjectPtr As Integer = Marshal.ReadInt32(io.ppvObj)
        Marshal.FreeHGlobal(io.lpFormatEtc)

        '-----------------------
        If ret <> 2 Then
          Dim pOleObject As IOleObject = DirectCast(Marshal.GetObjectForIUnknown(oleObjectPtr), IOleObject)
          '-----------------------


          '-----------------------
          Dim guid As New Guid()

          'guid = Marshal.GenerateGuidForType(pOleObject.GetType());
          pOleObject.GetUserClassID(guid)
          Debug.Print(guid.ToString)
          Debug.Print(io.clsid.ToString)
          Debug.Print(io.iid.ToString)
          '-----------------------

          '-----------------------
          OleSetContainedObject(pOleObject, True)

          Dim reoObject As New REOBJECT()

          reoObject.cp = Me._richEdit.SelectionStart

          reoObject.clsid = guid
          reoObject.pstg = pStorage
          reoObject.poleobj = Marshal.GetIUnknownForObject(pOleObject)
          reoObject.polesite = pOleClientSite
          reoObject.dvAspect = CUInt(DVASPECT.DVASPECT_CONTENT)
          reoObject.dwFlags = CUInt(REOOBJECTFLAGS.REO_BELOWBASELINE Or REOOBJECTFLAGS.REO_RESIZABLE)
          reoObject.dwUser = 0

          Me.IRichEditOle.InsertObject(reoObject)
          '-----------------------
        End If

        '-----------------------
        Marshal.ReleaseComObject(pLockBytes)
        Marshal.ReleaseComObject(pOleClientSite)
        Marshal.ReleaseComObject(pStorage)
        '-----------------------

        Return io
      End Function


      Public Sub InsertControl(ByVal control As Control)
        If control Is Nothing Then
          Return
        End If

        Dim guid As Guid = Marshal.GenerateGuidForType(control.[GetType]())

        '-----------------------
        Dim pLockBytes As ILockBytes
        CreateILockBytesOnHGlobal(IntPtr.Zero, True, pLockBytes)

        Dim pStorage As IStorage
        StgCreateDocfileOnILockBytes(pLockBytes, CUInt(STGM.STGM_SHARE_EXCLUSIVE Or STGM.STGM_CREATE Or STGM.STGM_READWRITE), 0, pStorage)

        Dim pOleClientSite As IOleClientSite
        Me.IRichEditOle.GetClientSite(pOleClientSite)
        '-----------------------

        '-----------------------
        Dim reoObject As New REOBJECT()

        reoObject.cp = Me._richEdit.SelectionStart

        reoObject.clsid = guid
        reoObject.pstg = pStorage
        reoObject.poleobj = Marshal.GetIUnknownForObject(control)
        reoObject.polesite = pOleClientSite
        reoObject.dvAspect = CUInt(DVASPECT.DVASPECT_CONTENT)
        reoObject.dwFlags = CUInt(REOOBJECTFLAGS.REO_BELOWBASELINE Or REOOBJECTFLAGS.REO_RESIZABLE)
        reoObject.dwUser = 1

        Me.IRichEditOle.InsertObject(reoObject)
        '-----------------------

        '-----------------------
        Marshal.ReleaseComObject(pLockBytes)
        Marshal.ReleaseComObject(pOleClientSite)
        Marshal.ReleaseComObject(pStorage)
        '-----------------------
      End Sub

      Public Function InsertImageFromFile(ByVal strFilename As String, ByVal charPos As Integer) As Boolean
        '-----------------------
        Dim pLockBytes As ILockBytes
        CreateILockBytesOnHGlobal(IntPtr.Zero, True, pLockBytes)

        Dim pStorage As IStorage
        StgCreateDocfileOnILockBytes(pLockBytes, CUInt(STGM.STGM_SHARE_EXCLUSIVE Or STGM.STGM_CREATE Or STGM.STGM_READWRITE), 0, pStorage)

        Dim pOleClientSite As IOleClientSite
        Me.IRichEditOle.GetClientSite(pOleClientSite)
        '-----------------------


        '-----------------------
        Dim formatEtc As New FORMATETC()

        formatEtc.cfFormat = 0
        formatEtc.ptd = IntPtr.Zero
        formatEtc.dwAspect = DVASPECT.DVASPECT_CONTENT
        formatEtc.lindex = -1
        formatEtc.tymed = TYMED.TYMED_NULL

        Dim IID_IOleObject As New Guid("{00000112-0000-0000-C000-000000000046}")
        Dim CLSID_NULL As New Guid("{00000000-0000-0000-0000-000000000000}")

        Dim pOleObjectOut As Object

        ' I don't sure, but it appears that this function only loads from bitmap
        ' You can also try OleCreateFromData, OleLoadPictureIndirect, etc.
        Dim hr As Integer = OleCreateFromFile(CLSID_NULL, strFilename, IID_IOleObject, CUInt(OLERENDER.OLERENDER_DRAW), formatEtc, pOleClientSite, _
         pStorage, pOleObjectOut)

        If pOleObjectOut Is Nothing Then
          Marshal.ReleaseComObject(pLockBytes)
          Marshal.ReleaseComObject(pOleClientSite)
          Marshal.ReleaseComObject(pStorage)

          Return False
        End If

        Dim pOleObject As IOleObject = DirectCast(pOleObjectOut, IOleObject)
        '-----------------------


        '-----------------------
        Dim guid As New Guid()

        'guid = Marshal.GenerateGuidForType(pOleObject.GetType());
        pOleObject.GetUserClassID(guid)
        '-----------------------

        '-----------------------
        OleSetContainedObject(pOleObject, True)

        Dim reoObject As New REOBJECT()

        reoObject.cp = charPos 'Me._richEdit.TextLength

        reoObject.clsid = guid
        reoObject.pstg = pStorage
        reoObject.poleobj = Marshal.GetIUnknownForObject(pOleObject)
        reoObject.polesite = pOleClientSite
        reoObject.dvAspect = CUInt(DVASPECT.DVASPECT_CONTENT)
        reoObject.dwFlags = CUInt(REOOBJECTFLAGS.REO_BELOWBASELINE)
        reoObject.dwUser = 0

        Me.IRichEditOle.InsertObject(reoObject)
        '-----------------------

        '-----------------------
        Marshal.ReleaseComObject(pLockBytes)
        Marshal.ReleaseComObject(pOleClientSite)
        Marshal.ReleaseComObject(pStorage)
        Marshal.ReleaseComObject(pOleObject)
        '-----------------------

        Return True
      End Function

      Public Sub InsertMyDataObject(ByVal mdo As myDataObject, ByVal charPos As Integer)
        If mdo Is Nothing Then
          Return
        End If

        '-----------------------
        Dim pLockBytes As ILockBytes
        Dim sc As Integer = CreateILockBytesOnHGlobal(IntPtr.Zero, True, pLockBytes)

        Dim pStorage As IStorage
        sc = StgCreateDocfileOnILockBytes(pLockBytes, CUInt(STGM.STGM_SHARE_EXCLUSIVE Or STGM.STGM_CREATE Or STGM.STGM_READWRITE), 0, pStorage)

        Dim pOleClientSite As IOleClientSite
        Me.IRichEditOle.GetClientSite(pOleClientSite)
        '-----------------------

        Dim guid As Guid = Marshal.GenerateGuidForType(mdo.[GetType]())

        Dim IID_IOleObject As New Guid("{00000112-0000-0000-C000-000000000046}")
        Dim IID_IDataObject As New Guid("{0000010e-0000-0000-C000-000000000046}")
        Dim IID_IUnknown As New Guid("{00000000-0000-0000-C000-000000000046}")

        Dim pOleObject As Object

        Dim hr As Integer = OleCreateStaticFromData(mdo, IID_IOleObject, CUInt(OLERENDER.OLERENDER_FORMAT), mdo.mpFormatetc, pOleClientSite, pStorage, _
         pOleObject)

        If pOleObject Is Nothing Then
          Return
        End If
        '-----------------------


        '-----------------------
        OleSetContainedObject(pOleObject, True)

        Dim reoObject As New REOBJECT()

        reoObject.cp = charPos 'Me._richEdit.TextLength

        reoObject.clsid = guid
        reoObject.pstg = pStorage
        reoObject.poleobj = Marshal.GetIUnknownForObject(pOleObject)
        reoObject.polesite = pOleClientSite
        reoObject.dvAspect = CUInt(DVASPECT.DVASPECT_CONTENT)
        reoObject.dwFlags = CUInt(REOOBJECTFLAGS.REO_BELOWBASELINE)
        reoObject.dwUser = 0

        Me.IRichEditOle.InsertObject(reoObject)
        '-----------------------

        '-----------------------
        Marshal.ReleaseComObject(pLockBytes)
        Marshal.ReleaseComObject(pOleClientSite)
        Marshal.ReleaseComObject(pStorage)
        Marshal.ReleaseComObject(pOleObject)
        '-----------------------
      End Sub

      Public Sub InsertOleObject(ByVal oleObject As IOleObject)
        If oleObject Is Nothing Then
          Return
        End If

        '-----------------------
        Dim pLockBytes As ILockBytes
        CreateILockBytesOnHGlobal(IntPtr.Zero, True, pLockBytes)

        Dim pStorage As IStorage
        StgCreateDocfileOnILockBytes(pLockBytes, CUInt(STGM.STGM_SHARE_EXCLUSIVE Or STGM.STGM_CREATE Or STGM.STGM_READWRITE), 0, pStorage)

        Dim pOleClientSite As IOleClientSite
        Me.IRichEditOle.GetClientSite(pOleClientSite)
        '-----------------------

        '-----------------------
        Dim guid As New Guid()

        oleObject.GetUserClassID(guid)
        '-----------------------

        '-----------------------
        OleSetContainedObject(oleObject, True)

        Dim reoObject As New REOBJECT()

        reoObject.cp = Me._richEdit.SelectionStart

        reoObject.clsid = guid
        reoObject.pstg = pStorage
        reoObject.poleobj = Marshal.GetIUnknownForObject(oleObject)
        reoObject.polesite = pOleClientSite
        reoObject.dvAspect = CUInt(DVASPECT.DVASPECT_CONTENT)
        reoObject.dwFlags = CUInt(REOOBJECTFLAGS.REO_BELOWBASELINE)

        Me.IRichEditOle.InsertObject(reoObject)
        '-----------------------

        '-----------------------
        Marshal.ReleaseComObject(pLockBytes)
        Marshal.ReleaseComObject(pOleClientSite)
        Marshal.ReleaseComObject(pStorage)
        '-----------------------
      End Sub

      Public Sub UpdateObjects()
        Dim k As Integer = Me.IRichEditOle.GetObjectCount()

        For i As Integer = 0 To k - 1
          Dim reoObject As New REOBJECT()

          Me.IRichEditOle.GetObject(i, reoObject, GETOBJECTOPTIONS.REO_GETOBJ_ALL_INTERFACES)

          If reoObject.dwUser = 1 Then
            Dim pt As Point = Me._richEdit.GetPositionFromCharIndex(reoObject.cp)
            Dim rect As New Rectangle(pt, reoObject.sizel)

            ' repaint
            Me._richEdit.Invalidate(rect, False)
          End If
        Next
      End Sub
    End Class
  End Class

  Module sys_rtfAPI
#Region "Api für rtfBox"
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure STRUCT_CHARFORMAT
      Public cbSize As Integer
      Public dwMask As UInt32
      Public dwEffects As UInt32
      Public yHeight As Int32
      Public yOffset As Int32
      Public crTextColor As Int32
      Public bCharSet As Byte
      Public bPitchAndFamily As Byte
      <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
      Public szFaceName() As Char
    End Structure

    ' Defines for STRUCT_CHARFORMAT member dwMask
    ' (Long because UInt32 is not an intrinsic type)
    Public Const CFM_BOLD As Long = &H1&
    Public Const CFM_ITALIC As Long = &H2&
    Public Const CFM_UNDERLINE As Long = &H4&
    Public Const CFM_STRIKEOUT As Long = &H8&
    Public Const CFM_PROTECTED As Long = &H10&
    Public Const CFM_LINK As Long = &H20&
    Public Const CFM_SIZE As Long = &H80000000&
    Public Const CFM_COLOR As Long = &H40000000&
    Public Const CFM_FACE As Long = &H20000000&
    Public Const CFM_OFFSET As Long = &H10000000&
    Public Const CFM_CHARSET As Long = &H8000000&

    ' Defines for STRUCT_CHARFORMAT member dwEffects
    Public Const CFE_BOLD As Long = &H1&
    Public Const CFE_ITALIC As Long = &H2&
    Public Const CFE_UNDERLINE As Long = &H4&
    Public Const CFE_STRIKEOUT As Long = &H8&
    Public Const CFE_PROTECTED As Long = &H10&
    Public Const CFE_LINK As Long = &H20&
    Public Const CFE_AUTOCOLOR As Long = &H40000000&

    ' Windows Message defines
    Public Const WM_USER As Int32 = &H400&
    Public Const EM_FORMATRANGE As Int32 = WM_USER + 57
    Public Const EM_GETCHARFORMAT As Int32 = WM_USER + 58
    Public Const EM_SETCHARFORMAT As Int32 = WM_USER + 68
    ' Defines for EM_GETCHARFORMAT/EM_SETCHARFORMAT
    Public SCF_SELECTION As Int32 = &H1&
    Public SCF_WORD As Int32 = &H2&
    Public SCF_ALL As Int32 = &H4&

#End Region
  End Module


End Namespace
