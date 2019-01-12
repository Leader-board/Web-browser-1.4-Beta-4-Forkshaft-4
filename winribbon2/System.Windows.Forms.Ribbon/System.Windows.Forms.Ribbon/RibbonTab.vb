Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Drawing
Namespace System.Windows.Forms
	<DesignTimeVisible(False)> _
	<Designer(GetType(RibbonTabDesigner))> _
	Public Class RibbonTab
		Inherits Component
		Implements IRibbonElement
		Implements IContainsRibbonComponents
		Private _panels As RibbonPanelCollection
		Private _tabBounds As Rectangle
		Private _tabContentBounds As Rectangle
		Private _owner As Ribbon
		Private _pressed As Boolean
		Private _selected As Boolean
		Private _active As Boolean
		Private _tag As Object
		Private _text As String
		Private _context As RibbonContext
		Private _scrollLeftVisible As Boolean
		Private _scrollLeftBounds As Rectangle
		Private _scrollLeftSelected As Boolean
		Private _scrollLeftPressed As Boolean
		Private _scrollRightBounds As Rectangle
		Private _scrollRightSelected As Boolean
		Private _scrollRightVisible As Boolean
		Private _scrollRightPressed As Boolean
		Private _offset As Integer
		Public Event MouseEnter As MouseEventHandler
		Public Event MouseLeave As MouseEventHandler
		Public Event MouseMove As MouseEventHandler
		Public Sub New()
			_panels = New RibbonPanelCollection(Me)
		End Sub
		Public Sub New(owner As Ribbon, text As String)
			_panels = New RibbonPanelCollection(owner, Me)
			_text = text
		End Sub
		Public Event ScrollRightVisibleChanged As EventHandler
		Public Event ScrollRightPressedChanged As EventHandler
		Public Event ScrollRightBoundsChanged As EventHandler
		Public Event ScrollRightSelectedChanged As EventHandler
		Public Event ScrollLeftVisibleChanged As EventHandler
		Public Event ScrollLeftPressedChanged As EventHandler
		Public Event ScrollLeftSelectedChanged As EventHandler
		Public Event ScrollLeftBoundsChanged As EventHandler
		Public Event TabBoundsChanged As EventHandler
		Public Event TabContentBoundsChanged As EventHandler
		Public Event OwnerChanged As EventHandler
		Public Event PressedChanged As EventHandler
		Public Event ActiveChanged As EventHandler
		Public Event TextChanged As EventHandler
		Public Event ContextChanged As EventHandler
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property ScrollRightVisible() As Boolean
			Get
				Return _scrollRightVisible
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property ScrollRightSelected() As Boolean
			Get
				Return _scrollRightSelected
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property ScrollRightPressed() As Boolean
			Get
				Return _scrollRightPressed
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property ScrollRightBounds() As Rectangle
			Get
				Return _scrollRightBounds
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property ScrollLeftVisible() As Boolean
			Get
				Return _scrollLeftVisible
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property ScrollLeftBounds() As Rectangle
			Get
				Return _scrollLeftBounds
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property ScrollLeftSelected() As Boolean
			Get
				Return _scrollLeftSelected
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property ScrollLeftPressed() As Boolean
			Get
				Return _scrollLeftPressed
			End Get
		End Property
		Public ReadOnly Property Bounds() As Rectangle
			Get
				Return TabBounds
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property Panels() As RibbonPanelCollection
			Get
				Return _panels
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property TabBounds() As System.Drawing.Rectangle
			Get
				Return _tabBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property TabContentBounds() As System.Drawing.Rectangle
			Get
				Return _tabContentBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Owner() As Ribbon
			Get
				Return _owner
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property Pressed() As Boolean
			Get
				Return _pressed
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property Selected() As Boolean
			Get
				Return _selected
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property Active() As Boolean
			Get
				Return _active
			End Get
		End Property
		Public Property Tag() As Object
			Get
				Return _tag
			End Get
			Set
				_tag = value
			End Set
		End Property
		<Localizable(True)> _
		Public Property Text() As String
			Get
				Return _text
			End Get
			Set
				_text = value
				OnTextChanged(EventArgs.Empty)
				If Owner IsNot Nothing Then
					Owner.OnRegionsChanged()
				End If
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property Contextual() As Boolean
			Get
				Return _context <> Nothing
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Context() As RibbonContext
			Get
				Return _context
			End Get
		End Property
		Public Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
			If Owner Is Nothing Then
				Return
			End If
			Owner.Renderer.OnRenderRibbonTab(New RibbonTabRenderEventArgs(Owner, e.Graphics, e.Clip, Me))
			Owner.Renderer.OnRenderRibbonTabText(New RibbonTabRenderEventArgs(Owner, e.Graphics, e.Clip, Me))
			If Active Then
				For Each panel As RibbonPanel In Panels
					panel.OnPaint(Me, New RibbonElementPaintEventArgs(e.Clip, e.Graphics, panel.SizeMode, e.Control))
				Next
			End If
			Owner.Renderer.OnRenderTabScrollButtons(New RibbonTabRenderEventArgs(Owner, e.Graphics, e.Clip, Me))
		End Sub
		Public Sub SetBounds(bounds As Rectangle)
			Throw New NotSupportedException()
		End Sub
		Public Sub SetContext(context As RibbonContext)
			Dim trigger As Boolean = Not context.Equals(context)
			If trigger Then
				OnContextChanged(EventArgs.Empty)
			End If
			_context = context
			Throw New NotImplementedException()
		End Sub
		Public Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim textSize As Size = TextRenderer.MeasureText(Text, Owner.Font)
			Return textSize
		End Function
		Friend Sub SetOwner(owner As Ribbon)
			_owner = owner
			Panels.SetOwner(owner)
			OnOwnerChanged(EventArgs.Empty)
		End Sub
		Friend Sub SetPressed(pressed As Boolean)
			_pressed = pressed
			OnPressedChanged(EventArgs.Empty)
		End Sub
		Friend Sub SetSelected(selected As Boolean)
			_selected = selected
			If selected Then
				OnMouseEnter(New MouseEventArgs(MouseButtons.None, 0, 0, 0, 0))
			Else
				OnMouseLeave(New MouseEventArgs(MouseButtons.None, 0, 0, 0, 0))
			End If
		End Sub
		Public Sub OnContextChanged(e As EventArgs)
			If ContextChanged IsNot Nothing Then
				ContextChanged(Me, e)
			End If
		End Sub
		Public Sub OnTextChanged(e As EventArgs)
			If TextChanged IsNot Nothing Then
				TextChanged(Me, e)
			End If
		End Sub
		Public Sub OnActiveChanged(e As EventArgs)
			If ActiveChanged IsNot Nothing Then
				ActiveChanged(Me, e)
			End If
		End Sub
		Public Sub OnPressedChanged(e As EventArgs)
			If PressedChanged IsNot Nothing Then
				PressedChanged(Me, e)
			End If
		End Sub
		Public Sub OnOwnerChanged(e As EventArgs)
			If OwnerChanged IsNot Nothing Then
				OwnerChanged(Me, e)
			End If
		End Sub
		Public Sub OnTabContentBoundsChanged(e As EventArgs)
			If TabContentBoundsChanged IsNot Nothing Then
				TabContentBoundsChanged(Me, e)
			End If
		End Sub
		Public Sub OnTabBoundsChanged(e As EventArgs)
			If TabBoundsChanged IsNot Nothing Then
				TabBoundsChanged(Me, e)
			End If
		End Sub
		Public Sub OnScrollRightVisibleChanged(e As EventArgs)
			If ScrollRightVisibleChanged IsNot Nothing Then
				ScrollRightVisibleChanged(Me, e)
			End If
		End Sub
		Public Sub OnScrollRightPressedChanged(e As EventArgs)
			If ScrollRightPressedChanged IsNot Nothing Then
				ScrollRightPressedChanged(Me, e)
			End If
		End Sub
		Public Sub OnScrollRightBoundsChanged(e As EventArgs)
			If ScrollRightBoundsChanged IsNot Nothing Then
				ScrollRightBoundsChanged(Me, e)
			End If
		End Sub
		Public Sub OnScrollRightSelectedChanged(e As EventArgs)
			If ScrollRightSelectedChanged IsNot Nothing Then
				ScrollRightSelectedChanged(Me, e)
			End If
		End Sub
		Public Sub OnScrollLeftVisibleChanged(e As EventArgs)
			If ScrollLeftVisibleChanged IsNot Nothing Then
				ScrollLeftVisibleChanged(Me, e)
			End If
		End Sub
		Public Sub OnScrollLeftPressedChanged(e As EventArgs)
			If ScrollLeftPressedChanged IsNot Nothing Then
				ScrollLeftPressedChanged(Me, e)
			End If
		End Sub
		Public Sub OnScrollLeftBoundsChanged(e As EventArgs)
			If ScrollLeftBoundsChanged IsNot Nothing Then
				ScrollLeftBoundsChanged(Me, e)
			End If
		End Sub
		Public Sub OnScrollLeftSelectedChanged(e As EventArgs)
			If ScrollLeftSelectedChanged IsNot Nothing Then
				ScrollLeftSelectedChanged(Me, e)
			End If
		End Sub
		Friend Sub SetActive(active As Boolean)
			Dim trigger As Boolean = _active <> active
			_active = active
			If trigger Then
				OnActiveChanged(EventArgs.Empty)
			End If
		End Sub
		Friend Sub SetTabBounds(tabBounds As Rectangle)
			Dim tigger As Boolean = _tabBounds <> tabBounds
			_tabBounds = tabBounds
			OnTabBoundsChanged(EventArgs.Empty)
		End Sub
		Friend Sub SetTabContentBounds(tabContentBounds As Rectangle)
			Dim trigger As Boolean = _tabContentBounds <> tabContentBounds
			_tabContentBounds = tabContentBounds
			OnTabContentBoundsChanged(EventArgs.Empty)
		End Sub
		Private Function GetLargerPanel(size As RibbonElementSizeMode) As RibbonPanel
			Dim result As RibbonPanel = Nothing
			For Each panel As RibbonPanel In Panels
				If panel.SizeMode <> size Then
					Continue For
				End If
				If result Is Nothing Then
					result = panel
				End If
				If panel.Bounds.Width > result.Bounds.Width Then
					result = panel
				End If
			Next
			Return result
		End Function
		Private Function GetLargerPanel() As RibbonPanel
			Dim largeLarger As RibbonPanel = GetLargerPanel(RibbonElementSizeMode.Large)
			If largeLarger IsNot Nothing Then
				Return largeLarger
			End If
			Dim mediumLarger As RibbonPanel = GetLargerPanel(RibbonElementSizeMode.Medium)
			If mediumLarger IsNot Nothing Then
				Return mediumLarger
			End If
			Dim compactLarger As RibbonPanel = GetLargerPanel(RibbonElementSizeMode.Compact)
			If compactLarger IsNot Nothing Then
				Return compactLarger
			End If
			Dim overflowLarger As RibbonPanel = GetLargerPanel(RibbonElementSizeMode.Overflow)
			If overflowLarger IsNot Nothing Then
				Return overflowLarger
			End If
			Return Nothing
		End Function
		Private Function AllPanelsOverflow() As Boolean
			For Each panel As RibbonPanel In Panels
				If panel.SizeMode <> RibbonElementSizeMode.Overflow Then
					Return False
				End If
			Next
			Return True
		End Function
		Friend Sub UpdatePanelsRegions()
			If Panels.Count = 0 Then
				Return
			End If
			Dim dMode As Boolean = Site IsNot Nothing AndAlso Site.DesignMode
			If Not dMode Then
				_offset = 0
			End If
			Dim curRight As Integer = TabContentBounds.Left + Owner.PanelPadding.Left + _offset
			Dim panelsTop As Integer = TabContentBounds.Top + Owner.PanelPadding.Top
			Using g As Graphics = Owner.CreateGraphics()
				For Each panel As RibbonPanel In Panels
					Dim sMode As RibbonElementSizeMode = If(panel.FlowsTo = RibbonPanelFlowDirection.Right, RibbonElementSizeMode.Medium, RibbonElementSizeMode.Large)
					panel.SetBounds(New Rectangle(0, 0, 1, TabContentBounds.Height - Owner.PanelPadding.Vertical))
					Dim size As Size = panel.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(g, sMode))
					Dim bounds As New Rectangle(curRight, panelsTop, size.Width, size.Height)
					panel.SetBounds(bounds)
					panel.SetSizeMode(sMode)
					curRight = bounds.Right + 1 + Owner.PanelSpacing
				Next
				If Not dMode Then
					While curRight > TabContentBounds.Right AndAlso Not AllPanelsOverflow()
						Dim larger As RibbonPanel = GetLargerPanel()
						If larger.SizeMode = RibbonElementSizeMode.Large Then
							larger.SetSizeMode(RibbonElementSizeMode.Medium)
						ElseIf larger.SizeMode = RibbonElementSizeMode.Medium Then
							larger.SetSizeMode(RibbonElementSizeMode.Compact)
						ElseIf larger.SizeMode = RibbonElementSizeMode.Compact Then
							larger.SetSizeMode(RibbonElementSizeMode.Overflow)
						End If
						Dim size As Size = larger.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(g, larger.SizeMode))
						larger.SetBounds(New Rectangle(larger.Bounds.Location, New Size(size.Width + Owner.PanelMargin.Horizontal, size.Height)))
						curRight = TabContentBounds.Left + Owner.PanelPadding.Left
						For Each panel As RibbonPanel In Panels
							Dim s As Size = panel.Bounds.Size
							panel.SetBounds(New Rectangle(New Point(curRight, panelsTop), s))
							curRight += panel.Bounds.Width + 1 + Owner.PanelSpacing
						Next
					End While
				End If
				For Each panel As RibbonPanel In Panels
					panel.UpdateItemsRegions(g, panel.SizeMode)
				Next
			End Using
			UpdateScrollBounds()
		End Sub
		Private Sub UpdateScrollBounds()
			Dim w As Integer = 13
			Dim scrBuffer As Boolean = _scrollRightVisible
			Dim sclBuffer As Boolean = _scrollLeftVisible
			Dim rrBuffer As Rectangle = _scrollRightBounds
			Dim rlBuffer As Rectangle = _scrollLeftBounds
			If Panels.Count = 0 Then
				Return
			End If
			If Panels(Panels.Count - 1).Bounds.Right > TabContentBounds.Right Then
				_scrollRightVisible = True
			Else
				_scrollRightVisible = False
			End If
			If _scrollRightVisible <> scrBuffer Then
				OnScrollRightVisibleChanged(EventArgs.Empty)
			End If
			If _offset < 0 Then
				_scrollLeftVisible = True
			Else
				_scrollLeftVisible = False
			End If
			If _scrollRightVisible <> scrBuffer Then
				OnScrollLeftVisibleChanged(EventArgs.Empty)
			End If
			If _scrollLeftVisible OrElse _scrollRightVisible Then
				_scrollRightBounds = Rectangle.FromLTRB(Owner.ClientRectangle.Right - w, TabContentBounds.Top, Owner.ClientRectangle.Right, TabContentBounds.Bottom)
				_scrollLeftBounds = Rectangle.FromLTRB(0, TabContentBounds.Top, w, TabContentBounds.Bottom)
				If _scrollRightBounds <> rrBuffer Then
					OnScrollRightBoundsChanged(EventArgs.Empty)
				End If
				If _scrollLeftBounds <> rlBuffer Then
					OnScrollLeftBoundsChanged(EventArgs.Empty)
				End If
			End If
		End Sub
		Public Overrides Function ToString() As String
			Return String.Format("Tab: {0}", Text)
		End Function
		Public Overridable Sub OnMouseEnter(e As MouseEventArgs)
			If MouseEnter IsNot Nothing Then
				MouseEnter(Me, e)
			End If
		End Sub
		Public Overridable Sub OnMouseLeave(e As MouseEventArgs)
			If MouseLeave IsNot Nothing Then
				MouseLeave(Me, e)
			End If
		End Sub
		Public Overridable Sub OnMouseMove(e As MouseEventArgs)
			If MouseMove IsNot Nothing Then
				MouseMove(Me, e)
			End If
		End Sub
		Friend Sub SetScrollLeftPressed(pressed As Boolean)
			_scrollLeftPressed = pressed
			If pressed Then
				ScrollLeft()
			End If
			OnScrollLeftPressedChanged(EventArgs.Empty)
		End Sub
		Friend Sub SetScrollLeftSelected(selected As Boolean)
			_scrollLeftSelected = selected
			OnScrollLeftSelectedChanged(EventArgs.Empty)
		End Sub
		Friend Sub SetScrollRightPressed(pressed As Boolean)
			_scrollRightPressed = pressed
			If pressed Then
				ScrollRight()
			End If
			OnScrollRightPressedChanged(EventArgs.Empty)
		End Sub
		Friend Sub SetScrollRightSelected(selected As Boolean)
			_scrollRightSelected = selected
			OnScrollRightSelectedChanged(EventArgs.Empty)
		End Sub
		Public Sub ScrollLeft()
			ScrollOffset(50)
		End Sub
		Public Sub ScrollRight()
			ScrollOffset(-50)
		End Sub
		Public Sub ScrollOffset(amount As Integer)
			_offset += amount
			For Each p As RibbonPanel In Panels
				p.SetBounds(New Rectangle(p.Bounds.Left + amount, p.Bounds.Top, p.Bounds.Width, p.Bounds.Height))
			Next
			If Site IsNot Nothing AndAlso Site.DesignMode Then
				UpdatePanelsRegions()
			End If
			UpdateScrollBounds()
			Owner.Invalidate()
		End Sub
		Public Function GetAllChildComponents() As IEnumerable(Of Component)
			Return Panels.ToArray()
		End Function
	End Class
End Namespace
