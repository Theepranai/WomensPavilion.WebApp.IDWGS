using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Application.Features.SubMessages;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Features.Contents.Queries.GetContentById
{
    public class ContentQueryHandler : IRequestHandler<ContentQuery, ContentVmResponse>
    {
        private readonly IAsyncRepository<Content> _content;
        private readonly IAsyncRepository<SubMessage> _subMessage;
        private readonly IMapper _mapper;

        public ContentQueryHandler(IAsyncRepository<Content> content, IAsyncRepository<SubMessage> subMessage, IMapper mapper)
        {
            _content = content;
            _subMessage = subMessage;
            _mapper = mapper;
        }
        public async Task<ContentVmResponse> Handle(ContentQuery request, CancellationToken cancellationToken)
        {
            var response = new ContentVmResponse();

            var validator = new ContentQueryValidation();
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


            var getContent = (await _content.GetByIdAsync(request.ContentId));

            var allSubMessage = (await _subMessage.ListAllAsync());

            response = _mapper.Map<ContentVmResponse>(getContent);

            var subMessage = allSubMessage.Where(x => x.ContentId == response.ContentId).ToList();

            response.SubMessages = _mapper.Map<List<SubMessageDto>>(subMessage);

            return response;
        }
    }
}
