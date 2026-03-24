using ControleDespesasCasa.Api.Models;

namespace ControleDespesasCasa.Api.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<List<FinancialTransaction>> GetAllAsync();
    Task<FinancialTransaction> CreateAsync (FinancialTransaction transaction);
}
