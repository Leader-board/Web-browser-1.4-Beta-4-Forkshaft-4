Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Namespace System.Windows.Forms
	Public Interface IContainsRibbonComponents
		Function GetAllChildComponents() As IEnumerable(Of Component)
	End Interface
End Namespace
