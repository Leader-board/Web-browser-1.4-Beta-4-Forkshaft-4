Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Namespace System.Windows.Forms
	Public NotInheritable Class RibbonContextCollection
		Inherits List(Of RibbonContext)
		Private _owner As Ribbon
		Friend Sub New(owner As Ribbon)
			If owner Is Nothing Then
				Throw New ArgumentNullException("owner")
			End If
			_owner = owner
		End Sub
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Owner() As Ribbon
			Get
				Return _owner
			End Get
		End Property
		Public Shadows Sub Add(item As RibbonContext)
			item.SetOwner(Owner)
			Owner.Tabs.AddRange(item.Tabs)
			MyBase.Add(item)
		End Sub
		Public Shadows Sub AddRange(items As System.Collections.Generic.IEnumerable(Of System.Windows.Forms.RibbonContext))
			For Each c As RibbonContext In items
				c.SetOwner(Owner)
				Owner.Tabs.AddRange(c.Tabs)
			Next
			MyBase.AddRange(items)
		End Sub
		Public Shadows Sub Insert(index As Integer, item As System.Windows.Forms.RibbonContext)
			item.SetOwner(Owner)
			Owner.Tabs.InsertRange(index, item.Tabs)
			MyBase.Insert(index, item)
		End Sub
		Public Shadows Sub Remove(context As RibbonContext)
			MyBase.Remove(context)
			For Each tab As RibbonTab In context.Tabs
				Owner.Tabs.Remove(tab)
			Next
		End Sub
		Public Shadows Function RemoveAll(predicate As Predicate(Of RibbonContext)) As Integer
			Throw New ApplicationException("RibbonContextCollectin.RemoveAll function is not supported")
		End Function
		Public Shadows Sub RemoveAt(index As Integer)
			MyBase.RemoveAt(index)
			Dim ctx As RibbonContext = Me(index)
			For Each tab As RibbonTab In ctx.Tabs
				Owner.Tabs.Remove(tab)
			Next
		End Sub
		Public Shadows Sub RemoveRange(index As Integer, count As Integer)
			Throw New ApplicationException("RibbonContextCollection.RemoveRange function is not supported")
		End Sub
		Friend Sub SetOwner(owner As Ribbon)
			If owner Is Nothing Then
				Throw New ArgumentNullException("owner")
			End If
			_owner = owner
		End Sub
	End Class
End Namespace
