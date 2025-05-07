using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Model;

namespace Movimentacao.Domain.Interfaces
{
    public interface IMovimentoManualRepository
    {
        Task<IEnumerable<MovimentoManualSPModel>> GetAllBySPAsync();

        Task AddAsync(MovimentoManual movimentoManual);
    }
}