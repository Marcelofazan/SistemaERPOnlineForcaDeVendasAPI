using Microsoft.Extensions.DependencyInjection;
using SistemaERPOnlineForcaDeVendasAPI.Aplicacao.Produto;

namespace SistemaERPOnlineForcaDeVendasAPI.Aplicacao;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProdutoService, ProdutoService>();
        return services;
    }
}
