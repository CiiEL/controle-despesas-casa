using System.ComponentModel.DataAnnotations;

namespace ControleDespesasCasa.Api.Dtos.Person
{
    public class CreatePersonDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Range(0,150)]
        public int Age { get; set; }    
    }
}

