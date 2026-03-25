using ControleDespesasCasa.Api.Dtos.Person;
using ControleDespesasCasa.Api.Interfaces.Repositories;
using ControleDespesasCasa.Api.Interfaces.Services;
using ControleDespesasCasa.Api.Models;

namespace ControleDespesasCasa.Api.Services;

// Serviço que contém a lógica de negócio para operações com pessoas
// (criar, atualizar, excluir, consultar). Faz a conversão entre DTOs e
// entidades e delega persistência ao repositório.
public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<PersonResponseDto> CreateAsync(CreatePersonDto personDto)
    {
        var person = new Person
        {
            Name = personDto.Name,
            Age = personDto.Age
        };

        await _personRepository.CreateAsync(person);

        return new PersonResponseDto
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person is null)
            return false;

        await _personRepository.DeleteAsync(person);
        return true;
    }

    public async Task<List<PersonResponseDto>> GetAllAsync()
    {
        var people = await _personRepository.GetAllAsync();

        // Mapeia entidades de domínio para DTOs de resposta.
        return people.Select(p => new PersonResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age
        }).ToList();
    }

    public async Task<PersonResponseDto?> GetByIdAsync(int id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person is null)
            return null;

        return new PersonResponseDto
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdatePersonDto personDto)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person is null)
            return false;

        person.Name = personDto.Name;
        person.Age = personDto.Age;

        await _personRepository.UpdateAsync(person);
        return true;
    }
}