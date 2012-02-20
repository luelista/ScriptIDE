Imports System.Reflection

Module app_scriptRHost

  Public glob As New cls_globPara(ParaService.SettingsFolder + "scRuntimeHost.para.txt")

  Public classes As New Dictionary(Of String, ScriptHost.ScriptHost)
  Public classListChanged As Boolean

  Sub loadScript(ByVal dllFile As String)
    Dim ass = Assembly.LoadFrom(dllFile)

    Dim clsName = IO.Path.GetFileNameWithoutExtension(dllFile)
    Dim oldInst As ScriptHost.ScriptHost
    If classes.TryGetValue(LCase(clsName), oldInst) = True Then
      oldInst.Dispose()
    End If

    ' Initialize ScriptHelper
    Dim myZZ As New ScriptHost.cls_scriptHelper()
    myZZ._scriptClassName = clsName
    myZZ._scriptFilespec = dllFile

    Dim inst = ass.CreateInstance(clsName, True, BindingFlags.Default, Nothing, New Object() {myZZ, Nothing}, Nothing, Nothing)
    Dim sH As New ScriptHost.ScriptHost(RuntimeMode.HostedDebug, clsName, inst)
    myZZ._scriptInst = New WeakReference(inst)
    myZZ.sH = sH

    classes(LCase(clsName)) = sH
    classListChanged = True

    inst.AutoStart()
  End Sub


End Module
