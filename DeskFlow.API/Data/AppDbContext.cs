using DeskFlow.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Chamado> Chamados => Set<Chamado>();
    public DbSet<Interacao> Interacoes => Set<Interacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chamado>()
            .Property(c => c.Prioridade).HasConversion<string>().HasMaxLength(20);

        modelBuilder.Entity<Chamado>()
            .Property(c => c.Status).HasConversion<string>().HasMaxLength(20);

        modelBuilder.Entity<Categoria>()
            .HasMany(c => c.Chamados)
            .WithOne(c => c.Categoria)
            .HasForeignKey(c => c.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Chamado>()
            .HasMany(c => c.Interacoes)
            .WithOne(i => i.Chamado)
            .HasForeignKey(i => i.ChamadoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}