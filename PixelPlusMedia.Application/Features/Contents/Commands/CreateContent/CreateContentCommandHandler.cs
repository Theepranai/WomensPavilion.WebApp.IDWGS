using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Features.Contents.Commands.CreateContent;

public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, ContentResponse>
{

    private readonly IMapper _mapper;
    private readonly IContentRepository _contentRepository;
    public CreateContentCommandHandler(IMapper mapper, IContentRepository contentRepository) { 
        _mapper= mapper;
        _contentRepository= contentRepository;
    }
    public async Task<ContentResponse> Handle(CreateContentCommand request, CancellationToken cancellationToken)
    {
        
        var response = new ContentResponse();
        var validator = new CreateContentCommandValidation();
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();

            foreach(var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        var content = _mapper.Map<Content>(request);
        content = await _contentRepository.AddAsync(content);

        response.Contents = _mapper.Map<ContentDto>(content);


        return response;
    }
}
