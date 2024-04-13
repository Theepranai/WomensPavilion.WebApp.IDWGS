using MediatR;

namespace PixelPlusMedia.Application.Features.Contents.Queries.GetContentById
{
    public class ContentQuery : IRequest<ContentVmResponse>
    {
        public Guid ContentId { get; set; }
    }
}
