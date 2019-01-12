Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.ComponentModel
Imports System.ComponentModel.Design
Namespace System.Windows.Forms
	<Designer(GetType(RibbonButtonDesigner))> _
	Public Class RibbonButton
		Inherits RibbonItem
		Implements IContainsRibbonComponents
		Private Const arrowWidth As Integer = 5
		Private _style As RibbonButtonStyle
		Private _dropDownBounds As Rectangle
		Private _buttonFaceBounds As Rectangle
		Private _dropDownItems As RibbonItemCollection
		Private _dropDownPressed As Boolean
		Private _dropDownSelected As Boolean
		Private _smallImage As Image
		Private _dropDownArrowSize As Size
		Private _dropDownMargin As Padding
		Private _dropDownVisible As Boolean
		Private _dropDown As RibbonDropDown
		Private _imageBounds As Rectangle
		Private _textBounds As Rectangle
		Private _dropDownResizable As Boolean
		Private _checkOnClick As Boolean
		Private _lastMousePos As Point
		Private _DropDownArrowDirection As RibbonArrowDirection
		Public Event DropDownShowing As EventHandler
		Public Sub New()
			_dropDownItems = New RibbonItemCollection()
			_dropDownArrowSize = New Size(5, 3)
			_dropDownMargin = New Padding(6)
			_DropDownArrowDirection = RibbonArrowDirection.Down
			Image = CreateImage(32)
			SmallImage = CreateImage(16)
		End Sub
		Public Sub New(text As String)
			Me.New()
			Text = text
		End Sub
		Public Sub New(smallImage As Image)
			Me.New()
			SmallImage = smallImage
		End Sub
		Friend ReadOnly Property DropDown() As RibbonDropDown
			Get
				Return _dropDown
			End Get
		End Property
		<DefaultValue(False)> _
		<Description("Toggles the Checked property of the button when clicked")> _
		Public Property CheckOnClick() As Boolean
			Get
				Return _checkOnClick
			End Get
			Set
				_checkOnClick = value
			End Set
		End Property
		<DefaultValue(False)> _
		<Description("Makes the DropDown resizable with a grip on the corner")> _
		Public Property DropDownResizable() As Boolean
			Get
				Return _dropDownResizable
			End Get
			Set
				_dropDownResizable = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ImageBounds() As Rectangle
			Get
				Return _imageBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property TextBounds() As Rectangle
			Get
				Return _textBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property DropDownVisible() As Boolean
			Get
				Return _dropDownVisible
			End Get
		End Property
		Public Property DropDownArrowSize() As Size
			Get
				Return _dropDownArrowSize
			End Get
			Set
				_dropDownArrowSize = value
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		Public Property DropDownArrowDirection() As RibbonArrowDirection
			Get
				Return _DropDownArrowDirection
			End Get
			Set
				_DropDownArrowDirection = value
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		Public Property Style() As RibbonButtonStyle
			Get
				Return _style
			End Get
			Set
				_style = value
				If TypeOf Canvas Is RibbonPopup OrElse (OwnerItem IsNot Nothing AndAlso TypeOf OwnerItem.Canvas Is RibbonPopup) Then
					DropDownArrowDirection = RibbonArrowDirection.Left
				End If
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property DropDownItems() As RibbonItemCollection
			Get
				Return _dropDownItems
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property ButtonFaceBounds() As Rectangle
			Get
				Return _buttonFaceBounds
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property DropDownBounds() As Rectangle
			Get
				Return _dropDownBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property DropDownSelected() As Boolean
			Get
				Return _dropDownSelected
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property DropDownPressed() As Boolean
			Get
				Return _dropDownPressed
			End Get
		End Property
		<DefaultValue(Nothing)> _
		Public Overridable Property SmallImage() As Image
			Get
				Return _smallImage
			End Get
			Set
				_smallImage = value
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		Protected Sub SetDropDownMargin(p As Padding)
			_dropDownMargin = p
		End Sub
		Public Sub PerformClick()
			OnClick(EventArgs.Empty)
		End Sub
		Private Function CreateImage(size As Integer) As Image
			Dim bmp As New Bitmap(size, size)
			Return bmp
		End Function
		Protected Overridable Sub CreateDropDown()
			_dropDown = New RibbonDropDown(Me, DropDownItems, Owner)
		End Sub
		Friend Overrides Sub SetPressed(pressed As Boolean)
			MyBase.SetPressed(pressed)
		End Sub
		Friend Overrides Sub SetOwner(owner As Ribbon)
			MyBase.SetOwner(owner)
			If _dropDownItems IsNot Nothing Then
				_dropDownItems.SetOwner(owner)
			End If
		End Sub
		Friend Overrides Sub SetOwnerPanel(ownerPanel As RibbonPanel)
			MyBase.SetOwnerPanel(ownerPanel)
			If _dropDownItems IsNot Nothing Then
				_dropDownItems.SetOwnerPanel(ownerPanel)
			End If
		End Sub
		Friend Overrides Sub SetOwnerTab(ownerTab As RibbonTab)
			MyBase.SetOwnerTab(ownerTab)
			If _dropDownItems IsNot Nothing Then
				_dropDownItems.SetOwnerTab(ownerTab)
			End If
		End Sub
		Public Overrides Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
			If Owner Is Nothing Then
				Return
			End If
			OnPaintBackground(e)
			OnPaintImage(e)
			OnPaintText(e)
		End Sub
		Protected Overridable Sub OnPaintText(e As RibbonElementPaintEventArgs)
			If SizeMode <> RibbonElementSizeMode.Compact Then
				Dim sf As New StringFormat()
				sf.LineAlignment = StringAlignment.Center
				sf.Alignment = StringAlignment.Near
				If SizeMode = RibbonElementSizeMode.Large Then
					sf.Alignment = StringAlignment.Center
					If Not String.IsNullOrEmpty(Text) AndAlso Not Text.Contains(" ") Then
						sf.LineAlignment = StringAlignment.Near
					End If
				End If
				Owner.Renderer.OnRenderRibbonItemText(New RibbonTextEventArgs(Owner, e.Graphics, e.Clip, Me, TextBounds, Text, _
					sf))
			End If
		End Sub
		Private Sub OnPaintImage(e As RibbonElementPaintEventArgs)
			Dim theSize As RibbonElementSizeMode = GetNearestSize(e.Mode)
			If (theSize = RibbonElementSizeMode.Large AndAlso Image <> Nothing) OrElse SmallImage IsNot Nothing Then
				Owner.Renderer.OnRenderRibbonItemImage(New RibbonItemBoundsEventArgs(Owner, e.Graphics, e.Clip, Me, OnGetImageBounds(theSize, Bounds)))
			End If
		End Sub
		Private Sub OnPaintBackground(e As RibbonElementPaintEventArgs)
			Owner.Renderer.OnRenderRibbonItem(New RibbonItemRenderEventArgs(Owner, e.Graphics, e.Clip, Me))
		End Sub
		Public Overrides Sub SetBounds(bounds As System.Drawing.Rectangle)
			MyBase.SetBounds(bounds)
			Dim sMode As RibbonElementSizeMode = GetNearestSize(SizeMode)
			_imageBounds = OnGetImageBounds(sMode, bounds)
			_textBounds = OnGetTextBounds(sMode, bounds)
			If Style = RibbonButtonStyle.SplitDropDown Then
				_dropDownBounds = OnGetDropDownBounds(sMode, bounds)
				_buttonFaceBounds = OnGetButtonFaceBounds(sMode, bounds)
			End If
		End Sub
		Friend Overridable Function OnGetImageBounds(sMode As RibbonElementSizeMode, bounds As Rectangle) As Rectangle
			If sMode = RibbonElementSizeMode.Large Then
				If Image IsNot Nothing Then
					Return New Rectangle(Bounds.Left + ((Bounds.Width - Image.Width) / 2), Bounds.Top + Owner.ItemMargin.Top, Image.Width, Image.Height)
				Else
					Return New Rectangle(ContentBounds.Location, New Size(32, 32))
				End If
			Else
				If SmallImage IsNot Nothing Then
					Return New Rectangle(Bounds.Left + Owner.ItemMargin.Left, Bounds.Top + ((Bounds.Height - SmallImage.Height) / 2), SmallImage.Width, SmallImage.Height)
				Else
					Return New Rectangle(ContentBounds.Location, New Size(0, 0))
				End If
			End If
		End Function
		Friend Overridable Function OnGetTextBounds(sMode As RibbonElementSizeMode, bounds As Rectangle) As Rectangle
			Dim imgw As Integer = _imageBounds.Width
			Dim imgh As Integer = _imageBounds.Height
			If sMode = RibbonElementSizeMode.Large Then
				Return Rectangle.FromLTRB(Bounds.Left + Owner.ItemMargin.Left, Bounds.Top + Owner.ItemMargin.Top + imgh, Bounds.Right - Owner.ItemMargin.Right, Bounds.Bottom - Owner.ItemMargin.Bottom)
			Else
				Dim ddw As Integer = If(Style <> RibbonButtonStyle.Normal, _dropDownMargin.Horizontal, 0)
				Return Rectangle.FromLTRB(Bounds.Left + imgw + Owner.ItemMargin.Horizontal + Owner.ItemMargin.Left, Bounds.Top + Owner.ItemMargin.Top, Bounds.Right - ddw, Bounds.Bottom - Owner.ItemMargin.Bottom)
			End If
		End Function
		Friend Overridable Function OnGetDropDownBounds(sMode As RibbonElementSizeMode, bounds As Rectangle) As Rectangle
			Dim sideBounds As Rectangle = Rectangle.FromLTRB(bounds.Right - _dropDownMargin.Horizontal - 2, bounds.Top, bounds.Right, bounds.Bottom)
			Select Case SizeMode
				Case RibbonElementSizeMode.Large, RibbonElementSizeMode.Overflow
					Return Rectangle.FromLTRB(bounds.Left, bounds.Top + Image.Height + Owner.ItemMargin.Vertical, bounds.Right, bounds.Bottom)
				Case RibbonElementSizeMode.DropDown, RibbonElementSizeMode.Medium, RibbonElementSizeMode.Compact
					Return sideBounds
			End Select
			Return Rectangle.Empty
		End Function
		Friend Overridable Function OnGetButtonFaceBounds(sMode As RibbonElementSizeMode, bounds As Rectangle) As Rectangle
			Dim sideBounds As Rectangle = Rectangle.FromLTRB(bounds.Right - _dropDownMargin.Horizontal - 2, bounds.Top, bounds.Right, bounds.Bottom)
			Select Case SizeMode
				Case RibbonElementSizeMode.Large, RibbonElementSizeMode.Overflow
					Return Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right, _dropDownBounds.Top)
				Case RibbonElementSizeMode.DropDown, RibbonElementSizeMode.Medium, RibbonElementSizeMode.Compact
					Return Rectangle.FromLTRB(bounds.Left, bounds.Top, _dropDownBounds.Left, bounds.Bottom)
			End Select
			Return Rectangle.Empty
		End Function
		Public Shared Function MeasureStringLargeSize(g As Graphics, text As String, font As Font) As Size
			If String.IsNullOrEmpty(text) Then
				Return Size.Empty
			End If
			Dim sz As Size = g.MeasureString(text, font).ToSize()
			Dim words As String() = text.Split(" "C)
			Dim longestWord As String = String.Empty
			Dim width As Integer = sz.Width
			Dim i As Integer = 0
			While i < words.Length
				If words(i).Length > longestWord.Length Then
					longestWord = words(i)
				End If
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While
			If words.Length > 1 Then
				width = Math.Max(sz.Width / 2, g.MeasureString(longestWord, font).ToSize().Width) + 1
			Else
				Return g.MeasureString(text, font).ToSize()
			End If
			Dim rs As Size = g.MeasureString(text, font, width).ToSize()
			Return New Size(rs.Width, rs.Height)
		End Function
		Public Overrides Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim theSize As RibbonElementSizeMode = GetNearestSize(e.SizeMode)
			Dim widthSum As Integer = Owner.ItemMargin.Horizontal
			Dim heightSum As Integer = Owner.ItemMargin.Vertical
			Dim largeHeight As Integer = If(OwnerPanel = Nothing, 0, OwnerPanel.ContentBounds.Height - Owner.ItemPadding.Vertical)
			Dim simg As Size = If(SmallImage <> Nothing, SmallImage.Size, Size.Empty)
			Dim img As Size = If(Image <> Nothing, Image.Size, Size.Empty)
			Dim sz As Size = Size.Empty
			Select Case theSize
				Case RibbonElementSizeMode.Large, RibbonElementSizeMode.Overflow
					sz = MeasureStringLargeSize(e.Graphics, Text, Owner.Font)
					If Not String.IsNullOrEmpty(Text) Then
						widthSum += Math.Max(sz.Width + 1, img.Width)
						heightSum = largeHeight
					Else
						widthSum += img.Width
						heightSum += img.Height
					End If
					Exit Select
				Case RibbonElementSizeMode.DropDown, RibbonElementSizeMode.Medium
					sz = TextRenderer.MeasureText(Text, Owner.Font)
					If Not String.IsNullOrEmpty(Text) Then
						widthSum += sz.Width + 1
					End If
					widthSum += simg.Width + Owner.ItemMargin.Horizontal
					heightSum += Math.Max(sz.Height, simg.Height)
					Exit Select
				Case RibbonElementSizeMode.Compact
					widthSum += simg.Width
					heightSum += simg.Height
					Exit Select
				Case Else
					Throw New ApplicationException("SizeMode not supported: " + e.SizeMode.ToString())
			End Select
			If theSize = RibbonElementSizeMode.DropDown Then
				heightSum += 2
			End If
			If Style = RibbonButtonStyle.DropDown Then
				widthSum += arrowWidth + Owner.ItemMargin.Right
			ElseIf Style = RibbonButtonStyle.SplitDropDown Then
				widthSum += arrowWidth + Owner.ItemMargin.Horizontal
			End If
			SetLastMeasuredSize(New Size(widthSum, heightSum))
			Return LastMeasuredSize
		End Function
		Friend Sub SetDropDownPressed(pressed As Boolean)
			Throw New System.NotImplementedException()
		End Sub
		Friend Sub SetDropDownSelected(selected As Boolean)
			Throw New Exception()
		End Sub
		Public Sub ShowDropDown()
			If Style = RibbonButtonStyle.Normal OrElse DropDownItems.Count = 0 Then
				If DropDown IsNot Nothing Then
					RibbonPopupManager.DismissChildren(DropDown, RibbonPopupManager.DismissReason.NewPopup)
				End If
				Return
			End If
			If Style = RibbonButtonStyle.DropDown Then
				SetPressed(True)
			Else
				_dropDownPressed = True
			End If
			CreateDropDown()
			DropDown.MouseEnter += New EventHandler(DropDown_MouseEnter)
			DropDown.Closed += New EventHandler(_dropDown_Closed)
			DropDown.ShowSizingGrip = DropDownResizable
			Dim canvasdd As RibbonPopup = TryCast(Canvas, RibbonPopup)
			Dim location As Point = OnGetDropDownMenuLocation()
			Dim minsize As Size = OnGetDropDownMenuSize()
			If Not minsize.IsEmpty Then
				DropDown.MinimumSize = minsize
			End If
			OnDropDownShowing(EventArgs.Empty)
			SetDropDownVisible(True)
			DropDown.SelectionService = TryCast(GetService(GetType(ISelectionService)), ISelectionService)
			DropDown.Show(location)
		End Sub
		Sub DropDown_MouseEnter(sender As Object, e As EventArgs)
			SetSelected(True)
			RedrawItem()
		End Sub
		Friend Overridable Function OnGetDropDownMenuLocation() As Point
			Dim location As Point = Point.Empty
			If TypeOf Canvas Is RibbonDropDown Then
				location = Canvas.PointToScreen(New Point(Bounds.Right, Bounds.Top))
			Else
				location = Canvas.PointToScreen(New Point(Bounds.Left, Bounds.Bottom))
			End If
			Return location
		End Function
		Friend Overridable Function OnGetDropDownMenuSize() As Size
			Return Size.Empty
		End Function
		Private Sub _dropDown_Closed(sender As Object, e As EventArgs)
			SetPressed(False)
			_dropDownPressed = False
			SetDropDownVisible(False)
			SetSelected(False)
			RedrawItem()
		End Sub
		Private Sub IgnoreDeactivation()
			If TypeOf Canvas Is RibbonPanelPopup Then
				(TryCast(Canvas, RibbonPanelPopup)).IgnoreNextClickDeactivation()
			End If
			If TypeOf Canvas Is RibbonDropDown Then
				(TryCast(Canvas, RibbonDropDown)).IgnoreNextClickDeactivation()
			End If
		End Sub
		Public Sub CloseDropDown()
			If DropDown IsNot Nothing Then
				RibbonPopupManager.Dismiss(DropDown, RibbonPopupManager.DismissReason.NewPopup)
			End If
			SetDropDownVisible(False)
		End Sub
		Public Overrides Function ToString() As String
			Return String.Format("{1}: {0}", Text, [GetType]().Name)
		End Function
		Friend Sub SetDropDownVisible(visible As Boolean)
			_dropDownVisible = visible
		End Sub
		Public Sub OnDropDownShowing(e As EventArgs)
			If DropDownShowing IsNot Nothing Then
				DropDownShowing(Me, e)
			End If
		End Sub
		Public Overrides Sub OnCanvasChanged(e As EventArgs)
			MyBase.OnCanvasChanged(e)
			If TypeOf Canvas Is RibbonDropDown Then
				DropDownArrowDirection = RibbonArrowDirection.Left
			End If
		End Sub
		Protected Overrides Function ClosesDropDownAt(p As Point) As Boolean
			If Style = RibbonButtonStyle.DropDown Then
				Return False
			ElseIf Style = RibbonButtonStyle.SplitDropDown Then
				Return ButtonFaceBounds.Contains(p)
			Else
				Return True
			End If
		End Function
		Friend Overrides Sub SetSizeMode(sizeMode As RibbonElementSizeMode)
			If sizeMode = RibbonElementSizeMode.Overflow Then
				MyBase.SetSizeMode(RibbonElementSizeMode.Large)
			Else
				MyBase.SetSizeMode(sizeMode)
			End If
		End Sub
		Friend Overrides Sub SetSelected(selected As Boolean)
			MyBase.SetSelected(selected)
			SetPressed(False)
		End Sub
		Public Overrides Sub OnMouseDown(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If (DropDownSelected OrElse Style = RibbonButtonStyle.DropDown) AndAlso DropDownItems.Count > 0 Then
				_dropDownPressed = True
				ShowDropDown()
			End If
			MyBase.OnMouseDown(e)
		End Sub
		Public Overrides Sub OnMouseUp(e As MouseEventArgs)
			MyBase.OnMouseUp(e)
		End Sub
		Public Overrides Sub OnMouseMove(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If Style = RibbonButtonStyle.SplitDropDown Then
				Dim lastState As Boolean = _dropDownSelected
				If DropDownBounds.Contains(e.X, e.Y) Then
					_dropDownSelected = True
				Else
					_dropDownSelected = False
				End If
				If lastState <> _dropDownSelected Then
					RedrawItem()
				End If
				lastState = _dropDownSelected
			End If
			_lastMousePos = New Point(e.X, e.Y)
			MyBase.OnMouseMove(e)
		End Sub
		Public Overrides Sub OnMouseLeave(e As MouseEventArgs)
			MyBase.OnMouseLeave(e)
			_dropDownSelected = False
		End Sub
		Public Overrides Sub OnClick(e As EventArgs)
			If Style <> RibbonButtonStyle.Normal AndAlso Not ButtonFaceBounds.Contains(_lastMousePos) Then
				Return
			End If
			If CheckOnClick Then
				Checked = Not Checked
			End If
			MyBase.OnClick(e)
		End Sub
		Public Function GetItems() As IEnumerable(Of RibbonItem)
			Return DropDownItems
		End Function
		Public Function GetAllChildComponents() As IEnumerable(Of Component)
			Return DropDownItems.ToArray()
		End Function
	End Class
End Namespace
