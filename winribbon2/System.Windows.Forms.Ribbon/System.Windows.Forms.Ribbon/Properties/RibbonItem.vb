Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Drawing
Namespace System.Windows.Forms
	<DesignTimeVisible(False)> _
	Public MustInherit Class RibbonItem
		Inherits Component
		Implements IRibbonElement
		Private _text As String
		Private _image As Image
		Private _tooltip As String
		Private _checked As Boolean
		Private _selected As Boolean
		Private _owner As Ribbon
		Private _bounds As Rectangle
		Private _pressed As Boolean
		Private _tooltipTitle As String
		Private _tooltipImage As Image
		Private _enabled As Boolean
		Private _tag As Object
		Private _altKey As String
		Private _ownerTab As RibbonTab
		Private _ownerPanel As RibbonPanel
		Private _maxSize As RibbonElementSizeMode
		Private _minSize As RibbonElementSizeMode
		Private _lastMeasureSize As Size
		Private _ownerItem As RibbonItem
		Private _sizeMode As RibbonElementSizeMode
		Private _canvas As Control
		Public Overridable Event DoubleClick As EventHandler
		Public Overridable Event Click As EventHandler
		Public Overridable Event MouseUp As System.Windows.Forms.MouseEventHandler
		Public Overridable Event MouseMove As System.Windows.Forms.MouseEventHandler
		Public Overridable Event MouseDown As System.Windows.Forms.MouseEventHandler
		Public Overridable Event MouseEnter As System.Windows.Forms.MouseEventHandler
		Public Overridable Event MouseLeave As System.Windows.Forms.MouseEventHandler
		Public Overridable Event CanvasChanged As EventHandler
		Public Sub New()
			_enabled = True
			Click += New EventHandler(RibbonItem_Click)
		End Sub
		Private Sub RibbonItem_Click(sender As Object, e As EventArgs)
			Dim dd As RibbonDropDown = TryCast(Canvas, RibbonDropDown)
			If dd IsNot Nothing AndAlso dd.SelectionService IsNot Nothing Then
				dd.SelectionService.SetSelectedComponents(New Component() {Me}, System.ComponentModel.Design.SelectionTypes.Primary)
			End If
		End Sub
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property ContentBounds() As Rectangle
			Get
				Return Rectangle.FromLTRB(Bounds.Left + Owner.ItemMargin.Left, Bounds.Top + Owner.ItemMargin.Top, Bounds.Right - Owner.ItemMargin.Right, Bounds.Bottom - Owner.ItemMargin.Bottom)
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Canvas() As Control
			Get
				If _canvas IsNot Nothing AndAlso Not _canvas.IsDisposed Then
					Return _canvas
				End If
				Return Owner
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OwnerItem() As RibbonItem
			Get
				Return _ownerItem
			End Get
		End Property
		<DefaultValue("")> _
		<Localizable(True)> _
		Public Overridable Property Text() As String
			Get
				Return _text
			End Get
			Set
				_text = value
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		Public Overridable Property Image() As Image
			Get
				Return _image
			End Get
			Set
				_image = value
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		<DefaultValue("")> _
		<Localizable(True)> _
		Public Overridable Property ToolTip() As String
			Get
				Return _tooltip
			End Get
			Set
				_tooltip = value
			End Set
		End Property
		<DefaultValue(False)> _
		Public Overridable Property Checked() As Boolean
			Get
				Return _checked
			End Get
			Set
				_checked = value
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property SizeMode() As RibbonElementSizeMode
			Get
				Return _sizeMode
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property Selected() As Boolean
			Get
				Return _selected
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property Pressed() As Boolean
			Get
				Return _pressed
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Owner() As Ribbon
			Get
				Return _owner
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Bounds() As Rectangle
			Get
				Return _bounds
			End Get
		End Property
		<DefaultValue(True)> _
		Public Overridable Property Enabled() As Boolean
			Get
				Return _enabled
			End Get
			Set
				_enabled = value
				Dim container As IContainsSelectableRibbonItems = TryCast(Me, IContainsSelectableRibbonItems)
				If container IsNot Nothing Then
					For Each item As RibbonItem In container.GetItems()
						item.Enabled = value
					Next
				End If
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		<DefaultValue("")> _
		Public Property ToolTipTitle() As String
			Get
				Return _tooltipTitle
			End Get
			Set
				_tooltipTitle = value
			End Set
		End Property
		<DefaultValue("")> _
		Public Property ToolTipImage() As Image
			Get
				Return _tooltipImage
			End Get
			Set
				_tooltipImage = value
			End Set
		End Property
		Public Property Tag() As Object
			Get
				Return _tag
			End Get
			Set
				_tag = value
			End Set
		End Property
		<DefaultValue("")> _
		Public Property AltKey() As String
			Get
				Return _altKey
			End Get
			Set
				_altKey = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OwnerTab() As RibbonTab
			Get
				Return _ownerTab
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OwnerPanel() As RibbonPanel
			Get
				Return _ownerPanel
			End Get
		End Property
		<DefaultValue(RibbonElementSizeMode.None)> _
		Public Property MaxSizeMode() As RibbonElementSizeMode
			Get
				Return _maxSize
			End Get
			Set
				_maxSize = value
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		<DefaultValue(RibbonElementSizeMode.None)> _
		Public Property MinSizeMode() As RibbonElementSizeMode
			Get
				Return _minSize
			End Get
			Set
				_minSize = value
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property LastMeasuredSize() As Size
			Get
				Return _lastMeasureSize
			End Get
		End Property
		Protected Overridable Function ClosesDropDownAt(p As Point) As Boolean
			Return True
		End Function
		Protected Sub NotifyOwnerRegionsChanged()
			If Owner IsNot Nothing Then
				If Owner = Canvas Then
					Owner.OnRegionsChanged()
				ElseIf Canvas IsNot Nothing Then
					If TypeOf Canvas Is RibbonOrbDropDown Then
						(TryCast(Canvas, RibbonOrbDropDown)).OnRegionsChanged()
					Else
						Canvas.Invalidate(Bounds)
					End If
				End If
			End If
		End Sub
		Friend Overridable Sub SetOwnerItem(item As RibbonItem)
			_ownerItem = item
		End Sub
		Friend Overridable Sub SetOwner(owner As Ribbon)
			_owner = owner
		End Sub
		Friend Overridable Sub SetOwnerPanel(ownerPanel As RibbonPanel)
			_ownerPanel = ownerPanel
		End Sub
		Friend Overridable Sub SetSelected(selected As Boolean)
			If Not Enabled Then
				Return
			End If
			_selected = selected
		End Sub
		Friend Overridable Sub SetPressed(pressed As Boolean)
			_pressed = pressed
		End Sub
		Friend Overridable Sub SetOwnerTab(ownerTab As RibbonTab)
			_ownerTab = ownerTab
		End Sub
		Friend Overridable Sub SetOwnerGroup(ownerGroup As RibbonItemGroup)
			_ownerItem = ownerGroup
		End Sub
		Protected Function GetNearestSize(sizeMode As RibbonElementSizeMode) As RibbonElementSizeMode
			Dim size As Integer = CType(sizeMode, Integer)
			Dim max As Integer = CType(MaxSizeMode, Integer)
			Dim min As Integer = CType(MinSizeMode, Integer)
			Dim result As Integer = CType(sizeMode, Integer)
			If max > 0 AndAlso size > max Then
				result = max
			End If
			If min > 0 AndAlso size < min Then
				result = min
			End If
			Return CType(result, RibbonElementSizeMode)
		End Function
		Protected Sub SetLastMeasuredSize(size As Size)
			_lastMeasureSize = size
		End Sub
		Friend Overridable Sub SetSizeMode(sizeMode As RibbonElementSizeMode)
			_sizeMode = GetNearestSize(sizeMode)
		End Sub
		Public Overridable Sub OnCanvasChanged(e As EventArgs)
			If CanvasChanged IsNot Nothing Then
				CanvasChanged(Me, e)
			End If
		End Sub
		Public Overridable Sub OnMouseEnter(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseEnter IsNot Nothing Then
				MouseEnter(Me, e)
			End If
		End Sub
		Public Overridable Sub OnMouseDown(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseDown IsNot Nothing Then
				MouseDown(Me, e)
			End If
			Dim pop As RibbonPopup = TryCast(Canvas, RibbonPopup)
			If pop IsNot Nothing Then
				If ClosesDropDownAt(e.Location) Then
					RibbonPopupManager.Dismiss(RibbonPopupManager.DismissReason.ItemClicked)
				End If
				OnClick(EventArgs.Empty)
			End If
			SetPressed(True)
		End Sub
		Public Overridable Sub OnMouseLeave(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseLeave IsNot Nothing Then
				MouseLeave(Me, e)
			End If
		End Sub
		Public Overridable Sub OnMouseUp(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseUp IsNot Nothing Then
				MouseUp(Me, e)
			End If
			If Pressed Then
				SetPressed(False)
				RedrawItem()
			End If
		End Sub
		Public Overridable Sub OnMouseMove(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseMove IsNot Nothing Then
				MouseMove(Me, e)
			End If
		End Sub
		Public Overridable Sub OnClick(e As EventArgs)
			If Not Enabled Then
				Return
			End If
			If Click IsNot Nothing Then
				Click(Me, e)
			End If
		End Sub
		Public Overridable Sub OnDoubleClick(e As EventArgs)
			If Not Enabled Then
				Return
			End If
			If DoubleClick IsNot Nothing Then
				DoubleClick(Me, e)
			End If
		End Sub
		Public Overridable Sub RedrawItem()
			If Canvas IsNot Nothing Then
				Canvas.Invalidate(Rectangle.Inflate(Bounds, 1, 1))
			End If
		End Sub
		Friend Sub SetCanvas(canvas As Control)
			_canvas = canvas
			SetCanvas(TryCast(Me, IContainsSelectableRibbonItems), canvas)
			OnCanvasChanged(EventArgs.Empty)
		End Sub
		Private Sub SetCanvas(parent As IContainsSelectableRibbonItems, canvas As Control)
			If parent Is Nothing Then
				Return
			End If
			For Each item As RibbonItem In parent.GetItems()
				item.SetCanvas(canvas)
			Next
		End Sub
		Public MustOverride Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
		Public Overridable Sub SetBounds(bounds As Rectangle)
			_bounds = bounds
		End Sub
		Public MustOverride Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
	End Class
End Namespace
