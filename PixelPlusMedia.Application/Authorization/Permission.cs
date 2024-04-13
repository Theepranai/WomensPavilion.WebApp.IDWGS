using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlusMedia.Application.Authorization
{
    public class Permission
    {
        public static class Users
        {
            public const string View = "users.view";
            public const string Add = "users.add";
            public const string Edit = "users.edit";
            public const string Delete = "users.delete";
            public const string EditRole = "users.edit.role";
            public const string ViewRole = "users.view.role";
            public const string AddRole = "users.add.role";
            public const string DeleteRole = "users.delete.role";
            public const string AssignRole = "users.assign.role";
            public const string DeleteAssignRole = "users.deleteassign.role";
            public const string PermissionsView = "users.view.permissions";
            public const string PermissionsEdit = "users.edit.permissions";
        }

        public static Task<IList<Claim>> ListAllPermissionClaims()
        {
            IList<Claim> PermissionList = new List<Claim>();
            var userFields = typeof(Permission.Users).GetFields();
            foreach (var field in userFields)
            {
                PermissionList.Add(new Claim(ClaimType.Permission, field.GetValue(field.Name).ToString()));
            }
            return Task.FromResult(PermissionList);
        }
    }   
}
