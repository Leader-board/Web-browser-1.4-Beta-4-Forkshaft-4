Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Namespace System.Windows.Forms
	<ToolboxItem(False)> _
	Public Class RibbonContext
		Inherits Component
		Private _text As String
		Private _glowColor As System.Drawing.Color
		Private _owner As Ribbon
		Private _tabs As RibbonTabCollection
		Public Sub New(owner As Ribbon)
			_tabs = New RibbonTabCollection(owner)
		End Sub
		Public Property Text() As String
			Get
				Return _text
			End Get
			Set
				_text = value
			End Set
		End Property
		Public Property GlowColor() As System.Drawing.Color
			Get
				Return _glowColor
			End Get
			Set
				_glowColor = value
			End Set
		End Property
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Owner() As Ribbon
			Get
				Return _owner
			End Get
		End Property
		Public ReadOnly Property Tabs() As RibbonTabCollection
			Get
				Return _tabs
			End Get
		End Property
		Friend Sub SetOwner(owner As Ribbon)
			_owner = owner
			_tabs.SetOwner(owner)
		End Sub
	End Class
End Namespace
