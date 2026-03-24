using ControleDespesasCasa.Api.Dtos.Person;
using ControleDespesasCasa.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesasCasa.Api.Controllers;

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
        var people = await _personService.GetAllAsync();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonResponseDto>> GetById(int id)
    {
        var person = await _personService.GetByIdAsync(id);

        if (person is null)
            return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<PersonResponseDto>> Create(CreatePersonDto dto)
    {
        var person = await _personService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePersonDto dto)
    {
        var updated = await _personService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _personService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}