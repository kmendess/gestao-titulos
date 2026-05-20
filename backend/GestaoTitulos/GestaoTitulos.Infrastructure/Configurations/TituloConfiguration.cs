using GestaoTitulos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoTitulos.Infrastructure.Configurations
{
    public class TituloConfiguration : IEntityTypeConfiguration<Titulo>
    {
        public void Configure(EntityTypeBuilder<Titulo> builder)
        {
            builder.ToTable("titulo", "gestao_titulos");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.NumeroTitulo)
                .HasColumnName("numero_titulo")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.NomeDevedor)
                .HasColumnName("nome_devedor")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(t => t.CpfDevedor)
                .HasColumnName("cpf_devedor")
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(t => t.PercentualJuros)
                .HasColumnName("percentual_juros")
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(t => t.PercentualMulta)
                .HasColumnName("percentual_multa")
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(t => t.CriadoEm)
                .HasColumnName("criado_em")
                .HasColumnType("timestamptz")
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasMany(t => t.Parcelas)
                .WithOne(t => t.Titulo)
                .HasForeignKey(t => t.TituloId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.NumeroTitulo)
                .IsUnique();

            builder.HasIndex(t => t.CpfDevedor);
        }
    }
}
