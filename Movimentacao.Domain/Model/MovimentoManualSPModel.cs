using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movimentacao.Domain.Model
{
    [Keyless]
    public class MovimentoManualSPModel
    {
        [Column("DAT_MES")]
        public decimal DatMes { get; set; }

        [Column("DAT_ANO")]
        public decimal DatAno { get; set; }

        [Column("COD_PRODUTO")]
        public string CodProduto { get; set; }

        [Column("DES_PRODUTO")]
        public string DesProduto { get; set; }

        [Column("NUM_LANCAMENTO")]
        public decimal NumLancamento { get; set; }

        [Column("DES_DESCRICAO")]
        public string DesDescricao { get; set; }

        [Column("VAL_VALOR")]
        public decimal ValValor { get; set; }
    }
}
