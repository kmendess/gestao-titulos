using GestaoTitulos.Application.DTOs;
using GestaoTitulos.Domain.Interfaces;
using MediatR;

namespace GestaoTitulos.Application.Commands
{
    public class ExcluirTituloCommandHandler : IRequestHandler<ExcluirTituloCommand, Result>
    {
        private readonly ITituloRepository _tituloRepository;

        public ExcluirTituloCommandHandler(ITituloRepository tituloRepository)
        {
            _tituloRepository = tituloRepository;
        }

        public async Task<Result> Handle(ExcluirTituloCommand request, CancellationToken cancellationToken)
        {
            var titulo = await _tituloRepository.ObterPorNumero(request.NumeroTitulo, cancellationToken);

            if (titulo == null)
                return Result.Error("Título não encontrado.");

            await _tituloRepository.Remover(titulo, cancellationToken);

            return Result.Success();
        }
    }
}
