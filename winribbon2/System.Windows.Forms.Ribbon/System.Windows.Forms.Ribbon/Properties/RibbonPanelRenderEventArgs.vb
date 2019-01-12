Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public NotInheritable Class RibbonPanelRenderEventArgs
		Inherits RibbonRenderEventArgs
		Private _panel As RibbonPanel
		Public Sub New(owner As Ribbon, g As Graphics, clip As Rectangle, panel As RibbonPanel, canvas As Control)
			MyBase.New(owner, g, clip)
			Panel = panel
			Canvas = canvas
		End Sub
		Public Property Panel() As RibbonPanel
			Get
				Return _panel
			End Get
			Set
				_panel = value
			End Set
		End Property
		Private _canvas As Control
		Public Property Canvas() As Control
			Get
				Return _canvas
			End Get
			Set
				_canvas = value
			End Set
		End Property
	End Class
End Namespace
