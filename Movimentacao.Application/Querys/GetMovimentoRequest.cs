using MediatR;

namespace Movimentacao.Application.Querys
{
    public class GetMovimentoRequest : IRequest<IEnumerable<GetMovimentoResponse>>
    {

    }
}
