using GestaoTitulos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoTitulos.Infrastructure.Configurations
{
    public class ParcelaConfiguration : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.ToTable("parcela", "gestao_titulos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.TituloId)
                .HasColumnName("titulo_id")
                .IsRequired();

            builder.Property(p => p.NumeroParcela)
                .HasColumnName("numero_parcela")
                .IsRequired();

            builder.Property(p => p.DataVencimento)
                .HasColumnName("data_vencimento")
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasColumnName("valor")
                .HasPrecision(15, 2)
                .IsRequired();

            builder.HasIndex(p => p.TituloId);
        }
    }
}
