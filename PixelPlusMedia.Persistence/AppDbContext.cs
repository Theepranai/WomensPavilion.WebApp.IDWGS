
using Microsoft.EntityFrameworkCore;
using PixelPlusMedia.Application.Contracts;
using PixelPlusMedia.Domain.Common;
using PixelPlusMedia.Domain.Entities;
using PixelPlusMedia.Persistence.Helper;

namespace PixelPlusMedia.Persistence;

public class AppDbContext: DbContext
{
    private readonly ILoggedInUserService _loggedInUserService;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public AppDbContext(DbContextOptions<AppDbContext> options, ILoggedInUserService loggedInUserService) : base(options) { 
        _loggedInUserService = loggedInUserService;
    }

    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<Content> Content { get; set; }
    public DbSet<SubMessage> SubMessage { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyUtcDateTimeConverter();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = _loggedInUserService.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
