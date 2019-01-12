Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonItemRenderEventArgs
		Inherits RibbonRenderEventArgs
		Private _item As RibbonItem
		Public Sub New(owner As Ribbon, g As Graphics, clip As Rectangle, item As RibbonItem)
			MyBase.New(owner, g, clip)
			Item = item
		End Sub
		Public Property Item() As RibbonItem
			Get
				Return _item
			End Get
			Set
				_item = value
			End Set
		End Property
	End Class
End Namespace
