Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Persistent.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl

Namespace DXExample.Module
	Partial Public Class HideRolesViewController
		Inherits ViewController
		Public Sub New()
			InitializeComponent()
			RegisterActions(components)
			TargetViewType = ViewType.ListView
			TargetObjectType = GetType(CustomRole)
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			Dim adminRole As CustomRole = View.ObjectSpace.Session.FindObject(Of CustomRole)(New BinaryOperator("Name", "Administrators"))
			If (TryCast(SecuritySystem.CurrentUser, CustomUser)).Roles.Lookup(adminRole.Oid) Is Nothing Then
				Dim listView As ListView = CType(View, ListView)
				listView.CollectionSource.Criteria("ByOwning") = CriteriaOperator.Parse("IsOwningRequired = false OR [Users][Oid = ?]", SecuritySystem.CurrentUserId)
			End If
		End Sub
	End Class
End Namespace