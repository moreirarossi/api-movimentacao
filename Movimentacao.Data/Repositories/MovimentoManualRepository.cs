using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Movimentacao.Domain.Configs;
using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Interfaces;
using Movimentacao.Domain.Model;
using Movimentacao.Infrastructure.Data;

namespace Movimentacao.Data.Repositories
{
    public class MovimentoManualRepository : IMovimentoManualRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly Configs _configs;

        public MovimentoManualRepository(ApplicationDbContext context, IOptions<Configs> configs)
        {
            _context = context;
            _configs = configs.Value;
        }

        public async Task<IEnumerable<MovimentoManualSPModel>> GetAllBySPAsync()
        {
            return await _context.Set<MovimentoManualSPModel>() // Aqui você especifica a model
                                 .FromSqlRaw($"EXEC {_configs.SPMovimentosManuais}")
                                 .ToListAsync();
        }

        public async Task AddAsync(MovimentoManual movimentoManual)
        {
            IDbContextTransaction transaction = null;
            try
            {
                transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

                movimentoManual.NumLancamento = (await _context.MovimentoManual
                    .Where(m => m.DatMes == movimentoManual.DatMes && m.DatAno == movimentoManual.DatAno)
                    .MaxAsync(m => (decimal?)m.NumLancamento) ?? 0) + 1;

                _context.MovimentoManual.Add(movimentoManual);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                throw;
            }
            finally
            {
                if (transaction != null)
                    await transaction.DisposeAsync();
            }
        }
    }
}
