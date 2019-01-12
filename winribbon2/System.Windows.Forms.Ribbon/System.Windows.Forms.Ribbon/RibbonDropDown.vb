Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.ComponentModel.Design
Namespace System.Windows.Forms
	<ToolboxItem(False)> _
	Public Partial Class RibbonDropDown
		Inherits RibbonPopup
		Private _items As IEnumerable(Of RibbonItem)
		Private _showSizingGrip As Boolean
		Private _sizingGripHeight As Integer
		Private _ownerRibbon As Ribbon
		Private _sensor As RibbonMouseSensor
		Private _parentItem As RibbonItem
		Private _ignoreNext As Boolean
		Private _MeasuringSize As RibbonElementSizeMode
		Private _resizing As Boolean
		Private _sizingGripBounds As Rectangle
		Private _resizeOrigin As Point
		Private _resizeSize As Size
		Private _SelectionService As ISelectionService
		Private _iconsBar As Boolean
		Private Sub New()
			DoubleBuffered = True
			DrawIconsBar = True
		End Sub
		Friend Sub New(parentItem As RibbonItem, items As IEnumerable(Of RibbonItem), ownerRibbon As Ribbon)
			Me.New(parentItem, items, ownerRibbon, RibbonElementSizeMode.DropDown)
		End Sub
		Friend Sub New(parentItem As RibbonItem, items As IEnumerable(Of RibbonItem), ownerRibbon As Ribbon, measuringSize As RibbonElementSizeMode)
			Me.New()
			_items = items
			_ownerRibbon = ownerRibbon
			_sizingGripHeight = 12
			_parentItem = parentItem
			_sensor = New RibbonMouseSensor(Me, OwnerRibbon, items)
			_MeasuringSize = measuringSize
			If Items IsNot Nothing Then
				For Each item As RibbonItem In Items
					item.SetSizeMode(RibbonElementSizeMode.DropDown)
					item.SetCanvas(Me)
				Next
			End If
			UpdateSize()
		End Sub
		Public Property DrawIconsBar() As Boolean
			Get
				Return _iconsBar
			End Get
			Set
				_iconsBar = value
			End Set
		End Property
		Protected Overrides Sub OnOpening(e As CancelEventArgs)
			MyBase.OnOpening(e)
			UpdateItemsBounds()
		End Sub
		Friend Property SelectionService() As ISelectionService
			Get
				Return _SelectionService
			End Get
			Set
				_SelectionService = value
			End Set
		End Property
		Public ReadOnly Property SizingGripBounds() As Rectangle
			Get
				Return _sizingGripBounds
			End Get
		End Property
		Public Property MeasuringSize() As RibbonElementSizeMode
			Get
				Return _MeasuringSize
			End Get
			Set
				_MeasuringSize = value
			End Set
		End Property
		Public ReadOnly Property ParentItem() As RibbonItem
			Get
				Return _parentItem
			End Get
		End Property
		Public ReadOnly Property Sensor() As RibbonMouseSensor
			Get
				Return _sensor
			End Get
		End Property
		Public ReadOnly Property OwnerRibbon() As Ribbon
			Get
				Return _ownerRibbon
			End Get
		End Property
		Public ReadOnly Property Items() As IEnumerable(Of RibbonItem)
			Get
				Return _items
			End Get
		End Property
		Public Property ShowSizingGrip() As Boolean
			Get
				Return _showSizingGrip
			End Get
			Set
				_showSizingGrip = value
				UpdateSize()
				UpdateItemsBounds()
			End Set
		End Property
		<DefaultValue(12)> _
		Public Property SizingGripHeight() As Integer
			Get
				Return _sizingGripHeight
			End Get
			Set
				_sizingGripHeight = value
			End Set
		End Property
		Public Sub IgnoreNextClickDeactivation()
			_ignoreNext = True
		End Sub
		Private Sub UpdateSize()
			Dim heightSum As Integer = OwnerRibbon.DropDownMargin.Vertical
			Dim maxWidth As Integer = 0
			Dim scrollableHeight As Integer = 0
			Using g As Graphics = CreateGraphics()
				For Each item As RibbonItem In Items
					Dim s As Size = item.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(g, MeasuringSize))
					heightSum += s.Height
					maxWidth = Math.Max(maxWidth, s.Width)
					If TypeOf item Is IScrollableRibbonItem Then
						scrollableHeight += s.Height
					End If
				Next
			End Using
			Dim sz As New Size(maxWidth + OwnerRibbon.DropDownMargin.Horizontal, heightSum + (If(ShowSizingGrip, SizingGripHeight + 2, 0)))
			Size = sz
			If WrappedDropDown IsNot Nothing Then
				WrappedDropDown.Size = Size
			End If
		End Sub
		Private Sub UpdateItemsBounds()
			Dim curTop As Integer = OwnerRibbon.DropDownMargin.Top
			Dim curLeft As Integer = OwnerRibbon.DropDownMargin.Left
			Dim itemsWidth As Integer = ClientSize.Width - OwnerRibbon.DropDownMargin.Horizontal
			Dim scrollableItemsHeight As Integer = 0
			Dim nonScrollableItemsHeight As Integer = 0
			Dim scrollableItems As Integer = 0
			Dim scrollableItemHeight As Integer = 0
			For Each item As RibbonItem In Items
				If TypeOf item Is IScrollableRibbonItem Then
					scrollableItemsHeight += item.LastMeasuredSize.Height
					System.Math.Max(System.Threading.Interlocked.Increment(scrollableItems),scrollableItems - 1)
				Else
					nonScrollableItemsHeight += item.LastMeasuredSize.Height
				End If
			Next
			If scrollableItems > 0 Then
				scrollableItemHeight = (Height - nonScrollableItemsHeight - (If(ShowSizingGrip, SizingGripHeight, 0))) / scrollableItems
			End If
			For Each item As RibbonItem In Items
				If TypeOf item Is IScrollableRibbonItem Then
					item.SetBounds(New Rectangle(curLeft, curTop, itemsWidth, scrollableItemHeight - 1))
				Else
					item.SetBounds(New Rectangle(curLeft, curTop, itemsWidth, item.LastMeasuredSize.Height))
				End If
				curTop += item.Bounds.Height
			Next
			If ShowSizingGrip Then
				_sizingGripBounds = Rectangle.FromLTRB(ClientSize.Width - SizingGripHeight, ClientSize.Height - SizingGripHeight, ClientSize.Width, ClientSize.Height)
			Else
				_sizingGripBounds = Rectangle.Empty
			End If
		End Sub
		Protected Overrides Sub OnShowed(e As EventArgs)
			MyBase.OnShowed(e)
			For Each item As RibbonItem In Items
				item.SetSelected(False)
			Next
		End Sub
		Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
			MyBase.OnMouseDown(e)
			If Cursor = Cursors.SizeNWSE Then
				_resizeOrigin = New Point(e.X, e.Y)
				_resizeSize = Size
				_resizing = True
			End If
		End Sub
		Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
			MyBase.OnMouseMove(e)
			If ShowSizingGrip AndAlso SizingGripBounds.Contains(e.X, e.Y) Then
				Cursor = Cursors.SizeNWSE
			ElseIf Cursor = Cursors.SizeNWSE Then
				Cursor = Cursors.[Default]
			End If
			If _resizing Then
				Dim dx As Integer = e.X - _resizeOrigin.X
				Dim dy As Integer = e.Y - _resizeOrigin.Y
				Dim w As Integer = _resizeSize.Width + dx
				Dim h As Integer = _resizeSize.Height + dy
				If w <> Width OrElse h <> Height Then
					Size = New Size(w, h)
					If WrappedDropDown IsNot Nothing Then
						WrappedDropDown.Size = Size
					End If
					UpdateItemsBounds()
				End If
			End If
		End Sub
		Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
			MyBase.OnMouseUp(e)
			If _resizing Then
				_resizing = False
				Return
			End If
			If _ignoreNext Then
				_ignoreNext = False
				Return
			End If
			If RibbonDesigner.Current IsNot Nothing Then
				Close()
			End If
		End Sub
		Protected Overrides Sub OnPaint(e As PaintEventArgs)
			MyBase.OnPaint(e)
			OwnerRibbon.Renderer.OnRenderDropDownBackground(New RibbonCanvasEventArgs(OwnerRibbon, e.Graphics, New Rectangle(Point.Empty, ClientSize), Me, ParentItem))
			For Each item As RibbonItem In Items
				item.OnPaint(Me, New RibbonElementPaintEventArgs(item.Bounds, e.Graphics, RibbonElementSizeMode.DropDown))
			Next
		End Sub
		Protected Overrides Sub OnMouseLeave(e As EventArgs)
			MyBase.OnMouseLeave(e)
			For Each item As RibbonItem In Items
				item.SetSelected(False)
			Next
		End Sub
	End Class
End Namespace
