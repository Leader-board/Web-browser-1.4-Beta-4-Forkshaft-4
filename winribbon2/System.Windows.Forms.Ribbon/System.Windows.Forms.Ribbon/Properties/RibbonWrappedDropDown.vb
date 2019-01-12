Imports System
Imports System.Collections.Generic
Imports System.Text
Namespace System.Windows.Forms
	Friend Class RibbonWrappedDropDown
		Inherits ToolStripDropDown
		Public Sub New()
			MyBase.New()
			DoubleBuffered = False
			SetStyle(ControlStyles.Opaque, True)
			SetStyle(ControlStyles.AllPaintingInWmPaint, True)
			SetStyle(ControlStyles.Selectable, False)
			SetStyle(ControlStyles.ResizeRedraw, False)
			AutoSize = False
		End Sub
	End Class
End Namespace
