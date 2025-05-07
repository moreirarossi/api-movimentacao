namespace Movimentacao.Application.Querys
{
    public class GetMovimentoResponse
    {
        public decimal DatMes { get; set; }
        public decimal DatAno { get; set; }
        public string CodProduto { get; set; }
        public string DesProduto { get; set; }
        public decimal NumLancamento { get; set; }
        public string DesDescricao { get; set; }
        public decimal ValValor { get; set; }
    }
}
