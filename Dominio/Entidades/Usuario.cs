using System.ComponentModel.DataAnnotations;

namespace SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public double TaxaPercentual { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    }
}
