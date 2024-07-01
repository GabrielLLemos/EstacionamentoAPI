using EstacionamentoAPI.Proprieties;
using EstacionamentoAPI.Repositories;
using EstacionamentoAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace EstacionamentoAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EstacionamentoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Carro>> GetList([FromBody] IEstacionamentoRepository carroRepository)
        {
            var carros = carroRepository.GetCarros().ToList();

            return Ok(carros);
        }

        [HttpGet]
        public ActionResult<Carro> GetCarro(
            Guid id,
            [FromServices] IEstacionamentoRepository estacionamentoRepository
            )
        {
            var carro = estacionamentoRepository;

            if (carro == null)
                return NotFound();
            
            return Ok(carro);
        }

        [HttpPost]
        public ActionResult<Carro> PostCarro(
            [FromBody] CarroRequest request,
            [FromServices] IEstacionamentoRepository estacionamentoRepository
            )
        {
            var carro = new Carro(request.tipo);

            estacionamentoRepository.EntradaCarro(carro.TipoDeVeiculo);

            return Ok(carro);
        }

        [HttpPut]
        public ActionResult Put(
            Guid id,
            string tipo,
            [FromServices] IEstacionamentoRepository estacionamentoRepository
            )
        {
            estacionamentoRepository.AlterarDados(id, tipo);

            return NoContent();
        }
    }
}
