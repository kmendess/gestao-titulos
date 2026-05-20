using FluentValidation;

namespace GestaoTitulos.Application.Commands
{
    public class CriarTituloCommandValidator : AbstractValidator<CriarTituloCommand>
    {
        public CriarTituloCommandValidator()
        {
            RuleFor(x => x.NumeroTitulo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Número do título é obrigatório.")
                .MaximumLength(50).WithMessage("Número do título deve ser menor ou igual a 50 caracteres.")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Número do título inválido.");

            RuleFor(x => x.NomeDevedor)
                .NotEmpty().WithMessage("Nome do devedor é obrigatório.")
                .MaximumLength(150).WithMessage("Nome do devedor deve ser menor ou igual a 150 caracteres.");

            RuleFor(x => x.CpfDevedor)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("CPF do devedor é obrigatório.")
                .Length(11).WithMessage("CPF do devedor deve ter 11 caracteres.")
                .Matches(@"^\d+$").WithMessage("CPF do devedor inválido.");

            RuleFor(x => x.PercentualJuros)
                .GreaterThanOrEqualTo(0).WithMessage("Percentual de juros deve ser superior ou igual a '0'.");

            RuleFor(x => x.PercentualMulta)
                .GreaterThanOrEqualTo(0).WithMessage("Percentual de multa deve ser superior ou igual a '0'.");

            RuleFor(x => x.Parcelas)
                .NotEmpty().WithMessage("O título deve possuir ao menos uma parcela.");

            RuleForEach(x => x.Parcelas).ChildRules(item =>
            {
                item.RuleFor(x => x.NumeroParcela)
                    .GreaterThan(0).WithMessage("Número da parcela deve ser superior a '0'.");

                item.RuleFor(x => x.DataVencimento)
                    .NotEmpty().WithMessage("Data de vencimento da parcela é obrigatória.");

                item.RuleFor(x => x.Valor)
                    .GreaterThan(0).WithMessage("Valor da parcela deve ser superior a '0'.");
            });
        }
    }
}
