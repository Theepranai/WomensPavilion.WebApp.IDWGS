using FluentValidation;

namespace PixelPlusMedia.Application.Features.SubMessages.Queries.GetSubMessageById
{
    public class SubMessageQueryValidation : AbstractValidator<SubMessageQuery>
    {
        public SubMessageQueryValidation()
        {
            RuleFor(x => x.SubMessageId).NotNull().NotEmpty();
        }
    }
}
