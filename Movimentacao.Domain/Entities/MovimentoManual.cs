namespace Movimentacao.Domain.Entities
{
    public class MovimentoManual
    {
        public int DatMes { get; set; }
        public int DatAno { get; set; }
        public decimal NumLancamento { get; set; }
        public string CodProduto { get; set; }
        public string CodCosif { get; set; }
        public string DesDescricao { get; set; }
        public DateTime DatMovimento { get; set; } = DateTime.Now;
        public string CodUsuario { get; set; }
        public decimal ValValor { get; set; }
    }
}
