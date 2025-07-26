using btg.cartao.vacina.domain.Command;
using btg.cartao.vacina.domain.Entities;
using btg.vaccine.card.application.Adapters;

namespace btg.vaccine.card.unitTests.Application.Adapters
{
    public class VaccineAdapterTests
    {
        [Fact]
        public void ToModel_ShouldCreateVaccine_WhenCommandIsValid()
        {
            // Arrange
            var command = new AddVaccineCommand("COVID-19");

            // Act
            var result = command.ToModel();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Vaccine>(result);
            Assert.Equal("COVID-19", result.Name);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public void ToModel_ShouldReturnNull_WhenCommandIsNull()
        {
            // Arrange
            AddVaccineCommand command = null;

            // Act
            var result = command.ToModel();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToModel_ShouldSetCorrectProperties()
        {
            // Arrange
            var vaccineName = "Influenza";
            var command = new AddVaccineCommand(vaccineName);

            // Act
            var result = command.ToModel();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(vaccineName, result.Name);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public void ToModel_ShouldGenerateNewGuid()
        {
            // Arrange
            var command = new AddVaccineCommand("Hepatite B");

            // Act
            var result1 = command.ToModel();
            var result2 = command.ToModel();

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotEqual(result1.Id, result2.Id);
        }


       

     
    }
}
