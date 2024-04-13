namespace PixelPlusMedia.Application.Features.SubMessages
{
    public class SubMessageDto
    {
        public Guid SubMessageId { get; set; }

        public string Top { get; set; }
        public string Right { get; set; }
        public string Bottom { get; set; }
        public string Left { get; set; }

        public Guid ContentId { get; set; }
    }
}
