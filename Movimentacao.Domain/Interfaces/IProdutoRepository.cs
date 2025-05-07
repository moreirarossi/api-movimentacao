using Movimentacao.Domain.Entities;

namespace Movimentacao.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> GetAllAsync();
        Task<Produto> GetByCodProduto(string codProduto);
    }
}