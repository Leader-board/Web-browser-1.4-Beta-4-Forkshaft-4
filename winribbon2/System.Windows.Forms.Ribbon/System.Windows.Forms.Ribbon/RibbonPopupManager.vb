Imports System
Imports System.Collections.Generic
Imports System.Text
Namespace System.Windows.Forms
	Public       Module RibbonPopupManager
		Public Enum DismissReason
			ItemClicked
			AppClicked
			NewPopup
			AppFocusChanged
			EscapePressed
		End Enum
		Private  pops As List(Of RibbonPopup)
		Public Sub New()
			pops = New List(Of RibbonPopup)()
		End Sub
		Friend  ReadOnly Property LastPopup() As RibbonPopup
			Get
				If pops.Count > 0 Then
					Return pops(pops.Count - 1)
				End If
				Return Nothing
			End Get
		End Property
		Friend  Sub Register(p As RibbonPopup)
			If Not pops.Contains(p) Then
				pops.Add(p)
			End If
		End Sub
		Friend  Sub Unregister(p As RibbonPopup)
			If pops.Contains(p) Then
				pops.Remove(p)
			End If
		End Sub
		Friend  Sub FeedHookClick(e As MouseEventArgs)
			For Each p As RibbonPopup In pops
				If p.WrappedDropDown.Bounds.Contains(e.Location) Then
					Return
				End If
			Next
			Dismiss(DismissReason.AppClicked)
		End Sub
		Friend  Function FeedMouseWheel(e As MouseEventArgs) As Boolean
			Dim dd As RibbonDropDown = TryCast(LastPopup, RibbonDropDown)
			If dd IsNot Nothing Then
				For Each item As RibbonItem In dd.Items
					If dd.RectangleToScreen(item.Bounds).Contains(e.Location) Then
						Dim sc As IScrollableRibbonItem = TryCast(item, IScrollableRibbonItem)
						If sc IsNot Nothing Then
							If e.Delta < 0 Then
								sc.ScrollDown()
							Else
								sc.ScrollUp()
							End If
							Return True
						End If
					End If
				Next
			End If
			Return False
		End Function
		Public  Sub DismissChildren(parent As RibbonPopup, reason As DismissReason)
			Dim index As Integer = pops.IndexOf(parent)
			If index >= 0 Then
				Dismiss(index + 1, reason)
			End If
		End Sub
		Public  Sub Dismiss(reason As DismissReason)
			Dismiss(0, reason)
		End Sub
		Public  Sub Dismiss(startPopup As RibbonPopup, reason As DismissReason)
			Dim index As Integer = pops.IndexOf(startPopup)
			If index >= 0 Then
				Dismiss(index, reason)
			End If
		End Sub
		Private  Sub Dismiss(startPopup As Integer, reason As DismissReason)
			Dim i As Integer = pops.Count - 1
			While i >= startPopup
				pops(i).Close()
				System.Math.Max(System.Threading.Interlocked.Decrement(i),i + 1)
			End While
		End Sub
	End Module
End Namespace
