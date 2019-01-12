Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public NotInheritable Class RibbonSeparator
		Inherits RibbonItem
		Public Sub New()
		End Sub
		Public Sub New(text As String)
			Text = text
		End Sub
		Public Overrides Sub OnPaint(sender As Object, e As RibbonElementPaintEventArgs)
			Owner.Renderer.OnRenderRibbonItem(New RibbonItemRenderEventArgs(Owner, e.Graphics, e.Clip, Me))
			If Not String.IsNullOrEmpty(Text) Then
				Owner.Renderer.OnRenderRibbonItemText(New RibbonTextEventArgs(Owner, e.Graphics, e.Clip, Me, Rectangle.FromLTRB(Bounds.Left + Owner.ItemMargin.Left, Bounds.Top + Owner.ItemMargin.Top, Bounds.Right - Owner.ItemMargin.Right, Bounds.Bottom - Owner.ItemMargin.Bottom), Text, _
					FontStyle.Bold))
			End If
		End Sub
		Public Overrides Sub SetBounds(bounds As System.Drawing.Rectangle)
			MyBase.SetBounds(bounds)
		End Sub
		Public Overrides Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
			If e.SizeMode = RibbonElementSizeMode.DropDown Then
				If String.IsNullOrEmpty(Text) Then
					SetLastMeasuredSize(New Size(1, 3))
				Else
					Dim sz As Size = e.Graphics.MeasureString(Text, New Font(Owner.Font, FontStyle.Bold)).ToSize()
					SetLastMeasuredSize(New Size(sz.Width + Owner.ItemMargin.Horizontal, sz.Height + Owner.ItemMargin.Vertical))
				End If
			Else
				SetLastMeasuredSize(New Size(2, OwnerPanel.ContentBounds.Height - Owner.ItemPadding.Vertical - Owner.ItemMargin.Vertical))
			End If
			Return LastMeasuredSize
		End Function
	End Class
End Namespace
