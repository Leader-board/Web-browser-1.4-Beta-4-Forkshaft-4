Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms.Design.Behavior
Imports System.Drawing
Imports System.Drawing.Drawing2D
Namespace System.Windows.Forms
	Public Class RibbonTabGlyph
		Inherits Glyph
		Private _behaviorService As BehaviorService
		Private _ribbon As Ribbon
		Private _componentDesigner As RibbonDesigner
		Private size As Size
		Public Sub New(behaviorService As BehaviorService, designer As RibbonDesigner, ribbon As Ribbon)
			MyBase.New(New RibbonTabGlyphBehavior(designer, ribbon))
			_behaviorService = behaviorService
			_componentDesigner = designer
			_ribbon = ribbon
			size = New Size(60, 16)
		End Sub
		Public Overrides ReadOnly Property Bounds() As Rectangle
			Get
				Dim edge As Point = _behaviorService.ControlToAdornerWindow(_ribbon)
				Dim tab As New Point(5, _ribbon.OrbBounds.Bottom + 5)
				If _ribbon.Tabs.Count > 0 Then
					Dim t As RibbonTab = _ribbon.Tabs(_ribbon.Tabs.Count - 1)
					tab.X = t.Bounds.Right + 5
					tab.Y = t.Bounds.Top + 2
				End If
				Return New Rectangle(edge.X + tab.X, edge.Y + tab.Y, size.Width, size.Height)
			End Get
		End Property
		Public Overrides Function GetHitTest(p As System.Drawing.Point) As Cursor
			If Bounds.Contains(p) Then
				Return Cursors.Hand
			End If
			Return Nothing
		End Function
		Public Overrides Sub Paint(pe As PaintEventArgs)
			Dim smbuff As SmoothingMode = pe.Graphics.SmoothingMode
			pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias
			Using p As GraphicsPath = RibbonProfessionalRenderer.RoundRectangle(Bounds, 2)
				Using b As New SolidBrush(Color.FromArgb(50, Color.Blue))
					pe.Graphics.FillPath(b, p)
				End Using
			End Using
			Dim sf As New StringFormat()
			sf.Alignment = StringAlignment.Center
			sf.LineAlignment = StringAlignment.Center
			pe.Graphics.DrawString("Add Tab", SystemFonts.DefaultFont, Brushes.White, Bounds, sf)
			pe.Graphics.SmoothingMode = smbuff
		End Sub
	End Class
	Public Class RibbonTabGlyphBehavior
		Inherits Behavior
		Private _ribbon As Ribbon
		Private _designer As RibbonDesigner
		Public Sub New(designer As RibbonDesigner, ribbon As Ribbon)
			_designer = designer
			_ribbon = ribbon
		End Sub
		Public Overrides Function OnMouseUp(g As Glyph, button As MouseButtons) As Boolean
			_designer.AddTabVerb(Me, EventArgs.Empty)
			Return MyBase.OnMouseUp(g, button)
		End Function
	End Class
End Namespace
