Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Reflection
Imports System.ComponentModel
Namespace System.Windows.Forms.RibbonHelpers
	Public Class GlobalHook
		Implements IDisposable
		Public Enum HookTypes
			Mouse
			Keyboard
		End Enum
		Private _HookProc As HookProcCallBack
		Private _hHook As Integer
		Private _hookType As HookTypes
		Public Event MouseClick As MouseEventHandler
		Public Event MouseDoubleClick As MouseEventHandler
		Public Event MouseWheel As MouseEventHandler
		Public Event MouseDown As MouseEventHandler
		Public Event MouseUp As MouseEventHandler
		Public Event MouseMove As MouseEventHandler
		Public Event KeyDown As KeyEventHandler
		Public Event KeyUp As KeyEventHandler
		Public Event KeyPress As KeyPressEventHandler
		Friend Delegate Function HookProcCallBack(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
		Public Sub New(hookType As HookTypes)
			_hookType = hookType
			InstallHook()
		End Sub
		Protected Overrides Sub Finalize()
			Try
				If Handle <> 0 Then
					Unhook()
				End If
			Finally
				MyBase.Finalize()
			End Try
		End Sub
		Public ReadOnly Property HookType() As HookTypes
			Get
				Return _hookType
			End Get
		End Property
		Public ReadOnly Property Handle() As Integer
			Get
				Return _hHook
			End Get
		End Property
		Protected Overridable Sub OnMouseClick(e As MouseEventArgs)
			If MouseClick IsNot Nothing Then
				MouseClick(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnMouseDoubleClick(e As MouseEventArgs)
			If MouseDoubleClick IsNot Nothing Then
				MouseDoubleClick(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnMouseWheel(e As MouseEventArgs)
			If MouseWheel IsNot Nothing Then
				MouseWheel(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnMouseDown(e As MouseEventArgs)
			If MouseDown IsNot Nothing Then
				MouseDown(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnMouseUp(e As MouseEventArgs)
			If MouseUp IsNot Nothing Then
				MouseUp(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnMouseMove(e As MouseEventArgs)
			If MouseMove IsNot Nothing Then
				MouseMove(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnKeyDown(e As KeyEventArgs)
			If KeyDown IsNot Nothing Then
				KeyDown(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnKeyUp(e As KeyEventArgs)
			If KeyUp IsNot Nothing Then
				KeyUp(Me, e)
			End If
		End Sub
		Protected Overridable Sub OnKeyPress(e As KeyPressEventArgs)
			If KeyPress IsNot Nothing Then
				KeyPress(Me, e)
			End If
		End Sub
		Private Function HookProc(code As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
			If code < 0 Then
				Return WinApi.CallNextHookEx(Handle, code, wParam, lParam)
			Else
				Select Case HookType
					Case HookTypes.Mouse
						Return MouseProc(code, wParam, lParam)
					Case HookTypes.Keyboard
						Return KeyboardProc(code, wParam, lParam)
					Case Else
						Throw New Exception("HookType not supported")
				End Select
			End If
		End Function
		Private Function KeyboardProc(code As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
			Dim hookStruct As WinApi.KeyboardLLHookStruct = CType(Marshal.PtrToStructure(lParam, GetType(WinApi.KeyboardLLHookStruct)), WinApi.KeyboardLLHookStruct)
			Dim msg As Integer = wParam.ToInt32()
			Dim handled As Boolean = False
			If msg = WinApi.WM_KEYDOWN OrElse msg = WinApi.WM_SYSKEYDOWN Then
				Dim e As New KeyEventArgs(CType(hookStruct.vkCode, Keys))
				OnKeyDown(e)
				handled = e.Handled
			ElseIf msg = WinApi.WM_KEYUP OrElse msg = WinApi.WM_SYSKEYUP Then
				Dim e As New KeyEventArgs(CType(hookStruct.vkCode, Keys))
				OnKeyUp(e)
				handled = e.Handled
			End If
			If msg = WinApi.WM_KEYDOWN AndAlso KeyPress IsNot Nothing Then
				Dim keyState As Byte() = New Byte(256) {}
				Dim buffer As Byte() = New Byte(2) {}
				WinApi.GetKeyboardState(keyState)
				Dim conversion As Integer = WinApi.ToAscii(hookStruct.vkCode, hookStruct.scanCode, keyState, buffer, hookStruct.flags)
				If conversion = 1 OrElse conversion = 2 Then
					Dim shift As Boolean = (WinApi.GetKeyState(WinApi.VK_SHIFT) And &H80) = &H80
					Dim capital As Boolean = WinApi.GetKeyState(WinApi.VK_CAPITAL) <> 0
					Dim c As Char = CType(buffer(0), Char)
					If (shift Xor capital) AndAlso [Char].IsLetter(c) Then
						c = [Char].ToUpper(c)
					End If
					Dim e As New KeyPressEventArgs(c)
					OnKeyPress(e)
					handled = handled Or e.Handled
				End If
			End If
			Return If(handled, 1, WinApi.CallNextHookEx(Handle, code, wParam, lParam))
		End Function
		Private Function MouseProc(code As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
			Dim hookStruct As WinApi.MouseLLHookStruct = CType(Marshal.PtrToStructure(lParam, GetType(WinApi.MouseLLHookStruct)), WinApi.MouseLLHookStruct)
			Dim msg As Integer = wParam.ToInt32()
			Dim x As Integer = hookStruct.pt.x
			Dim y As Integer = hookStruct.pt.y
			Dim delta As Integer = CType(((hookStruct.mouseData >> 16) And &Hffff), Short)
			If msg = WinApi.WM_MOUSEWHEEL Then
				OnMouseWheel(New MouseEventArgs(MouseButtons.None, 0, x, y, delta))
			ElseIf msg = WinApi.WM_MOUSEMOVE Then
				OnMouseMove(New MouseEventArgs(MouseButtons.None, 0, x, y, delta))
			ElseIf msg = WinApi.WM_LBUTTONDBLCLK Then
				OnMouseDoubleClick(New MouseEventArgs(MouseButtons.Left, 0, x, y, delta))
			ElseIf msg = WinApi.WM_LBUTTONDOWN Then
				OnMouseDown(New MouseEventArgs(MouseButtons.Left, 0, x, y, delta))
			ElseIf msg = WinApi.WM_LBUTTONUP Then
				OnMouseUp(New MouseEventArgs(MouseButtons.Left, 0, x, y, delta))
				OnMouseClick(New MouseEventArgs(MouseButtons.Left, 0, x, y, delta))
			ElseIf msg = WinApi.WM_MBUTTONDBLCLK Then
				OnMouseDoubleClick(New MouseEventArgs(MouseButtons.Middle, 0, x, y, delta))
			ElseIf msg = WinApi.WM_MBUTTONDOWN Then
				OnMouseDown(New MouseEventArgs(MouseButtons.Middle, 0, x, y, delta))
			ElseIf msg = WinApi.WM_MBUTTONUP Then
				OnMouseUp(New MouseEventArgs(MouseButtons.Middle, 0, x, y, delta))
			ElseIf msg = WinApi.WM_RBUTTONDBLCLK Then
				OnMouseDoubleClick(New MouseEventArgs(MouseButtons.Right, 0, x, y, delta))
			ElseIf msg = WinApi.WM_RBUTTONDOWN Then
				OnMouseDown(New MouseEventArgs(MouseButtons.Right, 0, x, y, delta))
			ElseIf msg = WinApi.WM_RBUTTONUP Then
				OnMouseUp(New MouseEventArgs(MouseButtons.Right, 0, x, y, delta))
			ElseIf msg = WinApi.WM_XBUTTONDBLCLK Then
				OnMouseDoubleClick(New MouseEventArgs(MouseButtons.XButton1, 0, x, y, delta))
			ElseIf msg = WinApi.WM_XBUTTONDOWN Then
				OnMouseDown(New MouseEventArgs(MouseButtons.XButton1, 0, x, y, delta))
			ElseIf msg = WinApi.WM_XBUTTONUP Then
				OnMouseUp(New MouseEventArgs(MouseButtons.XButton1, 0, x, y, delta))
			End If
			Return WinApi.CallNextHookEx(Handle, code, wParam, lParam)
		End Function
		Private Sub InstallHook()
			If Handle <> 0 Then
				Throw New Exception("Hook is already installed")
			End If
			Dim htype As Integer = 0
			Select Case HookType
				Case HookTypes.Mouse
					htype = WinApi.WH_MOUSE_LL
					Exit Select
				Case HookTypes.Keyboard
					htype = WinApi.WH_KEYBOARD_LL
					Exit Select
				Case Else
					Throw New Exception("HookType is not supported")
			End Select
			_HookProc = New HookProcCallBack(HookProc)
			_hHook = WinApi.SetWindowsHookEx(htype, _HookProc, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()(0)), 0)
			If Handle = 0 Then
				Throw New Win32Exception(Marshal.GetLastWin32Error())
			End If
		End Sub
		Private Sub Unhook()
			If Handle <> 0 Then
				Dim ret As Boolean = WinApi.UnhookWindowsHookEx(Handle)
				If ret = False Then
					Throw New Win32Exception(Marshal.GetLastWin32Error())
				End If
				_hHook = 0
			End If
		End Sub
		Public Sub Dispose()
			If Handle <> 0 Then
				Unhook()
			End If
		End Sub
	End Class
End Namespace
