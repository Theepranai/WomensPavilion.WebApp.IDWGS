using FluentValidation;

namespace PixelPlusMedia.Application.Features.SubMessages.Commands.DeleteSubMessage
{
    public class DeleteSubMessageValidation : AbstractValidator<DeleteSubMessageCommand>
    {
        public DeleteSubMessageValidation() 
        {
            RuleFor(x => x.SubMessageId).NotEmpty().NotNull();
        }
    }
}
