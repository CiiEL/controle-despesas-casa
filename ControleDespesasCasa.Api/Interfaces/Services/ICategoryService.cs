using ControleDespesasCasa.Api.Dtos.Category;

namespace ControleDespesasCasa.Api.Interfaces.Services;

public interface ICategoryService
{
    Task<List<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);
}