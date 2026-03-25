using System.ComponentModel.DataAnnotations;
using ControleDespesasCasa.Api.Enums;

namespace ControleDespesasCasa.Api.Models;

// Entidade que representa uma transação financeira.
// Contém campos básicos como descrição, valor, tipo e chaves estrangeiras
// para Category e Person, além das propriedades de navegação.
public class FinancialTransaction
{
    // Chave primária
    public int Id { get; set; }

    [Required]
    [MaxLength(400)]
    public string Description { get; set; } = string.Empty;

    // Valor da transação (deve ser positivo)
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    // Tipo: Income ou Expense
    [Required]
    public TransactionType Type { get; set; }

    // Relacionamento com Category
    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    // Relacionamento com Person
    [Required]
    public int PersonId { get; set; }
    public Person? Person { get; set; }
}
