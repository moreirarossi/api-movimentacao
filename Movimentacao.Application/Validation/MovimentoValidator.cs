using FluentValidation;
using Movimentacao.Application.Model;

namespace Movimentacao.Application.Validations
{
    public class MovimentoValidator : AbstractValidator<PostMovimentoRequest>
    {
        private int _ano = DateTime.Now.Year;
        private int _mes = DateTime.Now.Month;

        public MovimentoValidator()
        {
            RuleFor(x => new { x.DatMes, x.DatAno })
                .Must(data => data.DatMes > 0).WithMessage("Mês é obrigatório.")
                .Must(data => data.DatMes >= 1 && data.DatMes <= 12).WithMessage("Mês deve ser um número entre 1 e 12.")
                .Must(data => data.DatAno > 0).WithMessage("Ano é obrigatório.")
                .Must(data => data.DatAno >= DateTime.MinValue.Year).WithMessage($"Ano deve ser maior ou igual a {DateTime.MinValue.Year}.");

            RuleFor(x => x.CodProduto)
                .Must(value => !String.IsNullOrEmpty(value)).WithMessage("Produto é obrigatório.");

            RuleFor(x => x.CodCosif)
                .Must(value => !String.IsNullOrEmpty(value)).WithMessage("Cosif é obrigatório.");

            RuleFor(x => x.ValValor)
                .NotNull().WithMessage("Valor é obrigatório.")
                .GreaterThan(0)
                .WithMessage($"Valor deve ser maior que zero.");

            RuleFor(x => x.DesDescricao)
                .Must(value => !string.IsNullOrWhiteSpace(value))
                .WithMessage("A descrição é obrigatória e não pode conter apenas espaços.");
        }
    }
}