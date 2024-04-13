using MediatR;
using PixelPlusMedia.Application.Features.Submessages.Commands;
using PixelPlusMedia.Application.Features.Submessages.Queries;

namespace PixelPlusMedia.Application.Features.SubMessages.Queries.GetSubMessageById
{
    public class SubMessageQuery : IRequest<SubMessageVmResponse>
    {
        public Guid SubMessageId { get; set; }
    }
}
