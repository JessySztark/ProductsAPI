using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebApplication1.Models.EntityFramework
{
    public partial class ProductDBContext : DbContext
    {
        public ProductDBContext() { }
        public ProductDBContext(DbContextOptions<ProductDBContext> options)
            : base(options) { }

        public virtual DbSet<Marque> Marques { get; set; } = null!;
        public virtual DbSet<Produit> Produits { get; set; } = null!;
        public virtual DbSet<TypeProduit> TypesProduit { get; set; } = null!;

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=ProductDB; uid=postgres; password=postgres;");
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marque>(entity =>
            {
                entity.HasKey(e => e.MarqueID)
                    .HasName("pk_mrq");
            });

            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => e.ProduitID)
                    .HasName("pk_pdt");

                entity.HasOne(d => d.TypeProduit_Produit)
                    .WithMany(p => p.Produit_TypeProduit)
                    .HasForeignKey(d => d.TypeProduitID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pdt_typ");

                entity.HasOne(d => d.MarqueProduit)
                    .WithMany(p => p.ProduitMarque)
                    .HasForeignKey(d => d.MarqueID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pdt_mrq");
            });

            modelBuilder.Entity<TypeProduit>(entity =>
            {
                entity.HasKey(e => new { e.TypeProduitID })
                    .HasName("pk_typ");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
