Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.ComponentModel
Namespace System.Windows.Forms
	<Designer(GetType(RibbonItemGroupDesigner))> _
	Public Class RibbonItemGroup
		Inherits RibbonItem
		Implements IContainsSelectableRibbonItems
		Implements IContainsRibbonComponents
		Private _items As RibbonItemGroupItemCollection
		Private _drawBackground As Boolean
		Public Sub New()
			_items = New RibbonItemGroupItemCollection(Me)
			_drawBackground = True
		End Sub
		Public Sub New(items As IEnumerable(Of RibbonItem))
			Me.New()
			_items.AddRange(items)
		End Sub
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overrides Property Checked() As Boolean
			Get
				Return MyBase.Checked
			End Get
			Set
				MyBase.Checked = value
			End Set
		End Property
		<DefaultValue(True)> _
		<Description("Background drawing should be avoided when group contains only TextBoxes and ComboBoxes")> _
		Public Property DrawBackground() As Boolean
			Get
				Return _drawBackground
			End Get
			Set
				_drawBackground = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property FirstItem() As RibbonItem
			Get
				If Items.Count > 0 Then
					Return Items(0)
				End If
				Return Nothing
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property LastItem() As RibbonItem
			Get
				If Items.Count > 0 Then
					Return Items(Items.Count - 1)
				End If
				Return Nothing
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property Items() As RibbonItemGroupItemCollection
			Get
				Return _items
			End Get
		End Property
		Public Overrides Sub SetBounds(bounds As Rectangle)
			MyBase.SetBounds(bounds)
			Dim curLeft As Integer = bounds.Left
			For Each item As RibbonItem In Items
				item.SetBounds(New Rectangle(New Point(curLeft, bounds.Top), item.LastMeasuredSize))
				curLeft = item.Bounds.Right + 1
			Next
		End Sub
		Public Overrides Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
			If DrawBackground Then
				Owner.Renderer.OnRenderRibbonItem(New RibbonItemRenderEventArgs(Owner, e.Graphics, e.Clip, Me))
			End If
			For Each item As RibbonItem In Items
				item.OnPaint(Me, New RibbonElementPaintEventArgs(item.Bounds, e.Graphics, RibbonElementSizeMode.Compact))
			Next
			If DrawBackground Then
				Owner.Renderer.OnRenderRibbonItemBorder(New RibbonItemRenderEventArgs(Owner, e.Graphics, e.Clip, Me))
			End If
		End Sub
		Public Overrides Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim minWidth As Integer = 16
			Dim widthSum As Integer = 0
			Dim maxHeight As Integer = 16
			For Each item As RibbonItem In Items
				Dim s As Size = item.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(e.Graphics, RibbonElementSizeMode.Compact))
				widthSum += s.Width + 1
				maxHeight = Math.Max(maxHeight, s.Height)
			Next
			widthSum -= 1
			widthSum = Math.Max(widthSum, minWidth)
			If Site IsNot Nothing AndAlso Site.DesignMode Then
				widthSum += 10
			End If
			Dim result As New Size(widthSum, maxHeight)
			SetLastMeasuredSize(result)
			Return result
		End Function
		Friend Overrides Sub SetOwnerPanel(ownerPanel As System.Windows.Forms.RibbonPanel)
			MyBase.SetOwnerPanel(ownerPanel)
			Items.SetOwnerPanel(ownerPanel)
		End Sub
		Friend Overrides Sub SetOwner(owner As System.Windows.Forms.Ribbon)
			MyBase.SetOwner(owner)
			Items.SetOwner(owner)
		End Sub
		Friend Overrides Sub SetOwnerTab(ownerTab As System.Windows.Forms.RibbonTab)
			MyBase.SetOwnerTab(ownerTab)
			Items.SetOwnerTab(ownerTab)
		End Sub
		Friend Overrides Sub SetSizeMode(sizeMode As RibbonElementSizeMode)
			MyBase.SetSizeMode(sizeMode)
			For Each item As RibbonItem In Items
				item.SetSizeMode(RibbonElementSizeMode.Compact)
			Next
		End Sub
		Public Overrides Function ToString() As String
			Return "Group: " + Items.Count + " item(s)"
		End Function
		Public Function GetItems() As IEnumerable(Of RibbonItem)
			Return Items
		End Function
		Public Function GetContentBounds() As Rectangle
			Return Rectangle.FromLTRB(Bounds.Left + 1, Bounds.Top + 1, Bounds.Right - 1, Bounds.Bottom)
		End Function
		Public Function GetAllChildComponents() As IEnumerable(Of Component)
			Return Items.ToArray()
		End Function
	End Class
End Namespace
