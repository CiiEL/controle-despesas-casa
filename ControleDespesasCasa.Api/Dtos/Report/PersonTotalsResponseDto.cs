namespace ControleDespesasCasa.Api.Dtos.Report;

public class PersonTotalsResponseDto
{
    public List<PersonTotalDto> People { get; set; } = new();

    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }

    public decimal Balance => TotalIncome - TotalExpense;
}
