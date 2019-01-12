Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Namespace System.Windows.Forms
	Public Class RibbonQuickAccessToolbar
		Inherits RibbonItem
		Implements IContainsSelectableRibbonItems
		Implements IContainsRibbonComponents
		Private _items As RibbonQuickAccessToolbarItemCollection
		Private _menuButtonVisible As Boolean
		Private _margin As Padding
		Private _padding As Padding
		Private _sensor As RibbonMouseSensor
		Private _dropDownButton As RibbonButton
		Private _DropDownButtonVisible As Boolean
		Friend Sub New(ownerRibbon As Ribbon)
			If ownerRibbon Is Nothing Then
				Throw New ArgumentNullException("ownerRibbon")
			End If
			SetOwner(ownerRibbon)
			_dropDownButton = New RibbonButton()
			_dropDownButton.SetOwner(ownerRibbon)
			_dropDownButton.SmallImage = CreateDropDownButtonImage()
			_margin = New Padding(9)
			_padding = New Padding(3, 0, 0, 0)
			_items = New RibbonQuickAccessToolbarItemCollection(Me)
			_sensor = New RibbonMouseSensor(ownerRibbon, ownerRibbon, Items)
			_DropDownButtonVisible = True
		End Sub
		Private Function CreateDropDownButtonImage() As Image
			Dim bmp As New Bitmap(7, 7)
			Dim renderer As RibbonProfessionalRenderer = TryCast(Owner.Renderer, RibbonProfessionalRenderer)
			Dim dk As Color = Color.Navy
			Dim lt As Color = Color.White
			If renderer IsNot Nothing Then
				dk = renderer.ColorTable.Arrow
				lt = renderer.ColorTable.ArrowLight
			End If
			Using g As Graphics = Graphics.FromImage(bmp)
				DrawDropDownButtonArrow(g, lt, 0, 1)
				DrawDropDownButtonArrow(g, dk, 0, 0)
			End Using
			Return bmp
		End Function
		Private Sub DrawDropDownButtonArrow(g As Graphics, c As Color, x As Integer, y As Integer)
			Using p As New Pen(c)
				Using b As New SolidBrush(c)
					g.DrawLine(p, x, y, x + 4, y)
					g.FillPolygon(b, New Point() {New Point(x, y + 3), New Point(x + 5, y + 3), New Point(x + 2, y + 6)})
				End Using
			End Using
		End Sub
		<Description("Shows or hides the dropdown button of the toolbar")> _
		<DefaultValue(True)> _
		Public Property DropDownButtonVisible() As Boolean
			Get
				Return _DropDownButtonVisible
			End Get
			Set
				_DropDownButtonVisible = value
				Owner.OnRegionsChanged()
			End Set
		End Property
		<Browsable(False)> _
		Friend ReadOnly Property SuperBounds() As Rectangle
			Get
				Return Rectangle.FromLTRB(Bounds.Left - Padding.Horizontal, Bounds.Top, DropDownButton.Bounds.Right, Bounds.Bottom)
			End Get
		End Property
		<Browsable(False)> _
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property DropDownButton() As RibbonButton
			Get
				Return _dropDownButton
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Padding() As Padding
			Get
				Return _padding
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Margin() As Padding
			Get
				Return _margin
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property MenuButtonVisible() As Boolean
			Get
				Return _menuButtonVisible
			End Get
			Set
				_menuButtonVisible = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Sensor() As RibbonMouseSensor
			Get
				Return _sensor
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property Items() As RibbonQuickAccessToolbarItemCollection
			Get
				If DropDownButtonVisible Then
					If Not _items.Contains(DropDownButton) Then
						_items.Add(DropDownButton)
					End If
				Else
					If _items.Contains(DropDownButton) Then
						_items.Remove(DropDownButton)
					End If
				End If
				Return _items
			End Get
		End Property
		Public Overrides Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
			Owner.Renderer.OnRenderRibbonQuickAccessToolbarBackground(New RibbonRenderEventArgs(Owner, e.Graphics, e.Clip))
			For Each item As RibbonItem In Items
				item.OnPaint(Me, New RibbonElementPaintEventArgs(item.Bounds, e.Graphics, RibbonElementSizeMode.Compact))
			Next
		End Sub
		Public Overrides Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim widthSum As Integer = Padding.Horizontal
			Dim maxHeight As Integer = 16
			For Each item As RibbonItem In Items
				If item.Equals(DropDownButton) Then
					Continue For
				End If
				Dim s As Size = item.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(e.Graphics, RibbonElementSizeMode.Compact))
				widthSum += s.Width + 1
				maxHeight = Math.Max(maxHeight, s.Height)
			Next
			widthSum -= 1
			If Site IsNot Nothing AndAlso Site.DesignMode Then
				widthSum += 16
			End If
			Dim result As New Size(widthSum, maxHeight)
			SetLastMeasuredSize(result)
			Return result
		End Function
		Public Overrides Sub SetBounds(bounds As Rectangle)
			MyBase.SetBounds(bounds)
			Dim curLeft As Integer = bounds.Left + Padding.Left
			For Each item As RibbonItem In Items
				item.SetBounds(New Rectangle(New Point(curLeft, bounds.Top), item.LastMeasuredSize))
				curLeft = item.Bounds.Right + 1
			Next
			DropDownButton.SetBounds(New Rectangle(bounds.Right + bounds.Height / 2 + 2, bounds.Top, 12, bounds.Height))
		End Sub
		Public Function GetAllChildComponents() As IEnumerable(Of System.ComponentModel.Component)
			Return Items.ToArray()
		End Function
		Public Function GetItems() As IEnumerable(Of RibbonItem)
			Return Items
		End Function
		Public Function GetContentBounds() As System.Drawing.Rectangle
			Return Bounds
		End Function
	End Class
End Namespace
