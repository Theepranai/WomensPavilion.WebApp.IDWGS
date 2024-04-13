using PixelPlusMedia.Application.Features.SubMessages;
using PixelPlusMedia.Application.Responses;

namespace PixelPlusMedia.Application.Features.Submessages.Commands
{
    public class SubMessageResponse : BaseResponse
    {
        public SubMessageResponse() : base() { }

        public SubMessageDto Contents { get; set; }
    }
}
