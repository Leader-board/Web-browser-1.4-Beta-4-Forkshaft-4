Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.RibbonHelpers
Imports System.Windows.Forms.VisualStyles
Namespace System.Windows.Forms
	Public Class RibbonProfessionalRenderer
		Inherits RibbonRenderer
		Public Enum Corners
			None = 0
			NorthWest = 2
			NorthEast = 4
			SouthEast = 8
			SouthWest = 16
			All = NorthWest Or NorthEast Or SouthEast Or SouthWest
			North = NorthWest Or NorthEast
			South = SouthEast Or SouthWest
			East = NorthEast Or SouthEast
			West = NorthWest Or SouthWest
		End Enum
		Private arrowSize As New Size(5, 3)
		Private moreSize As New Size(7, 7)
		Public Sub New()
			ColorTable = New RibbonProfesionalRendererColorTable()
		End Sub
		Private _colorTable As RibbonProfesionalRendererColorTable
		Public Property ColorTable() As RibbonProfesionalRendererColorTable
			Get
				Return _colorTable
			End Get
			Set
				_colorTable = value
			End Set
		End Property
		Public Function GetTextColor(enabled As Boolean) As Color
			Return GetTextColor(enabled, ColorTable.Text)
		End Function
		Public Function GetTextColor(enabled As Boolean, alternative As Color) As Color
			If enabled Then
				Return alternative
			Else
				Return ColorTable.ArrowDisabled
			End If
		End Function
		Public Shared Function RoundRectangle(r As Rectangle, radius As Integer) As GraphicsPath
			Return RoundRectangle(r, radius, Corners.All)
		End Function
		Public Shared Function RoundRectangle(r As Rectangle, radius As Integer, corners As Corners) As GraphicsPath
			Dim path As New GraphicsPath()
			Dim d As Integer = radius * 2
			Dim nw As Integer = If((corners And Corners.NorthWest) = Corners.NorthWest, d, 0)
			Dim ne As Integer = If((corners And Corners.NorthEast) = Corners.NorthEast, d, 0)
			Dim se As Integer = If((corners And Corners.SouthEast) = Corners.SouthEast, d, 0)
			Dim sw As Integer = If((corners And Corners.SouthWest) = Corners.SouthWest, d, 0)
			path.AddLine(r.Left + nw, r.Top, r.Right - ne, r.Top)
			If ne > 0 Then
				path.AddArc(Rectangle.FromLTRB(r.Right - ne, r.Top, r.Right, r.Top + ne), -90, 90)
			End If
			path.AddLine(r.Right, r.Top + ne, r.Right, r.Bottom - se)
			If se > 0 Then
				path.AddArc(Rectangle.FromLTRB(r.Right - se, r.Bottom - se, r.Right, r.Bottom), 0, 90)
			End If
			path.AddLine(r.Right - se, r.Bottom, r.Left + sw, r.Bottom)
			If sw > 0 Then
				path.AddArc(Rectangle.FromLTRB(r.Left, r.Bottom - sw, r.Left + sw, r.Bottom), 90, 90)
			End If
			path.AddLine(r.Left, r.Bottom - sw, r.Left, r.Top + nw)
			If nw > 0 Then
				path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + nw, r.Top + nw), 180, 90)
			End If
			path.CloseFigure()
			Return path
		End Function
		Private Sub GradientRect(g As Graphics, r As Rectangle, northColor As Color, southColor As Color)
			Using b As Brush = New LinearGradientBrush(New Point(r.X, r.Y - 1), New Point(r.Left, r.Bottom), northColor, southColor)
				g.FillRectangle(b, r)
			End Using
		End Sub
		Public Sub DrawPressedShadow(g As Graphics, r As Rectangle)
			Dim shadow As Rectangle = Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Top + 4)
			Using path As GraphicsPath = RoundRectangle(shadow, 3, Corners.NorthEast Or Corners.NorthWest)
				Using b As New LinearGradientBrush(shadow, Color.FromArgb(50, Color.Black), Color.FromArgb(0, Color.Black), 90)
					b.WrapMode = WrapMode.TileFlipXY
					g.FillPath(b, path)
				End Using
			End Using
		End Sub
		Public Sub DrawArrow(g As Graphics, b As Rectangle, c As Color, d As RibbonArrowDirection)
			Dim path As New GraphicsPath()
			Dim bounds As Rectangle = b
			If b.Width Mod 2 <> 0 AndAlso (d = RibbonArrowDirection.Up) Then
				bounds = New Rectangle(New Point(b.Left - 1, b.Top - 1), New Size(b.Width + 1, b.Height + 1))
			End If
			If d = RibbonArrowDirection.Up Then
				path.AddLine(bounds.Left, bounds.Bottom, bounds.Right, bounds.Bottom)
				path.AddLine(bounds.Right, bounds.Bottom, bounds.Left + bounds.Width / 2, bounds.Top)
			ElseIf d = RibbonArrowDirection.Down Then
				path.AddLine(bounds.Left, bounds.Top, bounds.Right, bounds.Top)
				path.AddLine(bounds.Right, bounds.Top, bounds.Left + bounds.Width / 2, bounds.Bottom)
			ElseIf d = RibbonArrowDirection.Left Then
				path.AddLine(bounds.Left, bounds.Top, bounds.Right, bounds.Top + bounds.Height / 2)
				path.AddLine(bounds.Right, bounds.Top + bounds.Height / 2, bounds.Left, bounds.Bottom)
			Else
				path.AddLine(bounds.Right, bounds.Top, bounds.Left, bounds.Top + bounds.Height / 2)
				path.AddLine(bounds.Left, bounds.Top + bounds.Height / 2, bounds.Right, bounds.Bottom)
			End If
			path.CloseFigure()
			Using bb As New SolidBrush(c)
				Dim sm As SmoothingMode = g.SmoothingMode
				g.SmoothingMode = SmoothingMode.None
				g.FillPath(bb, path)
				g.SmoothingMode = sm
			End Using
			path.Dispose()
		End Sub
		Public Sub DrawArrowShaded(g As Graphics, b As Rectangle, d As RibbonArrowDirection, enabled As Boolean)
			Dim arrSize As Size = arrowSize
			If d = RibbonArrowDirection.Left OrElse d = RibbonArrowDirection.Right Then
				arrSize = New Size(arrowSize.Height, arrowSize.Width)
			End If
			Dim arrowP As New Point(b.Left + (b.Width - arrSize.Width) / 2, b.Top + (b.Height - arrSize.Height) / 2)
			Dim bounds As New Rectangle(arrowP, arrSize)
			Dim boundsLight As Rectangle = bounds
			boundsLight.Offset(0, 1)
			Dim lt As Color = ColorTable.ArrowLight
			Dim dk As Color = ColorTable.Arrow
			If Not enabled Then
				lt = Color.Transparent
				dk = ColorTable.ArrowDisabled
			End If
			DrawArrow(g, boundsLight, lt, d)
			DrawArrow(g, bounds, dk, d)
		End Sub
		Public Function CenterOn(container As Rectangle, r As Rectangle) As Rectangle
			Dim result As New Rectangle(container.Left + ((container.Width - r.Width) / 2), container.Top + ((container.Height - r.Height) / 2), r.Width, r.Height)
			Return result
		End Function
		Public Sub DrawGripDot(g As Graphics, location As Point)
			Dim lt As New Rectangle(location.X - 1, location.Y + 1, 2, 2)
			Dim dk As New Rectangle(location, New Size(2, 2))
			Using b As New SolidBrush(ColorTable.DropDownGripLight)
				g.FillRectangle(b, lt)
			End Using
			Using b As New SolidBrush(ColorTable.DropDownGripDark)
				g.FillRectangle(b, dk)
			End Using
		End Sub
		Public Function CreateCompleteTabPath(t As RibbonTab) As GraphicsPath
			Dim path As New GraphicsPath()
			Dim corner As Integer = 6
			path.AddLine(t.TabBounds.Left + corner, t.TabBounds.Top, t.TabBounds.Right - corner, t.TabBounds.Top)
			path.AddArc(Rectangle.FromLTRB(t.TabBounds.Right - corner, t.TabBounds.Top, t.TabBounds.Right, t.TabBounds.Top + corner), -90, 90)
			path.AddLine(t.TabBounds.Right, t.TabBounds.Top + corner, t.TabBounds.Right, t.TabBounds.Bottom - corner)
			path.AddArc(Rectangle.FromLTRB(t.TabBounds.Right, t.TabBounds.Bottom - corner, t.TabBounds.Right + corner, t.TabBounds.Bottom), -180, -90)
			path.AddLine(t.TabBounds.Right + corner, t.TabBounds.Bottom, t.TabContentBounds.Right - corner, t.TabBounds.Bottom)
			path.AddArc(Rectangle.FromLTRB(t.TabContentBounds.Right - corner, t.TabBounds.Bottom, t.TabContentBounds.Right, t.TabBounds.Bottom + corner), -90, 90)
			path.AddLine(t.TabContentBounds.Right, t.TabContentBounds.Top + corner, t.TabContentBounds.Right, t.TabContentBounds.Bottom - corner)
			path.AddArc(Rectangle.FromLTRB(t.TabContentBounds.Right - corner, t.TabContentBounds.Bottom - corner, t.TabContentBounds.Right, t.TabContentBounds.Bottom), 0, 90)
			path.AddLine(t.TabContentBounds.Right - corner, t.TabContentBounds.Bottom, t.TabContentBounds.Left + corner, t.TabContentBounds.Bottom)
			path.AddArc(Rectangle.FromLTRB(t.TabContentBounds.Left, t.TabContentBounds.Bottom - corner, t.TabContentBounds.Left + corner, t.TabContentBounds.Bottom), 90, 90)
			path.AddLine(t.TabContentBounds.Left, t.TabContentBounds.Bottom - corner, t.TabContentBounds.Left, t.TabBounds.Bottom + corner)
			path.AddArc(Rectangle.FromLTRB(t.TabContentBounds.Left, t.TabBounds.Bottom, t.TabContentBounds.Left + corner, t.TabBounds.Bottom + corner), 180, 90)
			path.AddLine(t.TabContentBounds.Left + corner, t.TabContentBounds.Top, t.TabBounds.Left - corner, t.TabBounds.Bottom)
			path.AddArc(Rectangle.FromLTRB(t.TabBounds.Left - corner, t.TabBounds.Bottom - corner, t.TabBounds.Left, t.TabBounds.Bottom), 90, -90)
			path.AddLine(t.TabBounds.Left, t.TabBounds.Bottom - corner, t.TabBounds.Left, t.TabBounds.Top + corner)
			path.AddArc(Rectangle.FromLTRB(t.TabBounds.Left, t.TabBounds.Top, t.TabBounds.Left + corner, t.TabBounds.Top + corner), 180, 90)
			path.CloseFigure()
			Return path
		End Function
		Public Function CreateTabPath(t As RibbonTab) As GraphicsPath
			Dim path As New GraphicsPath()
			Dim corner As Integer = 6
			Dim rightOffset As Integer = 1
			path.AddLine(t.TabBounds.Left, t.TabBounds.Bottom, t.TabBounds.Left, t.TabBounds.Top + corner)
			path.AddArc(New Rectangle(t.TabBounds.Left, t.TabBounds.Top, corner, corner), 180, 90)
			path.AddLine(t.TabBounds.Left + corner, t.TabBounds.Top, t.TabBounds.Right - corner - rightOffset, t.TabBounds.Top)
			path.AddArc(New Rectangle(t.TabBounds.Right - corner - rightOffset, t.TabBounds.Top, corner, corner), -90, 90)
			path.AddLine(t.TabBounds.Right - rightOffset, t.TabBounds.Top + corner, t.TabBounds.Right - rightOffset, t.TabBounds.Bottom)
			Return path
		End Function
		Public Sub DrawCompleteTab(e As RibbonTabRenderEventArgs)
			DrawTabActive(e)
			Using path As GraphicsPath = RoundRectangle(e.Tab.TabContentBounds, 4)
				Dim north As Color = ColorTable.TabContentNorth
				Dim south As Color = ColorTable.TabContentSouth
				If e.Tab.Contextual Then
					north = ColorTable.DropDownBg
					south = north
				End If
				Using b As New LinearGradientBrush(New Point(0, e.Tab.TabContentBounds.Top + 30), New Point(0, e.Tab.TabContentBounds.Bottom - 10), north, south)
					b.WrapMode = WrapMode.TileFlipXY
					e.Graphics.FillPath(b, path)
				End Using
			End Using
			Dim glossy As Rectangle = Rectangle.FromLTRB(e.Tab.TabContentBounds.Left, e.Tab.TabContentBounds.Top + 0, e.Tab.TabContentBounds.Right, e.Tab.TabContentBounds.Top + 18)
			Using path As GraphicsPath = RoundRectangle(glossy, 6, Corners.NorthWest Or Corners.NorthEast)
				Using b As Brush = New SolidBrush(Color.FromArgb(30, Color.White))
					e.Graphics.FillPath(b, path)
				End Using
			End Using
			Using path As GraphicsPath = CreateCompleteTabPath(e.Tab)
				Using p As New Pen(ColorTable.TabBorder)
					e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
					e.Graphics.DrawPath(p, path)
				End Using
			End Using
			If e.Tab.Selected Then
				Using path As GraphicsPath = CreateTabPath(e.Tab)
					Dim p As New Pen(Color.FromArgb(150, Color.Gold))
					p.Width = 2
					e.Graphics.DrawPath(p, path)
					p.Dispose()
				End Using
			End If
		End Sub
		Public Sub DrawTabNormal(e As RibbonTabRenderEventArgs)
			Dim lastClip As RectangleF = e.Graphics.ClipBounds
			Dim clip As Rectangle = Rectangle.FromLTRB(e.Tab.TabBounds.Left, e.Tab.TabBounds.Top, e.Tab.TabBounds.Right, e.Tab.TabBounds.Bottom)
			Dim r As Rectangle = Rectangle.FromLTRB(e.Tab.TabBounds.Left - 1, e.Tab.TabBounds.Top - 1, e.Tab.TabBounds.Right, e.Tab.TabBounds.Bottom)
			e.Graphics.SetClip(clip)
			Using b As Brush = New SolidBrush(ColorTable.RibbonBackground)
				e.Graphics.FillRectangle(b, r)
			End Using
			e.Graphics.SetClip(lastClip)
		End Sub
		Public Sub DrawTabSelected(e As RibbonTabRenderEventArgs)
			Dim outerR As Rectangle = Rectangle.FromLTRB(e.Tab.TabBounds.Left, e.Tab.TabBounds.Top, e.Tab.TabBounds.Right - 1, e.Tab.TabBounds.Bottom)
			Dim innerR As Rectangle = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom)
			Dim glossyR As Rectangle = Rectangle.FromLTRB(innerR.Left + 1, innerR.Top + 1, innerR.Right - 1, innerR.Top + e.Tab.TabBounds.Height / 2)
			Dim outer As GraphicsPath = RoundRectangle(outerR, 3, Corners.NorthEast Or Corners.NorthWest)
			Dim inner As GraphicsPath = RoundRectangle(innerR, 3, Corners.NorthEast Or Corners.NorthWest)
			Dim glossy As GraphicsPath = RoundRectangle(glossyR, 3, Corners.NorthEast Or Corners.NorthWest)
			Using p As New Pen(ColorTable.TabBorder)
				e.Graphics.DrawPath(p, outer)
			End Using
			Using p As New Pen(Color.FromArgb(200, Color.White))
				e.Graphics.DrawPath(p, inner)
			End Using
			Using radialPath As New GraphicsPath()
				radialPath.AddRectangle(innerR)
				radialPath.CloseFigure()
				Dim gr As New PathGradientBrush(radialPath)
				gr.CenterPoint = New PointF(Convert.ToSingle(innerR.Left + innerR.Width / 2), Convert.ToSingle(innerR.Top - 5))
				gr.CenterColor = Color.Transparent
				gr.SurroundColors = New Color() {ColorTable.TabSelectedGlow}
				Dim blend As New Blend(3)
				blend.Factors = New Single() {0F, 0.9F, 0F}
				blend.Positions = New Single() {0F, 0.8F, 1F}
				gr.Blend = blend
				e.Graphics.FillPath(gr, radialPath)
				gr.Dispose()
			End Using
			Using b As New SolidBrush(Color.FromArgb(100, Color.White))
				e.Graphics.FillPath(b, glossy)
			End Using
			outer.Dispose()
			inner.Dispose()
			glossy.Dispose()
		End Sub
		Public Sub DrawTabPressed(e As RibbonTabRenderEventArgs)
		End Sub
		Public Sub DrawTabActive(e As RibbonTabRenderEventArgs)
			DrawTabNormal(e)
			Dim glossy As New Rectangle(e.Tab.TabBounds.Left, e.Tab.TabBounds.Top, e.Tab.TabBounds.Width, 4)
			Dim shadow As Rectangle = e.Tab.TabBounds
			shadow.Offset(2, 1)
			Dim tab As Rectangle = e.Tab.TabBounds
			Using path As GraphicsPath = RoundRectangle(shadow, 6, Corners.NorthWest Or Corners.NorthEast)
				Using b As New PathGradientBrush(path)
					b.WrapMode = WrapMode.Clamp
					Dim cb As New ColorBlend(3)
					cb.Colors = New Color() {Color.Transparent, Color.FromArgb(50, Color.Black), Color.FromArgb(100, Color.Black)}
					cb.Positions = New Single() {0F, 0.1F, 1F}
					b.InterpolationColors = cb
					e.Graphics.FillPath(b, path)
				End Using
			End Using
			Using path As GraphicsPath = RoundRectangle(tab, 6, Corners.North)
				Dim north As Color = ColorTable.TabNorth
				Dim south As Color = ColorTable.TabSouth
				If e.Tab.Contextual Then
					north = e.Tab.Context.GlowColor
					south = Color.FromArgb(10, north)
				End If
				Using p As New Pen(ColorTable.TabNorth, 1.6F)
					e.Graphics.DrawPath(p, path)
				End Using
				Using b As New LinearGradientBrush(e.Tab.TabBounds, ColorTable.TabNorth, ColorTable.TabSouth, 90)
					e.Graphics.FillPath(b, path)
				End Using
			End Using
			Using path As GraphicsPath = RoundRectangle(glossy, 6, Corners.North)
				Using b As Brush = New SolidBrush(Color.FromArgb(180, Color.White))
					e.Graphics.FillPath(b, path)
				End Using
			End Using
		End Sub
		Public Sub DrawPanelNormal(e As RibbonPanelRenderEventArgs)
			Dim darkBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left, e.Panel.Bounds.Top, e.Panel.Bounds.Right, e.Panel.Bounds.Bottom)
			Dim lightBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left + 1, e.Panel.Bounds.Top + 1, e.Panel.Bounds.Right + 1, e.Panel.Bounds.Bottom)
			Dim textArea As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left + 1, e.Panel.ContentBounds.Bottom, e.Panel.Bounds.Right - 1, e.Panel.Bounds.Bottom - 1)
			Dim dark As GraphicsPath = RoundRectangle(darkBorder, 3)
			Dim light As GraphicsPath = RoundRectangle(lightBorder, 3)
			Dim txt As GraphicsPath = RoundRectangle(textArea, 3, Corners.SouthEast Or Corners.SouthWest)
			Using p As New Pen(ColorTable.PanelLightBorder)
				e.Graphics.DrawPath(p, light)
			End Using
			Using p As New Pen(ColorTable.PanelDarkBorder)
				e.Graphics.DrawPath(p, dark)
			End Using
			Using b As New SolidBrush(ColorTable.PanelTextBackground)
				e.Graphics.FillPath(b, txt)
			End Using
			If e.Panel.ButtonMoreVisible Then
				DrawButtonMoreGlyph(e.Graphics, e.Panel.ButtonMoreBounds, e.Panel.ButtonMoreEnabled AndAlso e.Panel.Enabled)
			End If
			txt.Dispose()
			dark.Dispose()
			light.Dispose()
		End Sub
		Public Sub DrawPanelSelected(e As RibbonPanelRenderEventArgs)
			Dim darkBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left, e.Panel.Bounds.Top, e.Panel.Bounds.Right, e.Panel.Bounds.Bottom)
			Dim lightBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left + 1, e.Panel.Bounds.Top + 1, e.Panel.Bounds.Right - 1, e.Panel.Bounds.Bottom - 1)
			Dim textArea As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left + 1, e.Panel.ContentBounds.Bottom, e.Panel.Bounds.Right - 1, e.Panel.Bounds.Bottom - 1)
			Dim dark As GraphicsPath = RoundRectangle(darkBorder, 3)
			Dim light As GraphicsPath = RoundRectangle(lightBorder, 3)
			Dim txt As GraphicsPath = RoundRectangle(textArea, 3, Corners.SouthEast Or Corners.SouthWest)
			Using p As New Pen(ColorTable.PanelLightBorder)
				e.Graphics.DrawPath(p, light)
			End Using
			Using p As New Pen(ColorTable.PanelDarkBorder)
				e.Graphics.DrawPath(p, dark)
			End Using
			Using b As New SolidBrush(ColorTable.PanelBackgroundSelected)
				e.Graphics.FillPath(b, light)
			End Using
			Using b As New SolidBrush(ColorTable.PanelTextBackgroundSelected)
				e.Graphics.FillPath(b, txt)
			End Using
			If e.Panel.ButtonMoreVisible Then
				If e.Panel.ButtonMorePressed Then
					DrawButtonPressed(e.Graphics, e.Panel.ButtonMoreBounds, Corners.SouthEast)
				ElseIf e.Panel.ButtonMoreSelected Then
					DrawButtonSelected(e.Graphics, e.Panel.ButtonMoreBounds, Corners.SouthEast)
				End If
				DrawButtonMoreGlyph(e.Graphics, e.Panel.ButtonMoreBounds, e.Panel.ButtonMoreEnabled AndAlso e.Panel.Enabled)
			End If
			txt.Dispose()
			dark.Dispose()
			light.Dispose()
		End Sub
		Public Sub DrawButtonMoreGlyph(g As Graphics, b As Rectangle, enabled As Boolean)
			Dim dark As Color = If(enabled, ColorTable.Arrow, ColorTable.ArrowDisabled)
			Dim light As Color = ColorTable.ArrowLight
			Dim bounds As Rectangle = CenterOn(b, New Rectangle(Point.Empty, moreSize))
			Dim boundsLight As Rectangle = bounds
			boundsLight.Offset(1, 1)
			DrawButtonMoreGlyph(g, boundsLight.Location, light)
			DrawButtonMoreGlyph(g, bounds.Location, dark)
		End Sub
		Public Sub DrawButtonMoreGlyph(gr As Graphics, p As Point, color As Color)
			Dim a As Point = p
			Dim b As New Point(p.X + moreSize.Width - 1, p.Y)
			Dim c As New Point(p.X, p.Y + moreSize.Height - 1)
			Dim f As New Point(p.X + moreSize.Width, p.Y + moreSize.Height)
			Dim d As New Point(f.X, f.Y - 3)
			Dim e As New Point(f.X - 3, f.Y)
			Dim g As New Point(f.X - 3, f.Y - 3)
			Dim lastMode As SmoothingMode = gr.SmoothingMode
			gr.SmoothingMode = SmoothingMode.None
			Using pen As New Pen(color)
				gr.DrawLine(pen, a, b)
				gr.DrawLine(pen, a, c)
				gr.DrawLine(pen, e, f)
				gr.DrawLine(pen, d, f)
				gr.DrawLine(pen, e, d)
				gr.DrawLine(pen, g, f)
			End Using
			gr.SmoothingMode = lastMode
		End Sub
		Public Sub DrawPanelOverflowNormal(e As RibbonPanelRenderEventArgs)
			Dim darkBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left, e.Panel.Bounds.Top, e.Panel.Bounds.Right, e.Panel.Bounds.Bottom)
			Dim lightBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left + 1, e.Panel.Bounds.Top + 1, e.Panel.Bounds.Right - 1, e.Panel.Bounds.Bottom - 1)
			Dim dark As GraphicsPath = RoundRectangle(darkBorder, 3)
			Dim light As GraphicsPath = RoundRectangle(lightBorder, 3)
			Using p As New Pen(ColorTable.PanelLightBorder)
				e.Graphics.DrawPath(p, light)
			End Using
			Using p As New Pen(ColorTable.PanelDarkBorder)
				e.Graphics.DrawPath(p, dark)
			End Using
			DrawPanelOverflowImage(e)
			dark.Dispose()
			light.Dispose()
		End Sub
		Public Sub DrawPannelOveflowSelected(e As RibbonPanelRenderEventArgs)
			Dim darkBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left, e.Panel.Bounds.Top, e.Panel.Bounds.Right, e.Panel.Bounds.Bottom)
			Dim lightBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left + 1, e.Panel.Bounds.Top + 1, e.Panel.Bounds.Right - 1, e.Panel.Bounds.Bottom - 1)
			Dim dark As GraphicsPath = RoundRectangle(darkBorder, 3)
			Dim light As GraphicsPath = RoundRectangle(lightBorder, 3)
			Using p As New Pen(ColorTable.PanelLightBorder)
				e.Graphics.DrawPath(p, light)
			End Using
			Using p As New Pen(ColorTable.PanelDarkBorder)
				e.Graphics.DrawPath(p, dark)
			End Using
			Using b As New LinearGradientBrush(lightBorder, ColorTable.PanelOverflowBackgroundSelectedNorth, Color.Transparent, 90)
				e.Graphics.FillPath(b, light)
			End Using
			DrawPanelOverflowImage(e)
			dark.Dispose()
			light.Dispose()
		End Sub
		Public Sub DrawPanelOverflowPressed(e As RibbonPanelRenderEventArgs)
			Dim darkBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left, e.Panel.Bounds.Top, e.Panel.Bounds.Right, e.Panel.Bounds.Bottom)
			Dim lightBorder As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left + 1, e.Panel.Bounds.Top + 1, e.Panel.Bounds.Right - 1, e.Panel.Bounds.Bottom - 1)
			Dim glossy As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left, e.Panel.Bounds.Top, e.Panel.Bounds.Right, e.Panel.Bounds.Top + 17)
			Dim dark As GraphicsPath = RoundRectangle(darkBorder, 3)
			Dim light As GraphicsPath = RoundRectangle(lightBorder, 3)
			Using b As New LinearGradientBrush(lightBorder, ColorTable.PanelOverflowBackgroundPressed, ColorTable.PanelOverflowBackgroundSelectedSouth, 90)
				b.WrapMode = WrapMode.TileFlipXY
				e.Graphics.FillPath(b, dark)
			End Using
			Using path As GraphicsPath = RoundRectangle(glossy, 3, Corners.NorthEast Or Corners.NorthWest)
				Using b As New LinearGradientBrush(glossy, Color.FromArgb(150, Color.White), Color.FromArgb(50, Color.White), 90)
					b.WrapMode = WrapMode.TileFlipXY
					e.Graphics.FillPath(b, path)
				End Using
			End Using
			Using p As New Pen(Color.FromArgb(40, Color.White))
				e.Graphics.DrawPath(p, light)
			End Using
			Using p As New Pen(ColorTable.PanelDarkBorder)
				e.Graphics.DrawPath(p, dark)
			End Using
			DrawPanelOverflowImage(e)
			DrawPressedShadow(e.Graphics, glossy)
			dark.Dispose()
			light.Dispose()
		End Sub
		Public Sub DrawPanelOverflowImage(e As RibbonPanelRenderEventArgs)
			Dim margin As Integer = 3
			Dim imgSquareSize As New Size(32, 32)
			Dim imgSquareR As New Rectangle(New Point(e.Panel.Bounds.Left + (e.Panel.Bounds.Width - imgSquareSize.Width) / 2, e.Panel.Bounds.Top + 5), imgSquareSize)
			Dim imgSquareBottomR As Rectangle = Rectangle.FromLTRB(imgSquareR.Left, imgSquareR.Bottom - 10, imgSquareR.Right, imgSquareR.Bottom)
			Dim textR As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left + margin, imgSquareR.Bottom + margin, e.Panel.Bounds.Right - margin, e.Panel.Bounds.Bottom - margin)
			Dim imgSq As GraphicsPath = RoundRectangle(imgSquareR, 5)
			Dim imgSqB As GraphicsPath = RoundRectangle(imgSquareBottomR, 5, Corners.South)
			Using b As New LinearGradientBrush(imgSquareR, ColorTable.TabContentNorth, ColorTable.TabContentSouth, 90)
				e.Graphics.FillPath(b, imgSq)
			End Using
			Using b As New SolidBrush(ColorTable.PanelTextBackground)
				e.Graphics.FillPath(b, imgSqB)
			End Using
			Using p As New Pen(ColorTable.PanelDarkBorder)
				e.Graphics.DrawPath(p, imgSq)
			End Using
			If e.Panel.Image IsNot Nothing Then
				e.Graphics.DrawImage(e.Panel.Image, imgSquareR.Left + (imgSquareR.Width - e.Panel.Image.Width) / 2, imgSquareR.Top + ((imgSquareR.Height - imgSquareBottomR.Height) - e.Panel.Image.Height) / 2, e.Panel.Image.Width, e.Panel.Image.Height)
			End If
			Using b As New SolidBrush(GetTextColor(e.Panel.Enabled))
				Dim sf As New StringFormat()
				sf.Alignment = StringAlignment.Center
				sf.LineAlignment = StringAlignment.Near
				sf.Trimming = StringTrimming.Character
				e.Graphics.DrawString(e.Panel.Text, e.Ribbon.Font, b, textR, sf)
			End Using
			Dim bounds As Rectangle = LargeButtonDropDownArrowBounds(e.Graphics, e.Panel.Owner.Font, e.Panel.Text, textR)
			If bounds.Right < e.Panel.Bounds.Right Then
				Dim boundsLight As Rectangle = bounds
				boundsLight.Offset(0, 1)
				Dim lt As Color = ColorTable.ArrowLight
				Dim dk As Color = ColorTable.Arrow
				DrawArrow(e.Graphics, boundsLight, lt, RibbonArrowDirection.Down)
				DrawArrow(e.Graphics, bounds, dk, RibbonArrowDirection.Down)
			End If
			imgSq.Dispose()
			imgSqB.Dispose()
		End Sub
		Private Function ButtonCorners(button As RibbonButton) As Corners
			If Not (TypeOf button.OwnerItem Is RibbonItemGroup) Then
				Return Corners.All
			Else
				Dim g As RibbonItemGroup = TryCast(button.OwnerItem, RibbonItemGroup)
				Dim c As Corners = Corners.None
				If button = g.FirstItem Then
					c = c Or Corners.West
				End If
				If button = g.LastItem Then
					c = c Or Corners.East
				End If
				Return c
			End If
		End Function
		Private Function ButtonFaceRounding(button As RibbonButton) As Corners
			If Not (TypeOf button.OwnerItem Is RibbonItemGroup) Then
				If button.SizeMode = RibbonElementSizeMode.Large Then
					Return Corners.North
				Else
					Return Corners.West
				End If
			Else
				Dim c As Corners = Corners.None
				Dim g As RibbonItemGroup = TryCast(button.OwnerItem, RibbonItemGroup)
				If button = g.FirstItem Then
					c = c Or Corners.West
				End If
				Return c
			End If
		End Function
		Private Function ButtonDdRounding(button As RibbonButton) As Corners
			If Not (TypeOf button.OwnerItem Is RibbonItemGroup) Then
				If button.SizeMode = RibbonElementSizeMode.Large Then
					Return Corners.South
				Else
					Return Corners.East
				End If
			Else
				Dim c As Corners = Corners.None
				Dim g As RibbonItemGroup = TryCast(button.OwnerItem, RibbonItemGroup)
				If button = g.LastItem Then
					c = c Or Corners.East
				End If
				Return c
			End If
		End Function
		Public Sub DrawOrbOptionButton(g As Graphics, bounds As Rectangle)
			bounds.Width -= 1
			bounds.Height -= 1
			Using p As GraphicsPath = RoundRectangle(bounds, 3)
				Using b As New SolidBrush(ColorTable.OrbOptionBackground)
					g.FillPath(b, p)
				End Using
				GradientRect(g, Rectangle.FromLTRB(bounds.Left, bounds.Top + bounds.Height / 2, bounds.Right, bounds.Bottom - 2), ColorTable.OrbOptionShine, ColorTable.OrbOptionBackground)
				Using pen As New Pen(ColorTable.OrbOptionBorder)
					g.DrawPath(pen, p)
				End Using
			End Using
		End Sub
		Public Sub DrawButton(g As Graphics, bounds As Rectangle, corners As Corners)
			If bounds.Height <= 0 OrElse bounds.Width <= 0 Then
				Return
			End If
			Dim outerR As Rectangle = Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right - 1, bounds.Bottom - 1)
			Dim innerR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Bottom - 2)
			Dim glossyR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Top + Convert.ToInt32(CType(bounds.Height, Double) * 0.36))
			Using boundsPath As GraphicsPath = RoundRectangle(outerR, 3, corners)
				Using brus As New SolidBrush(ColorTable.ButtonBgOut)
					g.FillPath(brus, boundsPath)
				End Using
				Using path As New GraphicsPath()
					path.AddEllipse(New Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height * 2))
					path.CloseFigure()
					Using gradient As New PathGradientBrush(path)
						gradient.WrapMode = WrapMode.Clamp
						gradient.CenterPoint = New PointF(Convert.ToSingle(bounds.Left + bounds.Width / 2), Convert.ToSingle(bounds.Bottom))
						gradient.CenterColor = ColorTable.ButtonBgCenter
						gradient.SurroundColors = New Color() {ColorTable.ButtonBgOut}
						Dim blend As New Blend(3)
						blend.Factors = New Single() {0F, 0.8F, 0F}
						blend.Positions = New Single() {0F, 0.3F, 1F}
						Dim lastClip As Region = g.Clip
						Dim newClip As New Region(boundsPath)
						newClip.Intersect(lastClip)
						g.SetClip(newClip.GetBounds(g))
						g.FillPath(gradient, path)
						g.Clip = lastClip
					End Using
				End Using
				Using p As New Pen(ColorTable.ButtonBorderOut)
					g.DrawPath(p, boundsPath)
				End Using
				Using path As GraphicsPath = RoundRectangle(innerR, 3, corners)
					Using p As New Pen(ColorTable.ButtonBorderIn)
						g.DrawPath(p, path)
					End Using
				End Using
				Using path As GraphicsPath = RoundRectangle(glossyR, 3, (corners And Corners.NorthWest) Or (corners And Corners.NorthEast))
					If glossyR.Width > 0 AndAlso glossyR.Height > 0 Then
						Using b As New LinearGradientBrush(glossyR, ColorTable.ButtonGlossyNorth, ColorTable.ButtonGlossySouth, 90)
							b.WrapMode = WrapMode.TileFlipXY
							g.FillPath(b, path)
						End Using
					End If
				End Using
			End Using
		End Sub
		Public Function LargeButtonDropDownArrowBounds(g As Graphics, font As Font, text As String, textLayout As Rectangle) As Rectangle
			Dim bounds As Rectangle = Rectangle.Empty
			Dim moreWords As Boolean = text.Contains(" ")
			Dim sf As New StringFormat()
			sf.Alignment = StringAlignment.Center
			sf.LineAlignment = If(moreWords, StringAlignment.Center, StringAlignment.Near)
			sf.Trimming = StringTrimming.EllipsisCharacter
			sf.SetMeasurableCharacterRanges(New CharacterRange() {New CharacterRange(0, text.Length)})
			Dim regions As Region() = g.MeasureCharacterRanges(text, font, textLayout, sf)
			Dim lastCharBounds As Rectangle = Rectangle.Round(regions(regions.Length - 1).GetBounds(g))
			If moreWords Then
				Return New Rectangle(lastCharBounds.Right + 3, lastCharBounds.Top + (lastCharBounds.Height - arrowSize.Height) / 2, arrowSize.Width, arrowSize.Height)
			Else
				Return New Rectangle(textLayout.Left + (textLayout.Width - arrowSize.Width) / 2, lastCharBounds.Bottom + ((textLayout.Bottom - lastCharBounds.Bottom) - arrowSize.Height) / 2, arrowSize.Width, arrowSize.Height)
			End If
		End Function
		Public Sub DrawButtonDropDownArrow(g As Graphics, button As RibbonButton, textLayout As Rectangle)
			Dim bounds As Rectangle = Rectangle.Empty
			If button.SizeMode = RibbonElementSizeMode.Large OrElse button.SizeMode = RibbonElementSizeMode.Overflow Then
				bounds = LargeButtonDropDownArrowBounds(g, button.Owner.Font, button.Text, textLayout)
			Else
				bounds = textLayout
			End If
			DrawArrowShaded(g, bounds, button.DropDownArrowDirection, button.Enabled)
		End Sub
		Public Sub DrawButtonDisabled(g As Graphics, bounds As Rectangle, corners As Corners)
			Dim outerR As Rectangle = Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right - 1, bounds.Bottom - 1)
			Dim innerR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Bottom - 2)
			Dim glossyR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Top + Convert.ToInt32(CType(bounds.Height, Double) * 0.36))
			Using boundsPath As GraphicsPath = RoundRectangle(outerR, 3, corners)
				Using brus As New SolidBrush(ColorTable.ButtonDisabledBgOut)
					g.FillPath(brus, boundsPath)
				End Using
				Using path As New GraphicsPath()
					path.AddEllipse(New Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height * 2))
					path.CloseFigure()
					Using gradient As New PathGradientBrush(path)
						gradient.WrapMode = WrapMode.Clamp
						gradient.CenterPoint = New PointF(Convert.ToSingle(bounds.Left + bounds.Width / 2), Convert.ToSingle(bounds.Bottom))
						gradient.CenterColor = ColorTable.ButtonDisabledBgCenter
						gradient.SurroundColors = New Color() {ColorTable.ButtonDisabledBgOut}
						Dim blend As New Blend(3)
						blend.Factors = New Single() {0F, 0.8F, 0F}
						blend.Positions = New Single() {0F, 0.3F, 1F}
						Dim lastClip As Region = g.Clip
						Dim newClip As New Region(boundsPath)
						newClip.Intersect(lastClip)
						g.SetClip(newClip.GetBounds(g))
						g.FillPath(gradient, path)
						g.Clip = lastClip
					End Using
				End Using
				Using p As New Pen(ColorTable.ButtonDisabledBorderOut)
					g.DrawPath(p, boundsPath)
				End Using
				Using path As GraphicsPath = RoundRectangle(innerR, 3, corners)
					Using p As New Pen(ColorTable.ButtonDisabledBorderIn)
						g.DrawPath(p, path)
					End Using
				End Using
				Using path As GraphicsPath = RoundRectangle(glossyR, 3, (corners And Corners.NorthWest) Or (corners And Corners.NorthEast))
					Using b As New LinearGradientBrush(glossyR, ColorTable.ButtonDisabledGlossyNorth, ColorTable.ButtonDisabledGlossySouth, 90)
						b.WrapMode = WrapMode.TileFlipXY
						g.FillPath(b, path)
					End Using
				End Using
			End Using
		End Sub
		Public Sub DrawButtonPressed(g As Graphics, bounds As Rectangle, corners As Corners)
			Dim outerR As Rectangle = Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right - 1, bounds.Bottom - 1)
			Dim innerR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Bottom - 2)
			Dim glossyR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Top + Convert.ToInt32(CType(bounds.Height, Double) * 0.36))
			Using boundsPath As GraphicsPath = RoundRectangle(outerR, 3, corners)
				Using brus As New SolidBrush(ColorTable.ButtonPressedBgOut)
					g.FillPath(brus, boundsPath)
				End Using
				Using path As New GraphicsPath()
					path.AddEllipse(New Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height * 2))
					path.CloseFigure()
					Using gradient As New PathGradientBrush(path)
						gradient.WrapMode = WrapMode.Clamp
						gradient.CenterPoint = New PointF(Convert.ToSingle(bounds.Left + bounds.Width / 2), Convert.ToSingle(bounds.Bottom))
						gradient.CenterColor = ColorTable.ButtonPressedBgCenter
						gradient.SurroundColors = New Color() {ColorTable.ButtonPressedBgOut}
						Dim blend As New Blend(3)
						blend.Factors = New Single() {0F, 0.8F, 0F}
						blend.Positions = New Single() {0F, 0.3F, 1F}
						Dim lastClip As Region = g.Clip
						Dim newClip As New Region(boundsPath)
						newClip.Intersect(lastClip)
						g.SetClip(newClip.GetBounds(g))
						g.FillPath(gradient, path)
						g.Clip = lastClip
					End Using
				End Using
				Using p As New Pen(ColorTable.ButtonPressedBorderOut)
					g.DrawPath(p, boundsPath)
				End Using
				Using path As GraphicsPath = RoundRectangle(innerR, 3, corners)
					Using p As New Pen(ColorTable.ButtonPressedBorderIn)
						g.DrawPath(p, path)
					End Using
				End Using
				Using path As GraphicsPath = RoundRectangle(glossyR, 3, (corners And Corners.NorthWest) Or (corners And Corners.NorthEast))
					Using b As New LinearGradientBrush(glossyR, ColorTable.ButtonPressedGlossyNorth, ColorTable.ButtonPressedGlossySouth, 90)
						b.WrapMode = WrapMode.TileFlipXY
						g.FillPath(b, path)
					End Using
				End Using
			End Using
			DrawPressedShadow(g, outerR)
		End Sub
		Public Sub DrawButtonSelected(g As Graphics, bounds As Rectangle, corners As Corners)
			Dim outerR As Rectangle = Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right - 1, bounds.Bottom - 1)
			Dim innerR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Bottom - 2)
			Dim glossyR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Top + Convert.ToInt32(CType(bounds.Height, Double) * 0.36))
			Using boundsPath As GraphicsPath = RoundRectangle(outerR, 3, corners)
				Using brus As New SolidBrush(ColorTable.ButtonSelectedBgOut)
					g.FillPath(brus, boundsPath)
				End Using
				Using path As New GraphicsPath()
					path.AddEllipse(New Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height * 2))
					path.CloseFigure()
					Using gradient As New PathGradientBrush(path)
						gradient.WrapMode = WrapMode.Clamp
						gradient.CenterPoint = New PointF(Convert.ToSingle(bounds.Left + bounds.Width / 2), Convert.ToSingle(bounds.Bottom))
						gradient.CenterColor = ColorTable.ButtonSelectedBgCenter
						gradient.SurroundColors = New Color() {ColorTable.ButtonSelectedBgOut}
						Dim blend As New Blend(3)
						blend.Factors = New Single() {0F, 0.8F, 0F}
						blend.Positions = New Single() {0F, 0.3F, 1F}
						Dim lastClip As Region = g.Clip
						Dim newClip As New Region(boundsPath)
						newClip.Intersect(lastClip)
						g.SetClip(newClip.GetBounds(g))
						g.FillPath(gradient, path)
						g.Clip = lastClip
					End Using
				End Using
				Using p As New Pen(ColorTable.ButtonSelectedBorderOut)
					g.DrawPath(p, boundsPath)
				End Using
				Using path As GraphicsPath = RoundRectangle(innerR, 3, corners)
					Using p As New Pen(ColorTable.ButtonSelectedBorderIn)
						g.DrawPath(p, path)
					End Using
				End Using
				Using path As GraphicsPath = RoundRectangle(glossyR, 3, (corners And Corners.NorthWest) Or (corners And Corners.NorthEast))
					Using b As New LinearGradientBrush(glossyR, ColorTable.ButtonSelectedGlossyNorth, ColorTable.ButtonSelectedGlossySouth, 90)
						b.WrapMode = WrapMode.TileFlipXY
						g.FillPath(b, path)
					End Using
				End Using
			End Using
		End Sub
		Public Sub DrawButtonPressed(g As Graphics, button As RibbonButton)
			DrawButtonPressed(g, button.Bounds, ButtonCorners(button))
		End Sub
		Public Sub DrawButtonChecked(g As Graphics, button As RibbonButton)
			DrawButtonChecked(g, button.Bounds, ButtonCorners(button))
		End Sub
		Public Sub DrawButtonChecked(g As Graphics, bounds As Rectangle, corners As Corners)
			Dim outerR As Rectangle = Rectangle.FromLTRB(bounds.Left, bounds.Top, bounds.Right - 1, bounds.Bottom - 1)
			Dim innerR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Bottom - 2)
			Dim glossyR As Rectangle = Rectangle.FromLTRB(bounds.Left + 1, bounds.Top + 1, bounds.Right - 2, bounds.Top + Convert.ToInt32(CType(bounds.Height, Double) * 0.36))
			Using boundsPath As GraphicsPath = RoundRectangle(outerR, 3, corners)
				Using brus As New SolidBrush(ColorTable.ButtonCheckedBgOut)
					g.FillPath(brus, boundsPath)
				End Using
				Using path As New GraphicsPath()
					path.AddEllipse(New Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height * 2))
					path.CloseFigure()
					Using gradient As New PathGradientBrush(path)
						gradient.WrapMode = WrapMode.Clamp
						gradient.CenterPoint = New PointF(Convert.ToSingle(bounds.Left + bounds.Width / 2), Convert.ToSingle(bounds.Bottom))
						gradient.CenterColor = ColorTable.ButtonCheckedBgCenter
						gradient.SurroundColors = New Color() {ColorTable.ButtonCheckedBgOut}
						Dim blend As New Blend(3)
						blend.Factors = New Single() {0F, 0.8F, 0F}
						blend.Positions = New Single() {0F, 0.3F, 1F}
						Dim lastClip As Region = g.Clip
						Dim newClip As New Region(boundsPath)
						newClip.Intersect(lastClip)
						g.SetClip(newClip.GetBounds(g))
						g.FillPath(gradient, path)
						g.Clip = lastClip
					End Using
				End Using
				Using p As New Pen(ColorTable.ButtonCheckedBorderOut)
					g.DrawPath(p, boundsPath)
				End Using
				Using path As GraphicsPath = RoundRectangle(innerR, 3, corners)
					Using p As New Pen(ColorTable.ButtonCheckedBorderIn)
						g.DrawPath(p, path)
					End Using
				End Using
				Using path As GraphicsPath = RoundRectangle(glossyR, 3, (corners And Corners.NorthWest) Or (corners And Corners.NorthEast))
					Using b As New LinearGradientBrush(glossyR, ColorTable.ButtonCheckedGlossyNorth, ColorTable.ButtonCheckedGlossySouth, 90)
						b.WrapMode = WrapMode.TileFlipXY
						g.FillPath(b, path)
					End Using
				End Using
			End Using
			DrawPressedShadow(g, outerR)
		End Sub
		Public Sub DrawButtonSelected(g As Graphics, button As RibbonButton)
			DrawButtonSelected(g, button.Bounds, ButtonCorners(button))
		End Sub
		Public Sub DrawSplitButton(e As RibbonItemRenderEventArgs, button As RibbonButton)
		End Sub
		Public Sub DrawSplitButtonPressed(e As RibbonItemRenderEventArgs, button As RibbonButton)
		End Sub
		Public Sub DrawSplitButtonSelected(e As RibbonItemRenderEventArgs, button As RibbonButton)
			Dim outerR As Rectangle = Rectangle.FromLTRB(button.DropDownBounds.Left, button.DropDownBounds.Top, button.DropDownBounds.Right - 1, button.DropDownBounds.Bottom - 1)
			Dim innerR As Rectangle = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1)
			Dim faceOuterR As Rectangle = Rectangle.FromLTRB(button.ButtonFaceBounds.Left, button.ButtonFaceBounds.Top, button.ButtonFaceBounds.Right - 1, button.ButtonFaceBounds.Bottom - 1)
			Dim faceInnerR As Rectangle = Rectangle.FromLTRB(faceOuterR.Left + 1, faceOuterR.Top + 1, faceOuterR.Right + (If(button.SizeMode = RibbonElementSizeMode.Large, -1, 0)), faceOuterR.Bottom + (If(button.SizeMode = RibbonElementSizeMode.Large, 0, -1)))
			Dim faceCorners As Corners = ButtonFaceRounding(button)
			Dim ddCorners As Corners = ButtonDdRounding(button)
			Dim outer As GraphicsPath = RoundRectangle(outerR, 3, ddCorners)
			Dim inner As GraphicsPath = RoundRectangle(innerR, 2, ddCorners)
			Dim faceOuter As GraphicsPath = RoundRectangle(faceOuterR, 3, faceCorners)
			Dim faceInner As GraphicsPath = RoundRectangle(faceInnerR, 2, faceCorners)
			Using b As New SolidBrush(Color.FromArgb(150, Color.White))
				e.Graphics.FillPath(b, inner)
			End Using
			Using p As New Pen(If(button.Pressed AndAlso button.SizeMode <> RibbonElementSizeMode.DropDown, ColorTable.ButtonPressedBorderOut, ColorTable.ButtonSelectedBorderOut))
				e.Graphics.DrawPath(p, outer)
			End Using
			Using p As New Pen(If(button.Pressed AndAlso button.SizeMode <> RibbonElementSizeMode.DropDown, ColorTable.ButtonPressedBorderIn, ColorTable.ButtonSelectedBorderIn))
				e.Graphics.DrawPath(p, faceInner)
			End Using
			outer.Dispose()
			inner.Dispose()
			faceOuter.Dispose()
			faceInner.Dispose()
		End Sub
		Public Sub DrawSplitButtonDropDownPressed(e As RibbonItemRenderEventArgs, button As RibbonButton)
		End Sub
		Public Sub DrawSplitButtonDropDownSelected(e As RibbonItemRenderEventArgs, button As RibbonButton)
			Dim outerR As Rectangle = Rectangle.FromLTRB(button.DropDownBounds.Left, button.DropDownBounds.Top, button.DropDownBounds.Right - 1, button.DropDownBounds.Bottom - 1)
			Dim innerR As Rectangle = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + (If(button.SizeMode = RibbonElementSizeMode.Large, 1, 0)), outerR.Right - 1, outerR.Bottom - 1)
			Dim faceOuterR As Rectangle = Rectangle.FromLTRB(button.ButtonFaceBounds.Left, button.ButtonFaceBounds.Top, button.ButtonFaceBounds.Right - 1, button.ButtonFaceBounds.Bottom - 1)
			Dim faceInnerR As Rectangle = Rectangle.FromLTRB(faceOuterR.Left + 1, faceOuterR.Top + 1, faceOuterR.Right + (If(button.SizeMode = RibbonElementSizeMode.Large, -1, 0)), faceOuterR.Bottom + (If(button.SizeMode = RibbonElementSizeMode.Large, 0, -1)))
			Dim faceCorners As Corners = ButtonFaceRounding(button)
			Dim ddCorners As Corners = ButtonDdRounding(button)
			Dim outer As GraphicsPath = RoundRectangle(outerR, 3, ddCorners)
			Dim inner As GraphicsPath = RoundRectangle(innerR, 2, ddCorners)
			Dim faceOuter As GraphicsPath = RoundRectangle(faceOuterR, 3, faceCorners)
			Dim faceInner As GraphicsPath = RoundRectangle(faceInnerR, 2, faceCorners)
			Using b As New SolidBrush(Color.FromArgb(150, Color.White))
				e.Graphics.FillPath(b, faceInner)
			End Using
			Using p As New Pen(If(button.Pressed AndAlso button.SizeMode <> RibbonElementSizeMode.DropDown, ColorTable.ButtonPressedBorderIn, ColorTable.ButtonSelectedBorderIn))
				e.Graphics.DrawPath(p, faceInner)
			End Using
			Using p As New Pen(If(button.Pressed AndAlso button.SizeMode <> RibbonElementSizeMode.DropDown, ColorTable.ButtonPressedBorderOut, ColorTable.ButtonSelectedBorderOut))
				e.Graphics.DrawPath(p, faceOuter)
			End Using
			outer.Dispose()
			inner.Dispose()
			faceOuter.Dispose()
			faceInner.Dispose()
		End Sub
		Public Sub DrawItemGroup(e As RibbonItemRenderEventArgs, grp As RibbonItemGroup)
			Dim outerR As Rectangle = Rectangle.FromLTRB(grp.Bounds.Left, grp.Bounds.Top, grp.Bounds.Right - 1, grp.Bounds.Bottom - 1)
			Dim innerR As Rectangle = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1)
			Dim glossyR As Rectangle = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + outerR.Height / 2 + 1, outerR.Right - 1, outerR.Bottom - 1)
			Dim outer As GraphicsPath = RoundRectangle(outerR, 2)
			Dim inner As GraphicsPath = RoundRectangle(innerR, 2)
			Dim glossy As GraphicsPath = RoundRectangle(glossyR, 2)
			Using b As New LinearGradientBrush(innerR, ColorTable.ItemGroupBgNorth, ColorTable.ItemGroupBgSouth, 90)
				e.Graphics.FillPath(b, inner)
			End Using
			Using b As New LinearGradientBrush(glossyR, ColorTable.ItemGroupBgGlossy, Color.Transparent, 90)
				e.Graphics.FillPath(b, glossy)
			End Using
			outer.Dispose()
			inner.Dispose()
		End Sub
		Public Sub DrawItemGroupBorder(e As RibbonItemRenderEventArgs, grp As RibbonItemGroup)
			Dim outerR As Rectangle = Rectangle.FromLTRB(grp.Bounds.Left, grp.Bounds.Top, grp.Bounds.Right - 1, grp.Bounds.Bottom - 1)
			Dim innerR As Rectangle = Rectangle.FromLTRB(outerR.Left + 1, outerR.Top + 1, outerR.Right - 1, outerR.Bottom - 1)
			Dim outer As GraphicsPath = RoundRectangle(outerR, 2)
			Dim inner As GraphicsPath = RoundRectangle(innerR, 2)
			Using dark As New Pen(ColorTable.ItemGroupSeparatorDark)
				Using light As New Pen(ColorTable.ItemGroupSeparatorLight)
					For Each item As RibbonItem In grp.Items
						If item = grp.LastItem Then
							Exit For
						End If
						e.Graphics.DrawLine(dark, New Point(item.Bounds.Right, item.Bounds.Top), New Point(item.Bounds.Right, item.Bounds.Bottom))
						e.Graphics.DrawLine(light, New Point(item.Bounds.Right + 1, item.Bounds.Top), New Point(item.Bounds.Right + 1, item.Bounds.Bottom))
					Next
				End Using
			End Using
			Using p As New Pen(ColorTable.ItemGroupOuterBorder)
				e.Graphics.DrawPath(p, outer)
			End Using
			Using p As New Pen(ColorTable.ItemGroupInnerBorder)
				e.Graphics.DrawPath(p, inner)
			End Using
			outer.Dispose()
			inner.Dispose()
		End Sub
		Public Sub DrawButtonList(g As Graphics, list As RibbonButtonList)
			Dim outerR As Rectangle = Rectangle.FromLTRB(list.Bounds.Left, list.Bounds.Top, list.Bounds.Right - 1, list.Bounds.Bottom)
			Using path As GraphicsPath = RoundRectangle(outerR, 3, Corners.East)
				Dim bgcolor As Color = If(list.Selected, ColorTable.ButtonListBgSelected, ColorTable.ButtonListBg)
				If TypeOf list.Canvas Is RibbonDropDown Then
					bgcolor = ColorTable.DropDownBg
				End If
				Using b As New SolidBrush(bgcolor)
					g.FillPath(b, path)
				End Using
				Using p As New Pen(ColorTable.ButtonListBorder)
					g.DrawPath(p, path)
				End Using
			End Using
			If list.ScrollType = RibbonButtonList.ListScrollType.Scrollbar AndAlso ScrollBarRenderer.IsSupported Then
				ScrollBarRenderer.DrawUpperVerticalTrack(g, list.ScrollBarBounds, ScrollBarState.Normal)
				If list.ThumbPressed Then
					ScrollBarRenderer.DrawVerticalThumb(g, list.ThumbBounds, ScrollBarState.Pressed)
					ScrollBarRenderer.DrawVerticalThumbGrip(g, list.ThumbBounds, ScrollBarState.Pressed)
				ElseIf list.ThumbSelected Then
					ScrollBarRenderer.DrawVerticalThumb(g, list.ThumbBounds, ScrollBarState.Hot)
					ScrollBarRenderer.DrawVerticalThumbGrip(g, list.ThumbBounds, ScrollBarState.Hot)
				Else
					ScrollBarRenderer.DrawVerticalThumb(g, list.ThumbBounds, ScrollBarState.Normal)
					ScrollBarRenderer.DrawVerticalThumbGrip(g, list.ThumbBounds, ScrollBarState.Normal)
				End If
				If list.ButtonUpPressed Then
					ScrollBarRenderer.DrawArrowButton(g, list.ButtonUpBounds, ScrollBarArrowButtonState.UpPressed)
				ElseIf list.ButtonUpSelected Then
					ScrollBarRenderer.DrawArrowButton(g, list.ButtonUpBounds, ScrollBarArrowButtonState.UpHot)
				Else
					ScrollBarRenderer.DrawArrowButton(g, list.ButtonUpBounds, ScrollBarArrowButtonState.UpNormal)
				End If
				If list.ButtonDownPressed Then
					ScrollBarRenderer.DrawArrowButton(g, list.ButtonDownBounds, ScrollBarArrowButtonState.DownPressed)
				ElseIf list.ButtonDownSelected Then
					ScrollBarRenderer.DrawArrowButton(g, list.ButtonDownBounds, ScrollBarArrowButtonState.DownHot)
				Else
					ScrollBarRenderer.DrawArrowButton(g, list.ButtonDownBounds, ScrollBarArrowButtonState.DownNormal)
				End If
			Else
				If list.ScrollType = RibbonButtonList.ListScrollType.Scrollbar Then
					Using b As New SolidBrush(ColorTable.ButtonGlossyNorth)
						g.FillRectangle(b, list.ScrollBarBounds)
					End Using
				End If
				If Not list.ButtonDownEnabled Then
					DrawButtonDisabled(g, list.ButtonDownBounds, If(list.ButtonDropDownPresent, Corners.None, Corners.SouthEast))
				ElseIf list.ButtonDownPressed Then
					DrawButtonPressed(g, list.ButtonDownBounds, If(list.ButtonDropDownPresent, Corners.None, Corners.SouthEast))
				ElseIf list.ButtonDownSelected Then
					DrawButtonSelected(g, list.ButtonDownBounds, If(list.ButtonDropDownPresent, Corners.None, Corners.SouthEast))
				Else
					DrawButton(g, list.ButtonDownBounds, Corners.None)
				End If
				If Not list.ButtonUpEnabled Then
					DrawButtonDisabled(g, list.ButtonUpBounds, Corners.NorthEast)
				ElseIf list.ButtonUpPressed Then
					DrawButtonPressed(g, list.ButtonUpBounds, Corners.NorthEast)
				ElseIf list.ButtonUpSelected Then
					DrawButtonSelected(g, list.ButtonUpBounds, Corners.NorthEast)
				Else
					DrawButton(g, list.ButtonUpBounds, Corners.NorthEast)
				End If
				If list.ButtonDropDownPresent Then
					If list.ButtonDropDownPressed Then
						DrawButtonPressed(g, list.ButtonDropDownBounds, Corners.SouthEast)
					ElseIf list.ButtonDropDownSelected Then
						DrawButtonSelected(g, list.ButtonDropDownBounds, Corners.SouthEast)
					Else
						DrawButton(g, list.ButtonDropDownBounds, Corners.SouthEast)
					End If
				End If
				If list.ScrollType = RibbonButtonList.ListScrollType.Scrollbar AndAlso list.ScrollBarEnabled Then
					If list.ThumbPressed Then
						DrawButtonPressed(g, list.ThumbBounds, Corners.All)
					ElseIf list.ThumbSelected Then
						DrawButtonSelected(g, list.ThumbBounds, Corners.All)
					Else
						DrawButton(g, list.ThumbBounds, Corners.All)
					End If
				End If
				Dim dk As Color = ColorTable.Arrow
				Dim lt As Color = ColorTable.ArrowLight
				Dim ds As Color = ColorTable.ArrowDisabled
				Dim arrUp As Rectangle = CenterOn(list.ButtonUpBounds, New Rectangle(Point.Empty, arrowSize))
				arrUp.Offset(0, 1)
				Dim arrD As Rectangle = CenterOn(list.ButtonDownBounds, New Rectangle(Point.Empty, arrowSize))
				arrD.Offset(0, 1)
				Dim arrdd As Rectangle = CenterOn(list.ButtonDropDownBounds, New Rectangle(Point.Empty, arrowSize))
				arrdd.Offset(0, 3)
				DrawArrow(g, arrUp, If(list.ButtonUpEnabled, lt, Color.Transparent), RibbonArrowDirection.Up)
				arrUp.Offset(0, -1)
				DrawArrow(g, arrUp, If(list.ButtonUpEnabled, dk, ds), RibbonArrowDirection.Up)
				DrawArrow(g, arrD, If(list.ButtonDownEnabled, lt, Color.Transparent), RibbonArrowDirection.Down)
				arrD.Offset(0, -1)
				DrawArrow(g, arrD, If(list.ButtonDownEnabled, dk, ds), RibbonArrowDirection.Down)
				If list.ButtonDropDownPresent Then
					Using b As New SolidBrush(ColorTable.Arrow)
						Dim sm As SmoothingMode = g.SmoothingMode
						g.SmoothingMode = SmoothingMode.None
						g.FillRectangle(b, New Rectangle(New Point(arrdd.Left - 1, arrdd.Top - 4), New Size(arrowSize.Width + 2, 1)))
						g.SmoothingMode = sm
					End Using
					DrawArrow(g, arrdd, lt, RibbonArrowDirection.Down)
					arrdd.Offset(0, -1)
					DrawArrow(g, arrdd, dk, RibbonArrowDirection.Down)
				End If
			End If
		End Sub
		Public Sub DrawSeparator(g As Graphics, separator As RibbonSeparator)
			If separator.SizeMode = RibbonElementSizeMode.DropDown Then
				If Not String.IsNullOrEmpty(separator.Text) Then
					Using b As New SolidBrush(ColorTable.SeparatorBg)
						g.FillRectangle(b, separator.Bounds)
					End Using
					Using p As New Pen(ColorTable.SeparatorLine)
						g.DrawLine(p, New Point(separator.Bounds.Left, separator.Bounds.Bottom), New Point(separator.Bounds.Right, separator.Bounds.Bottom))
					End Using
				Else
					Using p As New Pen(ColorTable.DropDownImageSeparator)
						g.DrawLine(p, New Point(separator.Bounds.Left + 30, separator.Bounds.Top + 1), New Point(separator.Bounds.Right, separator.Bounds.Top + 1))
					End Using
				End If
			Else
				Using p As New Pen(ColorTable.SeparatorDark)
					g.DrawLine(p, New Point(separator.Bounds.Left, separator.Bounds.Top), New Point(separator.Bounds.Left, separator.Bounds.Bottom))
				End Using
				Using p As New Pen(ColorTable.SeparatorLight)
					g.DrawLine(p, New Point(separator.Bounds.Left + 1, separator.Bounds.Top), New Point(separator.Bounds.Left + 1, separator.Bounds.Bottom))
				End Using
			End If
		End Sub
		Public Sub DrawTextBoxDisabled(g As Graphics, bounds As Rectangle)
			Using b As New SolidBrush(SystemColors.Control)
				g.FillRectangle(b, bounds)
			End Using
			Using p As New Pen(ColorTable.TextBoxBorder)
				g.DrawRectangle(p, bounds)
			End Using
		End Sub
		Public Sub DrawTextBoxUnselected(g As Graphics, bounds As Rectangle)
			Using b As New SolidBrush(ColorTable.TextBoxUnselectedBg)
				g.FillRectangle(b, bounds)
			End Using
			Using p As New Pen(ColorTable.TextBoxBorder)
				g.DrawRectangle(p, bounds)
			End Using
		End Sub
		Public Sub DrawTextBoxSelected(g As Graphics, bounds As Rectangle)
			Using path As GraphicsPath = RoundRectangle(bounds, 3)
				Using b As New SolidBrush(SystemColors.Window)
					g.FillRectangle(b, bounds)
				End Using
				Using p As New Pen(ColorTable.TextBoxBorder)
					g.DrawRectangle(p, bounds)
				End Using
			End Using
		End Sub
		Public Sub DrawComboxDropDown(g As Graphics, b As RibbonComboBox)
			If b.DropDownButtonPressed Then
				DrawButtonPressed(g, b.DropDownButtonBounds, Corners.None)
			ElseIf b.DropDownButtonSelected Then
				DrawButtonSelected(g, b.DropDownButtonBounds, Corners.None)
			ElseIf b.Selected Then
				DrawButton(g, b.DropDownButtonBounds, Corners.None)
			End If
			DrawArrowShaded(g, b.DropDownButtonBounds, RibbonArrowDirection.Down, True)
		End Sub
		Public Sub DrawCaptionBarBackground(r As Rectangle, g As Graphics)
			Dim smbuff As SmoothingMode = g.SmoothingMode
			Dim r1 As New Rectangle(r.Left, r.Top, r.Width, 4)
			Dim r2 As New Rectangle(r.Left, r1.Bottom, r.Width, 4)
			Dim r3 As New Rectangle(r.Left, r2.Bottom, r.Width, r.Height - 8)
			Dim r4 As New Rectangle(r.Left, r3.Bottom, r.Width, 1)
			Dim rects As Rectangle() = New Rectangle() {r1, r2, r3, r4}
			Dim colors As Color(,) = New Color(,) {{ColorTable.Caption1, ColorTable.Caption2}, {ColorTable.Caption3, ColorTable.Caption4}, {ColorTable.Caption5, ColorTable.Caption6}, {ColorTable.Caption7, ColorTable.Caption7}}
			g.SmoothingMode = SmoothingMode.None
			Dim i As Integer = 0
			While i < rects.Length
				Dim grect As Rectangle = rects(i)
				grect.Height += 2
				grect.Y -= 1
				Using b As New LinearGradientBrush(grect, colors(i, 0), colors(i, 1), 90)
					g.FillRectangle(b, rects(i))
				End Using
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While
			g.SmoothingMode = smbuff
		End Sub
		Public Overrides Sub OnRenderRibbonCaptionBar(e As RibbonRenderEventArgs)
			Dim captionBar As New Rectangle(0, 0, e.Ribbon.Width, e.Ribbon.CaptionBarSize)
			If Not (e.Ribbon.ActualBorderMode = RibbonWindowMode.NonClientAreaGlass AndAlso RibbonDesigner.Current = Nothing) Then
				DrawCaptionBarBackground(captionBar, e.Graphics)
			End If
			DrawCaptionBarText(e.Ribbon.CaptionTextBounds, e)
		End Sub
		Private Sub DrawCaptionBarText(captionBar As Rectangle, e As RibbonRenderEventArgs)
			Dim f As Form = e.Ribbon.FindForm()
			If f Is Nothing Then
				Return
			End If
			Dim sf As New StringFormat()
			sf.LineAlignment = sf.Alignment = StringAlignment.Center
			sf.Trimming = StringTrimming.EllipsisCharacter
			sf.FormatFlags = sf.FormatFlags Or StringFormatFlags.NoWrap
			Dim ft As New Font(SystemFonts.CaptionFont, FontStyle.Regular)
			If e.Ribbon.ActualBorderMode = RibbonWindowMode.NonClientAreaGlass Then
				Using path As New GraphicsPath()
					path.AddString(f.Text, ft.FontFamily, CType(ft.Style, Integer), ft.SizeInPoints + 3, captionBar, sf)
					If f.WindowState <> FormWindowState.Maximized Then
						Using p As New Pen(Color.FromArgb(90, Color.White), 4)
							e.Graphics.DrawPath(p, path)
						End Using
					End If
					e.Graphics.FillPath(If(f.WindowState = FormWindowState.Maximized, Brushes.White, Brushes.Black), path)
				End Using
			ElseIf e.Ribbon.ActualBorderMode = RibbonWindowMode.NonClientAreaCustomDrawn Then
				TextRenderer.DrawText(e.Graphics, f.Text, ft, captionBar, ColorTable.FormBorder)
			End If
		End Sub
		Public Overrides Sub OnRenderRibbonOrb(e As RibbonRenderEventArgs)
			If e.Ribbon.OrbVisible Then
				DrawOrb(e.Graphics, e.Ribbon.OrbBounds, e.Ribbon.OrbImage, e.Ribbon.OrbSelected, e.Ribbon.OrbPressed)
			End If
		End Sub
		Public Overrides Sub OnRenderRibbonQuickAccessToolbarBackground(e As RibbonRenderEventArgs)
			Dim bounds As Rectangle = e.Ribbon.QuickAcessToolbar.Bounds
			Dim padding As Padding = e.Ribbon.QuickAcessToolbar.Padding
			Dim margin As Padding = e.Ribbon.QuickAcessToolbar.Margin
			Dim a As New Point(bounds.Left - (If(e.Ribbon.OrbVisible, margin.Left, 0)), bounds.Top)
			Dim b As New Point(bounds.Right + padding.Right, bounds.Top)
			Dim c As New Point(bounds.Left, bounds.Bottom)
			Dim d As New Point(b.X, c.Y)
			Dim z As New Point(c.X - 2, a.Y + bounds.Height / 2 - 1)
			Dim aero As Boolean = e.Ribbon.ActualBorderMode = RibbonWindowMode.NonClientAreaGlass AndAlso RibbonDesigner.Current = Nothing
			If Not aero Then
				Using p As New Pen(ColorTable.QuickAccessBorderLight, 3)
					Using path As GraphicsPath = CreateQuickAccessPath(a, b, c, d, z, bounds, _
						0, 0, e.Ribbon)
						e.Graphics.DrawPath(p, path)
					End Using
				End Using
			End If
			Using path As GraphicsPath = CreateQuickAccessPath(a, b, c, d, z, bounds, _
				0, 0, e.Ribbon)
				Using p As New Pen(ColorTable.QuickAccessBorderDark)
					If aero Then
						p.Color = Color.FromArgb(150, 150, 150)
					End If
					e.Graphics.DrawPath(p, path)
				End Using
				If Not aero Then
					Using br As New LinearGradientBrush(b, d, Color.FromArgb(150, ColorTable.QuickAccessUpper), Color.FromArgb(150, ColorTable.QuickAccessLower))
						e.Graphics.FillPath(br, path)
					End Using
				Else
					Using br As New LinearGradientBrush(b, d, Color.FromArgb(66, RibbonProfesionalRendererColorTable.ToGray(ColorTable.QuickAccessUpper)), Color.FromArgb(66, RibbonProfesionalRendererColorTable.ToGray(ColorTable.QuickAccessLower)))
						e.Graphics.FillPath(br, path)
					End Using
				End If
			End Using
		End Sub
		Private Function CreateQuickAccessPath(a As Point, b As Point, c As Point, d As Point, e As Point, bounds As Rectangle, _
			offsetx As Integer, offsety As Integer, ribbon As Ribbon) As GraphicsPath
			a.Offset(offsetx, offsety)
			b.Offset(offsetx, offsety)
			c.Offset(offsetx, offsety)
			d.Offset(offsetx, offsety)
			e.Offset(offsetx, offsety)
			Dim path As New GraphicsPath()
			path.AddLine(a, b)
			path.AddArc(New Rectangle(b.X - bounds.Height / 2, b.Y, bounds.Height, bounds.Height), -90, 180)
			path.AddLine(d, c)
			If ribbon.OrbVisible Then
				path.AddCurve(New Point() {c, e, a})
			Else
				path.AddArc(New Rectangle(a.X - bounds.Height / 2, a.Y, bounds.Height, bounds.Height), 90, 180)
			End If
			Return path
		End Function
		Public Sub DrawOrb(g As Graphics, r As Rectangle, image As Image, selected As Boolean, pressed As Boolean)
			Dim sweep As Integer, start As Integer
			Dim p1 As Point, p2 As Point, p3 As Point
			Dim bgdark As Color, bgmed As Color, bglight As Color, light As Color
			Dim rinner As Rectangle = r
			rinner.Inflate(-1, -1)
			Dim shadow As Rectangle = r
			shadow.Offset(1, 1)
			shadow.Inflate(2, 2)
			If pressed Then
				bgdark = ColorTable.OrbPressedBackgroundDark
				bgmed = ColorTable.OrbPressedBackgroundMedium
				bglight = ColorTable.OrbPressedBackgroundLight
				light = ColorTable.OrbPressedLight
			ElseIf selected Then
				bgdark = ColorTable.OrbSelectedBackgroundDark
				bgmed = ColorTable.OrbSelectedBackgroundDark
				bglight = ColorTable.OrbSelectedBackgroundLight
				light = ColorTable.OrbSelectedLight
			Else
				bgdark = ColorTable.OrbBackgroundDark
				bgmed = ColorTable.OrbBackgroundMedium
				bglight = ColorTable.OrbBackgroundLight
				light = ColorTable.OrbLight
			End If
			Using p As New GraphicsPath()
				p.AddEllipse(shadow)
				Using gradient As New PathGradientBrush(p)
					gradient.WrapMode = WrapMode.Clamp
					gradient.CenterPoint = New PointF(shadow.Left + shadow.Width / 2, shadow.Top + shadow.Height / 2)
					gradient.CenterColor = Color.FromArgb(180, Color.Black)
					gradient.SurroundColors = New Color() {Color.Transparent}
					Dim blend As New Blend(3)
					blend.Factors = New Single() {0F, 1F, 1F}
					blend.Positions = New Single() {0, 0.2F, 1F}
					gradient.Blend = blend
					g.FillPath(gradient, p)
				End Using
			End Using
			Using p As New Pen(bgdark, 1)
				g.DrawEllipse(p, r)
			End Using
			Using p As New GraphicsPath()
				p.AddEllipse(r)
				Using gradient As New PathGradientBrush(p)
					gradient.WrapMode = WrapMode.Clamp
					gradient.CenterPoint = New PointF(Convert.ToSingle(r.Left + r.Width / 2), Convert.ToSingle(r.Bottom))
					gradient.CenterColor = bglight
					gradient.SurroundColors = New Color() {bgmed}
					Dim blend As New Blend(3)
					blend.Factors = New Single() {0F, 0.8F, 1F}
					blend.Positions = New Single() {0, 0.5F, 1F}
					gradient.Blend = blend
					g.FillPath(gradient, p)
				End Using
			End Using
			Dim bshine As New Rectangle(0, 0, r.Width / 2, r.Height / 2)
			bshine.X = r.X + (r.Width - bshine.Width) / 2
			bshine.Y = r.Y + r.Height / 2
			Using p As New GraphicsPath()
				p.AddEllipse(bshine)
				Using gradient As New PathGradientBrush(p)
					gradient.WrapMode = WrapMode.Clamp
					gradient.CenterPoint = New PointF(Convert.ToSingle(r.Left + r.Width / 2), Convert.ToSingle(r.Bottom))
					gradient.CenterColor = Color.White
					gradient.SurroundColors = New Color() {Color.Transparent}
					g.FillPath(gradient, p)
				End Using
			End Using
			Using p As New GraphicsPath()
				sweep = 160
				start = 180 + (180 - sweep) / 2
				p.AddArc(rinner, start, sweep)
				p1 = Point.Round(p.PathData.Points(0))
				p2 = Point.Round(p.PathData.Points(p.PathData.Points.Length - 1))
				p3 = New Point(rinner.Left + rinner.Width / 2, p2.Y - 3)
				p.AddCurve(New Point() {p2, p3, p1})
				Using gradient As New PathGradientBrush(p)
					gradient.WrapMode = WrapMode.Clamp
					gradient.CenterPoint = p3
					gradient.CenterColor = Color.Transparent
					gradient.SurroundColors = New Color() {light}
					Dim blend As New Blend(3)
					blend.Factors = New Single() {0.3F, 0.8F, 1F}
					blend.Positions = New Single() {0, 0.5F, 1F}
					gradient.Blend = blend
					g.FillPath(gradient, p)
				End Using
				Using b As New LinearGradientBrush(New Point(r.Left, r.Top), New Point(r.Left, p1.Y), Color.White, Color.Transparent)
					Dim blend As New Blend(4)
					blend.Factors = New Single() {0F, 0.4F, 0.8F, 1F}
					blend.Positions = New Single() {0F, 0.3F, 0.4F, 1F}
					b.Blend = blend
					g.FillPath(b, p)
				End Using
			End Using
			Using p As New GraphicsPath()
				sweep = 160
				start = 180 + (180 - sweep) / 2
				p.AddArc(rinner, start, sweep)
				Using pen As New Pen(Color.White)
					g.DrawPath(pen, p)
				End Using
			End Using
			Using p As New GraphicsPath()
				sweep = 160
				start = (180 - sweep) / 2
				p.AddArc(rinner, start, sweep)
				Dim pt As Point = Point.Round(p.PathData.Points(0))
				Dim rrinner As Rectangle = rinner
				rrinner.Inflate(-1, -1)
				sweep = 160
				start = (180 - sweep) / 2
				p.AddArc(rrinner, start, sweep)
				Using b As New LinearGradientBrush(New Point(rinner.Left, rinner.Bottom), New Point(rinner.Left, pt.Y - 1), light, Color.FromArgb(50, light))
					g.FillPath(b, p)
				End Using
			End Using
			If image IsNot Nothing Then
				Dim irect As New Rectangle(Point.Empty, image.Size)
				irect.X = r.X + (r.Width - irect.Width) / 2
				irect.Y = r.Y + (r.Height - irect.Height) / 2
				g.DrawImage(image, irect)
			End If
		End Sub
		Public Overrides Sub OnRenderOrbDropDownBackground(e As RibbonOrbDropDownEventArgs)
			Dim Width As Integer = e.RibbonOrbDropDown.Width
			Dim Height As Integer = e.RibbonOrbDropDown.Height
			Dim OrbDDContent As Rectangle = e.RibbonOrbDropDown.ContentBounds
			Dim Bcontent As Rectangle = e.RibbonOrbDropDown.ContentButtonsBounds
			Dim OuterRect As New Rectangle(0, 0, Width - 1, Height - 1)
			Dim InnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
			Dim NorthNorthRect As New Rectangle(1, 1, Width - 3, OrbDDContent.Top / 2)
			Dim northSouthRect As New Rectangle(1, NorthNorthRect.Bottom, NorthNorthRect.Width, OrbDDContent.Top / 2)
			Dim southSouthRect As Rectangle = Rectangle.FromLTRB(1, (Height - OrbDDContent.Bottom) / 2 + OrbDDContent.Bottom, Width - 1, Height - 1)
			Dim OrbDropDownDarkBorder As Color = ColorTable.OrbDropDownDarkBorder
			Dim OrbDropDownLightBorder As Color = ColorTable.OrbDropDownLightBorder
			Dim OrbDropDownBack As Color = ColorTable.OrbDropDownBack
			Dim OrbDropDownNorthA As Color = ColorTable.OrbDropDownNorthA
			Dim OrbDropDownNorthB As Color = ColorTable.OrbDropDownNorthB
			Dim OrbDropDownNorthC As Color = ColorTable.OrbDropDownNorthC
			Dim OrbDropDownNorthD As Color = ColorTable.OrbDropDownNorthD
			Dim OrbDropDownSouthC As Color = ColorTable.OrbDropDownSouthC
			Dim OrbDropDownSouthD As Color = ColorTable.OrbDropDownSouthD
			Dim OrbDropDownContentbg As Color = ColorTable.OrbDropDownContentbg
			Dim OrbDropDownContentbglight As Color = ColorTable.OrbDropDownContentbglight
			Dim OrbDropDownSeparatorlight As Color = ColorTable.OrbDropDownSeparatorlight
			Dim OrbDropDownSeparatordark As Color = ColorTable.OrbDropDownSeparatordark
			Dim innerPath As GraphicsPath = RoundRectangle(InnerRect, 6)
			Dim outerPath As GraphicsPath = RoundRectangle(OuterRect, 6)
			e.Graphics.SmoothingMode = SmoothingMode.None
			Using b As Brush = New SolidBrush(Color.FromArgb(&H8e, &H8e, &H8e))
				e.Graphics.FillRectangle(b, New Rectangle(Width - 10, Height - 10, 10, 10))
			End Using
			Using b As Brush = New SolidBrush(OrbDropDownBack)
				e.Graphics.FillPath(b, outerPath)
			End Using
			GradientRect(e.Graphics, NorthNorthRect, OrbDropDownNorthA, OrbDropDownNorthB)
			GradientRect(e.Graphics, northSouthRect, OrbDropDownNorthC, OrbDropDownNorthD)
			GradientRect(e.Graphics, southSouthRect, OrbDropDownSouthC, OrbDropDownSouthD)
			Using p As New Pen(OrbDropDownDarkBorder)
				e.Graphics.DrawPath(p, outerPath)
			End Using
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
			Using p As New Pen(OrbDropDownLightBorder)
				e.Graphics.DrawPath(p, innerPath)
			End Using
			innerPath.Dispose()
			outerPath.Dispose()
			InnerRect = OrbDDContent
			InnerRect.Inflate(0, 0)
			OuterRect = OrbDDContent
			OuterRect.Inflate(1, 1)
			Using b As New SolidBrush(OrbDropDownContentbg)
				e.Graphics.FillRectangle(b, OrbDDContent)
			End Using
			Using b As New SolidBrush(OrbDropDownContentbglight)
				e.Graphics.FillRectangle(b, Bcontent)
			End Using
			Using p As New Pen(OrbDropDownSeparatorlight)
				e.Graphics.DrawLine(p, Bcontent.Right, Bcontent.Top, Bcontent.Right, Bcontent.Bottom)
			End Using
			Using p As New Pen(OrbDropDownSeparatordark)
				e.Graphics.DrawLine(p, Bcontent.Right - 1, Bcontent.Top, Bcontent.Right - 1, Bcontent.Bottom)
			End Using
			Using p As New Pen(OrbDropDownLightBorder)
				e.Graphics.DrawRectangle(p, OuterRect)
			End Using
			Using p As New Pen(OrbDropDownDarkBorder)
				e.Graphics.DrawRectangle(p, InnerRect)
			End Using
			Dim orbb As Rectangle = e.Ribbon.RectangleToScreen(e.Ribbon.OrbBounds)
			orbb = e.RibbonOrbDropDown.RectangleToClient(orbb)
			DrawOrb(e.Graphics, orbb, e.Ribbon.OrbImage, e.Ribbon.OrbSelected, e.Ribbon.OrbPressed)
		End Sub
		Public Overrides Sub OnRenderRibbonBackground(e As RibbonRenderEventArgs)
			e.Graphics.Clear(ColorTable.RibbonBackground)
			If e.Ribbon.ActualBorderMode = RibbonWindowMode.NonClientAreaGlass Then
				WinApi.FillForGlass(e.Graphics, New Rectangle(0, 0, e.Ribbon.Width, e.Ribbon.CaptionBarSize + 1))
			End If
		End Sub
		Public Overrides Sub OnRenderRibbonTab(e As RibbonTabRenderEventArgs)
			If e.Tab.Active Then
				DrawCompleteTab(e)
			ElseIf e.Tab.Pressed Then
				DrawTabPressed(e)
			ElseIf e.Tab.Selected Then
				DrawTabSelected(e)
			Else
				DrawTabNormal(e)
			End If
		End Sub
		Public Overrides Sub OnRenderRibbonTabText(e As RibbonTabRenderEventArgs)
			Dim sf As New StringFormat()
			sf.Alignment = StringAlignment.Center
			sf.Trimming = StringTrimming.EllipsisCharacter
			sf.LineAlignment = StringAlignment.Center
			sf.FormatFlags = sf.FormatFlags Or StringFormatFlags.NoWrap
			Dim r As Rectangle = Rectangle.FromLTRB(e.Tab.TabBounds.Left + e.Ribbon.TabTextMargin.Left, e.Tab.TabBounds.Top + e.Ribbon.TabTextMargin.Top, e.Tab.TabBounds.Right - e.Ribbon.TabTextMargin.Right, e.Tab.TabBounds.Bottom - e.Ribbon.TabTextMargin.Bottom)
			Using b As Brush = New SolidBrush(GetTextColor(True, If(e.Tab.Active, ColorTable.TabActiveText, ColorTable.TabText)))
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
				e.Graphics.DrawString(e.Tab.Text, e.Ribbon.Font, b, r, sf)
			End Using
		End Sub
		Public Overrides Sub OnRenderRibbonPanelBackground(e As RibbonPanelRenderEventArgs)
			If e.Panel.OverflowMode AndAlso Not (TypeOf e.Canvas Is RibbonPanelPopup) Then
				If e.Panel.Pressed Then
					DrawPanelOverflowPressed(e)
				ElseIf e.Panel.Selected Then
					DrawPannelOveflowSelected(e)
				Else
					DrawPanelOverflowNormal(e)
				End If
			Else
				If e.Panel.Selected Then
					DrawPanelSelected(e)
				Else
					DrawPanelNormal(e)
				End If
			End If
		End Sub
		Public Overrides Sub OnRenderRibbonPanelText(e As RibbonPanelRenderEventArgs)
			If e.Panel.OverflowMode AndAlso Not (TypeOf e.Canvas Is RibbonPanelPopup) Then
				Return
			End If
			Dim textArea As Rectangle = Rectangle.FromLTRB(e.Panel.Bounds.Left + 1, e.Panel.ContentBounds.Bottom, e.Panel.Bounds.Right - 1, e.Panel.Bounds.Bottom - 1)
			Dim sf As New StringFormat()
			sf.Alignment = StringAlignment.Center
			sf.LineAlignment = StringAlignment.Center
			Using b As Brush = New SolidBrush(GetTextColor(e.Panel.Enabled, ColorTable.PanelText))
				e.Graphics.DrawString(e.Panel.Text, e.Ribbon.Font, b, textArea, sf)
			End Using
		End Sub
		Public Overrides Sub OnRenderRibbonItem(e As RibbonItemRenderEventArgs)
			If TypeOf e.Item Is RibbonButton Then
				Dim b As RibbonButton = TryCast(e.Item, RibbonButton)
				If b.Enabled Then
					If b.Style = RibbonButtonStyle.Normal Then
						If b.Pressed AndAlso b.SizeMode <> RibbonElementSizeMode.DropDown Then
							DrawButtonPressed(e.Graphics, b)
						ElseIf b.Selected Then
							If b.Checked Then
								DrawButtonPressed(e.Graphics, b)
							Else
								DrawButtonSelected(e.Graphics, b)
							End If
						ElseIf b.Checked Then
							DrawButtonChecked(e.Graphics, b)
						ElseIf TypeOf b Is RibbonOrbOptionButton Then
							DrawOrbOptionButton(e.Graphics, b.Bounds)
						Else
						End If
					Else
						If b.DropDownPressed AndAlso b.SizeMode <> RibbonElementSizeMode.DropDown Then
							DrawButtonPressed(e.Graphics, b)
							DrawSplitButtonDropDownSelected(e, b)
						ElseIf b.Pressed AndAlso b.SizeMode <> RibbonElementSizeMode.DropDown Then
							DrawButtonPressed(e.Graphics, b)
							DrawSplitButtonSelected(e, b)
						ElseIf b.DropDownSelected Then
							DrawButtonSelected(e.Graphics, b)
							DrawSplitButtonDropDownSelected(e, b)
						ElseIf b.Selected Then
							DrawButtonSelected(e.Graphics, b)
							DrawSplitButtonSelected(e, b)
						ElseIf b.Checked Then
							DrawButtonChecked(e.Graphics, b)
						Else
							DrawSplitButton(e, b)
						End If
					End If
				End If
				If b.Style <> RibbonButtonStyle.Normal AndAlso Not (b.Style = RibbonButtonStyle.DropDown AndAlso b.SizeMode = RibbonElementSizeMode.Large) Then
					If b.Style = RibbonButtonStyle.DropDown Then
						DrawButtonDropDownArrow(e.Graphics, b, b.OnGetDropDownBounds(b.SizeMode, b.Bounds))
					Else
						DrawButtonDropDownArrow(e.Graphics, b, b.DropDownBounds)
					End If
				End If
			ElseIf TypeOf e.Item Is RibbonItemGroup Then
				DrawItemGroup(e, TryCast(e.Item, RibbonItemGroup))
			ElseIf TypeOf e.Item Is RibbonButtonList Then
				DrawButtonList(e.Graphics, TryCast(e.Item, RibbonButtonList))
			ElseIf TypeOf e.Item Is RibbonSeparator Then
				DrawSeparator(e.Graphics, TryCast(e.Item, RibbonSeparator))
			ElseIf TypeOf e.Item Is RibbonTextBox Then
				Dim t As RibbonTextBox = TryCast(e.Item, RibbonTextBox)
				If t.Enabled Then
					If t IsNot Nothing AndAlso (t.Selected OrElse (t.Editing)) Then
						DrawTextBoxSelected(e.Graphics, t.TextBoxBounds)
					Else
						DrawTextBoxUnselected(e.Graphics, t.TextBoxBounds)
					End If
				Else
					DrawTextBoxDisabled(e.Graphics, t.TextBoxBounds)
				End If
				If TypeOf t Is RibbonComboBox Then
					DrawComboxDropDown(e.Graphics, TryCast(t, RibbonComboBox))
				End If
			End If
		End Sub
		Public Overrides Sub OnRenderRibbonItemBorder(e As RibbonItemRenderEventArgs)
			If TypeOf e.Item Is RibbonItemGroup Then
				DrawItemGroupBorder(e, TryCast(e.Item, RibbonItemGroup))
			End If
		End Sub
		Public Overrides Sub OnRenderRibbonItemText(e As RibbonTextEventArgs)
			Dim foreColor As Color = e.Color
			Dim sf As StringFormat = e.Format
			Dim f As Font = e.Ribbon.Font
			Dim embedded As Boolean = False
			If TypeOf e.Item Is RibbonButton Then
				Dim b As RibbonButton = TryCast(e.Item, RibbonButton)
				If TypeOf b Is RibbonCaptionButton Then
					If WinApi.IsWindows Then
						f = New Font("Marlett", f.Size)
					End If
					embedded = True
					foreColor = ColorTable.Arrow
				End If
				If b.Style = RibbonButtonStyle.DropDown AndAlso b.SizeMode = RibbonElementSizeMode.Large Then
					DrawButtonDropDownArrow(e.Graphics, b, e.Bounds)
				End If
			ElseIf TypeOf e.Item Is RibbonSeparator Then
				foreColor = GetTextColor(e.Item.Enabled)
			End If
			embedded = embedded OrElse Not e.Item.Enabled
			If embedded Then
				Dim cbr As Rectangle = e.Bounds
				System.Math.Max(System.Threading.Interlocked.Increment(cbr.Y),cbr.Y - 1)
				Using b As New SolidBrush(ColorTable.ArrowLight)
					e.Graphics.DrawString(e.Text, New Font(f, e.Style), b, cbr, sf)
				End Using
			End If
			If foreColor.Equals(Color.Empty) Then
				foreColor = GetTextColor(e.Item.Enabled)
			End If
			Using b As New SolidBrush(foreColor)
				e.Graphics.DrawString(e.Text, New Font(f, e.Style), b, e.Bounds, sf)
			End Using
		End Sub
		Public Overrides Sub OnRenderRibbonItemImage(e As RibbonItemBoundsEventArgs)
			Dim img As Image = e.Item.Image
			If TypeOf e.Item Is RibbonButton Then
				If Not (e.Item.SizeMode = RibbonElementSizeMode.Large OrElse e.Item.SizeMode = RibbonElementSizeMode.Overflow) Then
					img = (TryCast(e.Item, RibbonButton)).SmallImage
				End If
			End If
			If img IsNot Nothing Then
				If Not e.Item.Enabled Then
					img = CreateDisabledImage(img)
				End If
				e.Graphics.DrawImage(img, e.Bounds)
			End If
		End Sub
		Public Overrides Sub OnRenderPanelPopupBackground(e As RibbonCanvasEventArgs)
			Dim pnl As RibbonPanel = TryCast(e.RelatedObject, RibbonPanel)
			If pnl Is Nothing Then
				Return
			End If
			Dim darkBorder As Rectangle = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom)
			Dim lightBorder As Rectangle = Rectangle.FromLTRB(e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Right - 1, e.Bounds.Bottom - 1)
			Dim textArea As Rectangle = Rectangle.FromLTRB(e.Bounds.Left + 1, pnl.ContentBounds.Bottom, e.Bounds.Right - 1, e.Bounds.Bottom - 1)
			Dim dark As GraphicsPath = RoundRectangle(darkBorder, 3)
			Dim light As GraphicsPath = RoundRectangle(lightBorder, 3)
			Dim txt As GraphicsPath = RoundRectangle(textArea, 3, Corners.SouthEast Or Corners.SouthWest)
			Using p As New Pen(ColorTable.PanelLightBorder)
				e.Graphics.DrawPath(p, light)
			End Using
			Using p As New Pen(ColorTable.PanelDarkBorder)
				e.Graphics.DrawPath(p, dark)
			End Using
			Using b As New SolidBrush(ColorTable.PanelBackgroundSelected)
				e.Graphics.FillPath(b, light)
			End Using
			Using b As New SolidBrush(ColorTable.PanelTextBackground)
				e.Graphics.FillPath(b, txt)
			End Using
			txt.Dispose()
			dark.Dispose()
			light.Dispose()
		End Sub
		Public Overrides Sub OnRenderDropDownBackground(e As RibbonCanvasEventArgs)
			Dim outerR As New Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1)
			Dim imgsR As New Rectangle(0, 0, 26, e.Bounds.Height)
			Dim dd As RibbonDropDown = TryCast(e.Canvas, RibbonDropDown)
			Using b As New SolidBrush(ColorTable.DropDownBg)
				e.Graphics.Clear(Color.Transparent)
				Dim sbuff As SmoothingMode = e.Graphics.SmoothingMode
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
				e.Graphics.FillRectangle(b, outerR)
				e.Graphics.SmoothingMode = sbuff
			End Using
			If dd IsNot Nothing AndAlso dd.DrawIconsBar Then
				Using b As New SolidBrush(ColorTable.DropDownImageBg)
					e.Graphics.FillRectangle(b, imgsR)
				End Using
				Using p As New Pen(ColorTable.DropDownImageSeparator)
					e.Graphics.DrawLine(p, New Point(imgsR.Right, imgsR.Top), New Point(imgsR.Right, imgsR.Bottom))
				End Using
			End If
			Using p As New Pen(ColorTable.DropDownBorder)
				If dd IsNot Nothing Then
					Using r As GraphicsPath = RoundRectangle(New Rectangle(Point.Empty, New Size(dd.Size.Width - 1, dd.Size.Height - 1)), dd.BorderRoundness)
						Dim smb As SmoothingMode = e.Graphics.SmoothingMode
						e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
						e.Graphics.DrawPath(p, r)
						e.Graphics.SmoothingMode = smb
					End Using
				Else
					e.Graphics.DrawRectangle(p, outerR)
				End If
			End Using
			If dd.ShowSizingGrip Then
				Dim gripArea As Rectangle = Rectangle.FromLTRB(e.Bounds.Left + 1, e.Bounds.Bottom - dd.SizingGripHeight, e.Bounds.Right - 1, e.Bounds.Bottom - 1)
				Using b As New LinearGradientBrush(gripArea, ColorTable.DropDownGripNorth, ColorTable.DropDownGripSouth, 90)
					e.Graphics.FillRectangle(b, gripArea)
				End Using
				Using p As New Pen(ColorTable.DropDownGripBorder)
					e.Graphics.DrawLine(p, gripArea.Location, New Point(gripArea.Right - 1, gripArea.Top))
				End Using
				DrawGripDot(e.Graphics, New Point(gripArea.Right - 7, gripArea.Bottom - 3))
				DrawGripDot(e.Graphics, New Point(gripArea.Right - 3, gripArea.Bottom - 7))
				DrawGripDot(e.Graphics, New Point(gripArea.Right - 3, gripArea.Bottom - 3))
			End If
		End Sub
		Public Overrides Sub OnRenderTabScrollButtons(e As RibbonTabRenderEventArgs)
			If e.Tab.ScrollLeftVisible Then
				If e.Tab.ScrollLeftSelected Then
					DrawButtonSelected(e.Graphics, e.Tab.ScrollLeftBounds, Corners.West)
				Else
					DrawButton(e.Graphics, e.Tab.ScrollLeftBounds, Corners.West)
				End If
				DrawArrowShaded(e.Graphics, e.Tab.ScrollLeftBounds, RibbonArrowDirection.Right, True)
			End If
			If e.Tab.ScrollRightVisible Then
				If e.Tab.ScrollRightSelected Then
					DrawButtonSelected(e.Graphics, e.Tab.ScrollRightBounds, Corners.East)
				Else
					DrawButton(e.Graphics, e.Tab.ScrollRightBounds, Corners.East)
				End If
				DrawArrowShaded(e.Graphics, e.Tab.ScrollRightBounds, RibbonArrowDirection.Left, True)
			End If
		End Sub
	End Class
End Namespace
