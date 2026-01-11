using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestBlog.Models;

public partial class BlogDbContext : DbContext
{
    public BlogDbContext()
    {
    }

    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-07NSNMOC;Database=BlogPersonal;User ID=sa;Password=loc@del@rea;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.CodigoBlog).HasName("PK__Blog__87433BD2885DE84D");

            entity.HasOne(d => d.CodigoCategoriaNavigation).WithMany(p => p.Blogs).HasConstraintName("FK__Blog__CodigoCate__29572725");

            entity.HasOne(d => d.CodigoUsuarioNavigation).WithMany(p => p.Blogs).HasConstraintName("FK__Blog__CodigoUsua__286302EC");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CodigoCategoria).HasName("PK__Categori__3CEE2F4CB2857F3C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.CodigoUsuario).HasName("PK__Usuario__F0C18F58323DD908");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
