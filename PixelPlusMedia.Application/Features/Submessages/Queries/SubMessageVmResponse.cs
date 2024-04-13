using PixelPlusMedia.Application.Features.SubMessages;
using PixelPlusMedia.Application.Responses;

namespace PixelPlusMedia.Application.Features.Submessages.Queries
{
    public class SubMessageVmResponse : BaseResponse
    {
        public SubMessageVmResponse() : base() { }
        public List<SubMessageDto> Contents { get; set; }
    }
}
