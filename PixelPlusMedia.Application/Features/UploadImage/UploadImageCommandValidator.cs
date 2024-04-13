using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PixelPlusMedia.Application.Features.UploadImage;

public class UploadImageCommandValidator:AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator()
    {

        /*        RuleFor(um => um)
                    .MustAsync(IsValidImageFile)
                    .WithMessage("Invalid file type");*/


        RuleFor(um => um.ImagePath)
            .NotEmpty()
            .NotNull();
    }

    private async Task<bool> IsValidImageFile(UploadImageCommand image, CancellationToken token)
    {

        // Check file length
        if (image.ImagePath.Length < 0)
        {
            return false;
        }

        // Check file extension to prevent security threats associated with unknown file types
        string[] permittedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };
        var ext = Path.GetExtension(image.ImagePath.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains<string>(ext))
        {
            return false;
        }

        // Check if file size is greater than permitted limit
        if (image.ImagePath.Length > 6000) // 6MB
        {
            return false;
        }

        return true;
    }

}
