using MediatR;

namespace PixelPlusMedia.Application.Features.UserDetails.Queries.GetUserList;
public class UserListQuery : IRequest<List<UserListVm>>
{
    public int Days { get; set; }
    public bool OrderByDesc { get; set; }

    public bool OnlyApproved { get; set; }
}
