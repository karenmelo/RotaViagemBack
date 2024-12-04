using FluentValidation;
using RotaViagem.Domain.Entities;

namespace RotaViagem.Domain.Validations
{
    public class RotaValidation : AbstractValidator<Rota>
    {

        public RotaValidation()
        {
            RuleFor(r => r.Origem)
                .NotEmpty()
                .WithMessage("Por favor, certifique-se de ter inserido a Origem")
                .Length(3, 3)
                .WithMessage("A Origem deve conter 3 caracteres");

            RuleFor(r => r.Destino)
               .NotEmpty()
               .WithMessage("Por favor, certifique-se de ter inserido o Destino")
               .Length(3, 3)
               .WithMessage("O Destino deve conter 3 caracteres");


            RuleFor(r => r.Valor).NotEmpty()               
               .WithMessage("Por favor, certifique-se de ter inserido o Valor")
               .GreaterThan(1)
               .LessThanOrEqualTo(99999999999)
               .WithMessage("O valor deve estar entre 1 e 99999999999");
        }
    }
}
