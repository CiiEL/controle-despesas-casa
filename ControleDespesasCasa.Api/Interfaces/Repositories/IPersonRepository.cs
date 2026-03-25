using ControleDespesasCasa.Api.Models;

namespace ControleDespesasCasa.Api.Interfaces.Repositories;

// Interface que define operações de persistência para Person.
public interface IPersonRepository
{
    Task<List<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task<Person> CreateAsync(Person person);
    Task UpdateAsync(Person person);
    Task DeleteAsync(Person person);
}
