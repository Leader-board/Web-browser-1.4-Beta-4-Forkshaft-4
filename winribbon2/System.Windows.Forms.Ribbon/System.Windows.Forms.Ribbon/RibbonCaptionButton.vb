Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms.RibbonHelpers
Namespace System.Windows.Forms
	Public Class RibbonCaptionButton
		Inherits RibbonButton
		Public Enum CaptionButton
			Minimize
			Maximize
			Restore
			Close
		End Enum
		Public Shared Function GetCharFor(type As CaptionButton) As String
			If WinApi.IsWindows Then
				Select Case type
					Case CaptionButton.Minimize
						Return "0"
					Case CaptionButton.Maximize
						Return "1"
					Case CaptionButton.Restore
						Return "2"
					Case CaptionButton.Close
						Return "r"
					Case Else
						Return "?"
				End Select
			Else
				Select Case type
					Case CaptionButton.Minimize
						Return "_"
					Case CaptionButton.Maximize
						Return "+"
					Case CaptionButton.Restore
						Return "^"
					Case CaptionButton.Close
						Return "X"
					Case Else
						Return "?"
				End Select
			End If
		End Function
		Private _captionButtonType As CaptionButton
		Public Sub New(buttonType As CaptionButton)
			SetCaptionButtonType(buttonType)
		End Sub
		Public ReadOnly Property CaptionButtonType() As CaptionButton
			Get
				Return _captionButtonType
			End Get
		End Property
		Public Overrides Sub OnClick(e As EventArgs)
			MyBase.OnClick(e)
			Dim f As Form = Owner.FindForm()
			If f Is Nothing Then
				Return
			End If
			Select Case CaptionButtonType
				Case CaptionButton.Minimize
					f.WindowState = FormWindowState.Minimized
					Exit Select
				Case CaptionButton.Maximize
					f.WindowState = FormWindowState.Maximized
					Exit Select
				Case CaptionButton.Restore
					f.WindowState = FormWindowState.Normal
					Exit Select
				Case CaptionButton.Close
					f.Close()
					Exit Select
				Case Else
					Exit Select
			End Select
		End Sub
		Friend Sub SetCaptionButtonType(buttonType As CaptionButton)
			Text = GetCharFor(buttonType)
			_captionButtonType = buttonType
		End Sub
		Friend Overrides Function OnGetTextBounds(sMode As RibbonElementSizeMode, bounds As Rectangle) As Rectangle
			Dim r As Rectangle = bounds
			r.X = bounds.Left + 3
			Return r
		End Function
	End Class
End Namespace
