using System.ComponentModel.DataAnnotations;

namespace PixelPlusMedia.Application.Models.Authentication.Role
{
    public class AssignRoleRequest
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Role { get; set; }
    }
}
