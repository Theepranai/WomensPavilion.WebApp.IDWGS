using AutoMapper;
using PixelPlusMedia.Application.Features.UserDetails.Commands.CreateUser;
using PixelPlusMedia.Application.Features.UserDetails.Queries.GetUserList;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Profiles;

public class UserDetailProfile : Profile
{
    public UserDetailProfile()
    {
        CreateMap<UserDetail, CreateUserDetailsDto>().ReverseMap();
        CreateMap<UserDetail, CreateUserDetailsResponse>().ReverseMap();
        CreateMap<UserDetail, CreateUserDetailsCommand>().ReverseMap();

        CreateMap<UserDetail, UserListVm>().ReverseMap();
    }
}
