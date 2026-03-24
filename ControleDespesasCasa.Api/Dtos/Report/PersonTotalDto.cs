namespace ControleDespesasCasa.Api.Dtos.Report;

public class PersonTotalDto
{
    public  int  PersonId { get; set; }
    public  string Name { get; set; } = string.Empty;
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Balance => TotalIncome - TotalExpense;
}

