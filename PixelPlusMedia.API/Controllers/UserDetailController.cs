using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PixelPlusMedia.Application.Authorization;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Application.Features.UserDetails.Commands.CreateUser;
using PixelPlusMedia.Application.Features.UserDetails.Queries.GetUserList;

namespace PixelPlusMedia.API.Controllers
{
    [Route("api/participant")]
    [ApiController]
    public class UserDetailController : Controller
    {

        private readonly IMediator _mediator;
        private readonly IUserDetailRepository _userRepo;
        public UserDetailController(IMediator mediator, IUserDetailRepository userRepo)
        {
            _mediator = mediator;
            _userRepo = userRepo;
        }

        // [ValidateAntiForgeryToken]
       [HttpPost(Name = "CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreateUserDetailsResponse>> Create([FromForm] CreateUserDetailsCommand createUser)
        {
            var response = await _mediator.Send(createUser);
            return Ok(response);
        } 

        // [Authorize(Roles = "Administrator")]
        [HttpGet(Name = "GetUserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserListVm>> GetUserDetails([FromQuery] int days, [FromQuery] bool descOrder, bool onlyApproved)
        {
            var dtos = await _mediator.Send(new UserListQuery()
            {
                Days = days,
                OrderByDesc = descOrder,
                OnlyApproved = onlyApproved

            });
            return Ok(dtos);
        }

        [HttpPatch("{userId}", Name = "PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PartialUpdate([FromBody] JsonPatchDocument documentPatch, [FromRoute] Guid userId)
        {
            await _userRepo.UpdatePartialCustomOrder(userId, documentPatch);
            return NoContent();
        }
    }
}
