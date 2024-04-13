using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PixelPlusMedia.Application.Authorization;
using PixelPlusMedia.Application.Contracts.Identity;
using PixelPlusMedia.Application.Models.Authentication;
using PixelPlusMedia.Application.Models.Authentication.Permission;
using PixelPlusMedia.Application.Models.Authentication.Registration;
using PixelPlusMedia.Application.Models.Authentication.Role;
using PixelPlusMedia.Application.Models.Authentication.User;
using PixelPlusMedia.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PixelPlusMedia.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AuthenticationService(UserManager<ApplicationUser> userManager,
    IOptions<JwtSettings> jwtsettings,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtsettings.Value;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            throw new Exception($"User not found");
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            throw new Exception($"Invalid Username Or Password.");
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthenticationResponse response = new AuthenticationResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            UserName = user.UserName,
            ClientName = user.ClientName
        };

        return response;
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", roles[i]));
        }

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            //expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

    /*    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var userRoles = roles.Select(r => new Claim("roles", r)).ToArray();

            List<Claim> rolesClaims = new List<Claim>();

            foreach (var r in roles)
            {
                var role = await _roleManager.FindByNameAsync(r);
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                roleClaims.ToList().ForEach(c => { if (!rolesClaims.Contains(c)) rolesClaims.Add(c); });
            }

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    // new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
                }
            .Union(userClaims)
            .Union(rolesClaims)
            .Union(userRoles);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,

                //expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }*/

    public async Task<List<GetUserResponse>> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();

        var UserRoleList = new List<GetUserResponse>();

        users.ForEach(user =>
        {
            var roles = _userManager.GetRolesAsync(user);
            roles.Wait();

            UserRoleList.Add(new GetUserResponse
            {
                Id = user.Id,
                ClientName = user.ClientName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.Result.ToArray()
            });
        });

        return UserRoleList;
    }

    public async Task<GetUserResponse> GetUser(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var roles = await _userManager.GetRolesAsync(user);

        return new GetUserResponse
        {
            ClientName = user.ClientName,
            UserName = user.UserName,
            Email = user.Email,
            Id = user.Id,
            Roles = roles.ToArray()
        };
    }

    public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user == null)
        {
            throw new Exception($"User not found");
        }

        if (user.UserName.Equals("admin") && !request.UserName.Equals("admin"))
        {
            throw new Exception($"Cannot change the default Adminstrator Username");
        }

        user.ClientName = request.ClientName;
        user.Email = request.Email;
        user.UserName = request.UserName;
        if (request.Password != "")
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
        }

        await _userManager.UpdateAsync(user);

        return new UpdateUserResponse
        {
            UserName = user.UserName,
            ClientName = user.ClientName,
            Email = user.Email,
            Id = user.Id
        };
    }

    public async Task<RemoveUserResponse> RemoveUser(string Id)
    {
        var user = await _userManager.FindByIdAsync(Id);

        if (user == null)
        {
            throw new Exception($"User not found");
        }

        if (user.UserName.Equals("admin"))
        {
            throw new Exception($"Cannot delete default admin user");
        }

        await _userManager.DeleteAsync(user);

        return new RemoveUserResponse
        {
            Id = user.Id,
            ClientName = user.ClientName
        };
    }

    public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName);

        if (existingUser != null)
        {
            throw new Exception($"Username '{request.UserName}' already exists.");
        }

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            ClientName = request.ClientName,
            Email = request.Email,
            UserName = request.UserName,
            EmailConfirmed = false
        };

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);

        if (existingEmail == null)
        {
            var role = await _roleManager.FindByNameAsync(request.Role);
            if (role == null)
            {
                throw new Exception($"Role not found");
            }

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.Role);
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                throw new Exception($"{result.Errors}");
                // return (RegistrationResponse)result.Errors;
            }
        }
        else
        {
            throw new Exception($"Email {request.Email} already exists.");
        }
    }

    public async Task<GetRolesResponse> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();

        return new GetRolesResponse() { Roles = new List<object>(roles) };
    }

    public async Task<AddRoleResponse> AddRoleAsync(AddRoleRequest request)
    {
        var roleExists = await _roleManager.FindByNameAsync(request.Name);
        if (roleExists != null)
        {
            throw new Exception($"Role {request.Name} already exists");
        }

        var AllPermissionsClaims = await Permission.ListAllPermissionClaims();

        // validating request before updating role permissions
        request.PermissionSettings.ForEach(permissionSetting => {
            var roleClaim = new Claim(ClaimType.Permission, permissionSetting.Permission);
            bool roleClaimIsValid = AllPermissionsClaims.Any(c => c.Value.Equals(roleClaim.Value));
            if (!roleClaimIsValid)
            {
                throw new Exception($"{permissionSetting.Permission} permission does not exist.");
            }
        });

        // creating role with permissions attached
        var role = new ApplicationRole()
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name
        };
        await _roleManager.CreateAsync(role);

        foreach (var permissionSetting in request.PermissionSettings)
        {
            if (permissionSetting.Enabled)
            {
                var roleClaim = new Claim(ClaimType.Permission, permissionSetting.Permission);
                await _roleManager.AddClaimAsync(role, roleClaim);
            }
        }

        return new AddRoleResponse() { Id = role.Id, Name = role.Name };
    }

    public async Task<RemoveRoleResponse> RemoveRoleAsync(RemoveRoleRequest request)
    {
        if (request.Name.Equals("Administrator") ||
            request.Name.Equals("Kiosk") ||
            request.Name.Equals("Operation") ||
            request.Name.Equals("BackOffice"))
        {
            throw new Exception($"{request.Name} is a default role and cannot be deleted.");
        }

        var UsersUsingRoleList = await _userManager.GetUsersInRoleAsync(request.Name);
        if (UsersUsingRoleList.Count != 0)
        {
            throw new Exception($"Failed to delete. {request.Name} is being used by one or more users");
        }

        var role = await _roleManager.FindByNameAsync(request.Name);
        await _roleManager.DeleteAsync(role);
        return new RemoveRoleResponse() { Id = role.Id };
    }

    public async Task<AssignRoleResponse> AssignRoleAsync(AssignRoleRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            throw new Exception($"User not found");
        }

        var role = await _roleManager.FindByNameAsync(request.Role);
        if (role == null)
        {
            throw new Exception($"Role not found");
        }

        await _userManager.AddToRoleAsync(user, request.Role);

        return new AssignRoleResponse() { Id = user.Id };

    }

    public async Task<RemoveAssignedRoleResponse> RemoveAssignedRole(RemoveAssignedRoleRequest request)
    {
        /* Validation befre removing role */
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            throw new Exception($"User not found");
        }

        var role = await _roleManager.FindByNameAsync(request.Role);
        if (role == null)
        {
            throw new Exception($"Role not found");
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        if (!userRoles.Contains(role.Name))
        {
            throw new Exception($"Failed to remove. {request.Role} role is not assigned to {request.UserName}");
        }

        if (userRoles.Count == 1)
        {
            throw new Exception($"Assign another role to the user before removing the current one");
        }

        await _userManager.RemoveFromRoleAsync(user, request.Role);

        return new RemoveAssignedRoleResponse() { Id = user.Id };
    }

    public async Task<RolePermissionsResponse> ListRolePermissions(RolePermissionsRequest request)
    {
        var AllPermissionsClaims = await Permission.ListAllPermissionClaims();

        var role = await _roleManager.FindByNameAsync(request.Name);
        if (role == null)
        {
            throw new Exception($"Role not found");
        }
        var roleClaims = await _roleManager.GetClaimsAsync(role);

        var RolePermissionsResponseList = new RolePermissionsResponse()
        {
            Name = role.Name,
            PermissionSettings = new List<PermissionSetting>()
        };
        AllPermissionsClaims.ToList().ForEach(permission => {
            RolePermissionsResponseList.PermissionSettings.Add(
                new PermissionSetting()
                {
                    Permission = permission.Value,
                    Enabled = roleClaims.Any(c => c.Value.Equals(permission.Value))
                });
        });

        return RolePermissionsResponseList;
    }

    public async Task<UpdateRolePermissionsResponse> UpdateRolePermissions(
        UpdateRolePermissionsRequest request)
    {
        var AllPermissionsClaims = await Permission.ListAllPermissionClaims();

        // validating request before updating role permissions
        if (request.Name.Equals("Administrator") ||
            request.Name.Equals("Kiosk") ||
            request.Name.Equals("Operation") ||
            request.Name.Equals("BackOffice"))
        {
            throw new Exception($"{request.Name} is a default role and cannot be edited.");
        }
        request.PermissionSettings.ForEach(permissionSetting => {
            var roleClaim = new Claim(ClaimType.Permission, permissionSetting.Permission);
            bool roleClaimIsValid = AllPermissionsClaims.Any(c => c.Value.Equals(roleClaim.Value));
            if (!roleClaimIsValid)
            {
                throw new Exception($"{permissionSetting.Permission} permission does not exist.");
            }
        });

        var role = await _roleManager.FindByNameAsync(request.Name);
        var roleClaims = await _roleManager.GetClaimsAsync(role);

        var newRoleClaimsPermissions = new UpdateRolePermissionsResponse()
        {
            Name = role.Name,
            PermissionSettings = new List<PermissionSetting>()
        };
        foreach (var permissionSetting in request.PermissionSettings)
        {
            var roleClaim = new Claim(ClaimType.Permission, permissionSetting.Permission);
            bool ClaimIsAssingedToRole = roleClaims.Any(c => c.Value.Equals(roleClaim.Value));

            if (!ClaimIsAssingedToRole && permissionSetting.Enabled)
            {
                await _roleManager.AddClaimAsync(role, roleClaim);
            }

            if (ClaimIsAssingedToRole && !permissionSetting.Enabled)
            {
                await _roleManager.RemoveClaimAsync(role, roleClaim);
            }

            newRoleClaimsPermissions.PermissionSettings.Add(
                new PermissionSetting()
                {
                    Permission = roleClaim.Value,
                    Enabled = permissionSetting.Enabled
                });
        }

        return newRoleClaimsPermissions;
    }



}
