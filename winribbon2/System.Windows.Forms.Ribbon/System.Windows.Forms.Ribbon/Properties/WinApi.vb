Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Windows.Forms.VisualStyles
Namespace System.Windows.Forms.RibbonHelpers
	Public       Module WinApi
		Public Const WM_MOUSEFIRST As Integer = &H200
		Public Const WM_MOUSEMOVE As Integer = &H200
		Public Const WM_LBUTTONDOWN As Integer = &H201
		Public Const WM_LBUTTONUP As Integer = &H202
		Public Const WM_LBUTTONDBLCLK As Integer = &H203
		Public Const WM_RBUTTONDOWN As Integer = &H204
		Public Const WM_RBUTTONUP As Integer = &H205
		Public Const WM_RBUTTONDBLCLK As Integer = &H206
		Public Const WM_MBUTTONDOWN As Integer = &H207
		Public Const WM_MBUTTONUP As Integer = &H208
		Public Const WM_MBUTTONDBLCLK As Integer = &H209
		Public Const WM_MOUSEWHEEL As Integer = &H20a
		Public Const WM_XBUTTONDOWN As Integer = &H20b
		Public Const WM_XBUTTONUP As Integer = &H20c
		Public Const WM_XBUTTONDBLCLK As Integer = &H20d
		Public Const WM_MOUSELAST As Integer = &H20d
		Public Const WM_KEYDOWN As Integer = &H100
		Public Const WM_KEYUP As Integer = &H101
		Public Const WM_SYSKEYDOWN As Integer = &H104
		Public Const WM_SYSKEYUP As Integer = &H105
		Public Const VK_SHIFT As Byte = &H10
		Public Const VK_CAPITAL As Byte = &H14
		Public Const VK_NUMLOCK As Byte = &H90
		Private Const DTT_COMPOSITED As Integer = CType((1UL << 13), Integer)
		Private Const DTT_GLOWSIZE As Integer = CType((1UL << 11), Integer)
		Private Const DT_SINGLELINE As Integer = &H20
		Private Const DT_CENTER As Integer = &H1
		Private Const DT_VCENTER As Integer = &H4
		Private Const DT_NOPREFIX As Integer = &H800
		Public Const CS_DROPSHADOW As Integer = &H20000
		Public Const WH_MOUSE_LL As Integer = 14
		Public Const WH_KEYBOARD_LL As Integer = 13
		Public Const WH_MOUSE As Integer = 7
		Public Const WH_KEYBOARD As Integer = 2
		Public Const WM_NCLBUTTONUP As Integer = &Ha2
		Public Const WM_SIZE As Integer = &H5
		Public Const WM_ERASEBKGND As Integer = &H14
		Public Const WM_NCCALCSIZE As Integer = &H83
		Public Const WM_NCHITTEST As Integer = &H84
		Public Const WM_NCMOUSEMOVE As Integer = &Ha0
		Public Const WM_NCMOUSELEAVE As Integer = &H2a2
		Public Const BI_RGB As Integer = 0
		Public Const DIB_RGB_COLORS As Integer = 0
		Public Const SRCCOPY As Integer = &Hcc0020
		<DllImport("user32")> _
		Friend  Function GetCursorPos(ByRef lpPoint As POINT) As Boolean
		End Function
		<DllImport("user32")> _
		Friend  Function ToAscii(uVirtKey As Integer, uScanCode As Integer, lpbKeyState As Byte(), lpwTransKey As Byte(), fuState As Integer) As Integer
		End Function
		<DllImport("user32")> _
		Friend  Function GetKeyboardState(pbKeyState As Byte()) As Integer
		End Function
		<DllImport("user32.dll", CharSet := CharSet.Auto, CallingConvention := CallingConvention.StdCall)> _
		Friend  Function GetKeyState(vKey As Integer) As Short
		End Function
		<DllImport("user32.dll")> _
		Friend  Function SetWindowsHookEx(idHook As Integer, lpfn As GlobalHook.HookProcCallBack, hInstance As IntPtr, threadId As Integer) As Integer
		End Function
		<DllImport("user32.dll")> _
		Friend  Function UnhookWindowsHookEx(idHook As Integer) As Boolean
		End Function
		<DllImport("user32.dll")> _
		Friend  Function CallNextHookEx(idHook As Integer, nCode As Integer, wParam As IntPtr, lParam As IntPtr) As Integer
		End Function
		<DllImport("user32.dll")> _
		Friend  Function GetDC(hdc As IntPtr) As IntPtr
		End Function
		<DllImport("gdi32.dll")> _
		Friend  Function SaveDC(hdc As IntPtr) As Integer
		End Function
		<DllImport("user32.dll")> _
		Friend  Function ReleaseDC(hdc As IntPtr, state As Integer) As Integer
		End Function
		<DllImport("UxTheme.dll", CharSet := CharSet.Unicode)> _
		Private  Function DrawThemeTextEx(hTheme As IntPtr, hdc As IntPtr, iPartId As Integer, iStateId As Integer, text As String, iCharCount As Integer, _
			dwFlags As Integer, ByRef pRect As RECT, ByRef pOptions As DTTOPTS) As Integer
		End Function
		<DllImport("UxTheme.dll")> _
		Friend  Function DrawThemeText(hTheme As IntPtr, hdc As IntPtr, iPartId As Integer, iStateId As Integer, text As String, iCharCount As Integer, _
			dwFlags1 As Integer, dwFlags2 As Integer, ByRef pRect As RECT) As Integer
		End Function
		<DllImport("gdi32.dll")> _
		Private  Function CreateDIBSection(hdc As IntPtr, ByRef pbmi As BITMAPINFO, iUsage As UInteger, ppvBits As Integer, hSection As IntPtr, dwOffset As UInteger) As IntPtr
		End Function
		<DllImport("gdi32.dll")> _
		Friend  Function BitBlt(hdc As IntPtr, nXDest As Integer, nYDest As Integer, nWidth As Integer, nHeight As Integer, hdcSrc As IntPtr, _
			nXSrc As Integer, nYSrc As Integer, dwRop As UInteger) As Boolean
		End Function
		<DllImport("gdi32.dll")> _
		Friend  Function CreateCompatibleDC(hDC As IntPtr) As IntPtr
		End Function
		<DllImport("gdi32.dll")> _
		Friend  Function SelectObject(hDC As IntPtr, hObject As IntPtr) As IntPtr
		End Function
		<DllImport("gdi32.dll")> _
		Friend  Function DeleteObject(hObject As IntPtr) As Boolean
		End Function
		<DllImport("gdi32.dll")> _
		Friend  Function DeleteDC(hdc As IntPtr) As Boolean
		End Function
		<DllImport("dwmapi.dll")> _
		Friend  Function DwmExtendFrameIntoClientArea(hdc As IntPtr, ByRef marInset As MARGINS) As Integer
		End Function
		<DllImport("dwmapi.dll")> _
		Friend  Function DwmDefWindowProc(hwnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As IntPtr, ByRef result As IntPtr) As Integer
		End Function
		<DllImport("dwmapi.dll")> _
		Friend  Function DwmIsCompositionEnabled(ByRef pfEnabled As Integer) As Integer
		End Function
		<DllImport("user32.dll", CharSet := CharSet.Auto, SetLastError := False)> _
		Friend  Function SendMessage(hWnd As IntPtr, Msg As Int32, wParam As IntPtr, lParam As IntPtr) As IntPtr
		End Function
		<StructLayout(LayoutKind.Sequential)> _
		Friend Class MouseLLHookStruct
			Public pt As POINT
			Public mouseData As Integer
			Public flags As Integer
			Public time As Integer
			Public extraInfo As Integer
		End Class
		<StructLayout(LayoutKind.Sequential)> _
		Friend Class KeyboardLLHookStruct
			Public vkCode As Integer
			Public scanCode As Integer
			Public flags As Integer
			Public time As Integer
			Public dwExtraInfo As Integer
		End Class
		<StructLayout(LayoutKind.Sequential)> _
		Friend Class MouseHookStruct
			Public pt As POINT
			Public hwnd As Integer
			Public wHitTestCode As Integer
			Public dwExtraInfo As Integer
		End Class
		Friend Structure POINT
			Public x As Integer
			Public y As Integer
		End Structure
		<StructLayout(LayoutKind.Sequential)> _
		Friend Structure DTTOPTS
			Public dwSize As UInteger
			Public dwFlags As UInteger
			Public crText As UInteger
			Public crBorder As UInteger
			Public crShadow As UInteger
			Public iTextShadowType As Integer
			Public ptShadowOffset As POINT
			Public iBorderSize As Integer
			Public iFontPropId As Integer
			Public iColorPropId As Integer
			Public iStateId As Integer
			Public fApplyOverlay As Integer
			Public iGlowSize As Integer
			Public pfnDrawTextCallback As IntPtr
			Public lParam As Integer
		End Structure
		<StructLayout(LayoutKind.Sequential)> _
		Private Structure RGBQUAD
			Public rgbBlue As Byte
			Public rgbGreen As Byte
			Public rgbRed As Byte
			Public rgbReserved As Byte
		End Structure
		<StructLayout(LayoutKind.Sequential)> _
		Private Structure BITMAPINFOHEADER
			Public biSize As Integer
			Public biWidth As Integer
			Public biHeight As Integer
			Public biPlanes As Short
			Public biBitCount As Short
			Public biCompression As Integer
			Public biSizeImage As Integer
			Public biXPelsPerMeter As Integer
			Public biYPelsPerMeter As Integer
			Public biClrUsed As Integer
			Public biClrImportant As Integer
		End Structure
		<StructLayout(LayoutKind.Sequential)> _
		Private Structure BITMAPINFO
			Public bmiHeader As BITMAPINFOHEADER
			Public bmiColors As RGBQUAD
		End Structure
		<StructLayout(LayoutKind.Sequential)> _
		Public Structure RECT
			Public Left As Integer
			Public Top As Integer
			Public Right As Integer
			Public Bottom As Integer
		End Structure
		<StructLayout(LayoutKind.Sequential)> _
		Public Structure NCCALCSIZE_PARAMS
			Public rect0 As RECT, rect1 As RECT, rect2 As RECT
			Public lppos As IntPtr
		End Structure
		<StructLayout(LayoutKind.Sequential)> _
		Friend Structure MARGINS
			Public cxLeftWidth As Integer
			Public cxRightWidth As Integer
			Public cyTopHeight As Integer
			Public cyBottomHeight As Integer
			Public Sub New(Left As Integer, Right As Integer, Top As Integer, Bottom As Integer)
				Me.cxLeftWidth = Left
				Me.cxRightWidth = Right
				Me.cyTopHeight = Top
				Me.cyBottomHeight = Bottom
			End Sub
		End Structure
		Public  ReadOnly Property IsWindows() As Boolean
			Get
				Return Environment.OSVersion.Platform = PlatformID.Win32NT
			End Get
		End Property
		Public  ReadOnly Property IsVista() As Boolean
			Get
				Return IsWindows AndAlso Environment.OSVersion.Version.Major >= 6
			End Get
		End Property
		Public  ReadOnly Property IsXP() As Boolean
			Get
				Return IsWindows AndAlso Environment.OSVersion.Version.Major >= 5
			End Get
		End Property
		Public  ReadOnly Property IsGlassEnabled() As Boolean
			Get
				If IsVista Then
					Dim enabled As Integer = 0
					Dim response As Integer = DwmIsCompositionEnabled(enabled)
					Return enabled > 0
				End If
				Return False
			End Get
		End Property
		Public  Function HiWord(dwValue As Integer) As Integer
			Return (dwValue >> 16) And &Hffff
		End Function
		Public  Function LoWord(dwValue As Integer) As Integer
			Return dwValue And &Hffff
		End Function
		Public  Function MakeLParam(LoWord As Integer, HiWord As Integer) As IntPtr
			Return New IntPtr((HiWord << 16) Or (LoWord And &Hffff))
		End Function
		Public  Sub FillForGlass(g As Graphics, r As Rectangle)
			Dim rc As New RECT()
			rc.Left = r.Left
			rc.Right = r.Right
			rc.Top = r.Top
			rc.Bottom = r.Bottom
			Dim destdc As IntPtr = g.GetHdc()
			Dim Memdc As IntPtr = CreateCompatibleDC(destdc)
			Dim bitmap As IntPtr
			Dim bitmapOld As IntPtr = IntPtr.Zero
			Dim dib As New BITMAPINFO()
			dib.bmiHeader.biHeight = -(rc.Bottom - rc.Top)
			dib.bmiHeader.biWidth = rc.Right - rc.Left
			dib.bmiHeader.biPlanes = 1
			dib.bmiHeader.biSize = Marshal.SizeOf(GetType(BITMAPINFOHEADER))
			dib.bmiHeader.biBitCount = 32
			dib.bmiHeader.biCompression = BI_RGB
			If Not (SaveDC(Memdc) = 0) Then
				bitmap = CreateDIBSection(Memdc, dib, DIB_RGB_COLORS, 0, IntPtr.Zero, 0)
				If Not (bitmap = IntPtr.Zero) Then
					bitmapOld = SelectObject(Memdc, bitmap)
					BitBlt(destdc, rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top, Memdc, _
						0, 0, SRCCOPY)
				End If
				SelectObject(Memdc, bitmapOld)
				DeleteObject(bitmap)
				ReleaseDC(Memdc, -1)
				DeleteDC(Memdc)
			End If
			g.ReleaseHdc()
		End Sub
		Public  Sub DrawTextOnGlass(hwnd As IntPtr, text As [String], font As Font, ctlrct As Rectangle, iglowSize As Integer)
			If IsGlassEnabled Then
				Dim rc As New RECT()
				Dim rc2 As New RECT()
				rc.Left = ctlrct.Left
				rc.Right = ctlrct.Right
				rc.Top = ctlrct.Top
				rc.Bottom = ctlrct.Bottom
				rc2.Left = 0
				rc2.Top = 0
				rc2.Right = rc.Right - rc.Left
				rc2.Bottom = rc.Bottom - rc.Top
				Dim destdc As IntPtr = GetDC(hwnd)
				Dim Memdc As IntPtr = CreateCompatibleDC(destdc)
				Dim bitmap As IntPtr
				Dim bitmapOld As IntPtr = IntPtr.Zero
				Dim logfnotOld As IntPtr
				Dim uFormat As Integer = DT_SINGLELINE Or DT_CENTER Or DT_VCENTER Or DT_NOPREFIX
				Dim dib As New BITMAPINFO()
				dib.bmiHeader.biHeight = -(rc.Bottom - rc.Top)
				dib.bmiHeader.biWidth = rc.Right - rc.Left
				dib.bmiHeader.biPlanes = 1
				dib.bmiHeader.biSize = Marshal.SizeOf(GetType(BITMAPINFOHEADER))
				dib.bmiHeader.biBitCount = 32
				dib.bmiHeader.biCompression = BI_RGB
				If Not (SaveDC(Memdc) = 0) Then
					bitmap = CreateDIBSection(Memdc, dib, DIB_RGB_COLORS, 0, IntPtr.Zero, 0)
					If Not (bitmap = IntPtr.Zero) Then
						bitmapOld = SelectObject(Memdc, bitmap)
						Dim hFont As IntPtr = font.ToHfont()
						logfnotOld = SelectObject(Memdc, hFont)
						Try
							Dim renderer As New VisualStyleRenderer(VisualStyleElement.Window.Caption.Active)
							Dim dttOpts As New DTTOPTS()
							dttOpts.dwSize = CType(Marshal.SizeOf(GetType(DTTOPTS)), UInteger)
							dttOpts.dwFlags = DTT_COMPOSITED Or DTT_GLOWSIZE
							dttOpts.iGlowSize = iglowSize
							Dim dtter As Integer = DrawThemeTextEx(renderer.Handle, Memdc, 0, 0, text, -1, _
								uFormat, rc2, dttOpts)
							Dim bbr As Boolean = BitBlt(destdc, rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top, Memdc, _
								0, 0, SRCCOPY)
							If Not bbr Then
							End If
						Catch e As Exception
							Console.WriteLine(e.ToString())
						End Try
						SelectObject(Memdc, bitmapOld)
						SelectObject(Memdc, logfnotOld)
						DeleteObject(bitmap)
						DeleteObject(hFont)
						ReleaseDC(Memdc, -1)
						DeleteDC(Memdc)
					Else
					End If
				Else
				End If
			End If
		End Sub
	End Module
End Namespace
