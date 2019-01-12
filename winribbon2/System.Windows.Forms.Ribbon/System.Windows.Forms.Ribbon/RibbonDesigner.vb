Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.ComponentModel.Design
Imports System.Windows.Forms.Design
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms.Design.Behavior
Namespace System.Windows.Forms
	Public Class RibbonDesigner
		Inherits ControlDesigner
		Public Shared Function LoWord(dwValue As Integer) As Integer
			Return dwValue And &Hffff
		End Function
		Public Shared Function HiWord(dwValue As Integer) As Integer
			Return (dwValue >> 16) And &Hffff
		End Function
		Public Shared Current As RibbonDesigner
		Private _selectedElement As IRibbonElement
		Private quickAccessAdorner As Adorner
		Private orbAdorner As Adorner
		Private tabAdorner As Adorner
		Public Sub New()
			Current = Me
		End Sub
		Protected Overrides Sub Finalize()
			Try
				If Current = Me Then
					Current = Nothing
				End If
			Finally
				MyBase.Finalize()
			End Try
		End Sub
		Public Property SelectedElement() As IRibbonElement
			Get
				Return _selectedElement
			End Get
			Set
				_selectedElement = value
				Dim selector As ISelectionService = TryCast(GetService(GetType(ISelectionService)), ISelectionService)
				If selector IsNot Nothing AndAlso value IsNot Nothing Then
					selector.SetSelectedComponents(New Component() {TryCast(value, Component)}, SelectionTypes.Primary)
				End If
				If TypeOf value Is RibbonButton Then
					(TryCast(value, RibbonButton)).ShowDropDown()
				End If
				Ribbon.Refresh()
			End Set
		End Property
		Public ReadOnly Property Ribbon() As Ribbon
			Get
				Return TryCast(Control, Ribbon)
			End Get
		End Property
		Public Overridable Sub CreateItem(ribbon As Ribbon, collection As RibbonItemCollection, t As Type)
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
		Private Sub CreateOrbItem(collectionName As String, collection As RibbonItemCollection, t As Type)
			If Ribbon Is Nothing Then
				Return
			End If
			Dim host As IDesignerHost = TryCast(GetService(GetType(IDesignerHost)), IDesignerHost)
			Dim transaction As DesignerTransaction = host.CreateTransaction("AddRibbonOrbItem_" + Component.Site.Name)
			Dim member As MemberDescriptor = TypeDescriptor.GetProperties(Ribbon.OrbDropDown)(collectionName)
			RaiseComponentChanging(member)
			Dim item As RibbonItem = TryCast(host.CreateComponent(t), RibbonItem)
			If Not (TypeOf item Is RibbonSeparator) Then
				item.Text = item.Site.Name
			End If
			collection.Add(item)
			Ribbon.OrbDropDown.OnRegionsChanged()
			RaiseComponentChanged(member, Nothing, Nothing)
			transaction.Commit()
			Ribbon.OrbDropDown.SelectOnDesigner(item)
			Ribbon.OrbDropDown.WrappedDropDown.Size = Ribbon.OrbDropDown.Size
		End Sub
		Public Sub CreteOrbMenuItem(t As Type)
			CreateOrbItem("MenuItems", Ribbon.OrbDropDown.MenuItems, t)
		End Sub
		Public Sub CreteOrbRecentItem(t As Type)
			CreateOrbItem("RecentItems", Ribbon.OrbDropDown.RecentItems, t)
		End Sub
		Public Sub CreteOrbOptionItem(t As Type)
			CreateOrbItem("OptionItems", Ribbon.OrbDropDown.OptionItems, t)
		End Sub
		Private Sub AssignEventHandler()
		End Sub
		Private Sub SelectRibbon()
			Dim selector As ISelectionService = TryCast(GetService(GetType(ISelectionService)), ISelectionService)
			If selector IsNot Nothing Then
				selector.SetSelectedComponents(New Component() {Ribbon}, SelectionTypes.Primary)
			End If
		End Sub
		Public Overrides ReadOnly Property Verbs() As System.ComponentModel.Design.DesignerVerbCollection
			Get
				Dim verbs As New DesignerVerbCollection()
				verbs.Add(New DesignerVerb("Add Tab", New EventHandler(AddTabVerb)))
				Return verbs
			End Get
		End Property
		Public Sub AddTabVerb(sender As Object, e As EventArgs)
			Dim r As Ribbon = TryCast(Control, Ribbon)
			If r IsNot Nothing Then
				Dim host As IDesignerHost = TryCast(GetService(GetType(IDesignerHost)), IDesignerHost)
				If host Is Nothing Then
					Return
				End If
				Dim tab As RibbonTab = TryCast(host.CreateComponent(GetType(RibbonTab)), RibbonTab)
				If tab Is Nothing Then
					Return
				End If
				tab.Text = tab.Site.Name
				Ribbon.Tabs.Add(tab)
				r.Refresh()
			End If
		End Sub
		Protected Overrides Sub WndProc(ByRef m As Message)
			If m.HWnd = Control.Handle Then
				Select Case m.Msg
					Case &H203
						AssignEventHandler()
						Exit Select
					Case &H201, &H204
						Return
					Case &H202, &H205
						HitOn(LoWord(CType(m.LParam, Integer)), HiWord(CType(m.LParam, Integer)))
						Return
					Case Else
						Exit Select
				End Select
			End If
			MyBase.WndProc(m)
		End Sub
		Private Sub HitOn(x As Integer, y As Integer)
			If Ribbon.Tabs.Count = 0 OrElse Ribbon.ActiveTab Is Nothing Then
				SelectRibbon()
				Return
			End If
			If Ribbon IsNot Nothing Then
				If Ribbon.TabHitTest(x, y) Then
					SelectedElement = Ribbon.ActiveTab
				Else
					If Ribbon.ActiveTab.TabContentBounds.Contains(x, y) Then
						If Ribbon.ActiveTab.ScrollLeftBounds.Contains(x, y) AndAlso Ribbon.ActiveTab.ScrollLeftVisible Then
							Ribbon.ActiveTab.ScrollLeft()
							SelectedElement = Ribbon.ActiveTab
							Return
						End If
						If Ribbon.ActiveTab.ScrollRightBounds.Contains(x, y) AndAlso Ribbon.ActiveTab.ScrollRightVisible Then
							Ribbon.ActiveTab.ScrollRight()
							SelectedElement = Ribbon.ActiveTab
							Return
						End If
					End If
					If Ribbon.ActiveTab.TabContentBounds.Contains(x, y) Then
						Dim hittedPanel As RibbonPanel = Nothing
						For Each panel As RibbonPanel In Ribbon.ActiveTab.Panels
							If panel.Bounds.Contains(x, y) Then
								hittedPanel = panel
								Exit For
							End If
						Next
						If hittedPanel IsNot Nothing Then
							Dim hittedItem As RibbonItem = Nothing
							For Each item As RibbonItem In hittedPanel.Items
								If item.Bounds.Contains(x, y) Then
									hittedItem = item
									Exit For
								End If
							Next
							If hittedItem IsNot Nothing AndAlso TypeOf hittedItem Is IContainsSelectableRibbonItems Then
								Dim hittedSubItem As RibbonItem = Nothing
								For Each subItem As RibbonItem In (TryCast(hittedItem, IContainsSelectableRibbonItems)).GetItems()
									If subItem.Bounds.Contains(x, y) Then
										hittedSubItem = subItem
										Exit For
									End If
								Next
								If hittedSubItem IsNot Nothing Then
									SelectedElement = hittedSubItem
								Else
									SelectedElement = hittedItem
								End If
							ElseIf hittedItem IsNot Nothing Then
								SelectedElement = hittedItem
							Else
								SelectedElement = hittedPanel
							End If
						Else
							SelectedElement = Ribbon.ActiveTab
						End If
					ElseIf Ribbon.QuickAcessToolbar.SuperBounds.Contains(x, y) Then
						Dim itemHitted As Boolean = False
						For Each item As RibbonItem In Ribbon.QuickAcessToolbar.Items
							If item.Bounds.Contains(x, y) Then
								itemHitted = True
								SelectedElement = item
								Exit For
							End If
						Next
						If Not itemHitted Then
							SelectedElement = Ribbon.QuickAcessToolbar
						End If
					ElseIf Ribbon.OrbBounds.Contains(x, y) Then
						Ribbon.OrbMouseDown()
					Else
						SelectRibbon()
						Ribbon.ForceOrbMenu = False
						If Ribbon.OrbDropDown.Visible Then
							Ribbon.OrbDropDown.Close()
						End If
					End If
				End If
			End If
		End Sub
		Protected Overrides Sub OnPaintAdornments(pe As PaintEventArgs)
			MyBase.OnPaintAdornments(pe)
			Using p As New Pen(Color.Black)
				p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
				Dim host As ISelectionService = TryCast(GetService(GetType(ISelectionService)), ISelectionService)
				If host IsNot Nothing Then
					For Each comp As IComponent In host.GetSelectedComponents()
						Dim item As RibbonItem = TryCast(comp, RibbonItem)
						If item IsNot Nothing AndAlso Not Ribbon.OrbDropDown.AllItems.Contains(item) Then
							pe.Graphics.DrawRectangle(p, item.Bounds)
						End If
					Next
				End If
			End Using
		End Sub
		Public Function GetBehaviorService() As BehaviorService
			Return BehaviorService
		End Function
		Public Overrides Sub Initialize(component As IComponent)
			MyBase.Initialize(component)
			Dim changeService As IComponentChangeService = TryCast(GetService(GetType(IComponentChangeService)), IComponentChangeService)
			Dim desigerEvt As IDesignerEventService = TryCast(GetService(GetType(IDesignerEventService)), IDesignerEventService)
			changeService.ComponentRemoved += New ComponentEventHandler(changeService_ComponentRemoved)
			quickAccessAdorner = New Adorner()
			orbAdorner = New Adorner()
			tabAdorner = New Adorner()
			BehaviorService.Adorners.AddRange(New Adorner() {quickAccessAdorner, orbAdorner, tabAdorner})
			quickAccessAdorner.Glyphs.Add(New RibbonQuickAccessToolbarGlyph(BehaviorService, Me, Ribbon))
			tabAdorner.Glyphs.Add(New RibbonTabGlyph(BehaviorService, Me, Ribbon))
		End Sub
		Public Sub changeService_ComponentRemoved(sender As Object, e As ComponentEventArgs)
			Dim tab As RibbonTab = TryCast(e.Component, RibbonTab)
			Dim panel As RibbonPanel = TryCast(e.Component, RibbonPanel)
			Dim item As RibbonItem = TryCast(e.Component, RibbonItem)
			Dim designerService As IDesignerHost = TryCast(GetService(GetType(IDesignerHost)), IDesignerHost)
			If tab IsNot Nothing Then
				Ribbon.Tabs.Remove(tab)
			ElseIf panel IsNot Nothing Then
				panel.OwnerTab.Panels.Remove(panel)
			ElseIf item IsNot Nothing Then
				If TypeOf item.Canvas Is RibbonOrbDropDown Then
					Ribbon.OrbDropDown.HandleDesignerItemRemoved(item)
				ElseIf TypeOf item.OwnerItem Is RibbonItemGroup Then
					(TryCast(item.OwnerItem, RibbonItemGroup)).Items.Remove(item)
				ElseIf item.OwnerPanel IsNot Nothing Then
					item.OwnerPanel.Items.Remove(item)
				ElseIf Ribbon.QuickAcessToolbar.Items.Contains(item) Then
					Ribbon.QuickAcessToolbar.Items.Remove(item)
				End If
			End If
			RemoveRecursive(TryCast(e.Component, IContainsRibbonComponents), designerService)
			SelectedElement = Nothing
			Ribbon.OnRegionsChanged()
		End Sub
		Public Sub RemoveRecursive(item As IContainsRibbonComponents, service As IDesignerHost)
			If item Is Nothing OrElse service Is Nothing Then
				Return
			End If
			For Each c As Component In item.GetAllChildComponents()
				If TypeOf c Is IContainsRibbonComponents Then
					RemoveRecursive(TryCast(c, IContainsRibbonComponents), service)
				End If
				service.DestroyComponent(c)
			Next
		End Sub
	End Class
End Namespace
