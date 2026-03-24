using System.ComponentModel.DataAnnotations;

namespace ControleDespesasCasa.Api.Models;

public class Person
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Range(0,150)]
    public int Age { get; set; }

    public List<FinancialTransaction> Transactions { get; set; }

}
