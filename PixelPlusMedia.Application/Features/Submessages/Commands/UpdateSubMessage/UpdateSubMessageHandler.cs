using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Application.Features.Submessages.Commands;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Features.SubMessages.Commands.UpdateSubMessage
{
    public class UpdateSubMessageHandler : IRequestHandler<UpdateSubMessageCommand, SubMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubMessageRepository _subMessageRepository;

        public UpdateSubMessageHandler(IMapper mapper, ISubMessageRepository subMessageRepository)
        {
            _mapper = mapper;
            _subMessageRepository = subMessageRepository;
        }
        public async Task<SubMessageResponse> Handle(UpdateSubMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new SubMessageResponse();
            var validator = new UpdateSubMessageValidation();
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

            var content = await _subMessageRepository.GetByIdAsync(request.SubMessageId);
            if (content == null) throw new ArgumentNullException();

            _mapper.Map(request, content, typeof(UpdateSubMessageCommand), typeof(SubMessage));
            content = await _subMessageRepository.UpdateAsync(content);
            response.Contents = _mapper.Map<SubMessageDto>(content);

            return response;
        }
    }
}
