using ControleDespesasCasa.Api.Dtos.Report;
using ControleDespesasCasa.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesasCasa.Api.Controllers;

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
        var result = await _service.GetPersonTotalsAsync();
        return Ok(result);
    }
}