Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonElementPaintEventArgs
		Inherits EventArgs
		Private _clip As System.Drawing.Rectangle
		Private _graphics As Graphics
		Private _mode As RibbonElementSizeMode
		Private _control As Control
		Friend Sub New(clip As Rectangle, graphics As Graphics, mode As RibbonElementSizeMode)
			_clip = clip
			_graphics = graphics
			_mode = mode
		End Sub
		Friend Sub New(clip As Rectangle, graphics As Graphics, mode As RibbonElementSizeMode, control As Control)
			Me.New(clip, graphics, mode)
			_control = control
		End Sub
		Public ReadOnly Property Clip() As Rectangle
			Get
				Return _clip
			End Get
		End Property
		Public ReadOnly Property Graphics() As System.Drawing.Graphics
			Get
				Return _graphics
			End Get
		End Property
		Public ReadOnly Property Mode() As RibbonElementSizeMode
			Get
				Return _mode
			End Get
		End Property
		Public ReadOnly Property Control() As Control
			Get
				Return _control
			End Get
		End Property
	End Class
End Namespace
