Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonMouseSensor
		Implements IDisposable
		Private _control As Control
		Private _ribbon As Ribbon
		Private _tabs As List(Of RibbonTab)
		Private _panels As List(Of RibbonPanel)
		Private _items As List(Of RibbonItem)
		Private _tabLimit As RibbonTab
		Private _panelLimit As RibbonPanel
		Private _itemsLimit As IEnumerable(Of RibbonItem)
		Private _disposed As Boolean
		Private _suspended As Boolean
		Private _hittedTab As RibbonTab
		Private _hittedPanel As RibbonPanel
		Private _hittedItem As RibbonItem
		Private _hittedSubItem As RibbonItem
		Private _hittedTabScrollLeft As Boolean
		Private _hittedTabScrollRight As Boolean
		Private _selectedTab As RibbonTab
		Private _selectedPanel As RibbonPanel
		Private _selectedItem As RibbonItem
		Private _selectedSubItem As RibbonItem
		Private Sub New()
			_tabs = New List(Of RibbonTab)()
			_panels = New List(Of RibbonPanel)()
			_items = New List(Of RibbonItem)()
		End Sub
		Public Sub New(control As Control, ribbon As Ribbon)
			Me.New()
			If control Is Nothing Then
				Throw New ArgumentNullException("control")
			End If
			If ribbon Is Nothing Then
				Throw New ArgumentNullException("ribbon")
			End If
			_control = control
			_ribbon = ribbon
			AddHandlers()
		End Sub
		Public Sub New(control As Control, ribbon As Ribbon, tabs As IEnumerable(Of RibbonTab), panels As IEnumerable(Of RibbonPanel), items As IEnumerable(Of RibbonItem))
			Me.New(control, ribbon)
			If tabs IsNot Nothing Then
				Tabs.AddRange(tabs)
			End If
			If panels IsNot Nothing Then
				Panels.AddRange(panels)
			End If
			If items IsNot Nothing Then
				Items.AddRange(items)
			End If
		End Sub
		Public Sub New(control As Control, ribbon As Ribbon, tab As RibbonTab)
			Me.New(control, ribbon)
			Tabs.Add(tab)
			Panels.AddRange(tab.Panels)
			For Each panel As RibbonPanel In tab.Panels
				Items.AddRange(panel.Items)
			Next
		End Sub
		Public Sub New(control As Control, ribbon As Ribbon, itemsSource As IEnumerable(Of RibbonItem))
			Me.New(control, ribbon)
			ItemsSource = itemsSource
		End Sub
		Public ReadOnly Property Control() As Control
			Get
				Return _control
			End Get
		End Property
		Public ReadOnly Property Disposed() As Boolean
			Get
				Return _disposed
			End Get
		End Property
		Friend Property HittedTab() As RibbonTab
			Get
				Return _hittedTab
			End Get
			Set
				_hittedTab = value
			End Set
		End Property
		Friend ReadOnly Property HittedTabScroll() As Boolean
			Get
				Return HittedTabScrollLeft OrElse HittedTabScrollRight
			End Get
		End Property
		Friend Property HittedTabScrollLeft() As Boolean
			Get
				Return _hittedTabScrollLeft
			End Get
			Set
				_hittedTabScrollLeft = value
			End Set
		End Property
		Friend Property HittedTabScrollRight() As Boolean
			Get
				Return _hittedTabScrollRight
			End Get
			Set
				_hittedTabScrollRight = value
			End Set
		End Property
		Friend Property HittedPanel() As RibbonPanel
			Get
				Return _hittedPanel
			End Get
			Set
				_hittedPanel = value
			End Set
		End Property
		Friend Property HittedItem() As RibbonItem
			Get
				Return _hittedItem
			End Get
			Set
				_hittedItem = value
			End Set
		End Property
		Friend Property HittedSubItem() As RibbonItem
			Get
				Return _hittedSubItem
			End Get
			Set
				_hittedSubItem = value
			End Set
		End Property
		Public ReadOnly Property IsSupsended() As Boolean
			Get
				Return _suspended
			End Get
		End Property
		Public Property ItemsSource() As IEnumerable(Of RibbonItem)
			Get
				Return _itemsLimit
			End Get
			Set
				_itemsLimit = value
			End Set
		End Property
		Public ReadOnly Property Items() As List(Of RibbonItem)
			Get
				Return _items
			End Get
		End Property
		Public Property PanelLimit() As RibbonPanel
			Get
				Return _panelLimit
			End Get
			Set
				_panelLimit = value
			End Set
		End Property
		Public ReadOnly Property Panels() As List(Of RibbonPanel)
			Get
				Return _panels
			End Get
		End Property
		Public ReadOnly Property Ribbon() As Ribbon
			Get
				Return _ribbon
			End Get
		End Property
		Friend Property SelectedTab() As RibbonTab
			Get
				Return _selectedTab
			End Get
			Set
				_selectedTab = value
			End Set
		End Property
		Friend Property SelectedPanel() As RibbonPanel
			Get
				Return _selectedPanel
			End Get
			Set
				_selectedPanel = value
			End Set
		End Property
		Friend Property SelectedItem() As RibbonItem
			Get
				Return _selectedItem
			End Get
			Set
				_selectedItem = value
			End Set
		End Property
		Friend Property SelectedSubItem() As RibbonItem
			Get
				Return _selectedSubItem
			End Get
			Set
				_selectedSubItem = value
			End Set
		End Property
		Public Property TabLimit() As RibbonTab
			Get
				Return _tabLimit
			End Get
			Set
				_tabLimit = value
			End Set
		End Property
		Public ReadOnly Property Tabs() As List(Of RibbonTab)
			Get
				Return _tabs
			End Get
		End Property
		Private Sub AddHandlers()
			If Control Is Nothing Then
				Throw New ApplicationException("Control is Null, cant Add RibbonMouseSensor Handles")
			End If
			Control.MouseMove += New MouseEventHandler(Control_MouseMove)
			Control.MouseLeave += New EventHandler(Control_MouseLeave)
			Control.MouseDown += New MouseEventHandler(Control_MouseDown)
			Control.MouseUp += New MouseEventHandler(Control_MouseUp)
			Control.MouseClick += New MouseEventHandler(Control_MouseClick)
			Control.MouseDoubleClick += New MouseEventHandler(Control_MouseDoubleClick)
		End Sub
		Private Sub Control_MouseDoubleClick(sender As Object, e As MouseEventArgs)
			If IsSupsended OrElse Disposed Then
				Return
			End If
			If HittedPanel IsNot Nothing Then
				HittedPanel.OnDoubleClick(e)
			End If
			If HittedItem IsNot Nothing Then
				HittedItem.OnDoubleClick(e)
			End If
			If HittedSubItem IsNot Nothing Then
				HittedSubItem.OnDoubleClick(e)
			End If
		End Sub
		Private Sub Control_MouseClick(sender As Object, e As MouseEventArgs)
			If IsSupsended OrElse Disposed Then
				Return
			End If
			If HittedPanel IsNot Nothing Then
				HittedPanel.OnClick(e)
			End If
			If HittedItem IsNot Nothing Then
				HittedItem.OnClick(e)
			End If
			If HittedSubItem IsNot Nothing Then
				HittedSubItem.OnClick(e)
			End If
		End Sub
		Private Sub Control_MouseUp(sender As Object, e As MouseEventArgs)
			If IsSupsended OrElse Disposed Then
				Return
			End If
			If HittedTab IsNot Nothing Then
				If HittedTab.ScrollLeftVisible Then
					HittedTab.SetScrollLeftPressed(False)
					Control.Invalidate(HittedTab.ScrollLeftBounds)
				End If
				If HittedTab.ScrollRightVisible Then
					HittedTab.SetScrollRightPressed(False)
					Control.Invalidate(HittedTab.ScrollRightBounds)
				End If
			End If
			If HittedPanel IsNot Nothing Then
				HittedPanel.SetPressed(False)
				HittedPanel.OnMouseUp(e)
				Control.Invalidate(HittedPanel.Bounds)
			End If
			If HittedItem IsNot Nothing Then
				HittedItem.SetPressed(False)
				HittedItem.OnMouseUp(e)
				Control.Invalidate(HittedItem.Bounds)
			End If
			If HittedSubItem IsNot Nothing Then
				HittedSubItem.SetPressed(False)
				HittedSubItem.OnMouseUp(e)
				Control.Invalidate(Rectangle.Intersect(HittedItem.Bounds, HittedSubItem.Bounds))
			End If
		End Sub
		Private Sub Control_MouseDown(sender As Object, e As MouseEventArgs)
			If IsSupsended OrElse Disposed Then
				Return
			End If
			HitTest(e.Location)
			If HittedTab IsNot Nothing Then
				If HittedTabScrollLeft Then
					HittedTab.SetScrollLeftPressed(True)
					Control.Invalidate(HittedTab.ScrollLeftBounds)
				End If
				If HittedTabScrollRight Then
					HittedTab.SetScrollRightPressed(True)
					Control.Invalidate(HittedTab.ScrollRightBounds)
				End If
			End If
			If HittedPanel IsNot Nothing Then
				HittedPanel.SetPressed(True)
				HittedPanel.OnMouseDown(e)
				Control.Invalidate(HittedPanel.Bounds)
			End If
			If HittedItem IsNot Nothing Then
				HittedItem.SetPressed(True)
				HittedItem.OnMouseDown(e)
				Control.Invalidate(HittedItem.Bounds)
			End If
			If HittedSubItem IsNot Nothing Then
				HittedSubItem.SetPressed(True)
				HittedSubItem.OnMouseDown(e)
				Control.Invalidate(Rectangle.Intersect(HittedItem.Bounds, HittedSubItem.Bounds))
			End If
		End Sub
		Private Sub Control_MouseLeave(sender As Object, e As EventArgs)
			If IsSupsended OrElse Disposed Then
				Return
			End If
		End Sub
		Private Sub Control_MouseMove(sender As Object, e As MouseEventArgs)
			If IsSupsended OrElse Disposed Then
				Return
			End If
			HitTest(e.Location)
			If SelectedPanel IsNot Nothing AndAlso SelectedPanel <> HittedPanel Then
				SelectedPanel.SetSelected(False)
				SelectedPanel.OnMouseLeave(e)
				Control.Invalidate(SelectedPanel.Bounds)
			End If
			If SelectedItem IsNot Nothing AndAlso SelectedItem <> HittedItem Then
				SelectedItem.SetSelected(False)
				SelectedItem.OnMouseLeave(e)
				Control.Invalidate(SelectedItem.Bounds)
			End If
			If SelectedSubItem IsNot Nothing AndAlso SelectedSubItem <> HittedSubItem Then
				SelectedSubItem.SetSelected(False)
				SelectedSubItem.OnMouseLeave(e)
				Control.Invalidate(Rectangle.Intersect(SelectedItem.Bounds, SelectedSubItem.Bounds))
			End If
			If HittedTab IsNot Nothing Then
				If HittedTab.ScrollLeftVisible Then
					HittedTab.SetScrollLeftSelected(HittedTabScrollLeft)
					Control.Invalidate(HittedTab.ScrollLeftBounds)
				End If
				If HittedTab.ScrollRightVisible Then
					HittedTab.SetScrollRightSelected(HittedTabScrollRight)
					Control.Invalidate(HittedTab.ScrollRightBounds)
				End If
			End If
			If HittedPanel IsNot Nothing Then
				If HittedPanel = SelectedPanel Then
					HittedPanel.OnMouseMove(e)
				Else
					HittedPanel.SetSelected(True)
					HittedPanel.OnMouseEnter(e)
					Control.Invalidate(HittedPanel.Bounds)
				End If
			End If
			If HittedItem IsNot Nothing Then
				If HittedItem = SelectedItem Then
					HittedItem.OnMouseMove(e)
				Else
					HittedItem.SetSelected(True)
					HittedItem.OnMouseEnter(e)
					Control.Invalidate(HittedItem.Bounds)
				End If
			End If
			If HittedSubItem IsNot Nothing Then
				If HittedSubItem = SelectedSubItem Then
					HittedSubItem.OnMouseMove(e)
				Else
					HittedSubItem.SetSelected(True)
					HittedSubItem.OnMouseEnter(e)
					Control.Invalidate(Rectangle.Intersect(HittedItem.Bounds, HittedSubItem.Bounds))
				End If
			End If
		End Sub
		Friend Sub HitTest(p As Point)
			SelectedTab = HittedTab
			SelectedPanel = HittedPanel
			SelectedItem = HittedItem
			SelectedSubItem = HittedSubItem
			HittedTab = Nothing
			HittedTabScrollLeft = False
			HittedTabScrollRight = False
			HittedPanel = Nothing
			HittedItem = Nothing
			HittedSubItem = Nothing
			If TabLimit IsNot Nothing Then
				If TabLimit.TabContentBounds.Contains(p) Then
					HittedTab = TabLimit
				End If
			Else
				For Each tab As RibbonTab In Tabs
					If tab.TabContentBounds.Contains(p) Then
						HittedTab = tab
						Exit For
					End If
				Next
			End If
			If HittedTab IsNot Nothing Then
				HittedTabScrollLeft = HittedTab.ScrollLeftVisible AndAlso HittedTab.ScrollLeftBounds.Contains(p)
				HittedTabScrollRight = HittedTab.ScrollRightVisible AndAlso HittedTab.ScrollRightBounds.Contains(p)
			End If
			If Not HittedTabScroll Then
				If PanelLimit IsNot Nothing Then
					If PanelLimit.Bounds.Contains(p) Then
						HittedPanel = PanelLimit
					End If
				Else
					For Each pnl As RibbonPanel In Panels
						If pnl.Bounds.Contains(p) Then
							HittedPanel = pnl
							Exit For
						End If
					Next
				End If
				Dim items As IEnumerable(Of RibbonItem) = Items
				If ItemsSource IsNot Nothing Then
					items = ItemsSource
				End If
				For Each item As RibbonItem In items
					If item.OwnerPanel IsNot Nothing AndAlso item.OwnerPanel.OverflowMode AndAlso Not (TypeOf Control Is RibbonPanelPopup) Then
						Continue For
					End If
					If item.Bounds.Contains(p) Then
						HittedItem = item
						Exit For
					End If
				Next
				Dim container As IContainsSelectableRibbonItems = TryCast(HittedItem, IContainsSelectableRibbonItems)
				Dim scrollable As IScrollableRibbonItem = TryCast(HittedItem, IScrollableRibbonItem)
				If container IsNot Nothing Then
					Dim sensibleBounds As Rectangle = If(scrollable <> Nothing, scrollable.ContentBounds, HittedItem.Bounds)
					For Each item As RibbonItem In container.GetItems()
						Dim actualBounds As Rectangle = item.Bounds
						actualBounds.Intersect(sensibleBounds)
						If actualBounds.Contains(p) Then
							HittedSubItem = item
						End If
					Next
				End If
			End If
		End Sub
		Private Sub RemoveHandlers()
			For Each item As RibbonItem In Items
				item.SetSelected(False)
				item.SetPressed(False)
			Next
			Control.MouseMove -= Control_MouseMove
			Control.MouseLeave -= Control_MouseLeave
			Control.MouseDown -= Control_MouseDown
			Control.MouseUp -= Control_MouseUp
		End Sub
		Public Sub [Resume]()
			_suspended = False
		End Sub
		Public Sub Suspend()
			_suspended = True
		End Sub
		Public Sub Dispose()
			_disposed = True
			RemoveHandlers()
		End Sub
	End Class
End Namespace
