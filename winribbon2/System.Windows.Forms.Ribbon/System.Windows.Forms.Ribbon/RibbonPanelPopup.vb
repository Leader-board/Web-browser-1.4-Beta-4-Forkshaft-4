Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Namespace System.Windows.Forms
	<ToolboxItem(False)> _
	Public Partial Class RibbonPanelPopup
		Inherits RibbonPopup
		Private _sensor As RibbonMouseSensor
		Private _panel As RibbonPanel
		Private _ignoreNext As Boolean
		Friend Sub New(panel As RibbonPanel)
			DoubleBuffered = True
			_sensor = New RibbonMouseSensor(Me, panel.Owner, panel.Items)
			_sensor.PanelLimit = panel
			_panel = panel
			_panel.PopUp = Me
			panel.Owner.SuspendSensor()
			Using g As Graphics = CreateGraphics()
				panel.overflowBoundsBuffer = panel.Bounds
				Dim s As Size = panel.SwitchToSize(Me, g, GetSizeMode(panel))
				s.Width += 100
				s.Height += 100
				Size = s
			End Using
			For Each item As RibbonItem In panel.Items
				item.SetCanvas(Me)
			Next
		End Sub
		Public ReadOnly Property Sensor() As RibbonMouseSensor
			Get
				Return _sensor
			End Get
		End Property
		Public ReadOnly Property Panel() As RibbonPanel
			Get
				Return _panel
			End Get
		End Property
		Public Function GetSizeMode(pnl As RibbonPanel) As RibbonElementSizeMode
			If pnl.FlowsTo = RibbonPanelFlowDirection.Right Then
				Return RibbonElementSizeMode.Medium
			Else
				Return RibbonElementSizeMode.Large
			End If
		End Function
		Public Sub IgnoreNextClickDeactivation()
			_ignoreNext = True
		End Sub
		Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
			MyBase.OnMouseDown(e)
		End Sub
		Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
			MyBase.OnMouseUp(e)
			If _ignoreNext Then
				_ignoreNext = False
				Return
			End If
		End Sub
		Protected Overrides Sub OnPaint(e As PaintEventArgs)
			MyBase.OnPaint(e)
			Panel.Owner.Renderer.OnRenderPanelPopupBackground(New RibbonCanvasEventArgs(Panel.Owner, e.Graphics, New Rectangle(Point.Empty, ClientSize), Me, Panel))
			For Each item As RibbonItem In Panel.Items
				item.OnPaint(Me, New RibbonElementPaintEventArgs(e.ClipRectangle, e.Graphics, RibbonElementSizeMode.Large))
			Next
			Panel.Owner.Renderer.OnRenderRibbonPanelText(New RibbonPanelRenderEventArgs(Panel.Owner, e.Graphics, e.ClipRectangle, Panel, Me))
		End Sub
		Protected Overrides Sub OnClosed(e As EventArgs)
			For Each item As RibbonItem In _panel.Items
				item.SetCanvas(Nothing)
			Next
			Panel.SetPressed(False)
			Panel.SetSelected(False)
			Panel.Owner.UpdateRegions()
			Panel.Owner.Refresh()
			Panel.PopUp = Nothing
			Panel.Owner.ResumeSensor()
			Panel.PopupShowed = False
			Panel.Owner.RedrawArea(Panel.Bounds)
		End Sub
	End Class
End Namespace
