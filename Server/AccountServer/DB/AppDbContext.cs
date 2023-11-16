using Microsoft.EntityFrameworkCore;

namespace AccountServer.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<AccountDb> Accounts { get; set; }

        public DbSet<PlayerDb> Player { get; set; }

        public DbSet<PlayerStatDb> PlayerStat { get; set; }

        public DbSet<ItemDb> Item { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccountDb>()
                    .HasIndex(a => a.AccountId)
                    .IsUnique();

            builder.Entity<PlayerDb>()
                    .HasIndex(p => p.PlayerId)
                    .IsUnique();


            builder.Entity<PlayerStatDb>()
                    .HasIndex(p => p.PlayerStatId)
                    .IsUnique();

            builder.Entity<ItemDb>()
                    .HasIndex(i => i.ItemId)
                    .IsUnique();
        }
    }
}
