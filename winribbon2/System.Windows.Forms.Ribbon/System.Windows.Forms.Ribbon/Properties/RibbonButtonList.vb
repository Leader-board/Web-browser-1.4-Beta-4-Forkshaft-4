Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.ComponentModel
Namespace System.Windows.Forms
	<Designer(GetType(RibbonButtonListDesigner))> _
	Public NotInheritable Class RibbonButtonList
		Inherits RibbonItem
		Implements IContainsSelectableRibbonItems
		Implements IScrollableRibbonItem
		Implements IContainsRibbonComponents
		Public Enum ListScrollType
			UpDownButtons
			Scrollbar
		End Enum
		Private _buttons As RibbonButtonCollection
		Private _itemsInLargeMode As Integer
		Private _itemsInMediumMode As Integer
		Private _ItemsInDropwDownMode As Size
		Private _buttonUpBounds As Rectangle
		Private _buttonDownBounds As Rectangle
		Private _buttonDropDownBounds As Rectangle
		Private _contentBounds As Rectangle
		Private _controlButtonsWidth As Integer
		Private _buttonsSizeMode As RibbonElementSizeMode
		Private _jumpDownSize As Integer
		Private _jumpUpSize As Integer
		Private _offset As Integer
		Private _buttonDownSelected As Boolean
		Private _buttonDownPressed As Boolean
		Private _buttonUpSelected As Boolean
		Private _buttonUpPressed As Boolean
		Private _buttonDropDownSelected As Boolean
		Private _buttonDropDownPressed As Boolean
		Private _buttonUpEnabled As Boolean
		Private _buttonDownEnabled As Boolean
		Private _dropDown As RibbonDropDown
		Private _dropDownVisible As Boolean
		Private _dropDownItems As RibbonItemCollection
		Private _thumbBounds As Rectangle
		Private _thumbSelected As Boolean
		Private _thumbPressed As Boolean
		Private _scrollValue As Integer
		Private fullContentBounds As Rectangle
		Private _scrollType As ListScrollType
		Private _scrollBarEnabled As Boolean
		Private _thumbOffset As Integer
		Private _avoidNextThumbMeasure As Boolean
		Private _flowToBottom As Boolean
		Public Sub New()
			_buttons = New RibbonButtonCollection(Me)
			_dropDownItems = New RibbonItemCollection()
			_controlButtonsWidth = 16
			_itemsInLargeMode = 7
			_itemsInMediumMode = 3
			_ItemsInDropwDownMode = New Size(7, 5)
			_buttonsSizeMode = RibbonElementSizeMode.Large
			_scrollType = ListScrollType.UpDownButtons
		End Sub
		Public Sub New(buttons As IEnumerable(Of RibbonButton))
			Me.New(buttons, Nothing)
		End Sub
		Public Sub New(buttons As IEnumerable(Of RibbonButton), dropDownItems As IEnumerable(Of RibbonItem))
			Me.New()
			If buttons IsNot Nothing Then
				Dim items As New List(Of RibbonButton)(buttons)
				_buttons.AddRange(items.ToArray())
			End If
			If dropDownItems IsNot Nothing Then
				_dropDownItems.AddRange(dropDownItems)
			End If
		End Sub
		<Description("If activated, buttons will flow to bottom inside the list")> _
		Public Property FlowToBottom() As Boolean
			Get
				Return _flowToBottom
			End Get
			Set
				_flowToBottom = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ScrollBarBounds() As Rectangle
			Get
				Return Rectangle.FromLTRB(ButtonUpBounds.Left, ButtonUpBounds.Top, ButtonDownBounds.Right, ButtonDownBounds.Bottom)
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ScrollBarEnabled() As Boolean
			Get
				Return _scrollBarEnabled
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ScrollType() As ListScrollType
			Get
				Return _scrollType
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property ScrolledPercent() As Double
			Get
				Return (CType(ContentBounds.Top, Double) - CType(fullContentBounds.Top, Double)) / (CType(fullContentBounds.Height, Double) - CType(ContentBounds.Height, Double))
			End Get
			Set
				_avoidNextThumbMeasure = True
				ScrollTo(-Convert.ToInt32(CType((fullContentBounds.Height - ContentBounds.Height), Double) * value))
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ScrollMinimum() As Integer
			Get
				If ScrollType = ListScrollType.Scrollbar Then
					Return ButtonUpBounds.Bottom
				Else
					Return 0
				End If
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ScrollMaximum() As Integer
			Get
				If ScrollType = ListScrollType.Scrollbar Then
					Return ButtonDownBounds.Top - ThumbBounds.Height
				Else
					Return 0
				End If
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property ScrollValue() As Integer
			Get
				Return _scrollValue
			End Get
			Set
				If value > ScrollMaximum OrElse value < ScrollMinimum Then
					Throw New IndexOutOfRangeException("Scroll value must exist between ScrollMinimum and Scroll Maximum")
				End If
				_thumbBounds.Y = value
				Dim scrolledPixels As Double = value - ScrollMinimum
				Dim pixelsAvailable As Double = ScrollMaximum - ScrollMinimum
				ScrolledPercent = scrolledPixels / pixelsAvailable
				_scrollValue = value
			End Set
		End Property
		Private Sub RedrawScroll()
			If Canvas IsNot Nothing Then
				Canvas.Invalidate(Rectangle.FromLTRB(ButtonDownBounds.X, ButtonUpBounds.Y, ButtonDownBounds.Right, ButtonDownBounds.Bottom))
			End If
		End Sub
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ThumbSelected() As Boolean
			Get
				Return _thumbSelected
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ThumbPressed() As Boolean
			Get
				Return _thumbPressed
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ThumbBounds() As Rectangle
			Get
				Return _thumbBounds
			End Get
		End Property
		Public ReadOnly Property ButtonDropDownPresent() As Boolean
			Get
				Return ButtonDropDownBounds.Height > 0
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property DropDownItems() As RibbonItemCollection
			Get
				Return _dropDownItems
			End Get
		End Property
		Public Property ButtonsSizeMode() As RibbonElementSizeMode
			Get
				Return _buttonsSizeMode
			End Get
			Set
				_buttonsSizeMode = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonUpEnabled() As Boolean
			Get
				Return _buttonUpEnabled
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonDownEnabled() As Boolean
			Get
				Return _buttonDownEnabled
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonDropDownSelected() As Boolean
			Get
				Return _buttonDropDownSelected
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonDropDownPressed() As Boolean
			Get
				Return _buttonDropDownPressed
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonDownSelected() As Boolean
			Get
				Return _buttonDownSelected
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonDownPressed() As Boolean
			Get
				Return _buttonDownPressed
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonUpSelected() As Boolean
			Get
				Return _buttonUpSelected
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonUpPressed() As Boolean
			Get
				Return _buttonUpPressed
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overrides ReadOnly Property ContentBounds() As Rectangle
			Get
				Return _contentBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonUpBounds() As Rectangle
			Get
				Return _buttonUpBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonDownBounds() As Rectangle
			Get
				Return _buttonDownBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonDropDownBounds() As Rectangle
			Get
				Return _buttonDropDownBounds
			End Get
		End Property
		<DefaultValue(16)> _
		Public Property ControlButtonsWidth() As Integer
			Get
				Return _controlButtonsWidth
			End Get
			Set
				_controlButtonsWidth = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<DefaultValue(7)> _
		Public Property ItemsWideInLargeMode() As Integer
			Get
				Return _itemsInLargeMode
			End Get
			Set
				_itemsInLargeMode = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<DefaultValue(3)> _
		Public Property ItemsWideInMediumMode() As Integer
			Get
				Return _itemsInMediumMode
			End Get
			Set
				_itemsInMediumMode = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		Public Property ItemsSizeInDropwDownMode() As Size
			Get
				Return _ItemsInDropwDownMode
			End Get
			Set
				_ItemsInDropwDownMode = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property Buttons() As RibbonButtonCollection
			Get
				Return _buttons
			End Get
		End Property
		Private Sub IgnoreDeactivation()
			If TypeOf Canvas Is RibbonPanelPopup Then
				(TryCast(Canvas, RibbonPanelPopup)).IgnoreNextClickDeactivation()
			End If
			If TypeOf Canvas Is RibbonDropDown Then
				(TryCast(Canvas, RibbonDropDown)).IgnoreNextClickDeactivation()
			End If
		End Sub
		Private Sub RedrawControlButtons()
			If Canvas IsNot Nothing Then
				If ScrollType = ListScrollType.Scrollbar Then
					Canvas.Invalidate(ScrollBarBounds)
				Else
					Canvas.Invalidate(Rectangle.FromLTRB(ButtonUpBounds.Left, ButtonUpBounds.Top, ButtonDropDownBounds.Right, ButtonDropDownBounds.Bottom))
				End If
			End If
		End Sub
		Private Sub ScrollOffset(amount As Integer)
			ScrollTo(_offset + amount)
		End Sub
		Private Sub ScrollTo(offset As Integer)
			Dim minOffset As Integer = ContentBounds.Y - fullContentBounds.Height + ContentBounds.Height
			If offset < minOffset Then
				offset = minOffset
			End If
			_offset = offset
			SetBounds(Bounds)
			RedrawItem()
		End Sub
		Public Sub ScrollDown()
			ScrollOffset(-(_jumpDownSize + 1))
		End Sub
		Public Sub ScrollUp()
			ScrollOffset(_jumpDownSize + 1)
		End Sub
		Public Sub ShowDropDown()
			If DropDownItems.Count = 0 Then
				SetPressed(False)
				Return
			End If
			IgnoreDeactivation()
			_dropDown = New RibbonDropDown(Me, DropDownItems, Owner)
			_dropDown.ShowSizingGrip = True
			Dim location As Point = Canvas.PointToScreen(New Point(Bounds.Left, Bounds.Top))
			SetDropDownVisible(True)
			_dropDown.Show(location)
		End Sub
		Private Sub dropDown_FormClosed(sender As Object, e As FormClosedEventArgs)
			SetDropDownVisible(False)
		End Sub
		Public Sub CloseDropDown()
			If _dropDown IsNot Nothing Then
			End If
			SetDropDownVisible(False)
		End Sub
		Friend Sub SetDropDownVisible(visible As Boolean)
			_dropDownVisible = visible
		End Sub
		Public Overrides Sub OnCanvasChanged(e As EventArgs)
			MyBase.OnCanvasChanged(e)
			If TypeOf Canvas Is RibbonDropDown Then
				_scrollType = ListScrollType.Scrollbar
			Else
				_scrollType = ListScrollType.UpDownButtons
			End If
		End Sub
		Protected Overrides Function ClosesDropDownAt(p As Point) As Boolean
			Return Not (ButtonDropDownBounds.Contains(p) OrElse ButtonDownBounds.Contains(p) OrElse ButtonUpBounds.Contains(p) OrElse (ScrollType = ListScrollType.Scrollbar AndAlso ScrollBarBounds.Contains(p)))
		End Function
		Friend Overrides Sub SetOwner(owner As Ribbon)
			MyBase.SetOwner(owner)
			_buttons.SetOwner(owner)
			_dropDownItems.SetOwner(owner)
		End Sub
		Friend Overrides Sub SetOwnerPanel(ownerPanel As RibbonPanel)
			MyBase.SetOwnerPanel(ownerPanel)
			_buttons.SetOwnerPanel(ownerPanel)
			_dropDownItems.SetOwnerPanel(ownerPanel)
		End Sub
		Friend Overrides Sub SetOwnerTab(ownerTab As RibbonTab)
			MyBase.SetOwnerTab(ownerTab)
			_buttons.SetOwnerTab(ownerTab)
			_dropDownItems.SetOwnerTab(OwnerTab)
		End Sub
		Public Overrides Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
			Owner.Renderer.OnRenderRibbonItem(New RibbonItemRenderEventArgs(Owner, e.Graphics, e.Clip, Me))
			If e.Mode <> RibbonElementSizeMode.Compact Then
				Dim lastClip As Region = e.Graphics.Clip
				Dim newClip As New Region(lastClip.GetBounds(e.Graphics))
				newClip.Intersect(ContentBounds)
				e.Graphics.SetClip(newClip.GetBounds(e.Graphics))
				For Each button As RibbonButton In Buttons
					If Not button.Bounds.IsEmpty Then
						button.OnPaint(Me, New RibbonElementPaintEventArgs(button.Bounds, e.Graphics, ButtonsSizeMode))
					End If
				Next
				e.Graphics.SetClip(lastClip.GetBounds(e.Graphics))
			End If
		End Sub
		Public Overrides Sub SetBounds(bounds As System.Drawing.Rectangle)
			MyBase.SetBounds(bounds)
			If ScrollType <> ListScrollType.Scrollbar Then
				Dim cbtns As Integer = 3
				Dim buttonHeight As Integer = bounds.Height / cbtns
				Dim buttonWidth As Integer = _controlButtonsWidth
				_buttonUpBounds = Rectangle.FromLTRB(bounds.Right - buttonWidth, bounds.Top, bounds.Right, bounds.Top + buttonHeight)
				_buttonDownBounds = Rectangle.FromLTRB(_buttonUpBounds.Left, _buttonUpBounds.Bottom, bounds.Right, _buttonUpBounds.Bottom + buttonHeight)
				If cbtns = 2 Then
					_buttonDropDownBounds = Rectangle.Empty
				Else
					_buttonDropDownBounds = Rectangle.FromLTRB(_buttonDownBounds.Left, _buttonDownBounds.Bottom, bounds.Right, bounds.Bottom + 1)
				End If
				_thumbBounds.Location = Point.Empty
			Else
				Dim bwidth As Integer = ThumbBounds.Width
				Dim bheight As Integer = ThumbBounds.Width
				_buttonUpBounds = Rectangle.FromLTRB(bounds.Right - bwidth, bounds.Top, bounds.Right, bounds.Top + bheight)
				_buttonDownBounds = Rectangle.FromLTRB(_buttonUpBounds.Left, bounds.Height - bheight, bounds.Right, bounds.Height)
				_buttonDropDownBounds = Rectangle.Empty
				_thumbBounds.X = _buttonUpBounds.Left
			End If
			_contentBounds = Rectangle.FromLTRB(bounds.Left, bounds.Top, _buttonUpBounds.Left, bounds.Bottom)
			_buttonUpEnabled = _offset < 0
			If Not _buttonUpEnabled Then
				_offset = 0
			End If
			_buttonDownEnabled = False
			Dim curLeft As Integer = ContentBounds.Left + 1
			Dim curTop As Integer = ContentBounds.Top + 1 + _offset
			Dim maxBottom As Integer = curTop
			Dim iniTop As Integer = curTop
			For Each item As RibbonItem In Buttons
				item.SetBounds(Rectangle.Empty)
			Next
			Dim i As Integer = 0
			While i < Buttons.Count
				Dim button As RibbonButton = TryCast(Buttons(i), RibbonButton)
				If button Is Nothing Then
					Exit While
				End If
				If curLeft + button.LastMeasuredSize.Width > ContentBounds.Right Then
					curLeft = ContentBounds.Left + 1
					curTop = maxBottom + 1
				End If
				button.SetBounds(New Rectangle(curLeft, curTop, button.LastMeasuredSize.Width, button.LastMeasuredSize.Height))
				curLeft = button.Bounds.Right + 1
				maxBottom = Math.Max(maxBottom, button.Bounds.Bottom)
				If button.Bounds.Bottom > ContentBounds.Bottom Then
					_buttonDownEnabled = True
				End If
				_jumpDownSize = button.Bounds.Height
				_jumpUpSize = button.Bounds.Height
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While
			Dim contentHeight As Double = maxBottom - iniTop
			Dim viewHeight As Double = bounds.Height
			If contentHeight > viewHeight AndAlso contentHeight <> 0 Then
				Dim viewPercent As Double = If(contentHeight > viewHeight, viewHeight / contentHeight, 0.0)
				Dim availHeight As Double = ButtonDownBounds.Top - ButtonUpBounds.Bottom
				Dim thumbHeight As Double = Math.Ceiling(viewPercent * availHeight)
				If thumbHeight < 30 Then
					If availHeight >= 30 Then
						thumbHeight = 30
					Else
						thumbHeight = availHeight
					End If
				End If
				_thumbBounds.Height = Convert.ToInt32(thumbHeight)
				fullContentBounds = Rectangle.FromLTRB(ContentBounds.Left, iniTop, ContentBounds.Right, maxBottom)
				_scrollBarEnabled = True
				UpdateThumbPos()
			Else
				_scrollBarEnabled = False
			End If
		End Sub
		Private Sub UpdateThumbPos()
			If _avoidNextThumbMeasure Then
				_avoidNextThumbMeasure = False
				Return
			End If
			Dim scrolledp As Double = ScrolledPercent
			If Not Double.IsInfinity(scrolledp) Then
				Dim availSpace As Double = ScrollMaximum - ScrollMinimum
				Dim scrolledSpace As Double = Math.Ceiling(availSpace * ScrolledPercent)
				_thumbBounds.Y = ScrollMinimum + Convert.ToInt32(scrolledSpace)
			Else
				_thumbBounds.Y = ScrollMinimum
			End If
			If _thumbBounds.Y > ScrollMaximum Then
				_thumbBounds.Y = ScrollMaximum
			End If
		End Sub
		Public Overrides Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim itemsWide As Integer = 0
			Select Case e.SizeMode
				Case RibbonElementSizeMode.DropDown
					itemsWide = ItemsSizeInDropwDownMode.Width
					Exit Select
				Case RibbonElementSizeMode.Large
					itemsWide = ItemsWideInLargeMode
					Exit Select
				Case RibbonElementSizeMode.Medium
					itemsWide = ItemsWideInMediumMode
					Exit Select
				Case RibbonElementSizeMode.Compact
					itemsWide = 0
					Exit Select
			End Select
			Dim height As Integer = OwnerPanel.ContentBounds.Height - Owner.ItemPadding.Vertical - 4
			Dim scannedItems As Integer = 0
			Dim widthSum As Integer = 1
			Dim buttonHeight As Integer = 0
			Dim heightSum As Integer = 0
			Dim sumWidth As Boolean = True
			For Each button As RibbonButton In Buttons
				Dim s As Size = button.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(e.Graphics, Me.ButtonsSizeMode))
				If sumWidth Then
					widthSum += s.Width + 1
				End If
				buttonHeight = button.LastMeasuredSize.Height
				heightSum += buttonHeight
				If System.Threading.Interlocked.Increment(scannedItems) = itemsWide Then
					sumWidth = False
				End If
			Next
			If e.SizeMode = RibbonElementSizeMode.DropDown Then
				height = buttonHeight * ItemsSizeInDropwDownMode.Height
			End If
			If ScrollBarRenderer.IsSupported Then
				_thumbBounds = New Rectangle(Point.Empty, ScrollBarRenderer.GetSizeBoxSize(e.Graphics, System.Windows.Forms.VisualStyles.ScrollBarState.Normal))
			Else
				_thumbBounds = New Rectangle(Point.Empty, New Size(16, 16))
			End If
			SetLastMeasuredSize(New Size(widthSum + ControlButtonsWidth, height))
			Return LastMeasuredSize
		End Function
		Friend Overrides Sub SetSizeMode(sizeMode As RibbonElementSizeMode)
			MyBase.SetSizeMode(sizeMode)
			For Each item As RibbonItem In Buttons
				item.SetSizeMode(ButtonsSizeMode)
			Next
		End Sub
		Public Overrides Sub OnMouseMove(e As MouseEventArgs)
			MyBase.OnMouseMove(e)
			If ButtonDownPressed AndAlso ButtonDownSelected AndAlso ButtonDownEnabled Then
				ScrollOffset(-1)
			End If
			If ButtonUpPressed AndAlso ButtonUpSelected AndAlso ButtonUpEnabled Then
				ScrollOffset(1)
			End If
			Dim upCache As Boolean = _buttonUpSelected
			Dim downCache As Boolean = _buttonDownSelected
			Dim dropCache As Boolean = _buttonDropDownSelected
			Dim thumbCache As Boolean = _thumbSelected
			_buttonUpSelected = _buttonUpBounds.Contains(e.Location)
			_buttonDownSelected = _buttonDownBounds.Contains(e.Location)
			_buttonDropDownSelected = _buttonDropDownBounds.Contains(e.Location)
			_thumbSelected = _thumbBounds.Contains(e.Location) AndAlso ScrollType = ListScrollType.Scrollbar AndAlso ScrollBarEnabled
			If (upCache <> _buttonUpSelected) OrElse (downCache <> _buttonDownSelected) OrElse (dropCache <> _buttonDropDownSelected) OrElse (thumbCache <> _thumbSelected) Then
				RedrawControlButtons()
			End If
			If ThumbPressed Then
				Dim newval As Integer = e.Y - _thumbOffset
				If newval < ScrollMinimum Then
					newval = ScrollMinimum
				ElseIf newval > ScrollMaximum Then
					newval = ScrollMaximum
				End If
				ScrollValue = newval
				RedrawScroll()
			End If
		End Sub
		Public Overrides Sub OnMouseLeave(e As MouseEventArgs)
			MyBase.OnMouseLeave(e)
			Dim mustRedraw As Boolean = _buttonUpSelected OrElse _buttonDownSelected OrElse _buttonDropDownSelected
			_buttonUpSelected = False
			_buttonDownSelected = False
			_buttonDropDownSelected = False
			If mustRedraw Then
				RedrawControlButtons()
			End If
		End Sub
		Public Overrides Sub OnMouseDown(e As MouseEventArgs)
			MyBase.OnMouseDown(e)
			If ButtonDownSelected OrElse ButtonUpSelected OrElse ButtonDropDownSelected Then
				IgnoreDeactivation()
			End If
			If ButtonDownSelected AndAlso ButtonDownEnabled Then
				_buttonDownPressed = True
				ScrollDown()
			End If
			If ButtonUpSelected AndAlso ButtonUpEnabled Then
				_buttonUpPressed = True
				ScrollUp()
			End If
			If ButtonDropDownSelected Then
				_buttonDropDownPressed = True
				ShowDropDown()
			End If
			If ThumbSelected Then
				_thumbPressed = True
				_thumbOffset = e.Y - _thumbBounds.Y
			End If
			If ScrollType = ListScrollType.Scrollbar AndAlso ScrollBarBounds.Contains(e.Location) AndAlso e.Y >= ButtonUpBounds.Bottom AndAlso e.Y <= ButtonDownBounds.Y AndAlso Not ThumbBounds.Contains(e.Location) AndAlso Not ButtonDownBounds.Contains(e.Location) AndAlso Not ButtonUpBounds.Contains(e.Location) Then
				If e.Y < ThumbBounds.Y Then
					ScrollUp()
				Else
					ScrollDown()
				End If
			End If
		End Sub
		Public Overrides Sub OnMouseUp(e As MouseEventArgs)
			MyBase.OnMouseUp(e)
			_buttonDownPressed = False
			_buttonUpPressed = False
			_buttonDropDownPressed = False
			_thumbPressed = False
		End Sub
		Public Function GetItems() As IEnumerable(Of RibbonItem)
			Return Buttons
		End Function
		Public Function GetContentBounds() As Rectangle
			Return ContentBounds
		End Function
		Public Function GetAllChildComponents() As IEnumerable(Of Component)
			Dim result As New List(Of Component)(Buttons.ToArray())
			result.AddRange(DropDownItems.ToArray())
			Return result
		End Function
	End Class
End Namespace
