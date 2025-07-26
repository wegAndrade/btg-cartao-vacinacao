using btg.cartao.vacina.domain.Command;
using btg.cartao.vacina.domain.Entities;
using btg.cartao.vacina.domain.Handlers;
using btg.cartao.vacina.infra.Context;
using btg.vaccine.card.domain.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace btg.vaccine.card.unitTests.Application.Handlers
{
    public class AddVaccineCommandHandlerTests : IDisposable
    {
        private readonly AppDbContext _appContext;
        private readonly NotificationContext _notificationContext;
        private readonly AddVacinneCommandHandler _handler;

        public AddVaccineCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _appContext = new AppDbContext(options);
            _notificationContext = new NotificationContext();
            _handler = new AddVacinneCommandHandler(_appContext, _notificationContext);
        }

        public void Dispose()
        {
            _appContext.Database.EnsureDeleted();
            _appContext.Dispose();
        }

        [Fact]
        public async Task Handle_ShouldAddVaccine_WhenCommandIsValid()
        {
            // Arrange
            var command = new AddVaccineCommand("COVID-19");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            var savedVaccine = await _appContext.Vaccines.FirstOrDefaultAsync(v => v.Name == "COVID-19");
            Assert.NotNull(savedVaccine);
            Assert.Equal("COVID-19", savedVaccine.Name);
        }

        [Fact]
        public async Task Handle_ShouldNotAddVaccine_WhenValidationFails()
        {
            // Arrange
            var command = new AddVaccineCommand("");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            var savedVaccine = await _appContext.Vaccines.FirstOrDefaultAsync(v => v.Name == "");
            Assert.Null(savedVaccine);
        }

        [Fact]
        public async Task Handle_ShouldAddNotification_WhenValidationFails()
        {
            // Arrange
            var command = new AddVaccineCommand("");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            Assert.True(_notificationContext.HasNotifications);
            Assert.Contains(_notificationContext.Notifications, n => n.Message.Contains("Para registrar a vacina informe um nome"));
        }

        [Fact]
        public async Task Handle_ShouldNotAddVaccine_WhenVaccineAlreadyExists()
        {
            // Arrange
            var existingVaccine = new Vaccine { Id = Guid.NewGuid(), Name = "COVID-19" };
            await _appContext.Vaccines.AddAsync(existingVaccine);
            await _appContext.SaveChangesAsync();

            var command = new AddVaccineCommand("COVID-19");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            var vaccineCount = await _appContext.Vaccines.CountAsync(v => v.Name == "COVID-19");
            Assert.Equal(1, vaccineCount); // Apenas a vacina original deve existir
        }

        [Fact]
        public async Task Handle_ShouldAddNotification_WhenVaccineAlreadyExists()
        {
            // Arrange
            var existingVaccine = new Vaccine { Id = Guid.NewGuid(), Name = "COVID-19" };
            await _appContext.Vaccines.AddAsync(existingVaccine);
            await _appContext.SaveChangesAsync();

            var command = new AddVaccineCommand("COVID-19");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            Assert.True(_notificationContext.HasNotifications);
            Assert.Contains(_notificationContext.Notifications, n => 
                n.Key == "VaccineAlreadyExist" && 
                n.Message.Contains("COVID-19"));
        }

        [Fact]
        public async Task Handle_ShouldAddVaccineWithCorrectProperties_WhenValid()
        {
            // Arrange
            var command = new AddVaccineCommand("Sarampo");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            var savedVaccine = await _appContext.Vaccines.FirstOrDefaultAsync(v => v.Name == "Sarampo");
            Assert.NotNull(savedVaccine);
            Assert.Equal("Sarampo", savedVaccine.Name);
            Assert.NotEqual(Guid.Empty, savedVaccine.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task Handle_ShouldNotAddVaccine_WhenNameIsInvalid(string invalidName)
        {
            // Arrange
            var command = new AddVaccineCommand(invalidName);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            var savedVaccine = await _appContext.Vaccines.FirstOrDefaultAsync(v => v.Name == invalidName);
            Assert.Null(savedVaccine);
        }

        [Fact]
        public async Task Handle_ShouldHandleCancellationToken()
        {
            // Arrange
            var command = new AddVaccineCommand("Poliomielite");
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.Equal(Unit.Value, result);
            var savedVaccine = await _appContext.Vaccines.FirstOrDefaultAsync(v => v.Name == "Poliomielite");
            Assert.NotNull(savedVaccine);
        }

        [Fact]
        public async Task Handle_ShouldAddMultipleVaccines_WhenDifferentNames()
        {
            // Arrange
            var command1 = new AddVaccineCommand("COVID-19");
            var command2 = new AddVaccineCommand("Influenza");

            // Act
            var result1 = await _handler.Handle(command1, CancellationToken.None);
            var result2 = await _handler.Handle(command2, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result1);
            Assert.Equal(Unit.Value, result2);
            
            var vaccineCount = await _appContext.Vaccines.CountAsync();
            Assert.Equal(2, vaccineCount);
            
            var covidVaccine = await _appContext.Vaccines.FirstOrDefaultAsync(v => v.Name == "COVID-19");
            var influenzaVaccine = await _appContext.Vaccines.FirstOrDefaultAsync(v => v.Name == "Influenza");
            
            Assert.NotNull(covidVaccine);
            Assert.NotNull(influenzaVaccine);
        }

        [Fact]
        public async Task Handle_ShouldNotAddNotifications_WhenCommandIsValid()
        {
            // Arrange
            var command = new AddVaccineCommand("COVID-19");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            Assert.False(_notificationContext.HasNotifications);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task Handle_ShouldAddValidationNotifications_WhenNameIsInvalid(string invalidName)
        {
            // Arrange
            var command = new AddVaccineCommand(invalidName);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            Assert.True(_notificationContext.HasNotifications);
            Assert.Contains(_notificationContext.Notifications, n => 
                n.Message.Contains("Para registrar a vacina informe um nome"));
        }

        [Fact]
        public async Task Handle_ShouldAddCorrectNotificationKey_WhenVaccineAlreadyExists()
        {
            // Arrange
            var existingVaccine = new Vaccine { Id = Guid.NewGuid(), Name = "Influenza" };
            await _appContext.Vaccines.AddAsync(existingVaccine);
            await _appContext.SaveChangesAsync();

            var command = new AddVaccineCommand("Influenza");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            var notification = _notificationContext.Notifications.FirstOrDefault(n => n.Key == "VaccineAlreadyExist");
            Assert.NotNull(notification);
            Assert.Contains("Influenza", notification.Message);
        }

        [Fact]
        public async Task Handle_ShouldAddCorrectNotificationMessage_WhenVaccineAlreadyExists()
        {
            // Arrange
            var existingVaccine = new Vaccine { Id = Guid.NewGuid(), Name = "Hepatite B" };
            await _appContext.Vaccines.AddAsync(existingVaccine);
            await _appContext.SaveChangesAsync();

            var command = new AddVaccineCommand("Hepatite B");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            var notification = _notificationContext.Notifications.FirstOrDefault(n => n.Key == "VaccineAlreadyExist");
            Assert.NotNull(notification);
            Assert.Equal("Vacine with name Hepatite B already exist", notification.Message);
        }
    }
} 