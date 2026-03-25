using ControleDespesasCasa.Api.Dtos.Report;
using ControleDespesasCasa.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesasCasa.Api.Controllers;

// Controller que expõe endpoints relacionados a relatórios agregados.
// Usa diretamente o `ReportService` para obter dados pré-processados.
[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly ReportService _service;

    public ReportsController(ReportService service)
    {
        _service = service;
    }

    [HttpGet("person-totals")]
    public async Task<ActionResult<PersonTotalsResponseDto>> GetPersonTotals()
    {
        // Retorna totais agregados por pessoa (ex.: soma de transações).
        var result = await _service.GetPersonTotalsAsync();
        return Ok(result);
    }
}