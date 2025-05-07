using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movimentacao.Domain.Entities;

public class ProdutoCosifConfiguration : IEntityTypeConfiguration<ProdutoCosif>
{
    public void Configure(EntityTypeBuilder<ProdutoCosif> entity)
    {
        entity.ToTable("PRODUTO_COSIF");

        entity.HasKey(e => new { e.CodProduto, e.CodCosif });

        entity.Property(e => e.CodProduto)
            .HasColumnName("COD_PRODUTO")
            .HasMaxLength(4);

        entity.Property(e => e.CodCosif)
            .HasColumnName("COD_COSIF")
            .HasMaxLength(11);

        entity.Property(e => e.CodClassificacao)
            .HasColumnName("COD_CLASSIFICACAO")
            .HasMaxLength(6);

        entity.Property(e => e.StaStatus)
            .HasColumnName("STA_STATUS")
            .HasMaxLength(1);
    }
}
