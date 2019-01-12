Imports System
Imports System.Collections.Generic
Imports System.Text
Namespace System.Windows.Forms
	Friend Class RibbonButtonListDesigner
		Inherits RibbonElementWithItemCollectionDesigner
		Public Overrides ReadOnly Property Ribbon() As Ribbon
			Get
				If TypeOf Component Is RibbonButtonList Then
					Return (TryCast(Component, RibbonButtonList)).Owner
				End If
				Return Nothing
			End Get
		End Property
		Public Overrides ReadOnly Property Collection() As RibbonItemCollection
			Get
				If TypeOf Component Is RibbonButtonList Then
					Return (TryCast(Component, RibbonButtonList)).Buttons
				End If
				Return Nothing
			End Get
		End Property
	End Class
End Namespace
