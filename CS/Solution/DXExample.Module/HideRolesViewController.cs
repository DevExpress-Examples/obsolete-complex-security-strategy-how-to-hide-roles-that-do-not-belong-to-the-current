using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;

namespace DXExample.Module {
    public partial class HideRolesViewController : ViewController {
        public HideRolesViewController() {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(CustomRole);
        }
        protected override void OnActivated() {
            base.OnActivated();
            CustomRole adminRole = View.ObjectSpace.FindObject<CustomRole>(new BinaryOperator("Name", "Administrators"));
            if ((SecuritySystem.CurrentUser as CustomUser).Roles.Lookup(adminRole.Oid) == null) {
                ListView listView = (ListView)View;
                listView.CollectionSource.Criteria["ByOwning"] = CriteriaOperator.Parse("IsOwningRequired = false OR [Users][Oid = ?]", SecuritySystem.CurrentUserId);
            }
        }
    }
}