using FluentValidation;

namespace GestaoTitulos.Application.Commands
{
    public class ExcluirTituloCommandValidator : AbstractValidator<ExcluirTituloCommand>
    {
        public ExcluirTituloCommandValidator()
        {
            RuleFor(x => x.NumeroTitulo)
                .NotEmpty().WithMessage("Número do título é obrigatório.");
        }
    }
}
