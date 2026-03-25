using System.ComponentModel.DataAnnotations;
using ControleDespesasCasa.Api.Enums;

namespace ControleDespesasCasa.Api.Dtos.Category;

// DTO usado ao criar uma nova Category via API. Contém validações de data
// annotations para garantir integridade dos dados recebidos.
public class CreateCategoryDto
{
    [Required]
    [MaxLength(400)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public CategoryPurpose Purpose { get; set; }
}

