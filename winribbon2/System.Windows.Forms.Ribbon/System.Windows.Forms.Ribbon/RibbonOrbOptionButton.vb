Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Namespace System.Windows.Forms
	Public Class RibbonOrbOptionButton
		Inherits RibbonButton
		Public Sub New()
			MyBase.New()
		End Sub
		Public Sub New(text As String)
			Me.New()
			Text = text
		End Sub
		Public Overrides Property Image() As System.Drawing.Image
			Get
				Return MyBase.Image
			End Get
			Set
				MyBase.Image = value
				SmallImage = value
			End Set
		End Property
		<Browsable(False)> _
		Public Overrides Property SmallImage() As System.Drawing.Image
			Get
				Return MyBase.SmallImage
			End Get
			Set
				MyBase.SmallImage = value
			End Set
		End Property
	End Class
End Namespace
