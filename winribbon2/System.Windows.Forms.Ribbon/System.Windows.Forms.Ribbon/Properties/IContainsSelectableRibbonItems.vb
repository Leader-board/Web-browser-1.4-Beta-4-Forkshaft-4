Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Interface IContainsSelectableRibbonItems
		Function GetItems() As IEnumerable(Of RibbonItem)
		Function GetContentBounds() As Rectangle
	End Interface
End Namespace
