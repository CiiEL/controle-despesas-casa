using ControleDespesasCasa.Api.Dtos.Category;
using ControleDespesasCasa.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesasCasa.Api.Controllers;

// Controller responsável por expor endpoints REST relacionados a `Category`.
// Recebe requisições HTTP, delega a lógica para o `ICategoryService` e retorna
// respostas HTTP apropriadas.
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
        // Obtém todas as categorias via serviço e retorna 200 OK com a lista.
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> Create(CreateCategoryDto dto)
    {
        // Cria uma nova categoria delegando a lógica ao serviço e retorna o
        // objeto criado (200 OK). Em cenários REST estritos, poderia retornar
        // 201 Created com a localização do recurso.
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }
}