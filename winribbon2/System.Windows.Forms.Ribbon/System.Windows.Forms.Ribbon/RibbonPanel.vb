Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Design
Imports System.ComponentModel.Design
Namespace System.Windows.Forms
	<DesignTimeVisible(False)> _
	<Designer(GetType(RibbonPanelDesigner))> _
	Public Class RibbonPanel
		Inherits Component
		Implements IRibbonElement
		Implements IContainsSelectableRibbonItems
		Implements IContainsRibbonComponents
		Private _enabled As Boolean
		Private _image As System.Drawing.Image
		Private _items As RibbonItemCollection
		Private _text As String
		Private _owner As Ribbon
		Private _bounds As Rectangle
		Private _contentBounds As Rectangle
		Private _selected As Boolean
		Private _tag As Object
		Private _ownerTab As RibbonTab
		Private _sizeMode As RibbonElementSizeMode
		Private _flowsTo As RibbonPanelFlowDirection
		Private _popUp As Control
		Private _pressed As Boolean
		Private _buttonMoreBounds As Rectangle
		Private _buttonMorePressed As Boolean
		Private _butonMoreSelected As Boolean
		Private _buttonMoreVisible As Boolean
		Private _buttonMoreEnabled As Boolean
		Friend overflowBoundsBuffer As Rectangle
		Private _popupShowed As Boolean
		Public Event MouseEnter As MouseEventHandler
		Public Event MouseLeave As MouseEventHandler
		Public Event MouseMove As MouseEventHandler
		Public Event Paint As PaintEventHandler
		Public Event Resize As EventHandler
		Public Event ButtonMoreClick As EventHandler
		Public Overridable Event Click As EventHandler
		Public Overridable Event DoubleClick As EventHandler
		Public Overridable Event MouseDown As System.Windows.Forms.MouseEventHandler
		Public Overridable Event MouseUp As System.Windows.Forms.MouseEventHandler
		Public Sub New()
			_items = New RibbonItemCollection()
			_sizeMode = RibbonElementSizeMode.None
			_flowsTo = RibbonPanelFlowDirection.Bottom
			_buttonMoreEnabled = True
			_buttonMoreVisible = True
			_items.SetOwnerPanel(Me)
			_enabled = True
		End Sub
		Public Sub New(text As String)
			Me.New(text, RibbonPanelFlowDirection.Bottom)
		End Sub
		Public Sub New(text As String, flowsTo As RibbonPanelFlowDirection)
			Me.New(text, flowsTo, New RibbonItem() {})
		End Sub
		Public Sub New(text As String, flowsTo As RibbonPanelFlowDirection, items As IEnumerable(Of RibbonItem))
			Me.New()
			_text = text
			_flowsTo = flowsTo
			_items.AddRange(items)
		End Sub
		<Description("Sets if the panel should be enabled")> _
		<DefaultValue(True)> _
		Public Property Enabled() As Boolean
			Get
				Return _enabled
			End Get
			Set
				_enabled = value
				For Each item As RibbonItem In Items
					item.Enabled = value
				Next
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Collapsed() As Boolean
			Get
				Return SizeMode = RibbonElementSizeMode.Overflow
			End Get
		End Property
		<Description("Sets the visibility of the ""More..."" button")> _
		<DefaultValue(True)> _
		Public Property ButtonMoreVisible() As Boolean
			Get
				Return _buttonMoreVisible
			End Get
			Set
				_buttonMoreVisible = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<Description("Enables/Disables the ""More..."" button")> _
		<DefaultValue(True)> _
		Public Property ButtonMoreEnabled() As Boolean
			Get
				Return _buttonMoreEnabled
			End Get
			Set
				_buttonMoreEnabled = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonMoreSelected() As Boolean
			Get
				Return _butonMoreSelected
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonMorePressed() As Boolean
			Get
				Return _buttonMorePressed
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ButtonMoreBounds() As Rectangle
			Get
				Return _buttonMoreBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Pressed() As Boolean
			Get
				Return _pressed
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Friend Property PopUp() As Control
			Get
				Return _popUp
			End Get
			Set
				_popUp = value
			End Set
		End Property
		Public ReadOnly Property SizeMode() As RibbonElementSizeMode
			Get
				Return _sizeMode
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property Items() As RibbonItemCollection
			Get
				Return _items
			End Get
		End Property
		<Localizable(True)> _
		Public Property Text() As String
			Get
				Return _text
			End Get
			Set
				_text = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<DefaultValue(Nothing)> _
		Public Property Image() As System.Drawing.Image
			Get
				Return _image
			End Get
			Set
				_image = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OverflowMode() As Boolean
			Get
				Return SizeMode = RibbonElementSizeMode.Overflow
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
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable Property Selected() As Boolean
			Get
				Return _selected
			End Get
			Set
				_selected = value
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
		Public ReadOnly Property ContentBounds() As Rectangle
			Get
				Return _contentBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OwnerTab() As RibbonTab
			Get
				Return _ownerTab
			End Get
		End Property
		<DefaultValue(RibbonPanelFlowDirection.Bottom)> _
		Public Property FlowsTo() As RibbonPanelFlowDirection
			Get
				Return _flowsTo
			End Get
			Set
				_flowsTo = value
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Friend Property PopupShowed() As Boolean
			Get
				Return _popupShowed
			End Get
			Set
				_popupShowed = value
			End Set
		End Property
		Public Function SwitchToSize(ctl As Control, g As Graphics, size As RibbonElementSizeMode) As Size
			Dim s As Size = MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(g, size))
			Dim r As New Rectangle(0, 0, s.Width, s.Height)
			SetBounds(r)
			UpdateItemsRegions(g, size)
			Return s
		End Function
		Public Overridable Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
			If Paint IsNot Nothing Then
				Paint(Me, New PaintEventArgs(e.Graphics, e.Clip))
			End If
			If PopupShowed AndAlso e.Control = Owner Then
				Dim fakePanel As New RibbonPanel(Me.Text)
				fakePanel.Image = Me.Image
				fakePanel.SetSizeMode(RibbonElementSizeMode.Overflow)
				fakePanel.SetBounds(overflowBoundsBuffer)
				fakePanel.SetPressed(True)
				fakePanel.SetOwner(Owner)
				Owner.Renderer.OnRenderRibbonPanelBackground(New RibbonPanelRenderEventArgs(Owner, e.Graphics, e.Clip, fakePanel, e.Control))
				Owner.Renderer.OnRenderRibbonPanelText(New RibbonPanelRenderEventArgs(Owner, e.Graphics, e.Clip, fakePanel, e.Control))
			Else
				Owner.Renderer.OnRenderRibbonPanelBackground(New RibbonPanelRenderEventArgs(Owner, e.Graphics, e.Clip, Me, e.Control))
				Owner.Renderer.OnRenderRibbonPanelText(New RibbonPanelRenderEventArgs(Owner, e.Graphics, e.Clip, Me, e.Control))
			End If
			If e.Mode <> RibbonElementSizeMode.Overflow OrElse (e.Control IsNot Nothing AndAlso e.Control = PopUp) Then
				For Each item As RibbonItem In Items
					item.OnPaint(Me, New RibbonElementPaintEventArgs(item.Bounds, e.Graphics, item.SizeMode))
				Next
			End If
		End Sub
		Public Sub SetBounds(bounds As System.Drawing.Rectangle)
			Dim trigger As Boolean = _bounds <> bounds
			_bounds = bounds
			OnResize(EventArgs.Empty)
			If Owner IsNot Nothing Then
				_contentBounds = Rectangle.FromLTRB(bounds.X + Owner.PanelMargin.Left + 0, bounds.Y + Owner.PanelMargin.Top + 0, bounds.Right - Owner.PanelMargin.Right, bounds.Bottom - Owner.PanelMargin.Bottom)
			End If
			If ButtonMoreVisible Then
				SetMoreBounds(Rectangle.FromLTRB(bounds.Right - 15, _contentBounds.Bottom + 1, bounds.Right, bounds.Bottom))
			Else
				SetMoreBounds(Rectangle.Empty)
			End If
		End Sub
		Public Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim result As Size = Size.Empty
			Dim minSize As Size = Size.Empty
			Dim panelHeight As Integer = OwnerTab.TabContentBounds.Height - Owner.PanelPadding.Vertical
			minSize.Width = e.Graphics.MeasureString(Text, Owner.Font).ToSize().Width + Owner.PanelMargin.Horizontal + 1
			If ButtonMoreVisible Then
				minSize.Width += ButtonMoreBounds.Width + 3
			End If
			If e.SizeMode = RibbonElementSizeMode.Overflow Then
				Dim textSize As Size = RibbonButton.MeasureStringLargeSize(e.Graphics, Text, Owner.Font)
				Return New Size(textSize.Width + Owner.PanelMargin.Horizontal, panelHeight)
			End If
			Select Case FlowsTo
				Case RibbonPanelFlowDirection.Right
					result = MeasureSizeFlowsToRight(sender, e)
					Exit Select
				Case RibbonPanelFlowDirection.Bottom
					result = MeasureSizeFlowsToBottom(sender, e)
					Exit Select
				Case Else
					result = Size.Empty
					Exit Select
			End Select
			Return New Size(Math.Max(result.Width, minSize.Width), panelHeight)
		End Function
		Friend Sub SetOwner(owner As Ribbon)
			_owner = owner
			Items.SetOwner(owner)
		End Sub
		Friend Sub SetSelected(selected As Boolean)
			_selected = selected
		End Sub
		Protected Overridable Sub OnResize(e As EventArgs)
			If Resize IsNot Nothing Then
				Resize(Me, e)
			End If
		End Sub
		Private Sub ShowOverflowPopup()
			Dim b As Rectangle = Bounds
			Dim f As New RibbonPanelPopup(Me)
			Dim p As Point = Owner.PointToScreen(New Point(b.Left, b.Bottom))
			PopupShowed = True
			f.Show(p)
		End Sub
		Private Function MeasureSizeFlowsToRight(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim widthSum As Integer = Owner.PanelMargin.Horizontal
			Dim maxWidth As Integer = 0
			Dim maxHeight As Integer = 0
			Dim dividedWidth As Integer = 0
			For Each item As RibbonItem In Items
				Dim itemSize As Size = item.MeasureSize(Me, e)
				widthSum += itemSize.Width + Owner.ItemPadding.Horizontal + 1
				maxWidth = Math.Max(maxWidth, itemSize.Width)
				maxHeight = Math.Max(maxHeight, itemSize.Height)
			Next
			Select Case e.SizeMode
				Case RibbonElementSizeMode.Large
					dividedWidth = widthSum / 1
					Exit Select
				Case RibbonElementSizeMode.Medium
					dividedWidth = widthSum / 2
					Exit Select
				Case RibbonElementSizeMode.Compact
					dividedWidth = widthSum / 3
					Exit Select
				Case Else
					Exit Select
			End Select
			dividedWidth += Owner.PanelMargin.Horizontal
			Return New Size(Math.Max(maxWidth, dividedWidth) + Owner.PanelMargin.Horizontal, 0)
		End Function
		Private Function MeasureSizeFlowsToBottom(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim curRight As Integer = Owner.PanelMargin.Left + Owner.ItemPadding.Horizontal
			Dim curBottom As Integer = ContentBounds.Top + Owner.ItemPadding.Vertical
			Dim lastRight As Integer = 0
			Dim lastBottom As Integer = 0
			Dim availableHeight As Integer = OwnerTab.TabContentBounds.Height - Owner.TabContentMargin.Vertical - Owner.PanelPadding.Vertical - Owner.PanelMargin.Vertical
			Dim maxRight As Integer = 0
			Dim maxBottom As Integer = 0
			For Each item As RibbonItem In Items
				Dim itemSize As Size = item.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(e.Graphics, e.SizeMode))
				If curBottom + itemSize.Height > ContentBounds.Bottom Then
					curBottom = ContentBounds.Top + Owner.ItemPadding.Vertical + 0
					curRight = maxRight + Owner.ItemPadding.Horizontal + 0
				End If
				Dim bounds As New Rectangle(curRight, curBottom, itemSize.Width, itemSize.Height)
				lastRight = bounds.Right
				lastBottom = bounds.Bottom
				curBottom = bounds.Bottom + Owner.ItemPadding.Vertical + 1
				maxRight = Math.Max(maxRight, lastRight)
				maxBottom = Math.Max(maxBottom, lastBottom)
			Next
			Return New Size(maxRight + Owner.ItemPadding.Right + Owner.PanelMargin.Right + 1, 0)
		End Function
		Friend Sub SetSizeMode(sizeMode As RibbonElementSizeMode)
			_sizeMode = sizeMode
			For Each item As RibbonItem In Items
				item.SetSizeMode(sizeMode)
			Next
		End Sub
		Friend Sub SetContentBounds(contentBounds As Rectangle)
			_contentBounds = contentBounds
		End Sub
		Friend Sub SetOwnerTab(ownerTab As RibbonTab)
			_ownerTab = ownerTab
			Items.SetOwnerTab(OwnerTab)
		End Sub
		Friend Sub UpdateItemsRegions(g As Graphics, mode As RibbonElementSizeMode)
			Select Case FlowsTo
				Case RibbonPanelFlowDirection.Right
					UpdateRegionsFlowsToRight(g, mode)
					Exit Select
				Case RibbonPanelFlowDirection.Bottom
					UpdateRegionsFlowsToBottom(g, mode)
					Exit Select
			End Select
			CenterItems()
		End Sub
		Private Sub UpdateRegionsFlowsToBottom(g As Graphics, mode As RibbonElementSizeMode)
			Dim curRight As Integer = ContentBounds.Left + Owner.ItemPadding.Horizontal + 0
			Dim curBottom As Integer = ContentBounds.Top + Owner.ItemPadding.Vertical + 0
			Dim lastRight As Integer = curRight
			Dim lastBottom As Integer = 0
			Dim lastColumn As New List(Of RibbonItem)()
			For Each item As RibbonItem In Items
				Dim itemSize As Size = item.LastMeasuredSize
				If curBottom + itemSize.Height > ContentBounds.Bottom Then
					curBottom = ContentBounds.Top + Owner.ItemPadding.Vertical + 0
					curRight = lastRight + Owner.ItemPadding.Horizontal + 0
					Items.CenterItemsVerticallyInto(lastColumn, ContentBounds)
					lastColumn.Clear()
				End If
				item.SetBounds(New Rectangle(curRight, curBottom, itemSize.Width, itemSize.Height))
				lastRight = Math.Max(item.Bounds.Right, lastRight)
				lastBottom = item.Bounds.Bottom
				curBottom = item.Bounds.Bottom + Owner.ItemPadding.Vertical + 1
				lastColumn.Add(item)
			Next
			Items.CenterItemsVerticallyInto(lastColumn, Items.GetItemsBounds())
		End Sub
		Private Sub UpdateRegionsFlowsToRight(g As Graphics, mode As RibbonElementSizeMode)
			Dim curLeft As Integer = ContentBounds.Left
			Dim curTop As Integer = ContentBounds.Top
			Dim padding As Integer = If(mode = RibbonElementSizeMode.Medium, 7, 0)
			Dim maxBottom As Integer = 0
			Dim array As RibbonItem() = Items.ToArray()
			Dim i As Integer = (array.Length - 1)
			While i >= 0
				Dim j As Integer = 1
				While j <= i
					If array(j - 1).LastMeasuredSize.Width < array(j).LastMeasuredSize.Width Then
						Dim temp As RibbonItem = array(j - 1)
						array(j - 1) = array(j)
						array(j) = temp
					End If
					System.Math.Max(System.Threading.Interlocked.Increment(j),j - 1)
				End While
				System.Math.Max(System.Threading.Interlocked.Decrement(i),i + 1)
			End While
			Dim list As New List(Of RibbonItem)(array)
			While list.Count > 0
				Dim item As RibbonItem = list(0)
				list.Remove(item)
				If curLeft + item.LastMeasuredSize.Width > ContentBounds.Right Then
					curLeft = ContentBounds.Left
					curTop = maxBottom + Owner.ItemPadding.Vertical + 1 + padding
				End If
				item.SetBounds(New Rectangle(New Point(curLeft, curTop), item.LastMeasuredSize))
				curLeft += item.Bounds.Width + Owner.ItemPadding.Horizontal
				maxBottom = Math.Max(maxBottom, item.Bounds.Bottom)
				Dim spaceAvailable As Integer = ContentBounds.Right - curLeft
				Dim i As Integer = 0
				While i < list.Count
					If list(i).LastMeasuredSize.Width < spaceAvailable Then
						list(i).SetBounds(New Rectangle(New Point(curLeft, curTop), list(i).LastMeasuredSize))
						curLeft += list(i).Bounds.Width + Owner.ItemPadding.Horizontal
						maxBottom = Math.Max(maxBottom, list(i).Bounds.Bottom)
						spaceAvailable = ContentBounds.Right - curLeft
						list.RemoveAt(i)
						i = 0
					End If
					System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
				End While
			End While
		End Sub
		Private Sub CenterItems()
			Items.CenterItemsInto(ContentBounds)
		End Sub
		Public Overrides Function ToString() As String
			Return String.Format("Panel: {0} ({1})", Text, SizeMode)
		End Function
		Public Sub SetPressed(pressed As Boolean)
			_pressed = pressed
		End Sub
		Friend Sub SetMorePressed(pressed As Boolean)
			_buttonMorePressed = pressed
		End Sub
		Friend Sub SetMoreSelected(selected As Boolean)
			_butonMoreSelected = selected
		End Sub
		Friend Sub SetMoreBounds(bounds As Rectangle)
			_buttonMoreBounds = bounds
		End Sub
		Protected Sub OnButtonMoreClick(e As EventArgs)
			If ButtonMoreClick IsNot Nothing Then
				ButtonMoreClick(Me, e)
			End If
		End Sub
		Public Function GetItems() As IEnumerable(Of RibbonItem)
			Return Items
		End Function
		Public Function GetContentBounds() As Rectangle
			Return ContentBounds
		End Function
		Public Overridable Sub OnMouseEnter(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseEnter IsNot Nothing Then
				MouseEnter(Me, e)
			End If
		End Sub
		Public Overridable Sub OnMouseLeave(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseLeave IsNot Nothing Then
				MouseLeave(Me, e)
			End If
		End Sub
		Public Overridable Sub OnMouseMove(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseMove IsNot Nothing Then
				MouseMove(Me, e)
			End If
			Dim redraw As Boolean = False
			If ButtonMoreEnabled AndAlso ButtonMoreVisible AndAlso ButtonMoreBounds.Contains(e.X, e.Y) AndAlso Not Collapsed Then
				SetMoreSelected(True)
				redraw = True
			Else
				redraw = ButtonMoreSelected
				SetMoreSelected(False)
			End If
			If redraw Then
				Owner.Invalidate(Bounds)
			End If
		End Sub
		Public Overridable Sub OnClick(e As EventArgs)
			If Not Enabled Then
				Return
			End If
			If Click IsNot Nothing Then
				Click(Me, e)
			End If
			If Collapsed AndAlso PopUp Is Nothing Then
				ShowOverflowPopup()
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
		Public Overridable Sub OnMouseDown(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseDown IsNot Nothing Then
				MouseDown(Me, e)
			End If
			SetPressed(True)
			Dim redraw As Boolean = False
			If ButtonMoreEnabled AndAlso ButtonMoreVisible AndAlso ButtonMoreBounds.Contains(e.X, e.Y) AndAlso Not Collapsed Then
				SetMorePressed(True)
				redraw = True
			Else
				redraw = ButtonMoreSelected
				SetMorePressed(False)
			End If
			If redraw Then
				Owner.Invalidate(Bounds)
			End If
		End Sub
		Public Overridable Sub OnMouseUp(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If MouseUp IsNot Nothing Then
				MouseUp(Me, e)
			End If
			If ButtonMoreEnabled AndAlso ButtonMoreVisible AndAlso ButtonMorePressed AndAlso Not Collapsed Then
				OnButtonMoreClick(EventArgs.Empty)
			End If
			SetPressed(False)
			SetMorePressed(False)
		End Sub
		Public Function GetAllChildComponents() As IEnumerable(Of Component)
			Return Items.ToArray()
		End Function
	End Class
End Namespace
