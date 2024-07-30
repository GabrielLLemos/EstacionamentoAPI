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
        public async Task<ActionResult<Ticket>> Post
            (
                [FromBody] decimal? price,
                [FromServices] IEstacionamentoService estacionamentoService
            )
        {
            var checkIn = await estacionamentoService.CheckIn(price);
            return Ok(checkIn);
        }

        //Refatorar
        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> GetTickets
            (
                [FromServices] IEstacionamentoRepository estacionamentoRepository
            )
        {
            var tickets = await estacionamentoRepository.ListData();

            return Ok(tickets);
        }

        //Refatorar
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetId
            (
                Guid id,
                [FromServices] IEstacionamentoRepository estacionamentoRepository
            )
        {
            var ticket = await estacionamentoRepository.GetById(id);
            
            return Ok(ticket);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Exit
            (
                Guid id,
                [FromServices] IEstacionamentoService estacionamentoService
            )
        {
            var ticketCheckOut = await estacionamentoService.CheckOut(id);

            return Ok(ticketCheckOut);
        }

        //Refatorar
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete
            (
                Guid id,
                [FromServices] IEstacionamentoRepository estacionamentoRepository
            )
        {
            await estacionamentoRepository.Delete(id);

            return NoContent();
        }
    }
}
