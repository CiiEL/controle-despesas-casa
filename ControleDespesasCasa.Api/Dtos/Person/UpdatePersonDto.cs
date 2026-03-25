using System.ComponentModel.DataAnnotations;

namespace ControleDespesasCasa.Api.Dtos.Person;

// DTO usado para atualizar dados de uma pessoa. Inclui validações para
// garantir que os dados obrigatórios estejam presentes e dentro dos limites.
public class UpdatePersonDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Range(0,150)]
    public int Age { get; set; }    
}
