Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.ComponentModel
Namespace System.Windows.Forms
	Public Class RibbonTextBox
		Inherits RibbonItem
		Private Const spacing As Integer = 3
		Private _actualTextBox As TextBox
		Private _removingTxt As Boolean
		Private _labelVisible As Boolean
		Private _imageVisible As Boolean
		Private _labelBounds As Rectangle
		Private _imageBounds As Rectangle
		Private _textboxWidth As Integer
		Private _textBoxBounds As Rectangle
		Private _textBoxText As String
		Public Event TextBoxTextChanged As EventHandler
		Public Sub New()
			_textboxWidth = 100
		End Sub
		<Description("Text on the textbox")> _
		Public Property TextBoxText() As String
			Get
				Return _textBoxText
			End Get
			Set
				_textBoxText = value
				OnTextChanged(EventArgs.Empty)
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property TextBoxTextBounds() As Rectangle
			Get
				Return TextBoxBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ImageBounds() As Rectangle
			Get
				Return _imageBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property LabelBounds() As Rectangle
			Get
				Return _labelBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property ImageVisible() As Boolean
			Get
				Return _imageVisible
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property LabelVisible() As Boolean
			Get
				Return _labelVisible
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overridable ReadOnly Property TextBoxBounds() As Rectangle
			Get
				Return _textBoxBounds
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Editing() As Boolean
			Get
				Return _actualTextBox <> Nothing
			End Get
		End Property
		<DefaultValue(100)> _
		Public Property TextBoxWidth() As Integer
			Get
				Return _textboxWidth
			End Get
			Set
				_textboxWidth = value
				NotifyOwnerRegionsChanged()
			End Set
		End Property
		Public Sub StartEdit()
			PlaceActualTextBox()
			_actualTextBox.SelectAll()
			_actualTextBox.Focus()
		End Sub
		Public Sub EndEdit()
			RemoveActualTextBox()
		End Sub
		Protected Sub PlaceActualTextBox()
			_actualTextBox = New TextBox()
			InitTextBox(_actualTextBox)
			_actualTextBox.TextChanged += New EventHandler(_actualTextbox_TextChanged)
			_actualTextBox.KeyDown += New KeyEventHandler(_actualTextbox_KeyDown)
			_actualTextBox.LostFocus += New EventHandler(_actualTextbox_LostFocus)
			_actualTextBox.VisibleChanged += New EventHandler(_actualTextBox_VisibleChanged)
			_actualTextBox.Visible = True
			Canvas.Controls.Add(_actualTextBox)
		End Sub
		Private Sub _actualTextBox_VisibleChanged(sender As Object, e As EventArgs)
			If Not (TryCast(sender, TextBox)).Visible AndAlso Not _removingTxt Then
				RemoveActualTextBox()
			End If
		End Sub
		Protected Sub RemoveActualTextBox()
			If _actualTextBox Is Nothing OrElse _removingTxt Then
				Return
			End If
			_removingTxt = True
			TextBoxText = _actualTextBox.Text
			_actualTextBox.Visible = False
			_actualTextBox.Parent.Controls.Remove(_actualTextBox)
			_actualTextBox.Dispose()
			_actualTextBox = Nothing
			RedrawItem()
			_removingTxt = False
		End Sub
		Protected Overridable Sub InitTextBox(t As TextBox)
			t.Text = Me.TextBoxText
			t.BorderStyle = BorderStyle.None
			t.Width = TextBoxBounds.Width - 2
			t.Location = New Point(TextBoxBounds.Left + 2, Bounds.Top + (Bounds.Height - t.Height) / 2)
		End Sub
		Private Sub _actualTextbox_LostFocus(sender As Object, e As EventArgs)
			RemoveActualTextBox()
		End Sub
		Private Sub _actualTextbox_KeyDown(sender As Object, e As KeyEventArgs)
			If e.KeyCode = Keys.[Return] OrElse e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Escape Then
				RemoveActualTextBox()
			End If
		End Sub
		Private Sub _actualTextbox_TextChanged(sender As Object, e As EventArgs)
		End Sub
		Protected Overridable Function MeasureHeight() As Integer
			Return 16 + Owner.ItemMargin.Vertical
		End Function
		Public Overrides Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
			Owner.Renderer.OnRenderRibbonItem(New RibbonItemRenderEventArgs(Owner, e.Graphics, Bounds, Me))
			If ImageVisible Then
				Owner.Renderer.OnRenderRibbonItemImage(New RibbonItemBoundsEventArgs(Owner, e.Graphics, e.Clip, Me, _imageBounds))
			End If
			Dim f As New StringFormat()
			f.Alignment = StringAlignment.Near
			f.LineAlignment = StringAlignment.Center
			f.Trimming = StringTrimming.None
			f.FormatFlags = f.FormatFlags Or StringFormatFlags.NoWrap
			Owner.Renderer.OnRenderRibbonItemText(New RibbonTextEventArgs(Owner, e.Graphics, Bounds, Me, TextBoxTextBounds, TextBoxText, _
				f))
			If LabelVisible Then
				Owner.Renderer.OnRenderRibbonItemText(New RibbonTextEventArgs(Owner, e.Graphics, Bounds, Me, LabelBounds, Text, _
					f))
			End If
		End Sub
		Public Overrides Sub SetBounds(bounds As System.Drawing.Rectangle)
			MyBase.SetBounds(bounds)
			_textBoxBounds = Rectangle.FromLTRB(bounds.Right - TextBoxWidth, bounds.Top, bounds.Right, bounds.Bottom)
			If Image IsNot Nothing Then
				_imageBounds = New Rectangle(bounds.Left + Owner.ItemMargin.Left, bounds.Top + Owner.ItemMargin.Top, Image.Width, Image.Height)
			Else
				_imageBounds = New Rectangle(ContentBounds.Location, Size.Empty)
			End If
			_labelBounds = Rectangle.FromLTRB(_imageBounds.Right + (If(_imageBounds.Width > 0, spacing, 0)), bounds.Top, _textBoxBounds.Left - spacing, bounds.Bottom - Owner.ItemMargin.Bottom)
			If SizeMode = RibbonElementSizeMode.Large Then
				_imageVisible = True
				_labelVisible = True
			ElseIf SizeMode = RibbonElementSizeMode.Medium Then
				_imageVisible = True
				_labelVisible = False
				_labelBounds = Rectangle.Empty
			ElseIf SizeMode = RibbonElementSizeMode.Compact Then
				_imageBounds = Rectangle.Empty
				_imageVisible = False
				_labelBounds = Rectangle.Empty
				_labelVisible = False
			End If
		End Sub
		Public Overrides Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim size As Size = Size.Empty
			Dim w As Integer = 0
			Dim iwidth As Integer = If(Image <> Nothing, Image.Width + spacing, 0)
			Dim lwidth As Integer = If(String.IsNullOrEmpty(Text), 0, e.Graphics.MeasureString(Text, Owner.Font).ToSize().Width + spacing)
			Dim twidth As Integer = TextBoxWidth
			w += TextBoxWidth
			Select Case e.SizeMode
				Case RibbonElementSizeMode.Large
					w += iwidth + lwidth
					Exit Select
				Case RibbonElementSizeMode.Medium
					w += iwidth
					Exit Select
			End Select
			SetLastMeasuredSize(New Size(w, MeasureHeight()))
			Return LastMeasuredSize
		End Function
		Public Overrides Sub OnMouseEnter(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			MyBase.OnMouseEnter(e)
			Canvas.Cursor = Cursors.IBeam
		End Sub
		Public Overrides Sub OnMouseLeave(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			MyBase.OnMouseLeave(e)
			Canvas.Cursor = Cursors.[Default]
		End Sub
		Public Overrides Sub OnMouseDown(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			MyBase.OnMouseDown(e)
			If TextBoxBounds.Contains(e.X, e.Y) Then
				StartEdit()
			End If
		End Sub
		Public Overrides Sub OnMouseMove(e As MouseEventArgs)
			If Not Enabled Then
				Return
			End If
			MyBase.OnMouseMove(e)
			If TextBoxBounds.Contains(e.X, e.Y) Then
				Owner.Cursor = Cursors.IBeam
			Else
				Owner.Cursor = Cursors.[Default]
			End If
		End Sub
		Public Sub OnTextChanged(e As EventArgs)
			If Not Enabled Then
				Return
			End If
			NotifyOwnerRegionsChanged()
			If TextBoxTextChanged IsNot Nothing Then
				TextBoxTextChanged(Me, e)
			End If
		End Sub
	End Class
End Namespace
