Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Interface IScrollableRibbonItem
		Sub ScrollUp()
		Sub ScrollDown()
		ReadOnly Property ContentBounds() As Rectangle
	End Interface
End Namespace
