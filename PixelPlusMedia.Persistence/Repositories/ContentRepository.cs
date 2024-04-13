using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Persistence.Repositories;

public class ContentRepository:BaseRepository<Content>,IContentRepository
{
    public ContentRepository(AppDbContext dbContext) : base(dbContext) { }

}