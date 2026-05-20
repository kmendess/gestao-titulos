namespace GestaoTitulos.Application.DTOs
{
    public class ParcelaRequest
    {
        public int NumeroParcela { get; set; }
        public DateOnly DataVencimento { get; set; }
        public decimal Valor { get; set; }
    }
}
