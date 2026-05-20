using FluentAssertions;
using GestaoTitulos.Domain.Entities;

namespace GestaoTitulos.Tests
{
    public class ParcelaTests
    {
        [Fact]
        public void Deve_Calcular_Dias_Em_Atraso()
        {
            // Arrange
            var parcela = new Parcela(1, new DateOnly(2026, 05, 10), 100);

            var dataReferencia = new DateOnly(2026, 05, 19);

            // Act
            var dias = parcela.ObterDiasEmAtraso(dataReferencia);

            // Assert
            dias.Should().Be(9);
        }

        [Fact]
        public void Deve_Calcular_Juros()
        {
            // Arrange
            var parcela = new Parcela(1, new DateOnly(2026, 05, 10), 100);

            var dataReferencia = new DateOnly(2026, 05, 19);

            // Act
            var juros = parcela.CalcularJuros(1, dataReferencia);

            // Assert
            juros.Should().Be(0.30m);
        }

        [Fact]
        public void Nao_Deve_Calcular_Juros_Quando_Nao_Ha_Atraso()
        {
            // Arrange
            var parcela = new Parcela(1, new DateOnly(2026, 06, 10), 100);

            var dataReferencia = new DateOnly(2026, 05, 19);

            // Act
            var juros = parcela.CalcularJuros(1, dataReferencia);

            // Assert
            juros.Should().Be(0);
        }

        [Fact]
        public void Deve_Calcular_Valor_Atualizado()
        {
            // Arrange
            var titulo = new Titulo("123", "João da Silva", "12345678901", 1, 2);

            titulo.AdicionarParcela(new Parcela(1, new DateOnly(2026, 04, 19), 100));

            var dataReferencia = new DateOnly(2026, 05, 19);

            // Act
            var valorAtualizado = titulo.CalcularValorAtualizado(dataReferencia);

            // Assert
            valorAtualizado.Should().Be(103);
        }
    }
}
