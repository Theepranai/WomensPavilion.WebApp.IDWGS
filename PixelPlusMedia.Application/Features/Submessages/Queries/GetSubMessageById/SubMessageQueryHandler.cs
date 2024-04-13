using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Application.Features.Submessages.Queries;

namespace PixelPlusMedia.Application.Features.SubMessages.Queries.GetSubMessageById
{
    public class SubMessageQueryHandler : IRequestHandler<SubMessageQuery, SubMessageVmResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubMessageRepository _subMessageRepository;
        public SubMessageQueryHandler(IMapper mapper, ISubMessageRepository subMessageRepository)
        {
            _mapper = mapper;
            _subMessageRepository = subMessageRepository;
        }
        public async Task<SubMessageVmResponse> Handle(SubMessageQuery request, CancellationToken cancellationToken)
        {
            var response = new SubMessageVmResponse();
            var validator = new SubMessageQueryValidation();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }

                return response;
            }

            var getContent = (await _subMessageRepository.GetByIdAsync(request.SubMessageId));

            response.Contents = new List<SubMessageDto>();

            response.Contents.Add(_mapper.Map<SubMessageDto>(getContent));

            return response;
        }
    }
}
