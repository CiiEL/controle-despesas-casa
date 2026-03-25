using ControleDespesasCasa.Api.Models;

namespace ControleDespesasCasa.Api.Interfaces.Repositories;

// Interface que define operações de persistência para FinancialTransaction.
public interface ITransactionRepository
{
    Task<List<FinancialTransaction>> GetAllAsync();
    Task<FinancialTransaction> CreateAsync (FinancialTransaction transaction);
}
