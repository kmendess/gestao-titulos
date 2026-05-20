using GestaoTitulos.Domain.Entities;

namespace GestaoTitulos.Application.DTOs
{
    public class TituloResponse
    {
        public string NumeroTitulo { get; set; } = string.Empty;
        public string NomeDevedor { get; set; } = string.Empty;
        public string CpfDevedor { get; private set; } = string.Empty;
        public decimal PercentualJuros { get; private set; }
        public decimal PercentualMulta { get; private set; }
        public int QuantidadeParcelas { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorMulta { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorAtualizado { get; set; }
        public int DiasEmAtraso { get; set; }
        public List<ParcelaResponse> Parcelas { get; set; } = [];

        public static TituloResponse FromEntity(Titulo titulo, DateOnly dataReferencia)
        {
            return new TituloResponse
            {
                NumeroTitulo = titulo.NumeroTitulo,
                NomeDevedor = titulo.NomeDevedor,
                CpfDevedor = titulo.CpfDevedor,
                PercentualJuros = titulo.PercentualJuros,
                PercentualMulta = titulo.PercentualMulta,
                QuantidadeParcelas = titulo.ObterQuantidadeParcelas(),
                ValorOriginal = titulo.ObterValorOriginal(),
                ValorMulta = titulo.CalcularMulta(dataReferencia),
                ValorJuros = titulo.CalcularTotalJuros(dataReferencia),
                ValorAtualizado = titulo.CalcularValorAtualizado(dataReferencia),
                DiasEmAtraso = titulo.ObterMaiorDiasEmAtraso(dataReferencia),

                Parcelas = titulo.Parcelas
                    .Select(parcela => ParcelaResponse.FromEntity(
                        parcela,
                        titulo.PercentualJuros,
                        dataReferencia))
                    .ToList()
            };
        }
    }
}
