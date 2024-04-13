using PixelPlusMedia.Application.Responses;

namespace PixelPlusMedia.Application.Features.UploadImage;

public class UploadImageCommandResponse:BaseResponse
{
    public UploadImageCommandResponse() : base()
    {}
    public string Url { get;set; }
}
