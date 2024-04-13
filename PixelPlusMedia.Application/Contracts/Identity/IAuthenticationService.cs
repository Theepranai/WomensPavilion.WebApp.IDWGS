using PixelPlusMedia.Application.Models.Authentication;
using PixelPlusMedia.Application.Models.Authentication.Permission;
using PixelPlusMedia.Application.Models.Authentication.Registration;
using PixelPlusMedia.Application.Models.Authentication.Role;
using PixelPlusMedia.Application.Models.Authentication.User;

namespace PixelPlusMedia.Application.Contracts.Identity;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    Task<AddRoleResponse> AddRoleAsync(AddRoleRequest request);
    Task<RemoveRoleResponse> RemoveRoleAsync(RemoveRoleRequest request);
    Task<AssignRoleResponse> AssignRoleAsync(AssignRoleRequest request);
    Task<RemoveAssignedRoleResponse> RemoveAssignedRole(RemoveAssignedRoleRequest request);
    Task<GetRolesResponse> GetRoles();
    Task<List<GetUserResponse>> GetUsers();
    Task<GetUserResponse> GetUser(string userName);
    Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request);
    Task<RemoveUserResponse> RemoveUser(string Id);
    Task<RolePermissionsResponse> ListRolePermissions(RolePermissionsRequest request);
    Task<UpdateRolePermissionsResponse> UpdateRolePermissions(UpdateRolePermissionsRequest request);
}
