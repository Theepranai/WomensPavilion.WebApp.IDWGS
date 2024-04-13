using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixelPlusMedia.Application.Contracts.Identity;
using PixelPlusMedia.Application.Models.Authentication;
using PixelPlusMedia.Application.Models.Authentication.Permission;
using PixelPlusMedia.Application.Models.Authentication.Registration;
using PixelPlusMedia.Application.Models.Authentication.Role;
using PixelPlusMedia.Application.Models.Authentication.User;

namespace PixelPlusMedia.API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AccountController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// authenticate
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// 

    /// [ValidateAntiForgeryToken]
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
    {
        return Ok(await _authenticationService.AuthenticateAsync(request));
    }

    /// <summary>
    /// register
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
    {
        return Ok(await _authenticationService.RegisterAsync(request));
    }

    /// <summary>
    /// Get User List
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpGet("user")]
    public async Task<ActionResult<AddRoleResponse>> GetUsersAsync()
    {
        return Ok(await _authenticationService.GetUsers());
    }

    /// <summary>
    /// Get User
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpGet("user/{userName}", Name = "GetUserAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetUserResponse>> GetUserAsync(string userName)
    {
        return Ok(await _authenticationService.GetUser(userName));
    }

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpPut("user", Name = "UpdateUserAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UpdateUserResponse>> UpdateUserAsync(UpdateUserRequest request)
    {
        return Ok(await _authenticationService.UpdateUser(request));
    }

    /// <summary>
    /// Remove User
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpDelete("user/{Id}", Name = "RemoveUserAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RemoveUserResponse>> RemoveUserAsync(string Id)
    {
        return Ok(await _authenticationService.RemoveUser(Id));
    }

    /// <summary>
    /// Role
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpGet("role")]
    public async Task<ActionResult<AddRoleResponse>> GetRolesAsync()
    {
        return Ok(await _authenticationService.GetRoles());
    }

    /// <summary>
    /// Get Permission Details
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpGet("role/{roleName}")]
    public async Task<ActionResult<RolePermissionsResponse>> GetRoleAsync(string roleName)
    {
        return Ok(await _authenticationService.ListRolePermissions(new RolePermissionsRequest() { Name = roleName }));
    }

    /// <summary>
    /// Update Role
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpPut("role/update")]
    public async Task<ActionResult<UpdateRolePermissionsResponse>> UpdateRoleAsync(UpdateRolePermissionsRequest request)
    {
        return Ok(await _authenticationService.UpdateRolePermissions(request));
    }

    /// <summary>
    /// Add Role
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpPost("addrole")]
    public async Task<ActionResult<AddRoleResponse>> AddRoleAsync(AddRoleRequest request)
    {
        return Ok(await _authenticationService.AddRoleAsync(request));
    }

    /// <summary>
    /// Remove Role
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpPost("removerole")]
    public async Task<ActionResult<RemoveRoleResponse>> RemoveRole(RemoveRoleRequest request)
    {
        return Ok(await _authenticationService.RemoveRoleAsync(request));
    }

    /// <summary>
    /// Assign Role
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpPost("assignrole")]
    public async Task<ActionResult<AssignRoleResponse>> AssignRoleAsync(AssignRoleRequest request)
    {
        return Ok(await _authenticationService.AssignRoleAsync(request));
    }

    /// <summary>
    /// Remove Assigned Role
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [HttpPost("removeassignedrole")]
    public async Task<ActionResult<RemoveAssignedRoleResponse>> RemoveAssignedRole(RemoveAssignedRoleRequest request)
    {
        return Ok(await _authenticationService.RemoveAssignedRole(request));
    }
}
