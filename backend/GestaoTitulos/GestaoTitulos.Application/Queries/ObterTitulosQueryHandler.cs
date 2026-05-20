using GestaoTitulos.Application.DTOs;
using GestaoTitulos.Domain.Interfaces;
using MediatR;

namespace GestaoTitulos.Application.Queries
{
    public class ObterTitulosQueryHandler : IRequestHandler<ObterTitulosQuery, List<TituloResponse>>
    {
        private readonly ITituloRepository _tituloRepository;

        public ObterTitulosQueryHandler(ITituloRepository tituloRepository)
        {
            _tituloRepository = tituloRepository;
        }

        public async Task<List<TituloResponse>> Handle(ObterTitulosQuery request, CancellationToken cancellationToken)
        {
            var titulos = await _tituloRepository.ObterTodos(cancellationToken);

            var dataReferencia = DateOnly.FromDateTime(DateTime.UtcNow);

            return titulos
                .Select(titulo =>
                    TituloResponse.FromEntity(titulo, dataReferencia))
                .ToList();
        }
    }
}
