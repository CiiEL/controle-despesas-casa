using ControleDespesasCasa.Api.Dtos.Category;
using ControleDespesasCasa.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesasCasa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryResponseDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> Create(CreateCategoryDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }
}