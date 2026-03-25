using System.ComponentModel.DataAnnotations;

namespace ControleDespesasCasa.Api.Dtos.Person
{
    // DTO usado para criação de uma nova pessoa. Inclui validações para
    // obrigatoriedade e limites de tamanho/valor.
    public class CreatePersonDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Range(0,150)]
        public int Age { get; set; }    
    }
}

