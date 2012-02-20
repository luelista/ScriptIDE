'Except where stated all code and programs in this project are the copyright of Jim Blackler, 2008.
'jimblackler@gmail.com
'
'This is free software. Libraries and programs are distributed under the terms of the GNU Lesser
'General Public License. Please see the files COPYING and COPYING.LESSER.

Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Xml

Namespace JimBlackler.DocsByReflection
	''' <summary>
	''' Utility class to provide documentation for various types where available with the assembly
	''' </summary>
	Public Class DocsByReflection
		''' <summary>
		''' Provides the documentation comments for a specific method
		''' </summary>
		''' <param name="methodInfo">The MethodInfo (reflection data ) of the member to find documentation for</param>
		''' <returns>The XML fragment describing the method</returns>
		Public Shared Function XMLFromMember(methodInfo As MethodInfo) As XmlElement
			' Calculate the parameter string as this is in the member name in the XML
			Dim parametersString As String = ""
			For Each parameterInfo As ParameterInfo In methodInfo.GetParameters()
				If parametersString.Length > 0 Then
					parametersString += ","
				End If

				parametersString += parameterInfo.ParameterType.FullName
			Next

			'AL: 15.04.2008 ==> BUG-FIX remove �()� if parametersString is empty
			If parametersString.Length > 0 Then
				Return XMLFromName(methodInfo.DeclaringType, "M"C, methodInfo.Name & "(" & parametersString & ")")
			Else
				Return XMLFromName(methodInfo.DeclaringType, "M"C, methodInfo.Name)
			End If
		End Function

		''' <summary>
		''' Provides the documentation comments for a specific member
		''' </summary>
		''' <param name="memberInfo">The MemberInfo (reflection data) or the member to find documentation for</param>
		''' <returns>The XML fragment describing the member</returns>
		Public Shared Function XMLFromMember(memberInfo As MemberInfo) As XmlElement
			' First character [0] of member type is prefix character in the name in the XML
			Return XMLFromName(memberInfo.DeclaringType, memberInfo.MemberType.ToString()(0), memberInfo.Name)
		End Function

		''' <summary>
		''' Provides the documentation comments for a specific type
		''' </summary>
		''' <param name="type">Type to find the documentation for</param>
		''' <returns>The XML fragment that describes the type</returns>
		Public Shared Function XMLFromType(type As Type) As XmlElement
			' Prefix in type names is T
			Return XMLFromName(type, "T"C, "")
		End Function

		''' <summary>
		''' Obtains the XML Element that describes a reflection element by searching the 
		''' members for a member that has a name that describes the element.
		''' </summary>
		''' <param name="type">The type or parent type, used to fetch the assembly</param>
		''' <param name="prefix">The prefix as seen in the name attribute in the documentation XML</param>
		''' <param name="name">Where relevant, the full name qualifier for the element</param>
		''' <returns>The member that has a name that describes the specified reflection element</returns>
		Private Shared Function XMLFromName(type As Type, prefix As Char, name As String) As XmlElement
			Dim fullName As String

			If [String].IsNullOrEmpty(name) Then
				fullName = prefix & ":" & type.FullName
			Else
				fullName = prefix & ":" & type.FullName & "." & name
			End If

			Dim xmlDocument As XmlDocument = XMLFromAssembly(type.Assembly)

			Dim matchedElement As XmlElement = Nothing

			For Each xmlElement As XmlElement In xmlDocument("doc")("members")
				If xmlElement.Attributes("name").Value.Equals(fullName) Then
					If matchedElement IsNot Nothing Then
						Throw New DocsByReflectionException("Multiple matches to query", Nothing)
					End If

					matchedElement = xmlElement
				End If
			Next

			If matchedElement Is Nothing Then
				Throw New DocsByReflectionException("Could not find documentation for specified element", Nothing)
			End If

			Return matchedElement
		End Function

		''' <summary>
		''' A cache used to remember Xml documentation for assemblies
		''' </summary>
		Shared cache As New Dictionary(Of Assembly, XmlDocument)()

		''' <summary>
		''' A cache used to store failure exceptions for assembly lookups
		''' </summary>
		Shared failCache As New Dictionary(Of Assembly, Exception)()

		''' <summary>
		''' Obtains the documentation file for the specified assembly
		''' </summary>
		''' <param name="assembly">The assembly to find the XML document for</param>
		''' <returns>The XML document</returns>
		''' <remarks>This version uses a cache to preserve the assemblies, so that 
		''' the XML file is not loaded and parsed on every single lookup</remarks>
		Public Shared Function XMLFromAssembly(assembly As Assembly) As XmlDocument
			If failCache.ContainsKey(assembly) Then
				Throw failCache(assembly)
			End If

			Try

				If Not cache.ContainsKey(assembly) Then
					' load the docuemnt into the cache
					cache(assembly) = XMLFromAssemblyNonCached(assembly)
				End If

				Return cache(assembly)
			Catch exception As Exception
				failCache(assembly) = exception
				Throw exception
			End Try
		End Function

		''' <summary>
		''' Loads and parses the documentation file for the specified assembly
		''' </summary>
		''' <param name="assembly">The assembly to find the XML document for</param>
		''' <returns>The XML document</returns>
		Private Shared Function XMLFromAssemblyNonCached(assembly As Assembly) As XmlDocument
			Dim assemblyFilename As String = assembly.CodeBase

			Const  prefix As String = "file:///"

			If assemblyFilename.StartsWith(prefix) Then
				Dim streamReader As StreamReader

				Try
					streamReader = New StreamReader(Path.ChangeExtension(assemblyFilename.Substring(prefix.Length), ".xml"))
				Catch exception As FileNotFoundException
					Throw New DocsByReflectionException("XML documentation not present (make sure it is turned on in project properties when building)", exception)
				End Try

				Dim xmlDocument As New XmlDocument()
				xmlDocument.Load(streamReader)
				Return xmlDocument
			Else
				Throw New DocsByReflectionException("Could not ascertain assembly filename", Nothing)
			End If
		End Function
	End Class
End Namespace
