Imports System
Imports System.Collections.Generic
Imports System.Text
Namespace System.Windows.Forms
	Public Class RibbonToolTip
		Inherits RibbonPopup
		Public Sub New()
		End Sub
		Protected Overrides Sub OnPaint(e As PaintEventArgs)
			MyBase.OnPaint(e)
		End Sub
	End Class
End Namespace
