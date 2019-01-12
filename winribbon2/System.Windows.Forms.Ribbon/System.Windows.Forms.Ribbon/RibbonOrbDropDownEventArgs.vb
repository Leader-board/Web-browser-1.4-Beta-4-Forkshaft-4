Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Class RibbonOrbDropDownEventArgs
		Inherits RibbonRenderEventArgs
		Private _dropDown As RibbonOrbDropDown
		Public Sub New(ribbon As Ribbon, dropDown As RibbonOrbDropDown, g As Graphics, clip As Rectangle)
			MyBase.New(ribbon, g, clip)
			_dropDown = dropDown
		End Sub
		Public ReadOnly Property RibbonOrbDropDown() As RibbonOrbDropDown
			Get
				Return _dropDown
			End Get
		End Property
	End Class
End Namespace
