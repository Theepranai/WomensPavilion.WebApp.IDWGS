namespace PixelPlusMedia.Application.Features.UserDetails.Queries.GetUserList;
public class UserListVm
{
    public Guid UserId { get; set; }
    public string DefaultMessage { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string MediaUrl { get; set; } = string.Empty;
    public bool IsApprove { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime DateRegistered { get; set; }
}
