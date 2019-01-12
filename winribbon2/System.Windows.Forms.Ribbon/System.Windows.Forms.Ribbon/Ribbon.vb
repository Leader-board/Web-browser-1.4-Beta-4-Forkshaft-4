Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms.RibbonHelpers
Namespace System.Windows.Forms
	<Designer(GetType(RibbonDesigner))> _
	Public Class Ribbon
		Inherits Control
		Public Shared CaptionBarHeight As Integer = 24
		Friend ForceOrbMenu As Boolean
		Private _lastSizeMeasured As Size
		Private _tabs As RibbonTabCollection
		Private _tabsMargin As Padding
		Private _tabsPadding As Padding
		Private _contexts As RibbonContextCollection
		Private _minimized As Boolean
		Private _renderer As RibbonRenderer
		Private _tabSpacing As Integer
		Private _tabContentMargin As Padding
		Private _tabContentPadding As Padding
		Private _panelPadding As Padding
		Private _panelMargin As Padding
		Private _activeTab As RibbonTab
		Private _panelSpacing As Integer
		Private _itemMargin As Padding
		Private _itemPadding As Padding
		Private _lastSelectedTab As RibbonTab
		Private _sensor As RibbonMouseSensor
		Private _dropDownMargin As Padding
		Private _tabTextMargin As Padding
		Private _tabSum As Single
		Private _updatingSuspended As Boolean
		Private _orbSelected As Boolean
		Private _orbPressed As Boolean
		Private _orbVisible As Boolean
		Private _orbImage As Image
		Private _quickAcessToolbar As RibbonQuickAccessToolbar
		Private _quickAcessVisible As Boolean
		Private _orbDropDown As RibbonOrbDropDown
		Private _borderMode As RibbonWindowMode
		Private _actualBorderMode As RibbonWindowMode
		Private _CloseButton As RibbonCaptionButton
		Private _MaximizeRestoreButton As RibbonCaptionButton
		Private _MinimizeButton As RibbonCaptionButton
		Private _CaptionButtonsVisible As Boolean
		Private _mouseHook As GlobalHook
		Private _keyboardHook As GlobalHook
		Public Event OrbClicked As EventHandler
		Public Event OrbDoubleClick As EventHandler
		Public Event ActiveTabChanged As EventHandler
		Public Event ActualBorderModeChanged As EventHandler
		Public Event CaptionButtonsVisibleChanged As EventHandler
		Public Sub New()
			SetStyle(ControlStyles.ResizeRedraw, True)
			SetStyle(ControlStyles.Selectable, False)
			SetStyle(ControlStyles.UserPaint, True)
			SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
			Dock = DockStyle.Top
			_tabs = New RibbonTabCollection(Me)
			_contexts = New RibbonContextCollection(Me)
			_tabsMargin = New Padding(12, 24 + 2, 20, 0)
			_tabTextMargin = New Padding(4, 2, 4, 2)
			_tabsPadding = New Padding(8, 5, 8, 3)
			_tabContentMargin = New Padding(1, 0, 1, 2)
			_panelPadding = New Padding(3)
			_panelMargin = New Padding(3, 2, 3, 15)
			_panelSpacing = 3
			_itemPadding = New Padding(1, 0, 1, 0)
			_itemMargin = New Padding(4, 2, 4, 2)
			_tabSpacing = 6
			_dropDownMargin = New Padding(2)
			_renderer = New RibbonProfessionalRenderer()
			_orbVisible = True
			_orbDropDown = New RibbonOrbDropDown(Me)
			_quickAcessToolbar = New RibbonQuickAccessToolbar(Me)
			_quickAcessVisible = True
			_MinimizeButton = New RibbonCaptionButton(RibbonCaptionButton.CaptionButton.Minimize)
			_MaximizeRestoreButton = New RibbonCaptionButton(RibbonCaptionButton.CaptionButton.Maximize)
			_CloseButton = New RibbonCaptionButton(RibbonCaptionButton.CaptionButton.Close)
			_MinimizeButton.SetOwner(Me)
			_MaximizeRestoreButton.SetOwner(Me)
			_CloseButton.SetOwner(Me)
			Font = SystemFonts.CaptionFont
			BorderMode = RibbonWindowMode.NonClientAreaGlass
			Disposed += New EventHandler(Ribbon_Disposed)
		End Sub
		Protected Overrides Sub Finalize()
			Try
				If _mouseHook IsNot Nothing Then
					_mouseHook.Dispose()
				End If
			Finally
				MyBase.Finalize()
			End Try
		End Sub
		Friend ReadOnly Property CaptionTextBounds() As Rectangle
			Get
				Dim left As Integer = 0
				If OrbVisible Then
					left = OrbBounds.Right
				End If
				If QuickAccessVisible Then
					left = QuickAcessToolbar.Bounds.Right + 20
				End If
				If QuickAccessVisible AndAlso QuickAcessToolbar.DropDownButtonVisible Then
					left = QuickAcessToolbar.DropDownButton.Bounds.Right
				End If
				Dim r As Rectangle = Rectangle.FromLTRB(left, 0, Width - 100, CaptionBarSize)
				Return r
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property CaptionButtonsVisible() As Boolean
			Get
				Return _CaptionButtonsVisible
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property CloseButton() As RibbonCaptionButton
			Get
				Return _CloseButton
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property MaximizeRestoreButton() As RibbonCaptionButton
			Get
				Return _MaximizeRestoreButton
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property MinimizeButton() As RibbonCaptionButton
			Get
				Return _MinimizeButton
			End Get
		End Property
		<Browsable(False)> _
		Public ReadOnly Property FormHelper() As RibbonFormHelper
			Get
				Dim irf As IRibbonForm = TryCast(Parent, IRibbonForm)
				If irf IsNot Nothing Then
					Return irf.Helper
				End If
				Return Nothing
			End Get
		End Property
		<Browsable(False)> _
		Public ReadOnly Property ActualBorderMode() As RibbonWindowMode
			Get
				Return _actualBorderMode
			End Get
		End Property
		<DefaultValue(RibbonWindowMode.NonClientAreaGlass)> _
		<Browsable(True)> _
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
		<Description("Specifies how the Ribbon is placed on the window border and the non-client area")> _
		Public Property BorderMode() As RibbonWindowMode
			Get
				Return _borderMode
			End Get
			Set
				_borderMode = value
				Dim actual As RibbonWindowMode = value
				If value = RibbonWindowMode.NonClientAreaGlass AndAlso Not WinApi.IsGlassEnabled Then
					actual = RibbonWindowMode.NonClientAreaCustomDrawn
				End If
				If FormHelper Is Nothing OrElse (value = RibbonWindowMode.NonClientAreaCustomDrawn AndAlso Environment.OSVersion.Platform <> PlatformID.Win32NT) Then
					actual = RibbonWindowMode.InsideWindow
				End If
				SetActualBorderMode(actual)
			End Set
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		<Browsable(False)> _
		Public ReadOnly Property OrbDropDown() As RibbonOrbDropDown
			Get
				Return _orbDropDown
			End Get
		End Property
		<DefaultValue(True)> _
		<Description("Shows or hides the QuickAccess toolbar")> _
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
		Public Property QuickAccessVisible() As Boolean
			Get
				Return _quickAcessVisible
			End Get
			Set
				_quickAcessVisible = value
			End Set
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property QuickAcessToolbar() As RibbonQuickAccessToolbar
			Get
				Return _quickAcessToolbar
			End Get
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
		Public Property OrbImage() As Image
			Get
				Return _orbImage
			End Get
			Set
				_orbImage = value
				Invalidate(OrbBounds)
			End Set
		End Property
		<DefaultValue(True)> _
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
		Public Property OrbVisible() As Boolean
			Get
				Return _orbVisible
			End Get
			Set
				_orbVisible = value
				OnRegionsChanged()
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property OrbSelected() As Boolean
			Get
				Return _orbSelected
			End Get
			Set
				_orbSelected = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property OrbPressed() As Boolean
			Get
				Return _orbPressed
			End Get
			Set
				_orbPressed = value
				Invalidate(OrbBounds)
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property CaptionBarSize() As Integer
			Get
				Return CaptionBarHeight
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property OrbBounds() As Rectangle
			Get
				If OrbVisible Then
					Return New Rectangle(4, 4, 36, 36)
				Else
					Return New Rectangle(4, 4, 0, 0)
				End If
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property NextTab() As RibbonTab
			Get
				If ActiveTab Is Nothing OrElse Tabs.Count = 0 Then
					If Tabs.Count = 0 Then
						Return Nothing
					End If
					Return Tabs(0)
				End If
				Dim index As Integer = Tabs.IndexOf(ActiveTab)
				If index = Tabs.Count - 1 Then
					Return ActiveTab
				Else
					Return Tabs(index + 1)
				End If
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property PreviousTab() As RibbonTab
			Get
				If ActiveTab Is Nothing OrElse Tabs.Count = 0 Then
					If Tabs.Count = 0 Then
						Return Nothing
					End If
					Return Tabs(0)
				End If
				Dim index As Integer = Tabs.IndexOf(ActiveTab)
				If index = 0 Then
					Return ActiveTab
				Else
					Return Tabs(index - 1)
				End If
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property TabTextMargin() As Padding
			Get
				Return _tabTextMargin
			End Get
			Set
				_tabTextMargin = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property DropDownMargin() As Padding
			Get
				Return _dropDownMargin
			End Get
			Set
				_dropDownMargin = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property ItemPadding() As Padding
			Get
				Return _itemPadding
			End Get
			Set
				_itemPadding = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property ItemMargin() As Padding
			Get
				Return _itemMargin
			End Get
			Set
				_itemMargin = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property ActiveTab() As RibbonTab
			Get
				Return _activeTab
			End Get
			Set
				For Each tab As RibbonTab In Tabs
					If tab <> value Then
						tab.SetActive(False)
					Else
						tab.SetActive(True)
					End If
				Next
				_activeTab = value
				RemoveHelperControls()
				value.UpdatePanelsRegions()
				Invalidate()
				RenewSensor()
				OnActiveTabChanged(EventArgs.Empty)
			End Set
		End Property
		<DefaultValue(2)> _
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property PanelSpacing() As Integer
			Get
				Return _panelSpacing
			End Get
			Set
				_panelSpacing = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property PanelPadding() As Padding
			Get
				Return _panelPadding
			End Get
			Set
				_panelPadding = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property PanelMargin() As Padding
			Get
				Return _panelMargin
			End Get
			Set
				_panelMargin = value
			End Set
		End Property
		<DefaultValue(7)> _
		Public Property TabSpacing() As Integer
			Get
				Return _tabSpacing
			End Get
			Set
				_tabSpacing = value
			End Set
		End Property
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property Tabs() As RibbonTabCollection
			Get
				Return _tabs
			End Get
		End Property
		Public Property Minimized() As Boolean
			Get
				Return _minimized
			End Get
			Set
				_minimized = value
			End Set
		End Property
		Public ReadOnly Property Contexts() As RibbonContextCollection
			Get
				Return _contexts
			End Get
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property Renderer() As RibbonRenderer
			Get
				Return _renderer
			End Get
			Set
				If value Is Nothing Then
					Throw New ApplicationException("Null renderer!")
				End If
				_renderer = value
				Invalidate()
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property TabContentMargin() As Padding
			Get
				Return _tabContentMargin
			End Get
			Set
				_tabContentMargin = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property TabContentPadding() As Padding
			Get
				Return _tabContentPadding
			End Get
			Set
				_tabContentPadding = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property TabsMargin() As Padding
			Get
				Return _tabsMargin
			End Get
			Set
				_tabsMargin = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Property TabsPadding() As Padding
			Get
				Return _tabsPadding
			End Get
			Set
				_tabsPadding = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overrides Property MaximumSize() As Size
			Get
				Return New System.Drawing.Size(0, 138)
			End Get
			Set
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Overrides Property MinimumSize() As Size
			Get
				Return New System.Drawing.Size(0, 138)
			End Get
			Set
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		<DefaultValue(DockStyle.Top)> _
		Public Overrides Property Dock() As DockStyle
			Get
				Return MyBase.Dock
			End Get
			Set
				MyBase.Dock = value
			End Set
		End Property
		<Browsable(False)> _
		Public ReadOnly Property Sensor() As RibbonMouseSensor
			Get
				Return _sensor
			End Get
		End Property
		Private ReadOnly Property cr() As String
			Get
				Return "Professional Ribbon" & vbLf & vbLf & "2009 Jos� Manuel Men�ndez Poo" & vbLf & "www.menendezpoo.com"
			End Get
		End Property
		Private Sub _mouseHook_MouseDown(sender As Object, e As MouseEventArgs)
			If Not RectangleToScreen(OrbBounds).Contains(e.Location) Then
				RibbonPopupManager.FeedHookClick(e)
			End If
		End Sub
		Private Sub _mouseHook_MouseWheel(sender As Object, e As MouseEventArgs)
			If Not RibbonPopupManager.FeedMouseWheel(e) Then
				If RectangleToScreen(New Rectangle(Point.Empty, Size)).Contains(e.Location) Then
					OnMouseWheel(e)
				End If
			End If
		End Sub
		Friend Overridable Sub OnOrbClicked(e As EventArgs)
			If OrbPressed Then
				OrbDropDown.Close()
			Else
				ShowOrbDropDown()
			End If
			If OrbClicked IsNot Nothing Then
				OrbClicked(Me, e)
			End If
		End Sub
		Friend Overridable Sub OnOrbDoubleClicked(e As EventArgs)
			If OrbDoubleClick IsNot Nothing Then
				OrbDoubleClick(Me, e)
			End If
		End Sub
		Private Sub SetUpHooks()
			If Not (Site IsNot Nothing AndAlso Site.DesignMode) Then
				_mouseHook = New GlobalHook(GlobalHook.HookTypes.Mouse)
				_mouseHook.MouseWheel += New MouseEventHandler(_mouseHook_MouseWheel)
				_mouseHook.MouseDown += New MouseEventHandler(_mouseHook_MouseDown)
				_keyboardHook = New GlobalHook(GlobalHook.HookTypes.Keyboard)
				_keyboardHook.KeyDown += New KeyEventHandler(_keyboardHook_KeyDown)
			End If
		End Sub
		Private Sub _keyboardHook_KeyDown(sender As Object, e As KeyEventArgs)
			If e.KeyCode = Keys.Escape Then
				RibbonPopupManager.Dismiss(RibbonPopupManager.DismissReason.EscapePressed)
			End If
		End Sub
		Private Sub Ribbon_Disposed(sender As Object, e As EventArgs)
			If _mouseHook IsNot Nothing Then
				_mouseHook.Dispose()
			End If
			If _keyboardHook IsNot Nothing Then
				_keyboardHook.Dispose()
			End If
		End Sub
		Public Sub ShowOrbDropDown()
			OrbPressed = True
			OrbDropDown.Show(PointToScreen(New Point(OrbBounds.X - 4, OrbBounds.Bottom - OrbDropDown.ContentMargin.Top + 2)))
		End Sub
		Private Sub RenewSensor()
			If ActiveTab Is Nothing Then
				Return
			End If
			If Sensor IsNot Nothing Then
				Sensor.Dispose()
			End If
			_sensor = New RibbonMouseSensor(Me, Me, ActiveTab)
			If CaptionButtonsVisible Then
				Sensor.Items.AddRange(New RibbonItem() {CloseButton, MaximizeRestoreButton, MinimizeButton})
			End If
		End Sub
		Private Sub SetActualBorderMode(borderMode As RibbonWindowMode)
			Dim trigger As Boolean = _actualBorderMode <> borderMode
			_actualBorderMode = borderMode
			If trigger Then
				OnActualBorderModeChanged(EventArgs.Empty)
			End If
			SetCaptionButtonsVisible(borderMode = RibbonWindowMode.NonClientAreaCustomDrawn)
		End Sub
		Private Sub SetCaptionButtonsVisible(visible As Boolean)
			Dim trigger As Boolean = _CaptionButtonsVisible <> visible
			_CaptionButtonsVisible = visible
			If trigger Then
				OnCaptionButtonsVisibleChanged(EventArgs.Empty)
			End If
		End Sub
		Public Sub SuspendUpdating()
			_updatingSuspended = True
		End Sub
		Public Sub ResumeUpdating()
			ResumeUpdating(True)
		End Sub
		Public Sub ResumeUpdating(update As Boolean)
			_updatingSuspended = False
			If update Then
				OnRegionsChanged()
			End If
		End Sub
		Private Sub RemoveHelperControls()
			RibbonPopupManager.Dismiss(RibbonPopupManager.DismissReason.AppClicked)
			While Controls.Count > 0
				Dim ctl As Control = Controls(0)
				ctl.Visible = False
				Controls.Remove(ctl)
			End While
		End Sub
		Friend Function TabHitTest(x As Integer, y As Integer) As Boolean
			If Rectangle.FromLTRB(Right - 10, Bottom - 10, Right, Bottom).Contains(x, y) Then
				MessageBox.Show(cr)
			End If
			For Each tab As RibbonTab In Tabs
				If tab.TabBounds.Contains(x, y) Then
					ActiveTab = tab
					Return True
				End If
			Next
			Return False
		End Function
		Friend Sub UpdateRegions()
			UpdateRegions(Nothing)
		End Sub
		Friend Sub UpdateRegions(g As Graphics)
			Dim graphicsCreated As Boolean = False
			If IsDisposed OrElse _updatingSuspended Then
				Return
			End If
			If g Is Nothing Then
				g = CreateGraphics()
				graphicsCreated = True
			End If
			Dim curLeft As Integer = TabsMargin.Left + OrbBounds.Width
			Dim maxWidth As Integer = 0
			Dim tabsBottom As Integer = 0
			For Each tab As RibbonTab In Tabs
				Dim tabSize As Size = tab.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(g, RibbonElementSizeMode.None))
				Dim bounds As New Rectangle(curLeft, TabsMargin.Top, TabsPadding.Left + tabSize.Width + TabsPadding.Right, TabsPadding.Top + tabSize.Height + TabsPadding.Bottom)
				tab.SetTabBounds(bounds)
				curLeft = bounds.Right + TabSpacing
				maxWidth = Math.Max(bounds.Width, maxWidth)
				tabsBottom = Math.Max(bounds.Bottom, tabsBottom)
				tab.SetTabContentBounds(Rectangle.FromLTRB(TabContentMargin.Left, tabsBottom + TabContentMargin.Top, ClientSize.Width - 1 - TabContentMargin.Right, ClientSize.Height - 1 - TabContentMargin.Bottom))
				If tab.Active Then
					tab.UpdatePanelsRegions()
				End If
			Next
			While curLeft > ClientRectangle.Right AndAlso maxWidth > 0
				curLeft = TabsMargin.Left + OrbBounds.Width
				System.Math.Max(System.Threading.Interlocked.Decrement(maxWidth),maxWidth + 1)
				For Each tab As RibbonTab In Tabs
					If tab.TabBounds.Width >= maxWidth Then
						tab.SetTabBounds(New Rectangle(curLeft, TabsMargin.Top, maxWidth, tab.TabBounds.Height))
					Else
						tab.SetTabBounds(New Rectangle(New Point(curLeft, TabsMargin.Top), tab.TabBounds.Size))
					End If
					curLeft = tab.TabBounds.Right + TabSpacing
				Next
			End While
			QuickAcessToolbar.MeasureSize(Me, New RibbonElementMeasureSizeEventArgs(g, RibbonElementSizeMode.Compact))
			QuickAcessToolbar.SetBounds(New Rectangle(New Point(OrbBounds.Right + QuickAcessToolbar.Margin.Left, OrbBounds.Top - 2), QuickAcessToolbar.LastMeasuredSize))
			If CaptionButtonsVisible Then
				Dim cbs As New Size(20, 20)
				Dim cbg As Integer = 2
				CloseButton.SetBounds(New Rectangle(New Point(ClientRectangle.Right - cbs.Width - cbg, cbg), cbs))
				MaximizeRestoreButton.SetBounds(New Rectangle(New Point(CloseButton.Bounds.Left - cbs.Width, cbg), cbs))
				MinimizeButton.SetBounds(New Rectangle(New Point(MaximizeRestoreButton.Bounds.Left - cbs.Width, cbg), cbs))
			End If
			If graphicsCreated Then
				g.Dispose()
			End If
			_lastSizeMeasured = Size
			RenewSensor()
		End Sub
		Friend Sub OnRegionsChanged()
			If _updatingSuspended Then
				Return
			End If
			If Tabs.Count = 1 Then
				ActiveTab = Tabs(0)
			End If
			_lastSizeMeasured = Size.Empty
			Refresh()
		End Sub
		Private Sub RedrawTab(tab As RibbonTab)
			Using g As Graphics = CreateGraphics()
				Dim clip As Rectangle = Rectangle.FromLTRB(tab.TabBounds.Left, tab.TabBounds.Top, tab.TabBounds.Right, tab.TabBounds.Bottom)
				g.SetClip(clip)
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
				tab.OnPaint(Me, New RibbonElementPaintEventArgs(tab.TabBounds, g, RibbonElementSizeMode.None))
			End Using
		End Sub
		Private Sub SetSelectedTab(tab As RibbonTab)
			If tab = _lastSelectedTab Then
				Return
			End If
			If _lastSelectedTab IsNot Nothing Then
				_lastSelectedTab.SetSelected(False)
				RedrawTab(_lastSelectedTab)
			End If
			If tab IsNot Nothing Then
				tab.SetSelected(True)
				RedrawTab(tab)
			End If
			_lastSelectedTab = tab
		End Sub
		Friend Sub SuspendSensor()
			If Sensor IsNot Nothing Then
				Sensor.Suspend()
			End If
		End Sub
		Friend Sub ResumeSensor()
			Sensor.[Resume]()
		End Sub
		Public Sub RedrawArea(area As Rectangle)
			Sensor.Control.Invalidate(area)
		End Sub
		Public Sub ActivateNextTab()
			Dim tab As RibbonTab = NextTab
			If tab IsNot Nothing Then
				ActiveTab = tab
			End If
		End Sub
		Public Sub ActivatePreviousTab()
			Dim tab As RibbonTab = PreviousTab
			If tab IsNot Nothing Then
				ActiveTab = tab
			End If
		End Sub
		Friend Sub OrbMouseDown()
			OnOrbClicked(EventArgs.Empty)
		End Sub
		Protected Overrides Sub WndProc(ByRef m As Message)
			Dim bypassed As Boolean = False
			If WinApi.IsWindows AndAlso (ActualBorderMode = RibbonWindowMode.NonClientAreaGlass OrElse ActualBorderMode = RibbonWindowMode.NonClientAreaCustomDrawn) Then
				If m.Msg = WinApi.WM_NCHITTEST Then
					Dim f As Form = FindForm()
					Dim captionLeft As Integer = If(QuickAccessVisible, QuickAcessToolbar.Bounds.Right, OrbBounds.Right)
					If QuickAccessVisible AndAlso QuickAcessToolbar.DropDownButtonVisible Then
						captionLeft = QuickAcessToolbar.DropDownButton.Bounds.Right
					End If
					Dim caption As Rectangle = Rectangle.FromLTRB(captionLeft, 0, Width, CaptionBarSize)
					Dim screenPoint As New Point(WinApi.LoWord(CType(m.LParam, Integer)), WinApi.HiWord(CType(m.LParam, Integer)))
					Dim ribbonPoint As Point = PointToClient(screenPoint)
					Dim onCaptionButtons As Boolean = False
					If CaptionButtonsVisible Then
						onCaptionButtons = CloseButton.Bounds.Contains(ribbonPoint) OrElse MinimizeButton.Bounds.Contains(ribbonPoint) OrElse MaximizeRestoreButton.Bounds.Contains(ribbonPoint)
					End If
					If RectangleToScreen(caption).Contains(screenPoint) AndAlso Not onCaptionButtons Then
						Dim p As Point = PointToScreen(screenPoint)
						WinApi.SendMessage(f.Handle, WinApi.WM_NCHITTEST, m.WParam, WinApi.MakeLParam(p.X, p.Y))
						m.Result = New IntPtr(-1)
						bypassed = True
					End If
				End If
			End If
			If Not bypassed Then
				MyBase.WndProc(m)
			End If
		End Sub
		Private Sub PaintOn(g As Graphics, clip As Rectangle)
			If WinApi.IsWindows AndAlso Environment.OSVersion.Platform = PlatformID.Win32NT Then
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
			End If
			Renderer.OnRenderRibbonBackground(New RibbonRenderEventArgs(Me, g, clip))
			Renderer.OnRenderRibbonCaptionBar(New RibbonRenderEventArgs(Me, g, clip))
			If CaptionButtonsVisible Then
				MinimizeButton.OnPaint(Me, New RibbonElementPaintEventArgs(clip, g, RibbonElementSizeMode.Medium))
				MaximizeRestoreButton.OnPaint(Me, New RibbonElementPaintEventArgs(clip, g, RibbonElementSizeMode.Medium))
				CloseButton.OnPaint(Me, New RibbonElementPaintEventArgs(clip, g, RibbonElementSizeMode.Medium))
			End If
			Renderer.OnRenderRibbonOrb(New RibbonRenderEventArgs(Me, g, clip))
			QuickAcessToolbar.OnPaint(Me, New RibbonElementPaintEventArgs(clip, g, RibbonElementSizeMode.Compact))
			For Each tab As RibbonTab In Tabs
				tab.OnPaint(Me, New RibbonElementPaintEventArgs(tab.TabBounds, g, RibbonElementSizeMode.None, Me))
			Next
		End Sub
		Private Sub PaintDoubleBuffered(wndGraphics As Graphics, clip As Rectangle)
			Using bmp As New Bitmap(Width, Height)
				Using g As Graphics = Graphics.FromImage(bmp)
					g.Clear(Color.Black)
					PaintOn(g, clip)
					g.Flush()
					WinApi.BitBlt(wndGraphics.GetHdc(), clip.X, clip.Y, clip.Width, clip.Height, g.GetHdc(), _
						clip.X, clip.Y, WinApi.SRCCOPY)
				End Using
			End Using
		End Sub
		Protected Overridable Sub OnActiveTabChanged(e As EventArgs)
			If ActiveTabChanged IsNot Nothing Then
				ActiveTabChanged(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnActualBorderModeChanged(e As EventArgs)
			If ActualBorderModeChanged IsNot Nothing Then
				ActualBorderModeChanged(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnCaptionButtonsVisibleChanged(e As EventArgs)
			If CaptionButtonsVisibleChanged IsNot Nothing Then
				CaptionButtonsVisibleChanged(Me, e)
			End If
		End Sub
		Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
			MyBase.OnMouseDoubleClick(e)
			If OrbBounds.Contains(e.Location) Then
				OnOrbDoubleClicked(EventArgs.Empty)
			End If
		End Sub
		Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
		End Sub
		Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
			If _updatingSuspended Then
				Return
			End If
			If Size <> _lastSizeMeasured Then
				UpdateRegions(e.Graphics)
			End If
			PaintOn(e.Graphics, e.ClipRectangle)
		End Sub
		Protected Overrides Sub OnClick(e As System.EventArgs)
			MyBase.OnClick(e)
		End Sub
		Protected Overrides Sub OnMouseEnter(e As System.EventArgs)
			MyBase.OnMouseEnter(e)
		End Sub
		Protected Overrides Sub OnMouseLeave(e As System.EventArgs)
			MyBase.OnMouseLeave(e)
		End Sub
		Protected Overrides Sub OnMouseMove(e As System.Windows.Forms.MouseEventArgs)
			MyBase.OnMouseMove(e)
			If ActiveTab Is Nothing Then
				Return
			End If
			Dim someTabHitted As Boolean = False
			If ActiveTab.TabContentBounds.Contains(e.X, e.Y) Then
			ElseIf OrbVisible AndAlso OrbBounds.Contains(e.Location) AndAlso Not OrbSelected Then
				OrbSelected = True
				Invalidate(OrbBounds)
			ElseIf QuickAccessVisible AndAlso QuickAcessToolbar.Bounds.Contains(e.Location) Then
			Else
				For Each tab As RibbonTab In Tabs
					If tab.TabBounds.Contains(e.X, e.Y) Then
						SetSelectedTab(tab)
						someTabHitted = True
					End If
				Next
			End If
			If Not someTabHitted Then
				SetSelectedTab(Nothing)
			End If
			If OrbSelected AndAlso Not OrbBounds.Contains(e.Location) Then
				OrbSelected = False
				Invalidate(OrbBounds)
			End If
		End Sub
		Protected Overrides Sub OnMouseUp(e As System.Windows.Forms.MouseEventArgs)
			MyBase.OnMouseUp(e)
		End Sub
		Protected Overrides Sub OnMouseDown(e As System.Windows.Forms.MouseEventArgs)
			MyBase.OnMouseDown(e)
			If OrbBounds.Contains(e.Location) Then
				OrbMouseDown()
			Else
				TabHitTest(e.X, e.Y)
			End If
		End Sub
		Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
			MyBase.OnMouseWheel(e)
			If Tabs.Count = 0 OrElse ActiveTab Is Nothing Then
				Return
			End If
			Dim index As Integer = Tabs.IndexOf(ActiveTab)
			If e.Delta < 0 Then
				_tabSum += 0.4F
			Else
				_tabSum -= 0.4F
			End If
			Dim tabRounded As Integer = Convert.ToInt16(Math.Round(_tabSum))
			If tabRounded <> 0 Then
				index += tabRounded
				If index < 0 Then
					index = 0
				ElseIf index >= Tabs.Count - 1 Then
					index = Tabs.Count - 1
				End If
				ActiveTab = Tabs(index)
				_tabSum = 0F
			End If
		End Sub
		Protected Overrides Sub OnSizeChanged(e As System.EventArgs)
			UpdateRegions()
			RemoveHelperControls()
			MyBase.OnSizeChanged(e)
		End Sub
		Protected Overrides Sub OnParentChanged(e As EventArgs)
			MyBase.OnParentChanged(e)
			If Not (Site IsNot Nothing AndAlso Site.DesignMode) Then
				BorderMode = BorderMode
				If TypeOf Parent Is IRibbonForm Then
					FormHelper.Ribbon = Me
				End If
				SetUpHooks()
			End If
		End Sub
	End Class
End Namespace
