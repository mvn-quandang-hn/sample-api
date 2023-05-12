using Microsoft.EntityFrameworkCore;

namespace WebAPI.Application.Models
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public virtual DbSet<Languages> Languages { get; set; }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default
        )
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseModel trackAble)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackAble.UpdatedAt = utcNow;
                            entry.Property("CreatedAt").IsModified = false;
                            break;
                        case EntityState.Added:
                            trackAble.CreatedAt = utcNow;
                            trackAble.UpdatedAt = utcNow;
                            break;
                    }
                }
            }
        }
    }
}
