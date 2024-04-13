using FluentValidation;

namespace PixelPlusMedia.Application.Features.SubMessages.Commands.UpdateSubMessage
{
    public class UpdateSubMessageValidation : AbstractValidator<UpdateSubMessageCommand>
    {
        public UpdateSubMessageValidation()
        {
            RuleFor(x => x.SubMessageId).NotEmpty().NotNull();
            RuleFor(x => x.ContentId).NotNull().NotEmpty();
            RuleFor(x => x.Top).NotEmpty();
        }
    }
}
