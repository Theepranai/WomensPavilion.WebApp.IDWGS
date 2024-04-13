using PixelPlusMedia.Application.Responses;

namespace PixelPlusMedia.Application.Features.Contents.Commands
{
    public class ContentResponse : BaseResponse
    {
        public ContentResponse() : base() { }
        public ContentDto Contents { get; set; }
    }
}
