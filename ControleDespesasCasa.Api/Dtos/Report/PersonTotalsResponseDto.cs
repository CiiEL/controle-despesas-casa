namespace ControleDespesasCasa.Api.Dtos.Report;

// DTO de resposta do relatório agregando totais por pessoa e totais gerais.
// Contém lista de PersonTotalDto e agregados gerais de receita/despesa.
public class PersonTotalsResponseDto
{
    public List<PersonTotalDto> People { get; set; } = new();

    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }

    public decimal Balance => TotalIncome - TotalExpense;
}
