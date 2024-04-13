using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlusMedia.Application.Authorization
{
    public class PolicyType
    {
        public static class Users
        {
            public const string View = "users.view.policy";
            public const string Add = "users.add.policy";
            public const string Edit = "users.edit.policy";
            public const string Delete = "users.delete.policy";
            public const string EditRole = "users.edit.role.policy";
            public const string ViewRole = "users.view.role.policy";
            public const string AddRole = "users.add.role.policy";
            public const string DeleteRole = "users.delete.role.policy";
            public const string AssignRole = "users.assign.role.policy";
            public const string DeleteAssignRole = "users.deleteassign.role.policy";
            public const string PermissionsView = "users.view.permissions.policy";
            public const string PermissionsEdit = "users.edit.permissions.policy";
        }
    }
}
