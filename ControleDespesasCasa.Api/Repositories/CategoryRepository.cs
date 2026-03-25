using ControleDespesasCasa.Api.Data;
using ControleDespesasCasa.Api.Interfaces.Repositories;
using ControleDespesasCasa.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDespesasCasa.Api.Repositories;

// Repositório responsável por operações CRUD relacionadas à entidade `Category`.
// Encapsula o acesso ao `AppDbContext` e fornece métodos assíncronos usados pelos
// serviços para persistência e consulta de categorias.
public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        // Adiciona a nova categoria ao contexto e persiste as alterações na base.
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        // Retorna todas as categorias como leitura somente (AsNoTracking) para
        // evitar overhead de rastreamento de entidades quando não será feita
        // alteração nelas.
        return await _context.Categories
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        // Busca uma categoria por id em modo somente leitura.
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}