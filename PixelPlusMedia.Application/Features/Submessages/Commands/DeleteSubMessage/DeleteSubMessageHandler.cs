using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Application.Features.Submessages.Commands;

namespace PixelPlusMedia.Application.Features.SubMessages.Commands.DeleteSubMessage
{
    public class DeleteSubMessageHandler : IRequestHandler<DeleteSubMessageCommand, SubMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubMessageRepository _subMessageRepository;
        public DeleteSubMessageHandler(IMapper mapper, ISubMessageRepository subMessageRepository)
        {
            _mapper = mapper;
            _subMessageRepository = subMessageRepository;
        }
        public async Task<SubMessageResponse> Handle(DeleteSubMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new SubMessageResponse();

            var validator = new DeleteSubMessageValidation();
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

            response.Success = false;
            var content = await _subMessageRepository.GetByIdAsync(request.SubMessageId);

             if(content != null)
                {
                    try
                    {
                        await _subMessageRepository.DeleteAsync(content);
                        response.Success = true;
                    }
                    catch
                    {
                       
                    }
                }

            return response;
        }
    }
}
