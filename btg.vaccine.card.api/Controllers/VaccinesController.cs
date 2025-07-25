using btg.cartao.vacina.domain.Command;
using btg.vaccine.card.api.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace btg.vaccine.card.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinesController(IMediator mediator, ILogger logger) : ControllerBase
    {



        [HttpPost]
        public async Task<IActionResult>CreateVaccine([FromBody] VaccineRequest request)
        {
            try
            {
                var command = new AddVaccineCommand(request.Name);

                await mediator.Send(command);

                return Created();
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(),ex,"Erro ao adicionar vacina");
                return StatusCode(500, ex.Message);
            }
        } 
    }
}
