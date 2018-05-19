using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.Security;
using System.Collections.Generic;
using System.Security;

namespace DXExample.Module {
    [ImageName("BO_User")]
    public class CustomUser : Person, IUserWithRoles, IAuthenticationActiveDirectoryUser, IAuthenticationStandardUser {
        private UserImpl user;
        private List<IPermission> permissions;
        public CustomUser(Session session)
            : base(session) {
            permissions = new List<IPermission>();
            user = new UserImpl(this);
        }
        public void ReloadPermissions() {
            Roles.Reload();
            foreach (CustomRole role in Roles) {
                role.PersistentPermissions.Reload();
            }
        }
        public bool ComparePassword(string password) {
            return user.ComparePassword(password);
        }
        public void SetPassword(string password) {
            user.SetPassword(password);
        }
#if MediumTrust
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		[Persistent]
		public string StoredPassword {
			get { return user.StoredPassword; }
			set {
				user.StoredPassword = value;
				OnChanged("StoredPassword");
			}
		}
#else
        [Persistent]
        private string StoredPassword {
            get { return user.StoredPassword; }
            set {
                user.StoredPassword = value;
                OnChanged("StoredPassword");
            }
        }
#endif
        [Association("User-Role")]
        public XPCollection<CustomRole> Roles {
            get { return GetCollection<CustomRole>("Roles"); }
        }
        IList<IRole> IUserWithRoles.Roles {
            get {
                return new ListConverter<IRole, CustomRole>(Roles);
            }
        }
        [RuleRequiredField("Required User Name", "Save", "The user name must not be empty")]
        [RuleUniqueValue("Unique User Name", "Save", "The login with the entered UserName was already registered within the system")]
        public string UserName {
            get { return user.UserName; }
            set {
                user.UserName = value;
                OnChanged("UserName");
            }
        }
        public bool ChangePasswordOnFirstLogon {
            get { return user.ChangePasswordAfterLogon; }
            set {
                user.ChangePasswordAfterLogon = value;
                OnChanged("ChangePasswordOnFirstLogon");
            }
        }
        public bool IsActive {
            get { return user.IsActive; }
            set {
                user.IsActive = value;
                OnChanged("IsActive");
            }
        }
        public IList<IPermission> Permissions {
            get {
                permissions.Clear();
                foreach (CustomRole role in Roles) {
                    permissions.AddRange(role.Permissions);
                }
                return permissions.AsReadOnly();
            }
        }
    }

}
