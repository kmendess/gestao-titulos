using GestaoTitulos.Domain.Entities;

namespace GestaoTitulos.Application.DTOs
{
    public class ParcelaResponse
    {
        public int NumeroParcela { get; set; }
        public DateOnly DataVencimento { get; set; }
        public int DiasEmAtraso { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorJuros { get; set; }

        public static ParcelaResponse FromEntity(Parcela parcela, decimal percentualJuros, DateOnly dataReferencia)
        {
            return new ParcelaResponse
            {
                NumeroParcela = parcela.NumeroParcela,
                DataVencimento = parcela.DataVencimento,
                DiasEmAtraso = parcela.ObterDiasEmAtraso(dataReferencia),
                ValorParcela = parcela.Valor,
                ValorJuros = parcela.CalcularJuros(percentualJuros, dataReferencia)
            };
        }
    }
}
