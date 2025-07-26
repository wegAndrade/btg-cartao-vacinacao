using btg.cartao.vacina.domain.Command;
using btg.vaccine.card.application.Validators;

namespace btg.vaccine.card.unitTests.Application.Validators
{
    public class AddVaccineCommandValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
         public void Validate_ShouldFail_WhenNameIsEmptyOrNull(string name)
        {
            var addVaccineCommand = new AddVaccineCommand(name);

            var validator = new AddVaccineCommandValidator();

            var validationResult = validator.Validate(addVaccineCommand);

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
            Assert.Equal(ValidatorMessages.NameIsNullOrEmptyMessage, validationResult.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("COVID-19")]
        [InlineData("Influenza")]
        [InlineData("Hepatite B")]
        public void Validate_ShouldPass_WhenNameIsValid(string name)
        {
            
            var addVaccineCommand = new AddVaccineCommand(name);

            var validator = new AddVaccineCommandValidator();

            var validationResult = validator.Validate(addVaccineCommand);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Theory]
        [InlineData("Vacina com nome muito longo que excede o limite máximo permitido pelo sistema")]

        public void Validate_ShouldFail_WhenNameIsTooLong(string name)
        {

            var addVaccineCommand = new AddVaccineCommand(name);

            var validator = new AddVaccineCommandValidator();

            var validationResult = validator.Validate(addVaccineCommand);

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
            Assert.Equal(ValidatorMessages.NameIsTooLong, validationResult.Errors[0].ErrorMessage);
        }



    }
}

