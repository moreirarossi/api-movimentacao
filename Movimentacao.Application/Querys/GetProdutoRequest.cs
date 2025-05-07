using MediatR;

namespace Movimentacao.Application.Querys
{
    public class GetProdutoRequest : IRequest<IEnumerable<GetProdutoResponse>>
    {

    }
}
