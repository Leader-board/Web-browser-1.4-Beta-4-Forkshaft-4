Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonItemBoundsEventArgs
		Inherits RibbonItemRenderEventArgs
		Public Sub New(owner As Ribbon, g As Graphics, clip As Rectangle, item As RibbonItem, bounds As Rectangle)
			MyBase.New(owner, g, clip, item)
			Bounds = bounds
		End Sub
		Private _bounds As Rectangle
		Public Property Bounds() As Rectangle
			Get
				Return _bounds
			End Get
			Set
				_bounds = value
			End Set
		End Property
	End Class
End Namespace
