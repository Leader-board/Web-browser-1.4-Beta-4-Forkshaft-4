Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonRenderEventArgs
		Inherits EventArgs
		Private _ribbon As Ribbon
		Private _clipRectangle As Drawing.Rectangle
		Private _graphics As System.Drawing.Graphics
		Public Sub New(owner As Ribbon, g As Graphics, clip As Rectangle)
			Ribbon = owner
			Graphics = g
			ClipRectangle = clip
		End Sub
		Public Property Ribbon() As Ribbon
			Get
				Return _ribbon
			End Get
			Set
				_ribbon = value
			End Set
		End Property
		Public Property Graphics() As System.Drawing.Graphics
			Get
				Return _graphics
			End Get
			Set
				_graphics = value
			End Set
		End Property
		Public Property ClipRectangle() As System.Drawing.Rectangle
			Get
				Return _clipRectangle
			End Get
			Set
				_clipRectangle = value
			End Set
		End Property
	End Class
End Namespace
