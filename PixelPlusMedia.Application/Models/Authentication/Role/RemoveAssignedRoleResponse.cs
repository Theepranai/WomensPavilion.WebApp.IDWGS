using PixelPlusMedia.Application.Responses;
using System.ComponentModel.DataAnnotations;

namespace PixelPlusMedia.Application.Models.Authentication.Role;

public class RemoveAssignedRoleResponse : BaseResponse
{
    public RemoveAssignedRoleResponse() : base() { }
    public string Id { get; set; }
}
