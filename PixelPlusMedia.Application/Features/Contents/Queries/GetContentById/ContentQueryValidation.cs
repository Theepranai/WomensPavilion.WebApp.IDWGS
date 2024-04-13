using FluentValidation;

namespace PixelPlusMedia.Application.Features.Contents.Queries.GetContentById
{
    public class ContentQueryValidation : AbstractValidator<ContentQuery>
    {
        public ContentQueryValidation()
        {
            RuleFor(cn => cn.ContentId)
             .NotNull()
             .NotEmpty();
        }
    }
}
