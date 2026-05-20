namespace GestaoTitulos.Domain.Entities
{
    public class Parcela
    {
        public Guid Id { get; private set; }
        public Titulo Titulo { get; set; } = null!;
        public Guid TituloId { get; private set; }
        public int NumeroParcela { get; private set; }
        public DateOnly DataVencimento { get; private set; }
        public decimal Valor { get; private set; }

        public Parcela(int numeroParcela, DateOnly dataVencimento, decimal valor)
        {
            NumeroParcela = numeroParcela;
            DataVencimento = dataVencimento;
            Valor = valor;
        }

        public int ObterDiasEmAtraso(DateOnly dataReferencia)
        {
            if (DataVencimento >= dataReferencia)
                return 0;

            return dataReferencia.DayNumber - DataVencimento.DayNumber;
        }

        public decimal CalcularJuros(decimal percentualJuros, DateOnly dataReferencia)
        {
            var diasEmAtraso = ObterDiasEmAtraso(dataReferencia);

            if (diasEmAtraso <= 0)
                return 0;

            var juros = Valor * ((percentualJuros / 100) / 30) * diasEmAtraso;

            return Math.Round(juros, 2, MidpointRounding.AwayFromZero);
        }
    }
}
