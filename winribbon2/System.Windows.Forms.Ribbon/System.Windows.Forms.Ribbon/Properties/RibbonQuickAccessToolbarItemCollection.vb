Imports System
Imports System.Collections.Generic
Imports System.Text
Namespace System.Windows.Forms
	Public Class RibbonQuickAccessToolbarItemCollection
		Inherits RibbonItemCollection
		Private _ownerToolbar As RibbonQuickAccessToolbar
		Friend Sub New(toolbar As RibbonQuickAccessToolbar)
			_ownerToolbar = toolbar
			SetOwner(toolbar.Owner)
		End Sub
		Public ReadOnly Property OwnerToolbar() As RibbonQuickAccessToolbar
			Get
				Return _ownerToolbar
			End Get
		End Property
		Public Shadows Sub Add(item As RibbonItem)
			item.MaxSizeMode = RibbonElementSizeMode.Compact
			MyBase.Add(item)
		End Sub
		Public Shadows Sub AddRange(items As IEnumerable(Of RibbonItem))
			For Each item As RibbonItem In items
				item.MaxSizeMode = RibbonElementSizeMode.Compact
			Next
			MyBase.AddRange(items)
		End Sub
		Public Shadows Sub Insert(index As Integer, item As RibbonItem)
			item.MaxSizeMode = RibbonElementSizeMode.Compact
			MyBase.Insert(index, item)
		End Sub
	End Class
End Namespace
