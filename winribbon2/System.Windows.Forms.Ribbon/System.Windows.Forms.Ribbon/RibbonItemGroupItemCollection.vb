Imports System
Imports System.Collections.Generic
Imports System.Text
Namespace System.Windows.Forms
	Public Class RibbonItemGroupItemCollection
		Inherits RibbonItemCollection
		Private _ownerGroup As RibbonItemGroup
		Friend Sub New(ownerGroup As RibbonItemGroup)
			_ownerGroup = ownerGroup
		End Sub
		Public ReadOnly Property OwnerGroup() As RibbonItemGroup
			Get
				Return _ownerGroup
			End Get
		End Property
		Public Shadows Sub Add(item As RibbonItem)
			item.MaxSizeMode = RibbonElementSizeMode.Compact
			item.SetOwnerGroup(OwnerGroup)
			MyBase.Add(item)
		End Sub
		Public Shadows Sub AddRange(items As IEnumerable(Of RibbonItem))
			For Each item As RibbonItem In items
				item.MaxSizeMode = RibbonElementSizeMode.Compact
				item.SetOwnerGroup(OwnerGroup)
			Next
			MyBase.AddRange(items)
		End Sub
		Public Shadows Sub Insert(index As Integer, item As RibbonItem)
			item.MaxSizeMode = RibbonElementSizeMode.Compact
			item.SetOwnerGroup(OwnerGroup)
			MyBase.Insert(index, item)
		End Sub
	End Class
End Namespace
