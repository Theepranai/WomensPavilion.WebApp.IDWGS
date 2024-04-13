using FluentValidation;
using PixelPlusMedia.Application.Contracts.Persistence;

namespace PixelPlusMedia.Application.Features.SubMessages.Commands.CreateSubMessage
{
    public class CreateSubMessageValidation : AbstractValidator<CreateSubMessageCommand>
    {
        IContentRepository _contentRepository;
        public CreateSubMessageValidation(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
            RuleFor(x => x.ContentId).MustAsync(async (id, cancellation) =>
            {
                var exists = await _contentRepository.GetByIdAsync(id);
                return exists?.ContentId != null;
            }).WithMessage("ContentId not found.");

            RuleFor(x => x.ContentId).NotNull().NotEmpty();
            RuleFor(x => x.Top).NotEmpty();
           
        }
    }
}
