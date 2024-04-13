using PixelPlusMedia.Application.Features.SubMessages;

namespace PixelPlusMedia.Application.Features.Contents.Queries
{
    public class ContentVm
    {
        public Guid ContentId { get; set; }
        public string WelcomeMessage { get; set; }
        public string ThankyouMessage { get; set; }
        public string Tnc { get; set; }

        public List<SubMessageDto> SubMessages {  get; set; }
    }
}
