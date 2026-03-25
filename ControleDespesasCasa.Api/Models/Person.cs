using System.ComponentModel.DataAnnotations;

namespace ControleDespesasCasa.Api.Models;

// Entidade de domínio que representa uma pessoa. Contém nome, idade e as
// transações financeiras associadas a essa pessoa.
public class Person
{
    // Chave primária
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Range(0,150)]
    public int Age { get; set; }

    // Navegação para as transações associadas a esta pessoa
    public List<FinancialTransaction> Transactions { get; set; } = new();

}
