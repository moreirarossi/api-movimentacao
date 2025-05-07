using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movimentacao.Domain.Entities;

public class MovimentoManualConfiguration : IEntityTypeConfiguration<MovimentoManual>
{
    public void Configure(EntityTypeBuilder<MovimentoManual> entity)
    {
        entity.ToTable("MOVIMENTO_MANUAL");

        entity.HasKey(e => new { e.DatAno, e.DatMes, e.NumLancamento });

        entity.Property(e => e.DatMes)
            .IsRequired()
            .HasColumnName("DAT_MES");

        entity.Property(e => e.DatAno)
            .IsRequired()
            .HasColumnName("DAT_ANO");

        entity.Property(e => e.NumLancamento)
            .IsRequired()
            .HasColumnName("NUM_LANCAMENTO");

        entity.Property(e => e.CodProduto)
            .IsRequired()
            .HasColumnName("COD_PRODUTO")
            .HasMaxLength(4);

        entity.Property(e => e.CodCosif)
            .IsRequired()
            .HasColumnName("COD_COSIF")
            .HasMaxLength(11);

        entity.Property(e => e.DesDescricao)
            .IsRequired()
            .HasColumnName("DES_DESCRICAO")
            .HasMaxLength(100);

        entity.Property(e => e.DatMovimento)
            .HasColumnName("DAT_MOVIMENTO");

        entity.Property(e => e.CodUsuario)
            .HasColumnName("COD_USUARIO")
            .HasMaxLength(10);

        entity.Property(e => e.ValValor)
            .IsRequired()
            .HasColumnName("VAL_VALOR");
    }
}
