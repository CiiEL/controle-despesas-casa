using System.ComponentModel.DataAnnotations;
using ControleDespesasCasa.Api.Enums;

namespace ControleDespesasCasa.Api.Dtos.Category;

public class CreateCategoryDto
{
    [Required]
    [MaxLength(400)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public CategoryPurpose Purpose { get; set; }
}

