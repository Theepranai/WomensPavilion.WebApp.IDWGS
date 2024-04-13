using Microsoft.AspNetCore.JsonPatch;
using PixelPlusMedia.Domain.Entities;
namespace PixelPlusMedia.Application.Contracts.Persistence;

public interface IUserDetailRepository : IAsyncRepository<UserDetail>
{
    Task UpdatePartialCustomOrder(Guid userId, JsonPatchDocument documentPatch);
}
