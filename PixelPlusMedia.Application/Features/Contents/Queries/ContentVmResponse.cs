using PixelPlusMedia.Application.Features.SubMessages;
using PixelPlusMedia.Application.Responses;

namespace PixelPlusMedia.Application.Features.Contents.Queries
{
    public class ContentVmResponse : BaseResponse
    {
        public Guid ContentId { get; set; }
        public string WelcomeMessage { get; set; }
        public string ThankyouMessage { get; set; }
        public string Tnc { get; set; }

        public List<SubMessageDto> SubMessages { get; set; }
    }
}
