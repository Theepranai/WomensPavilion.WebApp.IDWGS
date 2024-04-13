using FluentValidation;

namespace PixelPlusMedia.Application.Features.Contents.Commands.CreateContent
{
    public  class CreateContentCommandValidation:AbstractValidator<CreateContentCommand>
    {
        public CreateContentCommandValidation()
        {
            RuleFor(cn => cn.WelcomeMessage)
                .NotNull()
                .NotEmpty();

            RuleFor(cn => cn.ThankyouMessage)
                .NotNull()
                .NotEmpty();

            RuleFor(cn => cn.Tnc)
                .NotNull()
                .NotEmpty();
        }
    }
}
