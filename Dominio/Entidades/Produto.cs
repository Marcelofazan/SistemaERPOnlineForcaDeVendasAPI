using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; } 
        public int IdProduto { get; set; }
        public double ValorUltimaCompra { get; set; }
        public double LucroMinimo { get; set; }
        public double LucroMaximo { get; set; } 
        public double PrecoVendaMinimo { get; set; }
        public double PrecoSugerido { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
