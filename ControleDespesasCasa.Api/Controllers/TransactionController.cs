using ControleDespesasCasa.Api.Dtos.Transaction;
using ControleDespesasCasa.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleDespesasCasa.Api.Controllers;

// Controller responsável por endpoints relacionados a transações financeiras.
// Recebe DTOs de criação e expõe operações de listagem e criação de transações,
// delegando a lógica ao `ITransactionService`.
[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TransactionResponseDto>>> GetAll()
    {
        // Obtém todas as transações via serviço e retorna 200 OK com a lista.
        var result = await _transactionService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionResponseDto>> Create(CreateTransactionDto dto)
    {
        // Cria uma nova transação. O serviço retorna um resultado que contém
        // sucesso/erro; em caso de falha devolve 400 Bad Request com mensagem.
        var result = await _transactionService.CreateAsync(dto);

        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(result.Data);
    }
}