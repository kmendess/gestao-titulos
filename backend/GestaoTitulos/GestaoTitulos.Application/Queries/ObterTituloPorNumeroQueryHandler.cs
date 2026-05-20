using GestaoTitulos.Application.DTOs;
using GestaoTitulos.Domain.Interfaces;
using MediatR;

namespace GestaoTitulos.Application.Queries
{
    public class ObterTituloPorNumeroQueryHandler : IRequestHandler<ObterTituloPorNumeroQuery, Result<TituloResponse?>>
    {
        private readonly ITituloRepository _tituloRepository;

        public ObterTituloPorNumeroQueryHandler(ITituloRepository tituloRepository)
        {
            _tituloRepository = tituloRepository;
        }

        public async Task<Result<TituloResponse?>> Handle(ObterTituloPorNumeroQuery request, CancellationToken cancellationToken)
        {
            var titulo = await _tituloRepository.ObterPorNumero(request.NumeroTitulo, cancellationToken);

            if (titulo == null)
                return Result<TituloResponse?>.Error("Título não encontrado.");

            var dataReferencia = DateOnly.FromDateTime(DateTime.UtcNow);

            var result = TituloResponse.FromEntity(titulo, dataReferencia);

            return Result<TituloResponse?>.Success(result);
        }
    }
}
