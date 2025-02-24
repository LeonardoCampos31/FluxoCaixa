using FluxoCaixa.Lancamentos.Modules.DataTransfers;
using FluxoCaixa.Lancamentos.Modules.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Lancamentos.Controllers
{
    [ApiController]
    [Route("api/lancamentos")]
    public class LancamentoController(ILancamentoService service) : ControllerBase
    {
        private readonly ILancamentoService _service = service;

        [HttpPost]
        public async Task<IActionResult> AdicionarLancamento([FromBody] LancamentoRequest request)
        {
            await _service.AdicionarLancamentoAsync(request.Valor, request.Tipo);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ObterLancamentosPorData(DateTime data)
        {
            var lancamentos = await _service.ObterLancamentosPorDataAsync(data);
            return Ok(lancamentos);
        }
    }
}
