using MediatR;
using PixelPlusMedia.Application.Features.Submessages.Commands;

namespace PixelPlusMedia.Application.Features.SubMessages.Commands.CreateSubMessage
{
    public class CreateSubMessageCommand : IRequest<SubMessageResponse>
    {
        public string Top { get; set; }
        public string Right { get; set; }
        public string Bottom { get; set; }
        public string Left { get; set; }

        public Guid ContentId { get; set; }
    }
}
