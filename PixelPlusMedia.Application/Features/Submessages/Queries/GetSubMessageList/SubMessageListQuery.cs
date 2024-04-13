using MediatR;
using PixelPlusMedia.Application.Features.Submessages.Queries;

namespace PixelPlusMedia.Application.Features.SubMessages.Queries.GetSubMessageList
{
    public class SubMessageListQuery : IRequest<SubMessageVmResponse>
    {
    }
}
