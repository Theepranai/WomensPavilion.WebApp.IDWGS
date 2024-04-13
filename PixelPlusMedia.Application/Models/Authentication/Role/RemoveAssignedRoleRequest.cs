using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlusMedia.Application.Models.Authentication.Role
{
    public class RemoveAssignedRoleRequest
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Role { get; set; }
    }
}
