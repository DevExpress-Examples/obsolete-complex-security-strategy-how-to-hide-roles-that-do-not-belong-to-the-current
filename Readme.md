# OBSOLETE: Complex Security Strategy - How to hide roles that do not belong to the current user


<p><strong>=======================</strong></p><p><strong>This example is related to the old security system and is not applicable to the new one.</strong><strong><br />
=======================</strong><strong><br />
</strong>In the new Security System it is possible to accomplish this task by creating an Object Permission for the SecuritySystemRole class and specifying the following Criteria in it: "Users[Oid = CurrentUserId()]". Please refer to the <a href="http://documentation.devexpress.com/#Xaf/CustomDocument3366"><u>New Security System Overview</u></a> topic for additional information.</p>

<br/>


