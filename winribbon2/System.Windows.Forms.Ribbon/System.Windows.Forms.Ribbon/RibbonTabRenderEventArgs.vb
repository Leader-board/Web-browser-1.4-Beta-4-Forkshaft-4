Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public NotInheritable Class RibbonTabRenderEventArgs
		Inherits RibbonRenderEventArgs
		Private _tab As RibbonTab
		Public Sub New(owner As Ribbon, g As Graphics, clip As Rectangle, tab As RibbonTab)
			MyBase.New(owner, g, clip)
			Tab = tab
		End Sub
		Public Property Tab() As RibbonTab
			Get
				Return _tab
			End Get
			Set
				_tab = value
			End Set
		End Property
	End Class
End Namespace
