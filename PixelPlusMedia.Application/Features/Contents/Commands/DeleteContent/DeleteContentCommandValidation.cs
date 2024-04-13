using FluentValidation;

namespace PixelPlusMedia.Application.Features.Contents.Commands.DeleteContent
{
    public class DeleteContentCommandValidation : AbstractValidator<DeleteContentCommand>
    {
        public DeleteContentCommandValidation()
        {
            RuleFor(cn => cn.ContentId)
               .NotNull()
               .NotEmpty();
        }
    }
}
