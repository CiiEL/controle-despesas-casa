using ControleDespesasCasa.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesasCasa.Api.Models;

// Entidade de domínio que representa uma categoria de transação.
// Contém informações sobre propósito (receita, despesa ou ambos) e
// a lista de transações associadas.
public class Category
{ 
    // Chave primária
    public int Id { get; set; }

    // Descrição da categoria
    [Required]
    [MaxLength(400)]
    public string Description { get; set; } = string.Empty;

    // Propósito da categoria (Income, Expense ou Both)
    [Required]
    public CategoryPurpose Purpose { get; set; }

    // Navegação para transações relacionadas
    public List<FinancialTransaction> Transactions { get; set; } = new();
}
