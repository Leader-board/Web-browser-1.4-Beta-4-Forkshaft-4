Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonTextEventArgs
		Inherits RibbonItemBoundsEventArgs
		Private _text As String
		Private _format As StringFormat
		Private _style As FontStyle
		Private _color As Color
		Public Sub New(owner As Ribbon, g As Graphics, clip As Rectangle, item As RibbonItem, bounds As Rectangle, text As String)
			MyBase.New(owner, g, clip, item, bounds)
			Text = text
			Style = FontStyle.Regular
			Format = New StringFormat()
			Color = Color.Empty
		End Sub
		Public Sub New(owner As Ribbon, g As Graphics, clip As Rectangle, item As RibbonItem, bounds As Rectangle, text As String, _
			style As FontStyle)
			MyBase.New(owner, g, clip, item, bounds)
			Text = text
			Style = style
			Format = New StringFormat()
			Color = Color.Empty
		End Sub
		Public Sub New(owner As Ribbon, g As Graphics, clip As Rectangle, item As RibbonItem, bounds As Rectangle, text As String, _
			format As StringFormat)
			MyBase.New(owner, g, clip, item, bounds)
			Text = text
			Style = FontStyle.Regular
			Format = format
			Color = Color.Empty
		End Sub
		Public Sub New(owner As Ribbon, g As Graphics, clip As Rectangle, item As RibbonItem, bounds As Rectangle, text As String, _
			color As Color, style As FontStyle, format As StringFormat)
			MyBase.New(owner, g, clip, item, bounds)
			Text = text
			Style = style
			Format = format
			Color = color
		End Sub
		Public Property Color() As Color
			Get
				Return _color
			End Get
			Set
				_color = value
			End Set
		End Property
		Public Property Text() As String
			Get
				Return _text
			End Get
			Set
				_text = value
			End Set
		End Property
		Public Property Format() As StringFormat
			Get
				Return _format
			End Get
			Set
				_format = value
			End Set
		End Property
		Public Property Style() As FontStyle
			Get
				Return _style
			End Get
			Set
				_style = value
			End Set
		End Property
	End Class
End Namespace
