using GestaoTitulos.Application.DTOs;
using MediatR;

namespace GestaoTitulos.Application.Queries
{
    public class ObterTitulosQuery : IRequest<List<TituloResponse>>
    {
    }
}
