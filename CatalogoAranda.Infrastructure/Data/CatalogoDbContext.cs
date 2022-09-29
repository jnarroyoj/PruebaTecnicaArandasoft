using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CatalogoAranda.ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CatalogoAranda.Infrastructure.Data
{
    public partial class CatalogoDbContext : IdentityDbContext<CatalogoUser>
    {

        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Imagen> Imagenes { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "cdda7ff4-4287-42f4-b1f6-6d710ae37e1e",
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR"
                }
                );

            modelBuilder.Entity<CatalogoUser>().HasData(
                new CatalogoUser
                {
                    Id = "93c22be8-1dfc-40a6-988d-409fb86aa29f",
                    UserName = "admin",
                    NormalizedUserName="ADMIN",
                    PasswordHash = hasher.HashPassword(new IdentityUser(), "adminPassword"),
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "cdda7ff4-4287-42f4-b1f6-6d710ae37e1e",
                    UserId = "93c22be8-1dfc-40a6-988d-409fb86aa29f"
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
                entity.HasIndex(e => e.Nombre, "IX_Productos_Nombre").IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasColumnType("nvarchar(1000)");

                entity.Property(e => e.Nombre).HasColumnType("nvarchar(250)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
