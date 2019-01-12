Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.RibbonHelpers
Namespace System.Windows.Forms
	Public Class RibbonOrbDropDown
		Inherits RibbonPopup
		Friend LastPoppedMenuItem As RibbonOrbMenuItem
		Private designerSelectedBounds As Rectangle
		Private glyphGap As Integer = 3
		Private _contentMargin As Padding
		Private _ribbon As Ribbon
		Private _menuItems As RibbonItemCollection
		Private _recentItems As RibbonItemCollection
		Private _optionItems As RibbonItemCollection
		Private _sensor As RibbonMouseSensor
		Private _optionsPadding As Integer
		Friend Sub New(ribbon As Ribbon)
			MyBase.New()
			DoubleBuffered = True
			_ribbon = ribbon
			_menuItems = New RibbonItemCollection()
			_recentItems = New RibbonItemCollection()
			_optionItems = New RibbonItemCollection()
			_menuItems.SetOwner(Ribbon)
			_recentItems.SetOwner(Ribbon)
			_optionItems.SetOwner(Ribbon)
			_optionsPadding = 6
			Size = New System.Drawing.Size(527, 447)
			BorderRoundness = 8
		End Sub
		Protected Overrides Sub Finalize()
			Try
				If _sensor IsNot Nothing Then
					_sensor.Dispose()
				End If
			Finally
				MyBase.Finalize()
			End Try
		End Sub
		Friend ReadOnly Property AllItems() As List(Of RibbonItem)
			Get
				Dim lst As New List(Of RibbonItem)()
				lst.AddRange(MenuItems)
				lst.AddRange(RecentItems)
				lst.AddRange(OptionItems)
				Return lst
			End Get
		End Property
		<Browsable(False)> _
		Public ReadOnly Property ContentMargin() As Padding
			Get
				If _contentMargin.Size.IsEmpty Then
					_contentMargin = New Padding(6, 17, 6, 29)
				End If
				Return _contentMargin
			End Get
		End Property
		<Browsable(False)> _
		Public ReadOnly Property ContentBounds() As Rectangle
			Get
				Return Rectangle.FromLTRB(ContentMargin.Left, ContentMargin.Top, ClientRectangle.Right - ContentMargin.Right, ClientRectangle.Bottom - ContentMargin.Bottom)
			End Get
		End Property
		<Browsable(False)> _
		Public ReadOnly Property ContentButtonsBounds() As Rectangle
			Get
				Dim r As Rectangle = ContentBounds
				r.Width = 150
				Return r
			End Get
		End Property
		<Browsable(False)> _
		Public ReadOnly Property ContentRecentItemsBounds() As Rectangle
			Get
				Dim r As Rectangle = ContentBounds
				r.Width -= 150
				r.X += 150
				Return r
			End Get
		End Property
		Private ReadOnly Property RibbonInDesignMode() As Boolean
			Get
				Return RibbonDesigner.Current <> Nothing
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property MenuItems() As RibbonItemCollection
			Get
				Return _menuItems
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property OptionItems() As RibbonItemCollection
			Get
				Return _optionItems
			End Get
		End Property
		<DefaultValue(6)> _
		<Description("Spacing between option buttons (those on the bottom)")> _
		Public Property OptionItemsPadding() As Integer
			Get
				Return _optionsPadding
			End Get
			Set
				_optionsPadding = value
			End Set
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property RecentItems() As RibbonItemCollection
			Get
				Return _recentItems
			End Get
		End Property
		<Browsable(False)> _
		Public ReadOnly Property Ribbon() As Ribbon
			Get
				Return _ribbon
			End Get
		End Property
		<Browsable(False)> _
		Public ReadOnly Property Sensor() As RibbonMouseSensor
			Get
				Return _sensor
			End Get
		End Property
		Friend ReadOnly Property ButtonsGlyphBounds() As Rectangle
			Get
				Dim s As New Size(50, 18)
				Dim rf As Rectangle = ContentButtonsBounds
				Dim r As New Rectangle(rf.Left + (rf.Width - s.Width * 2) / 2, rf.Top + glyphGap, s.Width, s.Height)
				If MenuItems.Count > 0 Then
					r.Y = MenuItems(MenuItems.Count - 1).Bounds.Bottom + glyphGap
				End If
				Return r
			End Get
		End Property
		Friend ReadOnly Property ButtonsSeparatorGlyphBounds() As Rectangle
			Get
				Dim s As New Size(18, 18)
				Dim r As Rectangle = ButtonsGlyphBounds
				r.X = r.Right + glyphGap
				Return r
			End Get
		End Property
		Friend ReadOnly Property RecentGlyphBounds() As Rectangle
			Get
				Dim s As New Size(50, 18)
				Dim rf As Rectangle = ContentRecentItemsBounds
				Dim r As New Rectangle(rf.Left + glyphGap, rf.Top + glyphGap, s.Width, s.Height)
				If RecentItems.Count > 0 Then
					r.Y = RecentItems(RecentItems.Count - 1).Bounds.Bottom + glyphGap
				End If
				Return r
			End Get
		End Property
		Friend ReadOnly Property OptionGlyphBounds() As Rectangle
			Get
				Dim s As New Size(50, 18)
				Dim rf As Rectangle = ContentBounds
				Dim r As New Rectangle(rf.Right - s.Width, rf.Bottom + glyphGap, s.Width, s.Height)
				If OptionItems.Count > 0 Then
					r.X = OptionItems(OptionItems.Count - 1).Bounds.Left - s.Width - glyphGap
				End If
				Return r
			End Get
		End Property
		Friend Sub HandleDesignerItemRemoved(item As RibbonItem)
			If MenuItems.Contains(item) Then
				MenuItems.Remove(item)
			ElseIf RecentItems.Contains(item) Then
				RecentItems.Remove(item)
			ElseIf OptionItems.Contains(item) Then
				OptionItems.Remove(item)
			End If
			OnRegionsChanged()
		End Sub
		Private Function SeparatorHeight(s As RibbonSeparator) As Integer
			If Not String.IsNullOrEmpty(s.Text) Then
				Return 20
			Else
				Return 3
			End If
		End Function
		Private Sub UpdateRegions()
			Dim curtop As Integer = 0
			Dim curright As Integer = 0
			Dim menuItemHeight As Integer = 44
			Dim recentHeight As Integer = 22
			Dim rcontent As Rectangle = ContentBounds
			Dim rbuttons As Rectangle = ContentButtonsBounds
			Dim rrecent As Rectangle = ContentRecentItemsBounds
			Dim mbuttons As Integer = 1
			Dim mrecent As Integer = 1
			Dim buttonsHeight As Integer = 0
			Dim recentsHeight As Integer = 0
			For Each item As RibbonItem In AllItems
				item.SetSizeMode(RibbonElementSizeMode.DropDown)
				item.SetCanvas(Me)
			Next
			curtop = rcontent.Top + 1
			For Each item As RibbonItem In MenuItems
				Dim ritem As New Rectangle(rbuttons.Left + mbuttons, curtop, rbuttons.Width - mbuttons * 2, menuItemHeight)
				If TypeOf item Is RibbonSeparator Then
					ritem.Height = SeparatorHeight(TryCast(item, RibbonSeparator))
				End If
				item.SetBounds(ritem)
				curtop += ritem.Height
			Next
			buttonsHeight = curtop - rcontent.Top + 1
			curtop = rbuttons.Top
			For Each item As RibbonItem In RecentItems
				Dim ritem As New Rectangle(rrecent.Left + mrecent, curtop, rrecent.Width - mrecent * 2, recentHeight)
				If TypeOf item Is RibbonSeparator Then
					ritem.Height = SeparatorHeight(TryCast(item, RibbonSeparator))
				End If
				item.SetBounds(ritem)
				curtop += ritem.Height
			Next
			recentsHeight = curtop - rbuttons.Top
			Dim actualHeight As Integer = Math.Max(buttonsHeight, recentsHeight)
			If RibbonDesigner.Current IsNot Nothing Then
				actualHeight += ButtonsGlyphBounds.Height + glyphGap * 2
			End If
			Height = actualHeight + ContentMargin.Vertical
			rcontent = ContentBounds
			curright = ClientSize.Width - ContentMargin.Right
			Using g As Graphics = CreateGraphics()
				For Each item As RibbonItem In OptionItems
					Dim s As Size = item.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(g, RibbonElementSizeMode.DropDown))
					curtop = rcontent.Bottom + (ContentMargin.Bottom - s.Height) / 2
					item.SetBounds(New Rectangle(New Point(curright - s.Width, curtop), s))
					curright = item.Bounds.Left - OptionItemsPadding
				Next
			End Using
		End Sub
		Private Sub UpdateSensor()
			If _sensor IsNot Nothing Then
				_sensor.Dispose()
			End If
			_sensor = New RibbonMouseSensor(Me, Ribbon, AllItems)
		End Sub
		Friend Sub OnRegionsChanged()
			UpdateRegions()
			UpdateSensor()
			UpdateDesignerSelectedBounds()
			Invalidate()
		End Sub
		Friend Sub SelectOnDesigner(item As RibbonItem)
			If RibbonDesigner.Current IsNot Nothing Then
				RibbonDesigner.Current.SelectedElement = item
				UpdateDesignerSelectedBounds()
				Invalidate()
			End If
		End Sub
		Friend Sub UpdateDesignerSelectedBounds()
			designerSelectedBounds = Rectangle.Empty
			If RibbonInDesignMode Then
				Dim item As RibbonItem = TryCast(RibbonDesigner.Current.SelectedElement, RibbonItem)
				If item IsNot Nothing AndAlso AllItems.Contains(item) Then
					designerSelectedBounds = item.Bounds
				End If
			End If
		End Sub
		Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
			MyBase.OnMouseDown(e)
			If RibbonInDesignMode Then
				If ContentBounds.Contains(e.Location) Then
					If ContentButtonsBounds.Contains(e.Location) Then
						For Each item As RibbonItem In MenuItems
							If item.Bounds.Contains(e.Location) Then
								SelectOnDesigner(item)
								Exit For
							End If
						Next
					ElseIf ContentRecentItemsBounds.Contains(e.Location) Then
						For Each item As RibbonItem In RecentItems
							If item.Bounds.Contains(e.Location) Then
								SelectOnDesigner(item)
								Exit For
							End If
						Next
					End If
				End If
				If ButtonsGlyphBounds.Contains(e.Location) Then
					RibbonDesigner.Current.CreteOrbMenuItem(GetType(RibbonOrbMenuItem))
				ElseIf ButtonsSeparatorGlyphBounds.Contains(e.Location) Then
					RibbonDesigner.Current.CreteOrbMenuItem(GetType(RibbonSeparator))
				ElseIf RecentGlyphBounds.Contains(e.Location) Then
					RibbonDesigner.Current.CreteOrbRecentItem(GetType(RibbonOrbRecentItem))
				ElseIf OptionGlyphBounds.Contains(e.Location) Then
					RibbonDesigner.Current.CreteOrbOptionItem(GetType(RibbonOrbOptionButton))
				Else
					For Each item As RibbonItem In OptionItems
						If item.Bounds.Contains(e.Location) Then
							SelectOnDesigner(item)
							Exit For
						End If
					Next
				End If
			End If
		End Sub
		Protected Overrides Sub OnOpening(e As System.ComponentModel.CancelEventArgs)
			MyBase.OnOpening(e)
			UpdateRegions()
		End Sub
		Protected Overrides Sub OnPaint(e As PaintEventArgs)
			MyBase.OnPaint(e)
			Ribbon.Renderer.OnRenderOrbDropDownBackground(New RibbonOrbDropDownEventArgs(Ribbon, Me, e.Graphics, e.ClipRectangle))
			For Each item As RibbonItem In AllItems
				item.OnPaint(Me, New RibbonElementPaintEventArgs(e.ClipRectangle, e.Graphics, RibbonElementSizeMode.DropDown))
			Next
			If RibbonInDesignMode Then
				Using b As New SolidBrush(Color.FromArgb(50, Color.Blue))
					e.Graphics.FillRectangle(b, ButtonsGlyphBounds)
					e.Graphics.FillRectangle(b, RecentGlyphBounds)
					e.Graphics.FillRectangle(b, OptionGlyphBounds)
					e.Graphics.FillRectangle(b, ButtonsSeparatorGlyphBounds)
				End Using
				Using sf As New StringFormat()
					sf.Alignment = StringAlignment.Center
					sf.LineAlignment = StringAlignment.Center
					sf.Trimming = StringTrimming.None
					e.Graphics.DrawString("+", Font, Brushes.White, ButtonsGlyphBounds, sf)
					e.Graphics.DrawString("+", Font, Brushes.White, RecentGlyphBounds, sf)
					e.Graphics.DrawString("+", Font, Brushes.White, OptionGlyphBounds, sf)
					e.Graphics.DrawString("---", Font, Brushes.White, ButtonsSeparatorGlyphBounds, sf)
				End Using
				Using p As New Pen(Color.Black)
					p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
					e.Graphics.DrawRectangle(p, designerSelectedBounds)
				End Using
			End If
		End Sub
		Protected Overrides Sub OnClosed(e As EventArgs)
			Ribbon.OrbPressed = False
			Ribbon.OrbSelected = False
			LastPoppedMenuItem = Nothing
			MyBase.OnClosed(e)
		End Sub
		Protected Overrides Sub OnShowed(e As EventArgs)
			MyBase.OnShowed(e)
			UpdateSensor()
		End Sub
		Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
			MyBase.OnMouseClick(e)
			If Ribbon.RectangleToScreen(Ribbon.OrbBounds).Contains(PointToScreen(e.Location)) Then
				Ribbon.OnOrbClicked(EventArgs.Empty)
			End If
		End Sub
		Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
			MyBase.OnMouseDoubleClick(e)
			If Ribbon.RectangleToScreen(Ribbon.OrbBounds).Contains(PointToScreen(e.Location)) Then
				Ribbon.OnOrbDoubleClicked(EventArgs.Empty)
			End If
		End Sub
	End Class
End Namespace
