using System.ComponentModel.DataAnnotations;


namespace ControleDespesasCasa.Api.Dtos.Person;

// DTO de resposta para Person. Usado para retornar dados de pessoa através
// da API sem expor a entidade de domínio diretamente.
public class PersonResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}
