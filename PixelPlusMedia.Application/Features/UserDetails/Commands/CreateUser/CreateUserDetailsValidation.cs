using FluentValidation;

namespace PixelPlusMedia.Application.Features.UserDetails.Commands.CreateUser;
public class CreateUserDetailsValidation : AbstractValidator<CreateUserDetailsCommand>
{
    public CreateUserDetailsValidation()
    {

        RuleFor(ud => ud.ImageFile)
            .NotNull()
            .NotEmpty();

        RuleFor(ud => ud.DefaultMessage)
            .NotNull()
            .NotEmpty()
            .Length(1,30);

        RuleFor(ud => ud.Message)
            .NotNull()
            .NotEmpty()
            .Length(1, 30);

        RuleFor(ud => ud.DateRegistered)
            .NotEmpty()
            .NotNull();

        RuleFor(ud => ud)
            .MustAsync(IsValidImageFile)
            .WithMessage("Invalid file type");
    }

    private async Task<bool> IsValidImageFile(CreateUserDetailsCommand image, CancellationToken token)
    {

        // Check file length
        if (image.ImageFile.Length < 0)
        {
            return false;
        }

        // Check file extension to prevent security threats associated with unknown file types
        string[] permittedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
        var ext = Path.GetExtension(image.ImageFile.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains<string>(ext))
        {
            return false;
        }

        // Check if file size is greater than permitted limit
        if (image.ImageFile.Length > 6291456) // 6MB
        {
            return false;
        }

        return true;
    }


}
