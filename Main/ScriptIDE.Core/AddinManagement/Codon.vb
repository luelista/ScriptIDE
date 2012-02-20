' <file>
'     <copyright see="prj:///doc/copyright.txt"/>
'     <license see="prj:///doc/license.txt"/>
'     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
'     <version>$Revision: 2059 $</version>
' </file>

Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization

''' <summary>
''' Represents a node in the add in tree that can produce an item.
''' </summary>
Public Class Codon
  Private m_addIn As AddinInstance
  Private m_name As String
  Private m_properties As Properties
  Private m_hasSubItems As Boolean
  'Private m_conditions As ICondition()

  Public ReadOnly Property Name() As String
    Get
      Return m_name
    End Get
  End Property

  Public ReadOnly Property AddIn() As AddinInstance
    Get
      Return m_addIn
    End Get
  End Property

  Public ReadOnly Property HasSubItems() As Boolean
    Get
      Return m_hasSubItems
    End Get
  End Property

  Public ReadOnly Property Id() As String
    Get
      If Not m_properties.Contains("id") AndAlso m_properties.Contains("refid") Then Return m_properties("refid")
      Return m_properties("id")
    End Get
  End Property

  Public Property InsertAfter() As String
    Get
      If Not m_properties.Contains("insertafter") Then
        Return ""
      End If
      Return m_properties("insertafter")
    End Get
    Set(ByVal value As String)
      m_properties("insertafter") = value
    End Set
  End Property

  Public Property InsertBefore() As String
    Get
      If Not m_properties.Contains("insertbefore") Then
        Return ""
      End If
      Return m_properties("insertbefore")
    End Get
    Set(ByVal value As String)
      m_properties("insertbefore") = value
    End Set
  End Property

  Public ReadOnly Property Properties() As Properties
    Get
      Return m_properties
    End Get
  End Property

  'Public ReadOnly Property Conditions() As ICondition()
  '  Get
  '    Return m_conditions
  '  End Get
  'End Property

  Public Sub New(ByVal addIn As AddinInstance, ByVal name As String, ByVal properties As Properties, ByVal hasSubItems As Boolean) ', ByVal conditions As ICondition())
    Me.m_addIn = addIn
    Me.m_name = name
    Me.m_properties = properties
    m_hasSubItems = hasSubItems
    ' Me.m_conditions = conditions
  End Sub

  'Public Function GetFailedAction(ByVal caller As Object) As ConditionFailedAction
  '  Return Condition.GetFailedAction(m_conditions, caller)
  'End Function

  '
  '		public void BinarySerialize(BinaryWriter writer)
  '		{
  '			writer.Write(AddInTree.GetNameOffset(name));
  '			writer.Write(AddInTree.GetAddInOffset(addIn));
  '			properties.BinarySerialize(writer);
  '		}
  '
  Public Function BuildItem(ByVal owner As Object, ByVal subItems As ArrayList) As Object
    Dim doozer As IDoozer
    If Not AddInTree.Doozers.TryGetValue(Name, doozer) Then
      Throw New Exception("Doozer " & Name & " not found!")
    End If

    'If Not doozer.HandleConditions AndAlso m_conditions.Length > 0 Then
    '  Dim action As ConditionFailedAction = GetFailedAction(owner)
    '  If action <> ConditionFailedAction.[Nothing] Then
    '    Return Nothing
    '  End If
    'End If
    Return doozer.BuildItem(owner, Me, subItems)
  End Function

  Public Overloads Overrides Function ToString() As String
    Return [String].Format("[Codon: name = {0}, addIn={1}]", m_name, m_addIn.ID)
  End Function
End Class
