using GestaoTitulos.Application.DTOs;
using MediatR;

namespace GestaoTitulos.Application.Queries
{
    public class ObterTituloPorNumeroQuery : IRequest<Result<TituloResponse?>>
    {
        public string NumeroTitulo { get; set; } = string.Empty;

        public ObterTituloPorNumeroQuery(string numeroTitulo)
        {
            NumeroTitulo = numeroTitulo;
        }
    }
}
