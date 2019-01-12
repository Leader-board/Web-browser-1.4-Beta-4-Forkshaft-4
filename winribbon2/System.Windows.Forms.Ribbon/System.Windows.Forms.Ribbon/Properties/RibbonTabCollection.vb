Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel
Namespace System.Windows.Forms
	Public NotInheritable Class RibbonTabCollection
		Inherits List(Of RibbonTab)
		Private _owner As Ribbon
		Friend Sub New(owner As Ribbon)
			If owner Is Nothing Then
				Throw New ArgumentNullException("null")
			End If
			_owner = owner
		End Sub
		<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public ReadOnly Property Owner() As Ribbon
			Get
				Return _owner
			End Get
		End Property
		Public Shadows Sub Add(item As RibbonTab)
			item.SetOwner(Owner)
			MyBase.Add(item)
			Owner.OnRegionsChanged()
		End Sub
		Public Shadows Sub AddRange(items As System.Collections.Generic.IEnumerable(Of System.Windows.Forms.RibbonTab))
			For Each tab As RibbonTab In items
				tab.SetOwner(Owner)
			Next
			MyBase.AddRange(items)
			Owner.OnRegionsChanged()
		End Sub
		Public Shadows Sub Insert(index As Integer, item As System.Windows.Forms.RibbonTab)
			item.SetOwner(Owner)
			MyBase.Insert(index, item)
			Owner.OnRegionsChanged()
		End Sub
		Public Shadows Sub Remove(context As RibbonTab)
			MyBase.Remove(context)
			Owner.OnRegionsChanged()
		End Sub
		Public Shadows Function RemoveAll(predicate As Predicate(Of RibbonTab)) As Integer
			Throw New ApplicationException("RibbonTabCollection.RemoveAll function is not supported")
		End Function
		Public Shadows Sub RemoveAt(index As Integer)
			MyBase.RemoveAt(index)
			Owner.OnRegionsChanged()
		End Sub
		Public Shadows Sub RemoveRange(index As Integer, count As Integer)
			MyBase.RemoveRange(index, count)
			Owner.OnRegionsChanged()
		End Sub
		Friend Sub SetOwner(owner As Ribbon)
			_owner = owner
		End Sub
	End Class
End Namespace
