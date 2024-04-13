namespace PixelPlusMedia.Application.Models.Authentication.Permission
{
    public class RolePermissionsResponse
    {
        public string Name { get; set; }
        public List<PermissionSetting> PermissionSettings { get; set; }
    }
}
