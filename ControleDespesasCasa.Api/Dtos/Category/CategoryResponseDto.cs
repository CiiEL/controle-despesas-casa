using ControleDespesasCasa.Api.Enums;

namespace ControleDespesasCasa.Api.Dtos.Category;

// DTO de resposta para Category. Usado para retornar dados de categoria
// através da API sem expor diretamente a entidade de domínio.
public class CategoryResponseDto
{
    public int Id { get; set; }
    public  string Description { get; set; }  = string.Empty;

    public CategoryPurpose Purpose { get; set; }
}
