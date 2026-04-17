using Moq;
using SistemaERPOnlineForcaDeVendasAPI.Aplicacao.Produto;
using SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades;
using Xunit;

namespace SistemaERPOnlineForcaDeVendasAPI.Testes.Servicos;

public class ProdutoServicoTestes
{
    private readonly Mock<IProdutoRepository> _repositoryMock;
    private readonly ProdutoService _service;

    public ProdutoServicoTestes()
    {
        _repositoryMock = new Mock<IProdutoRepository>();
        _service = new ProdutoService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_QuandoExiste_RetornaProjetoResponse()
    {
        var projeto = new Dominio.Entidades.Produto { Id = 1, IdEmpresa = 1, IdProduto = 1, ValorUltimaCompra = 100, LucroMinimo = 100, LucroMaximo = 100, PrecoVendaMinimo = 100, PrecoSugerido = 100, DataCadastro = DateTime.UtcNow };
        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(projeto);

           

    var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(1, result.IdEmpresa);
        Assert.Equal(1, result.IdProduto);
        Assert.Equal(100, result.ValorUltimaCompra);
        Assert.Equal(100, result.LucroMinimo);
        Assert.Equal(100, result.LucroMaximo);
        Assert.Equal(100, result.PrecoVendaMinimo);
        Assert.Equal(100, result.PrecoSugerido);
    }

    [Fact]
    public async Task GetByIdAsync_QuandoNaoExiste_RetornaNull()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((Dominio.Entidades.Produto?)null);

        var result = await _service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ChamaRepositoryAdd_RetornaProjetoResponse()
    {
        SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades.Produto? captured = null;
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Dominio.Entidades.Produto>(), It.IsAny<CancellationToken>()))
            .Callback<Dominio.Entidades.Produto, CancellationToken>((p, _) => captured = p)
            .ReturnsAsync((Dominio.Entidades.Produto p, CancellationToken _) => { p.Id = 1; return p; });

        var request = new ProdutoCreateRequest(1 , 1, 100, 100, 100, 100, 100);
        var result = await _service.CreateAsync(request);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(1, result.IdEmpresa);
        Assert.Equal(1, result.IdProduto);
        Assert.Equal(100, result.ValorUltimaCompra);
        Assert.NotNull(captured);
        Assert.Equal(100, captured.ValorUltimaCompra);


        Assert.Equal(1, result.Id);
        Assert.Equal(1, result.IdEmpresa);
        Assert.Equal(1, result.IdProduto);
        Assert.Equal(100, result.ValorUltimaCompra);
        Assert.Equal(100, result.LucroMinimo);
        Assert.Equal(100, result.LucroMaximo);
        Assert.Equal(100, result.PrecoVendaMinimo);
        Assert.Equal(100, result.PrecoSugerido);
    }

    [Fact]
    public async Task DeleteAsync_QuandoExiste_RetornaTrue()
    {
        var projeto = new Dominio.Entidades.Produto { Id = 1, IdEmpresa = 1, IdProduto = 1, DataCadastro = DateTime.UtcNow };
        _repositoryMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(projeto);
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Dominio.Entidades.Produto>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _service.DeleteAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_QuandoNaoExiste_RetornaFalse()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades.Produto?)null);

        var result = await _service.DeleteAsync(999);

        Assert.False(result);
    }
}
