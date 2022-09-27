using System;
using System.Collections.Generic;
using CatalogoAranda.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CatalogoAranda.Infrastructure.Data
{
    public partial class CatalogoDbContext : IdentityDbContext<CatalogoUser>
    {
        public CatalogoDbContext()
        {
        }

        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Imagen> Imagenes { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PruebaTecnicaDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<CatalogoUser>().HasData(
                new CatalogoUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    NormalizedUserName="ADMIN",
                    PasswordHash = hasher.HashPassword(new IdentityUser(), "adminPassword")
                }
            );
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasIndex(e => e.Nombre, "IX_Categorias_Nombre").IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasColumnType("ntext");

                entity.Property(e => e.Nombre).HasColumnType("nvarchar(250)");

                entity.HasMany(d => d.Productos)
                    .WithMany(p => p.Categoria)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProductoCategorium",
                        l => l.HasOne<Producto>().WithMany().HasForeignKey("ProductoId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ProductoCategoria_A_Productos"),
                        r => r.HasOne<Categoria>().WithMany().HasForeignKey("CategoriaId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ProductoCategoria_A_Categorias"),
                        j =>
                        {
                            j.HasKey("CategoriaId", "ProductoId").HasName("PK__Producto__D910CB0F80D358B2");

                            j.ToTable("ProductoCategoria");
                        });
            });

            modelBuilder.Entity<Imagen>(entity =>
            {
                entity.HasIndex(e => e.ProductoId, "IX_Imagenes_ProductoId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Base64).HasColumnType("text");

                entity.Property(e => e.Nombre).HasColumnType("ntext");

                entity.Property(e => e.Url).HasColumnType("text");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Imagenes)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Imagenes_A_Productos");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasColumnType("ntext");

                entity.Property(e => e.Nombre).HasColumnType("ntext");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
