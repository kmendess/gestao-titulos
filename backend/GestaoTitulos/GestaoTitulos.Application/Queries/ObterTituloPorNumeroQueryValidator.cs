using FluentValidation;

namespace GestaoTitulos.Application.Queries
{
    public class ObterTituloPorNumeroQueryValidator : AbstractValidator<ObterTituloPorNumeroQuery>
    {
        public ObterTituloPorNumeroQueryValidator()
        {
            RuleFor(x => x.NumeroTitulo)
                .NotEmpty().WithMessage("Número do título é obrigatório.");
        }
    }
}
