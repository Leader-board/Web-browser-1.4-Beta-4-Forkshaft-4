Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel.Design
Imports System.ComponentModel
Namespace System.Windows.Forms
	Friend MustInherit Class RibbonElementWithItemCollectionDesigner
		Inherits ComponentDesigner
		Public MustOverride ReadOnly Property Ribbon() As Ribbon
		Public MustOverride ReadOnly Property Collection() As RibbonItemCollection
		Protected Overridable Function OnGetVerbs() As DesignerVerbCollection
			Return New DesignerVerbCollection(New DesignerVerb() {New DesignerVerb("Add Button", New EventHandler(AddButton)), New DesignerVerb("Add ButtonList", New EventHandler(AddButtonList)), New DesignerVerb("Add ItemGroup", New EventHandler(AddItemGroup)), New DesignerVerb("Add Separator", New EventHandler(AddSeparator)), New DesignerVerb("Add TextBox", New EventHandler(AddTextBox)), New DesignerVerb("Add ComboBox", New EventHandler(AddComboBox)), _
				New DesignerVerb("Add ColorChooser", New EventHandler(AddColorChooser))})
		End Function
		Public Overrides ReadOnly Property Verbs() As DesignerVerbCollection
			Get
				Return OnGetVerbs()
			End Get
		End Property
		Private Sub CreateItem(t As Type)
			CreateItem(Ribbon, Collection, t)
		End Sub
		Protected Overridable Sub CreateItem(ribbon As Ribbon, collection As RibbonItemCollection, t As Type)
			Dim host As IDesignerHost = TryCast(GetService(GetType(IDesignerHost)), IDesignerHost)
			If host IsNot Nothing AndAlso collection IsNot Nothing AndAlso ribbon IsNot Nothing Then
				Dim transaction As DesignerTransaction = host.CreateTransaction("AddRibbonItem_" + Component.Site.Name)
				Dim member As MemberDescriptor = TypeDescriptor.GetProperties(Component)("Items")
				MyBase.RaiseComponentChanging(member)
				Dim item As RibbonItem = TryCast(host.CreateComponent(t), RibbonItem)
				If Not (TypeOf item Is RibbonSeparator) Then
					item.Text = item.Site.Name
				End If
				collection.Add(item)
				ribbon.OnRegionsChanged()
				MyBase.RaiseComponentChanged(member, Nothing, Nothing)
				transaction.Commit()
			End If
		End Sub
		Protected Overridable Sub AddButton(sender As Object, e As EventArgs)
			CreateItem(GetType(RibbonButton))
		End Sub
		Protected Overridable Sub AddButtonList(sender As Object, e As EventArgs)
			CreateItem(GetType(RibbonButtonList))
		End Sub
		Protected Overridable Sub AddItemGroup(sender As Object, e As EventArgs)
			CreateItem(GetType(RibbonItemGroup))
		End Sub
		Protected Overridable Sub AddSeparator(sender As Object, e As EventArgs)
			CreateItem(GetType(RibbonSeparator))
		End Sub
		Protected Overridable Sub AddTextBox(sender As Object, e As EventArgs)
			CreateItem(GetType(RibbonTextBox))
		End Sub
		Protected Overridable Sub AddComboBox(sender As Object, e As EventArgs)
			CreateItem(GetType(RibbonComboBox))
		End Sub
		Protected Overridable Sub AddColorChooser(sender As Object, e As EventArgs)
			CreateItem(GetType(RibbonColorChooser))
		End Sub
		Protected Overridable Sub AddDescriptionMenuItem(sender As Object, e As EventArgs)
			CreateItem(GetType(RibbonDescriptionMenuItem))
		End Sub
	End Class
End Namespace
