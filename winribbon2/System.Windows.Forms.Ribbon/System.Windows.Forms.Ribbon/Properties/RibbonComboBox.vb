Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Drawing
Namespace System.Windows.Forms
	<Designer(GetType(RibbonComboBoxDesigner))> _
	Public Class RibbonComboBox
		Inherits RibbonTextBox
		Implements IContainsRibbonComponents
		Implements IDropDownRibbonItem
		Private _dropDownItems As RibbonItemCollection
		Private _dropDownBounds As Rectangle
		Private _dropDownSelected As Boolean
		Private _dropDownPressed As Boolean
		Private _dropDownVisible As Boolean
		Private _allowTextEdit As Boolean
		Private _dropDownResizable As Boolean
		Public Event DropDownShowing As EventHandler
		Public Sub New()
			_dropDownItems = New RibbonItemCollection()
			_dropDownVisible = True
			_allowTextEdit = True
		End Sub
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
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Overrides ReadOnly Property TextBoxTextBounds() As Rectangle
			Get
				Dim r As Rectangle = MyBase.TextBoxTextBounds
				r.Width -= DropDownButtonBounds.Width
				Return r
			End Get
		End Property
		<Description("Allows user to change the text on the ComboBox")> _
		<DefaultValue(True)> _
		Public Property AllowTextEdit() As Boolean
			Get
				Return _allowTextEdit
			End Get
			Set
				_allowTextEdit = value
			End Set
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property DropDownItems() As RibbonItemCollection
			Get
				Return _dropDownItems
			End Get
		End Property
		Public Sub OnDropDownShowing(e As EventArgs)
			If DropDownShowing IsNot Nothing Then
				DropDownShowing(Me, e)
			End If
		End Sub
		Public Sub ShowDropDown()
			OnDropDownShowing(EventArgs.Empty)
			AssignHandlers()
			Dim dd As New RibbonDropDown(Me, DropDownItems, Owner)
			dd.ShowSizingGrip = DropDownResizable
			dd.Closed += New EventHandler(DropDown_Closed)
			dd.Show(Owner.PointToScreen(New Point(TextBoxBounds.Left, Bounds.Bottom)))
		End Sub
		Private Sub DropDown_Closed(sender As Object, e As EventArgs)
			RemoveHandlers()
		End Sub
		Private Sub AssignHandlers()
			For Each item As RibbonItem In DropDownItems
				item.Click += New EventHandler(item_Click)
			Next
		End Sub
		Sub item_Click(sender As Object, e As EventArgs)
			TextBoxText = (TryCast(sender, RibbonItem)).Text
		End Sub
		Private Sub RemoveHandlers()
			For Each item As RibbonItem In DropDownItems
				item.Click -= item_Click
			Next
		End Sub
		Protected Overrides Function ClosesDropDownAt(p As Point) As Boolean
			Return False
		End Function
		Protected Overrides Sub InitTextBox(t As TextBox)
			MyBase.InitTextBox(t)
			t.Width -= DropDownButtonBounds.Width
		End Sub
		Public Overrides Sub SetBounds(bounds As Rectangle)
			MyBase.SetBounds(bounds)
			_dropDownBounds = Rectangle.FromLTRB(bounds.Right - 15, bounds.Top, bounds.Right + 1, bounds.Bottom + 1)
		End Sub
		Public Overrides Sub OnMouseMove(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			MyBase.OnMouseMove(e)
			Dim mustRedraw As Boolean = False
			If DropDownButtonBounds.Contains(e.X, e.Y) Then
				Owner.Cursor = Cursors.[Default]
				mustRedraw = Not _dropDownSelected
				_dropDownSelected = True
			ElseIf TextBoxBounds.Contains(e.X, e.Y) Then
				Owner.Cursor = Cursors.IBeam
				mustRedraw = _dropDownSelected
				_dropDownSelected = False
			Else
				Owner.Cursor = Cursors.[Default]
			End If
			If mustRedraw Then
				RedrawItem()
			End If
		End Sub
		Public Overrides Sub OnMouseDown(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			If DropDownButtonBounds.Contains(e.X, e.Y) Then
				_dropDownPressed = True
				ShowDropDown()
			ElseIf TextBoxBounds.Contains(e.X, e.Y) AndAlso AllowTextEdit Then
				StartEdit()
			End If
		End Sub
		Public Overrides Sub OnMouseUp(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			MyBase.OnMouseUp(e)
			_dropDownPressed = False
		End Sub
		Public Overrides Sub OnMouseLeave(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			MyBase.OnMouseLeave(e)
			_dropDownSelected = False
		End Sub
		Public Function GetAllChildComponents() As IEnumerable(Of Component)
			Return DropDownItems.ToArray()
		End Function
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property DropDownButtonBounds() As Rectangle
			Get
				Return _dropDownBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property DropDownButtonVisible() As Boolean
			Get
				Return _dropDownVisible
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property DropDownButtonSelected() As Boolean
			Get
				Return _dropDownSelected
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property DropDownButtonPressed() As Boolean
			Get
				Return _dropDownPressed
			End Get
		End Property
		Friend Overrides Sub SetOwner(owner As Ribbon)
			MyBase.SetOwner(owner)
			_dropDownItems.SetOwner(owner)
		End Sub
		Friend Overrides Sub SetOwnerPanel(ownerPanel As RibbonPanel)
			MyBase.SetOwnerPanel(ownerPanel)
			_dropDownItems.SetOwnerPanel(ownerPanel)
		End Sub
		Friend Overrides Sub SetOwnerTab(ownerTab As RibbonTab)
			MyBase.SetOwnerTab(ownerTab)
			_dropDownItems.SetOwnerTab(OwnerTab)
		End Sub
	End Class
End Namespace
