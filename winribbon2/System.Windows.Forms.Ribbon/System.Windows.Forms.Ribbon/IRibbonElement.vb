Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Namespace System.Windows.Forms
	Public Interface IRibbonElement
		Sub OnPaint(sender As [Object], e As RibbonElementPaintEventArgs)
		Function MeasureSize(sender As Object, e As RibbonElementMeasureSizeEventArgs) As Size
		Sub SetBounds(bounds As System.Drawing.Rectangle)
		ReadOnly Property Bounds() As Rectangle
	End Interface
End Namespace
