using System.ComponentModel.DataAnnotations;

namespace PixelPlusMedia.Domain.Entities;

public class Content
{
    [Key]
    public Guid ContentId { get; set; }
    public string WelcomeMessage { get; set; }
    public string ThankyouMessage { get; set; }
    public string Tnc { get; set; }
}
