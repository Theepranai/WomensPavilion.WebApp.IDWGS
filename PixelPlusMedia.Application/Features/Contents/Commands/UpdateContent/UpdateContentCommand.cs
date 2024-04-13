using MediatR;
namespace PixelPlusMedia.Application.Features.Contents.Commands.UpdateContent;

public class UpdateContentCommand: IRequest<ContentResponse>
{
    public Guid ContentId { get; set; }
    public string WelcomeMessage { get; set; }
    public string ThankyouMessage { get; set; }
    public string Tnc { get; set; }
}
