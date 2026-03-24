using System.ComponentModel.DataAnnotations;
using ControleDespesasCasa.Api.Enums;

namespace ControleDespesasCasa.Api.Models;

public class FinancialTransaction
{
    public int Id { get; set; }

    [Required]
    [MaxLength(400)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Required]
    public int PersonId { get; set; }
    public Person? Person { get; set; }
}