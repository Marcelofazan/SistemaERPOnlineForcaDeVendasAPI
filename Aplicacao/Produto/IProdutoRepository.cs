using SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades;  

namespace SistemaERPOnlineForcaDeVendasAPI.Aplicacao.Produto;

public interface IProdutoRepository
{
    Task<Dominio.Entidades.Produto?> GetByIdAsync(int Id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Dominio.Entidades.Produto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Dominio.Entidades.Produto> AddAsync(Dominio.Entidades.Produto produto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Dominio.Entidades.Produto produto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Dominio.Entidades.Produto produto, CancellationToken cancellationToken = default);
}
