using GestaoTitulos.Application.DTOs;
using GestaoTitulos.Domain.Entities;
using GestaoTitulos.Domain.Interfaces;
using MediatR;

namespace GestaoTitulos.Application.Commands
{
    public class CriarTituloCommandHandler : IRequestHandler<CriarTituloCommand, Result<string>>
    {
        private readonly ITituloRepository _tituloRepository;

        public CriarTituloCommandHandler(ITituloRepository tituloRepository)
        {
            _tituloRepository = tituloRepository;
        }

        public async Task<Result<string>> Handle(CriarTituloCommand request, CancellationToken cancellationToken)
        {
            var tituloExistente = await _tituloRepository.ObterPorNumero(request.NumeroTitulo, cancellationToken);

            if (tituloExistente != null)
                return Result<string>.Error("Número do título já cadastrado.");

            var titulo = new Titulo(
                request.NumeroTitulo,
                request.NomeDevedor,
                request.CpfDevedor,
                request.PercentualJuros,
                request.PercentualMulta);

            foreach (var parcela in request.Parcelas)
            {
                titulo.AdicionarParcela(
                    new Parcela(
                        parcela.NumeroParcela,
                        parcela.DataVencimento,
                        parcela.Valor));
            }

            await _tituloRepository.Adicionar(titulo, cancellationToken);

            return Result<string>.Success(titulo.NumeroTitulo);
        }
    }
}
