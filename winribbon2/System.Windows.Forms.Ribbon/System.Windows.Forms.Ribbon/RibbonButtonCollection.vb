Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Namespace System.Windows.Forms
	Public Class RibbonButtonCollection
		Inherits RibbonItemCollection
		Private _ownerList As RibbonButtonList
		Friend Sub New(list As RibbonButtonList)
			_ownerList = list
		End Sub
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OwnerList() As RibbonButtonList
			Get
				Return _ownerList
			End Get
		End Property
		Private Sub CheckRestrictions(button As RibbonButton)
			If button Is Nothing Then
				Throw New ApplicationException("The RibbonButtonList only accepts button in the Buttons collection")
			End If
			If button.Style <> RibbonButtonStyle.Normal Then
				Throw New ApplicationException("The only style supported by the RibbonButtonList is Normal")
			End If
		End Sub
		Public Sub Add(item As RibbonButton)
			CheckRestrictions(TryCast(item, RibbonButton))
			item.SetOwner(Owner)
			item.SetOwnerPanel(OwnerPanel)
			item.SetOwnerTab(OwnerTab)
			item.SetOwnerItem(OwnerList)
			MyBase.Add(item)
		End Sub
		Public Shadows Sub AddRange(items As IEnumerable(Of RibbonItem))
			For Each item As RibbonItem In items
				CheckRestrictions(TryCast(item, RibbonButton))
				item.SetOwner(Owner)
				item.SetOwnerPanel(OwnerPanel)
				item.SetOwnerTab(OwnerTab)
				item.SetOwnerItem(OwnerList)
			Next
			MyBase.AddRange(items)
		End Sub
		Public Shadows Sub Insert(index As Integer, item As RibbonItem)
			CheckRestrictions(TryCast(item, RibbonButton))
			item.SetOwner(Owner)
			item.SetOwnerPanel(OwnerPanel)
			item.SetOwnerTab(OwnerTab)
			item.SetOwnerItem(OwnerList)
			MyBase.Insert(index, item)
		End Sub
	End Class
End Namespace
