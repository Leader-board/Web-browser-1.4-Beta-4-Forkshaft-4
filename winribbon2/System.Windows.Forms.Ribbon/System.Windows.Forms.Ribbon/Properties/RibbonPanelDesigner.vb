Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel.Design
Imports System.ComponentModel
Namespace System.Windows.Forms
	Friend Class RibbonPanelDesigner
		Inherits RibbonElementWithItemCollectionDesigner
		Public Overrides ReadOnly Property Ribbon() As Ribbon
			Get
				If TypeOf Component Is RibbonPanel Then
					Return (TryCast(Component, RibbonPanel)).Owner
				End If
				Return Nothing
			End Get
		End Property
		Public Overrides ReadOnly Property Collection() As RibbonItemCollection
			Get
				If TypeOf Component Is RibbonPanel Then
					Return (TryCast(Component, RibbonPanel)).Items
				End If
				Return Nothing
			End Get
		End Property
	End Class
End Namespace
