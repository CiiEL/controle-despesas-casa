using ControleDespesasCasa.Api.Dtos.Transaction;

namespace ControleDespesasCasa.Api.Interfaces.Services;

public interface ITransactionService
{
    Task<List<TransactionResponseDto>> GetAllAsync();
    Task<(bool Success, string Message, TransactionResponseDto? Data)> CreateAsync(CreateTransactionDto dto);
}