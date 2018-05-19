using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;

namespace DXExample.Module {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            // If a user named 'Sam' doesn't exist in the database, create this user
            CustomUser user1 = ObjectSpace.FindObject<CustomUser>(new BinaryOperator("UserName", "Sam"));
            if (user1 == null) {
                user1 = ObjectSpace.CreateObject<CustomUser>();
                user1.UserName = "Sam";
                user1.FirstName = "Sam";
                // Set a password if the standard authentication type is used
                user1.SetPassword("");
            }
            // If a user named 'John' doesn't exist in the database, create this user
            CustomUser user2 = ObjectSpace.FindObject<CustomUser>(new BinaryOperator("UserName", "John"));
            if (user2 == null) {
                user2 = ObjectSpace.CreateObject<CustomUser>();
                user2.UserName = "John";
                user2.FirstName = "John";
                // Set a password if the standard authentication type is used
                user2.SetPassword("");
            }
            // If a user named 'John' doesn't exist in the database, create this user
            CustomUser user3 = ObjectSpace.FindObject<CustomUser>(new BinaryOperator("UserName", "Mary"));
            if (user3 == null) {
                user3 = ObjectSpace.CreateObject<CustomUser>();
                user3.UserName = "Mary";
                user3.FirstName = "Mary";
                // Set a password if the standard authentication type is used
                user3.SetPassword("");
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            CustomRole adminRole = ObjectSpace.FindObject<CustomRole>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null) {
                adminRole = ObjectSpace.CreateObject<CustomRole>();
                adminRole.Name = "Administrators";
            }
            // If a role with the Users name doesn't exist in the database, create this role
            CustomRole userRole = ObjectSpace.FindObject<CustomRole>(new BinaryOperator("Name", "Users"));
            if (userRole == null) {
                userRole = ObjectSpace.CreateObject<CustomRole>();
                userRole.Name = "Users";
            }
            // If a role with the PowerUsers name doesn't exist in the database, create this role
            CustomRole powerUserRole = ObjectSpace.FindObject<CustomRole>(new BinaryOperator("Name", "PowerUsers"));
            if (powerUserRole == null) {
                powerUserRole = ObjectSpace.CreateObject<CustomRole>();
                powerUserRole.Name = "PowerUsers";
            }
            // Delete all permissions assigned to the Administrators and Users roles
            while (adminRole.PersistentPermissions.Count > 0) {
                ObjectSpace.Delete(adminRole.PersistentPermissions[0]);
            }
            while (userRole.PersistentPermissions.Count > 0) {
                ObjectSpace.Delete(userRole.PersistentPermissions[0]);
            }
            while (powerUserRole.PersistentPermissions.Count > 0) {
                ObjectSpace.Delete(powerUserRole.PersistentPermissions[0]);
            }
            // Allow full access to all objects to the Administrators role
            adminRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
            // Allow editing the Application Model to the Administrators role
            adminRole.AddPermission(new EditModelPermission(ModelAccessModifier.Allow));
            adminRole.IsOwningRequired = true;
            // Save the Administrators role to the database
            adminRole.Save();
            // Allow full access to all objects to the Users role
            userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
            // Deny change access to the User type objects to the Users role
            userRole.AddPermission(new ObjectAccessPermission(typeof(User),
               ObjectAccess.ChangeAccess, ObjectAccessModifier.Deny));
            // Deny full access to the Role type objects to the Users role
            userRole.AddPermission(new ObjectAccessPermission(typeof(Role),
               ObjectAccess.AllAccess, ObjectAccessModifier.Deny));
            // Deny editing the Application Model to the Users role
            userRole.AddPermission(new EditModelPermission(ModelAccessModifier.Deny));
            // Save the Users role to the database
            userRole.Save();
            // Allow full access to all objects to the Administrators role
            powerUserRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
            // Allow editing the Application Model to the Administrators role
            powerUserRole.AddPermission(new EditModelPermission(ModelAccessModifier.Deny));
            // Deny creating the Role type objects to the Users role
            userRole.AddPermission(new ObjectAccessPermission(typeof(Role), ObjectAccess.Create, ObjectAccessModifier.Deny));
            powerUserRole.IsOwningRequired = true;
            // Save the Administrators role to the database
            powerUserRole.Save();
            // Add the Administrators role to the user1
            user1.Roles.Add(adminRole);
            // Add the Users role to the user2
            user2.Roles.Add(userRole);
            // Add the PowerUsers role to the user3
            user3.Roles.Add(powerUserRole);
            // Save the users to the database
            user1.Save();
            user2.Save();
            user3.Save();
        }
    }
}
