Imports System
Imports System.Collections.Generic
Imports System.Text
Namespace System.Windows.Forms
	Friend Class RibbonButtonDesigner
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
		Protected Overrides Sub AddButton(sender As Object, e As EventArgs)
			MyBase.AddButton(sender, e)
		End Sub
	End Class
End Namespace
