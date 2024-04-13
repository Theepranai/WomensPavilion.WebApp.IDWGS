using PixelPlusMedia.Application.Responses;

namespace PixelPlusMedia.Application.Features.UserDetails.Commands.CreateUser;

public class CreateUserDetailsResponse : BaseResponse
{
    public CreateUserDetailsResponse():base(){} 
    public CreateUserDetailsDto UserDetails {  get; set; }
}
