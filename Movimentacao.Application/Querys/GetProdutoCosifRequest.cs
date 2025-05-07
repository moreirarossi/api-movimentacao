using MediatR;

namespace Movimentacao.Application.Querys
{
    public class GetProdutoCosifRequest : IRequest<IEnumerable<GetProdutoCosifResponse>>
    {
        public string CodProduto { get; set; }
    }
}
