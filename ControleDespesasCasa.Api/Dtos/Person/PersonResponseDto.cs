using System.ComponentModel.DataAnnotations;


namespace ControleDespesasCasa.Api.Dtos.Person;

public class PersonResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}
