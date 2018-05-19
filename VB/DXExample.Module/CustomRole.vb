Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.Xpo

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Persistent.Base.Security
Imports System.Collections.Generic
Imports System.Security
Imports System.Collections.ObjectModel

Namespace DXExample.Module
    Public Class CustomRole
        Inherits RoleBase
        Implements ICustomizableRole, IRole
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Private _IsOwningRequired As Boolean = False
        Public Property IsOwningRequired() As Boolean
            Get
                Return _IsOwningRequired
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue("IsOwningRequired", _IsOwningRequired, value)
            End Set
        End Property
        <Association("User-Role")> _
        Public ReadOnly Property Users() As XPCollection(Of CustomUser)
            Get
                Return GetCollection(Of CustomUser)("Users")
            End Get
        End Property
        Private ReadOnly Property IRole_Users() As IList(Of IUser) Implements IRole.Users
            Get
                Return New ListConverter(Of IUser, CustomUser)(Users)
            End Get
        End Property
        Public Shadows Property Name() As String Implements IRole.Name
            Get
                Return MyBase.Name
            End Get
            Set(ByVal value As String)
                MyBase.Name = value
            End Set
        End Property
        Private Shadows ReadOnly Property Permissions() As ReadOnlyCollection(Of IPermission) Implements IRole.Permissions
            Get
                Return MyBase.Permissions
            End Get
        End Property
        Public Shadows Sub AddPermission(ByVal permission As IPermission) Implements ICustomizableRole.AddPermission
            MyBase.AddPermission(permission)
        End Sub

    End Class

End Namespace
