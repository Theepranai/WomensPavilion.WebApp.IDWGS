using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Features.UserDetails.Queries.GetUserList;

public class UserListQueryHandler : IRequestHandler<UserListQuery, List<UserListVm>>
{

    private readonly IAsyncRepository<UserDetail> _userRepo;
    private readonly IMapper _mapper;

    public UserListQueryHandler(IAsyncRepository<UserDetail> userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public async Task<List<UserListVm>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        var allUser = (await _userRepo.ListAllAsync());

        if (request.Days > 0)
        {
            var checkDate = DateTime.Now.AddDays(-request.Days);
            allUser = allUser.Where(x => x.DateRegistered  >= checkDate).ToList();
        }

        if (request.OrderByDesc)
        {
            allUser = allUser.OrderByDescending(x => x.DateRegistered).ToList();
        }

        if (request.OnlyApproved)
        {
            allUser = allUser.Where(x => x.IsApprove == true).ToList();
        }

        return _mapper.Map<List<UserListVm>>(allUser);
    }
}
