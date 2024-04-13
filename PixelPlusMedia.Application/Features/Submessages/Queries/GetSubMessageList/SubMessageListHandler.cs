using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Application.Features.Submessages.Queries;

namespace PixelPlusMedia.Application.Features.SubMessages.Queries.GetSubMessageList
{
    public class SubMessageListHandler : IRequestHandler<SubMessageListQuery, SubMessageVmResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubMessageRepository _subMessageRepository;

        public SubMessageListHandler(IMapper mapper, ISubMessageRepository subMessageRepository)
        {
            _mapper = mapper;
            _subMessageRepository = subMessageRepository;
        }
        public async Task<SubMessageVmResponse> Handle(SubMessageListQuery request, CancellationToken cancellationToken)
        {
            var response = new SubMessageVmResponse();

            var allContent = (await _subMessageRepository.ListAllAsync());

            response.Contents = new List<SubMessageDto>();
            response.Contents = _mapper.Map<List<SubMessageDto>>(allContent);

            return response;
        }
    }
}
