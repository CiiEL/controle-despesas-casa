using ControleDespesasCasa.Api.Enums;

namespace ControleDespesasCasa.Api.Dtos.Transaction
{
    // DTO de resposta para transações, incluindo dados relacionados (categoria
    // e pessoa) para facilitar consumo pela camada de apresentação.
    public class TransactionResponseDto
    {
        public  int  Id { get; set; }
        public  string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }

        public int CategoryId { get; set; } 
        public string CategoryDescription { get; set; } = string.Empty;

        public int PersonId { get; set; }   
        public string PersonName { get; set; } = string.Empty;
    }
}
