Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel.Design
Namespace System.Windows.Forms
	Friend Class RibbonItemGroupDesigner
		Inherits RibbonElementWithItemCollectionDesigner
		Public Overrides ReadOnly Property Ribbon() As Ribbon
			Get
				If TypeOf Component Is RibbonItemGroup Then
					Return (TryCast(Component, RibbonItemGroup)).Owner
				End If
				Return Nothing
			End Get
		End Property
		Public Overrides ReadOnly Property Collection() As RibbonItemCollection
			Get
				If TypeOf Component Is RibbonItemGroup Then
					Return (TryCast(Component, RibbonItemGroup)).Items
				End If
				Return Nothing
			End Get
		End Property
	End Class
End Namespace
