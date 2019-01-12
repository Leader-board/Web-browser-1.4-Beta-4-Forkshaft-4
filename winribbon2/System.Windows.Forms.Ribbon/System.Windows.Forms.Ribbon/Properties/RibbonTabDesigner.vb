Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel.Design
Imports System.ComponentModel
Imports System.Windows.Forms.Design.Behavior
Namespace System.Windows.Forms
	Public Class RibbonTabDesigner
		Inherits ComponentDesigner
		Private panelAdorner As Adorner
		Public Overrides ReadOnly Property Verbs() As DesignerVerbCollection
			Get
				Return New DesignerVerbCollection(New DesignerVerb() {New DesignerVerb("Add Panel", New EventHandler(AddPanel))})
			End Get
		End Property
		Public ReadOnly Property Tab() As RibbonTab
			Get
				Return TryCast(Component, RibbonTab)
			End Get
		End Property
		Public Sub AddPanel(sender As Object, e As EventArgs)
			Dim host As IDesignerHost = TryCast(GetService(GetType(IDesignerHost)), IDesignerHost)
			If host IsNot Nothing AndAlso Tab IsNot Nothing Then
				Dim transaction As DesignerTransaction = host.CreateTransaction("AddPanel" + Component.Site.Name)
				Dim member As MemberDescriptor = TypeDescriptor.GetProperties(Component)("Panels")
				MyBase.RaiseComponentChanging(member)
				Dim panel As RibbonPanel = TryCast(host.CreateComponent(GetType(RibbonPanel)), RibbonPanel)
				If panel IsNot Nothing Then
					panel.Text = panel.Site.Name
					Tab.Panels.Add(panel)
					Tab.Owner.OnRegionsChanged()
				End If
				MyBase.RaiseComponentChanged(member, Nothing, Nothing)
				transaction.Commit()
			End If
		End Sub
		Public Overrides Sub Initialize(component As IComponent)
			MyBase.Initialize(component)
			panelAdorner = New Adorner()
			Dim bs As BehaviorService = RibbonDesigner.Current.GetBehaviorService()
			If bs Is Nothing Then
				Return
			End If
			bs.Adorners.AddRange(New Adorner() {panelAdorner})
			panelAdorner.Glyphs.Add(New RibbonPanelGlyph(bs, Me, Tab))
		End Sub
	End Class
End Namespace
