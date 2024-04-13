using System.ComponentModel.DataAnnotations;

namespace PixelPlusMedia.Application.Models.Authentication.User
{
    public class UpdateUserRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ClientName { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }
    }
}
