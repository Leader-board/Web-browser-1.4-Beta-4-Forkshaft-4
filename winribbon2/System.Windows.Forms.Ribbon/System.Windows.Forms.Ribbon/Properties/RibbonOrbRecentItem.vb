Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonOrbRecentItem
		Inherits RibbonButton
		Public Sub New()
			MyBase.New()
		End Sub
		Public Sub New(text As String)
			Me.New()
			Text = text
		End Sub
		Friend Overrides Function OnGetImageBounds(sMode As RibbonElementSizeMode, bounds As System.Drawing.Rectangle) As Rectangle
			Return Rectangle.Empty
		End Function
		Friend Overrides Function OnGetTextBounds(sMode As RibbonElementSizeMode, bounds As Rectangle) As Rectangle
			Dim r As Rectangle = MyBase.OnGetTextBounds(sMode, bounds)
			r.X = Bounds.Left + 3
			Return r
		End Function
	End Class
End Namespace
