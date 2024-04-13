namespace PixelPlusMedia.Application.Models.Authentication.Permission;

public class UpdateRolePermissionsResponse
{
    public string Name { get; set; }
    public List<PermissionSetting> PermissionSettings { get; set; }
}
