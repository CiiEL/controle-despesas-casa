using ControleDespesasCasa.Api.Dtos.Category;
using ControleDespesasCasa.Api.Interfaces.Repositories;
using ControleDespesasCasa.Api.Interfaces.Services;
using ControleDespesasCasa.Api.Models;

namespace ControleDespesasCasa.Api.Services;

// Serviço que orquestra operações de negócio relacionadas a categorias.
// Serve de camada entre controllers e repositório, transformando entidades em DTOs
// e aplicando qualquer regra de negócio necessária.
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        // Converte entidades em DTOs de resposta para retornar ao controller.
        return categories.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            Description = c.Description,
            Purpose = c.Purpose
        }).ToList();
    }

    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Description = dto.Description,
            Purpose = dto.Purpose
        };

        await _repository.CreateAsync(category);

        return new CategoryResponseDto
        {
            Id = category.Id,
            Description = category.Description,
            Purpose = category.Purpose
        };
    }
}