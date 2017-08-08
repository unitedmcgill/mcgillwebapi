using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace McGillWebAPI.Model
{
    public partial class AirSilenceContext : DbContext
    {
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductItem> ProductItem { get; set; }
        public virtual DbSet<ProductPrice> ProductPrice { get; set; }
        public virtual DbSet<RectSilencer> RectSilencer { get; set; }
        public virtual DbSet<RoundSilencer> RoundSilencer { get; set; }
        public virtual DbSet<RoundSilencerExtraData> RoundSilencerExtraData { get; set; }
        public virtual DbSet<Slide> Slide { get; set; }
        public virtual DbSet<SlideItem> SlideItem { get; set; }

        // Unable to generate entity type for table 'dbo.User'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Mfg'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning You need to set the environment variable SPIDERMAN_PASSWORD
            var password = Environment.GetEnvironmentVariable("SPIDERMAN_PASSWORD");
            optionsBuilder.UseSqlServer(@"Server=SPIDERMAN\mcgillweb;Database=AirSilence;User ID=sa;Password="+password+";");
        }

        // public AirSilenceContext( DbContextOptions<AirSilenceContext> options) : base(options)
        // {

        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.ImageSource).HasMaxLength(255);

                entity.Property(e => e.SafetyDesc).HasMaxLength(50);

                entity.Property(e => e.SafetyHref).HasMaxLength(255);

                entity.Property(e => e.SubCode).HasMaxLength(10);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductItem>(entity =>
            {
                entity.Property(e => e.ItemLine).HasColumnType("text");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductItem)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductItem_Product");
            });

            modelBuilder.Entity<ProductPrice>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal");

                entity.Property(e => e.Uom)
                    .HasColumnName("UOM")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Weight).HasColumnType("decimal");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPrice)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductPrice_Product");
            });

            modelBuilder.Entity<RectSilencer>(entity =>
            {
                entity.HasKey(e => new { e.Model, e.Length, e.Velocity })
                    .HasName("PK_Silencer");

                entity.Property(e => e.Model).HasColumnType("varchar(14)");

                entity.Property(e => e.Type).HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<RoundSilencer>(entity =>
            {
                entity.HasKey(e => new { e.Model, e.Diameter, e.Length, e.Velocity })
                    .HasName("PK_RoundSilencer");

                entity.Property(e => e.Model).HasColumnType("varchar(14)");

                entity.Property(e => e.Type).HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<RoundSilencerExtraData>(entity =>
            {
                entity.HasKey(e => e.Model)
                    .HasName("PK_RoundSilencerExtraData");

                entity.Property(e => e.Model).HasColumnType("varchar(14)");

                entity.Property(e => e.TlossCoef).HasColumnName("TLossCoef");
            });

            modelBuilder.Entity<Slide>(entity =>
            {
                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.SubTitle).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<SlideItem>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.Slide)
                    .WithMany(p => p.SlideItem)
                    .HasForeignKey(d => d.SlideId)
                    .HasConstraintName("FK_SlideItem_Slide");
            });
        }
    }
}