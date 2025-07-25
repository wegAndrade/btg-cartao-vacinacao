using btg.vaccine.card.application.Commands;
using MediatR;

namespace btg.cartao.vacina.domain.Command
{
    public class AddVaccineCommand(string name) : CommandBase,IRequest<Unit>
    {

        public string Name { get; private set; } = name;
    }
}
