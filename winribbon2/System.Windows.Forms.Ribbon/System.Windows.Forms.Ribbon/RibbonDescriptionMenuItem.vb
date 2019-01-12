Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonDescriptionMenuItem
		Inherits RibbonButton
		Private _description As String
		Private _descBounds As Rectangle
		Public Sub New()
			DropDownArrowDirection = RibbonArrowDirection.Left
			SetDropDownMargin(New Padding(10))
		End Sub
		Public Sub New(text As String)
			Me.New(Nothing, text, Nothing)
		End Sub
		Public Sub New(text As String, description As String)
			Me.New(Nothing, text, description)
		End Sub
		Public Sub New(image As Image, text As String, description As String)
			Image = image
			Text = text
			Description = description
		End Sub
		Public Property DescriptionBounds() As Rectangle
			Get
				Return _descBounds
			End Get
			Set
				_descBounds = value
			End Set
		End Property
		Public Overrides Property Image() As System.Drawing.Image
			Get
				Return MyBase.Image
			End Get
			Set
				MyBase.Image = value
				SmallImage = value
			End Set
		End Property
		<Browsable(False)> _
		Public Overrides Property SmallImage() As System.Drawing.Image
			Get
				Return MyBase.SmallImage
			End Get
			Set
				MyBase.SmallImage = value
			End Set
		End Property
		Public Property Description() As String
			Get
				Return _description
			End Get
			Set
				_description = value
			End Set
		End Property
		Protected Overrides Sub OnPaintText(e As RibbonElementPaintEventArgs)
			If e.Mode = RibbonElementSizeMode.DropDown Then
				Dim sf As New StringFormat()
				sf.LineAlignment = StringAlignment.Center
				sf.Alignment = StringAlignment.Near
				Owner.Renderer.OnRenderRibbonItemText(New RibbonTextEventArgs(Owner, e.Graphics, e.Clip, Me, TextBounds, Text, _
					Color.Empty, FontStyle.Bold, sf))
				sf.Alignment = StringAlignment.Near
				Owner.Renderer.OnRenderRibbonItemText(New RibbonTextEventArgs(Owner, e.Graphics, e.Clip, Me, DescriptionBounds, Description, _
					sf))
			Else
				MyBase.OnPaintText(e)
			End If
		End Sub
		Public Overrides Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			Dim s As Size = MyBase.MeasureSize(sender, e)
			s.Height = 52
			SetLastMeasuredSize(s)
			Return s
		End Function
		Friend Overrides Function OnGetTextBounds(sMode As RibbonElementSizeMode, bounds As Rectangle) As Rectangle
			Dim r As Rectangle = MyBase.OnGetTextBounds(sMode, bounds)
			DescriptionBounds = r
			r.Height = 20
			DescriptionBounds = Rectangle.FromLTRB(DescriptionBounds.Left, r.Bottom, DescriptionBounds.Right, DescriptionBounds.Bottom)
			Return r
		End Function
	End Class
End Namespace
