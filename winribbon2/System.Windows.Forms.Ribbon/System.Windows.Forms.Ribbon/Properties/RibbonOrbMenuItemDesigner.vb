Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel.Design
Namespace System.Windows.Forms
	Friend Class RibbonOrbMenuItemDesigner
		Inherits RibbonElementWithItemCollectionDesigner
		Public Overrides ReadOnly Property Ribbon() As Ribbon
			Get
				If TypeOf Component Is RibbonButton Then
					Return (TryCast(Component, RibbonButton)).Owner
				End If
				Return Nothing
			End Get
		End Property
		Public Overrides ReadOnly Property Collection() As RibbonItemCollection
			Get
				If TypeOf Component Is RibbonButton Then
					Return (TryCast(Component, RibbonButton)).DropDownItems
				End If
				Return Nothing
			End Get
		End Property
		Protected Overrides Function OnGetVerbs() As DesignerVerbCollection
			Return New DesignerVerbCollection(New DesignerVerb() {New DesignerVerb("Add DescriptionMenuItem", New EventHandler(AddDescriptionMenuItem)), New DesignerVerb("Add Separator", New EventHandler(AddSeparator))})
		End Function
	End Class
End Namespace
