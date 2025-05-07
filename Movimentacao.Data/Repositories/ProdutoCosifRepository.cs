using Microsoft.EntityFrameworkCore;
using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Enum;
using Movimentacao.Domain.Interfaces;
using Movimentacao.Infrastructure.Data;

namespace Movimentacao.Data.Repositories
{
    public class ProdutoCosifRepository : IProdutoCosifRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoCosifRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoCosif>> GetByProdutoAsync(string codProduto)
        {
            return await _context.ProdutoCosif.Where(_ => _.StaStatus == (char)Status.Ativo && _.CodProduto == codProduto).ToListAsync();
        }


        public async Task<ProdutoCosif> GetByProdutoCosif(string codProduto, string codCosif)
        {
            return await _context.ProdutoCosif
                .FirstOrDefaultAsync(_ => _.StaStatus == (char)Status.Ativo
                && _.CodProduto == codProduto
                && _.CodCosif == codCosif);
        }
    }
}
