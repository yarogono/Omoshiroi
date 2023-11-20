using AccountServer.Model;
using AccountServer.Model.Item;
using Microsoft.EntityFrameworkCore;

namespace AccountServer.DB
{
    public class AppDbContext : DbContext
    {

        public DbSet<PlayerDb> Player { get; set; }

        public DbSet<PlayerStatDb> PlayerStat { get; set; }

        public DbSet<OauthDb> Oauth { get; set; }

        public DbSet<GuestDb> Guest { get; set; }

        public DbSet<InventoryDb> Inventory { get; set; }

        public DbSet<CurrencyDb> Currency { get; set; }

        public DbSet<MaterialItemDb> MaterialItem { get; set; }

        public DbSet<PotionItemDb> PotionItem { get; set; }

        public DbSet<RuneItemDb> RuneItem { get; set; }

        public DbSet<WeaponItemDb> WeaponItem { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlayerDb>()
                    .HasIndex(p => p.PlayerId)
                    .IsUnique();


            builder.Entity<PlayerStatDb>()
                    .HasIndex(p => p.PlayerStatId)
                    .IsUnique();

            builder.Entity<OauthDb>()
                    .HasIndex(o => o.OauthId)
                    .IsUnique();

            builder.Entity<GuestDb>()
                    .HasIndex(g => g.GuestId)
                    .IsUnique();

            builder.Entity<InventoryDb>()
                    .HasIndex(i => i.InventoryId)
                    .IsUnique();

            builder.Entity<CurrencyDb>()
                    .HasIndex(c => c.CurrencyId)
                    .IsUnique();

            builder.Entity<MaterialItemDb>()
                    .HasIndex(m => m.MaterialItemId)
                    .IsUnique();


            builder.Entity<PotionItemDb>()
                    .HasIndex(p => p.PotionItemId)
                    .IsUnique();
        }
    }
}
