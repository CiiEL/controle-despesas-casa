using ControleDespesasCasa.Api.Data;
using ControleDespesasCasa.Api.Interfaces.Repositories;
using ControleDespesasCasa.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDespesasCasa.Api.Repositories;

// Repositório responsável por operações CRUD relacionadas à entidade `Person`.
// Fornece métodos assíncronos para consulta e manipulação de pessoas na base.
public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Person>> GetAllAsync()
    {
        // Retorna todas as pessoas como leitura somente (AsNoTracking) para evitar
        // overhead de rastreamento quando não haverá alterações nas entidades.
        return await _context.People
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        // Busca uma pessoa pelo id. Aqui não usamos AsNoTracking porque métodos
        // que atualizam/excluem podem precisar de entidades rastreadas.
        return await _context.People
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Person> CreateAsync(Person person)
    {
        // Adiciona nova pessoa e persiste na base de dados.
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task UpdateAsync(Person person)
    {
        // Atualiza uma pessoa existente no contexto e persiste as alterações.
        _context.People.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person person)
    {
        // Remove a pessoa do contexto e persiste a exclusão.
        _context.People.Remove(person);
        await _context.SaveChangesAsync();
    }
}