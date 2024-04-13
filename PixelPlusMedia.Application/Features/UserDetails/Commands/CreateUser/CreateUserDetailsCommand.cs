using MediatR;
using Microsoft.AspNetCore.Http;

namespace PixelPlusMedia.Application.Features.UserDetails.Commands.CreateUser;

public class CreateUserDetailsCommand : IRequest<CreateUserDetailsResponse>
{
    public string DefaultMessage { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public IFormFile ImageFile { get; set; }
    public string? MediaUrl { get; set; }
    public bool IsApprove { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime DateRegistered { get; set; }
}
