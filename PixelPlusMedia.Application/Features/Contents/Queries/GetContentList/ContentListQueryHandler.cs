using AutoMapper;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Application.Features.SubMessages;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Features.Contents.Queries.GetContentList
{
    public class ContentListQueryHandler : IRequestHandler<ContentListQuery, List<ContentVm>>
    {
        private readonly IAsyncRepository<Content> _content;
        private readonly IAsyncRepository<SubMessage> _subMessage;
        private readonly IMapper _mapper;

        public ContentListQueryHandler(IAsyncRepository<Content> content, IAsyncRepository<SubMessage> subMessage, IMapper mapper)
        {
            _content = content;
            _subMessage = subMessage;
            _mapper = mapper;
        }

        public async Task<List<ContentVm>> Handle(ContentListQuery request, CancellationToken cancellationToken)
        {
            var contentListVm = new List<ContentVm>();

            var allContent = (await _content.ListAllAsync());

            var allSubMessage = (await _subMessage.ListAllAsync());

            foreach (var content in allContent)
            {
                var c = _mapper.Map<ContentVm>(content);

                var subMessage = allSubMessage.Where(x => x.ContentId == content.ContentId).ToList();

                c.SubMessages = _mapper.Map<List<SubMessageDto>>(subMessage);

                contentListVm.Add(c);
            }

            return contentListVm;
        }
    }
}
