using ControleDespesasCasa.Api.Dtos.Transaction;
using ControleDespesasCasa.Api.Enums;
using ControleDespesasCasa.Api.Interfaces.Repositories;
using ControleDespesasCasa.Api.Interfaces.Services;
using ControleDespesasCasa.Api.Models;

namespace ControleDespesasCasa.Api.Services;

// Serviço que contém regras de negócio para criação e consulta de
// transações financeiras. Valida consistência entre pessoa, categoria e tipo
// antes de persistir uma nova transação.
public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPersonRepository _personRepository;
    private readonly ICategoryRepository _categoryRepository;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IPersonRepository personRepository,
        ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _personRepository = personRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<TransactionResponseDto>> GetAllAsync()
    {
        var transactions = await _transactionRepository.GetAllAsync();

        // Mapeia entidades de transação para DTOs de resposta incluindo
        // informações das entidades relacionadas (categoria e pessoa).
        return transactions.Select(t => new TransactionResponseDto
        {
            Id = t.Id,
            Description = t.Description,
            Amount = t.Amount,
            Type = t.Type,
            CategoryId = t.CategoryId,
            CategoryDescription = t.Category?.Description ?? string.Empty,
            PersonId = t.PersonId,
            PersonName = t.Person?.Name ?? string.Empty
        }).ToList();
    }

    public async Task<(bool Success, string Message, TransactionResponseDto? Data)> CreateAsync(CreateTransactionDto dto)
    {
        var person = await _personRepository.GetByIdAsync(dto.PersonId);
        if (person is null)
            return (false, "Pessoa não encontrada.", null);

        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
        if (category is null)
            return (false, "Categoria não encontrada.", null);

        // Regra: menores de idade não podem registrar transações do tipo
        // receita (Income). Retorna falha se violada.
        if (person.Age < 18 && dto.Type == TransactionType.Income)
            return (false, "Menores de idade só podem possuir transações do tipo despesa.", null);

        // Valida compatibilidade entre o propósito da categoria e o tipo de
        // transação (Expense/Income/Both).
        var categoryIsValid =
            category.Purpose == CategoryPurpose.Both ||
            (dto.Type == TransactionType.Expense && category.Purpose == CategoryPurpose.Expense) ||
            (dto.Type == TransactionType.Income && category.Purpose == CategoryPurpose.Income);

        if (!categoryIsValid)
            return (false, "A categoria informada não é compatível com o tipo da transação.", null);

        var transaction = new FinancialTransaction
        {
            Description = dto.Description,
            Amount = dto.Amount,
            Type = dto.Type,
            CategoryId = dto.CategoryId,
            PersonId = dto.PersonId
        };

        await _transactionRepository.CreateAsync(transaction);

        return (true, "Transação criada com sucesso.", new TransactionResponseDto
        {
            Id = transaction.Id,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Type = transaction.Type,
            CategoryId = category.Id,
            CategoryDescription = category.Description,
            PersonId = person.Id,
            PersonName = person.Name
        });
    }
}