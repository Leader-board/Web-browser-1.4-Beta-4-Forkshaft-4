Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonCanvasEventArgs
		Inherits EventArgs
		Public Sub New(owner As Ribbon, g As Graphics, bounds As Rectangle, canvas As Control, relatedObject As Object)
			Owner = owner
			Graphics = g
			Bounds = bounds
			Canvas = canvas
			RelatedObject = relatedObject
		End Sub
		Private _relatedObject As Object
		Public Property RelatedObject() As Object
			Get
				Return _relatedObject
			End Get
			Set
				_relatedObject = value
			End Set
		End Property
		Private _owner As Ribbon
		Public Property Owner() As Ribbon
			Get
				Return _owner
			End Get
			Set
				_owner = value
			End Set
		End Property
		Private _Graphics As Graphics
		Public Property Graphics() As Graphics
			Get
				Return _Graphics
			End Get
			Set
				_Graphics = value
			End Set
		End Property
		Private _bounds As Rectangle
		Public Property Bounds() As Rectangle
			Get
				Return _bounds
			End Get
			Set
				_bounds = value
			End Set
		End Property
		Private _canvas As Control
		Public Property Canvas() As Control
			Get
				Return _canvas
			End Get
			Set
				_canvas = value
			End Set
		End Property
	End Class
End Namespace
