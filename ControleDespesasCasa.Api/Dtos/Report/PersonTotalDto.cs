namespace ControleDespesasCasa.Api.Dtos.Report;

// DTO que representa totais financeiros (receitas, despesas) agregados por
// pessoa. Utilizado em relatórios retornados pelo ReportService.
public class PersonTotalDto
{
    public  int  PersonId { get; set; }
    public  string Name { get; set; } = string.Empty;
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Balance => TotalIncome - TotalExpense;
}

