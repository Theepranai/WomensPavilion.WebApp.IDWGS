using System;
namespace PixelPlusMedia.API.Utility;

public class FileResultContentTypeAttribute
{
    public FileResultContentTypeAttribute(string contentType)
    {
        ContentType = contentType;
    }

    public string ContentType { get; }
}
