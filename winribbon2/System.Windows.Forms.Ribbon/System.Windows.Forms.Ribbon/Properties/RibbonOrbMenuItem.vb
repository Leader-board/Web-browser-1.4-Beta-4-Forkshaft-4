Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Drawing
Namespace System.Windows.Forms
	<Designer(GetType(RibbonOrbMenuItemDesigner))> _
	Public Class RibbonOrbMenuItem
		Inherits RibbonButton
		Public Sub New()
			DropDownArrowDirection = RibbonArrowDirection.Left
			SetDropDownMargin(New Padding(10))
			DropDownShowing += New EventHandler(RibbonOrbMenuItem_DropDownShowing)
		End Sub
		Public Sub New(text As String)
			Me.New()
			Text = text
		End Sub
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
		Private Sub RibbonOrbMenuItem_DropDownShowing(sender As Object, e As EventArgs)
			If DropDown IsNot Nothing Then
				DropDown.DrawIconsBar = False
			End If
		End Sub
		Public Overrides Sub OnMouseEnter(e As MouseEventArgs)
			MyBase.OnMouseEnter(e)
			If RibbonDesigner.Current Is Nothing Then
				If Owner.OrbDropDown.LastPoppedMenuItem IsNot Nothing Then
					Owner.OrbDropDown.LastPoppedMenuItem.CloseDropDown()
				End If
				If Style = RibbonButtonStyle.DropDown OrElse Style = RibbonButtonStyle.SplitDropDown Then
					ShowDropDown()
					Owner.OrbDropDown.LastPoppedMenuItem = Me
				End If
			End If
		End Sub
		Public Overrides Sub OnMouseLeave(e As MouseEventArgs)
			MyBase.OnMouseLeave(e)
		End Sub
		Friend Overrides Function OnGetDropDownMenuLocation() As Point
			If Owner Is Nothing Then
				Return MyBase.OnGetDropDownMenuLocation()
			End If
			Dim b As Rectangle = Owner.RectangleToScreen(Bounds)
			Dim c As Rectangle = Owner.OrbDropDown.RectangleToScreen(Owner.OrbDropDown.ContentRecentItemsBounds)
			Return New Point(b.Right, c.Top)
		End Function
		Friend Overrides Function OnGetDropDownMenuSize() As Size
			Dim r As Rectangle = Owner.OrbDropDown.ContentRecentItemsBounds
			r.Inflate(-1, -1)
			Return r.Size
		End Function
	End Class
End Namespace
