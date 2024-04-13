using MediatR;
using PixelPlusMedia.Application.Features.Submessages.Commands;

namespace PixelPlusMedia.Application.Features.SubMessages.Commands.UpdateSubMessage
{
    public class UpdateSubMessageCommand : SubMessageDto, IRequest<SubMessageResponse>
    {

    }
}
