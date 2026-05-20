using GestaoTitulos.Application.DTOs;
using MediatR;

namespace GestaoTitulos.Application.Commands
{
    public class ExcluirTituloCommand : IRequest<Result>
    {
        public string NumeroTitulo { get; set; } = string.Empty;

        public ExcluirTituloCommand(string numeroTitulo)
        {
            NumeroTitulo = numeroTitulo;
        }
    }
}
