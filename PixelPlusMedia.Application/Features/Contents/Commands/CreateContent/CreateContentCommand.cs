using MediatR;

namespace PixelPlusMedia.Application.Features.Contents.Commands.CreateContent
{
    public class CreateContentCommand : IRequest<ContentResponse>
    {
        public string WelcomeMessage { get; set; }
        public string ThankyouMessage { get; set; }
        public string Tnc { get; set; }
    }
}
