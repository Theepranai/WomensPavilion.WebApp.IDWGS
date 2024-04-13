using PixelPlusMedia.Application.Models.Authentication.Permission;
using System.ComponentModel.DataAnnotations;

namespace PixelPlusMedia.Application.Models.Authentication.Role;

public class AddRoleRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    public List<PermissionSetting> PermissionSettings { get; set; }
}
