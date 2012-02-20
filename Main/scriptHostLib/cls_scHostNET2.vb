Imports System.Text.RegularExpressions
Imports System.Reflection

Public Class scHostNET2
  Implements IScriptClassHost

  Dim preProc As New cls_preprocVB2()

  Dim mFileSpec As String
  Dim mLastModified As Date
  Dim mRef As Object
  Dim mIniDone As Boolean
  Dim mScriptHelper As Object
  Dim mCompilerresult As CodeDom.Compiler.CompilerResults
  Dim mAssembly As Assembly
  Dim mLineCount As Integer
  Dim fileExt, scriptText, referenceFile As String
  Dim fileNames() As String
  Dim mCacheDllFileSpec As String
  Dim mAssemblyFileSpec As String


  '' debug oder compile(release/test)
  '' debug:  bei intern: klasse kompilieren und laden      bei extern: mit interproc
  '' !debug: bei intern: klasse kompilieren (syntaxCheck)  bei extern: als eig. programm
  '' ob int. oder ext. ermittelt der preProc
  'Public IsDebugMode As Boolean

  Shared Sub InitializePrecompiler(ByVal preProc As cls_preprocVB2, ByVal fileSpec As String, ByVal debugMode As Integer)
    preProc.initDefaultReferences()
    preProc.initScriptcompiler(fileSpec, debugMode)
    If preProc.targetRuntime = RuntimeMode.IDE Or preProc.targetRuntime = RuntimeMode.HostedDebug Then
      preProc.DebugMode = cls_preprocVB2.PPD_DEBUG
    Else

    End If
    preProc.injectCodeHelperForAll() 'ACHTUNG: hier wird auch die MethodList generiert

    For Each winData In preProc.winData
      Dim fcfileSpec = ZZ.IDEHelper.GetSettingsFolder() + "temp\formCode_" + preProc.className + "_" + Regex.Match(winData, "#WindowData ([a-zA-Z0-9_]*)").Groups(1).Value + ".vb"
      IO.File.WriteAllText(fcfileSpec, compileFormCode(winData, preProc.globMethodList, 200))
      preProc.AddFile(fcfileSpec, 0, cls_preprocVB2.PPC_NOINCETION, cls_preprocVB2.PPT_MODULE)
    Next

    If preProc.targetRuntime = RuntimeMode.IDE Then
      preProc.referenceList("scripthostlib.dll") = "scriptHostLib.dll"
    Else
      preProc.referenceList("sh06_run.dll") = "sh06_run.dll"
    End If
  End Sub

  Sub New(ByVal fileSpec As String, ByVal debugMode As Integer)
    Try
      mFileSpec = fileSpec
      TT.Write("New scHostNET2", fileSpec, "ini")

      mLastModified = IO.File.GetLastWriteTime(fileSpec)

      InitializePrecompiler(preProc, fileSpec, debugMode)

      mCompilerresult = preProc.runCompiler()
      If processCompileErrors(preProc.className, mCompilerresult) Then
        'MsgBox("COMPILER-FEHLER aufgetreten!")
        Exit Sub
      End If
      mAssemblyFileSpec = mCompilerresult.PathToAssembly

      Select Case preProc.targetRuntime
        Case RuntimeMode.IDE
          '--> intern gehostet
          mAssembly = mCompilerresult.CompiledAssembly
          initClassRef()

        Case RuntimeMode.Debug
          '--> extern debuggen


        Case Else

          MsgBox("Nicht unterstützt")
          Stop
      End Select


    Catch ex As Exception
      processCompileException(ex)
    End Try

  End Sub

  Sub initClassRef()
    Try
      If preProc.para("MultiInstance", "False") = "False" Then
        Dim hlp As New cls_scriptHelper()
        hlp._scriptClassName = className
        hlp._scriptFilespec = mFileSpec
        If preProc.para("SilentMode", "False") <> "False" Then hlp._isLocalSilentMode = True

        mRef = mAssembly.CreateInstance("ScriptClass." + preProc.className, True, Reflection.BindingFlags.Default, Nothing, New Object() {hlp, Nothing}, Nothing, Nothing)
        hlp._scriptInst = New WeakReference(mRef)

        'mRef.zz_setHlpObject(hlp)
        'HACK - Zeilenzahl 100000
        mRef.zz_BBreset(99999)
      End If
      mIniDone = True
    Catch ex As Exception
      processCompileException(ex)
      Exit Sub
    End Try
  End Sub


  Public Function getAddInDataXmlReader() As Xml.XmlTextReader
    Dim xmlText As String = preProc.addinData
    Dim nt As New Xml.NameTable()
    Dim ns As New Xml.XmlNamespaceManager(nt)
    Dim pc As New Xml.XmlParserContext(nt, ns, "de", Xml.XmlSpace.Default)
    Return New Xml.XmlTextReader(xmlText, Xml.XmlNodeType.Element, pc)
  End Function


  Public ReadOnly Property assemblyRef() As System.Reflection.Assembly Implements IScriptClassHost.assemblyRef
    Get
      Return mAssembly
    End Get
  End Property

  Public Property className() As String Implements IScriptClassHost.className
    Get
      Return preProc.className
    End Get
    Set(ByVal value As String)
      Stop
    End Set
  End Property

  Public Property debugMode() As RuntimeMode Implements IScriptClassHost.debugMode
    Get
      Return preProc.targetRuntime
    End Get
    Set(ByVal value As RuntimeMode)
      Stop
    End Set
  End Property

  Public ReadOnly Property fileSpec() As String Implements IScriptClassHost.fileSpec
    Get
      Return mFileSpec
    End Get
  End Property

  Public Function getClassRef() As Object Implements IScriptClassHost.getClassRef
    Return mRef
  End Function

  Public Function getNewClassRef(ByVal parentRef As System.WeakReference) As Object Implements IScriptClassHost.getNewClassRef
    Stop
  End Function

  Public Function getScriptHelper() As Object Implements IScriptClassHost.getScriptHelper
    Stop
  End Function

  Public Sub initScriptHost() Implements IScriptClassHost.initScriptHost
    ' Stop
  End Sub

  Public Function checkForNewerFile() As Boolean Implements IScriptClassHost.checkForNewerFile
    If mIsInvalid Then Return True
    Dim modDat = IO.File.GetLastWriteTime(mFileSpec)
    If modDat <= mLastModified Then
      Return False
    Else
      If MsgBox("Der Code der folgende ScriptClass wurde seit dem letzten Kompilieren verändert. Soll die Klasse neu erstellt werden?" + vbNewLine + vbNewLine + "Pfad: " + mFileSpec + vbNewLine + "Zuletzt kompiliert: " + mLastModified.ToShortDateString + " " + mLastModified.ToLongTimeString + vbNewLine + "Änderungsdatum der Datei: " + modDat, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Script-Class-Host scHostNET2") = MsgBoxResult.No Then
        mLastModified = modDat
        Return False
      End If
    End If
    Return True
  End Function

  Private mIsInvalid As Boolean
  Public Sub invalidate() Implements IScriptClassHost.invalidate
    mIsInvalid = True
  End Sub


  Public Function isIniDone() As Boolean Implements IScriptClassHost.isIniDone
    Return mIniDone
  End Function

  Public ReadOnly Property lastModified() As Date Implements IScriptClassHost.lastModified
    Get
      Return mLastModified
    End Get
  End Property

  Public ReadOnly Property scriptClassName() As String Implements IScriptClassHost.scriptClassName
    Get
      Return preProc.scriptClassName
    End Get
  End Property

  Public ReadOnly Property scriptPara() As ScriptParameters Implements IScriptClassHost.scriptPara
    Get
      Stop
    End Get
  End Property

  Public Function targetFolder() As String Implements IScriptClassHost.targetFolder
    Stop
  End Function

  Public Sub terminateScript() Implements IScriptClassHost.terminateScript
    On Error Resume Next
    mRef.onTerminate()
    mRef.Dispose()
  End Sub

  Public ReadOnly Property assemblyFilespec() As String Implements IScriptClassHost.assemblyFilespec
    Get
      Return mAssemblyFileSpec
    End Get
  End Property
End Class
