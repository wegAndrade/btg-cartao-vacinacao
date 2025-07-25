using btg.cartao.vacina.domain.Command;
using btg.cartao.vacina.infra.Context;
using btg.vaccine.card.application.Adapters;
using btg.vaccine.card.application.Validators;
using btg.vaccine.card.domain.Notifications;
using MediatR;

namespace btg.cartao.vacina.domain.Handlers
{
    public class AddVacinneCommandHandler(AppDbContext appContext, NotificationContext notificationContext) : IRequestHandler<AddVaccineCommand, Unit>
    {
        private readonly AppDbContext _appContext = appContext;
        private readonly NotificationContext _notificationContext = notificationContext;
       public async Task<Unit> Handle(AddVaccineCommand request, CancellationToken cancellationToken)
        {

            request.Validate(request, new AddVaccineCommandValidator());
            if (request.Invalid)
            {
                _notificationContext.AddNotifications(request.ValidationResult);
                return Unit.Value;
            }

            if (_appContext.Vaccines.Any(v => v.Name == request.Name))
            {

                _notificationContext.AddNotification(new Notification("VaccineAlreadyExist", $"Vacine with name {request.Name} already exist"));
                return Unit.Value;
            }

            var model = request.ToModel();

            _appContext.Vaccines.Add(model);
            await _appContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
