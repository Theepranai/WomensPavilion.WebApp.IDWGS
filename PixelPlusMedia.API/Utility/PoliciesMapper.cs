using Microsoft.AspNetCore.Authorization;
using PixelPlusMedia.Application.Authorization;

namespace PixelPlusMedia.API.Utility
{
    public class PoliciesMapper
    {
        public static void MapPoliciesOptions(AuthorizationOptions options)
        {
            /**
             * Map User Policies
             */
            options.AddPolicy(PolicyType.Users.View, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.View); });
            options.AddPolicy(PolicyType.Users.Add, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.Add); });
            options.AddPolicy(PolicyType.Users.Edit, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.Edit); });
            options.AddPolicy(PolicyType.Users.Delete, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.Delete); });
            options.AddPolicy(PolicyType.Users.EditRole, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.EditRole); });
            options.AddPolicy(PolicyType.Users.ViewRole, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.ViewRole); });
            options.AddPolicy(PolicyType.Users.AddRole, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.AddRole); });
            options.AddPolicy(PolicyType.Users.DeleteRole, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.DeleteRole); });
            options.AddPolicy(PolicyType.Users.AssignRole, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.AssignRole); });
            options.AddPolicy(PolicyType.Users.DeleteAssignRole, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.DeleteAssignRole); });
            options.AddPolicy(PolicyType.Users.PermissionsView, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.PermissionsView); });
            options.AddPolicy(PolicyType.Users.PermissionsEdit, policy => { policy.RequireClaim(ClaimType.Permission, Permission.Users.PermissionsEdit); });
        }
    }
}
