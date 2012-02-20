Imports System.Drawing
Public Class ScriptSyncSkin

  Private Shared _singletonInstance As ScriptSyncSkin
  Public Shared Function GetSingleton() As ScriptSyncSkin
    If _singletonInstance Is Nothing Then _singletonInstance = New ScriptSyncSkin
    Return _singletonInstance
  End Function
  Private Sub New()
  End Sub

  Private _InactiveGroupBackColor As Color = Color.FromKnownColor(KnownColor.Control)
  Public Property InactiveGroupBackColor() As Color
    Get
      Return _InactiveGroupBackColor
    End Get
    Set(ByVal value As Color)
      _InactiveGroupBackColor = value
      tbScriptSync.colorizeCatLabels()
    End Set
  End Property

  Private _InactiveGroupForeColor As Color = Color.FromKnownColor(KnownColor.ControlText)
  Public Property InactiveGroupForeColor() As Color
    Get
      Return _InactiveGroupForeColor
    End Get
    Set(ByVal value As System.Drawing.Color)
      _InactiveGroupForeColor = value
      tbScriptSync.colorizeCatLabels()
    End Set
  End Property

  Private _ActiveGroupBackColor As Color = Color.FromKnownColor(KnownColor.Highlight)
  Public Property ActiveGroupBackColor() As System.Drawing.Color
    Get
      Return _ActiveGroupBackColor
    End Get
    Set(ByVal value As System.Drawing.Color)
      _ActiveGroupBackColor = value
      tbScriptSync.colorizeCatLabels()
    End Set
  End Property

  Private _ActiveGroupForeColor As Color = Color.FromKnownColor(KnownColor.HighlightText)
  Public Property ActiveGroupForeColor() As System.Drawing.Color
    Get
      Return _ActiveGroupForeColor
    End Get
    Set(ByVal value As System.Drawing.Color)
      _ActiveGroupForeColor = value
      tbScriptSync.colorizeCatLabels()
    End Set
  End Property



End Class
