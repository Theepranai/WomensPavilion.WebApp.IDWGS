using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;

namespace PixelPlusMedia.Application.Features.Contents.Commands.DeleteContent
{
    public class DeleteContentCommandHandler : IRequestHandler<DeleteContentCommand, ContentResponse>
    {
        private readonly IContentRepository _contentRepository;

        public DeleteContentCommandHandler(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }
        public async Task<ContentResponse> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
        {
            var response = new ContentResponse()
            {
                Success = false
            };

            var validator = new DeleteContentCommandValidation();
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

            var content = await _contentRepository.GetByIdAsync(request.ContentId);
            if (content != null)
            {
                try
                {
                    var delContent = await _contentRepository.DeleteAsync(content);
                    response.Success = true;
                }
                catch
                {
                    response.Success = false;
                }              
            }

            return response;
        }
    }
}
