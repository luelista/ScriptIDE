' <file>
'     <copyright see="prj:///doc/copyright.txt"/>
'     <license see="prj:///doc/license.txt"/>
'     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
'     <version>$Revision: 1965 $</version>
' </file>

Imports System.Collections.Generic
Imports System.Xml

''' <summary>
''' Description of Path.
''' </summary>
Public Class ExtensionPath
  Private m_name As String
  Private m_addIn As AddinInstance
  Private m_codons As New List(Of Codon)()

  Public ReadOnly Property AddIn() As AddinInstance
    Get
      Return m_addIn
    End Get
  End Property

  Public ReadOnly Property Name() As String
    Get
      Return m_name
    End Get
  End Property
  Public ReadOnly Property Codons() As List(Of Codon)
    Get
      Return m_codons
    End Get
  End Property

  Public Sub New(ByVal name As String, ByVal addIn As AddinInstance)
    Me.m_addIn = addIn
    Me.m_name = name
  End Sub

  Public Shared Sub SetUp(ByVal extensionPath As ExtensionPath, ByVal reader As XmlReader, ByVal endElement As String)
    'Dim conditionStack As New Stack(Of ICondition)()
    While reader.Read()
      Select Case reader.NodeType
        Case XmlNodeType.EndElement
          'If reader.LocalName = "Condition" OrElse reader.LocalName = "ComplexCondition" Then
          'conditionStack.Pop()
          'Else
          If reader.LocalName = endElement Then
            Return
          End If
        Case XmlNodeType.Element
          Dim elementName As String = reader.LocalName
          'If elementName = "Condition" Then
          '  conditionStack.Push(Condition.Read(reader))
          'ElseIf elementName = "ComplexCondition" Then
          '  conditionStack.Push(Condition.ReadComplexCondition(reader))
          'Else
          Dim newCodon As New Codon(extensionPath.AddIn, elementName, Properties.ReadFromAttributes(reader), Not reader.IsEmptyElement) ', conditionStack.ToArray())
          extensionPath.Codons.Add(newCodon)
          If Not reader.IsEmptyElement Then
            Dim subPath As ExtensionPath = extensionPath.AddIn.GetExtensionPath((extensionPath.Name & "/") + newCodon.Id)
            'foreach (ICondition condition in extensionPath.conditionStack) {
            '	subPath.conditionStack.Push(condition);
            '}
            'foreach (ICondition condition in extensionPath.conditionStack) {
            '	subPath.conditionStack.Pop();
            '}
            SetUp(subPath, reader, elementName)
          End If
          'End If
      End Select
    End While
  End Sub

  Sub AddCodon(ByVal elementName As String, ByVal props As Properties, ByVal hasSubElement As Boolean)
    Dim newCodon As New Codon(Me.AddIn, elementName, props, hasSubElement)
    Me.Codons.Add(newCodon)
  End Sub

End Class

