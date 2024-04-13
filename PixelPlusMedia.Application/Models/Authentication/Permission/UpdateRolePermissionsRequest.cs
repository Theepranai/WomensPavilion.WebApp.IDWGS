using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlusMedia.Application.Models.Authentication.Permission
{
    public class UpdateRolePermissionsRequest
    {
        public string Name { get; set; }
        public List<PermissionSetting> PermissionSettings { get; set; }
    }
}
