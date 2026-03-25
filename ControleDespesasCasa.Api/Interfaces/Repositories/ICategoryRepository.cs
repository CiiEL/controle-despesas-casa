using ControleDespesasCasa.Api.Models;

namespace ControleDespesasCasa.Api.Interfaces.Repositories;

// Interface que define operações de persistência para Category. Implementada
// por um repositório que encapsula acesso ao DbContext.
public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category> CreateAsync(Category category);
}
