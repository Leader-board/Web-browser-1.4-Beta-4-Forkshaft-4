Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms.Design.Behavior
Imports System.Drawing
Imports System.Drawing.Drawing2D
Namespace System.Windows.Forms
	Public Class RibbonOrbAdornerGlyph
		Inherits Glyph
		Private _menuVisible As Boolean
		Private _behaviorService As BehaviorService
		Private _ribbon As Ribbon
		Private _componentDesigner As RibbonDesigner
		Public Sub New(behaviorService As BehaviorService, designer As RibbonDesigner, ribbon As Ribbon)
			MyBase.New(New RibbonOrbAdornerGlyphBehavior())
			_behaviorService = behaviorService
			_componentDesigner = designer
			_ribbon = ribbon
		End Sub
		Public Property MenuVisible() As Boolean
			Get
				Return _menuVisible
			End Get
			Set
				_menuVisible = value
			End Set
		End Property
		Public Overrides ReadOnly Property Bounds() As Rectangle
			Get
				Dim edge As Point = _behaviorService.ControlToAdornerWindow(_ribbon)
				Return New Rectangle(edge.X + _ribbon.OrbBounds.Left, edge.Y + _ribbon.OrbBounds.Top, _ribbon.OrbBounds.Height, _ribbon.OrbBounds.Height)
			End Get
		End Property
		Public Overrides Function GetHitTest(p As System.Drawing.Point) As Cursor
			If Bounds.Contains(p) Then
				Return Cursors.Hand
			End If
			Return Nothing
		End Function
		Public Overrides Sub Paint(pe As PaintEventArgs)
		End Sub
	End Class
	Public Class RibbonOrbAdornerGlyphBehavior
		Inherits Behavior
	End Class
End Namespace
