using Microsoft.AspNetCore.JsonPatch;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Persistence.Repositories;
public class UserDetailRepository : BaseRepository<UserDetail>, IUserDetailRepository
{
    public UserDetailRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task UpdatePartialCustomOrder(Guid userId, JsonPatchDocument documentPatch)
    {
        var user = await _dbContext.UserDetails.FindAsync(userId);
        if (user != null)
        {
            documentPatch.ApplyTo(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
