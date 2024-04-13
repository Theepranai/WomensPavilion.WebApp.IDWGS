using PixelPlusMedia.Domain.Entities;

namespace PixelPlusMedia.Application.Contracts.Persistence;

public interface ISubMessageRepository : IAsyncRepository<SubMessage>
{
}