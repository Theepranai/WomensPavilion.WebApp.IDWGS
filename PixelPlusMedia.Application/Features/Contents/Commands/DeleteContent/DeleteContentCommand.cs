using MediatR;

namespace PixelPlusMedia.Application.Features.Contents.Commands.DeleteContent
{
    public class DeleteContentCommand : IRequest<ContentResponse>
    {
        public Guid ContentId { get; set; }

    }
}
