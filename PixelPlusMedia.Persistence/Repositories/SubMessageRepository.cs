using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Persistence.Repositories
{
    public class SubMessageRepository:BaseRepository<SubMessage>, ISubMessageRepository
    {
        public SubMessageRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
