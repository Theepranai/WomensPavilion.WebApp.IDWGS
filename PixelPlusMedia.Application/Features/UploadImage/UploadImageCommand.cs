using MediatR;
using Microsoft.AspNetCore.Http;

namespace PixelPlusMedia.Application.Features.UploadImage
{
    public class UploadImageCommand:IRequest<UploadImageCommandResponse>
    {
        public IFormFile ImagePath { get; set; }
    }
}
