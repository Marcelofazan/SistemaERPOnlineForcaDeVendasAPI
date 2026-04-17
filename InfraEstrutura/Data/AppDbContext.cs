using Microsoft.EntityFrameworkCore;
using SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades;

namespace SistemaERPOnlineForcaDeVendasAPI.InfraEstrutura.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Produto> Produtos => Set<Produto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.IdEmpresa).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(120).IsRequired();
            entity.Property(e => e.SenhaHash).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Nome).HasMaxLength(80).IsRequired();
            entity.Property(e => e.TaxaPercentual).HasPrecision(5, 2).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.IdProduto).IsRequired();
            entity.Property(e => e.IdEmpresa).IsRequired();
            entity.Property(p => p.ValorUltimaCompra).HasPrecision(18, 4).IsRequired();
            entity.Property(p => p.LucroMinimo).HasPrecision(18, 4).IsRequired();
            entity.Property(p => p.LucroMaximo).HasPrecision(18, 4).IsRequired();
            entity.Property(p => p.PrecoVendaMinimo).HasPrecision(18, 4).IsRequired();
            entity.Property(p => p.PrecoSugerido).HasPrecision(18, 4).IsRequired();

        });
    }
}
