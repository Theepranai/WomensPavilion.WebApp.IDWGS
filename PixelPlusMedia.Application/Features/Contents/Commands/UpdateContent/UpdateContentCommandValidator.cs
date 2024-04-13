using FluentValidation;

namespace PixelPlusMedia.Application.Features.Contents.Commands.UpdateContent;

public class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
{
    public UpdateContentCommandValidator()
    {

        RuleFor(cn => cn.ContentId)
            .NotNull()
            .NotEmpty();

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
