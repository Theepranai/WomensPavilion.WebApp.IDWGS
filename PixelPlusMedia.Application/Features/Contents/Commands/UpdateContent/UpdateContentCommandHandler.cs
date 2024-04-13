using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Features.Contents.Commands.UpdateContent;

public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, ContentResponse>
{

    IMapper _mapper;
    IContentRepository _contentRepository;
    public UpdateContentCommandHandler(IMapper mapper, IContentRepository contentRepository)
    {
        _mapper = mapper;
        _contentRepository = contentRepository;
    }

    public async Task<ContentResponse> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
    {
        var response = new ContentResponse();
        var validator = new UpdateContentCommandValidator();
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Count > 0)
        {
            response.Success= false;
            response.ValidationErrors = new List<string>();

            foreach(var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        var content = await _contentRepository.GetByIdAsync(request.ContentId);
        if (content == null)  throw new ArgumentNullException();

        _mapper.Map(request, content, typeof(UpdateContentCommand), typeof(Content));
        content = await _contentRepository.UpdateAsync(content);
        response.Contents = _mapper.Map<ContentDto>(content);

        return response;
    }
}
