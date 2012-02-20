' <file>
'     <copyright see="prj:///doc/copyright.txt"/>
'     <license see="prj:///doc/license.txt"/>
'     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
'     <version>$Revision: 1624 $</version>
' </file>

Imports System.Runtime.Serialization

''' <summary>
''' Exception used when loading an AddIn fails.
''' </summary>
<Serializable()> _
Public Class AddInLoadException
  Inherits Exception
  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal message As String)
    MyBase.New(message)
  End Sub

  Public Sub New(ByVal message As String, ByVal innerException As Exception)
    MyBase.New(message, innerException)
  End Sub

  Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
    MyBase.New(info, context)
  End Sub
End Class
