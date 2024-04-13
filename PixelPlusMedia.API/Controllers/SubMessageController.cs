using Microsoft.AspNetCore.Mvc;
using PixelPlusMedia.Application.Features.SubMessages.Commands.CreateSubMessage;
using PixelPlusMedia.Application.Features.SubMessages.Commands.DeleteSubMessage;
using PixelPlusMedia.Application.Features.SubMessages.Commands.UpdateSubMessage;
using PixelPlusMedia.Application.Features.SubMessages.Queries.GetSubMessageById;
using PixelPlusMedia.Application.Features.SubMessages.Queries.GetSubMessageList;
using MediatR;
using PixelPlusMedia.Application.Features.Submessages.Commands;
using PixelPlusMedia.Application.Features.Submessages.Queries;

namespace PixelPlusMedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubMessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubMessageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // [ValidateAntiForgeryToken]
        [HttpPost(Name = "CreateSubMessage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SubMessageResponse>> Create([FromBody] CreateSubMessageCommand createSubMessage)
        {
            var response = await _mediator.Send(createSubMessage);
            return Ok(response);
        }

        // [ValidateAntiForgeryToken]
        [HttpPut(Name = "UpdateSubMessage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SubMessageResponse>> Update([FromBody] UpdateSubMessageCommand updateSubMessage)
        {
            var response = await _mediator.Send(updateSubMessage);
            return Ok(response);
        }

        // [ValidateAntiForgeryToken]
        [HttpDelete("{SubMessageId}", Name = "DeleteSubMessage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SubMessageResponse>> Delete(Guid SubMessageId)
        {
            var response = await _mediator.Send(new DeleteSubMessageCommand() { SubMessageId = SubMessageId });
            return Ok(response);
        }

        [HttpGet(Name = "GetSubMessage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SubMessageVmResponse>> Get()
        {
            var response = await _mediator.Send(new SubMessageListQuery());
            return Ok(response);
        }

        [HttpGet("{SubMessageId}", Name = "GetSubMessageById")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SubMessageVmResponse>> GetList(Guid SubMessageId)
        {
            var response = await _mediator.Send(new SubMessageQuery() { SubMessageId = SubMessageId });
            return Ok(response);
        }
    }
}
