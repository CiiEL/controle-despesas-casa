using ControleDespesasCasa.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesasCasa.Api.Dtos.Transaction
{
    // DTO usado para criar uma nova transação financeira. Contém validações
    // para campos obrigatórios e limites (ex.: valor positivo).
    public class CreateTransactionDto
    {
        [Required]
        [MaxLength(400)]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int PersonId { get; set; }   

    }
}
