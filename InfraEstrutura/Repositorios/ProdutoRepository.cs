using Microsoft.EntityFrameworkCore;
using SistemaERPOnlineForcaDeVendasAPI.Aplicacao.Produto;
using SistemaERPOnlineForcaDeVendasAPI.Dominio.Entidades;
using SistemaERPOnlineForcaDeVendasAPI.InfraEstrutura.Data;

namespace SistemaERPOnlineForcaDeVendasAPI.InfraEstrutura.Repositorios
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dominio.Entidades.Produto?> GetByIdAsync(int IdProduto, CancellationToken cancellationToken = default) =>
            await _context.Produtos.FindAsync([IdProduto], cancellationToken);

        public async Task<IReadOnlyList<Dominio.Entidades.Produto>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _context.Produtos.OrderBy(p => p.IdProduto).ToListAsync(cancellationToken);

        public async Task<Dominio.Entidades.Produto> AddAsync(Dominio.Entidades.Produto produto, CancellationToken cancellationToken = default)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync(cancellationToken);
            return produto;
        }

        public async Task UpdateAsync(Dominio.Entidades.Produto produto, CancellationToken cancellationToken = default)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Dominio.Entidades.Produto produto, CancellationToken cancellationToken = default)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
