using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaERPOnlineForcaDeVendasAPI.Aplicacao.Produto;

public record ProdutoCreateRequest(
    [Required] int IdEmpresa,
    [Required] int IdProduto,
    [Required][Range(0.0001, 9999999999.9999, ErrorMessage = "Valor Valor UltimaCompra Inválido")] double ValorUltimaCompra,
    [Required][Range(0.0001, 9999999999.9999, ErrorMessage = "Valor Lucro Minimo Inválido")] double LucroMinimo,
    [Required][Range(0.0001, 9999999999.9999, ErrorMessage = "Valor Lucro Maximo Inválido")] double LucroMaximo,
    [Required][Range(0.0001, 9999999999.9999, ErrorMessage = "Valor Preco Venda Minimo Inválido")] double PrecoVendaMinimo,
    [Required][Range(0.0001, 9999999999.9999, ErrorMessage = "Valor Preco Sugerido Inválido")] double PrecoSugerido
);

public record ProdutoUpdateRequest(
    [Required] int IdEmpresa,
    [Required] int IdProduto,
    [Required][Range(0.0001, 9999999999999.9999, ErrorMessage = "Valor Valor UltimaCompra Inválido")] double ValorUltimaCompra,
    [Required][Range(0.0001, 9999999999999.9999, ErrorMessage = "Valor Lucro Minimo Inválido")] double LucroMinimo,
    [Required][Range(0.0001, 9999999999999.9999, ErrorMessage = "Valor Lucro Maximo Inválido")] double LucroMaximo,
    [Required][Range(0.0001, 9999999999999.9999, ErrorMessage = "Valor Preco Venda Minimo Inválido")] double PrecoVendaMinimo,
    [Required][Range(0.0001, 9999999999999.9999, ErrorMessage = "Valor Preco Sugerido Inválido")] double PrecoSugerido
);

public record ProdutoResponse(int Id, int IdEmpresa, int IdProduto, double ValorUltimaCompra, double LucroMinimo, double LucroMaximo, double PrecoVendaMinimo, double PrecoSugerido);