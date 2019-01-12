Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Interface IDropDownRibbonItem
		ReadOnly Property DropDownItems() As RibbonItemCollection
		ReadOnly Property DropDownButtonBounds() As Rectangle
		ReadOnly Property DropDownButtonVisible() As Boolean
		ReadOnly Property DropDownButtonSelected() As Boolean
		ReadOnly Property DropDownButtonPressed() As Boolean
	End Interface
End Namespace
