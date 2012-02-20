' <file>
'     <copyright see="prj:///doc/copyright.txt"/>
'     <license see="prj:///doc/license.txt"/>
'     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
'     <version>$Revision: 934 $</version>
' </file>

Imports System.Collections

	''' <summary>
	''' Creates object instances by invocating a type's parameterless constructor
	''' via System.Reflection.
	''' </summary>
	''' <attribute name="class" use="required">
	''' The fully qualified type name of the class to create an instace of.
	''' </attribute>
	''' <usage>Everywhere where objects are expected.</usage>
	''' <returns>
	''' Any kind of object.
	''' </returns>
Public Class ClassDoozer
  Implements IDoozer
  ''' <summary>
  ''' Gets if the doozer handles codon conditions on its own.
  ''' If this property return false, the item is excluded when the condition is not met.
  ''' </summary>
  Public ReadOnly Property HandleConditions() As Boolean Implements IDoozer.HandleConditions
    Get
      Return False
    End Get
  End Property

  Public Function BuildItem(ByVal caller As Object, ByVal codon As Codon, ByVal subItems As ArrayList) As Object Implements IDoozer.BuildItem
    Return codon.AddIn.CreateObject(codon.Properties("class"))
  End Function
End Class
