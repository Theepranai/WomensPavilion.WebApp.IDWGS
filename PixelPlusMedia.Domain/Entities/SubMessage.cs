using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PixelPlusMedia.Domain.Entities;

public  class SubMessage
{
    [Key]
    public Guid SubMessageId { get; set; }
    public string Top { get; set; }
    public string Right { get; set; }
    public string Bottom { get; set; }
    public string Left { get; set; }

    [ForeignKey("ContentId")]
    public Content Content { get; set; }
    public Guid ContentId { get; set; }
}
