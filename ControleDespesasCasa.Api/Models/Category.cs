using ControleDespesasCasa.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesasCasa.Api.Models;

public class Category
{ public int Id { get; set; }
    [Required]
    [MaxLength(400)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public CategoryPurpose Purpose { get; set; }

    public List<FinancialTransaction> Transactions { get; set; } = new();
}
