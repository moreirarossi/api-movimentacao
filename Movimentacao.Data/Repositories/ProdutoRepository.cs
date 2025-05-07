using Microsoft.EntityFrameworkCore;
using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Enum;
using Movimentacao.Domain.Interfaces;
using Movimentacao.Infrastructure.Data;

namespace Movimentacao.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            return await _context.Produto.Where(_ => _.StaStatus == (char)Status.Ativo).ToListAsync();
        }

        public async Task<Produto> GetByCodProduto(string codProduto)
        {
            return await _context.Produto.FirstOrDefaultAsync(_ => _.CodProduto == codProduto);
        }
    }
}
