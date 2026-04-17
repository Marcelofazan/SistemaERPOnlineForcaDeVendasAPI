using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaERPOnlineForcaDeVendasAPI.Aplicacao.Produto;
using SistemaERPOnlineForcaDeVendasAPI.InfraEstrutura.Data;
using SistemaERPOnlineForcaDeVendasAPI.InfraEstrutura.Repositorios;

namespace SistemaERPOnlineForcaDeVendasAPI.InfraEstrutura;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        return services;
    }
}
