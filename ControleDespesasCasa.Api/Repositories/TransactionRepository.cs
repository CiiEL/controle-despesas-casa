using ControleDespesasCasa.Api.Data;
using ControleDespesasCasa.Api.Interfaces.Repositories;
using ControleDespesasCasa.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDespesasCasa.Api.Repositories;

// Repositório responsável por operações relacionadas à entidade `FinancialTransaction`.
// Realiza consultas e persistência de transações financeiras, incluindo carregamento
// das entidades relacionadas (Person e Category) quando necessário.
public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FinancialTransaction>> GetAllAsync()
    {
        // Retorna todas as transações com as entidades relacionadas (Person e
        // Category) carregadas. Usa AsNoTracking para otimizar consultas de
        // leitura quando não há intenção de alterar as entidades retornadas.
        return await _context.Transactions
            .Include(t => t.Person)
            .Include(t => t.Category)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<FinancialTransaction> CreateAsync(FinancialTransaction transaction)
    {
        // Adiciona nova transação e persiste na base.
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}