using ControleDespesasCasa.Api.Dtos.Person;

namespace ControleDespesasCasa.Api.Interfaces.Services;

public interface IPersonService
{
    Task<List<PersonResponseDto>> GetAllAsync();
    Task<PersonResponseDto?> GetByIdAsync(int id);
    Task<PersonResponseDto> CreateAsync(CreatePersonDto personDto);
    Task<bool> UpdateAsync(int id, UpdatePersonDto personDto);
    Task<bool> DeleteAsync(int id);
}