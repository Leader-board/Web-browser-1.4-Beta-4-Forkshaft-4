Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms.RibbonHelpers
Namespace System.Windows.Forms
	Public Class RibbonForm
		Inherits Form
		Implements IRibbonForm
		Private _helper As RibbonFormHelper
		Public Sub New()
			If WinApi.IsWindows AndAlso Not WinApi.IsGlassEnabled Then
				SetStyle(ControlStyles.ResizeRedraw, True)
				SetStyle(ControlStyles.Opaque, WinApi.IsGlassEnabled)
				SetStyle(ControlStyles.AllPaintingInWmPaint, True)
				DoubleBuffered = True
			End If
			_helper = New RibbonFormHelper(Me)
		End Sub
		Protected Overrides Sub OnNotifyMessage(m As Message)
			MyBase.OnNotifyMessage(m)
			Console.WriteLine(m.ToString())
		End Sub
		Protected Overrides Sub WndProc(ByRef m As Message)
			If Not Helper.WndProc(m) Then
				MyBase.WndProc(m)
			End If
		End Sub
		Public ReadOnly Property Helper() As RibbonFormHelper
			Get
				Return _helper
			End Get
		End Property
	End Class
End Namespace
