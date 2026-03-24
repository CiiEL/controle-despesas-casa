using ControleDespesasCasa.Api.Data;
using ControleDespesasCasa.Api.Interfaces.Repositories;
using ControleDespesasCasa.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDespesasCasa.Api.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FinancialTransaction>> GetAllAsync()
    {
        return await _context.Transactions
            .Include(t => t.Person)
            .Include(t => t.Category)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<FinancialTransaction> CreateAsync(FinancialTransaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}