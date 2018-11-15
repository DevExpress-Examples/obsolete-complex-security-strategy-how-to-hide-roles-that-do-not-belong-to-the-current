<!-- default file list -->
*Files to look at*:

* [CustomRole.cs](./CS/DXExample.Module/CustomRole.cs) (VB: [CustomRole.vb](./VB/DXExample.Module/CustomRole.vb))
* [CustomUser.cs](./CS/DXExample.Module/CustomUser.cs) (VB: [CustomUser.vb](./VB/DXExample.Module/CustomUser.vb))
* **[HideRolesViewController.cs](./CS/DXExample.Module/HideRolesViewController.cs) (VB: [HideRolesViewController.vb](./VB/DXExample.Module/HideRolesViewController.vb))**
* [Updater.cs](./CS/DXExample.Module/Updater.cs) (VB: [Updater.vb](./VB/DXExample.Module/Updater.vb))
<!-- default file list end -->
# OBSOLETE: Complex Security Strategy - How to hide roles that do not belong to the current user


<p><strong>=======================</strong></p><p><strong>This example is related to the old security system and is not applicable to the new one.</strong><strong><br />
=======================</strong><strong><br />
</strong>In the new Security System it is possible to accomplish this task by creating an Object Permission for the SecuritySystemRole class and specifying the following Criteria in it: "Users[Oid = CurrentUserId()]". Please refer to the <a href="http://documentation.devexpress.com/#Xaf/CustomDocument3366"><u>New Security System Overview</u></a> topic for additional information.</p>

<br/>


