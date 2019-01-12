Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Namespace System.Windows.Forms
	Public NotInheritable Class RibbonPanelCollection
		Inherits List(Of RibbonPanel)
		Private _ownerTab As RibbonTab
		Public Sub New(ownerTab As RibbonTab)
			_ownerTab = ownerTab
		End Sub
		Friend Sub New(owner As Ribbon, ownerTab As RibbonTab)
			If ownerTab Is Nothing Then
				Throw New ArgumentNullException("ownerTab")
			End If
			_ownerTab = ownerTab
		End Sub
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Owner() As Ribbon
			Get
				Return _ownerTab.Owner
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OwnerTab() As RibbonTab
			Get
				Return _ownerTab
			End Get
		End Property
		Public Shadows Sub Add(item As RibbonPanel)
			item.SetOwner(Owner)
			item.SetOwnerTab(OwnerTab)
			MyBase.Add(item)
		End Sub
		Public Shadows Sub AddRange(items As System.Collections.Generic.IEnumerable(Of System.Windows.Forms.RibbonPanel))
			For Each p As RibbonPanel In items
				p.SetOwner(Owner)
				p.SetOwnerTab(OwnerTab)
			Next
			MyBase.AddRange(items)
		End Sub
		Public Shadows Sub Insert(index As Integer, item As System.Windows.Forms.RibbonPanel)
			item.SetOwner(Owner)
			item.SetOwnerTab(OwnerTab)
			MyBase.Insert(index, item)
		End Sub
		Friend Sub SetOwner(owner As Ribbon)
			For Each panel As RibbonPanel In Me
				panel.SetOwner(owner)
			Next
		End Sub
		Friend Sub SetOwnerTab(ownerTab As RibbonTab)
			_ownerTab = ownerTab
			For Each panel As RibbonPanel In Me
				panel.SetOwnerTab(OwnerTab)
			Next
		End Sub
	End Class
End Namespace
