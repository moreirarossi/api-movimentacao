using MediatR;

namespace Movimentacao.Application.Model
{
    public class PostMovimentoRequest : IRequest<Unit>
    {
        public decimal DatMes { get; set; }
        public decimal DatAno { get; set; }
        public string CodProduto { get; set; }
        public string CodCosif { get; set; }
        public decimal ValValor { get; set; }
        public string DesDescricao { get; set; }
    }
}
