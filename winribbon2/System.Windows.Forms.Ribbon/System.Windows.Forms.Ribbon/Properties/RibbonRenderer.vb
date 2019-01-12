Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Imaging
Namespace System.Windows.Forms
	Public Class RibbonRenderer
		Private Shared _disabledImageColorMatrix As ColorMatrix
		Private Shared ReadOnly Property DisabledImageColorMatrix() As ColorMatrix
			Get
				If _disabledImageColorMatrix Is Nothing Then
					Dim numArray As Single()() = New Single(5)() {}
					numArray(0) = New Single() {0.2125F, 0.2125F, 0.2125F, 0F, 0F}
					numArray(1) = New Single() {0.2577F, 0.2577F, 0.2577F, 0F, 0F}
					numArray(2) = New Single() {0.0361F, 0.0361F, 0.0361F, 0F, 0F}
					Dim numArray3 As Single() = New Single(5) {}
					numArray3(3) = 1F
					numArray(3) = numArray3
					numArray(4) = New Single() {0.38F, 0.38F, 0.38F, 0F, 1F}
					Dim numArray2 As Single()() = New Single(5)() {}
					Dim numArray4 As Single() = New Single(5) {}
					numArray4(0) = 1F
					numArray2(0) = numArray4
					Dim numArray5 As Single() = New Single(5) {}
					numArray5(1) = 1F
					numArray2(1) = numArray5
					Dim numArray6 As Single() = New Single(5) {}
					numArray6(2) = 1F
					numArray2(2) = numArray6
					Dim numArray7 As Single() = New Single(5) {}
					numArray7(3) = 0.7F
					numArray2(3) = numArray7
					numArray2(4) = New Single(5) {}
					_disabledImageColorMatrix = MultiplyColorMatrix(numArray2, numArray)
				End If
				Return _disabledImageColorMatrix
			End Get
		End Property
		Friend Shared Function MultiplyColorMatrix(matrix1 As Single()(), matrix2 As Single()()) As ColorMatrix
			Dim num As Integer = 5
			Dim newColorMatrix As Single()() = New Single(num)() {}
			Dim i As Integer = 0
			While i < num
				newColorMatrix(i) = New Single(num) {}
				System.Math.Max(System.Threading.Interlocked.Increment(i),i - 1)
			End While
			Dim numArray2 As Single() = New Single(num) {}
			Dim j As Integer = 0
			While j < num
				Dim k As Integer = 0
				While k < num
					numArray2(k) = matrix1(k)(j)
					System.Math.Max(System.Threading.Interlocked.Increment(k),k - 1)
				End While
				Dim m As Integer = 0
				While m < num
					Dim numArray3 As Single() = matrix2(m)
					Dim num6 As Single = 0F
					Dim n As Integer = 0
					While n < num
						num6 += numArray3(n) * numArray2(n)
						System.Math.Max(System.Threading.Interlocked.Increment(n),n - 1)
					End While
					newColorMatrix(m)(j) = num6
					System.Math.Max(System.Threading.Interlocked.Increment(m),m - 1)
				End While
				System.Math.Max(System.Threading.Interlocked.Increment(j),j - 1)
			End While
			Return New ColorMatrix(newColorMatrix)
		End Function
		Public Shared Function CreateDisabledImage(normalImage As Image) As Image
			Dim imageAttr As New ImageAttributes()
			imageAttr.ClearColorKey()
			imageAttr.SetColorMatrix(DisabledImageColorMatrix)
			Dim size As Size = normalImage.Size
			Dim image As New Bitmap(size.Width, size.Height)
			Dim graphics As Graphics = Graphics.FromImage(image)
			graphics.DrawImage(normalImage, New Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height, _
				GraphicsUnit.Pixel, imageAttr)
			graphics.Dispose()
			Return image
		End Function
		Public Overridable Sub OnRenderOrbDropDownBackground(e As RibbonOrbDropDownEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonCaptionBar(e As RibbonRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonOrb(e As RibbonRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonQuickAccessToolbarBackground(e As RibbonRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonBackground(e As RibbonRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonTab(e As System.Windows.Forms.RibbonTabRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonItem(e As RibbonItemRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonTabContentBackground(e As System.Windows.Forms.RibbonTabRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonPanelBackground(e As RibbonPanelRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonTabText(e As RibbonTabRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonItemText(e As RibbonTextEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonItemBorder(e As RibbonItemRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonItemImage(e As RibbonItemBoundsEventArgs)
		End Sub
		Public Overridable Sub OnRenderRibbonPanelText(e As RibbonPanelRenderEventArgs)
		End Sub
		Public Overridable Sub OnRenderDropDownBackground(e As RibbonCanvasEventArgs)
		End Sub
		Public Overridable Sub OnRenderPanelPopupBackground(e As RibbonCanvasEventArgs)
		End Sub
		Public Overridable Sub OnRenderTabScrollButtons(e As RibbonTabRenderEventArgs)
		End Sub
	End Class
End Namespace
