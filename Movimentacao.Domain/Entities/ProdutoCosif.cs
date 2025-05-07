namespace Movimentacao.Domain.Entities
{
    public class ProdutoCosif
    {
        public string CodProduto { get; set; }
        public string CodCosif { get; set; }
        public string? CodClassificacao { get; set; }
        public char? StaStatus { get; set; }
    }
}
