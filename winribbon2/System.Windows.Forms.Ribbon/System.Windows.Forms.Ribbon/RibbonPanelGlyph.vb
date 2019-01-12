Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms.Design.Behavior
Imports System.Drawing
Imports System.Drawing.Drawing2D
Namespace System.Windows.Forms
	Public Class RibbonPanelGlyph
		Inherits Glyph
		Private _behaviorService As BehaviorService
		Private _tab As RibbonTab
		Private _componentDesigner As RibbonTabDesigner
		Private size As Size
		Public Sub New(behaviorService As BehaviorService, designer As RibbonTabDesigner, tab As RibbonTab)
			MyBase.New(New RibbonPanelGlyphBehavior(designer, tab))
			_behaviorService = behaviorService
			_componentDesigner = designer
			_tab = tab
			size = New Size(60, 16)
		End Sub
		Public Overrides ReadOnly Property Bounds() As Rectangle
			Get
				If Not _tab.Active OrElse Not _tab.Owner.Tabs.Contains(_tab) Then
					Return Rectangle.Empty
				End If
				Dim edge As Point = _behaviorService.ControlToAdornerWindow(_tab.Owner)
				Dim pnl As New Point(5, _tab.TabBounds.Bottom + 5)
				If _tab.Panels.Count > 0 Then
					Dim p As RibbonPanel = _tab.Panels(_tab.Panels.Count - 1)
					pnl.X = p.Bounds.Right + 5
				End If
				Return New Rectangle(edge.X + pnl.X, edge.Y + pnl.Y, size.Width, size.Height)
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
			Using p As GraphicsPath = RibbonProfessionalRenderer.RoundRectangle(Bounds, 9)
				Using b As New SolidBrush(Color.FromArgb(50, Color.Blue))
					pe.Graphics.FillPath(b, p)
				End Using
			End Using
			Dim sf As New StringFormat()
			sf.Alignment = StringAlignment.Center
			sf.LineAlignment = StringAlignment.Center
			pe.Graphics.DrawString("Add Panel", SystemFonts.DefaultFont, Brushes.White, Bounds, sf)
			pe.Graphics.SmoothingMode = smbuff
		End Sub
	End Class
	Public Class RibbonPanelGlyphBehavior
		Inherits Behavior
		Private _tab As RibbonTab
		Private _designer As RibbonTabDesigner
		Public Sub New(designer As RibbonTabDesigner, tab As RibbonTab)
			_designer = designer
			_tab = tab
		End Sub
		Public Overrides Function OnMouseUp(g As Glyph, button As MouseButtons) As Boolean
			_designer.AddPanel(Me, EventArgs.Empty)
			Return MyBase.OnMouseUp(g, button)
		End Function
	End Class
End Namespace
