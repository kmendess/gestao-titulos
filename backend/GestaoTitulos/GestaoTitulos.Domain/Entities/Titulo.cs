namespace GestaoTitulos.Domain.Entities
{
    public class Titulo
    {
        public Guid Id { get; private set; }
        public string NumeroTitulo { get; private set; } = string.Empty;
        public string NomeDevedor { get; private set; } = string.Empty;
        public string CpfDevedor { get; private set; } = string.Empty;
        public decimal PercentualJuros { get; private set; }
        public decimal PercentualMulta { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public List<Parcela> Parcelas { get; private set; } = [];

        public Titulo(
            string numeroTitulo,
            string nomeDevedor,
            string cpfDevedor,
            decimal percentualJuros,
            decimal percentualMulta)
        {
            Id = Guid.NewGuid();
            NumeroTitulo = numeroTitulo;
            NomeDevedor = nomeDevedor;
            CpfDevedor = cpfDevedor;
            PercentualJuros = percentualJuros;
            PercentualMulta = percentualMulta;
            CriadoEm = DateTime.UtcNow;
        }

        public void AdicionarParcela(Parcela parcela)
        {
            Parcelas.Add(parcela);
        }

        public int ObterQuantidadeParcelas()
        {
            return Parcelas.Count;
        }

        public decimal ObterValorOriginal()
        {
            return Parcelas.Sum(p => p.Valor);
        }

        public int ObterMaiorDiasEmAtraso(DateOnly dataReferencia)
        {
            return Parcelas.Max(p => p.ObterDiasEmAtraso(dataReferencia));
        }

        public decimal CalcularValorAtualizado(DateOnly dataReferencia)
        {
            var valorAtualizado = ObterValorOriginal() + CalcularMulta(dataReferencia) + CalcularTotalJuros(dataReferencia);

            return Math.Round(valorAtualizado, 2, MidpointRounding.AwayFromZero);
        }

        public decimal CalcularMulta(DateOnly dataReferencia)
        {
            var possuiParcelasEmAtraso = Parcelas.Any(p => p.ObterDiasEmAtraso(dataReferencia) > 0);

            if (!possuiParcelasEmAtraso)
                return 0;

            var multa = ObterValorOriginal() * (PercentualMulta / 100);

            return Math.Round(multa, 2, MidpointRounding.AwayFromZero);
        }

        public decimal CalcularTotalJuros(DateOnly dataReferencia)
        {
            var juros = Parcelas.Sum(p => p.CalcularJuros(PercentualJuros, dataReferencia));

            return Math.Round(juros, 2, MidpointRounding.AwayFromZero);
        }
    }
}
