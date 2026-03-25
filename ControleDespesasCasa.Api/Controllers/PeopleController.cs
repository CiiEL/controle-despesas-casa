using ControleDespesasCasa.Api.Dtos.Person;
using ControleDespesasCasa.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesasCasa.Api.Controllers;

// Controller responsável por endpoints relacionados a `Person` (pessoas).
// Valida requisições HTTP, chama `IPersonService` para operações de negócio
// e traduz resultados em respostas HTTP (status codes e payloads).
[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonResponseDto>>> GetAll()
    {
        // Retorna todas as pessoas.
        var people = await _personService.GetAllAsync();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonResponseDto>> GetById(int id)
    {
        // Busca uma pessoa por id. Se não encontrada, retorna 404 Not Found.
        var person = await _personService.GetByIdAsync(id);

        if (person is null)
            return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<PersonResponseDto>> Create(CreatePersonDto dto)
    {
        // Cria uma nova pessoa e retorna 201 Created apontando para o endpoint
        // que recupera a pessoa criada.
        var person = await _personService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePersonDto dto)
    {
        // Atualiza uma pessoa existente. Se o id não for encontrado, retorna
        // 404 Not Found. Caso contrário retorna 204 No Content.
        var updated = await _personService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Remove uma pessoa pelo id. Retorna 404 se não encontrado, 204 se
        // exclusão bem-sucedida.
        var deleted = await _personService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}