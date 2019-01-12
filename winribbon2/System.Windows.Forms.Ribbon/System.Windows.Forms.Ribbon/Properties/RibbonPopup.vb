Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.RibbonHelpers
Namespace System.Windows.Forms
	<ToolboxItemAttribute(False)> _
	Public Class RibbonPopup
		Inherits Control
		Private _toolStripDropDown As RibbonWrappedDropDown
		Private _borderRoundness As Integer
		Public Event Showed As EventHandler
		Public Event Closed As EventHandler
		Public Event Closing As ToolStripDropDownClosingEventHandler
		Public Event Opening As CancelEventHandler
		Public Sub New()
			SetStyle(ControlStyles.Opaque, True)
			SetStyle(ControlStyles.AllPaintingInWmPaint, True)
			SetStyle(ControlStyles.Selectable, False)
			BorderRoundness = 3
		End Sub
		<Browsable(False)> _
		Public Property BorderRoundness() As Integer
			Get
				Return _borderRoundness
			End Get
			Set
				_borderRoundness = value
			End Set
		End Property
		Friend Property WrappedDropDown() As RibbonWrappedDropDown
			Get
				Return _toolStripDropDown
			End Get
			Set
				_toolStripDropDown = value
			End Set
		End Property
		Public Sub Show(screenLocation As Point)
			Dim host As New ToolStripControlHost(Me)
			WrappedDropDown = New RibbonWrappedDropDown()
			WrappedDropDown.AutoClose = RibbonDesigner.Current <> Nothing
			WrappedDropDown.Items.Add(host)
			WrappedDropDown.Padding = Padding.Empty
			WrappedDropDown.Margin = Padding.Empty
			host.Padding = Padding.Empty
			host.Margin = Padding.Empty
			WrappedDropDown.Opening += New CancelEventHandler(ToolStripDropDown_Opening)
			WrappedDropDown.Closing += New ToolStripDropDownClosingEventHandler(ToolStripDropDown_Closing)
			WrappedDropDown.Closed += New ToolStripDropDownClosedEventHandler(ToolStripDropDown_Closed)
			WrappedDropDown.Size = Size
			WrappedDropDown.Show(screenLocation)
			RibbonPopupManager.Register(Me)
			OnShowed(EventArgs.Empty)
		End Sub
		Private Sub ToolStripDropDown_Opening(sender As Object, e As CancelEventArgs)
			OnOpening(e)
		End Sub
		Protected Overridable Sub OnOpening(e As CancelEventArgs)
			If Opening IsNot Nothing Then
				Opening(Me, e)
			End If
		End Sub
		Private Sub ToolStripDropDown_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs)
			OnClosing(e)
		End Sub
		Private Sub ToolStripDropDown_Closed(sender As Object, e As ToolStripDropDownClosedEventArgs)
			OnClosed(EventArgs.Empty)
		End Sub
		Public Sub Close()
			If WrappedDropDown IsNot Nothing Then
				WrappedDropDown.Close()
			End If
		End Sub
		Protected Overridable Sub OnClosing(e As ToolStripDropDownClosingEventArgs)
			If Closing IsNot Nothing Then
				Closing(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnClosed(e As EventArgs)
			RibbonPopupManager.Unregister(Me)
			If Closed IsNot Nothing Then
				Closed(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnShowed(e As EventArgs)
			If Showed IsNot Nothing Then
				Showed(Me, e)
			End If
		End Sub
		Protected Overrides Sub OnPaint(e As PaintEventArgs)
			MyBase.OnPaint(e)
			Using p As GraphicsPath = RibbonProfessionalRenderer.RoundRectangle(New Rectangle(Point.Empty, Size), BorderRoundness)
				Using r As New Region(p)
					WrappedDropDown.Region = r
				End Using
			End Using
		End Sub
		Protected Overrides ReadOnly Property CreateParams() As CreateParams
			Get
				Dim cp As CreateParams = MyBase.CreateParams
				If WinApi.IsXP Then
					cp.ClassStyle = cp.ClassStyle Or WinApi.CS_DROPSHADOW
				End If
				Return cp
			End Get
		End Property
	End Class
End Namespace
