Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms.Design.Behavior
Imports System.Drawing
Imports System.ComponentModel.Design
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Namespace System.Windows.Forms
	Public Class RibbonQuickAccessToolbarGlyph
		Inherits Glyph
		Private _behaviorService As BehaviorService
		Private _ribbon As Ribbon
		Private _componentDesigner As RibbonDesigner
		Public Sub New(behaviorService As BehaviorService, designer As RibbonDesigner, ribbon As Ribbon)
			MyBase.New(New RibbonQuickAccessGlyphBehavior(designer, ribbon))
			_behaviorService = behaviorService
			_componentDesigner = designer
			_ribbon = ribbon
		End Sub
		Public Overrides ReadOnly Property Bounds() As Rectangle
			Get
				Dim edge As Point = _behaviorService.ControlToAdornerWindow(_ribbon)
				Return New Rectangle(edge.X + _ribbon.QuickAcessToolbar.Bounds.Right + _ribbon.QuickAcessToolbar.Bounds.Height / 2 + 4 + _ribbon.QuickAcessToolbar.DropDownButton.Bounds.Width, edge.Y + _ribbon.QuickAcessToolbar.Bounds.Top, _ribbon.QuickAcessToolbar.Bounds.Height, _ribbon.QuickAcessToolbar.Bounds.Height)
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
			Using b As New SolidBrush(Color.FromArgb(50, Color.Blue))
				pe.Graphics.FillEllipse(b, Bounds)
			End Using
			Dim sf As New StringFormat()
			sf.Alignment = StringAlignment.Center
			sf.LineAlignment = StringAlignment.Center
			pe.Graphics.DrawString("+", SystemFonts.DefaultFont, Brushes.White, Bounds, sf)
			pe.Graphics.SmoothingMode = smbuff
		End Sub
	End Class
	Public Class RibbonQuickAccessGlyphBehavior
		Inherits Behavior
		Private _ribbon As Ribbon
		Private _designer As RibbonDesigner
		Public Sub New(designer As RibbonDesigner, ribbon As Ribbon)
			_designer = designer
			_ribbon = ribbon
		End Sub
		Public Overrides Function OnMouseUp(g As Glyph, button As MouseButtons) As Boolean
			_designer.CreateItem(_ribbon, _ribbon.QuickAcessToolbar.Items, GetType(RibbonButton))
			Return MyBase.OnMouseUp(g, button)
		End Function
	End Class
End Namespace
