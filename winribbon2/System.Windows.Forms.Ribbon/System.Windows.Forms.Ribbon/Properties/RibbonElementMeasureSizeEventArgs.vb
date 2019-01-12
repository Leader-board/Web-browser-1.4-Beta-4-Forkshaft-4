Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonElementMeasureSizeEventArgs
		Inherits EventArgs
		Private _sizeMode As RibbonElementSizeMode
		Private _graphics As System.Drawing.Graphics
		Friend Sub New(graphics As System.Drawing.Graphics, sizeMode As RibbonElementSizeMode)
			_graphics = graphics
			_sizeMode = sizeMode
		End Sub
		Public ReadOnly Property SizeMode() As RibbonElementSizeMode
			Get
				Return _sizeMode
			End Get
		End Property
		Public ReadOnly Property Graphics() As Graphics
			Get
				Return _graphics
			End Get
		End Property
	End Class
End Namespace
