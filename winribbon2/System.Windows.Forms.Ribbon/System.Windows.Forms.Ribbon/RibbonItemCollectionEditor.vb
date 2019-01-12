Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing.Design
Imports System.ComponentModel.Design
Namespace System.Windows.Forms
	Public Class RibbonItemCollectionEditor
		Inherits CollectionEditor
		Public Sub New()
			MyBase.New(GetType(RibbonItemCollection))
		End Sub
		Protected Overrides Function CreateCollectionItemType() As Type
			Return GetType(RibbonButton)
		End Function
		Protected Overrides Function CreateNewItemTypes() As Type()
			Return New Type() {GetType(RibbonButton), GetType(RibbonButtonList), GetType(RibbonItemGroup), GetType(RibbonSeparator)}
		End Function
	End Class
End Namespace
