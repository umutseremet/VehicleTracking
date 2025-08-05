using Microsoft.EntityFrameworkCore;
using AracTakip.Api.Models;

namespace AracTakip.Api.Data
{
    public class AracTakipDbContext : DbContext
    {
        public AracTakipDbContext(DbContextOptions<AracTakipDbContext> options) : base(options)
        {
        }

        public DbSet<Arac> Araclar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arac>(entity =>
            {
                entity.ToTable("Araclar");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Plaka).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Marka).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Model).HasMaxLength(50).IsRequired();
                entity.Property(e => e.VIN).HasMaxLength(50).IsRequired();
                entity.Property(e => e.SirketAdi).HasMaxLength(100).IsRequired();
                entity.Property(e => e.KaskoTrafik).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastikDurumu).HasMaxLength(20).IsRequired();
                entity.Property(e => e.RuhsatBilgisi).HasMaxLength(100).IsRequired();
                entity.Property(e => e.SirketKiralama).HasMaxLength(50).IsRequired();
                entity.Property(e => e.KullaniciAdi).HasMaxLength(100).IsRequired();
                entity.Property(e => e.KullaniciTelefon).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Konum).HasMaxLength(100).IsRequired();
                entity.Property(e => e.AracResimUrl).HasMaxLength(500);
                entity.Property(e => e.YakitTuketimi).HasPrecision(4, 1);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}