using MediatR;

namespace PixelPlusMedia.Application.Features.Contents.Queries.GetContentList
{
    public class ContentListQuery : IRequest<List<ContentVm>>
    {
    }
}
