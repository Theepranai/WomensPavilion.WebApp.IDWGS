using MediatR;
using Microsoft.AspNetCore.Mvc;
using PixelPlusMedia.Application.Features.UploadImage;

namespace PixelPlusMedia.API.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UploadImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "UploadImage")]
        public async Task<ActionResult<UploadImageCommandResponse>> UploadImage([FromForm] UploadImageCommand uploadImageCommand)
        {
            var response = await _mediator.Send(uploadImageCommand);
            return Ok(response);
        }

    }
}
