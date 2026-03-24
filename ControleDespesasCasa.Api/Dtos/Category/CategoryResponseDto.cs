using ControleDespesasCasa.Api.Enums;

namespace ControleDespesasCasa.Api.Dtos.Category;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public  string Description { get; set; }  = string.Empty;

    public CategoryPurpose Purpose { get; set; }
}
