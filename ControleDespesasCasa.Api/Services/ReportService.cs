using ControleDespesasCasa.Api.Data;
using ControleDespesasCasa.Api.Dtos.Report;
using ControleDespesasCasa.Api.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleDespesasCasa.Api.Services;

public class ReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PersonTotalsResponseDto> GetPersonTotalsAsync()
    {
        var people = await _context.People
            .Include(p => p.Transactions)
            .ToListAsync();

        var result = new PersonTotalsResponseDto();

        foreach (var person in people)
        {
            var totalIncome = person.Transactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);

            var totalExpense = person.Transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            result.People.Add(new PersonTotalDto
            {
                PersonId = person.Id,
                Name = person.Name,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense
            });

            result.TotalIncome += totalIncome;
            result.TotalExpense += totalExpense;
        }

        return result;
    }
}