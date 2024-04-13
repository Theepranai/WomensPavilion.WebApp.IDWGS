using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Application.Features.Submessages.Commands;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Features.SubMessages.Commands.CreateSubMessage
{
    public class CreateSubMessageHandler : IRequestHandler<CreateSubMessageCommand, SubMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubMessageRepository _subMessageRepository;
        private readonly IContentRepository _contentRepository;

        public CreateSubMessageHandler(IMapper mapper, ISubMessageRepository subMessageRepository, IContentRepository contentRepository)
        {
            _mapper = mapper;
            _subMessageRepository = subMessageRepository;
            _contentRepository = contentRepository;
        }

        public async Task<SubMessageResponse> Handle(CreateSubMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new SubMessageResponse();

            var validator = new CreateSubMessageValidation(_contentRepository);
            var validationResult = await validator.ValidateAsync(request);

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

            var content = _mapper.Map<SubMessage>(request);
            content = await _subMessageRepository.AddAsync(content);

            response.Contents = _mapper.Map<SubMessageDto>(content);

          return response;
        }
    }
}
