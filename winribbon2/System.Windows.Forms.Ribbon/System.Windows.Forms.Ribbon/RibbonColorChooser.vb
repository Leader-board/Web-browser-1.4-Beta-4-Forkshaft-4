Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Namespace System.Windows.Forms
	Public Class RibbonColorChooser
		Inherits RibbonButton
		Private _color As Color
		Private _imageColorHeight As Integer
		Private _smallImageColorHeight As Integer
		Public Event ColorChanged As EventHandler
		Public Sub New()
			_color = Color.Transparent
			_imageColorHeight = 8
			_smallImageColorHeight = 4
		End Sub
		<Description("Height of the color preview on the large image")> _
		<DefaultValue(8)> _
		Public Property ImageColorHeight() As Integer
			Get
				Return _imageColorHeight
			End Get
			Set
				_imageColorHeight = value
			End Set
		End Property
		<Description("Height of the color preview on the small image")> _
		<DefaultValue(4)> _
		Public Property SmallImageColorHeight() As Integer
			Get
				Return _smallImageColorHeight
			End Get
			Set
				_smallImageColorHeight = value
			End Set
		End Property
		Public Property Color() As Color
			Get
				Return _color
			End Get
			Set
				_color = value
				RedrawItem()
				OnColorChanged(EventArgs.Empty)
			End Set
		End Property
		Private Function CreateColorBmp(c As Color) As Image
			Dim b As New Bitmap(16, 16)
			Using g As Graphics = Graphics.FromImage(b)
				Using br As New SolidBrush(c)
					g.FillRectangle(br, New Rectangle(0, 0, 15, 15))
				End Using
				g.DrawRectangle(Pens.DimGray, New Rectangle(0, 0, 15, 15))
			End Using
			Return b
		End Function
		Protected Sub OnColorChanged(e As EventArgs)
			If ColorChanged IsNot Nothing Then
				ColorChanged(Me, e)
			End If
		End Sub
		Public Overrides Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
			MyBase.OnPaint(sender, e)
			Dim c As Color = If(Me.Color.Equals(Color.Transparent), Color.White, Color)
			Dim h As Integer = If(e.Mode = RibbonElementSizeMode.Large, ImageColorHeight, SmallImageColorHeight)
			Dim colorFill As Rectangle = Rectangle.FromLTRB(ImageBounds.Left, ImageBounds.Bottom - h, ImageBounds.Right, ImageBounds.Bottom)
			Dim sm As SmoothingMode = e.Graphics.SmoothingMode
			e.Graphics.SmoothingMode = SmoothingMode.None
			Using b As New SolidBrush(c)
				e.Graphics.FillRectangle(b, colorFill)
			End Using
			If Me.Color.Equals(Color.Transparent) Then
				e.Graphics.DrawRectangle(Pens.DimGray, colorFill)
			End If
			e.Graphics.SmoothingMode = sm
		End Sub
	End Class
End Namespace
