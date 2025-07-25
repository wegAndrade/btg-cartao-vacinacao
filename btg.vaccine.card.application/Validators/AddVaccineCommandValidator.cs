using btg.cartao.vacina.domain.Command;
using FluentValidation;

namespace btg.vaccine.card.application.Validators
{
    public class AddVaccineCommandValidator: AbstractValidator<AddVaccineCommand>
    {
        public AddVaccineCommandValidator() {

            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Para registrar a vacina informe um nome");
                
           
        }

    }
}
