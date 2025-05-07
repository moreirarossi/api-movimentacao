using Movimentacao.Domain.Entities;

namespace Movimentacao.Domain.Interfaces
{
    public interface IProdutoCosifRepository
    {
        Task<List<ProdutoCosif>> GetByProdutoAsync(string codProduto);
        Task<ProdutoCosif> GetByProdutoCosif(string codProduto, string codCosif);
    }
}