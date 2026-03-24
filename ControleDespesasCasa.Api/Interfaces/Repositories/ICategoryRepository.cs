using ControleDespesasCasa.Api.Models;

namespace ControleDespesasCasa.Api.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category> CreateAsync(Category category);
}