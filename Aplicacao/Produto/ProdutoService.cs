using SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades;

namespace SistemaERPOnlineForcaDeVendasAPI.Aplicacao.Produto;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProdutoResponse?> GetByIdAsync(int Id, CancellationToken cancellationToken = default)
    {
        var produto = await _repository.GetByIdAsync(Id, cancellationToken);
        return produto is null ? null : ToResponse(produto);
    }

    public async Task<IReadOnlyList<ProdutoResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var produtos = await _repository.GetAllAsync(cancellationToken);
        return produtos.Select(ToResponse).ToList();
    }

    public async Task<ProdutoResponse> CreateAsync(ProdutoCreateRequest request, CancellationToken cancellationToken = default)
    {
        var produto = new Dominio.Entidades.Produto
        {
            IdEmpresa = request.IdEmpresa,
            IdProduto = request.IdProduto,
            ValorUltimaCompra = request.ValorUltimaCompra,
            LucroMinimo = request.LucroMinimo,
            LucroMaximo = request.LucroMaximo,
            PrecoVendaMinimo = request.PrecoVendaMinimo,
            PrecoSugerido = request.PrecoSugerido,
        };
        var created = await _repository.AddAsync(produto, cancellationToken);
        return ToResponse(created);
    }

    public async Task<ProdutoResponse?> UpdateAsync(int Id, ProdutoUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var produto = await _repository.GetByIdAsync(Id, cancellationToken);
        if (produto is null) return null;

        produto.IdEmpresa = request.IdEmpresa;
        produto.IdProduto = request.IdProduto;
        produto.ValorUltimaCompra = request.ValorUltimaCompra;
        produto.LucroMinimo = request.LucroMinimo;
        produto.LucroMaximo = request.LucroMaximo;
        produto.PrecoVendaMinimo = request.PrecoVendaMinimo;
        produto.PrecoSugerido = request.PrecoSugerido;
        await _repository.UpdateAsync(produto, cancellationToken);
        return ToResponse(produto);
    }

    public async Task<bool> DeleteAsync(int Id, CancellationToken cancellationToken = default)
    {
        var produto = await _repository.GetByIdAsync(Id, cancellationToken);
        if (produto is null) return false;
        await _repository.DeleteAsync(produto, cancellationToken);
        return true;
    }

    private static ProdutoResponse ToResponse(Dominio.Entidades.Produto produto) =>
        new(produto.Id, produto.IdEmpresa, produto.IdProduto, produto.ValorUltimaCompra, produto.LucroMinimo, produto.LucroMaximo, produto.PrecoVendaMinimo, produto.PrecoSugerido);
}
