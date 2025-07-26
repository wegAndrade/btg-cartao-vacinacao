using btg.cartao.vacina.domain.Command;
using btg.cartao.vacina.domain.Entities;

namespace btg.vaccine.card.application.Adapters
{
    public static class VaccineAdapter
    {
        public static Vaccine ToModel(this AddVaccineCommand command)
        {
            if (command == null )
                return null;

            var model = new Vaccine()
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
            };

            return model;
        }
    }
}
