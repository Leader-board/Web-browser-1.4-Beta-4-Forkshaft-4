Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.RibbonHelpers
Namespace System.Windows.Forms
	Public Class RibbonFormHelper
		Public Enum NonClientHitTestResult
			Nowhere = 0
			Client = 1
			Caption = 2
			GrowBox = 4
			MinimizeButton = 8
			MaximizeButton = 9
			Left = 10
			Right = 11
			Top = 12
			TopLeft = 13
			TopRight = 14
			Bottom = 15
			BottomLeft = 16
			BottomRight = 17
		End Enum
		Private _lastState As FormWindowState
		Private _form As Form
		Private _margins As Padding
		Private _marginsChecked As Boolean
		Private _capionHeight As Integer
		Private _frameExtended As Boolean
		Private _ribbon As Ribbon
		Public Sub New(f As Form)
			_form = f
			_form.Load += New EventHandler(Form_Activated)
			_form.Paint += New PaintEventHandler(Form_Paint)
			_form.ResizeEnd += New EventHandler(_form_ResizeEnd)
			_form.Resize += New EventHandler(_form_Resize)
			_form.Layout += New LayoutEventHandler(_form_Layout)
		End Sub
		Sub _form_Layout(sender As Object, e As LayoutEventArgs)
			If _lastState = _form.WindowState Then
				Return
			End If
			Form.Invalidate()
			_lastState = _form.WindowState
		End Sub
		Sub _form_Resize(sender As Object, e As EventArgs)
			UpdateRibbonConditions()
			Using g As Graphics = Form.CreateGraphics()
				Using b As Brush = New SolidBrush(Form.BackColor)
					g.FillRectangle(b, Rectangle.FromLTRB(Margins.Left - 0, Margins.Top + 0, Form.Width - Margins.Right - 0, Form.Height - Margins.Bottom - 0))
				End Using
			End Using
		End Sub
		Sub _form_ResizeEnd(sender As Object, e As EventArgs)
			UpdateRibbonConditions()
			Form.Refresh()
		End Sub
		Public Property Ribbon() As Ribbon
			Get
				Return _ribbon
			End Get
			Set
				_ribbon = value
				UpdateRibbonConditions()
			End Set
		End Property
		Public Property CaptionHeight() As Integer
			Get
				Return _capionHeight
			End Get
			Set
				_capionHeight = value
			End Set
		End Property
		Public ReadOnly Property Form() As Form
			Get
				Return _form
			End Get
		End Property
		Public ReadOnly Property Margins() As Padding
			Get
				Return _margins
			End Get
		End Property
		Private Property MarginsChecked() As Boolean
			Get
				Return _marginsChecked
			End Get
			Set
				_marginsChecked = value
			End Set
		End Property
		Private ReadOnly Property DesignMode() As Boolean
			Get
				Return Form IsNot Nothing AndAlso Form.Site IsNot Nothing AndAlso Form.Site.DesignMode
			End Get
		End Property
		Private Sub UpdateRibbonConditions()
			If Ribbon Is Nothing Then
				Return
			End If
			If Ribbon.ActualBorderMode = RibbonWindowMode.NonClientAreaGlass Then
				If Ribbon.Dock <> DockStyle.None Then
					Ribbon.Dock = DockStyle.None
				End If
				Ribbon.SetBounds(Margins.Left, Margins.Bottom - 1, Form.Width - Margins.Horizontal, Ribbon.Height)
			Else
				If Ribbon.Dock <> DockStyle.Top Then
					Ribbon.Dock = DockStyle.Top
				End If
			End If
		End Sub
		Public Sub Form_Paint(sender As Object, e As PaintEventArgs)
			If DesignMode Then
				Return
			End If
			If WinApi.IsGlassEnabled Then
				WinApi.FillForGlass(e.Graphics, New Rectangle(0, 0, Form.Width, Form.Height))
				Using b As Brush = New SolidBrush(Form.BackColor)
					e.Graphics.FillRectangle(b, Rectangle.FromLTRB(Margins.Left - 0, Margins.Top + 0, Form.Width - Margins.Right - 0, Form.Height - Margins.Bottom - 0))
				End Using
			Else
				PaintTitleBar(e)
			End If
		End Sub
		Private Sub PaintTitleBar(e As PaintEventArgs)
			Dim radius1 As Integer = 4, radius2 As Integer = radius1 - 0
			Dim rPath As New Rectangle(Point.Empty, Form.Size)
			Dim rInner As New Rectangle(Point.Empty, New Size(rPath.Width - 1, rPath.Height - 1))
			Using path As GraphicsPath = RibbonProfessionalRenderer.RoundRectangle(rPath, radius1)
				Using innerPath As GraphicsPath = RibbonProfessionalRenderer.RoundRectangle(rInner, radius2)
					If Ribbon IsNot Nothing AndAlso Ribbon.ActualBorderMode = RibbonWindowMode.NonClientAreaCustomDrawn Then
						Dim renderer As RibbonProfessionalRenderer = TryCast(Ribbon.Renderer, RibbonProfessionalRenderer)
						If renderer IsNot Nothing Then
							Using p As New SolidBrush(renderer.ColorTable.Caption1)
								e.Graphics.FillRectangle(p, New Rectangle(0, 0, Form.Width, Ribbon.CaptionBarSize))
							End Using
							renderer.DrawCaptionBarBackground(New Rectangle(0, Margins.Bottom - 1, Form.Width, Ribbon.CaptionBarSize), e.Graphics)
							Using rgn As New Region(path)
								Form.Region = rgn
								Dim smbuf As SmoothingMode = e.Graphics.SmoothingMode
								e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
								Using p As New Pen(renderer.ColorTable.FormBorder, 1)
									e.Graphics.DrawPath(p, innerPath)
								End Using
								e.Graphics.SmoothingMode = smbuf
							End Using
						End If
					End If
				End Using
			End Using
		End Sub
		Protected Overridable Sub Form_Activated(sender As Object, e As EventArgs)
			If DesignMode Then
				Return
			End If
			Dim dwmMargins As New WinApi.MARGINS(Margins.Left, Margins.Right, Margins.Bottom + Ribbon.CaptionBarHeight, Margins.Bottom)
			If WinApi.IsVista AndAlso Not _frameExtended Then
				WinApi.DwmExtendFrameIntoClientArea(Form.Handle, dwmMargins)
				_frameExtended = True
			End If
		End Sub
		Public Overridable Function WndProc(ByRef m As Message) As Boolean
			If DesignMode Then
				Return False
			End If
			Dim handled As Boolean = False
			If WinApi.IsVista Then
				Dim result As IntPtr
				Dim dwmHandled As Integer = WinApi.DwmDefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam, result)
				If dwmHandled = 1 Then
					m.Result = result
					handled = True
				End If
			End If
			If Not handled Then
				If m.Msg = WinApi.WM_NCCALCSIZE AndAlso CType(m.WParam, Integer) = 1 Then
					Dim nccsp As WinApi.NCCALCSIZE_PARAMS = CType(Marshal.PtrToStructure(m.LParam, GetType(WinApi.NCCALCSIZE_PARAMS)), WinApi.NCCALCSIZE_PARAMS)
					If Not MarginsChecked Then
						SetMargins(New Padding(nccsp.rect2.Left - nccsp.rect1.Left, nccsp.rect2.Top - nccsp.rect1.Top, nccsp.rect1.Right - nccsp.rect2.Right, nccsp.rect1.Bottom - nccsp.rect2.Bottom))
						MarginsChecked = True
					End If
					Marshal.StructureToPtr(nccsp, m.LParam, False)
					m.Result = IntPtr.Zero
					handled = True
				ElseIf m.Msg = WinApi.WM_NCHITTEST AndAlso CType(m.Result, Integer) = 0 Then
					m.Result = New IntPtr(Convert.ToInt32(NonClientHitTest(New Point(WinApi.LoWord(CType(m.LParam, Integer)), WinApi.HiWord(CType(m.LParam, Integer))))))
					handled = True
					If Ribbon IsNot Nothing AndAlso Ribbon.ActualBorderMode = RibbonWindowMode.NonClientAreaCustomDrawn Then
						Form.Refresh()
					End If
				ElseIf (Ribbon IsNot Nothing AndAlso Ribbon.ActualBorderMode <> RibbonWindowMode.NonClientAreaCustomDrawn) AndAlso (m.Msg = &H112 OrElse m.Msg = &H47 OrElse m.Msg = &H46 OrElse m.Msg = &H2a2) Then
				End If
			End If
			Return handled
		End Function
		Public Overridable Function NonClientHitTest(hitPoint As Point) As NonClientHitTestResult
			Dim topleft As Rectangle = Form.RectangleToScreen(New Rectangle(0, 0, Margins.Left, Margins.Left))
			If topleft.Contains(hitPoint) Then
				Return NonClientHitTestResult.TopLeft
			End If
			Dim topright As Rectangle = Form.RectangleToScreen(New Rectangle(Form.Width - Margins.Right, 0, Margins.Right, Margins.Right))
			If topright.Contains(hitPoint) Then
				Return NonClientHitTestResult.TopRight
			End If
			Dim botleft As Rectangle = Form.RectangleToScreen(New Rectangle(0, Form.Height - Margins.Bottom, Margins.Left, Margins.Bottom))
			If botleft.Contains(hitPoint) Then
				Return NonClientHitTestResult.BottomLeft
			End If
			Dim botright As Rectangle = Form.RectangleToScreen(New Rectangle(Form.Width - Margins.Right, Form.Height - Margins.Bottom, Margins.Right, Margins.Bottom))
			If botright.Contains(hitPoint) Then
				Return NonClientHitTestResult.BottomRight
			End If
			Dim top As Rectangle = Form.RectangleToScreen(New Rectangle(0, 0, Form.Width, Margins.Left))
			If top.Contains(hitPoint) Then
				Return NonClientHitTestResult.Top
			End If
			Dim cap As Rectangle = Form.RectangleToScreen(New Rectangle(0, Margins.Left, Form.Width, Margins.Top - Margins.Left))
			If cap.Contains(hitPoint) Then
				Return NonClientHitTestResult.Caption
			End If
			Dim left As Rectangle = Form.RectangleToScreen(New Rectangle(0, 0, Margins.Left, Form.Height))
			If left.Contains(hitPoint) Then
				Return NonClientHitTestResult.Left
			End If
			Dim right As Rectangle = Form.RectangleToScreen(New Rectangle(Form.Width - Margins.Right, 0, Margins.Right, Form.Height))
			If right.Contains(hitPoint) Then
				Return NonClientHitTestResult.Right
			End If
			Dim bottom As Rectangle = Form.RectangleToScreen(New Rectangle(0, Form.Height - Margins.Bottom, Form.Width, Margins.Bottom))
			If bottom.Contains(hitPoint) Then
				Return NonClientHitTestResult.Bottom
			End If
			Return NonClientHitTestResult.Client
		End Function
		Private Sub SetMargins(p As Padding)
			_margins = p
			Dim formPadding As Padding = p
			formPadding.Top = p.Bottom - 1
			If Not DesignMode Then
				Form.Padding = formPadding
			End If
		End Sub
	End Class
End Namespace
