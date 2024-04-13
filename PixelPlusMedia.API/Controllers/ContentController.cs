using MediatR;
using Microsoft.AspNetCore.Mvc;
using PixelPlusMedia.Application.Features.Contents.Commands;
using PixelPlusMedia.Application.Features.Contents.Commands.CreateContent;
using PixelPlusMedia.Application.Features.Contents.Commands.DeleteContent;
using PixelPlusMedia.Application.Features.Contents.Commands.UpdateContent;
using PixelPlusMedia.Application.Features.Contents.Queries;
using PixelPlusMedia.Application.Features.Contents.Queries.GetContentById;
using PixelPlusMedia.Application.Features.Contents.Queries.GetContentList;

namespace PixelPlusMedia.API.Controllers;

[Route("api/content")]
[ApiController]
public class ContentController : Controller
{
    private readonly IMediator _mediator;
    public ContentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // [ValidateAntiForgeryToken]
    [HttpPost(Name = "CreateContent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ContentResponse>> Create([FromBody] CreateContentCommand createContent)
    {
        var response = await _mediator.Send(createContent);
        return Ok(response);
    }

    // [ValidateAntiForgeryToken]
    [HttpPut(Name = "UpdateContent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ContentResponse>> Update([FromBody] UpdateContentCommand updateContent)
    {
        var response = await _mediator.Send(updateContent);
        return Ok(response);
    }

    // [ValidateAntiForgeryToken]
    [HttpDelete("{contentId}", Name = "DeleteContent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ContentResponse>> Delete(Guid contentId)
    {
        var response = await _mediator.Send(new DeleteContentCommand() { ContentId = contentId});
        return Ok(response);
    }

    [HttpGet(Name = "GetContent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ContentVm>> Get()
    {
        var response = await _mediator.Send(new ContentListQuery());
        return Ok(response);
    }

    [HttpGet("{contentId}", Name = "GetContentById")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ContentVmResponse>> GetList(Guid contentId)
    {
        var response = await _mediator.Send(new ContentQuery() { ContentId = contentId });
        return Ok(response);
    }
}
