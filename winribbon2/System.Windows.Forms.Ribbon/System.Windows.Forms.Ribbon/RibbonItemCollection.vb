Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports System.ComponentModel.Design
Namespace System.Windows.Forms
	<Editor("System.Windows.Forms.RibbonItemCollectionEditor", GetType(UITypeEditor))> _
	Public Class RibbonItemCollection
		Inherits List(Of RibbonItem)
		Private _owner As Ribbon
		Private _ownerTab As RibbonTab
		Private _ownerPanel As RibbonPanel
		Friend Sub New()
		End Sub
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Owner() As Ribbon
			Get
				Return _owner
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OwnerPanel() As RibbonPanel
			Get
				Return _ownerPanel
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OwnerTab() As RibbonTab
			Get
				Return _ownerTab
			End Get
		End Property
		Public Shadows Sub Add(item As RibbonItem)
			item.SetOwner(Owner)
			item.SetOwnerPanel(OwnerPanel)
			item.SetOwnerTab(OwnerTab)
			MyBase.Add(item)
		End Sub
		Public Shadows Sub AddRange(items As IEnumerable(Of RibbonItem))
			For Each item As RibbonItem In items
				item.SetOwner(Owner)
				item.SetOwnerPanel(OwnerPanel)
				item.SetOwnerTab(OwnerTab)
			Next
			MyBase.AddRange(items)
		End Sub
		Public Shadows Sub Insert(index As Integer, item As RibbonItem)
			item.SetOwner(Owner)
			item.SetOwnerPanel(OwnerPanel)
			item.SetOwnerTab(OwnerTab)
			MyBase.Insert(index, item)
		End Sub
		Friend Function GetItemsLeft(items As IEnumerable(Of RibbonItem)) As Integer
			If Count = 0 Then
				Return 0
			End If
			Dim min As Integer = Integer.MaxValue
			For Each item As RibbonItem In items
				If item.Bounds.X < min Then
					min = item.Bounds.X
				End If
			Next
			Return min
		End Function
		Friend Function GetItemsRight(items As IEnumerable(Of RibbonItem)) As Integer
			If Count = 0 Then
				Return 0
			End If
			Dim max As Integer = Integer.MinValue
			

			For Each item As RibbonItem In items
				If item.Bounds.Right > max Then
					max = item.Bounds.Right
				End If
			Next
			Return max
		End Function
		Friend Function GetItemsTop(items As IEnumerable(Of RibbonItem)) As Integer
			If Count = 0 Then
				Return 0
			End If
			Dim min As Integer = Integer.MaxValue
			For Each item As RibbonItem In items
				If item.Bounds.Y < min Then
					min = item.Bounds.Y
				End If
			Next
			Return min
		End Function
		Friend Function GetItemsBottom(items As IEnumerable(Of RibbonItem)) As Integer
			If Count = 0 Then
				Return 0
			End If
			Dim max As Integer = Integer.MinValue
			For Each item As RibbonItem In items
				If item.Bounds.Bottom > max Then
					max = item.Bounds.Bottom
				End If
			Next
			Return max
		End Function
		Friend Function GetItemsWidth(items As IEnumerable(Of RibbonItem)) As Integer
			Return GetItemsRight(items) - GetItemsLeft(items)
		End Function
		Friend Function GetItemsHeight(items As IEnumerable(Of RibbonItem)) As Integer
			Return GetItemsBottom(items) - GetItemsTop(items)
		End Function
		Friend Function GetItemsBounds(items As IEnumerable(Of RibbonItem)) As Rectangle
			Return Rectangle.FromLTRB(GetItemsLeft(items), GetItemsTop(items), GetItemsRight(items), GetItemsBottom(items))
		End Function
		Friend Function GetItemsLeft() As Integer
			If Count = 0 Then
				Return 0
			End If
			Dim min As Integer = Integer.MaxValue
			For Each item As RibbonItem In Me
				If item.Bounds.X < min Then
					min = item.Bounds.X
				End If
			Next
			Return min
		End Function
		Friend Function GetItemsRight() As Integer
			If Count = 0 Then
				Return 0
			End If
			Dim max As Integer = Integer.MinValue
			

			For Each item As RibbonItem In Me
				If item.Bounds.Right > max Then
					max = item.Bounds.Right
				End If
			Next
			Return max
		End Function
		Friend Function GetItemsTop() As Integer
			If Count = 0 Then
				Return 0
			End If
			Dim min As Integer = Integer.MaxValue
			For Each item As RibbonItem In Me
				If item.Bounds.Y < min Then
					min = item.Bounds.Y
				End If
			Next
			Return min
		End Function
		Friend Function GetItemsBottom() As Integer
			If Count = 0 Then
				Return 0
			End If
			Dim max As Integer = Integer.MinValue
			For Each item As RibbonItem In Me
				If item.Bounds.Bottom > max Then
					max = item.Bounds.Bottom
				End If
			Next
			Return max
		End Function
		Friend Function GetItemsWidth() As Integer
			Return GetItemsRight() - GetItemsLeft()
		End Function
		Friend Function GetItemsHeight() As Integer
			Return GetItemsBottom() - GetItemsTop()
		End Function
		Friend Function GetItemsBounds() As Rectangle
			Return Rectangle.FromLTRB(GetItemsLeft(), GetItemsTop(), GetItemsRight(), GetItemsBottom())
		End Function
		Friend Sub MoveTo(p As Point)
			MoveTo(Me, p)
		End Sub
		Friend Sub MoveTo(items As IEnumerable(Of RibbonItem), p As Point)
			Dim oldBounds As Rectangle = GetItemsBounds(items)
			For Each item As RibbonItem In items
				Dim dx As Integer = item.Bounds.X - oldBounds.Left
				Dim dy As Integer = item.Bounds.Y - oldBounds.Top
				item.SetBounds(New Rectangle(New Point(p.X + dx, p.Y + dy), item.Bounds.Size))
			Next
		End Sub
		Friend Sub CenterItemsInto(rectangle As Rectangle)
			CenterItemsInto(Me, rectangle)
		End Sub
		Friend Sub CenterItemsVerticallyInto(rectangle As Rectangle)
			CenterItemsVerticallyInto(Me, rectangle)
		End Sub
		Friend Sub CenterItemsHorizontallyInto(rectangle As Rectangle)
			CenterItemsHorizontallyInto(Me, rectangle)
		End Sub
		Friend Sub CenterItemsInto(items As IEnumerable(Of RibbonItem), rectangle As Rectangle)
			Dim x As Integer = rectangle.Left + (rectangle.Width - GetItemsWidth()) / 2
			Dim y As Integer = rectangle.Top + (rectangle.Height - GetItemsHeight()) / 2
			MoveTo(items, New Point(x, y))
		End Sub
		Friend Sub CenterItemsVerticallyInto(items As IEnumerable(Of RibbonItem), rectangle As Rectangle)
			Dim x As Integer = GetItemsLeft(items)
			Dim y As Integer = rectangle.Top + (rectangle.Height - GetItemsHeight(items)) / 2
			MoveTo(items, New Point(x, y))
		End Sub
		Friend Sub CenterItemsHorizontallyInto(items As IEnumerable(Of RibbonItem), rectangle As Rectangle)
			Dim x As Integer = rectangle.Left + (rectangle.Width - GetItemsWidth(items)) / 2
			Dim y As Integer = GetItemsTop(items)
			MoveTo(items, New Point(x, y))
		End Sub
		Friend Sub SetOwner(owner As Ribbon)
			_owner = owner
			For Each item As RibbonItem In Me
				item.SetOwner(owner)
			Next
		End Sub
		Friend Sub SetOwnerTab(tab As RibbonTab)
			_ownerTab = tab
			For Each item As RibbonItem In Me
				item.SetOwnerTab(tab)
			Next
		End Sub
		Friend Sub SetOwnerPanel(panel As RibbonPanel)
			_ownerPanel = panel
			For Each item As RibbonItem In Me
				item.SetOwnerPanel(panel)
			Next
		End Sub
	End Class
End Namespace
