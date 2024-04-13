using MediatR;
using PixelPlusMedia.Application.Features.Submessages.Commands;

namespace PixelPlusMedia.Application.Features.SubMessages.Commands.DeleteSubMessage
{
    public class DeleteSubMessageCommand : IRequest<SubMessageResponse>
    {
        public Guid SubMessageId { get; set; }
    }
}
