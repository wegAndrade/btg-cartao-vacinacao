﻿using btg.cartao.vacina.domain.Command;
using FluentValidation;

namespace btg.vaccine.card.application.Validators
{
    public class AddVaccineCommandValidator: AbstractValidator<AddVaccineCommand>
    {
        public AddVaccineCommandValidator() {

            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage(ValidatorMessages.NameIsNullOrEmptyMessage)
                .MaximumLength(50)
                .WithMessage(ValidatorMessages.NameIsTooLong);
                
           
        }

    }
}
