using GestaoTitulos.Application.DTOs;
using MediatR;

namespace GestaoTitulos.Application.Commands
{
    public class CriarTituloCommand : IRequest<Result<string>>
    {
        public string NumeroTitulo { get; set; } = string.Empty;
        public string NomeDevedor { get; set; } = string.Empty;
        public string CpfDevedor { get; set; } = string.Empty;
        public decimal PercentualJuros { get; set; }
        public decimal PercentualMulta { get; set; }
        public List<ParcelaRequest> Parcelas { get; set; } = [];
    }
}
