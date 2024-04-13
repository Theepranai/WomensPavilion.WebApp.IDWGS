using Microsoft.AspNetCore.Http;
using PixelPlusMedia.Application.Models.Aws;

namespace PixelPlusMedia.Application.Contracts.aws;

public interface IAWSServiceRepository
{
    Task<string> UploadFileAsync(ConfigKey client, IFormFile filePath);

}
