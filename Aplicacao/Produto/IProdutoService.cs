namespace SistemaERPOnlineForcaDeVendasAPI.Aplicacao.Produto;

public interface IProdutoService
{
    Task<ProdutoResponse?> GetByIdAsync(int Id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProdutoResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProdutoResponse> CreateAsync(ProdutoCreateRequest request, CancellationToken cancellationToken = default);
    Task<ProdutoResponse?> UpdateAsync(int Id, ProdutoUpdateRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int Id, CancellationToken cancellationToken = default);
}
