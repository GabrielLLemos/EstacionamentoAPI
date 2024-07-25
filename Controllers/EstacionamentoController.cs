using EstacionamentoAPI.Entities;
using EstacionamentoAPI.Repositories;
using EstacionamentoAPI.Requests;
using EstacionamentoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EstacionamentoController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Ticket> Post
            (
                decimal? price,
                [FromServices] IEstacionamentoService estacionamentoService
            )
        {
            var checkIn = estacionamentoService.CheckIn(price);
            return Ok(checkIn);
        }

        //Refatorar
        [HttpGet]
        public ActionResult<List<Ticket>> GetTickets
            (
                [FromServices] IEstacionamentoRepository estacionamentoRepository
            )
        {
            var tickets = estacionamentoRepository.ListData();

            return Ok(tickets);
        }

        //Refatorar
        [HttpGet("{id}")]
        public ActionResult<Ticket> GetId
            (
                [FromBody] Guid id,
                [FromServices] IEstacionamentoRepository estacionamentoRepository
            )
        {
            var ticket = estacionamentoRepository.GetById(id);
            
            return Ok(ticket);
        }

        [HttpPut("{id}")]
        public ActionResult Exit
            (
                [FromBody] Guid id,
                [FromServices] IEstacionamentoService estacionamentoService
            )
        {
            var ticketCheckOut = estacionamentoService.CheckOut(id);

            return Ok(ticketCheckOut);
        }

        //Refatorar
        [HttpDelete("{id}")]
        public ActionResult Delete
            (
                [FromBody] Guid id,
                [FromServices] IEstacionamentoRepository estacionamentoRepository
            )
        {
            estacionamentoRepository.Delete(id);

            return NoContent();
        }
    }
}
