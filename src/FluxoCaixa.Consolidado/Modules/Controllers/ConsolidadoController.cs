using FluxoCaixa.Consolidado.Modules.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Consolidado.Modules.Controllers
{
    [ApiController]
    [Route("api/consolidado")]
    public class ConsolidadoController : ControllerBase
    {
        private readonly ConsolidadoService _service;

        public ConsolidadoController(ConsolidadoService service)
        {
            _service = service;
        }

        [HttpGet("{data}")]
        public async Task<IActionResult> ObterConsolidadoPorData(DateTime data)
        {
            var consolidado = await _service.ObterConsolidadoPorDataAsync(data);
            return Ok(consolidado);
        }
    }
}