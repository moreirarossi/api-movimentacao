using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movimentacao.Domain.Entities;

namespace Movimentacao.Infrastructure.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> entity)
        {
            entity.ToTable("PRODUTO");

            entity.HasKey(e => e.CodProduto);

            entity.Property(e => e.CodProduto)
                .HasColumnName("COD_PRODUTO")
                .HasMaxLength(4);

            entity.Property(e => e.DesProduto)
                .HasColumnName("DES_PRODUTO")
                .HasMaxLength(30);

            entity.Property(e => e.StaStatus)
                .HasColumnName("STA_STATUS")
                .HasMaxLength(1);
        }
    }
}
